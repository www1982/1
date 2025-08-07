using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000047 RID: 71
public class ClustercraftInteriorDoorConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000160 RID: 352 RVA: 0x0000A691 File Offset: 0x00008891
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000A698 File Offset: 0x00008898
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000A69C File Offset: 0x0000889C
	public GameObject CreatePrefab()
	{
		string id = ClustercraftInteriorDoorConfig.ID;
		string text = global::STRINGS.BUILDINGS.PREFABS.CLUSTERCRAFTINTERIORDOOR.NAME;
		string text2 = global::STRINGS.BUILDINGS.PREFABS.CLUSTERCRAFTINTERIORDOOR.DESC;
		float num = 400f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("rocket_hatch_door_kanim"), "closed", Grid.SceneLayer.TileMain, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<KBatchedAnimController>().fgLayer = Grid.SceneLayer.InteriorWall;
		gameObject.AddOrGet<ClustercraftInteriorDoor>();
		gameObject.AddOrGet<AssignmentGroupController>().generateGroupOnStart = false;
		gameObject.AddOrGet<NavTeleporter>().offset = new CellOffset(1, 0);
		gameObject.AddOrGet<AccessControl>();
		return gameObject;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000A781 File Offset: 0x00008981
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000A798 File Offset: 0x00008998
	public void OnSpawn(GameObject inst)
	{
		PrimaryElement component = inst.GetComponent<PrimaryElement>();
		OccupyArea component2 = inst.GetComponent<OccupyArea>();
		int num = Grid.PosToCell(inst);
		CellOffset[] occupiedCellsOffsets = component2.OccupiedCellsOffsets;
		int[] array = new int[occupiedCellsOffsets.Length];
		for (int i = 0; i < occupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = occupiedCellsOffsets[i];
			int num2 = Grid.OffsetCell(num, cellOffset);
			array[i] = num2;
		}
		foreach (int num3 in array)
		{
			Grid.HasDoor[num3] = true;
			SimMessages.SetCellProperties(num3, 8);
			Grid.RenderedByWorld[num3] = false;
			World.Instance.groundRenderer.MarkDirty(num3);
			SimMessages.ReplaceAndDisplaceElement(num3, component.ElementID, CellEventLogger.Instance.DoorClose, component.Mass / 2f, component.Temperature, byte.MaxValue, 0, -1);
			SimMessages.SetCellProperties(num3, 4);
		}
	}

	// Token: 0x040000D9 RID: 217
	public static string ID = "ClustercraftInteriorDoor";
}
