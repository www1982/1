using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003A0 RID: 928
public class PropGravitasLabTableConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012EE RID: 4846 RVA: 0x0006C74A File Offset: 0x0006A94A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012EF RID: 4847 RVA: 0x0006C751 File Offset: 0x0006A951
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012F0 RID: 4848 RVA: 0x0006C754 File Offset: 0x0006A954
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasLabTable";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASLABTABLE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASLABTABLE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_lab_table_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject);
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x0006C7ED File Offset: 0x0006A9ED
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x0006C804 File Offset: 0x0006AA04
	public void OnSpawn(GameObject inst)
	{
	}
}
