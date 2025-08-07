using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020009F2 RID: 2546
public class GunkMonitor : GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>
{
	// Token: 0x06004A4E RID: 19022 RVA: 0x001AEA20 File Offset: 0x001ACC20
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.idle;
		this.root.Update(new Action<GunkMonitor.Instance, float>(GunkMonitor.GunkAmountWatcherUpdate), UpdateRate.SIM_200ms, false);
		this.idle.OnSignal(this.gunkValueChangedSignal, this.mildUrge, new Func<GunkMonitor.Instance, bool>(GunkMonitor.IsGunkLevelsOverMildUrgeThreshold));
		this.mildUrge.OnSignal(this.gunkValueChangedSignal, this.criticalUrge, new Func<GunkMonitor.Instance, bool>(GunkMonitor.IsGunkLevelsOverCriticalUrgeThreshold)).OnSignal(this.gunkValueChangedSignal, this.idle, new Func<GunkMonitor.Instance, bool>(GunkMonitor.DoesNotWantToExpellGunk)).DefaultState(this.mildUrge.prevented);
		this.mildUrge.prevented.ScheduleChange(this.mildUrge.allowed, new StateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.Transition.ConditionCallback(GunkMonitor.ScheduleAllowsExpelling));
		this.mildUrge.allowed.ScheduleChange(this.mildUrge.prevented, GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.Not(new StateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.Transition.ConditionCallback(GunkMonitor.ScheduleAllowsExpelling))).ToggleUrge(Db.Get().Urges.Pee).ToggleUrge(Db.Get().Urges.GunkPee);
		this.criticalUrge.OnSignal(this.gunkValueChangedSignal, this.idle, new Func<GunkMonitor.Instance, bool>(GunkMonitor.DoesNotWantToExpellGunk)).OnSignal(this.gunkValueChangedSignal, this.mildUrge, (GunkMonitor.Instance smi) => !GunkMonitor.IsGunkLevelsOverCriticalUrgeThreshold(smi)).OnSignal(this.gunkValueChangedSignal, this.cantHold, new Func<GunkMonitor.Instance, bool>(GunkMonitor.CanNotHoldGunkAnymore))
			.ToggleUrge(Db.Get().Urges.GunkPee)
			.ToggleUrge(Db.Get().Urges.Pee)
			.ToggleEffect("GunkSick")
			.ToggleExpression(Db.Get().Expressions.FullBladder, null)
			.ToggleThought(Db.Get().Thoughts.ExpellGunkDesire, null)
			.ToggleAnims("anim_loco_walk_slouch_kanim", 0f)
			.ToggleAnims("anim_idle_slouch_kanim", 0f);
		this.cantHold.ToggleUrge(Db.Get().Urges.GunkPee).ToggleThought(Db.Get().Thoughts.ExpellingGunk, null).ToggleChore((GunkMonitor.Instance smi) => new BionicGunkSpillChore(smi.master), this.emptyRemaining);
		this.emptyRemaining.Enter(new StateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State.Callback(GunkMonitor.ExpellAllGunk)).Enter(new StateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State.Callback(GunkMonitor.ApplyGunkHungoverEffect)).GoTo(this.idle);
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x001AECC1 File Offset: 0x001ACEC1
	public static bool IsGunkLevelsOverCriticalUrgeThreshold(GunkMonitor.Instance smi)
	{
		return smi.CurrentGunkPercentage >= smi.def.DesperetlySeekForGunkToiletTreshold;
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x001AECD9 File Offset: 0x001ACED9
	public static bool IsGunkLevelsOverMildUrgeThreshold(GunkMonitor.Instance smi)
	{
		return smi.CurrentGunkPercentage >= smi.def.SeekForGunkToiletTreshold_InSchedule;
	}

	// Token: 0x06004A51 RID: 19025 RVA: 0x001AECF1 File Offset: 0x001ACEF1
	public static bool ScheduleAllowsExpelling(GunkMonitor.Instance smi)
	{
		return smi.DoesCurrentScheduleAllowsGunkToilet;
	}

	// Token: 0x06004A52 RID: 19026 RVA: 0x001AECF9 File Offset: 0x001ACEF9
	public static bool DoesNotWantToExpellGunk(GunkMonitor.Instance smi)
	{
		return !GunkMonitor.IsGunkLevelsOverMildUrgeThreshold(smi);
	}

	// Token: 0x06004A53 RID: 19027 RVA: 0x001AED04 File Offset: 0x001ACF04
	public static bool CanNotHoldGunkAnymore(GunkMonitor.Instance smi)
	{
		return smi.IsGunkBuildupAtMax;
	}

	// Token: 0x06004A54 RID: 19028 RVA: 0x001AED0C File Offset: 0x001ACF0C
	public static void ExpellAllGunk(GunkMonitor.Instance smi)
	{
		smi.ExpellAllGunk(null);
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x001AED15 File Offset: 0x001ACF15
	public static void ApplyGunkHungoverEffect(GunkMonitor.Instance smi)
	{
		smi.GetComponent<Effects>().Add("GunkHungover", true);
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x001AED29 File Offset: 0x001ACF29
	public static void GunkAmountWatcherUpdate(GunkMonitor.Instance smi, float dt)
	{
		smi.GunkAmountWatcherUpdate(dt);
	}

	// Token: 0x0400310F RID: 12559
	public const float BIONIC_RADS_REMOVED_WHEN_PEE = 300f;

	// Token: 0x04003110 RID: 12560
	public static readonly float GUNK_CAPACITY = 80f;

	// Token: 0x04003111 RID: 12561
	public const string GUNK_FULL_EFFECT_NAME = "GunkSick";

	// Token: 0x04003112 RID: 12562
	public const string GUNK_HUNGOVER_EFFECT_NAME = "GunkHungover";

	// Token: 0x04003113 RID: 12563
	public static SimHashes GunkElement = SimHashes.LiquidGunk;

	// Token: 0x04003114 RID: 12564
	public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State idle;

	// Token: 0x04003115 RID: 12565
	public GunkMonitor.MildUrgeStates mildUrge;

	// Token: 0x04003116 RID: 12566
	public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State criticalUrge;

	// Token: 0x04003117 RID: 12567
	public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State cantHold;

	// Token: 0x04003118 RID: 12568
	public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State emptyRemaining;

	// Token: 0x04003119 RID: 12569
	public StateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.Signal gunkValueChangedSignal;

	// Token: 0x02001A58 RID: 6744
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007F7E RID: 32638
		public float SeekForGunkToiletTreshold_InSchedule = 0.6f;

		// Token: 0x04007F7F RID: 32639
		public float DesperetlySeekForGunkToiletTreshold = 0.9f;
	}

	// Token: 0x02001A59 RID: 6745
	public class MildUrgeStates : GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State
	{
		// Token: 0x04007F80 RID: 32640
		public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State allowed;

		// Token: 0x04007F81 RID: 32641
		public GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.State prevented;
	}

	// Token: 0x02001A5A RID: 6746
	public new class Instance : GameStateMachine<GunkMonitor, GunkMonitor.Instance, IStateMachineTarget, GunkMonitor.Def>.GameInstance
	{
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x0600A336 RID: 41782 RVA: 0x003A3716 File Offset: 0x003A1916
		public bool HasGunk
		{
			get
			{
				return this.CurrentGunkMass > 0f;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x0600A337 RID: 41783 RVA: 0x003A3725 File Offset: 0x003A1925
		public bool IsGunkBuildupAtMax
		{
			get
			{
				return this.CurrentGunkPercentage >= 1f;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x0600A338 RID: 41784 RVA: 0x003A3737 File Offset: 0x003A1937
		public float CurrentGunkMass
		{
			get
			{
				if (this.gunkAmount != null)
				{
					return this.gunkAmount.value;
				}
				return 0f;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600A339 RID: 41785 RVA: 0x003A3752 File Offset: 0x003A1952
		public float CurrentGunkPercentage
		{
			get
			{
				return this.CurrentGunkMass / this.gunkAmount.GetMax();
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600A33A RID: 41786 RVA: 0x003A3766 File Offset: 0x003A1966
		public bool DoesCurrentScheduleAllowsGunkToilet
		{
			get
			{
				return this.schedulable.IsAllowed(Db.Get().ScheduleBlockTypes.Eat) || this.schedulable.IsAllowed(Db.Get().ScheduleBlockTypes.Hygiene);
			}
		}

		// Token: 0x0600A33B RID: 41787 RVA: 0x003A37A0 File Offset: 0x003A19A0
		public Instance(IStateMachineTarget master, GunkMonitor.Def def)
			: base(master, def)
		{
			this.bodyTemperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
			this.gunkAmount = Db.Get().Amounts.BionicGunk.Lookup(base.gameObject);
			this.schedulable = base.GetComponent<Schedulable>();
		}

		// Token: 0x0600A33C RID: 41788 RVA: 0x003A3804 File Offset: 0x003A1A04
		public override void StartSM()
		{
			this.oilMonitor = base.gameObject.GetSMI<BionicOilMonitor.Instance>();
			BionicOilMonitor.Instance instance = this.oilMonitor;
			instance.OnOilValueChanged = (Action<float>)Delegate.Combine(instance.OnOilValueChanged, new Action<float>(this.OnOilValueChanged));
			this.LastAmountOfGunkObserved = this.CurrentGunkMass;
			base.StartSM();
		}

		// Token: 0x0600A33D RID: 41789 RVA: 0x003A385B File Offset: 0x003A1A5B
		public void GunkAmountWatcherUpdate(float dt)
		{
			if (this.LastAmountOfGunkObserved != this.CurrentGunkMass)
			{
				this.LastAmountOfGunkObserved = this.CurrentGunkMass;
				base.sm.gunkValueChangedSignal.Trigger(this);
			}
		}

		// Token: 0x0600A33E RID: 41790 RVA: 0x003A3888 File Offset: 0x003A1A88
		protected override void OnCleanUp()
		{
			if (this.oilMonitor != null)
			{
				BionicOilMonitor.Instance instance = this.oilMonitor;
				instance.OnOilValueChanged = (Action<float>)Delegate.Remove(instance.OnOilValueChanged, new Action<float>(this.OnOilValueChanged));
			}
			base.OnCleanUp();
		}

		// Token: 0x0600A33F RID: 41791 RVA: 0x003A38C0 File Offset: 0x003A1AC0
		private void OnOilValueChanged(float delta)
		{
			float num = ((delta < 0f) ? Mathf.Abs(delta) : 0f);
			float num2 = Mathf.Clamp(this.CurrentGunkMass + num, 0f, this.gunkAmount.GetMax());
			this.SetGunkMassValue(num2);
		}

		// Token: 0x0600A340 RID: 41792 RVA: 0x003A3908 File Offset: 0x003A1B08
		public void SetGunkMassValue(float value)
		{
			float currentGunkMass = this.CurrentGunkMass;
			this.gunkAmount.SetValue(value);
			this.LastAmountOfGunkObserved = this.CurrentGunkMass;
			base.sm.gunkValueChangedSignal.Trigger(this);
		}

		// Token: 0x0600A341 RID: 41793 RVA: 0x003A393C File Offset: 0x003A1B3C
		public void ExpellGunk(float mass, Storage targetStorage = null)
		{
			if (this.HasGunk)
			{
				float currentGunkMass = this.CurrentGunkMass;
				float num = Mathf.Min(mass, this.CurrentGunkMass);
				num = Mathf.Max(num, Mathf.Epsilon);
				int num2 = Grid.PosToCell(base.transform.position);
				byte index = Db.Get().Diseases.GetIndex(DUPLICANTSTATS.BIONICS.Secretions.PEE_DISEASE);
				float num3 = num / GunkMonitor.GUNK_CAPACITY;
				if (targetStorage != null)
				{
					targetStorage.AddLiquid(GunkMonitor.GunkElement, num, this.bodyTemperature.value, index, (int)((float)DUPLICANTSTATS.BIONICS.Secretions.DISEASE_PER_PEE * num3), false, true);
				}
				else
				{
					Equippable equippable = base.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
					if (equippable != null)
					{
						equippable.GetComponent<Storage>().AddLiquid(GunkMonitor.GunkElement, num, this.bodyTemperature.value, index, (int)((float)DUPLICANTSTATS.BIONICS.Secretions.DISEASE_PER_PEE * num3), false, true);
					}
					else
					{
						SimMessages.AddRemoveSubstance(num2, GunkMonitor.GunkElement, CellEventLogger.Instance.Vomit, num, this.bodyTemperature.value, index, (int)((float)DUPLICANTSTATS.BIONICS.Secretions.DISEASE_PER_PEE * num3), true, -1);
					}
				}
				if (Sim.IsRadiationEnabled())
				{
					MinionIdentity component = base.transform.GetComponent<MinionIdentity>();
					AmountInstance amountInstance = Db.Get().Amounts.RadiationBalance.Lookup(component);
					RadiationMonitor.Instance smi = component.GetSMI<RadiationMonitor.Instance>();
					float num4 = DUPLICANTSTATS.STANDARD.BaseStats.BLADDER_INCREASE_PER_SECOND / DUPLICANTSTATS.BIONICS.BaseStats.BLADDER_INCREASE_PER_SECOND;
					float num5 = Math.Min(amountInstance.value, 300f * num4 * smi.difficultySettingMod * num3);
					if (num5 >= 1f)
					{
						PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Negative, Math.Floor((double)num5).ToString() + UI.UNITSUFFIXES.RADIATION.RADS, component.transform, Vector3.up * 2f, 1.5f, false, false);
					}
					amountInstance.ApplyDelta(-num5);
				}
				this.SetGunkMassValue(Mathf.Clamp(this.CurrentGunkMass - num, 0f, this.gunkAmount.GetMax()));
			}
		}

		// Token: 0x0600A342 RID: 41794 RVA: 0x003A3B6E File Offset: 0x003A1D6E
		public void ExpellAllGunk(Storage targetStorage = null)
		{
			this.ExpellGunk(this.CurrentGunkMass, targetStorage);
		}

		// Token: 0x04007F82 RID: 32642
		private float LastAmountOfGunkObserved;

		// Token: 0x04007F83 RID: 32643
		private BionicOilMonitor.Instance oilMonitor;

		// Token: 0x04007F84 RID: 32644
		private AmountInstance gunkAmount;

		// Token: 0x04007F85 RID: 32645
		private AmountInstance bodyTemperature;

		// Token: 0x04007F86 RID: 32646
		private Schedulable schedulable;
	}
}
