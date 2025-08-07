using System;

// Token: 0x02000B0A RID: 2826
public class Shirt : Resource
{
	// Token: 0x06005300 RID: 21248 RVA: 0x001E32B1 File Offset: 0x001E14B1
	public Shirt(string id)
		: base(id, null, null)
	{
		this.hash = new HashedString(id);
	}

	// Token: 0x040037D0 RID: 14288
	public HashedString hash;
}
