using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000302 RID: 770
public class EggConfig
{
	// Token: 0x06000FD7 RID: 4055 RVA: 0x0005F31C File Offset: 0x0005D51C
	[Obsolete("Mod compatibility: Use CreateEgg with requiredDlcIds and forbiddenDlcIds")]
	public static GameObject CreateEgg(string id, string name, string desc, Tag creature_id, string anim, float mass, int egg_sort_order, float base_incubation_rate)
	{
		return EggConfig.CreateEgg(id, name, desc, creature_id, anim, mass, egg_sort_order, base_incubation_rate, null, null);
	}

	// Token: 0x06000FD8 RID: 4056 RVA: 0x0005F33C File Offset: 0x0005D53C
	[Obsolete("Mod compatibility: Use CreateEgg with requiredDlcIds and forbiddenDlcIds")]
	public static GameObject CreateEgg(string id, string name, string desc, Tag creature_id, string anim, float mass, int egg_sort_order, float base_incubation_rate, string[] dlcIds)
	{
		string[] array;
		string[] array2;
		DlcManager.ConvertAvailableToRequireAndForbidden(dlcIds, out array, out array2);
		return EggConfig.CreateEgg(id, name, desc, creature_id, anim, mass, egg_sort_order, base_incubation_rate, array, array2);
	}

	// Token: 0x06000FD9 RID: 4057 RVA: 0x0005F368 File Offset: 0x0005D568
	public static GameObject CreateEgg(string id, string name, string desc, Tag creature_id, string anim, float mass, int egg_sort_order, float base_incubation_rate, string[] requiredDlcIds, string[] forbiddenDlcIds)
	{
		return EggConfig.CreateEgg(id, name, desc, creature_id, anim, mass, egg_sort_order, base_incubation_rate, requiredDlcIds, forbiddenDlcIds, false);
	}

	// Token: 0x06000FDA RID: 4058 RVA: 0x0005F38C File Offset: 0x0005D58C
	public static GameObject CreateEgg(string id, string name, string desc, Tag creature_id, string anim, float mass, int egg_sort_order, float base_incubation_rate, string[] requiredDlcIds, string[] forbiddenDlcIds, bool preventEggDrops)
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(id, name, desc, mass, true, Assets.GetAnim(anim), "idle", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.8f, true, 0, SimHashes.Creature, null);
		gameObject.AddOrGet<KBoxCollider2D>().offset = new Vector2f(0f, 0.36f);
		gameObject.AddOrGet<Pickupable>().sortOrder = SORTORDER.EGGS + egg_sort_order;
		gameObject.AddOrGet<Effects>();
		KPrefabID kprefabID = gameObject.AddOrGet<KPrefabID>();
		kprefabID.AddTag(GameTags.Egg, false);
		kprefabID.AddTag(GameTags.IncubatableEgg, false);
		kprefabID.AddTag(GameTags.PedestalDisplayable, false);
		kprefabID.requiredDlcIds = requiredDlcIds;
		kprefabID.forbiddenDlcIds = forbiddenDlcIds;
		IncubationMonitor.Def def = gameObject.AddOrGetDef<IncubationMonitor.Def>();
		def.preventEggDrops = preventEggDrops;
		def.spawnedCreature = creature_id;
		def.baseIncubationRate = base_incubation_rate;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		global::UnityEngine.Object.Destroy(gameObject.GetComponent<EntitySplitter>());
		Assets.AddPrefab(gameObject.GetComponent<KPrefabID>());
		if (!preventEggDrops)
		{
			EggCrackerConfig.RegisterEgg(id, name, desc, mass, requiredDlcIds, forbiddenDlcIds);
		}
		return gameObject;
	}
}
