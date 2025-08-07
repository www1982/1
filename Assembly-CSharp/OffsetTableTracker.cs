using System;

// Token: 0x02000A3F RID: 2623
public class OffsetTableTracker : OffsetTracker
{
	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06004C29 RID: 19497 RVA: 0x001BA239 File Offset: 0x001B8439
	private static NavGrid navGrid
	{
		get
		{
			if (OffsetTableTracker.navGridImpl == null)
			{
				OffsetTableTracker.navGridImpl = Pathfinding.Instance.GetNavGrid("MinionNavGrid");
			}
			return OffsetTableTracker.navGridImpl;
		}
	}

	// Token: 0x06004C2A RID: 19498 RVA: 0x001BA25B File Offset: 0x001B845B
	public OffsetTableTracker(CellOffset[][] table, KMonoBehaviour cmp)
	{
		this.table = table;
		this.cmp = cmp;
	}

	// Token: 0x06004C2B RID: 19499 RVA: 0x001BA274 File Offset: 0x001B8474
	protected override void UpdateCell(int previous_cell, int current_cell)
	{
		if (previous_cell == current_cell)
		{
			return;
		}
		base.UpdateCell(previous_cell, current_cell);
		Extents extents = new Extents(current_cell, this.table);
		extents.height += 2;
		extents.y--;
		if (!this.solidPartitionerEntry.IsValid())
		{
			this.solidPartitionerEntry = GameScenePartitioner.Instance.Add("OffsetTableTracker.UpdateCell", this.cmp.gameObject, extents, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnCellChanged));
			this.validNavCellChangedPartitionerEntry = GameScenePartitioner.Instance.Add("OffsetTableTracker.UpdateCell", this.cmp.gameObject, extents, GameScenePartitioner.Instance.validNavCellChangedLayer, new Action<object>(this.OnCellChanged));
		}
		else
		{
			GameScenePartitioner.Instance.UpdatePosition(this.solidPartitionerEntry, extents);
			GameScenePartitioner.Instance.UpdatePosition(this.validNavCellChangedPartitionerEntry, extents);
		}
		this.offsets = null;
	}

	// Token: 0x06004C2C RID: 19500 RVA: 0x001BA35C File Offset: 0x001B855C
	private static bool IsValidRow(int current_cell, CellOffset[] row, int rowIdx, int[] debugIdxs)
	{
		for (int i = 1; i < row.Length; i++)
		{
			int num = Grid.OffsetCell(current_cell, row[i]);
			if (!Grid.IsValidCell(num))
			{
				return false;
			}
			if (Grid.Solid[num])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004C2D RID: 19501 RVA: 0x001BA3A0 File Offset: 0x001B85A0
	private void UpdateOffsets(int cell, CellOffset[][] table)
	{
		HashSetPool<CellOffset, OffsetTableTracker>.PooledHashSet pooledHashSet = HashSetPool<CellOffset, OffsetTableTracker>.Allocate();
		if (Grid.IsValidCell(cell))
		{
			for (int i = 0; i < table.Length; i++)
			{
				CellOffset[] array = table[i];
				if (!pooledHashSet.Contains(array[0]))
				{
					int num = Grid.OffsetCell(cell, array[0]);
					for (int j = 0; j < OffsetTableTracker.navGrid.ValidNavTypes.Length; j++)
					{
						NavType navType = OffsetTableTracker.navGrid.ValidNavTypes[j];
						if (navType != NavType.Tube && OffsetTableTracker.navGrid.NavTable.IsValid(num, navType) && OffsetTableTracker.IsValidRow(cell, array, i, this.DEBUG_rowValidIdx))
						{
							pooledHashSet.Add(array[0]);
							break;
						}
					}
				}
			}
		}
		if (this.offsets == null || this.offsets.Length != pooledHashSet.Count)
		{
			this.offsets = new CellOffset[pooledHashSet.Count];
		}
		pooledHashSet.CopyTo(this.offsets);
		pooledHashSet.Recycle();
	}

	// Token: 0x06004C2E RID: 19502 RVA: 0x001BA491 File Offset: 0x001B8691
	protected override void UpdateOffsets(int current_cell)
	{
		base.UpdateOffsets(current_cell);
		this.UpdateOffsets(current_cell, this.table);
	}

	// Token: 0x06004C2F RID: 19503 RVA: 0x001BA4A7 File Offset: 0x001B86A7
	private void OnCellChanged(object data)
	{
		this.offsets = null;
	}

	// Token: 0x06004C30 RID: 19504 RVA: 0x001BA4B0 File Offset: 0x001B86B0
	public override void Clear()
	{
		GameScenePartitioner.Instance.Free(ref this.solidPartitionerEntry);
		GameScenePartitioner.Instance.Free(ref this.validNavCellChangedPartitionerEntry);
	}

	// Token: 0x06004C31 RID: 19505 RVA: 0x001BA4D2 File Offset: 0x001B86D2
	public static void OnPathfindingInvalidated()
	{
		OffsetTableTracker.navGridImpl = null;
	}

	// Token: 0x0400327C RID: 12924
	private readonly CellOffset[][] table;

	// Token: 0x0400327D RID: 12925
	public HandleVector<int>.Handle solidPartitionerEntry;

	// Token: 0x0400327E RID: 12926
	public HandleVector<int>.Handle validNavCellChangedPartitionerEntry;

	// Token: 0x0400327F RID: 12927
	private static NavGrid navGridImpl;

	// Token: 0x04003280 RID: 12928
	private KMonoBehaviour cmp;

	// Token: 0x04003281 RID: 12929
	private int[] DEBUG_rowValidIdx;
}
