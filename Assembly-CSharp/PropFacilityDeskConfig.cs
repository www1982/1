using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000389 RID: 905
public class PropFacilityDeskConfig : IEntityConfig
{
	// Token: 0x06001282 RID: 4738 RVA: 0x0006B248 File Offset: 0x00069448
	public GameObject CreatePrefab()
	{
		string text = "PropFacilityDesk";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDESK.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.PROPFACILITYDESK.DESC;
		float num = 50f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_desk_kanim"), "off", Grid.SceneLayer.Building, 4, 2, tier, PermittedRotations.R90, Orientation.Neutral, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Granite, true);
		component.Temperature = 294.15f;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("journal_magazine", UI.USERMENUACTIONS.READLORE.SEARCH_STERNSDESK));
		gameObject.AddOrGet<Demolishable>();
		gameObject.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		return gameObject;
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x0006B30E File Offset: 0x0006950E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x0006B310 File Offset: 0x00069510
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
