using System;

// Token: 0x02000892 RID: 2194
public class Death : Resource
{
	// Token: 0x06003CB1 RID: 15537 RVA: 0x00151058 File Offset: 0x0014F258
	public Death(string id, ResourceSet parent, string name, string description, string pre_anim, string loop_anim)
		: base(id, parent, name)
	{
		this.preAnim = pre_anim;
		this.loopAnim = loop_anim;
		this.description = description;
	}

	// Token: 0x0400252A RID: 9514
	public string preAnim;

	// Token: 0x0400252B RID: 9515
	public string loopAnim;

	// Token: 0x0400252C RID: 9516
	public string sound;

	// Token: 0x0400252D RID: 9517
	public string description;
}
