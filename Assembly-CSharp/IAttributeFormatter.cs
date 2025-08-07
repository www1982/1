using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000C5B RID: 3163
public interface IAttributeFormatter
{
	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x0600609F RID: 24735
	// (set) Token: 0x060060A0 RID: 24736
	GameUtil.TimeSlice DeltaTimeSlice { get; set; }

	// Token: 0x060060A1 RID: 24737
	string GetFormattedAttribute(AttributeInstance instance);

	// Token: 0x060060A2 RID: 24738
	string GetFormattedModifier(AttributeModifier modifier);

	// Token: 0x060060A3 RID: 24739
	string GetFormattedValue(float value, GameUtil.TimeSlice timeSlice);

	// Token: 0x060060A4 RID: 24740
	string GetTooltip(Klei.AI.Attribute master, AttributeInstance instance);

	// Token: 0x060060A5 RID: 24741
	string GetTooltip(Klei.AI.Attribute master, List<AttributeModifier> modifiers, AttributeConverters converters);
}
