using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007B1 RID: 1969
public class RanchStation : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>
{
	// Token: 0x06003450 RID: 13392 RVA: 0x00125520 File Offset: 0x00123720
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.Operational;
		this.Unoperational.TagTransition(GameTags.Operational, this.Operational, false);
		this.Operational.TagTransition(GameTags.Operational, this.Unoperational, true).ToggleChore((RanchStation.Instance smi) => smi.CreateChore(), new Action<RanchStation.Instance, Chore>(RanchStation.SetRemoteChore), this.Unoperational, this.Unoperational).Update("FindRanachable", delegate(RanchStation.Instance smi, float dt)
		{
			smi.FindRanchable(null);
		}, UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06003451 RID: 13393 RVA: 0x001255CC File Offset: 0x001237CC
	private static void SetRemoteChore(RanchStation.Instance smi, Chore chore)
	{
		smi.remoteChore.SetChore(chore);
	}

	// Token: 0x04001FA4 RID: 8100
	public StateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.BoolParameter RancherIsReady;

	// Token: 0x04001FA5 RID: 8101
	public GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.State Unoperational;

	// Token: 0x04001FA6 RID: 8102
	public RanchStation.OperationalState Operational;

	// Token: 0x020016CF RID: 5839
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040073D2 RID: 29650
		public Func<GameObject, RanchStation.Instance, bool> IsCritterEligibleToBeRanchedCb;

		// Token: 0x040073D3 RID: 29651
		public Action<GameObject, WorkerBase> OnRanchCompleteCb;

		// Token: 0x040073D4 RID: 29652
		public Action<GameObject, float, Workable> OnRanchWorkTick;

		// Token: 0x040073D5 RID: 29653
		public HashedString RanchedPreAnim = "idle_loop";

		// Token: 0x040073D6 RID: 29654
		public HashedString RanchedLoopAnim = "idle_loop";

		// Token: 0x040073D7 RID: 29655
		public HashedString RanchedPstAnim = "idle_loop";

		// Token: 0x040073D8 RID: 29656
		public HashedString RanchedAbortAnim = "idle_loop";

		// Token: 0x040073D9 RID: 29657
		public HashedString RancherInteractAnim = "anim_interacts_rancherstation_kanim";

		// Token: 0x040073DA RID: 29658
		public StatusItem RanchingStatusItem = Db.Get().DuplicantStatusItems.Ranching;

		// Token: 0x040073DB RID: 29659
		public StatusItem CreatureRanchingStatusItem = Db.Get().CreatureStatusItems.GettingRanched;

		// Token: 0x040073DC RID: 29660
		public float WorkTime = 12f;

		// Token: 0x040073DD RID: 29661
		public Func<RanchStation.Instance, int> GetTargetRanchCell = (RanchStation.Instance smi) => Grid.PosToCell(smi);
	}

	// Token: 0x020016D0 RID: 5840
	public class OperationalState : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.State
	{
	}

	// Token: 0x020016D1 RID: 5841
	public new class Instance : GameStateMachine<RanchStation, RanchStation.Instance, IStateMachineTarget, RanchStation.Def>.GameInstance
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060096A2 RID: 38562 RVA: 0x003791F1 File Offset: 0x003773F1
		public RanchedStates.Instance ActiveRanchable
		{
			get
			{
				return this.activeRanchable;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060096A3 RID: 38563 RVA: 0x003791F9 File Offset: 0x003773F9
		private bool isCritterAvailableForRanching
		{
			get
			{
				return this.targetRanchables.Count > 0;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060096A4 RID: 38564 RVA: 0x00379209 File Offset: 0x00377409
		public bool IsCritterAvailableForRanching
		{
			get
			{
				this.ValidateTargetRanchables();
				return this.isCritterAvailableForRanching;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060096A5 RID: 38565 RVA: 0x00379217 File Offset: 0x00377417
		public bool HasRancher
		{
			get
			{
				return this.rancher != null;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060096A6 RID: 38566 RVA: 0x00379225 File Offset: 0x00377425
		public bool IsRancherReady
		{
			get
			{
				return base.sm.RancherIsReady.Get(this);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060096A7 RID: 38567 RVA: 0x00379238 File Offset: 0x00377438
		public Extents StationExtents
		{
			get
			{
				return this.station.GetExtents();
			}
		}

		// Token: 0x060096A8 RID: 38568 RVA: 0x00379245 File Offset: 0x00377445
		public int GetRanchNavTarget()
		{
			return base.def.GetTargetRanchCell(this);
		}

		// Token: 0x060096A9 RID: 38569 RVA: 0x00379258 File Offset: 0x00377458
		public Instance(IStateMachineTarget master, RanchStation.Def def)
			: base(master, def)
		{
			base.gameObject.AddOrGet<RancherChore.RancherWorkable>();
			this.station = base.GetComponent<BuildingComplete>();
		}

		// Token: 0x060096AA RID: 38570 RVA: 0x00379288 File Offset: 0x00377488
		public Chore CreateChore()
		{
			RancherChore rancherChore = new RancherChore(base.GetComponent<KPrefabID>());
			StateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.TargetParameter targetParameter = rancherChore.smi.sm.rancher;
			StateMachine<RancherChore.RancherChoreStates, RancherChore.RancherChoreStates.Instance, IStateMachineTarget, object>.Parameter<GameObject>.Context context = targetParameter.GetContext(rancherChore.smi);
			context.onDirty = (Action<RancherChore.RancherChoreStates.Instance>)Delegate.Combine(context.onDirty, new Action<RancherChore.RancherChoreStates.Instance>(this.OnRancherChanged));
			this.rancher = targetParameter.Get<WorkerBase>(rancherChore.smi);
			return rancherChore;
		}

		// Token: 0x060096AB RID: 38571 RVA: 0x003792F2 File Offset: 0x003774F2
		public int GetTargetRanchCell()
		{
			return base.def.GetTargetRanchCell(this);
		}

		// Token: 0x060096AC RID: 38572 RVA: 0x00379308 File Offset: 0x00377508
		public override void StartSM()
		{
			base.StartSM();
			base.Subscribe(144050788, new Action<object>(this.OnRoomUpdated));
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(this.GetTargetRanchCell());
			if (cavityForCell != null && cavityForCell.room != null)
			{
				this.OnRoomUpdated(cavityForCell.room);
			}
		}

		// Token: 0x060096AD RID: 38573 RVA: 0x0037935F File Offset: 0x0037755F
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			base.Unsubscribe(144050788, new Action<object>(this.OnRoomUpdated));
		}

		// Token: 0x060096AE RID: 38574 RVA: 0x0037937F File Offset: 0x0037757F
		private void OnRoomUpdated(object data)
		{
			if (data == null)
			{
				return;
			}
			this.ranch = data as Room;
			if (this.ranch.roomType != Db.Get().RoomTypes.CreaturePen)
			{
				this.TriggerRanchStationNoLongerAvailable();
				this.ranch = null;
			}
		}

		// Token: 0x060096AF RID: 38575 RVA: 0x003793BA File Offset: 0x003775BA
		private void OnRancherChanged(RancherChore.RancherChoreStates.Instance choreInstance)
		{
			this.rancher = choreInstance.sm.rancher.Get<WorkerBase>(choreInstance);
			this.TriggerRanchStationNoLongerAvailable();
		}

		// Token: 0x060096B0 RID: 38576 RVA: 0x003793D9 File Offset: 0x003775D9
		public bool TryGetRanched(RanchedStates.Instance ranchable)
		{
			return this.activeRanchable == null || this.activeRanchable == ranchable;
		}

		// Token: 0x060096B1 RID: 38577 RVA: 0x003793EE File Offset: 0x003775EE
		public void MessageCreatureArrived(RanchedStates.Instance critter)
		{
			this.activeRanchable = critter;
			base.sm.RancherIsReady.Set(false, this, false);
			base.Trigger(-1357116271, null);
		}

		// Token: 0x060096B2 RID: 38578 RVA: 0x00379417 File Offset: 0x00377617
		public void MessageRancherReady()
		{
			base.sm.RancherIsReady.Set(true, base.smi, false);
			this.MessageRanchables(GameHashes.RancherReadyAtRanchStation);
		}

		// Token: 0x060096B3 RID: 38579 RVA: 0x00379440 File Offset: 0x00377640
		private bool CanRanchableBeRanchedAtRanchStation(RanchableMonitor.Instance ranchable)
		{
			bool flag = !ranchable.IsNullOrStopped();
			if (flag && ranchable.TargetRanchStation != null && ranchable.TargetRanchStation != this)
			{
				flag = !ranchable.TargetRanchStation.IsRunning() || !ranchable.TargetRanchStation.HasRancher;
			}
			flag = flag && base.def.IsCritterEligibleToBeRanchedCb(ranchable.gameObject, this);
			flag = flag && ranchable.ChoreConsumer.IsChoreEqualOrAboveCurrentChorePriority<RanchedStates>();
			if (flag)
			{
				int num = Grid.PosToCell(ranchable.transform.GetPosition());
				CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
				if (cavityForCell == null || this.ranch == null || cavityForCell != this.ranch.cavity)
				{
					flag = false;
				}
				else
				{
					int num2 = this.GetRanchNavTarget();
					if (ranchable.HasTag(GameTags.Creatures.Flyer))
					{
						num2 = Grid.CellAbove(num2);
					}
					flag = ranchable.NavComponent.GetNavigationCost(num2) != -1;
				}
			}
			return flag;
		}

		// Token: 0x060096B4 RID: 38580 RVA: 0x0037952C File Offset: 0x0037772C
		public void ValidateTargetRanchables()
		{
			if (!this.HasRancher)
			{
				return;
			}
			foreach (RanchableMonitor.Instance instance in this.targetRanchables.ToArray())
			{
				if (instance.States == null || !this.CanRanchableBeRanchedAtRanchStation(instance))
				{
					this.Abandon(instance);
				}
			}
		}

		// Token: 0x060096B5 RID: 38581 RVA: 0x00379578 File Offset: 0x00377778
		public void FindRanchable(object _ = null)
		{
			if (this.ranch == null)
			{
				return;
			}
			this.ValidateTargetRanchables();
			if (this.targetRanchables.Count == 2)
			{
				return;
			}
			List<KPrefabID> creatures = this.ranch.cavity.creatures;
			if (this.HasRancher && !this.isCritterAvailableForRanching && creatures.Count == 0)
			{
				this.TryNotifyEmptyRanch();
			}
			for (int i = 0; i < creatures.Count; i++)
			{
				KPrefabID kprefabID = creatures[i];
				if (!(kprefabID == null))
				{
					RanchableMonitor.Instance smi = kprefabID.GetSMI<RanchableMonitor.Instance>();
					if (!this.targetRanchables.Contains(smi) && this.CanRanchableBeRanchedAtRanchStation(smi) && smi != null)
					{
						smi.States.SetRanchStation(this);
						this.targetRanchables.Add(smi);
						return;
					}
				}
			}
		}

		// Token: 0x060096B6 RID: 38582 RVA: 0x0037962E File Offset: 0x0037782E
		public Option<CavityInfo> GetCavityInfo()
		{
			if (this.ranch.IsNullOrDestroyed())
			{
				return Option.None;
			}
			return this.ranch.cavity;
		}

		// Token: 0x060096B7 RID: 38583 RVA: 0x00379658 File Offset: 0x00377858
		public void RanchCreature()
		{
			if (this.activeRanchable.IsNullOrStopped())
			{
				return;
			}
			global::Debug.Assert(this.activeRanchable != null, "targetRanchable was null");
			global::Debug.Assert(this.activeRanchable.GetMaster() != null, "GetMaster was null");
			global::Debug.Assert(base.def != null, "def was null");
			global::Debug.Assert(base.def.OnRanchCompleteCb != null, "onRanchCompleteCb cb was null");
			base.def.OnRanchCompleteCb(this.activeRanchable.gameObject, this.rancher);
			this.targetRanchables.Remove(this.activeRanchable.Monitor);
			this.activeRanchable.Trigger(1827504087, null);
			this.activeRanchable = null;
			this.FindRanchable(null);
		}

		// Token: 0x060096B8 RID: 38584 RVA: 0x00379720 File Offset: 0x00377920
		public void TriggerRanchStationNoLongerAvailable()
		{
			for (int i = this.targetRanchables.Count - 1; i >= 0; i--)
			{
				RanchableMonitor.Instance instance = this.targetRanchables[i];
				if (instance.IsNullOrStopped() || instance.States.IsNullOrStopped())
				{
					this.targetRanchables.RemoveAt(i);
				}
				else
				{
					this.targetRanchables.Remove(instance);
					instance.Trigger(1689625967, null);
				}
			}
			global::Debug.Assert(this.targetRanchables.Count == 0, "targetRanchables is not empty");
			this.activeRanchable = null;
			base.sm.RancherIsReady.Set(false, this, false);
		}

		// Token: 0x060096B9 RID: 38585 RVA: 0x003797C4 File Offset: 0x003779C4
		public void MessageRanchables(GameHashes hash)
		{
			for (int i = 0; i < this.targetRanchables.Count; i++)
			{
				RanchableMonitor.Instance instance = this.targetRanchables[i];
				if (!instance.IsNullOrStopped())
				{
					Game.BrainScheduler.PrioritizeBrain(instance.GetComponent<CreatureBrain>());
					if (!instance.States.IsNullOrStopped())
					{
						instance.Trigger((int)hash, null);
					}
				}
			}
		}

		// Token: 0x060096BA RID: 38586 RVA: 0x00379824 File Offset: 0x00377A24
		public void Abandon(RanchableMonitor.Instance critter)
		{
			if (critter == null)
			{
				global::Debug.LogWarning("Null critter trying to abandon ranch station");
				this.targetRanchables.Remove(critter);
				return;
			}
			critter.TargetRanchStation = null;
			if (this.targetRanchables.Remove(critter))
			{
				if (critter.States == null)
				{
					return;
				}
				bool flag = !this.isCritterAvailableForRanching;
				if (critter.States == this.activeRanchable)
				{
					flag = true;
					this.activeRanchable = null;
				}
				if (flag)
				{
					this.TryNotifyEmptyRanch();
				}
			}
		}

		// Token: 0x060096BB RID: 38587 RVA: 0x00379894 File Offset: 0x00377A94
		private void TryNotifyEmptyRanch()
		{
			if (!this.HasRancher)
			{
				return;
			}
			this.rancher.Trigger(-364750427, null);
		}

		// Token: 0x060096BC RID: 38588 RVA: 0x003798B0 File Offset: 0x00377AB0
		public bool IsCritterInQueue(RanchableMonitor.Instance critter)
		{
			return this.targetRanchables.Contains(critter);
		}

		// Token: 0x060096BD RID: 38589 RVA: 0x003798BE File Offset: 0x00377ABE
		public List<RanchableMonitor.Instance> DEBUG_GetTargetRanchables()
		{
			return this.targetRanchables;
		}

		// Token: 0x040073DE RID: 29662
		[MyCmpAdd]
		public ManuallySetRemoteWorkTargetComponent remoteChore;

		// Token: 0x040073DF RID: 29663
		private const int QUEUE_SIZE = 2;

		// Token: 0x040073E0 RID: 29664
		private List<RanchableMonitor.Instance> targetRanchables = new List<RanchableMonitor.Instance>();

		// Token: 0x040073E1 RID: 29665
		private RanchedStates.Instance activeRanchable;

		// Token: 0x040073E2 RID: 29666
		private Room ranch;

		// Token: 0x040073E3 RID: 29667
		private WorkerBase rancher;

		// Token: 0x040073E4 RID: 29668
		private BuildingComplete station;
	}
}
