using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F39 RID: 3897
	public class CoolBuildingToXKelvin : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AB6 RID: 31414 RVA: 0x00309410 File Offset: 0x00307610
		public CoolBuildingToXKelvin(int kelvinToCoolTo)
		{
			this.kelvinToCoolTo = kelvinToCoolTo;
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x0030941F File Offset: 0x0030761F
		public override bool Success()
		{
			return BuildingComplete.MinKelvinSeen <= (float)this.kelvinToCoolTo;
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x00309432 File Offset: 0x00307632
		public void Deserialize(IReader reader)
		{
			this.kelvinToCoolTo = reader.ReadInt32();
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x00309440 File Offset: 0x00307640
		public override string GetProgress(bool complete)
		{
			float minKelvinSeen = BuildingComplete.MinKelvinSeen;
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.KELVIN_COOLING, minKelvinSeen);
		}

		// Token: 0x040059BE RID: 22974
		private int kelvinToCoolTo;
	}
}
