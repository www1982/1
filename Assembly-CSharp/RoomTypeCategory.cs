using System;

// Token: 0x02000657 RID: 1623
public class RoomTypeCategory : Resource
{
	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000E1C91 File Offset: 0x000DFE91
	// (set) Token: 0x060027A6 RID: 10150 RVA: 0x000E1C99 File Offset: 0x000DFE99
	public string colorName { get; private set; }

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x060027A7 RID: 10151 RVA: 0x000E1CA2 File Offset: 0x000DFEA2
	// (set) Token: 0x060027A8 RID: 10152 RVA: 0x000E1CAA File Offset: 0x000DFEAA
	public string icon { get; private set; }

	// Token: 0x060027A9 RID: 10153 RVA: 0x000E1CB3 File Offset: 0x000DFEB3
	public RoomTypeCategory(string id, string name, string colorName, string icon)
		: base(id, name)
	{
		this.colorName = colorName;
		this.icon = icon;
	}
}
