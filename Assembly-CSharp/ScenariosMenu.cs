using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DB9 RID: 3513
public class ScenariosMenu : KModalScreen, SteamUGCService.IClient
{
	// Token: 0x06006EC8 RID: 28360 RVA: 0x002A2FF8 File Offset: 0x002A11F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.dismissButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.dismissButton.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.FRONTEND.OPTIONS_SCREEN.BACK);
		this.closeButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.workshopButton.onClick += delegate
		{
			this.OnClickOpenWorkshop();
		};
		this.RebuildScreen();
	}

	// Token: 0x06006EC9 RID: 28361 RVA: 0x002A307C File Offset: 0x002A127C
	private void RebuildScreen()
	{
		foreach (GameObject gameObject in this.buttons)
		{
			global::UnityEngine.Object.Destroy(gameObject);
		}
		this.buttons.Clear();
		this.RebuildUGCButtons();
	}

	// Token: 0x06006ECA RID: 28362 RVA: 0x002A30E0 File Offset: 0x002A12E0
	private void RebuildUGCButtons()
	{
		ListPool<SteamUGCService.Mod, ScenariosMenu>.PooledList pooledList = ListPool<SteamUGCService.Mod, ScenariosMenu>.Allocate();
		bool flag = pooledList.Count > 0;
		this.noScenariosText.gameObject.SetActive(!flag);
		this.contentRoot.gameObject.SetActive(flag);
		bool flag2 = true;
		if (pooledList.Count != 0)
		{
			for (int i = 0; i < pooledList.Count; i++)
			{
				GameObject gameObject = Util.KInstantiateUI(this.ugcButtonPrefab, this.ugcContainer, false);
				gameObject.name = pooledList[i].title + "_button";
				gameObject.gameObject.SetActive(true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Title").SetText(pooledList[i].title);
				Texture2D previewImage = pooledList[i].previewImage;
				if (previewImage != null)
				{
					component.GetReference<Image>("Image").sprite = Sprite.Create(previewImage, new Rect(Vector2.zero, new Vector2((float)previewImage.width, (float)previewImage.height)), Vector2.one * 0.5f);
				}
				KButton component2 = gameObject.GetComponent<KButton>();
				int num = i;
				PublishedFileId_t item = pooledList[num].fileId;
				component2.onClick += delegate
				{
					this.ShowDetails(item);
				};
				component2.onDoubleClick += delegate
				{
					this.LoadScenario(item);
				};
				this.buttons.Add(gameObject);
				if (item == this.activeItem)
				{
					flag2 = false;
				}
			}
		}
		if (flag2)
		{
			this.HideDetails();
		}
		pooledList.Recycle();
	}

	// Token: 0x06006ECB RID: 28363 RVA: 0x002A328C File Offset: 0x002A148C
	private void LoadScenario(PublishedFileId_t item)
	{
		ulong num;
		string text;
		uint num2;
		SteamUGC.GetItemInstallInfo(item, out num, out text, 1024U, out num2);
		DebugUtil.LogArgs(new object[] { "LoadScenario", text, num, num2 });
		global::System.DateTime dateTime;
		byte[] bytesFromZip = SteamUGCService.GetBytesFromZip(item, new string[] { ".sav" }, out dateTime, false);
		string text2 = Path.Combine(SaveLoader.GetSavePrefix(), "scenario.sav");
		File.WriteAllBytes(text2, bytesFromZip);
		SaveLoader.SetActiveSaveFilePath(text2);
		Time.timeScale = 0f;
		App.LoadScene("backend");
	}

	// Token: 0x06006ECC RID: 28364 RVA: 0x002A331D File Offset: 0x002A151D
	private ConfirmDialogScreen GetConfirmDialog()
	{
		KScreen component = KScreenManager.AddChild(base.transform.parent.gameObject, ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<KScreen>();
		component.Activate();
		return component.GetComponent<ConfirmDialogScreen>();
	}

	// Token: 0x06006ECD RID: 28365 RVA: 0x002A3354 File Offset: 0x002A1554
	private void ShowDetails(PublishedFileId_t item)
	{
		this.activeItem = item;
		SteamUGCService.Mod mod = SteamUGCService.Instance.FindMod(item);
		if (mod != null)
		{
			this.scenarioTitle.text = mod.title;
			this.scenarioDetails.text = mod.description;
		}
		this.loadScenarioButton.onClick += delegate
		{
			this.LoadScenario(item);
		};
		this.detailsRoot.gameObject.SetActive(true);
	}

	// Token: 0x06006ECE RID: 28366 RVA: 0x002A33DF File Offset: 0x002A15DF
	private void HideDetails()
	{
		this.detailsRoot.gameObject.SetActive(false);
	}

	// Token: 0x06006ECF RID: 28367 RVA: 0x002A33F2 File Offset: 0x002A15F2
	protected override void OnActivate()
	{
		base.OnActivate();
		SteamUGCService.Instance.AddClient(this);
		this.HideDetails();
	}

	// Token: 0x06006ED0 RID: 28368 RVA: 0x002A340B File Offset: 0x002A160B
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		SteamUGCService.Instance.RemoveClient(this);
	}

	// Token: 0x06006ED1 RID: 28369 RVA: 0x002A341E File Offset: 0x002A161E
	private void OnClickOpenWorkshop()
	{
		App.OpenWebURL("http://steamcommunity.com/workshop/browse/?appid=457140&requiredtags[]=scenario");
	}

	// Token: 0x06006ED2 RID: 28370 RVA: 0x002A342A File Offset: 0x002A162A
	public void UpdateMods(IEnumerable<PublishedFileId_t> added, IEnumerable<PublishedFileId_t> updated, IEnumerable<PublishedFileId_t> removed, IEnumerable<SteamUGCService.Mod> loaded_previews)
	{
		this.RebuildScreen();
	}

	// Token: 0x04004C28 RID: 19496
	public const string TAG_SCENARIO = "scenario";

	// Token: 0x04004C29 RID: 19497
	public KButton textButton;

	// Token: 0x04004C2A RID: 19498
	public KButton dismissButton;

	// Token: 0x04004C2B RID: 19499
	public KButton closeButton;

	// Token: 0x04004C2C RID: 19500
	public KButton workshopButton;

	// Token: 0x04004C2D RID: 19501
	public KButton loadScenarioButton;

	// Token: 0x04004C2E RID: 19502
	[Space]
	public GameObject ugcContainer;

	// Token: 0x04004C2F RID: 19503
	public GameObject ugcButtonPrefab;

	// Token: 0x04004C30 RID: 19504
	public LocText noScenariosText;

	// Token: 0x04004C31 RID: 19505
	public RectTransform contentRoot;

	// Token: 0x04004C32 RID: 19506
	public RectTransform detailsRoot;

	// Token: 0x04004C33 RID: 19507
	public LocText scenarioTitle;

	// Token: 0x04004C34 RID: 19508
	public LocText scenarioDetails;

	// Token: 0x04004C35 RID: 19509
	private PublishedFileId_t activeItem;

	// Token: 0x04004C36 RID: 19510
	private List<GameObject> buttons = new List<GameObject>();
}
