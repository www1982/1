using System;
using System.Collections.Generic;

// Token: 0x020004A9 RID: 1193
public abstract class MultithreadedCollectChoreContext<ProviderType>
{
	// Token: 0x060018FD RID: 6397 RVA: 0x0008ACA7 File Offset: 0x00088EA7
	public MultithreadedCollectChoreContext()
	{
	}

	// Token: 0x060018FE RID: 6398 RVA: 0x0008ACAF File Offset: 0x00088EAF
	public void Setup(ProviderType provider, ChoreConsumerState consumerState)
	{
		this.provider = provider;
		this.consumerState = consumerState;
		if (this.succeeded == null || this.succeeded.Length != GlobalJobManager.ThreadCount)
		{
			this.SetupThreadContext();
		}
	}

	// Token: 0x060018FF RID: 6399 RVA: 0x0008ACDC File Offset: 0x00088EDC
	private void SetupThreadContext()
	{
		if (this.succeeded != null)
		{
			this.TearDownThreadContext();
		}
		int threadCount = GlobalJobManager.ThreadCount;
		this.succeeded = new ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[threadCount];
		this.failed = new ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[threadCount];
		this.incomplete = new ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[threadCount];
		for (int i = 0; i < threadCount; i++)
		{
			this.succeeded[i] = ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.Allocate();
			this.failed[i] = ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.Allocate();
			this.incomplete[i] = ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.Allocate();
		}
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x0008AD54 File Offset: 0x00088F54
	private void TearDownThreadContext()
	{
		int threadCount = GlobalJobManager.ThreadCount;
		for (int i = 0; i < threadCount; i++)
		{
			this.succeeded[i].Recycle();
			this.failed[i].Recycle();
			this.incomplete[i].Recycle();
		}
		this.succeeded = null;
		this.failed = null;
		this.incomplete = null;
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x0008ADB0 File Offset: 0x00088FB0
	public void Finish(List<Chore.Precondition.Context> pass, List<Chore.Precondition.Context> fail)
	{
		int threadCount = GlobalJobManager.ThreadCount;
		for (int i = 0; i < threadCount; i++)
		{
			pass.AddRange(this.succeeded[i]);
			this.succeeded[i].Clear();
			fail.AddRange(this.failed[i]);
			this.failed[i].Clear();
			foreach (Chore.Precondition.Context context in this.incomplete[i])
			{
				context.FinishPreconditions();
				if (context.IsSuccess())
				{
					pass.Add(context);
				}
				else
				{
					fail.Add(context);
				}
			}
			this.incomplete[i].Clear();
		}
	}

	// Token: 0x06001902 RID: 6402
	public abstract void CollectChore(int index, List<Chore.Precondition.Context> succeed, List<Chore.Precondition.Context> incomplete, List<Chore.Precondition.Context> failed);

	// Token: 0x06001903 RID: 6403 RVA: 0x0008AE7C File Offset: 0x0008907C
	public void DefaultCollectChore(int index, int threadIndex)
	{
		this.CollectChore(index, this.succeeded[threadIndex], this.incomplete[threadIndex], this.failed[threadIndex]);
	}

	// Token: 0x04000E65 RID: 3685
	public ProviderType provider;

	// Token: 0x04000E66 RID: 3686
	public ChoreConsumerState consumerState;

	// Token: 0x04000E67 RID: 3687
	public ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[] succeeded;

	// Token: 0x04000E68 RID: 3688
	public ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[] failed;

	// Token: 0x04000E69 RID: 3689
	public ListPool<Chore.Precondition.Context, MultithreadedCollectChoreContext<ProviderType>>.PooledList[] incomplete;

	// Token: 0x020012E0 RID: 4832
	public struct WorkBlock<Parent> : IWorkItem<Parent> where Parent : MultithreadedCollectChoreContext<ProviderType>
	{
		// Token: 0x06008808 RID: 34824 RVA: 0x00348589 File Offset: 0x00346789
		public WorkBlock(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		// Token: 0x06008809 RID: 34825 RVA: 0x0034859C File Offset: 0x0034679C
		void IWorkItem<Parent>.Run(Parent shared_data, int threadIndex)
		{
			for (int i = this.start; i < this.end; i++)
			{
				shared_data.DefaultCollectChore(i, threadIndex);
			}
		}

		// Token: 0x040067CB RID: 26571
		private int start;

		// Token: 0x040067CC RID: 26572
		private int end;
	}
}
