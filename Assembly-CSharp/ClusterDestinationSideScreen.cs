using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DE1 RID: 3553
public class ClusterDestinationSideScreen : SideScreenContent
{
	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x06007032 RID: 28722 RVA: 0x002AA8A7 File Offset: 0x002A8AA7
	// (set) Token: 0x06007033 RID: 28723 RVA: 0x002AA8AF File Offset: 0x002A8AAF
	private ClusterDestinationSelector targetSelector { get; set; }

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x06007034 RID: 28724 RVA: 0x002AA8B8 File Offset: 0x002A8AB8
	// (set) Token: 0x06007035 RID: 28725 RVA: 0x002AA8C0 File Offset: 0x002A8AC0
	private RocketClusterDestinationSelector targetRocketSelector { get; set; }

	// Token: 0x06007036 RID: 28726 RVA: 0x002AA8CC File Offset: 0x002A8ACC
	protected override void OnSpawn()
	{
		this.changeDestinationButton.onClick += this.OnClickChangeDestination;
		this.clearDestinationButton.onClick += this.OnClickClearDestination;
		this.launchPadDropDown.targetDropDownContainer = GameScreenManager.Instance.ssOverlayCanvas;
		this.launchPadDropDown.CustomizeEmptyRow(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE, null);
		this.repeatButton.onClick += this.OnRepeatClicked;
	}

	// Token: 0x06007037 RID: 28727 RVA: 0x002AA949 File Offset: 0x002A8B49
	public override int GetSideScreenSortOrder()
	{
		return 300;
	}

	// Token: 0x06007038 RID: 28728 RVA: 0x002AA950 File Offset: 0x002A8B50
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.Refresh(null);
			this.m_refreshHandle = this.targetSelector.Subscribe(543433792, delegate(object data)
			{
				this.Refresh(null);
			});
			return;
		}
		if (this.m_refreshHandle != -1)
		{
			this.targetSelector.Unsubscribe(this.m_refreshHandle);
			this.m_refreshHandle = -1;
			this.launchPadDropDown.Close();
		}
	}

	// Token: 0x06007039 RID: 28729 RVA: 0x002AA9C0 File Offset: 0x002A8BC0
	public override bool IsValidForTarget(GameObject target)
	{
		ClusterDestinationSelector component = target.GetComponent<ClusterDestinationSelector>();
		return (component != null && component.assignable) || (target.GetComponent<RocketModule>() != null && target.HasTag(GameTags.LaunchButtonRocketModule)) || (target.GetComponent<RocketControlStation>() != null && target.GetComponent<RocketControlStation>().GetMyWorld().GetComponent<Clustercraft>()
			.Status != Clustercraft.CraftStatus.Launching);
	}

	// Token: 0x0600703A RID: 28730 RVA: 0x002AAA30 File Offset: 0x002A8C30
	public override void SetTarget(GameObject target)
	{
		this.targetSelector = target.GetComponent<ClusterDestinationSelector>();
		if (this.targetSelector == null)
		{
			if (target.GetComponent<RocketModuleCluster>() != null)
			{
				this.targetSelector = target.GetComponent<RocketModuleCluster>().CraftInterface.GetClusterDestinationSelector();
			}
			else if (target.GetComponent<RocketControlStation>() != null)
			{
				this.targetSelector = target.GetMyWorld().GetComponent<Clustercraft>().ModuleInterface.GetClusterDestinationSelector();
			}
		}
		this.targetRocketSelector = this.targetSelector as RocketClusterDestinationSelector;
		this.changeDestinationButton.GetComponent<ToolTip>().SetSimpleTooltip(this.targetSelector.changeTargetButtonTooltipString);
		this.clearDestinationButton.GetComponent<ToolTip>().SetSimpleTooltip(this.targetSelector.clearTargetButtonTooltipString);
	}

	// Token: 0x0600703B RID: 28731 RVA: 0x002AAAF0 File Offset: 0x002A8CF0
	private void Refresh(object data = null)
	{
		if (!this.targetSelector.IsAtDestination())
		{
			ClusterGridEntity clusterEntityTarget = this.targetSelector.GetClusterEntityTarget();
			if (clusterEntityTarget != null)
			{
				this.destinationImage.sprite = clusterEntityTarget.GetUISprite();
				this.destinationLabel.text = this.targetSelector.sidescreenTitleString + ": " + clusterEntityTarget.GetProperName();
			}
			else
			{
				Sprite sprite;
				string text;
				string text2;
				ClusterGrid.Instance.GetLocationDescription(this.targetSelector.GetDestination(), out sprite, out text, out text2);
				this.destinationImage.sprite = sprite;
				this.destinationLabel.text = this.targetSelector.sidescreenTitleString + ": " + text;
			}
			this.clearDestinationButton.isInteractable = true;
		}
		else
		{
			this.destinationImage.sprite = Assets.GetSprite("hex_unknown");
			this.destinationLabel.text = this.targetSelector.sidescreenTitleString + ": " + UI.SPACEDESTINATIONS.NONE.NAME;
			this.clearDestinationButton.isInteractable = false;
		}
		if (this.targetRocketSelector != null)
		{
			List<LaunchPad> launchPadsForDestination = LaunchPad.GetLaunchPadsForDestination(this.targetRocketSelector.GetDestination());
			this.launchPadDropDown.gameObject.SetActive(true);
			this.repeatButton.gameObject.SetActive(true);
			this.launchPadDropDown.Initialize(launchPadsForDestination, new Action<IListableOption, object>(this.OnLaunchPadEntryClick), new Func<IListableOption, IListableOption, object, int>(this.PadDropDownSort), new Action<DropDownEntry, object>(this.PadDropDownEntryRefreshAction), true, this.targetRocketSelector);
			if (!this.targetRocketSelector.IsAtDestination() && launchPadsForDestination.Count > 0)
			{
				this.launchPadDropDown.openButton.isInteractable = true;
				LaunchPad destinationPad = this.targetRocketSelector.GetDestinationPad();
				if (destinationPad != null)
				{
					this.launchPadDropDown.selectedLabel.text = destinationPad.GetProperName();
				}
				else
				{
					this.launchPadDropDown.selectedLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE;
				}
			}
			else
			{
				this.launchPadDropDown.selectedLabel.text = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.FIRSTAVAILABLE;
				this.launchPadDropDown.openButton.isInteractable = false;
			}
			this.StyleRepeatButton();
		}
		else
		{
			this.launchPadDropDown.gameObject.SetActive(false);
			this.repeatButton.gameObject.SetActive(false);
		}
		this.StyleChangeDestinationButton();
	}

	// Token: 0x0600703C RID: 28732 RVA: 0x002AAD4D File Offset: 0x002A8F4D
	private void OnClickChangeDestination()
	{
		if (this.targetSelector.assignable)
		{
			ClusterMapScreen.Instance.ShowInSelectDestinationMode(this.targetSelector);
		}
		this.StyleChangeDestinationButton();
	}

	// Token: 0x0600703D RID: 28733 RVA: 0x002AAD72 File Offset: 0x002A8F72
	private void StyleChangeDestinationButton()
	{
	}

	// Token: 0x0600703E RID: 28734 RVA: 0x002AAD74 File Offset: 0x002A8F74
	private void OnClickClearDestination()
	{
		this.targetSelector.SetDestination(this.targetSelector.GetMyWorldLocation());
	}

	// Token: 0x0600703F RID: 28735 RVA: 0x002AAD8C File Offset: 0x002A8F8C
	private void OnLaunchPadEntryClick(IListableOption option, object data)
	{
		LaunchPad launchPad = (LaunchPad)option;
		this.targetRocketSelector.SetDestinationPad(launchPad);
	}

	// Token: 0x06007040 RID: 28736 RVA: 0x002AADAC File Offset: 0x002A8FAC
	private void PadDropDownEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		LaunchPad launchPad = (LaunchPad)entry.entryData;
		Clustercraft component = this.targetRocketSelector.GetComponent<Clustercraft>();
		if (!(launchPad != null))
		{
			entry.button.isInteractable = true;
			entry.image.sprite = Assets.GetBuildingDef("LaunchPad").GetUISprite("ui", false);
			entry.tooltip.SetSimpleTooltip(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_FIRST_AVAILABLE);
			return;
		}
		string text;
		if (component.CanLandAtPad(launchPad, out text) == Clustercraft.PadLandingStatus.CanNeverLand)
		{
			entry.button.isInteractable = false;
			entry.image.sprite = Assets.GetSprite("iconWarning");
			entry.tooltip.SetSimpleTooltip(text);
			return;
		}
		entry.button.isInteractable = true;
		entry.image.sprite = launchPad.GetComponent<Building>().Def.GetUISprite("ui", false);
		entry.tooltip.SetSimpleTooltip(string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_VALID_SITE, launchPad.GetProperName()));
	}

	// Token: 0x06007041 RID: 28737 RVA: 0x002AAEAB File Offset: 0x002A90AB
	private int PadDropDownSort(IListableOption a, IListableOption b, object targetData)
	{
		return 0;
	}

	// Token: 0x06007042 RID: 28738 RVA: 0x002AAEAE File Offset: 0x002A90AE
	private void OnRepeatClicked()
	{
		this.targetRocketSelector.Repeat = !this.targetRocketSelector.Repeat;
		this.StyleRepeatButton();
	}

	// Token: 0x06007043 RID: 28739 RVA: 0x002AAECF File Offset: 0x002A90CF
	private void StyleRepeatButton()
	{
		this.repeatButton.bgImage.colorStyleSetting = (this.targetRocketSelector.Repeat ? this.repeatOn : this.repeatOff);
		this.repeatButton.bgImage.ApplyColorStyleSetting();
	}

	// Token: 0x04004D2A RID: 19754
	public Image destinationImage;

	// Token: 0x04004D2B RID: 19755
	public LocText destinationLabel;

	// Token: 0x04004D2C RID: 19756
	public KButton changeDestinationButton;

	// Token: 0x04004D2D RID: 19757
	public KButton clearDestinationButton;

	// Token: 0x04004D2E RID: 19758
	public DropDown launchPadDropDown;

	// Token: 0x04004D2F RID: 19759
	public KButton repeatButton;

	// Token: 0x04004D30 RID: 19760
	public ColorStyleSetting repeatOff;

	// Token: 0x04004D31 RID: 19761
	public ColorStyleSetting repeatOn;

	// Token: 0x04004D32 RID: 19762
	public ColorStyleSetting defaultButton;

	// Token: 0x04004D33 RID: 19763
	public ColorStyleSetting highlightButton;

	// Token: 0x04004D36 RID: 19766
	private int m_refreshHandle = -1;
}
