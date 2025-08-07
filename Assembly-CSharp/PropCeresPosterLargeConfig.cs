using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class PropCeresPosterLargeConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600124E RID: 4686 RVA: 0x0006A828 File Offset: 0x00068A28
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x0006A82F File Offset: 0x00068A2F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x0006A834 File Offset: 0x00068A34
	public GameObject CreatePrefab()
	{
		string text = "PropCeresPosterLarge";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERLARGE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERLARGE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poster_ceres_7x5_kanim"), "art_7x5", Grid.SceneLayer.Building, 5, 7, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0006A8E0 File Offset: 0x00068AE0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x0006A8E2 File Offset: 0x00068AE2
	public void OnSpawn(GameObject inst)
	{
	}
}
