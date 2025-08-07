using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class PropDeskConfig : IEntityConfig
{
	// Token: 0x0600125E RID: 4702 RVA: 0x0006AAFC File Offset: 0x00068CFC
	public GameObject CreatePrefab()
	{
		string text = "PropDesk";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPDESK.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPDESK.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("setpiece_desk_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextEmail));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x0006ABA3 File Offset: 0x00068DA3
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06001260 RID: 4704 RVA: 0x0006ABBA File Offset: 0x00068DBA
	public void OnSpawn(GameObject inst)
	{
	}
}
