using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F3B RID: 3899
	public class EatXCaloriesFromY : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007ABF RID: 31423 RVA: 0x00309581 File Offset: 0x00307781
		public EatXCaloriesFromY(int numCalories, List<string> fromFoodType)
		{
			this.numCalories = numCalories;
			this.fromFoodType = fromFoodType;
		}

		// Token: 0x06007AC0 RID: 31424 RVA: 0x003095A2 File Offset: 0x003077A2
		public override bool Success()
		{
			return WorldResourceAmountTracker<RationTracker>.Get().GetAmountConsumedForIDs(this.fromFoodType) / 1000f > (float)this.numCalories;
		}

		// Token: 0x06007AC1 RID: 31425 RVA: 0x003095C4 File Offset: 0x003077C4
		public void Deserialize(IReader reader)
		{
			this.numCalories = reader.ReadInt32();
			int num = reader.ReadInt32();
			this.fromFoodType = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.fromFoodType.Add(text);
			}
		}

		// Token: 0x06007AC2 RID: 31426 RVA: 0x00309610 File Offset: 0x00307810
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CALORIES_FROM_MEAT, GameUtil.GetFormattedCalories(complete ? ((float)this.numCalories * 1000f) : WorldResourceAmountTracker<RationTracker>.Get().GetAmountConsumedForIDs(this.fromFoodType), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.numCalories * 1000f, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x040059BF RID: 22975
		private int numCalories;

		// Token: 0x040059C0 RID: 22976
		private List<string> fromFoodType = new List<string>();
	}
}
