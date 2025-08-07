using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F5A RID: 3930
	public class RunReactorForXDays : ColonyAchievementRequirement
	{
		// Token: 0x06007B32 RID: 31538 RVA: 0x0030B03C File Offset: 0x0030923C
		public RunReactorForXDays(int numCycles)
		{
			this.numCycles = numCycles;
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x0030B04C File Offset: 0x0030924C
		public override string GetProgress(bool complete)
		{
			int num = 0;
			foreach (Reactor reactor in Components.NuclearReactors.Items)
			{
				if (reactor.numCyclesRunning > num)
				{
					num = reactor.numCyclesRunning;
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.RUN_A_REACTOR, complete ? this.numCycles : num, this.numCycles);
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x0030B0DC File Offset: 0x003092DC
		public override bool Success()
		{
			using (List<Reactor>.Enumerator enumerator = Components.NuclearReactors.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.numCyclesRunning >= this.numCycles)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040059DD RID: 23005
		private int numCycles;
	}
}
