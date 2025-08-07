using System;
using System.Collections.Generic;

// Token: 0x0200045F RID: 1119
public class StringSearchableList<T>
{
	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06001773 RID: 6003 RVA: 0x00082A91 File Offset: 0x00080C91
	// (set) Token: 0x06001774 RID: 6004 RVA: 0x00082A99 File Offset: 0x00080C99
	public bool didUseFilter { get; private set; }

	// Token: 0x06001775 RID: 6005 RVA: 0x00082AA2 File Offset: 0x00080CA2
	public StringSearchableList(List<T> allValues, StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn)
	{
		this.allValues = allValues;
		this.shouldFilterOutFn = shouldFilterOutFn;
		this.filteredValues = new List<T>();
	}

	// Token: 0x06001776 RID: 6006 RVA: 0x00082ACE File Offset: 0x00080CCE
	public StringSearchableList(StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn)
	{
		this.shouldFilterOutFn = shouldFilterOutFn;
		this.allValues = new List<T>();
		this.filteredValues = new List<T>();
	}

	// Token: 0x06001777 RID: 6007 RVA: 0x00082B00 File Offset: 0x00080D00
	public void Refilter()
	{
		if (StringSearchableListUtil.ShouldUseFilter(this.filter))
		{
			this.filteredValues.Clear();
			foreach (T t in this.allValues)
			{
				if (!this.shouldFilterOutFn(t, in this.filter))
				{
					this.filteredValues.Add(t);
				}
			}
			this.didUseFilter = true;
			return;
		}
		if (this.filteredValues.Count != this.allValues.Count)
		{
			this.filteredValues.Clear();
			this.filteredValues.AddRange(this.allValues);
		}
		this.didUseFilter = false;
	}

	// Token: 0x04000D9E RID: 3486
	public string filter = "";

	// Token: 0x04000D9F RID: 3487
	public List<T> allValues;

	// Token: 0x04000DA0 RID: 3488
	public List<T> filteredValues;

	// Token: 0x04000DA2 RID: 3490
	public readonly StringSearchableList<T>.ShouldFilterOutFn shouldFilterOutFn;

	// Token: 0x02001238 RID: 4664
	// (Invoke) Token: 0x06008572 RID: 34162
	public delegate bool ShouldFilterOutFn(T candidateValue, in string filter);
}
