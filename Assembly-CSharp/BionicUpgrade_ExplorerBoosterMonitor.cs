using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x020006BC RID: 1724
public class BionicUpgrade_ExplorerBoosterMonitor : BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>
{
	// Token: 0x06002A5C RID: 10844 RVA: 0x000F554C File Offset: 0x000F374C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.attachToBooster;
		this.attachToBooster.Enter(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State.Callback(BionicUpgrade_ExplorerBoosterMonitor.FindAndAttachToInstalledBooster)).GoTo(this.Inactive);
		this.Inactive.EventTransition(GameHashes.ScheduleBlocksChanged, this.Active, new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.ShouldBeActive)).EventTransition(GameHashes.ScheduleChanged, this.Active, new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.ShouldBeActive)).EventTransition(GameHashes.BionicOnline, this.Active, new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.ShouldBeActive))
			.EventTransition(GameHashes.MinionMigration, (BionicUpgrade_ExplorerBoosterMonitor.Instance smi) => Game.Instance, this.Active, new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.ShouldBeActive))
			.TriggerOnEnter(GameHashes.BionicUpgradeWattageChanged, null);
		this.Active.EventTransition(GameHashes.ScheduleBlocksChanged, this.Inactive, GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Not(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.IsInBedTimeChore))).EventTransition(GameHashes.ScheduleChanged, this.Inactive, GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Not(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.IsInBedTimeChore))).EventTransition(GameHashes.BionicOffline, this.Inactive, GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Not(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.IsOnline)))
			.EventTransition(GameHashes.MinionMigration, (BionicUpgrade_ExplorerBoosterMonitor.Instance smi) => Game.Instance, this.Inactive, GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Not(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.ShouldBeActive)))
			.DefaultState(this.Active.gatheringData);
		this.Active.gatheringData.OnSignal(this.ReadyToDiscoverSignal, this.Active.discover, new Func<BionicUpgrade_ExplorerBoosterMonitor.Instance, bool>(BionicUpgrade_ExplorerBoosterMonitor.IsReadyToDiscoverAndThereIsSomethingToDiscover)).ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicExplorerBooster, null).Update(new Action<BionicUpgrade_ExplorerBoosterMonitor.Instance, float>(BionicUpgrade_ExplorerBoosterMonitor.DataGatheringUpdate), UpdateRate.SIM_200ms, false);
		this.Active.discover.Enter(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State.Callback(BionicUpgrade_ExplorerBoosterMonitor.ConsumeAllData)).Enter(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State.Callback(BionicUpgrade_ExplorerBoosterMonitor.RevealUndiscoveredGeyser)).EnterTransition(this.Inactive, GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Not(new StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Transition.ConditionCallback(BionicUpgrade_ExplorerBoosterMonitor.IsThereGeysersToDiscover)))
			.GoTo(this.Active.gatheringData);
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x000F5796 File Offset: 0x000F3996
	public static bool ShouldBeActive(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		return BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.IsOnline(smi) && BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.IsInBedTimeChore(smi) && BionicUpgrade_ExplorerBoosterMonitor.IsThereGeysersToDiscover(smi);
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x000F57B0 File Offset: 0x000F39B0
	public static bool IsReadyToDiscoverAndThereIsSomethingToDiscover(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		return smi.IsReadyToDiscover && BionicUpgrade_ExplorerBoosterMonitor.IsThereGeysersToDiscover(smi);
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x000F57C2 File Offset: 0x000F39C2
	public static void ConsumeAllData(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		smi.ConsumeAllData();
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x000F57CA File Offset: 0x000F39CA
	public static void FindAndAttachToInstalledBooster(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		smi.Initialize();
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x000F57D2 File Offset: 0x000F39D2
	public static void DataGatheringUpdate(BionicUpgrade_ExplorerBoosterMonitor.Instance smi, float dt)
	{
		smi.GatheringDataUpdate(dt);
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x000F57DC File Offset: 0x000F39DC
	public static bool IsThereGeysersToDiscover(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		WorldContainer myWorld = smi.GetMyWorld();
		if (myWorld.id != 255)
		{
			List<WorldGenSpawner.Spawnable> list = new List<WorldGenSpawner.Spawnable>();
			list.AddRange(SaveGame.Instance.worldGenSpawner.GeInfoOfUnspawnedWithType<Geyser>(myWorld.id));
			list.AddRange(SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag("GeyserGeneric", myWorld.id, false));
			list.AddRange(SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag("OilWell", myWorld.id, false));
			return list.Count > 0;
		}
		return false;
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x000F5878 File Offset: 0x000F3A78
	public static void RevealUndiscoveredGeyser(BionicUpgrade_ExplorerBoosterMonitor.Instance smi)
	{
		WorldContainer myWorld = smi.GetMyWorld();
		if (myWorld.id != 255)
		{
			List<WorldGenSpawner.Spawnable> list = new List<WorldGenSpawner.Spawnable>();
			list.AddRange(SaveGame.Instance.worldGenSpawner.GeInfoOfUnspawnedWithType<Geyser>(myWorld.id));
			list.AddRange(SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag("GeyserGeneric", myWorld.id, false));
			list.AddRange(SaveGame.Instance.worldGenSpawner.GetSpawnablesWithTag("OilWell", myWorld.id, false));
			if (list.Count > 0)
			{
				WorldGenSpawner.Spawnable random = list.GetRandom<WorldGenSpawner.Spawnable>();
				int num;
				int num2;
				Grid.CellToXY(random.cell, out num, out num2);
				GridVisibility.Reveal(num, num2, 4, 4f);
				Notifier notifier = smi.gameObject.AddOrGet<Notifier>();
				Notification geyserDiscoveredNotification = smi.GetGeyserDiscoveredNotification();
				int cell = random.cell;
				geyserDiscoveredNotification.customClickCallback = delegate(object obj)
				{
					GameUtil.FocusCamera(cell, true);
				};
				notifier.Add(geyserDiscoveredNotification, "");
			}
		}
	}

	// Token: 0x0400190E RID: 6414
	public GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State attachToBooster;

	// Token: 0x0400190F RID: 6415
	public new BionicUpgrade_ExplorerBoosterMonitor.ActiveStates Active;

	// Token: 0x04001910 RID: 6416
	public StateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.Signal ReadyToDiscoverSignal;

	// Token: 0x02001538 RID: 5432
	public new class Def : BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def
	{
		// Token: 0x0600905B RID: 36955 RVA: 0x0035FE0B File Offset: 0x0035E00B
		public Def(string upgradeID)
			: base(upgradeID)
		{
		}

		// Token: 0x0600905C RID: 36956 RVA: 0x0035FE14 File Offset: 0x0035E014
		public override string GetDescription()
		{
			return "BionicUpgrade_ExplorerBoosterMonitor.Def description not implemented";
		}
	}

	// Token: 0x02001539 RID: 5433
	public class ActiveStates : GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State
	{
		// Token: 0x04006F12 RID: 28434
		public GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State gatheringData;

		// Token: 0x04006F13 RID: 28435
		public GameStateMachine<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance, IStateMachineTarget, BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.Def>.State discover;
	}

	// Token: 0x0200153A RID: 5434
	public new class Instance : BionicUpgrade_SM<BionicUpgrade_ExplorerBoosterMonitor, BionicUpgrade_ExplorerBoosterMonitor.Instance>.BaseInstance
	{
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x0600905E RID: 36958 RVA: 0x0035FE23 File Offset: 0x0035E023
		public bool IsReadyToDiscover
		{
			get
			{
				return this.explorerBooster != null && this.explorerBooster.IsReady;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x0600905F RID: 36959 RVA: 0x0035FE3A File Offset: 0x0035E03A
		public float CurrentProgress
		{
			get
			{
				if (this.explorerBooster != null)
				{
					return this.explorerBooster.Progress;
				}
				return 0f;
			}
		}

		// Token: 0x06009060 RID: 36960 RVA: 0x0035FE55 File Offset: 0x0035E055
		public Instance(IStateMachineTarget master, BionicUpgrade_ExplorerBoosterMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009061 RID: 36961 RVA: 0x0035FE60 File Offset: 0x0035E060
		public void Initialize()
		{
			foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in base.gameObject.GetSMI<BionicUpgradesMonitor.Instance>().upgradeComponentSlots)
			{
				if (upgradeComponentSlot.HasUpgradeInstalled)
				{
					BionicUpgrade_ExplorerBooster.Instance smi = upgradeComponentSlot.installedUpgradeComponent.GetSMI<BionicUpgrade_ExplorerBooster.Instance>();
					if (smi != null && !smi.IsBeingMonitored)
					{
						this.explorerBooster = smi;
						smi.SetMonitor(this);
						return;
					}
				}
			}
		}

		// Token: 0x06009062 RID: 36962 RVA: 0x0035FEBE File Offset: 0x0035E0BE
		protected override void OnCleanUp()
		{
			if (this.explorerBooster != null)
			{
				this.explorerBooster.SetMonitor(null);
			}
			base.OnCleanUp();
		}

		// Token: 0x06009063 RID: 36963 RVA: 0x0035FEDC File Offset: 0x0035E0DC
		public void GatheringDataUpdate(float dt)
		{
			bool isReadyToDiscover = this.IsReadyToDiscover;
			float num = ((dt == 0f) ? 0f : (dt / 600f));
			this.explorerBooster.AddData(num);
			if (this.IsReadyToDiscover && !isReadyToDiscover)
			{
				base.sm.ReadyToDiscoverSignal.Trigger(this);
			}
		}

		// Token: 0x06009064 RID: 36964 RVA: 0x0035FF2F File Offset: 0x0035E12F
		public void ConsumeAllData()
		{
			this.explorerBooster.SetDataProgress(0f);
		}

		// Token: 0x06009065 RID: 36965 RVA: 0x0035FF44 File Offset: 0x0035E144
		public Notification GetGeyserDiscoveredNotification()
		{
			return new Notification(DUPLICANTS.STATUSITEMS.BIONICEXPLORERBOOSTER.NOTIFICATION_NAME, NotificationType.MessageImportant, (List<Notification> notificationList, object data) => DUPLICANTS.STATUSITEMS.BIONICEXPLORERBOOSTER.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
		}

		// Token: 0x06009066 RID: 36966 RVA: 0x0035FF8E File Offset: 0x0035E18E
		public override float GetCurrentWattageCost()
		{
			if (base.IsInsideState(base.sm.Active))
			{
				return base.Data.WattageCost;
			}
			return 0f;
		}

		// Token: 0x06009067 RID: 36967 RVA: 0x0035FFB4 File Offset: 0x0035E1B4
		public override string GetCurrentWattageCostName()
		{
			float currentWattageCost = this.GetCurrentWattageCost();
			if (base.IsInsideState(base.sm.Active))
			{
				return string.Format(DUPLICANTS.MODIFIERS.BIONIC_WATTS.TOOLTIP.STANDARD_ACTIVE_TEMPLATE, this.upgradeComponent.GetProperName(), GameUtil.GetFormattedWattage(currentWattageCost, GameUtil.WattageFormatterUnit.Automatic, true));
			}
			return string.Format(DUPLICANTS.MODIFIERS.BIONIC_WATTS.TOOLTIP.STANDARD_INACTIVE_TEMPLATE, this.upgradeComponent.GetProperName(), GameUtil.GetFormattedWattage(this.upgradeComponent.PotentialWattage, GameUtil.WattageFormatterUnit.Automatic, true));
		}

		// Token: 0x04006F14 RID: 28436
		private BionicUpgrade_ExplorerBooster.Instance explorerBooster;
	}
}
