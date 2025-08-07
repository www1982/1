using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B2C RID: 2860
public class CargoBayCluster : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x06005471 RID: 21617 RVA: 0x001EB459 File Offset: 0x001E9659
	// (set) Token: 0x06005472 RID: 21618 RVA: 0x001EB461 File Offset: 0x001E9661
	public float UserMaxCapacity
	{
		get
		{
			return this.userMaxCapacity;
		}
		set
		{
			this.userMaxCapacity = value;
			base.Trigger(-945020481, this);
		}
	}

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x06005473 RID: 21619 RVA: 0x001EB476 File Offset: 0x001E9676
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x06005474 RID: 21620 RVA: 0x001EB47D File Offset: 0x001E967D
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x06005475 RID: 21621 RVA: 0x001EB48A File Offset: 0x001E968A
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x170005F8 RID: 1528
	// (get) Token: 0x06005476 RID: 21622 RVA: 0x001EB497 File Offset: 0x001E9697
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x06005477 RID: 21623 RVA: 0x001EB49A File Offset: 0x001E969A
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x06005478 RID: 21624 RVA: 0x001EB4A2 File Offset: 0x001E96A2
	public float RemainingCapacity
	{
		get
		{
			return this.userMaxCapacity - this.storage.MassStored();
		}
	}

	// Token: 0x06005479 RID: 21625 RVA: 0x001EB4B6 File Offset: 0x001E96B6
	protected override void OnPrefabInit()
	{
		this.userMaxCapacity = this.storage.capacityKg;
	}

	// Token: 0x0600547A RID: 21626 RVA: 0x001EB4CC File Offset: 0x001E96CC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KBatchedAnimController>().Play("grounded", KAnim.PlayMode.Loop, 1f, 0f);
		base.Subscribe<CargoBayCluster>(493375141, CargoBayCluster.OnRefreshUserMenuDelegate);
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_target", "meter_fill", "meter_frame", "meter_OL" });
		KBatchedAnimTracker component = this.meter.gameObject.GetComponent<KBatchedAnimTracker>();
		component.matchParentOffset = true;
		component.forceAlwaysAlive = true;
		this.OnStorageChange(null);
		base.Subscribe<CargoBayCluster>(-1697596308, CargoBayCluster.OnStorageChangeDelegate);
	}

	// Token: 0x0600547B RID: 21627 RVA: 0x001EB58C File Offset: 0x001E978C
	private void OnRefreshUserMenu(object data)
	{
		KIconButtonMenu.ButtonInfo buttonInfo = new KIconButtonMenu.ButtonInfo("action_empty_contents", UI.USERMENUACTIONS.EMPTYSTORAGE.NAME, delegate
		{
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}, global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.EMPTYSTORAGE.TOOLTIP, true);
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x0600547C RID: 21628 RVA: 0x001EB5E8 File Offset: 0x001E97E8
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(this.storage.MassStored() / this.storage.Capacity());
		this.UpdateCargoStatusItem();
	}

	// Token: 0x0600547D RID: 21629 RVA: 0x001EB614 File Offset: 0x001E9814
	private void UpdateCargoStatusItem()
	{
		RocketModuleCluster component = base.GetComponent<RocketModuleCluster>();
		if (component == null)
		{
			return;
		}
		CraftModuleInterface craftInterface = component.CraftInterface;
		if (craftInterface == null)
		{
			return;
		}
		Clustercraft component2 = craftInterface.GetComponent<Clustercraft>();
		if (component2 == null)
		{
			return;
		}
		component2.UpdateStatusItem();
	}

	// Token: 0x040038C0 RID: 14528
	private MeterController meter;

	// Token: 0x040038C1 RID: 14529
	[SerializeField]
	public Storage storage;

	// Token: 0x040038C2 RID: 14530
	[SerializeField]
	public CargoBay.CargoType storageType;

	// Token: 0x040038C3 RID: 14531
	[Serialize]
	private float userMaxCapacity;

	// Token: 0x040038C4 RID: 14532
	private static readonly EventSystem.IntraObjectHandler<CargoBayCluster> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<CargoBayCluster>(delegate(CargoBayCluster component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x040038C5 RID: 14533
	private static readonly EventSystem.IntraObjectHandler<CargoBayCluster> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<CargoBayCluster>(delegate(CargoBayCluster component, object data)
	{
		component.OnStorageChange(data);
	});
}
