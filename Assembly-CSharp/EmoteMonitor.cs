using System;
using UnityEngine;

// Token: 0x020009EB RID: 2539
public class EmoteMonitor : GameStateMachine<EmoteMonitor, EmoteMonitor.Instance>
{
	// Token: 0x06004A2B RID: 18987 RVA: 0x001ADA84 File Offset: 0x001ABC84
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.ScheduleGoTo((EmoteMonitor.Instance smi) => (float)global::UnityEngine.Random.Range(30, 90), this.ready);
		this.ready.ToggleUrge(Db.Get().Urges.Emote).EventHandler(GameHashes.BeginChore, delegate(EmoteMonitor.Instance smi, object o)
		{
			smi.OnStartChore(o);
		});
	}

	// Token: 0x040030EC RID: 12524
	public GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040030ED RID: 12525
	public GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.State ready;

	// Token: 0x02001A45 RID: 6725
	public new class Instance : GameStateMachine<EmoteMonitor, EmoteMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A2CD RID: 41677 RVA: 0x003A1EDD File Offset: 0x003A00DD
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A2CE RID: 41678 RVA: 0x003A1EE6 File Offset: 0x003A00E6
		public void OnStartChore(object o)
		{
			if (((Chore)o).SatisfiesUrge(Db.Get().Urges.Emote))
			{
				this.GoTo(base.sm.satisfied);
			}
		}
	}
}
