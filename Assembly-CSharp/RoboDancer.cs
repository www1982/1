using System;
using TUNING;

// Token: 0x020004C4 RID: 1220
public class RoboDancer : GameStateMachine<RoboDancer, RoboDancer.Instance>
{
	// Token: 0x06001A1E RID: 6686 RVA: 0x0008F640 File Offset: 0x0008D840
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).DefaultState(this.overjoyed.idle).ParamTransition<float>(this.timeSpentDancing, this.overjoyed.exitEarly, (RoboDancer.Instance smi, float p) => p >= TRAITS.JOY_REACTIONS.ROBO_DANCER.DANCE_DURATION && !this.hasAudience.Get(smi))
			.Exit(delegate(RoboDancer.Instance smi)
			{
				this.timeSpentDancing.Set(0f, smi, false);
			});
		this.overjoyed.idle.Enter(delegate(RoboDancer.Instance smi)
		{
			if (smi.IsRecTime())
			{
				smi.GoTo(this.overjoyed.dancing);
			}
		}).ToggleStatusItem(Db.Get().DuplicantStatusItems.RoboDancerPlanning, null).EventTransition(GameHashes.ScheduleBlocksTick, this.overjoyed.dancing, (RoboDancer.Instance smi) => smi.IsRecTime());
		this.overjoyed.dancing.ToggleStatusItem(Db.Get().DuplicantStatusItems.RoboDancerDancing, null).EventTransition(GameHashes.ScheduleBlocksTick, this.overjoyed.idle, (RoboDancer.Instance smi) => !smi.IsRecTime()).ToggleChore((RoboDancer.Instance smi) => new RoboDancerChore(smi.master), this.overjoyed.idle);
		this.overjoyed.exitEarly.Enter(delegate(RoboDancer.Instance smi)
		{
			smi.ExitJoyReactionEarly();
		});
	}

	// Token: 0x04000F01 RID: 3841
	public StateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.FloatParameter timeSpentDancing;

	// Token: 0x04000F02 RID: 3842
	public StateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.BoolParameter hasAudience;

	// Token: 0x04000F03 RID: 3843
	public GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000F04 RID: 3844
	public RoboDancer.OverjoyedStates overjoyed;

	// Token: 0x02001317 RID: 4887
	public class OverjoyedStates : GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400684D RID: 26701
		public GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x0400684E RID: 26702
		public GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.State dancing;

		// Token: 0x0400684F RID: 26703
		public GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.State exitEarly;
	}

	// Token: 0x02001318 RID: 4888
	public new class Instance : GameStateMachine<RoboDancer, RoboDancer.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x060088A8 RID: 34984 RVA: 0x00349BA9 File Offset: 0x00347DA9
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x060088A9 RID: 34985 RVA: 0x00349BB2 File Offset: 0x00347DB2
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x060088AA RID: 34986 RVA: 0x00349BD4 File Offset: 0x00347DD4
		public void ExitJoyReactionEarly()
		{
			JoyBehaviourMonitor.Instance smi = base.master.gameObject.GetSMI<JoyBehaviourMonitor.Instance>();
			smi.sm.exitEarly.Trigger(smi);
		}
	}
}
