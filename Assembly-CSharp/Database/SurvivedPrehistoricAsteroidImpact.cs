using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F30 RID: 3888
	public class SurvivedPrehistoricAsteroidImpact : ColonyAchievementRequirement
	{
		// Token: 0x06007A90 RID: 31376 RVA: 0x003087E3 File Offset: 0x003069E3
		public SurvivedPrehistoricAsteroidImpact(int requiredCyclesAfterImpact)
		{
			this.requiredCyclesAfterImpact = requiredCyclesAfterImpact;
		}

		// Token: 0x06007A91 RID: 31377 RVA: 0x003087F4 File Offset: 0x003069F4
		public override string GetProgress(bool complete)
		{
			int num = (complete ? this.requiredCyclesAfterImpact : 0);
			if (!complete && SaveGame.Instance.ColonyAchievementTracker.largeImpactorLandedCycle >= 0)
			{
				num = Mathf.Clamp(GameClock.Instance.GetCycle() - SaveGame.Instance.ColonyAchievementTracker.largeImpactorLandedCycle, 0, this.requiredCyclesAfterImpact);
			}
			return GameUtil.SafeStringFormat(COLONY_ACHIEVEMENTS.ASTEROID_SURVIVED.REQUIREMENT_DESCRIPTION, new object[]
			{
				GameUtil.GetFormattedInt((float)num, GameUtil.TimeSlice.None),
				GameUtil.GetFormattedInt((float)this.requiredCyclesAfterImpact, GameUtil.TimeSlice.None)
			});
		}

		// Token: 0x06007A92 RID: 31378 RVA: 0x0030887A File Offset: 0x00306A7A
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.largeImpactorLandedCycle >= 0 && GameClock.Instance.GetCycle() - SaveGame.Instance.ColonyAchievementTracker.largeImpactorLandedCycle >= this.requiredCyclesAfterImpact;
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x003088B5 File Offset: 0x00306AB5
		public override bool Fail()
		{
			return SaveGame.Instance.ColonyAchievementTracker.largeImpactorState == ColonyAchievementTracker.LargeImpactorState.Defeated;
		}

		// Token: 0x040059AE RID: 22958
		private int requiredCyclesAfterImpact;
	}
}
