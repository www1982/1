using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200038C RID: 908
public class PropFacilityDisplay : IEntityConfig
{
	// Token: 0x0600128E RID: 4750 RVA: 0x0006B584 File Offset: 0x00069784
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDisplay";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY1.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY1.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display1_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("display_prop1", UI.USERMENUACTIONS.READLORE.SEARCH_DISPLAY));
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x0600128F RID: 4751 RVA: 0x0006B64A File Offset: 0x0006984A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001290 RID: 4752 RVA: 0x0006B64C File Offset: 0x0006984C
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
