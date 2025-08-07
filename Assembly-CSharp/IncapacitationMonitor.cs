using System;
using UnityEngine;

// Token: 0x020009F6 RID: 2550
public class IncapacitationMonitor : GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance>
{
	// Token: 0x06004A68 RID: 19048 RVA: 0x001AF1B8 File Offset: 0x001AD3B8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.healthy;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.healthy.Update(delegate(IncapacitationMonitor.Instance smi, float dt)
		{
			smi.RecoverBleedOutStamina(dt, smi);
		}, UpdateRate.SIM_200ms, false).EventTransition(GameHashes.BecameIncapacitated, this.incapacitated, null);
		this.incapacitated.EventTransition(GameHashes.IncapacitationRecovery, this.healthy, null).ToggleTag(GameTags.Incapacitated).ToggleRecurringChore((IncapacitationMonitor.Instance smi) => new BeIncapacitatedChore(smi.master), null)
			.ParamTransition<float>(this.bleedOutStamina, this.die, GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.IsLTEZero)
			.ToggleUrge(Db.Get().Urges.BeIncapacitated)
			.Update(delegate(IncapacitationMonitor.Instance smi, float dt)
			{
				smi.Bleed(dt, smi);
			}, UpdateRate.SIM_200ms, false);
		this.die.Enter(delegate(IncapacitationMonitor.Instance smi)
		{
			smi.master.gameObject.GetSMI<DeathMonitor.Instance>().Kill(smi.GetCauseOfIncapacitation());
		});
	}

	// Token: 0x04003123 RID: 12579
	public GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.State healthy;

	// Token: 0x04003124 RID: 12580
	public GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.State start_recovery;

	// Token: 0x04003125 RID: 12581
	public GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.State incapacitated;

	// Token: 0x04003126 RID: 12582
	public GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.State die;

	// Token: 0x04003127 RID: 12583
	private StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter bleedOutStamina = new StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter(120f);

	// Token: 0x04003128 RID: 12584
	private StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter baseBleedOutSpeed = new StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter(1f);

	// Token: 0x04003129 RID: 12585
	private StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter baseStaminaRecoverSpeed = new StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter(1f);

	// Token: 0x0400312A RID: 12586
	private StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter maxBleedOutStamina = new StateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.FloatParameter(120f);

	// Token: 0x02001A63 RID: 6755
	public new class Instance : GameStateMachine<IncapacitationMonitor, IncapacitationMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A35E RID: 41822 RVA: 0x003A3D94 File Offset: 0x003A1F94
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			Health component = master.GetComponent<Health>();
			if (component)
			{
				component.canBeIncapacitated = true;
			}
		}

		// Token: 0x0600A35F RID: 41823 RVA: 0x003A3DBE File Offset: 0x003A1FBE
		public void Bleed(float dt, IncapacitationMonitor.Instance smi)
		{
			smi.sm.bleedOutStamina.Delta(dt * -smi.sm.baseBleedOutSpeed.Get(smi), smi);
		}

		// Token: 0x0600A360 RID: 41824 RVA: 0x003A3DE8 File Offset: 0x003A1FE8
		public void RecoverBleedOutStamina(float dt, IncapacitationMonitor.Instance smi)
		{
			smi.sm.bleedOutStamina.Delta(Mathf.Min(dt * smi.sm.baseStaminaRecoverSpeed.Get(smi), smi.sm.maxBleedOutStamina.Get(smi) - smi.sm.bleedOutStamina.Get(smi)), smi);
		}

		// Token: 0x0600A361 RID: 41825 RVA: 0x003A3E42 File Offset: 0x003A2042
		public float GetBleedLifeTime(IncapacitationMonitor.Instance smi)
		{
			return Mathf.Floor(smi.sm.bleedOutStamina.Get(smi) / smi.sm.baseBleedOutSpeed.Get(smi));
		}

		// Token: 0x0600A362 RID: 41826 RVA: 0x003A3E6C File Offset: 0x003A206C
		public Death GetCauseOfIncapacitation()
		{
			Health component = base.GetComponent<Health>();
			if (component.CauseOfIncapacitation == GameTags.RadiationSicknessIncapacitation)
			{
				return Db.Get().Deaths.Radiation;
			}
			if (component.CauseOfIncapacitation == GameTags.HitPointsDepleted)
			{
				return Db.Get().Deaths.Slain;
			}
			return Db.Get().Deaths.Generic;
		}
	}
}
