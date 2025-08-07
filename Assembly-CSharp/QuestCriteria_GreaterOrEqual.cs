using System;
using System.Collections.Generic;

// Token: 0x02000A81 RID: 2689
public class QuestCriteria_GreaterOrEqual : QuestCriteria
{
	// Token: 0x06004E1C RID: 19996 RVA: 0x001C4D04 File Offset: 0x001C2F04
	public QuestCriteria_GreaterOrEqual(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06004E1D RID: 19997 RVA: 0x001C4D13 File Offset: 0x001C2F13
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current >= target;
	}
}
