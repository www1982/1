using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F22 RID: 3874
	public class BeforeCycleNumber : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A45 RID: 31301 RVA: 0x00307F78 File Offset: 0x00306178
		public BeforeCycleNumber(int cycleNumber = 100)
		{
			this.cycleNumber = cycleNumber;
		}

		// Token: 0x06007A46 RID: 31302 RVA: 0x00307F87 File Offset: 0x00306187
		public override bool Success()
		{
			return GameClock.Instance.GetCycle() + 1 <= this.cycleNumber;
		}

		// Token: 0x06007A47 RID: 31303 RVA: 0x00307FA0 File Offset: 0x003061A0
		public override bool Fail()
		{
			return !this.Success();
		}

		// Token: 0x06007A48 RID: 31304 RVA: 0x00307FAB File Offset: 0x003061AB
		public void Deserialize(IReader reader)
		{
			this.cycleNumber = reader.ReadInt32();
		}

		// Token: 0x06007A49 RID: 31305 RVA: 0x00307FB9 File Offset: 0x003061B9
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.REMAINING_CYCLES, Mathf.Max(this.cycleNumber - GameClock.Instance.GetCycle(), 0), this.cycleNumber);
		}

		// Token: 0x040059A7 RID: 22951
		private int cycleNumber;
	}
}
