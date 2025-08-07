using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D22 RID: 3362
public class LockerNavigator : KModalScreen
{
	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x060067B6 RID: 26550 RVA: 0x0027161E File Offset: 0x0026F81E
	public GameObject ContentSlot
	{
		get
		{
			return this.slot.gameObject;
		}
	}

	// Token: 0x060067B7 RID: 26551 RVA: 0x0027162B File Offset: 0x0026F82B
	protected override void OnActivate()
	{
		LockerNavigator.Instance = this;
		this.Show(false);
		this.backButton.onClick += this.OnClickBack;
	}

	// Token: 0x060067B8 RID: 26552 RVA: 0x00271651 File Offset: 0x0026F851
	public override float GetSortKey()
	{
		return 41f;
	}

	// Token: 0x060067B9 RID: 26553 RVA: 0x00271658 File Offset: 0x0026F858
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.PopScreen();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x060067BA RID: 26554 RVA: 0x0027167A File Offset: 0x0026F87A
	public override void Show(bool show = true)
	{
		base.Show(show);
		if (!show)
		{
			this.PopAllScreens();
		}
		StreamedTextures.SetBundlesLoaded(show);
	}

	// Token: 0x060067BB RID: 26555 RVA: 0x00271692 File Offset: 0x0026F892
	private void OnClickBack()
	{
		this.PopScreen();
	}

	// Token: 0x060067BC RID: 26556 RVA: 0x0027169C File Offset: 0x0026F89C
	public void PushScreen(GameObject screen, global::System.Action onClose = null)
	{
		if (screen == null)
		{
			return;
		}
		if (this.navigationHistory.Count == 0)
		{
			this.Show(true);
			if (!LockerNavigator.didDisplayDataCollectionWarningPopupOnce && KPrivacyPrefs.instance.disableDataCollection)
			{
				LockerNavigator.MakeDataCollectionWarningPopup(base.gameObject.transform.parent.gameObject);
				LockerNavigator.didDisplayDataCollectionWarningPopupOnce = true;
			}
		}
		if (this.navigationHistory.Count > 0 && screen == this.navigationHistory[this.navigationHistory.Count - 1].screen)
		{
			return;
		}
		if (this.navigationHistory.Count > 0)
		{
			this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(false);
		}
		this.navigationHistory.Add(new LockerNavigator.HistoryEntry(screen, onClose));
		this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(true);
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this.RefreshButtons();
	}

	// Token: 0x060067BD RID: 26557 RVA: 0x002717B4 File Offset: 0x0026F9B4
	public bool PopScreen()
	{
		while (this.preventScreenPop.Count > 0)
		{
			int num = this.preventScreenPop.Count - 1;
			Func<bool> func = this.preventScreenPop[num];
			this.preventScreenPop.RemoveAt(num);
			if (func())
			{
				return true;
			}
		}
		int num2 = this.navigationHistory.Count - 1;
		LockerNavigator.HistoryEntry historyEntry = this.navigationHistory[num2];
		historyEntry.screen.SetActive(false);
		if (historyEntry.onClose.IsSome())
		{
			historyEntry.onClose.Unwrap()();
		}
		this.navigationHistory.RemoveAt(num2);
		if (this.navigationHistory.Count > 0)
		{
			this.navigationHistory[this.navigationHistory.Count - 1].screen.SetActive(true);
			this.RefreshButtons();
			return true;
		}
		this.Show(false);
		MusicManager.instance.SetSongParameter("Music_SupplyCloset", "SupplyClosetView", "initial", true);
		return false;
	}

	// Token: 0x060067BE RID: 26558 RVA: 0x002718B0 File Offset: 0x0026FAB0
	public void PopAllScreens()
	{
		if (this.navigationHistory.Count == 0 && this.preventScreenPop.Count == 0)
		{
			return;
		}
		int num = 0;
		while (this.PopScreen())
		{
			if (num > 100)
			{
				DebugUtil.DevAssert(false, string.Format("Can't close all LockerNavigator screens, hit limit of trying to close {0} screens", 100), null);
				return;
			}
			num++;
		}
	}

	// Token: 0x060067BF RID: 26559 RVA: 0x00271906 File Offset: 0x0026FB06
	private void RefreshButtons()
	{
		this.backButton.isInteractable = true;
	}

	// Token: 0x060067C0 RID: 26560 RVA: 0x00271914 File Offset: 0x0026FB14
	public void ShowDialogPopup(Action<InfoDialogScreen> configureDialogFn)
	{
		InfoDialogScreen dialog = Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, this.ContentSlot, false);
		configureDialogFn(dialog);
		dialog.Activate();
		dialog.gameObject.AddOrGet<LayoutElement>().ignoreLayout = true;
		dialog.gameObject.AddOrGet<RectTransform>().Fill();
		Func<bool> preventScreenPopFn = delegate
		{
			dialog.Deactivate();
			return true;
		};
		this.preventScreenPop.Add(preventScreenPopFn);
		InfoDialogScreen dialog2 = dialog;
		dialog2.onDeactivateFn = (global::System.Action)Delegate.Combine(dialog2.onDeactivateFn, new global::System.Action(delegate
		{
			this.preventScreenPop.Remove(preventScreenPopFn);
		}));
	}

	// Token: 0x060067C1 RID: 26561 RVA: 0x002719DC File Offset: 0x0026FBDC
	public static void MakeDataCollectionWarningPopup(GameObject fullscreenParent)
	{
		Action<InfoDialogScreen> <>9__2;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.HEADER).AddPlainText(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BODY).AddOption(UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BUTTON_OK, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, true);
			string text = UI.LOCKER_NAVIGATOR.DATA_COLLECTION_WARNING_POPUP.BUTTON_OPEN_SETTINGS;
			Action<InfoDialogScreen> action;
			if ((action = <>9__2) == null)
			{
				action = (<>9__2 = delegate(InfoDialogScreen d)
				{
					d.Deactivate();
					LockerNavigator.Instance.PopAllScreens();
					LockerMenuScreen.Instance.Show(false);
					Util.KInstantiateUI<OptionsMenuScreen>(ScreenPrefabs.Instance.OptionsScreen.gameObject, fullscreenParent, true).ShowMetricsScreen();
				});
			}
			infoDialogScreen.AddOption(text, action, false);
		});
	}

	// Token: 0x04004712 RID: 18194
	public static LockerNavigator Instance;

	// Token: 0x04004713 RID: 18195
	[SerializeField]
	private RectTransform slot;

	// Token: 0x04004714 RID: 18196
	[SerializeField]
	private KButton backButton;

	// Token: 0x04004715 RID: 18197
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004716 RID: 18198
	[SerializeField]
	public GameObject kleiInventoryScreen;

	// Token: 0x04004717 RID: 18199
	[SerializeField]
	public GameObject duplicantCatalogueScreen;

	// Token: 0x04004718 RID: 18200
	[SerializeField]
	public GameObject outfitDesignerScreen;

	// Token: 0x04004719 RID: 18201
	[SerializeField]
	public GameObject outfitBrowserScreen;

	// Token: 0x0400471A RID: 18202
	[SerializeField]
	public GameObject joyResponseDesignerScreen;

	// Token: 0x0400471B RID: 18203
	private const string LOCKER_MENU_MUSIC = "Music_SupplyCloset";

	// Token: 0x0400471C RID: 18204
	private const string MUSIC_PARAMETER = "SupplyClosetView";

	// Token: 0x0400471D RID: 18205
	private List<LockerNavigator.HistoryEntry> navigationHistory = new List<LockerNavigator.HistoryEntry>();

	// Token: 0x0400471E RID: 18206
	private Dictionary<string, GameObject> screens = new Dictionary<string, GameObject>();

	// Token: 0x0400471F RID: 18207
	private static bool didDisplayDataCollectionWarningPopupOnce;

	// Token: 0x04004720 RID: 18208
	public List<Func<bool>> preventScreenPop = new List<Func<bool>>();

	// Token: 0x02001EEA RID: 7914
	public readonly struct HistoryEntry
	{
		// Token: 0x0600B1C9 RID: 45513 RVA: 0x003D621E File Offset: 0x003D441E
		public HistoryEntry(GameObject screen, global::System.Action onClose = null)
		{
			this.screen = screen;
			this.onClose = onClose;
		}

		// Token: 0x04008F42 RID: 36674
		public readonly GameObject screen;

		// Token: 0x04008F43 RID: 36675
		public readonly Option<global::System.Action> onClose;
	}
}
