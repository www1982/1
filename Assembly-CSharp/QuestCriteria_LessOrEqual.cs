using System;
using System.Collections.Generic;

// Token: 0x02000A82 RID: 2690
public class QuestCriteria_LessOrEqual : QuestCriteria
{
	// Token: 0x06004E1E RID: 19998 RVA: 0x001C4D1C File Offset: 0x001C2F1C
	public QuestCriteria_LessOrEqual(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06004E1F RID: 19999 RVA: 0x001C4D2B File Offset: 0x001C2F2B
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current <= target;
	}
}
