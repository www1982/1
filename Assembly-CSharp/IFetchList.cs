using System;
using System.Collections.Generic;

// Token: 0x0200091A RID: 2330
public interface IFetchList
{
	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x060040D1 RID: 16593
	Storage Destination { get; }

	// Token: 0x060040D2 RID: 16594
	float GetMinimumAmount(Tag tag);

	// Token: 0x060040D3 RID: 16595
	Dictionary<Tag, float> GetRemaining();

	// Token: 0x060040D4 RID: 16596
	Dictionary<Tag, float> GetRemainingMinimum();
}
