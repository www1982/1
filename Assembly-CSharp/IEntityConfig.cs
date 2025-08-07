using System;
using UnityEngine;

// Token: 0x020008EB RID: 2283
public interface IEntityConfig
{
	// Token: 0x06003FC9 RID: 16329
	GameObject CreatePrefab();

	// Token: 0x06003FCA RID: 16330
	void OnPrefabInit(GameObject inst);

	// Token: 0x06003FCB RID: 16331
	void OnSpawn(GameObject inst);

	// Token: 0x06003FCC RID: 16332 RVA: 0x001663F1 File Offset: 0x001645F1
	[Obsolete("Use IHasDlcRestrictions instead")]
	string[] GetDlcIds()
	{
		return null;
	}
}
