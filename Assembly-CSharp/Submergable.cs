using System;
using UnityEngine;

// Token: 0x02000621 RID: 1569
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Submergable")]
public class Submergable : KMonoBehaviour
{
	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06002611 RID: 9745 RVA: 0x000D983C File Offset: 0x000D7A3C
	public bool IsSubmerged
	{
		get
		{
			return this.isSubmerged;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06002612 RID: 9746 RVA: 0x000D9844 File Offset: 0x000D7A44
	public BuildingDef Def
	{
		get
		{
			return this.building.Def;
		}
	}

	// Token: 0x06002613 RID: 9747 RVA: 0x000D9854 File Offset: 0x000D7A54
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Submergable.OnSpawn", base.gameObject, this.building.GetExtents(), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnElementChanged));
		this.OnElementChanged(null);
		this.operational.SetFlag(Submergable.notSubmergedFlag, this.isSubmerged);
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NotSubmerged, !this.isSubmerged, this);
	}

	// Token: 0x06002614 RID: 9748 RVA: 0x000D98E8 File Offset: 0x000D7AE8
	private void OnElementChanged(object data)
	{
		bool flag = true;
		for (int i = 0; i < this.building.PlacementCells.Length; i++)
		{
			if (!Grid.IsLiquid(this.building.PlacementCells[i]))
			{
				flag = false;
				break;
			}
		}
		if (flag != this.isSubmerged)
		{
			this.isSubmerged = flag;
			this.operational.SetFlag(Submergable.notSubmergedFlag, this.isSubmerged);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NotSubmerged, !this.isSubmerged, this);
		}
	}

	// Token: 0x06002615 RID: 9749 RVA: 0x000D9972 File Offset: 0x000D7B72
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x04001661 RID: 5729
	[MyCmpReq]
	private Building building;

	// Token: 0x04001662 RID: 5730
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04001663 RID: 5731
	[MyCmpGet]
	private SimCellOccupier simCellOccupier;

	// Token: 0x04001664 RID: 5732
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001665 RID: 5733
	public static Operational.Flag notSubmergedFlag = new Operational.Flag("submerged", Operational.Flag.Type.Functional);

	// Token: 0x04001666 RID: 5734
	private bool isSubmerged;

	// Token: 0x04001667 RID: 5735
	private HandleVector<int>.Handle partitionerEntry;
}
