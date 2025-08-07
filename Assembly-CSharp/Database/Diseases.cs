using System;
using System.Collections.Generic;
using Klei.AI;

namespace Database
{
	// Token: 0x02000EEB RID: 3819
	public class Diseases : ResourceSet<Disease>
	{
		// Token: 0x06007975 RID: 31093 RVA: 0x002F993C File Offset: 0x002F7B3C
		public Diseases(ResourceSet parent, bool statsOnly = false)
			: base("Diseases", parent)
		{
			this.FoodGerms = base.Add(new FoodGerms(statsOnly));
			this.SlimeGerms = base.Add(new SlimeGerms(statsOnly));
			this.PollenGerms = base.Add(new PollenGerms(statsOnly));
			this.ZombieSpores = base.Add(new ZombieSpores(statsOnly));
			if (DlcManager.FeatureRadiationEnabled())
			{
				this.RadiationPoisoning = base.Add(new RadiationPoisoning(statsOnly));
			}
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x002F99B8 File Offset: 0x002F7BB8
		public bool IsValidID(string id)
		{
			bool flag = false;
			using (List<Disease>.Enumerator enumerator = this.resources.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Id == id)
					{
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x002F9A18 File Offset: 0x002F7C18
		public byte GetIndex(int hash)
		{
			byte b = 0;
			while ((int)b < this.resources.Count)
			{
				Disease disease = this.resources[(int)b];
				if (hash == disease.id.GetHashCode())
				{
					return b;
				}
				b += 1;
			}
			return byte.MaxValue;
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x002F9A64 File Offset: 0x002F7C64
		public byte GetIndex(HashedString id)
		{
			return this.GetIndex(id.GetHashCode());
		}

		// Token: 0x040056E3 RID: 22243
		public Disease FoodGerms;

		// Token: 0x040056E4 RID: 22244
		public Disease SlimeGerms;

		// Token: 0x040056E5 RID: 22245
		public Disease PollenGerms;

		// Token: 0x040056E6 RID: 22246
		public Disease ZombieSpores;

		// Token: 0x040056E7 RID: 22247
		public Disease RadiationPoisoning;
	}
}
