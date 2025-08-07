using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000852 RID: 2130
public class BeeHappinessMonitor : GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>
{
	// Token: 0x06003A7F RID: 14975 RVA: 0x001457C4 File Offset: 0x001439C4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.TriggerOnEnter(GameHashes.Satisfied, null).Transition(this.happy, new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).Transition(this.unhappy, new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsUnhappy), UpdateRate.SIM_1000ms)
			.ToggleEffect((BeeHappinessMonitor.Instance smi) => this.neutralEffect);
		this.happy.TriggerOnEnter(GameHashes.Happy, null).ToggleEffect((BeeHappinessMonitor.Instance smi) => this.happyEffect).Transition(this.satisfied, GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Not(new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms);
		this.unhappy.TriggerOnEnter(GameHashes.Unhappy, null).Transition(this.satisfied, GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Not(new StateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.Transition.ConditionCallback(BeeHappinessMonitor.IsUnhappy)), UpdateRate.SIM_1000ms).ToggleEffect((BeeHappinessMonitor.Instance smi) => this.unhappyEffect);
		this.happyEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY_WILD.NAME, CREATURES.MODIFIERS.HAPPY_WILD.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.neutralEffect = new Effect("Neutral", CREATURES.MODIFIERS.NEUTRAL.NAME, CREATURES.MODIFIERS.NEUTRAL.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.unhappyEffect = new Effect("Unhappy", CREATURES.MODIFIERS.GLUM.NAME, CREATURES.MODIFIERS.GLUM.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
	}

	// Token: 0x06003A80 RID: 14976 RVA: 0x00145963 File Offset: 0x00143B63
	private static bool IsHappy(BeeHappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() >= smi.def.happyThreshold;
	}

	// Token: 0x06003A81 RID: 14977 RVA: 0x00145980 File Offset: 0x00143B80
	private static bool IsUnhappy(BeeHappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() <= smi.def.unhappyThreshold;
	}

	// Token: 0x040023DC RID: 9180
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State satisfied;

	// Token: 0x040023DD RID: 9181
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State happy;

	// Token: 0x040023DE RID: 9182
	private GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.State unhappy;

	// Token: 0x040023DF RID: 9183
	private Effect happyEffect;

	// Token: 0x040023E0 RID: 9184
	private Effect neutralEffect;

	// Token: 0x040023E1 RID: 9185
	private Effect unhappyEffect;

	// Token: 0x020017D5 RID: 6101
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400771D RID: 30493
		public float happyThreshold = 4f;

		// Token: 0x0400771E RID: 30494
		public float unhappyThreshold = -1f;
	}

	// Token: 0x020017D6 RID: 6102
	public new class Instance : GameStateMachine<BeeHappinessMonitor, BeeHappinessMonitor.Instance, IStateMachineTarget, BeeHappinessMonitor.Def>.GameInstance
	{
		// Token: 0x06009A91 RID: 39569 RVA: 0x0038AD41 File Offset: 0x00388F41
		public Instance(IStateMachineTarget master, BeeHappinessMonitor.Def def)
			: base(master, def)
		{
			this.happiness = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Happiness);
		}

		// Token: 0x0400771F RID: 30495
		public AttributeInstance happiness;
	}
}
