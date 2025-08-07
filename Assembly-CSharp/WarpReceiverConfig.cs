using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class WarpReceiverConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001676 RID: 5750 RVA: 0x0007F93B File Offset: 0x0007DB3B
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001677 RID: 5751 RVA: 0x0007F942 File Offset: 0x0007DB42
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001678 RID: 5752 RVA: 0x0007F948 File Offset: 0x0007DB48
	public GameObject CreatePrefab()
	{
		string id = WarpReceiverConfig.ID;
		string text = global::STRINGS.BUILDINGS.PREFABS.WARPRECEIVER.NAME;
		string text2 = global::STRINGS.BUILDINGS.PREFABS.WARPRECEIVER.DESC;
		float num = 2000f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("warp_portal_receiver_kanim"), "idle", Grid.SceneLayer.Building, 3, 3, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		gameObject.AddTag(GameTags.WarpTech);
		gameObject.AddTag(GameTags.Gravitas);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGet<WarpReceiver>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Prioritizable>();
		LoreBearerUtil.AddLoreTo(gameObject, LoreBearerUtil.UnlockSpecificEntry("notes_AI", UI.USERMENUACTIONS.READLORE.SEARCH_TELEPORTER_RECEIVER));
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		return gameObject;
	}

	// Token: 0x06001679 RID: 5753 RVA: 0x0007FA38 File Offset: 0x0007DC38
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<WarpReceiver>().workLayer = Grid.SceneLayer.Building;
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		inst.GetComponent<Deconstructable>();
	}

	// Token: 0x0600167A RID: 5754 RVA: 0x0007FA63 File Offset: 0x0007DC63
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000D31 RID: 3377
	public static string ID = "WarpReceiver";
}
