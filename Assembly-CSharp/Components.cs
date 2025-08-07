using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000835 RID: 2101
public class Components
{
	// Token: 0x06003995 RID: 14741 RVA: 0x0013FCFC File Offset: 0x0013DEFC
	public static Components.Cmps<MinionIdentity> GetMinionIdentitiesByModel(Tag tag)
	{
		Components.Cmps<MinionIdentity> cmps = null;
		if (Components.MinionIdentitiesByModel.TryGetValue(tag, out cmps))
		{
			return cmps;
		}
		return new Components.Cmps<MinionIdentity>();
	}

	// Token: 0x040022ED RID: 8941
	public static Components.Cmps<RobotAi.Instance> LiveRobotsIdentities = new Components.Cmps<RobotAi.Instance>();

	// Token: 0x040022EE RID: 8942
	public static Components.Cmps<MinionIdentity> LiveMinionIdentities = new Components.Cmps<MinionIdentity>();

	// Token: 0x040022EF RID: 8943
	public static Components.Cmps<MinionIdentity> MinionIdentities = new Components.Cmps<MinionIdentity>();

	// Token: 0x040022F0 RID: 8944
	public static Components.Cmps<StoredMinionIdentity> StoredMinionIdentities = new Components.Cmps<StoredMinionIdentity>();

	// Token: 0x040022F1 RID: 8945
	public static Components.Cmps<MinionStorage> MinionStorages = new Components.Cmps<MinionStorage>();

	// Token: 0x040022F2 RID: 8946
	public static Components.Cmps<MinionResume> MinionResumes = new Components.Cmps<MinionResume>();

	// Token: 0x040022F3 RID: 8947
	public static Dictionary<Tag, Components.Cmps<MinionIdentity>> MinionIdentitiesByModel = new Dictionary<Tag, Components.Cmps<MinionIdentity>>();

	// Token: 0x040022F4 RID: 8948
	public static Dictionary<Tag, Components.Cmps<MinionIdentity>> LiveMinionIdentitiesByModel = new Dictionary<Tag, Components.Cmps<MinionIdentity>>();

	// Token: 0x040022F5 RID: 8949
	public static Components.CmpsByWorld<Sleepable> NormalBeds = new Components.CmpsByWorld<Sleepable>();

	// Token: 0x040022F6 RID: 8950
	public static Components.Cmps<IUsable> Toilets = new Components.Cmps<IUsable>();

	// Token: 0x040022F7 RID: 8951
	public static Components.Cmps<GunkEmptierWorkable> GunkExtractors = new Components.Cmps<GunkEmptierWorkable>();

	// Token: 0x040022F8 RID: 8952
	public static Components.Cmps<Pickupable> Pickupables = new Components.Cmps<Pickupable>();

	// Token: 0x040022F9 RID: 8953
	public static Components.Cmps<Brain> Brains = new Components.Cmps<Brain>();

	// Token: 0x040022FA RID: 8954
	public static Components.Cmps<BuildingComplete> BuildingCompletes = new Components.Cmps<BuildingComplete>();

	// Token: 0x040022FB RID: 8955
	public static Components.Cmps<Notifier> Notifiers = new Components.Cmps<Notifier>();

	// Token: 0x040022FC RID: 8956
	public static Components.Cmps<Fabricator> Fabricators = new Components.Cmps<Fabricator>();

	// Token: 0x040022FD RID: 8957
	public static Components.Cmps<Refinery> Refineries = new Components.Cmps<Refinery>();

	// Token: 0x040022FE RID: 8958
	public static Components.CmpsByWorld<PlantablePlot> PlantablePlots = new Components.CmpsByWorld<PlantablePlot>();

	// Token: 0x040022FF RID: 8959
	public static Components.Cmps<Ladder> Ladders = new Components.Cmps<Ladder>();

	// Token: 0x04002300 RID: 8960
	public static Components.Cmps<NavTeleporter> NavTeleporters = new Components.Cmps<NavTeleporter>();

	// Token: 0x04002301 RID: 8961
	public static Components.Cmps<ITravelTubePiece> ITravelTubePieces = new Components.Cmps<ITravelTubePiece>();

	// Token: 0x04002302 RID: 8962
	public static Components.CmpsByWorld<CreatureFeeder> CreatureFeeders = new Components.CmpsByWorld<CreatureFeeder>();

	// Token: 0x04002303 RID: 8963
	public static Components.CmpsByWorld<MilkFeeder.Instance> MilkFeeders = new Components.CmpsByWorld<MilkFeeder.Instance>();

	// Token: 0x04002304 RID: 8964
	public static Components.Cmps<Light2D> Light2Ds = new Components.Cmps<Light2D>();

	// Token: 0x04002305 RID: 8965
	public static Components.Cmps<Radiator> Radiators = new Components.Cmps<Radiator>();

	// Token: 0x04002306 RID: 8966
	public static Components.Cmps<Edible> Edibles = new Components.Cmps<Edible>();

	// Token: 0x04002307 RID: 8967
	public static Components.Cmps<Diggable> Diggables = new Components.Cmps<Diggable>();

	// Token: 0x04002308 RID: 8968
	public static Components.Cmps<IResearchCenter> ResearchCenters = new Components.Cmps<IResearchCenter>();

	// Token: 0x04002309 RID: 8969
	public static Components.Cmps<Harvestable> Harvestables = new Components.Cmps<Harvestable>();

	// Token: 0x0400230A RID: 8970
	public static Components.Cmps<HarvestDesignatable> HarvestDesignatables = new Components.Cmps<HarvestDesignatable>();

	// Token: 0x0400230B RID: 8971
	public static Components.Cmps<Uprootable> Uprootables = new Components.Cmps<Uprootable>();

	// Token: 0x0400230C RID: 8972
	public static Components.Cmps<Health> Health = new Components.Cmps<Health>();

	// Token: 0x0400230D RID: 8973
	public static Components.Cmps<Equipment> Equipment = new Components.Cmps<Equipment>();

	// Token: 0x0400230E RID: 8974
	public static Components.Cmps<FactionAlignment> FactionAlignments = new Components.Cmps<FactionAlignment>();

	// Token: 0x0400230F RID: 8975
	public static Components.Cmps<FactionAlignment> PlayerTargeted = new Components.Cmps<FactionAlignment>();

	// Token: 0x04002310 RID: 8976
	public static Components.Cmps<Telepad> Telepads = new Components.Cmps<Telepad>();

	// Token: 0x04002311 RID: 8977
	public static Components.Cmps<Generator> Generators = new Components.Cmps<Generator>();

	// Token: 0x04002312 RID: 8978
	public static Components.Cmps<EnergyConsumer> EnergyConsumers = new Components.Cmps<EnergyConsumer>();

	// Token: 0x04002313 RID: 8979
	public static Components.Cmps<Battery> Batteries = new Components.Cmps<Battery>();

	// Token: 0x04002314 RID: 8980
	public static Components.Cmps<Breakable> Breakables = new Components.Cmps<Breakable>();

	// Token: 0x04002315 RID: 8981
	public static Components.Cmps<Crop> Crops = new Components.Cmps<Crop>();

	// Token: 0x04002316 RID: 8982
	public static Components.Cmps<Prioritizable> Prioritizables = new Components.Cmps<Prioritizable>();

	// Token: 0x04002317 RID: 8983
	public static Components.Cmps<Clinic> Clinics = new Components.Cmps<Clinic>();

	// Token: 0x04002318 RID: 8984
	public static Components.Cmps<HandSanitizer> HandSanitizers = new Components.Cmps<HandSanitizer>();

	// Token: 0x04002319 RID: 8985
	public static Components.Cmps<EntityCellVisualizer> EntityCellVisualizers = new Components.Cmps<EntityCellVisualizer>();

	// Token: 0x0400231A RID: 8986
	public static Components.Cmps<RoleStation> RoleStations = new Components.Cmps<RoleStation>();

	// Token: 0x0400231B RID: 8987
	public static Components.Cmps<Telescope> Telescopes = new Components.Cmps<Telescope>();

	// Token: 0x0400231C RID: 8988
	public static Components.Cmps<Capturable> Capturables = new Components.Cmps<Capturable>();

	// Token: 0x0400231D RID: 8989
	public static Components.Cmps<NotCapturable> NotCapturables = new Components.Cmps<NotCapturable>();

	// Token: 0x0400231E RID: 8990
	public static Components.Cmps<DiseaseSourceVisualizer> DiseaseSourceVisualizers = new Components.Cmps<DiseaseSourceVisualizer>();

	// Token: 0x0400231F RID: 8991
	public static Components.Cmps<Grave> Graves = new Components.Cmps<Grave>();

	// Token: 0x04002320 RID: 8992
	public static Components.Cmps<AttachableBuilding> AttachableBuildings = new Components.Cmps<AttachableBuilding>();

	// Token: 0x04002321 RID: 8993
	public static Components.Cmps<BuildingAttachPoint> BuildingAttachPoints = new Components.Cmps<BuildingAttachPoint>();

	// Token: 0x04002322 RID: 8994
	public static Components.Cmps<MinionAssignablesProxy> MinionAssignablesProxy = new Components.Cmps<MinionAssignablesProxy>();

	// Token: 0x04002323 RID: 8995
	public static Components.Cmps<ComplexFabricator> ComplexFabricators = new Components.Cmps<ComplexFabricator>();

	// Token: 0x04002324 RID: 8996
	public static Components.Cmps<MonumentPart> MonumentParts = new Components.Cmps<MonumentPart>();

	// Token: 0x04002325 RID: 8997
	public static Components.Cmps<PlantableSeed> PlantableSeeds = new Components.Cmps<PlantableSeed>();

	// Token: 0x04002326 RID: 8998
	public static Components.Cmps<IBasicBuilding> BasicBuildings = new Components.Cmps<IBasicBuilding>();

	// Token: 0x04002327 RID: 8999
	public static Components.Cmps<Painting> Paintings = new Components.Cmps<Painting>();

	// Token: 0x04002328 RID: 9000
	public static Components.Cmps<BuildingComplete> TemplateBuildings = new Components.Cmps<BuildingComplete>();

	// Token: 0x04002329 RID: 9001
	public static Components.Cmps<Teleporter> Teleporters = new Components.Cmps<Teleporter>();

	// Token: 0x0400232A RID: 9002
	public static Components.Cmps<MutantPlant> MutantPlants = new Components.Cmps<MutantPlant>();

	// Token: 0x0400232B RID: 9003
	public static Components.Cmps<LandingBeacon.Instance> LandingBeacons = new Components.Cmps<LandingBeacon.Instance>();

	// Token: 0x0400232C RID: 9004
	public static Components.Cmps<HighEnergyParticle> HighEnergyParticles = new Components.Cmps<HighEnergyParticle>();

	// Token: 0x0400232D RID: 9005
	public static Components.Cmps<HighEnergyParticlePort> HighEnergyParticlePorts = new Components.Cmps<HighEnergyParticlePort>();

	// Token: 0x0400232E RID: 9006
	public static Components.Cmps<Clustercraft> Clustercrafts = new Components.Cmps<Clustercraft>();

	// Token: 0x0400232F RID: 9007
	public static Components.Cmps<ClustercraftInteriorDoor> ClusterCraftInteriorDoors = new Components.Cmps<ClustercraftInteriorDoor>();

	// Token: 0x04002330 RID: 9008
	public static Components.Cmps<PassengerRocketModule> PassengerRocketModules = new Components.Cmps<PassengerRocketModule>();

	// Token: 0x04002331 RID: 9009
	public static Components.Cmps<ClusterTraveler> ClusterTravelers = new Components.Cmps<ClusterTraveler>();

	// Token: 0x04002332 RID: 9010
	public static Components.Cmps<LaunchPad> LaunchPads = new Components.Cmps<LaunchPad>();

	// Token: 0x04002333 RID: 9011
	public static Components.Cmps<WarpReceiver> WarpReceivers = new Components.Cmps<WarpReceiver>();

	// Token: 0x04002334 RID: 9012
	public static Components.Cmps<RocketControlStation> RocketControlStations = new Components.Cmps<RocketControlStation>();

	// Token: 0x04002335 RID: 9013
	public static Components.Cmps<Reactor> NuclearReactors = new Components.Cmps<Reactor>();

	// Token: 0x04002336 RID: 9014
	public static Components.Cmps<BuildingComplete> EntombedBuildings = new Components.Cmps<BuildingComplete>();

	// Token: 0x04002337 RID: 9015
	public static Components.Cmps<SpaceArtifact> SpaceArtifacts = new Components.Cmps<SpaceArtifact>();

	// Token: 0x04002338 RID: 9016
	public static Components.Cmps<ArtifactAnalysisStationWorkable> ArtifactAnalysisStations = new Components.Cmps<ArtifactAnalysisStationWorkable>();

	// Token: 0x04002339 RID: 9017
	public static Components.Cmps<RocketConduitReceiver> RocketConduitReceivers = new Components.Cmps<RocketConduitReceiver>();

	// Token: 0x0400233A RID: 9018
	public static Components.Cmps<RocketConduitSender> RocketConduitSenders = new Components.Cmps<RocketConduitSender>();

	// Token: 0x0400233B RID: 9019
	public static Components.Cmps<LogicBroadcaster> LogicBroadcasters = new Components.Cmps<LogicBroadcaster>();

	// Token: 0x0400233C RID: 9020
	public static Components.Cmps<Telephone> Telephones = new Components.Cmps<Telephone>();

	// Token: 0x0400233D RID: 9021
	public static Components.Cmps<MissionControlWorkable> MissionControlWorkables = new Components.Cmps<MissionControlWorkable>();

	// Token: 0x0400233E RID: 9022
	public static Components.Cmps<MissionControlClusterWorkable> MissionControlClusterWorkables = new Components.Cmps<MissionControlClusterWorkable>();

	// Token: 0x0400233F RID: 9023
	public static Components.Cmps<MinorFossilDigSite.Instance> MinorFossilDigSites = new Components.Cmps<MinorFossilDigSite.Instance>();

	// Token: 0x04002340 RID: 9024
	public static Components.Cmps<MajorFossilDigSite.Instance> MajorFossilDigSites = new Components.Cmps<MajorFossilDigSite.Instance>();

	// Token: 0x04002341 RID: 9025
	public static Components.Cmps<GameObject> FoodRehydrators = new Components.Cmps<GameObject>();

	// Token: 0x04002342 RID: 9026
	public static Components.CmpsByWorld<SocialGatheringPoint> SocialGatheringPoints = new Components.CmpsByWorld<SocialGatheringPoint>();

	// Token: 0x04002343 RID: 9027
	public static Components.CmpsByWorld<Geyser> Geysers = new Components.CmpsByWorld<Geyser>();

	// Token: 0x04002344 RID: 9028
	public static Components.CmpsByWorld<GeoTuner.Instance> GeoTuners = new Components.CmpsByWorld<GeoTuner.Instance>();

	// Token: 0x04002345 RID: 9029
	public static Components.CmpsByWorld<CritterCondo.Instance> CritterCondos = new Components.CmpsByWorld<CritterCondo.Instance>();

	// Token: 0x04002346 RID: 9030
	public static Components.CmpsByWorld<GeothermalController> GeothermalControllers = new Components.CmpsByWorld<GeothermalController>();

	// Token: 0x04002347 RID: 9031
	public static Components.CmpsByWorld<GeothermalVent> GeothermalVents = new Components.CmpsByWorld<GeothermalVent>();

	// Token: 0x04002348 RID: 9032
	public static Components.CmpsByWorld<RemoteWorkerDock> RemoteWorkerDocks = new Components.CmpsByWorld<RemoteWorkerDock>();

	// Token: 0x04002349 RID: 9033
	public static Components.CmpsByWorld<IRemoteDockWorkTarget> RemoteDockWorkTargets = new Components.CmpsByWorld<IRemoteDockWorkTarget>();

	// Token: 0x0400234A RID: 9034
	public static Components.Cmps<Assignable> AssignableItems = new Components.Cmps<Assignable>();

	// Token: 0x0400234B RID: 9035
	public static Components.CmpsByWorld<Comet> Meteors = new Components.CmpsByWorld<Comet>();

	// Token: 0x0400234C RID: 9036
	public static Components.CmpsByWorld<DetectorNetwork.Instance> DetectorNetworks = new Components.CmpsByWorld<DetectorNetwork.Instance>();

	// Token: 0x0400234D RID: 9037
	public static Components.CmpsByWorld<ScannerNetworkVisualizer> ScannerVisualizers = new Components.CmpsByWorld<ScannerNetworkVisualizer>();

	// Token: 0x0400234E RID: 9038
	public static Components.CmpsByWorld<Electrobank> Electrobanks = new Components.CmpsByWorld<Electrobank>();

	// Token: 0x0400234F RID: 9039
	public static Components.CmpsByWorld<SelfChargingElectrobank> SelfChargingElectrobanks = new Components.CmpsByWorld<SelfChargingElectrobank>();

	// Token: 0x04002350 RID: 9040
	public static Components.Cmps<ClusterGridEntity> LongRangeMissileTargetables = new Components.Cmps<ClusterGridEntity>();

	// Token: 0x04002351 RID: 9041
	public static Components.Cmps<IncubationMonitor.Instance> IncubationMonitors = new Components.Cmps<IncubationMonitor.Instance>();

	// Token: 0x04002352 RID: 9042
	public static Components.Cmps<FixedCapturableMonitor.Instance> FixedCapturableMonitors = new Components.Cmps<FixedCapturableMonitor.Instance>();

	// Token: 0x04002353 RID: 9043
	public static Components.Cmps<BeeHive.StatesInstance> BeeHives = new Components.Cmps<BeeHive.StatesInstance>();

	// Token: 0x04002354 RID: 9044
	public static Components.Cmps<StateMachine.Instance> EffectImmunityProviderStations = new Components.Cmps<StateMachine.Instance>();

	// Token: 0x04002355 RID: 9045
	public static Components.Cmps<PeeChoreMonitor.Instance> CriticalBladders = new Components.Cmps<PeeChoreMonitor.Instance>();

	// Token: 0x04002356 RID: 9046
	public static Components.Cmps<MissileLauncher.Instance> MissileLaunchers = new Components.Cmps<MissileLauncher.Instance>();

	// Token: 0x020017A7 RID: 6055
	public class Cmps<T> : ICollection, IEnumerable, IEnumerable<T>
	{
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060099E1 RID: 39393 RVA: 0x0038863F File Offset: 0x0038683F
		public List<T> Items
		{
			get
			{
				return this.items.GetDataList();
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060099E2 RID: 39394 RVA: 0x0038864C File Offset: 0x0038684C
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x060099E3 RID: 39395 RVA: 0x00388659 File Offset: 0x00386859
		public Cmps()
		{
			App.OnPreLoadScene = (global::System.Action)Delegate.Combine(App.OnPreLoadScene, new global::System.Action(this.Clear));
			this.items = new KCompactedVector<T>(0);
			this.table = new Dictionary<T, HandleVector<int>.Handle>();
		}

		// Token: 0x17000A68 RID: 2664
		public T this[int idx]
		{
			get
			{
				return this.Items[idx];
			}
		}

		// Token: 0x060099E5 RID: 39397 RVA: 0x003886A6 File Offset: 0x003868A6
		private void Clear()
		{
			this.items.Clear();
			this.table.Clear();
			this.OnAdd = null;
			this.OnRemove = null;
		}

		// Token: 0x060099E6 RID: 39398 RVA: 0x003886CC File Offset: 0x003868CC
		public void Add(T cmp)
		{
			HandleVector<int>.Handle handle = this.items.Allocate(cmp);
			this.table[cmp] = handle;
			if (this.OnAdd != null)
			{
				this.OnAdd(cmp);
			}
		}

		// Token: 0x060099E7 RID: 39399 RVA: 0x00388708 File Offset: 0x00386908
		public void Remove(T cmp)
		{
			HandleVector<int>.Handle invalidHandle = HandleVector<int>.InvalidHandle;
			if (this.table.TryGetValue(cmp, out invalidHandle))
			{
				this.table.Remove(cmp);
				this.items.Free(invalidHandle);
				if (this.OnRemove != null)
				{
					this.OnRemove(cmp);
				}
			}
		}

		// Token: 0x060099E8 RID: 39400 RVA: 0x0038875C File Offset: 0x0038695C
		public void Register(Action<T> on_add, Action<T> on_remove)
		{
			this.OnAdd += on_add;
			this.OnRemove += on_remove;
			foreach (T t in this.Items)
			{
				this.OnAdd(t);
			}
		}

		// Token: 0x060099E9 RID: 39401 RVA: 0x003887C4 File Offset: 0x003869C4
		public void Unregister(Action<T> on_add, Action<T> on_remove)
		{
			this.OnAdd -= on_add;
			this.OnRemove -= on_remove;
		}

		// Token: 0x060099EA RID: 39402 RVA: 0x003887D4 File Offset: 0x003869D4
		public List<T> GetWorldItems(int worldId, bool checkChildWorlds = false)
		{
			if (ClusterManager.Instance.worldCount == 1)
			{
				return this.Items;
			}
			ICollection<int> collection = null;
			if (checkChildWorlds)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(worldId);
				if (world != null)
				{
					collection = world.GetChildWorldIds();
				}
			}
			return this.GetWorldItems(worldId, collection, null);
		}

		// Token: 0x060099EB RID: 39403 RVA: 0x00388820 File Offset: 0x00386A20
		public List<T> GetWorldItems(int worldId, bool checkChildWorlds, Func<T, bool> filter)
		{
			ICollection<int> collection = null;
			if (checkChildWorlds)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(worldId);
				if (world != null)
				{
					collection = world.GetChildWorldIds();
				}
			}
			return this.GetWorldItems(worldId, collection, filter);
		}

		// Token: 0x060099EC RID: 39404 RVA: 0x00388858 File Offset: 0x00386A58
		public List<T> GetWorldItems(int worldId, ICollection<int> otherWorldIds, Func<T, bool> filter)
		{
			List<T> list = new List<T>();
			for (int i = 0; i < this.Items.Count; i++)
			{
				T t = this.Items[i];
				int myWorldId = (t as KMonoBehaviour).GetMyWorldId();
				bool flag = worldId == myWorldId;
				if (!flag && otherWorldIds != null && otherWorldIds.Contains(myWorldId))
				{
					flag = true;
				}
				if (flag && filter != null)
				{
					flag = filter(t);
				}
				if (flag)
				{
					list.Add(t);
				}
			}
			return list;
		}

		// Token: 0x060099ED RID: 39405 RVA: 0x003888D4 File Offset: 0x00386AD4
		public IEnumerable<T> WorldItemsEnumerate(int worldId, bool checkChildWorlds = false)
		{
			ICollection<int> collection = null;
			if (checkChildWorlds)
			{
				collection = ClusterManager.Instance.GetWorld(worldId).GetChildWorldIds();
			}
			return this.WorldItemsEnumerate(worldId, collection);
		}

		// Token: 0x060099EE RID: 39406 RVA: 0x003888FF File Offset: 0x00386AFF
		public IEnumerable<T> WorldItemsEnumerate(int worldId, ICollection<int> otherWorldIds = null)
		{
			int num;
			for (int index = 0; index < this.Items.Count; index = num + 1)
			{
				T t = this.Items[index];
				int myWorldId = (t as KMonoBehaviour).GetMyWorldId();
				if (myWorldId == worldId || (otherWorldIds != null && otherWorldIds.Contains(myWorldId)))
				{
					yield return t;
				}
				num = index;
			}
			yield break;
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060099EF RID: 39407 RVA: 0x00388920 File Offset: 0x00386B20
		// (remove) Token: 0x060099F0 RID: 39408 RVA: 0x00388958 File Offset: 0x00386B58
		public event Action<T> OnAdd;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060099F1 RID: 39409 RVA: 0x00388990 File Offset: 0x00386B90
		// (remove) Token: 0x060099F2 RID: 39410 RVA: 0x003889C8 File Offset: 0x00386BC8
		public event Action<T> OnRemove;

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060099F3 RID: 39411 RVA: 0x003889FD File Offset: 0x00386BFD
		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060099F4 RID: 39412 RVA: 0x00388A04 File Offset: 0x00386C04
		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060099F5 RID: 39413 RVA: 0x00388A0B File Offset: 0x00386C0B
		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060099F6 RID: 39414 RVA: 0x00388A12 File Offset: 0x00386C12
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060099F7 RID: 39415 RVA: 0x00388A24 File Offset: 0x00386C24
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060099F8 RID: 39416 RVA: 0x00388A36 File Offset: 0x00386C36
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0400766E RID: 30318
		private Dictionary<T, HandleVector<int>.Handle> table;

		// Token: 0x0400766F RID: 30319
		private KCompactedVector<T> items;
	}

	// Token: 0x020017A8 RID: 6056
	public class CmpsByWorld<T>
	{
		// Token: 0x060099F9 RID: 39417 RVA: 0x00388A48 File Offset: 0x00386C48
		public CmpsByWorld()
		{
			App.OnPreLoadScene = (global::System.Action)Delegate.Combine(App.OnPreLoadScene, new global::System.Action(this.Clear));
			this.m_CmpsByWorld = new Dictionary<int, Components.Cmps<T>>();
		}

		// Token: 0x060099FA RID: 39418 RVA: 0x00388A7B File Offset: 0x00386C7B
		public void Clear()
		{
			this.m_CmpsByWorld.Clear();
		}

		// Token: 0x060099FB RID: 39419 RVA: 0x00388A88 File Offset: 0x00386C88
		public Components.Cmps<T> CreateOrGetCmps(int worldId)
		{
			Components.Cmps<T> cmps;
			if (!this.m_CmpsByWorld.TryGetValue(worldId, out cmps))
			{
				cmps = new Components.Cmps<T>();
				this.m_CmpsByWorld[worldId] = cmps;
			}
			return cmps;
		}

		// Token: 0x060099FC RID: 39420 RVA: 0x00388AB9 File Offset: 0x00386CB9
		public void Add(int worldId, T cmp)
		{
			DebugUtil.DevAssertArgs(worldId != -1, new object[] { "CmpsByWorld tried to add a component to an invalid world. Did you call this during a state machine's constructor instead of StartSM? ", cmp });
			this.CreateOrGetCmps(worldId).Add(cmp);
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x00388AEB File Offset: 0x00386CEB
		public void Remove(int worldId, T cmp)
		{
			this.CreateOrGetCmps(worldId).Remove(cmp);
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x00388AFA File Offset: 0x00386CFA
		public void Register(int worldId, Action<T> on_add, Action<T> on_remove)
		{
			this.CreateOrGetCmps(worldId).Register(on_add, on_remove);
		}

		// Token: 0x060099FF RID: 39423 RVA: 0x00388B0A File Offset: 0x00386D0A
		public void Unregister(int worldId, Action<T> on_add, Action<T> on_remove)
		{
			this.CreateOrGetCmps(worldId).Unregister(on_add, on_remove);
		}

		// Token: 0x06009A00 RID: 39424 RVA: 0x00388B1A File Offset: 0x00386D1A
		public List<T> GetItems(int worldId)
		{
			return this.CreateOrGetCmps(worldId).Items;
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x00388B28 File Offset: 0x00386D28
		public Dictionary<int, Components.Cmps<T>>.KeyCollection GetWorldsIds()
		{
			return this.m_CmpsByWorld.Keys;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06009A02 RID: 39426 RVA: 0x00388B38 File Offset: 0x00386D38
		public int GlobalCount
		{
			get
			{
				int num = 0;
				foreach (KeyValuePair<int, Components.Cmps<T>> keyValuePair in this.m_CmpsByWorld)
				{
					num += keyValuePair.Value.Count;
				}
				return num;
			}
		}

		// Token: 0x06009A03 RID: 39427 RVA: 0x00388B98 File Offset: 0x00386D98
		public int CountWorldItems(int worldId, bool includeChildren = false)
		{
			int num = this.GetItems(worldId).Count;
			if (includeChildren)
			{
				foreach (int num2 in ClusterManager.Instance.GetWorld(worldId).GetChildWorldIds())
				{
					num += this.GetItems(num2).Count;
				}
			}
			return num;
		}

		// Token: 0x06009A04 RID: 39428 RVA: 0x00388C08 File Offset: 0x00386E08
		public IEnumerable<T> WorldItemsEnumerate(int worldId, bool checkChildWorlds = false)
		{
			ICollection<int> collection = null;
			if (checkChildWorlds)
			{
				collection = ClusterManager.Instance.GetWorld(worldId).GetChildWorldIds();
			}
			return this.WorldItemsEnumerate(worldId, collection);
		}

		// Token: 0x06009A05 RID: 39429 RVA: 0x00388C33 File Offset: 0x00386E33
		public IEnumerable<T> WorldItemsEnumerate(int worldId, ICollection<int> otherWorldIds = null)
		{
			List<T> items = this.GetItems(worldId);
			int num;
			for (int index = 0; index < items.Count; index = num + 1)
			{
				yield return items[index];
				num = index;
			}
			if (otherWorldIds != null)
			{
				foreach (int num2 in otherWorldIds)
				{
					items = this.GetItems(num2);
					for (int index = 0; index < items.Count; index = num + 1)
					{
						yield return items[index];
						num = index;
					}
				}
				IEnumerator<int> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x04007672 RID: 30322
		private Dictionary<int, Components.Cmps<T>> m_CmpsByWorld;
	}
}
