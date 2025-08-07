using System;
using System.Collections;

// Token: 0x0200045A RID: 1114
public class Promise<T> : IEnumerator
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06001759 RID: 5977 RVA: 0x000827E3 File Offset: 0x000809E3
	public bool IsResolved
	{
		get
		{
			return this.promise.IsResolved;
		}
	}

	// Token: 0x0600175A RID: 5978 RVA: 0x000827F0 File Offset: 0x000809F0
	public Promise(Action<Action<T>> fn)
	{
		fn(delegate(T value)
		{
			this.Resolve(value);
		});
	}

	// Token: 0x0600175B RID: 5979 RVA: 0x00082815 File Offset: 0x00080A15
	public Promise()
	{
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x00082828 File Offset: 0x00080A28
	public void EnsureResolved(T value)
	{
		this.result = value;
		this.promise.EnsureResolved();
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x0008283C File Offset: 0x00080A3C
	public void Resolve(T value)
	{
		this.result = value;
		this.promise.Resolve();
	}

	// Token: 0x0600175E RID: 5982 RVA: 0x00082850 File Offset: 0x00080A50
	public Promise<T> Then(Action<T> fn)
	{
		this.promise.Then(delegate
		{
			fn(this.result);
		});
		return this;
	}

	// Token: 0x0600175F RID: 5983 RVA: 0x0008288A File Offset: 0x00080A8A
	public Promise ThenWait(Func<Promise> fn)
	{
		return this.promise.ThenWait(fn);
	}

	// Token: 0x06001760 RID: 5984 RVA: 0x00082898 File Offset: 0x00080A98
	public Promise<T> ThenWait(Func<Promise<T>> fn)
	{
		return this.promise.ThenWait<T>(fn);
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06001761 RID: 5985 RVA: 0x000828A6 File Offset: 0x00080AA6
	object IEnumerator.Current
	{
		get
		{
			return null;
		}
	}

	// Token: 0x06001762 RID: 5986 RVA: 0x000828A9 File Offset: 0x00080AA9
	bool IEnumerator.MoveNext()
	{
		return !this.promise.IsResolved;
	}

	// Token: 0x06001763 RID: 5987 RVA: 0x000828B9 File Offset: 0x00080AB9
	void IEnumerator.Reset()
	{
	}

	// Token: 0x04000D9A RID: 3482
	private Promise promise = new Promise();

	// Token: 0x04000D9B RID: 3483
	private T result;
}
