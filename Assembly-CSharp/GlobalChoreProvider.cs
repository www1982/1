using System;
using System.Collections.Generic;

// Token: 0x020004B3 RID: 1203
public class GlobalChoreProvider : ChoreProvider, IRender200ms
{
	// Token: 0x060019A3 RID: 6563 RVA: 0x0008D48B File Offset: 0x0008B68B
	public static void DestroyInstance()
	{
		GlobalChoreProvider.Instance = null;
	}

	// Token: 0x060019A4 RID: 6564 RVA: 0x0008D493 File Offset: 0x0008B693
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		GlobalChoreProvider.Instance = this;
		this.clearableManager = new ClearableManager();
	}

	// Token: 0x060019A5 RID: 6565 RVA: 0x0008D4AC File Offset: 0x0008B6AC
	protected override void OnWorldRemoved(object data)
	{
		int num = (int)data;
		int parentWorldId = ClusterManager.Instance.GetWorld(num).ParentWorldId;
		List<FetchChore> list;
		if (this.fetchMap.TryGetValue(parentWorldId, out list))
		{
			base.ClearWorldChores<FetchChore>(list, num);
		}
		base.OnWorldRemoved(data);
	}

	// Token: 0x060019A6 RID: 6566 RVA: 0x0008D4F0 File Offset: 0x0008B6F0
	protected override void OnWorldParentChanged(object data)
	{
		WorldParentChangedEventArgs worldParentChangedEventArgs = data as WorldParentChangedEventArgs;
		if (worldParentChangedEventArgs == null || worldParentChangedEventArgs.lastParentId == 255)
		{
			return;
		}
		base.OnWorldParentChanged(data);
		List<FetchChore> list;
		if (!this.fetchMap.TryGetValue(worldParentChangedEventArgs.lastParentId, out list))
		{
			return;
		}
		List<FetchChore> list2;
		if (!this.fetchMap.TryGetValue(worldParentChangedEventArgs.world.ParentWorldId, out list2))
		{
			list2 = (this.fetchMap[worldParentChangedEventArgs.world.ParentWorldId] = new List<FetchChore>());
		}
		base.TransferChores<FetchChore>(list, list2, worldParentChangedEventArgs.world.ParentWorldId);
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x0008D57C File Offset: 0x0008B77C
	public override void AddChore(Chore chore)
	{
		FetchChore fetchChore = chore as FetchChore;
		if (fetchChore != null)
		{
			int myParentWorldId = fetchChore.gameObject.GetMyParentWorldId();
			List<FetchChore> list;
			if (!this.fetchMap.TryGetValue(myParentWorldId, out list))
			{
				list = (this.fetchMap[myParentWorldId] = new List<FetchChore>());
			}
			chore.provider = this;
			list.Add(fetchChore);
			return;
		}
		base.AddChore(chore);
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x0008D5D8 File Offset: 0x0008B7D8
	public override void RemoveChore(Chore chore)
	{
		FetchChore fetchChore = chore as FetchChore;
		if (fetchChore != null)
		{
			int myParentWorldId = fetchChore.gameObject.GetMyParentWorldId();
			List<FetchChore> list;
			if (this.fetchMap.TryGetValue(myParentWorldId, out list))
			{
				list.Remove(fetchChore);
			}
			chore.provider = null;
			return;
		}
		base.RemoveChore(chore);
	}

	// Token: 0x060019A9 RID: 6569 RVA: 0x0008D624 File Offset: 0x0008B824
	public void UpdateFetches(PathProber path_prober)
	{
		List<FetchChore> list = null;
		int myParentWorldId = path_prober.gameObject.GetMyParentWorldId();
		if (!this.fetchMap.TryGetValue(myParentWorldId, out list))
		{
			return;
		}
		this.fetches.Clear();
		Navigator component = path_prober.GetComponent<Navigator>();
		for (int i = list.Count - 1; i >= 0; i--)
		{
			FetchChore fetchChore = list[i];
			if (!(fetchChore.driver != null) && (!(fetchChore.automatable != null) || !fetchChore.automatable.GetAutomationOnly()))
			{
				if (fetchChore.provider == null)
				{
					fetchChore.Cancel("no provider");
					list[i] = list[list.Count - 1];
					list.RemoveAt(list.Count - 1);
				}
				else
				{
					Storage destination = fetchChore.destination;
					if (!(destination == null))
					{
						int navigationCost = component.GetNavigationCost(destination);
						if (navigationCost != -1)
						{
							this.fetches.Add(new GlobalChoreProvider.Fetch
							{
								chore = fetchChore,
								idsHash = fetchChore.tagsHash,
								cost = navigationCost,
								priority = fetchChore.masterPriority,
								category = destination.fetchCategory
							});
						}
					}
				}
			}
		}
		if (this.fetches.Count > 0)
		{
			this.fetches.Sort(GlobalChoreProvider.Comparer);
			int j = 1;
			int num = 0;
			while (j < this.fetches.Count)
			{
				if (!this.fetches[num].IsBetterThan(this.fetches[j]))
				{
					num++;
					this.fetches[num] = this.fetches[j];
				}
				j++;
			}
			this.fetches.RemoveRange(num + 1, this.fetches.Count - num - 1);
		}
		this.clearableManager.CollectAndSortClearables(component);
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x0008D818 File Offset: 0x0008BA18
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		base.CollectChores(consumer_state, succeeded, failed_contexts);
		this.clearableManager.CollectChores(this.fetches, consumer_state, succeeded, failed_contexts);
		if (this.fetches.Count > 48)
		{
			GlobalChoreProvider.batch_context.Setup(this, consumer_state);
			GlobalChoreProvider.batch_work_items.Reset(GlobalChoreProvider.batch_context);
			for (int i = 0; i < this.fetches.Count; i += 16)
			{
				GlobalChoreProvider.batch_work_items.Add(new MultithreadedCollectChoreContext<GlobalChoreProvider>.WorkBlock<GlobalChoreProvider.GlobalChoreProviderMultithreader>(i, Math.Min(i + 16, this.fetches.Count)));
			}
			GlobalJobManager.Run(GlobalChoreProvider.batch_work_items);
			GlobalChoreProvider.batch_context.Finish(succeeded, failed_contexts);
			return;
		}
		for (int j = 0; j < this.fetches.Count; j++)
		{
			this.fetches[j].chore.CollectChoresFromGlobalChoreProvider(consumer_state, succeeded, failed_contexts, false);
		}
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x0008D8EE File Offset: 0x0008BAEE
	public HandleVector<int>.Handle RegisterClearable(Clearable clearable)
	{
		return this.clearableManager.RegisterClearable(clearable);
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x0008D8FC File Offset: 0x0008BAFC
	public void UnregisterClearable(HandleVector<int>.Handle handle)
	{
		this.clearableManager.UnregisterClearable(handle);
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x0008D90A File Offset: 0x0008BB0A
	protected override void OnLoadLevel()
	{
		base.OnLoadLevel();
		GlobalChoreProvider.Instance = null;
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x0008D918 File Offset: 0x0008BB18
	public void Render200ms(float dt)
	{
		this.UpdateStorageFetchableBits();
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x0008D920 File Offset: 0x0008BB20
	private void UpdateStorageFetchableBits()
	{
		ChoreType storageFetch = Db.Get().ChoreTypes.StorageFetch;
		ChoreType foodFetch = Db.Get().ChoreTypes.FoodFetch;
		this.storageFetchableTags.Clear();
		List<int> worldIDsSorted = ClusterManager.Instance.GetWorldIDsSorted();
		for (int i = 0; i < worldIDsSorted.Count; i++)
		{
			List<FetchChore> list;
			if (this.fetchMap.TryGetValue(worldIDsSorted[i], out list))
			{
				for (int j = 0; j < list.Count; j++)
				{
					FetchChore fetchChore = list[j];
					if ((fetchChore.choreType == storageFetch || fetchChore.choreType == foodFetch) && fetchChore.destination)
					{
						int num = Grid.PosToCell(fetchChore.destination);
						if (MinionGroupProber.Get().IsReachable(num, fetchChore.destination.GetOffsets(num)))
						{
							this.storageFetchableTags.UnionWith(fetchChore.tags);
						}
					}
				}
			}
		}
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x0008DA10 File Offset: 0x0008BC10
	public bool ClearableHasDestination(Pickupable pickupable)
	{
		KPrefabID kprefabID = pickupable.KPrefabID;
		return this.storageFetchableTags.Contains(kprefabID.PrefabTag);
	}

	// Token: 0x04000EBC RID: 3772
	public static GlobalChoreProvider Instance;

	// Token: 0x04000EBD RID: 3773
	public Dictionary<int, List<FetchChore>> fetchMap = new Dictionary<int, List<FetchChore>>();

	// Token: 0x04000EBE RID: 3774
	public List<GlobalChoreProvider.Fetch> fetches = new List<GlobalChoreProvider.Fetch>();

	// Token: 0x04000EBF RID: 3775
	private static readonly GlobalChoreProvider.FetchComparer Comparer = new GlobalChoreProvider.FetchComparer();

	// Token: 0x04000EC0 RID: 3776
	private ClearableManager clearableManager;

	// Token: 0x04000EC1 RID: 3777
	private HashSet<Tag> storageFetchableTags = new HashSet<Tag>();

	// Token: 0x04000EC2 RID: 3778
	private static GlobalChoreProvider.GlobalChoreProviderMultithreader batch_context = new GlobalChoreProvider.GlobalChoreProviderMultithreader();

	// Token: 0x04000EC3 RID: 3779
	private static WorkItemCollection<MultithreadedCollectChoreContext<GlobalChoreProvider>.WorkBlock<GlobalChoreProvider.GlobalChoreProviderMultithreader>, GlobalChoreProvider.GlobalChoreProviderMultithreader> batch_work_items = new WorkItemCollection<MultithreadedCollectChoreContext<GlobalChoreProvider>.WorkBlock<GlobalChoreProvider.GlobalChoreProviderMultithreader>, GlobalChoreProvider.GlobalChoreProviderMultithreader>();

	// Token: 0x020012EC RID: 4844
	public struct Fetch
	{
		// Token: 0x06008832 RID: 34866 RVA: 0x00348F38 File Offset: 0x00347138
		public bool IsBetterThan(GlobalChoreProvider.Fetch fetch)
		{
			if (this.category != fetch.category)
			{
				return false;
			}
			if (this.idsHash != fetch.idsHash)
			{
				return false;
			}
			if (this.chore.choreType != fetch.chore.choreType)
			{
				return false;
			}
			if (this.priority.priority_class > fetch.priority.priority_class)
			{
				return true;
			}
			if (this.priority.priority_class == fetch.priority.priority_class)
			{
				if (this.priority.priority_value > fetch.priority.priority_value)
				{
					return true;
				}
				if (this.priority.priority_value == fetch.priority.priority_value)
				{
					return this.cost <= fetch.cost;
				}
			}
			return false;
		}

		// Token: 0x040067E9 RID: 26601
		public FetchChore chore;

		// Token: 0x040067EA RID: 26602
		public int idsHash;

		// Token: 0x040067EB RID: 26603
		public int cost;

		// Token: 0x040067EC RID: 26604
		public PrioritySetting priority;

		// Token: 0x040067ED RID: 26605
		public Storage.FetchCategory category;
	}

	// Token: 0x020012ED RID: 4845
	private class GlobalChoreProviderMultithreader : MultithreadedCollectChoreContext<GlobalChoreProvider>
	{
		// Token: 0x06008833 RID: 34867 RVA: 0x00348FF6 File Offset: 0x003471F6
		public override void CollectChore(int index, List<Chore.Precondition.Context> succeed, List<Chore.Precondition.Context> incomplete, List<Chore.Precondition.Context> failed)
		{
			this.provider.fetches[index].chore.CollectChoresFromGlobalChoreProvider(this.consumerState, succeed, incomplete, failed, false);
		}
	}

	// Token: 0x020012EE RID: 4846
	private class FetchComparer : IComparer<GlobalChoreProvider.Fetch>
	{
		// Token: 0x06008835 RID: 34869 RVA: 0x00349028 File Offset: 0x00347228
		public int Compare(GlobalChoreProvider.Fetch a, GlobalChoreProvider.Fetch b)
		{
			int num = b.priority.priority_class - a.priority.priority_class;
			if (num != 0)
			{
				return num;
			}
			int num2 = b.priority.priority_value - a.priority.priority_value;
			if (num2 != 0)
			{
				return num2;
			}
			return a.cost - b.cost;
		}
	}

	// Token: 0x020012EF RID: 4847
	private struct FindTopPriorityTask : IWorkItem<object>
	{
		// Token: 0x06008837 RID: 34871 RVA: 0x00349084 File Offset: 0x00347284
		public FindTopPriorityTask(int start, int end, List<Prioritizable> worldCollection)
		{
			this.start = start;
			this.end = end;
			this.worldCollection = worldCollection;
			this.found = false;
		}

		// Token: 0x06008838 RID: 34872 RVA: 0x003490A4 File Offset: 0x003472A4
		public void Run(object context, int threadIndex)
		{
			if (GlobalChoreProvider.FindTopPriorityTask.abort)
			{
				return;
			}
			int num = this.start;
			while (num != this.end && this.worldCollection.Count > num)
			{
				if (!(this.worldCollection[num] == null) && this.worldCollection[num].IsTopPriority())
				{
					this.found = true;
					break;
				}
				num++;
			}
			if (this.found)
			{
				GlobalChoreProvider.FindTopPriorityTask.abort = true;
			}
		}

		// Token: 0x040067EE RID: 26606
		private int start;

		// Token: 0x040067EF RID: 26607
		private int end;

		// Token: 0x040067F0 RID: 26608
		private List<Prioritizable> worldCollection;

		// Token: 0x040067F1 RID: 26609
		public bool found;

		// Token: 0x040067F2 RID: 26610
		public static bool abort;
	}
}
