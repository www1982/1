using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class IceFlowerConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007F6 RID: 2038 RVA: 0x0003631D File Offset: 0x0003451D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x00036324 File Offset: 0x00034524
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x00036328 File Offset: 0x00034528
	public GameObject CreatePrefab()
	{
		string text = "IceFlower";
		string text2 = global::STRINGS.CREATURES.SPECIES.ICEFLOWER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.ICEFLOWER.DESC;
		float num = 1f;
		EffectorValues positive_DECOR_EFFECT = this.POSITIVE_DECOR_EFFECT;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("potted_ice_flower_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 1, positive_DECOR_EFFECT, default(EffectorValues), SimHashes.Creature, null, 243.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 173.15f, 203.15f, 278.15f, 318.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide,
			SimHashes.ChlorineGas,
			SimHashes.Hydrogen
		}, true, 0f, 0.15f, null, true, false, true, true, 2400f, 0f, 2200f, "IceFlowerOriginal", global::STRINGS.CREATURES.SPECIES.ICEFLOWER.NAME);
		PrickleGrass prickleGrass = gameObject.AddOrGet<PrickleGrass>();
		prickleGrass.positive_decor_effect = this.POSITIVE_DECOR_EFFECT;
		prickleGrass.negative_decor_effect = this.NEGATIVE_DECOR_EFFECT;
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text4 = "IceFlowerSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.ICEFLOWER.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.ICEFLOWER.DESC;
		KAnimFile anim = Assets.GetAnim("seed_ice_flower_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.DecorSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.ICEFLOWER.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 12, text8, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, null, "", false), "IceFlower_preview", Assets.GetAnim("potted_ice_flower_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x00036497 File Offset: 0x00034697
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x00036499 File Offset: 0x00034699
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005EA RID: 1514
	public const string ID = "IceFlower";

	// Token: 0x040005EB RID: 1515
	public const string SEED_ID = "IceFlowerSeed";

	// Token: 0x040005EC RID: 1516
	public readonly EffectorValues POSITIVE_DECOR_EFFECT = DECOR.BONUS.TIER3;

	// Token: 0x040005ED RID: 1517
	public readonly EffectorValues NEGATIVE_DECOR_EFFECT = DECOR.PENALTY.TIER3;
}
