using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class PropDlc2Display1Config : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001262 RID: 4706 RVA: 0x0006ABC4 File Offset: 0x00068DC4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x0006ABCB File Offset: 0x00068DCB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001264 RID: 4708 RVA: 0x0006ABD0 File Offset: 0x00068DD0
	public GameObject CreatePrefab()
	{
		string text = "PropDlc2Display1";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPDLC2DISPLAY1.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPDLC2DISPLAY1.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display_showroom_kanim"), "off", Grid.SceneLayer.Building, 1, 3, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextEmail));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x0006AC75 File Offset: 0x00068E75
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x0006AC8C File Offset: 0x00068E8C
	public void OnSpawn(GameObject inst)
	{
	}
}
