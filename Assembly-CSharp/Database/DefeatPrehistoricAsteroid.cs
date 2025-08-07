using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2F RID: 3887
	public class DefeatPrehistoricAsteroid : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A8A RID: 31370 RVA: 0x003086EC File Offset: 0x003068EC
		public override string GetProgress(bool complete)
		{
			int num = 1000;
			int num2 = (complete ? num : 0);
			GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
			if (gameplayEventInstance != null)
			{
				LargeImpactorEvent.StatesInstance statesInstance = (LargeImpactorEvent.StatesInstance)gameplayEventInstance.smi;
				if (statesInstance != null && statesInstance.impactorInstance != null)
				{
					LargeImpactorStatus.Instance smi = statesInstance.impactorInstance.GetSMI<LargeImpactorStatus.Instance>();
					num = smi.def.MAX_HEALTH;
					num2 = num - smi.Health;
				}
			}
			return GameUtil.SafeStringFormat(COLONY_ACHIEVEMENTS.ASTEROID_DESTROYED.REQUIREMENT_DESCRIPTION, new object[]
			{
				GameUtil.GetFormattedInt((float)num2, GameUtil.TimeSlice.None),
				GameUtil.GetFormattedInt((float)num, GameUtil.TimeSlice.None)
			});
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x0030879B File Offset: 0x0030699B
		public override string Description()
		{
			return COLONY_ACHIEVEMENTS.ASTEROID_DESTROYED.DESCRIPTION;
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x003087A7 File Offset: 0x003069A7
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.largeImpactorState == ColonyAchievementTracker.LargeImpactorState.Defeated;
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x003087BB File Offset: 0x003069BB
		public override bool Fail()
		{
			return SaveGame.Instance.ColonyAchievementTracker.largeImpactorState == ColonyAchievementTracker.LargeImpactorState.Landed;
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x003087CF File Offset: 0x003069CF
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.ASTEROID_DESTROYED.REQUIREMENT_NAME;
		}
	}
}
