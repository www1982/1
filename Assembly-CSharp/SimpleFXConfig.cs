using System;
using UnityEngine;

// Token: 0x02000328 RID: 808
public class SimpleFXConfig : IEntityConfig
{
	// Token: 0x060010AB RID: 4267 RVA: 0x00062CBE File Offset: 0x00060EBE
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SimpleFXConfig.ID, SimpleFXConfig.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>();
		return gameObject;
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x00062CD7 File Offset: 0x00060ED7
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x00062CD9 File Offset: 0x00060ED9
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A8C RID: 2700
	public static readonly string ID = "SimpleFX";
}
