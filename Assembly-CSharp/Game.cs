using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using FMOD.Studio;
using Klei;
using Klei.AI;
using Klei.CustomSettings;
using KSerialization;
using ProcGenGame;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

// Token: 0x02000926 RID: 2342
[AddComponentMenu("KMonoBehaviour/scripts/Game")]
public class Game : KMonoBehaviour
{
	// Token: 0x0600414E RID: 16718 RVA: 0x0016EF7E File Offset: 0x0016D17E
	public static bool IsOnMainThread()
	{
		return Game.MainThread == Thread.CurrentThread;
	}

	// Token: 0x0600414F RID: 16719 RVA: 0x0016EF8C File Offset: 0x0016D18C
	public static bool IsQuitting()
	{
		return Game.quitting;
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x06004150 RID: 16720 RVA: 0x0016EF93 File Offset: 0x0016D193
	// (set) Token: 0x06004151 RID: 16721 RVA: 0x0016EF9B File Offset: 0x0016D19B
	public KInputHandler inputHandler { get; set; }

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x06004152 RID: 16722 RVA: 0x0016EFA4 File Offset: 0x0016D1A4
	// (set) Token: 0x06004153 RID: 16723 RVA: 0x0016EFAB File Offset: 0x0016D1AB
	public static Game Instance { get; private set; }

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x06004154 RID: 16724 RVA: 0x0016EFB3 File Offset: 0x0016D1B3
	public static Camera MainCamera
	{
		get
		{
			if (Game.m_CachedCamera == null)
			{
				Game.m_CachedCamera = Camera.main;
			}
			return Game.m_CachedCamera;
		}
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06004155 RID: 16725 RVA: 0x0016EFD1 File Offset: 0x0016D1D1
	// (set) Token: 0x06004156 RID: 16726 RVA: 0x0016EFF4 File Offset: 0x0016D1F4
	public bool SaveToCloudActive
	{
		get
		{
			return CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.SaveToCloud).id == "Enabled";
		}
		set
		{
			string text = (value ? "Enabled" : "Disabled");
			CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.SaveToCloud, text);
		}
	}

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06004157 RID: 16727 RVA: 0x0016F021 File Offset: 0x0016D221
	// (set) Token: 0x06004158 RID: 16728 RVA: 0x0016F044 File Offset: 0x0016D244
	public bool FastWorkersModeActive
	{
		get
		{
			return CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.FastWorkersMode).id == "Enabled";
		}
		set
		{
			string text = (value ? "Enabled" : "Disabled");
			CustomGameSettings.Instance.SetQualitySetting(CustomGameSettingConfigs.FastWorkersMode, text);
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06004159 RID: 16729 RVA: 0x0016F071 File Offset: 0x0016D271
	// (set) Token: 0x0600415A RID: 16730 RVA: 0x0016F07C File Offset: 0x0016D27C
	public bool SandboxModeActive
	{
		get
		{
			return this.sandboxModeActive;
		}
		set
		{
			this.sandboxModeActive = value;
			base.Trigger(-1948169901, null);
			if (PlanScreen.Instance != null)
			{
				PlanScreen.Instance.Refresh();
			}
			if (BuildMenu.Instance != null)
			{
				BuildMenu.Instance.Refresh();
			}
			if (OverlayMenu.Instance != null)
			{
				OverlayMenu.Instance.Refresh();
			}
			if (ManagementMenu.Instance != null)
			{
				ManagementMenu.Instance.Refresh();
			}
		}
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x0600415B RID: 16731 RVA: 0x0016F0F8 File Offset: 0x0016D2F8
	public bool DebugOnlyBuildingsAllowed
	{
		get
		{
			return DebugHandler.enabled && (this.SandboxModeActive || DebugHandler.InstantBuildMode);
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x0600415C RID: 16732 RVA: 0x0016F112 File Offset: 0x0016D312
	// (set) Token: 0x0600415D RID: 16733 RVA: 0x0016F11A File Offset: 0x0016D31A
	public StatusItemRenderer statusItemRenderer { get; private set; }

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x0600415E RID: 16734 RVA: 0x0016F123 File Offset: 0x0016D323
	// (set) Token: 0x0600415F RID: 16735 RVA: 0x0016F12B File Offset: 0x0016D32B
	public PrioritizableRenderer prioritizableRenderer { get; private set; }

	// Token: 0x06004160 RID: 16736 RVA: 0x0016F134 File Offset: 0x0016D334
	protected override void OnPrefabInit()
	{
		global::UnityEngine.Debug.unityLogger.logHandler = new LogCatcher(global::UnityEngine.Debug.unityLogger.logHandler);
		DebugUtil.LogArgs(new object[]
		{
			Time.realtimeSinceStartup,
			"Level Loaded....",
			SceneManager.GetActiveScene().name
		});
		Components.EntityCellVisualizers.OnAdd += this.OnAddBuildingCellVisualizer;
		Components.EntityCellVisualizers.OnRemove += this.OnRemoveBuildingCellVisualizer;
		Singleton<KBatchedAnimUpdater>.CreateInstance();
		Singleton<CellChangeMonitor>.CreateInstance();
		this.userMenu = new UserMenu();
		SimTemperatureTransfer.ClearInstanceMap();
		StructureTemperatureComponents.ClearInstanceMap();
		ElementConsumer.ClearInstanceMap();
		App.OnPreLoadScene = (global::System.Action)Delegate.Combine(App.OnPreLoadScene, new global::System.Action(this.StopBE));
		Game.Instance = this;
		this.statusItemRenderer = new StatusItemRenderer();
		this.prioritizableRenderer = new PrioritizableRenderer();
		this.LoadEventHashes();
		this.savedInfo.InitializeEmptyVariables();
		this.gasFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.GasConduits) - 0.4f);
		this.liquidFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.LiquidConduits) - 0.4f);
		this.solidFlowPos = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.SolidConduitContents) - 0.4f);
		Shader.WarmupAllShaders();
		Db.Get();
		Game.quitting = false;
		Game.PickupableLayer = LayerMask.NameToLayer("Pickupable");
		Game.BlockSelectionLayerMask = LayerMask.GetMask(new string[] { "BlockSelection" });
		this.world = World.Instance;
		KPrefabID.NextUniqueID = KPlayerPrefs.GetInt(Game.NextUniqueIDKey, 0);
		this.circuitManager = new CircuitManager();
		this.energySim = new EnergySim();
		this.gasConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, Vent>(Grid.WidthInCells, Grid.HeightInCells, 13);
		this.liquidConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, Vent>(Grid.WidthInCells, Grid.HeightInCells, 17);
		this.electricalConduitSystem = new UtilityNetworkManager<ElectricalUtilityNetwork, Wire>(Grid.WidthInCells, Grid.HeightInCells, 27);
		this.logicCircuitSystem = new UtilityNetworkManager<LogicCircuitNetwork, LogicWire>(Grid.WidthInCells, Grid.HeightInCells, 32);
		this.logicCircuitManager = new LogicCircuitManager(this.logicCircuitSystem);
		this.travelTubeSystem = new UtilityNetworkTubesManager(Grid.WidthInCells, Grid.HeightInCells, 35);
		this.solidConduitSystem = new UtilityNetworkManager<FlowUtilityNetwork, SolidConduit>(Grid.WidthInCells, Grid.HeightInCells, 21);
		this.conduitTemperatureManager = new ConduitTemperatureManager();
		this.conduitDiseaseManager = new ConduitDiseaseManager(this.conduitTemperatureManager);
		this.gasConduitFlow = new ConduitFlow(ConduitType.Gas, Grid.CellCount, this.gasConduitSystem, 1f, 0.25f);
		this.liquidConduitFlow = new ConduitFlow(ConduitType.Liquid, Grid.CellCount, this.liquidConduitSystem, 10f, 0.75f);
		this.solidConduitFlow = new SolidConduitFlow(Grid.CellCount, this.solidConduitSystem, 0.75f);
		this.gasFlowVisualizer = new ConduitFlowVisualizer(this.gasConduitFlow, this.gasConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundGas, Lighting.Instance.Settings.GasConduit);
		this.liquidFlowVisualizer = new ConduitFlowVisualizer(this.liquidConduitFlow, this.liquidConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundLiquid, Lighting.Instance.Settings.LiquidConduit);
		this.solidFlowVisualizer = new SolidConduitFlowVisualizer(this.solidConduitFlow, this.solidConduitVisInfo, GlobalResources.Instance().ConduitOverlaySoundSolid, Lighting.Instance.Settings.SolidConduit);
		this.accumulators = new Accumulators();
		this.plantElementAbsorbers = new PlantElementAbsorbers();
		this.activeFX = new ushort[Grid.CellCount];
		this.UnsafePrefabInit();
		Shader.SetGlobalVector("_MetalParameters", new Vector4(0f, 0f, 0f, 0f));
		Shader.SetGlobalVector("_WaterParameters", new Vector4(0f, 0f, 0f, 0f));
		this.InitializeFXSpawners();
		PathFinder.Initialize();
		new GameNavGrids(Pathfinding.Instance);
		this.screenMgr = global::Util.KInstantiate(this.screenManagerPrefab, null, null).GetComponent<GameScreenManager>();
		this.roomProber = new RoomProber();
		this.spaceScannerNetworkManager = new SpaceScannerNetworkManager();
		this.fetchManager = base.gameObject.AddComponent<FetchManager>();
		this.ediblesManager = base.gameObject.AddComponent<EdiblesManager>();
		Singleton<CellChangeMonitor>.Instance.SetGridSize(Grid.WidthInCells, Grid.HeightInCells);
		this.unlocks = base.GetComponent<Unlocks>();
		this.changelistsPlayedOn = new List<uint>();
		this.changelistsPlayedOn.Add(679336U);
		this.dateGenerated = global::System.DateTime.UtcNow.ToString("U", CultureInfo.InvariantCulture);
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x0016F5C7 File Offset: 0x0016D7C7
	public void SetGameStarted()
	{
		this.gameStarted = true;
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x0016F5D0 File Offset: 0x0016D7D0
	public bool GameStarted()
	{
		return this.gameStarted;
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x0016F5D8 File Offset: 0x0016D7D8
	private IEnumerator SanityCheckBoundsNextFrame()
	{
		yield return null;
		using (List<WorldContainer>.Enumerator enumerator = ClusterManager.Instance.WorldContainers.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				WorldContainer worldContainer = enumerator.Current;
				if (worldContainer.IsDiscovered && !worldContainer.IsModuleInterior)
				{
					for (int i = worldContainer.WorldOffset.X; i < worldContainer.WorldOffset.X + worldContainer.WorldSize.X; i++)
					{
						for (int j = 0; j < Grid.TopBorderHeight; j++)
						{
							int num = Grid.XYToCell(i, worldContainer.WorldOffset.Y + worldContainer.WorldSize.Y - j);
							if (Grid.IsSolidCell(num) && Grid.Element[num].id != SimHashes.Unobtanium)
							{
								SimMessages.Dig(num, -1, true);
							}
						}
					}
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x0016F5E0 File Offset: 0x0016D7E0
	private void UnsafePrefabInit()
	{
		this.StepTheSim(0f);
		base.StartCoroutine(this.SanityCheckBoundsNextFrame());
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x0016F5FB File Offset: 0x0016D7FB
	protected override void OnLoadLevel()
	{
		base.Unsubscribe<Game>(1798162660, Game.MarkStatusItemRendererDirtyDelegate, false);
		base.Unsubscribe<Game>(1983128072, Game.ActiveWorldChangedDelegate, false);
		base.OnLoadLevel();
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x0016F625 File Offset: 0x0016D825
	private void MarkStatusItemRendererDirty(object data)
	{
		this.statusItemRenderer.MarkAllDirty();
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x0016F634 File Offset: 0x0016D834
	protected override void OnForcedCleanUp()
	{
		if (this.prioritizableRenderer != null)
		{
			this.prioritizableRenderer.Cleanup();
			this.prioritizableRenderer = null;
		}
		if (this.statusItemRenderer != null)
		{
			this.statusItemRenderer.Destroy();
			this.statusItemRenderer = null;
		}
		if (this.conduitTemperatureManager != null)
		{
			this.conduitTemperatureManager.Shutdown();
		}
		this.gasFlowVisualizer.FreeResources();
		this.liquidFlowVisualizer.FreeResources();
		this.solidFlowVisualizer.FreeResources();
		LightGridManager.Shutdown();
		RadiationGridManager.Shutdown();
		App.OnPreLoadScene = (global::System.Action)Delegate.Remove(App.OnPreLoadScene, new global::System.Action(this.StopBE));
		base.OnForcedCleanUp();
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x0016F6DC File Offset: 0x0016D8DC
	protected override void OnSpawn()
	{
		global::Debug.Log("-- GAME --");
		Game.BrainScheduler = base.GetComponent<BrainScheduler>();
		PropertyTextures.FogOfWarScale = 0f;
		if (CameraController.Instance != null)
		{
			CameraController.Instance.EnableFreeCamera(false);
		}
		this.LocalPlayer = this.SpawnPlayer();
		WaterCubes.Instance.Init();
		SpeedControlScreen.Instance.Pause(false, false);
		LightGridManager.Initialise();
		RadiationGridManager.Initialise();
		this.RefreshRadiationLoop();
		this.UnsafeOnSpawn();
		Time.timeScale = 0f;
		if (this.tempIntroScreenPrefab != null)
		{
			global::Util.KInstantiate(this.tempIntroScreenPrefab, null, null);
		}
		if (SaveLoader.Instance.Cluster != null)
		{
			foreach (WorldGen worldGen in SaveLoader.Instance.Cluster.worlds)
			{
				this.Reset(worldGen.data.gameSpawnData, worldGen.WorldOffset);
			}
			NewBaseScreen.SetInitialCamera();
		}
		TagManager.FillMissingProperNames();
		CameraController.Instance.OrthographicSize = 20f;
		if (SaveLoader.Instance.loadedFromSave)
		{
			this.baseAlreadyCreated = true;
			base.Trigger(-1992507039, null);
			base.Trigger(-838649377, null);
		}
		global::UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(MeshRenderer));
		for (int i = 0; i < array.Length; i++)
		{
			((MeshRenderer)array[i]).reflectionProbeUsage = ReflectionProbeUsage.Off;
		}
		base.Subscribe<Game>(1798162660, Game.MarkStatusItemRendererDirtyDelegate);
		base.Subscribe<Game>(1983128072, Game.ActiveWorldChangedDelegate);
		this.solidConduitFlow.Initialize();
		SimAndRenderScheduler.instance.Add(this.roomProber, false);
		SimAndRenderScheduler.instance.Add(this.spaceScannerNetworkManager, false);
		SimAndRenderScheduler.instance.Add(KComponentSpawn.instance, false);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim200ms, AmountInstance>(new UpdateBucketWithUpdater<ISim200ms>.BatchUpdateDelegate(AmountInstance.BatchUpdate));
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim1000ms, SolidTransferArm>(new UpdateBucketWithUpdater<ISim1000ms>.BatchUpdateDelegate(SolidTransferArm.BatchUpdate));
		if (!SaveLoader.Instance.loadedFromSave)
		{
			SettingConfig settingConfig = CustomGameSettings.Instance.QualitySettings[CustomGameSettingConfigs.SandboxMode.id];
			SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.SandboxMode);
			SaveGame.Instance.sandboxEnabled = !settingConfig.IsDefaultLevel(currentQualitySetting.id);
		}
		this.mingleCellTracker = base.gameObject.AddComponent<MingleCellTracker>();
		if (Global.Instance != null)
		{
			Global.Instance.GetComponent<PerformanceMonitor>().Reset();
			Global.Instance.modManager.NotifyDialog(UI.FRONTEND.MOD_DIALOGS.SAVE_GAME_MODS_DIFFER.TITLE, UI.FRONTEND.MOD_DIALOGS.SAVE_GAME_MODS_DIFFER.MESSAGE, Global.Instance.globalCanvas);
		}
	}

	// Token: 0x06004169 RID: 16745 RVA: 0x0016F994 File Offset: 0x0016DB94
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		SimAndRenderScheduler.instance.Remove(KComponentSpawn.instance);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim200ms, AmountInstance>(null);
		SimAndRenderScheduler.instance.RegisterBatchUpdate<ISim1000ms, SolidTransferArm>(null);
		this.DestroyInstances();
	}

	// Token: 0x0600416A RID: 16746 RVA: 0x0016F9C7 File Offset: 0x0016DBC7
	private new void OnDestroy()
	{
		base.OnDestroy();
		this.DestroyInstances();
	}

	// Token: 0x0600416B RID: 16747 RVA: 0x0016F9D5 File Offset: 0x0016DBD5
	private void UnsafeOnSpawn()
	{
		this.world.UpdateCellInfo(this.gameSolidInfo, this.callbackInfo, 0, null, 0, null);
	}

	// Token: 0x0600416C RID: 16748 RVA: 0x0016F9F4 File Offset: 0x0016DBF4
	private void RefreshRadiationLoop()
	{
		GameScheduler.Instance.Schedule("UpdateRadiation", 1f, delegate(object obj)
		{
			RadiationGridManager.Refresh();
			this.RefreshRadiationLoop();
		}, null, null);
	}

	// Token: 0x0600416D RID: 16749 RVA: 0x0016FA19 File Offset: 0x0016DC19
	public void SetMusicEnabled(bool enabled)
	{
		if (enabled)
		{
			MusicManager.instance.PlaySong("Music_FrontEnd", false);
			return;
		}
		MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x0016FA40 File Offset: 0x0016DC40
	private Player SpawnPlayer()
	{
		Player component = global::Util.KInstantiate(this.playerPrefab, base.gameObject, null).GetComponent<Player>();
		component.ScreenManager = this.screenMgr;
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.HudScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay);
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.HoverTextScreen.gameObject, null, GameScreenManager.UIRenderTarget.HoverTextScreen);
		component.ScreenManager.StartScreen(ScreenPrefabs.Instance.ToolTipScreen.gameObject, null, GameScreenManager.UIRenderTarget.HoverTextScreen);
		this.cameraController = global::Util.KInstantiate(this.cameraControllerPrefab, null, null).GetComponent<CameraController>();
		component.CameraController = this.cameraController;
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, this.cameraController, 1);
		}
		else
		{
			KInputHandler.Add(Global.GetInputManager().GetDefaultController(), this.cameraController, 1);
		}
		Global.GetInputManager().usedMenus.Add(this.cameraController);
		this.playerController = component.GetComponent<PlayerController>();
		if (KInputManager.currentController != null)
		{
			KInputHandler.Add(KInputManager.currentController, this.playerController, 20);
		}
		else
		{
			KInputHandler.Add(Global.GetInputManager().GetDefaultController(), this.playerController, 20);
		}
		Global.GetInputManager().usedMenus.Add(this.playerController);
		return component;
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x0016FB85 File Offset: 0x0016DD85
	public void SetDupePassableSolid(int cell, bool passable, bool solid)
	{
		Grid.DupePassable[cell] = passable;
		this.gameSolidInfo.Add(new SolidInfo(cell, solid));
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x0016FBA8 File Offset: 0x0016DDA8
	private unsafe Sim.GameDataUpdate* StepTheSim(float dt)
	{
		Sim.GameDataUpdate* ptr;
		using (new KProfiler.Region("StepTheSim", null))
		{
			IntPtr intPtr = IntPtr.Zero;
			using (new KProfiler.Region("WaitingForSim", null))
			{
				if (Grid.Visible == null || Grid.Visible.Length == 0)
				{
					global::Debug.LogError("Invalid Grid.Visible, what have you done?!");
					return null;
				}
				intPtr = Sim.HandleMessage(SimMessageHashes.PrepareGameData, Grid.Visible.Length, Grid.Visible);
			}
			if (intPtr == IntPtr.Zero)
			{
				ptr = null;
			}
			else
			{
				Sim.GameDataUpdate* ptr2 = (Sim.GameDataUpdate*)(void*)intPtr;
				Grid.elementIdx = ptr2->elementIdx;
				Grid.temperature = ptr2->temperature;
				Grid.mass = ptr2->mass;
				Grid.radiation = ptr2->radiation;
				Grid.properties = ptr2->properties;
				Grid.strengthInfo = ptr2->strengthInfo;
				Grid.insulation = ptr2->insulation;
				Grid.diseaseIdx = ptr2->diseaseIdx;
				Grid.diseaseCount = ptr2->diseaseCount;
				Grid.AccumulatedFlowValues = ptr2->accumulatedFlow;
				Grid.exposedToSunlight = (byte*)(void*)ptr2->propertyTextureExposedToSunlight;
				PropertyTextures.externalFlowTex = ptr2->propertyTextureFlow;
				PropertyTextures.externalLiquidTex = ptr2->propertyTextureLiquid;
				PropertyTextures.externalLiquidDataTex = ptr2->propertyTextureLiquidData;
				PropertyTextures.externalExposedToSunlight = ptr2->propertyTextureExposedToSunlight;
				List<Element> elements = ElementLoader.elements;
				this.simData.emittedMassEntries = ptr2->emittedMassEntries;
				this.simData.elementChunks = ptr2->elementChunkInfos;
				this.simData.buildingTemperatures = ptr2->buildingTemperatures;
				this.simData.diseaseEmittedInfos = ptr2->diseaseEmittedInfos;
				this.simData.diseaseConsumedInfos = ptr2->diseaseConsumedInfos;
				for (int i = 0; i < ptr2->numSubstanceChangeInfo; i++)
				{
					Sim.SubstanceChangeInfo substanceChangeInfo = ptr2->substanceChangeInfo[i];
					Element element = elements[(int)substanceChangeInfo.newElemIdx];
					Grid.Element[substanceChangeInfo.cellIdx] = element;
				}
				for (int j = 0; j < ptr2->numSolidInfo; j++)
				{
					Sim.SolidInfo solidInfo = ptr2->solidInfo[j];
					if (!this.solidChangedFilter.Contains(solidInfo.cellIdx))
					{
						this.solidInfo.Add(new SolidInfo(solidInfo.cellIdx, solidInfo.isSolid != 0));
						bool flag = solidInfo.isSolid != 0;
						Grid.SetSolid(solidInfo.cellIdx, flag, CellEventLogger.Instance.SimMessagesSolid);
						if (flag && Grid.IsWorldValidCell(solidInfo.cellIdx))
						{
							int num = (int)Grid.WorldIdx[solidInfo.cellIdx];
							if (num >= 0 && num < ClusterManager.Instance.WorldContainers.Count)
							{
								WorldContainer worldContainer = ClusterManager.Instance.WorldContainers[num];
								int num2;
								int num3;
								Grid.CellToXY(solidInfo.cellIdx, out num2, out num3);
								if (!worldContainer.IsModuleInterior && num3 > worldContainer.WorldOffset.Y + worldContainer.WorldSize.Y - Grid.TopBorderHeight)
								{
									SimMessages.Dig(solidInfo.cellIdx, -1, true);
								}
							}
						}
					}
				}
				for (int k = 0; k < ptr2->numCallbackInfo; k++)
				{
					Sim.CallbackInfo callbackInfo = ptr2->callbackInfo[k];
					HandleVector<Game.CallbackInfo>.Handle handle = new HandleVector<Game.CallbackInfo>.Handle
					{
						index = callbackInfo.callbackIdx
					};
					if (!this.IsManuallyReleasedHandle(handle))
					{
						this.callbackInfo.Add(new global::Klei.CallbackInfo(handle));
					}
				}
				int numSpawnFallingLiquidInfo = ptr2->numSpawnFallingLiquidInfo;
				for (int l = 0; l < numSpawnFallingLiquidInfo; l++)
				{
					Sim.SpawnFallingLiquidInfo spawnFallingLiquidInfo = ptr2->spawnFallingLiquidInfo[l];
					FallingWater.instance.AddParticle(spawnFallingLiquidInfo.cellIdx, spawnFallingLiquidInfo.elemIdx, spawnFallingLiquidInfo.mass, spawnFallingLiquidInfo.temperature, spawnFallingLiquidInfo.diseaseIdx, spawnFallingLiquidInfo.diseaseCount, false, false, false, false);
				}
				int numDigInfo = ptr2->numDigInfo;
				WorldDamage component = this.world.GetComponent<WorldDamage>();
				for (int m = 0; m < numDigInfo; m++)
				{
					Sim.SpawnOreInfo spawnOreInfo = ptr2->digInfo[m];
					if (spawnOreInfo.temperature <= 0f && spawnOreInfo.mass > 0f)
					{
						global::Debug.LogError("Sim is telling us to spawn a zero temperature object. This shouldn't be possible because I have asserts in the dll about this....");
					}
					component.OnDigComplete(spawnOreInfo.cellIdx, spawnOreInfo.mass, spawnOreInfo.temperature, spawnOreInfo.elemIdx, spawnOreInfo.diseaseIdx, spawnOreInfo.diseaseCount);
				}
				int numSpawnOreInfo = ptr2->numSpawnOreInfo;
				for (int n = 0; n < numSpawnOreInfo; n++)
				{
					Sim.SpawnOreInfo spawnOreInfo2 = ptr2->spawnOreInfo[n];
					Vector3 vector = Grid.CellToPosCCC(spawnOreInfo2.cellIdx, Grid.SceneLayer.Ore);
					Element element2 = ElementLoader.elements[(int)spawnOreInfo2.elemIdx];
					if (spawnOreInfo2.temperature <= 0f && spawnOreInfo2.mass > 0f)
					{
						global::Debug.LogError("Sim is telling us to spawn a zero temperature object. This shouldn't be possible because I have asserts in the dll about this....");
					}
					element2.substance.SpawnResource(vector, spawnOreInfo2.mass, spawnOreInfo2.temperature, spawnOreInfo2.diseaseIdx, spawnOreInfo2.diseaseCount, false, false, false);
				}
				int numSpawnFXInfo = ptr2->numSpawnFXInfo;
				for (int num4 = 0; num4 < numSpawnFXInfo; num4++)
				{
					Sim.SpawnFXInfo spawnFXInfo = ptr2->spawnFXInfo[num4];
					this.SpawnFX((SpawnFXHashes)spawnFXInfo.fxHash, spawnFXInfo.cellIdx, spawnFXInfo.rotation);
				}
				UnstableGroundManager component2 = this.world.GetComponent<UnstableGroundManager>();
				int numUnstableCellInfo = ptr2->numUnstableCellInfo;
				for (int num5 = 0; num5 < numUnstableCellInfo; num5++)
				{
					Sim.UnstableCellInfo unstableCellInfo = ptr2->unstableCellInfo[num5];
					if (unstableCellInfo.fallingInfo == 0)
					{
						component2.Spawn(unstableCellInfo.cellIdx, ElementLoader.elements[(int)unstableCellInfo.elemIdx], unstableCellInfo.mass, unstableCellInfo.temperature, unstableCellInfo.diseaseIdx, unstableCellInfo.diseaseCount);
					}
				}
				int numWorldDamageInfo = ptr2->numWorldDamageInfo;
				for (int num6 = 0; num6 < numWorldDamageInfo; num6++)
				{
					Sim.WorldDamageInfo worldDamageInfo = ptr2->worldDamageInfo[num6];
					WorldDamage.Instance.ApplyDamage(worldDamageInfo);
				}
				for (int num7 = 0; num7 < ptr2->numRemovedMassEntries; num7++)
				{
					ElementConsumer.AddMass(ptr2->removedMassEntries[num7]);
				}
				int numMassConsumedCallbacks = ptr2->numMassConsumedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle2 = default(HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle);
				for (int num8 = 0; num8 < numMassConsumedCallbacks; num8++)
				{
					Sim.MassConsumedCallback massConsumedCallback = ptr2->massConsumedCallbacks[num8];
					handle2.index = massConsumedCallback.callbackIdx;
					Game.ComplexCallbackInfo<Sim.MassConsumedCallback> complexCallbackInfo = this.massConsumedCallbackManager.Release(handle2, "massConsumedCB");
					if (complexCallbackInfo.cb != null)
					{
						complexCallbackInfo.cb(massConsumedCallback, complexCallbackInfo.callbackData);
					}
				}
				int numMassEmittedCallbacks = ptr2->numMassEmittedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle handle3 = default(HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle);
				for (int num9 = 0; num9 < numMassEmittedCallbacks; num9++)
				{
					Sim.MassEmittedCallback massEmittedCallback = ptr2->massEmittedCallbacks[num9];
					handle3.index = massEmittedCallback.callbackIdx;
					if (this.massEmitCallbackManager.IsVersionValid(handle3))
					{
						Game.ComplexCallbackInfo<Sim.MassEmittedCallback> item = this.massEmitCallbackManager.GetItem(handle3);
						if (item.cb != null)
						{
							item.cb(massEmittedCallback, item.callbackData);
						}
					}
				}
				int numDiseaseConsumptionCallbacks = ptr2->numDiseaseConsumptionCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback>>.Handle handle4 = default(HandleVector<Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback>>.Handle);
				for (int num10 = 0; num10 < numDiseaseConsumptionCallbacks; num10++)
				{
					Sim.DiseaseConsumptionCallback diseaseConsumptionCallback = ptr2->diseaseConsumptionCallbacks[num10];
					handle4.index = diseaseConsumptionCallback.callbackIdx;
					if (this.diseaseConsumptionCallbackManager.IsVersionValid(handle4))
					{
						Game.ComplexCallbackInfo<Sim.DiseaseConsumptionCallback> item2 = this.diseaseConsumptionCallbackManager.GetItem(handle4);
						if (item2.cb != null)
						{
							item2.cb(diseaseConsumptionCallback, item2.callbackData);
						}
					}
				}
				int numComponentStateChangedMessages = ptr2->numComponentStateChangedMessages;
				HandleVector<Game.ComplexCallbackInfo<int>>.Handle handle5 = default(HandleVector<Game.ComplexCallbackInfo<int>>.Handle);
				for (int num11 = 0; num11 < numComponentStateChangedMessages; num11++)
				{
					Sim.ComponentStateChangedMessage componentStateChangedMessage = ptr2->componentStateChangedMessages[num11];
					handle5.index = componentStateChangedMessage.callbackIdx;
					if (this.simComponentCallbackManager.IsVersionValid(handle5))
					{
						Game.ComplexCallbackInfo<int> complexCallbackInfo2 = this.simComponentCallbackManager.Release(handle5, "component state changed cb");
						if (complexCallbackInfo2.cb != null)
						{
							complexCallbackInfo2.cb(componentStateChangedMessage.simHandle, complexCallbackInfo2.callbackData);
						}
					}
				}
				int numRadiationConsumedCallbacks = ptr2->numRadiationConsumedCallbacks;
				HandleVector<Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback>>.Handle handle6 = default(HandleVector<Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback>>.Handle);
				for (int num12 = 0; num12 < numRadiationConsumedCallbacks; num12++)
				{
					Sim.ConsumedRadiationCallback consumedRadiationCallback = ptr2->radiationConsumedCallbacks[num12];
					handle6.index = consumedRadiationCallback.callbackIdx;
					Game.ComplexCallbackInfo<Sim.ConsumedRadiationCallback> complexCallbackInfo3 = this.radiationConsumedCallbackManager.Release(handle6, "radiationConsumedCB");
					if (complexCallbackInfo3.cb != null)
					{
						complexCallbackInfo3.cb(consumedRadiationCallback, complexCallbackInfo3.callbackData);
					}
				}
				int numElementChunkMeltedInfos = ptr2->numElementChunkMeltedInfos;
				for (int num13 = 0; num13 < numElementChunkMeltedInfos; num13++)
				{
					SimTemperatureTransfer.DoOreMeltTransition(ptr2->elementChunkMeltedInfos[num13].handle);
				}
				int numBuildingOverheatInfos = ptr2->numBuildingOverheatInfos;
				for (int num14 = 0; num14 < numBuildingOverheatInfos; num14++)
				{
					StructureTemperatureComponents.DoOverheat(ptr2->buildingOverheatInfos[num14].handle);
				}
				int numBuildingNoLongerOverheatedInfos = ptr2->numBuildingNoLongerOverheatedInfos;
				for (int num15 = 0; num15 < numBuildingNoLongerOverheatedInfos; num15++)
				{
					StructureTemperatureComponents.DoNoLongerOverheated(ptr2->buildingNoLongerOverheatedInfos[num15].handle);
				}
				int numBuildingMeltedInfos = ptr2->numBuildingMeltedInfos;
				for (int num16 = 0; num16 < numBuildingMeltedInfos; num16++)
				{
					StructureTemperatureComponents.DoStateTransition(ptr2->buildingMeltedInfos[num16].handle);
				}
				int numCellMeltedInfos = ptr2->numCellMeltedInfos;
				for (int num17 = 0; num17 < numCellMeltedInfos; num17++)
				{
					int gameCell = ptr2->cellMeltedInfos[num17].gameCell;
					GameObject gameObject = Grid.Objects[gameCell, 9];
					if (gameObject != null)
					{
						gameObject.Trigger(675471409, null);
						global::Util.KDestroyGameObject(gameObject);
					}
				}
				if (dt > 0f)
				{
					this.conduitTemperatureManager.Sim200ms(0.2f);
					this.conduitDiseaseManager.Sim200ms(0.2f);
					this.gasConduitFlow.Sim200ms(0.2f);
					this.liquidConduitFlow.Sim200ms(0.2f);
					this.solidConduitFlow.Sim200ms(0.2f);
					this.accumulators.Sim200ms(0.2f);
					this.plantElementAbsorbers.Sim200ms(0.2f);
				}
				Sim.DebugProperties debugProperties;
				debugProperties.buildingTemperatureScale = 100f;
				debugProperties.buildingToBuildingTemperatureScale = 0.001f;
				debugProperties.biomeTemperatureLerpRate = 0.001f;
				debugProperties.isDebugEditing = ((DebugPaintElementScreen.Instance != null && DebugPaintElementScreen.Instance.gameObject.activeSelf) ? 1 : 0);
				debugProperties.pad0 = (debugProperties.pad1 = (debugProperties.pad2 = 0));
				SimMessages.SetDebugProperties(debugProperties);
				if (dt > 0f)
				{
					if (this.circuitManager != null)
					{
						this.circuitManager.Sim200msFirst(dt);
					}
					if (this.energySim != null)
					{
						this.energySim.EnergySim200ms(dt);
					}
					if (this.circuitManager != null)
					{
						this.circuitManager.Sim200msLast(dt);
					}
				}
				ptr = ptr2;
			}
		}
		return ptr;
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x0017074C File Offset: 0x0016E94C
	public void AddSolidChangedFilter(int cell)
	{
		this.solidChangedFilter.Add(cell);
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x0017075B File Offset: 0x0016E95B
	public void RemoveSolidChangedFilter(int cell)
	{
		this.solidChangedFilter.Remove(cell);
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x0017076A File Offset: 0x0016E96A
	public void SetIsLoading()
	{
		this.isLoading = true;
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x00170773 File Offset: 0x0016E973
	public bool IsLoading()
	{
		return this.isLoading;
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x0017077C File Offset: 0x0016E97C
	private void ShowDebugCellInfo()
	{
		int mouseCell = DebugHandler.GetMouseCell();
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(mouseCell, out num, out num2);
		string text = string.Concat(new string[]
		{
			mouseCell.ToString(),
			" (",
			num.ToString(),
			", ",
			num2.ToString(),
			")"
		});
		DebugText.Instance.Draw(text, Grid.CellToPosCCC(mouseCell, Grid.SceneLayer.Move), Color.white);
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x001707F7 File Offset: 0x0016E9F7
	public void ForceSimStep()
	{
		DebugUtil.LogArgs(new object[] { "Force-stepping the sim" });
		this.simDt = 0.2f;
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x00170818 File Offset: 0x0016EA18
	private void Update()
	{
		if (this.isLoading)
		{
			return;
		}
		SuperluminalPerf.BeginEvent("Game.Update", null);
		float deltaTime = Time.deltaTime;
		if (global::Debug.developerConsoleVisible)
		{
			global::Debug.developerConsoleVisible = false;
		}
		if (DebugHandler.DebugCellInfo)
		{
			this.ShowDebugCellInfo();
		}
		this.gasConduitSystem.Update();
		this.liquidConduitSystem.Update();
		this.solidConduitSystem.Update();
		this.circuitManager.RenderEveryTick(deltaTime);
		this.logicCircuitManager.RenderEveryTick(deltaTime);
		this.solidConduitFlow.RenderEveryTick(deltaTime);
		Pathfinding.Instance.RenderEveryTick();
		Singleton<CellChangeMonitor>.Instance.RenderEveryTick();
		this.SimEveryTick(deltaTime);
		SuperluminalPerf.EndEvent();
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x001708C0 File Offset: 0x0016EAC0
	private void SimEveryTick(float dt)
	{
		dt = Mathf.Min(dt, 0.2f);
		this.simDt += dt;
		if (this.simDt >= 0.016666668f)
		{
			do
			{
				this.simSubTick++;
				this.simSubTick %= 12;
				if (this.simSubTick == 0)
				{
					this.hasFirstSimTickRun = true;
					this.UnsafeSim200ms(0.2f);
				}
				if (this.hasFirstSimTickRun)
				{
					Singleton<StateMachineUpdater>.Instance.AdvanceOneSimSubTick();
				}
				this.simDt -= 0.016666668f;
			}
			while (this.simDt >= 0.016666668f);
			return;
		}
		this.UnsafeSim200ms(0f);
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x0017096C File Offset: 0x0016EB6C
	private unsafe void UnsafeSim200ms(float dt)
	{
		this.simActiveRegions.Clear();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			if (worldContainer.IsDiscovered)
			{
				Game.SimActiveRegion simActiveRegion = new Game.SimActiveRegion();
				simActiveRegion.region = new Pair<Vector2I, Vector2I>(worldContainer.WorldOffset, worldContainer.WorldOffset + worldContainer.WorldSize);
				simActiveRegion.currentSunlightIntensity = worldContainer.currentSunlightIntensity;
				simActiveRegion.currentCosmicRadiationIntensity = worldContainer.currentCosmicIntensity;
				this.simActiveRegions.Add(simActiveRegion);
			}
		}
		global::Debug.Assert(this.simActiveRegions.Count > 0, "Cannot send a frame to the sim with zero active regions");
		SimMessages.NewGameFrame(dt, this.simActiveRegions);
		Sim.GameDataUpdate* ptr = this.StepTheSim(dt);
		if (ptr == null)
		{
			global::Debug.LogError("UNEXPECTED!");
			return;
		}
		if (ptr->numFramesProcessed <= 0)
		{
			return;
		}
		this.gameSolidInfo.AddRange(this.solidInfo);
		this.world.UpdateCellInfo(this.gameSolidInfo, this.callbackInfo, ptr->numSolidSubstanceChangeInfo, ptr->solidSubstanceChangeInfo, ptr->numLiquidChangeInfo, ptr->liquidChangeInfo);
		this.gameSolidInfo.Clear();
		this.solidInfo.Clear();
		this.callbackInfo.Clear();
		this.callbackManagerManuallyReleasedHandles.Clear();
		Pathfinding.Instance.UpdateNavGrids(false);
	}

	// Token: 0x0600417A RID: 16762 RVA: 0x00170AD8 File Offset: 0x0016ECD8
	private void LateUpdateComponents()
	{
		this.UpdateOverlayScreen();
	}

	// Token: 0x0600417B RID: 16763 RVA: 0x00170AE0 File Offset: 0x0016ECE0
	private void OnAddBuildingCellVisualizer(EntityCellVisualizer entity_cell_visualizer)
	{
		this.lastDrawnOverlayMode = default(HashedString);
		if (PlayerController.Instance != null)
		{
			BuildTool buildTool = PlayerController.Instance.ActiveTool as BuildTool;
			if (buildTool != null && buildTool.visualizer == entity_cell_visualizer.gameObject)
			{
				this.previewVisualizer = entity_cell_visualizer;
			}
		}
	}

	// Token: 0x0600417C RID: 16764 RVA: 0x00170B39 File Offset: 0x0016ED39
	private void OnRemoveBuildingCellVisualizer(EntityCellVisualizer entity_cell_visualizer)
	{
		if (this.previewVisualizer == entity_cell_visualizer)
		{
			this.previewVisualizer = null;
		}
	}

	// Token: 0x0600417D RID: 16765 RVA: 0x00170B50 File Offset: 0x0016ED50
	private void UpdateOverlayScreen()
	{
		if (OverlayScreen.Instance == null)
		{
			return;
		}
		HashedString mode = OverlayScreen.Instance.GetMode();
		if (this.previewVisualizer != null)
		{
			this.previewVisualizer.DrawIcons(mode);
		}
		if (mode == this.lastDrawnOverlayMode)
		{
			return;
		}
		foreach (EntityCellVisualizer entityCellVisualizer in Components.EntityCellVisualizers.Items)
		{
			entityCellVisualizer.DrawIcons(mode);
		}
		this.lastDrawnOverlayMode = mode;
	}

	// Token: 0x0600417E RID: 16766 RVA: 0x00170BF0 File Offset: 0x0016EDF0
	public void ForceOverlayUpdate(bool clearLastMode = false)
	{
		this.previousOverlayMode = OverlayModes.None.ID;
		if (clearLastMode)
		{
			this.lastDrawnOverlayMode = OverlayModes.None.ID;
		}
	}

	// Token: 0x0600417F RID: 16767 RVA: 0x00170C0C File Offset: 0x0016EE0C
	private void LateUpdate()
	{
		SuperluminalPerf.BeginEvent("Game.LateUpdate", null);
		if (this.OnSpawnComplete != null)
		{
			this.OnSpawnComplete();
			this.OnSpawnComplete = null;
		}
		if (Time.timeScale == 0f && !this.IsPaused)
		{
			this.IsPaused = true;
			base.Trigger(-1788536802, this.IsPaused);
		}
		else if (Time.timeScale != 0f && this.IsPaused)
		{
			this.IsPaused = false;
			base.Trigger(-1788536802, this.IsPaused);
		}
		if (Input.GetMouseButton(0))
		{
			this.VisualTunerElement = null;
			int mouseCell = DebugHandler.GetMouseCell();
			if (Grid.IsValidCell(mouseCell))
			{
				Element element = Grid.Element[mouseCell];
				this.VisualTunerElement = element;
			}
		}
		this.gasConduitSystem.Update();
		this.liquidConduitSystem.Update();
		this.solidConduitSystem.Update();
		HashedString mode = SimDebugView.Instance.GetMode();
		if (mode != this.previousOverlayMode)
		{
			this.previousOverlayMode = mode;
			if (mode == OverlayModes.LiquidConduits.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(true, true);
				this.gasFlowVisualizer.ColourizePipeContents(false, true);
				this.solidFlowVisualizer.ColourizePipeContents(false, true);
			}
			else if (mode == OverlayModes.GasConduits.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, true);
				this.gasFlowVisualizer.ColourizePipeContents(true, true);
				this.solidFlowVisualizer.ColourizePipeContents(false, true);
			}
			else if (mode == OverlayModes.SolidConveyor.ID)
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, true);
				this.gasFlowVisualizer.ColourizePipeContents(false, true);
				this.solidFlowVisualizer.ColourizePipeContents(true, true);
			}
			else
			{
				this.liquidFlowVisualizer.ColourizePipeContents(false, false);
				this.gasFlowVisualizer.ColourizePipeContents(false, false);
				this.solidFlowVisualizer.ColourizePipeContents(false, false);
			}
		}
		this.gasFlowVisualizer.Render(this.gasFlowPos.z, 0, this.gasConduitFlow.ContinuousLerpPercent, mode == OverlayModes.GasConduits.ID && this.gasConduitFlow.DiscreteLerpPercent != this.previousGasConduitFlowDiscreteLerpPercent);
		this.liquidFlowVisualizer.Render(this.liquidFlowPos.z, 0, this.liquidConduitFlow.ContinuousLerpPercent, mode == OverlayModes.LiquidConduits.ID && this.liquidConduitFlow.DiscreteLerpPercent != this.previousLiquidConduitFlowDiscreteLerpPercent);
		this.solidFlowVisualizer.Render(this.solidFlowPos.z, 0, this.solidConduitFlow.ContinuousLerpPercent, mode == OverlayModes.SolidConveyor.ID && this.solidConduitFlow.DiscreteLerpPercent != this.previousSolidConduitFlowDiscreteLerpPercent);
		this.previousGasConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.GasConduits.ID) ? this.gasConduitFlow.DiscreteLerpPercent : (-1f));
		this.previousLiquidConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.LiquidConduits.ID) ? this.liquidConduitFlow.DiscreteLerpPercent : (-1f));
		this.previousSolidConduitFlowDiscreteLerpPercent = ((mode == OverlayModes.SolidConveyor.ID) ? this.solidConduitFlow.DiscreteLerpPercent : (-1f));
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		Shader.SetGlobalVector("_WsToCs", new Vector4(vector.x / (float)Grid.WidthInCells, vector.y / (float)Grid.HeightInCells, (vector2.x - vector.x) / (float)Grid.WidthInCells, (vector2.y - vector.y) / (float)Grid.HeightInCells));
		WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
		Vector2I worldOffset = activeWorld.WorldOffset;
		Vector2I worldSize = activeWorld.WorldSize;
		Vector4 vector3 = new Vector4((vector.x - (float)worldOffset.x) / (float)worldSize.x, (vector.y - (float)worldOffset.y) / (float)(worldSize.y - activeWorld.HiddenYOffset), (vector2.x - vector.x) / (float)worldSize.x, (vector2.y - vector.y) / (float)(worldSize.y - activeWorld.HiddenYOffset));
		Shader.SetGlobalVector("_WsToCcs", vector3);
		if (this.drawStatusItems)
		{
			this.statusItemRenderer.RenderEveryTick();
			this.prioritizableRenderer.RenderEveryTick();
		}
		this.LateUpdateComponents();
		Singleton<StateMachineUpdater>.Instance.Render(Time.unscaledDeltaTime);
		Singleton<StateMachineUpdater>.Instance.RenderEveryTick(Time.unscaledDeltaTime);
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null)
		{
			Navigator component = SelectTool.Instance.selected.GetComponent<Navigator>();
			if (component != null)
			{
				component.DrawPath();
			}
		}
		KFMOD.RenderEveryTick(Time.deltaTime);
		SuperluminalPerf.EndEvent();
		if (GenericGameSettings.instance.performanceCapture.waitTime != 0f)
		{
			this.UpdatePerformanceCapture();
		}
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x00171120 File Offset: 0x0016F320
	private void UpdatePerformanceCapture()
	{
		if (this.IsPaused && SpeedControlScreen.Instance != null)
		{
			SpeedControlScreen.Instance.Unpause(true);
		}
		if (Time.timeSinceLevelLoad < GenericGameSettings.instance.performanceCapture.waitTime)
		{
			return;
		}
		uint num = 679336U;
		string text = global::System.DateTime.Now.ToShortDateString();
		string text2 = global::System.DateTime.Now.ToShortTimeString();
		string fileName = Path.GetFileName(GenericGameSettings.instance.performanceCapture.saveGame);
		string text3 = "Version,Date,Time,SaveGame";
		string text4 = string.Format("{0},{1},{2},{3}", new object[] { num, text, text2, fileName });
		float num2 = 0.1f;
		if (GenericGameSettings.instance.performanceCapture.gcStats)
		{
			global::Debug.Log("Begin GC profiling...");
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			GC.Collect();
			num2 = Time.realtimeSinceStartup - realtimeSinceStartup;
			global::Debug.Log("\tGC.Collect() took " + num2.ToString() + " seconds");
			MemorySnapshot memorySnapshot = new MemorySnapshot();
			string text5 = "{0},{1},{2},{3}";
			string text6 = "./memory/GCTypeMetrics.csv";
			if (!File.Exists(text6))
			{
				using (StreamWriter streamWriter = new StreamWriter(text6))
				{
					streamWriter.WriteLine(string.Format(text5, new object[] { text3, "Type", "Instances", "References" }));
				}
			}
			using (StreamWriter streamWriter2 = new StreamWriter(text6, true))
			{
				foreach (MemorySnapshot.TypeData typeData in memorySnapshot.types.Values)
				{
					streamWriter2.WriteLine(string.Format(text5, new object[]
					{
						text4,
						"\"" + typeData.type.ToString() + "\"",
						typeData.instanceCount,
						typeData.refCount
					}));
				}
			}
			global::Debug.Log("...end GC profiling");
		}
		float fps = Global.Instance.GetComponent<PerformanceMonitor>().FPS;
		Directory.CreateDirectory("./memory");
		string text7 = "{0},{1},{2}";
		string text8 = "./memory/GeneralMetrics.csv";
		if (!File.Exists(text8))
		{
			using (StreamWriter streamWriter3 = new StreamWriter(text8))
			{
				streamWriter3.WriteLine(string.Format(text7, text3, "GCDuration", "FPS"));
			}
		}
		using (StreamWriter streamWriter4 = new StreamWriter(text8, true))
		{
			streamWriter4.WriteLine(string.Format(text7, text4, num2, fps));
		}
		GenericGameSettings.instance.performanceCapture.waitTime = 0f;
		App.Quit();
	}

	// Token: 0x06004181 RID: 16769 RVA: 0x00171428 File Offset: 0x0016F628
	public void Reset(GameSpawnData gsd, Vector2I world_offset)
	{
		using (new KProfiler.Region("World.Reset", null))
		{
			if (gsd != null)
			{
				foreach (KeyValuePair<Vector2I, bool> keyValuePair in gsd.preventFoWReveal)
				{
					if (keyValuePair.Value)
					{
						Vector2I vector2I = new Vector2I(keyValuePair.Key.X + world_offset.X, keyValuePair.Key.Y + world_offset.Y);
						Grid.PreventFogOfWarReveal[Grid.PosToCell(vector2I)] = keyValuePair.Value;
					}
				}
			}
		}
	}

	// Token: 0x06004182 RID: 16770 RVA: 0x00171500 File Offset: 0x0016F700
	private void OnApplicationQuit()
	{
		Game.quitting = true;
		Sim.Shutdown();
		AudioMixer.Destroy();
		if (this.screenMgr != null && this.screenMgr.gameObject != null)
		{
			global::UnityEngine.Object.Destroy(this.screenMgr.gameObject);
		}
		Console.WriteLine("Game.OnApplicationQuit()");
	}

	// Token: 0x06004183 RID: 16771 RVA: 0x00171558 File Offset: 0x0016F758
	private void InitializeFXSpawners()
	{
		for (int i = 0; i < this.fxSpawnData.Length; i++)
		{
			int fx_idx = i;
			this.fxSpawnData[fx_idx].fxPrefab.SetActive(false);
			ushort fx_mask = (ushort)(1 << fx_idx);
			Action<SpawnFXHashes, GameObject> destroyer = delegate(SpawnFXHashes fxid, GameObject go)
			{
				if (!Game.IsQuitting())
				{
					int num = Grid.PosToCell(go);
					ushort[] array = this.activeFX;
					int num2 = num;
					array[num2] &= ~fx_mask;
					go.GetComponent<KAnimControllerBase>().enabled = false;
					this.fxPools[(int)fxid].ReleaseInstance(go);
				}
			};
			Func<GameObject> func = delegate
			{
				GameObject gameObject = GameUtil.KInstantiate(this.fxSpawnData[fx_idx].fxPrefab, Grid.SceneLayer.Front, null, 0);
				KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
				component.enabled = false;
				gameObject.SetActive(true);
				component.onDestroySelf = delegate(GameObject go)
				{
					destroyer(this.fxSpawnData[fx_idx].id, go);
				};
				return gameObject;
			};
			GameObjectPool pool = new GameObjectPool(func, this.fxSpawnData[fx_idx].initialCount);
			this.fxPools[(int)this.fxSpawnData[fx_idx].id] = pool;
			this.fxSpawner[(int)this.fxSpawnData[fx_idx].id] = delegate(Vector3 pos, float rotation)
			{
				Action<object> action = delegate(object obj)
				{
					int num3 = Grid.PosToCell(pos);
					if ((this.activeFX[num3] & fx_mask) == 0)
					{
						ushort[] array2 = this.activeFX;
						int num4 = num3;
						array2[num4] |= fx_mask;
						GameObject instance = pool.GetInstance();
						Game.SpawnPoolData spawnPoolData = this.fxSpawnData[fx_idx];
						Quaternion quaternion = Quaternion.identity;
						bool flag = false;
						string text = spawnPoolData.initialAnim;
						Game.SpawnRotationConfig rotationConfig = spawnPoolData.rotationConfig;
						if (rotationConfig != Game.SpawnRotationConfig.Normal)
						{
							if (rotationConfig == Game.SpawnRotationConfig.StringName)
							{
								int num5 = (int)(rotation / 90f);
								if (num5 < 0)
								{
									num5 += spawnPoolData.rotationData.Length;
								}
								text = spawnPoolData.rotationData[num5].animName;
								flag = spawnPoolData.rotationData[num5].flip;
							}
						}
						else
						{
							quaternion = Quaternion.Euler(0f, 0f, rotation);
						}
						pos += spawnPoolData.spawnOffset;
						Vector2 vector = global::UnityEngine.Random.insideUnitCircle;
						vector.x *= spawnPoolData.spawnRandomOffset.x;
						vector.y *= spawnPoolData.spawnRandomOffset.y;
						vector = quaternion * vector;
						pos.x += vector.x;
						pos.y += vector.y;
						instance.transform.SetPosition(pos);
						instance.transform.rotation = quaternion;
						KBatchedAnimController component2 = instance.GetComponent<KBatchedAnimController>();
						component2.FlipX = flag;
						component2.TintColour = spawnPoolData.colour;
						component2.Play(text, KAnim.PlayMode.Once, 1f, 0f);
						component2.enabled = true;
					}
				};
				if (Game.Instance.IsPaused)
				{
					action(null);
					return;
				}
				GameScheduler.Instance.Schedule("SpawnFX", 0f, action, null, null);
			};
		}
	}

	// Token: 0x06004184 RID: 16772 RVA: 0x00171658 File Offset: 0x0016F858
	public void SpawnFX(SpawnFXHashes fx_id, int cell, float rotation)
	{
		Vector3 vector = Grid.CellToPosCBC(cell, Grid.SceneLayer.Front);
		if (CameraController.Instance.IsVisiblePos(vector))
		{
			this.fxSpawner[(int)fx_id](vector, rotation);
		}
	}

	// Token: 0x06004185 RID: 16773 RVA: 0x0017168E File Offset: 0x0016F88E
	public void SpawnFX(SpawnFXHashes fx_id, Vector3 pos, float rotation)
	{
		this.fxSpawner[(int)fx_id](pos, rotation);
	}

	// Token: 0x06004186 RID: 16774 RVA: 0x001716A3 File Offset: 0x0016F8A3
	public static void SaveSettings(BinaryWriter writer)
	{
		Serializer.Serialize(new Game.Settings(Game.Instance), writer);
	}

	// Token: 0x06004187 RID: 16775 RVA: 0x001716B8 File Offset: 0x0016F8B8
	public static void LoadSettings(Deserializer deserializer)
	{
		Game.Settings settings = new Game.Settings();
		deserializer.Deserialize(settings);
		KPlayerPrefs.SetInt(Game.NextUniqueIDKey, settings.nextUniqueID);
		KleiMetrics.SetGameID(settings.gameID);
	}

	// Token: 0x06004188 RID: 16776 RVA: 0x001716F0 File Offset: 0x0016F8F0
	public void Save(BinaryWriter writer)
	{
		Game.GameSaveData gameSaveData = new Game.GameSaveData();
		gameSaveData.gasConduitFlow = this.gasConduitFlow;
		gameSaveData.liquidConduitFlow = this.liquidConduitFlow;
		gameSaveData.fallingWater = this.world.GetComponent<FallingWater>();
		gameSaveData.unstableGround = this.world.GetComponent<UnstableGroundManager>();
		gameSaveData.worldDetail = SaveLoader.Instance.clusterDetailSave;
		gameSaveData.debugWasUsed = this.debugWasUsed;
		gameSaveData.customGameSettings = CustomGameSettings.Instance;
		gameSaveData.storySetings = StoryManager.Instance;
		gameSaveData.spaceScannerNetworkManager = Game.Instance.spaceScannerNetworkManager;
		gameSaveData.autoPrioritizeRoles = this.autoPrioritizeRoles;
		gameSaveData.advancedPersonalPriorities = this.advancedPersonalPriorities;
		gameSaveData.savedInfo = this.savedInfo;
		global::Debug.Assert(gameSaveData.worldDetail != null, "World detail null");
		gameSaveData.dateGenerated = this.dateGenerated;
		if (!this.changelistsPlayedOn.Contains(679336U))
		{
			this.changelistsPlayedOn.Add(679336U);
		}
		gameSaveData.changelistsPlayedOn = this.changelistsPlayedOn;
		if (this.OnSave != null)
		{
			this.OnSave(gameSaveData);
		}
		Serializer.Serialize(gameSaveData, writer);
	}

	// Token: 0x06004189 RID: 16777 RVA: 0x0017180C File Offset: 0x0016FA0C
	public void Load(Deserializer deserializer)
	{
		Game.GameSaveData gameSaveData = new Game.GameSaveData();
		gameSaveData.gasConduitFlow = this.gasConduitFlow;
		gameSaveData.liquidConduitFlow = this.liquidConduitFlow;
		gameSaveData.fallingWater = this.world.GetComponent<FallingWater>();
		gameSaveData.unstableGround = this.world.GetComponent<UnstableGroundManager>();
		gameSaveData.worldDetail = new WorldDetailSave();
		gameSaveData.customGameSettings = CustomGameSettings.Instance;
		gameSaveData.storySetings = StoryManager.Instance;
		gameSaveData.spaceScannerNetworkManager = Game.Instance.spaceScannerNetworkManager;
		deserializer.Deserialize(gameSaveData);
		this.gasConduitFlow = gameSaveData.gasConduitFlow;
		this.liquidConduitFlow = gameSaveData.liquidConduitFlow;
		this.debugWasUsed = gameSaveData.debugWasUsed;
		this.autoPrioritizeRoles = gameSaveData.autoPrioritizeRoles;
		this.advancedPersonalPriorities = gameSaveData.advancedPersonalPriorities;
		this.dateGenerated = gameSaveData.dateGenerated;
		this.changelistsPlayedOn = gameSaveData.changelistsPlayedOn ?? new List<uint>();
		if (gameSaveData.dateGenerated.IsNullOrWhiteSpace())
		{
			this.dateGenerated = "Before U41 (Feb 2022)";
		}
		DebugUtil.LogArgs(new object[] { "SAVEINFO" });
		DebugUtil.LogArgs(new object[] { " - Generated: " + this.dateGenerated });
		DebugUtil.LogArgs(new object[] { " - Played on: " + string.Join<uint>(", ", this.changelistsPlayedOn) });
		DebugUtil.LogArgs(new object[] { " - Debug was used: " + Game.Instance.debugWasUsed.ToString() });
		this.savedInfo = gameSaveData.savedInfo;
		this.savedInfo.InitializeEmptyVariables();
		CustomGameSettings.Instance.Print();
		KCrashReporter.debugWasUsed = this.debugWasUsed;
		SaveLoader.Instance.SetWorldDetail(gameSaveData.worldDetail);
		if (this.OnLoad != null)
		{
			this.OnLoad(gameSaveData);
		}
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x001719D7 File Offset: 0x0016FBD7
	public void SetAutoSaveCallbacks(Game.SavingPreCB activatePreCB, Game.SavingActiveCB activateActiveCB, Game.SavingPostCB activatePostCB)
	{
		this.activatePreCB = activatePreCB;
		this.activateActiveCB = activateActiveCB;
		this.activatePostCB = activatePostCB;
	}

	// Token: 0x0600418B RID: 16779 RVA: 0x001719EE File Offset: 0x0016FBEE
	public void StartDelayedInitialSave()
	{
		base.StartCoroutine(this.DelayedInitialSave());
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x001719FD File Offset: 0x0016FBFD
	private IEnumerator DelayedInitialSave()
	{
		int num;
		for (int i = 0; i < 1; i = num)
		{
			yield return null;
			num = i + 1;
		}
		if (GenericGameSettings.instance.devAutoWorldGenActive)
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				worldContainer.SetDiscovered(true);
			}
			SaveGame.Instance.worldGenSpawner.SpawnEverything();
			SaveGame.Instance.GetSMI<ClusterFogOfWarManager.Instance>().DEBUG_REVEAL_ENTIRE_MAP();
			if (CameraController.Instance != null)
			{
				CameraController.Instance.EnableFreeCamera(true);
			}
			for (int num2 = 0; num2 != Grid.WidthInCells * Grid.HeightInCells; num2++)
			{
				Grid.Reveal(num2, byte.MaxValue, false);
			}
			GenericGameSettings.instance.devAutoWorldGenActive = false;
		}
		SaveLoader.Instance.InitialSave();
		yield break;
	}

	// Token: 0x0600418D RID: 16781 RVA: 0x00171A08 File Offset: 0x0016FC08
	public void StartDelayedSave(string filename, bool isAutoSave = false, bool updateSavePointer = true)
	{
		if (this.activatePreCB != null)
		{
			this.activatePreCB(delegate
			{
				this.StartCoroutine(this.DelayedSave(filename, isAutoSave, updateSavePointer));
			});
			return;
		}
		base.StartCoroutine(this.DelayedSave(filename, isAutoSave, updateSavePointer));
	}

	// Token: 0x0600418E RID: 16782 RVA: 0x00171A76 File Offset: 0x0016FC76
	private IEnumerator DelayedSave(string filename, bool isAutoSave, bool updateSavePointer)
	{
		while (PlayerController.Instance.IsDragging())
		{
			yield return null;
		}
		PlayerController.Instance.CancelDragging();
		PlayerController.Instance.AllowDragging(false);
		int num;
		for (int i = 0; i < 1; i = num)
		{
			yield return null;
			num = i + 1;
		}
		if (this.activateActiveCB != null)
		{
			this.activateActiveCB();
			for (int i = 0; i < 1; i = num)
			{
				yield return null;
				num = i + 1;
			}
		}
		SaveLoader.Instance.Save(filename, isAutoSave, updateSavePointer);
		if (this.activatePostCB != null)
		{
			this.activatePostCB();
		}
		for (int i = 0; i < 5; i = num)
		{
			yield return null;
			num = i + 1;
		}
		PlayerController.Instance.AllowDragging(true);
		yield break;
	}

	// Token: 0x0600418F RID: 16783 RVA: 0x00171A9A File Offset: 0x0016FC9A
	public void StartDelayed(int tick_delay, global::System.Action action)
	{
		base.StartCoroutine(this.DelayedExecutor(tick_delay, action));
	}

	// Token: 0x06004190 RID: 16784 RVA: 0x00171AAB File Offset: 0x0016FCAB
	private IEnumerator DelayedExecutor(int tick_delay, global::System.Action action)
	{
		int num;
		for (int i = 0; i < tick_delay; i = num)
		{
			yield return null;
			num = i + 1;
		}
		action();
		yield break;
	}

	// Token: 0x06004191 RID: 16785 RVA: 0x00171AC4 File Offset: 0x0016FCC4
	private void LoadEventHashes()
	{
		foreach (object obj in Enum.GetValues(typeof(GameHashes)))
		{
			GameHashes gameHashes = (GameHashes)obj;
			HashCache.Get().Add((int)gameHashes, gameHashes.ToString());
		}
		foreach (object obj2 in Enum.GetValues(typeof(UtilHashes)))
		{
			UtilHashes utilHashes = (UtilHashes)obj2;
			HashCache.Get().Add((int)utilHashes, utilHashes.ToString());
		}
		foreach (object obj3 in Enum.GetValues(typeof(UIHashes)))
		{
			UIHashes uihashes = (UIHashes)obj3;
			HashCache.Get().Add((int)uihashes, uihashes.ToString());
		}
	}

	// Token: 0x06004192 RID: 16786 RVA: 0x00171C00 File Offset: 0x0016FE00
	public void StopFE()
	{
		if (SteamUGCService.Instance)
		{
			SteamUGCService.Instance.enabled = false;
		}
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndSnapshot, STOP_MODE.ALLOWFADEOUT);
		if (MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MusicManager.instance.StopSong("Music_FrontEnd", true, STOP_MODE.ALLOWFADEOUT);
		}
		MainMenu.Instance.StopMainMenuMusic();
	}

	// Token: 0x06004193 RID: 16787 RVA: 0x00171C66 File Offset: 0x0016FE66
	public void StartBE()
	{
		Resources.UnloadUnusedAssets();
		AudioMixer.instance.Reset();
		AudioMixer.instance.StartPersistentSnapshots();
		MusicManager.instance.ConfigureSongs();
		if (MusicManager.instance.ShouldPlayDynamicMusicLoadedGame())
		{
			MusicManager.instance.PlayDynamicMusic();
		}
	}

	// Token: 0x06004194 RID: 16788 RVA: 0x00171CA4 File Offset: 0x0016FEA4
	public void StopBE()
	{
		if (SteamUGCService.Instance)
		{
			SteamUGCService.Instance.enabled = true;
		}
		LoopingSoundManager loopingSoundManager = LoopingSoundManager.Get();
		if (loopingSoundManager != null)
		{
			loopingSoundManager.StopAllSounds();
		}
		MusicManager.instance.KillAllSongs(STOP_MODE.ALLOWFADEOUT);
		AudioMixer.instance.StopPersistentSnapshots();
		foreach (List<SaveLoadRoot> list in SaveLoader.Instance.saveManager.GetLists().Values)
		{
			foreach (SaveLoadRoot saveLoadRoot in list)
			{
				if (saveLoadRoot.gameObject != null)
				{
					global::Util.KDestroyGameObject(saveLoadRoot.gameObject);
				}
			}
		}
		base.GetComponent<EntombedItemVisualizer>().Clear();
		SimTemperatureTransfer.ClearInstanceMap();
		StructureTemperatureComponents.ClearInstanceMap();
		ElementConsumer.ClearInstanceMap();
		KComponentSpawn.instance.comps.Clear();
		KInputHandler.Remove(Global.GetInputManager().GetDefaultController(), this.cameraController);
		KInputHandler.Remove(Global.GetInputManager().GetDefaultController(), this.playerController);
		Sim.Shutdown();
		SimAndRenderScheduler.instance.Reset();
		Resources.UnloadUnusedAssets();
	}

	// Token: 0x06004195 RID: 16789 RVA: 0x00171DF4 File Offset: 0x0016FFF4
	public void SetStatusItemOffset(Transform transform, Vector3 offset)
	{
		this.statusItemRenderer.SetOffset(transform, offset);
	}

	// Token: 0x06004196 RID: 16790 RVA: 0x00171E03 File Offset: 0x00170003
	public void AddStatusItem(Transform transform, StatusItem status_item)
	{
		this.statusItemRenderer.Add(transform, status_item);
	}

	// Token: 0x06004197 RID: 16791 RVA: 0x00171E12 File Offset: 0x00170012
	public void RemoveStatusItem(Transform transform, StatusItem status_item)
	{
		this.statusItemRenderer.Remove(transform, status_item);
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x06004198 RID: 16792 RVA: 0x00171E21 File Offset: 0x00170021
	public float LastTimeWorkStarted
	{
		get
		{
			return this.lastTimeWorkStarted;
		}
	}

	// Token: 0x06004199 RID: 16793 RVA: 0x00171E29 File Offset: 0x00170029
	public void StartedWork()
	{
		this.lastTimeWorkStarted = Time.time;
	}

	// Token: 0x0600419A RID: 16794 RVA: 0x00171E36 File Offset: 0x00170036
	private void SpawnOxygenBubbles(Vector3 position, float angle)
	{
	}

	// Token: 0x0600419B RID: 16795 RVA: 0x00171E38 File Offset: 0x00170038
	public void ManualReleaseHandle(HandleVector<Game.CallbackInfo>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return;
		}
		this.callbackManagerManuallyReleasedHandles.Add(handle.index);
		this.callbackManager.Release(handle);
	}

	// Token: 0x0600419C RID: 16796 RVA: 0x00171E63 File Offset: 0x00170063
	private bool IsManuallyReleasedHandle(HandleVector<Game.CallbackInfo>.Handle handle)
	{
		return !this.callbackManager.IsVersionValid(handle) && this.callbackManagerManuallyReleasedHandles.Contains(handle.index);
	}

	// Token: 0x0600419D RID: 16797 RVA: 0x00171E8A File Offset: 0x0017008A
	[ContextMenu("Print")]
	private void Print()
	{
		Console.WriteLine("This is a console writeline test");
		global::Debug.Log("This is a debug log test");
	}

	// Token: 0x0600419E RID: 16798 RVA: 0x00171EA0 File Offset: 0x001700A0
	private void DestroyInstances()
	{
		KMonoBehaviour.lastGameObject = null;
		KMonoBehaviour.lastObj = null;
		Db.Get().ResetProblematicDbs();
		GridSettings.ClearGrid();
		StateMachineManager.ResetParameters();
		ChoreTable.Instance.ResetParameters();
		BubbleManager.DestroyInstance();
		AmbientSoundManager.Destroy();
		AutoDisinfectableManager.DestroyInstance();
		BuildMenu.DestroyInstance();
		CancelTool.DestroyInstance();
		ClearTool.DestroyInstance();
		ChoreGroupManager.DestroyInstance();
		CO2Manager.DestroyInstance();
		ConsumerManager.DestroyInstance();
		CopySettingsTool.DestroyInstance();
		global::DateTime.DestroyInstance();
		DebugBaseTemplateButton.DestroyInstance();
		DebugPaintElementScreen.DestroyInstance();
		DetailsScreen.DestroyInstance();
		DietManager.DestroyInstance();
		DebugText.DestroyInstance();
		FactionManager.DestroyInstance();
		EmptyPipeTool.DestroyInstance();
		FetchListStatusItemUpdater.DestroyInstance();
		FishOvercrowingManager.DestroyInstance();
		FallingWater.DestroyInstance();
		GridCompositor.DestroyInstance();
		Infrared.DestroyInstance();
		KPrefabIDTracker.DestroyInstance();
		ManagementMenu.DestroyInstance();
		ClusterMapScreen.DestroyInstance();
		Messenger.DestroyInstance();
		LoopingSoundManager.DestroyInstance();
		MeterScreen.DestroyInstance();
		MinionGroupProber.DestroyInstance();
		NavPathDrawer.DestroyInstance();
		MinionIdentity.DestroyStatics();
		PathFinder.DestroyStatics();
		Pathfinding.DestroyInstance();
		PrebuildTool.DestroyInstance();
		PrioritizeTool.DestroyInstance();
		SelectTool.DestroyInstance();
		PopFXManager.DestroyInstance();
		ProgressBarsConfig.DestroyInstance();
		PropertyTextures.DestroyInstance();
		WorldResourceAmountTracker<RationTracker>.DestroyInstance();
		WorldResourceAmountTracker<ElectrobankTracker>.DestroyInstance();
		ReportManager.DestroyInstance();
		Research.DestroyInstance();
		RootMenu.DestroyInstance();
		SaveLoader.DestroyInstance();
		Scenario.DestroyInstance();
		SimDebugView.DestroyInstance();
		SpriteSheetAnimManager.DestroyInstance();
		ScheduleManager.DestroyInstance();
		Sounds.DestroyInstance();
		ToolMenu.DestroyInstance();
		WorldDamage.DestroyInstance();
		WaterCubes.DestroyInstance();
		WireBuildTool.DestroyInstance();
		VisibilityTester.DestroyInstance();
		Traces.DestroyInstance();
		TopLeftControlScreen.DestroyInstance();
		UtilityBuildTool.DestroyInstance();
		ReportScreen.DestroyInstance();
		ChorePreconditions.DestroyInstance();
		SandboxBrushTool.DestroyInstance();
		SandboxHeatTool.DestroyInstance();
		SandboxStressTool.DestroyInstance();
		SandboxCritterTool.DestroyInstance();
		SandboxClearFloorTool.DestroyInstance();
		GameScreenManager.DestroyInstance();
		GameScheduler.DestroyInstance();
		NavigationReservations.DestroyInstance();
		Tutorial.DestroyInstance();
		CameraController.DestroyInstance();
		CellEventLogger.DestroyInstance();
		GameFlowManager.DestroyInstance();
		Immigration.DestroyInstance();
		BuildTool.DestroyInstance();
		DebugTool.DestroyInstance();
		DeconstructTool.DestroyInstance();
		DisconnectTool.DestroyInstance();
		DigTool.DestroyInstance();
		DisinfectTool.DestroyInstance();
		HarvestTool.DestroyInstance();
		MopTool.DestroyInstance();
		MoveToLocationTool.DestroyInstance();
		PlaceTool.DestroyInstance();
		SpacecraftManager.DestroyInstance();
		GameplayEventManager.DestroyInstance();
		BuildingInventory.DestroyInstance();
		PlantSubSpeciesCatalog.DestroyInstance();
		SandboxDestroyerTool.DestroyInstance();
		SandboxFOWTool.DestroyInstance();
		SandboxFloodTool.DestroyInstance();
		SandboxSprinkleTool.DestroyInstance();
		StampTool.DestroyInstance();
		OnDemandUpdater.DestroyInstance();
		HoverTextScreen.DestroyInstance();
		ImmigrantScreen.DestroyInstance();
		OverlayMenu.DestroyInstance();
		NameDisplayScreen.DestroyInstance();
		PlanScreen.DestroyInstance();
		ResourceCategoryScreen.DestroyInstance();
		ResourceRemainingDisplayScreen.DestroyInstance();
		SandboxToolParameterMenu.DestroyInstance();
		SpeedControlScreen.DestroyInstance();
		Vignette.DestroyInstance();
		PlayerController.DestroyInstance();
		NotificationScreen.DestroyInstance();
		NotificationScreen_TemporaryActions.DestroyInstance();
		BuildingCellVisualizerResources.DestroyInstance();
		PauseScreen.DestroyInstance();
		SaveLoadRoot.DestroyStatics();
		KTime.DestroyInstance();
		DemoTimer.DestroyInstance();
		UIScheduler.DestroyInstance();
		SaveGame.DestroyInstance();
		GameClock.DestroyInstance();
		TimeOfDay.DestroyInstance();
		DeserializeWarnings.DestroyInstance();
		UISounds.DestroyInstance();
		RenderTextureDestroyer.DestroyInstance();
		HoverTextHelper.DestroyStatics();
		LoadScreen.DestroyInstance();
		LoadingOverlay.DestroyInstance();
		SimAndRenderScheduler.DestroyInstance();
		Singleton<CellChangeMonitor>.DestroyInstance();
		Singleton<StateMachineManager>.Instance.Clear();
		Singleton<StateMachineUpdater>.Instance.Clear();
		UpdateObjectCountParameter.Clear();
		MaterialSelectionPanel.ClearStatics();
		StarmapScreen.DestroyInstance();
		ClusterNameDisplayScreen.DestroyInstance();
		ClusterManager.DestroyInstance();
		ClusterGrid.DestroyInstance();
		PathFinderQueries.Reset();
		KBatchedAnimUpdater instance = Singleton<KBatchedAnimUpdater>.Instance;
		if (instance != null)
		{
			instance.InitializeGrid();
		}
		GlobalChoreProvider.DestroyInstance();
		WorldSelector.DestroyInstance();
		ColonyDiagnosticUtility.DestroyInstance();
		DiscoveredResources.DestroyInstance();
		ClusterMapSelectTool.DestroyInstance();
		StoryManager.DestroyInstance();
		AnimEventHandlerManager.DestroyInstance();
		Game.Instance = null;
		Game.BrainScheduler = null;
		Grid.OnReveal = null;
		this.VisualTunerElement = null;
		Assets.ClearOnAddPrefab();
		KMonoBehaviour.lastGameObject = null;
		KMonoBehaviour.lastObj = null;
		(KComponentSpawn.instance.comps as GameComps).Clear();
	}

	// Token: 0x0600419F RID: 16799 RVA: 0x001721E8 File Offset: 0x001703E8
	public static bool IsDlcActiveForCurrentSave(string dlcId)
	{
		if (Game.Instance == null)
		{
			DebugUtil.DevLogError("Game.IsDlcActiveForCurrentSave can only be called when the game is running");
			return false;
		}
		return dlcId == "" || dlcId == null || SaveLoader.Instance.GameInfo.dlcIds.Contains(dlcId);
	}

	// Token: 0x060041A0 RID: 16800 RVA: 0x00172235 File Offset: 0x00170435
	public static bool IsCorrectDlcActiveForCurrentSave(IHasDlcRestrictions restrictions)
	{
		if (Game.Instance == null)
		{
			DebugUtil.DevLogError("Game.IsCorrectDlcActiveForCurrentSave can only be called when the game is running");
			return false;
		}
		return Game.IsAllDlcActiveForCurrentSave(restrictions.GetRequiredDlcIds()) && !Game.IsAnyDlcActiveForCurrentSave(restrictions.GetForbiddenDlcIds());
	}

	// Token: 0x060041A1 RID: 16801 RVA: 0x00172270 File Offset: 0x00170470
	private static bool IsAllDlcActiveForCurrentSave(string[] dlcIds)
	{
		if (dlcIds == null || dlcIds.Length == 0)
		{
			return true;
		}
		foreach (string text in dlcIds)
		{
			if (!(text == "") && !Game.IsDlcActiveForCurrentSave(text))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060041A2 RID: 16802 RVA: 0x001722B4 File Offset: 0x001704B4
	private static bool IsAnyDlcActiveForCurrentSave(string[] dlcIds)
	{
		if (dlcIds == null || dlcIds.Length == 0)
		{
			return false;
		}
		foreach (string text in dlcIds)
		{
			if (!(text == "") && Game.IsDlcActiveForCurrentSave(text))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040028D4 RID: 10452
	private static readonly Thread MainThread = Thread.CurrentThread;

	// Token: 0x040028D5 RID: 10453
	private static readonly string NextUniqueIDKey = "NextUniqueID";

	// Token: 0x040028D6 RID: 10454
	public static string clusterId = null;

	// Token: 0x040028D7 RID: 10455
	private PlayerController playerController;

	// Token: 0x040028D8 RID: 10456
	private CameraController cameraController;

	// Token: 0x040028D9 RID: 10457
	public Action<Game.GameSaveData> OnSave;

	// Token: 0x040028DA RID: 10458
	public Action<Game.GameSaveData> OnLoad;

	// Token: 0x040028DB RID: 10459
	public global::System.Action OnSpawnComplete;

	// Token: 0x040028DC RID: 10460
	[NonSerialized]
	public bool baseAlreadyCreated;

	// Token: 0x040028DD RID: 10461
	[NonSerialized]
	public bool autoPrioritizeRoles;

	// Token: 0x040028DE RID: 10462
	[NonSerialized]
	public bool advancedPersonalPriorities;

	// Token: 0x040028DF RID: 10463
	public Game.SavedInfo savedInfo;

	// Token: 0x040028E0 RID: 10464
	public static bool quitting = false;

	// Token: 0x040028E2 RID: 10466
	public AssignmentManager assignmentManager;

	// Token: 0x040028E3 RID: 10467
	public GameObject playerPrefab;

	// Token: 0x040028E4 RID: 10468
	public GameObject screenManagerPrefab;

	// Token: 0x040028E5 RID: 10469
	public GameObject cameraControllerPrefab;

	// Token: 0x040028E7 RID: 10471
	private static Camera m_CachedCamera = null;

	// Token: 0x040028E8 RID: 10472
	public GameObject tempIntroScreenPrefab;

	// Token: 0x040028E9 RID: 10473
	public static int BlockSelectionLayerMask;

	// Token: 0x040028EA RID: 10474
	public static int PickupableLayer;

	// Token: 0x040028EB RID: 10475
	public static BrainScheduler BrainScheduler;

	// Token: 0x040028EC RID: 10476
	public Element VisualTunerElement;

	// Token: 0x040028ED RID: 10477
	public float currentFallbackSunlightIntensity;

	// Token: 0x040028EE RID: 10478
	public RoomProber roomProber;

	// Token: 0x040028EF RID: 10479
	public SpaceScannerNetworkManager spaceScannerNetworkManager;

	// Token: 0x040028F0 RID: 10480
	public FetchManager fetchManager;

	// Token: 0x040028F1 RID: 10481
	public EdiblesManager ediblesManager;

	// Token: 0x040028F2 RID: 10482
	public SpacecraftManager spacecraftManager;

	// Token: 0x040028F3 RID: 10483
	public UserMenu userMenu;

	// Token: 0x040028F4 RID: 10484
	public Unlocks unlocks;

	// Token: 0x040028F5 RID: 10485
	public Timelapser timelapser;

	// Token: 0x040028F6 RID: 10486
	private bool sandboxModeActive;

	// Token: 0x040028F7 RID: 10487
	public HandleVector<Game.CallbackInfo> callbackManager = new HandleVector<Game.CallbackInfo>(256);

	// Token: 0x040028F8 RID: 10488
	public List<int> callbackManagerManuallyReleasedHandles = new List<int>();

	// Token: 0x040028F9 RID: 10489
	public Game.ComplexCallbackHandleVector<int> simComponentCallbackManager = new Game.ComplexCallbackHandleVector<int>(256);

	// Token: 0x040028FA RID: 10490
	public Game.ComplexCallbackHandleVector<Sim.MassConsumedCallback> massConsumedCallbackManager = new Game.ComplexCallbackHandleVector<Sim.MassConsumedCallback>(64);

	// Token: 0x040028FB RID: 10491
	public Game.ComplexCallbackHandleVector<Sim.MassEmittedCallback> massEmitCallbackManager = new Game.ComplexCallbackHandleVector<Sim.MassEmittedCallback>(64);

	// Token: 0x040028FC RID: 10492
	public Game.ComplexCallbackHandleVector<Sim.DiseaseConsumptionCallback> diseaseConsumptionCallbackManager = new Game.ComplexCallbackHandleVector<Sim.DiseaseConsumptionCallback>(64);

	// Token: 0x040028FD RID: 10493
	public Game.ComplexCallbackHandleVector<Sim.ConsumedRadiationCallback> radiationConsumedCallbackManager = new Game.ComplexCallbackHandleVector<Sim.ConsumedRadiationCallback>(256);

	// Token: 0x040028FE RID: 10494
	[NonSerialized]
	public Player LocalPlayer;

	// Token: 0x040028FF RID: 10495
	[SerializeField]
	public TextAsset maleNamesFile;

	// Token: 0x04002900 RID: 10496
	[SerializeField]
	public TextAsset femaleNamesFile;

	// Token: 0x04002901 RID: 10497
	[NonSerialized]
	public World world;

	// Token: 0x04002902 RID: 10498
	[NonSerialized]
	public CircuitManager circuitManager;

	// Token: 0x04002903 RID: 10499
	[NonSerialized]
	public EnergySim energySim;

	// Token: 0x04002904 RID: 10500
	[NonSerialized]
	public LogicCircuitManager logicCircuitManager;

	// Token: 0x04002905 RID: 10501
	private GameScreenManager screenMgr;

	// Token: 0x04002906 RID: 10502
	public UtilityNetworkManager<FlowUtilityNetwork, Vent> gasConduitSystem;

	// Token: 0x04002907 RID: 10503
	public UtilityNetworkManager<FlowUtilityNetwork, Vent> liquidConduitSystem;

	// Token: 0x04002908 RID: 10504
	public UtilityNetworkManager<ElectricalUtilityNetwork, Wire> electricalConduitSystem;

	// Token: 0x04002909 RID: 10505
	public UtilityNetworkManager<LogicCircuitNetwork, LogicWire> logicCircuitSystem;

	// Token: 0x0400290A RID: 10506
	public UtilityNetworkTubesManager travelTubeSystem;

	// Token: 0x0400290B RID: 10507
	public UtilityNetworkManager<FlowUtilityNetwork, SolidConduit> solidConduitSystem;

	// Token: 0x0400290C RID: 10508
	public ConduitFlow gasConduitFlow;

	// Token: 0x0400290D RID: 10509
	public ConduitFlow liquidConduitFlow;

	// Token: 0x0400290E RID: 10510
	public SolidConduitFlow solidConduitFlow;

	// Token: 0x0400290F RID: 10511
	public Accumulators accumulators;

	// Token: 0x04002910 RID: 10512
	public PlantElementAbsorbers plantElementAbsorbers;

	// Token: 0x04002911 RID: 10513
	public Game.TemperatureOverlayModes temperatureOverlayMode;

	// Token: 0x04002912 RID: 10514
	public bool showExpandedTemperatures;

	// Token: 0x04002913 RID: 10515
	public List<Tag> tileOverlayFilters = new List<Tag>();

	// Token: 0x04002914 RID: 10516
	public bool showGasConduitDisease;

	// Token: 0x04002915 RID: 10517
	public bool showLiquidConduitDisease;

	// Token: 0x04002916 RID: 10518
	public ConduitFlowVisualizer gasFlowVisualizer;

	// Token: 0x04002917 RID: 10519
	public ConduitFlowVisualizer liquidFlowVisualizer;

	// Token: 0x04002918 RID: 10520
	public SolidConduitFlowVisualizer solidFlowVisualizer;

	// Token: 0x04002919 RID: 10521
	public ConduitTemperatureManager conduitTemperatureManager;

	// Token: 0x0400291A RID: 10522
	public ConduitDiseaseManager conduitDiseaseManager;

	// Token: 0x0400291B RID: 10523
	public MingleCellTracker mingleCellTracker;

	// Token: 0x0400291C RID: 10524
	private int simSubTick;

	// Token: 0x0400291D RID: 10525
	private bool hasFirstSimTickRun;

	// Token: 0x0400291E RID: 10526
	private float simDt;

	// Token: 0x0400291F RID: 10527
	public string dateGenerated;

	// Token: 0x04002920 RID: 10528
	public List<uint> changelistsPlayedOn;

	// Token: 0x04002921 RID: 10529
	[SerializeField]
	public Game.ConduitVisInfo liquidConduitVisInfo;

	// Token: 0x04002922 RID: 10530
	[SerializeField]
	public Game.ConduitVisInfo gasConduitVisInfo;

	// Token: 0x04002923 RID: 10531
	[SerializeField]
	public Game.ConduitVisInfo solidConduitVisInfo;

	// Token: 0x04002924 RID: 10532
	[SerializeField]
	private Material liquidFlowMaterial;

	// Token: 0x04002925 RID: 10533
	[SerializeField]
	private Material gasFlowMaterial;

	// Token: 0x04002926 RID: 10534
	[SerializeField]
	private Color flowColour;

	// Token: 0x04002927 RID: 10535
	private Vector3 gasFlowPos;

	// Token: 0x04002928 RID: 10536
	private Vector3 liquidFlowPos;

	// Token: 0x04002929 RID: 10537
	private Vector3 solidFlowPos;

	// Token: 0x0400292A RID: 10538
	public bool drawStatusItems = true;

	// Token: 0x0400292B RID: 10539
	private List<SolidInfo> solidInfo = new List<SolidInfo>();

	// Token: 0x0400292C RID: 10540
	private List<global::Klei.CallbackInfo> callbackInfo = new List<global::Klei.CallbackInfo>();

	// Token: 0x0400292D RID: 10541
	private List<SolidInfo> gameSolidInfo = new List<SolidInfo>();

	// Token: 0x0400292E RID: 10542
	private bool IsPaused;

	// Token: 0x0400292F RID: 10543
	private HashSet<int> solidChangedFilter = new HashSet<int>();

	// Token: 0x04002930 RID: 10544
	private HashedString lastDrawnOverlayMode;

	// Token: 0x04002931 RID: 10545
	private EntityCellVisualizer previewVisualizer;

	// Token: 0x04002934 RID: 10548
	public SafetyConditions safetyConditions = new SafetyConditions();

	// Token: 0x04002935 RID: 10549
	public SimData simData = new SimData();

	// Token: 0x04002936 RID: 10550
	[MyCmpGet]
	private GameScenePartitioner gameScenePartitioner;

	// Token: 0x04002937 RID: 10551
	private bool gameStarted;

	// Token: 0x04002938 RID: 10552
	private static readonly EventSystem.IntraObjectHandler<Game> MarkStatusItemRendererDirtyDelegate = new EventSystem.IntraObjectHandler<Game>(delegate(Game component, object data)
	{
		component.MarkStatusItemRendererDirty(data);
	});

	// Token: 0x04002939 RID: 10553
	private static readonly EventSystem.IntraObjectHandler<Game> ActiveWorldChangedDelegate = new EventSystem.IntraObjectHandler<Game>(delegate(Game component, object data)
	{
		component.ForceOverlayUpdate(true);
	});

	// Token: 0x0400293A RID: 10554
	private ushort[] activeFX;

	// Token: 0x0400293B RID: 10555
	public bool debugWasUsed;

	// Token: 0x0400293C RID: 10556
	private bool isLoading;

	// Token: 0x0400293D RID: 10557
	private List<Game.SimActiveRegion> simActiveRegions = new List<Game.SimActiveRegion>();

	// Token: 0x0400293E RID: 10558
	private HashedString previousOverlayMode = OverlayModes.None.ID;

	// Token: 0x0400293F RID: 10559
	private float previousGasConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002940 RID: 10560
	private float previousLiquidConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002941 RID: 10561
	private float previousSolidConduitFlowDiscreteLerpPercent = -1f;

	// Token: 0x04002942 RID: 10562
	[SerializeField]
	private Game.SpawnPoolData[] fxSpawnData;

	// Token: 0x04002943 RID: 10563
	private Dictionary<int, Action<Vector3, float>> fxSpawner = new Dictionary<int, Action<Vector3, float>>();

	// Token: 0x04002944 RID: 10564
	private Dictionary<int, GameObjectPool> fxPools = new Dictionary<int, GameObjectPool>();

	// Token: 0x04002945 RID: 10565
	private Game.SavingPreCB activatePreCB;

	// Token: 0x04002946 RID: 10566
	private Game.SavingActiveCB activateActiveCB;

	// Token: 0x04002947 RID: 10567
	private Game.SavingPostCB activatePostCB;

	// Token: 0x04002948 RID: 10568
	[SerializeField]
	public Game.UIColours uiColours = new Game.UIColours();

	// Token: 0x04002949 RID: 10569
	private float lastTimeWorkStarted = float.NegativeInfinity;

	// Token: 0x020018C1 RID: 6337
	[Serializable]
	public struct SavedInfo
	{
		// Token: 0x06009D7A RID: 40314 RVA: 0x0039345D File Offset: 0x0039165D
		[OnDeserialized]
		private void OnDeserialized()
		{
			this.InitializeEmptyVariables();
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x00393465 File Offset: 0x00391665
		public void InitializeEmptyVariables()
		{
			if (this.creaturePoopAmount == null)
			{
				this.creaturePoopAmount = new Dictionary<Tag, float>();
			}
			if (this.powerCreatedbyGeneratorType == null)
			{
				this.powerCreatedbyGeneratorType = new Dictionary<Tag, float>();
			}
		}

		// Token: 0x040079C5 RID: 31173
		public bool discoveredSurface;

		// Token: 0x040079C6 RID: 31174
		public bool discoveredOilField;

		// Token: 0x040079C7 RID: 31175
		public bool curedDisease;

		// Token: 0x040079C8 RID: 31176
		public bool blockedCometWithBunkerDoor;

		// Token: 0x040079C9 RID: 31177
		public Dictionary<Tag, float> creaturePoopAmount;

		// Token: 0x040079CA RID: 31178
		public Dictionary<Tag, float> powerCreatedbyGeneratorType;
	}

	// Token: 0x020018C2 RID: 6338
	public struct CallbackInfo
	{
		// Token: 0x06009D7C RID: 40316 RVA: 0x0039348D File Offset: 0x0039168D
		public CallbackInfo(global::System.Action cb, bool manually_release = false)
		{
			this.cb = cb;
			this.manuallyRelease = manually_release;
		}

		// Token: 0x040079CB RID: 31179
		public global::System.Action cb;

		// Token: 0x040079CC RID: 31180
		public bool manuallyRelease;
	}

	// Token: 0x020018C3 RID: 6339
	public struct ComplexCallbackInfo<DataType>
	{
		// Token: 0x06009D7D RID: 40317 RVA: 0x0039349D File Offset: 0x0039169D
		public ComplexCallbackInfo(Action<DataType, object> cb, object callback_data, string debug_info)
		{
			this.cb = cb;
			this.debugInfo = debug_info;
			this.callbackData = callback_data;
		}

		// Token: 0x040079CD RID: 31181
		public Action<DataType, object> cb;

		// Token: 0x040079CE RID: 31182
		public object callbackData;

		// Token: 0x040079CF RID: 31183
		public string debugInfo;
	}

	// Token: 0x020018C4 RID: 6340
	public class ComplexCallbackHandleVector<DataType>
	{
		// Token: 0x06009D7E RID: 40318 RVA: 0x003934B4 File Offset: 0x003916B4
		public ComplexCallbackHandleVector(int initial_size)
		{
			this.baseMgr = new HandleVector<Game.ComplexCallbackInfo<DataType>>(initial_size);
		}

		// Token: 0x06009D7F RID: 40319 RVA: 0x003934D3 File Offset: 0x003916D3
		public HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle Add(Action<DataType, object> cb, object callback_data, string debug_info)
		{
			return this.baseMgr.Add(new Game.ComplexCallbackInfo<DataType>(cb, callback_data, debug_info));
		}

		// Token: 0x06009D80 RID: 40320 RVA: 0x003934E8 File Offset: 0x003916E8
		public Game.ComplexCallbackInfo<DataType> GetItem(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle)
		{
			Game.ComplexCallbackInfo<DataType> item;
			try
			{
				item = this.baseMgr.GetItem(handle);
			}
			catch (Exception ex)
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandleUnchecked(handle, out b, out num);
				string text = null;
				if (this.releaseInfo.TryGetValue(num, out text))
				{
					KCrashReporter.Assert(false, "Trying to get data for handle that was already released by " + text, null);
				}
				else
				{
					KCrashReporter.Assert(false, "Trying to get data for handle that was released ...... magically", null);
				}
				throw ex;
			}
			return item;
		}

		// Token: 0x06009D81 RID: 40321 RVA: 0x00393558 File Offset: 0x00391758
		public Game.ComplexCallbackInfo<DataType> Release(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle, string release_info)
		{
			Game.ComplexCallbackInfo<DataType> complexCallbackInfo;
			try
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandle(handle, out b, out num);
				this.releaseInfo[num] = release_info;
				complexCallbackInfo = this.baseMgr.Release(handle);
			}
			catch (Exception ex)
			{
				byte b;
				int num;
				this.baseMgr.UnpackHandleUnchecked(handle, out b, out num);
				string text = null;
				if (this.releaseInfo.TryGetValue(num, out text))
				{
					KCrashReporter.Assert(false, release_info + "is trying to release handle but it was already released by " + text, null);
				}
				else
				{
					KCrashReporter.Assert(false, release_info + "is trying to release a handle that was already released by some unknown thing", null);
				}
				throw ex;
			}
			return complexCallbackInfo;
		}

		// Token: 0x06009D82 RID: 40322 RVA: 0x003935EC File Offset: 0x003917EC
		public void Clear()
		{
			this.baseMgr.Clear();
		}

		// Token: 0x06009D83 RID: 40323 RVA: 0x003935F9 File Offset: 0x003917F9
		public bool IsVersionValid(HandleVector<Game.ComplexCallbackInfo<DataType>>.Handle handle)
		{
			return this.baseMgr.IsVersionValid(handle);
		}

		// Token: 0x040079D0 RID: 31184
		private HandleVector<Game.ComplexCallbackInfo<DataType>> baseMgr;

		// Token: 0x040079D1 RID: 31185
		private Dictionary<int, string> releaseInfo = new Dictionary<int, string>();
	}

	// Token: 0x020018C5 RID: 6341
	public enum TemperatureOverlayModes
	{
		// Token: 0x040079D3 RID: 31187
		AbsoluteTemperature,
		// Token: 0x040079D4 RID: 31188
		AdaptiveTemperature,
		// Token: 0x040079D5 RID: 31189
		HeatFlow,
		// Token: 0x040079D6 RID: 31190
		StateChange,
		// Token: 0x040079D7 RID: 31191
		RelativeTemperature
	}

	// Token: 0x020018C6 RID: 6342
	[Serializable]
	public class ConduitVisInfo
	{
		// Token: 0x040079D8 RID: 31192
		public GameObject prefab;

		// Token: 0x040079D9 RID: 31193
		[Header("Main View")]
		public Color32 tint;

		// Token: 0x040079DA RID: 31194
		public Color32 insulatedTint;

		// Token: 0x040079DB RID: 31195
		public Color32 radiantTint;

		// Token: 0x040079DC RID: 31196
		[Header("Overlay")]
		public string overlayTintName;

		// Token: 0x040079DD RID: 31197
		public string overlayInsulatedTintName;

		// Token: 0x040079DE RID: 31198
		public string overlayRadiantTintName;

		// Token: 0x040079DF RID: 31199
		public Vector2 overlayMassScaleRange = new Vector2f(1f, 1000f);

		// Token: 0x040079E0 RID: 31200
		public Vector2 overlayMassScaleValues = new Vector2f(0.1f, 1f);
	}

	// Token: 0x020018C7 RID: 6343
	private class WorldRegion
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06009D85 RID: 40325 RVA: 0x00393643 File Offset: 0x00391843
		public Vector2I regionMin
		{
			get
			{
				return this.min;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06009D86 RID: 40326 RVA: 0x0039364B File Offset: 0x0039184B
		public Vector2I regionMax
		{
			get
			{
				return this.max;
			}
		}

		// Token: 0x06009D87 RID: 40327 RVA: 0x00393654 File Offset: 0x00391854
		public void UpdateGameActiveRegion(int x0, int y0, int x1, int y1)
		{
			this.min.x = Mathf.Max(0, x0);
			this.min.y = Mathf.Max(0, y0);
			this.max.x = Mathf.Max(x1, this.regionMax.x);
			this.max.y = Mathf.Max(y1, this.regionMax.y);
		}

		// Token: 0x06009D88 RID: 40328 RVA: 0x003936BE File Offset: 0x003918BE
		public void UpdateGameActiveRegion(Vector2I simActiveRegionMin, Vector2I simActiveRegionMax)
		{
			this.min = simActiveRegionMin;
			this.max = simActiveRegionMax;
		}

		// Token: 0x040079E1 RID: 31201
		private Vector2I min;

		// Token: 0x040079E2 RID: 31202
		private Vector2I max;

		// Token: 0x040079E3 RID: 31203
		public bool isActive;
	}

	// Token: 0x020018C8 RID: 6344
	public class SimActiveRegion
	{
		// Token: 0x06009D8A RID: 40330 RVA: 0x003936D6 File Offset: 0x003918D6
		public SimActiveRegion()
		{
			this.region = default(Pair<Vector2I, Vector2I>);
			this.currentSunlightIntensity = (float)FIXEDTRAITS.SUNLIGHT.DEFAULT_VALUE;
			this.currentCosmicRadiationIntensity = (float)FIXEDTRAITS.COSMICRADIATION.DEFAULT_VALUE;
		}

		// Token: 0x040079E4 RID: 31204
		public Pair<Vector2I, Vector2I> region;

		// Token: 0x040079E5 RID: 31205
		public float currentSunlightIntensity;

		// Token: 0x040079E6 RID: 31206
		public float currentCosmicRadiationIntensity;
	}

	// Token: 0x020018C9 RID: 6345
	private enum SpawnRotationConfig
	{
		// Token: 0x040079E8 RID: 31208
		Normal,
		// Token: 0x040079E9 RID: 31209
		StringName
	}

	// Token: 0x020018CA RID: 6346
	[Serializable]
	private struct SpawnRotationData
	{
		// Token: 0x040079EA RID: 31210
		public string animName;

		// Token: 0x040079EB RID: 31211
		public bool flip;
	}

	// Token: 0x020018CB RID: 6347
	[Serializable]
	private struct SpawnPoolData
	{
		// Token: 0x040079EC RID: 31212
		[HashedEnum]
		public SpawnFXHashes id;

		// Token: 0x040079ED RID: 31213
		public int initialCount;

		// Token: 0x040079EE RID: 31214
		public Color32 colour;

		// Token: 0x040079EF RID: 31215
		public GameObject fxPrefab;

		// Token: 0x040079F0 RID: 31216
		public string initialAnim;

		// Token: 0x040079F1 RID: 31217
		public Vector3 spawnOffset;

		// Token: 0x040079F2 RID: 31218
		public Vector2 spawnRandomOffset;

		// Token: 0x040079F3 RID: 31219
		public Game.SpawnRotationConfig rotationConfig;

		// Token: 0x040079F4 RID: 31220
		public Game.SpawnRotationData[] rotationData;
	}

	// Token: 0x020018CC RID: 6348
	[Serializable]
	private class Settings
	{
		// Token: 0x06009D8B RID: 40331 RVA: 0x00393702 File Offset: 0x00391902
		public Settings(Game game)
		{
			this.nextUniqueID = KPrefabID.NextUniqueID;
			this.gameID = KleiMetrics.GameID();
		}

		// Token: 0x06009D8C RID: 40332 RVA: 0x00393720 File Offset: 0x00391920
		public Settings()
		{
		}

		// Token: 0x040079F5 RID: 31221
		public int nextUniqueID;

		// Token: 0x040079F6 RID: 31222
		public int gameID;
	}

	// Token: 0x020018CD RID: 6349
	public class GameSaveData
	{
		// Token: 0x040079F7 RID: 31223
		public ConduitFlow gasConduitFlow;

		// Token: 0x040079F8 RID: 31224
		public ConduitFlow liquidConduitFlow;

		// Token: 0x040079F9 RID: 31225
		public FallingWater fallingWater;

		// Token: 0x040079FA RID: 31226
		public UnstableGroundManager unstableGround;

		// Token: 0x040079FB RID: 31227
		public WorldDetailSave worldDetail;

		// Token: 0x040079FC RID: 31228
		public CustomGameSettings customGameSettings;

		// Token: 0x040079FD RID: 31229
		public StoryManager storySetings;

		// Token: 0x040079FE RID: 31230
		public SpaceScannerNetworkManager spaceScannerNetworkManager;

		// Token: 0x040079FF RID: 31231
		public bool debugWasUsed;

		// Token: 0x04007A00 RID: 31232
		public bool autoPrioritizeRoles;

		// Token: 0x04007A01 RID: 31233
		public bool advancedPersonalPriorities;

		// Token: 0x04007A02 RID: 31234
		public Game.SavedInfo savedInfo;

		// Token: 0x04007A03 RID: 31235
		public string dateGenerated;

		// Token: 0x04007A04 RID: 31236
		public List<uint> changelistsPlayedOn;
	}

	// Token: 0x020018CE RID: 6350
	// (Invoke) Token: 0x06009D8F RID: 40335
	public delegate void CansaveCB();

	// Token: 0x020018CF RID: 6351
	// (Invoke) Token: 0x06009D93 RID: 40339
	public delegate void SavingPreCB(Game.CansaveCB cb);

	// Token: 0x020018D0 RID: 6352
	// (Invoke) Token: 0x06009D97 RID: 40343
	public delegate void SavingActiveCB();

	// Token: 0x020018D1 RID: 6353
	// (Invoke) Token: 0x06009D9B RID: 40347
	public delegate void SavingPostCB();

	// Token: 0x020018D2 RID: 6354
	[Serializable]
	public struct LocationColours
	{
		// Token: 0x04007A05 RID: 31237
		public Color unreachable;

		// Token: 0x04007A06 RID: 31238
		public Color invalidLocation;

		// Token: 0x04007A07 RID: 31239
		public Color validLocation;

		// Token: 0x04007A08 RID: 31240
		public Color requiresRole;

		// Token: 0x04007A09 RID: 31241
		public Color unreachable_requiresRole;
	}

	// Token: 0x020018D3 RID: 6355
	[Serializable]
	public class UIColours
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06009D9E RID: 40350 RVA: 0x00393730 File Offset: 0x00391930
		public Game.LocationColours Dig
		{
			get
			{
				return this.digColours;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06009D9F RID: 40351 RVA: 0x00393738 File Offset: 0x00391938
		public Game.LocationColours Build
		{
			get
			{
				return this.buildColours;
			}
		}

		// Token: 0x04007A0A RID: 31242
		[SerializeField]
		private Game.LocationColours digColours;

		// Token: 0x04007A0B RID: 31243
		[SerializeField]
		private Game.LocationColours buildColours;
	}
}
