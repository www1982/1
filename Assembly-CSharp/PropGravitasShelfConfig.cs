using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class PropGravitasShelfConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001307 RID: 4871 RVA: 0x0006CBF2 File Offset: 0x0006ADF2
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001308 RID: 4872 RVA: 0x0006CBF9 File Offset: 0x0006ADF9
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001309 RID: 4873 RVA: 0x0006CBFC File Offset: 0x0006ADFC
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasShelf";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASSHELF.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASSHELF.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_shelf_kanim"), "off", Grid.SceneLayer.Building, 2, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x0006CC8F File Offset: 0x0006AE8F
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x0006CCA6 File Offset: 0x0006AEA6
	public void OnSpawn(GameObject inst)
	{
	}
}
