using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2C RID: 3884
	public class OpenTemporalTear : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A7A RID: 31354 RVA: 0x00308595 File Offset: 0x00306795
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.OPEN_TEMPORAL_TEAR;
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x003085A1 File Offset: 0x003067A1
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x003085AF File Offset: 0x003067AF
		public override bool Success()
		{
			return ClusterManager.Instance.GetComponent<ClusterPOIManager>().IsTemporalTearOpen();
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x003085C0 File Offset: 0x003067C0
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.DISTANT_PLANET_REACHED.REQUIREMENTS.OPEN_TEMPORAL_TEAR;
		}
	}
}
