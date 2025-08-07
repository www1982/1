using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F2 RID: 498
public class ShellfishMeatConfig : IEntityConfig
{
	// Token: 0x060009F2 RID: 2546 RVA: 0x0003E404 File Offset: 0x0003C604
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("ShellfishMeat", global::STRINGS.ITEMS.FOOD.SHELLFISHMEAT.NAME, global::STRINGS.ITEMS.FOOD.SHELLFISHMEAT.DESC, 1f, false, Assets.GetAnim("shellfish_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.SHELLFISH_MEAT);
		return gameObject;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0003E46A File Offset: 0x0003C66A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0003E46C File Offset: 0x0003C66C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006FE RID: 1790
	public const string ID = "ShellfishMeat";
}
