using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000385 RID: 901
public class PropFacilityChairConfig : IEntityConfig
{
	// Token: 0x06001272 RID: 4722 RVA: 0x0006AE58 File Offset: 0x00069058
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChair";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chair_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001273 RID: 4723 RVA: 0x0006AF04 File Offset: 0x00069104
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001274 RID: 4724 RVA: 0x0006AF08 File Offset: 0x00069108
	public void OnSpawn(GameObject inst)
	{
		OccupyArea component = inst.GetComponent<OccupyArea>();
		int num = Grid.PosToCell(inst);
		foreach (CellOffset cellOffset in component.OccupiedCellsOffsets)
		{
			Grid.GravitasFacility[Grid.OffsetCell(num, cellOffset)] = true;
		}
	}
}
