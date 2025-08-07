using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E9 RID: 1001
public class PropHumanMurphyBedConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001479 RID: 5241 RVA: 0x000751D0 File Offset: 0x000733D0
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000751D7 File Offset: 0x000733D7
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600147B RID: 5243 RVA: 0x000751DC File Offset: 0x000733DC
	public GameObject CreatePrefab()
	{
		string text = "PropHumanMurphyBed";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANMURPHYBED.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANMURPHYBED.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_murphybed_kanim"), "on", Grid.SceneLayer.Building, 5, 4, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600147C RID: 5244 RVA: 0x0007526F File Offset: 0x0007346F
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600147D RID: 5245 RVA: 0x00075286 File Offset: 0x00073486
	public void OnSpawn(GameObject inst)
	{
	}
}
