using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using KSerialization;
using ProcGen;
using UnityEngine;

// Token: 0x02000B46 RID: 2886
public class LargeImpactorEvent : GameplayEvent<LargeImpactorEvent.StatesInstance>
{
	// Token: 0x060055CB RID: 21963 RVA: 0x001F27C9 File Offset: 0x001F09C9
	public LargeImpactorEvent(string id, string[] requiredDlcIds, string[] forbiddenDlcIds)
		: base(id, 0, 0, requiredDlcIds, forbiddenDlcIds)
	{
	}

	// Token: 0x060055CC RID: 21964 RVA: 0x001F27D6 File Offset: 0x001F09D6
	public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
	{
		return new LargeImpactorEvent.StatesInstance(manager, eventInstance, this);
	}

	// Token: 0x060055CD RID: 21965 RVA: 0x001F27E0 File Offset: 0x001F09E0
	private static void SpawnIridiumShowers(LargeImpactorEvent.StatesInstance smi)
	{
		GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.IridiumShowerEvent, smi.eventInstance.worldId, null);
	}

	// Token: 0x060055CE RID: 21966 RVA: 0x001F2808 File Offset: 0x001F0A08
	private static void PreventDemoliorFragmentsBGFromPlaying(LargeImpactorEvent.StatesInstance smi)
	{
		TerrainBG.preventLargeImpactorFragmentsFromProgressing = true;
	}

	// Token: 0x060055CF RID: 21967 RVA: 0x001F2810 File Offset: 0x001F0A10
	private static void AllowDemoliorFragmentsBGFromPlaying(LargeImpactorEvent.StatesInstance smi)
	{
		TerrainBG.preventLargeImpactorFragmentsFromProgressing = false;
	}

	// Token: 0x060055D0 RID: 21968 RVA: 0x001F2818 File Offset: 0x001F0A18
	private static void DestroyEventInstance(LargeImpactorEvent.StatesInstance smi)
	{
		smi.eventInstance.smi.StopSM("end");
	}

	// Token: 0x060055D1 RID: 21969 RVA: 0x001F282F File Offset: 0x001F0A2F
	private static bool WasWinAchievementAlreadyGranted(LargeImpactorEvent.StatesInstance smi)
	{
		return SaveGame.Instance.ColonyAchievementTracker.IsAchievementUnlocked(Db.Get().ColonyAchievements.AsteroidDestroyed);
	}

	// Token: 0x060055D2 RID: 21970 RVA: 0x001F284F File Offset: 0x001F0A4F
	private static void UnlockWinAchievement(LargeImpactorEvent.StatesInstance smi)
	{
		SaveGame.Instance.ColonyAchievementTracker.largeImpactorState = ColonyAchievementTracker.LargeImpactorState.Defeated;
	}

	// Token: 0x060055D3 RID: 21971 RVA: 0x001F2864 File Offset: 0x001F0A64
	private static void RegisterDemoliorSize(LargeImpactorEvent.StatesInstance smi)
	{
		ParallaxBackgroundObject component = smi.impactorInstance.GetComponent<ParallaxBackgroundObject>();
		SaveGame.Instance.ColonyAchievementTracker.LargeImpactorBackgroundScale = component.lastScaleUsed;
	}

	// Token: 0x060055D4 RID: 21972 RVA: 0x001F2892 File Offset: 0x001F0A92
	private static void RegisterLandedCycle(LargeImpactorEvent.StatesInstance smi)
	{
		SaveGame.Instance.ColonyAchievementTracker.largeImpactorState = ColonyAchievementTracker.LargeImpactorState.Landed;
		SaveGame.Instance.ColonyAchievementTracker.largeImpactorLandedCycle = GameClock.Instance.GetCycle();
	}

	// Token: 0x060055D5 RID: 21973 RVA: 0x001F28C0 File Offset: 0x001F0AC0
	private static bool IsSuitablePOISpawnLocation(AxialI location)
	{
		if (!ClusterGrid.Instance.IsValidCell(location))
		{
			return false;
		}
		foreach (ClusterGridEntity clusterGridEntity in ClusterGrid.Instance.GetEntitiesOnCell(location))
		{
			if (clusterGridEntity.Layer == EntityLayer.Asteroid || clusterGridEntity.Layer == EntityLayer.POI)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060055D6 RID: 21974 RVA: 0x001F2938 File Offset: 0x001F0B38
	private static List<AxialI> FindAvailablePOISpawnLocations(AxialI location)
	{
		List<AxialI> list = new List<AxialI>();
		if (LargeImpactorEvent.IsSuitablePOISpawnLocation(location))
		{
			list.Add(location);
		}
		for (int i = 1; i <= 2; i++)
		{
			foreach (AxialI axialI in AxialI.DIRECTIONS)
			{
				AxialI axialI2 = location + axialI * i;
				if (LargeImpactorEvent.IsSuitablePOISpawnLocation(axialI2))
				{
					list.Add(axialI2);
				}
			}
		}
		return list;
	}

	// Token: 0x060055D7 RID: 21975 RVA: 0x001F29C8 File Offset: 0x001F0BC8
	private static void SpawnPOI(string id, AxialI location)
	{
		GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab(id), null, null);
		gameObject.GetComponent<HarvestablePOIClusterGridEntity>().Init(location);
		gameObject.SetActive(true);
	}

	// Token: 0x060055D8 RID: 21976 RVA: 0x001F29F0 File Offset: 0x001F0BF0
	private static void HandleInterception(LargeImpactorEvent.StatesInstance smi)
	{
		if (DlcManager.IsExpansion1Active())
		{
			List<AxialI> list = LargeImpactorEvent.FindAvailablePOISpawnLocations(smi.impactorInstance.GetSMI<ClusterMapLargeImpactor.Instance>().ClusterGridPosition());
			if (list.Count > 0)
			{
				LargeImpactorEvent.SpawnPOI("HarvestableSpacePOI_DLC4ImpactorDebrisField1", list[0]);
			}
			if (list.Count > 1)
			{
				LargeImpactorEvent.SpawnPOI("HarvestableSpacePOI_DLC4ImpactorDebrisField2", list[1]);
			}
			if (list.Count > 2)
			{
				LargeImpactorEvent.SpawnPOI("HarvestableSpacePOI_DLC4ImpactorDebrisField3", list[2]);
			}
		}
		else
		{
			if (!SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination.Id, SpacecraftManager.DestinationLocationSelectionType.Nearest, 0, 2147483647, 3))
			{
				SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination.Id, SpacecraftManager.DestinationLocationSelectionType.Random, 0, int.MaxValue, 5);
			}
			if (!SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination2.Id, SpacecraftManager.DestinationLocationSelectionType.Random, 1, 5, 5))
			{
				SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination2.Id, SpacecraftManager.DestinationLocationSelectionType.Random, 0, int.MaxValue, 5);
			}
			if (!SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination3.Id, SpacecraftManager.DestinationLocationSelectionType.Random, 1, 5, 5))
			{
				SpacecraftManager.instance.AddDestination(Db.Get().SpaceDestinationTypes.DLC4PrehistoricDemoliorSpaceDestination3.Id, SpacecraftManager.DestinationLocationSelectionType.Random, 0, int.MaxValue, 5);
			}
		}
		smi.GoTo(smi.sm.finished);
	}

	// Token: 0x060055D9 RID: 21977 RVA: 0x001F2B66 File Offset: 0x001F0D66
	private static bool WasKilled(LargeImpactorEvent.StatesInstance smi, object _)
	{
		return smi.impactorInstance.GetSMI<LargeImpactorStatus.Instance>().Health <= 0;
	}

	// Token: 0x060055DA RID: 21978 RVA: 0x001F2B7E File Offset: 0x001F0D7E
	private static void PrepareForLargeImpactorDefeatedSequence(LargeImpactorEvent.StatesInstance smi)
	{
		smi.impactorInstance.GetComponent<LargeImpactorCrashStamp>();
		LargeImpactorEvent.ToggleOffLandingZoneVisualizer(smi);
		ClusterManager.Instance.GetWorld(smi.eventInstance.worldId).RevealSurface();
	}

	// Token: 0x060055DB RID: 21979 RVA: 0x001F2BAC File Offset: 0x001F0DAC
	private static void InitializeLandingSequence(LargeImpactorEvent.StatesInstance smi)
	{
		GameObject impactorInstance = smi.impactorInstance;
		LargeImpactorCrashStamp component = impactorInstance.GetComponent<LargeImpactorCrashStamp>();
		ParallaxBackgroundObject component2 = impactorInstance.GetComponent<ParallaxBackgroundObject>();
		LargeImpactorEvent.ToggleOffLandingZoneVisualizer(smi);
		WorldContainer world = ClusterManager.Instance.GetWorld(smi.eventInstance.worldId);
		world.RevealHiddenY();
		world.RevealSurface();
		component.RevealFogOfWar(7);
		component2.SetVisibilityState(false);
		LargeComet largeComet = LargeImpactorEvent.CreateLargeImpactorInWorldFallingAsteroid(smi, component, world);
		LargeImpactorLandingSequence.Start(component, largeComet, component, world.id);
	}

	// Token: 0x060055DC RID: 21980 RVA: 0x001F2C18 File Offset: 0x001F0E18
	private static void ToggleOffLandingZoneVisualizer(LargeImpactorEvent.StatesInstance smi)
	{
		LargeImpactorVisualizer component = smi.impactorInstance.GetComponent<LargeImpactorVisualizer>();
		if (component.Active)
		{
			component.Active = false;
		}
	}

	// Token: 0x060055DD RID: 21981 RVA: 0x001F2C40 File Offset: 0x001F0E40
	private static LargeComet CreateLargeImpactorInWorldFallingAsteroid(LargeImpactorEvent.StatesInstance smi, LargeImpactorCrashStamp crashStamp, WorldContainer world)
	{
		TemplateContainer asteroidTemplate = crashStamp.asteroidTemplate;
		Vector2I stampLocation = crashStamp.stampLocation;
		float layerZ = Grid.GetLayerZ(Grid.SceneLayer.FXFront);
		Vector3 vector = new Vector3((float)stampLocation.X, (float)(world.Height - world.HiddenYOffset - 1), layerZ);
		GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab(LargeImpactorCometConfig.ID), vector, Quaternion.identity, null, null, true, 0);
		LargeComet component = gameObject.GetComponent<LargeComet>();
		gameObject.SetActive(true);
		component.stampLocation = stampLocation;
		component.crashPosition = stampLocation;
		LargeComet largeComet = component;
		largeComet.crashPosition.y = largeComet.crashPosition.y + asteroidTemplate.GetTemplateBounds(0).yMin;
		component.asteroidTemplate = asteroidTemplate;
		component.bottomCellsOffsetOfTemplate = crashStamp.TemplateBottomCellsOffsets;
		return component;
	}

	// Token: 0x060055DE RID: 21982 RVA: 0x001F2CF8 File Offset: 0x001F0EF8
	private static GameObject CreateSpacedOutImpactorInstance(LargeImpactorEvent.StatesInstance smi)
	{
		if (!DlcManager.IsExpansion1Active() || ClusterGrid.Instance == null)
		{
			return null;
		}
		GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab("LargeImpactor"), null, null);
		float num = smi.eventInstance.eventStartTime * 600f + LargeImpactorEvent.GetImpactTime();
		AxialI location = ClusterManager.Instance.GetClusterPOIManager().GetTemporalTear().Location;
		ClusterMapMeteorShowerVisualizer component = gameObject.GetComponent<ClusterMapMeteorShowerVisualizer>();
		component.SetInitialLocation(location);
		component.forceRevealed = true;
		ClusterMapLargeImpactor.Def def = gameObject.AddOrGetDef<ClusterMapLargeImpactor.Def>();
		def.destinationWorldID = 0;
		def.arrivalTime = num;
		gameObject.AddOrGet<ParallaxBackgroundObject>().worldId = new int?(smi.eventInstance.worldId);
		return gameObject;
	}

	// Token: 0x060055DF RID: 21983 RVA: 0x001F2D99 File Offset: 0x001F0F99
	private static GameObject CreateVanillaImpactorInstance(LargeImpactorEvent.StatesInstance smi)
	{
		if (DlcManager.IsExpansion1Active())
		{
			return null;
		}
		return global::Util.KInstantiate(Assets.GetPrefab(LargeImpactorVanillaConfig.ID), null, null);
	}

	// Token: 0x060055E0 RID: 21984 RVA: 0x001F2DBC File Offset: 0x001F0FBC
	public static void CreateImpactorInstance(LargeImpactorEvent.StatesInstance smi)
	{
		GameObject gameObject;
		if (DlcManager.IsExpansion1Active())
		{
			gameObject = LargeImpactorEvent.CreateSpacedOutImpactorInstance(smi);
		}
		else
		{
			gameObject = LargeImpactorEvent.CreateVanillaImpactorInstance(smi);
		}
		if (gameObject == null)
		{
			KCrashReporter.ReportDevNotification("Failed to create LargeImpactor Object.", Environment.StackTrace, "", false, null);
			smi.StopSM("No Impactor created");
			return;
		}
		gameObject.SetActive(true);
		smi.sm.impactorTarget.Set(gameObject.GetComponent<KPrefabID>(), smi);
	}

	// Token: 0x060055E1 RID: 21985 RVA: 0x001F2E2C File Offset: 0x001F102C
	public static float GetImpactTime()
	{
		ClusterLayout currentClusterLayout = CustomGameSettings.Instance.GetCurrentClusterLayout();
		if (currentClusterLayout != null && currentClusterLayout.clusterTags.Contains("DemoliorImminentImpact"))
		{
			return 6000f;
		}
		float num = 200f;
		SettingLevel currentQualitySetting = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.DemoliorDifficulty);
		if (currentQualitySetting.id == "VeryHard")
		{
			num = 100f;
		}
		else if (currentQualitySetting.id == "Hard")
		{
			num = 150f;
		}
		else if (currentQualitySetting.id == "Easy")
		{
			num = 300f;
		}
		else if (currentQualitySetting.id == "VeryEasy")
		{
			num = 500f;
		}
		return num * 600f;
	}

	// Token: 0x02001C67 RID: 7271
	public class States : GameplayEventStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, LargeImpactorEvent>
	{
		// Token: 0x0600AAE3 RID: 43747 RVA: 0x003BBE94 File Offset: 0x003BA094
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			base.InitializeStates(out default_state);
			default_state = this.start;
			this.start.ParamTransition<GameObject>(this.impactorTarget, this.create, GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.IsNull).ParamTransition<GameObject>(this.impactorTarget, this.clusterMap, GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.IsNotNull);
			this.create.ParamTransition<GameObject>(this.impactorTarget, this.clusterMap, GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.IsNotNull).Enter(delegate(LargeImpactorEvent.StatesInstance smi)
			{
				LargeImpactorEvent.CreateImpactorInstance(smi);
			});
			this.clusterMap.Target(this.impactorTarget).EventTransition(GameHashes.LargeImpactorArrived, this.impacting, null).EventTransition(GameHashes.Died, this.killedByPlayer, null);
			this.impacting.Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.RegisterLandedCycle)).Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.InitializeLandingSequence)).Target(this.impactorTarget)
				.EventTransition(GameHashes.SequenceCompleted, this.finished, null);
			this.killedByPlayer.EnterTransition(this.finished, new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.Transition.ConditionCallback(LargeImpactorEvent.WasWinAchievementAlreadyGranted)).Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.PrepareForLargeImpactorDefeatedSequence)).Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.PreventDemoliorFragmentsBGFromPlaying))
				.Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.UnlockWinAchievement))
				.Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.RegisterDemoliorSize))
				.Exit(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.AllowDemoliorFragmentsBGFromPlaying))
				.Exit(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.SpawnIridiumShowers))
				.Target(this.impactorTarget)
				.EventHandler(GameHashes.SequenceCompleted, new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.HandleInterception));
			this.finished.Enter(delegate(LargeImpactorEvent.StatesInstance smi)
			{
				global::Util.KDestroyGameObject(smi.sm.impactorTarget.Get(smi));
			}).Enter(new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State.Callback(LargeImpactorEvent.DestroyEventInstance)).GoTo(null);
		}

		// Token: 0x04008638 RID: 34360
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State start;

		// Token: 0x04008639 RID: 34361
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State create;

		// Token: 0x0400863A RID: 34362
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State clusterMap;

		// Token: 0x0400863B RID: 34363
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State killedByPlayer;

		// Token: 0x0400863C RID: 34364
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State impacting;

		// Token: 0x0400863D RID: 34365
		public GameStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.State finished;

		// Token: 0x0400863E RID: 34366
		[Serialize]
		public StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.TargetParameter impactorTarget = new StateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, object>.TargetParameter();
	}

	// Token: 0x02001C68 RID: 7272
	public class StatesInstance : GameplayEventStateMachine<LargeImpactorEvent.States, LargeImpactorEvent.StatesInstance, GameplayEventManager, LargeImpactorEvent>.GameplayEventStateMachineInstance
	{
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600AAE5 RID: 43749 RVA: 0x003BC09E File Offset: 0x003BA29E
		public GameObject impactorInstance
		{
			get
			{
				return base.sm.impactorTarget.Get(base.smi);
			}
		}

		// Token: 0x0600AAE6 RID: 43750 RVA: 0x003BC0B6 File Offset: 0x003BA2B6
		public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, LargeImpactorEvent largeImpactorEvent)
			: base(master, eventInstance, largeImpactorEvent)
		{
		}
	}
}
