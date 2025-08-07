using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001013 RID: 4115
	public class TagGrowthRule : GrowthRule
	{
		// Token: 0x06007EE8 RID: 32488 RVA: 0x0032B2C5 File Offset: 0x003294C5
		public TagGrowthRule(Tag tag)
		{
			this.tag = tag;
		}

		// Token: 0x06007EE9 RID: 32489 RVA: 0x0032B2D4 File Offset: 0x003294D4
		public override bool Test(Element e)
		{
			return e.HasTag(this.tag);
		}

		// Token: 0x06007EEA RID: 32490 RVA: 0x0032B2E2 File Offset: 0x003294E2
		public override string Name()
		{
			return this.tag.ProperName();
		}

		// Token: 0x04005FA5 RID: 24485
		public Tag tag;
	}
}
