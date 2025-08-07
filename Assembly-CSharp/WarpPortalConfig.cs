using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000439 RID: 1081
public class WarpPortalConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001670 RID: 5744 RVA: 0x0007F7D4 File Offset: 0x0007D9D4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x0007F7DB File Offset: 0x0007D9DB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x0007F7E0 File Offset: 0x0007D9E0
	public GameObject CreatePrefab()
	{
		string text = "WarpPortal";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.WARPPORTAL.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.WARPPORTAL.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("warp_portal_sender_kanim"), "idle", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		gameObject.AddTag(GameTags.WarpTech);
		gameObject.AddTag(GameTags.Gravitas);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<WarpPortal>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Ownable>().tintWhenUnassigned = false;
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("notes_teleportation", UI.USERMENUACTIONS.READLORE.SEARCH_TELEPORTER_SENDER));
		gameObject.AddOrGet<Prioritizable>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		return gameObject;
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0007F8DC File Offset: 0x0007DADC
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<WarpPortal>().workLayer = Grid.SceneLayer.Building;
		inst.GetComponent<Ownable>().slotID = Db.Get().AssignableSlots.WarpPortal.Id;
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		inst.GetComponent<Deconstructable>();
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x0007F931 File Offset: 0x0007DB31
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000D30 RID: 3376
	public const string ID = "WarpPortal";
}
