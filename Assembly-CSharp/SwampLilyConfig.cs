using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B4 RID: 436
public class SwampLilyConfig : IEntityConfig
{
	// Token: 0x060008B5 RID: 2229 RVA: 0x0003ADD4 File Offset: 0x00038FD4
	public GameObject CreatePrefab()
	{
		string text = "SwampLily";
		string text2 = global::STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SWAMPLILY.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swamplily_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 328.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 258.15f, 308.15f, 358.15f, 448.15f, new SimHashes[] { SimHashes.ChlorineGas }, true, 0f, 0.15f, SwampLilyFlowerConfig.ID, true, true, true, true, 2400f, 0f, 4600f, SwampLilyConfig.ID + "Original", global::STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text4 = "SwampLilySeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.DESC;
		KAnimFile anim = Assets.GetAnim("seed_swampLily_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.SWAMPLILY.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 21, text8, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), SwampLilyConfig.ID + "_preview", Assets.GetAnim("swamplily_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death_bloom", NOISE_POLLUTION.CREATURES.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, SwampLilyConfig.ID);
		return gameObject;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0003AFC0 File Offset: 0x000391C0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0003AFC2 File Offset: 0x000391C2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000667 RID: 1639
	public static string ID = "SwampLily";

	// Token: 0x04000668 RID: 1640
	public const string SEED_ID = "SwampLilySeed";
}
