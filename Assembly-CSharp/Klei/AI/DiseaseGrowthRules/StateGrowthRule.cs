using System;

namespace Klei.AI.DiseaseGrowthRules
{
	// Token: 0x02001011 RID: 4113
	public class StateGrowthRule : GrowthRule
	{
		// Token: 0x06007EE2 RID: 32482 RVA: 0x0032B26A File Offset: 0x0032946A
		public StateGrowthRule(Element.State state)
		{
			this.state = state;
		}

		// Token: 0x06007EE3 RID: 32483 RVA: 0x0032B279 File Offset: 0x00329479
		public override bool Test(Element e)
		{
			return e.IsState(this.state);
		}

		// Token: 0x06007EE4 RID: 32484 RVA: 0x0032B287 File Offset: 0x00329487
		public override string Name()
		{
			return Element.GetStateString(this.state);
		}

		// Token: 0x04005FA3 RID: 24483
		public Element.State state;
	}
}
