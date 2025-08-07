using System;
using Klei.AI;
using KSerialization;
using UnityEngine;

// Token: 0x02000877 RID: 2167
public class MoltDropperMonitor : GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>
{
	// Token: 0x06003BA3 RID: 15267 RVA: 0x0014ABA8 File Offset: 0x00148DA8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.root.EventHandler(GameHashes.NewDay, (MoltDropperMonitor.Instance smi) => GameClock.Instance, delegate(MoltDropperMonitor.Instance smi)
		{
			smi.spawnedThisCycle = false;
		});
		this.satisfied.UpdateTransition(this.drop, (MoltDropperMonitor.Instance smi, float dt) => smi.ShouldDropElement(), UpdateRate.SIM_4000ms, false);
		this.drop.DefaultState(this.drop.dropping);
		this.drop.dropping.EnterTransition(this.drop.complete, (MoltDropperMonitor.Instance smi) => !smi.def.synchWithBehaviour).ToggleBehaviour(GameTags.Creatures.ReadyToMolt, (MoltDropperMonitor.Instance smi) => true, delegate(MoltDropperMonitor.Instance smi)
		{
			smi.GoTo(this.drop.complete);
		});
		this.drop.complete.Enter(delegate(MoltDropperMonitor.Instance smi)
		{
			smi.Drop();
		}).TriggerOnEnter(GameHashes.Molt, null).EventTransition(GameHashes.NewDay, (MoltDropperMonitor.Instance smi) => GameClock.Instance, this.satisfied, null);
	}

	// Token: 0x04002494 RID: 9364
	public StateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.BoolParameter droppedThisCycle = new StateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.BoolParameter(false);

	// Token: 0x04002495 RID: 9365
	public GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State satisfied;

	// Token: 0x04002496 RID: 9366
	public MoltDropperMonitor.DropStates drop;

	// Token: 0x02001829 RID: 6185
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007813 RID: 30739
		public bool synchWithBehaviour;

		// Token: 0x04007814 RID: 30740
		public string onGrowDropID;

		// Token: 0x04007815 RID: 30741
		public float massToDrop;

		// Token: 0x04007816 RID: 30742
		public string amountName;

		// Token: 0x04007817 RID: 30743
		public Func<MoltDropperMonitor.Instance, bool> isReadyToMolt;
	}

	// Token: 0x0200182A RID: 6186
	public class DropStates : GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State
	{
		// Token: 0x04007818 RID: 30744
		public GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State dropping;

		// Token: 0x04007819 RID: 30745
		public GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.State complete;
	}

	// Token: 0x0200182B RID: 6187
	public new class Instance : GameStateMachine<MoltDropperMonitor, MoltDropperMonitor.Instance, IStateMachineTarget, MoltDropperMonitor.Def>.GameInstance
	{
		// Token: 0x06009BBF RID: 39871 RVA: 0x0038ED88 File Offset: 0x0038CF88
		public Instance(IStateMachineTarget master, MoltDropperMonitor.Def def)
			: base(master, def)
		{
			if (!string.IsNullOrEmpty(def.amountName))
			{
				AmountInstance amountInstance = Db.Get().Amounts.Get(def.amountName).Lookup(base.smi.gameObject);
				amountInstance.OnMaxValueReached = (global::System.Action)Delegate.Combine(amountInstance.OnMaxValueReached, new global::System.Action(this.OnAmountMaxValueReached));
			}
		}

		// Token: 0x06009BC0 RID: 39872 RVA: 0x0038EDF0 File Offset: 0x0038CFF0
		private void OnAmountMaxValueReached()
		{
			this.lastTineAmountReachedMax = GameClock.Instance.GetTime();
		}

		// Token: 0x06009BC1 RID: 39873 RVA: 0x0038EE04 File Offset: 0x0038D004
		protected override void OnCleanUp()
		{
			if (!string.IsNullOrEmpty(base.def.amountName))
			{
				AmountInstance amountInstance = Db.Get().Amounts.Get(base.def.amountName).Lookup(base.smi.gameObject);
				amountInstance.OnMaxValueReached = (global::System.Action)Delegate.Remove(amountInstance.OnMaxValueReached, new global::System.Action(this.OnAmountMaxValueReached));
			}
			base.OnCleanUp();
		}

		// Token: 0x06009BC2 RID: 39874 RVA: 0x0038EE74 File Offset: 0x0038D074
		public bool ShouldDropElement()
		{
			return base.def.isReadyToMolt(this);
		}

		// Token: 0x06009BC3 RID: 39875 RVA: 0x0038EE88 File Offset: 0x0038D088
		public void Drop()
		{
			GameObject gameObject = Scenario.SpawnPrefab(this.GetDropSpawnLocation(), 0, 0, base.def.onGrowDropID, Grid.SceneLayer.Ore);
			gameObject.SetActive(true);
			gameObject.GetComponent<PrimaryElement>().Mass = base.def.massToDrop;
			this.spawnedThisCycle = true;
			this.timeOfLastDrop = GameClock.Instance.GetTime();
			if (!string.IsNullOrEmpty(base.def.amountName))
			{
				AmountInstance amountInstance = Db.Get().Amounts.Get(base.def.amountName).Lookup(base.smi.gameObject);
				amountInstance.value = amountInstance.GetMin();
			}
		}

		// Token: 0x06009BC4 RID: 39876 RVA: 0x0038EF2C File Offset: 0x0038D12C
		private int GetDropSpawnLocation()
		{
			int num = Grid.PosToCell(base.gameObject);
			int num2 = Grid.CellAbove(num);
			if (Grid.IsValidCell(num2) && !Grid.Solid[num2])
			{
				return num2;
			}
			return num;
		}

		// Token: 0x0400781A RID: 30746
		[MyCmpGet]
		public KPrefabID prefabID;

		// Token: 0x0400781B RID: 30747
		[Serialize]
		public bool spawnedThisCycle;

		// Token: 0x0400781C RID: 30748
		[Serialize]
		public float timeOfLastDrop;

		// Token: 0x0400781D RID: 30749
		[Serialize]
		public float lastTineAmountReachedMax;
	}
}
