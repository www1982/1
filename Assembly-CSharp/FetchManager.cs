using System;
using System.Collections.Generic;
using System.Diagnostics;
using FoodRehydrator;
using UnityEngine;

// Token: 0x0200091D RID: 2333
[AddComponentMenu("KMonoBehaviour/scripts/FetchManager")]
public class FetchManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x060040F3 RID: 16627 RVA: 0x0016D223 File Offset: 0x0016B423
	private static int QuantizeRotValue(float rot_value)
	{
		return (int)(4f * rot_value);
	}

	// Token: 0x060040F4 RID: 16628 RVA: 0x0016D22D File Offset: 0x0016B42D
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name)
	{
	}

	// Token: 0x060040F5 RID: 16629 RVA: 0x0016D22F File Offset: 0x0016B42F
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void BeginDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x060040F6 RID: 16630 RVA: 0x0016D231 File Offset: 0x0016B431
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name)
	{
	}

	// Token: 0x060040F7 RID: 16631 RVA: 0x0016D233 File Offset: 0x0016B433
	[Conditional("ENABLE_FETCH_PROFILING")]
	private static void EndDetailedSample(string region_name, int count)
	{
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x0016D238 File Offset: 0x0016B438
	public HandleVector<int>.Handle Add(Pickupable pickupable)
	{
		Tag tag = pickupable.KPrefabID.PrefabID();
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId = null;
		if (!this.prefabIdToFetchables.TryGetValue(tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId = new FetchManager.FetchablesByPrefabId(tag);
			this.prefabIdToFetchables[tag] = fetchablesByPrefabId;
		}
		return fetchablesByPrefabId.AddPickupable(pickupable);
	}

	// Token: 0x060040F9 RID: 16633 RVA: 0x0016D280 File Offset: 0x0016B480
	public void Remove(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle)
	{
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId;
		if (this.prefabIdToFetchables.TryGetValue(prefab_tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId.RemovePickupable(fetchable_handle);
		}
	}

	// Token: 0x060040FA RID: 16634 RVA: 0x0016D2A4 File Offset: 0x0016B4A4
	public void UpdateStorage(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle, Storage storage)
	{
		FetchManager.FetchablesByPrefabId fetchablesByPrefabId;
		if (this.prefabIdToFetchables.TryGetValue(prefab_tag, out fetchablesByPrefabId))
		{
			fetchablesByPrefabId.UpdateStorage(fetchable_handle, storage);
		}
	}

	// Token: 0x060040FB RID: 16635 RVA: 0x0016D2C9 File Offset: 0x0016B4C9
	public void UpdateTags(Tag prefab_tag, HandleVector<int>.Handle fetchable_handle)
	{
		this.prefabIdToFetchables[prefab_tag].UpdateTags(fetchable_handle);
	}

	// Token: 0x060040FC RID: 16636 RVA: 0x0016D2E0 File Offset: 0x0016B4E0
	public void Sim1000ms(float dt)
	{
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair in this.prefabIdToFetchables)
		{
			keyValuePair.Value.Sim1000ms(dt);
		}
	}

	// Token: 0x060040FD RID: 16637 RVA: 0x0016D33C File Offset: 0x0016B53C
	public void UpdatePickups(PathProber path_prober, WorkerBase worker)
	{
		Navigator component = worker.GetComponent<Navigator>();
		this.updateOffsetTables.Reset(null);
		this.updatePickupsWorkItems.Reset(null);
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair in this.prefabIdToFetchables)
		{
			FetchManager.FetchablesByPrefabId value = keyValuePair.Value;
			this.updateOffsetTables.Add(new FetchManager.UpdateOffsetTables(value));
			this.updatePickupsWorkItems.Add(new FetchManager.UpdatePickupWorkItem
			{
				fetchablesByPrefabId = value,
				pathProber = path_prober,
				navigator = component,
				worker = worker.GetComponent<KPrefabID>().InstanceID
			});
		}
		GlobalJobManager.Run(this.updateOffsetTables);
		for (int i = 0; i < this.updateOffsetTables.Count; i++)
		{
			this.updateOffsetTables.GetWorkItem(i).Finish();
		}
		OffsetTracker.isExecutingWithinJob = true;
		GlobalJobManager.Run(this.updatePickupsWorkItems);
		OffsetTracker.isExecutingWithinJob = false;
		this.pickups.Clear();
		foreach (KeyValuePair<Tag, FetchManager.FetchablesByPrefabId> keyValuePair2 in this.prefabIdToFetchables)
		{
			this.pickups.AddRange(keyValuePair2.Value.finalPickups);
		}
		this.pickups.Sort(FetchManager.PickupComparerNoPriority.CompareInst);
	}

	// Token: 0x060040FE RID: 16638 RVA: 0x0016D4C0 File Offset: 0x0016B6C0
	public static bool IsFetchablePickup(Pickupable pickup, FetchChore chore, Storage destination)
	{
		KPrefabID kprefabID = pickup.KPrefabID;
		Storage storage = pickup.storage;
		if (pickup.UnreservedFetchAmount <= 0f)
		{
			return false;
		}
		if (pickup.PrimaryElement.MassPerUnit > 1f && pickup.PrimaryElement.MassPerUnit > chore.originalAmount)
		{
			return false;
		}
		if (kprefabID == null)
		{
			return false;
		}
		if (!pickup.isChoreAllowedToPickup(chore.choreType))
		{
			return false;
		}
		if (chore.criteria == FetchChore.MatchCriteria.MatchID && !chore.tags.Contains(kprefabID.PrefabTag))
		{
			return false;
		}
		if (chore.criteria == FetchChore.MatchCriteria.MatchTags && !kprefabID.HasTag(chore.tagsFirst))
		{
			return false;
		}
		if (chore.requiredTag.IsValid && !kprefabID.HasTag(chore.requiredTag))
		{
			return false;
		}
		if (kprefabID.HasAnyTags(chore.forbiddenTags))
		{
			return false;
		}
		if (kprefabID.HasTag(GameTags.MarkedForMove))
		{
			return false;
		}
		if (storage != null)
		{
			if (!storage.ignoreSourcePriority && destination.ShouldOnlyTransferFromLowerPriority && destination.masterPriority <= storage.masterPriority)
			{
				return false;
			}
			if (destination.storageNetworkID != -1 && destination.storageNetworkID == storage.storageNetworkID)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060040FF RID: 16639 RVA: 0x0016D5E8 File Offset: 0x0016B7E8
	public static Pickupable FindFetchTarget(List<Pickupable> pickupables, Storage destination, FetchChore chore)
	{
		foreach (Pickupable pickupable in pickupables)
		{
			if (FetchManager.IsFetchablePickup(pickupable, chore, destination))
			{
				return pickupable;
			}
		}
		return null;
	}

	// Token: 0x06004100 RID: 16640 RVA: 0x0016D640 File Offset: 0x0016B840
	public Pickupable FindFetchTarget(Storage destination, FetchChore chore)
	{
		foreach (FetchManager.Pickup pickup in this.pickups)
		{
			if (FetchManager.IsFetchablePickup(pickup.pickupable, chore, destination))
			{
				return pickup.pickupable;
			}
		}
		return null;
	}

	// Token: 0x06004101 RID: 16641 RVA: 0x0016D6A8 File Offset: 0x0016B8A8
	public static bool IsFetchablePickup_Exclude(KPrefabID pickup_id, Storage source, float pickup_unreserved_amount, HashSet<Tag> exclude_tags, Tag required_tag, Storage destination)
	{
		return FetchManager.IsFetchablePickup_Exclude(pickup_id, source, pickup_unreserved_amount, exclude_tags, new Tag[] { required_tag }, destination);
	}

	// Token: 0x06004102 RID: 16642 RVA: 0x0016D6C4 File Offset: 0x0016B8C4
	public static bool IsFetchablePickup_Exclude(KPrefabID pickup_id, Storage source, float pickup_unreserved_amount, HashSet<Tag> exclude_tags, Tag[] required_tags, Storage destination)
	{
		if (pickup_unreserved_amount <= 0f)
		{
			return false;
		}
		if (pickup_id == null)
		{
			return false;
		}
		if (exclude_tags.Contains(pickup_id.PrefabTag))
		{
			return false;
		}
		if (!pickup_id.HasAllTags(required_tags))
		{
			return false;
		}
		if (source != null)
		{
			if (!source.ignoreSourcePriority && destination.ShouldOnlyTransferFromLowerPriority && destination.masterPriority <= source.masterPriority)
			{
				return false;
			}
			if (destination.storageNetworkID != -1 && destination.storageNetworkID == source.storageNetworkID)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06004103 RID: 16643 RVA: 0x0016D74E File Offset: 0x0016B94E
	public Pickupable FindEdibleFetchTarget(Storage destination, HashSet<Tag> exclude_tags, Tag required_tag)
	{
		return this.FindEdibleFetchTarget(destination, exclude_tags, new Tag[] { required_tag });
	}

	// Token: 0x06004104 RID: 16644 RVA: 0x0016D768 File Offset: 0x0016B968
	public Pickupable FindEdibleFetchTarget(Storage destination, HashSet<Tag> exclude_tags, Tag[] required_tags)
	{
		FetchManager.Pickup pickup = new FetchManager.Pickup
		{
			PathCost = ushort.MaxValue,
			foodQuality = int.MinValue
		};
		int num = int.MaxValue;
		foreach (FetchManager.Pickup pickup2 in this.pickups)
		{
			Pickupable pickupable = pickup2.pickupable;
			if (FetchManager.IsFetchablePickup_Exclude(pickupable.KPrefabID, pickupable.storage, pickupable.UnreservedFetchAmount, exclude_tags, required_tags, destination))
			{
				int num2 = (int)pickup2.PathCost + (5 - pickup2.foodQuality) * 50;
				if (num2 < num)
				{
					pickup = pickup2;
					num = num2;
				}
			}
		}
		Navigator component = destination.GetComponent<Navigator>();
		if (component != null)
		{
			foreach (object obj in Components.FoodRehydrators)
			{
				GameObject gameObject = (GameObject)obj;
				int num3 = Grid.PosToCell(gameObject);
				int cost = component.PathProber.GetCost(num3);
				if (cost != -1 && num > cost + 50 + 5)
				{
					AccessabilityManager accessabilityManager = ((gameObject != null) ? gameObject.GetComponent<AccessabilityManager>() : null);
					if (accessabilityManager != null && accessabilityManager.CanAccess(destination.gameObject))
					{
						foreach (GameObject gameObject2 in gameObject.GetComponent<Storage>().items)
						{
							Storage storage = ((gameObject2 != null) ? gameObject2.GetComponent<Storage>() : null);
							if (storage != null && !storage.IsEmpty())
							{
								Edible component2 = storage.items[0].GetComponent<Edible>();
								Pickupable component3 = component2.GetComponent<Pickupable>();
								if (FetchManager.IsFetchablePickup_Exclude(component3.KPrefabID, component3.storage, component3.UnreservedFetchAmount, exclude_tags, required_tags, destination))
								{
									int num4 = cost + (5 - component2.FoodInfo.Quality + 1) * 50 + 5;
									if (num4 < num)
									{
										pickup.pickupable = component3;
										pickup.foodQuality = component2.FoodInfo.Quality;
										pickup.tagBitsHash = component2.PrefabID().GetHashCode();
										num = num4;
									}
								}
							}
						}
					}
				}
			}
		}
		return pickup.pickupable;
	}

	// Token: 0x040028A0 RID: 10400
	private List<FetchManager.Pickup> pickups = new List<FetchManager.Pickup>();

	// Token: 0x040028A1 RID: 10401
	public Dictionary<Tag, FetchManager.FetchablesByPrefabId> prefabIdToFetchables = new Dictionary<Tag, FetchManager.FetchablesByPrefabId>();

	// Token: 0x040028A2 RID: 10402
	private WorkItemCollection<FetchManager.UpdateOffsetTables, object> updateOffsetTables = new WorkItemCollection<FetchManager.UpdateOffsetTables, object>();

	// Token: 0x040028A3 RID: 10403
	private WorkItemCollection<FetchManager.UpdatePickupWorkItem, object> updatePickupsWorkItems = new WorkItemCollection<FetchManager.UpdatePickupWorkItem, object>();

	// Token: 0x020018B4 RID: 6324
	public struct Fetchable
	{
		// Token: 0x040079A0 RID: 31136
		public Pickupable pickupable;

		// Token: 0x040079A1 RID: 31137
		public int tagBitsHash;

		// Token: 0x040079A2 RID: 31138
		public int masterPriority;

		// Token: 0x040079A3 RID: 31139
		public int freshness;

		// Token: 0x040079A4 RID: 31140
		public int foodQuality;
	}

	// Token: 0x020018B5 RID: 6325
	[DebuggerDisplay("{pickupable.name}")]
	public struct Pickup
	{
		// Token: 0x040079A5 RID: 31141
		public Pickupable pickupable;

		// Token: 0x040079A6 RID: 31142
		public int tagBitsHash;

		// Token: 0x040079A7 RID: 31143
		public ushort PathCost;

		// Token: 0x040079A8 RID: 31144
		public int masterPriority;

		// Token: 0x040079A9 RID: 31145
		public int freshness;

		// Token: 0x040079AA RID: 31146
		public int foodQuality;
	}

	// Token: 0x020018B6 RID: 6326
	private static class PickupComparerIncludingPriority
	{
		// Token: 0x06009D5D RID: 40285 RVA: 0x00392AA4 File Offset: 0x00390CA4
		private static int Compare(FetchManager.Pickup a, FetchManager.Pickup b)
		{
			int num = a.tagBitsHash.CompareTo(b.tagBitsHash);
			if (num != 0)
			{
				return num;
			}
			num = b.masterPriority.CompareTo(a.masterPriority);
			if (num != 0)
			{
				return num;
			}
			num = a.PathCost.CompareTo(b.PathCost);
			if (num != 0)
			{
				return num;
			}
			num = b.foodQuality.CompareTo(a.foodQuality);
			if (num != 0)
			{
				return num;
			}
			return b.freshness.CompareTo(a.freshness);
		}

		// Token: 0x040079AB RID: 31147
		public static Comparison<FetchManager.Pickup> CompareInst = new Comparison<FetchManager.Pickup>(FetchManager.PickupComparerIncludingPriority.Compare);
	}

	// Token: 0x020018B7 RID: 6327
	private static class PickupComparerNoPriority
	{
		// Token: 0x06009D5F RID: 40287 RVA: 0x00392B38 File Offset: 0x00390D38
		public static int Compare(FetchManager.Pickup a, FetchManager.Pickup b)
		{
			int num = a.PathCost.CompareTo(b.PathCost);
			if (num != 0)
			{
				return num;
			}
			num = b.foodQuality.CompareTo(a.foodQuality);
			if (num != 0)
			{
				return num;
			}
			return b.freshness.CompareTo(a.freshness);
		}

		// Token: 0x040079AC RID: 31148
		public static Comparison<FetchManager.Pickup> CompareInst = new Comparison<FetchManager.Pickup>(FetchManager.PickupComparerNoPriority.Compare);
	}

	// Token: 0x020018B8 RID: 6328
	public class FetchablesByPrefabId
	{
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06009D61 RID: 40289 RVA: 0x00392B9C File Offset: 0x00390D9C
		// (set) Token: 0x06009D62 RID: 40290 RVA: 0x00392BA4 File Offset: 0x00390DA4
		public Tag prefabId { get; private set; }

		// Token: 0x06009D63 RID: 40291 RVA: 0x00392BB0 File Offset: 0x00390DB0
		public FetchablesByPrefabId(Tag prefab_id)
		{
			this.prefabId = prefab_id;
			this.fetchables = new KCompactedVector<FetchManager.Fetchable>(0);
			this.rotUpdaters = new Dictionary<HandleVector<int>.Handle, Rottable.Instance>();
			this.finalPickups = new List<FetchManager.Pickup>();
		}

		// Token: 0x06009D64 RID: 40292 RVA: 0x00392C10 File Offset: 0x00390E10
		public HandleVector<int>.Handle AddPickupable(Pickupable pickupable)
		{
			int num = 5;
			Edible component = pickupable.GetComponent<Edible>();
			if (component != null)
			{
				num = component.GetQuality();
			}
			int num2 = 0;
			if (pickupable.storage != null)
			{
				Prioritizable prioritizable = pickupable.storage.prioritizable;
				if (prioritizable != null)
				{
					num2 = prioritizable.GetMasterPriority().priority_value;
				}
			}
			Rottable.Instance smi = pickupable.GetSMI<Rottable.Instance>();
			int num3 = 0;
			if (!smi.IsNullOrStopped())
			{
				num3 = FetchManager.QuantizeRotValue(smi.RotValue);
			}
			KPrefabID kprefabID = pickupable.KPrefabID;
			HandleVector<int>.Handle handle = this.fetchables.Allocate(new FetchManager.Fetchable
			{
				pickupable = pickupable,
				foodQuality = num,
				freshness = num3,
				masterPriority = num2,
				tagBitsHash = kprefabID.GetTagsHash()
			});
			if (!smi.IsNullOrStopped())
			{
				this.rotUpdaters[handle] = smi;
			}
			return handle;
		}

		// Token: 0x06009D65 RID: 40293 RVA: 0x00392CF3 File Offset: 0x00390EF3
		public void RemovePickupable(HandleVector<int>.Handle fetchable_handle)
		{
			this.fetchables.Free(fetchable_handle);
			this.rotUpdaters.Remove(fetchable_handle);
		}

		// Token: 0x06009D66 RID: 40294 RVA: 0x00392D10 File Offset: 0x00390F10
		public void UpdatePickups(PathProber path_prober, Navigator worker_navigator, int worker)
		{
			this.GatherPickupablesWhichCanBePickedUp(worker);
			this.GatherReachablePickups(worker_navigator);
			this.finalPickups.Sort(FetchManager.PickupComparerIncludingPriority.CompareInst);
			if (this.finalPickups.Count > 0)
			{
				FetchManager.Pickup pickup = this.finalPickups[0];
				int num = pickup.tagBitsHash;
				int num2 = this.finalPickups.Count;
				int num3 = 0;
				for (int i = 1; i < this.finalPickups.Count; i++)
				{
					bool flag = false;
					FetchManager.Pickup pickup2 = this.finalPickups[i];
					int tagBitsHash = pickup2.tagBitsHash;
					if (pickup.masterPriority == pickup2.masterPriority && tagBitsHash == num)
					{
						flag = true;
					}
					if (flag)
					{
						num2--;
					}
					else
					{
						num3++;
						pickup = pickup2;
						num = tagBitsHash;
						if (i > num3)
						{
							this.finalPickups[num3] = pickup2;
						}
					}
				}
				this.finalPickups.RemoveRange(num2, this.finalPickups.Count - num2);
			}
		}

		// Token: 0x06009D67 RID: 40295 RVA: 0x00392DFC File Offset: 0x00390FFC
		private void GatherPickupablesWhichCanBePickedUp(int worker)
		{
			this.pickupsWhichCanBePickedUp.Clear();
			foreach (FetchManager.Fetchable fetchable in this.fetchables.GetDataList())
			{
				Pickupable pickupable = fetchable.pickupable;
				if (pickupable.CouldBePickedUpByMinion(worker))
				{
					this.pickupsWhichCanBePickedUp.Add(new FetchManager.Pickup
					{
						pickupable = pickupable,
						tagBitsHash = fetchable.tagBitsHash,
						PathCost = ushort.MaxValue,
						masterPriority = fetchable.masterPriority,
						freshness = fetchable.freshness,
						foodQuality = fetchable.foodQuality
					});
				}
			}
		}

		// Token: 0x06009D68 RID: 40296 RVA: 0x00392EC4 File Offset: 0x003910C4
		public void UpdateOffsetTables()
		{
			foreach (FetchManager.Fetchable fetchable in this.fetchables.GetDataList())
			{
				fetchable.pickupable.GetOffsets(fetchable.pickupable.cachedCell);
			}
		}

		// Token: 0x06009D69 RID: 40297 RVA: 0x00392F2C File Offset: 0x0039112C
		private void GatherReachablePickups(Navigator navigator)
		{
			this.cellCosts.Clear();
			this.finalPickups.Clear();
			foreach (FetchManager.Pickup pickup in this.pickupsWhichCanBePickedUp)
			{
				Pickupable pickupable = pickup.pickupable;
				int num = -1;
				if (!this.cellCosts.TryGetValue(pickupable.cachedCell, out num))
				{
					num = pickupable.GetNavigationCost(navigator, pickupable.cachedCell);
					this.cellCosts[pickupable.cachedCell] = num;
				}
				if (num != -1)
				{
					this.finalPickups.Add(new FetchManager.Pickup
					{
						pickupable = pickupable,
						tagBitsHash = pickup.tagBitsHash,
						PathCost = (ushort)num,
						masterPriority = pickup.masterPriority,
						freshness = pickup.freshness,
						foodQuality = pickup.foodQuality
					});
				}
			}
		}

		// Token: 0x06009D6A RID: 40298 RVA: 0x00393030 File Offset: 0x00391230
		public void UpdateStorage(HandleVector<int>.Handle fetchable_handle, Storage storage)
		{
			FetchManager.Fetchable data = this.fetchables.GetData(fetchable_handle);
			int num = 0;
			Pickupable pickupable = data.pickupable;
			if (pickupable.storage != null)
			{
				Prioritizable prioritizable = pickupable.storage.prioritizable;
				if (prioritizable != null)
				{
					num = prioritizable.GetMasterPriority().priority_value;
				}
			}
			data.masterPriority = num;
			this.fetchables.SetData(fetchable_handle, data);
		}

		// Token: 0x06009D6B RID: 40299 RVA: 0x0039309C File Offset: 0x0039129C
		public void UpdateTags(HandleVector<int>.Handle fetchable_handle)
		{
			FetchManager.Fetchable data = this.fetchables.GetData(fetchable_handle);
			data.tagBitsHash = data.pickupable.KPrefabID.GetTagsHash();
			this.fetchables.SetData(fetchable_handle, data);
		}

		// Token: 0x06009D6C RID: 40300 RVA: 0x003930DC File Offset: 0x003912DC
		public void Sim1000ms(float dt)
		{
			foreach (KeyValuePair<HandleVector<int>.Handle, Rottable.Instance> keyValuePair in this.rotUpdaters)
			{
				HandleVector<int>.Handle key = keyValuePair.Key;
				Rottable.Instance value = keyValuePair.Value;
				FetchManager.Fetchable data = this.fetchables.GetData(key);
				data.freshness = FetchManager.QuantizeRotValue(value.RotValue);
				this.fetchables.SetData(key, data);
			}
		}

		// Token: 0x040079AD RID: 31149
		public KCompactedVector<FetchManager.Fetchable> fetchables;

		// Token: 0x040079AE RID: 31150
		public List<FetchManager.Pickup> finalPickups = new List<FetchManager.Pickup>();

		// Token: 0x040079AF RID: 31151
		private Dictionary<HandleVector<int>.Handle, Rottable.Instance> rotUpdaters;

		// Token: 0x040079B0 RID: 31152
		private List<FetchManager.Pickup> pickupsWhichCanBePickedUp = new List<FetchManager.Pickup>();

		// Token: 0x040079B1 RID: 31153
		private Dictionary<int, int> cellCosts = new Dictionary<int, int>();
	}

	// Token: 0x020018B9 RID: 6329
	private struct UpdateOffsetTables : IWorkItem<object>
	{
		// Token: 0x06009D6D RID: 40301 RVA: 0x00393168 File Offset: 0x00391368
		public UpdateOffsetTables(FetchManager.FetchablesByPrefabId fetchables)
		{
			this.data = fetchables;
			this.failed = ListPool<Pickupable, FetchManager.UpdateOffsetTables>.Allocate();
		}

		// Token: 0x06009D6E RID: 40302 RVA: 0x0039317C File Offset: 0x0039137C
		public void Run(object _, int threadIndex)
		{
			if (Game.IsOnMainThread())
			{
				this.data.UpdateOffsetTables();
				return;
			}
			foreach (FetchManager.Fetchable fetchable in this.data.fetchables.GetDataList())
			{
				if (!fetchable.pickupable.ValidateOffsets(fetchable.pickupable.cachedCell))
				{
					this.failed.Add(fetchable.pickupable);
				}
			}
		}

		// Token: 0x06009D6F RID: 40303 RVA: 0x00393210 File Offset: 0x00391410
		public void Finish()
		{
			foreach (Pickupable pickupable in this.failed)
			{
				pickupable.GetOffsets(pickupable.cachedCell);
			}
			this.failed.Recycle();
		}

		// Token: 0x040079B3 RID: 31155
		public FetchManager.FetchablesByPrefabId data;

		// Token: 0x040079B4 RID: 31156
		private ListPool<Pickupable, FetchManager.UpdateOffsetTables>.PooledList failed;
	}

	// Token: 0x020018BA RID: 6330
	private struct UpdatePickupWorkItem : IWorkItem<object>
	{
		// Token: 0x06009D70 RID: 40304 RVA: 0x00393274 File Offset: 0x00391474
		public void Run(object shared_data, int threadIndex)
		{
			this.fetchablesByPrefabId.UpdatePickups(this.pathProber, this.navigator, this.worker);
		}

		// Token: 0x040079B5 RID: 31157
		public FetchManager.FetchablesByPrefabId fetchablesByPrefabId;

		// Token: 0x040079B6 RID: 31158
		public PathProber pathProber;

		// Token: 0x040079B7 RID: 31159
		public Navigator navigator;

		// Token: 0x040079B8 RID: 31160
		public int worker;
	}
}
