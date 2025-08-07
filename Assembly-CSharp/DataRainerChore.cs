using System;
using TUNING;
using UnityEngine;

// Token: 0x02000479 RID: 1145
public class DataRainerChore : Chore<DataRainerChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x06001818 RID: 6168 RVA: 0x00086300 File Offset: 0x00084500
	public DataRainerChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.JoyReaction, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new DataRainerChore.StatesInstance(this, target.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		this.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x06001819 RID: 6169 RVA: 0x0008639A File Offset: 0x0008459A
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		return true;
	}

	// Token: 0x04000E14 RID: 3604
	private int basePriority = RELAXATION.PRIORITY.TIER1;

	// Token: 0x0200126D RID: 4717
	public class States : GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore>
	{
		// Token: 0x06008673 RID: 34419 RVA: 0x0033D75C File Offset: 0x0033B95C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.goToStand;
			base.Target(this.dataRainer);
			this.idle.EventTransition(GameHashes.ScheduleBlocksTick, this.goToStand, (DataRainerChore.StatesInstance smi) => !smi.IsRecTime());
			this.goToStand.MoveTo((DataRainerChore.StatesInstance smi) => smi.GetTargetCell(), this.raining, this.idle, false);
			this.raining.ToggleAnims("anim_bionic_joy_kanim", 0f).DefaultState(this.raining.loop).Update(delegate(DataRainerChore.StatesInstance smi, float dt)
			{
				this.nextBankTimer.Delta(dt, smi);
				if (this.nextBankTimer.Get(smi) >= DataRainer.databankSpawnInterval)
				{
					this.nextBankTimer.Delta(-DataRainer.databankSpawnInterval, smi);
					GameObject gameObject = Util.KInstantiate(Assets.GetPrefab("PowerStationTools"), smi.master.transform.position + Vector3.up);
					gameObject.GetComponent<PrimaryElement>().SetElement(SimHashes.Iron, true);
					gameObject.SetActive(true);
					KBatchedAnimController component = smi.master.GetComponent<KBatchedAnimController>();
					float num = (float)component.currentFrame / (float)component.GetCurrentNumFrames();
					Vector2 vector = new Vector2((num < 0.5f) ? (-2.5f) : 2.5f, 4f);
					if (GameComps.Fallers.Has(gameObject))
					{
						GameComps.Fallers.Remove(gameObject);
					}
					GameComps.Fallers.Add(gameObject, vector);
					DataRainer.Instance smi2 = this.dataRainer.Get(smi).GetSMI<DataRainer.Instance>();
					DataRainer sm = smi2.sm;
					sm.databanksCreated.Set(sm.databanksCreated.Get(smi2) + 1, smi2, false);
				}
			}, UpdateRate.SIM_33ms, false);
			this.raining.loop.PlayAnim("makeitrain2", KAnim.PlayMode.Loop);
		}

		// Token: 0x0400663F RID: 26175
		public StateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.TargetParameter dataRainer;

		// Token: 0x04006640 RID: 26176
		public StateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.FloatParameter nextBankTimer = new StateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.FloatParameter(DataRainer.databankSpawnInterval / 2f);

		// Token: 0x04006641 RID: 26177
		public GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State idle;

		// Token: 0x04006642 RID: 26178
		public GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State goToStand;

		// Token: 0x04006643 RID: 26179
		public DataRainerChore.States.RainingStates raining;

		// Token: 0x0200263F RID: 9791
		public class RainingStates : GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State
		{
			// Token: 0x0400AA11 RID: 43537
			public GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State pre;

			// Token: 0x0400AA12 RID: 43538
			public GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State loop;

			// Token: 0x0400AA13 RID: 43539
			public GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.State pst;
		}
	}

	// Token: 0x0200126E RID: 4718
	public class StatesInstance : GameStateMachine<DataRainerChore.States, DataRainerChore.StatesInstance, DataRainerChore, object>.GameInstance
	{
		// Token: 0x06008676 RID: 34422 RVA: 0x0033D984 File Offset: 0x0033BB84
		public StatesInstance(DataRainerChore master, GameObject dataRainer)
			: base(master)
		{
			this.dataRainer = dataRainer;
			base.sm.dataRainer.Set(dataRainer, base.smi, false);
		}

		// Token: 0x06008677 RID: 34423 RVA: 0x0033D9AD File Offset: 0x0033BBAD
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x06008678 RID: 34424 RVA: 0x0033D9D0 File Offset: 0x0033BBD0
		public int GetTargetCell()
		{
			Navigator component = base.GetComponent<Navigator>();
			float num = float.MaxValue;
			SocialGatheringPoint socialGatheringPoint = null;
			foreach (SocialGatheringPoint socialGatheringPoint2 in Components.SocialGatheringPoints.GetItems((int)Grid.WorldIdx[Grid.PosToCell(this)]))
			{
				float num2 = (float)component.GetNavigationCost(Grid.PosToCell(socialGatheringPoint2));
				if (num2 != -1f && num2 < num)
				{
					num = num2;
					socialGatheringPoint = socialGatheringPoint2;
				}
			}
			if (socialGatheringPoint != null)
			{
				return Grid.PosToCell(socialGatheringPoint);
			}
			return Grid.PosToCell(base.master.gameObject);
		}

		// Token: 0x04006644 RID: 26180
		private GameObject dataRainer;
	}
}
