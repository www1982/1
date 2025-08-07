using System;
using UnityEngine;

// Token: 0x020005B1 RID: 1457
public struct ElementSplitter
{
	// Token: 0x06002191 RID: 8593 RVA: 0x000C1F8C File Offset: 0x000C018C
	public ElementSplitter(GameObject go)
	{
		this.primaryElement = go.GetComponent<PrimaryElement>();
		this.kPrefabID = go.GetComponent<KPrefabID>();
		this.onTakeCB = null;
		this.canAbsorbCB = null;
	}

	// Token: 0x04001390 RID: 5008
	public PrimaryElement primaryElement;

	// Token: 0x04001391 RID: 5009
	public Func<Pickupable, float, Pickupable> onTakeCB;

	// Token: 0x04001392 RID: 5010
	public Func<Pickupable, bool> canAbsorbCB;

	// Token: 0x04001393 RID: 5011
	public KPrefabID kPrefabID;
}
