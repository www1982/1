using System;
using UnityEngine;

// Token: 0x02000318 RID: 792
public class MeterConfig : IEntityConfig
{
	// Token: 0x06001053 RID: 4179 RVA: 0x00061847 File Offset: 0x0005FA47
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MeterConfig.ID, MeterConfig.ID, false);
		gameObject.AddOrGet<KBatchedAnimController>();
		gameObject.AddOrGet<KBatchedAnimTracker>();
		return gameObject;
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x00061867 File Offset: 0x0005FA67
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x00061869 File Offset: 0x0005FA69
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A70 RID: 2672
	public static readonly string ID = "Meter";
}
