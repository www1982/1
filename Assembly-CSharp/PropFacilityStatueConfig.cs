using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000390 RID: 912
public class PropFacilityStatueConfig : IEntityConfig
{
	// Token: 0x0600129E RID: 4766 RVA: 0x0006B9A4 File Offset: 0x00069BA4
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityStatue";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYSTATUE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYSTATUE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_statue_kanim"), "off", Grid.SceneLayer.Building, 5, 9, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600129F RID: 4767 RVA: 0x0006BA51 File Offset: 0x00069C51
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060012A0 RID: 4768 RVA: 0x0006BA54 File Offset: 0x00069C54
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
