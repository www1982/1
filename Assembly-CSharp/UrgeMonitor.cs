using System;
using Klei.AI;

// Token: 0x02000A1C RID: 2588
public class UrgeMonitor : GameStateMachine<UrgeMonitor, UrgeMonitor.Instance>
{
	// Token: 0x06004B31 RID: 19249 RVA: 0x001B43F0 File Offset: 0x001B25F0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.Transition(this.hasurge, (UrgeMonitor.Instance smi) => smi.HasUrge(), UpdateRate.SIM_200ms);
		this.hasurge.Transition(this.satisfied, (UrgeMonitor.Instance smi) => !smi.HasUrge(), UpdateRate.SIM_200ms).ToggleUrge((UrgeMonitor.Instance smi) => smi.GetUrge());
	}

	// Token: 0x040031D7 RID: 12759
	public GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040031D8 RID: 12760
	public GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.State hasurge;

	// Token: 0x02001ACA RID: 6858
	public new class Instance : GameStateMachine<UrgeMonitor, UrgeMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A518 RID: 42264 RVA: 0x003A8378 File Offset: 0x003A6578
		public Instance(IStateMachineTarget master, Urge urge, Amount amount, ScheduleBlockType schedule_block, float in_schedule_threshold, float out_of_schedule_threshold, bool is_threshold_minimum)
			: base(master)
		{
			this.urge = urge;
			this.scheduleBlock = schedule_block;
			this.schedulable = base.GetComponent<Schedulable>();
			this.amountInstance = base.gameObject.GetAmounts().Get(amount);
			this.isThresholdMinimum = is_threshold_minimum;
			this.inScheduleThreshold = in_schedule_threshold;
			this.outOfScheduleThreshold = out_of_schedule_threshold;
		}

		// Token: 0x0600A519 RID: 42265 RVA: 0x003A83D6 File Offset: 0x003A65D6
		private float GetThreshold()
		{
			if (this.schedulable.IsAllowed(this.scheduleBlock))
			{
				return this.inScheduleThreshold;
			}
			return this.outOfScheduleThreshold;
		}

		// Token: 0x0600A51A RID: 42266 RVA: 0x003A83F8 File Offset: 0x003A65F8
		public Urge GetUrge()
		{
			return this.urge;
		}

		// Token: 0x0600A51B RID: 42267 RVA: 0x003A8400 File Offset: 0x003A6600
		public bool HasUrge()
		{
			if (this.isThresholdMinimum)
			{
				return this.amountInstance.value >= this.GetThreshold();
			}
			return this.amountInstance.value <= this.GetThreshold();
		}

		// Token: 0x040080E8 RID: 33000
		private AmountInstance amountInstance;

		// Token: 0x040080E9 RID: 33001
		private Urge urge;

		// Token: 0x040080EA RID: 33002
		private ScheduleBlockType scheduleBlock;

		// Token: 0x040080EB RID: 33003
		private Schedulable schedulable;

		// Token: 0x040080EC RID: 33004
		private float inScheduleThreshold;

		// Token: 0x040080ED RID: 33005
		private float outOfScheduleThreshold;

		// Token: 0x040080EE RID: 33006
		private bool isThresholdMinimum;
	}
}
