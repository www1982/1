using System;

// Token: 0x0200047A RID: 1146
public class DeliverFoodChore : Chore<DeliverFoodChore.StatesInstance>
{
	// Token: 0x0600181A RID: 6170 RVA: 0x000863A8 File Offset: 0x000845A8
	public DeliverFoodChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.DeliverFood, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new DeliverFoodChore.StatesInstance(this);
		this.AddPrecondition(ChorePreconditions.instance.IsChattable, target);
	}

	// Token: 0x0600181B RID: 6171 RVA: 0x000863FC File Offset: 0x000845FC
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.sm.requestedrationcount.Set(base.smi.GetComponent<StateMachineController>().GetSMI<RationMonitor.Instance>().GetRationsRemaining(), base.smi, false);
		base.smi.sm.ediblesource.Set(context.consumerState.gameObject.GetComponent<Sensors>().GetSensor<ClosestEdibleSensor>().GetEdible(), base.smi);
		base.smi.sm.deliverypoint.Set(this.gameObject, base.smi, false);
		base.smi.sm.deliverer.Set(context.consumerState.gameObject, base.smi, false);
		base.Begin(context);
	}

	// Token: 0x0200126F RID: 4719
	public class StatesInstance : GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.GameInstance
	{
		// Token: 0x06008679 RID: 34425 RVA: 0x0033DA80 File Offset: 0x0033BC80
		public StatesInstance(DeliverFoodChore master)
			: base(master)
		{
		}
	}

	// Token: 0x02001270 RID: 4720
	public class States : GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore>
	{
		// Token: 0x0600867A RID: 34426 RVA: 0x0033DA8C File Offset: 0x0033BC8C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetch;
			this.fetch.InitializeStates(this.deliverer, this.ediblesource, this.ediblechunk, this.requestedrationcount, this.actualrationcount, this.movetodeliverypoint, null);
			this.movetodeliverypoint.InitializeStates(this.deliverer, this.deliverypoint, this.drop, null, null, null);
			this.drop.InitializeStates(this.deliverer, this.ediblechunk, this.deliverypoint, this.success, null);
			this.success.ReturnSuccess();
		}

		// Token: 0x04006645 RID: 26181
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.TargetParameter deliverer;

		// Token: 0x04006646 RID: 26182
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.TargetParameter ediblesource;

		// Token: 0x04006647 RID: 26183
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.TargetParameter ediblechunk;

		// Token: 0x04006648 RID: 26184
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.TargetParameter deliverypoint;

		// Token: 0x04006649 RID: 26185
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.FloatParameter requestedrationcount;

		// Token: 0x0400664A RID: 26186
		public StateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.FloatParameter actualrationcount;

		// Token: 0x0400664B RID: 26187
		public GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.FetchSubState fetch;

		// Token: 0x0400664C RID: 26188
		public GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.ApproachSubState<Chattable> movetodeliverypoint;

		// Token: 0x0400664D RID: 26189
		public GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.DropSubState drop;

		// Token: 0x0400664E RID: 26190
		public GameStateMachine<DeliverFoodChore.States, DeliverFoodChore.StatesInstance, DeliverFoodChore, object>.State success;
	}
}
