using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F55 RID: 3925
	public class HarvestAmountFromSpacePOI : ColonyAchievementRequirement
	{
		// Token: 0x06007B23 RID: 31523 RVA: 0x0030ADA5 File Offset: 0x00308FA5
		public HarvestAmountFromSpacePOI(float amountToHarvest)
		{
			this.amountToHarvest = amountToHarvest;
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x0030ADB4 File Offset: 0x00308FB4
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.MINE_SPACE_POI, SaveGame.Instance.ColonyAchievementTracker.totalMaterialsHarvestFromPOI, this.amountToHarvest);
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x0030ADE4 File Offset: 0x00308FE4
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.totalMaterialsHarvestFromPOI > this.amountToHarvest;
		}

		// Token: 0x040059D9 RID: 23001
		private float amountToHarvest;
	}
}
