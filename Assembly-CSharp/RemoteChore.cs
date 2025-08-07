using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000A8F RID: 2703
public class RemoteChore : WorkChore<RemoteWorkTerminal>
{
	// Token: 0x06004E80 RID: 20096 RVA: 0x001C6948 File Offset: 0x001C4B48
	public RemoteChore(RemoteWorkTerminal terminal)
		: base(Db.Get().ChoreTypes.RemoteOperate, terminal, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true)
	{
		this.terminal = terminal;
		this.AddPrecondition(RemoteChore.RemoteTerminalHasDock, terminal);
		this.AddPrecondition(RemoteChore.RemoteDockHasWorker, terminal);
		this.AddPrecondition(RemoteChore.RemoteDockAvailable, terminal);
		this.AddPrecondition(RemoteChore.RemoteChoreSubchorePreconditions, terminal);
		this.AddPrecondition(RemoteChore.RemoteDockOperational, terminal);
	}

	// Token: 0x06004E81 RID: 20097 RVA: 0x001C69C0 File Offset: 0x001C4BC0
	public override void CollectChores(ChoreConsumerState duplicantState, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		Chore.Precondition.Context context = new Chore.Precondition.Context(this, duplicantState, is_attempting_override, null);
		context.RunPreconditions();
		if (!context.IsComplete())
		{
			ListPool<Chore.Precondition.Context, Chore.Precondition>.PooledList pooledList = ListPool<Chore.Precondition.Context, Chore.Precondition>.Allocate();
			ListPool<Chore.Precondition.Context, Chore.Precondition>.PooledList pooledList2 = ListPool<Chore.Precondition.Context, Chore.Precondition>.Allocate();
			ListPool<Chore.Precondition.Context, Chore.Precondition>.PooledList pooledList3 = ListPool<Chore.Precondition.Context, Chore.Precondition>.Allocate();
			RemoteWorkerDock currentDock = this.terminal.CurrentDock;
			if (currentDock != null)
			{
				currentDock.CollectChores(duplicantState, pooledList, pooledList3, pooledList2, is_attempting_override);
			}
			foreach (Chore.Precondition.Context context2 in pooledList)
			{
				context.data = context2;
				context.SetPriority(context2.chore);
				incomplete_contexts.Add(context);
			}
			foreach (Chore.Precondition.Context context3 in pooledList3)
			{
				context.data = context3;
				context.SetPriority(context3.chore);
				incomplete_contexts.Add(context);
			}
			List<Chore.PreconditionInstance> preconditions = context.chore.GetPreconditions();
			context.failedPreconditionId = 0;
			while (context.failedPreconditionId < preconditions.Count && !(preconditions[context.failedPreconditionId].condition.id == RemoteChore.RemoteChoreSubchorePreconditions.id))
			{
				context.failedPreconditionId++;
			}
			foreach (Chore.Precondition.Context context4 in pooledList2)
			{
				context.data = context4;
				context.SetPriority(context4.chore);
				failed_contexts.Add(context);
			}
			pooledList.Recycle();
			pooledList2.Recycle();
			pooledList3.Recycle();
			return;
		}
		if (context.IsSuccess())
		{
			ListPool<Chore.Precondition.Context, Chore.Precondition>.PooledList pooledList4 = ListPool<Chore.Precondition.Context, Chore.Precondition>.Allocate();
			ListPool<Chore.Precondition.Context, Chore.Precondition>.PooledList pooledList5 = ListPool<Chore.Precondition.Context, Chore.Precondition>.Allocate();
			RemoteWorkerDock currentDock2 = this.terminal.CurrentDock;
			if (currentDock2 != null)
			{
				currentDock2.CollectChores(duplicantState, pooledList4, null, pooledList5, is_attempting_override);
			}
			foreach (Chore.Precondition.Context context5 in pooledList4)
			{
				context.data = context5;
				context.SetPriority(context5.chore);
				succeeded_contexts.Add(context);
			}
			foreach (Chore.Precondition.Context context6 in pooledList5)
			{
				context.data = context6;
				context.SetPriority(context6.chore);
				failed_contexts.Add(context);
			}
			pooledList4.Recycle();
			pooledList5.Recycle();
			return;
		}
		failed_contexts.Add(context);
	}

	// Token: 0x06004E82 RID: 20098 RVA: 0x001C6CA8 File Offset: 0x001C4EA8
	public override void PrepareChore(ref Chore.Precondition.Context context)
	{
		base.PrepareChore(ref context);
		DebugUtil.Assert(this.active_subchore == null);
		this.active_subchore = ((Chore.Precondition.Context)context.data).chore;
		RemoteWorkerDock currentDock = this.terminal.CurrentDock;
		if (currentDock == null)
		{
			return;
		}
		currentDock.SetNextChore(this.terminal, (Chore.Precondition.Context)context.data);
	}

	// Token: 0x06004E83 RID: 20099 RVA: 0x001C6D08 File Offset: 0x001C4F08
	protected override void End(string reason)
	{
		if (this.active_subchore != null && this.active_subchore.driver != null && !this.active_subchore.driver.HasChore())
		{
			this.active_subchore.Reserve(null);
		}
		this.active_subchore = null;
		base.End(reason);
		if (this.terminal.worker != null)
		{
			this.terminal.StopWork(this.terminal.worker, true);
		}
	}

	// Token: 0x0400341F RID: 13343
	private static Chore.Precondition RemoteTerminalHasDock = new Chore.Precondition
	{
		id = "RemoteDockAssigned",
		description = DUPLICANTS.CHORES.PRECONDITIONS.REMOTE_CHORE_NO_REMOTE_DOCK,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			return ((RemoteWorkTerminal)data).CurrentDock != null;
		},
		canExecuteOnAnyThread = true
	};

	// Token: 0x04003420 RID: 13344
	private static Chore.Precondition RemoteDockOperational = new Chore.Precondition
	{
		id = "RemoteDockOperational",
		description = DUPLICANTS.CHORES.PRECONDITIONS.REMOTE_CHORE_DOCK_INOPERABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			RemoteWorkTerminal remoteWorkTerminal = (RemoteWorkTerminal)data;
			return remoteWorkTerminal.CurrentDock != null && remoteWorkTerminal.CurrentDock.IsOperational;
		},
		canExecuteOnAnyThread = true
	};

	// Token: 0x04003421 RID: 13345
	private static Chore.Precondition RemoteDockHasWorker = new Chore.Precondition
	{
		id = "RemoteDockHasAvailableWorker",
		description = DUPLICANTS.CHORES.PRECONDITIONS.REMOTE_CHORE_NO_REMOTE_WORKER,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			RemoteWorkerDock currentDock = ((RemoteWorkTerminal)data).CurrentDock;
			return !(currentDock == null) && (currentDock.HasWorker() && currentDock.RemoteWorker.Available) && !currentDock.RemoteWorker.RequiresMaintnence;
		},
		canExecuteOnAnyThread = true
	};

	// Token: 0x04003422 RID: 13346
	private static Chore.Precondition RemoteDockAvailable = new Chore.Precondition
	{
		id = "RemoteDockAvailable",
		description = DUPLICANTS.CHORES.PRECONDITIONS.REMOTE_CHORE_DOCK_UNAVAILABLE,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			RemoteWorkTerminal remoteWorkTerminal2 = (RemoteWorkTerminal)data;
			RemoteWorkerDock currentDock2 = remoteWorkTerminal2.CurrentDock;
			return !(currentDock2 == null) && currentDock2.AvailableForWorkBy(remoteWorkTerminal2);
		},
		canExecuteOnAnyThread = true
	};

	// Token: 0x04003423 RID: 13347
	private static Chore.Precondition RemoteChoreSubchorePreconditions = new Chore.Precondition
	{
		id = "RemoteChorePreconditionsMet",
		description = DUPLICANTS.CHORES.PRECONDITIONS.REMOTE_CHORE_SUBCHORE_PRECONDITIONS,
		fn = delegate(ref Chore.Precondition.Context context, object data)
		{
			if (context.data == null)
			{
				return true;
			}
			Chore.Precondition.Context context2 = (Chore.Precondition.Context)context.data;
			if (context2.failedPreconditionId != -1)
			{
				return false;
			}
			context2.RunPreconditions();
			return context2.failedPreconditionId == -1;
		},
		canExecuteOnAnyThread = false
	};

	// Token: 0x04003424 RID: 13348
	private RemoteWorkTerminal terminal;

	// Token: 0x04003425 RID: 13349
	private Chore active_subchore;
}
