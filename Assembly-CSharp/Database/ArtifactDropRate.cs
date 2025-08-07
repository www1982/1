using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000ED5 RID: 3797
	public class ArtifactDropRate : Resource
	{
		// Token: 0x0600792C RID: 31020 RVA: 0x002ED826 File Offset: 0x002EBA26
		public void AddItem(ArtifactTier tier, float weight)
		{
			this.rates.Add(new global::Tuple<ArtifactTier, float>(tier, weight));
			this.totalWeight += weight;
		}

		// Token: 0x0600792D RID: 31021 RVA: 0x002ED848 File Offset: 0x002EBA48
		public float GetTierWeight(ArtifactTier tier)
		{
			float num = 0f;
			foreach (global::Tuple<ArtifactTier, float> tuple in this.rates)
			{
				if (tuple.first == tier)
				{
					num = tuple.second;
				}
			}
			return num;
		}

		// Token: 0x0400542A RID: 21546
		public List<global::Tuple<ArtifactTier, float>> rates = new List<global::Tuple<ArtifactTier, float>>();

		// Token: 0x0400542B RID: 21547
		public float totalWeight;
	}
}
