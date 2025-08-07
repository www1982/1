using System;
using Klei.AI;

// Token: 0x02000C55 RID: 3157
public class CaloriesDisplayer : StandardAmountDisplayer
{
	// Token: 0x06006092 RID: 24722 RVA: 0x0023B990 File Offset: 0x00239B90
	public CaloriesDisplayer()
		: base(GameUtil.UnitClass.Calories, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new CaloriesDisplayer.CaloriesAttributeFormatter();
	}

	// Token: 0x02001E21 RID: 7713
	public class CaloriesAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x0600AFB5 RID: 44981 RVA: 0x003D0C27 File Offset: 0x003CEE27
		public CaloriesAttributeFormatter()
			: base(GameUtil.UnitClass.Calories, GameUtil.TimeSlice.PerCycle)
		{
		}

		// Token: 0x0600AFB6 RID: 44982 RVA: 0x003D0C31 File Offset: 0x003CEE31
		public override string GetFormattedModifier(AttributeModifier modifier)
		{
			if (modifier.IsMultiplier)
			{
				return GameUtil.GetFormattedPercent(-modifier.Value * 100f, GameUtil.TimeSlice.None);
			}
			return base.GetFormattedModifier(modifier);
		}
	}
}
