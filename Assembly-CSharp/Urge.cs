using System;

// Token: 0x020004B5 RID: 1205
public class Urge : Resource
{
	// Token: 0x060019B8 RID: 6584 RVA: 0x0008DD24 File Offset: 0x0008BF24
	public Urge(string id)
		: base(id, null, null)
	{
	}

	// Token: 0x060019B9 RID: 6585 RVA: 0x0008DD2F File Offset: 0x0008BF2F
	public override string ToString()
	{
		return this.Id;
	}
}
