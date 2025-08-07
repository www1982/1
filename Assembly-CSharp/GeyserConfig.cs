using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000196 RID: 406
public class GeyserConfig : IEntityConfig
{
	// Token: 0x060007CE RID: 1998 RVA: 0x00034F94 File Offset: 0x00033194
	public GameObject CreatePrefab()
	{
		string text = "Geyser";
		string text2 = global::STRINGS.CREATURES.SPECIES.GEYSER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.GEYSER.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_steam_kanim"), "inactive", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.DeprecatedContent, false);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.IgneousRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<UserNameable>();
		GeyserConfigurator geyserConfigurator = gameObject.AddOrGet<GeyserConfigurator>();
		geyserConfigurator.presetType = "steam";
		geyserConfigurator.presetMin = 0.5f;
		geyserConfigurator.presetMax = 0.75f;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x000350BA File Offset: 0x000332BA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x000350BC File Offset: 0x000332BC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005C2 RID: 1474
	public const int GEOTUNERS_REQUIRED_FOR_MAJOR_TRACKER_ANIMATION = 5;

	// Token: 0x0200115C RID: 4444
	public enum TrackerMeterAnimNames
	{
		// Token: 0x040062A5 RID: 25253
		tracker,
		// Token: 0x040062A6 RID: 25254
		geotracker,
		// Token: 0x040062A7 RID: 25255
		geotracker_minor,
		// Token: 0x040062A8 RID: 25256
		geotracker_major
	}
}
