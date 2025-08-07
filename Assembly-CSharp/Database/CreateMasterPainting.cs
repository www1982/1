using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F45 RID: 3909
	public class CreateMasterPainting : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AE8 RID: 31464 RVA: 0x0030A288 File Offset: 0x00308488
		public override bool Success()
		{
			foreach (Painting painting in Components.Paintings.Items)
			{
				if (painting != null)
				{
					ArtableStage artableStage = Db.GetArtableStages().TryGet(painting.CurrentStage);
					if (artableStage != null && artableStage.statusItem == Db.Get().ArtableStatuses.LookingGreat)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x0030A314 File Offset: 0x00308514
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x0030A316 File Offset: 0x00308516
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.CREATE_A_PAINTING;
		}
	}
}
