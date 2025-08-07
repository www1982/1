using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A7E RID: 2686
public class QuestCriteria_Equals : QuestCriteria
{
	// Token: 0x06004E16 RID: 19990 RVA: 0x001C4CB7 File Offset: 0x001C2EB7
	public QuestCriteria_Equals(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06004E17 RID: 19991 RVA: 0x001C4CC6 File Offset: 0x001C2EC6
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return Mathf.Abs(target - current) <= Mathf.Epsilon;
	}
}
