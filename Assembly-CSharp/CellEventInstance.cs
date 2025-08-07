using System;
using KSerialization;

// Token: 0x02000907 RID: 2311
[SerializationConfig(MemberSerialization.OptIn)]
public class CellEventInstance : EventInstanceBase, ISaveLoadable
{
	// Token: 0x0600406F RID: 16495 RVA: 0x00169346 File Offset: 0x00167546
	public CellEventInstance(int cell, int data, int data2, CellEvent ev)
		: base(ev)
	{
		this.cell = cell;
		this.data = data;
		this.data2 = data2;
	}

	// Token: 0x040027F6 RID: 10230
	[Serialize]
	public int cell;

	// Token: 0x040027F7 RID: 10231
	[Serialize]
	public int data;

	// Token: 0x040027F8 RID: 10232
	[Serialize]
	public int data2;
}
