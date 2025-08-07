using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class PropFacilityWallDegreeConfig : IEntityConfig
{
	// Token: 0x060012A6 RID: 4774 RVA: 0x0006BB9C File Offset: 0x00069D9C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityWallDegree";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYWALLDEGREE.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYWALLDEGREE.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_degree_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x060012A7 RID: 4775 RVA: 0x0006BC48 File Offset: 0x00069E48
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060012A8 RID: 4776 RVA: 0x0006BC4C File Offset: 0x00069E4C
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
