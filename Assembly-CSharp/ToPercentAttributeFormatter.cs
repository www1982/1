using System;
using Klei.AI;

// Token: 0x02000C61 RID: 3169
public class ToPercentAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060BB RID: 24763 RVA: 0x0023C591 File Offset: 0x0023A791
	public ToPercentAttributeFormatter(float max, GameUtil.TimeSlice deltaTimeSlice = GameUtil.TimeSlice.None)
		: base(GameUtil.UnitClass.Percent, deltaTimeSlice)
	{
		this.max = max;
	}

	// Token: 0x060060BC RID: 24764 RVA: 0x0023C5AD File Offset: 0x0023A7AD
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), base.DeltaTimeSlice);
	}

	// Token: 0x060060BD RID: 24765 RVA: 0x0023C5C1 File Offset: 0x0023A7C1
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.GetFormattedValue(modifier.Value, base.DeltaTimeSlice);
	}

	// Token: 0x060060BE RID: 24766 RVA: 0x0023C5D5 File Offset: 0x0023A7D5
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return GameUtil.GetFormattedPercent(value / this.max * 100f, timeSlice);
	}

	// Token: 0x04004155 RID: 16725
	public float max = 1f;
}
