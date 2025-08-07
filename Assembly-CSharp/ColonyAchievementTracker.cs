using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000820 RID: 2080
[AddComponentMenu("KMonoBehaviour/scripts/ColonyAchievementTracker")]
public class ColonyAchievementTracker : KMonoBehaviour, ISaveLoadableDetails, IRenderEveryTick
{
	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06003907 RID: 14599 RVA: 0x0013C3B1 File Offset: 0x0013A5B1
	public bool HasAnyDupeDied
	{
		get
		{
			return this.deadDupeCounter > 0;
		}
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x06003908 RID: 14600 RVA: 0x0013C3BC File Offset: 0x0013A5BC
	// (set) Token: 0x06003909 RID: 14601 RVA: 0x0013C3C9 File Offset: 0x0013A5C9
	public bool GeothermalFacilityDiscovered
	{
		get
		{
			return (this.geothermalProgress & 1) == 1;
		}
		set
		{
			if (value)
			{
				this.geothermalProgress |= 1;
				return;
			}
			DebugUtil.DevAssert(value, "unsetting progress? why", null);
			this.geothermalProgress &= -2;
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x0600390A RID: 14602 RVA: 0x0013C3F8 File Offset: 0x0013A5F8
	// (set) Token: 0x0600390B RID: 14603 RVA: 0x0013C405 File Offset: 0x0013A605
	public bool GeothermalControllerRepaired
	{
		get
		{
			return (this.geothermalProgress & 2) == 2;
		}
		set
		{
			if (value)
			{
				this.geothermalProgress |= 2;
				return;
			}
			DebugUtil.DevAssert(value, "unsetting progress? why", null);
			this.geothermalProgress &= -3;
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x0600390C RID: 14604 RVA: 0x0013C434 File Offset: 0x0013A634
	// (set) Token: 0x0600390D RID: 14605 RVA: 0x0013C441 File Offset: 0x0013A641
	public bool GeothermalControllerHasVented
	{
		get
		{
			return (this.geothermalProgress & 4) == 4;
		}
		set
		{
			if (value)
			{
				this.geothermalProgress |= 4;
				return;
			}
			DebugUtil.DevAssert(value, "unsetting progress? why", null);
			this.geothermalProgress &= -5;
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x0600390E RID: 14606 RVA: 0x0013C470 File Offset: 0x0013A670
	// (set) Token: 0x0600390F RID: 14607 RVA: 0x0013C47D File Offset: 0x0013A67D
	public bool GeothermalClearedEntombedVent
	{
		get
		{
			return (this.geothermalProgress & 8) == 8;
		}
		set
		{
			if (value)
			{
				this.geothermalProgress |= 8;
				return;
			}
			DebugUtil.DevAssert(value, "unsetting progress? why", null);
			this.geothermalProgress &= -9;
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06003910 RID: 14608 RVA: 0x0013C4AC File Offset: 0x0013A6AC
	// (set) Token: 0x06003911 RID: 14609 RVA: 0x0013C4BB File Offset: 0x0013A6BB
	public bool GeothermalVictoryPopupDismissed
	{
		get
		{
			return (this.geothermalProgress & 16) == 16;
		}
		set
		{
			if (value)
			{
				this.geothermalProgress |= 16;
				return;
			}
			DebugUtil.DevAssert(value, "unsetting progress? why", null);
			this.geothermalProgress &= -17;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06003912 RID: 14610 RVA: 0x0013C4EB File Offset: 0x0013A6EB
	public List<string> achievementsToDisplay
	{
		get
		{
			return this.completedAchievementsToDisplay;
		}
	}

	// Token: 0x06003913 RID: 14611 RVA: 0x0013C4F3 File Offset: 0x0013A6F3
	public void ClearDisplayAchievements()
	{
		this.achievementsToDisplay.Clear();
	}

	// Token: 0x06003914 RID: 14612 RVA: 0x0013C500 File Offset: 0x0013A700
	protected override void OnSpawn()
	{
		base.OnSpawn();
		foreach (ColonyAchievement colonyAchievement in Db.Get().ColonyAchievements.resources)
		{
			if (!this.achievements.ContainsKey(colonyAchievement.Id))
			{
				ColonyAchievementStatus colonyAchievementStatus = new ColonyAchievementStatus(colonyAchievement.Id);
				this.achievements.Add(colonyAchievement.Id, colonyAchievementStatus);
			}
		}
		this.forceCheckAchievementHandle = Game.Instance.Subscribe(395452326, new Action<object>(this.CheckAchievements));
		base.Subscribe<ColonyAchievementTracker>(631075836, ColonyAchievementTracker.OnNewDayDelegate);
		this.UpgradeTamedCritterAchievements();
	}

	// Token: 0x06003915 RID: 14613 RVA: 0x0013C5C4 File Offset: 0x0013A7C4
	private void UpgradeTamedCritterAchievements()
	{
		foreach (ColonyAchievementRequirement colonyAchievementRequirement in Db.Get().ColonyAchievements.TameAllBasicCritters.requirementChecklist)
		{
			CritterTypesWithTraits critterTypesWithTraits = colonyAchievementRequirement as CritterTypesWithTraits;
			if (critterTypesWithTraits != null)
			{
				critterTypesWithTraits.UpdateSavedState();
			}
		}
		foreach (ColonyAchievementRequirement colonyAchievementRequirement2 in Db.Get().ColonyAchievements.TameAGassyMoo.requirementChecklist)
		{
			CritterTypesWithTraits critterTypesWithTraits2 = colonyAchievementRequirement2 as CritterTypesWithTraits;
			if (critterTypesWithTraits2 != null)
			{
				critterTypesWithTraits2.UpdateSavedState();
			}
		}
	}

	// Token: 0x06003916 RID: 14614 RVA: 0x0013C684 File Offset: 0x0013A884
	public void RenderEveryTick(float dt)
	{
		if (this.updatingAchievement >= this.achievements.Count)
		{
			this.updatingAchievement = 0;
		}
		KeyValuePair<string, ColonyAchievementStatus> keyValuePair = this.achievements.ElementAt(this.updatingAchievement);
		this.updatingAchievement++;
		if (!keyValuePair.Value.success && !keyValuePair.Value.failed)
		{
			keyValuePair.Value.UpdateAchievement();
			if (keyValuePair.Value.success && !keyValuePair.Value.failed)
			{
				ColonyAchievementTracker.UnlockPlatformAchievement(keyValuePair.Key);
				this.completedAchievementsToDisplay.Add(keyValuePair.Key);
				this.TriggerNewAchievementCompleted(keyValuePair.Key, null);
				RetireColonyUtility.SaveColonySummaryData();
			}
		}
	}

	// Token: 0x06003917 RID: 14615 RVA: 0x0013C744 File Offset: 0x0013A944
	private void CheckAchievements(object data = null)
	{
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			if (!keyValuePair.Value.success && !keyValuePair.Value.failed)
			{
				keyValuePair.Value.UpdateAchievement();
				if (keyValuePair.Value.success && !keyValuePair.Value.failed)
				{
					ColonyAchievementTracker.UnlockPlatformAchievement(keyValuePair.Key);
					this.completedAchievementsToDisplay.Add(keyValuePair.Key);
					this.TriggerNewAchievementCompleted(keyValuePair.Key, null);
				}
			}
		}
		RetireColonyUtility.SaveColonySummaryData();
	}

	// Token: 0x06003918 RID: 14616 RVA: 0x0013C80C File Offset: 0x0013AA0C
	private static void UnlockPlatformAchievement(string achievement_id)
	{
		if (DebugHandler.InstantBuildMode)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: instant build mode", new object[] { achievement_id });
			return;
		}
		if (SaveGame.Instance.sandboxEnabled)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: sandbox mode", new object[] { achievement_id });
			return;
		}
		if (Game.Instance.debugWasUsed)
		{
			global::Debug.LogWarningFormat("UnlockPlatformAchievement {0} skipping: debug was used.", new object[] { achievement_id });
			return;
		}
		ColonyAchievement colonyAchievement = Db.Get().ColonyAchievements.Get(achievement_id);
		if (colonyAchievement != null && !string.IsNullOrEmpty(colonyAchievement.platformAchievementId))
		{
			if (SteamAchievementService.Instance)
			{
				SteamAchievementService.Instance.Unlock(colonyAchievement.platformAchievementId);
				return;
			}
			global::Debug.LogWarningFormat("Steam achievement [{0}] was achieved, but achievement service was null", new object[] { colonyAchievement.platformAchievementId });
		}
	}

	// Token: 0x06003919 RID: 14617 RVA: 0x0013C8CE File Offset: 0x0013AACE
	public void DebugTriggerAchievement(string id)
	{
		this.achievements[id].failed = false;
		this.achievements[id].success = true;
	}

	// Token: 0x0600391A RID: 14618 RVA: 0x0013C8F4 File Offset: 0x0013AAF4
	private void BeginVictorySequence(string achievementID)
	{
		RootMenu.Instance.canTogglePauseScreen = false;
		CameraController.Instance.DisableUserCameraControl = true;
		if (!SpeedControlScreen.Instance.IsPaused)
		{
			SpeedControlScreen.Instance.Pause(false, false);
		}
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().VictoryMessageSnapshot);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().MuteDynamicMusicSnapshot);
		this.ToggleVictoryUI(true);
		StoryMessageScreen component = GameScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.StoryMessageScreen.gameObject, null, GameScreenManager.UIRenderTarget.ScreenSpaceOverlay).GetComponent<StoryMessageScreen>();
		component.restoreInterfaceOnClose = false;
		component.title = COLONY_ACHIEVEMENTS.PRE_VICTORY_MESSAGE_HEADER;
		component.body = string.Format(COLONY_ACHIEVEMENTS.PRE_VICTORY_MESSAGE_BODY, "<b>" + Db.Get().ColonyAchievements.Get(achievementID).Name + "</b>\n" + Db.Get().ColonyAchievements.Get(achievementID).description);
		component.Show(true);
		CameraController.Instance.SetWorldInteractive(false);
		component.OnClose = (global::System.Action)Delegate.Combine(component.OnClose, new global::System.Action(delegate
		{
			SpeedControlScreen.Instance.SetSpeed(1);
			if (!SpeedControlScreen.Instance.IsPaused)
			{
				SpeedControlScreen.Instance.Pause(false, false);
			}
			CameraController.Instance.SetWorldInteractive(true);
			Db.Get().ColonyAchievements.Get(achievementID).victorySequence(this);
		}));
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x0013CA38 File Offset: 0x0013AC38
	public bool IsAchievementUnlocked(ColonyAchievement achievement)
	{
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			if (keyValuePair.Key == achievement.Id)
			{
				if (keyValuePair.Value.success)
				{
					return true;
				}
				keyValuePair.Value.UpdateAchievement();
				return keyValuePair.Value.success;
			}
		}
		return false;
	}

	// Token: 0x0600391C RID: 14620 RVA: 0x0013CAC8 File Offset: 0x0013ACC8
	protected override void OnCleanUp()
	{
		this.victorySchedulerHandle.ClearScheduler();
		Game.Instance.Unsubscribe(this.forceCheckAchievementHandle);
		this.checkAchievementsHandle.ClearScheduler();
		base.OnCleanUp();
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x0013CAF8 File Offset: 0x0013ACF8
	private void TriggerNewAchievementCompleted(string achievement, GameObject cameraTarget = null)
	{
		this.unlockedAchievementMetric[ColonyAchievementTracker.UnlockedAchievementKey] = achievement;
		ThreadedHttps<KleiMetrics>.Instance.SendEvent(this.unlockedAchievementMetric, "TriggerNewAchievementCompleted");
		bool flag = false;
		if (Db.Get().ColonyAchievements.Get(achievement).isVictoryCondition)
		{
			flag = true;
			this.BeginVictorySequence(achievement);
		}
		if (!flag)
		{
			AchievementEarnedMessage achievementEarnedMessage = new AchievementEarnedMessage();
			Messenger.Instance.QueueMessage(achievementEarnedMessage);
		}
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x0013CB64 File Offset: 0x0013AD64
	private void ToggleVictoryUI(bool victoryUIActive)
	{
		List<KScreen> list = new List<KScreen>();
		list.Add(NotificationScreen.Instance);
		list.Add(OverlayMenu.Instance);
		if (PlanScreen.Instance != null)
		{
			list.Add(PlanScreen.Instance);
		}
		if (BuildMenu.Instance != null)
		{
			list.Add(BuildMenu.Instance);
		}
		list.Add(ManagementMenu.Instance);
		list.Add(ToolMenu.Instance);
		list.Add(ToolMenu.Instance.PriorityScreen);
		list.Add(ResourceCategoryScreen.Instance);
		list.Add(TopLeftControlScreen.Instance);
		list.Add(global::DateTime.Instance);
		list.Add(BuildWatermark.Instance);
		list.Add(HoverTextScreen.Instance);
		list.Add(DetailsScreen.Instance);
		list.Add(DebugPaintElementScreen.Instance);
		list.Add(DebugBaseTemplateButton.Instance);
		list.Add(StarmapScreen.Instance);
		foreach (KScreen kscreen in list)
		{
			if (kscreen != null)
			{
				kscreen.Show(!victoryUIActive);
			}
		}
	}

	// Token: 0x0600391F RID: 14623 RVA: 0x0013CC94 File Offset: 0x0013AE94
	public void Serialize(BinaryWriter writer)
	{
		writer.Write(this.achievements.Count);
		foreach (KeyValuePair<string, ColonyAchievementStatus> keyValuePair in this.achievements)
		{
			writer.WriteKleiString(keyValuePair.Key);
			keyValuePair.Value.Serialize(writer);
		}
	}

	// Token: 0x06003920 RID: 14624 RVA: 0x0013CD0C File Offset: 0x0013AF0C
	public void Deserialize(IReader reader)
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 10))
		{
			return;
		}
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			string text = reader.ReadKleiString();
			ColonyAchievementStatus colonyAchievementStatus = ColonyAchievementStatus.Deserialize(reader, text);
			if (Db.Get().ColonyAchievements.Exists(text))
			{
				this.achievements.Add(text, colonyAchievementStatus);
			}
		}
	}

	// Token: 0x06003921 RID: 14625 RVA: 0x0013CD74 File Offset: 0x0013AF74
	public void LogFetchChore(GameObject fetcher, ChoreType choreType)
	{
		if (choreType == Db.Get().ChoreTypes.StorageFetch || choreType == Db.Get().ChoreTypes.BuildFetch || choreType == Db.Get().ChoreTypes.RepairFetch || choreType == Db.Get().ChoreTypes.FoodFetch || choreType == Db.Get().ChoreTypes.Transport)
		{
			return;
		}
		Dictionary<int, int> dictionary = null;
		if (fetcher.GetComponent<SolidTransferArm>() != null)
		{
			dictionary = this.fetchAutomatedChoreDeliveries;
		}
		else if (fetcher.GetComponent<MinionIdentity>() != null)
		{
			dictionary = this.fetchDupeChoreDeliveries;
		}
		if (dictionary != null)
		{
			int cycle = GameClock.Instance.GetCycle();
			if (!dictionary.ContainsKey(cycle))
			{
				dictionary.Add(cycle, 0);
			}
			Dictionary<int, int> dictionary2 = dictionary;
			int num = cycle;
			int num2 = dictionary2[num];
			dictionary2[num] = num2 + 1;
		}
	}

	// Token: 0x06003922 RID: 14626 RVA: 0x0013CE3D File Offset: 0x0013B03D
	public void LogCritterTamed(Tag prefabId)
	{
		this.tamedCritterTypes.Add(prefabId);
	}

	// Token: 0x06003923 RID: 14627 RVA: 0x0013CE4C File Offset: 0x0013B04C
	public void LogSuitChore(ChoreDriver driver)
	{
		if (driver == null || driver.GetComponent<MinionIdentity>() == null)
		{
			return;
		}
		bool flag = false;
		foreach (AssignableSlotInstance assignableSlotInstance in driver.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			Equippable equippable = ((EquipmentSlotInstance)assignableSlotInstance).assignable as Equippable;
			if (equippable && equippable.GetComponent<KPrefabID>().IsAnyPrefabID(ColonyAchievementTracker.SuitTags))
			{
				flag = true;
				break;
			}
		}
		if (flag)
		{
			int cycle = GameClock.Instance.GetCycle();
			int instanceID = driver.GetComponent<KPrefabID>().InstanceID;
			if (!this.dupesCompleteChoresInSuits.ContainsKey(cycle))
			{
				this.dupesCompleteChoresInSuits.Add(cycle, new List<int> { instanceID });
				return;
			}
			if (!this.dupesCompleteChoresInSuits[cycle].Contains(instanceID))
			{
				this.dupesCompleteChoresInSuits[cycle].Add(instanceID);
			}
		}
	}

	// Token: 0x06003924 RID: 14628 RVA: 0x0013CF54 File Offset: 0x0013B154
	public void LogAnalyzedSeed(Tag seed)
	{
		this.analyzedSeeds.Add(seed);
	}

	// Token: 0x06003925 RID: 14629 RVA: 0x0013CF64 File Offset: 0x0013B164
	public void OnNewDay(object data)
	{
		foreach (MinionStorage minionStorage in Components.MinionStorages.Items)
		{
			if (minionStorage.GetComponent<CommandModule>() != null)
			{
				List<MinionStorage.Info> storedMinionInfo = minionStorage.GetStoredMinionInfo();
				if (storedMinionInfo.Count > 0)
				{
					int cycle = GameClock.Instance.GetCycle();
					if (!this.dupesCompleteChoresInSuits.ContainsKey(cycle))
					{
						this.dupesCompleteChoresInSuits.Add(cycle, new List<int>());
					}
					for (int i = 0; i < storedMinionInfo.Count; i++)
					{
						KPrefabID kprefabID = storedMinionInfo[i].serializedMinion.Get();
						if (kprefabID != null)
						{
							this.dupesCompleteChoresInSuits[cycle].Add(kprefabID.InstanceID);
						}
					}
				}
			}
		}
		if (DlcManager.IsExpansion1Active())
		{
			SurviveARocketWithMinimumMorale surviveARocketWithMinimumMorale = Db.Get().ColonyAchievements.SurviveInARocket.requirementChecklist[0] as SurviveARocketWithMinimumMorale;
			if (surviveARocketWithMinimumMorale != null)
			{
				float minimumMorale = surviveARocketWithMinimumMorale.minimumMorale;
				int numberOfCycles = surviveARocketWithMinimumMorale.numberOfCycles;
				foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
				{
					if (worldContainer.IsModuleInterior)
					{
						if (!this.cyclesRocketDupeMoraleAboveRequirement.ContainsKey(worldContainer.id))
						{
							this.cyclesRocketDupeMoraleAboveRequirement.Add(worldContainer.id, 0);
						}
						if (worldContainer.GetComponent<Clustercraft>().Status != Clustercraft.CraftStatus.Grounded)
						{
							List<MinionIdentity> worldItems = Components.MinionIdentities.GetWorldItems(worldContainer.id, false);
							bool flag = worldItems.Count > 0;
							foreach (MinionIdentity minionIdentity in worldItems)
							{
								if (Db.Get().Attributes.QualityOfLife.Lookup(minionIdentity).GetTotalValue() < minimumMorale)
								{
									flag = false;
									break;
								}
							}
							this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] = (flag ? (this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] + 1) : 0);
						}
						else if (this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] < numberOfCycles)
						{
							this.cyclesRocketDupeMoraleAboveRequirement[worldContainer.id] = 0;
						}
					}
				}
			}
		}
	}

	// Token: 0x04002268 RID: 8808
	public Dictionary<string, ColonyAchievementStatus> achievements = new Dictionary<string, ColonyAchievementStatus>();

	// Token: 0x04002269 RID: 8809
	[Serialize]
	public Dictionary<int, int> fetchAutomatedChoreDeliveries = new Dictionary<int, int>();

	// Token: 0x0400226A RID: 8810
	[Serialize]
	public Dictionary<int, int> fetchDupeChoreDeliveries = new Dictionary<int, int>();

	// Token: 0x0400226B RID: 8811
	[Serialize]
	public Dictionary<int, List<int>> dupesCompleteChoresInSuits = new Dictionary<int, List<int>>();

	// Token: 0x0400226C RID: 8812
	[Serialize]
	public HashSet<Tag> tamedCritterTypes = new HashSet<Tag>();

	// Token: 0x0400226D RID: 8813
	[Serialize]
	public bool defrostedDuplicant;

	// Token: 0x0400226E RID: 8814
	[Serialize]
	public HashSet<Tag> analyzedSeeds = new HashSet<Tag>();

	// Token: 0x0400226F RID: 8815
	[Serialize]
	public float totalMaterialsHarvestFromPOI;

	// Token: 0x04002270 RID: 8816
	[Serialize]
	public float radBoltTravelDistance;

	// Token: 0x04002271 RID: 8817
	[Serialize]
	public bool harvestAHiveWithoutGettingStung;

	// Token: 0x04002272 RID: 8818
	[Serialize]
	public Dictionary<int, int> cyclesRocketDupeMoraleAboveRequirement = new Dictionary<int, int>();

	// Token: 0x04002273 RID: 8819
	[Serialize]
	public bool efficientlyGatheredData;

	// Token: 0x04002274 RID: 8820
	[Serialize]
	public bool fullyBoostedBionic;

	// Token: 0x04002275 RID: 8821
	[Serialize]
	public int deadDupeCounter;

	// Token: 0x04002276 RID: 8822
	[Serialize]
	private int geothermalProgress;

	// Token: 0x04002277 RID: 8823
	[Serialize]
	public ColonyAchievementTracker.LargeImpactorState largeImpactorState;

	// Token: 0x04002278 RID: 8824
	[Serialize]
	public float LargeImpactorBackgroundScale = 0.6f;

	// Token: 0x04002279 RID: 8825
	[Serialize]
	public int largeImpactorLandedCycle = -1;

	// Token: 0x0400227A RID: 8826
	private const int GEO_DISCOVERED_BIT = 1;

	// Token: 0x0400227B RID: 8827
	private const int GEO_CONTROLLER_REPAIRED_BIT = 2;

	// Token: 0x0400227C RID: 8828
	private const int GEO_CONTROLLER_VENTED_BIT = 4;

	// Token: 0x0400227D RID: 8829
	private const int GEO_CLEARED_ENTOMBED_BIT = 8;

	// Token: 0x0400227E RID: 8830
	private const int GEO_VICTORY_ACK_BIT = 16;

	// Token: 0x0400227F RID: 8831
	private SchedulerHandle checkAchievementsHandle;

	// Token: 0x04002280 RID: 8832
	private int forceCheckAchievementHandle = -1;

	// Token: 0x04002281 RID: 8833
	[Serialize]
	private int updatingAchievement;

	// Token: 0x04002282 RID: 8834
	[Serialize]
	private List<string> completedAchievementsToDisplay = new List<string>();

	// Token: 0x04002283 RID: 8835
	private SchedulerHandle victorySchedulerHandle;

	// Token: 0x04002284 RID: 8836
	public static readonly string UnlockedAchievementKey = "UnlockedAchievement";

	// Token: 0x04002285 RID: 8837
	private Dictionary<string, object> unlockedAchievementMetric = new Dictionary<string, object> { 
	{
		ColonyAchievementTracker.UnlockedAchievementKey,
		null
	} };

	// Token: 0x04002286 RID: 8838
	private static readonly Tag[] SuitTags = new Tag[]
	{
		GameTags.AtmoSuit,
		GameTags.JetSuit,
		GameTags.LeadSuit
	};

	// Token: 0x04002287 RID: 8839
	private static readonly EventSystem.IntraObjectHandler<ColonyAchievementTracker> OnNewDayDelegate = new EventSystem.IntraObjectHandler<ColonyAchievementTracker>(delegate(ColonyAchievementTracker component, object data)
	{
		component.OnNewDay(data);
	});

	// Token: 0x02001789 RID: 6025
	public enum LargeImpactorState
	{
		// Token: 0x040075F7 RID: 30199
		Alive,
		// Token: 0x040075F8 RID: 30200
		Defeated,
		// Token: 0x040075F9 RID: 30201
		Landed
	}
}
