using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000A09 RID: 2569
public class ScaldingMonitor : GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>
{
	// Token: 0x06004AD0 RID: 19152 RVA: 0x001B1AD0 File Offset: 0x001AFCD0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.Enter(new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State.Callback(ScaldingMonitor.SetInitialAverageExternalTemperature)).EventHandler(GameHashes.OnUnequip, new GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.GameEvent.Callback(ScaldingMonitor.OnSuitUnequipped)).Update(new Action<ScaldingMonitor.Instance, float>(ScaldingMonitor.AverageExternalTemperatureUpdate), UpdateRate.SIM_200ms, false);
		this.idle.Transition(this.transitionToScalding, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScalding), UpdateRate.SIM_200ms).Transition(this.transitionToScolding, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScolding), UpdateRate.SIM_200ms);
		this.transitionToScalding.Transition(this.idle, GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Not(new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScalding)), UpdateRate.SIM_200ms).Transition(this.scalding, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScaldingTimed), UpdateRate.SIM_200ms);
		this.transitionToScolding.Transition(this.idle, GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Not(new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScolding)), UpdateRate.SIM_200ms).Transition(this.scolding, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.IsScoldingTimed), UpdateRate.SIM_200ms);
		this.scalding.Transition(this.idle, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.CanEscapeScalding), UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Hot, null).ToggleThought(Db.Get().Thoughts.Hot, null)
			.ToggleStatusItem(Db.Get().CreatureStatusItems.Scalding, (ScaldingMonitor.Instance smi) => smi)
			.Update(new Action<ScaldingMonitor.Instance, float>(ScaldingMonitor.TakeScaldDamage), UpdateRate.SIM_1000ms, false);
		this.scolding.Transition(this.idle, new StateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.Transition.ConditionCallback(ScaldingMonitor.CanEscapeScolding), UpdateRate.SIM_200ms).ToggleExpression(Db.Get().Expressions.Cold, null).ToggleThought(Db.Get().Thoughts.Cold, null)
			.ToggleStatusItem(Db.Get().CreatureStatusItems.Scolding, (ScaldingMonitor.Instance smi) => smi)
			.Update(new Action<ScaldingMonitor.Instance, float>(ScaldingMonitor.TakeColdDamage), UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x06004AD1 RID: 19153 RVA: 0x001B1CFA File Offset: 0x001AFEFA
	public static void OnSuitUnequipped(ScaldingMonitor.Instance smi, object obj)
	{
		if (obj != null && ((Equippable)obj).HasTag(GameTags.AirtightSuit))
		{
			smi.ResetExternalTemperatureAverage();
		}
	}

	// Token: 0x06004AD2 RID: 19154 RVA: 0x001B1D17 File Offset: 0x001AFF17
	public static void SetInitialAverageExternalTemperature(ScaldingMonitor.Instance smi)
	{
		smi.AverageExternalTemperature = smi.GetCurrentExternalTemperature();
	}

	// Token: 0x06004AD3 RID: 19155 RVA: 0x001B1D25 File Offset: 0x001AFF25
	public static bool CanEscapeScalding(ScaldingMonitor.Instance smi)
	{
		return !smi.IsScalding() && smi.timeinstate > 1f;
	}

	// Token: 0x06004AD4 RID: 19156 RVA: 0x001B1D3E File Offset: 0x001AFF3E
	public static bool CanEscapeScolding(ScaldingMonitor.Instance smi)
	{
		return !smi.IsScolding() && smi.timeinstate > 1f;
	}

	// Token: 0x06004AD5 RID: 19157 RVA: 0x001B1D57 File Offset: 0x001AFF57
	public static bool IsScaldingTimed(ScaldingMonitor.Instance smi)
	{
		return smi.IsScalding() && smi.timeinstate > 1f;
	}

	// Token: 0x06004AD6 RID: 19158 RVA: 0x001B1D70 File Offset: 0x001AFF70
	public static bool IsScalding(ScaldingMonitor.Instance smi)
	{
		return smi.IsScalding();
	}

	// Token: 0x06004AD7 RID: 19159 RVA: 0x001B1D78 File Offset: 0x001AFF78
	public static bool IsScolding(ScaldingMonitor.Instance smi)
	{
		return smi.IsScolding();
	}

	// Token: 0x06004AD8 RID: 19160 RVA: 0x001B1D80 File Offset: 0x001AFF80
	public static bool IsScoldingTimed(ScaldingMonitor.Instance smi)
	{
		return smi.IsScolding() && smi.timeinstate > 1f;
	}

	// Token: 0x06004AD9 RID: 19161 RVA: 0x001B1D99 File Offset: 0x001AFF99
	public static void TakeScaldDamage(ScaldingMonitor.Instance smi, float dt)
	{
		smi.TemperatureDamage(dt);
	}

	// Token: 0x06004ADA RID: 19162 RVA: 0x001B1DA2 File Offset: 0x001AFFA2
	public static void TakeColdDamage(ScaldingMonitor.Instance smi, float dt)
	{
		smi.TemperatureDamage(dt);
	}

	// Token: 0x06004ADB RID: 19163 RVA: 0x001B1DAC File Offset: 0x001AFFAC
	public static void AverageExternalTemperatureUpdate(ScaldingMonitor.Instance smi, float dt)
	{
		smi.AverageExternalTemperature *= Mathf.Max(0f, 1f - dt / 6f);
		smi.AverageExternalTemperature += smi.GetCurrentExternalTemperature() * (dt / 6f);
	}

	// Token: 0x0400317E RID: 12670
	private const float TRANSITION_TO_DELAY = 1f;

	// Token: 0x0400317F RID: 12671
	private const float TEMPERATURE_AVERAGING_RANGE = 6f;

	// Token: 0x04003180 RID: 12672
	private const float MIN_SCALD_INTERVAL = 5f;

	// Token: 0x04003181 RID: 12673
	private const float SCALDING_DAMAGE_AMOUNT = 10f;

	// Token: 0x04003182 RID: 12674
	public GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State idle;

	// Token: 0x04003183 RID: 12675
	public GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State transitionToScalding;

	// Token: 0x04003184 RID: 12676
	public GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State transitionToScolding;

	// Token: 0x04003185 RID: 12677
	public GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State scalding;

	// Token: 0x04003186 RID: 12678
	public GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.State scolding;

	// Token: 0x02001A95 RID: 6805
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04008024 RID: 32804
		public float defaultScaldingTreshold = 345f;

		// Token: 0x04008025 RID: 32805
		public float defaultScoldingTreshold = 183f;
	}

	// Token: 0x02001A96 RID: 6806
	public new class Instance : GameStateMachine<ScaldingMonitor, ScaldingMonitor.Instance, IStateMachineTarget, ScaldingMonitor.Def>.GameInstance
	{
		// Token: 0x0600A41D RID: 42013 RVA: 0x003A5600 File Offset: 0x003A3800
		public Instance(IStateMachineTarget master, ScaldingMonitor.Def def)
			: base(master, def)
		{
			this.internalTemperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
			this.baseScalindingThreshold = new AttributeModifier("ScaldingThreshold", def.defaultScaldingTreshold, DUPLICANTS.STATS.SKIN_DURABILITY.NAME, false, false, true);
			this.baseScoldingThreshold = new AttributeModifier("ScoldingThreshold", def.defaultScoldingTreshold, DUPLICANTS.STATS.SKIN_DURABILITY.NAME, false, false, true);
			this.attributes = base.gameObject.GetAttributes();
		}

		// Token: 0x0600A41E RID: 42014 RVA: 0x003A568C File Offset: 0x003A388C
		public override void StartSM()
		{
			base.smi.attributes.Get(Db.Get().Attributes.ScaldingThreshold).Add(this.baseScalindingThreshold);
			base.smi.attributes.Get(Db.Get().Attributes.ScoldingThreshold).Add(this.baseScoldingThreshold);
			base.StartSM();
		}

		// Token: 0x0600A41F RID: 42015 RVA: 0x003A56F4 File Offset: 0x003A38F4
		public bool IsScalding()
		{
			int num = Grid.PosToCell(base.gameObject);
			return Grid.IsValidCell(num) && Grid.Element[num].id != SimHashes.Vacuum && Grid.Element[num].id != SimHashes.Void && this.AverageExternalTemperature > this.GetScaldingThreshold();
		}

		// Token: 0x0600A420 RID: 42016 RVA: 0x003A574B File Offset: 0x003A394B
		public float GetScaldingThreshold()
		{
			return base.smi.attributes.GetValue("ScaldingThreshold");
		}

		// Token: 0x0600A421 RID: 42017 RVA: 0x003A5764 File Offset: 0x003A3964
		public bool IsScolding()
		{
			int num = Grid.PosToCell(base.gameObject);
			return Grid.IsValidCell(num) && Grid.Element[num].id != SimHashes.Vacuum && Grid.Element[num].id != SimHashes.Void && this.AverageExternalTemperature < this.GetScoldingThreshold();
		}

		// Token: 0x0600A422 RID: 42018 RVA: 0x003A57BB File Offset: 0x003A39BB
		public float GetScoldingThreshold()
		{
			return base.smi.attributes.GetValue("ScoldingThreshold");
		}

		// Token: 0x0600A423 RID: 42019 RVA: 0x003A57D2 File Offset: 0x003A39D2
		public void TemperatureDamage(float dt)
		{
			if (this.health != null && Time.time - this.lastScaldTime > 5f)
			{
				this.lastScaldTime = Time.time;
				this.health.Damage(dt * 10f);
			}
		}

		// Token: 0x0600A424 RID: 42020 RVA: 0x003A5812 File Offset: 0x003A3A12
		public void ResetExternalTemperatureAverage()
		{
			base.smi.AverageExternalTemperature = this.internalTemperature.value;
		}

		// Token: 0x0600A425 RID: 42021 RVA: 0x003A582C File Offset: 0x003A3A2C
		public float GetCurrentExternalTemperature()
		{
			int num = Grid.PosToCell(base.gameObject);
			if (this.occupyArea != null)
			{
				float num2 = 0f;
				int num3 = 0;
				for (int i = 0; i < this.occupyArea.OccupiedCellsOffsets.Length; i++)
				{
					int num4 = Grid.OffsetCell(num, this.occupyArea.OccupiedCellsOffsets[i]);
					if (Grid.IsValidCell(num4))
					{
						bool flag = Grid.Element[num4].id == SimHashes.Vacuum || Grid.Element[num4].id == SimHashes.Void;
						num3++;
						num2 += (flag ? this.internalTemperature.value : Grid.Temperature[num4]);
					}
				}
				return num2 / (float)Mathf.Max(1, num3);
			}
			if (Grid.Element[num].id != SimHashes.Vacuum && Grid.Element[num].id != SimHashes.Void)
			{
				return Grid.Temperature[num];
			}
			return this.internalTemperature.value;
		}

		// Token: 0x04008026 RID: 32806
		public float AverageExternalTemperature;

		// Token: 0x04008027 RID: 32807
		private float lastScaldTime;

		// Token: 0x04008028 RID: 32808
		private Attributes attributes;

		// Token: 0x04008029 RID: 32809
		[MyCmpGet]
		private Health health;

		// Token: 0x0400802A RID: 32810
		[MyCmpGet]
		private OccupyArea occupyArea;

		// Token: 0x0400802B RID: 32811
		private AttributeModifier baseScalindingThreshold;

		// Token: 0x0400802C RID: 32812
		private AttributeModifier baseScoldingThreshold;

		// Token: 0x0400802D RID: 32813
		public AmountInstance internalTemperature;
	}
}
