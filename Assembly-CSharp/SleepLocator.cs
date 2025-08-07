using System;
using UnityEngine;

// Token: 0x02000314 RID: 788
public class SleepLocator : IEntityConfig
{
	// Token: 0x0600103C RID: 4156 RVA: 0x000612EE File Offset: 0x0005F4EE
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SleepLocator.ID, SleepLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<Approachable>();
		gameObject.AddOrGet<Sleepable>().isNormalBed = false;
		return gameObject;
	}

	// Token: 0x0600103D RID: 4157 RVA: 0x0006131E File Offset: 0x0005F51E
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x00061320 File Offset: 0x0005F520
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A52 RID: 2642
	public static readonly string ID = "SleepLocator";
}
