using System;
using System.Text;
using Klei.AI;
using STRINGS;

// Token: 0x02000C5F RID: 3167
public class QualityOfLifeAttributeFormatter : StandardAttributeFormatter
{
	// Token: 0x060060B6 RID: 24758 RVA: 0x0023C45E File Offset: 0x0023A65E
	public QualityOfLifeAttributeFormatter()
		: base(GameUtil.UnitClass.SimpleInteger, GameUtil.TimeSlice.None)
	{
	}

	// Token: 0x060060B7 RID: 24759 RVA: 0x0023C468 File Offset: 0x0023A668
	public override string GetFormattedAttribute(AttributeInstance instance)
	{
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(instance.gameObject);
		return string.Format(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.DESC_FORMAT, this.GetFormattedValue(instance.GetTotalDisplayValue(), GameUtil.TimeSlice.None), this.GetFormattedValue(attributeInstance.GetTotalDisplayValue(), GameUtil.TimeSlice.None));
	}

	// Token: 0x060060B8 RID: 24760 RVA: 0x0023C4BC File Offset: 0x0023A6BC
	public override string GetTooltip(Klei.AI.Attribute master, AttributeInstance instance)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		stringBuilder.Append(base.GetTooltip(master, instance));
		AttributeInstance attributeInstance = Db.Get().Attributes.QualityOfLifeExpectation.Lookup(instance.gameObject);
		stringBuilder.Append("\n\n");
		stringBuilder.AppendFormat(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION, this.GetFormattedValue(attributeInstance.GetTotalDisplayValue(), GameUtil.TimeSlice.None));
		if (instance.GetTotalDisplayValue() - attributeInstance.GetTotalDisplayValue() >= 0f)
		{
			stringBuilder.Append("\n\n");
			stringBuilder.Append(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION_OVER);
		}
		else
		{
			stringBuilder.Append("\n\n");
			stringBuilder.Append(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.TOOLTIP_EXPECTATION_UNDER);
		}
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}
}
