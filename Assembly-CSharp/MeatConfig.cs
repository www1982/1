using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E1 RID: 481
public class MeatConfig : IEntityConfig
{
	// Token: 0x060009A0 RID: 2464 RVA: 0x0003D954 File Offset: 0x0003BB54
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Meat", global::STRINGS.ITEMS.FOOD.MEAT.NAME, global::STRINGS.ITEMS.FOOD.MEAT.DESC, 1f, false, Assets.GetAnim("creaturemeat_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.MEAT);
		return gameObject;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0003D9BA File Offset: 0x0003BBBA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0003D9BC File Offset: 0x0003BBBC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D7 RID: 1751
	public const string ID = "Meat";
}
