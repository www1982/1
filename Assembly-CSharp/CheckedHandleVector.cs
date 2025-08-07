using System;
using System.Collections.Generic;

// Token: 0x02000809 RID: 2057
public class CheckedHandleVector<T> where T : new()
{
	// Token: 0x060037F5 RID: 14325 RVA: 0x0013685C File Offset: 0x00134A5C
	public CheckedHandleVector(int initial_size)
	{
		this.handleVector = new HandleVector<T>(initial_size);
		this.isFree = new List<bool>(initial_size);
		for (int i = 0; i < initial_size; i++)
		{
			this.isFree.Add(true);
		}
	}

	// Token: 0x060037F6 RID: 14326 RVA: 0x001368AC File Offset: 0x00134AAC
	public HandleVector<T>.Handle Add(T item, string debug_info)
	{
		HandleVector<T>.Handle handle = this.handleVector.Add(item);
		if (handle.index >= this.isFree.Count)
		{
			this.isFree.Add(false);
		}
		else
		{
			this.isFree[handle.index] = false;
		}
		int i = this.handleVector.Items.Count;
		while (i > this.debugInfo.Count)
		{
			this.debugInfo.Add(null);
		}
		this.debugInfo[handle.index] = debug_info;
		return handle;
	}

	// Token: 0x060037F7 RID: 14327 RVA: 0x0013693C File Offset: 0x00134B3C
	public T Release(HandleVector<T>.Handle handle)
	{
		if (this.isFree[handle.index])
		{
			DebugUtil.LogErrorArgs(new object[]
			{
				"Tried to double free checked handle ",
				handle.index,
				"- Debug info:",
				this.debugInfo[handle.index]
			});
		}
		this.isFree[handle.index] = true;
		return this.handleVector.Release(handle);
	}

	// Token: 0x060037F8 RID: 14328 RVA: 0x001369BB File Offset: 0x00134BBB
	public T Get(HandleVector<T>.Handle handle)
	{
		return this.handleVector.GetItem(handle);
	}

	// Token: 0x0400220D RID: 8717
	private HandleVector<T> handleVector;

	// Token: 0x0400220E RID: 8718
	private List<string> debugInfo = new List<string>();

	// Token: 0x0400220F RID: 8719
	private List<bool> isFree;
}
