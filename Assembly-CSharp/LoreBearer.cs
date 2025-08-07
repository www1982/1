using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020009BB RID: 2491
[AddComponentMenu("KMonoBehaviour/scripts/LoreBearer")]
public class LoreBearer : KMonoBehaviour, ISidescreenButtonControl
{
	// Token: 0x17000510 RID: 1296
	// (get) Token: 0x060048A4 RID: 18596 RVA: 0x001A3635 File Offset: 0x001A1835
	public string content
	{
		get
		{
			return Strings.Get("STRINGS.LORE.BUILDINGS." + base.gameObject.name + ".ENTRY");
		}
	}

	// Token: 0x060048A5 RID: 18597 RVA: 0x001A365B File Offset: 0x001A185B
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060048A6 RID: 18598 RVA: 0x001A3663 File Offset: 0x001A1863
	public LoreBearer Internal_SetContent(LoreBearerAction action)
	{
		this.displayContentAction = action;
		return this;
	}

	// Token: 0x060048A7 RID: 18599 RVA: 0x001A366D File Offset: 0x001A186D
	public LoreBearer Internal_SetContent(LoreBearerAction action, string[] collectionsToUnlockFrom)
	{
		this.displayContentAction = action;
		this.collectionsToUnlockFrom = collectionsToUnlockFrom;
		return this;
	}

	// Token: 0x060048A8 RID: 18600 RVA: 0x001A367E File Offset: 0x001A187E
	public static InfoDialogScreen ShowPopupDialog()
	{
		return (InfoDialogScreen)GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
	}

	// Token: 0x060048A9 RID: 18601 RVA: 0x001A36B0 File Offset: 0x001A18B0
	private void OnClickRead()
	{
		InfoDialogScreen infoDialogScreen = LoreBearer.ShowPopupDialog().SetHeader(base.gameObject.GetComponent<KSelectable>().GetProperName()).AddDefaultOK(true);
		if (this.BeenClicked)
		{
			infoDialogScreen.AddPlainText(this.BeenSearched);
			return;
		}
		this.BeenClicked = true;
		if (DlcManager.IsExpansion1Active())
		{
			Scenario.SpawnPrefab(Grid.PosToCell(base.gameObject), 0, 1, "OrbitalResearchDatabank", Grid.SceneLayer.Front).SetActive(true);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, Assets.GetPrefab("OrbitalResearchDatabank".ToTag()).GetProperName(), base.gameObject.transform, 1.5f, false);
		}
		if (this.displayContentAction != null)
		{
			this.displayContentAction(infoDialogScreen);
			return;
		}
		LoreBearerUtil.UnlockNextJournalEntry(infoDialogScreen);
	}

	// Token: 0x17000511 RID: 1297
	// (get) Token: 0x060048AA RID: 18602 RVA: 0x001A3776 File Offset: 0x001A1976
	public string SidescreenButtonText
	{
		get
		{
			return this.BeenClicked ? UI.USERMENUACTIONS.READLORE.ALREADYINSPECTED : UI.USERMENUACTIONS.READLORE.NAME;
		}
	}

	// Token: 0x17000512 RID: 1298
	// (get) Token: 0x060048AB RID: 18603 RVA: 0x001A3791 File Offset: 0x001A1991
	public string SidescreenButtonTooltip
	{
		get
		{
			return this.BeenClicked ? UI.USERMENUACTIONS.READLORE.TOOLTIP_ALREADYINSPECTED : UI.USERMENUACTIONS.READLORE.TOOLTIP;
		}
	}

	// Token: 0x060048AC RID: 18604 RVA: 0x001A37AC File Offset: 0x001A19AC
	public int HorizontalGroupID()
	{
		return -1;
	}

	// Token: 0x060048AD RID: 18605 RVA: 0x001A37AF File Offset: 0x001A19AF
	public bool SidescreenEnabled()
	{
		return true;
	}

	// Token: 0x060048AE RID: 18606 RVA: 0x001A37B2 File Offset: 0x001A19B2
	public void OnSidescreenButtonPressed()
	{
		this.OnClickRead();
	}

	// Token: 0x060048AF RID: 18607 RVA: 0x001A37BA File Offset: 0x001A19BA
	public bool SidescreenButtonInteractable()
	{
		return !this.BeenClicked;
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x001A37C5 File Offset: 0x001A19C5
	public int ButtonSideScreenSortOrder()
	{
		return 20;
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x001A37C9 File Offset: 0x001A19C9
	public void SetButtonTextOverride(ButtonMenuTextOverride text)
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002FE8 RID: 12264
	[Serialize]
	private bool BeenClicked;

	// Token: 0x04002FE9 RID: 12265
	public string BeenSearched = UI.USERMENUACTIONS.READLORE.ALREADY_SEARCHED;

	// Token: 0x04002FEA RID: 12266
	private string[] collectionsToUnlockFrom;

	// Token: 0x04002FEB RID: 12267
	private LoreBearerAction displayContentAction;
}
