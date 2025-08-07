using System;

// Token: 0x02000AF5 RID: 2805
public class ScenePartitionerLayer
{
	// Token: 0x06005271 RID: 21105 RVA: 0x001E0CCA File Offset: 0x001DEECA
	public ScenePartitionerLayer(HashedString name, int layer)
	{
		this.name = name;
		this.layer = layer;
	}

	// Token: 0x04003776 RID: 14198
	public HashedString name;

	// Token: 0x04003777 RID: 14199
	public int layer;

	// Token: 0x04003778 RID: 14200
	public Action<int, object> OnEvent;
}
