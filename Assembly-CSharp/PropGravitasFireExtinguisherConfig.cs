using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039A RID: 922
public class PropGravitasFireExtinguisherConfig : IEntityConfig
{
	// Token: 0x060012D1 RID: 4817 RVA: 0x0006C230 File Offset: 0x0006A430
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasFireExtinguisher";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIREEXTINGUISHER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASFIREEXTINGUISHER.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_fireextinguisher_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x0006C2C3 File Offset: 0x0006A4C3
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012D3 RID: 4819 RVA: 0x0006C2DA File Offset: 0x0006A4DA
	public void OnSpawn(GameObject inst)
	{
	}
}
