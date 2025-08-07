using System;
using System.Collections.Generic;
using System.IO;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x0200100F RID: 4111
	public struct ElemGrowthInfo
	{
		// Token: 0x06007EDB RID: 32475 RVA: 0x0032AFE8 File Offset: 0x003291E8
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.underPopulationDeathRate);
			writer.Write(this.populationHalfLife);
			writer.Write(this.overPopulationHalfLife);
			writer.Write(this.diffusionScale);
			writer.Write(this.minCountPerKG);
			writer.Write(this.maxCountPerKG);
			writer.Write(this.minDiffusionCount);
			writer.Write(this.minDiffusionInfestationTickCount);
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x0032B058 File Offset: 0x00329258
		public static void SetBulk(ElemGrowthInfo[] info, Func<Element, bool> test, ElemGrowthInfo settings)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (test(elements[i]))
				{
					info[i] = settings;
				}
			}
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x0032B094 File Offset: 0x00329294
		public float CalculateDiseaseCountDelta(int disease_count, float kg, float dt)
		{
			float num = this.minCountPerKG * kg;
			float num2 = this.maxCountPerKG * kg;
			float num3;
			if (num <= (float)disease_count && (float)disease_count <= num2)
			{
				num3 = (Disease.HalfLifeToGrowthRate(this.populationHalfLife, dt) - 1f) * (float)disease_count;
			}
			else if ((float)disease_count < num)
			{
				num3 = -this.underPopulationDeathRate * dt;
			}
			else
			{
				num3 = (Disease.HalfLifeToGrowthRate(this.overPopulationHalfLife, dt) - 1f) * (float)disease_count;
			}
			return num3;
		}

		// Token: 0x04005F93 RID: 24467
		public float underPopulationDeathRate;

		// Token: 0x04005F94 RID: 24468
		public float populationHalfLife;

		// Token: 0x04005F95 RID: 24469
		public float overPopulationHalfLife;

		// Token: 0x04005F96 RID: 24470
		public float diffusionScale;

		// Token: 0x04005F97 RID: 24471
		public float minCountPerKG;

		// Token: 0x04005F98 RID: 24472
		public float maxCountPerKG;

		// Token: 0x04005F99 RID: 24473
		public int minDiffusionCount;

		// Token: 0x04005F9A RID: 24474
		public byte minDiffusionInfestationTickCount;
	}
}
