using System;
using UnityEngine;

// Token: 0x0200058D RID: 1421
public class ClimbableTreeMonitor : GameStateMachine<ClimbableTreeMonitor, ClimbableTreeMonitor.Instance, IStateMachineTarget, ClimbableTreeMonitor.Def>
{
	// Token: 0x06002073 RID: 8307 RVA: 0x000BB4D0 File Offset: 0x000B96D0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToClimbTree, (ClimbableTreeMonitor.Instance smi) => smi.UpdateHasClimbable(), delegate(ClimbableTreeMonitor.Instance smi)
		{
			smi.OnClimbComplete();
		});
	}

	// Token: 0x040012E8 RID: 4840
	private const int MAX_NAV_COST = 2147483647;

	// Token: 0x020013E4 RID: 5092
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006AFE RID: 27390
		public float searchMinInterval = 60f;

		// Token: 0x04006AFF RID: 27391
		public float searchMaxInterval = 120f;
	}

	// Token: 0x020013E5 RID: 5093
	public new class Instance : GameStateMachine<ClimbableTreeMonitor, ClimbableTreeMonitor.Instance, IStateMachineTarget, ClimbableTreeMonitor.Def>.GameInstance
	{
		// Token: 0x06008BBE RID: 35774 RVA: 0x003539B2 File Offset: 0x00351BB2
		public Instance(IStateMachineTarget master, ClimbableTreeMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x003539C2 File Offset: 0x00351BC2
		private void RefreshSearchTime()
		{
			this.nextSearchTime = Time.time + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, global::UnityEngine.Random.value);
		}

		// Token: 0x06008BC0 RID: 35776 RVA: 0x003539F0 File Offset: 0x00351BF0
		public bool UpdateHasClimbable()
		{
			if (this.climbTarget == null)
			{
				if (Time.time < this.nextSearchTime)
				{
					return false;
				}
				this.FindClimbableTree();
				this.RefreshSearchTime();
			}
			return this.climbTarget != null;
		}

		// Token: 0x06008BC1 RID: 35777 RVA: 0x00353A28 File Offset: 0x00351C28
		private static bool FindClimbableTreeVisitor(object obj, ClimbableTreeMonitor.Instance.FindClimableTreeContext context)
		{
			KMonoBehaviour kmonoBehaviour = obj as KMonoBehaviour;
			if (kmonoBehaviour.HasTag(GameTags.Creatures.ReservedByCreature))
			{
				return true;
			}
			int num = Grid.PosToCell(kmonoBehaviour);
			if (!context.navigator.CanReach(num))
			{
				return true;
			}
			ForestTreeSeedMonitor component = kmonoBehaviour.GetComponent<ForestTreeSeedMonitor>();
			StorageLocker component2 = kmonoBehaviour.GetComponent<StorageLocker>();
			if (component != null)
			{
				if (!component.ExtraSeedAvailable)
				{
					return true;
				}
			}
			else
			{
				if (!(component2 != null))
				{
					return true;
				}
				Storage component3 = component2.GetComponent<Storage>();
				if (!component3.allowItemRemoval)
				{
					return true;
				}
				if (component3.IsEmpty())
				{
					return true;
				}
			}
			context.targets.Add(kmonoBehaviour);
			return true;
		}

		// Token: 0x06008BC2 RID: 35778 RVA: 0x00353ABC File Offset: 0x00351CBC
		private void FindClimbableTree()
		{
			this.climbTarget = null;
			Vector3 position = base.master.transform.GetPosition();
			Extents extents = new Extents(Grid.PosToCell(position), 10);
			ClimbableTreeMonitor.Instance.FindClimableTreeContext findClimableTreeContext;
			findClimableTreeContext.navigator = base.GetComponent<Navigator>();
			findClimableTreeContext.targets = ListPool<KMonoBehaviour, ClimbableTreeMonitor>.Allocate();
			GameScenePartitioner.Instance.AsyncSafeVisit<ClimbableTreeMonitor.Instance.FindClimableTreeContext>(extents.x, extents.y, extents.width, extents.height, GameScenePartitioner.Instance.plants, new Func<object, ClimbableTreeMonitor.Instance.FindClimableTreeContext, bool>(ClimbableTreeMonitor.Instance.FindClimbableTreeVisitor), findClimableTreeContext);
			GameScenePartitioner.Instance.AsyncSafeVisit<ClimbableTreeMonitor.Instance.FindClimableTreeContext>(extents.x, extents.y, extents.width, extents.height, GameScenePartitioner.Instance.completeBuildings, new Func<object, ClimbableTreeMonitor.Instance.FindClimableTreeContext, bool>(ClimbableTreeMonitor.Instance.FindClimbableTreeVisitor), findClimableTreeContext);
			if (findClimableTreeContext.targets.Count > 0)
			{
				int num = global::UnityEngine.Random.Range(0, findClimableTreeContext.targets.Count);
				KMonoBehaviour kmonoBehaviour = findClimableTreeContext.targets[num];
				this.climbTarget = kmonoBehaviour.gameObject;
			}
			findClimableTreeContext.targets.Recycle();
		}

		// Token: 0x06008BC3 RID: 35779 RVA: 0x00353BC1 File Offset: 0x00351DC1
		public void OnClimbComplete()
		{
			this.climbTarget = null;
		}

		// Token: 0x04006B00 RID: 27392
		public GameObject climbTarget;

		// Token: 0x04006B01 RID: 27393
		public float nextSearchTime;

		// Token: 0x02002730 RID: 10032
		private struct FindClimableTreeContext
		{
			// Token: 0x0400AD14 RID: 44308
			public Navigator navigator;

			// Token: 0x0400AD15 RID: 44309
			public ListPool<KMonoBehaviour, ClimbableTreeMonitor>.PooledList targets;
		}
	}
}
