using System;
using System.Collections.Generic;
using System.IO;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001015 RID: 4117
	public struct ElemExposureInfo
	{
		// Token: 0x06007EEF RID: 32495 RVA: 0x0032B44D File Offset: 0x0032964D
		public void Write(BinaryWriter writer)
		{
			writer.Write(this.populationHalfLife);
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x0032B45C File Offset: 0x0032965C
		public static void SetBulk(ElemExposureInfo[] info, Func<Element, bool> test, ElemExposureInfo settings)
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

		// Token: 0x06007EF1 RID: 32497 RVA: 0x0032B497 File Offset: 0x00329697
		public float CalculateExposureDiseaseCountDelta(int disease_count, float dt)
		{
			return (Disease.HalfLifeToGrowthRate(this.populationHalfLife, dt) - 1f) * (float)disease_count;
		}

		// Token: 0x04005FAF RID: 24495
		public float populationHalfLife;
	}
}
