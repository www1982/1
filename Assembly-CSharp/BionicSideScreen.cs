using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DD8 RID: 3544
public class BionicSideScreen : SideScreenContent
{
	// Token: 0x06006FD9 RID: 28633 RVA: 0x002A9094 File Offset: 0x002A7294
	private void OnBionicUpgradeSlotClicked(BionicSideScreenUpgradeSlot slotClicked)
	{
		bool flag = slotClicked == null || this.lastSlotSelected == slotClicked.upgradeSlot.GetAssignableSlotInstance();
		bool flag2 = !flag && slotClicked.upgradeSlot.IsLocked;
		this.lastSlotSelected = (flag ? null : slotClicked.upgradeSlot.GetAssignableSlotInstance());
		this.RefreshSelectedStateInSlots();
		AssignableSlot bionicUpgrade = Db.Get().AssignableSlots.BionicUpgrade;
		AssignableSlotInstance assignableSlotInstance = ((flag || flag2) ? null : slotClicked.upgradeSlot.GetAssignableSlotInstance());
		if (this.ownableSidescreen != null)
		{
			this.ownableSidescreen.SetSelectedSlot(assignableSlotInstance);
			return;
		}
		if (flag || flag2)
		{
			DetailsScreen.Instance.ClearSecondarySideScreen();
			return;
		}
		((OwnablesSecondSideScreen)DetailsScreen.Instance.SetSecondarySideScreen(this.ownableSecondSideScreenPrefab, bionicUpgrade.Name)).SetSlot(assignableSlotInstance);
	}

	// Token: 0x06006FDA RID: 28634 RVA: 0x002A9160 File Offset: 0x002A7360
	private void RefreshSelectedStateInSlots()
	{
		for (int i = 0; i < this.bionicSlots.Count; i++)
		{
			BionicSideScreenUpgradeSlot bionicSideScreenUpgradeSlot = this.bionicSlots[i];
			bionicSideScreenUpgradeSlot.SetSelected(bionicSideScreenUpgradeSlot.upgradeSlot.GetAssignableSlotInstance() == this.lastSlotSelected);
		}
	}

	// Token: 0x06006FDB RID: 28635 RVA: 0x002A91A8 File Offset: 0x002A73A8
	public void RecreateBionicSlots()
	{
		int num = ((this.upgradeMonitor != null) ? this.upgradeMonitor.upgradeComponentSlots.Length : 0);
		for (int i = 0; i < Mathf.Max(num, this.bionicSlots.Count); i++)
		{
			if (i >= this.bionicSlots.Count)
			{
				BionicSideScreenUpgradeSlot bionicSideScreenUpgradeSlot = this.CreateBionicSlot();
				this.bionicSlots.Add(bionicSideScreenUpgradeSlot);
			}
			BionicSideScreenUpgradeSlot bionicSideScreenUpgradeSlot2 = this.bionicSlots[i];
			if (i < num)
			{
				BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot = this.upgradeMonitor.upgradeComponentSlots[i];
				bionicSideScreenUpgradeSlot2.gameObject.SetActive(true);
				bionicSideScreenUpgradeSlot2.Setup(upgradeComponentSlot);
				bionicSideScreenUpgradeSlot2.SetSelected(bionicSideScreenUpgradeSlot2.upgradeSlot.GetAssignableSlotInstance() == this.lastSlotSelected);
			}
			else
			{
				bionicSideScreenUpgradeSlot2.Setup(null);
				bionicSideScreenUpgradeSlot2.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06006FDC RID: 28636 RVA: 0x002A9274 File Offset: 0x002A7474
	private BionicSideScreenUpgradeSlot CreateBionicSlot()
	{
		BionicSideScreenUpgradeSlot bionicSideScreenUpgradeSlot = Util.KInstantiateUI<BionicSideScreenUpgradeSlot>(this.originalBionicSlot.gameObject, this.originalBionicSlot.transform.parent.gameObject, false);
		bionicSideScreenUpgradeSlot.OnClick = (Action<BionicSideScreenUpgradeSlot>)Delegate.Combine(bionicSideScreenUpgradeSlot.OnClick, new Action<BionicSideScreenUpgradeSlot>(this.OnBionicUpgradeSlotClicked));
		return bionicSideScreenUpgradeSlot;
	}

	// Token: 0x06006FDD RID: 28637 RVA: 0x002A92C9 File Offset: 0x002A74C9
	private void OnBionicBecameOnline(object o)
	{
		this.RefreshSlots();
	}

	// Token: 0x06006FDE RID: 28638 RVA: 0x002A92D1 File Offset: 0x002A74D1
	private void OnBionicBecameOffline(object o)
	{
		this.RefreshSlots();
	}

	// Token: 0x06006FDF RID: 28639 RVA: 0x002A92D9 File Offset: 0x002A74D9
	private void OnBionicWattageChanged(object o)
	{
		this.RefreshSlots();
	}

	// Token: 0x06006FE0 RID: 28640 RVA: 0x002A92E1 File Offset: 0x002A74E1
	private void OnBionicBedTimeChoreStateChanged(object o)
	{
		this.RefreshSlots();
	}

	// Token: 0x06006FE1 RID: 28641 RVA: 0x002A92E9 File Offset: 0x002A74E9
	private void OnBionicUpgradeComponentSlotCountChanged(object o)
	{
		this.RefreshSlots();
	}

	// Token: 0x06006FE2 RID: 28642 RVA: 0x002A92F1 File Offset: 0x002A74F1
	private void OnBionicUpgradeChanged(object o)
	{
		this.RecreateBionicSlots();
	}

	// Token: 0x06006FE3 RID: 28643 RVA: 0x002A92F9 File Offset: 0x002A74F9
	private void OnBionicTagsChanged(object o)
	{
		if (o == null)
		{
			return;
		}
		if (((TagChangedEventData)o).tag == GameTags.BionicBedTime)
		{
			this.OnBionicBedTimeChoreStateChanged(o);
		}
	}

	// Token: 0x06006FE4 RID: 28644 RVA: 0x002A9320 File Offset: 0x002A7520
	private void RefreshSlots()
	{
		for (int i = this.bionicSlots.Count - 1; i >= 0; i--)
		{
			BionicSideScreenUpgradeSlot bionicSideScreenUpgradeSlot = this.bionicSlots[i];
			if (bionicSideScreenUpgradeSlot != null)
			{
				bionicSideScreenUpgradeSlot.Refresh();
				bionicSideScreenUpgradeSlot.gameObject.transform.SetAsFirstSibling();
			}
		}
	}

	// Token: 0x06006FE5 RID: 28645 RVA: 0x002A9374 File Offset: 0x002A7574
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.originalBionicSlot.gameObject.SetActive(false);
		this.ownableSidescreen = base.transform.parent.GetComponentInChildren<OwnablesSidescreen>();
		if (this.ownableSidescreen != null)
		{
			OwnablesSidescreen ownablesSidescreen = this.ownableSidescreen;
			ownablesSidescreen.OnSlotInstanceSelected = (Action<AssignableSlotInstance>)Delegate.Combine(ownablesSidescreen.OnSlotInstanceSelected, new Action<AssignableSlotInstance>(this.OnOwnableSidescreenRowSelected));
		}
	}

	// Token: 0x06006FE6 RID: 28646 RVA: 0x002A93E3 File Offset: 0x002A75E3
	private void OnOwnableSidescreenRowSelected(AssignableSlotInstance slot)
	{
		this.lastSlotSelected = slot;
		this.RefreshSelectedStateInSlots();
	}

	// Token: 0x06006FE7 RID: 28647 RVA: 0x002A93F4 File Offset: 0x002A75F4
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.lastSlotSelected = null;
		if (this.upgradeMonitor != null)
		{
			this.upgradeMonitor.Unsubscribe(160824499, new Action<object>(this.OnBionicBecameOnline));
			this.upgradeMonitor.Unsubscribe(-1730800797, new Action<object>(this.OnBionicBecameOffline));
			this.upgradeMonitor.Unsubscribe(2000325176, new Action<object>(this.OnBionicUpgradeChanged));
			this.upgradeMonitor.Unsubscribe(1095596132, new Action<object>(this.OnBionicUpgradeComponentSlotCountChanged));
		}
		if (this.batteryMonitor != null)
		{
			this.batteryMonitor.Unsubscribe(1361471071, new Action<object>(this.OnBionicWattageChanged));
		}
		if (this.bedTimeMonitor != null)
		{
			this.bedTimeMonitor.Unsubscribe(-1582839653, new Action<object>(this.OnBionicTagsChanged));
		}
		this.batteryMonitor = target.GetSMI<BionicBatteryMonitor.Instance>();
		this.upgradeMonitor = target.GetSMI<BionicUpgradesMonitor.Instance>();
		this.bedTimeMonitor = target.GetSMI<BionicBedTimeMonitor.Instance>();
		this.upgradeMonitor.Subscribe(160824499, new Action<object>(this.OnBionicBecameOnline));
		this.upgradeMonitor.Subscribe(-1730800797, new Action<object>(this.OnBionicBecameOffline));
		this.upgradeMonitor.Subscribe(2000325176, new Action<object>(this.OnBionicUpgradeChanged));
		this.batteryMonitor.Subscribe(1095596132, new Action<object>(this.OnBionicUpgradeComponentSlotCountChanged));
		this.batteryMonitor.Subscribe(1361471071, new Action<object>(this.OnBionicWattageChanged));
		this.bedTimeMonitor.Subscribe(-1582839653, new Action<object>(this.OnBionicTagsChanged));
		this.RecreateBionicSlots();
		this.RefreshSlots();
	}

	// Token: 0x06006FE8 RID: 28648 RVA: 0x002A95A7 File Offset: 0x002A77A7
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.RefreshSlots();
		}
	}

	// Token: 0x06006FE9 RID: 28649 RVA: 0x002A95BC File Offset: 0x002A77BC
	public override void ClearTarget()
	{
		base.ClearTarget();
		if (this.upgradeMonitor != null)
		{
			this.upgradeMonitor.Unsubscribe(2000325176, new Action<object>(this.OnBionicUpgradeChanged));
		}
		this.bedTimeMonitor = null;
		this.upgradeMonitor = null;
		this.lastSlotSelected = null;
	}

	// Token: 0x06006FEA RID: 28650 RVA: 0x002A9608 File Offset: 0x002A7808
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetSMI<BionicBatteryMonitor.Instance>() != null;
	}

	// Token: 0x06006FEB RID: 28651 RVA: 0x002A9613 File Offset: 0x002A7813
	public override int GetSideScreenSortOrder()
	{
		return 300;
	}

	// Token: 0x04004CF3 RID: 19699
	public OwnablesSecondSideScreen ownableSecondSideScreenPrefab;

	// Token: 0x04004CF4 RID: 19700
	public BionicSideScreenUpgradeSlot originalBionicSlot;

	// Token: 0x04004CF5 RID: 19701
	private BionicUpgradesMonitor.Instance upgradeMonitor;

	// Token: 0x04004CF6 RID: 19702
	private BionicBatteryMonitor.Instance batteryMonitor;

	// Token: 0x04004CF7 RID: 19703
	private BionicBedTimeMonitor.Instance bedTimeMonitor;

	// Token: 0x04004CF8 RID: 19704
	private List<BionicSideScreenUpgradeSlot> bionicSlots = new List<BionicSideScreenUpgradeSlot>();

	// Token: 0x04004CF9 RID: 19705
	private OwnablesSidescreen ownableSidescreen;

	// Token: 0x04004CFA RID: 19706
	private AssignableSlotInstance lastSlotSelected;
}
