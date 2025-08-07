using System;

// Token: 0x020004D0 RID: 1232
public class NavTableValidator
{
	// Token: 0x06001A69 RID: 6761 RVA: 0x0009204C File Offset: 0x0009024C
	protected bool IsClear(int cell, CellOffset[] bounding_offsets, bool is_dupe)
	{
		foreach (CellOffset cellOffset in bounding_offsets)
		{
			int num = Grid.OffsetCell(cell, cellOffset);
			if (!Grid.IsWorldValidCell(num) || !NavTableValidator.IsCellPassable(num, is_dupe))
			{
				return false;
			}
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && Grid.Element[num2].IsUnstable)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06001A6A RID: 6762 RVA: 0x000920AC File Offset: 0x000902AC
	protected static bool IsCellPassable(int cell, bool is_dupe)
	{
		Grid.BuildFlags buildFlags = Grid.BuildMasks[cell] & ~(Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.FakeFloor);
		if (buildFlags == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor))
		{
			return true;
		}
		if (is_dupe)
		{
			return (buildFlags & Grid.BuildFlags.DupeImpassable) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) && ((buildFlags & Grid.BuildFlags.Solid) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor) || (buildFlags & Grid.BuildFlags.DupePassable) > ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor));
		}
		return (buildFlags & (Grid.BuildFlags.Solid | Grid.BuildFlags.CritterImpassable)) == ~(Grid.BuildFlags.Solid | Grid.BuildFlags.Foundation | Grid.BuildFlags.Door | Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable | Grid.BuildFlags.CritterImpassable | Grid.BuildFlags.FakeFloor);
	}

	// Token: 0x06001A6B RID: 6763 RVA: 0x000920E9 File Offset: 0x000902E9
	public virtual void UpdateCell(int cell, NavTable nav_table, CellOffset[] bounding_offsets)
	{
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x000920EB File Offset: 0x000902EB
	public virtual void Clear()
	{
	}

	// Token: 0x04000F4D RID: 3917
	public Action<int> onDirty;
}
