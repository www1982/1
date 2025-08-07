using System;

// Token: 0x02000838 RID: 2104
public interface IConduitConsumer
{
	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x0600399D RID: 14749
	Storage Storage { get; }

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x0600399E RID: 14750
	ConduitType ConduitType { get; }
}
