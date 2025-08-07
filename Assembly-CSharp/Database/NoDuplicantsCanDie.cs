using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2E RID: 3886
	public class NoDuplicantsCanDie : ColonyAchievementRequirement
	{
		// Token: 0x06007A86 RID: 31366 RVA: 0x003086B3 File Offset: 0x003068B3
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.NO_DUPES_HAVE_DIED.REQUIREMENT_NAME;
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x003086BF File Offset: 0x003068BF
		public override bool Success()
		{
			return !SaveGame.Instance.ColonyAchievementTracker.HasAnyDupeDied;
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x003086D3 File Offset: 0x003068D3
		public override bool Fail()
		{
			return SaveGame.Instance.ColonyAchievementTracker.HasAnyDupeDied;
		}
	}
}
