using System;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200080E RID: 2062
[AddComponentMenu("KMonoBehaviour/Workable/Clinic")]
public class Clinic : Workable, IGameObjectEffectDescriptor, ISingleSliderControl, ISliderControl
{
	// Token: 0x0600382E RID: 14382 RVA: 0x00138280 File Offset: 0x00136480
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
		this.assignable.subSlots = new AssignableSlot[] { Db.Get().AssignableSlots.MedicalBed };
		this.assignable.AddAutoassignPrecondition(new Func<MinionAssignablesProxy, bool>(this.CanAutoAssignTo));
		this.assignable.AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.CanManuallyAssignTo));
	}

	// Token: 0x0600382F RID: 14383 RVA: 0x001382EB File Offset: 0x001364EB
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Prioritizable.AddRef(base.gameObject);
		Components.Clinics.Add(this);
		base.SetWorkTime(float.PositiveInfinity);
		this.clinicSMI = new Clinic.ClinicSM.Instance(this);
		this.clinicSMI.StartSM();
	}

	// Token: 0x06003830 RID: 14384 RVA: 0x0013832B File Offset: 0x0013652B
	protected override void OnCleanUp()
	{
		Prioritizable.RemoveRef(base.gameObject);
		Components.Clinics.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x06003831 RID: 14385 RVA: 0x0013834C File Offset: 0x0013654C
	private KAnimFile[] GetAppropriateOverrideAnims(WorkerBase worker)
	{
		KAnimFile[] array = null;
		if (!worker.GetSMI<WoundMonitor.Instance>().ShouldExitInfirmary())
		{
			array = this.workerInjuredAnims;
		}
		else if (this.workerDiseasedAnims != null && this.IsValidEffect(this.diseaseEffect) && worker.GetSMI<SicknessMonitor.Instance>().IsSick())
		{
			array = this.workerDiseasedAnims;
		}
		return array;
	}

	// Token: 0x06003832 RID: 14386 RVA: 0x0013839C File Offset: 0x0013659C
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		this.overrideAnims = this.GetAppropriateOverrideAnims(worker);
		return base.GetAnim(worker);
	}

	// Token: 0x06003833 RID: 14387 RVA: 0x001383B2 File Offset: 0x001365B2
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("Sleep", false);
		this.InstantExtremeRadiationRecovery();
	}

	// Token: 0x06003834 RID: 14388 RVA: 0x001383D4 File Offset: 0x001365D4
	private void InstantExtremeRadiationRecovery()
	{
		if (Game.IsDlcActiveForCurrentSave("EXPANSION1_ID"))
		{
			RadiationMonitor.Instance smi = base.worker.GetSMI<RadiationMonitor.Instance>();
			if (smi.sm.radiationExposure.Get(smi) >= 900f * smi.difficultySettingMod)
			{
				smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).SetValue(600f * smi.difficultySettingMod);
			}
		}
	}

	// Token: 0x06003835 RID: 14389 RVA: 0x00138450 File Offset: 0x00136650
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		KAnimFile[] appropriateOverrideAnims = this.GetAppropriateOverrideAnims(worker);
		if (appropriateOverrideAnims == null || appropriateOverrideAnims != this.overrideAnims)
		{
			return true;
		}
		base.OnWorkTick(worker, dt);
		return false;
	}

	// Token: 0x06003836 RID: 14390 RVA: 0x0013847D File Offset: 0x0013667D
	protected override void OnStopWork(WorkerBase worker)
	{
		worker.GetComponent<Effects>().Remove("Sleep");
		base.OnStopWork(worker);
	}

	// Token: 0x06003837 RID: 14391 RVA: 0x00138498 File Offset: 0x00136698
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.assignable.Unassign();
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < Clinic.EffectsRemoved.Length; i++)
		{
			string text = Clinic.EffectsRemoved[i];
			component.Remove(text);
		}
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x001384DF File Offset: 0x001366DF
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x001384E4 File Offset: 0x001366E4
	private Chore CreateWorkChore(ChoreType chore_type, bool allow_prioritization, bool allow_in_red_alert, PriorityScreen.PriorityClass priority_class, bool ignore_schedule_block = false)
	{
		return new WorkChore<Clinic>(chore_type, this, null, true, null, null, null, allow_in_red_alert, null, ignore_schedule_block, true, null, false, true, allow_prioritization, priority_class, 5, false, false);
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x00138510 File Offset: 0x00136710
	private Chore CreateBionicWorkChore(ChoreType chore_type, bool allow_prioritization, bool allow_in_red_alert, PriorityScreen.PriorityClass priority_class, bool ignore_schedule_block = false)
	{
		WorkChore<Clinic> workChore = new WorkChore<Clinic>(chore_type, this, null, true, null, null, null, allow_in_red_alert, null, ignore_schedule_block, true, null, false, true, allow_prioritization, priority_class, 5, false, false);
		workChore.AddPrecondition(ChorePreconditions.instance.IsBionic, null);
		return workChore;
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x0013854C File Offset: 0x0013674C
	private bool CanAutoAssignTo(MinionAssignablesProxy worker)
	{
		bool flag = false;
		MinionIdentity minionIdentity = worker.target as MinionIdentity;
		if (minionIdentity != null)
		{
			if (this.IsValidEffect(this.healthEffect))
			{
				Health component = minionIdentity.GetComponent<Health>();
				if (component != null && component.hitPoints < component.maxHitPoints)
				{
					flag = true;
				}
			}
			if (!flag && this.IsValidEffect(this.diseaseEffect))
			{
				flag = minionIdentity.GetComponent<MinionModifiers>().sicknesses.Count > 0;
			}
		}
		return flag;
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x001385C4 File Offset: 0x001367C4
	private bool CanManuallyAssignTo(MinionAssignablesProxy worker)
	{
		bool flag = false;
		MinionIdentity minionIdentity = worker.target as MinionIdentity;
		if (minionIdentity != null)
		{
			flag = this.IsHealthBelowThreshold(minionIdentity.gameObject);
		}
		return flag;
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x001385F8 File Offset: 0x001367F8
	private bool IsHealthBelowThreshold(GameObject minion)
	{
		Health health = ((minion != null) ? minion.GetComponent<Health>() : null);
		if (health != null)
		{
			float num = health.hitPoints / health.maxHitPoints;
			if (health != null)
			{
				return num < this.MedicalAttentionMinimum;
			}
		}
		return false;
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x00138643 File Offset: 0x00136843
	private bool IsValidEffect(string effect)
	{
		return effect != null && effect != "";
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x00138655 File Offset: 0x00136855
	private bool AllowDoctoring()
	{
		return this.IsValidEffect(this.doctoredDiseaseEffect) || this.IsValidEffect(this.doctoredHealthEffect);
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x00138674 File Offset: 0x00136874
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (this.IsValidEffect(this.healthEffect))
		{
			Effect.AddModifierDescriptions(base.gameObject, descriptors, this.healthEffect, false);
		}
		if (this.diseaseEffect != this.healthEffect && this.IsValidEffect(this.diseaseEffect))
		{
			Effect.AddModifierDescriptions(base.gameObject, descriptors, this.diseaseEffect, false);
		}
		if (this.AllowDoctoring())
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.DOCTORING, UI.BUILDINGEFFECTS.TOOLTIPS.DOCTORING, Descriptor.DescriptorType.Effect);
			descriptors.Add(descriptor);
			if (this.IsValidEffect(this.doctoredHealthEffect))
			{
				Effect.AddModifierDescriptions(base.gameObject, descriptors, this.doctoredHealthEffect, true);
			}
			if (this.doctoredDiseaseEffect != this.doctoredHealthEffect && this.IsValidEffect(this.doctoredDiseaseEffect))
			{
				Effect.AddModifierDescriptions(base.gameObject, descriptors, this.doctoredDiseaseEffect, true);
			}
		}
		return descriptors;
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06003841 RID: 14401 RVA: 0x0013876A File Offset: 0x0013696A
	public float MedicalAttentionMinimum
	{
		get
		{
			return this.sicknessSliderValue / 100f;
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x06003842 RID: 14402 RVA: 0x00138778 File Offset: 0x00136978
	string ISliderControl.SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TITLE";
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x06003843 RID: 14403 RVA: 0x0013877F File Offset: 0x0013697F
	string ISliderControl.SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.PERCENT;
		}
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x0013878B File Offset: 0x0013698B
	int ISliderControl.SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x0013878E File Offset: 0x0013698E
	float ISliderControl.GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06003846 RID: 14406 RVA: 0x00138795 File Offset: 0x00136995
	float ISliderControl.GetSliderMax(int index)
	{
		return 100f;
	}

	// Token: 0x06003847 RID: 14407 RVA: 0x0013879C File Offset: 0x0013699C
	float ISliderControl.GetSliderValue(int index)
	{
		return this.sicknessSliderValue;
	}

	// Token: 0x06003848 RID: 14408 RVA: 0x001387A4 File Offset: 0x001369A4
	void ISliderControl.SetSliderValue(float percent, int index)
	{
		if (percent != this.sicknessSliderValue)
		{
			this.sicknessSliderValue = (float)Mathf.RoundToInt(percent);
			Game.Instance.Trigger(875045922, null);
		}
	}

	// Token: 0x06003849 RID: 14409 RVA: 0x001387CC File Offset: 0x001369CC
	string ISliderControl.GetSliderTooltip(int index)
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TOOLTIP"), this.sicknessSliderValue);
	}

	// Token: 0x0600384A RID: 14410 RVA: 0x001387ED File Offset: 0x001369ED
	string ISliderControl.GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.MEDICALCOTSIDESCREEN.TOOLTIP";
	}

	// Token: 0x04002221 RID: 8737
	[MyCmpReq]
	private Assignable assignable;

	// Token: 0x04002222 RID: 8738
	private static readonly string[] EffectsRemoved = new string[] { "SoreBack" };

	// Token: 0x04002223 RID: 8739
	private const int MAX_RANGE = 10;

	// Token: 0x04002224 RID: 8740
	private const float CHECK_RANGE_INTERVAL = 10f;

	// Token: 0x04002225 RID: 8741
	public float doctorVisitInterval = 300f;

	// Token: 0x04002226 RID: 8742
	public KAnimFile[] workerInjuredAnims;

	// Token: 0x04002227 RID: 8743
	public KAnimFile[] workerDiseasedAnims;

	// Token: 0x04002228 RID: 8744
	public string diseaseEffect;

	// Token: 0x04002229 RID: 8745
	public string healthEffect;

	// Token: 0x0400222A RID: 8746
	public string doctoredDiseaseEffect;

	// Token: 0x0400222B RID: 8747
	public string doctoredHealthEffect;

	// Token: 0x0400222C RID: 8748
	public string doctoredPlaceholderEffect;

	// Token: 0x0400222D RID: 8749
	private Clinic.ClinicSM.Instance clinicSMI;

	// Token: 0x0400222E RID: 8750
	public static readonly Chore.Precondition IsOverSicknessThreshold = new Chore.Precondition
	{
		id = "IsOverSicknessThreshold",
		description = DUPLICANTS.CHORES.PRECONDITIONS.IS_NOT_BEING_ATTACKED,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((Clinic)data).IsHealthBelowThreshold(context.consumerState.gameObject);
		}
	};

	// Token: 0x0400222F RID: 8751
	[Serialize]
	private float sicknessSliderValue = 70f;

	// Token: 0x0200176C RID: 5996
	public class ClinicSM : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic>
	{
		// Token: 0x060098F5 RID: 39157 RVA: 0x00382EA4 File Offset: 0x003810A4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Never;
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (Clinic.ClinicSM.Instance smi) => smi.GetComponent<Operational>().IsOperational).Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.master.GetComponent<Assignable>().Unassign();
			});
			this.operational.DefaultState(this.operational.idle).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Clinic.ClinicSM.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.AssigneeChanged, this.unoperational, null)
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.Heal, false, true, PriorityScreen.PriorityClass.personalNeeds, false), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.healthEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.HealCritical, false, true, PriorityScreen.PriorityClass.personalNeeds, false), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.healthEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.RestDueToDisease, false, true, PriorityScreen.PriorityClass.personalNeeds, true), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.diseaseEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateWorkChore(Db.Get().ChoreTypes.SleepDueToDisease, false, true, PriorityScreen.PriorityClass.personalNeeds, true), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.diseaseEffect))
				.ToggleRecurringChore((Clinic.ClinicSM.Instance smi) => smi.master.CreateBionicWorkChore(Db.Get().ChoreTypes.BionicRestDueToDisease, false, true, PriorityScreen.PriorityClass.personalNeeds, true), (Clinic.ClinicSM.Instance smi) => !string.IsNullOrEmpty(smi.master.diseaseEffect));
			this.operational.idle.WorkableStartTransition((Clinic.ClinicSM.Instance smi) => smi.master, this.operational.healing);
			this.operational.healing.DefaultState(this.operational.healing.undoctored).WorkableStopTransition((Clinic.ClinicSM.Instance smi) => smi.GetComponent<Clinic>(), this.operational.idle).Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.master.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(Clinic.ClinicSM.Instance smi)
				{
					smi.master.GetComponent<Operational>().SetActive(false, false);
				});
			this.operational.healing.undoctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StartEffect(smi.master.healthEffect, false);
				smi.StartEffect(smi.master.diseaseEffect, false);
				bool flag = false;
				if (smi.master.worker != null)
				{
					flag = smi.HasEffect(smi.master.doctoredHealthEffect) || smi.HasEffect(smi.master.doctoredDiseaseEffect) || smi.HasEffect(smi.master.doctoredPlaceholderEffect);
				}
				if (smi.master.AllowDoctoring())
				{
					if (flag)
					{
						smi.GoTo(this.operational.healing.doctored);
						return;
					}
					smi.StartDoctorChore();
				}
			}).Exit(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StopEffect(smi.master.healthEffect);
				smi.StopEffect(smi.master.diseaseEffect);
				smi.StopDoctorChore();
			});
			this.operational.healing.newlyDoctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				smi.StartEffect(smi.master.doctoredDiseaseEffect, true);
				smi.StartEffect(smi.master.doctoredHealthEffect, true);
				smi.GoTo(this.operational.healing.doctored);
			});
			this.operational.healing.doctored.Enter(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component = smi.master.worker.GetComponent<Effects>();
				if (smi.HasEffect(smi.master.doctoredPlaceholderEffect))
				{
					EffectInstance effectInstance = component.Get(smi.master.doctoredPlaceholderEffect);
					EffectInstance effectInstance2 = smi.StartEffect(smi.master.doctoredDiseaseEffect, true);
					if (effectInstance2 != null)
					{
						float num = effectInstance.effect.duration - effectInstance.timeRemaining;
						effectInstance2.timeRemaining = effectInstance2.effect.duration - num;
					}
					EffectInstance effectInstance3 = smi.StartEffect(smi.master.doctoredHealthEffect, true);
					if (effectInstance3 != null)
					{
						float num2 = effectInstance.effect.duration - effectInstance.timeRemaining;
						effectInstance3.timeRemaining = effectInstance3.effect.duration - num2;
					}
					component.Remove(smi.master.doctoredPlaceholderEffect);
				}
			}).ScheduleGoTo(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component2 = smi.master.worker.GetComponent<Effects>();
				float num3 = smi.master.doctorVisitInterval;
				if (smi.HasEffect(smi.master.doctoredHealthEffect))
				{
					EffectInstance effectInstance4 = component2.Get(smi.master.doctoredHealthEffect);
					num3 = Mathf.Min(num3, effectInstance4.GetTimeRemaining());
				}
				if (smi.HasEffect(smi.master.doctoredDiseaseEffect))
				{
					EffectInstance effectInstance4 = component2.Get(smi.master.doctoredDiseaseEffect);
					num3 = Mathf.Min(num3, effectInstance4.GetTimeRemaining());
				}
				return num3;
			}, this.operational.healing.undoctored).Exit(delegate(Clinic.ClinicSM.Instance smi)
			{
				Effects component3 = smi.master.worker.GetComponent<Effects>();
				if (smi.HasEffect(smi.master.doctoredDiseaseEffect) || smi.HasEffect(smi.master.doctoredHealthEffect))
				{
					EffectInstance effectInstance5 = component3.Get(smi.master.doctoredDiseaseEffect);
					if (effectInstance5 == null)
					{
						effectInstance5 = component3.Get(smi.master.doctoredHealthEffect);
					}
					EffectInstance effectInstance6 = smi.StartEffect(smi.master.doctoredPlaceholderEffect, true);
					effectInstance6.timeRemaining = effectInstance6.effect.duration - (effectInstance5.effect.duration - effectInstance5.timeRemaining);
					component3.Remove(smi.master.doctoredDiseaseEffect);
					component3.Remove(smi.master.doctoredHealthEffect);
				}
			});
		}

		// Token: 0x040075A6 RID: 30118
		public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State unoperational;

		// Token: 0x040075A7 RID: 30119
		public Clinic.ClinicSM.OperationalStates operational;

		// Token: 0x02002804 RID: 10244
		public class OperationalStates : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State
		{
			// Token: 0x0400B187 RID: 45447
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State idle;

			// Token: 0x0400B188 RID: 45448
			public Clinic.ClinicSM.HealingStates healing;
		}

		// Token: 0x02002805 RID: 10245
		public class HealingStates : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State
		{
			// Token: 0x0400B189 RID: 45449
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State undoctored;

			// Token: 0x0400B18A RID: 45450
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State doctored;

			// Token: 0x0400B18B RID: 45451
			public GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.State newlyDoctored;
		}

		// Token: 0x02002806 RID: 10246
		public new class Instance : GameStateMachine<Clinic.ClinicSM, Clinic.ClinicSM.Instance, Clinic, object>.GameInstance
		{
			// Token: 0x0600CA5A RID: 51802 RVA: 0x004174CA File Offset: 0x004156CA
			public Instance(Clinic master)
				: base(master)
			{
			}

			// Token: 0x0600CA5B RID: 51803 RVA: 0x004174D4 File Offset: 0x004156D4
			public void StartDoctorChore()
			{
				if (base.master.IsValidEffect(base.master.doctoredHealthEffect) || base.master.IsValidEffect(base.master.doctoredDiseaseEffect))
				{
					this.doctorChore = new WorkChore<DoctorChoreWorkable>(Db.Get().ChoreTypes.Doctor, base.smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, true, true);
					WorkChore<DoctorChoreWorkable> workChore = this.doctorChore;
					workChore.onComplete = (Action<Chore>)Delegate.Combine(workChore.onComplete, new Action<Chore>(delegate(Chore chore)
					{
						base.smi.GoTo(base.smi.sm.operational.healing.newlyDoctored);
					}));
				}
			}

			// Token: 0x0600CA5C RID: 51804 RVA: 0x0041756E File Offset: 0x0041576E
			public void StopDoctorChore()
			{
				if (this.doctorChore != null)
				{
					this.doctorChore.Cancel("StopDoctorChore");
					this.doctorChore = null;
				}
			}

			// Token: 0x0600CA5D RID: 51805 RVA: 0x00417590 File Offset: 0x00415790
			public bool HasEffect(string effect)
			{
				bool flag = false;
				if (base.master.IsValidEffect(effect))
				{
					flag = base.smi.master.worker.GetComponent<Effects>().HasEffect(effect);
				}
				return flag;
			}

			// Token: 0x0600CA5E RID: 51806 RVA: 0x004175CC File Offset: 0x004157CC
			public EffectInstance StartEffect(string effect, bool should_save)
			{
				if (base.master.IsValidEffect(effect))
				{
					WorkerBase worker = base.smi.master.worker;
					if (worker != null)
					{
						Effects component = worker.GetComponent<Effects>();
						if (!component.HasEffect(effect))
						{
							return component.Add(effect, should_save);
						}
					}
				}
				return null;
			}

			// Token: 0x0600CA5F RID: 51807 RVA: 0x0041761C File Offset: 0x0041581C
			public void StopEffect(string effect)
			{
				if (base.master.IsValidEffect(effect))
				{
					WorkerBase worker = base.smi.master.worker;
					if (worker != null)
					{
						Effects component = worker.GetComponent<Effects>();
						if (component.HasEffect(effect))
						{
							component.Remove(effect);
						}
					}
				}
			}

			// Token: 0x0400B18C RID: 45452
			private WorkChore<DoctorChoreWorkable> doctorChore;
		}
	}
}
