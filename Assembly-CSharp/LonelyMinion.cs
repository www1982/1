using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
public class LonelyMinion : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>
{
	// Token: 0x06002281 RID: 8833 RVA: 0x000C5AE8 File Offset: 0x000C3CE8
	private bool HahCheckedMail(LonelyMinion.Instance smi)
	{
		if (smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL)
		{
			if (this.Mail.Get(smi) == smi.gameObject)
			{
				this.Mail.Set(null, smi, true);
				smi.AnimController.Play(LonelyMinionConfig.CHECK_MAIL_FAILURE, KAnim.PlayMode.Once, 1f, 0f);
				return false;
			}
			this.CheckForMail(smi);
			return false;
		}
		else
		{
			if (smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_FAILURE || smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_DUPLICATE)
			{
				smi.Drop();
				return false;
			}
			return smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_FAILURE || smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_SUCCESS || smi.AnimController.currentAnim == LonelyMinionConfig.CHECK_MAIL_DUPLICATE;
		}
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x000C5BD8 File Offset: 0x000C3DD8
	private void CheckForMail(LonelyMinion.Instance smi)
	{
		Tag prefabTag = this.Mail.Get(smi).GetComponent<KPrefabID>().PrefabTag;
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
		global::Debug.Assert(instance != null);
		Quest.ItemData itemData = new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.FoodCriteriaId,
			SatisfyingItem = prefabTag,
			QualifyingTag = GameTags.Edible,
			CurrentValue = (float)EdiblesManager.GetFoodInfo(prefabTag.Name).Quality
		};
		LonelyMinion.MailStates mailStates = smi.GetCurrentState() as LonelyMinion.MailStates;
		bool flag;
		bool flag2;
		instance.TrackProgress(itemData, out flag, out flag2);
		StateMachine.BaseState baseState = mailStates.Success;
		string text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.TASTYFOOD.NAME;
		string text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.TASTYFOOD.TOOLTIP;
		if (flag2)
		{
			baseState = mailStates.Duplicate;
			text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.REPEATEDFOOD.NAME;
			text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.REPEATEDFOOD.TOOLTIP;
		}
		else if (!flag)
		{
			baseState = mailStates.Failure;
			text = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.CRAPPYFOOD.NAME;
			text2 = CODEX.STORY_TRAITS.LONELYMINION.GIFTRESPONSE_POPUP.CRAPPYFOOD.TOOLTIP;
		}
		Pickupable component = this.Mail.Get(smi).GetComponent<Pickupable>();
		smi.Pickup(component, baseState != mailStates.Success);
		smi.ScheduleGoTo(0.016f, baseState);
		Notification notification = new Notification(text, NotificationType.Event, (List<Notification> notificationList, object data) => data as string, text2, false, 0f, null, null, smi.transform.parent, true, true, true);
		smi.transform.parent.gameObject.AddOrGet<Notifier>().Add(notification, "");
	}

	// Token: 0x06002283 RID: 8835 RVA: 0x000C5D80 File Offset: 0x000C3F80
	private void EvaluateCurrentDecor(LonelyMinion.Instance smi, float dt)
	{
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
		if (smi.GetCurrentState() == this.Inactive || instance.IsComplete)
		{
			return;
		}
		float num = LonelyMinionHouse.CalculateAverageDecor(smi.def.DecorInspectionArea);
		bool flag = num >= 0f || (num > smi.StartingAverageDecor && 1f - num / smi.StartingAverageDecor > 0.01f);
		if (!instance.IsStarted && !flag)
		{
			return;
		}
		bool flag2;
		bool flag3;
		instance.TrackProgress(new Quest.ItemData
		{
			CriteriaId = LonelyMinionConfig.DecorCriteriaId,
			CurrentValue = num
		}, out flag2, out flag3);
	}

	// Token: 0x06002284 RID: 8836 RVA: 0x000C5E38 File Offset: 0x000C4038
	private void DelayIdle(LonelyMinion.Instance smi, float dt)
	{
		if (smi.AnimController.currentAnim != smi.AnimController.defaultAnim)
		{
			return;
		}
		if (smi.IdleDelayTimer > 0f)
		{
			smi.IdleDelayTimer -= dt;
		}
		if (smi.IdleDelayTimer <= 0f)
		{
			this.PlayIdle(smi, smi.ChooseIdle());
			smi.IdleDelayTimer = global::UnityEngine.Random.Range(20f, 40f);
		}
	}

	// Token: 0x06002285 RID: 8837 RVA: 0x000C5EB4 File Offset: 0x000C40B4
	private void PlayIdle(LonelyMinion.Instance smi, HashedString idleAnim)
	{
		if (!idleAnim.IsValid)
		{
			return;
		}
		if (idleAnim == LonelyMinionConfig.CHECK_MAIL)
		{
			this.Mail.Set(smi.gameObject, smi, false);
			return;
		}
		QuestInstance instance = QuestManager.GetInstance(smi.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
		int num = instance.GetCurrentCount(LonelyMinionConfig.FoodCriteriaId) - 1;
		if (idleAnim == LonelyMinionConfig.FOOD_IDLE && num >= 0)
		{
			GameObject prefab = Assets.GetPrefab(instance.GetSatisfyingItem(LonelyMinionConfig.FoodCriteriaId, global::UnityEngine.Random.Range(0, num)));
			if (prefab != null)
			{
				KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
				smi.PackageSnapPoint.SwapAnims(component.AnimFiles);
				smi.PackageSnapPoint.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
			}
		}
		if (!(idleAnim == LonelyMinionConfig.FOOD_IDLE) && !(idleAnim == LonelyMinionConfig.DECOR_IDLE) && !(idleAnim == LonelyMinionConfig.POWER_IDLE))
		{
			LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			smi.AnimController.GetSynchronizer().Remove(smi2.AnimController);
			if (idleAnim == LonelyMinionConfig.BLINDS_IDLE_0)
			{
				smi2.BlindsController.Play(LonelyMinionConfig.BLINDS_IDLE_0, KAnim.PlayMode.Once, 1f, 0f);
			}
		}
		smi.AnimController.Play(idleAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002286 RID: 8838 RVA: 0x000C6018 File Offset: 0x000C4218
	private void OnIdleAnimComplete(LonelyMinion.Instance smi)
	{
		if (smi.AnimController.currentAnim == smi.AnimController.defaultAnim)
		{
			return;
		}
		if (!(smi.AnimController.currentAnim == LonelyMinionConfig.FOOD_IDLE) && !(smi.AnimController.currentAnim == LonelyMinionConfig.DECOR_IDLE) && !(smi.AnimController.currentAnim == LonelyMinionConfig.POWER_IDLE))
		{
			LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			smi.AnimController.GetSynchronizer().Add(smi2.AnimController);
			if (smi.AnimController.currentAnim == LonelyMinionConfig.BLINDS_IDLE_0)
			{
				smi2.BlindsController.Play(string.Format("{0}_{1}", "meter_blinds", 0), KAnim.PlayMode.Paused, 1f, 0f);
			}
		}
		smi.AnimController.Play(smi.AnimController.defaultAnim, smi.AnimController.initialMode, 1f, 0f);
		if (this.Active.Get(smi) && this.Mail.Get(smi) != null)
		{
			smi.ScheduleGoTo(0f, this.CheckMail);
		}
	}

	// Token: 0x06002287 RID: 8839 RVA: 0x000C6164 File Offset: 0x000C4364
	private void OnBecomeInactive(LonelyMinion.Instance smi)
	{
		smi.AnimController.GetSynchronizer().Clear();
		smi.AnimController.Play(smi.AnimController.initialAnim, smi.AnimController.initialMode, 1f, 0f);
	}

	// Token: 0x06002288 RID: 8840 RVA: 0x000C61B4 File Offset: 0x000C43B4
	private void OnBecomeActive(LonelyMinion.Instance smi)
	{
		LonelyMinionHouse.Instance smi2 = smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
		if (smi2 == null)
		{
			return;
		}
		smi.AnimController.GetSynchronizer().Add(smi2.AnimController);
		if (smi.StartingAverageDecor == float.NegativeInfinity)
		{
			smi.StartingAverageDecor = LonelyMinionHouse.CalculateAverageDecor(smi.def.DecorInspectionArea);
		}
	}

	// Token: 0x06002289 RID: 8841 RVA: 0x000C6210 File Offset: 0x000C4410
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Inactive;
		this.root.ParamTransition<bool>(this.Active, this.Inactive, (LonelyMinion.Instance smi, bool p) => !this.Active.Get(smi)).ParamTransition<bool>(this.Active, this.Idle, (LonelyMinion.Instance smi, bool p) => this.Active.Get(smi)).Update(new Action<LonelyMinion.Instance, float>(this.EvaluateCurrentDecor), UpdateRate.SIM_1000ms, false);
		this.Inactive.Enter(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnBecomeInactive)).Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnBecomeActive));
		this.Idle.ParamTransition<GameObject>(this.Mail, this.CheckMail, (LonelyMinion.Instance smi, GameObject p) => smi.AnimController.currentAnim == smi.AnimController.defaultAnim && this.Mail.Get(smi) != null).Update(new Action<LonelyMinion.Instance, float>(this.DelayIdle), UpdateRate.SIM_EVERY_TICK, false).EventHandler(GameHashes.AnimQueueComplete, new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnIdleAnimComplete))
			.Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(this.OnIdleAnimComplete));
		this.CheckMail.Enter(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(LonelyMinion.MailStates.OnEnter)).ParamTransition<GameObject>(this.Mail, this.Idle, (LonelyMinion.Instance smi, GameObject p) => this.Mail.Get(smi) == null).EventTransition(GameHashes.AnimQueueComplete, this.Idle, new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.Transition.ConditionCallback(this.HahCheckedMail))
			.Exit(new StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State.Callback(LonelyMinion.MailStates.OnExit));
		this.CheckMail.Success.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_SUCCESS);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_SUCCESS);
		});
		this.CheckMail.Failure.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_FAILURE);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_FAILURE);
		});
		this.CheckMail.Duplicate.Enter(delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.FOOD_DUPLICATE);
		}).EventHandler(GameHashes.AnimQueueComplete, delegate(LonelyMinion.Instance smi)
		{
			LonelyMinion.MailStates.PlayAnims(smi, LonelyMinionConfig.CHECK_MAIL_DUPLICATE);
		});
	}

	// Token: 0x04001411 RID: 5137
	public StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.TargetParameter Mail;

	// Token: 0x04001412 RID: 5138
	public StateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.BoolParameter Active;

	// Token: 0x04001413 RID: 5139
	public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Idle;

	// Token: 0x04001414 RID: 5140
	public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Inactive;

	// Token: 0x04001415 RID: 5141
	public LonelyMinion.MailStates CheckMail;

	// Token: 0x02001465 RID: 5221
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006C6D RID: 27757
		public Personality Personality;

		// Token: 0x04006C6E RID: 27758
		public HashedString QuestOwnerId;

		// Token: 0x04006C6F RID: 27759
		public Extents DecorInspectionArea;
	}

	// Token: 0x02001466 RID: 5222
	public new class Instance : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.GameInstance
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06008D93 RID: 36243 RVA: 0x00358D2B File Offset: 0x00356F2B
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animControllers[0];
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06008D94 RID: 36244 RVA: 0x00358D35 File Offset: 0x00356F35
		public KBatchedAnimController PackageSnapPoint
		{
			get
			{
				return this.animControllers[1];
			}
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x00358D40 File Offset: 0x00356F40
		public Instance(StateMachineController master, LonelyMinion.Def def)
			: base(master, def)
		{
			this.animControllers = base.gameObject.GetComponentsInChildren<KBatchedAnimController>(true);
			this.storage = base.GetComponent<Storage>();
			global::Debug.Assert(def.Personality != null);
			base.GetComponent<Accessorizer>().ApplyMinionPersonality(def.Personality);
			StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
			storyInstance.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Combine(storyInstance.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x00358DE8 File Offset: 0x00356FE8
		public override void StartSM()
		{
			LonelyMinionHouse.Instance smi = base.smi.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			base.smi.AnimController.GetSynchronizer().Add(smi.AnimController);
			QuestInstance instance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Combine(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.ShowQuestCompleteNotification));
			base.smi.IdleDelayTimer = global::UnityEngine.Random.Range(20f, 40f);
			this.InitializeIdles();
			base.StartSM();
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x00358E8C File Offset: 0x0035708C
		public override void StopSM(string reason)
		{
			QuestInstance instance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionGreetingQuest);
			instance.QuestProgressChanged = (Action<QuestInstance, Quest.State, float>)Delegate.Remove(instance.QuestProgressChanged, new Action<QuestInstance, Quest.State, float>(this.ShowQuestCompleteNotification));
			this.StoryCleanUp();
			base.StopSM(reason);
			this.ResetHandle.ClearScheduler();
			this.ResetHandle.FreeResources();
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x00358EFC File Offset: 0x003570FC
		public HashedString ChooseIdle()
		{
			if (this.availableIdles.Count > 1)
			{
				this.availableIdles.Shuffle<HashedString>();
			}
			return this.availableIdles[0];
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x00358F24 File Offset: 0x00357124
		public void Pickup(Pickupable pickupable, bool store)
		{
			base.sm.Mail.Set(null, this, true);
			pickupable.storage.GetComponent<SingleEntityReceptacle>().OrderRemoveOccupant();
			this.PackageSnapPoint.Play("object", KAnim.PlayMode.Loop, 1f, 0f);
			if (store)
			{
				this.storage.Store(pickupable.gameObject, true, true, false, false);
				return;
			}
			global::UnityEngine.Object.Destroy(pickupable.gameObject);
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x00358F9C File Offset: 0x0035719C
		public void Drop()
		{
			this.storage.DropAll(this.PackageSnapPoint.transform.position, false, false, default(Vector3), true, null);
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x00358FD1 File Offset: 0x003571D1
		private void OnStoryStateChanged(StoryInstance.State state)
		{
			if (state != StoryInstance.State.COMPLETE)
			{
				return;
			}
			this.StoryCleanUp();
		}

		// Token: 0x06008D9C RID: 36252 RVA: 0x00358FE0 File Offset: 0x003571E0
		private void StoryCleanUp()
		{
			this.AnimController.GetSynchronizer().Clear();
			StoryInstance storyInstance = StoryManager.Instance.GetStoryInstance(Db.Get().Stories.LonelyMinion.HashId);
			storyInstance.StoryStateChanged = (Action<StoryInstance.State>)Delegate.Remove(storyInstance.StoryStateChanged, new Action<StoryInstance.State>(this.OnStoryStateChanged));
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x0035903C File Offset: 0x0035723C
		private void InitializeIdles()
		{
			QuestInstance questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionFoodQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.FOOD_IDLE);
				if (!questInstance.IsComplete)
				{
					this.availableIdles.Add(LonelyMinionConfig.CHECK_MAIL);
				}
			}
			questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionDecorQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.DECOR_IDLE);
			}
			questInstance = QuestManager.GetInstance(base.def.QuestOwnerId, Db.Get().Quests.LonelyMinionPowerQuest);
			if (questInstance.IsStarted)
			{
				this.availableIdles.Add(LonelyMinionConfig.POWER_IDLE);
			}
			LonelyMinionHouse.Instance smi = base.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			LonelyMinionHouse lonelyMinionHouse = smi.GetStateMachine() as LonelyMinionHouse;
			float num = 3f * lonelyMinionHouse.QuestProgress.Get(smi);
			int num2 = (Mathf.Approximately((float)Mathf.CeilToInt(num), num) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 == 0)
			{
				this.availableIdles.Add(LonelyMinionConfig.BLINDS_IDLE_0);
				return;
			}
			int num3 = 1;
			while (num3 <= num2 && num3 != 3)
			{
				this.availableIdles.Add(string.Format("{0}_{1}", "idle_blinds", num3));
				num3++;
			}
		}

		// Token: 0x06008D9E RID: 36254 RVA: 0x003591AC File Offset: 0x003573AC
		public void UnlockQuestIdle(QuestInstance quest, Quest.State prevState, float delta)
		{
			if (prevState == Quest.State.NotStarted && quest.IsStarted)
			{
				if (quest.Id == Db.Get().Quests.LonelyMinionFoodQuest.IdHash)
				{
					this.availableIdles.Add(LonelyMinionConfig.FOOD_IDLE);
				}
				else if (quest.Id == Db.Get().Quests.LonelyMinionDecorQuest.IdHash)
				{
					this.availableIdles.Add(LonelyMinionConfig.DECOR_IDLE);
				}
				else
				{
					this.availableIdles.Add(LonelyMinionConfig.POWER_IDLE);
				}
			}
			if (!quest.IsComplete)
			{
				return;
			}
			if (quest.Id == Db.Get().Quests.LonelyMinionFoodQuest.IdHash)
			{
				this.availableIdles.Remove(LonelyMinionConfig.CHECK_MAIL);
			}
			LonelyMinionHouse.Instance smi = base.transform.parent.GetSMI<LonelyMinionHouse.Instance>();
			LonelyMinionHouse lonelyMinionHouse = smi.GetStateMachine() as LonelyMinionHouse;
			float num = 3f * lonelyMinionHouse.QuestProgress.Get(smi);
			int num2 = (Mathf.Approximately((float)Mathf.CeilToInt(num), num) ? Mathf.CeilToInt(num) : Mathf.FloorToInt(num));
			if (num2 > 0 && num2 < 3)
			{
				this.availableIdles.Add(string.Format("{0}_{1}", "idle_blinds", num2));
			}
			this.availableIdles.Remove(LonelyMinionConfig.BLINDS_IDLE_0);
		}

		// Token: 0x06008D9F RID: 36255 RVA: 0x00359304 File Offset: 0x00357504
		public void ShowQuestCompleteNotification(QuestInstance quest, Quest.State prevState, float delta = 0f)
		{
			if (!quest.IsComplete)
			{
				return;
			}
			string text = string.Empty;
			if (quest.Id != Db.Get().Quests.LonelyMinionGreetingQuest.IdHash)
			{
				text = "story_trait_lonelyminion_" + quest.Name.ToLower();
				Game.Instance.unlocks.Unlock(text, false);
			}
			Notification notification = new Notification(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.NAME, NotificationType.Event, null, null, false, 0f, new Notification.ClickCallback(this.ShowQuestCompletePopup), new global::Tuple<string, string>(text, quest.CompletionText), null, true, true, true);
			base.transform.parent.gameObject.AddOrGet<Notifier>().Add(notification, "");
		}

		// Token: 0x06008DA0 RID: 36256 RVA: 0x003593C0 File Offset: 0x003575C0
		private void ShowQuestCompletePopup(object data)
		{
			global::Tuple<string, string> tuple = data as global::Tuple<string, string>;
			InfoDialogScreen infoDialogScreen = LoreBearer.ShowPopupDialog().SetHeader(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.NAME).AddPlainText(tuple.second)
				.AddDefaultOK(false);
			if (!string.IsNullOrEmpty(tuple.first))
			{
				infoDialogScreen.AddOption(CODEX.STORY_TRAITS.LONELYMINION.QUESTCOMPLETE_POPUP.VIEW_IN_CODEX, LoreBearerUtil.OpenCodexByLockKeyID(tuple.first, true), false);
			}
		}

		// Token: 0x04006C70 RID: 27760
		public SchedulerHandle ResetHandle;

		// Token: 0x04006C71 RID: 27761
		public float StartingAverageDecor = float.NegativeInfinity;

		// Token: 0x04006C72 RID: 27762
		public float IdleDelayTimer;

		// Token: 0x04006C73 RID: 27763
		private KBatchedAnimController[] animControllers;

		// Token: 0x04006C74 RID: 27764
		private Storage storage;

		// Token: 0x04006C75 RID: 27765
		private const int maxIdles = 8;

		// Token: 0x04006C76 RID: 27766
		private List<HashedString> availableIdles = new List<HashedString>(8);
	}

	// Token: 0x02001467 RID: 5223
	public class MailStates : GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State
	{
		// Token: 0x06008DA1 RID: 36257 RVA: 0x00359428 File Offset: 0x00357628
		public static void OnEnter(LonelyMinion.Instance smi)
		{
			KBatchedAnimController component = smi.sm.Mail.Get(smi).GetComponent<KBatchedAnimController>();
			smi.PackageSnapPoint.gameObject.SetActive(component.gameObject != smi.gameObject);
			if (smi.PackageSnapPoint.gameObject.activeSelf)
			{
				smi.PackageSnapPoint.SwapAnims(component.AnimFiles);
			}
			smi.AnimController.Play(LonelyMinionConfig.CHECK_MAIL, KAnim.PlayMode.Once, 1f, 0f);
		}

		// Token: 0x06008DA2 RID: 36258 RVA: 0x003594AB File Offset: 0x003576AB
		public static void OnExit(LonelyMinion.Instance smi)
		{
			smi.ResetHandle = smi.ScheduleNextFrame(new Action<object>(LonelyMinion.MailStates.ResetState), smi);
		}

		// Token: 0x06008DA3 RID: 36259 RVA: 0x003594C8 File Offset: 0x003576C8
		private static void ResetState(object data)
		{
			LonelyMinion.Instance instance = data as LonelyMinion.Instance;
			instance.AnimController.Play(instance.AnimController.initialAnim, instance.AnimController.initialMode, 1f, 0f);
			instance.Drop();
		}

		// Token: 0x06008DA4 RID: 36260 RVA: 0x00359512 File Offset: 0x00357712
		public static void PlayAnims(LonelyMinion.Instance smi, HashedString anim)
		{
			if (anim.IsValid)
			{
				smi.AnimController.Queue(anim, KAnim.PlayMode.Once, 1f, 0f);
				return;
			}
			smi.GoTo(smi.sm.Idle);
		}

		// Token: 0x04006C77 RID: 27767
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Success;

		// Token: 0x04006C78 RID: 27768
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Failure;

		// Token: 0x04006C79 RID: 27769
		public GameStateMachine<LonelyMinion, LonelyMinion.Instance, StateMachineController, LonelyMinion.Def>.State Duplicate;
	}
}
