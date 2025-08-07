using System;
using System.Collections.Generic;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001016 RID: 4118
	public class ExposureRule
	{
		// Token: 0x06007EF2 RID: 32498 RVA: 0x0032B4B0 File Offset: 0x003296B0
		public void Apply(ElemExposureInfo[] infoList)
		{
			List<Element> elements = ElementLoader.elements;
			for (int i = 0; i < elements.Count; i++)
			{
				if (this.Test(elements[i]))
				{
					ElemExposureInfo elemExposureInfo = infoList[i];
					if (this.populationHalfLife != null)
					{
						elemExposureInfo.populationHalfLife = this.populationHalfLife.Value;
					}
					infoList[i] = elemExposureInfo;
				}
			}
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x0032B513 File Offset: 0x00329713
		public virtual bool Test(Element e)
		{
			return true;
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x0032B516 File Offset: 0x00329716
		public virtual string Name()
		{
			return null;
		}

		// Token: 0x04005FB0 RID: 24496
		public float? populationHalfLife;
	}
}
