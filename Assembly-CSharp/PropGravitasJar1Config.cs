using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039E RID: 926
public class PropGravitasJar1Config : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012E2 RID: 4834 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x0006C5AF File Offset: 0x0006A7AF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x0006C5B4 File Offset: 0x0006A7B4
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasJar1";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR1.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR1.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_jar1_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x0006C659 File Offset: 0x0006A859
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x0006C670 File Offset: 0x0006A870
	public void OnSpawn(GameObject inst)
	{
	}
}
