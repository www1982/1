using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001018 RID: 4120
	public class CompositeExposureRule
	{
		// Token: 0x06007EF9 RID: 32505 RVA: 0x0032B552 File Offset: 0x00329752
		public string Name()
		{
			return this.name;
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x0032B55A File Offset: 0x0032975A
		public void Overlay(ExposureRule rule)
		{
			if (rule.populationHalfLife != null)
			{
				this.populationHalfLife = rule.populationHalfLife.Value;
			}
			this.name = rule.Name();
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x0032B587 File Offset: 0x00329787
		public float GetHalfLifeForCount(int count)
		{
			return this.populationHalfLife;
		}

		// Token: 0x04005FB2 RID: 24498
		public string name;

		// Token: 0x04005FB3 RID: 24499
		public float populationHalfLife;
	}
}
