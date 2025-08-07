using System;
using UnityEngine;

// Token: 0x02000A48 RID: 2632
public struct OreSizeVisualizerData
{
	// Token: 0x06004C5E RID: 19550 RVA: 0x001BB30C File Offset: 0x001B950C
	public OreSizeVisualizerData(GameObject go)
	{
		this.primaryElement = go.GetComponent<PrimaryElement>();
		this.onMassChangedCB = null;
		this.tierSetType = OreSizeVisualizerComponents.TiersSetType.Ores;
	}

	// Token: 0x0400329C RID: 12956
	public PrimaryElement primaryElement;

	// Token: 0x0400329D RID: 12957
	public Action<object> onMassChangedCB;

	// Token: 0x0400329E RID: 12958
	public OreSizeVisualizerComponents.TiersSetType tierSetType;
}
