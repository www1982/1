using System;
using KSerialization;

// Token: 0x02000705 RID: 1797
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitElementSensor : ConduitSensor
{
	// Token: 0x06002D12 RID: 11538 RVA: 0x00102976 File Offset: 0x00100B76
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.filterable.onFilterChanged += this.OnFilterChanged;
		this.OnFilterChanged(this.filterable.SelectedTag);
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x001029A8 File Offset: 0x00100BA8
	private void OnFilterChanged(Tag tag)
	{
		if (!tag.IsValid)
		{
			return;
		}
		bool flag = tag == GameTags.Void;
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoFilterElementSelected, flag, null);
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x001029E8 File Offset: 0x00100BE8
	protected override void ConduitUpdate(float dt)
	{
		Tag tag;
		bool flag;
		this.GetContentsElement(out tag, out flag);
		if (!base.IsSwitchedOn)
		{
			if (tag == this.filterable.SelectedTag && flag)
			{
				this.Toggle();
				return;
			}
		}
		else if (tag != this.filterable.SelectedTag || !flag)
		{
			this.Toggle();
		}
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x00102A40 File Offset: 0x00100C40
	private void GetContentsElement(out Tag element, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			element = contents.element.CreateTag();
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		KPrefabID kprefabID = ((pickupable != null) ? pickupable.GetComponent<KPrefabID>() : null);
		if (kprefabID != null && pickupable.PrimaryElement.Mass > 0f)
		{
			element = kprefabID.PrefabTag;
			hasMass = true;
			return;
		}
		element = GameTags.Void;
		hasMass = false;
	}

	// Token: 0x04001A87 RID: 6791
	[MyCmpGet]
	private Filterable filterable;
}
