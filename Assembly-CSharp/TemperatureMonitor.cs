using System;
using Klei.AI;
using TUNING;

// Token: 0x02000A17 RID: 2583
public class TemperatureMonitor : GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance>
{
	// Token: 0x06004B15 RID: 19221 RVA: 0x001B3A8C File Offset: 0x001B1C8C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.homeostatic;
		this.root.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.averageTemperature = smi.primaryElement.Temperature;
		}).Update("UpdateTemperature", delegate(TemperatureMonitor.Instance smi, float dt)
		{
			smi.UpdateTemperature(dt);
		}, UpdateRate.SIM_200ms, false);
		this.homeostatic.Transition(this.hyperthermic_pre, (TemperatureMonitor.Instance smi) => smi.IsHyperthermic(), UpdateRate.SIM_200ms).Transition(this.hypothermic_pre, (TemperatureMonitor.Instance smi) => smi.IsHypothermic(), UpdateRate.SIM_200ms).TriggerOnEnter(GameHashes.OptimalTemperatureAchieved, null);
		this.hyperthermic_pre.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.GoTo(this.hyperthermic);
		});
		this.hypothermic_pre.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.GoTo(this.hypothermic);
		});
		this.hyperthermic.Transition(this.homeostatic, (TemperatureMonitor.Instance smi) => !smi.IsHyperthermic(), UpdateRate.SIM_200ms).ToggleUrge(Db.Get().Urges.CoolDown);
		this.hypothermic.Transition(this.homeostatic, (TemperatureMonitor.Instance smi) => !smi.IsHypothermic(), UpdateRate.SIM_200ms).ToggleUrge(Db.Get().Urges.WarmUp);
	}

	// Token: 0x040031C6 RID: 12742
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State homeostatic;

	// Token: 0x040031C7 RID: 12743
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hyperthermic;

	// Token: 0x040031C8 RID: 12744
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hypothermic;

	// Token: 0x040031C9 RID: 12745
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hyperthermic_pre;

	// Token: 0x040031CA RID: 12746
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hypothermic_pre;

	// Token: 0x040031CB RID: 12747
	private const float TEMPERATURE_AVERAGING_RANGE = 4f;

	// Token: 0x040031CC RID: 12748
	public StateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.IntParameter warmUpCell;

	// Token: 0x040031CD RID: 12749
	public StateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.IntParameter coolDownCell;

	// Token: 0x02001ABC RID: 6844
	public new class Instance : GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A4CB RID: 42187 RVA: 0x003A717C File Offset: 0x003A537C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.primaryElement = base.GetComponent<PrimaryElement>();
			this.temperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
			this.warmUpQuery = new SafetyQuery(Game.Instance.safetyConditions.WarmUpChecker, base.GetComponent<KMonoBehaviour>(), int.MaxValue);
			this.coolDownQuery = new SafetyQuery(Game.Instance.safetyConditions.CoolDownChecker, base.GetComponent<KMonoBehaviour>(), int.MaxValue);
			this.navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x0600A4CC RID: 42188 RVA: 0x003A7228 File Offset: 0x003A5428
		public void UpdateTemperature(float dt)
		{
			base.smi.averageTemperature *= 1f - dt / 4f;
			base.smi.averageTemperature += base.smi.primaryElement.Temperature * (dt / 4f);
			base.smi.temperature.SetValue(base.smi.averageTemperature);
		}

		// Token: 0x0600A4CD RID: 42189 RVA: 0x003A729A File Offset: 0x003A549A
		public bool IsHyperthermic()
		{
			return this.temperature.value > this.HyperthermiaThreshold;
		}

		// Token: 0x0600A4CE RID: 42190 RVA: 0x003A72AF File Offset: 0x003A54AF
		public bool IsHypothermic()
		{
			return this.temperature.value < this.HypothermiaThreshold;
		}

		// Token: 0x0600A4CF RID: 42191 RVA: 0x003A72C4 File Offset: 0x003A54C4
		public float ExtremeTemperatureDelta()
		{
			if (this.temperature.value > this.HyperthermiaThreshold)
			{
				return this.temperature.value - this.HyperthermiaThreshold;
			}
			if (this.temperature.value < this.HypothermiaThreshold)
			{
				return this.temperature.value - this.HypothermiaThreshold;
			}
			return 0f;
		}

		// Token: 0x0600A4D0 RID: 42192 RVA: 0x003A7322 File Offset: 0x003A5522
		public float IdealTemperatureDelta()
		{
			return this.temperature.value - DUPLICANTSTATS.STANDARD.Temperature.Internal.IDEAL;
		}

		// Token: 0x0600A4D1 RID: 42193 RVA: 0x003A7344 File Offset: 0x003A5544
		public int GetWarmUpCell()
		{
			return base.sm.warmUpCell.Get(base.smi);
		}

		// Token: 0x0600A4D2 RID: 42194 RVA: 0x003A735C File Offset: 0x003A555C
		public int GetCoolDownCell()
		{
			return base.sm.coolDownCell.Get(base.smi);
		}

		// Token: 0x0600A4D3 RID: 42195 RVA: 0x003A7374 File Offset: 0x003A5574
		public void UpdateWarmUpCell()
		{
			this.warmUpQuery.Reset();
			this.navigator.RunQuery(this.warmUpQuery);
			base.sm.warmUpCell.Set(this.warmUpQuery.GetResultCell(), base.smi, false);
		}

		// Token: 0x0600A4D4 RID: 42196 RVA: 0x003A73C0 File Offset: 0x003A55C0
		public void UpdateCoolDownCell()
		{
			this.coolDownQuery.Reset();
			this.navigator.RunQuery(this.coolDownQuery);
			base.sm.coolDownCell.Set(this.coolDownQuery.GetResultCell(), base.smi, false);
		}

		// Token: 0x040080AC RID: 32940
		public AmountInstance temperature;

		// Token: 0x040080AD RID: 32941
		public PrimaryElement primaryElement;

		// Token: 0x040080AE RID: 32942
		private Navigator navigator;

		// Token: 0x040080AF RID: 32943
		private SafetyQuery warmUpQuery;

		// Token: 0x040080B0 RID: 32944
		private SafetyQuery coolDownQuery;

		// Token: 0x040080B1 RID: 32945
		public float averageTemperature;

		// Token: 0x040080B2 RID: 32946
		public float HypothermiaThreshold = 307.15f;

		// Token: 0x040080B3 RID: 32947
		public float HyperthermiaThreshold = 313.15f;
	}
}
