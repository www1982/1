using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020004A6 RID: 1190
public class WaterCoolerChore : Chore<WaterCoolerChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x060018C2 RID: 6338 RVA: 0x0008A6D4 File Offset: 0x000888D4
	public WaterCoolerChore(IStateMachineTarget master, Workable chat_workable, Action<Chore> on_complete = null, Action<Chore> on_begin = null, Action<Chore> on_end = null)
		: base(Db.Get().ChoreTypes.Relax, master, master.GetComponent<ChoreProvider>(), true, on_complete, on_begin, on_end, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		base.smi = new WaterCoolerChore.StatesInstance(this);
		base.smi.sm.chitchatlocator.Set(chat_workable, base.smi);
		this.AddPrecondition(ChorePreconditions.instance.CanMoveTo, chat_workable);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		this.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x060018C3 RID: 6339 RVA: 0x0008A7A6 File Offset: 0x000889A6
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.drinker.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x060018C4 RID: 6340 RVA: 0x0008A7D8 File Offset: 0x000889D8
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect) && component.HasEffect(this.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.specificEffect) && component.HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04000E50 RID: 3664
	public int basePriority = RELAXATION.PRIORITY.TIER2;

	// Token: 0x04000E51 RID: 3665
	public string specificEffect = "Socialized";

	// Token: 0x04000E52 RID: 3666
	public string trackingEffect = "RecentlySocialized";

	// Token: 0x020012D9 RID: 4825
	public class States : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore>
	{
		// Token: 0x060087F9 RID: 34809 RVA: 0x003481AC File Offset: 0x003463AC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.drink_move;
			base.Target(this.drinker);
			this.drink_move.InitializeStates(this.drinker, this.masterTarget, this.drink, null, null, null);
			this.drink.ToggleAnims(new Func<WaterCoolerChore.StatesInstance, KAnimFile>(WaterCoolerChore.States.GetAnimFileName)).DefaultState(this.drink.drink);
			this.drink.drink.Face(this.masterTarget, 0.5f).PlayAnim("working_pre").QueueAnim("working_loop", false, null)
				.OnAnimQueueComplete(this.drink.post);
			this.drink.post.Enter("Drink", new StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State.Callback(this.TriggerDrink)).Enter("Mark", new StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State.Callback(this.MarkAsRecentlySocialized)).PlayAnim("working_pst")
				.OnAnimQueueComplete(this.chat_move);
			this.chat_move.InitializeStates(this.drinker, this.chitchatlocator, this.chat, null, null, null);
			this.chat.ToggleWork<SocialGatheringPointWorkable>(this.chitchatlocator, this.success, null, null);
			this.success.ReturnSuccess();
		}

		// Token: 0x060087FA RID: 34810 RVA: 0x003482EC File Offset: 0x003464EC
		public static KAnimFile GetAnimFileName(WaterCoolerChore.StatesInstance smi)
		{
			GameObject gameObject = smi.sm.drinker.Get(smi);
			if (gameObject == null)
			{
				return Assets.GetAnim("anim_interacts_watercooler_kanim");
			}
			MinionIdentity component = gameObject.GetComponent<MinionIdentity>();
			if (component != null && component.model == BionicMinionConfig.MODEL)
			{
				return Assets.GetAnim("anim_bionic_interacts_watercooler_kanim");
			}
			return Assets.GetAnim("anim_interacts_watercooler_kanim");
		}

		// Token: 0x060087FB RID: 34811 RVA: 0x00348368 File Offset: 0x00346568
		private void MarkAsRecentlySocialized(WaterCoolerChore.StatesInstance smi)
		{
			Effects component = this.stateTarget.Get<WorkerBase>(smi).GetComponent<Effects>();
			if (!string.IsNullOrEmpty(smi.master.trackingEffect))
			{
				component.Add(smi.master.trackingEffect, true);
			}
		}

		// Token: 0x060087FC RID: 34812 RVA: 0x003483AC File Offset: 0x003465AC
		private void TriggerDrink(WaterCoolerChore.StatesInstance smi)
		{
			WorkerBase workerBase = this.stateTarget.Get<WorkerBase>(smi);
			smi.master.target.gameObject.GetSMI<WaterCooler.StatesInstance>().Drink(workerBase.gameObject, true);
		}

		// Token: 0x040067B7 RID: 26551
		public StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.TargetParameter drinker;

		// Token: 0x040067B8 RID: 26552
		public StateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.TargetParameter chitchatlocator;

		// Token: 0x040067B9 RID: 26553
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.ApproachSubState<WaterCooler> drink_move;

		// Token: 0x040067BA RID: 26554
		public WaterCoolerChore.States.DrinkStates drink;

		// Token: 0x040067BB RID: 26555
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.ApproachSubState<IApproachable> chat_move;

		// Token: 0x040067BC RID: 26556
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State chat;

		// Token: 0x040067BD RID: 26557
		public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State success;

		// Token: 0x0200268C RID: 9868
		public class DrinkStates : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State
		{
			// Token: 0x0400AB50 RID: 43856
			public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State drink;

			// Token: 0x0400AB51 RID: 43857
			public GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.State post;
		}
	}

	// Token: 0x020012DA RID: 4826
	public class StatesInstance : GameStateMachine<WaterCoolerChore.States, WaterCoolerChore.StatesInstance, WaterCoolerChore, object>.GameInstance
	{
		// Token: 0x060087FE RID: 34814 RVA: 0x003483EF File Offset: 0x003465EF
		public StatesInstance(WaterCoolerChore master)
			: base(master)
		{
		}
	}
}
