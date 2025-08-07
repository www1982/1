using System;
using UnityEngine;

// Token: 0x020009B4 RID: 2484
internal class LogicEventSender : ILogicEventSender, ILogicNetworkConnection, ILogicUIElement, IUniformGridObject
{
	// Token: 0x06004851 RID: 18513 RVA: 0x001A1BAA File Offset: 0x0019FDAA
	public LogicEventSender(HashedString id, int cell, Action<int, int> on_value_changed, Action<int, bool> on_connection_changed, LogicPortSpriteType sprite_type)
	{
		this.id = id;
		this.cell = cell;
		this.onValueChanged = on_value_changed;
		this.onConnectionChanged = on_connection_changed;
		this.spriteType = sprite_type;
	}

	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x06004852 RID: 18514 RVA: 0x001A1BDF File Offset: 0x0019FDDF
	public HashedString ID
	{
		get
		{
			return this.id;
		}
	}

	// Token: 0x06004853 RID: 18515 RVA: 0x001A1BE7 File Offset: 0x0019FDE7
	public int GetLogicCell()
	{
		return this.cell;
	}

	// Token: 0x06004854 RID: 18516 RVA: 0x001A1BEF File Offset: 0x0019FDEF
	public int GetLogicValue()
	{
		return this.logicValue;
	}

	// Token: 0x06004855 RID: 18517 RVA: 0x001A1BF7 File Offset: 0x0019FDF7
	public int GetLogicUICell()
	{
		return this.GetLogicCell();
	}

	// Token: 0x06004856 RID: 18518 RVA: 0x001A1BFF File Offset: 0x0019FDFF
	public LogicPortSpriteType GetLogicPortSpriteType()
	{
		return this.spriteType;
	}

	// Token: 0x06004857 RID: 18519 RVA: 0x001A1C07 File Offset: 0x0019FE07
	public Vector2 PosMin()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06004858 RID: 18520 RVA: 0x001A1C19 File Offset: 0x0019FE19
	public Vector2 PosMax()
	{
		return Grid.CellToPos2D(this.cell);
	}

	// Token: 0x06004859 RID: 18521 RVA: 0x001A1C2C File Offset: 0x0019FE2C
	public void SetValue(int value)
	{
		int num = this.logicValue;
		this.logicValue = value;
		this.onValueChanged(value, num);
	}

	// Token: 0x0600485A RID: 18522 RVA: 0x001A1C54 File Offset: 0x0019FE54
	public void LogicTick()
	{
	}

	// Token: 0x0600485B RID: 18523 RVA: 0x001A1C56 File Offset: 0x0019FE56
	public void OnLogicNetworkConnectionChanged(bool connected)
	{
		if (this.onConnectionChanged != null)
		{
			this.onConnectionChanged(this.cell, connected);
		}
	}

	// Token: 0x04002FBF RID: 12223
	private HashedString id;

	// Token: 0x04002FC0 RID: 12224
	private int cell;

	// Token: 0x04002FC1 RID: 12225
	private int logicValue = -16;

	// Token: 0x04002FC2 RID: 12226
	private Action<int, int> onValueChanged;

	// Token: 0x04002FC3 RID: 12227
	private Action<int, bool> onConnectionChanged;

	// Token: 0x04002FC4 RID: 12228
	private LogicPortSpriteType spriteType;
}
