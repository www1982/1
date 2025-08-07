using System;
using Klei.AI;

// Token: 0x02000C60 RID: 3168
public class GermResistanceAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060B9 RID: 24761 RVA: 0x0023C579 File Offset: 0x0023A779
	public GermResistanceAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x060060BA RID: 24762 RVA: 0x0023C583 File Offset: 0x0023A783
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return GameUtil.GetGermResistanceModifierString(modifier.Value, false);
	}
}
