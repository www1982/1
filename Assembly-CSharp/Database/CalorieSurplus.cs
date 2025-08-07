using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F33 RID: 3891
	public class CalorieSurplus : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A9C RID: 31388 RVA: 0x00308AE8 File Offset: 0x00306CE8
		public CalorieSurplus(float surplusAmount)
		{
			this.surplusAmount = (double)surplusAmount;
		}

		// Token: 0x06007A9D RID: 31389 RVA: 0x00308AF8 File Offset: 0x00306CF8
		public override bool Success()
		{
			return (double)(ClusterManager.Instance.CountAllRations() / 1000f) >= this.surplusAmount;
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x00308B16 File Offset: 0x00306D16
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x00308B21 File Offset: 0x00306D21
		public void Deserialize(IReader reader)
		{
			this.surplusAmount = reader.ReadDouble();
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x00308B2F File Offset: 0x00306D2F
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CALORIE_SURPLUS, GameUtil.GetFormattedCalories(complete ? ((float)this.surplusAmount) : ClusterManager.Instance.CountAllRations(), GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories((float)this.surplusAmount, GameUtil.TimeSlice.None, true));
		}

		// Token: 0x040059B2 RID: 22962
		private double surplusAmount;
	}
}
