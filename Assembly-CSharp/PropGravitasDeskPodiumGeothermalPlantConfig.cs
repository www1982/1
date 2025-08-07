using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class PropGravitasDeskPodiumGeothermalPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012C5 RID: 4805 RVA: 0x0006C08A File Offset: 0x0006A28A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060012C6 RID: 4806 RVA: 0x0006C091 File Offset: 0x0006A291
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012C7 RID: 4807 RVA: 0x0006C094 File Offset: 0x0006A294
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasDeskPodiumGeothermalPlant";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASDESKPODIUM.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_desk_podium_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new string[] { "dlc2geoplantinput" });
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x0006C13D File Offset: 0x0006A33D
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x0006C154 File Offset: 0x0006A354
	public void OnSpawn(GameObject inst)
	{
	}
}
