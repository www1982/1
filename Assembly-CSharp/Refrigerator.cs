using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007B6 RID: 1974
[AddComponentMenu("KMonoBehaviour/scripts/Refrigerator")]
public class Refrigerator : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x0600348D RID: 13453 RVA: 0x00126761 File Offset: 0x00124961
	protected override void OnPrefabInit()
	{
		this.filteredStorage = new FilteredStorage(this, new Tag[] { GameTags.Compostable }, this, true, Db.Get().ChoreTypes.FoodFetch);
	}

	// Token: 0x0600348E RID: 13454 RVA: 0x00126794 File Offset: 0x00124994
	protected override void OnSpawn()
	{
		base.GetComponent<KAnimControllerBase>().Play("off", KAnim.PlayMode.Once, 1f, 0f);
		FoodStorage component = base.GetComponent<FoodStorage>();
		component.FilteredStorage = this.filteredStorage;
		component.SpicedFoodOnly = component.SpicedFoodOnly;
		this.filteredStorage.FilterChanged();
		this.UpdateLogicCircuit();
		base.Subscribe<Refrigerator>(-905833192, Refrigerator.OnCopySettingsDelegate);
		base.Subscribe<Refrigerator>(-1697596308, Refrigerator.UpdateLogicCircuitCBDelegate);
		base.Subscribe<Refrigerator>(-592767678, Refrigerator.UpdateLogicCircuitCBDelegate);
	}

	// Token: 0x0600348F RID: 13455 RVA: 0x00126822 File Offset: 0x00124A22
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x06003490 RID: 13456 RVA: 0x0012682F File Offset: 0x00124A2F
	public bool IsActive()
	{
		return this.operational.IsActive;
	}

	// Token: 0x06003491 RID: 13457 RVA: 0x0012683C File Offset: 0x00124A3C
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		Refrigerator component = gameObject.GetComponent<Refrigerator>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06003492 RID: 13458 RVA: 0x00126877 File Offset: 0x00124A77
	// (set) Token: 0x06003493 RID: 13459 RVA: 0x0012688F File Offset: 0x00124A8F
	public float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, this.storage.capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
			this.UpdateLogicCircuit();
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x06003494 RID: 13460 RVA: 0x001268A9 File Offset: 0x00124AA9
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x06003495 RID: 13461 RVA: 0x001268B6 File Offset: 0x00124AB6
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06003496 RID: 13462 RVA: 0x001268BD File Offset: 0x00124ABD
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06003497 RID: 13463 RVA: 0x001268CA File Offset: 0x00124ACA
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06003498 RID: 13464 RVA: 0x001268CD File Offset: 0x00124ACD
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x06003499 RID: 13465 RVA: 0x001268D5 File Offset: 0x00124AD5
	private void UpdateLogicCircuitCB(object data)
	{
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x001268E0 File Offset: 0x00124AE0
	private void UpdateLogicCircuit()
	{
		bool flag = this.filteredStorage.IsFull();
		bool isOperational = this.operational.IsOperational;
		bool flag2 = flag && isOperational;
		this.ports.SendSignal(FilteredStorage.FULL_PORT_ID, flag2 ? 1 : 0);
		this.filteredStorage.SetLogicMeter(flag2);
	}

	// Token: 0x04001FC5 RID: 8133
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001FC6 RID: 8134
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001FC7 RID: 8135
	[MyCmpGet]
	private LogicPorts ports;

	// Token: 0x04001FC8 RID: 8136
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x04001FC9 RID: 8137
	private FilteredStorage filteredStorage;

	// Token: 0x04001FCA RID: 8138
	private static readonly EventSystem.IntraObjectHandler<Refrigerator> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Refrigerator>(delegate(Refrigerator component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001FCB RID: 8139
	private static readonly EventSystem.IntraObjectHandler<Refrigerator> UpdateLogicCircuitCBDelegate = new EventSystem.IntraObjectHandler<Refrigerator>(delegate(Refrigerator component, object data)
	{
		component.UpdateLogicCircuitCB(data);
	});
}
