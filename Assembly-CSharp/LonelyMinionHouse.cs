using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200077B RID: 1915
public class LonelyMinionHouse : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>
{
	// Token: 0x06003260 RID: 12896 RVA: 0x0011C0BC File Offset: 0x0011A2BC
	private bool ValidateOperationalTransition(LonelyMinionHouse.Instance smi)
	{
		Operational component = smi.GetComponent<Operational>();
		bool flag = smi.IsInsideState(smi.sm.Active);
		return component != null && flag != component.IsOperational;
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x0011C0F9 File Offset: 0x0011A2F9
	private static bool AllQuestsComplete(LonelyMinionHouse.Instance smi)
	{
		return 1f - smi.sm.QuestProgress.Get(smi) <= Mathf.Epsilon;
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x0011C11C File Offset: 0x0011A31C
	public static void EvaluateLights(LonelyMinionHouse.Instance smi, float dt)
	{
		bool flag = smi.IsInsideState(smi.sm.Active);
		QuestInstance instance = QuestManager.GetInstance(smi.QuestOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
		if (!flag || !smi.Light.enabled || instance.IsComplete)
		{
			return;
		}
		bool flag2;
		bool flag3;
		instance.TrackProgress(new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.PowerCriteriaId,
			CurrentValue = instance.GetCurrentValue(LonelyMinionConfig.PowerCriteriaId, 0) + dt
		}, out flag2, out flag3);
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x0011C1A4 File Offset: 0x0011A3A4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inactive;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.Update(new Action<LonelyMinionHouse.Instance, float>(LonelyMinionHouse.EvaluateLights), UpdateRate.SIM_1000ms, false);
		this.Inactive.EventTransition(GameHashes.OperationalChanged, this.Active, new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Active.Enter(delegate(LonelyMinionHouse.Instance smi)
		{
			smi.OnPoweredStateChanged(smi.GetComponent<NonEssentialEnergyConsumer>().IsPowered);
		}).Exit(delegate(LonelyMinionHouse.Instance smi)
		{
			smi.OnPoweredStateChanged(smi.GetComponent<NonEssentialEnergyConsumer>().IsPowered);
		}).OnSignal(this.CompleteStory, this.Active.StoryComplete, new Func<LonelyMinionHouse.Instance, bool>(LonelyMinionHouse.AllQuestsComplete))
			.EventTransition(GameHashes.OperationalChanged, this.Inactive, new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Transition.ConditionCallback(this.ValidateOperationalTransition));
		this.Active.StoryComplete.Enter(new StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State.Callback(LonelyMinionHouse.ActiveStates.OnEnterStoryComplete));
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x0011C2A8 File Offset: 0x0011A4A8
	public static float CalculateAverageDecor(Extents area)
	{
		float num = 0f;
		int num2 = Grid.XYToCell(area.x, area.y);
		for (int i = 0; i < area.width * area.height; i++)
		{
			int num3 = Grid.OffsetCell(num2, i % area.width, i / area.width);
			num += Grid.Decor[num3];
		}
		return num / (float)(area.width * area.height);
	}

	// Token: 0x04001E2F RID: 7727
	public GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State Inactive;

	// Token: 0x04001E30 RID: 7728
	public LonelyMinionHouse.ActiveStates Active;

	// Token: 0x04001E31 RID: 7729
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Signal MailDelivered;

	// Token: 0x04001E32 RID: 7730
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.Signal CompleteStory;

	// Token: 0x04001E33 RID: 7731
	public StateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.FloatParameter QuestProgress;

	// Token: 0x0200165D RID: 5725
	public class Def : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>.TraitDef
	{
	}

	// Token: 0x0200165E RID: 5726
	public new class Instance : StoryTraitStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, LonelyMinionHouse.Def>.TraitInstance, ICheckboxListGroupControl
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060094BE RID: 38078 RVA: 0x0036FCCE File Offset: 0x0036DECE
		public HashedString QuestOwnerId
		{
			get
			{
				return this.questOwnerId;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060094BF RID: 38079 RVA: 0x0036FCD6 File Offset: 0x0036DED6
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animControllers[0];
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060094C0 RID: 38080 RVA: 0x0036FCE0 File Offset: 0x0036DEE0
		public KBatchedAnimController LightsController
		{
			get
			{
				return this.animControllers[1];
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060094C1 RID: 38081 RVA: 0x0036FCEA File Offset: 0x0036DEEA
		public KBatchedAnimController BlindsController
		{
			get
			{
				return this.blinds.meterController;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060094C2 RID: 38082 RVA: 0x0036FCF7 File Offset: 0x0036DEF7
		public Light2D Light
		{
			get
			{
				return this.light;
			}
		}

		// Token: 0x060094C3 RID: 38083 RVA: 0x0036FCFF File Offset: 0x0036DEFF
		public Instance(StateMachineController master, LonelyMinionHouse.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060094C4 RID: 38084 RVA: 0x0036FD10 File Offset: 0x0036DF10
		public override void StartSM()
		{
			this.animControllers = base.gameObject.GetComponentsInChildren<KBatchedAnimController>(true);
			this.light = this.LightsController.GetComponent<Light2D>();
			this.light.transform.position += Vector3.forward * Grid.GetLayerZ(Grid.SceneLayer.TransferArm);
			this.light.gameObject.SetActive(true);
			this.lightsLink = new KAnimLink(this.AnimController, this.LightsController);
			Activatable component = base.GetComponent<Activatable>();
			component.SetOffsets(new CellOffset[]
			{
				new CellOffset(-3, 0)
			});
			if (!component.IsActivated)
			{
				Activatable activatable = component;
				activatable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(activatable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
				Activatable activatable2 = component;
				activatable2.onActivate = (global::System.Action)Delegate.Combine(activatable2.onActivate, new global::System.Action(this.StartStoryTrait));
			}
			this.meter = new MeterController(this.AnimController, "meter_storage_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.TransferArm, LonelyMinionHouseConfig.METER_SYMBOLS);
			this.blinds = new MeterController(this.AnimController, "blinds_target", string.Format("{0}_{1}", "meter_blinds", 0), Meter.Offset.UserSpecified, Grid.SceneLayer.TransferArm, LonelyMinionHouseConfig.BLINDS_SYMBOLS);
			KPrefabID component2 = base.GetComponent<KPrefabID>();
			this.questOwnerId = new HashedString(component2.PrefabTag.GetHash());
			this.SpawnMinion();
			if (this.lonelyMinion != null && !this.TryFindMailbox())
			{
				GameScenePartitioner.Instance.AddGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingLayerChanged));
			}
			QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			QuestInstance questInstance = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			QuestInstance questInstance2 = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			QuestInstance questInstance3 = QuestManager.InitializeQuest(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			NonEssentialEnergyConsumer component3 = base.GetComponent<NonEssentialEnergyConsumer>();
			NonEssentialEnergyConsumer nonEssentialEnergyConsumer = component3;
			nonEssentialEnergyConsumer.PoweredStateChanged = (Action<bool>)Delegate.Combine(nonEssentialEnergyConsumer.PoweredStateChanged, new Action<bool>(this.OnPoweredStateChanged));
			this.OnPoweredStateChanged(component3.IsPowered);
			if (this.lonelyMinion == null)
			{
				base.StartSM();
				return;
			}
			base.Subscribe(-592767678, new Action<object>(this.OnBuildingActivated));
			base.StartSM();
			QuestInstance questInstance4 = questInstance;
			questInstance4.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance4.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance questInstance5 = questInstance2;
			questInstance5.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance5.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance questInstance6 = questInstance3;
			questInstance6.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(questInstance6.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			float num = base.sm.QuestProgress.Get(this) * 3f;
			int num2 = (Mathf.Approximately(num, Mathf.Ceil(num)) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 == 0)
			{
				return;
			}
			HashedString[] array = new HashedString[num2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = string.Format("{0}_{1}", "meter_blinds", i);
			}
			this.blinds.meterController.Play(array, KAnim.PlayMode.Once);
		}

		// Token: 0x060094C5 RID: 38085 RVA: 0x0037007C File Offset: 0x0036E27C
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Activatable component = base.GetComponent<Activatable>();
			component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
			component.onActivate = (global::System.Action)Delegate.Remove(component.onActivate, new global::System.Action(this.StartStoryTrait));
			base.Unsubscribe(-592767678, new Action<object>(this.OnBuildingActivated));
		}

		// Token: 0x060094C6 RID: 38086 RVA: 0x003700F4 File Offset: 0x0036E2F4
		private void OnQuestProgressChanged(QuestInstance quest, Quest.State prevState, float delta)
		{
			float num = base.sm.QuestProgress.Get(this);
			num += delta / 3f;
			if (1f - num <= 0.001f)
			{
				num = 1f;
			}
			base.sm.QuestProgress.Set(Mathf.Clamp01(num), this, true);
			this.lonelyMinion.UnlockQuestIdle(quest, prevState, delta);
			this.lonelyMinion.ShowQuestCompleteNotification(quest, prevState, 0f);
			base.gameObject.Trigger(1980521255, null);
			if (!quest.IsComplete)
			{
				return;
			}
			if (num == 1f)
			{
				base.sm.CompleteStory.Trigger(this);
			}
			float num2 = num * 3f;
			int num3 = (Mathf.Approximately(num2, Mathf.Ceil(num2)) ? Mathf.CeilToInt(num2) : Mathf.FloorToInt(num2));
			this.blinds.meterController.Queue(string.Format("{0}_{1}", "meter_blinds", num3 - 1), KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x060094C7 RID: 38087 RVA: 0x003701F9 File Offset: 0x0036E3F9
		public void MailboxContentChanged(GameObject item)
		{
			this.lonelyMinion.sm.Mail.Set(item, this.lonelyMinion, false);
		}

		// Token: 0x060094C8 RID: 38088 RVA: 0x0037021C File Offset: 0x0036E41C
		public override void CompleteEvent()
		{
			if (this.lonelyMinion == null)
			{
				base.smi.AnimController.Play(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Loop, 1f, 0f);
				base.gameObject.AddOrGet<TreeFilterable>();
				base.gameObject.AddOrGet<BuildingEnabledButton>();
				base.gameObject.GetComponent<Deconstructable>().allowDeconstruction = true;
				base.gameObject.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
				base.gameObject.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5));
				Storage component = base.GetComponent<Storage>();
				component.allowItemRemoval = true;
				component.showInUI = true;
				component.showDescriptor = true;
				component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
				this.storageFilter = new FilteredStorage(base.smi.GetComponent<KPrefabID>(), null, null, false, Db.Get().ChoreTypes.StorageFetch);
				this.storageFilter.SetMeter(this.meter);
				this.meter = null;
				RootMenu.Instance.Refresh();
				component.RenotifyAll();
				return;
			}
			List<MinionIdentity> list = new List<MinionIdentity>(Components.LiveMinionIdentities.Items);
			list.Shuffle<MinionIdentity>();
			int num = 3;
			base.def.EventCompleteInfo.Minions = new GameObject[1 + Mathf.Min(num, list.Count)];
			base.def.EventCompleteInfo.Minions[0] = this.lonelyMinion.gameObject;
			int num2 = 0;
			while (num2 < list.Count && num > 0)
			{
				base.def.EventCompleteInfo.Minions[num2 + 1] = list[num2].gameObject;
				num--;
				num2++;
			}
			base.CompleteEvent();
		}

		// Token: 0x060094C9 RID: 38089 RVA: 0x003703CC File Offset: 0x0036E5CC
		public override void OnCompleteStorySequence()
		{
			this.SpawnMinion();
			base.Unsubscribe(-592767678, new Action<object>(this.OnBuildingActivated));
			base.OnCompleteStorySequence();
			QuestInstance instance = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance instance2 = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			instance2.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance2.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			QuestInstance instance3 = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			instance3.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance3.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.OnQuestProgressChanged));
			this.blinds.meterController.Play(this.blinds.meterController.initialAnim, this.blinds.meterController.initialMode, 1f, 0f);
			base.smi.AnimController.Play(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Loop, 1f, 0f);
			base.gameObject.AddOrGet<TreeFilterable>();
			base.gameObject.AddOrGet<BuildingEnabledButton>();
			base.gameObject.GetComponent<Deconstructable>().allowDeconstruction = true;
			base.gameObject.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.None;
			base.gameObject.GetComponent<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5));
			Storage component = base.GetComponent<Storage>();
			component.allowItemRemoval = true;
			component.showInUI = true;
			component.showDescriptor = true;
			component.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(component.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
			this.storageFilter = new FilteredStorage(base.smi.GetComponent<KPrefabID>(), null, null, false, Db.Get().ChoreTypes.StorageFetch);
			this.storageFilter.SetMeter(this.meter);
			this.meter = null;
			RootMenu.Instance.Refresh();
		}

		// Token: 0x060094CA RID: 38090 RVA: 0x003705E4 File Offset: 0x0036E7E4
		private void SpawnMinion()
		{
			if (StoryManager.Instance.IsStoryComplete(Db.Get().Stories.LonelyMinion))
			{
				return;
			}
			if (this.lonelyMinion == null)
			{
				GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(LonelyMinionConfig.ID), base.gameObject, null);
				global::Debug.Assert(gameObject != null);
				gameObject.transform.localPosition = new Vector3(0.54f, 0f, -0.01f);
				gameObject.SetActive(true);
				Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(base.gameObject));
				BuildingDef def = base.GetComponent<Building>().Def;
				this.lonelyMinion = gameObject.GetSMI<LonelyMinion.Instance>();
				this.lonelyMinion.def.QuestOwnerId = this.questOwnerId;
				this.lonelyMinion.def.DecorInspectionArea = new Extents(vector2I.x - Mathf.CeilToInt((float)def.WidthInCells / 2f) + 1, vector2I.y, def.WidthInCells, def.HeightInCells);
				return;
			}
			MinionStartingStats minionStartingStats = new MinionStartingStats(this.lonelyMinion.def.Personality, null, "AncientKnowledge", false);
			minionStartingStats.Traits.Add(Db.Get().traits.TryGet("Chatty"));
			minionStartingStats.voiceIdx = -2;
			string[] all_ATTRIBUTES = DUPLICANTSTATS.ALL_ATTRIBUTES;
			for (int i = 0; i < all_ATTRIBUTES.Length; i++)
			{
				Dictionary<string, int> startingLevels = minionStartingStats.StartingLevels;
				string text = all_ATTRIBUTES[i];
				startingLevels[text] += 7;
			}
			global::UnityEngine.Object.Destroy(this.lonelyMinion.gameObject);
			this.lonelyMinion = null;
			GameObject prefab = Assets.GetPrefab(BaseMinionConfig.GetMinionIDForModel(minionStartingStats.personality.model));
			MinionIdentity minionIdentity = Util.KInstantiate<MinionIdentity>(prefab, null, null);
			minionIdentity.name = prefab.name;
			Immigration.Instance.ApplyDefaultPersonalPriorities(minionIdentity.gameObject);
			minionIdentity.gameObject.SetActive(true);
			minionStartingStats.Apply(minionIdentity.gameObject);
			minionIdentity.arrivalTime += (float)global::UnityEngine.Random.Range(2190, 3102);
			minionIdentity.arrivalTime *= -1f;
			MinionResume component = minionIdentity.GetComponent<MinionResume>();
			for (int j = 0; j < 3; j++)
			{
				component.ForceAddSkillPoint();
			}
			Vector3 vector = base.transform.position + Vector3.left * Grid.CellSizeInMeters * 2f;
			vector.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			minionIdentity.transform.SetPosition(vector);
		}

		// Token: 0x060094CB RID: 38091 RVA: 0x00370878 File Offset: 0x0036EA78
		private bool TryFindMailbox()
		{
			if (base.sm.QuestProgress.Get(this) == 1f)
			{
				return true;
			}
			int num = Grid.PosToCell(base.gameObject);
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(new Extents(num, 10), GameScenePartitioner.Instance.objectLayers[1], pooledList);
			bool flag = false;
			int num2 = 0;
			while (!flag && num2 < pooledList.Count)
			{
				if ((pooledList[num2].obj as GameObject).GetComponent<KPrefabID>().PrefabTag.GetHash() == LonelyMinionMailboxConfig.IdHash.HashValue)
				{
					this.OnBuildingLayerChanged(0, pooledList[num2].obj);
					flag = true;
				}
				num2++;
			}
			pooledList.Recycle();
			return flag;
		}

		// Token: 0x060094CC RID: 38092 RVA: 0x00370934 File Offset: 0x0036EB34
		private void OnBuildingLayerChanged(int cell, object data)
		{
			GameObject gameObject = data as GameObject;
			if (gameObject == null)
			{
				return;
			}
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component.PrefabTag.GetHash() == LonelyMinionMailboxConfig.IdHash.HashValue)
			{
				component.GetComponent<LonelyMinionMailbox>().Initialize(this);
				GameScenePartitioner.Instance.RemoveGlobalLayerListener(GameScenePartitioner.Instance.objectLayers[1], new Action<int, object>(this.OnBuildingLayerChanged));
			}
		}

		// Token: 0x060094CD RID: 38093 RVA: 0x003709A4 File Offset: 0x0036EBA4
		public void OnPoweredStateChanged(bool isPowered)
		{
			this.light.enabled = isPowered && base.GetComponent<Operational>().IsOperational;
			this.LightsController.Play(this.light.enabled ? LonelyMinionHouseConfig.LIGHTS_ON : LonelyMinionHouseConfig.LIGHTS_OFF, KAnim.PlayMode.Loop, 1f, 0f);
		}

		// Token: 0x060094CE RID: 38094 RVA: 0x003709FC File Offset: 0x0036EBFC
		private void StartStoryTrait()
		{
			base.TriggerStoryEvent(StoryInstance.State.IN_PROGRESS);
		}

		// Token: 0x060094CF RID: 38095 RVA: 0x00370A08 File Offset: 0x0036EC08
		protected override void OnBuildingActivated(object data)
		{
			if (!this.IsIntroSequenceComplete())
			{
				return;
			}
			bool isActivated = base.GetComponent<Activatable>().IsActivated;
			if (this.lonelyMinion != null)
			{
				this.lonelyMinion.sm.Active.Set(isActivated && base.GetComponent<Operational>().IsOperational, this.lonelyMinion, false);
			}
			if (isActivated && base.sm.QuestProgress.Get(this) < 1f)
			{
				base.GetComponent<RequireInputs>().visualizeRequirements = RequireInputs.Requirements.AllPower;
			}
		}

		// Token: 0x060094D0 RID: 38096 RVA: 0x00370A88 File Offset: 0x0036EC88
		protected override void OnObjectSelect(object clicked)
		{
			if (!(bool)clicked)
			{
				return;
			}
			if (this.knockNotification != null)
			{
				this.knocker.gameObject.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
				this.knockNotification.Clear();
				this.knockNotification = null;
				this.PlayIntroSequence(null);
				return;
			}
			if (!StoryManager.Instance.HasDisplayedPopup(Db.Get().Stories.LonelyMinion, EventInfoDataHelper.PopupType.BEGIN))
			{
				int count = Components.LiveMinionIdentities.Count;
				int num = global::UnityEngine.Random.Range(0, count);
				base.def.EventIntroInfo.Minions = new GameObject[]
				{
					this.lonelyMinion.gameObject,
					(count == 0) ? null : Components.LiveMinionIdentities[num].gameObject
				};
			}
			base.OnObjectSelect(clicked);
		}

		// Token: 0x060094D1 RID: 38097 RVA: 0x00370B58 File Offset: 0x0036ED58
		private void OnWorkStateChanged(Workable w, Workable.WorkableEvent state)
		{
			Activatable activatable = w as Activatable;
			if (activatable != null)
			{
				if (state == Workable.WorkableEvent.WorkStarted)
				{
					this.knocker = w.worker.GetComponent<KBatchedAnimController>();
					this.knocker.gameObject.Subscribe(-1503271301, new Action<object>(this.OnObjectSelect));
					this.knockNotification = new Notification(CODEX.STORY_TRAITS.LONELYMINION.KNOCK_KNOCK.TEXT, NotificationType.Event, null, null, false, 0f, new Notification.ClickCallback(this.PlayIntroSequence), null, null, true, true, false);
					base.gameObject.AddOrGet<Notifier>().Add(this.knockNotification, "");
					base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.AttentionRequired, base.smi);
				}
				if (state == Workable.WorkableEvent.WorkStopped)
				{
					if (this.currentWorkState == Workable.WorkableEvent.WorkStarted)
					{
						if (this.knockNotification != null)
						{
							base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
							this.knockNotification.Clear();
							this.knockNotification = null;
						}
						FocusTargetSequence.Cancel(base.master);
						this.knocker.gameObject.Unsubscribe(-1503271301, new Action<object>(this.OnObjectSelect));
						this.knocker = null;
					}
					if (this.currentWorkState == Workable.WorkableEvent.WorkCompleted)
					{
						Activatable activatable2 = activatable;
						activatable2.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(activatable2.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkStateChanged));
						Activatable activatable3 = activatable;
						activatable3.onActivate = (global::System.Action)Delegate.Remove(activatable3.onActivate, new global::System.Action(this.StartStoryTrait));
					}
				}
				this.currentWorkState = state;
				return;
			}
			if (state == Workable.WorkableEvent.WorkStopped)
			{
				this.AnimController.Play(LonelyMinionHouseConfig.STORAGE_WORK_PST, KAnim.PlayMode.Once, 1f, 0f);
				this.AnimController.Queue(LonelyMinionHouseConfig.STORAGE, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			bool flag = this.AnimController.currentAnim == LonelyMinionHouseConfig.STORAGE_WORKING[0] || this.AnimController.currentAnim == LonelyMinionHouseConfig.STORAGE_WORKING[1];
			if (state == Workable.WorkableEvent.WorkStarted && !flag)
			{
				this.AnimController.Play(LonelyMinionHouseConfig.STORAGE_WORKING, KAnim.PlayMode.Loop);
			}
		}

		// Token: 0x060094D2 RID: 38098 RVA: 0x00370D7C File Offset: 0x0036EF7C
		private void ReleaseKnocker(object _)
		{
			Navigator component = this.knocker.GetComponent<Navigator>();
			NavGrid.NavTypeData navTypeData = component.NavGrid.GetNavTypeData(component.CurrentNavType);
			this.knocker.RemoveAnimOverrides(base.GetComponent<Activatable>().overrideAnims[0]);
			this.knocker.Play(navTypeData.idleAnim, KAnim.PlayMode.Once, 1f, 0f);
			this.blinds.meterController.Play(this.blinds.meterController.initialAnim, this.blinds.meterController.initialMode, 1f, 0f);
			this.lonelyMinion.AnimController.Play(this.lonelyMinion.AnimController.defaultAnim, this.lonelyMinion.AnimController.initialMode, 1f, 0f);
			this.knocker.gameObject.Unsubscribe(-1061186183, new Action<object>(this.ReleaseKnocker));
			this.knocker.GetComponent<Brain>().Reset("knock sequence");
			this.knocker = null;
		}

		// Token: 0x060094D3 RID: 38099 RVA: 0x00370E98 File Offset: 0x0036F098
		private void PlayIntroSequence(object _ = null)
		{
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().MiscStatusItems.AttentionRequired, false);
			Vector3 vector = Grid.CellToPosCCC(Grid.OffsetCell(Grid.PosToCell(base.gameObject), base.def.CompletionData.CameraTargetOffset), Grid.SceneLayer.Ore);
			FocusTargetSequence.Start(base.master, new FocusTargetSequence.Data
			{
				WorldId = base.master.GetMyWorldId(),
				OrthographicSize = 2f,
				TargetSize = 6f,
				Target = vector,
				PopupData = null,
				CompleteCB = new global::System.Action(this.OnIntroSequenceComplete),
				CanCompleteCB = new Func<bool>(this.IsIntroSequenceComplete)
			});
			base.GetComponent<KnockKnock>().AnswerDoor();
			this.knockNotification = null;
		}

		// Token: 0x060094D4 RID: 38100 RVA: 0x00370F70 File Offset: 0x0036F170
		private void OnIntroSequenceComplete()
		{
			this.OnBuildingActivated(null);
			bool flag;
			bool flag2;
			QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest).TrackProgress(new Quest.ItemData
			{
				CriteriaId = LonelyMinionConfig.GreetingCriteraId
			}, out flag, out flag2);
		}

		// Token: 0x060094D5 RID: 38101 RVA: 0x00370FC0 File Offset: 0x0036F1C0
		private bool IsIntroSequenceComplete()
		{
			if (this.currentWorkState == Workable.WorkableEvent.WorkStarted)
			{
				return false;
			}
			if (this.currentWorkState == Workable.WorkableEvent.WorkStopped && this.knocker != null && this.knocker.currentAnim != LonelyMinionHouseConfig.ANSWER)
			{
				this.knocker.GetComponent<Brain>().Stop("knock sequence");
				this.knocker.gameObject.Subscribe(-1061186183, new Action<object>(this.ReleaseKnocker));
				this.knocker.AddAnimOverrides(base.GetComponent<Activatable>().overrideAnims[0], 0f);
				this.knocker.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
				this.lonelyMinion.AnimController.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
				this.blinds.meterController.Play(LonelyMinionHouseConfig.ANSWER, KAnim.PlayMode.Once, 1f, 0f);
			}
			return this.currentWorkState == Workable.WorkableEvent.WorkStopped && this.knocker == null;
		}

		// Token: 0x060094D6 RID: 38102 RVA: 0x003710D4 File Offset: 0x0036F2D4
		public Vector3 GetParcelPosition()
		{
			int num = -1;
			KAnimFileData data = Assets.GetAnim("anim_interacts_lonely_dupe_kanim").GetData();
			for (int i = 0; i < data.animCount; i++)
			{
				if (data.GetAnim(i).hash == LonelyMinionConfig.CHECK_MAIL)
				{
					num = data.GetAnim(i).firstFrameIdx;
					break;
				}
			}
			List<KAnim.Anim.FrameElement> frameElements = this.lonelyMinion.AnimController.GetBatch().group.data.frameElements;
			KAnim.Anim.Frame frame;
			this.lonelyMinion.AnimController.GetBatch().group.data.TryGetFrame(num, out frame);
			bool flag = false;
			Matrix2x3 matrix2x = default(Matrix2x3);
			int num2 = 0;
			while (!flag && num2 < frame.numElements)
			{
				if (frameElements[frame.firstElementIdx + num2].symbol == LonelyMinionConfig.PARCEL_SNAPTO)
				{
					flag = true;
					matrix2x = frameElements[frame.firstElementIdx + num2].transform;
					break;
				}
				num2++;
			}
			Vector3 vector = Vector3.zero;
			if (flag)
			{
				Matrix4x4 matrix4x = this.lonelyMinion.AnimController.GetTransformMatrix();
				vector = (matrix4x * matrix2x).GetColumn(3);
			}
			return vector;
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060094D7 RID: 38103 RVA: 0x0037121B File Offset: 0x0036F41B
		public string Title
		{
			get
			{
				return CODEX.STORY_TRAITS.LONELYMINION.NAME;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060094D8 RID: 38104 RVA: 0x00371227 File Offset: 0x0036F427
		public string Description
		{
			get
			{
				return CODEX.STORY_TRAITS.LONELYMINION.DESCRIPTION_BUILDINGMENU;
			}
		}

		// Token: 0x060094D9 RID: 38105 RVA: 0x00371234 File Offset: 0x0036F434
		public ICheckboxListGroupControl.ListGroup[] GetData()
		{
			QuestInstance greetingQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			if (!greetingQuest.IsComplete)
			{
				return new ICheckboxListGroupControl.ListGroup[]
				{
					new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionGreetingQuest.Title, greetingQuest.GetCheckBoxData(null), (string title) => this.ResolveQuestTitle(title, greetingQuest), null)
				};
			}
			QuestInstance foodQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			QuestInstance decorQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			QuestInstance powerQuest = QuestManager.GetInstance(this.questOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			return new ICheckboxListGroupControl.ListGroup[]
			{
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionGreetingQuest.Title, greetingQuest.GetCheckBoxData(null), (string title) => this.ResolveQuestTitle(title, greetingQuest), null),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionFoodQuest.Title, foodQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, foodQuest), null),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionDecorQuest.Title, decorQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, decorQuest), null),
				new ICheckboxListGroupControl.ListGroup(Db.Get().Quests.LonelyMinionPowerQuest.Title, powerQuest.GetCheckBoxData(new Func<int, string, QuestInstance, string>(this.ResolveQuestToolTips)), (string title) => this.ResolveQuestTitle(title, powerQuest), null)
			};
		}

		// Token: 0x060094DA RID: 38106 RVA: 0x00371428 File Offset: 0x0036F628
		private string ResolveQuestTitle(string title, QuestInstance quest)
		{
			string text = GameUtil.FloatToString(quest.CurrentProgress * 100f, "##0") + UI.UNITSUFFIXES.PERCENT;
			return title + " - " + text;
		}

		// Token: 0x060094DB RID: 38107 RVA: 0x00371468 File Offset: 0x0036F668
		private string ResolveQuestToolTips(int criteriaId, string toolTip, QuestInstance quest)
		{
			if (criteriaId == LonelyMinionConfig.FoodCriteriaId.HashValue)
			{
				int num = (int)quest.GetTargetValue(LonelyMinionConfig.FoodCriteriaId, 0);
				int targetCount = quest.GetTargetCount(LonelyMinionConfig.FoodCriteriaId);
				string text = string.Empty;
				for (int i = 0; i < targetCount; i++)
				{
					Tag satisfyingItem = quest.GetSatisfyingItem(LonelyMinionConfig.FoodCriteriaId, i);
					if (!satisfyingItem.IsValid)
					{
						break;
					}
					text = text + "    • " + TagManager.GetProperName(satisfyingItem, false);
					if (targetCount - i != 1)
					{
						text += "\n";
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = string.Format("{0}{1}", "    • ", CODEX.QUESTS.CRITERIA.FOODQUALITY.NONE);
				}
				return string.Format(toolTip, GameUtil.GetFormattedFoodQuality(num), text);
			}
			if (criteriaId == LonelyMinionConfig.DecorCriteriaId.HashValue)
			{
				int num2 = (int)quest.GetTargetValue(LonelyMinionConfig.DecorCriteriaId, 0);
				float num3 = LonelyMinionHouse.CalculateAverageDecor(this.lonelyMinion.def.DecorInspectionArea);
				return string.Format(toolTip, num2, num3);
			}
			float num4 = quest.GetTargetValue(LonelyMinionConfig.PowerCriteriaId, 0) - quest.GetCurrentValue(LonelyMinionConfig.PowerCriteriaId, 0);
			return string.Format(toolTip, Mathf.CeilToInt(num4));
		}

		// Token: 0x060094DC RID: 38108 RVA: 0x003715A0 File Offset: 0x0036F7A0
		public bool SidescreenEnabled()
		{
			return StoryManager.Instance.HasDisplayedPopup(Db.Get().Stories.LonelyMinion, EventInfoDataHelper.PopupType.BEGIN) && !StoryManager.Instance.CheckState(StoryInstance.State.COMPLETE, Db.Get().Stories.LonelyMinion);
		}

		// Token: 0x060094DD RID: 38109 RVA: 0x003715DD File Offset: 0x0036F7DD
		public int CheckboxSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x04007280 RID: 29312
		private KAnimLink lightsLink;

		// Token: 0x04007281 RID: 29313
		private HashedString questOwnerId;

		// Token: 0x04007282 RID: 29314
		private LonelyMinion.Instance lonelyMinion;

		// Token: 0x04007283 RID: 29315
		private KBatchedAnimController[] animControllers;

		// Token: 0x04007284 RID: 29316
		private Light2D light;

		// Token: 0x04007285 RID: 29317
		private FilteredStorage storageFilter;

		// Token: 0x04007286 RID: 29318
		private MeterController meter;

		// Token: 0x04007287 RID: 29319
		private MeterController blinds;

		// Token: 0x04007288 RID: 29320
		private Workable.WorkableEvent currentWorkState = Workable.WorkableEvent.WorkStopped;

		// Token: 0x04007289 RID: 29321
		private Notification knockNotification;

		// Token: 0x0400728A RID: 29322
		private KBatchedAnimController knocker;
	}

	// Token: 0x0200165F RID: 5727
	public class ActiveStates : GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State
	{
		// Token: 0x060094DE RID: 38110 RVA: 0x003715E1 File Offset: 0x0036F7E1
		public static void OnEnterStoryComplete(LonelyMinionHouse.Instance smi)
		{
			smi.CompleteEvent();
		}

		// Token: 0x0400728B RID: 29323
		public GameStateMachine<LonelyMinionHouse, LonelyMinionHouse.Instance, StateMachineController, LonelyMinionHouse.Def>.State StoryComplete;
	}
}
