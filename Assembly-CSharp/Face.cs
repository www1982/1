using System;

// Token: 0x02000915 RID: 2325
public class Face : Resource
{
	// Token: 0x060040A5 RID: 16549 RVA: 0x0016A6DD File Offset: 0x001688DD
	public Face(string id, string headFXSymbol = null)
		: base(id, null, null)
	{
		this.hash = new HashedString(id);
		this.headFXHash = headFXSymbol;
	}

	// Token: 0x04002858 RID: 10328
	public HashedString hash;

	// Token: 0x04002859 RID: 10329
	public HashedString headFXHash;

	// Token: 0x0400285A RID: 10330
	private const string SYMBOL_PREFIX = "headfx_";
}
