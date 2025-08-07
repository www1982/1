using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003B2 RID: 946
public class PropTallPlantConfig : IEntityConfig
{
	// Token: 0x06001341 RID: 4929 RVA: 0x0006DA1C File Offset: 0x0006BC1C
	public GameObject CreatePrefab()
	{
		string text = "PropTallPlant";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYTALLPLANT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYTALLPLANT.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_tall_plant_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Polypropylene, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001342 RID: 4930 RVA: 0x0006DAB1 File Offset: 0x0006BCB1
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x0006DAC8 File Offset: 0x0006BCC8
	public void OnSpawn(GameObject inst)
	{
	}
}
