using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001017 RID: 4119
	public class ElementExposureRule : ExposureRule
	{
		// Token: 0x06007EF6 RID: 32502 RVA: 0x0032B521 File Offset: 0x00329721
		public ElementExposureRule(SimHashes element)
		{
			this.element = element;
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x0032B530 File Offset: 0x00329730
		public override bool Test(Element e)
		{
			return e.id == this.element;
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x0032B540 File Offset: 0x00329740
		public override string Name()
		{
			return ElementLoader.FindElementByHash(this.element).name;
		}

		// Token: 0x04005FB1 RID: 24497
		public SimHashes element;
	}
}
