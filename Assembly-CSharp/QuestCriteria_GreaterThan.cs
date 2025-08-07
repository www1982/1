using System;
using System.Collections.Generic;

// Token: 0x02000A7F RID: 2687
public class QuestCriteria_GreaterThan : QuestCriteria
{
	// Token: 0x06004E18 RID: 19992 RVA: 0x001C4CDA File Offset: 0x001C2EDA
	public QuestCriteria_GreaterThan(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06004E19 RID: 19993 RVA: 0x001C4CE9 File Offset: 0x001C2EE9
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current > target;
	}
}
