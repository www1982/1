using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000198 RID: 408
public class GingerConfig : IEntityConfig
{
	// Token: 0x060007D9 RID: 2009 RVA: 0x00035E7C File Offset: 0x0003407C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(GingerConfig.ID, global::STRINGS.ITEMS.INGREDIENTS.GINGER.NAME, global::STRINGS.ITEMS.INGREDIENTS.GINGER.DESC, 1f, true, Assets.GetAnim("ginger_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.45f, 0.4f, true, global::TUNING.SORTORDER.BUILDINGELEMENTS + GingerConfig.SORTORDER, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x060007DA RID: 2010 RVA: 0x00035EF6 File Offset: 0x000340F6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007DB RID: 2011 RVA: 0x00035EF8 File Offset: 0x000340F8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005DE RID: 1502
	public static string ID = "GingerConfig";

	// Token: 0x040005DF RID: 1503
	public static int SORTORDER = 1;
}
