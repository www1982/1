using System;
using UnityEngine;

// Token: 0x020000D4 RID: 212
public class BeeForagingMonitor : GameStateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>
{
	// Token: 0x060003C1 RID: 961 RVA: 0x0001FF88 File Offset: 0x0001E188
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToForage, new StateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>.Transition.ConditionCallback(BeeForagingMonitor.ShouldForage), delegate(BeeForagingMonitor.Instance smi)
		{
			smi.RefreshSearchTime();
		});
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x0001FFDC File Offset: 0x0001E1DC
	public static bool ShouldForage(BeeForagingMonitor.Instance smi)
	{
		bool flag = GameClock.Instance.GetTimeInCycles() >= smi.nextSearchTime;
		KPrefabID kprefabID = smi.master.GetComponent<Bee>().FindHiveInRoom();
		if (kprefabID != null)
		{
			BeehiveCalorieMonitor.Instance smi2 = kprefabID.GetSMI<BeehiveCalorieMonitor.Instance>();
			if (smi2 == null || !smi2.IsHungry())
			{
				flag = false;
			}
		}
		return flag && kprefabID != null;
	}

	// Token: 0x02001096 RID: 4246
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040060E5 RID: 24805
		public float searchMinInterval = 0.25f;

		// Token: 0x040060E6 RID: 24806
		public float searchMaxInterval = 0.3f;
	}

	// Token: 0x02001097 RID: 4247
	public new class Instance : GameStateMachine<BeeForagingMonitor, BeeForagingMonitor.Instance, IStateMachineTarget, BeeForagingMonitor.Def>.GameInstance
	{
		// Token: 0x06008042 RID: 32834 RVA: 0x0032D35F File Offset: 0x0032B55F
		public Instance(IStateMachineTarget master, BeeForagingMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x0032D36F File Offset: 0x0032B56F
		public void RefreshSearchTime()
		{
			this.nextSearchTime = GameClock.Instance.GetTimeInCycles() + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, global::UnityEngine.Random.value);
		}

		// Token: 0x040060E7 RID: 24807
		public float nextSearchTime;
	}
}
