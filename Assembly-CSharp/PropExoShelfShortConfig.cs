using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E5 RID: 997
public class PropExoShelfShortConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001463 RID: 5219 RVA: 0x00074DB4 File Offset: 0x00072FB4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001464 RID: 5220 RVA: 0x00074DBB File Offset: 0x00072FBB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001465 RID: 5221 RVA: 0x00074DC0 File Offset: 0x00072FC0
	public GameObject CreatePrefab()
	{
		string text = "PropExoShelfShort";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPEXOSHELSHORT.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPEXOSHELSHORT.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_shelf_short_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001466 RID: 5222 RVA: 0x00074E53 File Offset: 0x00073053
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001467 RID: 5223 RVA: 0x00074E6A File Offset: 0x0007306A
	public void OnSpawn(GameObject inst)
	{
	}
}
