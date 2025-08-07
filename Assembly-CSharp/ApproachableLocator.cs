using System;
using UnityEngine;

// Token: 0x02000313 RID: 787
public class ApproachableLocator : IEntityConfig
{
	// Token: 0x06001037 RID: 4151 RVA: 0x000612B2 File Offset: 0x0005F4B2
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(ApproachableLocator.ID, ApproachableLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<Approachable>();
		return gameObject;
	}

	// Token: 0x06001038 RID: 4152 RVA: 0x000612D6 File Offset: 0x0005F4D6
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001039 RID: 4153 RVA: 0x000612D8 File Offset: 0x0005F4D8
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A51 RID: 2641
	public static readonly string ID = "ApproachableLocator";
}
