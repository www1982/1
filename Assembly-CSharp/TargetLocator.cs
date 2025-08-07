using System;
using UnityEngine;

// Token: 0x02000312 RID: 786
public class TargetLocator : IEntityConfig
{
	// Token: 0x06001032 RID: 4146 RVA: 0x0006127D File Offset: 0x0005F47D
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(TargetLocator.ID, TargetLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		return gameObject;
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x0006129A File Offset: 0x0005F49A
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x0006129C File Offset: 0x0005F49C
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A50 RID: 2640
	public static readonly string ID = "TargetLocator";
}
