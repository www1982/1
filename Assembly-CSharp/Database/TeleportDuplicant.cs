using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F51 RID: 3921
	public class TeleportDuplicant : ColonyAchievementRequirement
	{
		// Token: 0x06007B17 RID: 31511 RVA: 0x0030AC28 File Offset: 0x00308E28
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.TELEPORT_DUPLICANT;
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x0030AC34 File Offset: 0x00308E34
		public override bool Success()
		{
			using (List<WarpReceiver>.Enumerator enumerator = Components.WarpReceivers.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Used)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
