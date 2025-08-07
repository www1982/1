using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000899 RID: 2201
public class DefragmentationZone : Workable
{
	// Token: 0x06003CE4 RID: 15588 RVA: 0x00152DA0 File Offset: 0x00150FA0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.showProgressBar = false;
		this.workerStatusItem = null;
		this.synchronizeAnims = false;
		this.triggerWorkReactions = false;
		this.lightEfficiencyBonus = false;
		this.approachable = base.GetComponent<IApproachable>();
		this.workAnims = new HashedString[] { "microchip_bed_pre", "microchip_bed_loop" };
		this.workingPstComplete = new HashedString[] { "microchip_bed_pst" };
		this.workingPstFailed = new HashedString[] { "microchip_bed_pst" };
	}

	// Token: 0x06003CE5 RID: 15589 RVA: 0x00152E52 File Offset: 0x00151052
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(float.PositiveInfinity);
		this.OnWorkableEventCB = (Action<Workable, Workable.WorkableEvent>)Delegate.Combine(this.OnWorkableEventCB, new Action<Workable, Workable.WorkableEvent>(this.OnWorkableEvent));
	}

	// Token: 0x06003CE6 RID: 15590 RVA: 0x00152E87 File Offset: 0x00151087
	private void OnWorkableEvent(Workable workable, Workable.WorkableEvent workable_event)
	{
		if (workable_event == Workable.WorkableEvent.WorkStarted)
		{
			this.AddRoomEffects();
		}
	}

	// Token: 0x06003CE7 RID: 15591 RVA: 0x00152E94 File Offset: 0x00151094
	private void AddRoomEffects()
	{
		if (base.worker == null)
		{
			return;
		}
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject == null)
		{
			return;
		}
		RoomType roomType = roomOfGameObject.roomType;
		List<EffectInstance> list = null;
		roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), base.worker.GetComponent<Effects>(), out list);
		if (list != null)
		{
			foreach (EffectInstance effectInstance in list)
			{
				effectInstance.timeRemaining = 1800f;
			}
		}
	}

	// Token: 0x06003CE8 RID: 15592 RVA: 0x00152F30 File Offset: 0x00151130
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x04002557 RID: 9559
	private const float BEDROOM_EFFECTS_DURATION_OVERRIDE = 1800f;

	// Token: 0x04002558 RID: 9560
	[MyCmpGet]
	public Assignable assignable;

	// Token: 0x04002559 RID: 9561
	public IApproachable approachable;
}
