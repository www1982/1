using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;

// Token: 0x020009D3 RID: 2515
public class BionicOilMonitor : GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>
{
	// Token: 0x0600499F RID: 18847 RVA: 0x001AA640 File Offset: 0x001A8840
	private static Effect CreateFreshOilEffectVariation(string id, float stressBonus, float moralBonus)
	{
		Effect effect = new Effect("FreshOil_" + id, DUPLICANTS.MODIFIERS.FRESHOIL.NAME, DUPLICANTS.MODIFIERS.FRESHOIL.TOOLTIP, 4800f, true, true, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().Attributes.QualityOfLife.Id, moralBonus, DUPLICANTS.MODIFIERS.FRESHOIL.NAME, false, false, true));
		effect.Add(new AttributeModifier(Db.Get().Amounts.Stress.deltaAttribute.Id, stressBonus, DUPLICANTS.MODIFIERS.FRESHOIL.NAME, false, false, true));
		return effect;
	}

	// Token: 0x060049A0 RID: 18848 RVA: 0x001AA6EC File Offset: 0x001A88EC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.offline;
		this.root.Update(new Action<BionicOilMonitor.Instance, float>(BionicOilMonitor.OilAmountInstanceWatcherUpdate), UpdateRate.SIM_200ms, false).Exit(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State.Callback(BionicOilMonitor.RemoveBaseOilDeltaModifier));
		this.offline.EventTransition(GameHashes.BionicOnline, this.online, new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Transition.ConditionCallback(BionicOilMonitor.IsBionicOnline)).Enter(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State.Callback(BionicOilMonitor.RemoveBaseOilDeltaModifier));
		this.online.EventTransition(GameHashes.BionicOffline, this.offline, GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Not(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Transition.ConditionCallback(BionicOilMonitor.IsBionicOnline))).Enter(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State.Callback(BionicOilMonitor.AddBaseOilDeltaModifier)).DefaultState(this.online.idle)
			.Enter(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State.Callback(BionicOilMonitor.EnableSolidLubricationSensor))
			.Exit(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State.Callback(BionicOilMonitor.DisableSolidLubricationSensor));
		this.online.idle.EnterTransition(this.online.seeking, new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Transition.ConditionCallback(BionicOilMonitor.WantsOilChange)).OnSignal(this.OilValueChanged, this.online.seeking, new Func<BionicOilMonitor.Instance, bool>(BionicOilMonitor.WantsOilChange));
		this.online.seeking.OnSignal(this.OilFilledSignal, this.online.idle).OnSignal(this.OilValueChanged, this.online.idle, new Func<BionicOilMonitor.Instance, bool>(BionicOilMonitor.HasDecentAmountOfOil)).DefaultState(this.online.seeking.hasOil)
			.ToggleThought(Db.Get().Thoughts.RefillOilDesire, null)
			.ToggleUrge(Db.Get().Urges.OilRefill)
			.ToggleChore((BionicOilMonitor.Instance smi) => new UseSolidLubricantChore(smi.master), this.online.idle);
		this.online.seeking.hasOil.EnterTransition(this.online.seeking.noOil, GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Not(new StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Transition.ConditionCallback(BionicOilMonitor.HasAnyAmountOfOil))).OnSignal(this.OilRanOutSignal, this.online.seeking.noOil).ToggleStatusItem(Db.Get().DuplicantStatusItems.BionicWantsOilChange, null);
		this.online.seeking.noOil.Enter(delegate(BionicOilMonitor.Instance smi)
		{
			smi.currentNoLubricationEffectApplied = smi.effects.Add(smi.GetEffect(), false).effect.IdHash;
		}).Exit(delegate(BionicOilMonitor.Instance smi)
		{
			smi.effects.Remove(smi.currentNoLubricationEffectApplied);
		}).ToggleReactable(new Func<BionicOilMonitor.Instance, Reactable>(BionicOilMonitor.GrindingGearsReactable))
			.EventTransition(GameHashes.AssignedRoleChanged, this.online.seeking.hasOil, null);
	}

	// Token: 0x060049A1 RID: 18849 RVA: 0x001AA9B9 File Offset: 0x001A8BB9
	public static bool IsBionicOnline(BionicOilMonitor.Instance smi)
	{
		return smi.IsOnline;
	}

	// Token: 0x060049A2 RID: 18850 RVA: 0x001AA9C1 File Offset: 0x001A8BC1
	public static bool HasAnyAmountOfOil(BionicOilMonitor.Instance smi)
	{
		return smi.CurrentOilMass > 0f;
	}

	// Token: 0x060049A3 RID: 18851 RVA: 0x001AA9D0 File Offset: 0x001A8BD0
	public static bool HasDecentAmountOfOil(BionicOilMonitor.Instance smi)
	{
		return smi.CurrentOilPercentage > 0.2f;
	}

	// Token: 0x060049A4 RID: 18852 RVA: 0x001AA9DF File Offset: 0x001A8BDF
	public static bool WantsOilChange(BionicOilMonitor.Instance smi)
	{
		return smi.CurrentOilPercentage <= 0.2f;
	}

	// Token: 0x060049A5 RID: 18853 RVA: 0x001AA9F1 File Offset: 0x001A8BF1
	public static void AddBaseOilDeltaModifier(BionicOilMonitor.Instance smi)
	{
		smi.SetBaseDeltaModifierActiveState(true);
	}

	// Token: 0x060049A6 RID: 18854 RVA: 0x001AA9FA File Offset: 0x001A8BFA
	public static void RemoveBaseOilDeltaModifier(BionicOilMonitor.Instance smi)
	{
		smi.SetBaseDeltaModifierActiveState(false);
	}

	// Token: 0x060049A7 RID: 18855 RVA: 0x001AAA04 File Offset: 0x001A8C04
	public static void OilAmountInstanceWatcherUpdate(BionicOilMonitor.Instance smi, float dt)
	{
		float lastOilAmountMassRecorded = smi.LastOilAmountMassRecorded;
		float num = smi.CurrentOilMass - lastOilAmountMassRecorded;
		if (num != 0f)
		{
			smi.LastOilAmountMassRecorded = smi.CurrentOilMass;
			if (!smi.HasOil)
			{
				smi.ReportOilRanOut();
			}
			smi.ReportOilValueChanged(num);
		}
	}

	// Token: 0x060049A8 RID: 18856 RVA: 0x001AAA4A File Offset: 0x001A8C4A
	public static void EnableSolidLubricationSensor(BionicOilMonitor.Instance smi)
	{
		smi.SetSolidLubricationSensorActiveState(true);
	}

	// Token: 0x060049A9 RID: 18857 RVA: 0x001AAA53 File Offset: 0x001A8C53
	public static void DisableSolidLubricationSensor(BionicOilMonitor.Instance smi)
	{
		smi.SetSolidLubricationSensorActiveState(false);
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x001AAA5C File Offset: 0x001A8C5C
	private static Reactable GrindingGearsReactable(BionicOilMonitor.Instance smi)
	{
		return smi.GetGrindingGearReactable();
	}

	// Token: 0x060049AB RID: 18859 RVA: 0x001AAA64 File Offset: 0x001A8C64
	public static void ApplyLubricationEffects(Effects targetBionicEffects, SimHashes lubricant)
	{
		foreach (SimHashes simHashes in BionicOilMonitor.LUBRICANT_TYPE_EFFECT.Keys)
		{
			if (BionicOilMonitor.LUBRICANT_TYPE_EFFECT.ContainsKey(simHashes))
			{
				Effect effect = BionicOilMonitor.LUBRICANT_TYPE_EFFECT[simHashes];
				if (lubricant == simHashes)
				{
					targetBionicEffects.Add(effect, true);
				}
				else
				{
					targetBionicEffects.Remove(effect);
				}
			}
		}
	}

	// Token: 0x060049AD RID: 18861 RVA: 0x001AAAEC File Offset: 0x001A8CEC
	// Note: this type is marked as 'beforefieldinit'.
	static BionicOilMonitor()
	{
		Dictionary<SimHashes, Effect> dictionary = new Dictionary<SimHashes, Effect>();
		dictionary[SimHashes.Tallow] = BionicOilMonitor.CreateFreshOilEffectVariation(SimHashes.Tallow.ToString(), -0.016666668f, 3f);
		dictionary[SimHashes.CrudeOil] = BionicOilMonitor.CreateFreshOilEffectVariation(SimHashes.CrudeOil.ToString(), -0.016666668f, 3f);
		dictionary[SimHashes.PhytoOil] = BionicOilMonitor.CreateFreshOilEffectVariation(SimHashes.PhytoOil.ToString(), -0.008333334f, 2f);
		BionicOilMonitor.LUBRICANT_TYPE_EFFECT = dictionary;
	}

	// Token: 0x04003085 RID: 12421
	public static Dictionary<SimHashes, Effect> LUBRICANT_TYPE_EFFECT;

	// Token: 0x04003086 RID: 12422
	public const float OIL_CAPACITY = 200f;

	// Token: 0x04003087 RID: 12423
	public const float OIL_TANK_DURATION = 6000f;

	// Token: 0x04003088 RID: 12424
	public const float OIL_REFILL_TRESHOLD = 0.2f;

	// Token: 0x04003089 RID: 12425
	public const string NO_OIL_EFFECT_NAME_MINOR = "NoLubricationMinor";

	// Token: 0x0400308A RID: 12426
	public const string NO_OIL_EFFECT_NAME_MAJOR = "NoLubricationMajor";

	// Token: 0x0400308B RID: 12427
	public GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State offline;

	// Token: 0x0400308C RID: 12428
	public BionicOilMonitor.OnlineStates online;

	// Token: 0x0400308D RID: 12429
	public StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Signal OilFilledSignal;

	// Token: 0x0400308E RID: 12430
	public StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Signal OilRanOutSignal;

	// Token: 0x0400308F RID: 12431
	public StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Signal OilValueChanged;

	// Token: 0x04003090 RID: 12432
	public StateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.Signal OnClosestSolidLubricantChangedSignal;

	// Token: 0x020019F8 RID: 6648
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020019F9 RID: 6649
	public class WantsOilChangeState : GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State
	{
		// Token: 0x04007E3B RID: 32315
		public GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State hasOil;

		// Token: 0x04007E3C RID: 32316
		public GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State noOil;
	}

	// Token: 0x020019FA RID: 6650
	public class OnlineStates : GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State
	{
		// Token: 0x04007E3D RID: 32317
		public GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.State idle;

		// Token: 0x04007E3E RID: 32318
		public BionicOilMonitor.WantsOilChangeState seeking;
	}

	// Token: 0x020019FB RID: 6651
	public new class Instance : GameStateMachine<BionicOilMonitor, BionicOilMonitor.Instance, IStateMachineTarget, BionicOilMonitor.Def>.GameInstance
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600A175 RID: 41333 RVA: 0x0039EAF4 File Offset: 0x0039CCF4
		public bool IsOnline
		{
			get
			{
				return this.batterySMI != null && this.batterySMI.IsOnline;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600A176 RID: 41334 RVA: 0x0039EB0B File Offset: 0x0039CD0B
		public bool HasOil
		{
			get
			{
				return this.CurrentOilMass > 0f;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600A177 RID: 41335 RVA: 0x0039EB1A File Offset: 0x0039CD1A
		public float CurrentOilPercentage
		{
			get
			{
				return this.CurrentOilMass / this.oilAmount.GetMax();
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600A178 RID: 41336 RVA: 0x0039EB2E File Offset: 0x0039CD2E
		public float CurrentOilMass
		{
			get
			{
				if (this.oilAmount != null)
				{
					return this.oilAmount.value;
				}
				return 0f;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600A17A RID: 41338 RVA: 0x0039EB52 File Offset: 0x0039CD52
		// (set) Token: 0x0600A179 RID: 41337 RVA: 0x0039EB49 File Offset: 0x0039CD49
		public AmountInstance oilAmount { get; private set; }

		// Token: 0x0600A17B RID: 41339 RVA: 0x0039EB5C File Offset: 0x0039CD5C
		public Instance(IStateMachineTarget master, BionicOilMonitor.Def def)
			: base(master, def)
		{
			this.oilAmount = Db.Get().Amounts.BionicOil.Lookup(base.gameObject);
			this.batterySMI = base.gameObject.GetSMI<BionicBatteryMonitor.Instance>();
		}

		// Token: 0x0600A17C RID: 41340 RVA: 0x0039EBE0 File Offset: 0x0039CDE0
		public override void StartSM()
		{
			this.closestSolidLubricantSensor = base.GetComponent<Sensors>().GetSensor<ClosestLubricantSensor>();
			ClosestLubricantSensor closestLubricantSensor = this.closestSolidLubricantSensor;
			closestLubricantSensor.OnItemChanged = (Action<Pickupable>)Delegate.Combine(closestLubricantSensor.OnItemChanged, new Action<Pickupable>(this.OnClosestSolidLubricantChanged));
			this.LastOilAmountMassRecorded = this.CurrentOilMass;
			base.StartSM();
		}

		// Token: 0x0600A17D RID: 41341 RVA: 0x0039EC37 File Offset: 0x0039CE37
		public string GetEffect()
		{
			if (!this.resume.HasPerk(Db.Get().SkillPerks.EfficientBionicGears))
			{
				return "NoLubricationMajor";
			}
			return "NoLubricationMinor";
		}

		// Token: 0x0600A17E RID: 41342 RVA: 0x0039EC60 File Offset: 0x0039CE60
		private void ReportOilTankFilled()
		{
			base.sm.OilFilledSignal.Trigger(this);
		}

		// Token: 0x0600A17F RID: 41343 RVA: 0x0039EC73 File Offset: 0x0039CE73
		public void ReportOilRanOut()
		{
			base.sm.OilRanOutSignal.Trigger(this);
		}

		// Token: 0x0600A180 RID: 41344 RVA: 0x0039EC86 File Offset: 0x0039CE86
		public void ReportOilValueChanged(float delta)
		{
			base.sm.OilValueChanged.Trigger(this);
			Action<float> onOilValueChanged = this.OnOilValueChanged;
			if (onOilValueChanged == null)
			{
				return;
			}
			onOilValueChanged(delta);
		}

		// Token: 0x0600A181 RID: 41345 RVA: 0x0039ECAA File Offset: 0x0039CEAA
		public void SetOilMassValue(float value)
		{
			this.oilAmount.SetValue(value);
		}

		// Token: 0x0600A182 RID: 41346 RVA: 0x0039ECBC File Offset: 0x0039CEBC
		public void SetBaseDeltaModifierActiveState(bool isActive)
		{
			MinionModifiers component = base.GetComponent<MinionModifiers>();
			if (isActive)
			{
				bool flag = false;
				int count = component.attributes.Get(this.BaseOilDeltaModifier.AttributeId).Modifiers.Count;
				for (int i = 0; i < count; i++)
				{
					if (component.attributes.Get(this.BaseOilDeltaModifier.AttributeId).Modifiers[i] == this.BaseOilDeltaModifier)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					component.attributes.Add(this.BaseOilDeltaModifier);
					return;
				}
			}
			else
			{
				component.attributes.Remove(this.BaseOilDeltaModifier);
			}
		}

		// Token: 0x0600A183 RID: 41347 RVA: 0x0039ED55 File Offset: 0x0039CF55
		public void RefillOil(float amount)
		{
			this.oilAmount.SetValue(this.CurrentOilMass + amount);
			this.ReportOilTankFilled();
		}

		// Token: 0x0600A184 RID: 41348 RVA: 0x0039ED71 File Offset: 0x0039CF71
		private void OnClosestSolidLubricantChanged(Pickupable newItem)
		{
			base.sm.OnClosestSolidLubricantChangedSignal.Trigger(this);
		}

		// Token: 0x0600A185 RID: 41349 RVA: 0x0039ED84 File Offset: 0x0039CF84
		public Pickupable GetClosestSolidLubricant()
		{
			return this.closestSolidLubricantSensor.GetItem();
		}

		// Token: 0x0600A186 RID: 41350 RVA: 0x0039ED91 File Offset: 0x0039CF91
		public void SetSolidLubricationSensorActiveState(bool shouldItBeActive)
		{
			this.closestSolidLubricantSensor.SetActive(shouldItBeActive);
			if (shouldItBeActive)
			{
				this.closestSolidLubricantSensor.Update();
			}
		}

		// Token: 0x0600A187 RID: 41351 RVA: 0x0039EDB0 File Offset: 0x0039CFB0
		public Reactable GetGrindingGearReactable()
		{
			SelfEmoteReactable selfEmoteReactable = new SelfEmoteReactable(base.master.gameObject, Db.Get().Emotes.Minion.GrindingGears.Id, Db.Get().ChoreTypes.EmoteHighPriority, 0f, 10f, float.PositiveInfinity, 0f);
			Emote grindingGears = Db.Get().Emotes.Minion.GrindingGears;
			selfEmoteReactable.SetEmote(grindingGears);
			selfEmoteReactable.SetThought(Db.Get().Thoughts.RefillOilDesire);
			selfEmoteReactable.preventChoreInterruption = true;
			return selfEmoteReactable;
		}

		// Token: 0x04007E3F RID: 32319
		public float LastOilAmountMassRecorded = -1f;

		// Token: 0x04007E40 RID: 32320
		public Action<float> OnOilValueChanged;

		// Token: 0x04007E41 RID: 32321
		private BionicBatteryMonitor.Instance batterySMI;

		// Token: 0x04007E42 RID: 32322
		[MyCmpGet]
		private MinionResume resume;

		// Token: 0x04007E43 RID: 32323
		[MyCmpGet]
		public Effects effects;

		// Token: 0x04007E44 RID: 32324
		public HashedString currentNoLubricationEffectApplied;

		// Token: 0x04007E45 RID: 32325
		private AttributeModifier BaseOilDeltaModifier = new AttributeModifier(Db.Get().Amounts.BionicOil.deltaAttribute.Id, -0.033333335f, BionicMinionConfig.NAME, false, false, true);

		// Token: 0x04007E47 RID: 32327
		private ClosestLubricantSensor closestSolidLubricantSensor;
	}
}
