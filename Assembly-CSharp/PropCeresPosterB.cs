using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class PropCeresPosterB : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001248 RID: 4680 RVA: 0x0006A764 File Offset: 0x00068964
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x0006A76B File Offset: 0x0006896B
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x0006A770 File Offset: 0x00068970
	public GameObject CreatePrefab()
	{
		string text = "PropCeresPosterB";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERB.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPCERESPOSTERB.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poster_ceres_b_kanim"), "art_b", Grid.SceneLayer.Building, 2, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x0006A81C File Offset: 0x00068A1C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x0006A81E File Offset: 0x00068A1E
	public void OnSpawn(GameObject inst)
	{
	}
}
