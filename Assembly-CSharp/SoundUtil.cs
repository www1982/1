using System;
using UnityEngine;

// Token: 0x02000B23 RID: 2851
public static class SoundUtil
{
	// Token: 0x06005435 RID: 21557 RVA: 0x001EA224 File Offset: 0x001E8424
	public static float GetLiquidDepth(int cell)
	{
		float num = 0f;
		num += Grid.Mass[cell] * (Grid.Element[cell].IsLiquid ? 1f : 0f);
		int num2 = Grid.CellBelow(cell);
		if (Grid.IsValidCell(num2))
		{
			num += Grid.Mass[num2] * (Grid.Element[num2].IsLiquid ? 1f : 0f);
		}
		return Mathf.Min(num / 1000f, 1f);
	}

	// Token: 0x06005436 RID: 21558 RVA: 0x001EA2A9 File Offset: 0x001E84A9
	public static float GetLiquidVolume(float mass)
	{
		return Mathf.Min(mass / 100f, 1f);
	}
}
