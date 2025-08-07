using System;
using UnityEngine;

// Token: 0x0200031A RID: 794
public class MinionAssignablesProxyConfig : IEntityConfig
{
	// Token: 0x0600105D RID: 4189 RVA: 0x00061986 File Offset: 0x0005FB86
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MinionAssignablesProxyConfig.ID, MinionAssignablesProxyConfig.ID, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<Ownables>();
		gameObject.AddOrGet<Equipment>();
		gameObject.AddOrGet<MinionAssignablesProxy>();
		return gameObject;
	}

	// Token: 0x0600105E RID: 4190 RVA: 0x000619B4 File Offset: 0x0005FBB4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x000619B6 File Offset: 0x0005FBB6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A74 RID: 2676
	public static string ID = "MinionAssignablesProxy";
}
