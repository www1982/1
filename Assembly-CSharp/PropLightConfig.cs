using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003AB RID: 939
public class PropLightConfig : IEntityConfig
{
	// Token: 0x06001321 RID: 4897 RVA: 0x0006D13C File Offset: 0x0006B33C
	public GameObject CreatePrefab()
	{
		string text = "PropLight";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPLIGHT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPLIGHT.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("setpiece_light_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x0006D1E8 File Offset: 0x0006B3E8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001323 RID: 4899 RVA: 0x0006D1EA File Offset: 0x0006B3EA
	public void OnSpawn(GameObject inst)
	{
	}
}
