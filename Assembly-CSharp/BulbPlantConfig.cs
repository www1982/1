using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017B RID: 379
public class BulbPlantConfig : IEntityConfig
{
	// Token: 0x06000739 RID: 1849 RVA: 0x00031EB8 File Offset: 0x000300B8
	public GameObject CreatePrefab()
	{
		string text = "BulbPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.BULBPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.BULBPLANT.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_bulb_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 288f, 293.15f, 313.15f, 333.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "BulbPlantOriginal", global::STRINGS.CREATURES.SPECIES.BULBPLANT.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text4 = "BulbPlantSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.BULBPLANT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.BULBPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_potted_bulb_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.DecorSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.BULBPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 12, text8, EntityTemplates.CollisionShape.CIRCLE, 0.4f, 0.4f, null, "", false), "BulbPlant_preview", Assets.GetAnim("potted_bulb_kanim"), "place", 1, 1);
		DiseaseDropper.Def def = gameObject.AddOrGetDef<DiseaseDropper.Def>();
		def.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.PollenGerms.id);
		def.singleEmitQuantity = 0;
		def.averageEmitPerSecond = 5000;
		def.emitFrequency = 5f;
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "PollenGerms";
		return gameObject;
	}

	// Token: 0x0600073A RID: 1850 RVA: 0x00032087 File Offset: 0x00030287
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600073B RID: 1851 RVA: 0x00032089 File Offset: 0x00030289
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000567 RID: 1383
	public const string ID = "BulbPlant";

	// Token: 0x04000568 RID: 1384
	public const string SEED_ID = "BulbPlantSeed";

	// Token: 0x04000569 RID: 1385
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER1;

	// Token: 0x0400056A RID: 1386
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
