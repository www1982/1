using System;
using UnityEngine;

// Token: 0x02000958 RID: 2392
public class HotTubWorkerStateMachine : GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase>
{
	// Token: 0x060044BF RID: 17599 RVA: 0x0018ACE4 File Offset: 0x00188EE4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre_front;
		base.Target(this.worker);
		this.root.ToggleAnims("anim_interacts_hottub_kanim", 0f);
		this.pre_front.PlayAnim("working_pre_front").OnAnimQueueComplete(this.pre_back);
		this.pre_back.PlayAnim("working_pre_back").Enter(delegate(HotTubWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position = smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
			smi.transform.SetPosition(position);
		}).OnAnimQueueComplete(this.loop);
		this.loop.PlayAnim((HotTubWorkerStateMachine.StatesInstance smi) => HotTubWorkerStateMachine.workAnimLoopVariants[global::UnityEngine.Random.Range(0, HotTubWorkerStateMachine.workAnimLoopVariants.Length)], KAnim.PlayMode.Once).OnAnimQueueComplete(this.loop_reenter).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (HotTubWorkerStateMachine.StatesInstance smi) => smi.GetComponent<WorkerBase>().GetState() == WorkerBase.State.PendingCompletion);
		this.loop_reenter.GoTo(this.loop).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (HotTubWorkerStateMachine.StatesInstance smi) => smi.GetComponent<WorkerBase>().GetState() == WorkerBase.State.PendingCompletion);
		this.pst_back.PlayAnim("working_pst_back").OnAnimQueueComplete(this.pst_front);
		this.pst_front.PlayAnim("working_pst_front").Enter(delegate(HotTubWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position2 = smi.transform.GetPosition();
			position2.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			smi.transform.SetPosition(position2);
		}).OnAnimQueueComplete(this.complete);
	}

	// Token: 0x04002E04 RID: 11780
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State pre_front;

	// Token: 0x04002E05 RID: 11781
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State pre_back;

	// Token: 0x04002E06 RID: 11782
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State loop;

	// Token: 0x04002E07 RID: 11783
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State loop_reenter;

	// Token: 0x04002E08 RID: 11784
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State pst_back;

	// Token: 0x04002E09 RID: 11785
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State pst_front;

	// Token: 0x04002E0A RID: 11786
	private GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.State complete;

	// Token: 0x04002E0B RID: 11787
	public StateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.TargetParameter worker;

	// Token: 0x04002E0C RID: 11788
	public static string[] workAnimLoopVariants = new string[] { "working_loop1", "working_loop2", "working_loop3" };

	// Token: 0x0200196F RID: 6511
	public class StatesInstance : GameStateMachine<HotTubWorkerStateMachine, HotTubWorkerStateMachine.StatesInstance, WorkerBase, object>.GameInstance
	{
		// Token: 0x06009F2B RID: 40747 RVA: 0x00398D8B File Offset: 0x00396F8B
		public StatesInstance(WorkerBase master)
			: base(master)
		{
			base.sm.worker.Set(master, base.smi);
		}
	}
}
