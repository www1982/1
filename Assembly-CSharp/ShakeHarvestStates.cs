using System;
using STRINGS;
using UnityEngine;

// Token: 0x020005A3 RID: 1443
public class ShakeHarvestStates : GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>
{
	// Token: 0x060020EE RID: 8430 RVA: 0x000BDD47 File Offset: 0x000BBF47
	private static StatusItem GoingToHarvestStatus(ShakeHarvestStates.Instance smi)
	{
		return ShakeHarvestStates.MakeStatus(smi, CREATURES.STATUSITEMS.GOING_TO_HARVEST.NAME, CREATURES.STATUSITEMS.GOING_TO_HARVEST.TOOLTIP);
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x000BDD63 File Offset: 0x000BBF63
	private static StatusItem HarvestingStatus(ShakeHarvestStates.Instance smi)
	{
		return ShakeHarvestStates.MakeStatus(smi, CREATURES.STATUSITEMS.HARVESTING.NAME, CREATURES.STATUSITEMS.HARVESTING.TOOLTIP);
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000BDD80 File Offset: 0x000BBF80
	private static StatusItem MakeStatus(ShakeHarvestStates.Instance smi, string name, string tooltip)
	{
		return new StatusItem(smi.GetCurrentState().longName, name, tooltip, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, true, null);
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000BDDB8 File Offset: 0x000BBFB8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Never;
		default_state = this.approach;
		this.root.Enter(delegate(ShakeHarvestStates.Instance smi)
		{
			ShakeHarvestMonitor.Instance smi2 = smi.GetSMI<ShakeHarvestMonitor.Instance>();
			this.plant.Set(smi2.sm.plant.Get(smi2), smi, false);
		});
		this.approach.InitializeStates(this.harvester, this.plant, delegate(ShakeHarvestStates.Instance smi)
		{
			ListPool<CellOffset, ShakeHarvestStates>.PooledList pooledList = ListPool<CellOffset, ShakeHarvestStates>.Allocate();
			ShakeHarvestMonitor.Def.GetApproachOffsets(this.plant.Get(smi), pooledList);
			CellOffset[] array = pooledList.ToArray();
			pooledList.Recycle();
			return array;
		}, this.harvest, this.failed, null).ToggleMainStatusItem(new Func<ShakeHarvestStates.Instance, StatusItem>(ShakeHarvestStates.GoingToHarvestStatus), null).OnTargetLost(this.plant, this.failed)
			.Target(this.plant)
			.EventTransition(GameHashes.Harvest, this.failed, null)
			.EventTransition(GameHashes.Uprooted, this.failed, null)
			.EventTransition(GameHashes.QueueDestroyObject, this.failed, null);
		this.harvest.PlayAnim("shake", KAnim.PlayMode.Once).ToggleMainStatusItem(new Func<ShakeHarvestStates.Instance, StatusItem>(ShakeHarvestStates.HarvestingStatus), null).OnAnimQueueComplete(this.complete)
			.OnTargetLost(this.plant, this.failed);
		this.complete.Enter(delegate(ShakeHarvestStates.Instance smi)
		{
			GameObject gameObject = this.plant.Get(smi);
			if (gameObject.IsNullOrDestroyed())
			{
				return;
			}
			Harvestable component = gameObject.GetComponent<Harvestable>();
			if (component != null && component.CanBeHarvested)
			{
				component.Trigger(2127324410, true);
				component.Harvest();
			}
		}).BehaviourComplete(GameTags.Creatures.WantsToHarvest, false);
		this.failed.Enter(delegate(ShakeHarvestStates.Instance smi)
		{
			ShakeHarvestMonitor.Instance smi3 = smi.GetSMI<ShakeHarvestMonitor.Instance>();
			if (smi3 != null)
			{
				smi3.sm.failed.Trigger(smi3);
			}
		}).EnterGoTo(null);
	}

	// Token: 0x04001329 RID: 4905
	private readonly GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.ApproachSubState<IApproachable> approach;

	// Token: 0x0400132A RID: 4906
	private readonly GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.State harvest;

	// Token: 0x0400132B RID: 4907
	private readonly GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.State complete;

	// Token: 0x0400132C RID: 4908
	private readonly GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.State failed;

	// Token: 0x0400132D RID: 4909
	private readonly StateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.TargetParameter harvester;

	// Token: 0x0400132E RID: 4910
	private readonly StateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.TargetParameter plant;

	// Token: 0x0200142C RID: 5164
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200142D RID: 5165
	public new class Instance : GameStateMachine<ShakeHarvestStates, ShakeHarvestStates.Instance, IStateMachineTarget, ShakeHarvestStates.Def>.GameInstance
	{
		// Token: 0x06008CBA RID: 36026 RVA: 0x00356AD8 File Offset: 0x00354CD8
		public Instance(Chore<ShakeHarvestStates.Instance> chore, ShakeHarvestStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToHarvest);
			base.sm.harvester.Set(base.gameObject, this, false);
		}
	}
}
