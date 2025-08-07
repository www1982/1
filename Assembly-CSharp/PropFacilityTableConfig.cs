using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000391 RID: 913
public class PropFacilityTableConfig : IEntityConfig
{
	// Token: 0x060012A2 RID: 4770 RVA: 0x0006BAA0 File Offset: 0x00069CA0
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityTable";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYTABLE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYTABLE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_table_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x060012A3 RID: 4771 RVA: 0x0006BB4C File Offset: 0x00069D4C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060012A4 RID: 4772 RVA: 0x0006BB50 File Offset: 0x00069D50
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
