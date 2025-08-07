using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000450 RID: 1104
public class ListWithEvents<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	// Token: 0x17000060 RID: 96
	// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00081B0A File Offset: 0x0007FD0A
	public int Count
	{
		get
		{
			return this.internalList.Count;
		}
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00081B17 File Offset: 0x0007FD17
	public bool IsReadOnly
	{
		get
		{
			return ((ICollection<T>)this.internalList).IsReadOnly;
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060016F3 RID: 5875 RVA: 0x00081B24 File Offset: 0x0007FD24
	// (remove) Token: 0x060016F4 RID: 5876 RVA: 0x00081B5C File Offset: 0x0007FD5C
	public event Action<T> onAdd;

	// Token: 0x14000003 RID: 3
	// (add) Token: 0x060016F5 RID: 5877 RVA: 0x00081B94 File Offset: 0x0007FD94
	// (remove) Token: 0x060016F6 RID: 5878 RVA: 0x00081BCC File Offset: 0x0007FDCC
	public event Action<T> onRemove;

	// Token: 0x17000062 RID: 98
	public T this[int index]
	{
		get
		{
			return this.internalList[index];
		}
		set
		{
			this.internalList[index] = value;
		}
	}

	// Token: 0x060016F9 RID: 5881 RVA: 0x00081C1E File Offset: 0x0007FE1E
	public void Add(T item)
	{
		this.internalList.Add(item);
		if (this.onAdd != null)
		{
			this.onAdd(item);
		}
	}

	// Token: 0x060016FA RID: 5882 RVA: 0x00081C40 File Offset: 0x0007FE40
	public void Insert(int index, T item)
	{
		this.internalList.Insert(index, item);
		if (this.onAdd != null)
		{
			this.onAdd(item);
		}
	}

	// Token: 0x060016FB RID: 5883 RVA: 0x00081C64 File Offset: 0x0007FE64
	public void RemoveAt(int index)
	{
		T t = this.internalList[index];
		this.internalList.RemoveAt(index);
		if (this.onRemove != null)
		{
			this.onRemove(t);
		}
	}

	// Token: 0x060016FC RID: 5884 RVA: 0x00081C9E File Offset: 0x0007FE9E
	public bool Remove(T item)
	{
		bool flag = this.internalList.Remove(item);
		if (flag && this.onRemove != null)
		{
			this.onRemove(item);
		}
		return flag;
	}

	// Token: 0x060016FD RID: 5885 RVA: 0x00081CC3 File Offset: 0x0007FEC3
	public void Clear()
	{
		while (this.Count > 0)
		{
			this.RemoveAt(0);
		}
	}

	// Token: 0x060016FE RID: 5886 RVA: 0x00081CD7 File Offset: 0x0007FED7
	public int IndexOf(T item)
	{
		return this.internalList.IndexOf(item);
	}

	// Token: 0x060016FF RID: 5887 RVA: 0x00081CE5 File Offset: 0x0007FEE5
	public void CopyTo(T[] array, int arrayIndex)
	{
		this.internalList.CopyTo(array, arrayIndex);
	}

	// Token: 0x06001700 RID: 5888 RVA: 0x00081CF4 File Offset: 0x0007FEF4
	public bool Contains(T item)
	{
		return this.internalList.Contains(item);
	}

	// Token: 0x06001701 RID: 5889 RVA: 0x00081D02 File Offset: 0x0007FF02
	public IEnumerator<T> GetEnumerator()
	{
		return this.internalList.GetEnumerator();
	}

	// Token: 0x06001702 RID: 5890 RVA: 0x00081D14 File Offset: 0x0007FF14
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.internalList.GetEnumerator();
	}

	// Token: 0x04000D87 RID: 3463
	private List<T> internalList = new List<T>();
}
