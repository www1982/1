using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F57 RID: 3927
	public class RadBoltTravelDistance : ColonyAchievementRequirement
	{
		// Token: 0x06007B29 RID: 31529 RVA: 0x0030AF08 File Offset: 0x00309108
		public RadBoltTravelDistance(int travelDistance)
		{
			this.travelDistance = travelDistance;
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x0030AF17 File Offset: 0x00309117
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.RADBOLT_TRAVEL, SaveGame.Instance.ColonyAchievementTracker.radBoltTravelDistance, this.travelDistance);
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x0030AF47 File Offset: 0x00309147
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.radBoltTravelDistance > (float)this.travelDistance;
		}

		// Token: 0x040059DA RID: 23002
		private int travelDistance;
	}
}
