using System;
using UnityEngine;

// Token: 0x020003C3 RID: 963
public class DeliverToSweepLockerStates : GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>
{
	// Token: 0x0600139B RID: 5019 RVA: 0x0006F950 File Offset: 0x0006DB50
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.movingToStorage;
		this.idle.ScheduleGoTo(1f, this.movingToStorage);
		this.movingToStorage.MoveTo(delegate(DeliverToSweepLockerStates.Instance smi)
		{
			if (!(this.GetSweepLocker(smi) == null))
			{
				return Grid.PosToCell(this.GetSweepLocker(smi));
			}
			return Grid.InvalidCell;
		}, this.unloading, this.idle, false);
		this.unloading.Enter(delegate(DeliverToSweepLockerStates.Instance smi)
		{
			Storage sweepLocker = this.GetSweepLocker(smi);
			if (sweepLocker == null)
			{
				smi.GoTo(this.behaviourcomplete);
				return;
			}
			Storage storage = smi.master.gameObject.GetComponents<Storage>()[1];
			float num = Mathf.Max(0f, Mathf.Min(storage.ExactMassStored(), sweepLocker.RemainingCapacity()));
			for (int i = storage.items.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = storage.items[i];
				if (!(gameObject == null))
				{
					float num2 = Mathf.Min(gameObject.GetComponent<PrimaryElement>().Mass, num);
					if (num2 != 0f)
					{
						storage.Transfer(sweepLocker, gameObject.GetComponent<KPrefabID>().PrefabTag, num2, false, false);
					}
					num -= num2;
					if (num <= 0f)
					{
						break;
					}
				}
			}
			smi.master.GetComponent<KBatchedAnimController>().Play("dropoff", KAnim.PlayMode.Once, 1f, 0f);
			smi.master.GetComponent<KBatchedAnimController>().FlipX = false;
			sweepLocker.GetComponent<KBatchedAnimController>().Play("dropoff", KAnim.PlayMode.Once, 1f, 0f);
			if (storage.MassStored() > 0f)
			{
				smi.ScheduleGoTo(2f, this.lockerFull);
				return;
			}
			smi.ScheduleGoTo(2f, this.behaviourcomplete);
		});
		this.lockerFull.PlayAnim("react_bored", KAnim.PlayMode.Once).OnAnimQueueComplete(this.movingToStorage);
		this.behaviourcomplete.BehaviourComplete(GameTags.Robots.Behaviours.UnloadBehaviour, false);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x0006F9E8 File Offset: 0x0006DBE8
	public Storage GetSweepLocker(DeliverToSweepLockerStates.Instance smi)
	{
		StorageUnloadMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
		if (smi2 == null)
		{
			return null;
		}
		return smi2.sm.sweepLocker.Get(smi2);
	}

	// Token: 0x04000BDB RID: 3035
	public GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.State idle;

	// Token: 0x04000BDC RID: 3036
	public GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.State movingToStorage;

	// Token: 0x04000BDD RID: 3037
	public GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.State unloading;

	// Token: 0x04000BDE RID: 3038
	public GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.State lockerFull;

	// Token: 0x04000BDF RID: 3039
	public GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.State behaviourcomplete;

	// Token: 0x020011FB RID: 4603
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020011FC RID: 4604
	public new class Instance : GameStateMachine<DeliverToSweepLockerStates, DeliverToSweepLockerStates.Instance, IStateMachineTarget, DeliverToSweepLockerStates.Def>.GameInstance
	{
		// Token: 0x06008494 RID: 33940 RVA: 0x003362FF File Offset: 0x003344FF
		public Instance(Chore<DeliverToSweepLockerStates.Instance> chore, DeliverToSweepLockerStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Robots.Behaviours.UnloadBehaviour);
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x00336323 File Offset: 0x00334523
		public override void StartSM()
		{
			base.StartSM();
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().RobotStatusItems.UnloadingStorage, base.gameObject);
		}

		// Token: 0x06008496 RID: 33942 RVA: 0x0033635B File Offset: 0x0033455B
		protected override void OnCleanUp()
		{
			base.OnCleanUp();
			base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().RobotStatusItems.UnloadingStorage, false);
		}
	}
}
