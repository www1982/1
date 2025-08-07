using System;
using System.Collections;
using STRINGS;

namespace Database
{
	// Token: 0x02000F50 RID: 3920
	public class LaunchedCraft : ColonyAchievementRequirement
	{
		// Token: 0x06007B14 RID: 31508 RVA: 0x0030ABB1 File Offset: 0x00308DB1
		public override string GetProgress(bool completed)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.LAUNCHED_ROCKET;
		}

		// Token: 0x06007B15 RID: 31509 RVA: 0x0030ABC0 File Offset: 0x00308DC0
		public override bool Success()
		{
			using (IEnumerator enumerator = Components.Clustercrafts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((Clustercraft)enumerator.Current).Status == Clustercraft.CraftStatus.InFlight)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
