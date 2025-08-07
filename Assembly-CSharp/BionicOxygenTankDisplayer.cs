using System;
using System.Text;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000C54 RID: 3156
public class BionicOxygenTankDisplayer : StandardAmountDisplayer
{
	// Token: 0x06006090 RID: 24720 RVA: 0x0023B783 File Offset: 0x00239983
	public BionicOxygenTankDisplayer(GameUtil.UnitClass unitClass, GameUtil.TimeSlice deltaTimeSlice)
		: base(unitClass, deltaTimeSlice, null, GameUtil.IdentityDescriptorTense.Normal)
	{
	}

	// Token: 0x06006091 RID: 24721 RVA: 0x0023B790 File Offset: 0x00239990
	public override string GetTooltip(Amount master, AmountInstance instance)
	{
		StringBuilder stringBuilder = GlobalStringBuilderPool.Alloc();
		BionicOxygenTankMonitor.Instance smi = instance.gameObject.GetSMI<BionicOxygenTankMonitor.Instance>();
		stringBuilder.AppendFormat(master.description, this.formatter.GetFormattedValue(instance.value, GameUtil.TimeSlice.None));
		stringBuilder.Append("\n\n");
		stringBuilder.AppendFormat(DUPLICANTS.STATS.BIONICOXYGENTANK.TOOLTIP_MASS_LINE, GameUtil.GetFormattedMass(instance.value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), GameUtil.GetFormattedMass(instance.GetMax(), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		if (smi != null)
		{
			foreach (GameObject gameObject in smi.storage.items)
			{
				if (gameObject != null)
				{
					PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
					if (component != null && component.Mass > 0f)
					{
						string text = ((component.DiseaseIdx != byte.MaxValue && component.DiseaseCount > 0) ? string.Format(DUPLICANTS.STATS.BIONICOXYGENTANK.TOOLTIP_GERM_DETAIL, GameUtil.GetFormattedDisease(component.DiseaseIdx, component.DiseaseCount, false)) : "");
						stringBuilder.Append("\n");
						stringBuilder.AppendFormat(DUPLICANTS.STATS.BIONICOXYGENTANK.TOOLTIP_MASS_ROW_DETAIL, component.Element.name, GameUtil.GetFormattedMass(component.Mass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), text);
					}
				}
			}
		}
		stringBuilder.Append("\n\n");
		float num = instance.deltaAttribute.GetTotalDisplayValue();
		if (smi != null)
		{
			float totalValue = smi.airConsumptionRate.GetTotalValue();
			num += totalValue;
		}
		stringBuilder.AppendFormat(UI.CHANGEPERSECOND, this.formatter.GetFormattedValue(num, GameUtil.TimeSlice.PerSecond));
		global::Debug.Assert(instance.deltaAttribute.Modifiers.Count <= 0, "BionicOxygenTankDisplayer has found an invalid AttributeModifier. This particular Amount should not use AttributeModifiers, the rate of breathing is defined by  Db.Get().Attributes.AirConsumptionRate");
		return GlobalStringBuilderPool.ReturnAndFree(stringBuilder);
	}
}
