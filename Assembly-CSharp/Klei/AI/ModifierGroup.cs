using System;
using System.Collections.Generic;

namespace Klei.AI
{
	// Token: 0x02001002 RID: 4098
	public class ModifierGroup<T> : Resource
	{
		// Token: 0x06007E5C RID: 32348 RVA: 0x00328DB4 File Offset: 0x00326FB4
		public IEnumerator<T> GetEnumerator()
		{
			return this.modifiers.GetEnumerator();
		}

		// Token: 0x170008EC RID: 2284
		public T this[int idx]
		{
			get
			{
				return this.modifiers[idx];
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06007E5E RID: 32350 RVA: 0x00328DD4 File Offset: 0x00326FD4
		public int Count
		{
			get
			{
				return this.modifiers.Count;
			}
		}

		// Token: 0x06007E5F RID: 32351 RVA: 0x00328DE1 File Offset: 0x00326FE1
		public ModifierGroup(string id, string name)
			: base(id, name)
		{
		}

		// Token: 0x06007E60 RID: 32352 RVA: 0x00328DF6 File Offset: 0x00326FF6
		public void Add(T modifier)
		{
			this.modifiers.Add(modifier);
		}

		// Token: 0x04005F58 RID: 24408
		public List<T> modifiers = new List<T>();
	}
}
