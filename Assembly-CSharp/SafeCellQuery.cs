using System;

// Token: 0x020004E7 RID: 1255
public class SafeCellQuery : PathFinderQuery
{
	// Token: 0x06001AD5 RID: 6869 RVA: 0x00093F55 File Offset: 0x00092155
	public SafeCellQuery Reset(MinionBrain brain, bool avoid_light, SafeCellQuery.SafeFlags ignoredFlags = (SafeCellQuery.SafeFlags)0)
	{
		this.brain = brain;
		this.targetCell = PathFinder.InvalidCell;
		this.targetCost = int.MaxValue;
		this.targetCellFlags = (SafeCellQuery.SafeFlags)0;
		this.avoid_light = avoid_light;
		this.ignoredFlags = ignoredFlags;
		return this;
	}

	// Token: 0x06001AD6 RID: 6870 RVA: 0x00093F8C File Offset: 0x0009218C
	public static SafeCellQuery.SafeFlags GetFlags(int cell, MinionBrain brain, bool avoid_light = false, SafeCellQuery.SafeFlags ignoredFlags = (SafeCellQuery.SafeFlags)0)
	{
		int num = Grid.CellAbove(cell);
		if (!Grid.IsValidCell(num))
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		if (Grid.Solid[cell] || Grid.Solid[num])
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		if (Grid.IsTileUnderConstruction[cell] || Grid.IsTileUnderConstruction[num])
		{
			return (SafeCellQuery.SafeFlags)0;
		}
		bool flag = brain.IsCellClear(cell);
		bool flag2 = (ignoredFlags & SafeCellQuery.SafeFlags.IsNotLiquid) != (SafeCellQuery.SafeFlags)0 || !Grid.Element[cell].IsLiquid;
		bool flag3 = (ignoredFlags & SafeCellQuery.SafeFlags.IsNotLiquidOnMyFace) != (SafeCellQuery.SafeFlags)0 || !Grid.Element[num].IsLiquid;
		bool flag4 = (ignoredFlags & SafeCellQuery.SafeFlags.CorrectTemperature) != (SafeCellQuery.SafeFlags)0 || (Grid.Temperature[cell] > 285.15f && Grid.Temperature[cell] < 303.15f);
		bool flag5 = (ignoredFlags & SafeCellQuery.SafeFlags.IsNotRadiated) != (SafeCellQuery.SafeFlags)0 || Grid.Radiation[cell] < 250f;
		bool flag6 = (ignoredFlags & SafeCellQuery.SafeFlags.IsBreathable) != (SafeCellQuery.SafeFlags)0 || brain.OxygenBreather == null || GasBreatherFromWorldProvider.GetBestBreathableCellAroundSpecificCell(cell, Grid.DefaultOffset, brain.OxygenBreather).IsBreathable;
		bool flag7 = !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Ladder) && !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Pole);
		bool flag8 = !brain.Navigator.NavGrid.NavTable.IsValid(cell, NavType.Tube);
		bool flag9 = !avoid_light || SleepChore.IsDarkAtCell(cell);
		if (cell == Grid.PosToCell(brain))
		{
			flag6 = (ignoredFlags & SafeCellQuery.SafeFlags.IsBreathable) != (SafeCellQuery.SafeFlags)0 || brain.OxygenBreather == null || brain.OxygenBreather.HasOxygen;
		}
		SafeCellQuery.SafeFlags safeFlags = (SafeCellQuery.SafeFlags)0;
		if (flag)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsClear;
		}
		if (flag4)
		{
			safeFlags |= SafeCellQuery.SafeFlags.CorrectTemperature;
		}
		if (flag5)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotRadiated;
		}
		if (flag6)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsBreathable;
		}
		if (flag7)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLadder;
		}
		if (flag8)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotTube;
		}
		if (flag2)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLiquid;
		}
		if (flag3)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsNotLiquidOnMyFace;
		}
		if (flag9)
		{
			safeFlags |= SafeCellQuery.SafeFlags.IsLightOk;
		}
		return safeFlags;
	}

	// Token: 0x06001AD7 RID: 6871 RVA: 0x00094194 File Offset: 0x00092394
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		SafeCellQuery.SafeFlags flags = SafeCellQuery.GetFlags(cell, this.brain, this.avoid_light, this.ignoredFlags);
		bool flag = flags > this.targetCellFlags;
		bool flag2 = flags == this.targetCellFlags && cost < this.targetCost;
		if (flag || flag2)
		{
			this.targetCellFlags = flags;
			this.targetCost = cost;
			this.targetCell = cell;
		}
		return false;
	}

	// Token: 0x06001AD8 RID: 6872 RVA: 0x000941F3 File Offset: 0x000923F3
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000FC0 RID: 4032
	private MinionBrain brain;

	// Token: 0x04000FC1 RID: 4033
	private int targetCell;

	// Token: 0x04000FC2 RID: 4034
	private int targetCost;

	// Token: 0x04000FC3 RID: 4035
	public SafeCellQuery.SafeFlags targetCellFlags;

	// Token: 0x04000FC4 RID: 4036
	private bool avoid_light;

	// Token: 0x04000FC5 RID: 4037
	private SafeCellQuery.SafeFlags ignoredFlags;

	// Token: 0x02001338 RID: 4920
	public enum SafeFlags
	{
		// Token: 0x040068CF RID: 26831
		IsClear = 1,
		// Token: 0x040068D0 RID: 26832
		IsLightOk,
		// Token: 0x040068D1 RID: 26833
		IsNotLadder = 4,
		// Token: 0x040068D2 RID: 26834
		IsNotTube = 8,
		// Token: 0x040068D3 RID: 26835
		CorrectTemperature = 16,
		// Token: 0x040068D4 RID: 26836
		IsNotRadiated = 32,
		// Token: 0x040068D5 RID: 26837
		IsBreathable = 64,
		// Token: 0x040068D6 RID: 26838
		IsNotLiquidOnMyFace = 128,
		// Token: 0x040068D7 RID: 26839
		IsNotLiquid = 256
	}
}
