using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F4C RID: 3916
	public class BlockedCometWithBunkerDoor : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007B05 RID: 31493 RVA: 0x0030A85F File Offset: 0x00308A5F
		public override bool Success()
		{
			return Game.Instance.savedInfo.blockedCometWithBunkerDoor;
		}

		// Token: 0x06007B06 RID: 31494 RVA: 0x0030A870 File Offset: 0x00308A70
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007B07 RID: 31495 RVA: 0x0030A872 File Offset: 0x00308A72
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BLOCKED_A_COMET;
		}
	}
}
