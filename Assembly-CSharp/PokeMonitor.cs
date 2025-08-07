using System;
using UnityEngine;

// Token: 0x0200059D RID: 1437
public class PokeMonitor : StateMachineComponent<PokeMonitor.Instance>
{
	// Token: 0x060020D2 RID: 8402 RVA: 0x000BD899 File Offset: 0x000BBA99
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000BD8AC File Offset: 0x000BBAAC
	private static void ClearTarget(PokeMonitor.Instance smi)
	{
		smi.AbortPoke();
	}

	// Token: 0x0200141C RID: 5148
	public class States : GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor>
	{
		// Token: 0x06008C87 RID: 35975 RVA: 0x0035624C File Offset: 0x0035444C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Never;
			default_state = this.noTarget;
			this.noTarget.ParamTransition<GameObject>(this.target, this.hasTarget, GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.IsNotNull);
			this.hasTarget.ParamTransition<GameObject>(this.target, this.noTarget, GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.IsNull).ToggleBehaviour(GameTags.Creatures.UrgeToPoke, (PokeMonitor.Instance smi) => true, new Action<PokeMonitor.Instance>(PokeMonitor.ClearTarget));
		}

		// Token: 0x04006BB4 RID: 27572
		public StateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.TargetParameter target;

		// Token: 0x04006BB5 RID: 27573
		public GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.State noTarget;

		// Token: 0x04006BB6 RID: 27574
		public GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.State hasTarget;
	}

	// Token: 0x0200141D RID: 5149
	public class Instance : GameStateMachine<PokeMonitor.States, PokeMonitor.Instance, PokeMonitor, object>.GameInstance
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06008C89 RID: 35977 RVA: 0x003562DF File Offset: 0x003544DF
		public GameObject Target
		{
			get
			{
				return base.sm.target.Get(this);
			}
		}

		// Token: 0x06008C8A RID: 35978 RVA: 0x003562F2 File Offset: 0x003544F2
		public Instance(PokeMonitor master)
			: base(master)
		{
		}

		// Token: 0x06008C8B RID: 35979 RVA: 0x00356315 File Offset: 0x00354515
		public void InitiatePoke(GameObject target)
		{
			this.InitiatePoke(target, new CellOffset[]
			{
				new CellOffset(0, 0)
			});
		}

		// Token: 0x06008C8C RID: 35980 RVA: 0x00356332 File Offset: 0x00354532
		public void InitiatePoke(GameObject target, CellOffset[] pokeOffesets)
		{
			base.sm.target.Set(target, this, false);
			this.TargetOffsets = pokeOffesets;
		}

		// Token: 0x06008C8D RID: 35981 RVA: 0x0035634F File Offset: 0x0035454F
		public void AbortPoke()
		{
			base.sm.target.Set(null, this);
			this.TargetOffsets = new CellOffset[]
			{
				new CellOffset(0, 0)
			};
		}

		// Token: 0x04006BB7 RID: 27575
		public CellOffset[] TargetOffsets = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
	}
}
