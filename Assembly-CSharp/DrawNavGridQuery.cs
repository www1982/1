using System;
using UnityEngine;

// Token: 0x020004DE RID: 1246
public class DrawNavGridQuery : PathFinderQuery
{
	// Token: 0x06001AB4 RID: 6836 RVA: 0x0009346E File Offset: 0x0009166E
	public DrawNavGridQuery Reset(MinionBrain brain)
	{
		return this;
	}

	// Token: 0x06001AB5 RID: 6837 RVA: 0x00093474 File Offset: 0x00091674
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (parent_cell == Grid.InvalidCell || (int)Grid.WorldIdx[parent_cell] != ClusterManager.Instance.activeWorldId || (int)Grid.WorldIdx[cell] != ClusterManager.Instance.activeWorldId)
		{
			return false;
		}
		GL.Color(Color.white);
		GL.Vertex(Grid.CellToPosCCC(parent_cell, Grid.SceneLayer.Move));
		GL.Vertex(Grid.CellToPosCCC(cell, Grid.SceneLayer.Move));
		return false;
	}
}
