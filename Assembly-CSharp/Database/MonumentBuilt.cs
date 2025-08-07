using System;
using System.Collections;
using STRINGS;

namespace Database
{
	// Token: 0x02000F1F RID: 3871
	public class MonumentBuilt : VictoryColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007A33 RID: 31283 RVA: 0x00307D86 File Offset: 0x00305F86
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.BUILT_MONUMENT;
		}

		// Token: 0x06007A34 RID: 31284 RVA: 0x00307D92 File Offset: 0x00305F92
		public override string Description()
		{
			return COLONY_ACHIEVEMENTS.THRIVING.REQUIREMENTS.BUILT_MONUMENT_DESCRIPTION;
		}

		// Token: 0x06007A35 RID: 31285 RVA: 0x00307DA0 File Offset: 0x00305FA0
		public override bool Success()
		{
			using (IEnumerator enumerator = Components.MonumentParts.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((MonumentPart)enumerator.Current).IsMonumentCompleted())
					{
						Game.Instance.unlocks.Unlock("thriving", true);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06007A36 RID: 31286 RVA: 0x00307E14 File Offset: 0x00306014
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x06007A37 RID: 31287 RVA: 0x00307E16 File Offset: 0x00306016
		public override string GetProgress(bool complete)
		{
			return this.Name();
		}
	}
}
