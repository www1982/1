using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000395 RID: 917
public class PropGravitasDecorativeWindowConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012B4 RID: 4788 RVA: 0x0006BE26 File Offset: 0x0006A026
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012B5 RID: 4789 RVA: 0x0006BE2D File Offset: 0x0006A02D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012B6 RID: 4790 RVA: 0x0006BE30 File Offset: 0x0006A030
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDecorativeWindow";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDECORATIVEWINDOW.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDECORATIVEWINDOW.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER2;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_top_window_kanim"), "on", Grid.SceneLayer.Building, 2, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Glass, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012B7 RID: 4791 RVA: 0x0006BEC3 File Offset: 0x0006A0C3
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012B8 RID: 4792 RVA: 0x0006BEDA File Offset: 0x0006A0DA
	public void OnSpawn(GameObject inst)
	{
	}
}
