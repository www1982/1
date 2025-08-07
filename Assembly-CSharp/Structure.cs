using System;
using UnityEngine;

// Token: 0x0200061F RID: 1567
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Structure")]
public class Structure : KMonoBehaviour
{
	// Token: 0x060025F4 RID: 9716 RVA: 0x000D920A File Offset: 0x000D740A
	public bool IsEntombed()
	{
		return this.isEntombed;
	}

	// Token: 0x060025F5 RID: 9717 RVA: 0x000D9214 File Offset: 0x000D7414
	public static bool IsBuildingEntombed(Building building)
	{
		if (!Grid.IsValidCell(Grid.PosToCell(building)))
		{
			return false;
		}
		for (int i = 0; i < building.PlacementCells.Length; i++)
		{
			int num = building.PlacementCells[i];
			if (Grid.Element[num].IsSolid && !Grid.Foundation[num])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060025F6 RID: 9718 RVA: 0x000D926C File Offset: 0x000D746C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Extents extents = this.building.GetExtents();
		this.partitionerEntry = GameScenePartitioner.Instance.Add("Structure.OnSpawn", base.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.OnSolidChanged(null);
		base.Subscribe<Structure>(-887025858, Structure.RocketLandedDelegate);
	}

	// Token: 0x060025F7 RID: 9719 RVA: 0x000D92D5 File Offset: 0x000D74D5
	public void UpdatePosition()
	{
		GameScenePartitioner.Instance.UpdatePosition(this.partitionerEntry, this.building.GetExtents());
	}

	// Token: 0x060025F8 RID: 9720 RVA: 0x000D92F2 File Offset: 0x000D74F2
	private void RocketChanged(object data)
	{
		this.OnSolidChanged(data);
	}

	// Token: 0x060025F9 RID: 9721 RVA: 0x000D92FC File Offset: 0x000D74FC
	private void OnSolidChanged(object data)
	{
		bool flag = Structure.IsBuildingEntombed(this.building);
		if (flag != this.isEntombed)
		{
			this.isEntombed = flag;
			if (this.isEntombed)
			{
				base.GetComponent<KPrefabID>().AddTag(GameTags.Entombed, false);
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.Entombed);
			}
			this.operational.SetFlag(Structure.notEntombedFlag, !this.isEntombed);
			base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.Entombed, this.isEntombed, this);
			base.Trigger(-1089732772, null);
		}
	}

	// Token: 0x060025FA RID: 9722 RVA: 0x000D9397 File Offset: 0x000D7597
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
	}

	// Token: 0x04001651 RID: 5713
	[MyCmpReq]
	private Building building;

	// Token: 0x04001652 RID: 5714
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04001653 RID: 5715
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001654 RID: 5716
	public static readonly Operational.Flag notEntombedFlag = new Operational.Flag("not_entombed", Operational.Flag.Type.Functional);

	// Token: 0x04001655 RID: 5717
	private bool isEntombed;

	// Token: 0x04001656 RID: 5718
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001657 RID: 5719
	private static EventSystem.IntraObjectHandler<Structure> RocketLandedDelegate = new EventSystem.IntraObjectHandler<Structure>(delegate(Structure cmp, object data)
	{
		cmp.RocketChanged(data);
	});
}
