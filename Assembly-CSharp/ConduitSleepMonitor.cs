using System;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class ConduitSleepMonitor : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>
{
	// Token: 0x060003EA RID: 1002 RVA: 0x00021194 File Offset: 0x0001F394
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.idle.Enter(delegate(ConduitSleepMonitor.Instance smi)
		{
			this.targetSleepCell.Set(Grid.InvalidCell, smi, false);
			smi.GetComponent<Staterpillar>().DestroyOrphanedConnectorBuilding();
		}).EventTransition(GameHashes.NewBlock, (ConduitSleepMonitor.Instance smi) => GameClock.Instance, this.searching.looking, new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Transition.ConditionCallback(ConduitSleepMonitor.IsSleepyTime));
		this.searching.Enter(new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State.Callback(this.TryRecoverSave)).EventTransition(GameHashes.NewBlock, (ConduitSleepMonitor.Instance smi) => GameClock.Instance, this.idle, GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Not(new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.Transition.ConditionCallback(ConduitSleepMonitor.IsSleepyTime))).Exit(delegate(ConduitSleepMonitor.Instance smi)
		{
			this.targetSleepCell.Set(Grid.InvalidCell, smi, false);
			smi.GetComponent<Staterpillar>().DestroyOrphanedConnectorBuilding();
		});
		this.searching.looking.Update(delegate(ConduitSleepMonitor.Instance smi, float dt)
		{
			this.FindSleepLocation(smi);
		}, UpdateRate.SIM_1000ms, false).ToggleStatusItem(Db.Get().CreatureStatusItems.NoSleepSpot, null).ParamTransition<int>(this.targetSleepCell, this.searching.found, (ConduitSleepMonitor.Instance smi, int sleepCell) => sleepCell != Grid.InvalidCell);
		this.searching.found.Enter(delegate(ConduitSleepMonitor.Instance smi)
		{
			smi.GetComponent<Staterpillar>().SpawnConnectorBuilding(this.targetSleepCell.Get(smi));
		}).ParamTransition<int>(this.targetSleepCell, this.searching.looking, (ConduitSleepMonitor.Instance smi, int sleepCell) => sleepCell == Grid.InvalidCell).ToggleBehaviour(GameTags.Creatures.WantsConduitConnection, (ConduitSleepMonitor.Instance smi) => this.targetSleepCell.Get(smi) != Grid.InvalidCell && ConduitSleepMonitor.IsSleepyTime(smi), null);
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x00021343 File Offset: 0x0001F543
	public static bool IsSleepyTime(ConduitSleepMonitor.Instance smi)
	{
		return GameClock.Instance.GetTimeSinceStartOfCycle() >= 500f;
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0002135C File Offset: 0x0001F55C
	private void TryRecoverSave(ConduitSleepMonitor.Instance smi)
	{
		Staterpillar component = smi.GetComponent<Staterpillar>();
		if (this.targetSleepCell.Get(smi) == Grid.InvalidCell && component.IsConnectorBuildingSpawned())
		{
			int num = Grid.PosToCell(component.GetConnectorBuilding());
			this.targetSleepCell.Set(num, smi, false);
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x000213A8 File Offset: 0x0001F5A8
	private void FindSleepLocation(ConduitSleepMonitor.Instance smi)
	{
		StaterpillarCellQuery staterpillarCellQuery = PathFinderQueries.staterpillarCellQuery.Reset(10, smi.gameObject, smi.def.conduitLayer);
		smi.GetComponent<Navigator>().RunQuery(staterpillarCellQuery);
		if (staterpillarCellQuery.result_cells.Count > 0)
		{
			foreach (int num in staterpillarCellQuery.result_cells)
			{
				int cellInDirection = Grid.GetCellInDirection(num, Direction.Down);
				if (Grid.Objects[cellInDirection, (int)smi.def.conduitLayer] != null)
				{
					this.targetSleepCell.Set(num, smi, false);
					break;
				}
			}
			if (this.targetSleepCell.Get(smi) == Grid.InvalidCell)
			{
				this.targetSleepCell.Set(staterpillarCellQuery.result_cells[global::UnityEngine.Random.Range(0, staterpillarCellQuery.result_cells.Count)], smi, false);
			}
		}
	}

	// Token: 0x040002EB RID: 747
	private GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State idle;

	// Token: 0x040002EC RID: 748
	private ConduitSleepMonitor.SleepSearchStates searching;

	// Token: 0x040002ED RID: 749
	public StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.IntParameter targetSleepCell = new StateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.IntParameter(Grid.InvalidCell);

	// Token: 0x020010B6 RID: 4278
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006130 RID: 24880
		public ObjectLayer conduitLayer;
	}

	// Token: 0x020010B7 RID: 4279
	private class SleepSearchStates : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State
	{
		// Token: 0x04006131 RID: 24881
		public GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State looking;

		// Token: 0x04006132 RID: 24882
		public GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.State found;
	}

	// Token: 0x020010B8 RID: 4280
	public new class Instance : GameStateMachine<ConduitSleepMonitor, ConduitSleepMonitor.Instance, IStateMachineTarget, ConduitSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06008094 RID: 32916 RVA: 0x0032DA4F File Offset: 0x0032BC4F
		public Instance(IStateMachineTarget master, ConduitSleepMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
