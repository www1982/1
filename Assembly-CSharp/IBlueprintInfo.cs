using System;
using Database;

// Token: 0x0200055A RID: 1370
public interface IBlueprintInfo : IHasDlcRestrictions
{
	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06001E3D RID: 7741
	// (set) Token: 0x06001E3E RID: 7742
	string id { get; set; }

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06001E3F RID: 7743
	// (set) Token: 0x06001E40 RID: 7744
	string name { get; set; }

	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x06001E41 RID: 7745
	// (set) Token: 0x06001E42 RID: 7746
	string desc { get; set; }

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06001E43 RID: 7747
	// (set) Token: 0x06001E44 RID: 7748
	PermitRarity rarity { get; set; }

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06001E45 RID: 7749
	// (set) Token: 0x06001E46 RID: 7750
	string animFile { get; set; }
}
