using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000B37 RID: 2871
public class Clustercraft : ClusterGridEntity, IClusterRange, ISim4000ms, ISim1000ms
{
	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x060054D9 RID: 21721 RVA: 0x001ED199 File Offset: 0x001EB399
	public override string Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x060054DA RID: 21722 RVA: 0x001ED1A1 File Offset: 0x001EB3A1
	// (set) Token: 0x060054DB RID: 21723 RVA: 0x001ED1A9 File Offset: 0x001EB3A9
	public bool Exploding { get; protected set; }

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x060054DC RID: 21724 RVA: 0x001ED1B2 File Offset: 0x001EB3B2
	public override EntityLayer Layer
	{
		get
		{
			return EntityLayer.Craft;
		}
	}

	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x060054DD RID: 21725 RVA: 0x001ED1B8 File Offset: 0x001EB3B8
	public override List<ClusterGridEntity.AnimConfig> AnimConfigs
	{
		get
		{
			return new List<ClusterGridEntity.AnimConfig>
			{
				new ClusterGridEntity.AnimConfig
				{
					animFile = Assets.GetAnim("rocket01_kanim"),
					initialAnim = "idle_loop"
				}
			};
		}
	}

	// Token: 0x060054DE RID: 21726 RVA: 0x001ED1FC File Offset: 0x001EB3FC
	public override Sprite GetUISprite()
	{
		PassengerRocketModule passengerModule = this.m_moduleInterface.GetPassengerModule();
		if (passengerModule != null)
		{
			return Def.GetUISprite(passengerModule.gameObject, "ui", false).first;
		}
		return Assets.GetSprite("ic_rocket");
	}

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x060054DF RID: 21727 RVA: 0x001ED244 File Offset: 0x001EB444
	public override bool IsVisible
	{
		get
		{
			return !this.Exploding;
		}
	}

	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x060054E0 RID: 21728 RVA: 0x001ED24F File Offset: 0x001EB44F
	public override ClusterRevealLevel IsVisibleInFOW
	{
		get
		{
			return ClusterRevealLevel.Visible;
		}
	}

	// Token: 0x060054E1 RID: 21729 RVA: 0x001ED252 File Offset: 0x001EB452
	public override bool SpaceOutInSameHex()
	{
		return true;
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x060054E2 RID: 21730 RVA: 0x001ED255 File Offset: 0x001EB455
	public CraftModuleInterface ModuleInterface
	{
		get
		{
			return this.m_moduleInterface;
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x060054E3 RID: 21731 RVA: 0x001ED25D File Offset: 0x001EB45D
	public AxialI Destination
	{
		get
		{
			return this.m_moduleInterface.GetClusterDestinationSelector().GetDestination();
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x060054E4 RID: 21732 RVA: 0x001ED270 File Offset: 0x001EB470
	public float Speed
	{
		get
		{
			float num = this.EnginePower / this.TotalBurden;
			float num2 = num * this.PilotSkillMultiplier;
			bool flag = this.AutoPilotMultiplier > 0.5f;
			bool flag2 = this.ModuleInterface.GetPassengerModule() != null;
			RoboPilotModule robotPilotModule = this.ModuleInterface.GetRobotPilotModule();
			bool flag3 = robotPilotModule != null && robotPilotModule.GetDataBanksStored() > 1f;
			if (flag3 && flag)
			{
				num2 *= 1.5f;
			}
			else if (!flag && flag2)
			{
				num2 *= 0.5f;
			}
			else if (!flag3 && !flag2)
			{
				num2 = 0f;
			}
			if (this.controlStationBuffTimeRemaining > 0f)
			{
				num2 += num * 0.20000005f;
			}
			return num2;
		}
	}

	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x060054E5 RID: 21733 RVA: 0x001ED328 File Offset: 0x001EB528
	public float EnginePower
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.EnginePower;
			}
			return num;
		}
	}

	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x060054E6 RID: 21734 RVA: 0x001ED390 File Offset: 0x001EB590
	public float FuelPerDistance
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.FuelKilogramPerDistance;
			}
			return num;
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x060054E7 RID: 21735 RVA: 0x001ED3F8 File Offset: 0x001EB5F8
	public float TotalBurden
	{
		get
		{
			float num = 0f;
			foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
			{
				num += @ref.Get().performanceStats.Burden;
			}
			global::Debug.Assert(num > 0f);
			return num;
		}
	}

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x060054E8 RID: 21736 RVA: 0x001ED46C File Offset: 0x001EB66C
	// (set) Token: 0x060054E9 RID: 21737 RVA: 0x001ED474 File Offset: 0x001EB674
	public bool LaunchRequested
	{
		get
		{
			return this.m_launchRequested;
		}
		private set
		{
			this.m_launchRequested = value;
			this.m_moduleInterface.TriggerEventOnCraftAndRocket(GameHashes.RocketRequestLaunch, this);
		}
	}

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x060054EA RID: 21738 RVA: 0x001ED48E File Offset: 0x001EB68E
	public Clustercraft.CraftStatus Status
	{
		get
		{
			return this.status;
		}
	}

	// Token: 0x060054EB RID: 21739 RVA: 0x001ED496 File Offset: 0x001EB696
	public void SetCraftStatus(Clustercraft.CraftStatus craft_status)
	{
		this.status = craft_status;
		this.UpdateGroundTags();
		this.m_moduleInterface.TriggerEventOnCraftAndRocket(GameHashes.ClustercraftStateChanged, craft_status);
	}

	// Token: 0x060054EC RID: 21740 RVA: 0x001ED4BB File Offset: 0x001EB6BB
	public void SetExploding()
	{
		this.Exploding = true;
	}

	// Token: 0x060054ED RID: 21741 RVA: 0x001ED4C4 File Offset: 0x001EB6C4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Components.Clustercrafts.Add(this);
	}

	// Token: 0x060054EE RID: 21742 RVA: 0x001ED4D8 File Offset: 0x001EB6D8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.m_clusterTraveler.getSpeedCB = new Func<float>(this.GetSpeed);
		this.m_clusterTraveler.getCanTravelCB = new Func<bool, bool>(this.CanTravel);
		this.m_clusterTraveler.onTravelCB = new global::System.Action(this.BurnFuelForTravel);
		this.m_clusterTraveler.validateTravelCB = new Func<AxialI, bool>(this.CanTravelToCell);
		this.UpdateGroundTags();
		base.Subscribe<Clustercraft>(1512695988, Clustercraft.RocketModuleChangedHandler);
		base.Subscribe<Clustercraft>(543433792, Clustercraft.ClusterDestinationChangedHandler);
		base.Subscribe<Clustercraft>(1796608350, Clustercraft.ClusterDestinationReachedHandler);
		base.Subscribe(-688990705, delegate(object o)
		{
			this.UpdateStatusItem();
		});
		base.Subscribe<Clustercraft>(1102426921, Clustercraft.NameChangedHandler);
		this.SetRocketName(this.m_name);
		this.UpdateStatusItem();
	}

	// Token: 0x060054EF RID: 21743 RVA: 0x001ED5BC File Offset: 0x001EB7BC
	public void Sim1000ms(float dt)
	{
		this.controlStationBuffTimeRemaining = Mathf.Max(this.controlStationBuffTimeRemaining - dt, 0f);
		if (this.controlStationBuffTimeRemaining > 0f)
		{
			this.missionControlStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.MissionControlBoosted, this);
			return;
		}
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.MissionControlBoosted, false);
		this.missionControlStatusHandle = Guid.Empty;
	}

	// Token: 0x060054F0 RID: 21744 RVA: 0x001ED638 File Offset: 0x001EB838
	public void Sim4000ms(float dt)
	{
		RocketClusterDestinationSelector clusterDestinationSelector = this.m_moduleInterface.GetClusterDestinationSelector();
		if (this.Status == Clustercraft.CraftStatus.InFlight && this.m_location == clusterDestinationSelector.GetDestination())
		{
			this.OnClusterDestinationReached(null);
		}
	}

	// Token: 0x060054F1 RID: 21745 RVA: 0x001ED674 File Offset: 0x001EB874
	public void Init(AxialI location, LaunchPad pad)
	{
		this.m_location = location;
		base.GetComponent<RocketClusterDestinationSelector>().SetDestination(this.m_location);
		this.SetRocketName(GameUtil.GenerateRandomRocketName());
		if (pad != null)
		{
			this.Land(pad, true);
		}
		this.UpdateStatusItem();
	}

	// Token: 0x060054F2 RID: 21746 RVA: 0x001ED6B0 File Offset: 0x001EB8B0
	protected override void OnCleanUp()
	{
		Components.Clustercrafts.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x060054F3 RID: 21747 RVA: 0x001ED6C3 File Offset: 0x001EB8C3
	private bool CanTravel(bool tryingToLand)
	{
		return this.HasTag(GameTags.RocketInSpace) && (tryingToLand || this.HasResourcesToMove(1, Clustercraft.CombustionResource.All));
	}

	// Token: 0x060054F4 RID: 21748 RVA: 0x001ED6E1 File Offset: 0x001EB8E1
	private bool CanTravelToCell(AxialI location)
	{
		return !(ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(location, EntityLayer.Asteroid) != null) || this.CanLandAtAsteroid(location, true);
	}

	// Token: 0x060054F5 RID: 21749 RVA: 0x001ED701 File Offset: 0x001EB901
	private float GetSpeed()
	{
		return this.Speed;
	}

	// Token: 0x060054F6 RID: 21750 RVA: 0x001ED70C File Offset: 0x001EB90C
	private void RocketModuleChanged(object data)
	{
		RocketModuleCluster rocketModuleCluster = (RocketModuleCluster)data;
		if (rocketModuleCluster != null)
		{
			this.UpdateGroundTags(rocketModuleCluster.gameObject);
		}
	}

	// Token: 0x060054F7 RID: 21751 RVA: 0x001ED735 File Offset: 0x001EB935
	private void OnClusterDestinationChanged(object data)
	{
		this.UpdateStatusItem();
	}

	// Token: 0x060054F8 RID: 21752 RVA: 0x001ED740 File Offset: 0x001EB940
	private void OnClusterDestinationReached(object data)
	{
		RocketClusterDestinationSelector clusterDestinationSelector = this.m_moduleInterface.GetClusterDestinationSelector();
		global::Debug.Assert(base.Location == clusterDestinationSelector.GetDestination());
		if (clusterDestinationSelector.HasAsteroidDestination())
		{
			LaunchPad destinationPad = clusterDestinationSelector.GetDestinationPad();
			this.Land(base.Location, destinationPad);
		}
		this.UpdateStatusItem();
	}

	// Token: 0x060054F9 RID: 21753 RVA: 0x001ED791 File Offset: 0x001EB991
	public void SetRocketName(object newName)
	{
		this.SetRocketName((string)newName);
	}

	// Token: 0x060054FA RID: 21754 RVA: 0x001ED7A0 File Offset: 0x001EB9A0
	public void SetRocketName(string newName)
	{
		this.m_name = newName;
		base.name = "Clustercraft: " + newName;
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CharacterOverlay component = @ref.Get().GetComponent<CharacterOverlay>();
			if (component != null)
			{
				NameDisplayScreen.Instance.UpdateName(component.gameObject);
				break;
			}
		}
		ClusterManager.Instance.Trigger(1943181844, newName);
	}

	// Token: 0x060054FB RID: 21755 RVA: 0x001ED838 File Offset: 0x001EBA38
	public bool CheckPreppedForLaunch()
	{
		return this.m_moduleInterface.CheckPreppedForLaunch();
	}

	// Token: 0x060054FC RID: 21756 RVA: 0x001ED845 File Offset: 0x001EBA45
	public bool CheckReadyToLaunch()
	{
		return this.m_moduleInterface.CheckReadyToLaunch();
	}

	// Token: 0x060054FD RID: 21757 RVA: 0x001ED852 File Offset: 0x001EBA52
	public bool IsFlightInProgress()
	{
		return this.Status == Clustercraft.CraftStatus.InFlight && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x060054FE RID: 21758 RVA: 0x001ED86C File Offset: 0x001EBA6C
	public ClusterGridEntity GetPOIAtCurrentLocation()
	{
		if ((this.status != Clustercraft.CraftStatus.InFlight || this.IsFlightInProgress()) && (this.status != Clustercraft.CraftStatus.Launching || !(this.m_location == this.Destination)))
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_location, EntityLayer.POI);
	}

	// Token: 0x060054FF RID: 21759 RVA: 0x001ED8B9 File Offset: 0x001EBAB9
	public ClusterGridEntity GetStableOrbitAsteroid()
	{
		if (this.status != Clustercraft.CraftStatus.InFlight || this.IsFlightInProgress())
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x06005500 RID: 21760 RVA: 0x001ED8DF File Offset: 0x001EBADF
	public ClusterGridEntity GetOrbitAsteroid()
	{
		if (this.status != Clustercraft.CraftStatus.InFlight)
		{
			return null;
		}
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x06005501 RID: 21761 RVA: 0x001ED8FD File Offset: 0x001EBAFD
	public ClusterGridEntity GetAdjacentAsteroid()
	{
		return ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(this.m_location, EntityLayer.Asteroid);
	}

	// Token: 0x06005502 RID: 21762 RVA: 0x001ED910 File Offset: 0x001EBB10
	private bool CheckDesinationInRange()
	{
		return this.m_clusterTraveler.CurrentPath != null && this.Speed * this.m_clusterTraveler.TravelETA() <= this.ModuleInterface.Range;
	}

	// Token: 0x06005503 RID: 21763 RVA: 0x001ED944 File Offset: 0x001EBB44
	public bool HasResourcesToMove(int hexes = 1, Clustercraft.CombustionResource combustionResource = Clustercraft.CombustionResource.All)
	{
		switch (combustionResource)
		{
		case Clustercraft.CombustionResource.Fuel:
			return this.m_moduleInterface.FuelRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		case Clustercraft.CombustionResource.Oxidizer:
			return this.m_moduleInterface.OxidizerPowerRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		case Clustercraft.CombustionResource.All:
			return this.m_moduleInterface.BurnableMassRemaining / this.FuelPerDistance >= 600f * (float)hexes - 0.001f;
		default:
		{
			bool flag;
			RocketModuleCluster primaryPilotModule = this.m_moduleInterface.GetPrimaryPilotModule(out flag);
			return flag && primaryPilotModule.GetComponent<RoboPilotModule>().HasResourcesToMove(hexes);
		}
		}
	}

	// Token: 0x06005504 RID: 21764 RVA: 0x001ED9F8 File Offset: 0x001EBBF8
	private void BurnFuelForTravel()
	{
		float num = 600f;
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			RocketModuleCluster rocketModuleCluster = @ref.Get();
			RocketEngineCluster component = rocketModuleCluster.GetComponent<RocketEngineCluster>();
			if (component != null)
			{
				Tag fuelTag = component.fuelTag;
				float num2 = 0f;
				if (component.requireOxidizer)
				{
					num2 = this.ModuleInterface.OxidizerPowerRemaining;
				}
				if (num > 0f)
				{
					foreach (Ref<RocketModuleCluster> ref2 in this.m_moduleInterface.ClusterModules)
					{
						IFuelTank component2 = ref2.Get().GetComponent<IFuelTank>();
						if (!component2.IsNullOrDestroyed())
						{
							num -= this.BurnFromTank(num, component, fuelTag, component2.Storage, ref num2);
						}
						if (num <= 0f)
						{
							break;
						}
					}
				}
			}
			RoboPilotModule component3 = rocketModuleCluster.GetComponent<RoboPilotModule>();
			if (component3 != null)
			{
				component3.ConsumeDataBanksInFlight();
			}
		}
		this.UpdateStatusItem();
	}

	// Token: 0x06005505 RID: 21765 RVA: 0x001EDB28 File Offset: 0x001EBD28
	private float BurnFromTank(float attemptTravelAmount, RocketEngineCluster engine, Tag fuelTag, IStorage storage, ref float totalOxidizerRemaining)
	{
		float num = attemptTravelAmount * engine.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance;
		num = Mathf.Min(storage.GetAmountAvailable(fuelTag), num);
		if (engine.requireOxidizer)
		{
			num = Mathf.Min(num, totalOxidizerRemaining);
		}
		storage.ConsumeIgnoringDisease(fuelTag, num);
		if (engine.requireOxidizer)
		{
			this.BurnOxidizer(num);
			totalOxidizerRemaining -= num;
		}
		return num / engine.GetComponent<RocketModuleCluster>().performanceStats.FuelKilogramPerDistance;
	}

	// Token: 0x06005506 RID: 21766 RVA: 0x001EDB9C File Offset: 0x001EBD9C
	private void BurnOxidizer(float fuelEquivalentKGs)
	{
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			OxidizerTank component = @ref.Get().GetComponent<OxidizerTank>();
			if (component != null)
			{
				foreach (KeyValuePair<Tag, float> keyValuePair in component.GetOxidizersAvailable())
				{
					float num = Clustercraft.dlc1OxidizerEfficiencies[keyValuePair.Key];
					float num2 = Mathf.Min(fuelEquivalentKGs / num, keyValuePair.Value);
					if (num2 > 0f)
					{
						component.storage.ConsumeIgnoringDisease(keyValuePair.Key, num2);
						fuelEquivalentKGs -= num2 * num;
					}
				}
			}
			if (fuelEquivalentKGs <= 0f)
			{
				break;
			}
		}
	}

	// Token: 0x06005507 RID: 21767 RVA: 0x001EDC90 File Offset: 0x001EBE90
	public List<ResourceHarvestModule.StatesInstance> GetAllResourceHarvestModules()
	{
		List<ResourceHarvestModule.StatesInstance> list = new List<ResourceHarvestModule.StatesInstance>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			ResourceHarvestModule.StatesInstance smi = @ref.Get().GetSMI<ResourceHarvestModule.StatesInstance>();
			if (smi != null)
			{
				list.Add(smi);
			}
		}
		return list;
	}

	// Token: 0x06005508 RID: 21768 RVA: 0x001EDCF8 File Offset: 0x001EBEF8
	public List<ArtifactHarvestModule.StatesInstance> GetAllArtifactHarvestModules()
	{
		List<ArtifactHarvestModule.StatesInstance> list = new List<ArtifactHarvestModule.StatesInstance>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			ArtifactHarvestModule.StatesInstance smi = @ref.Get().GetSMI<ArtifactHarvestModule.StatesInstance>();
			if (smi != null)
			{
				list.Add(smi);
			}
		}
		return list;
	}

	// Token: 0x06005509 RID: 21769 RVA: 0x001EDD60 File Offset: 0x001EBF60
	public List<CargoBayCluster> GetAllCargoBays()
	{
		List<CargoBayCluster> list = new List<CargoBayCluster>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
			if (component != null)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x0600550A RID: 21770 RVA: 0x001EDDCC File Offset: 0x001EBFCC
	public List<CargoBayCluster> GetCargoBaysOfType(CargoBay.CargoType cargoType)
	{
		List<CargoBayCluster> list = new List<CargoBayCluster>();
		foreach (Ref<RocketModuleCluster> @ref in this.m_moduleInterface.ClusterModules)
		{
			CargoBayCluster component = @ref.Get().GetComponent<CargoBayCluster>();
			if (component != null && component.storageType == cargoType)
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x0600550B RID: 21771 RVA: 0x001EDE44 File Offset: 0x001EC044
	public void DestroyCraftAndModules()
	{
		WorldContainer interiorWorld = this.m_moduleInterface.GetInteriorWorld();
		if (interiorWorld != null)
		{
			NameDisplayScreen.Instance.RemoveWorldEntries(interiorWorld.id);
		}
		List<RocketModuleCluster> list = this.m_moduleInterface.ClusterModules.Select((Ref<RocketModuleCluster> x) => x.Get()).ToList<RocketModuleCluster>();
		for (int i = list.Count - 1; i >= 0; i--)
		{
			RocketModuleCluster rocketModuleCluster = list[i];
			Storage component = rocketModuleCluster.GetComponent<Storage>();
			if (component != null)
			{
				component.ConsumeAllIgnoringDisease();
			}
			MinionStorage component2 = rocketModuleCluster.GetComponent<MinionStorage>();
			if (component2 != null)
			{
				List<MinionStorage.Info> storedMinionInfo = component2.GetStoredMinionInfo();
				for (int j = storedMinionInfo.Count - 1; j >= 0; j--)
				{
					component2.DeleteStoredMinion(storedMinionInfo[j].id);
				}
			}
			Util.KDestroyGameObject(rocketModuleCluster.gameObject);
		}
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x0600550C RID: 21772 RVA: 0x001EDF3D File Offset: 0x001EC13D
	public void CancelLaunch()
	{
		if (this.LaunchRequested)
		{
			global::Debug.Log("Cancelling launch!");
			this.LaunchRequested = false;
		}
	}

	// Token: 0x0600550D RID: 21773 RVA: 0x001EDF58 File Offset: 0x001EC158
	public void RequestLaunch(bool automated = false)
	{
		if (this.HasTag(GameTags.RocketNotOnGround) || this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination())
		{
			return;
		}
		if (DebugHandler.InstantBuildMode && !automated)
		{
			this.Launch(false);
		}
		if (this.LaunchRequested)
		{
			return;
		}
		if (!this.CheckPreppedForLaunch())
		{
			return;
		}
		global::Debug.Log("Triggering launch!");
		if (this.m_moduleInterface.GetRobotPilotModule() != null)
		{
			this.Launch(automated);
		}
		this.LaunchRequested = true;
	}

	// Token: 0x0600550E RID: 21774 RVA: 0x001EDFD4 File Offset: 0x001EC1D4
	public void Launch(bool automated = false)
	{
		if (this.HasTag(GameTags.RocketNotOnGround) || this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination())
		{
			this.LaunchRequested = false;
			return;
		}
		if ((!DebugHandler.InstantBuildMode || automated) && !this.CheckReadyToLaunch())
		{
			return;
		}
		if (automated && !this.m_moduleInterface.CheckReadyForAutomatedLaunchCommand())
		{
			this.LaunchRequested = false;
			return;
		}
		this.LaunchRequested = false;
		this.SetCraftStatus(Clustercraft.CraftStatus.Launching);
		this.m_moduleInterface.DoLaunch();
		this.BurnFuelForTravel();
		this.m_clusterTraveler.AdvancePathOneStep();
		this.UpdateStatusItem();
	}

	// Token: 0x0600550F RID: 21775 RVA: 0x001EE064 File Offset: 0x001EC264
	public void LandAtPad(LaunchPad pad)
	{
		this.m_moduleInterface.GetClusterDestinationSelector().SetDestinationPad(pad);
	}

	// Token: 0x06005510 RID: 21776 RVA: 0x001EE078 File Offset: 0x001EC278
	public Clustercraft.PadLandingStatus CanLandAtPad(LaunchPad pad, out string failReason)
	{
		if (pad == null)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.NONEAVAILABLE;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		if (pad.HasRocket() && pad.LandedRocket.CraftInterface != this.m_moduleInterface)
		{
			failReason = "<TEMP>The pad already has a rocket on it!<TEMP>";
			return Clustercraft.PadLandingStatus.CanLandEventually;
		}
		if (ConditionFlightPathIsClear.PadTopEdgeDistanceToCeilingEdge(pad.gameObject) < this.ModuleInterface.RocketHeight)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_TOO_SHORT;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		int num = -1;
		if (!ConditionFlightPathIsClear.CheckFlightPathClear(this.ModuleInterface, pad.gameObject, out num))
		{
			failReason = string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PATH_OBSTRUCTED, pad.GetProperName());
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		if (!pad.GetComponent<Operational>().IsOperational)
		{
			failReason = UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_PAD_DISABLED;
			return Clustercraft.PadLandingStatus.CanNeverLand;
		}
		int rocketBottomPosition = pad.RocketBottomPosition;
		foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
		{
			GameObject gameObject = @ref.Get().gameObject;
			int moduleRelativeVerticalPosition = this.ModuleInterface.GetModuleRelativeVerticalPosition(gameObject);
			Building component = gameObject.GetComponent<Building>();
			BuildingUnderConstruction component2 = gameObject.GetComponent<BuildingUnderConstruction>();
			BuildingDef buildingDef = ((component != null) ? component.Def : component2.Def);
			for (int i = 0; i < buildingDef.WidthInCells; i++)
			{
				for (int j = 0; j < buildingDef.HeightInCells; j++)
				{
					int num2 = Grid.OffsetCell(rocketBottomPosition, 0, moduleRelativeVerticalPosition);
					num2 = Grid.OffsetCell(num2, -(buildingDef.WidthInCells / 2) + i, j);
					if (Grid.Solid[num2])
					{
						num = num2;
						failReason = string.Format(UI.UISIDESCREENS.CLUSTERDESTINATIONSIDESCREEN.DROPDOWN_TOOLTIP_SITE_OBSTRUCTED, pad.GetProperName());
						return Clustercraft.PadLandingStatus.CanNeverLand;
					}
				}
			}
		}
		failReason = null;
		return Clustercraft.PadLandingStatus.CanLandImmediately;
	}

	// Token: 0x06005511 RID: 21777 RVA: 0x001EE248 File Offset: 0x001EC448
	private LaunchPad FindValidLandingPad(AxialI location, bool mustLandImmediately)
	{
		LaunchPad launchPad = null;
		int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(location);
		LaunchPad preferredLaunchPadForWorld = this.m_moduleInterface.GetPreferredLaunchPadForWorld(asteroidWorldIdAtLocation);
		string text;
		if (preferredLaunchPadForWorld != null && this.CanLandAtPad(preferredLaunchPadForWorld, out text) == Clustercraft.PadLandingStatus.CanLandImmediately)
		{
			return preferredLaunchPadForWorld;
		}
		foreach (object obj in Components.LaunchPads)
		{
			LaunchPad launchPad2 = (LaunchPad)obj;
			if (launchPad2.GetMyWorldLocation() == location)
			{
				string text2;
				Clustercraft.PadLandingStatus padLandingStatus = this.CanLandAtPad(launchPad2, out text2);
				if (padLandingStatus == Clustercraft.PadLandingStatus.CanLandImmediately)
				{
					return launchPad2;
				}
				if (!mustLandImmediately && padLandingStatus == Clustercraft.PadLandingStatus.CanLandEventually)
				{
					launchPad = launchPad2;
				}
			}
		}
		return launchPad;
	}

	// Token: 0x06005512 RID: 21778 RVA: 0x001EE304 File Offset: 0x001EC504
	public bool CanLandAtAsteroid(AxialI location, bool mustLandImmediately)
	{
		LaunchPad destinationPad = this.m_moduleInterface.GetClusterDestinationSelector().GetDestinationPad();
		global::Debug.Assert(destinationPad == null || destinationPad.GetMyWorldLocation() == location, "A rocket is trying to travel to an asteroid but has selected a landing pad at a different asteroid!");
		if (destinationPad != null)
		{
			string text;
			Clustercraft.PadLandingStatus padLandingStatus = this.CanLandAtPad(destinationPad, out text);
			return padLandingStatus == Clustercraft.PadLandingStatus.CanLandImmediately || (!mustLandImmediately && padLandingStatus == Clustercraft.PadLandingStatus.CanLandEventually);
		}
		return this.FindValidLandingPad(location, mustLandImmediately) != null;
	}

	// Token: 0x06005513 RID: 21779 RVA: 0x001EE374 File Offset: 0x001EC574
	private void Land(LaunchPad pad, bool forceGrounded)
	{
		string text;
		if (this.CanLandAtPad(pad, out text) != Clustercraft.PadLandingStatus.CanLandImmediately)
		{
			return;
		}
		this.BurnFuelForTravel();
		this.m_location = pad.GetMyWorldLocation();
		this.SetCraftStatus(forceGrounded ? Clustercraft.CraftStatus.Grounded : Clustercraft.CraftStatus.Landing);
		this.m_moduleInterface.DoLand(pad);
		this.UpdateStatusItem();
	}

	// Token: 0x06005514 RID: 21780 RVA: 0x001EE3C0 File Offset: 0x001EC5C0
	private void Land(AxialI destination, LaunchPad chosenPad)
	{
		if (chosenPad == null)
		{
			chosenPad = this.FindValidLandingPad(destination, true);
		}
		global::Debug.Assert(chosenPad == null || chosenPad.GetMyWorldLocation() == this.m_location, "Attempting to land on a pad that isn't at our current position");
		this.Land(chosenPad, false);
	}

	// Token: 0x06005515 RID: 21781 RVA: 0x001EE410 File Offset: 0x001EC610
	public void UpdateStatusItem()
	{
		if (ClusterGrid.Instance == null)
		{
			return;
		}
		if (this.mainStatusHandle != Guid.Empty)
		{
			this.selectable.RemoveStatusItem(this.mainStatusHandle, false);
		}
		ClusterGridEntity visibleEntityOfLayerAtCell = ClusterGrid.Instance.GetVisibleEntityOfLayerAtCell(this.m_location, EntityLayer.Asteroid);
		ClusterGridEntity orbitAsteroid = this.GetOrbitAsteroid();
		bool flag = false;
		if (orbitAsteroid != null)
		{
			using (IEnumerator enumerator = Components.LaunchPads.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((LaunchPad)enumerator.Current).GetMyWorldLocation() == orbitAsteroid.Location)
					{
						flag = true;
						break;
					}
				}
			}
		}
		bool flag2 = false;
		if (visibleEntityOfLayerAtCell != null)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InFlight, this.m_clusterTraveler);
		}
		else if (!this.HasResourcesToMove(1, Clustercraft.CombustionResource.All) && !flag)
		{
			flag2 = true;
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.RocketStranded, orbitAsteroid);
		}
		else if (!this.m_moduleInterface.GetClusterDestinationSelector().IsAtDestination() && !this.CheckDesinationInRange())
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.DestinationOutOfRange, this.m_clusterTraveler);
		}
		else if (orbitAsteroid != null && this.Destination == orbitAsteroid.Location)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.WaitingToLand, orbitAsteroid);
		}
		else if (this.IsFlightInProgress() || this.Status == Clustercraft.CraftStatus.Launching)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InFlight, this.m_clusterTraveler);
		}
		else if (orbitAsteroid != null)
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.InOrbit, orbitAsteroid);
		}
		else
		{
			this.mainStatusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, null);
		}
		base.GetComponent<KPrefabID>().SetTag(GameTags.RocketStranded, flag2);
		float num = 0f;
		float num2 = 0f;
		foreach (CargoBayCluster cargoBayCluster in this.GetAllCargoBays())
		{
			num += cargoBayCluster.MaxCapacity;
			num2 += cargoBayCluster.RemainingCapacity;
		}
		if (this.Status != Clustercraft.CraftStatus.Grounded && num > 0f)
		{
			if (num2 == 0f)
			{
				this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, null);
				this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, false);
			}
			else
			{
				this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, false);
				if (this.cargoStatusHandle == Guid.Empty)
				{
					this.cargoStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, num2);
				}
				else
				{
					this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, true);
					this.cargoStatusHandle = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, num2);
				}
			}
		}
		else
		{
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightCargoRemaining, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.FlightAllCargoFull, false);
		}
		this.UpdatePilotedStatusItems();
	}

	// Token: 0x06005516 RID: 21782 RVA: 0x001EE85C File Offset: 0x001ECA5C
	private void UpdateGroundTags()
	{
		foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
		{
			if (@ref != null && !(@ref.Get() == null))
			{
				this.UpdateGroundTags(@ref.Get().gameObject);
			}
		}
		this.UpdateGroundTags(base.gameObject);
	}

	// Token: 0x06005517 RID: 21783 RVA: 0x001EE8D8 File Offset: 0x001ECAD8
	private void UpdateGroundTags(GameObject go)
	{
		this.SetTagOnGameObject(go, GameTags.RocketOnGround, this.status == Clustercraft.CraftStatus.Grounded);
		this.SetTagOnGameObject(go, GameTags.RocketNotOnGround, this.status > Clustercraft.CraftStatus.Grounded);
		this.SetTagOnGameObject(go, GameTags.RocketInSpace, this.status == Clustercraft.CraftStatus.InFlight);
		this.SetTagOnGameObject(go, GameTags.EntityInSpace, this.status == Clustercraft.CraftStatus.InFlight);
	}

	// Token: 0x06005518 RID: 21784 RVA: 0x001EE93C File Offset: 0x001ECB3C
	private void UpdatePilotedStatusItems()
	{
		if (this.Status == Clustercraft.CraftStatus.Grounded)
		{
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightUnpiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightPiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightSuperPilot, false);
			return;
		}
		bool flag = false;
		bool flag2 = false;
		this.GetPilotedStatus(out flag, out flag2);
		if (flag && flag2)
		{
			this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.InFlightSuperPilot, this);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightUnpiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightAutoPiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightPiloted, false);
			return;
		}
		if (flag || flag2)
		{
			this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.InFlightPiloted, this);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightUnpiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightAutoPiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightSuperPilot, false);
			return;
		}
		if (this.ModuleInterface.GetPassengerModule() != null)
		{
			this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.InFlightAutoPiloted, this);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightUnpiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightPiloted, false);
			this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightSuperPilot, false);
			return;
		}
		this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.InFlightUnpiloted, this);
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightAutoPiloted, false);
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightPiloted, false);
		this.selectable.RemoveStatusItem(Db.Get().BuildingStatusItems.InFlightSuperPilot, false);
	}

	// Token: 0x06005519 RID: 21785 RVA: 0x001EEB98 File Offset: 0x001ECD98
	public void GetPilotedStatus(out bool dupe_piloted, out bool robo_piloted)
	{
		dupe_piloted = false;
		robo_piloted = false;
		global::UnityEngine.Object passengerModule = this.ModuleInterface.GetPassengerModule();
		RoboPilotModule robotPilotModule = this.ModuleInterface.GetRobotPilotModule();
		if (passengerModule != null)
		{
			dupe_piloted = this.AutoPilotMultiplier > 0.5f;
		}
		if (robotPilotModule != null)
		{
			robo_piloted = robotPilotModule.GetDataBanksStored() > 0f;
		}
	}

	// Token: 0x0600551A RID: 21786 RVA: 0x001EEBF1 File Offset: 0x001ECDF1
	private void SetTagOnGameObject(GameObject go, Tag tag, bool set)
	{
		if (set)
		{
			go.AddTag(tag);
			return;
		}
		go.RemoveTag(tag);
	}

	// Token: 0x0600551B RID: 21787 RVA: 0x001EEC05 File Offset: 0x001ECE05
	public override bool ShowName()
	{
		return this.status > Clustercraft.CraftStatus.Grounded;
	}

	// Token: 0x0600551C RID: 21788 RVA: 0x001EEC10 File Offset: 0x001ECE10
	public override bool ShowPath()
	{
		return this.status > Clustercraft.CraftStatus.Grounded;
	}

	// Token: 0x0600551D RID: 21789 RVA: 0x001EEC1B File Offset: 0x001ECE1B
	public bool IsTravellingAndFueled()
	{
		return this.HasResourcesToMove(1, Clustercraft.CombustionResource.All) && this.m_clusterTraveler.IsTraveling();
	}

	// Token: 0x0600551E RID: 21790 RVA: 0x001EEC34 File Offset: 0x001ECE34
	public override bool ShowProgressBar()
	{
		return this.IsTravellingAndFueled();
	}

	// Token: 0x0600551F RID: 21791 RVA: 0x001EEC3C File Offset: 0x001ECE3C
	public override float GetProgress()
	{
		return this.m_clusterTraveler.GetMoveProgress();
	}

	// Token: 0x06005520 RID: 21792 RVA: 0x001EEC4C File Offset: 0x001ECE4C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.Status != Clustercraft.CraftStatus.Grounded && SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 27))
		{
			UIScheduler.Instance.ScheduleNextFrame("Check Fuel Costs", delegate(object o)
			{
				foreach (Ref<RocketModuleCluster> @ref in this.ModuleInterface.ClusterModules)
				{
					RocketModuleCluster rocketModuleCluster = @ref.Get();
					IFuelTank component = rocketModuleCluster.GetComponent<IFuelTank>();
					if (component != null && !component.Storage.IsEmpty())
					{
						component.DEBUG_FillTank();
					}
					OxidizerTank component2 = rocketModuleCluster.GetComponent<OxidizerTank>();
					if (component2 != null)
					{
						Dictionary<Tag, float> oxidizersAvailable = component2.GetOxidizersAvailable();
						if (oxidizersAvailable.Count > 0)
						{
							foreach (KeyValuePair<Tag, float> keyValuePair in oxidizersAvailable)
							{
								if (keyValuePair.Value > 0f)
								{
									component2.DEBUG_FillTank(ElementLoader.GetElementID(keyValuePair.Key));
									break;
								}
							}
						}
					}
				}
			}, null, null);
		}
	}

	// Token: 0x06005521 RID: 21793 RVA: 0x001EEC96 File Offset: 0x001ECE96
	public float GetRange()
	{
		return this.ModuleInterface.Range;
	}

	// Token: 0x06005522 RID: 21794 RVA: 0x001EECA3 File Offset: 0x001ECEA3
	public int GetRangeInTiles()
	{
		return this.ModuleInterface.RangeInTiles;
	}

	// Token: 0x04003901 RID: 14593
	[Serialize]
	private string m_name;

	// Token: 0x04003903 RID: 14595
	[MyCmpReq]
	private ClusterTraveler m_clusterTraveler;

	// Token: 0x04003904 RID: 14596
	[MyCmpReq]
	private CraftModuleInterface m_moduleInterface;

	// Token: 0x04003905 RID: 14597
	private Guid mainStatusHandle;

	// Token: 0x04003906 RID: 14598
	private Guid cargoStatusHandle;

	// Token: 0x04003907 RID: 14599
	private Guid missionControlStatusHandle = Guid.Empty;

	// Token: 0x04003908 RID: 14600
	public static Dictionary<Tag, float> dlc1OxidizerEfficiencies = new Dictionary<Tag, float>
	{
		{
			SimHashes.OxyRock.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.LOW
		},
		{
			SimHashes.LiquidOxygen.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.HIGH
		},
		{
			SimHashes.Fertilizer.CreateTag(),
			ROCKETRY.DLC1_OXIDIZER_EFFICIENCY.VERY_LOW
		}
	};

	// Token: 0x04003909 RID: 14601
	[Serialize]
	[Range(0f, 1f)]
	public float AutoPilotMultiplier = 1f;

	// Token: 0x0400390A RID: 14602
	[Serialize]
	[Range(0f, 2f)]
	public float PilotSkillMultiplier = 1f;

	// Token: 0x0400390B RID: 14603
	[Serialize]
	public float controlStationBuffTimeRemaining;

	// Token: 0x0400390C RID: 14604
	[Serialize]
	private bool m_launchRequested;

	// Token: 0x0400390D RID: 14605
	[Serialize]
	private Clustercraft.CraftStatus status;

	// Token: 0x0400390E RID: 14606
	[MyCmpGet]
	private KSelectable selectable;

	// Token: 0x0400390F RID: 14607
	private static EventSystem.IntraObjectHandler<Clustercraft> RocketModuleChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.RocketModuleChanged(data);
	});

	// Token: 0x04003910 RID: 14608
	private static EventSystem.IntraObjectHandler<Clustercraft> ClusterDestinationChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.OnClusterDestinationChanged(data);
	});

	// Token: 0x04003911 RID: 14609
	private static EventSystem.IntraObjectHandler<Clustercraft> ClusterDestinationReachedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.OnClusterDestinationReached(data);
	});

	// Token: 0x04003912 RID: 14610
	private static EventSystem.IntraObjectHandler<Clustercraft> NameChangedHandler = new EventSystem.IntraObjectHandler<Clustercraft>(delegate(Clustercraft cmp, object data)
	{
		cmp.SetRocketName(data);
	});

	// Token: 0x02001C53 RID: 7251
	public enum CraftStatus
	{
		// Token: 0x040085F1 RID: 34289
		Grounded,
		// Token: 0x040085F2 RID: 34290
		Launching,
		// Token: 0x040085F3 RID: 34291
		InFlight,
		// Token: 0x040085F4 RID: 34292
		Landing
	}

	// Token: 0x02001C54 RID: 7252
	public enum CombustionResource
	{
		// Token: 0x040085F6 RID: 34294
		Fuel,
		// Token: 0x040085F7 RID: 34295
		Oxidizer,
		// Token: 0x040085F8 RID: 34296
		All
	}

	// Token: 0x02001C55 RID: 7253
	public enum PadLandingStatus
	{
		// Token: 0x040085FA RID: 34298
		CanLandImmediately,
		// Token: 0x040085FB RID: 34299
		CanLandEventually,
		// Token: 0x040085FC RID: 34300
		CanNeverLand
	}
}
