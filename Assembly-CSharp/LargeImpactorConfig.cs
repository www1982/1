using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B44 RID: 2884
public class LargeImpactorConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060055BA RID: 21946 RVA: 0x001F214B File Offset: 0x001F034B
	public string[] GetRequiredDlcIds()
	{
		return new string[] { "EXPANSION1_ID", "DLC4_ID" };
	}

	// Token: 0x060055BB RID: 21947 RVA: 0x001F2163 File Offset: 0x001F0363
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060055BC RID: 21948 RVA: 0x001F2168 File Offset: 0x001F0368
	GameObject IEntityConfig.CreatePrefab()
	{
		GameObject gameObject = LargeImpactorVanillaConfig.ConfigCommon("LargeImpactor", this.NAME);
		gameObject.AddOrGet<InfoDescription>().description = this.DESC;
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.canNavigateFogOfWar = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		clusterDestinationSelector.requireLaunchPadOnAsteroidDestination = false;
		clusterDestinationSelector.dodgesHiddenAsteroids = true;
		ClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = gameObject.AddOrGet<ClusterMapMeteorShowerVisualizer>();
		clusterMapMeteorShowerVisualizer.p_name = this.NAME;
		clusterMapMeteorShowerVisualizer.clusterAnimName = "shower_cluster_demolior_kanim";
		clusterMapMeteorShowerVisualizer.revealed = true;
		clusterMapMeteorShowerVisualizer.forceRevealed = true;
		clusterMapMeteorShowerVisualizer.isWorldEntity = true;
		ClusterTraveler clusterTraveler = gameObject.AddOrGet<ClusterTraveler>();
		clusterTraveler.revealsFogOfWarAsItTravels = true;
		clusterTraveler.peekRadius = 0;
		clusterTraveler.quickTravelToAsteroidIfInOrbit = false;
		ClusterMapLargeImpactor.Def def = gameObject.AddOrGetDef<ClusterMapLargeImpactor.Def>();
		def.name = this.NAME;
		def.description = this.DESC;
		def.eventID = "LargeImpactor";
		return gameObject;
	}

	// Token: 0x060055BD RID: 21949 RVA: 0x001F2231 File Offset: 0x001F0431
	void IEntityConfig.OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060055BE RID: 21950 RVA: 0x001F2233 File Offset: 0x001F0433
	void IEntityConfig.OnSpawn(GameObject inst)
	{
		inst.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.ImpactorStatus, inst.GetComponent<ClusterTraveler>());
		LargeImpactorVanillaConfig.SpawnCommon(inst);
	}

	// Token: 0x04003941 RID: 14657
	public const string ID = "LargeImpactor";

	// Token: 0x04003942 RID: 14658
	public string NAME = UI.SPACEDESTINATIONS.CLUSTERMAPMETEORS.LARGEIMACTOR.NAME;

	// Token: 0x04003943 RID: 14659
	public string DESC = UI.SPACEDESTINATIONS.CLUSTERMAPMETEORS.LARGEIMACTOR.DESCRIPTION;

	// Token: 0x04003944 RID: 14660
	public const string ANIMFILE = "shower_cluster_demolior_kanim";

	// Token: 0x04003945 RID: 14661
	public const int HEALTH = 1000;
}
