using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000388 RID: 904
public class PropFacilityCouchConfig : IEntityConfig
{
	// Token: 0x0600127E RID: 4734 RVA: 0x0006B14C File Offset: 0x0006934C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityCouch";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCOUCH.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCOUCH.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_couch_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600127F RID: 4735 RVA: 0x0006B1F8 File Offset: 0x000693F8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x0006B1FC File Offset: 0x000693FC
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
