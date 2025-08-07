using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F58 RID: 3928
	public class HarvestAHiveWithoutBeingStung : ColonyAchievementRequirement
	{
		// Token: 0x06007B2C RID: 31532 RVA: 0x0030AF61 File Offset: 0x00309161
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.HARVEST_HIVE;
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x0030AF6D File Offset: 0x0030916D
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.harvestAHiveWithoutGettingStung;
		}
	}
}
