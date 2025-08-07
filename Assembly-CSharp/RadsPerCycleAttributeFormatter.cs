using System;
using Klei.AI;

// Token: 0x02000C5D RID: 3165
public class RadsPerCycleAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060AF RID: 24751 RVA: 0x0023C400 File Offset: 0x0023A600
	public RadsPerCycleAttributeFormatter()
		: base(GameUtil.UnitClass.Radiation, GameUtil.TimeSlice.PerCycle)
	{
	}

	// Token: 0x060060B0 RID: 24752 RVA: 0x0023C40A File Offset: 0x0023A60A
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		return this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.PerCycle);
	}

	// Token: 0x060060B1 RID: 24753 RVA: 0x0023C419 File Offset: 0x0023A619
	public override string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice)
	{
		return base.GetFormattedValue(value / 600f, timeSlice);
	}
}
