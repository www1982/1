using System;

// Token: 0x0200083B RID: 2107
public interface IConduitDispenser
{
	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x060039BD RID: 14781
	Storage Storage { get; }

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x060039BE RID: 14782
	ConduitType ConduitType { get; }
}
