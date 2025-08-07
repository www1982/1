using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class DebrisPayloadConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060001FF RID: 511 RVA: 0x0000E668 File Offset: 0x0000C868
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x0000E66F File Offset: 0x0000C86F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000201 RID: 513 RVA: 0x0000E674 File Offset: 0x0000C874
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("DebrisPayload", ITEMS.DEBRISPAYLOAD.NAME, ITEMS.DEBRISPAYLOAD.DESC, 100f, true, Assets.GetAnim("rocket_debris_combined_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IgnoreMaterialCategory,
			GameTags.Experimental
		});
		RailGunPayload.Def def = gameObject.AddOrGetDef<RailGunPayload.Def>();
		def.attractToBeacons = false;
		def.clusterAnimSymbolSwapTarget = "debris1";
		def.randomClusterSymbolSwaps = new List<string> { "debris1", "debris2", "debris3" };
		def.worldAnimSymbolSwapTarget = "debris";
		def.randomWorldSymbolSwaps = new List<string> { "debris", "2_debris", "3_debris" };
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		gameObject.AddOrGet<LoopingSounds>();
		Storage storage = BuildingTemplates.CreateDefaultStorage(gameObject, false);
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.allowItemRemoval = false;
		storage.capacityKg = 5000f;
		DropAllWorkable dropAllWorkable = gameObject.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 30f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		clusterDestinationSelector.canNavigateFogOfWar = true;
		BallisticClusterGridEntity ballisticClusterGridEntity = gameObject.AddOrGet<BallisticClusterGridEntity>();
		ballisticClusterGridEntity.clusterAnimName = "rocket_debris_kanim";
		ballisticClusterGridEntity.isWorldEntity = true;
		ballisticClusterGridEntity.nameKey = new StringKey("STRINGS.ITEMS.DEBRISPAYLOAD.NAME");
		gameObject.AddOrGet<ClusterTraveler>();
		return gameObject;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x0000E834 File Offset: 0x0000CA34
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000203 RID: 515 RVA: 0x0000E836 File Offset: 0x0000CA36
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000147 RID: 327
	public const string ID = "DebrisPayload";

	// Token: 0x04000148 RID: 328
	public const float MASS = 100f;

	// Token: 0x04000149 RID: 329
	public const float MAX_STORAGE_KG_MASS = 5000f;

	// Token: 0x0400014A RID: 330
	public const float STARMAP_SPEED = 10f;
}
