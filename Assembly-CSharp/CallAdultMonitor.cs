using System;
using UnityEngine;

// Token: 0x02000855 RID: 2133
public class CallAdultMonitor : GameStateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>
{
	// Token: 0x06003A93 RID: 14995 RVA: 0x00145B2C File Offset: 0x00143D2C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.CallAdultBehaviour, new StateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>.Transition.ConditionCallback(CallAdultMonitor.ShouldCallAdult), delegate(CallAdultMonitor.Instance smi)
		{
			smi.RefreshCallTime();
		});
	}

	// Token: 0x06003A94 RID: 14996 RVA: 0x00145B7D File Offset: 0x00143D7D
	public static bool ShouldCallAdult(CallAdultMonitor.Instance smi)
	{
		return Time.time >= smi.nextCallTime;
	}

	// Token: 0x020017DA RID: 6106
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007724 RID: 30500
		public float callMinInterval = 120f;

		// Token: 0x04007725 RID: 30501
		public float callMaxInterval = 240f;
	}

	// Token: 0x020017DB RID: 6107
	public new class Instance : GameStateMachine<CallAdultMonitor, CallAdultMonitor.Instance, IStateMachineTarget, CallAdultMonitor.Def>.GameInstance
	{
		// Token: 0x06009A99 RID: 39577 RVA: 0x0038AE9F File Offset: 0x0038909F
		public Instance(IStateMachineTarget master, CallAdultMonitor.Def def)
			: base(master, def)
		{
			this.RefreshCallTime();
		}

		// Token: 0x06009A9A RID: 39578 RVA: 0x0038AEAF File Offset: 0x003890AF
		public void RefreshCallTime()
		{
			this.nextCallTime = Time.time + global::UnityEngine.Random.value * (base.def.callMaxInterval - base.def.callMinInterval) + base.def.callMinInterval;
		}

		// Token: 0x04007726 RID: 30502
		public float nextCallTime;
	}
}
