using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000CAE RID: 3246
public class DLCToggle : KMonoBehaviour
{
	// Token: 0x060063CF RID: 25551 RVA: 0x00257593 File Offset: 0x00255793
	protected override void OnPrefabInit()
	{
		this.expansion1Active = DlcManager.IsExpansion1Active();
	}

	// Token: 0x060063D0 RID: 25552 RVA: 0x002575A0 File Offset: 0x002557A0
	public void ToggleExpansion1Cicked()
	{
		Util.KInstantiateUI<InfoDialogScreen>(ScreenPrefabs.Instance.InfoDialogScreen.gameObject, base.GetComponentInParent<Canvas>().gameObject, true).AddDefaultCancel().SetHeader(this.expansion1Active ? UI.FRONTEND.MAINMENU.DLC.DEACTIVATE_EXPANSION1 : UI.FRONTEND.MAINMENU.DLC.ACTIVATE_EXPANSION1)
			.AddSprite(this.expansion1Active ? GlobalResources.Instance().baseGameLogoSmall : GlobalResources.Instance().expansion1LogoSmall)
			.AddPlainText(this.expansion1Active ? UI.FRONTEND.MAINMENU.DLC.DEACTIVATE_EXPANSION1_DESC : UI.FRONTEND.MAINMENU.DLC.ACTIVATE_EXPANSION1_DESC)
			.AddOption(UI.CONFIRMDIALOG.OK, delegate(InfoDialogScreen screen)
			{
				DlcManager.ToggleDLC("EXPANSION1_ID");
			}, true);
	}

	// Token: 0x040043EF RID: 17391
	private bool expansion1Active;
}
