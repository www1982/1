using System;
using UnityEngine;

// Token: 0x02000D1C RID: 3356
public readonly struct Alignment
{
	// Token: 0x06006755 RID: 26453 RVA: 0x0026F9FD File Offset: 0x0026DBFD
	public Alignment(float x, float y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06006756 RID: 26454 RVA: 0x0026FA0D File Offset: 0x0026DC0D
	public static Alignment Custom(float x, float y)
	{
		return new Alignment(x, y);
	}

	// Token: 0x06006757 RID: 26455 RVA: 0x0026FA16 File Offset: 0x0026DC16
	public static Alignment TopLeft()
	{
		return new Alignment(0f, 1f);
	}

	// Token: 0x06006758 RID: 26456 RVA: 0x0026FA27 File Offset: 0x0026DC27
	public static Alignment Top()
	{
		return new Alignment(0.5f, 1f);
	}

	// Token: 0x06006759 RID: 26457 RVA: 0x0026FA38 File Offset: 0x0026DC38
	public static Alignment TopRight()
	{
		return new Alignment(1f, 1f);
	}

	// Token: 0x0600675A RID: 26458 RVA: 0x0026FA49 File Offset: 0x0026DC49
	public static Alignment Left()
	{
		return new Alignment(0f, 0.5f);
	}

	// Token: 0x0600675B RID: 26459 RVA: 0x0026FA5A File Offset: 0x0026DC5A
	public static Alignment Center()
	{
		return new Alignment(0.5f, 0.5f);
	}

	// Token: 0x0600675C RID: 26460 RVA: 0x0026FA6B File Offset: 0x0026DC6B
	public static Alignment Right()
	{
		return new Alignment(1f, 0.5f);
	}

	// Token: 0x0600675D RID: 26461 RVA: 0x0026FA7C File Offset: 0x0026DC7C
	public static Alignment BottomLeft()
	{
		return new Alignment(0f, 0f);
	}

	// Token: 0x0600675E RID: 26462 RVA: 0x0026FA8D File Offset: 0x0026DC8D
	public static Alignment Bottom()
	{
		return new Alignment(0.5f, 0f);
	}

	// Token: 0x0600675F RID: 26463 RVA: 0x0026FA9E File Offset: 0x0026DC9E
	public static Alignment BottomRight()
	{
		return new Alignment(1f, 0f);
	}

	// Token: 0x06006760 RID: 26464 RVA: 0x0026FAAF File Offset: 0x0026DCAF
	public static implicit operator Vector2(Alignment a)
	{
		return new Vector2(a.x, a.y);
	}

	// Token: 0x06006761 RID: 26465 RVA: 0x0026FAC2 File Offset: 0x0026DCC2
	public static implicit operator Alignment(Vector2 v)
	{
		return new Alignment(v.x, v.y);
	}

	// Token: 0x040046DC RID: 18140
	public readonly float x;

	// Token: 0x040046DD RID: 18141
	public readonly float y;
}
