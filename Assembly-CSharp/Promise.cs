using System;
using System.Collections;

// Token: 0x02000459 RID: 1113
public class Promise : IEnumerator
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06001748 RID: 5960 RVA: 0x000825E4 File Offset: 0x000807E4
	public bool IsResolved
	{
		get
		{
			return this.m_is_resolved;
		}
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x000825EC File Offset: 0x000807EC
	public Promise(Action<global::System.Action> fn)
	{
		fn(delegate
		{
			this.Resolve();
		});
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x00082606 File Offset: 0x00080806
	public Promise()
	{
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x0008260E File Offset: 0x0008080E
	public void EnsureResolved()
	{
		if (this.IsResolved)
		{
			return;
		}
		this.Resolve();
	}

	// Token: 0x0600174C RID: 5964 RVA: 0x0008261F File Offset: 0x0008081F
	public void Resolve()
	{
		DebugUtil.Assert(!this.m_is_resolved, "Can only resolve a promise once");
		this.m_is_resolved = true;
		if (this.on_complete != null)
		{
			this.on_complete();
			this.on_complete = null;
		}
	}

	// Token: 0x0600174D RID: 5965 RVA: 0x00082655 File Offset: 0x00080855
	public Promise Then(global::System.Action callback)
	{
		if (this.m_is_resolved)
		{
			callback();
		}
		else
		{
			this.on_complete = (global::System.Action)Delegate.Combine(this.on_complete, callback);
		}
		return this;
	}

	// Token: 0x0600174E RID: 5966 RVA: 0x00082680 File Offset: 0x00080880
	public Promise ThenWait(Func<Promise> callback)
	{
		if (this.m_is_resolved)
		{
			return callback();
		}
		return new Promise(delegate(global::System.Action resolve)
		{
			this.on_complete = (global::System.Action)Delegate.Combine(this.on_complete, new global::System.Action(delegate
			{
				callback().Then(resolve);
			}));
		});
	}

	// Token: 0x0600174F RID: 5967 RVA: 0x000826C8 File Offset: 0x000808C8
	public Promise<T> ThenWait<T>(Func<Promise<T>> callback)
	{
		if (this.m_is_resolved)
		{
			return callback();
		}
		return new Promise<T>(delegate(Action<T> resolve)
		{
			this.on_complete = (global::System.Action)Delegate.Combine(this.on_complete, new global::System.Action(delegate
			{
				callback().Then(resolve);
			}));
		});
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06001750 RID: 5968 RVA: 0x0008270E File Offset: 0x0008090E
	object IEnumerator.Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06001751 RID: 5969 RVA: 0x00082711 File Offset: 0x00080911
	bool IEnumerator.MoveNext()
	{
		return !this.IsResolved;
	}

	// Token: 0x06001752 RID: 5970 RVA: 0x0008271C File Offset: 0x0008091C
	void IEnumerator.Reset()
	{
	}

	// Token: 0x06001753 RID: 5971 RVA: 0x0008271E File Offset: 0x0008091E
	static Promise()
	{
		Promise.m_instant.Resolve();
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06001754 RID: 5972 RVA: 0x00082734 File Offset: 0x00080934
	public static Promise Instant
	{
		get
		{
			return Promise.m_instant;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06001755 RID: 5973 RVA: 0x0008273B File Offset: 0x0008093B
	public static Promise Fail
	{
		get
		{
			return new Promise();
		}
	}

	// Token: 0x06001756 RID: 5974 RVA: 0x00082744 File Offset: 0x00080944
	public static Promise All(params Promise[] promises)
	{
		Promise.<>c__DisplayClass21_0 CS$<>8__locals1 = new Promise.<>c__DisplayClass21_0();
		CS$<>8__locals1.promises = promises;
		if (CS$<>8__locals1.promises == null || CS$<>8__locals1.promises.Length == 0)
		{
			return Promise.Instant;
		}
		CS$<>8__locals1.all_resolved_promise = new Promise();
		Promise[] promises2 = CS$<>8__locals1.promises;
		for (int i = 0; i < promises2.Length; i++)
		{
			promises2[i].Then(new global::System.Action(CS$<>8__locals1.<All>g__TryResolve|0));
		}
		return CS$<>8__locals1.all_resolved_promise;
	}

	// Token: 0x06001757 RID: 5975 RVA: 0x000827B0 File Offset: 0x000809B0
	public static Promise Chain(params Func<Promise>[] make_promise_fns)
	{
		Promise.<>c__DisplayClass22_0 CS$<>8__locals1 = new Promise.<>c__DisplayClass22_0();
		CS$<>8__locals1.make_promise_fns = make_promise_fns;
		CS$<>8__locals1.all_resolve_promise = new Promise();
		CS$<>8__locals1.current_promise_fn_index = 0;
		CS$<>8__locals1.<Chain>g__TryNext|0();
		return CS$<>8__locals1.all_resolve_promise;
	}

	// Token: 0x04000D97 RID: 3479
	private global::System.Action on_complete;

	// Token: 0x04000D98 RID: 3480
	private bool m_is_resolved;

	// Token: 0x04000D99 RID: 3481
	private static Promise m_instant = new Promise();
}
