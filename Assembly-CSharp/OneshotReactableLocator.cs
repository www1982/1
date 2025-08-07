using System;
using UnityEngine;

// Token: 0x02000315 RID: 789
public class OneshotReactableLocator : IEntityConfig
{
	// Token: 0x06001041 RID: 4161 RVA: 0x00061338 File Offset: 0x0005F538
	public static EmoteReactable CreateOneshotReactable(GameObject source, float lifetime, string id, ChoreType chore_type, int range_width = 15, int range_height = 15, float min_reactor_time = 20f)
	{
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(OneshotReactableLocator.ID), source.transform.GetPosition());
		EmoteReactable emoteReactable = new EmoteReactable(gameObject, id, chore_type, range_width, range_height, 100000f, min_reactor_time, float.PositiveInfinity, 0f);
		emoteReactable.AddPrecondition(OneshotReactableLocator.ReactorIsNotSource(source));
		OneshotReactableHost component = gameObject.GetComponent<OneshotReactableHost>();
		component.lifetime = lifetime;
		component.SetReactable(emoteReactable);
		gameObject.SetActive(true);
		return emoteReactable;
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x000613AE File Offset: 0x0005F5AE
	private static Reactable.ReactablePrecondition ReactorIsNotSource(GameObject source)
	{
		return (GameObject reactor, Navigator.ActiveTransition transition) => reactor != source;
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x000613C7 File Offset: 0x0005F5C7
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(OneshotReactableLocator.ID, OneshotReactableLocator.ID, false);
		gameObject.AddTag(GameTags.NotConversationTopic);
		gameObject.AddOrGet<OneshotReactableHost>();
		return gameObject;
	}

	// Token: 0x06001044 RID: 4164 RVA: 0x000613EB File Offset: 0x0005F5EB
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x000613ED File Offset: 0x0005F5ED
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A53 RID: 2643
	public static readonly string ID = "OneshotReactableLocator";
}
