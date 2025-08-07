using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Token: 0x02000ABB RID: 2747
[AddComponentMenu("KMonoBehaviour/scripts/Timelapser")]
public class Timelapser : KMonoBehaviour
{
	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x06004FAE RID: 20398 RVA: 0x001CD1EC File Offset: 0x001CB3EC
	public bool CapturingTimelapseScreenshot
	{
		get
		{
			return this.screenshotActive;
		}
	}

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x06004FAF RID: 20399 RVA: 0x001CD1F4 File Offset: 0x001CB3F4
	// (set) Token: 0x06004FB0 RID: 20400 RVA: 0x001CD1FC File Offset: 0x001CB3FC
	public Texture2D freezeTexture { get; private set; }

	// Token: 0x06004FB1 RID: 20401 RVA: 0x001CD208 File Offset: 0x001CB408
	protected override void OnPrefabInit()
	{
		this.RefreshRenderTextureSize(null);
		Game.Instance.Subscribe(75424175, new Action<object>(this.RefreshRenderTextureSize));
		this.freezeCamera = CameraController.Instance.timelapseFreezeCamera;
		if (this.CycleTimeToScreenshot() > 0f)
		{
			this.OnNewDay(null);
		}
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDay));
		this.OnResize();
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		base.StartCoroutine(this.Render());
	}

	// Token: 0x06004FB2 RID: 20402 RVA: 0x001CD2B1 File Offset: 0x001CB4B1
	private void OnResize()
	{
		if (this.freezeTexture != null)
		{
			global::UnityEngine.Object.Destroy(this.freezeTexture);
		}
		this.freezeTexture = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight, TextureFormat.ARGB32, false);
	}

	// Token: 0x06004FB3 RID: 20403 RVA: 0x001CD2F0 File Offset: 0x001CB4F0
	private void RefreshRenderTextureSize(object data = null)
	{
		if (this.previewScreenshot)
		{
			if (this.bufferRenderTexture != null)
			{
				this.bufferRenderTexture.DestroyRenderTexture();
			}
			this.bufferRenderTexture = new RenderTexture(this.previewScreenshotResolution.x, this.previewScreenshotResolution.y, 32, RenderTextureFormat.ARGB32);
			this.bufferRenderTexture.name = "Timelapser.PreviewScreenshot";
			return;
		}
		if (this.timelapseUserEnabled)
		{
			if (this.bufferRenderTexture != null)
			{
				this.bufferRenderTexture.DestroyRenderTexture();
			}
			this.bufferRenderTexture = new RenderTexture(SaveGame.Instance.TimelapseResolution.x, SaveGame.Instance.TimelapseResolution.y, 32, RenderTextureFormat.ARGB32);
			this.bufferRenderTexture.name = "Timelapser.Timelapse";
		}
	}

	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x06004FB4 RID: 20404 RVA: 0x001CD3B0 File Offset: 0x001CB5B0
	private bool timelapseUserEnabled
	{
		get
		{
			return SaveGame.Instance.TimelapseResolution.x > 0;
		}
	}

	// Token: 0x06004FB5 RID: 20405 RVA: 0x001CD3C4 File Offset: 0x001CB5C4
	private void OnNewDay(object data = null)
	{
		if (this.worldsToScreenshot.Count == 0)
		{
			DebugUtil.LogArgs(new object[] { "Timelapse.OnNewDay but worldsToScreenshot is not empty" });
		}
		int cycle = GameClock.Instance.GetCycle();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.IsDiscovered && !worldContainer.IsModuleInterior)
			{
				if (worldContainer.DiscoveryTimestamp + (float)cycle > (float)this.timelapseScreenshotCycles[this.timelapseScreenshotCycles.Length - 1])
				{
					if (worldContainer.DiscoveryTimestamp + (float)(cycle % 10) == 0f)
					{
						this.screenshotToday = true;
						this.worldsToScreenshot.Add(worldContainer.id);
					}
				}
				else
				{
					for (int i = 0; i < this.timelapseScreenshotCycles.Length; i++)
					{
						if ((int)worldContainer.DiscoveryTimestamp + cycle == this.timelapseScreenshotCycles[i])
						{
							this.screenshotToday = true;
							this.worldsToScreenshot.Add(worldContainer.id);
						}
					}
				}
			}
		}
	}

	// Token: 0x06004FB6 RID: 20406 RVA: 0x001CD4E4 File Offset: 0x001CB6E4
	private void Update()
	{
		if (this.screenshotToday)
		{
			if (this.CycleTimeToScreenshot() <= 0f || GameClock.Instance.GetCycle() == 0)
			{
				if (!this.timelapseUserEnabled)
				{
					this.screenshotToday = false;
					this.worldsToScreenshot.Clear();
					return;
				}
				if (!PlayerController.Instance.CanDrag())
				{
					CameraController.Instance.ForcePanningState(false);
					this.screenshotToday = false;
					this.SaveScreenshot();
					return;
				}
			}
		}
		else
		{
			this.screenshotToday = !this.screenshotPending && this.worldsToScreenshot.Count > 0;
		}
	}

	// Token: 0x06004FB7 RID: 20407 RVA: 0x001CD571 File Offset: 0x001CB771
	private float CycleTimeToScreenshot()
	{
		return 300f - GameClock.Instance.GetTime() % 600f;
	}

	// Token: 0x06004FB8 RID: 20408 RVA: 0x001CD589 File Offset: 0x001CB789
	private IEnumerator Render()
	{
		for (;;)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
			if (this.screenshotPending)
			{
				int num = (this.previewScreenshot ? ClusterManager.Instance.GetStartWorld().id : this.worldsToScreenshot[0]);
				if (!this.freezeCamera.enabled)
				{
					this.freezeTexture.ReadPixels(new Rect(0f, 0f, (float)Camera.main.pixelWidth, (float)Camera.main.pixelHeight), 0, 0);
					this.freezeTexture.Apply();
					this.freezeCamera.gameObject.GetComponent<FillRenderTargetEffect>().SetFillTexture(this.freezeTexture);
					this.freezeCamera.enabled = true;
					this.screenshotActive = true;
					this.RefreshRenderTextureSize(null);
					DebugHandler.SetTimelapseMode(true, num);
					this.SetPostionAndOrtho(num);
					this.activeOverlay = OverlayScreen.Instance.mode;
					OverlayScreen.Instance.ToggleOverlay(OverlayModes.None.ID, false);
				}
				else
				{
					this.RenderAndPrint(num);
					if (!this.previewScreenshot)
					{
						this.worldsToScreenshot.Remove(num);
					}
					this.freezeCamera.enabled = false;
					DebugHandler.SetTimelapseMode(false, 0);
					this.screenshotPending = false;
					this.previewScreenshot = false;
					this.screenshotActive = false;
					this.debugScreenShot = false;
					this.previewSaveGamePath = "";
					OverlayScreen.Instance.ToggleOverlay(this.activeOverlay, false);
				}
			}
		}
		yield break;
	}

	// Token: 0x06004FB9 RID: 20409 RVA: 0x001CD598 File Offset: 0x001CB798
	public void InitialScreenshot()
	{
		this.worldsToScreenshot.Add(ClusterManager.Instance.GetStartWorld().id);
		this.SaveScreenshot();
	}

	// Token: 0x06004FBA RID: 20410 RVA: 0x001CD5BA File Offset: 0x001CB7BA
	private void SaveScreenshot()
	{
		this.screenshotPending = true;
	}

	// Token: 0x06004FBB RID: 20411 RVA: 0x001CD5C3 File Offset: 0x001CB7C3
	public void SaveColonyPreview(string saveFileName)
	{
		this.previewSaveGamePath = saveFileName;
		this.previewScreenshot = true;
		this.SaveScreenshot();
	}

	// Token: 0x06004FBC RID: 20412 RVA: 0x001CD5DC File Offset: 0x001CB7DC
	private void SetPostionAndOrtho(int world_id)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(world_id);
		if (world == null)
		{
			return;
		}
		float num = 0f;
		Camera overlayCamera = CameraController.Instance.overlayCamera;
		this.camSize = overlayCamera.orthographicSize;
		this.camPosition = CameraController.Instance.transform.position;
		if (!world.IsStartWorld)
		{
			CameraController.Instance.OrthographicSize = (float)(world.WorldSize.y / 2);
			CameraController.Instance.SetPosition(new Vector3((float)(world.WorldOffset.x + world.WorldSize.x / 2), (float)(world.WorldOffset.y + world.WorldSize.y / 2), CameraController.Instance.transform.position.z));
			return;
		}
		GameObject telepad = GameUtil.GetTelepad(world_id);
		if (telepad == null)
		{
			return;
		}
		Vector3 position = telepad.transform.GetPosition();
		foreach (BuildingComplete buildingComplete in Components.BuildingCompletes.Items)
		{
			Vector3 position2 = buildingComplete.transform.GetPosition();
			float num2 = (float)this.bufferRenderTexture.width / (float)this.bufferRenderTexture.height;
			Vector3 vector = position - position2;
			num = Mathf.Max(new float[]
			{
				num,
				vector.x / num2,
				vector.y
			});
		}
		num += 10f;
		num = Mathf.Max(num, 18f);
		CameraController.Instance.OrthographicSize = num;
		CameraController.Instance.SetPosition(new Vector3(telepad.transform.position.x, telepad.transform.position.y, CameraController.Instance.transform.position.z));
	}

	// Token: 0x06004FBD RID: 20413 RVA: 0x001CD7C8 File Offset: 0x001CB9C8
	private void RenderAndPrint(int world_id)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(world_id);
		if (world == null)
		{
			return;
		}
		if (world.IsStartWorld)
		{
			GameObject telepad = GameUtil.GetTelepad(world.id);
			if (telepad == null)
			{
				global::Debug.Log("No telepad present, aborting screenshot.");
				return;
			}
			Vector3 position = telepad.transform.position;
			position.z = CameraController.Instance.transform.position.z;
			CameraController.Instance.SetPosition(position);
		}
		else
		{
			CameraController.Instance.SetPosition(new Vector3((float)(world.WorldOffset.x + world.WorldSize.x / 2), (float)(world.WorldOffset.y + world.WorldSize.y / 2), CameraController.Instance.transform.position.z));
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = this.bufferRenderTexture;
		CameraController.Instance.RenderForTimelapser(ref this.bufferRenderTexture);
		this.WriteToPng(this.bufferRenderTexture, world_id);
		CameraController.Instance.OrthographicSize = this.camSize;
		CameraController.Instance.SetPosition(this.camPosition);
		RenderTexture.active = active;
	}

	// Token: 0x06004FBE RID: 20414 RVA: 0x001CD8F0 File Offset: 0x001CBAF0
	public void WriteToPng(RenderTexture renderTex, int world_id = -1)
	{
		Texture2D texture2D = new Texture2D(renderTex.width, renderTex.height, TextureFormat.ARGB32, false);
		texture2D.ReadPixels(new Rect(0f, 0f, (float)renderTex.width, (float)renderTex.height), 0, 0);
		texture2D.Apply();
		byte[] array = texture2D.EncodeToPNG();
		global::UnityEngine.Object.Destroy(texture2D);
		if (!Directory.Exists(Util.RootFolder()))
		{
			Directory.CreateDirectory(Util.RootFolder());
		}
		string text = Path.Combine(Util.RootFolder(), Util.GetRetiredColoniesFolderName());
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string text2 = RetireColonyUtility.StripInvalidCharacters(SaveGame.Instance.BaseName);
		if (!this.previewScreenshot)
		{
			string text3 = Path.Combine(text, text2);
			if (!Directory.Exists(text3))
			{
				Directory.CreateDirectory(text3);
			}
			string text4 = text3;
			if (world_id >= 0)
			{
				string name = ClusterManager.Instance.GetWorld(world_id).GetComponent<ClusterGridEntity>().Name;
				text4 = Path.Combine(text4, world_id.ToString("D5"));
				if (!Directory.Exists(text4))
				{
					Directory.CreateDirectory(text4);
				}
				text4 = Path.Combine(text4, name);
			}
			else
			{
				text4 = Path.Combine(text4, text2);
			}
			DebugUtil.LogArgs(new object[] { "Saving screenshot to", text4 });
			string text5 = "0000.##";
			text4 = text4 + "_cycle_" + GameClock.Instance.GetCycle().ToString(text5);
			if (this.debugScreenShot)
			{
				text4 = string.Concat(new string[]
				{
					text4,
					"_",
					global::System.DateTime.Now.Day.ToString(),
					"-",
					global::System.DateTime.Now.Month.ToString(),
					"_",
					global::System.DateTime.Now.Hour.ToString(),
					"-",
					global::System.DateTime.Now.Minute.ToString(),
					"-",
					global::System.DateTime.Now.Second.ToString()
				});
			}
			File.WriteAllBytes(text4 + ".png", array);
			return;
		}
		string text6 = this.previewSaveGamePath;
		text6 = Path.ChangeExtension(text6, ".png");
		DebugUtil.LogArgs(new object[] { "Saving screenshot to", text6 });
		File.WriteAllBytes(text6, array);
	}

	// Token: 0x040035A5 RID: 13733
	private bool screenshotActive;

	// Token: 0x040035A6 RID: 13734
	private bool screenshotPending;

	// Token: 0x040035A7 RID: 13735
	private bool previewScreenshot;

	// Token: 0x040035A8 RID: 13736
	private string previewSaveGamePath = "";

	// Token: 0x040035A9 RID: 13737
	private bool screenshotToday;

	// Token: 0x040035AA RID: 13738
	private List<int> worldsToScreenshot = new List<int>();

	// Token: 0x040035AB RID: 13739
	private HashedString activeOverlay;

	// Token: 0x040035AC RID: 13740
	private Camera freezeCamera;

	// Token: 0x040035AD RID: 13741
	private RenderTexture bufferRenderTexture;

	// Token: 0x040035AF RID: 13743
	private Vector3 camPosition;

	// Token: 0x040035B0 RID: 13744
	private float camSize;

	// Token: 0x040035B1 RID: 13745
	private bool debugScreenShot;

	// Token: 0x040035B2 RID: 13746
	private Vector2Int previewScreenshotResolution = new Vector2Int(Grid.WidthInCells * 2, Grid.HeightInCells * 2);

	// Token: 0x040035B3 RID: 13747
	private const int DEFAULT_SCREENSHOT_INTERVAL = 10;

	// Token: 0x040035B4 RID: 13748
	private int[] timelapseScreenshotCycles = new int[]
	{
		1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
		11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
		21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
		31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
		41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
		55, 60, 65, 70, 75, 80, 85, 90, 95, 100,
		110, 120, 130, 140, 150, 160, 170, 180, 190, 200,
		210, 220, 230, 240, 250, 260, 270, 280, 290, 200,
		310, 320, 330, 340, 350, 360, 370, 380, 390, 400,
		410, 420, 430, 440, 450, 460, 470, 480, 490, 500
	};
}
