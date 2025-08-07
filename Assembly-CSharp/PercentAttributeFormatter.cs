using System;
using Klei.AI;

// Token: 0x02000C62 RID: 3170
public class PercentAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060BF RID: 24767 RVA: 0x0023C5EB File Offset: 0x0023A7EB
	public PercentAttributeFormatter()
		: base(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x060060C0 RID: 24768 RVA: 0x0023C5F5 File Offset: 0x0023A7F5
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), base.DeltaTimeSlice);
	}

	// Token: 0x060060C1 RID: 24769 RVA: 0x0023C609 File Offset: 0x0023A809
	public override string GetFormattedModifier(AttributeModifier modifier)
	{
		return this.GetFormattedValue(modifier.Value, base.DeltaTimeSlice);
	}

	// Token: 0x060060C2 RID: 24770 RVA: 0x0023C61D File Offset: 0x0023A81D
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return GameUtil.GetFormattedPercent(value * 100f, timeSlice);
	}
}
