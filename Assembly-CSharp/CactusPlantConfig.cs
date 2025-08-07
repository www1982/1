using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017D RID: 381
public class CactusPlantConfig : IEntityConfig
{
	// Token: 0x06000743 RID: 1859 RVA: 0x000322AC File Offset: 0x000304AC
	public GameObject CreatePrefab()
	{
		string text = "CactusPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.CACTUSPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.CACTUSPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_cactus_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 200f, 273.15f, 373.15f, 400f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, false, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "CactusPlantOriginal", global::STRINGS.CREATURES.SPECIES.CACTUSPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text4 = "CactusPlantSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.CACTUSPLANT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.CACTUSPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_potted_cactus_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.DecorSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.CACTUSPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 13, text8, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "CactusPlant_preview", Assets.GetAnim("potted_cactus_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x00032420 File Offset: 0x00030620
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x00032422 File Offset: 0x00030622
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400056D RID: 1389
	public const string ID = "CactusPlant";

	// Token: 0x0400056E RID: 1390
	public const string SEED_ID = "CactusPlantSeed";

	// Token: 0x0400056F RID: 1391
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x04000570 RID: 1392
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
