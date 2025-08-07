using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F59 RID: 3929
	public class SurviveARocketWithMinimumMorale : ColonyAchievementRequirement
	{
		// Token: 0x06007B2F RID: 31535 RVA: 0x0030AF86 File Offset: 0x00309186
		public SurviveARocketWithMinimumMorale(float minimumMorale, int numberOfCycles)
		{
			this.minimumMorale = minimumMorale;
			this.numberOfCycles = numberOfCycles;
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x0030AF9C File Offset: 0x0030919C
		public override string GetProgress(bool complete)
		{
			if (complete)
			{
				return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.SURVIVE_SPACE_COMPLETE, this.minimumMorale, this.numberOfCycles);
			}
			return base.GetProgress(complete);
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x0030AFD0 File Offset: 0x003091D0
		public override bool Success()
		{
			foreach (KeyValuePair<int, int> keyValuePair in SaveGame.Instance.ColonyAchievementTracker.cyclesRocketDupeMoraleAboveRequirement)
			{
				if (keyValuePair.Value >= this.numberOfCycles)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040059DB RID: 23003
		public float minimumMorale;

		// Token: 0x040059DC RID: 23004
		public int numberOfCycles;
	}
}
