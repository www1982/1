using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D8 RID: 1240
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/PathProber")]
public class PathProber : KMonoBehaviour
{
	// Token: 0x06001A9B RID: 6811 RVA: 0x00093003 File Offset: 0x00091203
	protected override void OnCleanUp()
	{
		if (this.PathGrid != null)
		{
			this.PathGrid.OnCleanUp();
		}
		base.OnCleanUp();
	}

	// Token: 0x06001A9C RID: 6812 RVA: 0x0009301E File Offset: 0x0009121E
	public void SetGroupProber(IGroupProber group_prober)
	{
		this.PathGrid.SetGroupProber(group_prober);
	}

	// Token: 0x06001A9D RID: 6813 RVA: 0x0009302C File Offset: 0x0009122C
	public void SetValidNavTypes(NavType[] nav_types, int max_probing_radius)
	{
		if (max_probing_radius != 0)
		{
			this.PathGrid = new PathGrid(max_probing_radius * 2, max_probing_radius * 2, true, nav_types);
			return;
		}
		this.PathGrid = new PathGrid(Grid.WidthInCells, Grid.HeightInCells, false, nav_types);
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x0009305C File Offset: 0x0009125C
	public int GetCost(int cell)
	{
		return this.PathGrid.GetCost(cell);
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x0009306A File Offset: 0x0009126A
	public int GetNavigationCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		return this.PathGrid.GetCostIgnoreProberOffset(cell, offsets);
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x00093079 File Offset: 0x00091279
	public PathGrid GetPathGrid()
	{
		return this.PathGrid;
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x00093084 File Offset: 0x00091284
	public void UpdateProbe(NavGrid nav_grid, int cell, NavType nav_type, PathFinderAbilities abilities, PathFinder.PotentialPath.Flags flags)
	{
		if (this.scratchPad == null)
		{
			this.scratchPad = new PathFinder.PotentialScratchPad(nav_grid.maxLinksPerCell);
		}
		bool flag = this.updateCount == -1;
		bool flag2 = this.Potentials.Count == 0 || flag;
		this.PathGrid.BeginUpdate(cell, !flag2);
		if (flag2)
		{
			this.updateCount = 0;
			bool flag3;
			PathFinder.Cell cell2 = this.PathGrid.GetCell(cell, nav_type, out flag3);
			PathFinder.AddPotential(new PathFinder.PotentialPath(cell, nav_type, flags), Grid.InvalidCell, NavType.NumNavTypes, 0, 0, this.Potentials, this.PathGrid, ref cell2);
		}
		int num = ((this.potentialCellsPerUpdate <= 0 || flag) ? int.MaxValue : this.potentialCellsPerUpdate);
		this.updateCount++;
		while (this.Potentials.Count > 0 && num > 0)
		{
			KeyValuePair<int, PathFinder.PotentialPath> keyValuePair = this.Potentials.Next();
			num--;
			bool flag3;
			PathFinder.Cell cell3 = this.PathGrid.GetCell(keyValuePair.Value, out flag3);
			if (cell3.cost == keyValuePair.Key)
			{
				PathFinder.AddPotentials(this.scratchPad, keyValuePair.Value, cell3.cost, ref abilities, null, nav_grid.maxLinksPerCell, nav_grid.Links, this.Potentials, this.PathGrid, cell3.parent, cell3.parentNavType);
			}
		}
		bool flag4 = this.Potentials.Count == 0;
		this.PathGrid.EndUpdate(flag4);
		if (flag4)
		{
			int num2 = this.updateCount;
		}
	}

	// Token: 0x04000F7F RID: 3967
	public const int InvalidHandle = -1;

	// Token: 0x04000F80 RID: 3968
	public const int InvalidIdx = -1;

	// Token: 0x04000F81 RID: 3969
	public const int InvalidCell = -1;

	// Token: 0x04000F82 RID: 3970
	public const int InvalidCost = -1;

	// Token: 0x04000F83 RID: 3971
	private PathGrid PathGrid;

	// Token: 0x04000F84 RID: 3972
	private PathFinder.PotentialList Potentials = new PathFinder.PotentialList();

	// Token: 0x04000F85 RID: 3973
	public int updateCount = -1;

	// Token: 0x04000F86 RID: 3974
	private const int updateCountThreshold = 25;

	// Token: 0x04000F87 RID: 3975
	private PathFinder.PotentialScratchPad scratchPad;

	// Token: 0x04000F88 RID: 3976
	public int potentialCellsPerUpdate = -1;
}
