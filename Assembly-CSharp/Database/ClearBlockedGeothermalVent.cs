using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2B RID: 3883
	public class ClearBlockedGeothermalVent : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A75 RID: 31349 RVA: 0x00308556 File Offset: 0x00306756
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x00308564 File Offset: 0x00306764
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.UNBLOCK_VENT_TITLE;
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x00308570 File Offset: 0x00306770
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.GeothermalClearedEntombedVent;
		}

		// Token: 0x06007A78 RID: 31352 RVA: 0x00308581 File Offset: 0x00306781
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.UNBLOCK_VENT_DESCRIPTION;
		}
	}
}
