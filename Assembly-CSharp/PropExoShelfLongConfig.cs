using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E4 RID: 996
public class PropExoShelfLongConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600145D RID: 5213 RVA: 0x00074CF6 File Offset: 0x00072EF6
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600145E RID: 5214 RVA: 0x00074CFD File Offset: 0x00072EFD
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600145F RID: 5215 RVA: 0x00074D00 File Offset: 0x00072F00
	public GameObject CreatePrefab()
	{
		string text = "PropExoShelfLong";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPEXOSHELFLONG.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPEXOSHELFLONG.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_shelf_long_kanim"), "off", Grid.SceneLayer.Building, 3, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001460 RID: 5216 RVA: 0x00074D93 File Offset: 0x00072F93
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001461 RID: 5217 RVA: 0x00074DAA File Offset: 0x00072FAA
	public void OnSpawn(GameObject inst)
	{
	}
}
