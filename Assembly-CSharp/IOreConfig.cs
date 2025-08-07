using System;
using UnityEngine;

// Token: 0x02000353 RID: 851
public interface IOreConfig
{
	// Token: 0x1700004A RID: 74
	// (get) Token: 0x06001190 RID: 4496
	SimHashes ElementID { get; }

	// Token: 0x06001191 RID: 4497
	GameObject CreatePrefab();
}
