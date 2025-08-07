using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x02000A14 RID: 2580
public class SuffocationMonitor : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>
{
	// Token: 0x06004B0F RID: 19215 RVA: 0x001B363C File Offset: 0x001B183C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.satisfied;
		this.root.TagTransition(GameTags.Dead, this.dead, false);
		this.satisfied.DefaultState(this.satisfied.normal).ToggleAttributeModifier("Breathing", (SuffocationMonitor.Instance smi) => smi.increaseBreathModifier, null).EventTransition(GameHashes.OxygenBreatherHasAirChanged, this.noOxygen, (SuffocationMonitor.Instance smi) => !smi.CanBreath())
			.Transition(this.noOxygen, (SuffocationMonitor.Instance smi) => !smi.CanBreath(), UpdateRate.SIM_200ms);
		this.satisfied.normal.Transition(this.satisfied.low, (SuffocationMonitor.Instance smi) => smi.oxygenBreather.IsLowOxygen(), UpdateRate.SIM_200ms);
		this.satisfied.low.Transition(this.satisfied.normal, (SuffocationMonitor.Instance smi) => !smi.oxygenBreather.IsLowOxygen(), UpdateRate.SIM_200ms).ToggleEffect("LowOxygen");
		this.noOxygen.EventTransition(GameHashes.OxygenBreatherHasAirChanged, this.satisfied, (SuffocationMonitor.Instance smi) => smi.CanBreath()).TagTransition(GameTags.RecoveringBreath, this.satisfied, false).ToggleExpression(Db.Get().Expressions.Suffocate, null)
			.ToggleAttributeModifier("Holding Breath", (SuffocationMonitor.Instance smi) => smi.decreaseBreathModifier, null)
			.ToggleTag(GameTags.NoOxygen)
			.DefaultState(this.noOxygen.holdingbreath);
		this.noOxygen.holdingbreath.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.HoldingBreath, null).Transition(this.noOxygen.suffocating, (SuffocationMonitor.Instance smi) => smi.IsSuffocating(), UpdateRate.SIM_200ms);
		this.noOxygen.suffocating.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.Suffocating, null).Transition(this.death, (SuffocationMonitor.Instance smi) => smi.HasSuffocated(), UpdateRate.SIM_200ms);
		this.death.Enter("SuffocationDeath", delegate(SuffocationMonitor.Instance smi)
		{
			smi.Kill();
		});
		this.dead.DoNothing();
	}

	// Token: 0x040031BE RID: 12734
	public SuffocationMonitor.SatisfiedState satisfied;

	// Token: 0x040031BF RID: 12735
	public SuffocationMonitor.NoOxygenState noOxygen;

	// Token: 0x040031C0 RID: 12736
	public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State death;

	// Token: 0x040031C1 RID: 12737
	public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State dead;

	// Token: 0x02001AB3 RID: 6835
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001AB4 RID: 6836
	public class NoOxygenState : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State
	{
		// Token: 0x0400808D RID: 32909
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State holdingbreath;

		// Token: 0x0400808E RID: 32910
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State suffocating;
	}

	// Token: 0x02001AB5 RID: 6837
	public class SatisfiedState : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State
	{
		// Token: 0x0400808F RID: 32911
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State normal;

		// Token: 0x04008090 RID: 32912
		public GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.State low;
	}

	// Token: 0x02001AB6 RID: 6838
	public new class Instance : GameStateMachine<SuffocationMonitor, SuffocationMonitor.Instance, IStateMachineTarget, SuffocationMonitor.Def>.GameInstance
	{
		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600A4A6 RID: 42150 RVA: 0x003A6B81 File Offset: 0x003A4D81
		// (set) Token: 0x0600A4A7 RID: 42151 RVA: 0x003A6B89 File Offset: 0x003A4D89
		public OxygenBreather oxygenBreather { get; private set; }

		// Token: 0x0600A4A8 RID: 42152 RVA: 0x003A6B94 File Offset: 0x003A4D94
		public Instance(IStateMachineTarget master, SuffocationMonitor.Def def)
			: base(master, def)
		{
			this.breath = Db.Get().Amounts.Breath.Lookup(master.gameObject);
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float breath_RATE = DUPLICANTSTATS.STANDARD.Breath.BREATH_RATE;
			this.increaseBreathModifier = new AttributeModifier(deltaAttribute.Id, breath_RATE, DUPLICANTS.MODIFIERS.BREATHING.NAME, false, false, true);
			this.decreaseBreathModifier = new AttributeModifier(deltaAttribute.Id, -breath_RATE, DUPLICANTS.MODIFIERS.HOLDINGBREATH.NAME, false, false, true);
			this.oxygenBreather = base.GetComponent<OxygenBreather>();
		}

		// Token: 0x0600A4A9 RID: 42153 RVA: 0x003A6C39 File Offset: 0x003A4E39
		public override void StartSM()
		{
			base.StartSM();
		}

		// Token: 0x0600A4AA RID: 42154 RVA: 0x003A6C41 File Offset: 0x003A4E41
		public bool CanBreath()
		{
			return this.oxygenBreather.prefabID.HasTag(GameTags.RecoveringBreath) || this.oxygenBreather.prefabID.HasTag(GameTags.InTransitTube) || this.oxygenBreather.HasOxygen;
		}

		// Token: 0x0600A4AB RID: 42155 RVA: 0x003A6C7E File Offset: 0x003A4E7E
		public bool HasSuffocated()
		{
			return this.breath.value <= 0f;
		}

		// Token: 0x0600A4AC RID: 42156 RVA: 0x003A6C95 File Offset: 0x003A4E95
		public bool IsSuffocating()
		{
			return this.breath.deltaAttribute.GetTotalValue() <= 0f && this.breath.value <= DUPLICANTSTATS.STANDARD.Breath.SUFFOCATE_AMOUNT;
		}

		// Token: 0x0600A4AD RID: 42157 RVA: 0x003A6CCF File Offset: 0x003A4ECF
		public void Kill()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Suffocation);
		}

		// Token: 0x04008091 RID: 32913
		private AmountInstance breath;

		// Token: 0x04008092 RID: 32914
		public AttributeModifier increaseBreathModifier;

		// Token: 0x04008093 RID: 32915
		public AttributeModifier decreaseBreathModifier;
	}
}
