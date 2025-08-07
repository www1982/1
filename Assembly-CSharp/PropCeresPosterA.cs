using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200037C RID: 892
public class PropCeresPosterA : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001242 RID: 4674 RVA: 0x0006A69F File Offset: 0x0006889F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x0006A6A6 File Offset: 0x000688A6
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x0006A6AC File Offset: 0x000688AC
	public GameObject CreatePrefab()
	{
		string text = "PropCeresPosterA";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERA.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERA.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poster_ceres_a_kanim"), "art_a", Grid.SceneLayer.Building, 2, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x0006A758 File Offset: 0x00068958
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x0006A75A File Offset: 0x0006895A
	public void OnSpawn(GameObject inst)
	{
	}
}
