using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000871 RID: 2161
public class HappinessMonitor : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>
{
	// Token: 0x06003B6E RID: 15214 RVA: 0x00149770 File Offset: 0x00147970
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.happy, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy), UpdateRate.SIM_1000ms).Transition(this.neutral, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsNeutral), UpdateRate.SIM_1000ms).Transition(this.glum, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsGlum), UpdateRate.SIM_1000ms)
			.Transition(this.miserable, new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsMisirable), UpdateRate.SIM_1000ms);
		this.happy.DefaultState(this.happy.wild).Transition(this.satisfied, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsHappy)), UpdateRate.SIM_1000ms).ToggleTag(GameTags.Creatures.Happy);
		this.happy.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.happyWildEffect).TagTransition(GameTags.Creatures.Wild, this.happy.tame, true);
		this.happy.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.happyTameEffect).TagTransition(GameTags.Creatures.Wild, this.happy.wild, false);
		this.neutral.DefaultState(this.neutral.wild).Transition(this.satisfied, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsNeutral)), UpdateRate.SIM_1000ms);
		this.neutral.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.neutralWildEffect).TagTransition(GameTags.Creatures.Wild, this.neutral.tame, true);
		this.neutral.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.neutralTameEffect).TagTransition(GameTags.Creatures.Wild, this.neutral.wild, false);
		this.glum.DefaultState(this.glum.wild).Transition(this.satisfied, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsGlum)), UpdateRate.SIM_1000ms).ToggleTag(GameTags.Creatures.Unhappy);
		this.glum.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.glumWildEffect).TagTransition(GameTags.Creatures.Wild, this.glum.tame, true);
		this.glum.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.glumTameEffect).TagTransition(GameTags.Creatures.Wild, this.glum.wild, false);
		this.miserable.DefaultState(this.miserable.wild).Transition(this.satisfied, GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Not(new StateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.Transition.ConditionCallback(HappinessMonitor.IsMisirable)), UpdateRate.SIM_1000ms).ToggleTag(GameTags.Creatures.Unhappy);
		this.miserable.wild.ToggleEffect((HappinessMonitor.Instance smi) => this.miserableWildEffect).TagTransition(GameTags.Creatures.Wild, this.miserable.tame, true);
		this.miserable.tame.ToggleEffect((HappinessMonitor.Instance smi) => this.miserableTameEffect).TagTransition(GameTags.Creatures.Wild, this.miserable.wild, false);
		this.happyWildEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY_WILD.NAME, CREATURES.MODIFIERS.HAPPY_WILD.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.happyTameEffect = new Effect("Happy", CREATURES.MODIFIERS.HAPPY_TAME.NAME, CREATURES.MODIFIERS.HAPPY_TAME.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.neutralWildEffect = new Effect("Neutral", CREATURES.MODIFIERS.NEUTRAL.NAME, CREATURES.MODIFIERS.NEUTRAL.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.neutralTameEffect = new Effect("Neutral", CREATURES.MODIFIERS.NEUTRAL.NAME, CREATURES.MODIFIERS.NEUTRAL.TOOLTIP, 0f, true, false, false, null, -1f, 0f, null, "");
		this.glumWildEffect = new Effect("Glum", CREATURES.MODIFIERS.GLUM.NAME, CREATURES.MODIFIERS.GLUM.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.glumTameEffect = new Effect("Glum", CREATURES.MODIFIERS.GLUM.NAME, CREATURES.MODIFIERS.GLUM.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.miserableWildEffect = new Effect("Miserable", CREATURES.MODIFIERS.MISERABLE.NAME, CREATURES.MODIFIERS.MISERABLE.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.miserableTameEffect = new Effect("Miserable", CREATURES.MODIFIERS.MISERABLE.NAME, CREATURES.MODIFIERS.MISERABLE.TOOLTIP, 0f, true, false, true, null, -1f, 0f, null, "");
		this.happyTameEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, 9f, CREATURES.MODIFIERS.HAPPY_TAME.NAME, true, false, true));
		this.glumWildEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -15f, CREATURES.MODIFIERS.GLUM.NAME, false, false, true));
		this.glumTameEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -80f, CREATURES.MODIFIERS.GLUM.NAME, false, false, true));
		this.miserableTameEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -80f, CREATURES.MODIFIERS.MISERABLE.NAME, false, false, true));
		this.miserableTameEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, -1f, CREATURES.MODIFIERS.MISERABLE.NAME, true, false, true));
		this.miserableWildEffect.Add(new AttributeModifier(Db.Get().CritterAttributes.Metabolism.Id, -15f, CREATURES.MODIFIERS.MISERABLE.NAME, false, false, true));
		this.miserableWildEffect.Add(new AttributeModifier(Db.Get().Amounts.Fertility.deltaAttribute.Id, -1f, CREATURES.MODIFIERS.MISERABLE.NAME, true, false, true));
	}

	// Token: 0x06003B6F RID: 15215 RVA: 0x00149DE3 File Offset: 0x00147FE3
	private static bool IsHappy(HappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() >= smi.def.happyThreshold;
	}

	// Token: 0x06003B70 RID: 15216 RVA: 0x00149E00 File Offset: 0x00148000
	private static bool IsNeutral(HappinessMonitor.Instance smi)
	{
		float totalValue = smi.happiness.GetTotalValue();
		return totalValue > smi.def.glumThreshold && totalValue < smi.def.happyThreshold;
	}

	// Token: 0x06003B71 RID: 15217 RVA: 0x00149E38 File Offset: 0x00148038
	private static bool IsGlum(HappinessMonitor.Instance smi)
	{
		float totalValue = smi.happiness.GetTotalValue();
		return totalValue > smi.def.miserableThreshold && totalValue <= smi.def.glumThreshold;
	}

	// Token: 0x06003B72 RID: 15218 RVA: 0x00149E72 File Offset: 0x00148072
	private static bool IsMisirable(HappinessMonitor.Instance smi)
	{
		return smi.happiness.GetTotalValue() <= smi.def.miserableThreshold;
	}

	// Token: 0x0400246E RID: 9326
	private GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State satisfied;

	// Token: 0x0400246F RID: 9327
	private HappinessMonitor.HappyState happy;

	// Token: 0x04002470 RID: 9328
	private HappinessMonitor.NeutralState neutral;

	// Token: 0x04002471 RID: 9329
	private HappinessMonitor.UnhappyState glum;

	// Token: 0x04002472 RID: 9330
	private HappinessMonitor.MiserableState miserable;

	// Token: 0x04002473 RID: 9331
	private Effect happyWildEffect;

	// Token: 0x04002474 RID: 9332
	private Effect happyTameEffect;

	// Token: 0x04002475 RID: 9333
	private Effect neutralTameEffect;

	// Token: 0x04002476 RID: 9334
	private Effect neutralWildEffect;

	// Token: 0x04002477 RID: 9335
	private Effect glumWildEffect;

	// Token: 0x04002478 RID: 9336
	private Effect glumTameEffect;

	// Token: 0x04002479 RID: 9337
	private Effect miserableWildEffect;

	// Token: 0x0400247A RID: 9338
	private Effect miserableTameEffect;

	// Token: 0x02001817 RID: 6167
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040077DF RID: 30687
		public float happyThreshold = 4f;

		// Token: 0x040077E0 RID: 30688
		public float glumThreshold = -1f;

		// Token: 0x040077E1 RID: 30689
		public float miserableThreshold = -10f;
	}

	// Token: 0x02001818 RID: 6168
	public class MiserableState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040077E2 RID: 30690
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040077E3 RID: 30691
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x02001819 RID: 6169
	public class NeutralState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040077E4 RID: 30692
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040077E5 RID: 30693
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x0200181A RID: 6170
	public class UnhappyState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040077E6 RID: 30694
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040077E7 RID: 30695
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x0200181B RID: 6171
	public class HappyState : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State
	{
		// Token: 0x040077E8 RID: 30696
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State wild;

		// Token: 0x040077E9 RID: 30697
		public GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.State tame;
	}

	// Token: 0x0200181C RID: 6172
	public new class Instance : GameStateMachine<HappinessMonitor, HappinessMonitor.Instance, IStateMachineTarget, HappinessMonitor.Def>.GameInstance
	{
		// Token: 0x06009B86 RID: 39814 RVA: 0x0038DF1B File Offset: 0x0038C11B
		public Instance(IStateMachineTarget master, HappinessMonitor.Def def)
			: base(master, def)
		{
			this.happiness = base.gameObject.GetAttributes().Add(Db.Get().CritterAttributes.Happiness);
		}

		// Token: 0x040077EA RID: 30698
		public AttributeInstance happiness;
	}
}
