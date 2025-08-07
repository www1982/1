using System;

// Token: 0x0200080C RID: 2060
public interface ICircuitConnected
{
	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06003803 RID: 14339
	bool IsVirtual { get; }

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06003804 RID: 14340
	int PowerCell { get; }

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06003805 RID: 14341
	object VirtualCircuitKey { get; }
}
