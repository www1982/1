using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F27 RID: 3879
	public class CollectedSpaceArtifacts : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A60 RID: 31328 RVA: 0x00308400 File Offset: 0x00306600
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.COLLECT_SPACE_ARTIFACTS.Replace("{collectedCount}", this.GetStudiedSpaceArtifactCount().ToString()).Replace("{neededCount}", 10.ToString());
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x00308443 File Offset: 0x00306643
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x00308451 File Offset: 0x00306651
		public override bool Success()
		{
			return ArtifactSelector.Instance.AnalyzedSpaceArtifactCount >= 10;
		}

		// Token: 0x06007A63 RID: 31331 RVA: 0x00308464 File Offset: 0x00306664
		private int GetStudiedSpaceArtifactCount()
		{
			return ArtifactSelector.Instance.AnalyzedSpaceArtifactCount;
		}

		// Token: 0x06007A64 RID: 31332 RVA: 0x00308470 File Offset: 0x00306670
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.STUDY_SPACE_ARTIFACTS.Replace("{artifactCount}", 10.ToString());
		}

		// Token: 0x040059AC RID: 22956
		private const int REQUIRED_ARTIFACT_COUNT = 10;
	}
}
