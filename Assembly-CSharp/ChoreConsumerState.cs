using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020004AD RID: 1197
public class ChoreConsumerState
{
	// Token: 0x06001954 RID: 6484 RVA: 0x0008BA14 File Offset: 0x00089C14
	public ChoreConsumerState(ChoreConsumer consumer)
	{
		this.consumer = consumer;
		this.navigator = consumer.GetComponent<Navigator>();
		this.prefabid = consumer.GetComponent<KPrefabID>();
		this.ownable = consumer.GetComponent<Ownable>();
		this.gameObject = consumer.gameObject;
		this.solidTransferArm = consumer.GetComponent<SolidTransferArm>();
		this.hasSolidTransferArm = this.solidTransferArm != null;
		this.resume = consumer.GetComponent<MinionResume>();
		this.choreDriver = consumer.GetComponent<ChoreDriver>();
		this.schedulable = consumer.GetComponent<Schedulable>();
		this.traits = consumer.GetComponent<Traits>();
		this.choreProvider = consumer.GetComponent<ChoreProvider>();
		MinionIdentity component = consumer.GetComponent<MinionIdentity>();
		if (component != null)
		{
			if (component.assignableProxy == null)
			{
				component.assignableProxy = MinionAssignablesProxy.InitAssignableProxy(component.assignableProxy, component);
			}
			this.assignables = component.GetSoleOwner();
			this.equipment = component.GetEquipment();
		}
		else
		{
			this.assignables = consumer.GetComponent<Assignables>();
			this.equipment = consumer.GetComponent<Equipment>();
		}
		this.storage = consumer.GetComponent<Storage>();
		this.consumableConsumer = consumer.GetComponent<ConsumableConsumer>();
		this.worker = consumer.GetComponent<WorkerBase>();
		this.selectable = consumer.GetComponent<KSelectable>();
		if (this.schedulable != null)
		{
			this.scheduleBlock = this.schedulable.GetSchedule().GetCurrentScheduleBlock();
		}
	}

	// Token: 0x06001955 RID: 6485 RVA: 0x0008BB68 File Offset: 0x00089D68
	public void Refresh()
	{
		if (this.schedulable != null)
		{
			Schedule schedule = this.schedulable.GetSchedule();
			if (schedule != null)
			{
				this.scheduleBlock = schedule.GetCurrentScheduleBlock();
			}
		}
	}

	// Token: 0x04000E81 RID: 3713
	public KPrefabID prefabid;

	// Token: 0x04000E82 RID: 3714
	public GameObject gameObject;

	// Token: 0x04000E83 RID: 3715
	public ChoreConsumer consumer;

	// Token: 0x04000E84 RID: 3716
	public ChoreProvider choreProvider;

	// Token: 0x04000E85 RID: 3717
	public Navigator navigator;

	// Token: 0x04000E86 RID: 3718
	public Ownable ownable;

	// Token: 0x04000E87 RID: 3719
	public Assignables assignables;

	// Token: 0x04000E88 RID: 3720
	public MinionResume resume;

	// Token: 0x04000E89 RID: 3721
	public ChoreDriver choreDriver;

	// Token: 0x04000E8A RID: 3722
	public Schedulable schedulable;

	// Token: 0x04000E8B RID: 3723
	public Traits traits;

	// Token: 0x04000E8C RID: 3724
	public Equipment equipment;

	// Token: 0x04000E8D RID: 3725
	public Storage storage;

	// Token: 0x04000E8E RID: 3726
	public ConsumableConsumer consumableConsumer;

	// Token: 0x04000E8F RID: 3727
	public KSelectable selectable;

	// Token: 0x04000E90 RID: 3728
	public WorkerBase worker;

	// Token: 0x04000E91 RID: 3729
	public SolidTransferArm solidTransferArm;

	// Token: 0x04000E92 RID: 3730
	public bool hasSolidTransferArm;

	// Token: 0x04000E93 RID: 3731
	public ScheduleBlock scheduleBlock;
}
