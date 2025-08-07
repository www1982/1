using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x020009E3 RID: 2531
public class CritterEmoteMonitor : GameStateMachine<CritterEmoteMonitor, CritterEmoteMonitor.Instance>
{
	// Token: 0x06004A11 RID: 18961 RVA: 0x001ACF84 File Offset: 0x001AB184
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.satisfied.ScheduleGoTo((CritterEmoteMonitor.Instance smi) => global::UnityEngine.Random.Range(75f, 150f), this.ready);
		this.ready.Enter(new StateMachine<CritterEmoteMonitor, CritterEmoteMonitor.Instance, IStateMachineTarget, object>.State.Callback(CritterEmoteMonitor.CreateChore)).ToggleUrge(Db.Get().Urges.Emote).EventHandler(GameHashes.BeginChore, delegate(CritterEmoteMonitor.Instance smi, object o)
		{
			smi.OnStartChore(o);
		});
	}

	// Token: 0x06004A12 RID: 18962 RVA: 0x001AD026 File Offset: 0x001AB226
	public static void CreateChore(CritterEmoteMonitor.Instance smi)
	{
		new EmoteChore(smi.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.Emote, smi.emotes.GetRandom<Emote>(), 1, null);
	}

	// Token: 0x040030D8 RID: 12504
	public GameStateMachine<CritterEmoteMonitor, CritterEmoteMonitor.Instance, IStateMachineTarget, object>.State satisfied;

	// Token: 0x040030D9 RID: 12505
	public GameStateMachine<CritterEmoteMonitor, CritterEmoteMonitor.Instance, IStateMachineTarget, object>.State ready;

	// Token: 0x02001A2F RID: 6703
	public new class Instance : GameStateMachine<CritterEmoteMonitor, CritterEmoteMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A27F RID: 41599 RVA: 0x003A13B8 File Offset: 0x0039F5B8
		public Instance(IStateMachineTarget master, List<Emote> emotes)
			: base(master)
		{
			this.emotes = emotes;
		}

		// Token: 0x0600A280 RID: 41600 RVA: 0x003A13C8 File Offset: 0x0039F5C8
		public void OnStartChore(object o)
		{
			if (((Chore)o).SatisfiesUrge(Db.Get().Urges.Emote))
			{
				this.GoTo(base.sm.satisfied);
			}
		}

		// Token: 0x04007EE2 RID: 32482
		public List<Emote> emotes;
	}
}
