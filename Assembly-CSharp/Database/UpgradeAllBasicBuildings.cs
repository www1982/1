using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F43 RID: 3907
	public class UpgradeAllBasicBuildings : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AE0 RID: 31456 RVA: 0x00309FAC File Offset: 0x003081AC
		public UpgradeAllBasicBuildings(Tag basicBuilding, Tag upgradeBuilding)
		{
			this.basicBuilding = basicBuilding;
			this.upgradeBuilding = upgradeBuilding;
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x00309FC4 File Offset: 0x003081C4
		public override bool Success()
		{
			bool flag = false;
			foreach (IBasicBuilding basicBuilding in Components.BasicBuildings.Items)
			{
				KPrefabID component = basicBuilding.transform.GetComponent<KPrefabID>();
				if (component.HasTag(this.basicBuilding))
				{
					return false;
				}
				if (component.HasTag(this.upgradeBuilding))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x0030A048 File Offset: 0x00308248
		public void Deserialize(IReader reader)
		{
			string text = reader.ReadKleiString();
			this.basicBuilding = new Tag(text);
			string text2 = reader.ReadKleiString();
			this.upgradeBuilding = new Tag(text2);
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x0030A07C File Offset: 0x0030827C
		public override string GetProgress(bool complete)
		{
			BuildingDef buildingDef = Assets.GetBuildingDef(this.basicBuilding.Name);
			BuildingDef buildingDef2 = Assets.GetBuildingDef(this.upgradeBuilding.Name);
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.UPGRADE_ALL_BUILDINGS, buildingDef.Name, buildingDef2.Name);
		}

		// Token: 0x040059C9 RID: 22985
		private Tag basicBuilding;

		// Token: 0x040059CA RID: 22986
		private Tag upgradeBuilding;
	}
}
