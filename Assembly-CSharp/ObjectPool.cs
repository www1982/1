using System;
using System.Collections.Generic;

// Token: 0x02000451 RID: 1105
public class ObjectPool<T>
{
	// Token: 0x06001704 RID: 5892 RVA: 0x00081D3C File Offset: 0x0007FF3C
	public ObjectPool(Func<T> instantiator, int initial_count = 0)
	{
		this.instantiator = instantiator;
		this.unused = new Stack<T>(initial_count);
		for (int i = 0; i < initial_count; i++)
		{
			this.unused.Push(instantiator());
		}
	}

	// Token: 0x06001705 RID: 5893 RVA: 0x00081D80 File Offset: 0x0007FF80
	public virtual T GetInstance()
	{
		T t = default(T);
		if (this.unused.Count > 0)
		{
			t = this.unused.Pop();
		}
		else
		{
			t = this.instantiator();
		}
		return t;
	}

	// Token: 0x06001706 RID: 5894 RVA: 0x00081DBE File Offset: 0x0007FFBE
	public void ReleaseInstance(T instance)
	{
		if (object.Equals(instance, null))
		{
			return;
		}
		this.unused.Push(instance);
	}

	// Token: 0x04000D8A RID: 3466
	protected Stack<T> unused;

	// Token: 0x04000D8B RID: 3467
	protected Func<T> instantiator;
}
