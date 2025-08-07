using System;
using UnityEngine;

// Token: 0x02000608 RID: 1544
public interface IRottable
{
	// Token: 0x1700019D RID: 413
	// (get) Token: 0x060024AD RID: 9389
	GameObject gameObject { get; }

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x060024AE RID: 9390
	float RotTemperature { get; }

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x060024AF RID: 9391
	float PreserveTemperature { get; }
}
