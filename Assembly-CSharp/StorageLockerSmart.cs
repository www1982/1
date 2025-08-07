using System;

// Token: 0x020007D1 RID: 2001
public class StorageLockerSmart : StorageLocker
{
	// Token: 0x060035C4 RID: 13764 RVA: 0x0012C675 File Offset: 0x0012A875
	protected override void OnPrefabInit()
	{
		base.Initialize(true);
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x0012C680 File Offset: 0x0012A880
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ports = base.gameObject.GetComponent<LogicPorts>();
		base.Subscribe<StorageLockerSmart>(-1697596308, StorageLockerSmart.UpdateLogicCircuitCBDelegate);
		base.Subscribe<StorageLockerSmart>(-592767678, StorageLockerSmart.UpdateLogicCircuitCBDelegate);
		this.UpdateLogicAndActiveState();
	}

	// Token: 0x060035C6 RID: 13766 RVA: 0x0012C6CC File Offset: 0x0012A8CC
	private void UpdateLogicCircuitCB(object data)
	{
		this.UpdateLogicAndActiveState();
	}

	// Token: 0x060035C7 RID: 13767 RVA: 0x0012C6D4 File Offset: 0x0012A8D4
	private void UpdateLogicAndActiveState()
	{
		bool flag = this.filteredStorage.IsFull();
		bool isOperational = this.operational.IsOperational;
		bool flag2 = flag && isOperational;
		this.ports.SendSignal(FilteredStorage.FULL_PORT_ID, flag2 ? 1 : 0);
		this.filteredStorage.SetLogicMeter(flag2);
		this.operational.SetActive(isOperational, false);
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x060035C8 RID: 13768 RVA: 0x0012C72B File Offset: 0x0012A92B
	// (set) Token: 0x060035C9 RID: 13769 RVA: 0x0012C733 File Offset: 0x0012A933
	public override float UserMaxCapacity
	{
		get
		{
			return base.UserMaxCapacity;
		}
		set
		{
			base.UserMaxCapacity = value;
			this.UpdateLogicAndActiveState();
		}
	}

	// Token: 0x04002089 RID: 8329
	[MyCmpGet]
	private LogicPorts ports;

	// Token: 0x0400208A RID: 8330
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400208B RID: 8331
	private static readonly EventSystem.IntraObjectHandler<StorageLockerSmart> UpdateLogicCircuitCBDelegate = new EventSystem.IntraObjectHandler<StorageLockerSmart>(delegate(StorageLockerSmart component, object data)
	{
		component.UpdateLogicCircuitCB(data);
	});
}
