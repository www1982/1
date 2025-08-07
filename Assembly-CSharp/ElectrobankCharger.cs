using System;
using UnityEngine;

// Token: 0x0200071F RID: 1823
public class ElectrobankCharger : GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>
{
	// Token: 0x06002DEE RID: 11758 RVA: 0x001075CC File Offset: 0x001057CC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noBattery;
		this.noBattery.PlayAnim("off").EventHandler(GameHashes.OnStorageChange, delegate(ElectrobankCharger.Instance smi, object data)
		{
			smi.QueueElectrobank(null);
		}).ParamTransition<bool>(this.hasElectrobank, this.charging, GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.IsTrue)
			.Enter(delegate(ElectrobankCharger.Instance smi)
			{
				smi.QueueElectrobank(null);
			});
		this.inoperational.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.charging, (ElectrobankCharger.Instance smi) => smi.master.GetComponent<Operational>().IsOperational);
		this.charging.QueueAnim("working_pre", false, null).QueueAnim("working_loop", true, null).Enter(delegate(ElectrobankCharger.Instance smi)
		{
			smi.QueueElectrobank(null);
			smi.master.GetComponent<Operational>().SetActive(true, false);
		})
			.Exit(delegate(ElectrobankCharger.Instance smi)
			{
				smi.master.GetComponent<Operational>().SetActive(false, false);
			})
			.ToggleStatusItem(Db.Get().BuildingStatusItems.PowerBankChargerInProgress, null)
			.Update(delegate(ElectrobankCharger.Instance smi, float dt)
			{
				smi.ChargeInternal(smi, dt);
			}, UpdateRate.SIM_EVERY_TICK, false)
			.EventTransition(GameHashes.OperationalChanged, this.inoperational, (ElectrobankCharger.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational)
			.ParamTransition<float>(this.internalChargeAmount, this.full, (ElectrobankCharger.Instance smi, float dt) => this.internalChargeAmount.Get(smi) >= 120000f);
		this.full.PlayAnim("working_pst").Enter(delegate(ElectrobankCharger.Instance smi)
		{
			smi.TransferChargeToElectrobank();
		}).OnAnimQueueComplete(this.noBattery);
	}

	// Token: 0x04001B0F RID: 6927
	public GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.State noBattery;

	// Token: 0x04001B10 RID: 6928
	public GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.State inoperational;

	// Token: 0x04001B11 RID: 6929
	public GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.State charging;

	// Token: 0x04001B12 RID: 6930
	public GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.State full;

	// Token: 0x04001B13 RID: 6931
	public StateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.FloatParameter internalChargeAmount;

	// Token: 0x04001B14 RID: 6932
	public StateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.BoolParameter hasElectrobank;

	// Token: 0x020015C1 RID: 5569
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015C2 RID: 5570
	public new class Instance : GameStateMachine<ElectrobankCharger, ElectrobankCharger.Instance, IStateMachineTarget, ElectrobankCharger.Def>.GameInstance
	{
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06009297 RID: 37527 RVA: 0x0036731E File Offset: 0x0036551E
		public Storage Storage
		{
			get
			{
				if (this.storage == null)
				{
					this.storage = base.GetComponent<Storage>();
				}
				return this.storage;
			}
		}

		// Token: 0x06009298 RID: 37528 RVA: 0x00367340 File Offset: 0x00365540
		public Instance(IStateMachineTarget master, ElectrobankCharger.Def def)
			: base(master, def)
		{
			this.meterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
		}

		// Token: 0x06009299 RID: 37529 RVA: 0x0036736D File Offset: 0x0036556D
		public void ChargeInternal(ElectrobankCharger.Instance smi, float dt)
		{
			smi.sm.internalChargeAmount.Delta(dt * 400f, smi);
			this.UpdateMeter();
		}

		// Token: 0x0600929A RID: 37530 RVA: 0x0036738E File Offset: 0x0036558E
		public void UpdateMeter()
		{
			this.meterController.SetPositionPercent(base.sm.internalChargeAmount.Get(base.smi) / 120000f);
		}

		// Token: 0x0600929B RID: 37531 RVA: 0x003673B7 File Offset: 0x003655B7
		public void TransferChargeToElectrobank()
		{
			this.targetElectrobank = Electrobank.ReplaceEmptyWithCharged(this.targetElectrobank, true);
			this.DequeueElectrobank();
		}

		// Token: 0x0600929C RID: 37532 RVA: 0x003673D4 File Offset: 0x003655D4
		public void DequeueElectrobank()
		{
			this.targetElectrobank = null;
			base.smi.sm.hasElectrobank.Set(false, base.smi, false);
			base.smi.sm.internalChargeAmount.Set(0f, base.smi, false);
			this.UpdateMeter();
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x00367430 File Offset: 0x00365630
		public void QueueElectrobank(object data = null)
		{
			if (this.targetElectrobank == null)
			{
				for (int i = 0; i < this.Storage.items.Count; i++)
				{
					GameObject gameObject = this.Storage.items[i];
					if (gameObject != null && gameObject.HasTag(GameTags.EmptyPortableBattery))
					{
						this.targetElectrobank = gameObject;
						base.smi.sm.hasElectrobank.Set(true, base.smi, false);
						break;
					}
				}
			}
			this.UpdateMeter();
		}

		// Token: 0x040070C4 RID: 28868
		private Storage storage;

		// Token: 0x040070C5 RID: 28869
		public GameObject targetElectrobank;

		// Token: 0x040070C6 RID: 28870
		private MeterController meterController;
	}
}
