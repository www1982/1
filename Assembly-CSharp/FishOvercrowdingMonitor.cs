using System;

// Token: 0x0200086B RID: 2155
public class FishOvercrowdingMonitor : GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>
{
	// Token: 0x06003B28 RID: 15144 RVA: 0x00148C5C File Offset: 0x00146E5C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.Enter(new StateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State.Callback(FishOvercrowdingMonitor.Register)).Exit(new StateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State.Callback(FishOvercrowdingMonitor.Unregister));
		this.satisfied.DoNothing();
		this.overcrowded.DoNothing();
	}

	// Token: 0x06003B29 RID: 15145 RVA: 0x00148CB2 File Offset: 0x00146EB2
	private static void Register(FishOvercrowdingMonitor.Instance smi)
	{
		FishOvercrowingManager.Instance.Add(smi);
	}

	// Token: 0x06003B2A RID: 15146 RVA: 0x00148CC0 File Offset: 0x00146EC0
	private static void Unregister(FishOvercrowdingMonitor.Instance smi)
	{
		FishOvercrowingManager instance = FishOvercrowingManager.Instance;
		if (instance == null)
		{
			return;
		}
		instance.Remove(smi);
	}

	// Token: 0x04002453 RID: 9299
	public GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State satisfied;

	// Token: 0x04002454 RID: 9300
	public GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.State overcrowded;

	// Token: 0x02001810 RID: 6160
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001811 RID: 6161
	public new class Instance : GameStateMachine<FishOvercrowdingMonitor, FishOvercrowdingMonitor.Instance, IStateMachineTarget, FishOvercrowdingMonitor.Def>.GameInstance
	{
		// Token: 0x06009B65 RID: 39781 RVA: 0x0038D58D File Offset: 0x0038B78D
		public Instance(IStateMachineTarget master, FishOvercrowdingMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06009B66 RID: 39782 RVA: 0x0038D597 File Offset: 0x0038B797
		public void SetOvercrowdingInfo(int cell_count, int fish_count)
		{
			this.cellCount = cell_count;
			this.fishCount = fish_count;
		}

		// Token: 0x040077CB RID: 30667
		public int cellCount;

		// Token: 0x040077CC RID: 30668
		public int fishCount;
	}
}
