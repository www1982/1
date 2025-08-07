using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F52 RID: 3922
	public class DefrostDuplicant : ColonyAchievementRequirement
	{
		// Token: 0x06007B1A RID: 31514 RVA: 0x0030AC9C File Offset: 0x00308E9C
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.DEFROST_DUPLICANT;
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x0030ACA8 File Offset: 0x00308EA8
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.defrostedDuplicant;
		}
	}
}
