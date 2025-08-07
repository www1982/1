using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class PropGravitasJar2Config : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060012E8 RID: 4840 RVA: 0x0006C67A File Offset: 0x0006A87A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012E9 RID: 4841 RVA: 0x0006C681 File Offset: 0x0006A881
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x0006C684 File Offset: 0x0006A884
	public GameObject CreatePrefab()
	{
		string text = "PropGravitasJar2";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR2.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPGRAVITASJAR2.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_jar2_kanim"), "off", Grid.SceneLayer.Building, 1, 1, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextDimensionalLore));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x0006C729 File Offset: 0x0006A929
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x060012EC RID: 4844 RVA: 0x0006C740 File Offset: 0x0006A940
	public void OnSpawn(GameObject inst)
	{
	}
}
