using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004B0 RID: 1200
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/ChoreProvider")]
public class ChoreProvider : KMonoBehaviour
{
	// Token: 0x1700009D RID: 157
	// (get) Token: 0x06001987 RID: 6535 RVA: 0x0008CB9F File Offset: 0x0008AD9F
	// (set) Token: 0x06001988 RID: 6536 RVA: 0x0008CBA7 File Offset: 0x0008ADA7
	public string Name { get; private set; }

	// Token: 0x06001989 RID: 6537 RVA: 0x0008CBB0 File Offset: 0x0008ADB0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Game.Instance.Subscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		Game.Instance.Subscribe(586301400, new Action<object>(this.OnMinionMigrated));
		Game.Instance.Subscribe(1142724171, new Action<object>(this.OnEntityMigrated));
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x0008CC1A File Offset: 0x0008AE1A
	protected override void OnSpawn()
	{
		if (ClusterManager.Instance != null)
		{
			ClusterManager.Instance.Subscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
		}
		base.OnSpawn();
		this.Name = base.name;
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x0008CC58 File Offset: 0x0008AE58
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.Unsubscribe(880851192, new Action<object>(this.OnWorldParentChanged));
		Game.Instance.Unsubscribe(586301400, new Action<object>(this.OnMinionMigrated));
		Game.Instance.Unsubscribe(1142724171, new Action<object>(this.OnEntityMigrated));
		if (ClusterManager.Instance != null)
		{
			ClusterManager.Instance.Unsubscribe(-1078710002, new Action<object>(this.OnWorldRemoved));
		}
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0008CCE8 File Offset: 0x0008AEE8
	protected virtual void OnWorldRemoved(object data)
	{
		int num = (int)data;
		int parentWorldId = ClusterManager.Instance.GetWorld(num).ParentWorldId;
		List<Chore> list;
		if (this.choreWorldMap.TryGetValue(parentWorldId, out list))
		{
			this.ClearWorldChores<Chore>(list, num);
		}
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x0008CD28 File Offset: 0x0008AF28
	protected virtual void OnWorldParentChanged(object data)
	{
		WorldParentChangedEventArgs worldParentChangedEventArgs = data as WorldParentChangedEventArgs;
		List<Chore> list;
		if (worldParentChangedEventArgs == null || worldParentChangedEventArgs.lastParentId == 255 || worldParentChangedEventArgs.lastParentId == worldParentChangedEventArgs.world.ParentWorldId || !this.choreWorldMap.TryGetValue(worldParentChangedEventArgs.lastParentId, out list))
		{
			return;
		}
		List<Chore> list2;
		if (!this.choreWorldMap.TryGetValue(worldParentChangedEventArgs.world.ParentWorldId, out list2))
		{
			list2 = (this.choreWorldMap[worldParentChangedEventArgs.world.ParentWorldId] = new List<Chore>());
		}
		this.TransferChores<Chore>(list, list2, worldParentChangedEventArgs.world.ParentWorldId);
	}

	// Token: 0x0600198E RID: 6542 RVA: 0x0008CDD0 File Offset: 0x0008AFD0
	protected virtual void OnEntityMigrated(object data)
	{
		MigrationEventArgs migrationEventArgs = data as MigrationEventArgs;
		List<Chore> list;
		if (migrationEventArgs == null || !(migrationEventArgs.entity == base.gameObject) || migrationEventArgs.prevWorldId == migrationEventArgs.targetWorldId || !this.choreWorldMap.TryGetValue(migrationEventArgs.prevWorldId, out list))
		{
			return;
		}
		List<Chore> list2;
		if (!this.choreWorldMap.TryGetValue(migrationEventArgs.targetWorldId, out list2))
		{
			list2 = (this.choreWorldMap[migrationEventArgs.targetWorldId] = new List<Chore>());
		}
		this.TransferChores<Chore>(list, list2, migrationEventArgs.targetWorldId);
	}

	// Token: 0x0600198F RID: 6543 RVA: 0x0008CE64 File Offset: 0x0008B064
	protected virtual void OnMinionMigrated(object data)
	{
		MinionMigrationEventArgs minionMigrationEventArgs = data as MinionMigrationEventArgs;
		List<Chore> list;
		if (minionMigrationEventArgs == null || !(minionMigrationEventArgs.minionId.gameObject == base.gameObject) || minionMigrationEventArgs.prevWorldId == minionMigrationEventArgs.targetWorldId || !this.choreWorldMap.TryGetValue(minionMigrationEventArgs.prevWorldId, out list))
		{
			return;
		}
		List<Chore> list2;
		if (!this.choreWorldMap.TryGetValue(minionMigrationEventArgs.targetWorldId, out list2))
		{
			list2 = (this.choreWorldMap[minionMigrationEventArgs.targetWorldId] = new List<Chore>());
		}
		this.TransferChores<Chore>(list, list2, minionMigrationEventArgs.targetWorldId);
	}

	// Token: 0x06001990 RID: 6544 RVA: 0x0008CF00 File Offset: 0x0008B100
	protected void TransferChores<T>(List<T> oldChores, List<T> newChores, int transferId) where T : Chore
	{
		int num = oldChores.Count - 1;
		for (int i = num; i >= 0; i--)
		{
			T t = oldChores[i];
			if (t.isNull)
			{
				DebugUtil.DevLogError(string.Concat(new string[]
				{
					"[",
					t.GetType().Name,
					"] ",
					t.GetReportName(null),
					" has no target"
				}));
			}
			else if (t.gameObject.GetMyParentWorldId() == transferId)
			{
				newChores.Add(t);
				oldChores[i] = oldChores[num];
				oldChores.RemoveAt(num--);
			}
		}
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x0008CFBC File Offset: 0x0008B1BC
	protected void ClearWorldChores<T>(List<T> chores, int worldId) where T : Chore
	{
		int num = chores.Count - 1;
		for (int i = num; i >= 0; i--)
		{
			if (chores[i].gameObject.GetMyWorldId() == worldId)
			{
				chores[i] = chores[num];
				chores.RemoveAt(num--);
			}
		}
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x0008D010 File Offset: 0x0008B210
	public virtual void AddChore(Chore chore)
	{
		chore.provider = this;
		List<Chore> list = null;
		int myParentWorldId = chore.gameObject.GetMyParentWorldId();
		if (!this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			list = (this.choreWorldMap[myParentWorldId] = new List<Chore>());
		}
		list.Add(chore);
	}

	// Token: 0x06001993 RID: 6547 RVA: 0x0008D05C File Offset: 0x0008B25C
	public virtual void RemoveChore(Chore chore)
	{
		if (chore == null)
		{
			return;
		}
		chore.provider = null;
		List<Chore> list = null;
		int myParentWorldId = chore.gameObject.GetMyParentWorldId();
		if (this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			list.Remove(chore);
		}
	}

	// Token: 0x06001994 RID: 6548 RVA: 0x0008D09C File Offset: 0x0008B29C
	public virtual void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded, List<Chore.Precondition.Context> failed_contexts)
	{
		List<Chore> list = null;
		int myParentWorldId = consumer_state.gameObject.GetMyParentWorldId();
		if (!this.choreWorldMap.TryGetValue(myParentWorldId, out list))
		{
			return;
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i].provider == null)
			{
				list[i].Cancel("no provider");
				list[i] = list[list.Count - 1];
				list.RemoveAt(list.Count - 1);
			}
		}
		int num = 48;
		if (list.Count > num)
		{
			ChoreProvider.batch_context.Setup(list, consumer_state);
			ChoreProvider.batch_work_items.Reset(ChoreProvider.batch_context);
			for (int j = 0; j < list.Count; j += 16)
			{
				ChoreProvider.batch_work_items.Add(new MultithreadedCollectChoreContext<List<Chore>>.WorkBlock<ChoreProvider.ChoreProviderCollectContext>(j, Math.Min(j + 16, list.Count)));
			}
			GlobalJobManager.Run(ChoreProvider.batch_work_items);
			ChoreProvider.batch_context.Finish(succeeded, failed_contexts);
			return;
		}
		foreach (Chore chore in list)
		{
			chore.CollectChores(consumer_state, succeeded, failed_contexts, false);
		}
	}

	// Token: 0x04000EB3 RID: 3763
	public Dictionary<int, List<Chore>> choreWorldMap = new Dictionary<int, List<Chore>>();

	// Token: 0x04000EB4 RID: 3764
	private static ChoreProvider.ChoreProviderCollectContext batch_context = new ChoreProvider.ChoreProviderCollectContext();

	// Token: 0x04000EB5 RID: 3765
	private static WorkItemCollection<MultithreadedCollectChoreContext<List<Chore>>.WorkBlock<ChoreProvider.ChoreProviderCollectContext>, ChoreProvider.ChoreProviderCollectContext> batch_work_items = new WorkItemCollection<MultithreadedCollectChoreContext<List<Chore>>.WorkBlock<ChoreProvider.ChoreProviderCollectContext>, ChoreProvider.ChoreProviderCollectContext>();

	// Token: 0x020012E7 RID: 4839
	private class ChoreProviderCollectContext : MultithreadedCollectChoreContext<List<Chore>>
	{
		// Token: 0x06008822 RID: 34850 RVA: 0x00348AE3 File Offset: 0x00346CE3
		public override void CollectChore(int index, List<Chore.Precondition.Context> succeed, List<Chore.Precondition.Context> incomplete, List<Chore.Precondition.Context> failed)
		{
			this.provider[index].CollectChores(this.consumerState, succeed, incomplete, failed, false);
		}
	}
}
