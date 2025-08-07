using System;

namespace Database
{
	// Token: 0x02000F1E RID: 3870
	public abstract class ColonyAchievementRequirement
	{
		// Token: 0x06007A2F RID: 31279
		public abstract bool Success();

		// Token: 0x06007A30 RID: 31280 RVA: 0x00307D74 File Offset: 0x00305F74
		public virtual bool Fail()
		{
			return false;
		}

		// Token: 0x06007A31 RID: 31281 RVA: 0x00307D77 File Offset: 0x00305F77
		public virtual string GetProgress(bool complete)
		{
			return "";
		}
	}
}
