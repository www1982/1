using System;
using UnityEngine;

// Token: 0x020009B8 RID: 2488
public class LogicPortVisualizer : ILogicUIElement, IUniformGridObject
{
	// Token: 0x06004896 RID: 18582 RVA: 0x001A3510 File Offset: 0x001A1710
	public LogicPortVisualizer(int cell, LogicPortSpriteType sprite_type)
	{
		this.cell = cell;
		this.spriteType = sprite_type;
	}

	// Token: 0x06004897 RID: 18583 RVA: 0x001A3526 File Offset: 0x001A1726
	public int GetLogicUICell()
	{
		return this.cell;
	}

	// Token: 0x06004898 RID: 18584 RVA: 0x001A352E File Offset: 0x001A172E
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06004899 RID: 18585 RVA: 0x001A3540 File Offset: 0x001A1740
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x0600489A RID: 18586 RVA: 0x001A3552 File Offset: 0x001A1752
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x04002FE3 RID: 12259
	private int cell;

	// Token: 0x04002FE4 RID: 12260
	private LogicPortSpriteType spriteType;
}
