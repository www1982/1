using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020007C1 RID: 1985
[AddComponentMenu("KMonoBehaviour/Workable/Shower")]
public class Shower : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x060034F6 RID: 13558 RVA: 0x00128E31 File Offset: 0x00127031
	private Shower()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060034F7 RID: 13559 RVA: 0x00128E41 File Offset: 0x00127041
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.resetProgressOnStop = true;
		this.smi = new Shower.ShowerSM.Instance(this);
		this.smi.StartSM();
	}

	// Token: 0x060034F8 RID: 13560 RVA: 0x00128E68 File Offset: 0x00127068
	protected override void OnStartWork(WorkerBase worker)
	{
		HygieneMonitor.Instance instance = worker.GetSMI<HygieneMonitor.Instance>();
		base.WorkTimeRemaining = this.workTime * instance.GetDirtiness();
		this.accumulatedDisease = SimUtil.DiseaseInfo.Invalid;
		this.smi.SetActive(true);
		base.OnStartWork(worker);
	}

	// Token: 0x060034F9 RID: 13561 RVA: 0x00128EAD File Offset: 0x001270AD
	protected override void OnStopWork(WorkerBase worker)
	{
		this.smi.SetActive(false);
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x00128EBC File Offset: 0x001270BC
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		Effects component = worker.GetComponent<Effects>();
		for (int i = 0; i < Shower.EffectsRemoved.Length; i++)
		{
			string text = Shower.EffectsRemoved[i];
			component.Remove(text);
		}
		if (!worker.HasTag(GameTags.HasSuitTank))
		{
			GasLiquidExposureMonitor.Instance instance = worker.GetSMI<GasLiquidExposureMonitor.Instance>();
			if (instance != null)
			{
				instance.ResetExposure();
			}
		}
		component.Add(Shower.SHOWER_EFFECT, true);
		HygieneMonitor.Instance instance2 = worker.GetSMI<HygieneMonitor.Instance>();
		if (instance2 != null)
		{
			instance2.SetDirtiness(0f);
		}
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x00128F3C File Offset: 0x0012713C
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		PrimaryElement component = worker.GetComponent<PrimaryElement>();
		if (component.DiseaseCount > 0)
		{
			SimUtil.DiseaseInfo diseaseInfo = new SimUtil.DiseaseInfo
			{
				idx = component.DiseaseIdx,
				count = Mathf.CeilToInt((float)component.DiseaseCount * (1f - Mathf.Pow(this.fractionalDiseaseRemoval, dt)) - (float)this.absoluteDiseaseRemoval)
			};
			component.ModifyDiseaseCount(-diseaseInfo.count, "Shower.RemoveDisease");
			this.accumulatedDisease = SimUtil.CalculateFinalDiseaseInfo(this.accumulatedDisease, diseaseInfo);
			PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(this.outputTargetElement);
			if (primaryElement != null)
			{
				primaryElement.GetComponent<PrimaryElement>().AddDisease(this.accumulatedDisease.idx, this.accumulatedDisease.count, "Shower.RemoveDisease");
				this.accumulatedDisease = SimUtil.DiseaseInfo.Invalid;
			}
		}
		return false;
	}

	// Token: 0x060034FC RID: 13564 RVA: 0x00129014 File Offset: 0x00127214
	protected override void OnAbortWork(WorkerBase worker)
	{
		base.OnAbortWork(worker);
		HygieneMonitor.Instance instance = worker.GetSMI<HygieneMonitor.Instance>();
		if (instance != null)
		{
			instance.SetDirtiness(1f - this.GetPercentComplete());
		}
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x00129044 File Offset: 0x00127244
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> descriptors = base.GetDescriptors(go);
		if (Shower.EffectsRemoved.Length != 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(UI.BUILDINGEFFECTS.REMOVESEFFECTSUBTITLE, UI.BUILDINGEFFECTS.TOOLTIPS.REMOVESEFFECTSUBTITLE, Descriptor.DescriptorType.Effect);
			descriptors.Add(descriptor);
			for (int i = 0; i < Shower.EffectsRemoved.Length; i++)
			{
				string text = Shower.EffectsRemoved[i];
				string text2 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".NAME");
				string text3 = Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + text.ToUpper() + ".CAUSE");
				Descriptor descriptor2 = default(Descriptor);
				descriptor2.IncreaseIndent();
				descriptor2.SetupDescriptor("• " + string.Format(UI.BUILDINGEFFECTS.REMOVEDEFFECT, text2), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.REMOVEDEFFECT, text3), Descriptor.DescriptorType.Effect);
				descriptors.Add(descriptor2);
			}
		}
		Effect.AddModifierDescriptions(base.gameObject, descriptors, Shower.SHOWER_EFFECT, true);
		return descriptors;
	}

	// Token: 0x04001FF6 RID: 8182
	private Shower.ShowerSM.Instance smi;

	// Token: 0x04001FF7 RID: 8183
	public static string SHOWER_EFFECT = "Showered";

	// Token: 0x04001FF8 RID: 8184
	public SimHashes outputTargetElement;

	// Token: 0x04001FF9 RID: 8185
	public float fractionalDiseaseRemoval;

	// Token: 0x04001FFA RID: 8186
	public int absoluteDiseaseRemoval;

	// Token: 0x04001FFB RID: 8187
	private SimUtil.DiseaseInfo accumulatedDisease;

	// Token: 0x04001FFC RID: 8188
	public const float WATER_PER_USE = 5f;

	// Token: 0x04001FFD RID: 8189
	private static readonly string[] EffectsRemoved = new string[] { "SoakingWet", "WetFeet", "MinorIrritation", "MajorIrritation" };

	// Token: 0x020016F1 RID: 5873
	public class ShowerSM : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower>
	{
		// Token: 0x06009739 RID: 38713 RVA: 0x0037B5A4 File Offset: 0x003797A4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.root.Update(new Action<Shower.ShowerSM.Instance, float>(this.UpdateStatusItems), UpdateRate.SIM_200ms, false);
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (Shower.ShowerSM.Instance smi) => smi.IsOperational).PlayAnim("off");
			this.operational.DefaultState(this.operational.not_ready).EventTransition(GameHashes.OperationalChanged, this.unoperational, (Shower.ShowerSM.Instance smi) => !smi.IsOperational);
			this.operational.not_ready.EventTransition(GameHashes.OnStorageChange, this.operational.ready, (Shower.ShowerSM.Instance smi) => smi.IsReady()).PlayAnim("off");
			this.operational.ready.ToggleChore(new Func<Shower.ShowerSM.Instance, Chore>(this.CreateShowerChore), this.operational.not_ready);
		}

		// Token: 0x0600973A RID: 38714 RVA: 0x0037B6CC File Offset: 0x003798CC
		private Chore CreateShowerChore(Shower.ShowerSM.Instance smi)
		{
			WorkChore<Shower> workChore = new WorkChore<Shower>(Db.Get().ChoreTypes.Shower, smi.master, null, true, null, null, null, false, Db.Get().ScheduleBlockTypes.Hygiene, false, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
			workChore.AddPrecondition(ChorePreconditions.instance.IsNotABionic, smi);
			return workChore;
		}

		// Token: 0x0600973B RID: 38715 RVA: 0x0037B724 File Offset: 0x00379924
		private void UpdateStatusItems(Shower.ShowerSM.Instance smi, float dt)
		{
			if (smi.OutputFull())
			{
				smi.master.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, this);
				return;
			}
			smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.OutputPipeFull, false);
		}

		// Token: 0x04007440 RID: 29760
		public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State unoperational;

		// Token: 0x04007441 RID: 29761
		public Shower.ShowerSM.OperationalState operational;

		// Token: 0x020027D3 RID: 10195
		public class OperationalState : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State
		{
			// Token: 0x0400B09B RID: 45211
			public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State not_ready;

			// Token: 0x0400B09C RID: 45212
			public GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.State ready;
		}

		// Token: 0x020027D4 RID: 10196
		public new class Instance : GameStateMachine<Shower.ShowerSM, Shower.ShowerSM.Instance, Shower, object>.GameInstance
		{
			// Token: 0x0600C969 RID: 51561 RVA: 0x00416180 File Offset: 0x00414380
			public Instance(Shower master)
				: base(master)
			{
				this.operational = master.GetComponent<Operational>();
				this.consumer = master.GetComponent<ConduitConsumer>();
				this.dispenser = master.GetComponent<ConduitDispenser>();
			}

			// Token: 0x17000CD9 RID: 3289
			// (get) Token: 0x0600C96A RID: 51562 RVA: 0x004161AD File Offset: 0x004143AD
			public bool IsOperational
			{
				get
				{
					return this.operational.IsOperational && this.consumer.IsConnected && this.dispenser.IsConnected;
				}
			}

			// Token: 0x0600C96B RID: 51563 RVA: 0x004161D6 File Offset: 0x004143D6
			public void SetActive(bool active)
			{
				this.operational.SetActive(active, false);
			}

			// Token: 0x0600C96C RID: 51564 RVA: 0x004161E8 File Offset: 0x004143E8
			private bool HasSufficientMass()
			{
				bool flag = false;
				PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(SimHashes.Water);
				if (primaryElement != null)
				{
					flag = primaryElement.Mass >= 5f;
				}
				return flag;
			}

			// Token: 0x0600C96D RID: 51565 RVA: 0x00416224 File Offset: 0x00414424
			public bool OutputFull()
			{
				PrimaryElement primaryElement = base.GetComponent<Storage>().FindPrimaryElement(SimHashes.DirtyWater);
				return primaryElement != null && primaryElement.Mass >= 5f;
			}

			// Token: 0x0600C96E RID: 51566 RVA: 0x0041625D File Offset: 0x0041445D
			public bool IsReady()
			{
				return this.HasSufficientMass() && !this.OutputFull();
			}

			// Token: 0x0400B09D RID: 45213
			private Operational operational;

			// Token: 0x0400B09E RID: 45214
			private ConduitConsumer consumer;

			// Token: 0x0400B09F RID: 45215
			private ConduitDispenser dispenser;
		}
	}
}
