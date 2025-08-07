using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020001DF RID: 479
public class KelpConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000995 RID: 2453 RVA: 0x0003D850 File Offset: 0x0003BA50
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000996 RID: 2454 RVA: 0x0003D857 File Offset: 0x0003BA57
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000997 RID: 2455 RVA: 0x0003D85C File Offset: 0x0003BA5C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(KelpConfig.ID, ITEMS.INGREDIENTS.KELP.NAME, ITEMS.INGREDIENTS.KELP.DESC, 1f, false, Assets.GetAnim("kelp_leaf_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000998 RID: 2456 RVA: 0x0003D8CC File Offset: 0x0003BACC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0003D8CE File Offset: 0x0003BACE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D4 RID: 1748
	public static string ID = "Kelp";

	// Token: 0x040006D5 RID: 1749
	public const float MASS_PER_UNIT = 1f;
}
