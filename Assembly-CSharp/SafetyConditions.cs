using System;
using System.Collections.Generic;

// Token: 0x020004E4 RID: 1252
public class SafetyConditions
{
	// Token: 0x06001ACC RID: 6860 RVA: 0x00093A34 File Offset: 0x00091C34
	public SafetyConditions()
	{
		int num = 1;
		this.IsNearby = new SafetyChecker.Condition("IsNearby", num *= 2, (int cell, int cost, SafetyChecker.Context context) => cost > 5);
		this.IsNotLedge = new SafetyChecker.Condition("IsNotLedge", num *= 2, delegate(int cell, int cost, SafetyChecker.Context context)
		{
			int num2 = Grid.CellBelow(Grid.CellLeft(cell));
			if (Grid.Solid[num2])
			{
				return false;
			}
			int num3 = Grid.CellBelow(Grid.CellRight(cell));
			return Grid.Solid[num3];
		});
		this.IsNotLiquid = new SafetyChecker.Condition("IsNotLiquid", num *= 2, (int cell, int cost, SafetyChecker.Context context) => !Grid.Element[cell].IsLiquid);
		this.IsNotCoveredInLiquid = new SafetyChecker.Condition("IsNotCoveredInLiquid", num *= 2, delegate(int cell, int cost, SafetyChecker.Context context)
		{
			int num4 = Grid.CellAbove(cell);
			return Grid.IsValidCell(num4) && (!Grid.Element[cell].IsLiquid || !Grid.Element[num4].IsLiquid);
		});
		this.IsNotLadder = new SafetyChecker.Condition("IsNotLadder", num *= 2, (int cell, int cost, SafetyChecker.Context context) => !context.navigator.NavGrid.NavTable.IsValid(cell, NavType.Ladder) && !context.navigator.NavGrid.NavTable.IsValid(cell, NavType.Pole));
		this.IsNotDoor = new SafetyChecker.Condition("IsNotDoor", num *= 2, delegate(int cell, int cost, SafetyChecker.Context context)
		{
			int num5 = Grid.CellAbove(cell);
			return !Grid.HasDoor[cell] && Grid.IsValidCell(num5) && !Grid.HasDoor[num5];
		});
		this.IsCorrectTemperature = new SafetyChecker.Condition("IsCorrectTemperature", num *= 2, (int cell, int cost, SafetyChecker.Context context) => Grid.Temperature[cell] > 285.15f && Grid.Temperature[cell] < 303.15f);
		this.IsWarming = new SafetyChecker.Condition("IsWarming", num *= 2, (int cell, int cost, SafetyChecker.Context context) => WarmthProvider.IsWarmCell(cell));
		this.IsCooling = new SafetyChecker.Condition("IsCooling", num *= 2, (int cell, int cost, SafetyChecker.Context context) => false);
		this.HasSomeOxygen = new SafetyChecker.Condition("HasSomeOxygen", num *= 2, (int cell, int cost, SafetyChecker.Context context) => context.oxygenBreather == null || GasBreatherFromWorldProvider.GetBestBreathableCellAroundSpecificCell(cell, Grid.DefaultOffset, context.oxygenBreather).IsBreathable);
		this.HasSomeOxygenAround = new SafetyChecker.Condition("HasSomeOxygenAround", num *= 2, (int cell, int cost, SafetyChecker.Context context) => context.oxygenBreather == null || GasBreatherFromWorldProvider.GetBestBreathableCellAroundSpecificCell(cell, GasBreatherFromWorldProvider.DEFAULT_BREATHABLE_OFFSETS, context.oxygenBreather).IsBreathable);
		this.IsClear = new SafetyChecker.Condition("IsClear", num * 2, (int cell, int cost, SafetyChecker.Context context) => context.minionBrain.IsCellClear(cell));
		this.WarmUpChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsWarming }.ToArray());
		this.CoolDownChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsCooling }.ToArray());
		this.AbsorbCellCellChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsNotCoveredInLiquid, this.IsNotDoor, this.HasSomeOxygenAround }.ToArray());
		List<SafetyChecker.Condition> list = new List<SafetyChecker.Condition>();
		list.Add(this.HasSomeOxygen);
		list.Add(this.IsNotDoor);
		this.RecoverBreathChecker = new SafetyChecker(list.ToArray());
		List<SafetyChecker.Condition> list2 = new List<SafetyChecker.Condition>(list);
		list2.Add(this.IsNotLiquid);
		list2.Add(this.IsCorrectTemperature);
		this.SafeCellChecker = new SafetyChecker(list2.ToArray());
		this.IdleCellChecker = new SafetyChecker(new List<SafetyChecker.Condition>(list2) { this.IsClear, this.IsNotLadder }.ToArray());
		this.VomitCellChecker = new SafetyChecker(new List<SafetyChecker.Condition> { this.IsNotLiquid, this.IsNotLedge, this.IsNearby }.ToArray());
	}

	// Token: 0x04000FA4 RID: 4004
	public SafetyChecker.Condition IsNotLiquid;

	// Token: 0x04000FA5 RID: 4005
	public SafetyChecker.Condition IsNotCoveredInLiquid;

	// Token: 0x04000FA6 RID: 4006
	public SafetyChecker.Condition IsNotLadder;

	// Token: 0x04000FA7 RID: 4007
	public SafetyChecker.Condition IsCorrectTemperature;

	// Token: 0x04000FA8 RID: 4008
	public SafetyChecker.Condition IsWarming;

	// Token: 0x04000FA9 RID: 4009
	public SafetyChecker.Condition IsCooling;

	// Token: 0x04000FAA RID: 4010
	public SafetyChecker.Condition HasSomeOxygen;

	// Token: 0x04000FAB RID: 4011
	public SafetyChecker.Condition HasSomeOxygenAround;

	// Token: 0x04000FAC RID: 4012
	public SafetyChecker.Condition IsClear;

	// Token: 0x04000FAD RID: 4013
	public SafetyChecker.Condition IsNotFoundation;

	// Token: 0x04000FAE RID: 4014
	public SafetyChecker.Condition IsNotDoor;

	// Token: 0x04000FAF RID: 4015
	public SafetyChecker.Condition IsNotLedge;

	// Token: 0x04000FB0 RID: 4016
	public SafetyChecker.Condition IsNearby;

	// Token: 0x04000FB1 RID: 4017
	public SafetyChecker WarmUpChecker;

	// Token: 0x04000FB2 RID: 4018
	public SafetyChecker CoolDownChecker;

	// Token: 0x04000FB3 RID: 4019
	public SafetyChecker RecoverBreathChecker;

	// Token: 0x04000FB4 RID: 4020
	public SafetyChecker AbsorbCellCellChecker;

	// Token: 0x04000FB5 RID: 4021
	public SafetyChecker VomitCellChecker;

	// Token: 0x04000FB6 RID: 4022
	public SafetyChecker SafeCellChecker;

	// Token: 0x04000FB7 RID: 4023
	public SafetyChecker IdleCellChecker;
}
