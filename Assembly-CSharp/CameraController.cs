using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

// Token: 0x0200057A RID: 1402
[AddComponentMenu("KMonoBehaviour/scripts/CameraController")]
public class CameraController : KMonoBehaviour, IInputHandler
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x06001F6B RID: 8043 RVA: 0x000B3E48 File Offset: 0x000B2048
	public string handlerName
	{
		get
		{
			return base.gameObject.name;
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06001F6C RID: 8044 RVA: 0x000B3E55 File Offset: 0x000B2055
	// (set) Token: 0x06001F6D RID: 8045 RVA: 0x000B3E78 File Offset: 0x000B2078
	public float OrthographicSize
	{
		get
		{
			if (!(this.baseCamera == null))
			{
				return this.baseCamera.orthographicSize;
			}
			return 0f;
		}
		set
		{
			for (int i = 0; i < this.cameras.Count; i++)
			{
				this.cameras[i].orthographicSize = value;
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06001F6E RID: 8046 RVA: 0x000B3EAD File Offset: 0x000B20AD
	// (set) Token: 0x06001F6F RID: 8047 RVA: 0x000B3EB5 File Offset: 0x000B20B5
	public KInputHandler inputHandler { get; set; }

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06001F70 RID: 8048 RVA: 0x000B3EBE File Offset: 0x000B20BE
	// (set) Token: 0x06001F71 RID: 8049 RVA: 0x000B3EC6 File Offset: 0x000B20C6
	public float targetOrthographicSize { get; private set; }

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06001F72 RID: 8050 RVA: 0x000B3ECF File Offset: 0x000B20CF
	// (set) Token: 0x06001F73 RID: 8051 RVA: 0x000B3ED7 File Offset: 0x000B20D7
	public bool isTargetPosSet { get; set; }

	// Token: 0x1700012E RID: 302
	// (get) Token: 0x06001F74 RID: 8052 RVA: 0x000B3EE0 File Offset: 0x000B20E0
	// (set) Token: 0x06001F75 RID: 8053 RVA: 0x000B3EE8 File Offset: 0x000B20E8
	public Vector3 targetPos { get; private set; }

	// Token: 0x1700012F RID: 303
	// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000B3EF1 File Offset: 0x000B20F1
	// (set) Token: 0x06001F77 RID: 8055 RVA: 0x000B3EF9 File Offset: 0x000B20F9
	public bool ignoreClusterFX { get; private set; }

	// Token: 0x06001F78 RID: 8056 RVA: 0x000B3F02 File Offset: 0x000B2102
	public void ToggleClusterFX()
	{
		this.ignoreClusterFX = !this.ignoreClusterFX;
	}

	// Token: 0x06001F79 RID: 8057 RVA: 0x000B3F14 File Offset: 0x000B2114
	protected override void OnForcedCleanUp()
	{
		GameInputManager inputManager = Global.GetInputManager();
		if (inputManager == null)
		{
			return;
		}
		inputManager.usedMenus.Remove(this);
	}

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06001F7A RID: 8058 RVA: 0x000B3F38 File Offset: 0x000B2138
	public int cameraActiveCluster
	{
		get
		{
			if (ClusterManager.Instance == null)
			{
				return 255;
			}
			return ClusterManager.Instance.activeWorldId;
		}
	}

	// Token: 0x06001F7B RID: 8059 RVA: 0x000B3F58 File Offset: 0x000B2158
	public void GetWorldCamera(out Vector2I worldOffset, out Vector2I worldSize)
	{
		WorldContainer worldContainer = null;
		if (ClusterManager.Instance != null)
		{
			worldContainer = ClusterManager.Instance.activeWorld;
		}
		if (!this.ignoreClusterFX && worldContainer != null)
		{
			worldOffset = worldContainer.WorldOffset;
			worldSize = worldContainer.WorldSize;
			return;
		}
		worldOffset = new Vector2I(0, 0);
		worldSize = new Vector2I(Grid.WidthInCells, Grid.HeightInCells);
	}

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06001F7C RID: 8060 RVA: 0x000B3FCB File Offset: 0x000B21CB
	// (set) Token: 0x06001F7D RID: 8061 RVA: 0x000B3FD3 File Offset: 0x000B21D3
	public bool DisableUserCameraControl
	{
		get
		{
			return this.userCameraControlDisabled;
		}
		set
		{
			this.userCameraControlDisabled = value;
			if (this.userCameraControlDisabled)
			{
				this.panning = false;
				this.panLeft = false;
				this.panRight = false;
				this.panUp = false;
				this.panDown = false;
			}
		}
	}

	// Token: 0x17000132 RID: 306
	// (get) Token: 0x06001F7E RID: 8062 RVA: 0x000B4007 File Offset: 0x000B2207
	// (set) Token: 0x06001F7F RID: 8063 RVA: 0x000B400E File Offset: 0x000B220E
	public static CameraController Instance { get; private set; }

	// Token: 0x06001F80 RID: 8064 RVA: 0x000B4016 File Offset: 0x000B2216
	public static void DestroyInstance()
	{
		CameraController.Instance = null;
	}

	// Token: 0x06001F81 RID: 8065 RVA: 0x000B401E File Offset: 0x000B221E
	public void ToggleColouredOverlayView(bool enabled)
	{
		this.mrt.ToggleColouredOverlayView(enabled);
	}

	// Token: 0x06001F82 RID: 8066 RVA: 0x000B402C File Offset: 0x000B222C
	public void EnableKAnimPostProcessingEffect(KAnimConverter.PostProcessingEffects effect)
	{
		this.kAnimPostProcessingEffects.EnableEffect(effect);
	}

	// Token: 0x06001F83 RID: 8067 RVA: 0x000B403A File Offset: 0x000B223A
	public void DisableKAnimPostProcessingEffect(KAnimConverter.PostProcessingEffects effect)
	{
		this.kAnimPostProcessingEffects.DisableEffect(effect);
	}

	// Token: 0x06001F84 RID: 8068 RVA: 0x000B4048 File Offset: 0x000B2248
	public void RegisterCustomScreenPostProcessingEffect(Func<RenderTexture, Material> effectFn)
	{
		this.customActiveScreenPostProcessingEffects.RegisterEffect(effectFn);
	}

	// Token: 0x06001F85 RID: 8069 RVA: 0x000B4056 File Offset: 0x000B2256
	public void UnregisterCustomScreenPostProcessingEffect(Func<RenderTexture, Material> effectFn)
	{
		this.customActiveScreenPostProcessingEffects.UnregisterEffect(effectFn);
	}

	// Token: 0x06001F86 RID: 8070 RVA: 0x000B4064 File Offset: 0x000B2264
	protected override void OnPrefabInit()
	{
		global::Util.Reset(base.transform);
		base.transform.SetLocalPosition(new Vector3(Grid.WidthInMeters / 2f, Grid.HeightInMeters / 2f, -100f));
		this.targetOrthographicSize = this.maxOrthographicSize;
		CameraController.Instance = this;
		this.DisableUserCameraControl = false;
		this.baseCamera = this.CopyCamera(Camera.main, "baseCamera");
		this.mrt = this.baseCamera.gameObject.AddComponent<MultipleRenderTarget>();
		this.mrt.onSetupComplete += this.OnMRTSetupComplete;
		this.baseCamera.gameObject.AddComponent<LightBufferCompositor>();
		this.baseCamera.transparencySortMode = TransparencySortMode.Orthographic;
		this.baseCamera.transform.parent = base.transform;
		global::Util.Reset(this.baseCamera.transform);
		int mask = LayerMask.GetMask(new string[] { "PlaceWithDepth", "Overlay" });
		int mask2 = LayerMask.GetMask(new string[] { "Construction" });
		this.baseCamera.cullingMask &= ~mask;
		this.baseCamera.cullingMask |= mask2;
		this.baseCamera.tag = "Untagged";
		this.baseCamera.gameObject.AddComponent<CameraRenderTexture>().TextureName = "_LitTex";
		this.infraredCamera = this.CopyCamera(this.baseCamera, "Infrared");
		this.infraredCamera.cullingMask = 0;
		this.infraredCamera.clearFlags = CameraClearFlags.Color;
		this.infraredCamera.depth = this.baseCamera.depth - 1f;
		this.infraredCamera.transform.parent = base.transform;
		this.infraredCamera.gameObject.AddComponent<Infrared>();
		if (SimDebugView.Instance != null)
		{
			this.simOverlayCamera = this.CopyCamera(this.baseCamera, "SimOverlayCamera");
			this.simOverlayCamera.cullingMask = LayerMask.GetMask(new string[] { "SimDebugView" });
			this.simOverlayCamera.clearFlags = CameraClearFlags.Color;
			this.simOverlayCamera.depth = this.baseCamera.depth + 1f;
			this.simOverlayCamera.transform.parent = base.transform;
			this.simOverlayCamera.gameObject.AddComponent<CameraRenderTexture>().TextureName = "_SimDebugViewTex";
		}
		this.overlayCamera = Camera.main;
		this.overlayCamera.name = "Overlay";
		this.overlayCamera.cullingMask = mask | mask2;
		this.overlayCamera.clearFlags = CameraClearFlags.Nothing;
		this.overlayCamera.transform.parent = base.transform;
		this.overlayCamera.depth = this.baseCamera.depth + 3f;
		this.overlayCamera.transform.SetLocalPosition(Vector3.zero);
		this.overlayCamera.transform.localRotation = Quaternion.identity;
		this.overlayCamera.renderingPath = RenderingPath.Forward;
		this.overlayCamera.allowHDR = false;
		this.overlayCamera.tag = "Untagged";
		this.kAnimPostProcessingEffects = this.overlayCamera.GetComponent<KAnimActivePostProcessingEffects>();
		this.customActiveScreenPostProcessingEffects = this.overlayCamera.GetComponent<CustomActiveScreenPostProcessingEffects>();
		this.overlayCamera.gameObject.AddComponent<CameraReferenceTexture>().referenceCamera = this.baseCamera;
		ColorCorrectionLookup component = this.overlayCamera.GetComponent<ColorCorrectionLookup>();
		component.Convert(this.dayColourCube, "");
		component.Convert2(this.nightColourCube, "");
		this.cameras.Add(this.overlayCamera);
		this.lightBufferCamera = this.CopyCamera(this.overlayCamera, "Light Buffer");
		this.lightBufferCamera.clearFlags = CameraClearFlags.Color;
		this.lightBufferCamera.cullingMask = LayerMask.GetMask(new string[] { "Lights" });
		this.lightBufferCamera.depth = this.baseCamera.depth - 1f;
		this.lightBufferCamera.transform.parent = base.transform;
		this.lightBufferCamera.transform.SetLocalPosition(Vector3.zero);
		this.lightBufferCamera.rect = new Rect(0f, 0f, 1f, 1f);
		LightBuffer lightBuffer = this.lightBufferCamera.gameObject.AddComponent<LightBuffer>();
		lightBuffer.Material = this.LightBufferMaterial;
		lightBuffer.CircleMaterial = this.LightCircleOverlay;
		lightBuffer.ConeMaterial = this.LightConeOverlay;
		this.overlayNoDepthCamera = this.CopyCamera(this.overlayCamera, "overlayNoDepth");
		int mask3 = LayerMask.GetMask(new string[] { "Overlay", "Place" });
		this.baseCamera.cullingMask &= ~mask3;
		this.overlayNoDepthCamera.clearFlags = CameraClearFlags.Depth;
		this.overlayNoDepthCamera.cullingMask = mask3;
		this.overlayNoDepthCamera.transform.parent = base.transform;
		this.overlayNoDepthCamera.transform.SetLocalPosition(Vector3.zero);
		this.overlayNoDepthCamera.depth = this.baseCamera.depth + 4f;
		this.overlayNoDepthCamera.tag = "MainCamera";
		this.overlayNoDepthCamera.gameObject.AddComponent<NavPathDrawer>();
		this.overlayNoDepthCamera.gameObject.AddComponent<RangeVisualizerEffect>();
		this.overlayNoDepthCamera.gameObject.AddComponent<SkyVisibilityVisualizerEffect>();
		this.overlayNoDepthCamera.gameObject.AddComponent<ScannerNetworkVisualizerEffect>();
		this.overlayNoDepthCamera.gameObject.AddComponent<RocketLaunchConditionVisualizerEffect>();
		if (DlcManager.IsContentSubscribed("DLC4_ID"))
		{
			this.overlayNoDepthCamera.gameObject.AddComponent<LargeImpactorVisualizerEffect>();
		}
		this.uiCamera = this.CopyCamera(this.overlayCamera, "uiCamera");
		this.uiCamera.clearFlags = CameraClearFlags.Depth;
		this.uiCamera.cullingMask = LayerMask.GetMask(new string[] { "UI" });
		this.uiCamera.transform.parent = base.transform;
		this.uiCamera.transform.SetLocalPosition(Vector3.zero);
		this.uiCamera.depth = this.baseCamera.depth + 5f;
		if (Game.Instance != null)
		{
			this.timelapseFreezeCamera = this.CopyCamera(this.uiCamera, "timelapseFreezeCamera");
			this.timelapseFreezeCamera.depth = this.uiCamera.depth + 3f;
			this.timelapseFreezeCamera.gameObject.AddComponent<FillRenderTargetEffect>();
			this.timelapseFreezeCamera.enabled = false;
			Camera camera = CameraController.CloneCamera(this.overlayCamera, "timelapseCamera");
			Timelapser timelapser = camera.gameObject.AddComponent<Timelapser>();
			camera.transparencySortMode = TransparencySortMode.Orthographic;
			camera.depth = this.baseCamera.depth + 2f;
			Game.Instance.timelapser = timelapser;
		}
		if (GameScreenManager.Instance != null)
		{
			for (int i = 0; i < this.uiCameraTargets.Count; i++)
			{
				GameScreenManager.Instance.SetCamera(this.uiCameraTargets[i], this.uiCamera);
			}
			this.infoText = GameScreenManager.Instance.screenshotModeCanvas.GetComponentInChildren<LocText>();
		}
		if (!KPlayerPrefs.HasKey("CameraSpeed"))
		{
			CameraController.SetDefaultCameraSpeed();
		}
		this.SetSpeedFromPrefs(null);
		Game.Instance.Subscribe(75424175, new Action<object>(this.SetSpeedFromPrefs));
		this.VisibleArea.Update();
	}

	// Token: 0x06001F87 RID: 8071 RVA: 0x000B47D9 File Offset: 0x000B29D9
	private void SetSpeedFromPrefs(object data = null)
	{
		this.keyPanningSpeed = Mathf.Clamp(0.1f, KPlayerPrefs.GetFloat("CameraSpeed"), 2f);
	}

	// Token: 0x06001F88 RID: 8072 RVA: 0x000B47FC File Offset: 0x000B29FC
	public int GetCursorCell()
	{
		Vector3 vector = Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos());
		Vector3 vector2 = Vector3.Max(ClusterManager.Instance.activeWorld.minimumBounds, vector);
		vector2 = Vector3.Min(ClusterManager.Instance.activeWorld.maximumBounds, vector2);
		return Grid.PosToCell(vector2);
	}

	// Token: 0x06001F89 RID: 8073 RVA: 0x000B4855 File Offset: 0x000B2A55
	public static Camera CloneCamera(Camera camera, string name)
	{
		Camera camera2 = new GameObject
		{
			name = name
		}.AddComponent<Camera>();
		camera2.CopyFrom(camera);
		return camera2;
	}

	// Token: 0x06001F8A RID: 8074 RVA: 0x000B4870 File Offset: 0x000B2A70
	private Camera CopyCamera(Camera camera, string name)
	{
		Camera camera2 = CameraController.CloneCamera(camera, name);
		this.cameras.Add(camera2);
		return camera2;
	}

	// Token: 0x06001F8B RID: 8075 RVA: 0x000B4892 File Offset: 0x000B2A92
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Restore();
	}

	// Token: 0x06001F8C RID: 8076 RVA: 0x000B48A0 File Offset: 0x000B2AA0
	public static void SetDefaultCameraSpeed()
	{
		KPlayerPrefs.SetFloat("CameraSpeed", 1f);
	}

	// Token: 0x17000133 RID: 307
	// (get) Token: 0x06001F8D RID: 8077 RVA: 0x000B48B1 File Offset: 0x000B2AB1
	// (set) Token: 0x06001F8E RID: 8078 RVA: 0x000B48B9 File Offset: 0x000B2AB9
	public Coroutine activeFadeRoutine { get; private set; }

	// Token: 0x06001F8F RID: 8079 RVA: 0x000B48C2 File Offset: 0x000B2AC2
	public void FadeOut(float targetPercentage = 1f, float speed = 1f, global::System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithBlack(true, 0f, targetPercentage, speed, callback));
	}

	// Token: 0x06001F90 RID: 8080 RVA: 0x000B48F3 File Offset: 0x000B2AF3
	public void FadeIn(float targetPercentage = 0f, float speed = 1f, global::System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithBlack(true, 1f, targetPercentage, speed, callback));
	}

	// Token: 0x06001F91 RID: 8081 RVA: 0x000B4924 File Offset: 0x000B2B24
	public void FadeOutColor(Color color, float targetPercentage = 1f, float speed = 1f, global::System.Action callback = null)
	{
		this.FadeOutColor(color, 1f, targetPercentage, speed, callback);
	}

	// Token: 0x06001F92 RID: 8082 RVA: 0x000B4938 File Offset: 0x000B2B38
	public void FadeOutColor(Color color, float initialPercentage, float targetPercentage = 1f, float speed = 1f, global::System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithColor(true, initialPercentage, targetPercentage, color, speed, callback));
	}

	// Token: 0x06001F93 RID: 8083 RVA: 0x000B4974 File Offset: 0x000B2B74
	public void FadeInColor(Color color, float targetPercentage = 0f, float speed = 1f, global::System.Action callback = null)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		this.activeFadeRoutine = base.StartCoroutine(this.FadeWithColor(true, 1f, targetPercentage, color, speed, callback));
	}

	// Token: 0x06001F94 RID: 8084 RVA: 0x000B49B4 File Offset: 0x000B2BB4
	public void ActiveWorldStarWipe(int id, global::System.Action callback = null)
	{
		this.ActiveWorldStarWipe(id, false, default(Vector3), 10f, callback);
	}

	// Token: 0x06001F95 RID: 8085 RVA: 0x000B49D8 File Offset: 0x000B2BD8
	public void ActiveWorldStarWipe(int id, Vector3 position, float forceOrthgraphicSize = 10f, global::System.Action callback = null)
	{
		this.ActiveWorldStarWipe(id, true, position, forceOrthgraphicSize, callback);
	}

	// Token: 0x06001F96 RID: 8086 RVA: 0x000B49E8 File Offset: 0x000B2BE8
	private void ActiveWorldStarWipe(int id, bool useForcePosition, Vector3 forcePosition, float forceOrthgraphicSize, global::System.Action callback)
	{
		if (this.activeFadeRoutine != null)
		{
			base.StopCoroutine(this.activeFadeRoutine);
		}
		if (ClusterManager.Instance.activeWorldId != id)
		{
			if (DetailsScreen.Instance != null)
			{
				DetailsScreen.Instance.DeselectAndClose();
			}
			this.activeFadeRoutine = base.StartCoroutine(this.SwapToWorldFade(id, useForcePosition, forcePosition, forceOrthgraphicSize, callback));
			return;
		}
		ManagementMenu.Instance.CloseAll();
		if (useForcePosition)
		{
			CameraController.Instance.SetTargetPos(forcePosition, 8f, true);
			if (callback != null)
			{
				callback();
			}
		}
	}

	// Token: 0x06001F97 RID: 8087 RVA: 0x000B4A70 File Offset: 0x000B2C70
	private IEnumerator SwapToWorldFade(int worldId, bool useForcePosition, Vector3 forcePosition, float forceOrthgraphicSize, global::System.Action newWorldCallback)
	{
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().ActiveBaseChangeSnapshot);
		ClusterManager.Instance.UpdateWorldReverbSnapshot(worldId);
		yield return base.StartCoroutine(this.FadeWithBlack(false, 0f, 1f, 3f, null));
		ClusterManager.Instance.SetActiveWorld(worldId);
		if (useForcePosition)
		{
			CameraController.Instance.SetTargetPos(forcePosition, forceOrthgraphicSize, false);
			CameraController.Instance.SetPosition(forcePosition);
		}
		if (newWorldCallback != null)
		{
			newWorldCallback();
		}
		ManagementMenu.Instance.CloseAll();
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().ActiveBaseChangeSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		yield return base.StartCoroutine(this.FadeWithBlack(false, 1f, 0f, 3f, null));
		yield break;
	}

	// Token: 0x06001F98 RID: 8088 RVA: 0x000B4AA4 File Offset: 0x000B2CA4
	public void SetWorldInteractive(bool state)
	{
		GameScreenManager.Instance.fadePlaneFront.raycastTarget = !state;
	}

	// Token: 0x06001F99 RID: 8089 RVA: 0x000B4AB9 File Offset: 0x000B2CB9
	private IEnumerator FadeWithBlack(bool fadeUI, float startBlackPercent, float targetBlackPercent, float speed = 1f, global::System.Action callback = null)
	{
		return this.FadeWithColor(fadeUI, startBlackPercent, targetBlackPercent, Color.black, speed, callback);
	}

	// Token: 0x06001F9A RID: 8090 RVA: 0x000B4ACD File Offset: 0x000B2CCD
	private IEnumerator FadeWithColor(bool fadeUI, float startPercent, float targetPercent, Color color, float speed = 1f, global::System.Action callback = null)
	{
		Image fadePlane = (fadeUI ? GameScreenManager.Instance.fadePlaneFront : GameScreenManager.Instance.fadePlaneBack);
		float percent = 0f;
		while (percent < 1f)
		{
			percent += Time.unscaledDeltaTime * speed;
			float num = MathUtil.ReRange(percent, 0f, 1f, startPercent, targetPercent);
			color.a = num;
			fadePlane.color = color;
			yield return SequenceUtil.WaitForNextFrame;
		}
		color.a = targetPercent;
		fadePlane.color = color;
		if (callback != null)
		{
			callback();
		}
		this.activeFadeRoutine = null;
		yield return SequenceUtil.WaitForNextFrame;
		yield break;
	}

	// Token: 0x06001F9B RID: 8091 RVA: 0x000B4B09 File Offset: 0x000B2D09
	public void EnableFreeCamera(bool enable)
	{
		this.FreeCameraEnabled = enable;
		this.SetInfoText("Screenshot Mode (ESC to exit)");
	}

	// Token: 0x06001F9C RID: 8092 RVA: 0x000B4B20 File Offset: 0x000B2D20
	private static bool WithinInputField()
	{
		global::UnityEngine.EventSystems.EventSystem current = global::UnityEngine.EventSystems.EventSystem.current;
		if (current == null)
		{
			return false;
		}
		bool flag = false;
		if (current.currentSelectedGameObject != null && (current.currentSelectedGameObject.GetComponent<KInputTextField>() != null || current.currentSelectedGameObject.GetComponent<InputField>() != null))
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x17000134 RID: 308
	// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000B4B78 File Offset: 0x000B2D78
	public static bool IsMouseOverGameWindow
	{
		get
		{
			return 0f <= Input.mousePosition.x && 0f <= Input.mousePosition.y && (float)Screen.width >= Input.mousePosition.x && (float)Screen.height >= Input.mousePosition.y;
		}
	}

	// Token: 0x06001F9E RID: 8094 RVA: 0x000B4BD0 File Offset: 0x000B2DD0
	private void SetInfoText(string text)
	{
		this.infoText.text = text;
		Color color = this.infoText.color;
		color.a = 0.5f;
		this.infoText.color = color;
	}

	// Token: 0x06001F9F RID: 8095 RVA: 0x000B4C10 File Offset: 0x000B2E10
	public void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (this.DisableUserCameraControl)
		{
			return;
		}
		if (CameraController.WithinInputField())
		{
			return;
		}
		if (SaveGame.Instance != null && SaveGame.Instance.GetComponent<UserNavigation>().Handle(e))
		{
			return;
		}
		if (!this.ChangeWorldInput(e))
		{
			if (e.TryConsume(global::Action.TogglePause))
			{
				SpeedControlScreen.Instance.TogglePause(false);
			}
			else if (e.TryConsume(global::Action.ZoomIn) && CameraController.IsMouseOverGameWindow)
			{
				float num = this.targetOrthographicSize * (1f / this.zoomFactor);
				this.targetOrthographicSize = Mathf.Max(num, this.minOrthographicSize);
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (e.TryConsume(global::Action.ZoomOut) && CameraController.IsMouseOverGameWindow)
			{
				float num2 = this.targetOrthographicSize * this.zoomFactor;
				this.targetOrthographicSize = Mathf.Min(num2, this.FreeCameraEnabled ? TuningData<CameraController.Tuning>.Get().maxOrthographicSizeDebug : this.maxOrthographicSize);
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (e.TryConsume(global::Action.MouseMiddle) || e.IsAction(global::Action.MouseRight))
			{
				this.panning = true;
				this.overrideZoomSpeed = 0f;
				this.isTargetPosSet = false;
			}
			else if (this.FreeCameraEnabled && e.TryConsume(global::Action.CinemaCamEnable))
			{
				this.cinemaCamEnabled = !this.cinemaCamEnabled;
				DebugUtil.LogArgs(new object[] { "Cinema Cam Enabled ", this.cinemaCamEnabled });
				this.SetInfoText(this.cinemaCamEnabled ? "Cinema Cam Enabled" : "Cinema Cam Disabled");
			}
			else if (this.FreeCameraEnabled && this.cinemaCamEnabled)
			{
				if (e.TryConsume(global::Action.CinemaToggleLock))
				{
					this.cinemaToggleLock = !this.cinemaToggleLock;
					DebugUtil.LogArgs(new object[] { "Cinema Toggle Lock ", this.cinemaToggleLock });
					this.SetInfoText(this.cinemaToggleLock ? "Cinema Input Lock ON" : "Cinema Input Lock OFF");
				}
				else if (e.TryConsume(global::Action.CinemaToggleEasing))
				{
					this.cinemaToggleEasing = !this.cinemaToggleEasing;
					DebugUtil.LogArgs(new object[] { "Cinema Toggle Easing ", this.cinemaToggleEasing });
					this.SetInfoText(this.cinemaToggleEasing ? "Cinema Easing ON" : "Cinema Easing OFF");
				}
				else if (e.TryConsume(global::Action.CinemaUnpauseOnMove))
				{
					this.cinemaUnpauseNextMove = !this.cinemaUnpauseNextMove;
					DebugUtil.LogArgs(new object[] { "Cinema Unpause Next Move ", this.cinemaUnpauseNextMove });
					this.SetInfoText(this.cinemaUnpauseNextMove ? "Cinema Unpause Next Move ON" : "Cinema Unpause Next Move OFF");
				}
				else if (e.TryConsume(global::Action.CinemaPanLeft))
				{
					this.cinemaPanLeft = !this.cinemaToggleLock || !this.cinemaPanLeft;
					this.cinemaPanRight = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanRight))
				{
					this.cinemaPanRight = !this.cinemaToggleLock || !this.cinemaPanRight;
					this.cinemaPanLeft = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanUp))
				{
					this.cinemaPanUp = !this.cinemaToggleLock || !this.cinemaPanUp;
					this.cinemaPanDown = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaPanDown))
				{
					this.cinemaPanDown = !this.cinemaToggleLock || !this.cinemaPanDown;
					this.cinemaPanUp = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomIn))
				{
					this.cinemaZoomIn = !this.cinemaToggleLock || !this.cinemaZoomIn;
					this.cinemaZoomOut = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomOut))
				{
					this.cinemaZoomOut = !this.cinemaToggleLock || !this.cinemaZoomOut;
					this.cinemaZoomIn = false;
					this.CheckMoveUnpause();
				}
				else if (e.TryConsume(global::Action.CinemaZoomSpeedPlus))
				{
					this.cinemaZoomSpeed++;
					DebugUtil.LogArgs(new object[] { "Cinema Zoom Speed ", this.cinemaZoomSpeed });
					this.SetInfoText("Cinema Zoom Speed: " + this.cinemaZoomSpeed.ToString());
				}
				else if (e.TryConsume(global::Action.CinemaZoomSpeedMinus))
				{
					this.cinemaZoomSpeed--;
					DebugUtil.LogArgs(new object[] { "Cinema Zoom Speed ", this.cinemaZoomSpeed });
					this.SetInfoText("Cinema Zoom Speed: " + this.cinemaZoomSpeed.ToString());
				}
			}
			else if (e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = true;
			}
			else if (e.TryConsume(global::Action.PanRight))
			{
				this.panRight = true;
			}
			else if (e.TryConsume(global::Action.PanUp))
			{
				this.panUp = true;
			}
			else if (e.TryConsume(global::Action.PanDown))
			{
				this.panDown = true;
			}
		}
		if (!e.Consumed && OverlayMenu.Instance != null)
		{
			OverlayMenu.Instance.OnKeyDown(e);
		}
	}

	// Token: 0x06001FA0 RID: 8096 RVA: 0x000B5168 File Offset: 0x000B3368
	public bool ChangeWorldInput(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return true;
		}
		int num = -1;
		if (e.TryConsume(global::Action.SwitchActiveWorld1))
		{
			num = 0;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld2))
		{
			num = 1;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld3))
		{
			num = 2;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld4))
		{
			num = 3;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld5))
		{
			num = 4;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld6))
		{
			num = 5;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld7))
		{
			num = 6;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld8))
		{
			num = 7;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld9))
		{
			num = 8;
		}
		else if (e.TryConsume(global::Action.SwitchActiveWorld10))
		{
			num = 9;
		}
		if (num != -1)
		{
			List<int> discoveredAsteroidIDsSorted = ClusterManager.Instance.GetDiscoveredAsteroidIDsSorted();
			if (num < discoveredAsteroidIDsSorted.Count && num >= 0)
			{
				num = discoveredAsteroidIDsSorted[num];
				WorldContainer world = ClusterManager.Instance.GetWorld(num);
				if (world != null && world.IsDiscovered && ClusterManager.Instance.activeWorldId != world.id)
				{
					ManagementMenu.Instance.CloseClusterMap();
					this.ActiveWorldStarWipe(world.id, null);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001FA1 RID: 8097 RVA: 0x000B52A0 File Offset: 0x000B34A0
	public void OnKeyUp(KButtonEvent e)
	{
		if (this.DisableUserCameraControl)
		{
			return;
		}
		if (CameraController.WithinInputField())
		{
			return;
		}
		if (e.TryConsume(global::Action.MouseMiddle) || e.IsAction(global::Action.MouseRight))
		{
			this.panning = false;
			return;
		}
		if (this.FreeCameraEnabled && this.cinemaCamEnabled)
		{
			if (e.TryConsume(global::Action.CinemaPanLeft))
			{
				this.cinemaPanLeft = this.cinemaToggleLock && this.cinemaPanLeft;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanRight))
			{
				this.cinemaPanRight = this.cinemaToggleLock && this.cinemaPanRight;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanUp))
			{
				this.cinemaPanUp = this.cinemaToggleLock && this.cinemaPanUp;
				return;
			}
			if (e.TryConsume(global::Action.CinemaPanDown))
			{
				this.cinemaPanDown = this.cinemaToggleLock && this.cinemaPanDown;
				return;
			}
			if (e.TryConsume(global::Action.CinemaZoomIn))
			{
				this.cinemaZoomIn = this.cinemaToggleLock && this.cinemaZoomIn;
				return;
			}
			if (e.TryConsume(global::Action.CinemaZoomOut))
			{
				this.cinemaZoomOut = this.cinemaToggleLock && this.cinemaZoomOut;
				return;
			}
		}
		else
		{
			if (e.TryConsume(global::Action.CameraHome))
			{
				this.CameraGoHome(2f, true);
				return;
			}
			if (e.TryConsume(global::Action.PanLeft))
			{
				this.panLeft = false;
				return;
			}
			if (e.TryConsume(global::Action.PanRight))
			{
				this.panRight = false;
				return;
			}
			if (e.TryConsume(global::Action.PanUp))
			{
				this.panUp = false;
				return;
			}
			if (e.TryConsume(global::Action.PanDown))
			{
				this.panDown = false;
			}
		}
	}

	// Token: 0x06001FA2 RID: 8098 RVA: 0x000B543C File Offset: 0x000B363C
	public void ForcePanningState(bool state)
	{
		this.panning = false;
	}

	// Token: 0x06001FA3 RID: 8099 RVA: 0x000B5448 File Offset: 0x000B3648
	public void CameraGoHome(float speed = 2f, bool showCameraReturnButton = false)
	{
		GameObject activeTelepad = GameUtil.GetActiveTelepad();
		if (activeTelepad != null && ClusterUtil.ActiveWorldHasPrinter())
		{
			GameUtil.FocusCamera(new Vector3(activeTelepad.transform.GetPosition().x, activeTelepad.transform.GetPosition().y + 1f, base.transform.GetPosition().z), speed, true, showCameraReturnButton);
			this.SetOverrideZoomSpeed(speed);
		}
	}

	// Token: 0x06001FA4 RID: 8100 RVA: 0x000B54B5 File Offset: 0x000B36B5
	public void CameraGoTo(Vector3 pos, float speed = 2f, bool playSound = true)
	{
		pos.z = base.transform.GetPosition().z;
		this.SetTargetPos(pos, 10f, playSound);
		this.SetOverrideZoomSpeed(speed);
	}

	// Token: 0x06001FA5 RID: 8101 RVA: 0x000B54E4 File Offset: 0x000B36E4
	public void SnapTo(Vector3 pos)
	{
		this.ClearFollowTarget();
		pos.z = -100f;
		this.targetPos = Vector3.zero;
		this.isTargetPosSet = false;
		base.transform.SetPosition(pos);
		this.keyPanDelta = Vector3.zero;
		this.OrthographicSize = this.targetOrthographicSize;
	}

	// Token: 0x06001FA6 RID: 8102 RVA: 0x000B5539 File Offset: 0x000B3739
	public void SnapTo(Vector3 pos, float orthographicSize)
	{
		this.targetOrthographicSize = orthographicSize;
		this.SnapTo(pos);
	}

	// Token: 0x06001FA7 RID: 8103 RVA: 0x000B5549 File Offset: 0x000B3749
	public void SetOverrideZoomSpeed(float tempZoomSpeed)
	{
		this.overrideZoomSpeed = tempZoomSpeed;
	}

	// Token: 0x06001FA8 RID: 8104 RVA: 0x000B5554 File Offset: 0x000B3754
	public void SetTargetPos(Vector3 pos, float orthographic_size, bool playSound)
	{
		int num = Grid.PosToCell(pos);
		if (!Grid.IsValidCell(num) || Grid.WorldIdx[num] == 255 || ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]) == null)
		{
			return;
		}
		this.ClearFollowTarget();
		if (playSound && !this.isTargetPosSet)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Click_Notification", false));
		}
		pos.z = -100f;
		if ((int)Grid.WorldIdx[num] != ClusterManager.Instance.activeWorldId)
		{
			this.targetOrthographicSize = 20f;
			this.ActiveWorldStarWipe((int)Grid.WorldIdx[num], pos, 10f, delegate
			{
				this.targetPos = pos;
				this.isTargetPosSet = true;
				this.OrthographicSize = orthographic_size + 5f;
				this.targetOrthographicSize = orthographic_size;
			});
		}
		else
		{
			this.targetPos = pos;
			this.isTargetPosSet = true;
			this.targetOrthographicSize = orthographic_size;
		}
		PlayerController.Instance.CancelDragging();
		this.CheckMoveUnpause();
	}

	// Token: 0x06001FA9 RID: 8105 RVA: 0x000B565C File Offset: 0x000B385C
	public void SetTargetPosForWorldChange(Vector3 pos, float orthographic_size, bool playSound)
	{
		int num = Grid.PosToCell(pos);
		if (!Grid.IsValidCell(num) || Grid.WorldIdx[num] == 255 || ClusterManager.Instance.GetWorld((int)Grid.WorldIdx[num]) == null)
		{
			return;
		}
		this.ClearFollowTarget();
		if (playSound && !this.isTargetPosSet)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Click_Notification", false));
		}
		pos.z = -100f;
		this.targetPos = pos;
		this.isTargetPosSet = true;
		this.targetOrthographicSize = orthographic_size;
		PlayerController.Instance.CancelDragging();
		this.CheckMoveUnpause();
		this.SetPosition(pos);
		this.OrthographicSize = orthographic_size;
	}

	// Token: 0x06001FAA RID: 8106 RVA: 0x000B5700 File Offset: 0x000B3900
	public void SetMaxOrthographicSize(float size)
	{
		this.maxOrthographicSize = size;
	}

	// Token: 0x06001FAB RID: 8107 RVA: 0x000B5709 File Offset: 0x000B3909
	public void SetPosition(Vector3 pos)
	{
		base.transform.SetPosition(pos);
	}

	// Token: 0x06001FAC RID: 8108 RVA: 0x000B5718 File Offset: 0x000B3918
	public IEnumerator DoCinematicZoom(float targetOrthographicSize)
	{
		this.cinemaCamEnabled = true;
		this.FreeCameraEnabled = true;
		this.targetOrthographicSize = targetOrthographicSize;
		while (targetOrthographicSize - this.OrthographicSize >= 0.001f)
		{
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.OrthographicSize = targetOrthographicSize;
		this.FreeCameraEnabled = false;
		this.cinemaCamEnabled = false;
		yield break;
	}

	// Token: 0x06001FAD RID: 8109 RVA: 0x000B5730 File Offset: 0x000B3930
	private Vector3 PointUnderCursor(Vector3 mousePos, Camera cam)
	{
		Ray ray = cam.ScreenPointToRay(mousePos);
		Vector3 direction = ray.direction;
		Vector3 vector = direction * Mathf.Abs(cam.transform.GetPosition().z / direction.z);
		return ray.origin + vector;
	}

	// Token: 0x06001FAE RID: 8110 RVA: 0x000B5780 File Offset: 0x000B3980
	private void CinemaCamUpdate()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		Camera main = Camera.main;
		Vector3 localPosition = base.transform.GetLocalPosition();
		float num = Mathf.Pow((float)this.cinemaZoomSpeed, 3f);
		if (this.cinemaZoomIn)
		{
			this.overrideZoomSpeed = -num / TuningData<CameraController.Tuning>.Get().cinemaZoomFactor;
			this.isTargetPosSet = false;
		}
		else if (this.cinemaZoomOut)
		{
			this.overrideZoomSpeed = num / TuningData<CameraController.Tuning>.Get().cinemaZoomFactor;
			this.isTargetPosSet = false;
		}
		else
		{
			this.overrideZoomSpeed = 0f;
		}
		if (this.cinemaToggleEasing)
		{
			this.cinemaZoomVelocity += (this.overrideZoomSpeed - this.cinemaZoomVelocity) * this.cinemaEasing;
		}
		else
		{
			this.cinemaZoomVelocity = this.overrideZoomSpeed;
		}
		if (this.cinemaZoomVelocity != 0f)
		{
			this.OrthographicSize = main.orthographicSize + this.cinemaZoomVelocity * unscaledDeltaTime * (main.orthographicSize / 20f);
			this.targetOrthographicSize = main.orthographicSize;
		}
		float num2 = num / TuningData<CameraController.Tuning>.Get().cinemaZoomToFactor;
		float num3 = this.keyPanningSpeed / 20f * main.orthographicSize;
		float num4 = num3 * (num / TuningData<CameraController.Tuning>.Get().cinemaPanToFactor);
		if (!this.isTargetPosSet && this.targetOrthographicSize != main.orthographicSize)
		{
			float num5 = Mathf.Min(num2 * unscaledDeltaTime, 0.1f);
			this.OrthographicSize = Mathf.Lerp(main.orthographicSize, this.targetOrthographicSize, num5);
		}
		Vector3 vector = Vector3.zero;
		if (this.isTargetPosSet)
		{
			float num6 = this.cinemaEasing * TuningData<CameraController.Tuning>.Get().targetZoomEasingFactor;
			float num7 = this.cinemaEasing * TuningData<CameraController.Tuning>.Get().targetPanEasingFactor;
			float num8 = this.targetOrthographicSize - main.orthographicSize;
			Vector3 vector2 = this.targetPos - localPosition;
			float num9;
			float num10;
			if (!this.cinemaToggleEasing)
			{
				num9 = num2 * unscaledDeltaTime;
				num10 = num4 * unscaledDeltaTime;
			}
			else
			{
				DebugUtil.LogArgs(new object[]
				{
					"Min zoom of:",
					num2 * unscaledDeltaTime,
					Mathf.Abs(num8) * num6 * unscaledDeltaTime
				});
				num9 = Mathf.Min(num2 * unscaledDeltaTime, Mathf.Abs(num8) * num6 * unscaledDeltaTime);
				DebugUtil.LogArgs(new object[]
				{
					"Min pan of:",
					num4 * unscaledDeltaTime,
					vector2.magnitude * num7 * unscaledDeltaTime
				});
				num10 = Mathf.Min(num4 * unscaledDeltaTime, vector2.magnitude * num7 * unscaledDeltaTime);
			}
			float num11;
			if (Mathf.Abs(num8) < num9)
			{
				num11 = num8;
			}
			else
			{
				num11 = Mathf.Sign(num8) * num9;
			}
			if (vector2.magnitude < num10)
			{
				vector = vector2;
			}
			else
			{
				vector = vector2.normalized * num10;
			}
			if (Mathf.Abs(num11) < 0.001f && vector.magnitude < 0.001f)
			{
				this.isTargetPosSet = false;
				num11 = num8;
				vector = vector2;
			}
			this.OrthographicSize = main.orthographicSize + num11 * (main.orthographicSize / 20f);
		}
		if (!PlayerController.Instance.CanDrag())
		{
			this.panning = false;
		}
		Vector3 vector3 = Vector3.zero;
		if (this.panning)
		{
			vector3 = -PlayerController.Instance.GetWorldDragDelta();
			this.isTargetPosSet = false;
			if (vector3.magnitude > 0f)
			{
				this.ClearFollowTarget();
			}
			this.keyPanDelta = Vector3.zero;
		}
		else
		{
			float num12 = num / TuningData<CameraController.Tuning>.Get().cinemaPanFactor;
			Vector3 zero = Vector3.zero;
			if (this.cinemaPanLeft)
			{
				this.ClearFollowTarget();
				zero.x = -num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanRight)
			{
				this.ClearFollowTarget();
				zero.x = num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanUp)
			{
				this.ClearFollowTarget();
				zero.y = num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaPanDown)
			{
				this.ClearFollowTarget();
				zero.y = -num3 * num12;
				this.isTargetPosSet = false;
			}
			if (this.cinemaToggleEasing)
			{
				this.keyPanDelta += (zero - this.keyPanDelta) * this.cinemaEasing;
			}
			else
			{
				this.keyPanDelta = zero;
			}
		}
		Vector3 vector4 = localPosition + vector + vector3 + this.keyPanDelta * unscaledDeltaTime;
		if (this.followTarget != null)
		{
			vector4.x = this.followTargetPos.x;
			vector4.y = this.followTargetPos.y;
		}
		vector4.z = -100f;
		if ((double)(vector4 - base.transform.GetLocalPosition()).magnitude > 0.001)
		{
			base.transform.SetLocalPosition(vector4);
		}
	}

	// Token: 0x06001FAF RID: 8111 RVA: 0x000B5C48 File Offset: 0x000B3E48
	private void NormalCamUpdate()
	{
		float unscaledDeltaTime = Time.unscaledDeltaTime;
		Camera main = Camera.main;
		this.smoothDt = this.smoothDt * 2f / 3f + unscaledDeltaTime / 3f;
		float num = ((this.overrideZoomSpeed != 0f) ? this.overrideZoomSpeed : this.zoomSpeed);
		Vector3 localPosition = base.transform.GetLocalPosition();
		Vector3 vector = ((this.overrideZoomSpeed != 0f) ? new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 0f) : KInputManager.GetMousePos());
		Vector3 vector2 = this.PointUnderCursor(vector, main);
		Vector3 vector3 = main.ScreenToViewportPoint(vector);
		float num2 = this.keyPanningSpeed / 20f * main.orthographicSize;
		num2 *= Mathf.Min(unscaledDeltaTime / 0.016666666f, 10f);
		float num3 = num * Mathf.Min(this.smoothDt, 0.3f);
		this.OrthographicSize = Mathf.Lerp(main.orthographicSize, this.targetOrthographicSize, num3);
		base.transform.SetLocalPosition(localPosition);
		Vector3 vector4 = main.WorldToViewportPoint(vector2);
		vector3.z = vector4.z;
		Vector3 vector5 = main.ViewportToWorldPoint(vector4) - main.ViewportToWorldPoint(vector3);
		if (this.isTargetPosSet)
		{
			vector5 = Vector3.Lerp(localPosition, this.targetPos, num * this.smoothDt) - localPosition;
			if (vector5.magnitude < 0.001f)
			{
				this.isTargetPosSet = false;
				vector5 = this.targetPos - localPosition;
			}
		}
		if (!PlayerController.Instance.CanDrag())
		{
			this.panning = false;
		}
		Vector3 vector6 = Vector3.zero;
		if (this.panning)
		{
			vector6 = -PlayerController.Instance.GetWorldDragDelta();
			this.isTargetPosSet = false;
		}
		Vector3 vector7 = localPosition + vector5 + vector6;
		if (this.panning)
		{
			if (vector6.magnitude > 0f)
			{
				this.ClearFollowTarget();
			}
			this.keyPanDelta = Vector3.zero;
		}
		else if (!this.DisableUserCameraControl)
		{
			if (this.panLeft)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.x = this.keyPanDelta.x - num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panRight)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.x = this.keyPanDelta.x + num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panUp)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.y = this.keyPanDelta.y + num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (this.panDown)
			{
				this.ClearFollowTarget();
				this.keyPanDelta.y = this.keyPanDelta.y - num2;
				this.isTargetPosSet = false;
				this.overrideZoomSpeed = 0f;
			}
			if (KInputManager.currentControllerIsGamepad)
			{
				Vector2 vector8 = num2 * KInputManager.steamInputInterpreter.GetSteamCameraMovement();
				if (Mathf.Abs(vector8.x) > Mathf.Epsilon || Mathf.Abs(vector8.y) > Mathf.Epsilon)
				{
					this.ClearFollowTarget();
					this.isTargetPosSet = false;
					this.overrideZoomSpeed = 0f;
				}
				this.keyPanDelta += new Vector3(vector8.x, vector8.y, 0f);
			}
			Vector3 vector9 = new Vector3(Mathf.Lerp(0f, this.keyPanDelta.x, this.smoothDt * this.keyPanningEasing), Mathf.Lerp(0f, this.keyPanDelta.y, this.smoothDt * this.keyPanningEasing), 0f);
			this.keyPanDelta -= vector9;
			vector7.x += vector9.x;
			vector7.y += vector9.y;
		}
		if (this.followTarget != null)
		{
			vector7.x = this.followTargetPos.x;
			vector7.y = this.followTargetPos.y;
		}
		vector7.z = -100f;
		if ((double)(vector7 - base.transform.GetLocalPosition()).magnitude > 0.001)
		{
			base.transform.SetLocalPosition(vector7);
		}
	}

	// Token: 0x06001FB0 RID: 8112 RVA: 0x000B6094 File Offset: 0x000B4294
	private void Update()
	{
		if (Game.Instance == null || !Game.Instance.timelapser.CapturingTimelapseScreenshot)
		{
			if (this.FreeCameraEnabled && this.cinemaCamEnabled)
			{
				this.CinemaCamUpdate();
			}
			else
			{
				this.NormalCamUpdate();
			}
		}
		if (this.infoText != null && this.infoText.color.a > 0f)
		{
			Color color = this.infoText.color;
			color.a = Mathf.Max(0f, this.infoText.color.a - Time.unscaledDeltaTime * 0.5f);
			this.infoText.color = color;
		}
		this.ConstrainToWorld();
		Vector3 vector = this.PointUnderCursor(KInputManager.GetMousePos(), Camera.main);
		Shader.SetGlobalVector("_WorldCameraPos", new Vector4(base.transform.GetPosition().x, base.transform.GetPosition().y, base.transform.GetPosition().z, Camera.main.orthographicSize));
		Shader.SetGlobalVector("_WorldCursorPos", new Vector4(vector.x, vector.y, 0f, 0f));
		this.VisibleArea.Update();
		this.soundCuller = SoundCuller.CreateCuller();
	}

	// Token: 0x06001FB1 RID: 8113 RVA: 0x000B61E4 File Offset: 0x000B43E4
	private Vector3 GetFollowPos()
	{
		if (this.followTarget != null)
		{
			Vector3 vector = this.followTarget.transform.GetPosition();
			KAnimControllerBase component = this.followTarget.GetComponent<KAnimControllerBase>();
			if (component != null)
			{
				vector = component.GetWorldPivot();
			}
			return vector;
		}
		return Vector3.zero;
	}

	// Token: 0x06001FB2 RID: 8114 RVA: 0x000B6234 File Offset: 0x000B4434
	public static float GetHighestVisibleCell_Height(byte worldID = 255)
	{
		Vector2 zero = Vector2.zero;
		Vector2 vector = new Vector2(Grid.WidthInMeters, Grid.HeightInMeters);
		Camera main = Camera.main;
		float orthographicSize = main.orthographicSize;
		main.orthographicSize = 20f;
		Ray ray = main.ViewportPointToRay(Vector3.one - Vector3.one * 0.33f);
		Vector3 vector2 = CameraController.Instance.transform.GetPosition() - ray.origin;
		main.orthographicSize = orthographicSize;
		if (ClusterManager.Instance != null)
		{
			WorldContainer worldContainer = ((worldID == byte.MaxValue) ? ClusterManager.Instance.activeWorld : ClusterManager.Instance.GetWorld((int)worldID));
			worldContainer.minimumBounds * Grid.CellSizeInMeters;
			vector = worldContainer.maximumBounds * Grid.CellSizeInMeters;
			new Vector2((float)worldContainer.Width, (float)worldContainer.Height) * Grid.CellSizeInMeters;
		}
		return vector.y * 1.1f + 20f + vector2.y;
	}

	// Token: 0x06001FB3 RID: 8115 RVA: 0x000B633C File Offset: 0x000B453C
	private void ConstrainToWorld()
	{
		if (Game.Instance != null && Game.Instance.IsLoading())
		{
			return;
		}
		if (this.FreeCameraEnabled)
		{
			return;
		}
		Camera main = Camera.main;
		Ray ray = main.ViewportPointToRay(Vector3.zero + Vector3.one * 0.33f);
		Ray ray2 = main.ViewportPointToRay(Vector3.one - Vector3.one * 0.33f);
		float num = Mathf.Abs(ray.origin.z / ray.direction.z);
		float num2 = Mathf.Abs(ray2.origin.z / ray2.direction.z);
		Vector3 point = ray.GetPoint(num);
		Vector3 point2 = ray2.GetPoint(num2);
		Vector2 vector = Vector2.zero;
		Vector2 vector2 = new Vector2(Grid.WidthInMeters, Grid.HeightInMeters);
		Vector2 vector3 = vector2;
		if (ClusterManager.Instance != null)
		{
			WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
			vector = activeWorld.minimumBounds * Grid.CellSizeInMeters;
			vector2 = activeWorld.maximumBounds * Grid.CellSizeInMeters;
			vector3 = new Vector2((float)activeWorld.Width, (float)activeWorld.Height) * Grid.CellSizeInMeters;
		}
		if (point2.x - point.x > vector3.x || point2.y - point.y > vector3.y)
		{
			return;
		}
		Vector3 vector4 = base.transform.GetPosition() - ray.origin;
		Vector3 vector5 = point;
		vector5.x = Mathf.Max(vector.x, vector5.x);
		vector5.y = Mathf.Max(vector.y * Grid.CellSizeInMeters, vector5.y);
		ray.origin = vector5;
		ray.direction = -ray.direction;
		vector5 = ray.GetPoint(num);
		base.transform.SetPosition(vector5 + vector4);
		vector4 = base.transform.GetPosition() - ray2.origin;
		vector5 = point2;
		vector5.x = Mathf.Min(vector2.x, vector5.x);
		vector5.y = Mathf.Min(vector2.y * 1.1f, vector5.y);
		ray2.origin = vector5;
		ray2.direction = -ray2.direction;
		vector5 = ray2.GetPoint(num2);
		Vector3 vector6 = vector5 + vector4;
		vector6.z = -100f;
		base.transform.SetPosition(vector6);
	}

	// Token: 0x06001FB4 RID: 8116 RVA: 0x000B65DC File Offset: 0x000B47DC
	public void Save(BinaryWriter writer)
	{
		writer.Write(base.transform.GetPosition());
		writer.Write(base.transform.localScale);
		writer.Write(base.transform.rotation);
		writer.Write(this.targetOrthographicSize);
		CameraSaveData.position = base.transform.GetPosition();
		CameraSaveData.localScale = base.transform.localScale;
		CameraSaveData.rotation = base.transform.rotation;
	}

	// Token: 0x06001FB5 RID: 8117 RVA: 0x000B6658 File Offset: 0x000B4858
	private void Restore()
	{
		if (CameraSaveData.valid)
		{
			int num = Grid.PosToCell(CameraSaveData.position);
			if (Grid.IsValidCell(num) && !Grid.IsVisible(num))
			{
				global::Debug.LogWarning("Resetting Camera Position... camera was saved in an undiscovered area of the map.");
				this.CameraGoHome(2f, false);
				return;
			}
			base.transform.SetPosition(CameraSaveData.position);
			base.transform.localScale = CameraSaveData.localScale;
			base.transform.rotation = CameraSaveData.rotation;
			this.targetOrthographicSize = Mathf.Clamp(CameraSaveData.orthographicsSize, this.minOrthographicSize, this.FreeCameraEnabled ? TuningData<CameraController.Tuning>.Get().maxOrthographicSizeDebug : this.maxOrthographicSize);
			this.SnapTo(base.transform.GetPosition());
		}
	}

	// Token: 0x06001FB6 RID: 8118 RVA: 0x000B6713 File Offset: 0x000B4913
	private void OnMRTSetupComplete(Camera cam)
	{
		this.cameras.Add(cam);
	}

	// Token: 0x06001FB7 RID: 8119 RVA: 0x000B6721 File Offset: 0x000B4921
	public bool IsAudibleSound(Vector2 pos)
	{
		return this.soundCuller.IsAudible(pos);
	}

	// Token: 0x06001FB8 RID: 8120 RVA: 0x000B6730 File Offset: 0x000B4930
	public bool IsAudibleSound(Vector3 pos, EventReference event_ref)
	{
		string eventReferencePath = KFMOD.GetEventReferencePath(event_ref);
		return this.soundCuller.IsAudible(pos, eventReferencePath);
	}

	// Token: 0x06001FB9 RID: 8121 RVA: 0x000B675B File Offset: 0x000B495B
	public bool IsAudibleSound(Vector3 pos, HashedString sound_path)
	{
		return this.soundCuller.IsAudible(pos, sound_path);
	}

	// Token: 0x06001FBA RID: 8122 RVA: 0x000B676F File Offset: 0x000B496F
	public Vector3 GetVerticallyScaledPosition(Vector3 pos, bool objectIsSelectedAndVisible = false)
	{
		return this.soundCuller.GetVerticallyScaledPosition(pos, objectIsSelectedAndVisible);
	}

	// Token: 0x06001FBB RID: 8123 RVA: 0x000B6780 File Offset: 0x000B4980
	public bool IsVisiblePos(Vector3 pos)
	{
		return this.VisibleArea.CurrentArea.Contains(pos);
	}

	// Token: 0x06001FBC RID: 8124 RVA: 0x000B67A4 File Offset: 0x000B49A4
	public bool IsVisiblePosExtended(Vector3 pos)
	{
		return this.VisibleArea.CurrentAreaExtended.Contains(pos);
	}

	// Token: 0x06001FBD RID: 8125 RVA: 0x000B67C5 File Offset: 0x000B49C5
	protected override void OnCleanUp()
	{
		CameraController.Instance = null;
	}

	// Token: 0x06001FBE RID: 8126 RVA: 0x000B67D0 File Offset: 0x000B49D0
	public void SetFollowTarget(Transform follow_target)
	{
		this.ClearFollowTarget();
		if (follow_target == null)
		{
			return;
		}
		this.followTarget = follow_target;
		this.OrthographicSize = 6f;
		this.targetOrthographicSize = 6f;
		Vector3 followPos = this.GetFollowPos();
		this.followTargetPos = new Vector3(followPos.x, followPos.y, base.transform.GetPosition().z);
		base.transform.SetPosition(this.followTargetPos);
		this.followTarget.GetComponent<KMonoBehaviour>().Trigger(-1506069671, null);
	}

	// Token: 0x06001FBF RID: 8127 RVA: 0x000B6860 File Offset: 0x000B4A60
	public void ClearFollowTarget()
	{
		if (this.followTarget == null)
		{
			return;
		}
		this.followTarget.GetComponent<KMonoBehaviour>().Trigger(-485480405, null);
		this.followTarget = null;
	}

	// Token: 0x06001FC0 RID: 8128 RVA: 0x000B6890 File Offset: 0x000B4A90
	public void UpdateFollowTarget()
	{
		if (this.followTarget != null)
		{
			Vector3 followPos = this.GetFollowPos();
			Vector2 vector = new Vector2(base.transform.GetLocalPosition().x, base.transform.GetLocalPosition().y);
			byte b = Grid.WorldIdx[Grid.PosToCell(followPos)];
			if (ClusterManager.Instance.activeWorldId != (int)b)
			{
				Transform transform = this.followTarget;
				this.SetFollowTarget(null);
				ClusterManager.Instance.SetActiveWorld((int)b);
				this.SetFollowTarget(transform);
				return;
			}
			Vector2 vector2 = Vector2.Lerp(vector, followPos, Time.unscaledDeltaTime * 25f);
			this.followTargetPos = new Vector3(vector2.x, vector2.y, base.transform.GetLocalPosition().z);
		}
	}

	// Token: 0x06001FC1 RID: 8129 RVA: 0x000B695C File Offset: 0x000B4B5C
	public void RenderForTimelapser(ref RenderTexture tex)
	{
		this.RenderCameraForTimelapse(this.baseCamera, ref tex, this.timelapseCameraCullingMask, -1f);
		CameraClearFlags clearFlags = this.overlayCamera.clearFlags;
		this.overlayCamera.clearFlags = CameraClearFlags.Nothing;
		this.RenderCameraForTimelapse(this.overlayCamera, ref tex, this.timelapseOverlayCameraCullingMask, -1f);
		this.overlayCamera.clearFlags = clearFlags;
	}

	// Token: 0x06001FC2 RID: 8130 RVA: 0x000B69C0 File Offset: 0x000B4BC0
	private void RenderCameraForTimelapse(Camera cam, ref RenderTexture tex, LayerMask mask, float overrideAspect = -1f)
	{
		int cullingMask = cam.cullingMask;
		RenderTexture targetTexture = cam.targetTexture;
		cam.targetTexture = tex;
		cam.aspect = (float)tex.width / (float)tex.height;
		if (overrideAspect != -1f)
		{
			cam.aspect = overrideAspect;
		}
		if (mask != -1)
		{
			cam.cullingMask = mask;
		}
		cam.Render();
		cam.ResetAspect();
		cam.cullingMask = cullingMask;
		cam.targetTexture = targetTexture;
	}

	// Token: 0x06001FC3 RID: 8131 RVA: 0x000B6A3A File Offset: 0x000B4C3A
	private void CheckMoveUnpause()
	{
		if (this.cinemaCamEnabled && this.cinemaUnpauseNextMove)
		{
			this.cinemaUnpauseNextMove = !this.cinemaUnpauseNextMove;
			if (SpeedControlScreen.Instance.IsPaused)
			{
				SpeedControlScreen.Instance.Unpause(false);
			}
		}
	}

	// Token: 0x04001242 RID: 4674
	public const float DEFAULT_MAX_ORTHO_SIZE = 20f;

	// Token: 0x04001243 RID: 4675
	public const float MAX_Y_SCALE = 1.1f;

	// Token: 0x04001244 RID: 4676
	public LocText infoText;

	// Token: 0x04001245 RID: 4677
	private const float FIXED_Z = -100f;

	// Token: 0x04001247 RID: 4679
	public bool FreeCameraEnabled;

	// Token: 0x04001248 RID: 4680
	public float zoomSpeed;

	// Token: 0x04001249 RID: 4681
	public float minOrthographicSize;

	// Token: 0x0400124A RID: 4682
	public float zoomFactor;

	// Token: 0x0400124B RID: 4683
	public float keyPanningSpeed;

	// Token: 0x0400124C RID: 4684
	public float keyPanningEasing;

	// Token: 0x0400124D RID: 4685
	public Texture2D dayColourCube;

	// Token: 0x0400124E RID: 4686
	public Texture2D nightColourCube;

	// Token: 0x0400124F RID: 4687
	public Material LightBufferMaterial;

	// Token: 0x04001250 RID: 4688
	public Material LightCircleOverlay;

	// Token: 0x04001251 RID: 4689
	public Material LightConeOverlay;

	// Token: 0x04001252 RID: 4690
	public Transform followTarget;

	// Token: 0x04001253 RID: 4691
	public Vector3 followTargetPos;

	// Token: 0x04001254 RID: 4692
	public GridVisibleArea VisibleArea = new GridVisibleArea(8);

	// Token: 0x04001256 RID: 4694
	private float maxOrthographicSize = 20f;

	// Token: 0x04001257 RID: 4695
	private float overrideZoomSpeed;

	// Token: 0x04001258 RID: 4696
	private bool panning;

	// Token: 0x04001259 RID: 4697
	private const float MaxEdgePaddingPercent = 0.33f;

	// Token: 0x0400125A RID: 4698
	private Vector3 keyPanDelta;

	// Token: 0x0400125D RID: 4701
	[SerializeField]
	private LayerMask timelapseCameraCullingMask;

	// Token: 0x0400125E RID: 4702
	[SerializeField]
	private LayerMask timelapseOverlayCameraCullingMask;

	// Token: 0x04001260 RID: 4704
	private bool userCameraControlDisabled;

	// Token: 0x04001261 RID: 4705
	private bool panLeft;

	// Token: 0x04001262 RID: 4706
	private bool panRight;

	// Token: 0x04001263 RID: 4707
	private bool panUp;

	// Token: 0x04001264 RID: 4708
	private bool panDown;

	// Token: 0x04001266 RID: 4710
	[NonSerialized]
	public Camera baseCamera;

	// Token: 0x04001267 RID: 4711
	[NonSerialized]
	public Camera overlayCamera;

	// Token: 0x04001268 RID: 4712
	[NonSerialized]
	public Camera overlayNoDepthCamera;

	// Token: 0x04001269 RID: 4713
	[NonSerialized]
	public Camera uiCamera;

	// Token: 0x0400126A RID: 4714
	[NonSerialized]
	public Camera lightBufferCamera;

	// Token: 0x0400126B RID: 4715
	[NonSerialized]
	public Camera simOverlayCamera;

	// Token: 0x0400126C RID: 4716
	[NonSerialized]
	public Camera infraredCamera;

	// Token: 0x0400126D RID: 4717
	[NonSerialized]
	public Camera timelapseFreezeCamera;

	// Token: 0x0400126E RID: 4718
	[SerializeField]
	private List<GameScreenManager.UIRenderTarget> uiCameraTargets;

	// Token: 0x0400126F RID: 4719
	public List<Camera> cameras = new List<Camera>();

	// Token: 0x04001270 RID: 4720
	private MultipleRenderTarget mrt;

	// Token: 0x04001271 RID: 4721
	private KAnimActivePostProcessingEffects kAnimPostProcessingEffects;

	// Token: 0x04001272 RID: 4722
	private CustomActiveScreenPostProcessingEffects customActiveScreenPostProcessingEffects;

	// Token: 0x04001273 RID: 4723
	public SoundCuller soundCuller;

	// Token: 0x04001274 RID: 4724
	private bool cinemaCamEnabled;

	// Token: 0x04001275 RID: 4725
	private bool cinemaToggleLock;

	// Token: 0x04001276 RID: 4726
	private bool cinemaToggleEasing;

	// Token: 0x04001277 RID: 4727
	private bool cinemaUnpauseNextMove;

	// Token: 0x04001278 RID: 4728
	private bool cinemaPanLeft;

	// Token: 0x04001279 RID: 4729
	private bool cinemaPanRight;

	// Token: 0x0400127A RID: 4730
	private bool cinemaPanUp;

	// Token: 0x0400127B RID: 4731
	private bool cinemaPanDown;

	// Token: 0x0400127C RID: 4732
	private bool cinemaZoomIn;

	// Token: 0x0400127D RID: 4733
	private bool cinemaZoomOut;

	// Token: 0x0400127E RID: 4734
	private int cinemaZoomSpeed = 10;

	// Token: 0x0400127F RID: 4735
	private float cinemaEasing = 0.05f;

	// Token: 0x04001280 RID: 4736
	private float cinemaZoomVelocity;

	// Token: 0x04001282 RID: 4738
	private float smoothDt;

	// Token: 0x020013BB RID: 5051
	public class Tuning : TuningData<CameraController.Tuning>
	{
		// Token: 0x04006A7E RID: 27262
		public float maxOrthographicSizeDebug;

		// Token: 0x04006A7F RID: 27263
		public float cinemaZoomFactor = 100f;

		// Token: 0x04006A80 RID: 27264
		public float cinemaPanFactor = 50f;

		// Token: 0x04006A81 RID: 27265
		public float cinemaZoomToFactor = 100f;

		// Token: 0x04006A82 RID: 27266
		public float cinemaPanToFactor = 50f;

		// Token: 0x04006A83 RID: 27267
		public float targetZoomEasingFactor = 400f;

		// Token: 0x04006A84 RID: 27268
		public float targetPanEasingFactor = 100f;
	}
}
