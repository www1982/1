using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008EC RID: 2284
public interface IMultiEntityConfig
{
	// Token: 0x06003FCD RID: 16333
	List<GameObject> CreatePrefabs();

	// Token: 0x06003FCE RID: 16334
	void OnPrefabInit(GameObject inst);

	// Token: 0x06003FCF RID: 16335
	void OnSpawn(GameObject inst);
}
