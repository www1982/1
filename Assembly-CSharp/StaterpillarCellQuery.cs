using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E9 RID: 1257
public class StaterpillarCellQuery : PathFinderQuery
{
	// Token: 0x06001ADF RID: 6879 RVA: 0x00094680 File Offset: 0x00092880
	public StaterpillarCellQuery Reset(int max_results, GameObject tester, ObjectLayer conduitLayer)
	{
		this.max_results = max_results;
		this.tester = tester;
		this.result_cells.Clear();
		ObjectLayer objectLayer;
		if (conduitLayer <= ObjectLayer.LiquidConduit)
		{
			if (conduitLayer == ObjectLayer.GasConduit)
			{
				objectLayer = ObjectLayer.GasConduitConnection;
				goto IL_004A;
			}
			if (conduitLayer == ObjectLayer.LiquidConduit)
			{
				objectLayer = ObjectLayer.LiquidConduitConnection;
				goto IL_004A;
			}
		}
		else
		{
			if (conduitLayer == ObjectLayer.SolidConduit)
			{
				objectLayer = ObjectLayer.SolidConduitConnection;
				goto IL_004A;
			}
			if (conduitLayer == ObjectLayer.Wire)
			{
				objectLayer = ObjectLayer.WireConnectors;
				goto IL_004A;
			}
		}
		objectLayer = conduitLayer;
		IL_004A:
		this.connectorLayer = objectLayer;
		return this;
	}

	// Token: 0x06001AE0 RID: 6880 RVA: 0x000946DF File Offset: 0x000928DF
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidRoofCell(cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001AE1 RID: 6881 RVA: 0x0009471C File Offset: 0x0009291C
	private bool CheckValidRoofCell(int testCell)
	{
		if (!this.tester.GetComponent<Navigator>().NavGrid.NavTable.IsValid(testCell, NavType.Ceiling))
		{
			return false;
		}
		int cellInDirection = Grid.GetCellInDirection(testCell, Direction.Down);
		return !Grid.ObjectLayers[1].ContainsKey(testCell) && !Grid.ObjectLayers[1].ContainsKey(cellInDirection) && !Grid.Objects[cellInDirection, (int)this.connectorLayer] && Grid.IsValidBuildingCell(testCell) && Grid.IsValidCell(cellInDirection) && Grid.IsValidBuildingCell(cellInDirection) && !Grid.IsSolidCell(cellInDirection);
	}

	// Token: 0x04000FD5 RID: 4053
	public List<int> result_cells = new List<int>();

	// Token: 0x04000FD6 RID: 4054
	private int max_results;

	// Token: 0x04000FD7 RID: 4055
	private GameObject tester;

	// Token: 0x04000FD8 RID: 4056
	private ObjectLayer connectorLayer;
}
