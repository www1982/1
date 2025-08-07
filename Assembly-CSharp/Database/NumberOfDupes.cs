using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F20 RID: 3872
	public class NumberOfDupes : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A39 RID: 31289 RVA: 0x00307E26 File Offset: 0x00306026
		public override string Name()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_DUPLICANTS, this.numDupes);
		}

		// Token: 0x06007A3A RID: 31290 RVA: 0x00307E42 File Offset: 0x00306042
		public override string Description()
		{
			return string.Format(COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.MINIMUM_DUPLICANTS_DESCRIPTION, this.numDupes);
		}

		// Token: 0x06007A3B RID: 31291 RVA: 0x00307E5E File Offset: 0x0030605E
		public NumberOfDupes(int num)
		{
			this.numDupes = num;
		}

		// Token: 0x06007A3C RID: 31292 RVA: 0x00307E6D File Offset: 0x0030606D
		public override bool Success()
		{
			return Components.LiveMinionIdentities.Items.Count >= this.numDupes;
		}

		// Token: 0x06007A3D RID: 31293 RVA: 0x00307E89 File Offset: 0x00306089
		public void Deserialize(IReader reader)
		{
			this.numDupes = reader.ReadInt32();
		}

		// Token: 0x06007A3E RID: 31294 RVA: 0x00307E97 File Offset: 0x00306097
		public override string GetProgress(bool complete)
		{
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.POPULATION, complete ? this.numDupes : Components.LiveMinionIdentities.Items.Count, this.numDupes);
		}

		// Token: 0x040059A5 RID: 22949
		private int numDupes;
	}
}
