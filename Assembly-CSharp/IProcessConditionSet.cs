using System;
using System.Collections.Generic;

// Token: 0x02000DE8 RID: 3560
public interface IProcessConditionSet
{
	// Token: 0x06007085 RID: 28805
	List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType);
}
