using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004FC RID: 1276
public abstract class ClosestPickupableSensor<T> : Sensor where T : Component
{
	// Token: 0x06001B4F RID: 6991 RVA: 0x00096199 File Offset: 0x00094399
	public ClosestPickupableSensor(Sensors sensors, Tag itemSearchTag, bool shouldStartActive)
		: base(sensors, shouldStartActive)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.consumableConsumer = base.GetComponent<ConsumableConsumer>();
		this.storage = base.GetComponent<Storage>();
		this.itemSearchTag = itemSearchTag;
	}

	// Token: 0x06001B50 RID: 6992 RVA: 0x000961D9 File Offset: 0x000943D9
	public T GetItem()
	{
		return this.item;
	}

	// Token: 0x06001B51 RID: 6993 RVA: 0x000961E1 File Offset: 0x000943E1
	public int GetItemNavCost()
	{
		if (!(this.item == null))
		{
			return this.itemNavCost;
		}
		return int.MaxValue;
	}

	// Token: 0x06001B52 RID: 6994 RVA: 0x00096202 File Offset: 0x00094402
	public virtual HashSet<Tag> GetForbbidenTags()
	{
		if (!(this.consumableConsumer == null))
		{
			return this.consumableConsumer.forbiddenTagSet;
		}
		return new HashSet<Tag>(0);
	}

	// Token: 0x06001B53 RID: 6995 RVA: 0x00096224 File Offset: 0x00094424
	public override void Update()
	{
		HashSet<Tag> forbbidenTags = this.GetForbbidenTags();
		int maxValue = int.MaxValue;
		Pickupable pickupable = this.FindClosestPickupable(this.storage, forbbidenTags, out maxValue, this.itemSearchTag, this.requiredTags);
		bool flag = this.itemInReachButNotPermitted;
		T t = default(T);
		bool flag2 = false;
		if (pickupable != null)
		{
			t = pickupable.GetComponent<T>();
			flag2 = true;
			flag = false;
		}
		else
		{
			int num;
			flag = this.FindClosestPickupable(this.storage, new HashSet<Tag>(), out num, this.itemSearchTag, this.requiredTags) != null;
		}
		if (t != this.item || this.isThereAnyItemAvailable != flag2)
		{
			this.item = t;
			this.itemNavCost = maxValue;
			this.isThereAnyItemAvailable = flag2;
			this.itemInReachButNotPermitted = flag;
			this.ItemChanged();
		}
	}

	// Token: 0x06001B54 RID: 6996 RVA: 0x000962F4 File Offset: 0x000944F4
	public Pickupable FindClosestPickupable(Storage destination, HashSet<Tag> exclude_tags, out int cost, Tag categoryTag, Tag[] otherRequiredTags = null)
	{
		ICollection<Pickupable> pickupables = base.gameObject.GetMyWorld().worldInventory.GetPickupables(categoryTag, false);
		if (pickupables == null)
		{
			cost = int.MaxValue;
			return null;
		}
		if (otherRequiredTags == null)
		{
			otherRequiredTags = new Tag[] { categoryTag };
		}
		Pickupable pickupable = null;
		int num = int.MaxValue;
		foreach (Pickupable pickupable2 in pickupables)
		{
			if (FetchManager.IsFetchablePickup_Exclude(pickupable2.KPrefabID, pickupable2.storage, pickupable2.UnreservedFetchAmount, exclude_tags, otherRequiredTags, destination))
			{
				int navigationCost = pickupable2.GetNavigationCost(this.navigator, pickupable2.cachedCell);
				if (navigationCost != -1 && navigationCost < num)
				{
					pickupable = pickupable2;
					num = navigationCost;
				}
			}
		}
		cost = num;
		return pickupable;
	}

	// Token: 0x06001B55 RID: 6997 RVA: 0x000963C4 File Offset: 0x000945C4
	public virtual void ItemChanged()
	{
		Action<T> onItemChanged = this.OnItemChanged;
		if (onItemChanged == null)
		{
			return;
		}
		onItemChanged(this.item);
	}

	// Token: 0x04001014 RID: 4116
	public Action<T> OnItemChanged;

	// Token: 0x04001015 RID: 4117
	protected T item;

	// Token: 0x04001016 RID: 4118
	protected int itemNavCost = int.MaxValue;

	// Token: 0x04001017 RID: 4119
	protected Tag itemSearchTag;

	// Token: 0x04001018 RID: 4120
	protected Tag[] requiredTags;

	// Token: 0x04001019 RID: 4121
	protected bool isThereAnyItemAvailable;

	// Token: 0x0400101A RID: 4122
	protected bool itemInReachButNotPermitted;

	// Token: 0x0400101B RID: 4123
	private Navigator navigator;

	// Token: 0x0400101C RID: 4124
	protected ConsumableConsumer consumableConsumer;

	// Token: 0x0400101D RID: 4125
	private Storage storage;
}
