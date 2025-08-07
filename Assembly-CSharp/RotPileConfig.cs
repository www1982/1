using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001EF RID: 495
public class RotPileConfig : IEntityConfig
{
	// Token: 0x060009E4 RID: 2532 RVA: 0x0003E238 File Offset: 0x0003C438
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(RotPileConfig.ID, global::STRINGS.ITEMS.FOOD.ROTPILE.NAME, global::STRINGS.ITEMS.FOOD.ROTPILE.DESC, 1f, false, Assets.GetAnim("rotfood_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.AddTag(GameTags.Organics, false);
		component.AddTag(GameTags.Compostable, false);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<OccupyArea>();
		gameObject.AddOrGet<Modifiers>();
		gameObject.AddOrGet<RotPile>();
		gameObject.AddComponent<DecorProvider>().SetValues(DECOR.PENALTY.TIER2);
		return gameObject;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0003E2DB File Offset: 0x0003C4DB
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<DecorProvider>().overrideName = global::STRINGS.ITEMS.FOOD.ROTPILE.NAME;
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0003E2F2 File Offset: 0x0003C4F2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006F7 RID: 1783
	public static string ID = "RotPile";
}
