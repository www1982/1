using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F53 RID: 3923
	public class BuildALaunchPad : ColonyAchievementRequirement
	{
		// Token: 0x06007B1D RID: 31517 RVA: 0x0030ACC1 File Offset: 0x00308EC1
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.BUILD_A_LAUNCHPAD;
		}

		// Token: 0x06007B1E RID: 31518 RVA: 0x0030ACD0 File Offset: 0x00308ED0
		public override bool Success()
		{
			foreach (LaunchPad launchPad in Components.LaunchPads.Items)
			{
				WorldContainer myWorld = launchPad.GetMyWorld();
				if (!myWorld.IsStartWorld && Components.WarpReceivers.GetWorldItems(myWorld.id, false).Count == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
