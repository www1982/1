using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003AC RID: 940
public class PropReceptionDeskConfig : IEntityConfig
{
	// Token: 0x06001325 RID: 4901 RVA: 0x0006D1F4 File Offset: 0x0006B3F4
	public GameObject CreatePrefab()
	{
		string text = "PropReceptionDesk";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPRECEPTIONDESK.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPRECEPTIONDESK.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_reception_kanim"), "off", Grid.SceneLayer.Building, 5, 3, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("email_pens", UI.USERMENUACTIONS.READLORE.SEARCH_ELLIESDESK));
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001326 RID: 4902 RVA: 0x0006D2BA File Offset: 0x0006B4BA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001327 RID: 4903 RVA: 0x0006D2BC File Offset: 0x0006B4BC
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
