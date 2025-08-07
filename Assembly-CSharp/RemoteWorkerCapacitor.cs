using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A99 RID: 2713
public class RemoteWorkerCapacitor : StateMachineComponent<RemoteWorkerCapacitor.StatesInstance>
{
	// Token: 0x06004EAF RID: 20143 RVA: 0x001C7357 File Offset: 0x001C5557
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x001C736C File Offset: 0x001C556C
	public float ApplyDeltaEnergy(float delta)
	{
		float num = this.charge;
		this.charge = Mathf.Clamp(this.charge + delta, 0f, 60f);
		return this.charge - num;
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x001C73A5 File Offset: 0x001C55A5
	public float ChargeRatio
	{
		get
		{
			return this.charge / 60f;
		}
	}

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x001C73B3 File Offset: 0x001C55B3
	public float Charge
	{
		get
		{
			return this.charge;
		}
	}

	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x06004EB3 RID: 20147 RVA: 0x001C73BB File Offset: 0x001C55BB
	public bool IsLowPower
	{
		get
		{
			return this.charge < 12f;
		}
	}

	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x001C73CA File Offset: 0x001C55CA
	public bool IsOutOfPower
	{
		get
		{
			return this.charge < float.Epsilon;
		}
	}

	// Token: 0x0400343A RID: 13370
	[Serialize]
	private float charge;

	// Token: 0x0400343B RID: 13371
	public const float LOW_LEVEL = 12f;

	// Token: 0x0400343C RID: 13372
	public const float POWER_USE_RATE_J_PER_S = -0.1f;

	// Token: 0x0400343D RID: 13373
	public const float POWER_CHARGE_RATE_J_PER_S = 7.5f;

	// Token: 0x0400343E RID: 13374
	public const float CAPACITY_J = 60f;

	// Token: 0x02001B7A RID: 7034
	public class StatesInstance : GameStateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.GameInstance
	{
		// Token: 0x0600A7AE RID: 42926 RVA: 0x003B1BB6 File Offset: 0x003AFDB6
		public StatesInstance(RemoteWorkerCapacitor master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B7B RID: 7035
	public class States : GameStateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor>
	{
		// Token: 0x0600A7AF RID: 42927 RVA: 0x003B1BC0 File Offset: 0x003AFDC0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.InitializeStates(out default_state);
			default_state = this.ok;
			this.root.ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerCapacitorStatus, (RemoteWorkerCapacitor.StatesInstance smi) => smi.master);
			this.ok.Transition(this.out_of_power, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsOutOfPower), UpdateRate.SIM_200ms).Transition(this.low_power, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsLowPower), UpdateRate.SIM_200ms);
			this.low_power.Transition(this.out_of_power, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsOutOfPower), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsOkForPower), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerLowPower, null);
			this.out_of_power.Transition(this.low_power, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsLowPower), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.Transition.ConditionCallback(RemoteWorkerCapacitor.States.IsOkForPower), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerOutOfPower, null);
		}

		// Token: 0x0600A7B0 RID: 42928 RVA: 0x003B1CE5 File Offset: 0x003AFEE5
		public static bool IsOkForPower(RemoteWorkerCapacitor.StatesInstance smi)
		{
			return !smi.master.IsLowPower;
		}

		// Token: 0x0600A7B1 RID: 42929 RVA: 0x003B1CF5 File Offset: 0x003AFEF5
		public static bool IsLowPower(RemoteWorkerCapacitor.StatesInstance smi)
		{
			return smi.master.IsLowPower && !smi.master.IsOutOfPower;
		}

		// Token: 0x0600A7B2 RID: 42930 RVA: 0x003B1D14 File Offset: 0x003AFF14
		public static bool IsOutOfPower(RemoteWorkerCapacitor.StatesInstance smi)
		{
			return smi.master.IsOutOfPower;
		}

		// Token: 0x040082FD RID: 33533
		private GameStateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.State ok;

		// Token: 0x040082FE RID: 33534
		private GameStateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.State low_power;

		// Token: 0x040082FF RID: 33535
		private GameStateMachine<RemoteWorkerCapacitor.States, RemoteWorkerCapacitor.StatesInstance, RemoteWorkerCapacitor, object>.State out_of_power;
	}
}
