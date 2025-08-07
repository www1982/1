using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000ADD RID: 2781
[AddComponentMenu("KMonoBehaviour/Workable/RoleStation")]
public class RoleStation : Workable, IGameObjectEffectDescriptor
{
	// Token: 0x060050D2 RID: 20690 RVA: 0x001D5784 File Offset: 0x001D3984
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = true;
		this.UpdateStatusItemDelegate = new Action<object>(this.UpdateSkillPointAvailableStatusItem);
	}

	// Token: 0x060050D3 RID: 20691 RVA: 0x001D57A8 File Offset: 0x001D39A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.RoleStations.Add(this);
		this.smi = new RoleStation.RoleStationSM.Instance(this);
		this.smi.StartSM();
		base.SetWorkTime(7.53f);
		this.resetProgressOnStop = true;
		this.subscriptions.Add(Game.Instance.Subscribe(-1523247426, this.UpdateStatusItemDelegate));
		this.subscriptions.Add(Game.Instance.Subscribe(1505456302, this.UpdateStatusItemDelegate));
		this.UpdateSkillPointAvailableStatusItem(null);
	}

	// Token: 0x060050D4 RID: 20692 RVA: 0x001D5838 File Offset: 0x001D3A38
	protected override void OnStopWork(WorkerBase worker)
	{
		Telepad.StatesInstance statesInstance = this.GetSMI<Telepad.StatesInstance>();
		statesInstance.sm.idlePortal.Trigger(statesInstance);
	}

	// Token: 0x060050D5 RID: 20693 RVA: 0x001D5860 File Offset: 0x001D3A60
	private void UpdateSkillPointAvailableStatusItem(object data = null)
	{
		foreach (object obj in Components.MinionResumes)
		{
			MinionResume minionResume = (MinionResume)obj;
			if (!minionResume.HasTag(GameTags.Dead) && minionResume.TotalSkillPointsGained - minionResume.SkillsMastered > 0)
			{
				if (this.skillPointAvailableStatusItem == Guid.Empty)
				{
					this.skillPointAvailableStatusItem = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.SkillPointsAvailable, null);
				}
				return;
			}
		}
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.SkillPointsAvailable, false);
		this.skillPointAvailableStatusItem = Guid.Empty;
	}

	// Token: 0x060050D6 RID: 20694 RVA: 0x001D592C File Offset: 0x001D3B2C
	private Chore CreateWorkChore()
	{
		return new WorkChore<RoleStation>(Db.Get().ChoreTypes.LearnSkill, this, null, true, null, null, null, false, null, false, true, Assets.GetAnim("anim_hat_kanim"), false, true, false, PriorityScreen.PriorityClass.personalNeeds, 5, false, false);
	}

	// Token: 0x060050D7 RID: 20695 RVA: 0x001D596D File Offset: 0x001D3B6D
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		worker.GetComponent<MinionResume>().SkillLearned();
	}

	// Token: 0x060050D8 RID: 20696 RVA: 0x001D5981 File Offset: 0x001D3B81
	private void OnSelectRolesClick()
	{
		DetailsScreen.Instance.Show(false);
		ManagementMenu.Instance.ToggleSkills();
	}

	// Token: 0x060050D9 RID: 20697 RVA: 0x001D5998 File Offset: 0x001D3B98
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		foreach (int num in this.subscriptions)
		{
			Game.Instance.Unsubscribe(num);
		}
		Components.RoleStations.Remove(this);
	}

	// Token: 0x060050DA RID: 20698 RVA: 0x001D5A00 File Offset: 0x001D3C00
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		return base.GetDescriptors(go);
	}

	// Token: 0x0400365F RID: 13919
	private Chore chore;

	// Token: 0x04003660 RID: 13920
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04003661 RID: 13921
	[MyCmpAdd]
	private Operational operational;

	// Token: 0x04003662 RID: 13922
	private RoleStation.RoleStationSM.Instance smi;

	// Token: 0x04003663 RID: 13923
	private Guid skillPointAvailableStatusItem;

	// Token: 0x04003664 RID: 13924
	private Action<object> UpdateStatusItemDelegate;

	// Token: 0x04003665 RID: 13925
	private List<int> subscriptions = new List<int>();

	// Token: 0x02001BC6 RID: 7110
	public class RoleStationSM : GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation>
	{
		// Token: 0x0600A887 RID: 43143 RVA: 0x003B4A88 File Offset: 0x003B2C88
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.unoperational;
			this.unoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (RoleStation.RoleStationSM.Instance smi) => smi.GetComponent<Operational>().IsOperational);
			this.operational.ToggleChore((RoleStation.RoleStationSM.Instance smi) => smi.master.CreateWorkChore(), this.unoperational);
		}

		// Token: 0x040083EF RID: 33775
		public GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.State unoperational;

		// Token: 0x040083F0 RID: 33776
		public GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.State operational;

		// Token: 0x020028A3 RID: 10403
		public new class Instance : GameStateMachine<RoleStation.RoleStationSM, RoleStation.RoleStationSM.Instance, RoleStation, object>.GameInstance
		{
			// Token: 0x0600CCCF RID: 52431 RVA: 0x0041BDDB File Offset: 0x00419FDB
			public Instance(RoleStation master)
				: base(master)
			{
			}
		}
	}
}
