using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017F RID: 383
public class ChlorineGeyserConfig : IEntityConfig
{
	// Token: 0x0600074D RID: 1869 RVA: 0x00032638 File Offset: 0x00030838
	public GameObject CreatePrefab()
	{
		string text = "ChlorineGeyser";
		string text2 = global::STRINGS.CREATURES.SPECIES.CHLORINEGEYSER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.CHLORINEGEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_chlorine_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "chlorine_gas";
		geyserConfigurator.presetMin = 0.35f;
		geyserConfigurator.presetMax = 0.65f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x0600074E RID: 1870 RVA: 0x00032757 File Offset: 0x00030957
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00032759 File Offset: 0x00030959
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000578 RID: 1400
	public const string ID = "ChlorineGeyser";
}
