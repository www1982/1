using System;
using UnityEngine;

// Token: 0x020005FD RID: 1533
[AddComponentMenu("KMonoBehaviour/scripts/RangeVisualizer")]
public class RangeVisualizer : KMonoBehaviour
{
	// Token: 0x0400152A RID: 5418
	public Vector2I OriginOffset;

	// Token: 0x0400152B RID: 5419
	public Vector2I RangeMin;

	// Token: 0x0400152C RID: 5420
	public Vector2I RangeMax;

	// Token: 0x0400152D RID: 5421
	public Vector2I TexSize = new Vector2I(64, 64);

	// Token: 0x0400152E RID: 5422
	public bool TestLineOfSight = true;

	// Token: 0x0400152F RID: 5423
	public bool BlockingTileVisible;

	// Token: 0x04001530 RID: 5424
	public Func<int, bool> BlockingVisibleCb;

	// Token: 0x04001531 RID: 5425
	public Func<int, bool> BlockingCb = new Func<int, bool>(Grid.IsSolidCell);

	// Token: 0x04001532 RID: 5426
	public bool AllowLineOfSightInvalidCells;
}
