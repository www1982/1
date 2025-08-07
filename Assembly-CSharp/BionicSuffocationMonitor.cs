using System;
using Klei.AI;
using STRINGS;
using TUNING;

// Token: 0x020009D5 RID: 2517
public class BionicSuffocationMonitor : GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>
{
	// Token: 0x060049C5 RID: 18885 RVA: 0x001AB430 File Offset: 0x001A9630
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.normal;
		this.root.TagTransition(GameTags.Dead, this.dead, false);
		this.normal.ToggleAttributeModifier("Breathing", (BionicSuffocationMonitor.Instance smi) => smi.breathing, null).EventTransition(GameHashes.OxygenBreatherHasAirChanged, this.noOxygen, (BionicSuffocationMonitor.Instance smi) => !smi.IsBreathing());
		this.noOxygen.EventTransition(GameHashes.OxygenBreatherHasAirChanged, this.normal, (BionicSuffocationMonitor.Instance smi) => smi.IsBreathing()).TagTransition(GameTags.RecoveringBreath, this.normal, false).ToggleExpression(Db.Get().Expressions.Suffocate, null)
			.ToggleAttributeModifier("Holding Breath", (BionicSuffocationMonitor.Instance smi) => smi.holdingbreath, null)
			.ToggleTag(GameTags.NoOxygen)
			.DefaultState(this.noOxygen.holdingbreath);
		this.noOxygen.holdingbreath.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.HoldingBreath, null).Transition(this.noOxygen.suffocating, (BionicSuffocationMonitor.Instance smi) => smi.IsSuffocating(), UpdateRate.SIM_200ms);
		this.noOxygen.suffocating.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Suffocation, Db.Get().DuplicantStatusItems.Suffocating, null).Transition(this.death, (BionicSuffocationMonitor.Instance smi) => smi.HasSuffocated(), UpdateRate.SIM_200ms);
		this.death.Enter("SuffocationDeath", delegate(BionicSuffocationMonitor.Instance smi)
		{
			smi.Kill();
		});
		this.dead.DoNothing();
	}

	// Token: 0x040030A1 RID: 12449
	public BionicSuffocationMonitor.NoOxygenState noOxygen;

	// Token: 0x040030A2 RID: 12450
	public GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State normal;

	// Token: 0x040030A3 RID: 12451
	public GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State death;

	// Token: 0x040030A4 RID: 12452
	public GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State dead;

	// Token: 0x02001A04 RID: 6660
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A05 RID: 6661
	public class NoOxygenState : GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State
	{
		// Token: 0x04007E6C RID: 32364
		public GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State holdingbreath;

		// Token: 0x04007E6D RID: 32365
		public GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.State suffocating;
	}

	// Token: 0x02001A06 RID: 6662
	public new class Instance : GameStateMachine<BionicSuffocationMonitor, BionicSuffocationMonitor.Instance, IStateMachineTarget, BionicSuffocationMonitor.Def>.GameInstance
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x0600A1C1 RID: 41409 RVA: 0x0039F711 File Offset: 0x0039D911
		// (set) Token: 0x0600A1C2 RID: 41410 RVA: 0x0039F719 File Offset: 0x0039D919
		public OxygenBreather oxygenBreather { get; private set; }

		// Token: 0x0600A1C3 RID: 41411 RVA: 0x0039F724 File Offset: 0x0039D924
		public Instance(IStateMachineTarget master, BionicSuffocationMonitor.Def def)
			: base(master, def)
		{
			this.breath = Db.Get().Amounts.Breath.Lookup(master.gameObject);
			Klei.AI.Attribute deltaAttribute = Db.Get().Amounts.Breath.deltaAttribute;
			float breath_RATE = DUPLICANTSTATS.STANDARD.Breath.BREATH_RATE;
			this.breathing = new AttributeModifier(deltaAttribute.Id, breath_RATE, DUPLICANTS.MODIFIERS.BREATHING.NAME, false, false, true);
			this.holdingbreath = new AttributeModifier(deltaAttribute.Id, -breath_RATE, DUPLICANTS.MODIFIERS.HOLDINGBREATH.NAME, false, false, true);
			this.oxygenBreather = base.GetComponent<OxygenBreather>();
		}

		// Token: 0x0600A1C4 RID: 41412 RVA: 0x0039F7C9 File Offset: 0x0039D9C9
		public bool IsBreathing()
		{
			return this.oxygenBreather.HasOxygen || base.master.GetComponent<KPrefabID>().HasTag(GameTags.RecoveringBreath) || this.oxygenBreather.HasTag(GameTags.InTransitTube);
		}

		// Token: 0x0600A1C5 RID: 41413 RVA: 0x0039F801 File Offset: 0x0039DA01
		public bool HasSuffocated()
		{
			return this.breath.value <= 0f;
		}

		// Token: 0x0600A1C6 RID: 41414 RVA: 0x0039F818 File Offset: 0x0039DA18
		public bool IsSuffocating()
		{
			return this.breath.deltaAttribute.GetTotalValue() <= 0f && this.breath.value <= DUPLICANTSTATS.STANDARD.Breath.SUFFOCATE_AMOUNT;
		}

		// Token: 0x0600A1C7 RID: 41415 RVA: 0x0039F852 File Offset: 0x0039DA52
		public void Kill()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Suffocation);
		}

		// Token: 0x04007E6E RID: 32366
		private AmountInstance breath;

		// Token: 0x04007E6F RID: 32367
		public AttributeModifier breathing;

		// Token: 0x04007E70 RID: 32368
		public AttributeModifier holdingbreath;
	}
}
