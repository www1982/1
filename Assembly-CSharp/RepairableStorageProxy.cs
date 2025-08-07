using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
public class RepairableStorageProxy : IEntityConfig
{
	// Token: 0x0600109A RID: 4250 RVA: 0x00062AFF File Offset: 0x00060CFF
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(RepairableStorageProxy.ID, RepairableStorageProxy.ID, true);
		gameObject.AddOrGet<Storage>();
		gameObject.AddTag(GameTags.NotConversationTopic);
		return gameObject;
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x00062B23 File Offset: 0x00060D23
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x00062B25 File Offset: 0x00060D25
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A86 RID: 2694
	public static string ID = "RepairableStorageProxy";
}
