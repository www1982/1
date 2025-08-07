using System;
using System.Collections.Generic;

// Token: 0x02000B51 RID: 2897
public class LaunchPadConditions : KMonoBehaviour, IProcessConditionSet
{
	// Token: 0x06005656 RID: 22102 RVA: 0x001F4CC8 File Offset: 0x001F2EC8
	public List<ProcessCondition> GetConditionSet(ProcessCondition.ProcessConditionType conditionType)
	{
		if (conditionType != ProcessCondition.ProcessConditionType.RocketStorage)
		{
			return null;
		}
		return this.conditions;
	}

	// Token: 0x06005657 RID: 22103 RVA: 0x001F4CD6 File Offset: 0x001F2ED6
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.conditions = new List<ProcessCondition>();
		this.conditions.Add(new TransferCargoCompleteCondition(base.gameObject));
	}

	// Token: 0x040039BD RID: 14781
	private List<ProcessCondition> conditions;
}
