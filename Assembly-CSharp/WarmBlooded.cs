using System;
using Klei;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000BDC RID: 3036
public class WarmBlooded : StateMachineComponent<WarmBlooded.StatesInstance>
{
	// Token: 0x06005AF6 RID: 23286 RVA: 0x0020DCD3 File Offset: 0x0020BED3
	public static bool IsCold(WarmBlooded.StatesInstance smi)
	{
		return !smi.IsSimpleHeatProducer() && smi.IsCold();
	}

	// Token: 0x06005AF7 RID: 23287 RVA: 0x0020DCE5 File Offset: 0x0020BEE5
	public static bool IsHot(WarmBlooded.StatesInstance smi)
	{
		return !smi.IsSimpleHeatProducer() && smi.IsHot();
	}

	// Token: 0x06005AF8 RID: 23288 RVA: 0x0020DCF8 File Offset: 0x0020BEF8
	public static void WarmingRegulator(WarmBlooded.StatesInstance smi, float dt)
	{
		PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
		float num = SimUtil.EnergyFlowToTemperatureDelta(smi.master.CoolingKW, component.Element.specificHeatCapacity, component.Mass);
		float num2 = smi.IdealTemperature - smi.BodyTemperature;
		float num3 = 1f;
		if ((num - smi.baseTemperatureModification.Value) * dt < num2)
		{
			num3 = Mathf.Clamp(num2 / ((num - smi.baseTemperatureModification.Value) * dt), 0f, 1f);
		}
		smi.bodyRegulator.SetValue(-num * num3);
		if (smi.master.complexity == WarmBlooded.ComplexityType.FullHomeostasis)
		{
			smi.burningCalories.SetValue(-smi.master.CoolingKW * num3 / smi.master.KCal2Joules);
		}
	}

	// Token: 0x06005AF9 RID: 23289 RVA: 0x0020DDBC File Offset: 0x0020BFBC
	public static void CoolingRegulator(WarmBlooded.StatesInstance smi, float dt)
	{
		PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
		float num = SimUtil.EnergyFlowToTemperatureDelta(smi.master.BaseGenerationKW, component.Element.specificHeatCapacity, component.Mass);
		float num2 = SimUtil.EnergyFlowToTemperatureDelta(smi.master.WarmingKW, component.Element.specificHeatCapacity, component.Mass);
		float num3 = smi.IdealTemperature - smi.BodyTemperature;
		float num4 = 1f;
		if (num2 + num > num3)
		{
			num4 = Mathf.Max(0f, num3 - num) / num2;
		}
		smi.bodyRegulator.SetValue(num2 * num4);
		if (smi.master.complexity == WarmBlooded.ComplexityType.FullHomeostasis)
		{
			smi.burningCalories.SetValue(-smi.master.WarmingKW * num4 * 1000f / smi.master.KCal2Joules);
		}
	}

	// Token: 0x06005AFA RID: 23290 RVA: 0x0020DE8E File Offset: 0x0020C08E
	protected override void OnPrefabInit()
	{
		this.temperature = Db.Get().Amounts.Get(this.TemperatureAmountName).Lookup(base.gameObject);
		this.primaryElement = base.GetComponent<PrimaryElement>();
	}

	// Token: 0x06005AFB RID: 23291 RVA: 0x0020DEC2 File Offset: 0x0020C0C2
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06005AFC RID: 23292 RVA: 0x0020DECF File Offset: 0x0020C0CF
	public void SetTemperatureImmediate(float t)
	{
		this.temperature.value = t;
	}

	// Token: 0x04003C5D RID: 15453
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04003C5E RID: 15454
	public AmountInstance temperature;

	// Token: 0x04003C5F RID: 15455
	private PrimaryElement primaryElement;

	// Token: 0x04003C60 RID: 15456
	public WarmBlooded.ComplexityType complexity = WarmBlooded.ComplexityType.FullHomeostasis;

	// Token: 0x04003C61 RID: 15457
	public string TemperatureAmountName = "Temperature";

	// Token: 0x04003C62 RID: 15458
	public float IdealTemperature = DUPLICANTSTATS.STANDARD.Temperature.Internal.IDEAL;

	// Token: 0x04003C63 RID: 15459
	public float BaseGenerationKW = DUPLICANTSTATS.STANDARD.BaseStats.DUPLICANT_BASE_GENERATION_KILOWATTS;

	// Token: 0x04003C64 RID: 15460
	public string BaseTemperatureModifierDescription = DUPLICANTS.MODEL.STANDARD.NAME;

	// Token: 0x04003C65 RID: 15461
	public float KCal2Joules = DUPLICANTSTATS.STANDARD.BaseStats.KCAL2JOULES;

	// Token: 0x04003C66 RID: 15462
	public float WarmingKW = DUPLICANTSTATS.STANDARD.BaseStats.DUPLICANT_WARMING_KILOWATTS;

	// Token: 0x04003C67 RID: 15463
	public float CoolingKW = DUPLICANTSTATS.STANDARD.BaseStats.DUPLICANT_COOLING_KILOWATTS;

	// Token: 0x04003C68 RID: 15464
	public string CaloriesModifierDescription = DUPLICANTS.MODIFIERS.BURNINGCALORIES.NAME;

	// Token: 0x04003C69 RID: 15465
	public string BodyRegulatorModifierDescription = DUPLICANTS.MODIFIERS.HOMEOSTASIS.NAME;

	// Token: 0x04003C6A RID: 15466
	public const float TRANSITION_DELAY_HOT = 3f;

	// Token: 0x04003C6B RID: 15467
	public const float TRANSITION_DELAY_COLD = 3f;

	// Token: 0x02001D0A RID: 7434
	public enum ComplexityType
	{
		// Token: 0x04008804 RID: 34820
		SimpleHeatProduction,
		// Token: 0x04008805 RID: 34821
		HomeostasisWithoutCaloriesImpact,
		// Token: 0x04008806 RID: 34822
		FullHomeostasis
	}

	// Token: 0x02001D0B RID: 7435
	public class StatesInstance : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.GameInstance
	{
		// Token: 0x0600ACED RID: 44269 RVA: 0x003C2FF0 File Offset: 0x003C11F0
		public StatesInstance(WarmBlooded smi)
			: base(smi)
		{
			this.baseTemperatureModification = new AttributeModifier(base.master.TemperatureAmountName + "Delta", 0f, base.master.BaseTemperatureModifierDescription, false, true, false);
			base.master.GetAttributes().Add(this.baseTemperatureModification);
			if (base.master.complexity != WarmBlooded.ComplexityType.SimpleHeatProduction)
			{
				this.bodyRegulator = new AttributeModifier(base.master.TemperatureAmountName + "Delta", 0f, base.master.BodyRegulatorModifierDescription, false, true, false);
				base.master.GetAttributes().Add(this.bodyRegulator);
			}
			if (base.master.complexity == WarmBlooded.ComplexityType.FullHomeostasis)
			{
				this.burningCalories = new AttributeModifier("CaloriesDelta", 0f, base.master.CaloriesModifierDescription, false, false, false);
				base.master.GetAttributes().Add(this.burningCalories);
			}
			base.master.SetTemperatureImmediate(this.IdealTemperature);
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600ACEE RID: 44270 RVA: 0x003C30FB File Offset: 0x003C12FB
		public float IdealTemperature
		{
			get
			{
				return base.master.IdealTemperature;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600ACEF RID: 44271 RVA: 0x003C3108 File Offset: 0x003C1308
		public float TemperatureDelta
		{
			get
			{
				return this.bodyRegulator.Value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x0600ACF0 RID: 44272 RVA: 0x003C3115 File Offset: 0x003C1315
		public float BodyTemperature
		{
			get
			{
				return base.master.primaryElement.Temperature;
			}
		}

		// Token: 0x0600ACF1 RID: 44273 RVA: 0x003C3127 File Offset: 0x003C1327
		public bool IsSimpleHeatProducer()
		{
			return base.master.complexity == WarmBlooded.ComplexityType.SimpleHeatProduction;
		}

		// Token: 0x0600ACF2 RID: 44274 RVA: 0x003C3137 File Offset: 0x003C1337
		public bool IsHot()
		{
			return this.BodyTemperature > this.IdealTemperature;
		}

		// Token: 0x0600ACF3 RID: 44275 RVA: 0x003C3147 File Offset: 0x003C1347
		public bool IsCold()
		{
			return this.BodyTemperature < this.IdealTemperature;
		}

		// Token: 0x04008807 RID: 34823
		public AttributeModifier baseTemperatureModification;

		// Token: 0x04008808 RID: 34824
		public AttributeModifier bodyRegulator;

		// Token: 0x04008809 RID: 34825
		public AttributeModifier burningCalories;
	}

	// Token: 0x02001D0C RID: 7436
	public class States : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded>
	{
		// Token: 0x0600ACF4 RID: 44276 RVA: 0x003C3158 File Offset: 0x003C1358
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.alive.normal;
			this.root.TagTransition(GameTags.Dead, this.dead, false).Enter(delegate(WarmBlooded.StatesInstance smi)
			{
				PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
				float num = SimUtil.EnergyFlowToTemperatureDelta(smi.master.BaseGenerationKW, component.Element.specificHeatCapacity, component.Mass);
				smi.baseTemperatureModification.SetValue(num);
				CreatureSimTemperatureTransfer component2 = smi.master.GetComponent<CreatureSimTemperatureTransfer>();
				component2.NonSimTemperatureModifiers.Add(smi.baseTemperatureModification);
				if (!smi.IsSimpleHeatProducer())
				{
					component2.NonSimTemperatureModifiers.Add(smi.bodyRegulator);
				}
			});
			this.alive.normal.Transition(this.alive.cold.transition, new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsCold), UpdateRate.SIM_200ms).Transition(this.alive.hot.transition, new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsHot), UpdateRate.SIM_200ms);
			this.alive.cold.transition.ScheduleGoTo(3f, this.alive.cold.regulating).Transition(this.alive.normal, GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Not(new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsCold)), UpdateRate.SIM_200ms);
			this.alive.cold.regulating.Transition(this.alive.normal, GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Not(new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsCold)), UpdateRate.SIM_200ms).Update("ColdRegulating", new Action<WarmBlooded.StatesInstance, float>(WarmBlooded.CoolingRegulator), UpdateRate.SIM_200ms, false).Exit(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.bodyRegulator.SetValue(0f);
				if (smi.master.complexity == WarmBlooded.ComplexityType.FullHomeostasis)
				{
					smi.burningCalories.SetValue(0f);
				}
			});
			this.alive.hot.transition.ScheduleGoTo(3f, this.alive.hot.regulating).Transition(this.alive.normal, GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Not(new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsHot)), UpdateRate.SIM_200ms);
			this.alive.hot.regulating.Transition(this.alive.normal, GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Not(new StateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.Transition.ConditionCallback(WarmBlooded.IsHot)), UpdateRate.SIM_200ms).Update("WarmRegulating", new Action<WarmBlooded.StatesInstance, float>(WarmBlooded.WarmingRegulator), UpdateRate.SIM_200ms, false).Exit(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.bodyRegulator.SetValue(0f);
			});
			this.dead.Enter(delegate(WarmBlooded.StatesInstance smi)
			{
				smi.master.enabled = false;
			});
		}

		// Token: 0x0400880A RID: 34826
		public WarmBlooded.States.AliveState alive;

		// Token: 0x0400880B RID: 34827
		public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State dead;

		// Token: 0x020028D5 RID: 10453
		public class RegulatingState : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State
		{
			// Token: 0x0400B4FC RID: 46332
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State transition;

			// Token: 0x0400B4FD RID: 46333
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State regulating;
		}

		// Token: 0x020028D6 RID: 10454
		public class AliveState : GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State
		{
			// Token: 0x0400B4FE RID: 46334
			public GameStateMachine<WarmBlooded.States, WarmBlooded.StatesInstance, WarmBlooded, object>.State normal;

			// Token: 0x0400B4FF RID: 46335
			public WarmBlooded.States.RegulatingState cold;

			// Token: 0x0400B500 RID: 46336
			public WarmBlooded.States.RegulatingState hot;
		}
	}
}
