using System;

// Token: 0x020006BE RID: 1726
public class BionicUpgrade_SM<SMType, StateMachineInstanceType> : GameStateMachine<SMType, StateMachineInstanceType, IStateMachineTarget, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def> where SMType : GameStateMachine<SMType, StateMachineInstanceType, IStateMachineTarget, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def> where StateMachineInstanceType : BionicUpgrade_SM<SMType, StateMachineInstanceType>.BaseInstance
{
	// Token: 0x06002A6B RID: 10859 RVA: 0x000F5AE3 File Offset: 0x000F3CE3
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.Inactive;
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x000F5AF4 File Offset: 0x000F3CF4
	public static bool IsOnline(BionicUpgrade_SM<SMType, StateMachineInstanceType>.BaseInstance smi)
	{
		return smi.IsOnline;
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x000F5AFC File Offset: 0x000F3CFC
	public static bool IsInBedTimeChore(BionicUpgrade_SM<SMType, StateMachineInstanceType>.BaseInstance smi)
	{
		return smi.IsInBedTimeChore;
	}

	// Token: 0x04001911 RID: 6417
	public GameStateMachine<SMType, StateMachineInstanceType, IStateMachineTarget, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def>.State Active;

	// Token: 0x04001912 RID: 6418
	public GameStateMachine<SMType, StateMachineInstanceType, IStateMachineTarget, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def>.State Inactive;

	// Token: 0x0200153F RID: 5439
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06009075 RID: 36981 RVA: 0x003601F1 File Offset: 0x0035E3F1
		public Def(string upgradeID)
		{
			this.UpgradeID = upgradeID;
		}

		// Token: 0x06009076 RID: 36982 RVA: 0x00360200 File Offset: 0x0035E400
		public virtual string GetDescription()
		{
			return "";
		}

		// Token: 0x04006F1C RID: 28444
		public string UpgradeID;

		// Token: 0x04006F1D RID: 28445
		public Func<StateMachine.Instance, StateMachine.Instance>[] StateMachinesWhenActive;
	}

	// Token: 0x02001540 RID: 5440
	public abstract class BaseInstance : GameStateMachine<SMType, StateMachineInstanceType, IStateMachineTarget, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def>.GameInstance, BionicUpgradeComponent.IWattageController
	{
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06009077 RID: 36983 RVA: 0x00360207 File Offset: 0x0035E407
		public bool IsInBedTimeChore
		{
			get
			{
				return this.bedTimeMonitor != null && this.bedTimeMonitor.IsBedTimeChoreRunning;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06009078 RID: 36984 RVA: 0x0036021E File Offset: 0x0035E41E
		public bool IsOnline
		{
			get
			{
				return this.batteryMonitor != null && this.batteryMonitor.IsOnline;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06009079 RID: 36985 RVA: 0x00360235 File Offset: 0x0035E435
		public BionicUpgradeComponentConfig.BionicUpgradeData Data
		{
			get
			{
				return BionicUpgradeComponentConfig.UpgradesData[base.def.UpgradeID];
			}
		}

		// Token: 0x0600907A RID: 36986 RVA: 0x00360251 File Offset: 0x0035E451
		public BaseInstance(IStateMachineTarget master, BionicUpgrade_SM<SMType, StateMachineInstanceType>.Def def)
			: base(master, def)
		{
			this.batteryMonitor = base.gameObject.GetSMI<BionicBatteryMonitor.Instance>();
			this.bedTimeMonitor = base.gameObject.GetSMI<BionicBedTimeMonitor.Instance>();
			this.RegisterMonitorToUpgradeComponent();
		}

		// Token: 0x0600907B RID: 36987 RVA: 0x00360284 File Offset: 0x0035E484
		private void RegisterMonitorToUpgradeComponent()
		{
			foreach (BionicUpgradesMonitor.UpgradeComponentSlot upgradeComponentSlot in base.gameObject.GetSMI<BionicUpgradesMonitor.Instance>().upgradeComponentSlots)
			{
				if (upgradeComponentSlot.HasUpgradeInstalled)
				{
					BionicUpgradeComponent installedUpgradeComponent = upgradeComponentSlot.installedUpgradeComponent;
					if (installedUpgradeComponent != null && !installedUpgradeComponent.HasWattageController)
					{
						this.upgradeComponent = installedUpgradeComponent;
						installedUpgradeComponent.SetWattageController(this);
						return;
					}
				}
			}
		}

		// Token: 0x0600907C RID: 36988 RVA: 0x003602E3 File Offset: 0x0035E4E3
		private void UnregisterMonitorToUpgradeComponent()
		{
			if (this.upgradeComponent != null)
			{
				this.upgradeComponent.SetWattageController(null);
			}
		}

		// Token: 0x0600907D RID: 36989
		public abstract float GetCurrentWattageCost();

		// Token: 0x0600907E RID: 36990
		public abstract string GetCurrentWattageCostName();

		// Token: 0x0600907F RID: 36991 RVA: 0x003602FF File Offset: 0x0035E4FF
		protected override void OnCleanUp()
		{
			this.UnregisterMonitorToUpgradeComponent();
			base.OnCleanUp();
		}

		// Token: 0x04006F1E RID: 28446
		protected BionicBedTimeMonitor.Instance bedTimeMonitor;

		// Token: 0x04006F1F RID: 28447
		protected BionicBatteryMonitor.Instance batteryMonitor;

		// Token: 0x04006F20 RID: 28448
		protected BionicUpgradeComponent upgradeComponent;
	}
}
