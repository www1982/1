using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F46 RID: 3910
	public class ActivateLorePOI : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AEC RID: 31468 RVA: 0x0030A32A File Offset: 0x0030852A
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x0030A32C File Offset: 0x0030852C
		public override bool Success()
		{
			foreach (BuildingComplete buildingComplete in Components.TemplateBuildings.Items)
			{
				if (!(buildingComplete == null))
				{
					Unsealable component = buildingComplete.GetComponent<Unsealable>();
					if (component != null && component.unsealed)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x0030A3A4 File Offset: 0x003085A4
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.INVESTIGATE_A_POI;
		}
	}
}
