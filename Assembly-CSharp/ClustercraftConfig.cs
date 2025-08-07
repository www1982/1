using System;
using UnityEngine;

// Token: 0x020002E8 RID: 744
public class ClustercraftConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F3B RID: 3899 RVA: 0x0005D321 File Offset: 0x0005B521
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F3C RID: 3900 RVA: 0x0005D328 File Offset: 0x0005B528
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F3D RID: 3901 RVA: 0x0005D32C File Offset: 0x0005B52C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("Clustercraft", "Clustercraft", true);
		SaveLoadRoot saveLoadRoot = gameObject.AddOrGet<SaveLoadRoot>();
		saveLoadRoot.DeclareOptionalComponent<WorldInventory>();
		saveLoadRoot.DeclareOptionalComponent<WorldContainer>();
		saveLoadRoot.DeclareOptionalComponent<OrbitalMechanics>();
		gameObject.AddOrGet<Clustercraft>();
		gameObject.AddOrGet<CraftModuleInterface>();
		gameObject.AddOrGet<UserNameable>();
		RocketClusterDestinationSelector rocketClusterDestinationSelector = gameObject.AddOrGet<RocketClusterDestinationSelector>();
		rocketClusterDestinationSelector.requireLaunchPadOnAsteroidDestination = true;
		rocketClusterDestinationSelector.assignable = true;
		rocketClusterDestinationSelector.shouldPointTowardsPath = true;
		gameObject.AddOrGet<ClusterTraveler>().stopAndNotifyWhenPathChanges = true;
		gameObject.AddOrGetDef<AlertStateManager.Def>();
		gameObject.AddOrGet<Notifier>();
		gameObject.AddOrGetDef<RocketSelfDestructMonitor.Def>();
		return gameObject;
	}

	// Token: 0x06000F3E RID: 3902 RVA: 0x0005D3B0 File Offset: 0x0005B5B0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000F3F RID: 3903 RVA: 0x0005D3B2 File Offset: 0x0005B5B2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040009F1 RID: 2545
	public const string ID = "Clustercraft";
}
