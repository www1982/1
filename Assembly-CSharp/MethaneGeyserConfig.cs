using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class MethaneGeyserConfig : IEntityConfig
{
	// Token: 0x06000807 RID: 2055 RVA: 0x00036878 File Offset: 0x00034A78
	public GameObject CreatePrefab()
	{
		string text = "MethaneGeyser";
		string text2 = global::STRINGS.CREATURES.SPECIES.METHANEGEYSER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.METHANEGEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_methane_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "methane";
		geyserConfigurator.presetMin = 0.35f;
		geyserConfigurator.presetMax = 0.65f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x00036997 File Offset: 0x00034B97
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x00036999 File Offset: 0x00034B99
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005FC RID: 1532
	public const string ID = "MethaneGeyser";
}
