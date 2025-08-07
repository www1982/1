using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000E17 RID: 3607
public class OwnablesSidescreenItemRow : KMonoBehaviour
{
	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x060071EA RID: 29162 RVA: 0x002B49D4 File Offset: 0x002B2BD4
	// (set) Token: 0x060071E9 RID: 29161 RVA: 0x002B49CB File Offset: 0x002B2BCB
	public bool IsLocked { get; private set; }

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x060071EB RID: 29163 RVA: 0x002B49DC File Offset: 0x002B2BDC
	public bool SlotIsAssigned
	{
		get
		{
			return this.Slot != null && this.SlotInstance != null && !this.SlotInstance.IsUnassigning() && this.SlotInstance.IsAssigned();
		}
	}

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x060071ED RID: 29165 RVA: 0x002B4A11 File Offset: 0x002B2C11
	// (set) Token: 0x060071EC RID: 29164 RVA: 0x002B4A08 File Offset: 0x002B2C08
	public AssignableSlotInstance SlotInstance { get; private set; }

	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x060071EF RID: 29167 RVA: 0x002B4A22 File Offset: 0x002B2C22
	// (set) Token: 0x060071EE RID: 29166 RVA: 0x002B4A19 File Offset: 0x002B2C19
	public AssignableSlot Slot { get; private set; }

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x060071F1 RID: 29169 RVA: 0x002B4A33 File Offset: 0x002B2C33
	// (set) Token: 0x060071F0 RID: 29168 RVA: 0x002B4A2A File Offset: 0x002B2C2A
	public Assignables Owner { get; private set; }

	// Token: 0x060071F2 RID: 29170 RVA: 0x002B4A3B File Offset: 0x002B2C3B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.toggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnRowClicked));
		this.SetSelectedVisualState(false);
	}

	// Token: 0x060071F3 RID: 29171 RVA: 0x002B4A71 File Offset: 0x002B2C71
	private void OnRowClicked()
	{
		Action<OwnablesSidescreenItemRow> onSlotRowClicked = this.OnSlotRowClicked;
		if (onSlotRowClicked == null)
		{
			return;
		}
		onSlotRowClicked(this);
	}

	// Token: 0x060071F4 RID: 29172 RVA: 0x002B4A84 File Offset: 0x002B2C84
	public void SetLockState(bool locked)
	{
		this.IsLocked = locked;
		this.Refresh();
	}

	// Token: 0x060071F5 RID: 29173 RVA: 0x002B4A94 File Offset: 0x002B2C94
	public void SetData(Assignables owner, AssignableSlot slot, bool IsLocked)
	{
		if (this.Owner != null)
		{
			this.ClearData();
		}
		this.Owner = owner;
		this.Slot = slot;
		this.SlotInstance = owner.GetSlot(slot);
		this.subscribe_IDX = this.Owner.Subscribe(-1585839766, delegate(object o)
		{
			this.Refresh();
		});
		this.SetLockState(IsLocked);
		if (!IsLocked)
		{
			this.Refresh();
		}
	}

	// Token: 0x060071F6 RID: 29174 RVA: 0x002B4B04 File Offset: 0x002B2D04
	public void ClearData()
	{
		if (this.Owner != null && this.subscribe_IDX != -1)
		{
			this.Owner.Unsubscribe(this.subscribe_IDX);
		}
		this.Owner = null;
		this.Slot = null;
		this.SlotInstance = null;
		this.IsLocked = false;
		this.subscribe_IDX = -1;
		this.DisplayAsEmpty();
	}

	// Token: 0x060071F7 RID: 29175 RVA: 0x002B4B62 File Offset: 0x002B2D62
	private void Refresh()
	{
		if (this.IsNullOrDestroyed())
		{
			return;
		}
		if (this.IsLocked)
		{
			this.DisplayAsLocked();
			return;
		}
		if (!this.SlotIsAssigned)
		{
			this.DisplayAsEmpty();
			return;
		}
		this.DisplayAsOccupied();
	}

	// Token: 0x060071F8 RID: 29176 RVA: 0x002B4B94 File Offset: 0x002B2D94
	public void SetSelectedVisualState(bool shouldDisplayAsSelected)
	{
		int num = (shouldDisplayAsSelected ? 1 : 0);
		this.toggle.ChangeState(num);
	}

	// Token: 0x060071F9 RID: 29177 RVA: 0x002B4BB8 File Offset: 0x002B2DB8
	private void DisplayAsOccupied()
	{
		Assignable assignable = this.SlotInstance.assignable;
		string properName = assignable.GetProperName();
		string text = this.Slot.Name + ": " + properName;
		this.textLabel.SetText(text);
		this.itemIcon.sprite = Def.GetUISprite(assignable.gameObject, "ui", false).first;
		this.itemIcon.gameObject.SetActive(true);
		this.lockedIcon.gameObject.SetActive(false);
		InfoDescription component = assignable.gameObject.GetComponent<InfoDescription>();
		string text2 = string.Format(UI.UISIDESCREENS.OWNABLESSIDESCREEN.TOOLTIPS.ITEM_ASSIGNED_GENERIC, properName);
		if (component != null && !string.IsNullOrEmpty(component.description))
		{
			text2 = string.Format(UI.UISIDESCREENS.OWNABLESSIDESCREEN.TOOLTIPS.ITEM_ASSIGNED, properName, component.description);
		}
		this.tooltip.SetSimpleTooltip(text2);
	}

	// Token: 0x060071FA RID: 29178 RVA: 0x002B4C98 File Offset: 0x002B2E98
	private void DisplayAsEmpty()
	{
		this.textLabel.SetText(((this.Slot != null) ? (this.Slot.Name + ": ") : "") + OwnablesSidescreenItemRow.EMPTY_TEXT);
		this.lockedIcon.gameObject.SetActive(false);
		this.itemIcon.sprite = null;
		this.itemIcon.gameObject.SetActive(false);
		this.tooltip.SetSimpleTooltip((this.Slot != null) ? string.Format(UI.UISIDESCREENS.OWNABLESSIDESCREEN.TOOLTIPS.NO_ITEM_ASSIGNED, this.Slot.Name) : null);
	}

	// Token: 0x060071FB RID: 29179 RVA: 0x002B4D3C File Offset: 0x002B2F3C
	private void DisplayAsLocked()
	{
		this.lockedIcon.gameObject.SetActive(true);
		this.itemIcon.sprite = null;
		this.itemIcon.gameObject.SetActive(false);
		this.textLabel.SetText(string.Format(UI.UISIDESCREENS.OWNABLESSIDESCREEN.NO_APPLICABLE, this.Slot.Name));
		this.tooltip.SetSimpleTooltip(string.Format(UI.UISIDESCREENS.OWNABLESSIDESCREEN.TOOLTIPS.NO_APPLICABLE, this.Slot.Name));
	}

	// Token: 0x060071FC RID: 29180 RVA: 0x002B4DC1 File Offset: 0x002B2FC1
	protected override void OnCleanUp()
	{
		this.ClearData();
	}

	// Token: 0x04004E62 RID: 20066
	private static string EMPTY_TEXT = UI.UISIDESCREENS.OWNABLESSIDESCREEN.NO_ITEM_ASSIGNED;

	// Token: 0x04004E63 RID: 20067
	public KImage lockedIcon;

	// Token: 0x04004E64 RID: 20068
	public KImage itemIcon;

	// Token: 0x04004E65 RID: 20069
	public LocText textLabel;

	// Token: 0x04004E66 RID: 20070
	public ToolTip tooltip;

	// Token: 0x04004E67 RID: 20071
	[Header("Icon settings")]
	public KImage frameOuterBorder;

	// Token: 0x04004E68 RID: 20072
	public Action<OwnablesSidescreenItemRow> OnSlotRowClicked;

	// Token: 0x04004E6D RID: 20077
	public MultiToggle toggle;

	// Token: 0x04004E6E RID: 20078
	private int subscribe_IDX = -1;
}
