using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000386 RID: 902
public class PropFacilityChairFlipConfig : IEntityConfig
{
	// Token: 0x06001276 RID: 4726 RVA: 0x0006AF54 File Offset: 0x00069154
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChairFlip";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHAIR.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chairFlip_kanim"), "off", Grid.SceneLayer.Building, 2, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001277 RID: 4727 RVA: 0x0006B000 File Offset: 0x00069200
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001278 RID: 4728 RVA: 0x0006B004 File Offset: 0x00069204
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
