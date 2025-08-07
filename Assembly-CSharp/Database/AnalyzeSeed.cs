using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F54 RID: 3924
	public class AnalyzeSeed : ColonyAchievementRequirement
	{
		// Token: 0x06007B20 RID: 31520 RVA: 0x0030AD54 File Offset: 0x00308F54
		public AnalyzeSeed(string seedname)
		{
			this.seedName = seedname;
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x0030AD63 File Offset: 0x00308F63
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ANALYZE_SEED, this.seedName.ProperName());
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x0030AD84 File Offset: 0x00308F84
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.analyzedSeeds.Contains(this.seedName);
		}

		// Token: 0x040059D8 RID: 23000
		private string seedName;
	}
}
