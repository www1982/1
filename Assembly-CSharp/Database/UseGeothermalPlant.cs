using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2A RID: 3882
	public class UseGeothermalPlant : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A70 RID: 31344 RVA: 0x00308517 File Offset: 0x00306717
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x00308525 File Offset: 0x00306725
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.ACTIVATE_PLANT_TITLE;
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x00308531 File Offset: 0x00306731
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.GeothermalControllerHasVented;
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x00308542 File Offset: 0x00306742
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.ACTIVATE_PLANT_DESCRIPTION;
		}
	}
}
