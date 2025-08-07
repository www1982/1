using System;
using UnityEngine;

// Token: 0x02000BD8 RID: 3032
public class WindTunnelWorkerStateMachine : GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase>
{
	// Token: 0x06005AEB RID: 23275 RVA: 0x0020D970 File Offset: 0x0020BB70
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.pre_front;
		base.Target(this.worker);
		this.root.ToggleAnims((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.OverrideAnim);
		this.pre_front.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PreFrontAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.pre_back);
		this.pre_back.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PreBackAnim, KAnim.PlayMode.Once).Enter(delegate(WindTunnelWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position = smi.transform.GetPosition();
			position.z = Grid.GetLayerZ(Grid.SceneLayer.BuildingUse);
			smi.transform.SetPosition(position);
		}).OnAnimQueueComplete(this.loop);
		this.loop.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.LoopAnim, KAnim.PlayMode.Loop).EventTransition(GameHashes.WorkerPlayPostAnim, this.pst_back, (WindTunnelWorkerStateMachine.StatesInstance smi) => smi.GetComponent<WorkerBase>().GetState() == WorkerBase.State.PendingCompletion);
		this.pst_back.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PstBackAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.pst_front);
		this.pst_front.PlayAnim((WindTunnelWorkerStateMachine.StatesInstance smi) => smi.PstFrontAnim, KAnim.PlayMode.Once).Enter(delegate(WindTunnelWorkerStateMachine.StatesInstance smi)
		{
			Vector3 position2 = smi.transform.GetPosition();
			position2.z = Grid.GetLayerZ(Grid.SceneLayer.Move);
			smi.transform.SetPosition(position2);
		}).OnAnimQueueComplete(this.complete);
	}

	// Token: 0x04003C4C RID: 15436
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State pre_front;

	// Token: 0x04003C4D RID: 15437
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State pre_back;

	// Token: 0x04003C4E RID: 15438
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State loop;

	// Token: 0x04003C4F RID: 15439
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State pst_back;

	// Token: 0x04003C50 RID: 15440
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State pst_front;

	// Token: 0x04003C51 RID: 15441
	private GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.State complete;

	// Token: 0x04003C52 RID: 15442
	public StateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.TargetParameter worker;

	// Token: 0x02001D08 RID: 7432
	public class StatesInstance : GameStateMachine<WindTunnelWorkerStateMachine, WindTunnelWorkerStateMachine.StatesInstance, WorkerBase, object>.GameInstance
	{
		// Token: 0x0600ACDB RID: 44251 RVA: 0x003C2EB7 File Offset: 0x003C10B7
		public StatesInstance(WorkerBase master, VerticalWindTunnelWorkable workable)
			: base(master)
		{
			this.workable = workable;
			base.sm.worker.Set(master, base.smi);
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x0600ACDC RID: 44252 RVA: 0x003C2EDE File Offset: 0x003C10DE
		public HashedString OverrideAnim
		{
			get
			{
				return this.workable.overrideAnim;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600ACDD RID: 44253 RVA: 0x003C2EEB File Offset: 0x003C10EB
		public string PreFrontAnim
		{
			get
			{
				return this.workable.preAnims[0];
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600ACDE RID: 44254 RVA: 0x003C2EFA File Offset: 0x003C10FA
		public string PreBackAnim
		{
			get
			{
				return this.workable.preAnims[1];
			}
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600ACDF RID: 44255 RVA: 0x003C2F09 File Offset: 0x003C1109
		public string LoopAnim
		{
			get
			{
				return this.workable.loopAnim;
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600ACE0 RID: 44256 RVA: 0x003C2F16 File Offset: 0x003C1116
		public string PstBackAnim
		{
			get
			{
				return this.workable.pstAnims[0];
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600ACE1 RID: 44257 RVA: 0x003C2F25 File Offset: 0x003C1125
		public string PstFrontAnim
		{
			get
			{
				return this.workable.pstAnims[1];
			}
		}

		// Token: 0x040087F8 RID: 34808
		private VerticalWindTunnelWorkable workable;
	}
}
