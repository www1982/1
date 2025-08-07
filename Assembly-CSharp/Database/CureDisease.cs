using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F48 RID: 3912
	public class CureDisease : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AF4 RID: 31476 RVA: 0x0030A490 File Offset: 0x00308690
		public override bool Success()
		{
			return Game.Instance.savedInfo.curedDisease;
		}

		// Token: 0x06007AF5 RID: 31477 RVA: 0x0030A4A1 File Offset: 0x003086A1
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x0030A4A3 File Offset: 0x003086A3
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CURED_DISEASE;
		}
	}
}
