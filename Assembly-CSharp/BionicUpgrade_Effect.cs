using System;
using Klei.AI;

// Token: 0x020006BA RID: 1722
public class BionicUpgrade_Effect : GameStateMachine<BionicUpgrade_Effect, BionicUpgrade_Effect.Instance, IStateMachineTarget, BionicUpgrade_Effect.Def>
{
	// Token: 0x06002A56 RID: 10838 RVA: 0x000F5470 File Offset: 0x000F3670
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.root;
		this.root.Enter(new StateMachine<BionicUpgrade_Effect, BionicUpgrade_Effect.Instance, IStateMachineTarget, BionicUpgrade_Effect.Def>.State.Callback(BionicUpgrade_Effect.EnableEffect)).Exit(new StateMachine<BionicUpgrade_Effect, BionicUpgrade_Effect.Instance, IStateMachineTarget, BionicUpgrade_Effect.Def>.State.Callback(BionicUpgrade_Effect.DisableEffect));
	}

	// Token: 0x06002A57 RID: 10839 RVA: 0x000F54AA File Offset: 0x000F36AA
	public static void EnableEffect(BionicUpgrade_Effect.Instance smi)
	{
		smi.ApplyEffect();
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x000F54B2 File Offset: 0x000F36B2
	public static void DisableEffect(BionicUpgrade_Effect.Instance smi)
	{
		smi.RemoveEffect();
	}

	// Token: 0x02001534 RID: 5428
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006F0F RID: 28431
		public string EFFECT_NAME;
	}

	// Token: 0x02001535 RID: 5429
	public new class Instance : GameStateMachine<BionicUpgrade_Effect, BionicUpgrade_Effect.Instance, IStateMachineTarget, BionicUpgrade_Effect.Def>.GameInstance
	{
		// Token: 0x06009050 RID: 36944 RVA: 0x0035FCEE File Offset: 0x0035DEEE
		public Instance(IStateMachineTarget master, BionicUpgrade_Effect.Def def)
			: base(master, def)
		{
			this.effects = base.GetComponent<Effects>();
		}

		// Token: 0x06009051 RID: 36945 RVA: 0x0035FD04 File Offset: 0x0035DF04
		public void ApplyEffect()
		{
			Effect effect = Db.Get().effects.Get(base.def.EFFECT_NAME);
			this.effects.Add(effect, false);
		}

		// Token: 0x06009052 RID: 36946 RVA: 0x0035FD3C File Offset: 0x0035DF3C
		public void RemoveEffect()
		{
			Effect effect = Db.Get().effects.Get(base.def.EFFECT_NAME);
			this.effects.Remove(effect);
		}

		// Token: 0x04006F10 RID: 28432
		private Effects effects;
	}
}
