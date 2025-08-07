using System;
using Klei.AI;

// Token: 0x02000C4F RID: 3151
public interface IAmountDisplayer
{
	// Token: 0x06006070 RID: 24688
	string GetValueString(Amount master, AmountInstance instance);

	// Token: 0x06006071 RID: 24689
	string GetDescription(Amount master, AmountInstance instance);

	// Token: 0x06006072 RID: 24690
	string GetTooltip(Amount master, AmountInstance instance);

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x06006073 RID: 24691
	IAttributeFormatter Formatter { get; }
}
