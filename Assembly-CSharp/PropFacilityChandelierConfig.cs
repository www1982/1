using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000387 RID: 903
public class PropFacilityChandelierConfig : IEntityConfig
{
	// Token: 0x0600127A RID: 4730 RVA: 0x0006B050 File Offset: 0x00069250
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityChandelier";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHANDELIER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYCHANDELIER.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_chandelier_kanim"), "off", Grid.SceneLayer.Building, 5, 7, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600127B RID: 4731 RVA: 0x0006B0FC File Offset: 0x000692FC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600127C RID: 4732 RVA: 0x0006B100 File Offset: 0x00069300
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
