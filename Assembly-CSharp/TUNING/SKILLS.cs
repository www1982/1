using System;

namespace TUNING
{
	// Token: 0x02000F83 RID: 3971
	public class SKILLS
	{
		// Token: 0x04005BA7 RID: 23463
		public static int TARGET_SKILLS_EARNED = 15;

		// Token: 0x04005BA8 RID: 23464
		public static int TARGET_SKILLS_CYCLE = 250;

		// Token: 0x04005BA9 RID: 23465
		public static float EXPERIENCE_LEVEL_POWER = 1.44f;

		// Token: 0x04005BAA RID: 23466
		public static float PASSIVE_EXPERIENCE_PORTION = 0.5f;

		// Token: 0x04005BAB RID: 23467
		public static float ACTIVE_EXPERIENCE_PORTION = 0.6f;

		// Token: 0x04005BAC RID: 23468
		public static float FULL_EXPERIENCE = 1f;

		// Token: 0x04005BAD RID: 23469
		public static float ALL_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.9f;

		// Token: 0x04005BAE RID: 23470
		public static float MOST_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.75f;

		// Token: 0x04005BAF RID: 23471
		public static float PART_DAY_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.5f;

		// Token: 0x04005BB0 RID: 23472
		public static float BARELY_EVER_EXPERIENCE = SKILLS.FULL_EXPERIENCE / 0.25f;

		// Token: 0x04005BB1 RID: 23473
		public static float APTITUDE_EXPERIENCE_MULTIPLIER = 0.5f;

		// Token: 0x04005BB2 RID: 23474
		public static int[] SKILL_TIER_MORALE_COST = new int[] { 1, 2, 3, 4, 5, 6, 7 };
	}
}
