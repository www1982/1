using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007D0 RID: 2000
[AddComponentMenu("KMonoBehaviour/scripts/StorageLocker")]
public class StorageLocker : KMonoBehaviour, IUserControlledCapacity
{
	// Token: 0x060035B5 RID: 13749 RVA: 0x0012C4AB File Offset: 0x0012A6AB
	protected override void OnPrefabInit()
	{
		this.Initialize(false);
	}

	// Token: 0x060035B6 RID: 13750 RVA: 0x0012C4B4 File Offset: 0x0012A6B4
	protected void Initialize(bool use_logic_meter)
	{
		base.OnPrefabInit();
		this.log = new LoggerFS("StorageLocker", 35);
		ChoreType choreType = Db.Get().ChoreTypes.Get(this.choreTypeID);
		this.filteredStorage = new FilteredStorage(this, null, this, use_logic_meter, choreType);
		base.Subscribe<StorageLocker>(-905833192, StorageLocker.OnCopySettingsDelegate);
	}

	// Token: 0x060035B7 RID: 13751 RVA: 0x0012C510 File Offset: 0x0012A710
	protected override void OnSpawn()
	{
		this.filteredStorage.FilterChanged();
		if (this.nameable != null && !this.lockerName.IsNullOrWhiteSpace())
		{
			this.nameable.SetName(this.lockerName);
		}
		base.Trigger(-1683615038, null);
	}

	// Token: 0x060035B8 RID: 13752 RVA: 0x0012C560 File Offset: 0x0012A760
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x060035B9 RID: 13753 RVA: 0x0012C570 File Offset: 0x0012A770
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		StorageLocker component = gameObject.GetComponent<StorageLocker>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x060035BA RID: 13754 RVA: 0x0012C5AB File Offset: 0x0012A7AB
	public void UpdateForbiddenTag(Tag game_tag, bool forbidden)
	{
		if (forbidden)
		{
			this.filteredStorage.RemoveForbiddenTag(game_tag);
			return;
		}
		this.filteredStorage.AddForbiddenTag(game_tag);
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x060035BB RID: 13755 RVA: 0x0012C5C9 File Offset: 0x0012A7C9
	// (set) Token: 0x060035BC RID: 13756 RVA: 0x0012C5E1 File Offset: 0x0012A7E1
	public virtual float UserMaxCapacity
	{
		get
		{
			return Mathf.Min(this.userMaxCapacity, base.GetComponent<Storage>().capacityKg);
		}
		set
		{
			this.userMaxCapacity = value;
			this.filteredStorage.FilterChanged();
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x060035BD RID: 13757 RVA: 0x0012C5F5 File Offset: 0x0012A7F5
	public float AmountStored
	{
		get
		{
			return base.GetComponent<Storage>().MassStored();
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x060035BE RID: 13758 RVA: 0x0012C602 File Offset: 0x0012A802
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x060035BF RID: 13759 RVA: 0x0012C609 File Offset: 0x0012A809
	public float MaxCapacity
	{
		get
		{
			return base.GetComponent<Storage>().capacityKg;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x060035C0 RID: 13760 RVA: 0x0012C616 File Offset: 0x0012A816
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x060035C1 RID: 13761 RVA: 0x0012C619 File Offset: 0x0012A819
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x04002082 RID: 8322
	private LoggerFS log;

	// Token: 0x04002083 RID: 8323
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x04002084 RID: 8324
	[Serialize]
	public string lockerName = "";

	// Token: 0x04002085 RID: 8325
	protected FilteredStorage filteredStorage;

	// Token: 0x04002086 RID: 8326
	[MyCmpGet]
	private UserNameable nameable;

	// Token: 0x04002087 RID: 8327
	public string choreTypeID = Db.Get().ChoreTypes.StorageFetch.Id;

	// Token: 0x04002088 RID: 8328
	private static readonly EventSystem.IntraObjectHandler<StorageLocker> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<StorageLocker>(delegate(StorageLocker component, object data)
	{
		component.OnCopySettings(data);
	});
}
