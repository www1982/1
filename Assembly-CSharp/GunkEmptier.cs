using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000744 RID: 1860
public class GunkEmptier : GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>
{
	// Token: 0x06002F23 RID: 12067 RVA: 0x0010E6F0 File Offset: 0x0010C8F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.noOperational;
		this.noOperational.EventTransition(GameHashes.OperationalChanged, this.operational, new StateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Transition.ConditionCallback(GunkEmptier.IsOperational));
		this.operational.EventTransition(GameHashes.OperationalChanged, this.noOperational, GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Not(new StateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Transition.ConditionCallback(GunkEmptier.IsOperational))).DefaultState(this.operational.noStorageSpace);
		this.operational.noStorageSpace.ToggleStatusItem(Db.Get().BuildingStatusItems.GunkEmptierFull, null).EventTransition(GameHashes.OnStorageChange, this.operational.ready, new StateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Transition.ConditionCallback(GunkEmptier.HasSpaceToEmptyABionicGunkTank));
		this.operational.ready.EventTransition(GameHashes.OnStorageChange, this.operational.noStorageSpace, GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Not(new StateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.Transition.ConditionCallback(GunkEmptier.HasSpaceToEmptyABionicGunkTank))).ToggleRecurringChore(new Func<GunkEmptier.Instance, Chore>(GunkEmptier.CreateChore), null);
	}

	// Token: 0x06002F24 RID: 12068 RVA: 0x0010E7ED File Offset: 0x0010C9ED
	public static bool HasSpaceToEmptyABionicGunkTank(GunkEmptier.Instance smi)
	{
		return smi.RemainingStorageCapacity >= GunkMonitor.GUNK_CAPACITY;
	}

	// Token: 0x06002F25 RID: 12069 RVA: 0x0010E7FF File Offset: 0x0010C9FF
	public static bool IsOperational(GunkEmptier.Instance smi)
	{
		return smi.IsOperational;
	}

	// Token: 0x06002F26 RID: 12070 RVA: 0x0010E808 File Offset: 0x0010CA08
	private static WorkChore<GunkEmptierWorkable> CreateChore(GunkEmptier.Instance smi)
	{
		WorkChore<GunkEmptierWorkable> workChore = new WorkChore<GunkEmptierWorkable>(Db.Get().ChoreTypes.ExpellGunk, smi.master, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
		workChore.AddPrecondition(ChorePreconditions.instance.IsPreferredAssignableOrUrgentBladder, smi.master.GetComponent<Assignable>());
		return workChore;
	}

	// Token: 0x04001BE2 RID: 7138
	private static string DISEASE_ID = DUPLICANTSTATS.BIONICS.Secretions.PEE_DISEASE;

	// Token: 0x04001BE3 RID: 7139
	private static int DISEASE_ON_DUPE_COUNT_PER_USE = DUPLICANTSTATS.BIONICS.Secretions.DISEASE_PER_PEE / 20;

	// Token: 0x04001BE4 RID: 7140
	public GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.State noOperational;

	// Token: 0x04001BE5 RID: 7141
	public GunkEmptier.OperationalStates operational;

	// Token: 0x0200160C RID: 5644
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200160D RID: 5645
	public class OperationalStates : GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.State
	{
		// Token: 0x040071CB RID: 29131
		public GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.State noStorageSpace;

		// Token: 0x040071CC RID: 29132
		public GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.State ready;
	}

	// Token: 0x0200160E RID: 5646
	public new class Instance : GameStateMachine<GunkEmptier, GunkEmptier.Instance, IStateMachineTarget, GunkEmptier.Def>.GameInstance
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060093BF RID: 37823 RVA: 0x0036CDCE File Offset: 0x0036AFCE
		public float RemainingStorageCapacity
		{
			get
			{
				return this.storage.RemainingCapacity();
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060093C0 RID: 37824 RVA: 0x0036CDDB File Offset: 0x0036AFDB
		public bool IsOperational
		{
			get
			{
				return this.operational.IsOperational;
			}
		}

		// Token: 0x060093C1 RID: 37825 RVA: 0x0036CDE8 File Offset: 0x0036AFE8
		public Instance(IStateMachineTarget master, GunkEmptier.Def def)
			: base(master, def)
		{
			GunkEmptierWorkable component = base.GetComponent<GunkEmptierWorkable>();
			GunkEmptierWorkable gunkEmptierWorkable = component;
			gunkEmptierWorkable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(gunkEmptierWorkable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnGunkEmptierUsed));
			Components.GunkExtractors.Add(component);
			this.storage = base.GetComponent<Storage>();
			this.operational = base.GetComponent<Operational>();
			base.gameObject.AddOrGet<Ownable>().AddAssignPrecondition(new Func<MinionAssignablesProxy, bool>(this.AssignablePrecondition_OnlyOnBionics));
		}

		// Token: 0x060093C2 RID: 37826 RVA: 0x0036CE68 File Offset: 0x0036B068
		protected override void OnCleanUp()
		{
			GunkEmptierWorkable component = base.GetComponent<GunkEmptierWorkable>();
			GunkEmptierWorkable gunkEmptierWorkable = component;
			gunkEmptierWorkable.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Remove(gunkEmptierWorkable.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnGunkEmptierUsed));
			Components.GunkExtractors.Remove(component);
			base.OnCleanUp();
		}

		// Token: 0x060093C3 RID: 37827 RVA: 0x0036CEAF File Offset: 0x0036B0AF
		private bool AssignablePrecondition_OnlyOnBionics(MinionAssignablesProxy worker)
		{
			return worker.GetMinionModel() == BionicMinionConfig.MODEL;
		}

		// Token: 0x060093C4 RID: 37828 RVA: 0x0036CEC1 File Offset: 0x0036B0C1
		public void OnGunkEmptierUsed(Workable workable, Workable.WorkableEvent ev)
		{
			if (ev == Workable.WorkableEvent.WorkCompleted)
			{
				this.AddDisseaseToWorker(workable.worker);
			}
		}

		// Token: 0x060093C5 RID: 37829 RVA: 0x0036CED4 File Offset: 0x0036B0D4
		public void AddDisseaseToWorker(WorkerBase worker)
		{
			if (worker != null)
			{
				byte index = Db.Get().Diseases.GetIndex(GunkEmptier.DISEASE_ID);
				worker.GetComponent<PrimaryElement>().AddDisease(index, GunkEmptier.DISEASE_ON_DUPE_COUNT_PER_USE, "GunkEmptier.Flush");
				PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(DUPLICANTS.DISEASES.ADDED_POPFX, Db.Get().Diseases[(int)index].Name, GunkEmptier.DISEASE_ON_DUPE_COUNT_PER_USE), base.transform, Vector3.up, 1.5f, false, false);
				Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_LotsOfGerms, true);
				return;
			}
			DebugUtil.LogWarningArgs(new object[] { "Tried to add disease on gunk emptier use but worker was null" });
		}

		// Token: 0x040071CD RID: 29133
		private Operational operational;

		// Token: 0x040071CE RID: 29134
		private Storage storage;
	}
}
