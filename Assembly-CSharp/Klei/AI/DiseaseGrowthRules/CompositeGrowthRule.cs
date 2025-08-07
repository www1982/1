using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001014 RID: 4116
	public class CompositeGrowthRule
	{
		// Token: 0x06007EEB RID: 32491 RVA: 0x0032B2EF File Offset: 0x003294EF
		public string Name()
		{
			return this.name;
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x0032B2F8 File Offset: 0x003294F8
		public void Overlay(GrowthRule rule)
		{
			if (rule.underPopulationDeathRate != null)
			{
				this.underPopulationDeathRate = rule.underPopulationDeathRate.Value;
			}
			if (rule.populationHalfLife != null)
			{
				this.populationHalfLife = rule.populationHalfLife.Value;
			}
			if (rule.overPopulationHalfLife != null)
			{
				this.overPopulationHalfLife = rule.overPopulationHalfLife.Value;
			}
			if (rule.diffusionScale != null)
			{
				this.diffusionScale = rule.diffusionScale.Value;
			}
			if (rule.minCountPerKG != null)
			{
				this.minCountPerKG = rule.minCountPerKG.Value;
			}
			if (rule.maxCountPerKG != null)
			{
				this.maxCountPerKG = rule.maxCountPerKG.Value;
			}
			if (rule.minDiffusionCount != null)
			{
				this.minDiffusionCount = rule.minDiffusionCount.Value;
			}
			if (rule.minDiffusionInfestationTickCount != null)
			{
				this.minDiffusionInfestationTickCount = rule.minDiffusionInfestationTickCount.Value;
			}
			this.name = rule.Name();
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x0032B408 File Offset: 0x00329608
		public float GetHalfLifeForCount(int count, float kg)
		{
			int num = (int)(this.minCountPerKG * kg);
			int num2 = (int)(this.maxCountPerKG * kg);
			if (count < num)
			{
				return this.populationHalfLife;
			}
			if (count < num2)
			{
				return this.populationHalfLife;
			}
			return this.overPopulationHalfLife;
		}

		// Token: 0x04005FA6 RID: 24486
		public string name;

		// Token: 0x04005FA7 RID: 24487
		public float underPopulationDeathRate;

		// Token: 0x04005FA8 RID: 24488
		public float populationHalfLife;

		// Token: 0x04005FA9 RID: 24489
		public float overPopulationHalfLife;

		// Token: 0x04005FAA RID: 24490
		public float diffusionScale;

		// Token: 0x04005FAB RID: 24491
		public float minCountPerKG;

		// Token: 0x04005FAC RID: 24492
		public float maxCountPerKG;

		// Token: 0x04005FAD RID: 24493
		public int minDiffusionCount;

		// Token: 0x04005FAE RID: 24494
		public byte minDiffusionInfestationTickCount;
	}
}
