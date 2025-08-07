using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005BB RID: 1467
public class FoodStorage : KMonoBehaviour
{
	// Token: 0x17000155 RID: 341
	// (get) Token: 0x060021D3 RID: 8659 RVA: 0x000C3B3F File Offset: 0x000C1D3F
	// (set) Token: 0x060021D4 RID: 8660 RVA: 0x000C3B47 File Offset: 0x000C1D47
	public FilteredStorage FilteredStorage { get; set; }

	// Token: 0x17000156 RID: 342
	// (get) Token: 0x060021D5 RID: 8661 RVA: 0x000C3B50 File Offset: 0x000C1D50
	// (set) Token: 0x060021D6 RID: 8662 RVA: 0x000C3B58 File Offset: 0x000C1D58
	public bool SpicedFoodOnly
	{
		get
		{
			return this.onlyStoreSpicedFood;
		}
		set
		{
			this.onlyStoreSpicedFood = value;
			base.Trigger(1163645216, this.onlyStoreSpicedFood);
			if (this.onlyStoreSpicedFood)
			{
				this.FilteredStorage.AddForbiddenTag(GameTags.UnspicedFood);
				this.storage.DropHasTags(new Tag[]
				{
					GameTags.Edible,
					GameTags.UnspicedFood
				});
				return;
			}
			this.FilteredStorage.RemoveForbiddenTag(GameTags.UnspicedFood);
		}
	}

	// Token: 0x060021D7 RID: 8663 RVA: 0x000C3BD5 File Offset: 0x000C1DD5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<FoodStorage>(-905833192, FoodStorage.OnCopySettingsDelegate);
	}

	// Token: 0x060021D8 RID: 8664 RVA: 0x000C3BEE File Offset: 0x000C1DEE
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060021D9 RID: 8665 RVA: 0x000C3BF8 File Offset: 0x000C1DF8
	private void OnCopySettings(object data)
	{
		FoodStorage component = ((GameObject)data).GetComponent<FoodStorage>();
		if (component != null)
		{
			this.SpicedFoodOnly = component.SpicedFoodOnly;
		}
	}

	// Token: 0x040013C7 RID: 5063
	[Serialize]
	private bool onlyStoreSpicedFood;

	// Token: 0x040013C8 RID: 5064
	[MyCmpReq]
	public Storage storage;

	// Token: 0x040013CA RID: 5066
	private static readonly EventSystem.IntraObjectHandler<FoodStorage> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<FoodStorage>(delegate(FoodStorage component, object data)
	{
		component.OnCopySettings(data);
	});
}
