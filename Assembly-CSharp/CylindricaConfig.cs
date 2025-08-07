using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000183 RID: 387
public class CylindricaConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000761 RID: 1889 RVA: 0x00032F03 File Offset: 0x00031103
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x00032F0A File Offset: 0x0003110A
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x00032F10 File Offset: 0x00031110
	public GameObject CreatePrefab()
	{
		string text = "Cylindrica";
		string text2 = global::STRINGS.CREATURES.SPECIES.CYLINDRICA.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.CYLINDRICA.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = CylindricaConfig.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_cylindricafan_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 298.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 288.15f, 293.15f, 323.15f, 373.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "CylindricaOriginal", global::STRINGS.CREATURES.SPECIES.CYLINDRICA.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = CylindricaConfig.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = CylindricaConfig.NEGATIVE_DECOR_EFFECT;
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text4 = "CylindricaSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.CYLINDRICA.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.CYLINDRICA.DESC;
		KAnimFile anim = Assets.GetAnim("seed_potted_cylindricafan_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.DecorSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.CYLINDRICA.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 12, text8, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "Cylindrica_preview", Assets.GetAnim("potted_cylindricafan_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0003307C File Offset: 0x0003127C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0003307E File Offset: 0x0003127E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400058B RID: 1419
	public const string ID = "Cylindrica";

	// Token: 0x0400058C RID: 1420
	public const string SEED_ID = "CylindricaSeed";

	// Token: 0x0400058D RID: 1421
	public static readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x0400058E RID: 1422
	public static readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
