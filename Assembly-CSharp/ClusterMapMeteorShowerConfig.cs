using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000B32 RID: 2866
public class ClusterMapMeteorShowerConfig : IMultiEntityConfig
{
	// Token: 0x06005499 RID: 21657 RVA: 0x001EBDB4 File Offset: 0x001E9FB4
	public static string GetFullID(string id)
	{
		return "ClusterMapMeteorShower_" + id;
	}

	// Token: 0x0600549A RID: 21658 RVA: 0x001EBDC1 File Offset: 0x001E9FC1
	public static string GetReverseFullID(string fullID)
	{
		return fullID.Replace("ClusterMapMeteorShower_", "");
	}

	// Token: 0x0600549B RID: 21659 RVA: 0x001EBDD4 File Offset: 0x001E9FD4
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		if (!DlcManager.IsExpansion1Active())
		{
			return list;
		}
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Biological", "ClusterBiologicalShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.SLIME.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.SLIME.DESCRIPTION, "shower_cluster_biological_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Snow", "ClusterSnowShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.SNOW.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.SNOW.DESCRIPTION, "shower_cluster_snow_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Ice", "ClusterIceShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.ICE.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.ICE.DESCRIPTION, "shower_cluster_ice_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Copper", "ClusterCopperShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.COPPER.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.COPPER.DESCRIPTION, "shower_cluster_copper_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Iron", "ClusterIronShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.IRON.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.IRON.DESCRIPTION, "shower_cluster_iron_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Gold", "ClusterGoldShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.GOLD.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.GOLD.DESCRIPTION, "shower_cluster_gold_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Uranium", "ClusterUraniumShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.URANIUM.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.URANIUM.DESCRIPTION, "shower_cluster_uranium_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("HeavyDust", "ClusterRegolithShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.HEAVYDUST.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.HEAVYDUST.DESCRIPTION, "shower_cluster_regolith_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("LightDust", "ClusterLightRegolithShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.LIGHTDUST.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.LIGHTDUST.DESCRIPTION, "shower_cluster_light_regolith_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Moo", "GassyMooteorEvent", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.MOO.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.MOO.DESCRIPTION, "shower_mooteor_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Regolith", "MeteorShowerDustEvent", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.REGOLITH.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.REGOLITH.DESCRIPTION, "shower_cluster_regolith_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("Oxylite", "ClusterOxyliteShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.OXYLITE.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.OXYLITE.DESCRIPTION, "shower_cluster_oxylite_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("BleachStone", "ClusterBleachStoneShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.BLEACHSTONE.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.BLEACHSTONE.DESCRIPTION, "shower_cluster_biological_kanim", "idle_loop", "ui", DlcManager.EXPANSION1, null, SimHashes.Unobtanium));
		list.Add(ClusterMapMeteorShowerConfig.CreateClusterMeteor("IceAndTrees", "ClusterIceAndTreesShower", UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.ICEANDTREES.NAME, UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.ICEANDTREES.DESCRIPTION, "shower_cluster_ice_kanim", "idle_loop", "ui", DlcManager.EXPANSION1.Append(DlcManager.DLC2), null, SimHashes.Unobtanium));
		list.RemoveAll((GameObject x) => x == null);
		return list;
	}

	// Token: 0x0600549C RID: 21660 RVA: 0x001EC1CC File Offset: 0x001EA3CC
	public static GameObject CreateClusterMeteor(string id, string meteorEventID, string name, string desc, string animFile, string initial_anim = "idle_loop", string ui_anim = "ui", string[] requiredDlcIds = null, string[] forbiddenDlcIds = null, SimHashes element = SimHashes.Unobtanium)
	{
		if (!DlcManager.IsCorrectDlcSubscribed(requiredDlcIds, forbiddenDlcIds))
		{
			return null;
		}
		GameObject gameObject = EntityTemplates.CreateLooseEntity(ClusterMapMeteorShowerConfig.GetFullID(id), name, desc, 25f, true, Assets.GetAnim(animFile), initial_anim, Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, false, SORTORDER.KEEPSAKES, element, new List<Tag>());
		gameObject.AddOrGet<KSelectable>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.GetComponent<KBatchedAnimController>().initialMode = KAnim.PlayMode.Loop;
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.canNavigateFogOfWar = true;
		clusterDestinationSelector.requireAsteroidDestination = true;
		clusterDestinationSelector.requireLaunchPadOnAsteroidDestination = false;
		clusterDestinationSelector.dodgesHiddenAsteroids = true;
		ClusterMapMeteorShowerVisualizer clusterMapMeteorShowerVisualizer = gameObject.AddOrGet<ClusterMapMeteorShowerVisualizer>();
		clusterMapMeteorShowerVisualizer.p_name = name;
		clusterMapMeteorShowerVisualizer.clusterAnimName = animFile;
		ClusterTraveler clusterTraveler = gameObject.AddOrGet<ClusterTraveler>();
		clusterTraveler.revealsFogOfWarAsItTravels = false;
		clusterTraveler.quickTravelToAsteroidIfInOrbit = false;
		ClusterMapMeteorShower.Def def = gameObject.AddOrGetDef<ClusterMapMeteorShower.Def>();
		def.name = name;
		def.description = desc;
		def.name_Hidden = UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.UNIDENTIFIED.NAME;
		def.description_Hidden = UI.SPACEDESTINATIONS.CLUSTERMAPMETEORSHOWERS.UNIDENTIFIED.DESCRIPTION;
		def.eventID = meteorEventID;
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.requiredDlcIds = requiredDlcIds;
		component.forbiddenDlcIds = forbiddenDlcIds;
		return gameObject;
	}

	// Token: 0x0600549D RID: 21661 RVA: 0x001EC2DC File Offset: 0x001EA4DC
	[Obsolete]
	public static GameObject CreateClusterMeteor(string id, string meteorEventID, string name, string desc, string animFile, string initial_anim = "idle_loop", string ui_anim = "ui", string[] dlcIDs = null, SimHashes element = SimHashes.Unobtanium)
	{
		DlcRestrictionsUtil.TemporaryHelperObject transientHelperObjectFromAllowList = DlcRestrictionsUtil.GetTransientHelperObjectFromAllowList(dlcIDs);
		return ClusterMapMeteorShowerConfig.CreateClusterMeteor(id, meteorEventID, name, desc, animFile, initial_anim, ui_anim, transientHelperObjectFromAllowList.GetRequiredDlcIds(), transientHelperObjectFromAllowList.GetForbiddenDlcIds(), element);
	}

	// Token: 0x0600549E RID: 21662 RVA: 0x001EC30E File Offset: 0x001EA50E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600549F RID: 21663 RVA: 0x001EC310 File Offset: 0x001EA510
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040038E5 RID: 14565
	public const string IDENTIFY_AUDIO_NAME = "ClusterMapMeteor_Reveal";

	// Token: 0x040038E6 RID: 14566
	public const string ID_SIGNATURE = "ClusterMapMeteorShower_";
}
