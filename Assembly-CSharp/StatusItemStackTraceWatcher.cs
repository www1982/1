using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000683 RID: 1667
public class StatusItemStackTraceWatcher : IDisposable
{
	// Token: 0x060028D8 RID: 10456 RVA: 0x000ED766 File Offset: 0x000EB966
	public bool GetShouldWatch()
	{
		return this.shouldWatch;
	}

	// Token: 0x060028D9 RID: 10457 RVA: 0x000ED76E File Offset: 0x000EB96E
	public void SetShouldWatch(bool shouldWatch)
	{
		if (this.shouldWatch == shouldWatch)
		{
			return;
		}
		this.shouldWatch = shouldWatch;
		this.Refresh();
	}

	// Token: 0x060028DA RID: 10458 RVA: 0x000ED787 File Offset: 0x000EB987
	public Option<StatusItemGroup> GetTarget()
	{
		return this.currentTarget;
	}

	// Token: 0x060028DB RID: 10459 RVA: 0x000ED790 File Offset: 0x000EB990
	public void SetTarget(Option<StatusItemGroup> nextTarget)
	{
		if (this.currentTarget.IsNone() && nextTarget.IsNone())
		{
			return;
		}
		if (this.currentTarget.IsSome() && nextTarget.IsSome() && this.currentTarget.Unwrap() == nextTarget.Unwrap())
		{
			return;
		}
		this.currentTarget = nextTarget;
		this.Refresh();
	}

	// Token: 0x060028DC RID: 10460 RVA: 0x000ED7EC File Offset: 0x000EB9EC
	private void Refresh()
	{
		if (this.onCleanup != null)
		{
			global::System.Action action = this.onCleanup;
			if (action != null)
			{
				action();
			}
			this.onCleanup = null;
		}
		if (!this.shouldWatch)
		{
			return;
		}
		if (this.currentTarget.IsSome())
		{
			StatusItemGroup target = this.currentTarget.Unwrap();
			Action<StatusItemGroup.Entry, StatusItemCategory> onAddStatusItem = delegate(StatusItemGroup.Entry entry, StatusItemCategory category)
			{
				this.entryIdToStackTraceMap[entry.id] = new StackTrace(true);
			};
			StatusItemGroup target3 = target;
			target3.OnAddStatusItem = (Action<StatusItemGroup.Entry, StatusItemCategory>)Delegate.Combine(target3.OnAddStatusItem, onAddStatusItem);
			this.onCleanup = (global::System.Action)Delegate.Combine(this.onCleanup, new global::System.Action(delegate
			{
				StatusItemGroup target2 = target;
				target2.OnAddStatusItem = (Action<StatusItemGroup.Entry, StatusItemCategory>)Delegate.Remove(target2.OnAddStatusItem, onAddStatusItem);
			}));
			StatusItemStackTraceWatcher.StatusItemStackTraceWatcher_OnDestroyListenerMB destroyListener = this.currentTarget.Unwrap().gameObject.AddOrGet<StatusItemStackTraceWatcher.StatusItemStackTraceWatcher_OnDestroyListenerMB>();
			destroyListener.owner = this;
			this.onCleanup = (global::System.Action)Delegate.Combine(this.onCleanup, new global::System.Action(delegate
			{
				if (destroyListener.IsNullOrDestroyed())
				{
					return;
				}
				global::UnityEngine.Object.Destroy(destroyListener);
			}));
			this.onCleanup = (global::System.Action)Delegate.Combine(this.onCleanup, new global::System.Action(delegate
			{
				this.entryIdToStackTraceMap.Clear();
			}));
		}
	}

	// Token: 0x060028DD RID: 10461 RVA: 0x000ED909 File Offset: 0x000EBB09
	public bool GetStackTraceForEntry(StatusItemGroup.Entry entry, out StackTrace stackTrace)
	{
		return this.entryIdToStackTraceMap.TryGetValue(entry.id, out stackTrace);
	}

	// Token: 0x060028DE RID: 10462 RVA: 0x000ED91D File Offset: 0x000EBB1D
	public void Dispose()
	{
		if (this.onCleanup != null)
		{
			global::System.Action action = this.onCleanup;
			if (action != null)
			{
				action();
			}
			this.onCleanup = null;
		}
	}

	// Token: 0x04001820 RID: 6176
	private Dictionary<Guid, StackTrace> entryIdToStackTraceMap = new Dictionary<Guid, StackTrace>();

	// Token: 0x04001821 RID: 6177
	private Option<StatusItemGroup> currentTarget;

	// Token: 0x04001822 RID: 6178
	private bool shouldWatch;

	// Token: 0x04001823 RID: 6179
	private global::System.Action onCleanup;

	// Token: 0x02001500 RID: 5376
	public class StatusItemStackTraceWatcher_OnDestroyListenerMB : MonoBehaviour
	{
		// Token: 0x06008FA0 RID: 36768 RVA: 0x0035DDD4 File Offset: 0x0035BFD4
		private void OnDestroy()
		{
			bool flag = this.owner != null;
			bool flag2 = this.owner.currentTarget.IsSome() && this.owner.currentTarget.Unwrap().gameObject == base.gameObject;
			if (flag && flag2)
			{
				this.owner.SetTarget(Option.None);
			}
		}

		// Token: 0x04006E85 RID: 28293
		public StatusItemStackTraceWatcher owner;
	}
}
