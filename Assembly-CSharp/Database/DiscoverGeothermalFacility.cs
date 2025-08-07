using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F28 RID: 3880
	public class DiscoverGeothermalFacility : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A66 RID: 31334 RVA: 0x003084A3 File Offset: 0x003066A3
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A67 RID: 31335 RVA: 0x003084B1 File Offset: 0x003066B1
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.DISCOVER_GEOTHERMAL_FACILITY_DESCRIPTION;
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x003084BD File Offset: 0x003066BD
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.REQUIREMENTS.DISCOVER_GEOTHERMAL_FACILITY_TITLE;
		}

		// Token: 0x06007A69 RID: 31337 RVA: 0x003084C9 File Offset: 0x003066C9
		public override bool Success()
		{
			return GeothermalPlantComponent.GeothermalFacilityDiscovered();
		}
	}
}
