using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000E13 RID: 3603
public class OwnablesSecondSideScreen : KScreen
{
	// Token: 0x170007D8 RID: 2008
	// (get) Token: 0x060071AA RID: 29098 RVA: 0x002B3805 File Offset: 0x002B1A05
	// (set) Token: 0x060071A9 RID: 29097 RVA: 0x002B37FC File Offset: 0x002B19FC
	public AssignableSlotInstance Slot { get; private set; }

	// Token: 0x170007D9 RID: 2009
	// (get) Token: 0x060071AC RID: 29100 RVA: 0x002B3816 File Offset: 0x002B1A16
	// (set) Token: 0x060071AB RID: 29099 RVA: 0x002B380D File Offset: 0x002B1A0D
	public IAssignableIdentity OwnerIdentity { get; private set; }

	// Token: 0x170007DA RID: 2010
	// (get) Token: 0x060071AD RID: 29101 RVA: 0x002B381E File Offset: 0x002B1A1E
	public AssignableSlot SlotType
	{
		get
		{
			if (this.Slot != null)
			{
				return this.Slot.slot;
			}
			return null;
		}
	}

	// Token: 0x170007DB RID: 2011
	// (get) Token: 0x060071AE RID: 29102 RVA: 0x002B3835 File Offset: 0x002B1A35
	public Assignable CurrentSlotItem
	{
		get
		{
			if (!this.HasItem)
			{
				return null;
			}
			return this.Slot.assignable;
		}
	}

	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x060071AF RID: 29103 RVA: 0x002B384C File Offset: 0x002B1A4C
	public bool HasItem
	{
		get
		{
			return this.Slot != null && this.Slot.IsAssigned();
		}
	}

	// Token: 0x060071B0 RID: 29104 RVA: 0x002B3863 File Offset: 0x002B1A63
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.originalRow.gameObject.SetActive(false);
		MultiToggle multiToggle = this.noneRow;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnNoneRowClicked));
	}

	// Token: 0x060071B1 RID: 29105 RVA: 0x002B38A3 File Offset: 0x002B1AA3
	private void OnNoneRowClicked()
	{
		this.UnassignCurrentItem();
		this.RefreshNoneRow();
	}

	// Token: 0x060071B2 RID: 29106 RVA: 0x002B38B1 File Offset: 0x002B1AB1
	protected override void OnCmpDisable()
	{
		this.SetSlot(null);
		base.OnCmpDisable();
	}

	// Token: 0x060071B3 RID: 29107 RVA: 0x002B38C0 File Offset: 0x002B1AC0
	public void SetSlot(AssignableSlotInstance slot)
	{
		Components.AssignableItems.Unregister(new Action<Assignable>(this.OnNewItemAvailable), new Action<Assignable>(this.OnItemUnregistered));
		this.Slot = slot;
		this.OwnerIdentity = ((slot == null) ? null : slot.assignables.GetComponent<IAssignableIdentity>());
		if (this.Slot != null)
		{
			Components.AssignableItems.Register(new Action<Assignable>(this.OnNewItemAvailable), new Action<Assignable>(this.OnItemUnregistered));
		}
		this.RefreshItemListOptions(true);
	}

	// Token: 0x060071B4 RID: 29108 RVA: 0x002B3940 File Offset: 0x002B1B40
	public void SortRows()
	{
		if (this.itemRows != null)
		{
			this.itemRows.Sort((OwnablesSecondSideScreenRow a, OwnablesSecondSideScreenRow b) => string.Compare(UI.StripLinkFormatting(a.nameLabel.text), UI.StripLinkFormatting(b.nameLabel.text)) * -1);
			OwnablesSecondSideScreenRow ownablesSecondSideScreenRow = null;
			for (int i = 0; i < this.itemRows.Count; i++)
			{
				OwnablesSecondSideScreenRow ownablesSecondSideScreenRow2 = this.itemRows[i];
				if (ownablesSecondSideScreenRow2.item == null || ownablesSecondSideScreenRow2.item.IsAssigned())
				{
					if (ownablesSecondSideScreenRow == null && ownablesSecondSideScreenRow2 != null && ownablesSecondSideScreenRow2.item != null && ownablesSecondSideScreenRow2.item.IsAssigned() && ownablesSecondSideScreenRow2.item == this.CurrentSlotItem)
					{
						ownablesSecondSideScreenRow = ownablesSecondSideScreenRow2;
					}
					else
					{
						ownablesSecondSideScreenRow2.transform.SetAsLastSibling();
					}
				}
				else
				{
					ownablesSecondSideScreenRow2.transform.SetAsFirstSibling();
				}
			}
			if (ownablesSecondSideScreenRow != null)
			{
				ownablesSecondSideScreenRow.transform.SetAsFirstSibling();
			}
		}
		this.noneRow.transform.SetAsFirstSibling();
	}

	// Token: 0x060071B5 RID: 29109 RVA: 0x002B3A48 File Offset: 0x002B1C48
	public void RefreshItemListOptions(bool sortRows = false)
	{
		GameObject gameObject = ((this.OwnerIdentity == null) ? null : this.OwnerIdentity.GetOwners()[0].GetComponent<MinionAssignablesProxy>().GetTargetGameObject());
		int worldID = ((this.OwnerIdentity == null) ? 255 : gameObject.GetMyWorldId());
		List<Assignable> list = null;
		int num = 0;
		bool showItemsAssignedToOthers = true;
		if (this.Slot != null && (this.Slot is EquipmentSlotInstance || this.Slot.ID.Contains("BionicUpgrade")))
		{
			showItemsAssignedToOthers = false;
		}
		if (worldID != 255)
		{
			list = Components.AssignableItems.Items.FindAll(delegate(Assignable i)
			{
				bool flag = i.slotID == this.SlotType.Id && i.CanAssignTo(this.OwnerIdentity);
				if (flag && i is Equippable)
				{
					Equippable equippable = i as Equippable;
					GameObject gameObject2 = equippable.gameObject;
					if (equippable.isEquipped)
					{
						gameObject2 = equippable.assignee.GetOwners()[0].GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
					}
					flag = flag && gameObject2.GetMyWorldId() == worldID;
				}
				bool flag2 = i.assignee != null && i.assignee.GetSoleOwner() == this.OwnerIdentity.GetSoleOwner();
				bool flag3 = flag2 && this.Slot.assignable == i;
				if (!showItemsAssignedToOthers)
				{
					if (i.assignee != null && !flag2)
					{
						flag = false;
					}
					if (flag2 && !flag3)
					{
						flag = false;
					}
				}
				return flag;
			});
			num = list.Count;
		}
		for (int j = 0; j < Mathf.Max(this.itemRows.Count, num); j++)
		{
			if (list != null && j < list.Count)
			{
				Assignable assignable = list[j];
				if (j >= this.itemRows.Count)
				{
					OwnablesSecondSideScreenRow ownablesSecondSideScreenRow = this.CreateItemRow(assignable);
					this.itemRows.Add(ownablesSecondSideScreenRow);
				}
				OwnablesSecondSideScreenRow ownablesSecondSideScreenRow2 = this.itemRows[j];
				ownablesSecondSideScreenRow2.gameObject.SetActive(true);
				ownablesSecondSideScreenRow2.SetData(this.Slot, assignable);
			}
			else
			{
				OwnablesSecondSideScreenRow ownablesSecondSideScreenRow3 = this.itemRows[j];
				ownablesSecondSideScreenRow3.ClearData();
				ownablesSecondSideScreenRow3.gameObject.SetActive(false);
			}
		}
		if (sortRows)
		{
			this.SortRows();
		}
		this.RefreshNoneRow();
	}

	// Token: 0x060071B6 RID: 29110 RVA: 0x002B3BCC File Offset: 0x002B1DCC
	private void RefreshNoneRow()
	{
		this.noneRow.ChangeState(this.HasItem ? 0 : 1);
	}

	// Token: 0x060071B7 RID: 29111 RVA: 0x002B3BE8 File Offset: 0x002B1DE8
	private OwnablesSecondSideScreenRow CreateItemRow(Assignable item)
	{
		OwnablesSecondSideScreenRow component = Util.KInstantiateUI(this.originalRow.gameObject, this.originalRow.transform.parent.gameObject, false).GetComponent<OwnablesSecondSideScreenRow>();
		component.OnRowClicked = (Action<OwnablesSecondSideScreenRow>)Delegate.Combine(component.OnRowClicked, new Action<OwnablesSecondSideScreenRow>(this.OnItemRowClicked));
		component.OnRowItemAssigneeChanged = (Action<OwnablesSecondSideScreenRow>)Delegate.Combine(component.OnRowItemAssigneeChanged, new Action<OwnablesSecondSideScreenRow>(this.OnItemRowAsigneeChanged));
		component.OnRowItemDestroyed = (Action<OwnablesSecondSideScreenRow>)Delegate.Combine(component.OnRowItemDestroyed, new Action<OwnablesSecondSideScreenRow>(this.OnItemDestroyed));
		return component;
	}

	// Token: 0x060071B8 RID: 29112 RVA: 0x002B3C86 File Offset: 0x002B1E86
	private void OnItemDestroyed(OwnablesSecondSideScreenRow correspondingItemRow)
	{
		correspondingItemRow.ClearData();
		correspondingItemRow.gameObject.SetActive(false);
	}

	// Token: 0x060071B9 RID: 29113 RVA: 0x002B3C9A File Offset: 0x002B1E9A
	private void OnItemRowAsigneeChanged(OwnablesSecondSideScreenRow correspondingItemRow)
	{
		correspondingItemRow.Refresh();
		this.RefreshNoneRow();
	}

	// Token: 0x060071BA RID: 29114 RVA: 0x002B3CA8 File Offset: 0x002B1EA8
	private void OnItemRowClicked(OwnablesSecondSideScreenRow rowClicked)
	{
		Assignable item = rowClicked.item;
		bool flag = item.IsAssigned() && item.assignee is AssignmentGroup;
		bool flag2 = item.IsAssigned() && item.IsAssignedTo(this.OwnerIdentity) && !flag && this.Slot.IsAssigned() && this.Slot.assignable == item;
		if (item.IsAssigned())
		{
			item.Unassign();
		}
		if (!flag2)
		{
			item.Assign(this.OwnerIdentity, this.Slot);
		}
		rowClicked.Refresh();
		this.RefreshNoneRow();
	}

	// Token: 0x060071BB RID: 29115 RVA: 0x002B3D3E File Offset: 0x002B1F3E
	private void UnassignCurrentItem()
	{
		if (this.Slot != null)
		{
			this.Slot.Unassign(true);
			this.RefreshItemListOptions(false);
		}
	}

	// Token: 0x060071BC RID: 29116 RVA: 0x002B3D5B File Offset: 0x002B1F5B
	private void OnNewItemAvailable(Assignable item)
	{
		if (this.Slot != null && item.slotID == this.SlotType.Id)
		{
			this.RefreshItemListOptions(false);
		}
	}

	// Token: 0x060071BD RID: 29117 RVA: 0x002B3D84 File Offset: 0x002B1F84
	private void OnItemUnregistered(Assignable item)
	{
		if (this.Slot != null && item.slotID == this.SlotType.Id)
		{
			this.RefreshItemListOptions(false);
		}
	}

	// Token: 0x04004E36 RID: 20022
	public MultiToggle noneRow;

	// Token: 0x04004E37 RID: 20023
	public OwnablesSecondSideScreenRow originalRow;

	// Token: 0x04004E3A RID: 20026
	public global::System.Action OnScreenDeactivated;

	// Token: 0x04004E3B RID: 20027
	private List<OwnablesSecondSideScreenRow> itemRows = new List<OwnablesSecondSideScreenRow>();
}
