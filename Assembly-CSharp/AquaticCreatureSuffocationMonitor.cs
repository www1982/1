using System;
using UnityEngine;

// Token: 0x02000589 RID: 1417
public class AquaticCreatureSuffocationMonitor : GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>
{
	// Token: 0x06002061 RID: 8289 RVA: 0x000BAF48 File Offset: 0x000B9148
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.safe;
		this.root.TagTransition(GameTags.Dead, this.dead, false);
		this.safe.Transition(this.suffocating, new StateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.Transition.ConditionCallback(AquaticCreatureSuffocationMonitor.IsSuffocating), UpdateRate.SIM_1000ms).Update(new Action<AquaticCreatureSuffocationMonitor.Instance, float>(AquaticCreatureSuffocationMonitor.RecoveryDeathTimerUpdate), UpdateRate.SIM_200ms, false);
		this.suffocating.ParamTransition<float>(this.DeathTimer, this.die, new StateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.Parameter<float>.Callback(AquaticCreatureSuffocationMonitor.CanNotHoldAnymore)).Transition(this.safe, new StateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.Transition.ConditionCallback(AquaticCreatureSuffocationMonitor.CanBreath), UpdateRate.SIM_1000ms).ToggleStatusItem(Db.Get().CreatureStatusItems.AquaticCreatureSuffocating, null)
			.Update(new Action<AquaticCreatureSuffocationMonitor.Instance, float>(AquaticCreatureSuffocationMonitor.DeathTimerUpdate), UpdateRate.SIM_200ms, false);
		this.die.Enter(new StateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.State.Callback(AquaticCreatureSuffocationMonitor.Kill));
		this.dead.DoNothing();
	}

	// Token: 0x06002062 RID: 8290 RVA: 0x000BB036 File Offset: 0x000B9236
	public static bool IsSuffocating(AquaticCreatureSuffocationMonitor.Instance smi)
	{
		return !smi.CanBreath();
	}

	// Token: 0x06002063 RID: 8291 RVA: 0x000BB041 File Offset: 0x000B9241
	public static bool CanBreath(AquaticCreatureSuffocationMonitor.Instance smi)
	{
		return smi.CanBreath();
	}

	// Token: 0x06002064 RID: 8292 RVA: 0x000BB049 File Offset: 0x000B9249
	public static bool CanNotHoldAnymore(AquaticCreatureSuffocationMonitor.Instance smi, float deathTimerValue)
	{
		return deathTimerValue > smi.def.DeathTimerDuration;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x000BB059 File Offset: 0x000B9259
	public static void DeathTimerUpdate(AquaticCreatureSuffocationMonitor.Instance smi, float dt)
	{
		smi.sm.DeathTimer.Set(smi.DeathTimerValue + dt, smi, false);
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x000BB076 File Offset: 0x000B9276
	public static void RecoveryDeathTimerUpdate(AquaticCreatureSuffocationMonitor.Instance smi, float dt)
	{
		if (smi.DeathTimerValue > 0f)
		{
			smi.sm.DeathTimer.Set(Mathf.Max(smi.DeathTimerValue - dt * smi.def.RecoveryModifier, 0f), smi, false);
		}
	}

	// Token: 0x06002067 RID: 8295 RVA: 0x000BB0B6 File Offset: 0x000B92B6
	public static void Kill(AquaticCreatureSuffocationMonitor.Instance smi)
	{
		smi.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Suffocation);
	}

	// Token: 0x040012DD RID: 4829
	public GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.State safe;

	// Token: 0x040012DE RID: 4830
	public GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.State suffocating;

	// Token: 0x040012DF RID: 4831
	public GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.State die;

	// Token: 0x040012E0 RID: 4832
	public GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.State dead;

	// Token: 0x040012E1 RID: 4833
	public StateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.FloatParameter DeathTimer;

	// Token: 0x020013D9 RID: 5081
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006AD5 RID: 27349
		public float DeathTimerDuration = 2400f;

		// Token: 0x04006AD6 RID: 27350
		public float RecoveryModifier = 4f;
	}

	// Token: 0x020013DA RID: 5082
	public new class Instance : GameStateMachine<AquaticCreatureSuffocationMonitor, AquaticCreatureSuffocationMonitor.Instance, IStateMachineTarget, AquaticCreatureSuffocationMonitor.Def>.GameInstance
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06008B8F RID: 35727 RVA: 0x00353019 File Offset: 0x00351219
		public float DeathTimerValue
		{
			get
			{
				return base.sm.DeathTimer.Get(this);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06008B90 RID: 35728 RVA: 0x0035302C File Offset: 0x0035122C
		public float TimeUntilDeath
		{
			get
			{
				return Mathf.Max(base.smi.def.DeathTimerDuration - this.DeathTimerValue, 0f);
			}
		}

		// Token: 0x06008B91 RID: 35729 RVA: 0x0035304F File Offset: 0x0035124F
		public Instance(IStateMachineTarget master, AquaticCreatureSuffocationMonitor.Def def)
			: base(master, def)
		{
			this.pickupable = base.GetComponent<Pickupable>();
		}

		// Token: 0x06008B92 RID: 35730 RVA: 0x00353068 File Offset: 0x00351268
		public bool CanBreath()
		{
			int num = Grid.PosToCell(this);
			return !(this.pickupable.storage == null) || Grid.IsSubstantialLiquid(num, 0.35f);
		}

		// Token: 0x04006AD7 RID: 27351
		private Pickupable pickupable;
	}
}
