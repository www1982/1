using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200038F RID: 911
public class PropFacilityPaintingConfig : IEntityConfig
{
	// Token: 0x0600129A RID: 4762 RVA: 0x0006B8A8 File Offset: 0x00069AA8
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityPainting";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYPAINTING.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYPAINTING.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_painting_kanim"), "off", Grid.SceneLayer.Building, 3, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600129B RID: 4763 RVA: 0x0006B954 File Offset: 0x00069B54
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600129C RID: 4764 RVA: 0x0006B958 File Offset: 0x00069B58
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
