using System;
using KSerialization;
using UnityEngine;

// Token: 0x020007B2 RID: 1970
[AddComponentMenu("KMonoBehaviour/scripts/RationBox")]
public class RationBox : KMonoBehaviour, IUserControlledCapacity, IRender1000ms, IRottable
{
	// Token: 0x06003453 RID: 13395 RVA: 0x001255E4 File Offset: 0x001237E4
	protected override void OnPrefabInit()
	{
		this.filteredStorage = new FilteredStorage(this, new Tag[] { GameTags.Compostable }, this, false, Db.Get().ChoreTypes.FoodFetch);
		base.Subscribe<RationBox>(-592767678, RationBox.OnOperationalChangedDelegate);
		base.Subscribe<RationBox>(-905833192, RationBox.OnCopySettingsDelegate);
		DiscoveredResources.Instance.Discover("FieldRation".ToTag(), GameTags.Edible);
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x0012565B File Offset: 0x0012385B
	protected override void OnSpawn()
	{
		Operational component = base.GetComponent<Operational>();
		component.SetActive(component.IsOperational, false);
		this.filteredStorage.FilterChanged();
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x0012567A File Offset: 0x0012387A
	protected override void OnCleanUp()
	{
		this.filteredStorage.CleanUp();
	}

	// Token: 0x06003456 RID: 13398 RVA: 0x00125687 File Offset: 0x00123887
	private void OnOperationalChanged(object data)
	{
		Operational component = base.GetComponent<Operational>();
		component.SetActive(component.IsOperational, false);
	}

	// Token: 0x06003457 RID: 13399 RVA: 0x0012569C File Offset: 0x0012389C
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		RationBox component = gameObject.GetComponent<RationBox>();
		if (component == null)
		{
			return;
		}
		this.UserMaxCapacity = component.UserMaxCapacity;
	}

	// Token: 0x06003458 RID: 13400 RVA: 0x001256D7 File Offset: 0x001238D7
	public void Render1000ms(float dt)
	{
		Rottable.SetStatusItems(this);
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x06003459 RID: 13401 RVA: 0x001256DF File Offset: 0x001238DF
	// (set) Token: 0x0600345A RID: 13402 RVA: 0x001256F7 File Offset: 0x001238F7
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
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x0600345B RID: 13403 RVA: 0x0012570B File Offset: 0x0012390B
	public float AmountStored
	{
		get
		{
			return this.storage.MassStored();
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x0600345C RID: 13404 RVA: 0x00125718 File Offset: 0x00123918
	public float MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x0600345D RID: 13405 RVA: 0x0012571F File Offset: 0x0012391F
	public float MaxCapacity
	{
		get
		{
			return this.storage.capacityKg;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x0600345E RID: 13406 RVA: 0x0012572C File Offset: 0x0012392C
	public bool WholeValues
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x0600345F RID: 13407 RVA: 0x0012572F File Offset: 0x0012392F
	public LocString CapacityUnits
	{
		get
		{
			return GameUtil.GetCurrentMassUnit(false);
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06003460 RID: 13408 RVA: 0x00125737 File Offset: 0x00123937
	public float RotTemperature
	{
		get
		{
			return 277.15f;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x06003461 RID: 13409 RVA: 0x0012573E File Offset: 0x0012393E
	public float PreserveTemperature
	{
		get
		{
			return 255.15f;
		}
	}

	// Token: 0x06003464 RID: 13412 RVA: 0x0012578E File Offset: 0x0012398E
	GameObject IRottable.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04001FA7 RID: 8103
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04001FA8 RID: 8104
	[Serialize]
	private float userMaxCapacity = float.PositiveInfinity;

	// Token: 0x04001FA9 RID: 8105
	private FilteredStorage filteredStorage;

	// Token: 0x04001FAA RID: 8106
	private static readonly EventSystem.IntraObjectHandler<RationBox> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<RationBox>(delegate(RationBox component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001FAB RID: 8107
	private static readonly EventSystem.IntraObjectHandler<RationBox> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<RationBox>(delegate(RationBox component, object data)
	{
		component.OnCopySettings(data);
	});
}
