using System;
using System.Text;
using Klei.AI;
using STRINGS;

// Token: 0x02000C57 RID: 3159
public class DecorDisplayer : StandardAmountDisplayer
{
	// Token: 0x06006096 RID: 24726 RVA: 0x0023BAD1 File Offset: 0x00239CD1
	public DecorDisplayer()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new DecorDisplayer.DecorAttributeFormatter();
	}

	// Token: 0x06006097 RID: 24727 RVA: 0x0023BAE8 File Offset: 0x00239CE8
	public override string GetTooltip(Amount master, AmountInstance instance)
	{
		string text = LocText.ParseText(master.description);
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		stringBuilder.AppendFormat(text, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
		int num = Grid.PosToCell(instance.gameObject);
		if (Grid.IsValidCell(num))
		{
			stringBuilder.Append(string.Format(DUPLICANTS.STATS.DECOR.TOOLTIP_CURRENT, GameUtil.GetDecorAtCell(num)));
		}
		stringBuilder.Append("\n");
		DecorMonitor.Instance smi = instance.gameObject.GetSMI<DecorMonitor.Instance>();
		if (smi != null)
		{
			stringBuilder.AppendFormat(DUPLICANTS.STATS.DECOR.TOOLTIP_AVERAGE_TODAY, this.formatter.GetFormattedValue(smi.GetTodaysAverageDecor(), GameUtil.TimeSlice.None));
			stringBuilder.AppendFormat(DUPLICANTS.STATS.DECOR.TOOLTIP_AVERAGE_YESTERDAY, this.formatter.GetFormattedValue(smi.GetYesterdaysAverageDecor(), GameUtil.TimeSlice.None));
		}
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x02001E23 RID: 7715
	public class DecorAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x0600AFB8 RID: 44984 RVA: 0x003D0C60 File Offset: 0x003CEE60
		public DecorAttributeFormatter()
			: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle)
		{
		}
	}
}
