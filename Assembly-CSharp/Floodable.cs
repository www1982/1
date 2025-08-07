using System;
using UnityEngine;

// Token: 0x020005BA RID: 1466
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Floodable")]
public class Floodable : KMonoBehaviour
{
	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060021CC RID: 8652 RVA: 0x000C3A17 File Offset: 0x000C1C17
	public bool IsFlooded
	{
		get
		{
			return this.isFlooded;
		}
	}

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x060021CD RID: 8653 RVA: 0x000C3A1F File Offset: 0x000C1C1F
	public BuildingDef Def
	{
		get
		{
			return this.building.Def;
		}
	}

	// Token: 0x060021CE RID: 8654 RVA: 0x000C3A2C File Offset: 0x000C1C2C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Floodable.OnSpawn", base.gameObject, this.building.GetExtents(), GameScenePartitioner.Instance.liquidChangedLayer, new Action<object>(this.OnElementChanged));
		this.OnElementChanged(null);
	}

	// Token: 0x060021CF RID: 8655 RVA: 0x000C3A84 File Offset: 0x000C1C84
	private void OnElementChanged(object data)
	{
		bool flag = false;
		for (int i = 0; i < this.building.PlacementCells.Length; i++)
		{
			if (Grid.IsSubstantialLiquid(this.building.PlacementCells[i], 0.35f))
			{
				flag = true;
				break;
			}
		}
		if (flag != this.isFlooded)
		{
			this.isFlooded = flag;
			this.operational.SetFlag(Floodable.notFloodedFlag, !this.isFlooded);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.Flooded, this.isFlooded, this);
		}
	}

	// Token: 0x060021D0 RID: 8656 RVA: 0x000C3B13 File Offset: 0x000C1D13
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x040013C0 RID: 5056
	[MyCmpReq]
	private Building building;

	// Token: 0x040013C1 RID: 5057
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x040013C2 RID: 5058
	[MyCmpGet]
	private SimCellOccupier simCellOccupier;

	// Token: 0x040013C3 RID: 5059
	[MyCmpReq]
	private Operational operational;

	// Token: 0x040013C4 RID: 5060
	public static Operational.Flag notFloodedFlag = new Operational.Flag("not_flooded", Operational.Flag.Type.Functional);

	// Token: 0x040013C5 RID: 5061
	private bool isFlooded;

	// Token: 0x040013C6 RID: 5062
	private HandleVector<int>.Handle partitionerEntry;
}
