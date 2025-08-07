using System;
using UnityEngine;

// Token: 0x02000473 RID: 1139
public class BionicBedTimeModeChore : Chore<BionicBedTimeModeChore.Instance>
{
	// Token: 0x060017F4 RID: 6132 RVA: 0x00084B48 File Offset: 0x00082D48
	public BionicBedTimeModeChore(IStateMachineTarget master)
		: base(Db.Get().ChoreTypes.BionicBedtimeMode, master, master.GetComponent<ChoreProvider>(), true, null, null, null, PriorityScreen.PriorityClass.personalNeeds, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BionicBedTimeModeChore.Instance(this, master.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x00084BA0 File Offset: 0x00082DA0
	public static void BeginWorkOnZone(BionicBedTimeModeChore.Instance smi)
	{
		WorkerBase workerBase = smi.sm.bionic.Get<WorkerBase>(smi);
		DefragmentationZone assignedDefragmentationZone = smi.GetAssignedDefragmentationZone();
		workerBase.StartWork(new WorkerBase.StartWorkInfo(assignedDefragmentationZone));
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00084BD0 File Offset: 0x00082DD0
	public static bool HasDefragmentationZoneAssignedAndReachable(BionicBedTimeModeChore.Instance smi, GameObject defragmentationZone)
	{
		return defragmentationZone != null && smi.IsDefragmentationZoneReachable();
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x00084BE3 File Offset: 0x00082DE3
	public static bool HasDefragmentationZoneAssignedAndReachable(BionicBedTimeModeChore.Instance smi)
	{
		return smi.sm.defragmentationZone.Get(smi) != null && smi.IsDefragmentationZoneReachable();
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x00084C06 File Offset: 0x00082E06
	public static bool IsBedTimeAllowed(BionicBedTimeModeChore.Instance smi)
	{
		return BionicBedTimeMonitor.CanGoToBedTime(smi.bedTimeMonitor);
	}

	// Token: 0x060017F9 RID: 6137 RVA: 0x00084C13 File Offset: 0x00082E13
	public static void UpdateAssignedDefragmentationZone(BionicBedTimeModeChore.Instance smi)
	{
		smi.UpdateAssignedDefragmentationZone(null);
	}

	// Token: 0x04000DCE RID: 3534
	public const string EFFECT_NAME = "BionicBedTimeEffect";

	// Token: 0x02001265 RID: 4709
	public class States : GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore>
	{
		// Token: 0x06008624 RID: 34340 RVA: 0x0033B8C0 File Offset: 0x00339AC0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.enter;
			this.root.ToggleEffect("BionicBedTimeEffect");
			this.enter.Transition(null, GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Not(new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Transition.ConditionCallback(BionicBedTimeModeChore.IsBedTimeAllowed)), UpdateRate.SIM_200ms).ParamTransition<GameObject>(this.defragmentationZone, this.approach, new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Parameter<GameObject>.Callback(BionicBedTimeModeChore.HasDefragmentationZoneAssignedAndReachable)).GoTo(this.defragmentingWithoutAssignable);
			this.unassigning.ScheduleActionNextFrame("Frame delay on unassign", delegate(BionicBedTimeModeChore.Instance smi)
			{
				BionicBedTimeModeChore.UpdateAssignedDefragmentationZone(smi);
				smi.GoTo(this.enter);
			});
			this.approach.InitializeStates(this.bionic, this.defragmentationZone, this.defragmentingOnAssignable, null, null, null).OnSignal(this.defragmentationZoneUnassignined, this.unassigning).ScheduleChange(null, GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Not(new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Transition.ConditionCallback(BionicBedTimeModeChore.IsBedTimeAllowed)))
				.EventTransition(GameHashes.BionicOffline, null, null);
			this.defragmentingOnAssignable.OnTargetLost(this.defragmentationZone, this.defragmentingWithoutAssignable).OnSignal(this.defragmentationZoneChangedSignal, this.enter).OnSignal(this.defragmentationZoneUnassignined, this.unassigning)
				.EventTransition(GameHashes.BionicOffline, null, null)
				.ScheduleChange(null, GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Not(new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Transition.ConditionCallback(BionicBedTimeModeChore.IsBedTimeAllowed)))
				.ToggleWork("Defragmenting", new Action<BionicBedTimeModeChore.Instance>(BionicBedTimeModeChore.BeginWorkOnZone), (BionicBedTimeModeChore.Instance smi) => smi.GetAssignedDefragmentationZone() != null, this.end, null)
				.ToggleTag(GameTags.BionicBedTime);
			this.defragmentingWithoutAssignable.ParamTransition<GameObject>(this.defragmentationZone, this.approach, new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Parameter<GameObject>.Callback(BionicBedTimeModeChore.HasDefragmentationZoneAssignedAndReachable)).EventTransition(GameHashes.AssignableReachabilityChanged, this.approach, new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Transition.ConditionCallback(BionicBedTimeModeChore.HasDefragmentationZoneAssignedAndReachable)).ToggleAnims("anim_bionic_kanim", 0f)
				.ToggleTag(GameTags.BionicBedTime)
				.DefaultState(this.defragmentingWithoutAssignable.pre);
			this.defragmentingWithoutAssignable.pre.PlayAnim("low_power_pre").OnAnimQueueComplete(this.defragmentingWithoutAssignable.loop).ScheduleGoTo(1.5f, this.defragmentingWithoutAssignable.loop);
			this.defragmentingWithoutAssignable.loop.ScheduleChange(this.defragmentingWithoutAssignable.pst, GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Not(new StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Transition.ConditionCallback(BionicBedTimeModeChore.IsBedTimeAllowed))).EventTransition(GameHashes.BionicOffline, this.defragmentingWithoutAssignable.pst, null).PlayAnim("low_power_loop", KAnim.PlayMode.Loop);
			this.defragmentingWithoutAssignable.pst.PlayAnim("low_power_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.end);
			this.end.ReturnSuccess();
		}

		// Token: 0x040065EC RID: 26092
		public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.ApproachSubState<IApproachable> approach;

		// Token: 0x040065ED RID: 26093
		public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State defragmentingOnAssignable;

		// Token: 0x040065EE RID: 26094
		public BionicBedTimeModeChore.States.DefragmentingStates defragmentingWithoutAssignable;

		// Token: 0x040065EF RID: 26095
		public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State enter;

		// Token: 0x040065F0 RID: 26096
		public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State end;

		// Token: 0x040065F1 RID: 26097
		public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State unassigning;

		// Token: 0x040065F2 RID: 26098
		public StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.TargetParameter bionic;

		// Token: 0x040065F3 RID: 26099
		public StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.TargetParameter defragmentationZone;

		// Token: 0x040065F4 RID: 26100
		public StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Signal defragmentationZoneChangedSignal;

		// Token: 0x040065F5 RID: 26101
		public StateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.Signal defragmentationZoneUnassignined;

		// Token: 0x02002637 RID: 9783
		public class DefragmentingStates : GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State
		{
			// Token: 0x0400A9F9 RID: 43513
			public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State pre;

			// Token: 0x0400A9FA RID: 43514
			public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State loop;

			// Token: 0x0400A9FB RID: 43515
			public GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.State pst;
		}
	}

	// Token: 0x02001266 RID: 4710
	public class Instance : GameStateMachine<BionicBedTimeModeChore.States, BionicBedTimeModeChore.Instance, BionicBedTimeModeChore, object>.GameInstance
	{
		// Token: 0x06008627 RID: 34343 RVA: 0x0033BB7E File Offset: 0x00339D7E
		public DefragmentationZone GetAssignedDefragmentationZone()
		{
			return this.lastAssignedDefragmentationZone;
		}

		// Token: 0x06008628 RID: 34344 RVA: 0x0033BB88 File Offset: 0x00339D88
		public Instance(BionicBedTimeModeChore master, GameObject duplicant)
			: base(master)
		{
			this.bedTimeMonitor = duplicant.GetSMI<BionicBedTimeMonitor.Instance>();
			base.sm.bionic.Set(duplicant, this, false);
			this.ownables = base.GetComponent<MinionIdentity>().GetSoleOwner();
			base.gameObject.Subscribe(-1585839766, new Action<object>(this.UpdateAssignedDefragmentationZone));
			this.UpdateAssignedDefragmentationZone(null);
		}

		// Token: 0x06008629 RID: 34345 RVA: 0x0033BBF1 File Offset: 0x00339DF1
		protected override void OnCleanUp()
		{
			base.gameObject.Unsubscribe(-1585839766, new Action<object>(this.UpdateAssignedDefragmentationZone));
			base.OnCleanUp();
		}

		// Token: 0x0600862A RID: 34346 RVA: 0x0033BC15 File Offset: 0x00339E15
		public override void StartSM()
		{
			this.UpdateAssignedDefragmentationZone(null);
			base.StartSM();
		}

		// Token: 0x0600862B RID: 34347 RVA: 0x0033BC24 File Offset: 0x00339E24
		public bool IsDefragmentationZoneReachable()
		{
			return base.GetComponent<Sensors>().GetSensor<AssignableReachabilitySensor>().IsReachable(Db.Get().AssignableSlots.Bed);
		}

		// Token: 0x0600862C RID: 34348 RVA: 0x0033BC48 File Offset: 0x00339E48
		public void UpdateAssignedDefragmentationZone(object slotInstanceObject)
		{
			DefragmentationZone defragmentationZone = null;
			AssignableSlotInstance assignableSlotInstance = ((slotInstanceObject == null) ? null : ((AssignableSlotInstance)slotInstanceObject));
			Assignable assignable = this.ownables.GetAssignable(Db.Get().AssignableSlots.Bed);
			if (assignableSlotInstance != null && assignableSlotInstance.IsUnassigning())
			{
				base.sm.defragmentationZoneUnassignined.Trigger(this);
				return;
			}
			if (assignable == null)
			{
				assignable = this.ownables.AutoAssignSlot(Db.Get().AssignableSlots.Bed);
			}
			if (assignable != null)
			{
				defragmentationZone = assignable.GetComponent<DefragmentationZone>();
			}
			if (this.lastAssignedDefragmentationZone != defragmentationZone)
			{
				AssignableReachabilitySensor sensor = base.GetComponent<Sensors>().GetSensor<AssignableReachabilitySensor>();
				if (sensor.IsEnabled)
				{
					sensor.Update();
				}
				this.lastAssignedDefragmentationZone = defragmentationZone;
				base.sm.defragmentationZone.Set(this.lastAssignedDefragmentationZone, this);
				base.sm.defragmentationZoneChangedSignal.Trigger(this);
			}
		}

		// Token: 0x040065F6 RID: 26102
		public BionicBedTimeMonitor.Instance bedTimeMonitor;

		// Token: 0x040065F7 RID: 26103
		private DefragmentationZone lastAssignedDefragmentationZone;

		// Token: 0x040065F8 RID: 26104
		private Ownables ownables;
	}
}
