using System;
using UnityEngine;

// Token: 0x02000AC8 RID: 2760
public class TechTreeTitle : Resource
{
	// Token: 0x17000599 RID: 1433
	// (get) Token: 0x06005035 RID: 20533 RVA: 0x001D0582 File Offset: 0x001CE782
	public Vector2 center
	{
		get
		{
			return this.node.center;
		}
	}

	// Token: 0x1700059A RID: 1434
	// (get) Token: 0x06005036 RID: 20534 RVA: 0x001D058F File Offset: 0x001CE78F
	public float width
	{
		get
		{
			return this.node.width;
		}
	}

	// Token: 0x1700059B RID: 1435
	// (get) Token: 0x06005037 RID: 20535 RVA: 0x001D059C File Offset: 0x001CE79C
	public float height
	{
		get
		{
			return this.node.height;
		}
	}

	// Token: 0x06005038 RID: 20536 RVA: 0x001D05A9 File Offset: 0x001CE7A9
	public TechTreeTitle(string id, ResourceSet parent, string name, ResourceTreeNode node)
		: base(id, parent, name)
	{
		this.node = node;
	}

	// Token: 0x04003601 RID: 13825
	public string desc;

	// Token: 0x04003602 RID: 13826
	private ResourceTreeNode node;
}
