using System;
using TUNING;

// Token: 0x020004C2 RID: 1218
public class DataRainer : GameStateMachine<DataRainer, DataRainer.Instance>
{
	// Token: 0x06001A16 RID: 6678 RVA: 0x0008F268 File Offset: 0x0008D468
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.neutral;
		this.root.TagTransition(GameTags.Dead, null, false);
		this.neutral.TagTransition(GameTags.Overjoyed, this.overjoyed, false);
		this.overjoyed.TagTransition(GameTags.Overjoyed, this.neutral, true).DefaultState(this.overjoyed.idle).ParamTransition<int>(this.databanksCreated, this.overjoyed.exitEarly, (DataRainer.Instance smi, int p) => p >= TRAITS.JOY_REACTIONS.DATA_RAINER.NUM_MICROCHIPS)
			.Exit(delegate(DataRainer.Instance smi)
			{
				this.databanksCreated.Set(0, smi, false);
			});
		this.overjoyed.idle.Enter(delegate(DataRainer.Instance smi)
		{
			if (smi.IsRecTime())
			{
				smi.GoTo(this.overjoyed.raining);
			}
		}).ToggleStatusItem(Db.Get().DuplicantStatusItems.DataRainerPlanning, null).EventTransition(GameHashes.ScheduleBlocksTick, this.overjoyed.raining, (DataRainer.Instance smi) => smi.IsRecTime());
		this.overjoyed.raining.ToggleStatusItem(Db.Get().DuplicantStatusItems.DataRainerRaining, null).EventTransition(GameHashes.ScheduleBlocksTick, this.overjoyed.idle, (DataRainer.Instance smi) => !smi.IsRecTime()).ToggleChore((DataRainer.Instance smi) => new DataRainerChore(smi.master), this.overjoyed.idle);
		this.overjoyed.exitEarly.Enter(delegate(DataRainer.Instance smi)
		{
			smi.ExitJoyReactionEarly();
		});
	}

	// Token: 0x04000EF9 RID: 3833
	public StateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.IntParameter databanksCreated;

	// Token: 0x04000EFA RID: 3834
	public static float databankSpawnInterval = 1.8f;

	// Token: 0x04000EFB RID: 3835
	public GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.State neutral;

	// Token: 0x04000EFC RID: 3836
	public DataRainer.OverjoyedStates overjoyed;

	// Token: 0x02001311 RID: 4881
	public class OverjoyedStates : GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x0400683B RID: 26683
		public GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.State idle;

		// Token: 0x0400683C RID: 26684
		public GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.State raining;

		// Token: 0x0400683D RID: 26685
		public GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.State exitEarly;
	}

	// Token: 0x02001312 RID: 4882
	public new class Instance : GameStateMachine<DataRainer, DataRainer.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008891 RID: 34961 RVA: 0x0034991C File Offset: 0x00347B1C
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x06008892 RID: 34962 RVA: 0x00349925 File Offset: 0x00347B25
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x06008893 RID: 34963 RVA: 0x00349948 File Offset: 0x00347B48
		public void ExitJoyReactionEarly()
		{
			JoyBehaviourMonitor.Instance smi = base.master.gameObject.GetSMI<JoyBehaviourMonitor.Instance>();
			smi.sm.exitEarly.Trigger(smi);
		}
	}
}
