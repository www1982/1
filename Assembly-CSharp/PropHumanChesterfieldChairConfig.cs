using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003E7 RID: 999
public class PropHumanChesterfieldChairConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600146D RID: 5229 RVA: 0x00075051 File Offset: 0x00073251
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600146E RID: 5230 RVA: 0x00075058 File Offset: 0x00073258
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x0007505C File Offset: 0x0007325C
	public GameObject CreatePrefab()
	{
		string text = "PropHumanChesterfieldChair";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANCHESTERFIELDCHAIR.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPHUMANCHESTERFIELDCHAIR.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("poi_chair_kanim"), "off", Grid.SceneLayer.Building, 5, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001470 RID: 5232 RVA: 0x000750EF File Offset: 0x000732EF
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001471 RID: 5233 RVA: 0x00075106 File Offset: 0x00073306
	public void OnSpawn(GameObject inst)
	{
	}
}
