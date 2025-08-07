using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class PropGravitasToolCrateConfig : IEntityConfig
{
	// Token: 0x0600130D RID: 4877 RVA: 0x0006CCB0 File Offset: 0x0006AEB0
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasToolCrate";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASTOOLCRATE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASTOOLCRATE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_1x1_crate_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x0006CD43 File Offset: 0x0006AF43
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600130F RID: 4879 RVA: 0x0006CD5A File Offset: 0x0006AF5A
	public void OnSpawn(GameObject inst)
	{
	}
}
