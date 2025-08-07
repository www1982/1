using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000383 RID: 899
public class PropDlc2GeothermalCartConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001268 RID: 4712 RVA: 0x0006AC96 File Offset: 0x00068E96
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001269 RID: 4713 RVA: 0x0006AC9D File Offset: 0x00068E9D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x0006ACA0 File Offset: 0x00068EA0
	public GameObject CreatePrefab()
	{
		string text = "PropDlc2GeothermalCart";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPDLC2GEOTHERMALCART.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPDLC2GEOTHERMALCART.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_geothermal_cart_kanim"), "on", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x0006AD39 File Offset: 0x00068F39
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x0006AD50 File Offset: 0x00068F50
	public void OnSpawn(GameObject inst)
	{
	}
}
