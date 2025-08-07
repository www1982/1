using System;

// Token: 0x02000E16 RID: 3606
public class OwnablesSidescreenCategoryRow : KMonoBehaviour
{
	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x060071E0 RID: 29152 RVA: 0x002B47D0 File Offset: 0x002B29D0
	private AssignableSlot[] slots
	{
		get
		{
			return this.data.slots;
		}
	}

	// Token: 0x060071E1 RID: 29153 RVA: 0x002B47DD File Offset: 0x002B29DD
	public void SetCategoryData(OwnablesSidescreenCategoryRow.Data categoryData)
	{
		this.DeleteAllRows();
		this.data = categoryData;
		this.titleLabel.text = categoryData.name;
	}

	// Token: 0x060071E2 RID: 29154 RVA: 0x002B47FD File Offset: 0x002B29FD
	public void SetOwner(Assignables owner)
	{
		this.owner = owner;
		if (owner != null)
		{
			this.RecreateAllItemRows();
			return;
		}
		this.DeleteAllRows();
	}

	// Token: 0x060071E3 RID: 29155 RVA: 0x002B481C File Offset: 0x002B2A1C
	private void RecreateAllItemRows()
	{
		this.DeleteAllRows();
		this.itemRows = new OwnablesSidescreenItemRow[this.slots.Length];
		IAssignableIdentity component = this.owner.gameObject.GetComponent<IAssignableIdentity>();
		for (int i = 0; i < this.slots.Length; i++)
		{
			AssignableSlot assignableSlot = this.slots[i];
			this.itemRows[i] = this.CreateRow(assignableSlot, component);
		}
	}

	// Token: 0x060071E4 RID: 29156 RVA: 0x002B4880 File Offset: 0x002B2A80
	private OwnablesSidescreenItemRow CreateRow(AssignableSlot slot, IAssignableIdentity ownerIdentity)
	{
		this.originalItemRow.gameObject.SetActive(false);
		OwnablesSidescreenItemRow component = Util.KInstantiateUI(this.originalItemRow.gameObject, this.originalItemRow.transform.parent.gameObject, false).GetComponent<OwnablesSidescreenItemRow>();
		component.OnSlotRowClicked = (Action<OwnablesSidescreenItemRow>)Delegate.Combine(component.OnSlotRowClicked, new Action<OwnablesSidescreenItemRow>(this.OnRowClicked));
		component.gameObject.SetActive(true);
		component.SetData(this.owner, slot, !this.data.IsSlotApplicable(ownerIdentity, slot));
		return component;
	}

	// Token: 0x060071E5 RID: 29157 RVA: 0x002B4914 File Offset: 0x002B2B14
	private void OnRowClicked(OwnablesSidescreenItemRow row)
	{
		Action<OwnablesSidescreenItemRow> onSlotRowClicked = this.OnSlotRowClicked;
		if (onSlotRowClicked == null)
		{
			return;
		}
		onSlotRowClicked(row);
	}

	// Token: 0x060071E6 RID: 29158 RVA: 0x002B4928 File Offset: 0x002B2B28
	private void DeleteAllRows()
	{
		this.originalItemRow.gameObject.SetActive(false);
		if (this.itemRows != null)
		{
			for (int i = 0; i < this.itemRows.Length; i++)
			{
				this.itemRows[i].ClearData();
				this.itemRows[i].DeleteObject();
			}
			this.itemRows = null;
		}
	}

	// Token: 0x060071E7 RID: 29159 RVA: 0x002B4984 File Offset: 0x002B2B84
	public void SetSelectedRow_VisualsOnly(AssignableSlotInstance slotInstance)
	{
		if (this.itemRows == null)
		{
			return;
		}
		for (int i = 0; i < this.itemRows.Length; i++)
		{
			OwnablesSidescreenItemRow ownablesSidescreenItemRow = this.itemRows[i];
			ownablesSidescreenItemRow.SetSelectedVisualState(ownablesSidescreenItemRow.SlotInstance == slotInstance);
		}
	}

	// Token: 0x04004E5C RID: 20060
	public Action<OwnablesSidescreenItemRow> OnSlotRowClicked;

	// Token: 0x04004E5D RID: 20061
	public LocText titleLabel;

	// Token: 0x04004E5E RID: 20062
	public OwnablesSidescreenItemRow originalItemRow;

	// Token: 0x04004E5F RID: 20063
	private Assignables owner;

	// Token: 0x04004E60 RID: 20064
	private OwnablesSidescreenCategoryRow.Data data;

	// Token: 0x04004E61 RID: 20065
	private OwnablesSidescreenItemRow[] itemRows;

	// Token: 0x0200201E RID: 8222
	public struct AssignableSlotData
	{
		// Token: 0x0600B546 RID: 46406 RVA: 0x003DF417 File Offset: 0x003DD617
		public AssignableSlotData(AssignableSlot slot, Func<IAssignableIdentity, bool> isApplicableCallback)
		{
			this.slot = slot;
			this.IsApplicableCallback = isApplicableCallback;
		}

		// Token: 0x0400931C RID: 37660
		public AssignableSlot slot;

		// Token: 0x0400931D RID: 37661
		public Func<IAssignableIdentity, bool> IsApplicableCallback;
	}

	// Token: 0x0200201F RID: 8223
	public struct Data
	{
		// Token: 0x0600B547 RID: 46407 RVA: 0x003DF428 File Offset: 0x003DD628
		public Data(string name, OwnablesSidescreenCategoryRow.AssignableSlotData[] slotsData)
		{
			this.name = name;
			this.slotsData = slotsData;
			this.slots = new AssignableSlot[slotsData.Length];
			for (int i = 0; i < slotsData.Length; i++)
			{
				this.slots[i] = slotsData[i].slot;
			}
		}

		// Token: 0x0600B548 RID: 46408 RVA: 0x003DF474 File Offset: 0x003DD674
		public bool IsSlotApplicable(IAssignableIdentity identity, AssignableSlot slot)
		{
			for (int i = 0; i < this.slotsData.Length; i++)
			{
				OwnablesSidescreenCategoryRow.AssignableSlotData assignableSlotData = this.slotsData[i];
				if (assignableSlotData.slot == slot)
				{
					return assignableSlotData.IsApplicableCallback(identity);
				}
			}
			return false;
		}

		// Token: 0x0400931E RID: 37662
		public string name;

		// Token: 0x0400931F RID: 37663
		public AssignableSlot[] slots;

		// Token: 0x04009320 RID: 37664
		private OwnablesSidescreenCategoryRow.AssignableSlotData[] slotsData;
	}
}
