using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003AD RID: 941
public class PropSkeletonConfig : IEntityConfig
{
	// Token: 0x06001329 RID: 4905 RVA: 0x0006D308 File Offset: 0x0006B508
	public GameObject CreatePrefab()
	{
		string text = "PropSkeleton";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPSKELETON.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPSKELETON.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER5;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("skeleton_poi_kanim"), "off", Grid.SceneLayer.Building, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Creature, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		return gameObject;
	}

	// Token: 0x0600132A RID: 4906 RVA: 0x0006D39B File Offset: 0x0006B59B
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x0600132B RID: 4907 RVA: 0x0006D3B2 File Offset: 0x0006B5B2
	public void OnSpawn(GameObject inst)
	{
	}
}
