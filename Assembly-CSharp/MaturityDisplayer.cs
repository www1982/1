using System;
using System.Text;
using Klei.AI;
using STRINGS;

// Token: 0x02000C58 RID: 3160
public class MaturityDisplayer : AsPercentAmountDisplayer
{
	// Token: 0x06006098 RID: 24728 RVA: 0x0023BBBD File Offset: 0x00239DBD
	public MaturityDisplayer()
		: base(GameUtil.TimeSlice.PerCycle)
	{
		this.formatter = new MaturityDisplayer.MaturityAttributeFormatter();
	}

	// Token: 0x06006099 RID: 24729 RVA: 0x0023BBD4 File Offset: 0x00239DD4
	public override string GetTooltipDescription(Amount master, AmountInstance instance)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		stringBuilder.Append(base.GetTooltipDescription(master, instance));
		Growing component = instance.gameObject.GetComponent<Growing>();
		if (component.IsGrowing())
		{
			float num = (instance.GetMax() - instance.value) / instance.GetDelta();
			if (component != null && component.IsGrowing())
			{
				stringBuilder.AppendFormat(CREATURES.STATS.MATURITY.TOOLTIP_GROWING_CROP, GameUtil.GetFormattedCycles(num, "F1", false), GameUtil.GetFormattedCycles(component.TimeUntilNextHarvest(), "F1", false));
			}
			else
			{
				stringBuilder.AppendFormat(CREATURES.STATS.MATURITY.TOOLTIP_GROWING, GameUtil.GetFormattedCycles(num, "F1", false));
			}
		}
		else if (component.ReachedNextHarvest())
		{
			stringBuilder.Append(CREATURES.STATS.MATURITY.TOOLTIP_GROWN);
		}
		else
		{
			stringBuilder.Append(CREATURES.STATS.MATURITY.TOOLTIP_STALLED);
		}
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x0600609A RID: 24730 RVA: 0x0023BCB4 File Offset: 0x00239EB4
	public override string GetDescription(Amount master, AmountInstance instance)
	{
		Growing component = instance.gameObject.GetComponent<Growing>();
		if (component != null && component.IsGrowing())
		{
			return string.Format(CREATURES.STATS.MATURITY.AMOUNT_DESC_FMT, master.Name, this.formatter.GetFormattedValue(base.ToPercent(instance.value, instance), GameUtil.TimeSlice.None), GameUtil.GetFormattedCycles(component.TimeUntilNextHarvest(), "F1", false));
		}
		return base.GetDescription(master, instance);
	}

	// Token: 0x02001E24 RID: 7716
	public class MaturityAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x0600AFB9 RID: 44985 RVA: 0x003D0C6A File Offset: 0x003CEE6A
		public MaturityAttributeFormatter()
			: base(GameUtil.UnitClass.Percent, GameUtil.TimeSlice.None)
		{
		}

		// Token: 0x0600AFBA RID: 44986 RVA: 0x003D0C74 File Offset: 0x003CEE74
		public override string GetFormattedModifier(AttributeModifier modifier)
		{
			float num = modifier.Value;
			GameUtil.TimeSlice timeSlice = base.DeltaTimeSlice;
			if (modifier.IsMultiplier)
			{
				num *= 100f;
				timeSlice = GameUtil.TimeSlice.None;
			}
			return this.GetFormattedValue(num, timeSlice);
		}
	}
}
