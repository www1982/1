using System;
using UnityEngine;

// Token: 0x02000613 RID: 1555
[AddComponentMenu("KMonoBehaviour/scripts/SkyVisibilityVisualizer")]
public class SkyVisibilityVisualizer : KMonoBehaviour
{
	// Token: 0x060024EF RID: 9455 RVA: 0x000D2DCA File Offset: 0x000D0FCA
	private static bool HasSkyVisibility(int cell)
	{
		return Grid.ExposedToSunlight[cell] >= 1;
	}

	// Token: 0x040015A7 RID: 5543
	public Vector2I OriginOffset = new Vector2I(0, 0);

	// Token: 0x040015A8 RID: 5544
	public bool TwoWideOrgin;

	// Token: 0x040015A9 RID: 5545
	public int RangeMin;

	// Token: 0x040015AA RID: 5546
	public int RangeMax;

	// Token: 0x040015AB RID: 5547
	public int ScanVerticalStep;

	// Token: 0x040015AC RID: 5548
	public bool SkipOnModuleInteriors;

	// Token: 0x040015AD RID: 5549
	public bool AllOrNothingVisibility;

	// Token: 0x040015AE RID: 5550
	public Func<int, bool> SkyVisibilityCb = new Func<int, bool>(SkyVisibilityVisualizer.HasSkyVisibility);
}
