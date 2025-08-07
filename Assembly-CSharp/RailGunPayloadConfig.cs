using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020003B6 RID: 950
public class RailGunPayloadConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06001355 RID: 4949 RVA: 0x0006E130 File Offset: 0x0006C330
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x0006E137 File Offset: 0x0006C337
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x0006E13C File Offset: 0x0006C33C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("RailGunPayload", ITEMS.RAILGUNPAYLOAD.NAME, ITEMS.RAILGUNPAYLOAD.DESC, 200f, true, Assets.GetAnim("railgun_capsule_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 1f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IgnoreMaterialCategory,
			GameTags.Experimental
		});
		gameObject.AddOrGetDef<RailGunPayload.Def>().attractToBeacons = true;
		gameObject.AddComponent<LoopingSounds>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(gameObject, false);
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.allowItemRemoval = false;
		storage.capacityKg = 200f;
		DropAllWorkable dropAllWorkable = gameObject.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 30f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		BallisticClusterGridEntity ballisticClusterGridEntity = gameObject.AddOrGet<BallisticClusterGridEntity>();
		ballisticClusterGridEntity.clusterAnimName = "payload01_kanim";
		ballisticClusterGridEntity.isWorldEntity = true;
		ballisticClusterGridEntity.nameKey = new StringKey("STRINGS.ITEMS.RAILGUNPAYLOAD.NAME");
		gameObject.AddOrGet<ClusterTraveler>();
		return gameObject;
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x0006E280 File Offset: 0x0006C480
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x0006E282 File Offset: 0x0006C482
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000BB4 RID: 2996
	public const string ID = "RailGunPayload";

	// Token: 0x04000BB5 RID: 2997
	public const float MASS = 200f;

	// Token: 0x04000BB6 RID: 2998
	public const int LANDING_EDGE_PADDING = 3;
}
