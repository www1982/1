using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F5B RID: 3931
	public class EfficientDataMiningCheck : ColonyAchievementRequirement
	{
		// Token: 0x06007B35 RID: 31541 RVA: 0x0030B140 File Offset: 0x00309340
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.DATA_DRIVEN_DESCRIPTION;
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x0030B14C File Offset: 0x0030934C
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.efficientlyGatheredData;
		}
	}
}
