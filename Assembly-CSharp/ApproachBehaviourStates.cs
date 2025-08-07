using System;
using UnityEngine;

// Token: 0x020000CF RID: 207
public class ApproachBehaviourStates : GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>
{
	// Token: 0x0600039A RID: 922 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.approach;
		this.root.Enter(new StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State.Callback(ApproachBehaviourStates.RefreshTarget)).Enter(new StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State.Callback(ApproachBehaviourStates.Reserve)).Exit(new StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State.Callback(ApproachBehaviourStates.Unreserve))
			.EventHandler(GameHashes.ApproachableTargetChanged, new StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State.Callback(ApproachBehaviourStates.RefreshTarget));
		this.approach.InitializeStates(this.self, this.target, (ApproachBehaviourStates.Instance smi) => smi.targetOffsets, this.interact, this.failure, null).ToggleMainStatusItem((ApproachBehaviourStates.Instance smi) => smi.GetMonitor().GetApproachStatusItem(), null);
		this.interact.Enter(delegate(ApproachBehaviourStates.Instance smi)
		{
			smi.GetMonitor().OnArrive();
		}).DefaultState(this.interact.pre).OnTargetLost(this.target, this.failure)
			.ToggleMainStatusItem((ApproachBehaviourStates.Instance smi) => smi.GetMonitor().GetBehaviourStatusItem(), null);
		this.interact.pre.PlayAnim((ApproachBehaviourStates.Instance smi) => smi.def.preAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.interact.loop);
		this.interact.loop.PlayAnim((ApproachBehaviourStates.Instance smi) => smi.def.loopAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.interact.pst);
		this.interact.pst.PlayAnim((ApproachBehaviourStates.Instance smi) => smi.def.pstAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.behaviourComplete);
		this.behaviourComplete.BehaviourComplete((ApproachBehaviourStates.Instance smi) => smi.def.behaviourTag, false).Exit(delegate(ApproachBehaviourStates.Instance smi)
		{
			smi.GetMonitor().OnSuccess();
		});
		this.failure.Enter(delegate(ApproachBehaviourStates.Instance smi)
		{
			smi.GetMonitor().OnFailure();
		}).GoTo(null);
	}

	// Token: 0x0600039B RID: 923 RVA: 0x0001ED26 File Offset: 0x0001CF26
	private static void Reserve(ApproachBehaviourStates.Instance smi)
	{
		if (smi.def.reserveTag != Tag.Invalid)
		{
			smi.sm.target.Get(smi).GetComponent<KPrefabID>().SetTag(smi.def.reserveTag, true);
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x0001ED68 File Offset: 0x0001CF68
	private static void Unreserve(ApproachBehaviourStates.Instance smi)
	{
		if (smi.def.reserveTag != Tag.Invalid && smi.sm.target.Get(smi) != null)
		{
			smi.sm.target.Get(smi).GetComponent<KPrefabID>().RemoveTag(smi.def.reserveTag);
		}
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001EDCC File Offset: 0x0001CFCC
	public static void RefreshTarget(ApproachBehaviourStates.Instance smi)
	{
		GameObject gameObject = smi.GetMonitor().GetTarget();
		if (gameObject == null)
		{
			smi.GoTo(smi.sm.failure);
			return;
		}
		smi.targetOffsets = smi.GetMonitor().GetApproachOffsets();
		smi.sm.target.Set(gameObject, smi, false);
	}

	// Token: 0x040002BD RID: 701
	public ApproachBehaviourStates.InteractState interact;

	// Token: 0x040002BE RID: 702
	public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State behaviourComplete;

	// Token: 0x040002BF RID: 703
	public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.ApproachSubState<IApproachable> approach;

	// Token: 0x040002C0 RID: 704
	public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State failure;

	// Token: 0x040002C1 RID: 705
	public StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.TargetParameter self;

	// Token: 0x040002C2 RID: 706
	public StateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.TargetParameter target;

	// Token: 0x02001080 RID: 4224
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x06008014 RID: 32788 RVA: 0x0032CD8C File Offset: 0x0032AF8C
		public Def(Tag monitorId, Tag behaviourTag)
		{
			this.monitorId = monitorId;
			this.behaviourTag = behaviourTag;
		}

		// Token: 0x040060A2 RID: 24738
		public Tag monitorId;

		// Token: 0x040060A3 RID: 24739
		public Tag behaviourTag;

		// Token: 0x040060A4 RID: 24740
		public Tag reserveTag = GameTags.Creatures.ReservedByCreature;

		// Token: 0x040060A5 RID: 24741
		public string preAnim = "";

		// Token: 0x040060A6 RID: 24742
		public string loopAnim = "";

		// Token: 0x040060A7 RID: 24743
		public string pstAnim = "";
	}

	// Token: 0x02001081 RID: 4225
	public class InteractState : GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State
	{
		// Token: 0x040060A8 RID: 24744
		public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State pre;

		// Token: 0x040060A9 RID: 24745
		public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State loop;

		// Token: 0x040060AA RID: 24746
		public GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.State pst;
	}

	// Token: 0x02001082 RID: 4226
	public new class Instance : GameStateMachine<ApproachBehaviourStates, ApproachBehaviourStates.Instance, IStateMachineTarget, ApproachBehaviourStates.Def>.GameInstance
	{
		// Token: 0x06008016 RID: 32790 RVA: 0x0032CDE4 File Offset: 0x0032AFE4
		public Instance(Chore<ApproachBehaviourStates.Instance> chore, ApproachBehaviourStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, def.behaviourTag);
			base.sm.self.Set(base.smi.gameObject, base.smi, false);
		}

		// Token: 0x06008017 RID: 32791 RVA: 0x0032CE37 File Offset: 0x0032B037
		public IApproachableBehaviour GetMonitor()
		{
			if (this.monitor.IsNullOrDestroyed())
			{
				this.SetMonitor();
			}
			return this.monitor;
		}

		// Token: 0x06008018 RID: 32792 RVA: 0x0032CE54 File Offset: 0x0032B054
		private void SetMonitor()
		{
			foreach (ICreatureMonitor creatureMonitor in base.gameObject.GetAllSMI<ICreatureMonitor>())
			{
				if (creatureMonitor.Id == base.def.monitorId)
				{
					this.monitor = creatureMonitor as IApproachableBehaviour;
					break;
				}
			}
			global::Debug.Assert(base.smi.monitor != null, "Could not find monitor with ID");
		}

		// Token: 0x040060AB RID: 24747
		private IApproachableBehaviour monitor;

		// Token: 0x040060AC RID: 24748
		public CellOffset[] targetOffsets;
	}
}
