using System;
using System.Text;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000C56 RID: 3158
public class RadiationBalanceDisplayer : StandardAmountDisplayer
{
	// Token: 0x06006093 RID: 24723 RVA: 0x0023B9A7 File Offset: 0x00239BA7
	public RadiationBalanceDisplayer()
		: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle, null, GameUtil.IdentityDescriptorTense.Normal)
	{
		this.formatter = new RadiationBalanceDisplayer.RadiationAttributeFormatter();
	}

	// Token: 0x06006094 RID: 24724 RVA: 0x0023B9BE File Offset: 0x00239BBE
	public override string GetValueString(Amount master, AmountInstance instance)
	{
		return base.GetValueString(master, instance) + UI.UNITSUFFIXES.RADIATION.RADS;
	}

	// Token: 0x06006095 RID: 24725 RVA: 0x0023B9D8 File Offset: 0x00239BD8
	public override string GetTooltip(Amount master, AmountInstance instance)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		if (instance.gameObject.GetSMI<RadiationMonitor.Instance>() != null)
		{
			int num = Grid.PosToCell(instance.gameObject);
			if (Grid.IsValidCell(num))
			{
				stringBuilder.Append(DUPLICANTS.STATS.RADIATIONBALANCE.TOOLTIP_CURRENT_BALANCE);
			}
			stringBuilder.Append("\n\n");
			float num2 = Mathf.Clamp01(1f - Db.Get().Attributes.RadiationResistance.Lookup(instance.gameObject).GetTotalValue());
			stringBuilder.AppendFormat(DUPLICANTS.STATS.RADIATIONBALANCE.CURRENT_EXPOSURE, Mathf.RoundToInt(Grid.Radiation[num] * num2));
			stringBuilder.Append("\n");
			stringBuilder.AppendFormat(DUPLICANTS.STATS.RADIATIONBALANCE.CURRENT_REJUVENATION, Mathf.RoundToInt(Db.Get().Attributes.RadiationRecovery.Lookup(instance.gameObject).GetTotalValue() * 600f));
		}
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}

	// Token: 0x02001E22 RID: 7714
	public class RadiationAttributeFormatter : StandardAttributeFormatter
	{
		// Token: 0x0600AFB7 RID: 44983 RVA: 0x003D0C56 File Offset: 0x003CEE56
		public RadiationAttributeFormatter()
			: base(GameUtil.UnitClass.SimpleFloat, GameUtil.TimeSlice.PerCycle)
		{
		}
	}
}
