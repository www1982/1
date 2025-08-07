using System;
using System.Collections.Generic;

// Token: 0x020004B4 RID: 1204
internal class ClearableManager
{
	// Token: 0x060019B3 RID: 6579 RVA: 0x0008DA80 File Offset: 0x0008BC80
	public HandleVector<int>.Handle RegisterClearable(Clearable clearable)
	{
		return this.markedClearables.Allocate(new ClearableManager.MarkedClearable
		{
			clearable = clearable,
			pickupable = clearable.GetComponent<Pickupable>(),
			prioritizable = clearable.GetComponent<Prioritizable>()
		});
	}

	// Token: 0x060019B4 RID: 6580 RVA: 0x0008DAC3 File Offset: 0x0008BCC3
	public void UnregisterClearable(HandleVector<int>.Handle handle)
	{
		this.markedClearables.Free(handle);
	}

	// Token: 0x060019B5 RID: 6581 RVA: 0x0008DAD4 File Offset: 0x0008BCD4
	public void CollectAndSortClearables(Navigator navigator)
	{
		this.sortedClearables.Clear();
		foreach (ClearableManager.MarkedClearable markedClearable in this.markedClearables.GetDataList())
		{
			int navigationCost = markedClearable.pickupable.GetNavigationCost(navigator, markedClearable.pickupable.cachedCell);
			if (navigationCost != -1)
			{
				this.sortedClearables.Add(new ClearableManager.SortedClearable
				{
					pickupable = markedClearable.pickupable,
					masterPriority = markedClearable.prioritizable.GetMasterPriority(),
					cost = navigationCost
				});
			}
		}
		this.sortedClearables.Sort(ClearableManager.SortedClearable.comparer);
	}

	// Token: 0x060019B6 RID: 6582 RVA: 0x0008DB98 File Offset: 0x0008BD98
	public void CollectChores(List<GlobalChoreProvider.Fetch> fetches, ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		ChoreType transport = Db.Get().ChoreTypes.Transport;
		int personalPriority = consumer_state.consumer.GetPersonalPriority(transport);
		int num = (Game.Instance.advancedPersonalPriorities ? transport.explicitPriority : transport.priority);
		bool flag = false;
		for (int i = 0; i < this.sortedClearables.Count; i++)
		{
			ClearableManager.SortedClearable sortedClearable = this.sortedClearables[i];
			Pickupable pickupable = sortedClearable.pickupable;
			PrioritySetting masterPriority = sortedClearable.masterPriority;
			Chore.Precondition.Context context = default(Chore.Precondition.Context);
			context.personalPriority = personalPriority;
			KPrefabID kprefabID = pickupable.KPrefabID;
			int num2 = 0;
			while (fetches != null && num2 < fetches.Count)
			{
				GlobalChoreProvider.Fetch fetch = fetches[num2];
				if ((fetch.chore.criteria == FetchChore.MatchCriteria.MatchID && fetch.chore.tags.Contains(kprefabID.PrefabTag)) || (fetch.chore.criteria == FetchChore.MatchCriteria.MatchTags && kprefabID.HasTag(fetch.chore.tagsFirst)))
				{
					context.Set(fetch.chore, consumer_state, false, pickupable);
					context.choreTypeForPermission = transport;
					context.RunPreconditions();
					if (context.IsSuccess())
					{
						context.masterPriority = masterPriority;
						context.priority = num;
						context.interruptPriority = transport.interruptPriority;
						succeeded.Add(context);
						flag = true;
						break;
					}
				}
				num2++;
			}
			if (flag)
			{
				break;
			}
		}
	}

	// Token: 0x04000EC4 RID: 3780
	private KCompactedVector<ClearableManager.MarkedClearable> markedClearables = new KCompactedVector<ClearableManager.MarkedClearable>(0);

	// Token: 0x04000EC5 RID: 3781
	private List<ClearableManager.SortedClearable> sortedClearables = new List<ClearableManager.SortedClearable>();

	// Token: 0x020012F0 RID: 4848
	private struct MarkedClearable
	{
		// Token: 0x040067F3 RID: 26611
		public Clearable clearable;

		// Token: 0x040067F4 RID: 26612
		public Pickupable pickupable;

		// Token: 0x040067F5 RID: 26613
		public Prioritizable prioritizable;
	}

	// Token: 0x020012F1 RID: 4849
	private struct SortedClearable
	{
		// Token: 0x040067F6 RID: 26614
		public Pickupable pickupable;

		// Token: 0x040067F7 RID: 26615
		public PrioritySetting masterPriority;

		// Token: 0x040067F8 RID: 26616
		public int cost;

		// Token: 0x040067F9 RID: 26617
		public static ClearableManager.SortedClearable.Comparer comparer = new ClearableManager.SortedClearable.Comparer();

		// Token: 0x02002693 RID: 9875
		public class Comparer : IComparer<ClearableManager.SortedClearable>
		{
			// Token: 0x0600C436 RID: 50230 RVA: 0x0040D328 File Offset: 0x0040B528
			public int Compare(ClearableManager.SortedClearable a, ClearableManager.SortedClearable b)
			{
				int num = b.masterPriority.priority_value - a.masterPriority.priority_value;
				if (num == 0)
				{
					return a.cost - b.cost;
				}
				return num;
			}
		}
	}
}
