using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020004A4 RID: 1188
public class UseSolidLubricantChore : Chore<UseSolidLubricantChore.Instance>
{
	// Token: 0x060018BB RID: 6331 RVA: 0x0008A330 File Offset: 0x00088530
	public UseSolidLubricantChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.SolidOilChange, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new UseSolidLubricantChore.Instance(this, target.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(UseSolidLubricantChore.SolidLubricantIsNotNull, null);
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x0008A394 File Offset: 0x00088594
	public override void Begin(Chore.Precondition.Context context)
	{
		if (context.consumerState.consumer == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null context.consumer");
			return;
		}
		BionicOilMonitor.Instance smi = context.consumerState.consumer.GetSMI<BionicOilMonitor.Instance>();
		if (smi == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null RationMonitor.Instance");
			return;
		}
		Pickupable closestSolidLubricant = smi.GetClosestSolidLubricant();
		if (closestSolidLubricant == null)
		{
			global::Debug.LogError("ReloadElectrobankChore null electrobank.gameObject");
			return;
		}
		base.smi.sm.solidLubricantSource.Set(closestSolidLubricant.gameObject, base.smi, false);
		base.smi.sm.dupe.Set(context.consumerState.consumer, base.smi);
		base.Begin(context);
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x0008A44C File Offset: 0x0008864C
	public static void ConsumeLubricant(UseSolidLubricantChore.Instance smi)
	{
		PrimaryElement component = smi.sm.pickedUpSolidLubricant.Get(smi).GetComponent<PrimaryElement>();
		float num = Mathf.Min(component.Mass, 200f - smi.oilMonitor.oilAmount.value);
		smi.oilMonitor.RefillOil(num);
		if (num >= component.Mass)
		{
			Util.KDestroyGameObject(component.gameObject);
			smi.sm.pickedUpSolidLubricant.Set(null, smi);
		}
		else
		{
			component.Mass -= num;
		}
		BionicOilMonitor.ApplyLubricationEffects(smi.master.GetComponent<Effects>(), component.GetComponent<PrimaryElement>().ElementID);
	}

	// Token: 0x060018BE RID: 6334 RVA: 0x0008A4F0 File Offset: 0x000886F0
	public static void SetOverrideAnimSymbol(UseSolidLubricantChore.Instance smi, bool overriding)
	{
		string text = "lubricant";
		KBatchedAnimController component = smi.GetComponent<KBatchedAnimController>();
		SymbolOverrideController component2 = smi.gameObject.GetComponent<SymbolOverrideController>();
		GameObject gameObject = smi.sm.pickedUpSolidLubricant.Get(smi);
		if (gameObject != null)
		{
			KBatchedAnimTracker component3 = gameObject.GetComponent<KBatchedAnimTracker>();
			if (component3 != null)
			{
				component3.enabled = !overriding;
			}
			Storage.MakeItemInvisible(gameObject, overriding, false);
		}
		if (!overriding)
		{
			component2.RemoveSymbolOverride(text, 0);
			component.SetSymbolVisiblity(text, false);
			return;
		}
		KAnim.Build.Symbol symbolByIndex = gameObject.GetComponent<KBatchedAnimController>().AnimFiles[0].GetData().build.GetSymbolByIndex(0U);
		component2.AddSymbolOverride(text, symbolByIndex, 0);
		component.SetSymbolVisiblity(text, true);
	}

	// Token: 0x04000E4E RID: 3662
	public const float LOOP_LENGTH = 6.666f;

	// Token: 0x04000E4F RID: 3663
	public static readonly Chore.Precondition SolidLubricantIsNotNull = new Chore.Precondition
	{
		id = "SolidLubricantIsNotNull ",
		description = DUPLICANTS.CHORES.PRECONDITIONS.EDIBLE_IS_NOT_NULL,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return null != context.consumerState.consumer.GetSMI<BionicOilMonitor.Instance>().GetClosestSolidLubricant();
		}
	};

	// Token: 0x020012D4 RID: 4820
	public class States : GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore>
	{
		// Token: 0x060087E9 RID: 34793 RVA: 0x00347AC8 File Offset: 0x00345CC8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			base.Target(this.dupe);
			this.fetch.InitializeStates(this.dupe, this.solidLubricantSource, this.pickedUpSolidLubricant, this.amountRequested, this.actualunits, this.consume, null).OnTargetLost(this.solidLubricantSource, this.lubricantLost);
			this.consume.DefaultState(this.consume.pre).ToggleAnims("anim_bionic_kanim", 0f).Enter("Add Symbol Override", delegate(UseSolidLubricantChore.Instance smi)
			{
				UseSolidLubricantChore.SetOverrideAnimSymbol(smi, true);
			})
				.Exit("Revert Symbol Override", delegate(UseSolidLubricantChore.Instance smi)
				{
					UseSolidLubricantChore.SetOverrideAnimSymbol(smi, false);
				});
			this.consume.pre.PlayAnim("lubricate_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.consume.loop).ScheduleGoTo(4.7f, this.consume.loop);
			this.consume.loop.PlayAnim("lubricate_loop", KAnim.PlayMode.Loop).ScheduleGoTo(6.666f, this.consume.pst);
			this.consume.pst.PlayAnim("lubricate_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete).ScheduleGoTo(3.5f, this.complete);
			this.complete.Enter(new StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State.Callback(UseSolidLubricantChore.ConsumeLubricant)).ReturnSuccess();
			this.lubricantLost.Target(this.dupe).ReturnFailure();
		}

		// Token: 0x040067A1 RID: 26529
		public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.FetchSubState fetch;

		// Token: 0x040067A2 RID: 26530
		public UseSolidLubricantChore.States.InstallState consume;

		// Token: 0x040067A3 RID: 26531
		public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State complete;

		// Token: 0x040067A4 RID: 26532
		public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State lubricantLost;

		// Token: 0x040067A5 RID: 26533
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.TargetParameter dupe;

		// Token: 0x040067A6 RID: 26534
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.TargetParameter solidLubricantSource;

		// Token: 0x040067A7 RID: 26535
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.TargetParameter pickedUpSolidLubricant;

		// Token: 0x040067A8 RID: 26536
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.TargetParameter messstation;

		// Token: 0x040067A9 RID: 26537
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.FloatParameter actualunits;

		// Token: 0x040067AA RID: 26538
		public StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.FloatParameter amountRequested = new StateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.FloatParameter(LubricationStickConfig.MASS_PER_RECIPE);

		// Token: 0x02002688 RID: 9864
		public class InstallState : GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State
		{
			// Token: 0x0400AB40 RID: 43840
			public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State pre;

			// Token: 0x0400AB41 RID: 43841
			public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State loop;

			// Token: 0x0400AB42 RID: 43842
			public GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.State pst;
		}
	}

	// Token: 0x020012D5 RID: 4821
	public class Instance : GameStateMachine<UseSolidLubricantChore.States, UseSolidLubricantChore.Instance, UseSolidLubricantChore, object>.GameInstance
	{
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060087EB RID: 34795 RVA: 0x00347C89 File Offset: 0x00345E89
		public BionicOilMonitor.Instance oilMonitor
		{
			get
			{
				return base.sm.dupe.Get(this).GetSMI<BionicOilMonitor.Instance>();
			}
		}

		// Token: 0x060087EC RID: 34796 RVA: 0x00347CA1 File Offset: 0x00345EA1
		public Instance(UseSolidLubricantChore master, GameObject duplicant)
			: base(master)
		{
		}
	}
}
