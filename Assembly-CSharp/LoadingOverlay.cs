using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000C24 RID: 3108
public class LoadingOverlay : KModalScreen
{
	// Token: 0x06005E52 RID: 24146 RVA: 0x00228A7A File Offset: 0x00226C7A
	protected override void OnPrefabInit()
	{
		this.pause = false;
		this.fadeIn = false;
		base.OnPrefabInit();
	}

	// Token: 0x06005E53 RID: 24147 RVA: 0x00228A90 File Offset: 0x00226C90
	private void Update()
	{
		if (!this.loadNextFrame && this.showLoad)
		{
			this.loadNextFrame = true;
			this.showLoad = false;
			return;
		}
		if (this.loadNextFrame)
		{
			this.loadNextFrame = false;
			this.loadCb();
		}
	}

	// Token: 0x06005E54 RID: 24148 RVA: 0x00228ACB File Offset: 0x00226CCB
	public static void DestroyInstance()
	{
		LoadingOverlay.instance = null;
	}

	// Token: 0x06005E55 RID: 24149 RVA: 0x00228AD4 File Offset: 0x00226CD4
	public static void Load(global::System.Action cb)
	{
		GameObject gameObject = GameObject.Find("/SceneInitializerFE/FrontEndManager");
		if (LoadingOverlay.instance == null)
		{
			LoadingOverlay.instance = Util.KInstantiateUI<LoadingOverlay>(ScreenPrefabs.Instance.loadingOverlay.gameObject, (GameScreenManager.Instance == null) ? gameObject : GameScreenManager.Instance.ssOverlayCanvas, false);
			LoadingOverlay.instance.GetComponentInChildren<LocText>().SetText(UI.FRONTEND.LOADING);
		}
		if (GameScreenManager.Instance != null)
		{
			LoadingOverlay.instance.transform.SetParent(GameScreenManager.Instance.ssOverlayCanvas.transform);
			LoadingOverlay.instance.transform.SetSiblingIndex(GameScreenManager.Instance.ssOverlayCanvas.transform.childCount - 1);
		}
		else
		{
			LoadingOverlay.instance.transform.SetParent(gameObject.transform);
			LoadingOverlay.instance.transform.SetSiblingIndex(gameObject.transform.childCount - 1);
			if (MainMenu.Instance != null)
			{
				MainMenu.Instance.StopAmbience();
			}
		}
		LoadingOverlay.instance.loadCb = cb;
		LoadingOverlay.instance.showLoad = true;
		LoadingOverlay.instance.Activate();
	}

	// Token: 0x06005E56 RID: 24150 RVA: 0x00228C00 File Offset: 0x00226E00
	public static void Clear()
	{
		if (LoadingOverlay.instance != null)
		{
			LoadingOverlay.instance.Deactivate();
		}
	}

	// Token: 0x04003EC1 RID: 16065
	private bool loadNextFrame;

	// Token: 0x04003EC2 RID: 16066
	private bool showLoad;

	// Token: 0x04003EC3 RID: 16067
	private global::System.Action loadCb;

	// Token: 0x04003EC4 RID: 16068
	private static LoadingOverlay instance;
}
