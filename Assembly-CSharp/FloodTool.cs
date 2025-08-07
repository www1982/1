using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000976 RID: 2422
public class FloodTool : InterfaceTool
{
	// Token: 0x060045F0 RID: 17904 RVA: 0x00192C78 File Offset: 0x00190E78
	public HashSet<int> Flood(int startCell)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> hashSet2 = new HashSet<int>();
		GameUtil.FloodFillConditional(startCell, this.floodCriteria, hashSet, hashSet2);
		return hashSet2;
	}

	// Token: 0x060045F1 RID: 17905 RVA: 0x00192CA0 File Offset: 0x00190EA0
	public override void OnLeftClickDown(Vector3 cursor_pos)
	{
		base.OnLeftClickDown(cursor_pos);
		this.paintArea(this.Flood(Grid.PosToCell(cursor_pos)));
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x00192CC0 File Offset: 0x00190EC0
	public override void OnMouseMove(Vector3 cursor_pos)
	{
		base.OnMouseMove(cursor_pos);
		this.mouseCell = Grid.PosToCell(cursor_pos);
	}

	// Token: 0x04002E83 RID: 11907
	public Func<int, bool> floodCriteria;

	// Token: 0x04002E84 RID: 11908
	public Action<HashSet<int>> paintArea;

	// Token: 0x04002E85 RID: 11909
	protected Color32 areaColour = new Color(0.5f, 0.7f, 0.5f, 0.2f);

	// Token: 0x04002E86 RID: 11910
	protected int mouseCell = -1;
}
