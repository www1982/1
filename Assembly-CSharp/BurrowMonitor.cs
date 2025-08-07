using System;
using UnityEngine;

// Token: 0x0200058B RID: 1419
public class BurrowMonitor : GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>
{
	// Token: 0x0600206D RID: 8301 RVA: 0x000BB280 File Offset: 0x000B9480
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.openair;
		this.openair.ToggleBehaviour(GameTags.Creatures.WantsToEnterBurrow, (BurrowMonitor.Instance smi) => smi.ShouldBurrow() && smi.timeinstate > smi.def.minimumAwakeTime, delegate(BurrowMonitor.Instance smi)
		{
			smi.BurrowComplete();
		}).Transition(this.entombed, (BurrowMonitor.Instance smi) => smi.IsEntombed() && !smi.HasTag(GameTags.Creatures.Bagged), UpdateRate.SIM_200ms).Enter("SetCollider", delegate(BurrowMonitor.Instance smi)
		{
			smi.SetCollider(true);
		});
		this.entombed.Enter("SetCollider", delegate(BurrowMonitor.Instance smi)
		{
			smi.SetCollider(false);
		}).Transition(this.openair, (BurrowMonitor.Instance smi) => !smi.IsEntombed(), UpdateRate.SIM_200ms).TagTransition(GameTags.Creatures.Bagged, this.openair, false)
			.ToggleBehaviour(GameTags.Creatures.Burrowed, (BurrowMonitor.Instance smi) => smi.IsEntombed(), delegate(BurrowMonitor.Instance smi)
			{
				smi.GoTo(this.openair);
			})
			.ToggleBehaviour(GameTags.Creatures.WantsToExitBurrow, (BurrowMonitor.Instance smi) => smi.EmergeIsClear() && GameClock.Instance.IsNighttime(), delegate(BurrowMonitor.Instance smi)
			{
				smi.ExitBurrowComplete();
			});
	}

	// Token: 0x040012E4 RID: 4836
	public GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.State openair;

	// Token: 0x040012E5 RID: 4837
	public GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.State entombed;

	// Token: 0x020013DE RID: 5086
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006AE8 RID: 27368
		public float burrowHardnessLimit = 20f;

		// Token: 0x04006AE9 RID: 27369
		public float minimumAwakeTime = 24f;

		// Token: 0x04006AEA RID: 27370
		public Vector2 moundColliderSize = new Vector2f(1f, 1.5f);

		// Token: 0x04006AEB RID: 27371
		public Vector2 moundColliderOffset = new Vector2(0f, -0.25f);
	}

	// Token: 0x020013DF RID: 5087
	public new class Instance : GameStateMachine<BurrowMonitor, BurrowMonitor.Instance, IStateMachineTarget, BurrowMonitor.Def>.GameInstance
	{
		// Token: 0x06008BA3 RID: 35747 RVA: 0x00353558 File Offset: 0x00351758
		public Instance(IStateMachineTarget master, BurrowMonitor.Def def)
			: base(master, def)
		{
			KBoxCollider2D component = master.GetComponent<KBoxCollider2D>();
			this.originalColliderSize = component.size;
			this.originalColliderOffset = component.offset;
		}

		// Token: 0x06008BA4 RID: 35748 RVA: 0x0035358C File Offset: 0x0035178C
		public bool EmergeIsClear()
		{
			int num = Grid.PosToCell(base.gameObject);
			if (!Grid.IsValidCell(num) || !Grid.IsValidCell(Grid.CellAbove(num)))
			{
				return false;
			}
			int num2 = Grid.CellAbove(num);
			return !Grid.Solid[num2] && !Grid.IsSubstantialLiquid(Grid.CellAbove(num), 0.9f);
		}

		// Token: 0x06008BA5 RID: 35749 RVA: 0x003535E7 File Offset: 0x003517E7
		public bool ShouldBurrow()
		{
			return !GameClock.Instance.IsNighttime() && this.CanBurrowInto(Grid.CellBelow(Grid.PosToCell(base.gameObject))) && !base.HasTag(GameTags.Creatures.Bagged);
		}

		// Token: 0x06008BA6 RID: 35750 RVA: 0x00353624 File Offset: 0x00351824
		public bool CanBurrowInto(int cell)
		{
			return Grid.IsValidCell(cell) && Grid.Solid[cell] && !Grid.IsSubstantialLiquid(Grid.CellAbove(cell), 0.35f) && !(Grid.Objects[cell, 1] != null) && (float)Grid.Element[cell].hardness <= base.def.burrowHardnessLimit && !Grid.Foundation[cell];
		}

		// Token: 0x06008BA7 RID: 35751 RVA: 0x003536A0 File Offset: 0x003518A0
		public bool IsEntombed()
		{
			int num = Grid.PosToCell(base.smi);
			return Grid.IsValidCell(num) && Grid.Solid[num];
		}

		// Token: 0x06008BA8 RID: 35752 RVA: 0x003536CE File Offset: 0x003518CE
		public void ExitBurrowComplete()
		{
			base.smi.GetComponent<KBatchedAnimController>().Play("idle_loop", KAnim.PlayMode.Once, 1f, 0f);
			this.GoTo(base.sm.openair);
		}

		// Token: 0x06008BA9 RID: 35753 RVA: 0x00353708 File Offset: 0x00351908
		public void BurrowComplete()
		{
			base.smi.transform.SetPosition(Grid.CellToPosCBC(Grid.CellBelow(Grid.PosToCell(base.transform.GetPosition())), Grid.SceneLayer.Creatures));
			base.smi.GetComponent<KBatchedAnimController>().Play("idle_mound", KAnim.PlayMode.Once, 1f, 0f);
			this.GoTo(base.sm.entombed);
		}

		// Token: 0x06008BAA RID: 35754 RVA: 0x00353778 File Offset: 0x00351978
		public void SetCollider(bool original_size)
		{
			KBoxCollider2D component = base.master.GetComponent<KBoxCollider2D>();
			AnimEventHandler component2 = base.master.GetComponent<AnimEventHandler>();
			if (original_size)
			{
				component.size = this.originalColliderSize;
				component.offset = this.originalColliderOffset;
				component2.baseOffset = this.originalColliderOffset;
				return;
			}
			component.size = base.def.moundColliderSize;
			component.offset = base.def.moundColliderOffset;
			component2.baseOffset = base.def.moundColliderOffset;
		}

		// Token: 0x04006AEC RID: 27372
		private Vector2 originalColliderSize;

		// Token: 0x04006AED RID: 27373
		private Vector2 originalColliderOffset;
	}
}
