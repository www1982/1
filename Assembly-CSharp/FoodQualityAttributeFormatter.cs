using System;
using Klei.AI;

// Token: 0x02000C5E RID: 3166
public class FoodQualityAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060B2 RID: 24754 RVA: 0x0023C429 File Offset: 0x0023A629
	public FoodQualityAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x060060B3 RID: 24755 RVA: 0x0023C433 File Offset: 0x0023A633
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.None);
	}

	// Token: 0x060060B4 RID: 24756 RVA: 0x0023C442 File Offset: 0x0023A642
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return GameUtil.GetFormattedInt(modifier.Value, GameUtil.TimeSlice.None);
	}

	// Token: 0x060060B5 RID: 24757 RVA: 0x0023C450 File Offset: 0x0023A650
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return Util.StripTextFormatting(GameUtil.GetFormattedFoodQuality((int)value));
	}
}
