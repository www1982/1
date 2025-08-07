using System;
using System.Collections.Generic;

// Token: 0x02000A80 RID: 2688
public class QuestCriteria_LessThan : QuestCriteria
{
	// Token: 0x06004E1A RID: 19994 RVA: 0x001C4CEF File Offset: 0x001C2EEF
	public QuestCriteria_LessThan(Tag id, float[] targetValues, int requiredCount = 1, HashSet<Tag> acceptedTags = null, QuestCriteria.BehaviorFlags flags = QuestCriteria.BehaviorFlags.TrackValues)
		: base(id, targetValues, requiredCount, acceptedTags, flags)
	{
	}

	// Token: 0x06004E1B RID: 19995 RVA: 0x001C4CFE File Offset: 0x001C2EFE
	protected override bool ValueSatisfies_Internal(float current, float target)
	{
		return current < target;
	}
}
