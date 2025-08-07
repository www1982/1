using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006B8 RID: 1720
public class BionicMinionStorageExtension : KMonoBehaviour, StoredMinionIdentity.IStoredMinionExtension
{
	// Token: 0x06002A3C RID: 10812 RVA: 0x000F4BE4 File Offset: 0x000F2DE4
	public void AddStoredMinionGameObjectRequirements(GameObject storedMinionGameObject)
	{
		Storage[] components = storedMinionGameObject.GetComponents<Storage>();
		using (List<Tag>.Enumerator enumerator = BionicMinionStorageExtension.StoragesTypesToTransfer.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Tag inventoryType = enumerator.Current;
				if (components == null || !(components.FindFirst((Storage s) => s.storageID == inventoryType) != null))
				{
					Storage storage = storedMinionGameObject.AddComponent<Storage>();
					storage.allowItemRemoval = false;
					storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
					storage.storageID = inventoryType;
				}
			}
		}
	}

	// Token: 0x06002A3D RID: 10813 RVA: 0x000F4C84 File Offset: 0x000F2E84
	void StoredMinionIdentity.IStoredMinionExtension.PullFrom(StoredMinionIdentity source)
	{
		Storage[] components = source.GetComponents<Storage>();
		Storage[] components2 = base.GetComponents<Storage>();
		foreach (Storage storage in components)
		{
			bool flag = false;
			foreach (Storage storage2 in components2)
			{
				if (storage2.storageID == storage.storageID)
				{
					storage.Transfer(storage2, false, true);
					flag = true;
					break;
				}
			}
			DebugUtil.DevAssert(flag, "Missmatched storages on BionicMinionStorageExtension", null);
		}
	}

	// Token: 0x06002A3E RID: 10814 RVA: 0x000F4D04 File Offset: 0x000F2F04
	void StoredMinionIdentity.IStoredMinionExtension.PushTo(StoredMinionIdentity destination)
	{
		GameObject gameObject = destination.gameObject;
		this.AddStoredMinionGameObjectRequirements(gameObject);
		Storage[] components = base.GetComponents<Storage>();
		Storage[] components2 = gameObject.GetComponents<Storage>();
		foreach (Tag tag in BionicMinionStorageExtension.StoragesTypesToTransfer)
		{
			Storage storage = null;
			Storage storage2 = null;
			foreach (Storage storage3 in components)
			{
				if (storage3.storageID == tag)
				{
					storage = storage3;
					break;
				}
			}
			foreach (Storage storage4 in components2)
			{
				if (storage4.storageID == tag)
				{
					storage2 = storage4;
					break;
				}
			}
			storage.Transfer(storage2, true, true);
		}
	}

	// Token: 0x04001905 RID: 6405
	private static readonly List<Tag> StoragesTypesToTransfer = new List<Tag>
	{
		GameTags.StoragesIds.BionicBatteryStorage,
		GameTags.StoragesIds.BionicUpgradeStorage,
		GameTags.StoragesIds.BionicOxygenTankStorage
	};
}
