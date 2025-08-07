using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200038E RID: 910
public class PropFacilityHangingLightConfig : IEntityConfig
{
	// Token: 0x06001296 RID: 4758 RVA: 0x0006B7AC File Offset: 0x000699AC
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityHangingLight";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYLAMP.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYLAMP.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_light_kanim"), "off", Grid.SceneLayer.Building, 1, 4, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001297 RID: 4759 RVA: 0x0006B858 File Offset: 0x00069A58
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x0006B85C File Offset: 0x00069A5C
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
