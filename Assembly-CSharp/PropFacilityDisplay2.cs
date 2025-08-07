using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200038A RID: 906
public class PropFacilityDisplay2 : IEntityConfig
{
	// Token: 0x06001286 RID: 4742 RVA: 0x0006B35C File Offset: 0x0006955C
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDisplay2";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY2.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDISPLAY2.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_display2_kanim"), "off", Grid.SceneLayer.Building, 2, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("display_prop2", UI.USERMENUACTIONS.READLORE.SEARCH_DISPLAY));
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001287 RID: 4743 RVA: 0x0006B422 File Offset: 0x00069622
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001288 RID: 4744 RVA: 0x0006B424 File Offset: 0x00069624
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
