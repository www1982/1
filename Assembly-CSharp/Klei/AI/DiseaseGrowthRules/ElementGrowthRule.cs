using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001012 RID: 4114
	public class ElementGrowthRule : GrowthRule
	{
		// Token: 0x06007EE5 RID: 32485 RVA: 0x0032B294 File Offset: 0x00329494
		public ElementGrowthRule(SimHashes element)
		{
			this.element = element;
		}

		// Token: 0x06007EE6 RID: 32486 RVA: 0x0032B2A3 File Offset: 0x003294A3
		public override bool Test(Element e)
		{
			return e.id == this.element;
		}

		// Token: 0x06007EE7 RID: 32487 RVA: 0x0032B2B3 File Offset: 0x003294B3
		public override string Name()
		{
			return ElementLoader.FindElementByHash(this.element).name;
		}

		// Token: 0x04005FA4 RID: 24484
		public SimHashes element;
	}
}
