using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A6F RID: 2671
public class StandardCropPlant : StateMachineComponent<StandardCropPlant.StatesInstance>
{
	// Token: 0x06004D41 RID: 19777 RVA: 0x001BF760 File Offset: 0x001BD960
	public static string GetWiltAnimFromAnimSet(StandardCropPlant.AnimSet set, float growingPercentage)
	{
		int num;
		if (growingPercentage < 0.75f)
		{
			num = 1;
		}
		else if (growingPercentage < 1f)
		{
			num = 2;
		}
		else
		{
			num = 3;
		}
		return set.GetWiltLevel(num);
	}

	// Token: 0x06004D42 RID: 19778 RVA: 0x001BF78E File Offset: 0x001BD98E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004D43 RID: 19779 RVA: 0x001BF7A1 File Offset: 0x001BD9A1
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004D44 RID: 19780 RVA: 0x001BF7BC File Offset: 0x001BD9BC
	public Notification CreateDeathNotification()
	{
		return new Notification(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION, NotificationType.Bad, (List<Notification> notificationList, object data) => CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), "/t• " + base.gameObject.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06004D45 RID: 19781 RVA: 0x001BF819 File Offset: 0x001BDA19
	public void RefreshPositionPercent()
	{
		this.animController.SetPositionPercent(this.growing.PercentOfCurrentHarvest());
	}

	// Token: 0x06004D46 RID: 19782 RVA: 0x001BF834 File Offset: 0x001BDA34
	private static string ToolTipResolver(List<Notification> notificationList, object data)
	{
		string text = "";
		for (int i = 0; i < notificationList.Count; i++)
		{
			Notification notification = notificationList[i];
			text += (string)notification.tooltipData;
			if (i < notificationList.Count - 1)
			{
				text += "\n";
			}
		}
		return string.Format(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP, text);
	}

	// Token: 0x0400335B RID: 13147
	private const int WILT_LEVELS = 3;

	// Token: 0x0400335C RID: 13148
	[MyCmpReq]
	private Crop crop;

	// Token: 0x0400335D RID: 13149
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x0400335E RID: 13150
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x0400335F RID: 13151
	[MyCmpReq]
	private Growing growing;

	// Token: 0x04003360 RID: 13152
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x04003361 RID: 13153
	[MyCmpGet]
	private Harvestable harvestable;

	// Token: 0x04003362 RID: 13154
	public bool wiltsOnReadyToHarvest;

	// Token: 0x04003363 RID: 13155
	public bool preventGrowPositionUpdate;

	// Token: 0x04003364 RID: 13156
	public static StandardCropPlant.AnimSet defaultAnimSet = new StandardCropPlant.AnimSet
	{
		pre_grow = null,
		grow = "grow",
		grow_pst = "grow_pst",
		idle_full = "idle_full",
		wilt_base = "wilt",
		harvest = "harvest",
		waning = "waning"
	};

	// Token: 0x04003365 RID: 13157
	public StandardCropPlant.AnimSet anims = StandardCropPlant.defaultAnimSet;

	// Token: 0x02001B47 RID: 6983
	public class AnimSet
	{
		// Token: 0x0600A6CE RID: 42702 RVA: 0x003AE678 File Offset: 0x003AC878
		public void ClearWiltLevelCache()
		{
			this.m_wilt = null;
		}

		// Token: 0x0600A6CF RID: 42703 RVA: 0x003AE684 File Offset: 0x003AC884
		public string GetWiltLevel(int level)
		{
			if (this.m_wilt == null)
			{
				this.m_wilt = new string[3];
				for (int i = 0; i < 3; i++)
				{
					this.m_wilt[i] = this.wilt_base + (i + 1).ToString();
				}
			}
			return this.m_wilt[level - 1];
		}

		// Token: 0x0600A6D0 RID: 42704 RVA: 0x003AE6D9 File Offset: 0x003AC8D9
		public AnimSet()
		{
		}

		// Token: 0x0600A6D1 RID: 42705 RVA: 0x003AE6E8 File Offset: 0x003AC8E8
		public AnimSet(StandardCropPlant.AnimSet template)
		{
			this.pre_grow = template.pre_grow;
			this.grow = template.grow;
			this.grow_pst = template.grow_pst;
			this.idle_full = template.idle_full;
			this.wilt_base = template.wilt_base;
			this.harvest = template.harvest;
			this.waning = template.waning;
			this.grow_playmode = template.grow_playmode;
		}

		// Token: 0x0400821F RID: 33311
		public string pre_grow;

		// Token: 0x04008220 RID: 33312
		public string grow;

		// Token: 0x04008221 RID: 33313
		public string grow_pst;

		// Token: 0x04008222 RID: 33314
		public string idle_full;

		// Token: 0x04008223 RID: 33315
		public string wilt_base;

		// Token: 0x04008224 RID: 33316
		public string harvest;

		// Token: 0x04008225 RID: 33317
		public string waning;

		// Token: 0x04008226 RID: 33318
		public KAnim.PlayMode grow_playmode = KAnim.PlayMode.Paused;

		// Token: 0x04008227 RID: 33319
		private string[] m_wilt;
	}

	// Token: 0x02001B48 RID: 6984
	public class States : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant>
	{
		// Token: 0x0600A6D2 RID: 42706 RVA: 0x003AE764 File Offset: 0x003AC964
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.alive;
			this.dead.ToggleMainStatusItem(Db.Get().CreatureStatusItems.Dead, null).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.rm.Replanted && !smi.master.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted))
				{
					Notifier notifier = smi.master.gameObject.AddOrGet<Notifier>();
					Notification notification = smi.master.CreateDeathNotification();
					notifier.Add(notification, "");
				}
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				Harvestable component = smi.master.GetComponent<Harvestable>();
				if (component != null && component.CanBeHarvested && GameScheduler.Instance != null)
				{
					GameScheduler.Instance.Schedule("SpawnFruit", 0.2f, new Action<object>(smi.master.crop.SpawnConfiguredFruit), null, null);
				}
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				global::UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
			this.blighted.InitializeStates(this.masterTarget, this.dead).PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.waning, KAnim.PlayMode.Once).ToggleMainStatusItem(Db.Get().CreatureStatusItems.Crop_Blighted, null)
				.TagTransition(GameTags.Blighted, this.alive, true);
			this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.pre_idle).ToggleComponent<Growing>(false)
				.TagTransition(GameTags.Blighted, this.blighted, false);
			this.alive.pre_idle.EnterTransition(this.alive.idle, (StandardCropPlant.StatesInstance smi) => smi.master.anims.pre_grow == null).PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.pre_grow, KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.idle)
				.ScheduleGoTo(8f, this.alive.idle);
			this.alive.idle.EventTransition(GameHashes.Wilt, this.alive.wilting, (StandardCropPlant.StatesInstance smi) => smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Grow, this.alive.pre_fruiting, (StandardCropPlant.StatesInstance smi) => smi.master.growing.ReachedNextHarvest()).PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.grow, (StandardCropPlant.StatesInstance smi) => smi.master.anims.grow_playmode)
				.Enter(new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State.Callback(StandardCropPlant.States.RefreshPositionPercent))
				.Update(new Action<StandardCropPlant.StatesInstance, float>(StandardCropPlant.States.RefreshPositionPercent), UpdateRate.SIM_4000ms, false)
				.EventHandler(GameHashes.ConsumePlant, new StateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State.Callback(StandardCropPlant.States.RefreshPositionPercent));
			this.alive.pre_fruiting.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.grow_pst, KAnim.PlayMode.Once).TriggerOnEnter(GameHashes.BurstEmitDisease, null).EventTransition(GameHashes.AnimQueueComplete, this.alive.fruiting, null)
				.EventTransition(GameHashes.Wilt, this.alive.wilting, null)
				.ScheduleGoTo(8f, this.alive.fruiting);
			this.alive.fruiting_lost.Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(false);
				}
			}).GoTo(this.alive.idle);
			this.alive.wilting.PlayAnim(new Func<StandardCropPlant.StatesInstance, string>(StandardCropPlant.States.GetWiltAnim), KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.idle, (StandardCropPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting()).EventTransition(GameHashes.Harvest, this.alive.harvest, null);
			this.alive.fruiting.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.idle_full, KAnim.PlayMode.Loop).ToggleTag(GameTags.FullyGrown).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(true);
				}
			})
				.EventHandlerTransition(GameHashes.Wilt, this.alive.wilting, (StandardCropPlant.StatesInstance smi, object obj) => smi.master.wiltsOnReadyToHarvest)
				.EventTransition(GameHashes.Harvest, this.alive.harvest, null)
				.EventTransition(GameHashes.Grow, this.alive.fruiting_lost, (StandardCropPlant.StatesInstance smi) => !smi.master.growing.ReachedNextHarvest());
			this.alive.harvest.PlayAnim((StandardCropPlant.StatesInstance smi) => smi.master.anims.harvest, KAnim.PlayMode.Once).Enter(delegate(StandardCropPlant.StatesInstance smi)
			{
				if (smi.master != null)
				{
					smi.master.crop.SpawnConfiguredFruit(null);
				}
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(false);
				}
			}).Exit(delegate(StandardCropPlant.StatesInstance smi)
			{
				smi.Trigger(113170146, null);
			})
				.OnAnimQueueComplete(this.alive.idle);
		}

		// Token: 0x0600A6D3 RID: 42707 RVA: 0x003AEC64 File Offset: 0x003ACE64
		private static string GetWiltAnim(StandardCropPlant.StatesInstance smi)
		{
			float num = smi.master.growing.PercentOfCurrentHarvest();
			return StandardCropPlant.GetWiltAnimFromAnimSet(smi.master.anims, num);
		}

		// Token: 0x0600A6D4 RID: 42708 RVA: 0x003AEC93 File Offset: 0x003ACE93
		private static void RefreshPositionPercent(StandardCropPlant.StatesInstance smi, float dt)
		{
			StandardCropPlant.States.RefreshPositionPercent(smi);
		}

		// Token: 0x0600A6D5 RID: 42709 RVA: 0x003AEC9B File Offset: 0x003ACE9B
		private static void RefreshPositionPercent(StandardCropPlant.StatesInstance smi)
		{
			if (smi.master.preventGrowPositionUpdate)
			{
				return;
			}
			smi.master.RefreshPositionPercent();
		}

		// Token: 0x04008228 RID: 33320
		public StandardCropPlant.States.AliveStates alive;

		// Token: 0x04008229 RID: 33321
		public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State dead;

		// Token: 0x0400822A RID: 33322
		public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.PlantAliveSubState blighted;

		// Token: 0x02002892 RID: 10386
		public class AliveStates : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.PlantAliveSubState
		{
			// Token: 0x0400B3E0 RID: 46048
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State pre_idle;

			// Token: 0x0400B3E1 RID: 46049
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State idle;

			// Token: 0x0400B3E2 RID: 46050
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State pre_fruiting;

			// Token: 0x0400B3E3 RID: 46051
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State fruiting_lost;

			// Token: 0x0400B3E4 RID: 46052
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State barren;

			// Token: 0x0400B3E5 RID: 46053
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State fruiting;

			// Token: 0x0400B3E6 RID: 46054
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State wilting;

			// Token: 0x0400B3E7 RID: 46055
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State destroy;

			// Token: 0x0400B3E8 RID: 46056
			public GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.State harvest;
		}
	}

	// Token: 0x02001B49 RID: 6985
	public class StatesInstance : GameStateMachine<StandardCropPlant.States, StandardCropPlant.StatesInstance, StandardCropPlant, object>.GameInstance
	{
		// Token: 0x0600A6D7 RID: 42711 RVA: 0x003AECBE File Offset: 0x003ACEBE
		public StatesInstance(StandardCropPlant master)
			: base(master)
		{
		}
	}
}
