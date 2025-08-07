using System;
using UnityEngine;

// Token: 0x02000AD3 RID: 2771
public class FetchDrone : KMonoBehaviour
{
	// Token: 0x0600509C RID: 20636 RVA: 0x001D3AC8 File Offset: 0x001D1CC8
	protected override void OnSpawn()
	{
		ChoreGroup[] array = new ChoreGroup[]
		{
			Db.Get().ChoreGroups.Build,
			Db.Get().ChoreGroups.Basekeeping,
			Db.Get().ChoreGroups.Cook,
			Db.Get().ChoreGroups.Art,
			Db.Get().ChoreGroups.Dig,
			Db.Get().ChoreGroups.Research,
			Db.Get().ChoreGroups.Farming,
			Db.Get().ChoreGroups.Ranching,
			Db.Get().ChoreGroups.MachineOperating,
			Db.Get().ChoreGroups.MedicalAid,
			Db.Get().ChoreGroups.Combat,
			Db.Get().ChoreGroups.LifeSupport,
			Db.Get().ChoreGroups.Recreation,
			Db.Get().ChoreGroups.Toggle,
			Db.Get().ChoreGroups.Rocketry
		};
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				this.choreConsumer.SetPermittedByUser(array[i], false);
			}
		}
		foreach (Storage storage in base.GetComponents<Storage>())
		{
			if (storage.storageID != GameTags.ChargedPortableBattery)
			{
				this.pickupableStorage = storage;
				break;
			}
		}
		this.animController = base.GetComponent<KBatchedAnimController>();
		this.pickupableStorage.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		base.Subscribe(-1582839653, new Action<object>(this.OnTagsChanged));
	}

	// Token: 0x0600509D RID: 20637 RVA: 0x001D3C8B File Offset: 0x001D1E8B
	protected override void OnCleanUp()
	{
		base.Unsubscribe(-1697596308);
		base.Unsubscribe(-1582839653);
		base.OnCleanUp();
	}

	// Token: 0x0600509E RID: 20638 RVA: 0x001D3CAC File Offset: 0x001D1EAC
	private void OnTagsChanged(object data)
	{
		TagChangedEventData tagChangedEventData = (TagChangedEventData)data;
		if (tagChangedEventData.added && tagChangedEventData.tag == GameTags.Creatures.Die)
		{
			Brain component = base.GetComponent<Brain>();
			if (component != null && !component.IsRunning())
			{
				component.Resume("death");
			}
		}
	}

	// Token: 0x0600509F RID: 20639 RVA: 0x001D3D00 File Offset: 0x001D1F00
	private void OnStorageChanged(object data)
	{
		GameObject gameObject = (GameObject)data;
		this.RemoveTracker(gameObject);
		this.ShowPickupSymbol(gameObject);
	}

	// Token: 0x060050A0 RID: 20640 RVA: 0x001D3D24 File Offset: 0x001D1F24
	private void ShowPickupSymbol(GameObject pickupable)
	{
		bool flag = this.pickupableStorage.items.Contains(pickupable);
		if (flag)
		{
			this.AddAnimTracker(pickupable);
		}
		this.animController.SetSymbolVisiblity(FetchDrone.BOTTOM, !flag);
		this.animController.SetSymbolVisiblity(FetchDrone.BOTTOM_CARRY, flag);
	}

	// Token: 0x060050A1 RID: 20641 RVA: 0x001D3D7C File Offset: 0x001D1F7C
	private void AddAnimTracker(GameObject go)
	{
		KAnimControllerBase component = go.GetComponent<KAnimControllerBase>();
		if (component == null)
		{
			return;
		}
		if (component.AnimFiles != null && component.AnimFiles.Length != 0 && component.AnimFiles[0] != null && component.GetComponent<Pickupable>().trackOnPickup)
		{
			KBatchedAnimTracker kbatchedAnimTracker = go.GetComponent<KBatchedAnimTracker>();
			if (kbatchedAnimTracker != null && kbatchedAnimTracker.controller == this.animController)
			{
				return;
			}
			kbatchedAnimTracker = go.AddComponent<KBatchedAnimTracker>();
			kbatchedAnimTracker.useTargetPoint = false;
			kbatchedAnimTracker.fadeOut = false;
			kbatchedAnimTracker.symbol = ((go.GetComponent<Brain>() != null) ? new HashedString("snapTo_pivot") : new HashedString("snapTo_thing"));
			kbatchedAnimTracker.forceAlwaysVisible = true;
		}
	}

	// Token: 0x060050A2 RID: 20642 RVA: 0x001D3E38 File Offset: 0x001D2038
	private void RemoveTracker(GameObject go)
	{
		KBatchedAnimTracker kbatchedAnimTracker = ((go != null) ? go.GetComponent<KBatchedAnimTracker>() : null);
		if (kbatchedAnimTracker != null && kbatchedAnimTracker.controller == this.animController)
		{
			global::UnityEngine.Object.Destroy(kbatchedAnimTracker);
		}
	}

	// Token: 0x04003633 RID: 13875
	private static string BOTTOM = "bottom";

	// Token: 0x04003634 RID: 13876
	private static string BOTTOM_CARRY = "bottom_carry";

	// Token: 0x04003635 RID: 13877
	private KBatchedAnimController animController;

	// Token: 0x04003636 RID: 13878
	private Storage pickupableStorage;

	// Token: 0x04003637 RID: 13879
	[MyCmpAdd]
	private ChoreConsumer choreConsumer;
}
