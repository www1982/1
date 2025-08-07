using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F40 RID: 3904
	public class ExploreOilFieldSubZone : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AD4 RID: 31444 RVA: 0x00309B90 File Offset: 0x00307D90
		public override bool Success()
		{
			return Game.Instance.savedInfo.discoveredOilField;
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x00309BA1 File Offset: 0x00307DA1
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x00309BA3 File Offset: 0x00307DA3
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ENTER_OIL_BIOME;
		}
	}
}
