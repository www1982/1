using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000497 RID: 1175
public class RescueIncapacitatedChore : Chore<RescueIncapacitatedChore.StatesInstance>
{
	// Token: 0x06001893 RID: 6291 RVA: 0x00089404 File Offset: 0x00087604
	public RescueIncapacitatedChore(IStateMachineTarget master, GameObject incapacitatedDuplicant)
		: base(Db.Get().ChoreTypes.RescueIncapacitated, master, null, false, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new RescueIncapacitatedChore.StatesInstance(this);
		this.runUntilComplete = true;
		this.AddPrecondition(ChorePreconditions.instance.NotChoreCreator, incapacitatedDuplicant.gameObject);
		this.AddPrecondition(RescueIncapacitatedChore.CanReachIncapacitated, incapacitatedDuplicant);
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x0008946C File Offset: 0x0008766C
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.rescuer.Set(context.consumerState.gameObject, base.smi, false);
		base.smi.sm.rescueTarget.Set(this.gameObject, base.smi, false);
		base.smi.sm.deliverTarget.Set(this.gameObject.GetSMI<BeIncapacitatedChore.StatesInstance>().master.GetChosenClinic(), base.smi, false);
		base.Begin(context);
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x000894FD File Offset: 0x000876FD
	protected override void End(string reason)
	{
		this.DropIncapacitatedDuplicant();
		base.End(reason);
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x0008950C File Offset: 0x0008770C
	private void DropIncapacitatedDuplicant()
	{
		if (base.smi.sm.rescuer.Get(base.smi) != null && base.smi.sm.rescueTarget.Get(base.smi) != null)
		{
			base.smi.sm.rescuer.Get(base.smi).GetComponent<Storage>().Drop(base.smi.sm.rescueTarget.Get(base.smi), true);
		}
	}

	// Token: 0x04000E42 RID: 3650
	public static Chore.Precondition CanReachIncapacitated = new Chore.Precondition
	{
		id = "CanReachIncapacitated",
		description = DUPLICANTS.CHORES.PRECONDITIONS.CAN_MOVE_TO,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject == null)
			{
				return false;
			}
			int navigationCost = context.consumerState.navigator.GetNavigationCost(Grid.PosToCell(gameObject.transform.GetPosition()));
			if (-1 != navigationCost)
			{
				context.cost += navigationCost;
				return true;
			}
			return false;
		}
	};

	// Token: 0x020012B5 RID: 4789
	public class StatesInstance : GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.GameInstance
	{
		// Token: 0x06008779 RID: 34681 RVA: 0x00344246 File Offset: 0x00342446
		public StatesInstance(RescueIncapacitatedChore master)
			: base(master)
		{
		}
	}

	// Token: 0x020012B6 RID: 4790
	public class States : GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore>
	{
		// Token: 0x0600877A RID: 34682 RVA: 0x00344250 File Offset: 0x00342450
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.approachIncapacitated;
			this.approachIncapacitated.InitializeStates(this.rescuer, this.rescueTarget, this.holding.pickup, this.failure, Grid.DefaultOffset, null).Enter(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				DeathMonitor.Instance smi2 = this.rescueTarget.GetSMI<DeathMonitor.Instance>(smi);
				if (smi2 == null || smi2.IsDead())
				{
					smi.StopSM("target died");
				}
			});
			this.holding.Target(this.rescuer).Enter(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				smi.sm.rescueTarget.Get(smi).Subscribe(1623392196, delegate(object d)
				{
					smi.GoTo(this.holding.ditch);
				});
				GameObject gameObject = this.rescuer.Get(smi).gameObject;
				if (!gameObject.IsNullOrDestroyed() && gameObject.HasTag(GameTags.BaseMinion))
				{
					KAnimFile anim = Assets.GetAnim("anim_incapacitated_carrier_kanim");
					smi.master.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(anim);
					smi.master.GetComponent<KAnimControllerBase>().AddAnimOverrides(anim, 0f);
				}
			}).Exit(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				GameObject gameObject2 = this.rescuer.Get(smi).gameObject;
				if (!gameObject2.IsNullOrDestroyed() && gameObject2.HasTag(GameTags.BaseMinion))
				{
					KAnimFile anim2 = Assets.GetAnim("anim_incapacitated_carrier_kanim");
					smi.master.GetComponent<KAnimControllerBase>().RemoveAnimOverrides(anim2);
				}
			});
			this.holding.pickup.Target(this.rescuer).PlayAnim("pickup").Enter(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				this.rescueTarget.Get(smi).gameObject.GetComponent<KBatchedAnimController>().Play("pickup", KAnim.PlayMode.Once, 1f, 0f);
			})
				.Exit(delegate(RescueIncapacitatedChore.StatesInstance smi)
				{
					this.rescuer.Get(smi).GetComponent<Storage>().Store(this.rescueTarget.Get(smi), false, false, true, false);
					this.rescueTarget.Get(smi).transform.SetLocalPosition(Vector3.zero);
					KBatchedAnimTracker component = this.rescueTarget.Get(smi).GetComponent<KBatchedAnimTracker>();
					if (component != null)
					{
						component.symbol = new HashedString("snapTo_pivot");
						component.offset = new Vector3(0f, 0f, 1f);
					}
				})
				.EventTransition(GameHashes.AnimQueueComplete, this.holding.delivering, null);
			this.holding.delivering.InitializeStates(this.rescuer, this.deliverTarget, this.holding.deposit, this.holding.ditch, null, null).Enter(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				DeathMonitor.Instance smi3 = this.rescueTarget.GetSMI<DeathMonitor.Instance>(smi);
				if (smi3 == null || smi3.IsDead())
				{
					smi.StopSM("target died");
				}
			}).Update(delegate(RescueIncapacitatedChore.StatesInstance smi, float dt)
			{
				if (this.deliverTarget.Get(smi) == null)
				{
					smi.GoTo(this.holding.ditch);
				}
			}, UpdateRate.SIM_200ms, false);
			this.holding.deposit.PlayAnim("place").EventHandler(GameHashes.AnimQueueComplete, delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				smi.master.DropIncapacitatedDuplicant();
				smi.SetStatus(StateMachine.Status.Success);
				smi.StopSM("complete");
			});
			this.holding.ditch.PlayAnim("place").ScheduleGoTo(0.5f, this.failure).Exit(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				smi.master.DropIncapacitatedDuplicant();
			});
			this.failure.Enter(delegate(RescueIncapacitatedChore.StatesInstance smi)
			{
				smi.SetStatus(StateMachine.Status.Failed);
				smi.StopSM("failed");
			});
		}

		// Token: 0x04006739 RID: 26425
		public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.ApproachSubState<Chattable> approachIncapacitated;

		// Token: 0x0400673A RID: 26426
		public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.State failure;

		// Token: 0x0400673B RID: 26427
		public RescueIncapacitatedChore.States.HoldingIncapacitated holding;

		// Token: 0x0400673C RID: 26428
		public StateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.TargetParameter rescueTarget;

		// Token: 0x0400673D RID: 26429
		public StateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.TargetParameter deliverTarget;

		// Token: 0x0400673E RID: 26430
		public StateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.TargetParameter rescuer;

		// Token: 0x02002677 RID: 9847
		public class HoldingIncapacitated : GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.State
		{
			// Token: 0x0400AAEA RID: 43754
			public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.State pickup;

			// Token: 0x0400AAEB RID: 43755
			public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.ApproachSubState<IApproachable> delivering;

			// Token: 0x0400AAEC RID: 43756
			public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.State deposit;

			// Token: 0x0400AAED RID: 43757
			public GameStateMachine<RescueIncapacitatedChore.States, RescueIncapacitatedChore.StatesInstance, RescueIncapacitatedChore, object>.State ditch;
		}
	}
}
