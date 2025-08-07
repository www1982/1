using System;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C6B RID: 3179
public class BarterConfirmationScreen : KModalScreen
{
	// Token: 0x06006107 RID: 24839 RVA: 0x0023E5DF File Offset: 0x0023C7DF
	protected override void OnActivate()
	{
		base.OnActivate();
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		this.cancelButton.onClick += delegate
		{
			this.Show(false);
		};
	}

	// Token: 0x06006108 RID: 24840 RVA: 0x0023E618 File Offset: 0x0023C818
	public void Present(PermitResource permit, bool isPurchase)
	{
		this.Show(true);
		this.ShowContentContainer(true);
		this.ShowLoadingPanel(false);
		this.HideResultPanel();
		if (isPurchase)
		{
			this.itemIcon.transform.SetAsLastSibling();
			this.filamentIcon.transform.SetAsFirstSibling();
		}
		else
		{
			this.itemIcon.transform.SetAsFirstSibling();
			this.filamentIcon.transform.SetAsLastSibling();
		}
		KleiItems.ResponseCallback <>9__1;
		KleiItems.ResponseCallback <>9__2;
		this.confirmButton.onClick += delegate
		{
			string serverTypeFromPermit = PermitItems.GetServerTypeFromPermit(permit);
			if (serverTypeFromPermit == null)
			{
				return;
			}
			this.ShowContentContainer(false);
			this.HideResultPanel();
			this.ShowLoadingPanel(true);
			if (isPurchase)
			{
				string text = serverTypeFromPermit;
				KleiItems.ResponseCallback responseCallback;
				if ((responseCallback = <>9__1) == null)
				{
					responseCallback = (<>9__1 = delegate(KleiItems.Result result)
					{
						if (this.IsNullOrDestroyed())
						{
							return;
						}
						this.ShowContentContainer(false);
						this.ShowLoadingPanel(false);
						if (!result.Success)
						{
							this.ShowResultPanel(permit, true, false);
							return;
						}
						this.ShowResultPanel(permit, true, true);
					});
				}
				KleiItems.AddRequestBarterGainItem(text, responseCallback);
				return;
			}
			ulong itemInstanceID = KleiItems.GetItemInstanceID(serverTypeFromPermit);
			KleiItems.ResponseCallback responseCallback2;
			if ((responseCallback2 = <>9__2) == null)
			{
				responseCallback2 = (<>9__2 = delegate(KleiItems.Result result)
				{
					if (this.IsNullOrDestroyed())
					{
						return;
					}
					this.ShowContentContainer(false);
					this.ShowLoadingPanel(false);
					if (!result.Success)
					{
						this.ShowResultPanel(permit, false, false);
						return;
					}
					this.ShowResultPanel(permit, false, true);
				});
			}
			KleiItems.AddRequestBarterLoseItem(itemInstanceID, responseCallback2);
		};
		ulong num;
		ulong num2;
		PermitItems.TryGetBarterPrice(permit.Id, out num, out num2);
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		this.itemIcon.GetComponent<Image>().sprite = permitPresentationInfo.sprite;
		this.itemLabel.SetText(permit.Name);
		this.transactionDescriptionLabel.SetText(isPurchase ? UI.KLEI_INVENTORY_SCREEN.BARTERING.ACTION_DESCRIPTION_PRINT : UI.KLEI_INVENTORY_SCREEN.BARTERING.ACTION_DESCRIPTION_RECYCLE);
		this.panelHeaderLabel.SetText(isPurchase ? UI.KLEI_INVENTORY_SCREEN.BARTERING.CONFIRM_PRINT_HEADER : UI.KLEI_INVENTORY_SCREEN.BARTERING.CONFIRM_RECYCLE_HEADER);
		this.confirmButtonActionLabel.SetText(isPurchase ? UI.KLEI_INVENTORY_SCREEN.BARTERING.BUY : UI.KLEI_INVENTORY_SCREEN.BARTERING.SELL);
		this.confirmButtonFilamentLabel.SetText(isPurchase ? num.ToString() : (UIConstants.ColorPrefixGreen + "+" + num2.ToString() + UIConstants.ColorSuffix));
		this.largeCostLabel.SetText(isPurchase ? ("x" + num.ToString()) : ("x" + num2.ToString()));
	}

	// Token: 0x06006109 RID: 24841 RVA: 0x0023E7E3 File Offset: 0x0023C9E3
	private void Update()
	{
		if (this.shouldCloseScreen)
		{
			this.ShowContentContainer(false);
			this.ShowLoadingPanel(false);
			this.HideResultPanel();
			this.Show(false);
		}
	}

	// Token: 0x0600610A RID: 24842 RVA: 0x0023E808 File Offset: 0x0023CA08
	private void ShowContentContainer(bool show)
	{
		this.contentContainer.SetActive(show);
	}

	// Token: 0x0600610B RID: 24843 RVA: 0x0023E818 File Offset: 0x0023CA18
	private void ShowLoadingPanel(bool show)
	{
		this.loadingContainer.SetActive(show);
		this.resultLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.LOADING);
		if (show)
		{
			this.loadingAnimation.Play("loading_rocket", KAnim.PlayMode.Loop, 1f, 0f);
		}
		else
		{
			this.loadingAnimation.Stop();
		}
		if (!show)
		{
			this.shouldCloseScreen = false;
		}
	}

	// Token: 0x0600610C RID: 24844 RVA: 0x0023E880 File Offset: 0x0023CA80
	private void HideResultPanel()
	{
		this.resultContainer.SetActive(false);
	}

	// Token: 0x0600610D RID: 24845 RVA: 0x0023E890 File Offset: 0x0023CA90
	private void ShowResultPanel(PermitResource permit, bool isPurchase, bool transationResult)
	{
		this.resultContainer.SetActive(true);
		if (!transationResult)
		{
			this.resultIcon.sprite = Assets.GetSprite("error_message");
			this.mainResultLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.TRANSACTION_ERROR);
			this.panelHeaderLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.TRANSACTION_INCOMPLETE_HEADER);
			this.resultFilamentLabel.SetText("");
			KFMOD.PlayUISound(GlobalAssets.GetSound("SupplyCloset_Bartering_Failed", false));
			return;
		}
		this.panelHeaderLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.TRANSACTION_COMPLETE_HEADER);
		if (isPurchase)
		{
			PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
			this.resultIcon.sprite = permitPresentationInfo.sprite;
			this.resultFilamentLabel.SetText("");
			this.mainResultLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.PURCHASE_SUCCESS);
			KFMOD.PlayUISound(GlobalAssets.GetSound("SupplyCloset_Print_Succeed", false));
			return;
		}
		ulong num;
		ulong num2;
		PermitItems.TryGetBarterPrice(permit.Id, out num, out num2);
		this.resultIcon.sprite = Assets.GetSprite("filament");
		this.resultFilamentLabel.GetComponent<LocText>().SetText("x" + num2.ToString());
		this.mainResultLabel.SetText(UI.KLEI_INVENTORY_SCREEN.BARTERING.SELL_SUCCESS);
		KFMOD.PlayUISound(GlobalAssets.GetSound("SupplyCloset_Bartering_Succeed", false));
	}

	// Token: 0x040041A2 RID: 16802
	[SerializeField]
	private GameObject itemIcon;

	// Token: 0x040041A3 RID: 16803
	[SerializeField]
	private GameObject filamentIcon;

	// Token: 0x040041A4 RID: 16804
	[SerializeField]
	private LocText largeCostLabel;

	// Token: 0x040041A5 RID: 16805
	[SerializeField]
	private LocText largeQuantityLabel;

	// Token: 0x040041A6 RID: 16806
	[SerializeField]
	private LocText itemLabel;

	// Token: 0x040041A7 RID: 16807
	[SerializeField]
	private LocText transactionDescriptionLabel;

	// Token: 0x040041A8 RID: 16808
	[SerializeField]
	private KButton confirmButton;

	// Token: 0x040041A9 RID: 16809
	[SerializeField]
	private KButton cancelButton;

	// Token: 0x040041AA RID: 16810
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040041AB RID: 16811
	[SerializeField]
	private LocText panelHeaderLabel;

	// Token: 0x040041AC RID: 16812
	[SerializeField]
	private LocText confirmButtonActionLabel;

	// Token: 0x040041AD RID: 16813
	[SerializeField]
	private LocText confirmButtonFilamentLabel;

	// Token: 0x040041AE RID: 16814
	[SerializeField]
	private LocText resultLabel;

	// Token: 0x040041AF RID: 16815
	[SerializeField]
	private KBatchedAnimController loadingAnimation;

	// Token: 0x040041B0 RID: 16816
	[SerializeField]
	private GameObject contentContainer;

	// Token: 0x040041B1 RID: 16817
	[SerializeField]
	private GameObject loadingContainer;

	// Token: 0x040041B2 RID: 16818
	[SerializeField]
	private GameObject resultContainer;

	// Token: 0x040041B3 RID: 16819
	[SerializeField]
	private Image resultIcon;

	// Token: 0x040041B4 RID: 16820
	[SerializeField]
	private LocText mainResultLabel;

	// Token: 0x040041B5 RID: 16821
	[SerializeField]
	private LocText resultFilamentLabel;

	// Token: 0x040041B6 RID: 16822
	private bool shouldCloseScreen;
}
