using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F26 RID: 3878
	public class CollectedArtifacts : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A5A RID: 31322 RVA: 0x0030835C File Offset: 0x0030655C
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.COLLECT_ARTIFACTS.Replace("{collectedCount}", this.GetStudiedArtifactCount().ToString()).Replace("{neededCount}", 10.ToString());
		}

		// Token: 0x06007A5B RID: 31323 RVA: 0x0030839F File Offset: 0x0030659F
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A5C RID: 31324 RVA: 0x003083AD File Offset: 0x003065AD
		public override bool Success()
		{
			return ArtifactSelector.Instance.AnalyzedArtifactCount >= 10;
		}

		// Token: 0x06007A5D RID: 31325 RVA: 0x003083C0 File Offset: 0x003065C0
		private int GetStudiedArtifactCount()
		{
			return ArtifactSelector.Instance.AnalyzedArtifactCount;
		}

		// Token: 0x06007A5E RID: 31326 RVA: 0x003083CC File Offset: 0x003065CC
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.STUDY_ARTIFACTS.Replace("{artifactCount}", 10.ToString());
		}

		// Token: 0x040059AB RID: 22955
		private const int REQUIRED_ARTIFACT_COUNT = 10;
	}
}
