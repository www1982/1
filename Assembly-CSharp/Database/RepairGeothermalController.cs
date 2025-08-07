using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F29 RID: 3881
	public class RepairGeothermalController : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A6B RID: 31339 RVA: 0x003084D8 File Offset: 0x003066D8
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A6C RID: 31340 RVA: 0x003084E6 File Offset: 0x003066E6
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.REPAIR_CONTROLLER_DESCRIPTION;
		}

		// Token: 0x06007A6D RID: 31341 RVA: 0x003084F2 File Offset: 0x003066F2
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.REPAIR_CONTROLLER_TITLE;
		}

		// Token: 0x06007A6E RID: 31342 RVA: 0x003084FE File Offset: 0x003066FE
		public override bool Success()
		{
			return SaveGame.Instance.ColonyAchievementTracker.GeothermalControllerRepaired;
		}
	}
}
