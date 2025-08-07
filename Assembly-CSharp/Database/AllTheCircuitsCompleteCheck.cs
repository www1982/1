using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F5C RID: 3932
	public class AllTheCircuitsCompleteCheck : ColonyAchievementRequirement
	{
		// Token: 0x06007B38 RID: 31544 RVA: 0x0030B165 File Offset: 0x00309365
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.MVB_DESCRIPTION, 8);
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x0030B17C File Offset: 0x0030937C
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.fullyBoostedBionic;
		}
	}
}
