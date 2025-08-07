using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F21 RID: 3873
	public class CycleNumber : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A3F RID: 31295 RVA: 0x00307ED2 File Offset: 0x003060D2
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_CYCLE, this.cycleNumber);
		}

		// Token: 0x06007A40 RID: 31296 RVA: 0x00307EEE File Offset: 0x003060EE
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_CYCLE_DESCRIPTION, this.cycleNumber);
		}

		// Token: 0x06007A41 RID: 31297 RVA: 0x00307F0A File Offset: 0x0030610A
		public CycleNumber(int cycleNumber = 100)
		{
			this.cycleNumber = cycleNumber;
		}

		// Token: 0x06007A42 RID: 31298 RVA: 0x00307F19 File Offset: 0x00306119
		public override bool Success()
		{
			return GameClock.Instance.GetCycle() + 1 >= this.cycleNumber;
		}

		// Token: 0x06007A43 RID: 31299 RVA: 0x00307F32 File Offset: 0x00306132
		public void Deserialize(IReader reader)
		{
			this.cycleNumber = reader.ReadInt32();
		}

		// Token: 0x06007A44 RID: 31300 RVA: 0x00307F40 File Offset: 0x00306140
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CYCLE_NUMBER, complete ? this.cycleNumber : (GameClock.Instance.GetCycle() + 1), this.cycleNumber);
		}

		// Token: 0x040059A6 RID: 22950
		private int cycleNumber;
	}
}
