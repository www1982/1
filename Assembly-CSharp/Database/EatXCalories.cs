using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F3C RID: 3900
	public class EatXCalories : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AC3 RID: 31427 RVA: 0x00309669 File Offset: 0x00307869
		public EatXCalories(int numCalories)
		{
			this.numCalories = numCalories;
		}

		// Token: 0x06007AC4 RID: 31428 RVA: 0x00309678 File Offset: 0x00307878
		public override bool Success()
		{
			return WorldResourceAmountTracker<RationTracker>.Get().GetAmountConsumed() / 1000f > (float)this.numCalories;
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x00309693 File Offset: 0x00307893
		public void Deserialize(IReader reader)
		{
			this.numCalories = reader.ReadInt32();
		}

		// Token: 0x06007AC6 RID: 31430 RVA: 0x003096A4 File Offset: 0x003078A4
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CONSUME_CALORIES, GameUtil.GetFormattedCalories(complete ? ((float)this.numCalories * 1000f) : WorldResourceAmountTracker<RationTracker>.Get().GetAmountConsumed(), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.numCalories * 1000f, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x040059C1 RID: 22977
		private int numCalories;
	}
}
