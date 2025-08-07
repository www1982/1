using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003B1 RID: 945
public class PropTableConfig : IEntityConfig
{
	// Token: 0x0600133D RID: 4925 RVA: 0x0006D954 File Offset: 0x0006BB54
	public GameObject CreatePrefab()
	{
		string text = "PropTable";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPTABLE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPTABLE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("table_breakroom_kanim"), "off", Grid.SceneLayer.Building, 3, 1, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, new LoreBearerAction(LoreBearerUtil.UnlockNextJournalEntry));
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x0006D9FB File Offset: 0x0006BBFB
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600133F RID: 4927 RVA: 0x0006DA12 File Offset: 0x0006BC12
	public void OnSpawn(GameObject inst)
	{
	}
}
