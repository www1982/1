using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class OilWellConfig : IEntityConfig
{
	// Token: 0x06000815 RID: 2069 RVA: 0x00036C44 File Offset: 0x00034E44
	public GameObject CreatePrefab()
	{
		string text = "OilWell";
		string text2 = global::STRINGS.CREATURES.SPECIES.OIL_WELL.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.OIL_WELL.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("geyser_side_oil_kanim"), "off", Grid.SceneLayer.BuildingBack, 4, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.SedimentaryRock, true);
		component.Temperature = 372.15f;
		gameObject.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 0), GameTags.OilWell, null)
		};
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_methane_kanim", "GeyserMethane_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		return gameObject;
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x00036D34 File Offset: 0x00034F34
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x00036D36 File Offset: 0x00034F36
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000601 RID: 1537
	public const string ID = "OilWell";
}
