using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class PropClockConfig : IEntityConfig
{
	// Token: 0x06001254 RID: 4692 RVA: 0x0006A8EC File Offset: 0x00068AEC
	public GameObject CreatePrefab()
	{
		string text = "PropClock";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPCLOCK.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPCLOCK.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("clock_poi_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001255 RID: 4693 RVA: 0x0006A998 File Offset: 0x00068B98
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0006A99A File Offset: 0x00068B9A
	public void OnSpawn(GameObject inst)
	{
	}
}
