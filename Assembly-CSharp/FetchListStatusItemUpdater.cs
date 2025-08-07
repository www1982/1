using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200091C RID: 2332
[AddComponentMenu("KMonoBehaviour/scripts/FetchListStatusItemUpdater")]
public class FetchListStatusItemUpdater : KMonoBehaviour, IRender200ms
{
	// Token: 0x060040ED RID: 16621 RVA: 0x0016CBCA File Offset: 0x0016ADCA
	public static void DestroyInstance()
	{
		FetchListStatusItemUpdater.instance = null;
	}

	// Token: 0x060040EE RID: 16622 RVA: 0x0016CBD2 File Offset: 0x0016ADD2
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		FetchListStatusItemUpdater.instance = this;
	}

	// Token: 0x060040EF RID: 16623 RVA: 0x0016CBE0 File Offset: 0x0016ADE0
	public void AddFetchList(FetchList2 fetch_list)
	{
		this.fetchLists.Add(fetch_list);
	}

	// Token: 0x060040F0 RID: 16624 RVA: 0x0016CBEE File Offset: 0x0016ADEE
	public void RemoveFetchList(FetchList2 fetch_list)
	{
		this.fetchLists.Remove(fetch_list);
	}

	// Token: 0x060040F1 RID: 16625 RVA: 0x0016CC00 File Offset: 0x0016AE00
	public void Render200ms(float dt)
	{
		foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
		{
			int id = worldContainer.id;
			DictionaryPool<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary = DictionaryPool<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList, FetchListStatusItemUpdater>.Allocate();
			int num = Math.Min(this.maxIteratingCount, this.fetchLists.Count - this.currentIterationIndex[id]);
			for (int i = 0; i < num; i++)
			{
				FetchList2 fetchList = this.fetchLists[i + this.currentIterationIndex[id]];
				if (!(fetchList.Destination == null) && fetchList.Destination.gameObject.GetMyWorldId() == id)
				{
					ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList pooledList = null;
					int instanceID = fetchList.Destination.GetInstanceID();
					if (!pooledDictionary.TryGetValue(instanceID, out pooledList))
					{
						pooledList = ListPool<FetchList2, FetchListStatusItemUpdater>.Allocate();
						pooledDictionary[instanceID] = pooledList;
					}
					pooledList.Add(fetchList);
				}
			}
			this.currentIterationIndex[id] += num;
			if (this.currentIterationIndex[id] >= this.fetchLists.Count)
			{
				this.currentIterationIndex[id] = 0;
			}
			DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary2 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
			DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary3 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
			foreach (KeyValuePair<int, ListPool<FetchList2, FetchListStatusItemUpdater>.PooledList> keyValuePair in pooledDictionary)
			{
				if (!(keyValuePair.Value[0].Destination.GetMyWorld() == null))
				{
					ListPool<Tag, FetchListStatusItemUpdater>.PooledList pooledList2 = ListPool<Tag, FetchListStatusItemUpdater>.Allocate();
					Storage destination = keyValuePair.Value[0].Destination;
					foreach (FetchList2 fetchList2 in keyValuePair.Value)
					{
						fetchList2.UpdateRemaining();
						foreach (KeyValuePair<Tag, float> keyValuePair2 in fetchList2.GetRemaining())
						{
							if (!pooledList2.Contains(keyValuePair2.Key))
							{
								pooledList2.Add(keyValuePair2.Key);
							}
						}
					}
					ListPool<Pickupable, FetchListStatusItemUpdater>.PooledList pooledList3 = ListPool<Pickupable, FetchListStatusItemUpdater>.Allocate();
					foreach (GameObject gameObject in destination.items)
					{
						if (!(gameObject == null))
						{
							Pickupable component = gameObject.GetComponent<Pickupable>();
							if (!(component == null))
							{
								pooledList3.Add(component);
							}
						}
					}
					DictionaryPool<Tag, float, FetchListStatusItemUpdater>.PooledDictionary pooledDictionary4 = DictionaryPool<Tag, float, FetchListStatusItemUpdater>.Allocate();
					foreach (Tag tag in pooledList2)
					{
						float num2 = 0f;
						foreach (Pickupable pickupable in pooledList3)
						{
							if (pickupable.KPrefabID.HasTag(tag))
							{
								num2 += pickupable.FetchTotalAmount;
							}
						}
						pooledDictionary4[tag] = num2;
					}
					foreach (Tag tag2 in pooledList2)
					{
						if (!pooledDictionary2.ContainsKey(tag2))
						{
							pooledDictionary2[tag2] = destination.GetMyWorld().worldInventory.GetTotalAmount(tag2, true);
						}
						if (!pooledDictionary3.ContainsKey(tag2))
						{
							pooledDictionary3[tag2] = destination.GetMyWorld().worldInventory.GetAmount(tag2, true);
						}
					}
					foreach (FetchList2 fetchList3 in keyValuePair.Value)
					{
						bool flag = false;
						bool flag2 = true;
						bool flag3 = false;
						foreach (KeyValuePair<Tag, float> keyValuePair3 in fetchList3.GetRemaining())
						{
							Tag key = keyValuePair3.Key;
							float value = keyValuePair3.Value;
							float num3 = pooledDictionary4[key];
							float num4 = pooledDictionary2[key];
							float num5 = pooledDictionary3[key];
							float num6 = Mathf.Min(value, num4);
							float num7 = num5 + num6;
							float minimumAmount = fetchList3.GetMinimumAmount(key);
							if (num3 + num7 < minimumAmount)
							{
								flag = true;
							}
							if (num7 < value)
							{
								flag2 = false;
							}
							if (num3 + num7 > value && value > num7)
							{
								flag3 = true;
							}
						}
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.WaitingForMaterials, ref fetchList3.waitingForMaterialsHandle, flag2);
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailable, ref fetchList3.materialsUnavailableHandle, flag);
						fetchList3.UpdateStatusItem(Db.Get().BuildingStatusItems.MaterialsUnavailableForRefill, ref fetchList3.materialsUnavailableForRefillHandle, flag3);
					}
					pooledDictionary4.Recycle();
					pooledList3.Recycle();
					pooledList2.Recycle();
					keyValuePair.Value.Recycle();
				}
			}
			pooledDictionary3.Recycle();
			pooledDictionary2.Recycle();
			pooledDictionary.Recycle();
		}
	}

	// Token: 0x0400289C RID: 10396
	public static FetchListStatusItemUpdater instance;

	// Token: 0x0400289D RID: 10397
	private List<FetchList2> fetchLists = new List<FetchList2>();

	// Token: 0x0400289E RID: 10398
	private int[] currentIterationIndex = new int[255];

	// Token: 0x0400289F RID: 10399
	private int maxIteratingCount = 100;
}
