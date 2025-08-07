using System;
using UnityEngine;

// Token: 0x02000CDB RID: 3291
[RequireComponent(typeof(GraphBase))]
[AddComponentMenu("KMonoBehaviour/scripts/GraphLayer")]
public class GraphLayer : KMonoBehaviour
{
	// Token: 0x1700075E RID: 1886
	// (get) Token: 0x0600656E RID: 25966 RVA: 0x00262957 File Offset: 0x00260B57
	public GraphBase graph
	{
		get
		{
			if (this.graph_base == null)
			{
				this.graph_base = base.GetComponent<GraphBase>();
			}
			return this.graph_base;
		}
	}

	// Token: 0x0400456C RID: 17772
	[MyCmpReq]
	private GraphBase graph_base;
}
