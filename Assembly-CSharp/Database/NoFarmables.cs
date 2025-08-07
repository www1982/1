using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F3A RID: 3898
	public class NoFarmables : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007ABA RID: 31418 RVA: 0x00309468 File Offset: 0x00307668
		public override bool Success()
		{
			foreach (WorldContainer worldContainer in ClusterManager.Instance.WorldContainers)
			{
				foreach (PlantablePlot plantablePlot in Components.PlantablePlots.GetItems(worldContainer.id))
				{
					if (plantablePlot.Occupant != null)
					{
						using (IEnumerator<Tag> enumerator3 = plantablePlot.possibleDepositObjectTags.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								if (enumerator3.Current != GameTags.DecorSeed)
								{
									return false;
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x00309560 File Offset: 0x00307760
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x06007ABC RID: 31420 RVA: 0x0030956B File Offset: 0x0030776B
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007ABD RID: 31421 RVA: 0x0030956D File Offset: 0x0030776D
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.NO_FARM_TILES;
		}
	}
}
