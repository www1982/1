using System;
using System.Collections.Generic;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001010 RID: 4112
	public class GrowthRule
	{
		// Token: 0x06007EDE RID: 32478 RVA: 0x0032B100 File Offset: 0x00329300
		public void Apply(ElemGrowthInfo[] infoList)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				Element element = elements[i];
				if (element.id != SimHashes.Vacuum && this.Test(element))
				{
					ElemGrowthInfo elemGrowthInfo = infoList[i];
					if (this.underPopulationDeathRate != null)
					{
						elemGrowthInfo.underPopulationDeathRate = this.underPopulationDeathRate.Value;
					}
					if (this.populationHalfLife != null)
					{
						elemGrowthInfo.populationHalfLife = this.populationHalfLife.Value;
					}
					if (this.overPopulationHalfLife != null)
					{
						elemGrowthInfo.overPopulationHalfLife = this.overPopulationHalfLife.Value;
					}
					if (this.diffusionScale != null)
					{
						elemGrowthInfo.diffusionScale = this.diffusionScale.Value;
					}
					if (this.minCountPerKG != null)
					{
						elemGrowthInfo.minCountPerKG = this.minCountPerKG.Value;
					}
					if (this.maxCountPerKG != null)
					{
						elemGrowthInfo.maxCountPerKG = this.maxCountPerKG.Value;
					}
					if (this.minDiffusionCount != null)
					{
						elemGrowthInfo.minDiffusionCount = this.minDiffusionCount.Value;
					}
					if (this.minDiffusionInfestationTickCount != null)
					{
						elemGrowthInfo.minDiffusionInfestationTickCount = this.minDiffusionInfestationTickCount.Value;
					}
					infoList[i] = elemGrowthInfo;
				}
			}
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x0032B25C File Offset: 0x0032945C
		public virtual bool Test(Element e)
		{
			return true;
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x0032B25F File Offset: 0x0032945F
		public virtual string Name()
		{
			return null;
		}

		// Token: 0x04005F9B RID: 24475
		public float? underPopulationDeathRate;

		// Token: 0x04005F9C RID: 24476
		public float? populationHalfLife;

		// Token: 0x04005F9D RID: 24477
		public float? overPopulationHalfLife;

		// Token: 0x04005F9E RID: 24478
		public float? diffusionScale;

		// Token: 0x04005F9F RID: 24479
		public float? minCountPerKG;

		// Token: 0x04005FA0 RID: 24480
		public float? maxCountPerKG;

		// Token: 0x04005FA1 RID: 24481
		public int? minDiffusionCount;

		// Token: 0x04005FA2 RID: 24482
		public byte? minDiffusionInfestationTickCount;
	}
}
