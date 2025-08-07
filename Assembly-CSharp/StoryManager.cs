using System;
using System.Collections.Generic;
using Database;
using Klei.CustomSettings;
using KSerialization;
using UnityEngine;

// Token: 0x02000BA5 RID: 2981
[SerializationConfig(MemberSerialization.OptIn)]
public class StoryManager : KMonoBehaviour
{
	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x060058EA RID: 22762 RVA: 0x00202158 File Offset: 0x00200358
	// (set) Token: 0x060058EB RID: 22763 RVA: 0x0020215F File Offset: 0x0020035F
	public static StoryManager Instance { get; private set; }

	// Token: 0x060058EC RID: 22764 RVA: 0x00202167 File Offset: 0x00200367
	public static IReadOnlyList<StoryManager.StoryTelemetry> GetTelemetry()
	{
		return StoryManager.storyTelemetry;
	}

	// Token: 0x060058ED RID: 22765 RVA: 0x00202170 File Offset: 0x00200370
	protected override void OnPrefabInit()
	{
		StoryManager.Instance = this;
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDayStarted));
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Combine(instance.OnLoad, new Action<Game.GameSaveData>(this.OnGameLoaded));
	}

	// Token: 0x060058EE RID: 22766 RVA: 0x002021C8 File Offset: 0x002003C8
	protected override void OnCleanUp()
	{
		GameClock.Instance.Unsubscribe(631075836, new Action<object>(this.OnNewDayStarted));
		Game instance = Game.Instance;
		instance.OnLoad = (Action<Game.GameSaveData>)Delegate.Remove(instance.OnLoad, new Action<Game.GameSaveData>(this.OnGameLoaded));
	}

	// Token: 0x060058EF RID: 22767 RVA: 0x00202218 File Offset: 0x00200418
	public void InitialSaveSetup()
	{
		this.highestStoryCoordinateWhenGenerated = Db.Get().Stories.GetHighestCoordinate();
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			foreach (string text in worldContainer.StoryTraitIds)
			{
				Story storyFromStoryTrait = Db.Get().Stories.GetStoryFromStoryTrait(text);
				this.CreateStory(storyFromStoryTrait, worldContainer.id);
			}
		}
		this.LogInitialSaveSetup();
	}

	// Token: 0x060058F0 RID: 22768 RVA: 0x002022E0 File Offset: 0x002004E0
	public StoryInstance CreateStory(string id, int worldId)
	{
		Story story = Db.Get().Stories.Get(id);
		return this.CreateStory(story, worldId);
	}

	// Token: 0x060058F1 RID: 22769 RVA: 0x00202308 File Offset: 0x00200508
	public StoryInstance CreateStory(Story story, int worldId)
	{
		StoryInstance storyInstance = new StoryInstance(story, worldId);
		this._stories.Add(story.HashId, storyInstance);
		StoryManager.InitTelemetry(storyInstance);
		if (story.autoStart)
		{
			this.BeginStoryEvent(story);
		}
		return storyInstance;
	}

	// Token: 0x060058F2 RID: 22770 RVA: 0x00202345 File Offset: 0x00200545
	public StoryInstance GetStoryInstance(Story story)
	{
		return this.GetStoryInstance(story.HashId);
	}

	// Token: 0x060058F3 RID: 22771 RVA: 0x00202354 File Offset: 0x00200554
	public StoryInstance GetStoryInstance(int hash)
	{
		StoryInstance storyInstance;
		this._stories.TryGetValue(hash, out storyInstance);
		return storyInstance;
	}

	// Token: 0x060058F4 RID: 22772 RVA: 0x00202371 File Offset: 0x00200571
	public Dictionary<int, StoryInstance> GetStoryInstances()
	{
		return this._stories;
	}

	// Token: 0x060058F5 RID: 22773 RVA: 0x00202379 File Offset: 0x00200579
	public int GetHighestCoordinate()
	{
		return this.highestStoryCoordinateWhenGenerated;
	}

	// Token: 0x060058F6 RID: 22774 RVA: 0x00202381 File Offset: 0x00200581
	private string GetCompleteUnlockId(string id)
	{
		return id + "_STORY_COMPLETE";
	}

	// Token: 0x060058F7 RID: 22775 RVA: 0x0020238E File Offset: 0x0020058E
	public void ForceCreateStory(Story story, int worldId)
	{
		if (this.GetStoryInstance(story.HashId) == null)
		{
			this.CreateStory(story, worldId);
		}
	}

	// Token: 0x060058F8 RID: 22776 RVA: 0x002023A8 File Offset: 0x002005A8
	public void DiscoverStoryEvent(Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || this.CheckState(StoryInstance.State.DISCOVERED, story))
		{
			return;
		}
		storyInstance.CurrentState = StoryInstance.State.DISCOVERED;
	}

	// Token: 0x060058F9 RID: 22777 RVA: 0x002023D8 File Offset: 0x002005D8
	public void BeginStoryEvent(Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || this.CheckState(StoryInstance.State.IN_PROGRESS, story))
		{
			return;
		}
		storyInstance.CurrentState = StoryInstance.State.IN_PROGRESS;
	}

	// Token: 0x060058FA RID: 22778 RVA: 0x00202407 File Offset: 0x00200607
	public void CompleteStoryEvent(Story story, MonoBehaviour keepsakeSpawnTarget, FocusTargetSequence.Data sequenceData)
	{
		if (this.GetStoryInstance(story.HashId) == null || this.CheckState(StoryInstance.State.COMPLETE, story))
		{
			return;
		}
		FocusTargetSequence.Start(keepsakeSpawnTarget, sequenceData);
	}

	// Token: 0x060058FB RID: 22779 RVA: 0x0020242C File Offset: 0x0020062C
	public void CompleteStoryEvent(Story story, Vector3 keepsakeSpawnPosition)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null)
		{
			return;
		}
		GameObject prefab = Assets.GetPrefab(storyInstance.GetStory().keepsakePrefabId);
		if (prefab != null)
		{
			keepsakeSpawnPosition.z = Grid.GetLayerZ(Grid.SceneLayer.Ore);
			GameObject gameObject = Util.KInstantiate(prefab, keepsakeSpawnPosition);
			gameObject.SetActive(true);
			new UpgradeFX.Instance(gameObject.GetComponent<KMonoBehaviour>(), new Vector3(0f, -0.5f, -0.1f)).StartSM();
		}
		storyInstance.CurrentState = StoryInstance.State.COMPLETE;
		Game.Instance.unlocks.Unlock(this.GetCompleteUnlockId(story.Id), true);
	}

	// Token: 0x060058FC RID: 22780 RVA: 0x002024CC File Offset: 0x002006CC
	public bool CheckState(StoryInstance.State state, Story story)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		return storyInstance != null && storyInstance.CurrentState >= state;
	}

	// Token: 0x060058FD RID: 22781 RVA: 0x002024F7 File Offset: 0x002006F7
	public bool IsStoryComplete(Story story)
	{
		return this.CheckState(StoryInstance.State.COMPLETE, story);
	}

	// Token: 0x060058FE RID: 22782 RVA: 0x00202501 File Offset: 0x00200701
	public bool IsStoryCompleteGlobal(Story story)
	{
		return Game.Instance.unlocks.IsUnlocked(this.GetCompleteUnlockId(story.Id));
	}

	// Token: 0x060058FF RID: 22783 RVA: 0x00202520 File Offset: 0x00200720
	public StoryInstance DisplayPopup(Story story, StoryManager.PopupInfo info, global::System.Action popupCB = null, Notification.ClickCallback notificationCB = null)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		if (storyInstance == null || storyInstance.HasDisplayedPopup(info.PopupType))
		{
			return null;
		}
		EventInfoData eventInfoData = EventInfoDataHelper.GenerateStoryTraitData(info.Title, info.Description, info.CloseButtonText, info.TextureName, info.PopupType, info.CloseButtonToolTip, info.Minions, popupCB);
		if (info.extraButtons != null && info.extraButtons.Length != 0)
		{
			foreach (StoryManager.ExtraButtonInfo extraButtonInfo in info.extraButtons)
			{
				eventInfoData.SimpleOption(extraButtonInfo.ButtonText, extraButtonInfo.OnButtonClick).tooltip = extraButtonInfo.ButtonToolTip;
			}
		}
		Notification notification = null;
		if (!info.DisplayImmediate)
		{
			notification = EventInfoScreen.CreateNotification(eventInfoData, notificationCB);
		}
		storyInstance.SetPopupData(info, eventInfoData, notification);
		return storyInstance;
	}

	// Token: 0x06005900 RID: 22784 RVA: 0x002025F0 File Offset: 0x002007F0
	public bool HasDisplayedPopup(Story story, EventInfoDataHelper.PopupType type)
	{
		StoryInstance storyInstance = this.GetStoryInstance(story.HashId);
		return storyInstance != null && storyInstance.HasDisplayedPopup(type);
	}

	// Token: 0x06005901 RID: 22785 RVA: 0x00202618 File Offset: 0x00200818
	private void LogInitialSaveSetup()
	{
		int num = 0;
		StoryManager.StoryCreationTelemetry[] array = new StoryManager.StoryCreationTelemetry[CustomGameSettings.Instance.CurrentStoryLevelsBySetting.Count];
		foreach (KeyValuePair<string, string> keyValuePair in CustomGameSettings.Instance.CurrentStoryLevelsBySetting)
		{
			array[num] = new StoryManager.StoryCreationTelemetry
			{
				StoryId = keyValuePair.Key,
				Enabled = CustomGameSettings.Instance.IsStoryActive(keyValuePair.Key, keyValuePair.Value)
			};
			num++;
		}
		OniMetrics.LogEvent(OniMetrics.Event.NewSave, "StoryTraitsCreation", array);
	}

	// Token: 0x06005902 RID: 22786 RVA: 0x002026BC File Offset: 0x002008BC
	private void OnNewDayStarted(object _)
	{
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, "SavedHighestStoryCoordinate", this.highestStoryCoordinateWhenGenerated);
		OniMetrics.LogEvent(OniMetrics.Event.EndOfCycle, "StoryTraits", StoryManager.storyTelemetry);
	}

	// Token: 0x06005903 RID: 22787 RVA: 0x002026E4 File Offset: 0x002008E4
	private static void InitTelemetry(StoryInstance story)
	{
		WorldContainer world = ClusterManager.Instance.GetWorld(story.worldId);
		if (world == null)
		{
			return;
		}
		story.Telemetry.StoryId = story.storyId;
		story.Telemetry.WorldId = world.worldName;
		StoryManager.storyTelemetry.Add(story.Telemetry);
	}

	// Token: 0x06005904 RID: 22788 RVA: 0x00202740 File Offset: 0x00200940
	private void OnGameLoaded(object _)
	{
		StoryManager.storyTelemetry.Clear();
		foreach (KeyValuePair<int, StoryInstance> keyValuePair in this._stories)
		{
			StoryManager.InitTelemetry(keyValuePair.Value);
		}
		CustomGameSettings.Instance.DisableAllStories();
		foreach (KeyValuePair<int, StoryInstance> keyValuePair2 in this._stories)
		{
			SettingConfig settingConfig;
			if (keyValuePair2.Value.Telemetry.Retrofitted < 0f && CustomGameSettings.Instance.StorySettings.TryGetValue(keyValuePair2.Value.storyId, out settingConfig))
			{
				CustomGameSettings.Instance.SetStorySetting(settingConfig, true);
			}
		}
	}

	// Token: 0x06005905 RID: 22789 RVA: 0x0020282C File Offset: 0x00200A2C
	public static void DestroyInstance()
	{
		StoryManager.storyTelemetry.Clear();
		StoryManager.Instance = null;
	}

	// Token: 0x04003B16 RID: 15126
	public const int BEFORE_STORIES = -2;

	// Token: 0x04003B18 RID: 15128
	private static List<StoryManager.StoryTelemetry> storyTelemetry = new List<StoryManager.StoryTelemetry>();

	// Token: 0x04003B19 RID: 15129
	[Serialize]
	private Dictionary<int, StoryInstance> _stories = new Dictionary<int, StoryInstance>();

	// Token: 0x04003B1A RID: 15130
	[Serialize]
	private int highestStoryCoordinateWhenGenerated = -2;

	// Token: 0x04003B1B RID: 15131
	private const string STORY_TRAIT_KEY = "StoryTraits";

	// Token: 0x04003B1C RID: 15132
	private const string STORY_CREATION_KEY = "StoryTraitsCreation";

	// Token: 0x04003B1D RID: 15133
	private const string STORY_COORDINATE_KEY = "SavedHighestStoryCoordinate";

	// Token: 0x02001CD0 RID: 7376
	public struct ExtraButtonInfo
	{
		// Token: 0x04008768 RID: 34664
		public string ButtonText;

		// Token: 0x04008769 RID: 34665
		public string ButtonToolTip;

		// Token: 0x0400876A RID: 34666
		public global::System.Action OnButtonClick;
	}

	// Token: 0x02001CD1 RID: 7377
	public struct PopupInfo
	{
		// Token: 0x0400876B RID: 34667
		public string Title;

		// Token: 0x0400876C RID: 34668
		public string Description;

		// Token: 0x0400876D RID: 34669
		public string CloseButtonText;

		// Token: 0x0400876E RID: 34670
		public string CloseButtonToolTip;

		// Token: 0x0400876F RID: 34671
		public StoryManager.ExtraButtonInfo[] extraButtons;

		// Token: 0x04008770 RID: 34672
		public string TextureName;

		// Token: 0x04008771 RID: 34673
		public GameObject[] Minions;

		// Token: 0x04008772 RID: 34674
		public bool DisplayImmediate;

		// Token: 0x04008773 RID: 34675
		public EventInfoDataHelper.PopupType PopupType;
	}

	// Token: 0x02001CD2 RID: 7378
	[SerializationConfig(MemberSerialization.OptIn)]
	public class StoryTelemetry : ISaveLoadable
	{
		// Token: 0x0600AC4F RID: 44111 RVA: 0x003C18B0 File Offset: 0x003BFAB0
		public void LogStateChange(StoryInstance.State state, float time)
		{
			switch (state)
			{
			case StoryInstance.State.RETROFITTED:
				this.Retrofitted = ((this.Retrofitted >= 0f) ? this.Retrofitted : time);
				return;
			case StoryInstance.State.NOT_STARTED:
				break;
			case StoryInstance.State.DISCOVERED:
				this.Discovered = ((this.Discovered >= 0f) ? this.Discovered : time);
				return;
			case StoryInstance.State.IN_PROGRESS:
				this.InProgress = ((this.InProgress >= 0f) ? this.InProgress : time);
				return;
			case StoryInstance.State.COMPLETE:
				this.Completed = ((this.Completed >= 0f) ? this.Completed : time);
				break;
			default:
				return;
			}
		}

		// Token: 0x04008774 RID: 34676
		public string StoryId;

		// Token: 0x04008775 RID: 34677
		public string WorldId;

		// Token: 0x04008776 RID: 34678
		[Serialize]
		public float Retrofitted = -1f;

		// Token: 0x04008777 RID: 34679
		[Serialize]
		public float Discovered = -1f;

		// Token: 0x04008778 RID: 34680
		[Serialize]
		public float InProgress = -1f;

		// Token: 0x04008779 RID: 34681
		[Serialize]
		public float Completed = -1f;
	}

	// Token: 0x02001CD3 RID: 7379
	public class StoryCreationTelemetry
	{
		// Token: 0x0400877A RID: 34682
		public string StoryId;

		// Token: 0x0400877B RID: 34683
		public bool Enabled;
	}
}
