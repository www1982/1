using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E8 RID: 1000
public class PropHumanChesterfieldSofaConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001473 RID: 5235 RVA: 0x00075110 File Offset: 0x00073310
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x00075117 File Offset: 0x00073317
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x0007511C File Offset: 0x0007331C
	public GameObject CreatePrefab()
	{
		string text = "PropHumanChesterfieldSofa";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANCHESTERFIELDSOFA.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANCHESTERFIELDSOFA.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_couch_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000751AF File Offset: 0x000733AF
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000751C6 File Offset: 0x000733C6
	public void OnSpawn(GameObject inst)
	{
	}
}
