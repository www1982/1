using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000399 RID: 921
public class PropGravitasDisplay4Config : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012CB RID: 4811 RVA: 0x0006C15E File Offset: 0x0006A35E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x0006C165 File Offset: 0x0006A365
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x0006C168 File Offset: 0x0006A368
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDisplay4";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDISPLAY4.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDISPLAY4.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display4_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0006C20D File Offset: 0x0006A40D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x0006C224 File Offset: 0x0006A424
	public void OnSpawn(GameObject inst)
	{
	}
}
