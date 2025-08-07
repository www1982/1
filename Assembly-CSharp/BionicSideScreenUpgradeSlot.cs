using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DD9 RID: 3545
public class BionicSideScreenUpgradeSlot : KMonoBehaviour
{
	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x06006FEE RID: 28654 RVA: 0x002A9636 File Offset: 0x002A7836
	// (set) Token: 0x06006FED RID: 28653 RVA: 0x002A962D File Offset: 0x002A782D
	public BionicUpgradesMonitor.UpgradeComponentSlot upgradeSlot { get; private set; }

	// Token: 0x06006FEF RID: 28655 RVA: 0x002A963E File Offset: 0x002A783E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnSlotClicked));
	}

	// Token: 0x06006FF0 RID: 28656 RVA: 0x002A9670 File Offset: 0x002A7870
	public void Setup(BionicUpgradesMonitor.UpgradeComponentSlot upgradeSlot)
	{
		if (this.upgradeSlot != null)
		{
			BionicUpgradesMonitor.UpgradeComponentSlot upgradeSlot2 = this.upgradeSlot;
			upgradeSlot2.OnAssignedUpgradeChanged = (Action<BionicUpgradesMonitor.UpgradeComponentSlot>)Delegate.Remove(upgradeSlot2.OnAssignedUpgradeChanged, new Action<BionicUpgradesMonitor.UpgradeComponentSlot>(this.OnAssignedUpgradeChanged));
		}
		this.upgradeSlot = upgradeSlot;
		if (upgradeSlot != null)
		{
			upgradeSlot.OnAssignedUpgradeChanged = (Action<BionicUpgradesMonitor.UpgradeComponentSlot>)Delegate.Combine(upgradeSlot.OnAssignedUpgradeChanged, new Action<BionicUpgradesMonitor.UpgradeComponentSlot>(this.OnAssignedUpgradeChanged));
		}
		this.Refresh();
	}

	// Token: 0x06006FF1 RID: 28657 RVA: 0x002A96DE File Offset: 0x002A78DE
	private void OnAssignedUpgradeChanged(BionicUpgradesMonitor.UpgradeComponentSlot slot)
	{
		this.Refresh();
	}

	// Token: 0x06006FF2 RID: 28658 RVA: 0x002A96E8 File Offset: 0x002A78E8
	public void Refresh()
	{
		this.label.color = this.standardColor;
		BionicSideScreenUpgradeSlot.State state = (this.upgradeSlot.IsLocked ? BionicSideScreenUpgradeSlot.State.Locked : BionicSideScreenUpgradeSlot.State.Empty);
		if (state == BionicSideScreenUpgradeSlot.State.Empty && this.upgradeSlot.HasUpgradeInstalled)
		{
			state = BionicSideScreenUpgradeSlot.State.Installed;
		}
		else if (state == BionicSideScreenUpgradeSlot.State.Empty && this.upgradeSlot.HasUpgradeComponentAssigned && !this.upgradeSlot.GetAssignableSlotInstance().IsUnassigning())
		{
			state = BionicSideScreenUpgradeSlot.State.Assigned;
		}
		switch (state)
		{
		case BionicSideScreenUpgradeSlot.State.Locked:
			this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.DynamicWidthNoWrap;
			this.tooltip.SetSimpleTooltip(BionicSideScreenUpgradeSlot.TEXT_TOOLTIP_BLOCKED);
			this.label.SetText(BionicSideScreenUpgradeSlot.TEXT_BLOCKED_SLOT);
			this.label.Opacity(0.5f);
			this.icon.gameObject.SetActive(false);
			break;
		case BionicSideScreenUpgradeSlot.State.Empty:
			this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.DynamicWidthNoWrap;
			this.tooltip.SetSimpleTooltip(BionicSideScreenUpgradeSlot.TEXT_TOOLTIP_EMPTY);
			this.label.SetText(BionicSideScreenUpgradeSlot.TEXT_NO_UPGRADE_INSTALLED);
			this.label.Opacity(1f);
			this.icon.gameObject.SetActive(false);
			break;
		case BionicSideScreenUpgradeSlot.State.Assigned:
			this.icon.sprite = Def.GetUISprite(this.upgradeSlot.assignedUpgradeComponent.gameObject, "ui", false).first;
			this.icon.Opacity(0.5f);
			this.icon.gameObject.SetActive(true);
			this.label.SetText(BionicSideScreenUpgradeSlot.TEXT_UPGRADE_ASSIGNED_NOT_INSTALLED);
			this.label.Opacity(1f);
			this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
			this.tooltip.SetSimpleTooltip(string.Format(BionicSideScreenUpgradeSlot.TEXT_TOOLTIP_ASSIGNED, this.upgradeSlot.assignedUpgradeComponent.GetProperName()));
			break;
		case BionicSideScreenUpgradeSlot.State.Installed:
			this.icon.sprite = Def.GetUISprite(this.upgradeSlot.installedUpgradeComponent.gameObject, "ui", false).first;
			this.icon.Opacity(1f);
			this.icon.gameObject.SetActive(true);
			this.label.SetText(BionicSideScreenUpgradeSlot.TEXT_UPGRADE_INSTALLED);
			this.label.Opacity(1f);
			this.tooltip.SizingSetting = ToolTip.ToolTipSizeSetting.MaxWidthWrapContent;
			this.tooltip.SetSimpleTooltip(string.Format(BionicSideScreenUpgradeSlot.TEXT_TOOLTIP_INSTALLED, BionicUpgradeComponentConfig.GenerateTooltipForBooster(this.upgradeSlot.installedUpgradeComponent)));
			break;
		}
		this.SetSelected(this._isSelected);
	}

	// Token: 0x06006FF3 RID: 28659 RVA: 0x002A995C File Offset: 0x002A7B5C
	private void OnSlotClicked()
	{
		Action<BionicSideScreenUpgradeSlot> onClick = this.OnClick;
		if (onClick == null)
		{
			return;
		}
		onClick(this);
	}

	// Token: 0x06006FF4 RID: 28660 RVA: 0x002A9970 File Offset: 0x002A7B70
	public void SetSelected(bool isSelected)
	{
		this._isSelected = isSelected;
		bool flag = this.upgradeSlot == null || this.upgradeSlot.IsLocked;
		bool flag2 = this.upgradeSlot != null && this.upgradeSlot.HasUpgradeComponentAssigned && !this.upgradeSlot.GetAssignableSlotInstance().IsUnassigning();
		bool flag3 = flag2 && this.upgradeSlot.assignedUpgradeComponent.Booster == BionicUpgradeComponentConfig.BoosterType.Basic;
		this.toggle.ChangeState((flag ? 0 : 2) + (flag2 ? 2 : 0) + ((flag2 && flag3) ? 2 : 0) + (isSelected ? 1 : 0));
	}

	// Token: 0x04004CFB RID: 19707
	public static string TEXT_BLOCKED_SLOT = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.UPGRADE_SLOT_LOCKED;

	// Token: 0x04004CFC RID: 19708
	public static string TEXT_NO_UPGRADE_INSTALLED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.UPGRADE_SLOT_EMPTY;

	// Token: 0x04004CFD RID: 19709
	public static string TEXT_UPGRADE_ASSIGNED_NOT_INSTALLED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.UPGRADE_SLOT_ASSIGNED;

	// Token: 0x04004CFE RID: 19710
	public static string TEXT_UPGRADE_INSTALLED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.UPGRADE_SLOT_INSTALLED;

	// Token: 0x04004CFF RID: 19711
	public static string TEXT_TOOLTIP_BLOCKED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.TOOLTIP.SLOT_LOCKED;

	// Token: 0x04004D00 RID: 19712
	public static string TEXT_TOOLTIP_EMPTY = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.TOOLTIP.SLOT_EMPTY;

	// Token: 0x04004D01 RID: 19713
	public static string TEXT_TOOLTIP_ASSIGNED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.TOOLTIP.SLOT_ASSIGNED;

	// Token: 0x04004D02 RID: 19714
	public static string TEXT_TOOLTIP_INSTALLED = UI.UISIDESCREENS.BIONIC_SIDE_SCREEN.TOOLTIP.SLOT_INSTALLED;

	// Token: 0x04004D03 RID: 19715
	public MultiToggle toggle;

	// Token: 0x04004D04 RID: 19716
	public KImage icon;

	// Token: 0x04004D05 RID: 19717
	public LocText label;

	// Token: 0x04004D06 RID: 19718
	public ToolTip tooltip;

	// Token: 0x04004D07 RID: 19719
	[Header("Effects settings")]
	public float inUseAnimationDuration = 0.5f;

	// Token: 0x04004D08 RID: 19720
	public Color standardColor = Color.black;

	// Token: 0x04004D09 RID: 19721
	public Color activeColor = Color.blue;

	// Token: 0x04004D0A RID: 19722
	public Color activeColorTooltip = Color.blue;

	// Token: 0x04004D0B RID: 19723
	public Action<BionicSideScreenUpgradeSlot> OnClick;

	// Token: 0x04004D0D RID: 19725
	private bool _isSelected;

	// Token: 0x02001FF4 RID: 8180
	public enum State
	{
		// Token: 0x040092AB RID: 37547
		Locked,
		// Token: 0x040092AC RID: 37548
		Empty,
		// Token: 0x040092AD RID: 37549
		Assigned,
		// Token: 0x040092AE RID: 37550
		Installed
	}
}
