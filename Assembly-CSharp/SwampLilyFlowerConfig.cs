using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001FF RID: 511
public class SwampLilyFlowerConfig : IEntityConfig
{
	// Token: 0x06000A35 RID: 2613 RVA: 0x0003EA98 File Offset: 0x0003CC98
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(SwampLilyFlowerConfig.ID, ITEMS.INGREDIENTS.SWAMPLILYFLOWER.NAME, ITEMS.INGREDIENTS.SWAMPLILYFLOWER.DESC, 1f, false, Assets.GetAnim("swamplilyflower_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0003EB0F File Offset: 0x0003CD0F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x0003EB11 File Offset: 0x0003CD11
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400071C RID: 1820
	public static float SEEDS_PER_FRUIT = 1f;

	// Token: 0x0400071D RID: 1821
	public static string ID = "SwampLilyFlower";
}
