using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020007B9 RID: 1977
[AddComponentMenu("KMonoBehaviour/Workable/ResetSkillsStation")]
public class ResetSkillsStation : Workable
{
	// Token: 0x060034C0 RID: 13504 RVA: 0x00127F1C File Offset: 0x0012611C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.lightEfficiencyBonus = false;
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x00127F2B File Offset: 0x0012612B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.OnAssign(this.assignable.assignee);
		this.assignable.OnAssign += this.OnAssign;
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x00127F5B File Offset: 0x0012615B
	private void OnAssign(IAssignableIdentity obj)
	{
		if (obj != null)
		{
			this.CreateChore();
			return;
		}
		if (this.chore != null)
		{
			this.chore.Cancel("Unassigned");
			this.chore = null;
		}
	}

	// Token: 0x060034C3 RID: 13507 RVA: 0x00127F88 File Offset: 0x00126188
	private void CreateChore()
	{
		this.chore = new WorkChore<ResetSkillsStation>(Db.Get().ChoreTypes.UnlearnSkill, this, null, true, null, null, null, false, null, true, true, null, false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
	}

	// Token: 0x060034C4 RID: 13508 RVA: 0x00127FC1 File Offset: 0x001261C1
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		base.GetComponent<Operational>().SetActive(true, false);
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorTraining, this);
	}

	// Token: 0x060034C5 RID: 13509 RVA: 0x00127FF4 File Offset: 0x001261F4
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		this.assignable.Unassign();
		MinionResume component = worker.GetComponent<MinionResume>();
		if (component != null)
		{
			component.ResetSkillLevels(true);
			component.SetHats(component.CurrentHat, null);
			component.ApplyTargetHat();
			this.notification = new Notification(MISC.NOTIFICATIONS.RESETSKILL.NAME, NotificationType.Good, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.RESETSKILL.TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);
			worker.GetComponent<Notifier>().Add(this.notification, "");
		}
	}

	// Token: 0x060034C6 RID: 13510 RVA: 0x00128095 File Offset: 0x00126295
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		base.GetComponent<Operational>().SetActive(false, false);
		base.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ComplexFabricatorTraining, this);
		this.chore = null;
	}

	// Token: 0x04001FD7 RID: 8151
	[MyCmpReq]
	public Assignable assignable;

	// Token: 0x04001FD8 RID: 8152
	private Notification notification;

	// Token: 0x04001FD9 RID: 8153
	private Chore chore;
}
