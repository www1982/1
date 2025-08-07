using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class CookedFishConfig : IEntityConfig
{
	// Token: 0x06000937 RID: 2359 RVA: 0x0003CF78 File Offset: 0x0003B178
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedFish", global::STRINGS.ITEMS.FOOD.COOKEDFISH.NAME, global::STRINGS.ITEMS.FOOD.COOKEDFISH.DESC, 1f, false, Assets.GetAnim("grilled_pacu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_FISH);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0003CFDC File Offset: 0x0003B1DC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0003CFDE File Offset: 0x0003B1DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006B0 RID: 1712
	public const string ID = "CookedFish";

	// Token: 0x040006B1 RID: 1713
	public static ComplexRecipe recipe;
}
