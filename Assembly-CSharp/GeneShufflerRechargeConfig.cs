using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200030A RID: 778
public class GeneShufflerRechargeConfig : IEntityConfig
{
	// Token: 0x06001002 RID: 4098 RVA: 0x0005F9A4 File Offset: 0x0005DBA4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("GeneShufflerRecharge", ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME, ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.DESC, 5f, true, Assets.GetAnim("vacillator_charge_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.IndustrialIngredient });
	}

	// Token: 0x06001003 RID: 4099 RVA: 0x0005FA0D File Offset: 0x0005DC0D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001004 RID: 4100 RVA: 0x0005FA0F File Offset: 0x0005DC0F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A25 RID: 2597
	public const string ID = "GeneShufflerRecharge";

	// Token: 0x04000A26 RID: 2598
	public static readonly Tag tag = TagManager.Create("GeneShufflerRecharge");

	// Token: 0x04000A27 RID: 2599
	public const float MASS = 5f;
}
