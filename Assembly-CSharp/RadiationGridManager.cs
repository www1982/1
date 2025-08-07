using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AB3 RID: 2739
public static class RadiationGridManager
{
	// Token: 0x06004F74 RID: 20340 RVA: 0x001CBA74 File Offset: 0x001C9C74
	public static int CalculateFalloff(float falloffRate, int cell, int origin)
	{
		return Mathf.Max(1, Mathf.RoundToInt(falloffRate * (float)Mathf.Max(Grid.GetCellDistance(origin, cell), 1)));
	}

	// Token: 0x06004F75 RID: 20341 RVA: 0x001CBA91 File Offset: 0x001C9C91
	public static void Initialise()
	{
		RadiationGridManager.emitters = new List<RadiationGridEmitter>();
	}

	// Token: 0x06004F76 RID: 20342 RVA: 0x001CBA9D File Offset: 0x001C9C9D
	public static void Shutdown()
	{
		RadiationGridManager.emitters.Clear();
	}

	// Token: 0x06004F77 RID: 20343 RVA: 0x001CBAAC File Offset: 0x001C9CAC
	public static void Refresh()
	{
		for (int i = 0; i < RadiationGridManager.emitters.Count; i++)
		{
			if (RadiationGridManager.emitters[i].enabled)
			{
				RadiationGridManager.emitters[i].Emit();
			}
		}
	}

	// Token: 0x0400356E RID: 13678
	public const float STANDARD_MASS_FALLOFF = 1000000f;

	// Token: 0x0400356F RID: 13679
	public const int RADIATION_LINGER_RATE = 4;

	// Token: 0x04003570 RID: 13680
	public static List<RadiationGridEmitter> emitters = new List<RadiationGridEmitter>();

	// Token: 0x04003571 RID: 13681
	public static List<global::Tuple<int, int>> previewLightCells = new List<global::Tuple<int, int>>();

	// Token: 0x04003572 RID: 13682
	public static int[] previewLux;
}
