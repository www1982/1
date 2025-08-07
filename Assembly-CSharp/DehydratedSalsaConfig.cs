using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F1 RID: 497
public class DehydratedSalsaConfig : IEntityConfig
{
	// Token: 0x060009ED RID: 2541 RVA: 0x0003E378 File Offset: 0x0003C578
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0003E37A File Offset: 0x0003C57A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0003E37C File Offset: 0x0003C57C
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_salsa_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedSalsaConfig.ID.Name, global::STRINGS.ITEMS.FOOD.SALSA.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.SALSA.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.SALSA);
		return gameObject;
	}

	// Token: 0x040006FA RID: 1786
	public static Tag ID = new Tag("DehydratedSalsa");

	// Token: 0x040006FB RID: 1787
	public const float MASS = 1f;

	// Token: 0x040006FC RID: 1788
	public const string ANIM_FILE = "dehydrated_food_salsa_kanim";

	// Token: 0x040006FD RID: 1789
	public const string INITIAL_ANIM = "idle";
}
