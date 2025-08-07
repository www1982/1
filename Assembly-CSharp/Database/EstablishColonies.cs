using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000F2D RID: 3885
	public class EstablishColonies : VictoryColonyAchievementRequirement
	{
		// Token: 0x06007A7F RID: 31359 RVA: 0x003085D4 File Offset: 0x003067D4
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.ESTABLISH_COLONIES.Replace("{goalBaseCount}", EstablishColonies.BASE_COUNT.ToString()).Replace("{baseCount}", this.GetColonyCount().ToString()).Replace("{neededCount}", EstablishColonies.BASE_COUNT.ToString());
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x0030862B File Offset: 0x0030682B
		public override string Description()
		{
			return this.GetProgress(this.Success());
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x00308639 File Offset: 0x00306839
		public override bool Success()
		{
			return this.GetColonyCount() >= EstablishColonies.BASE_COUNT;
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x0030864B File Offset: 0x0030684B
		public override string Name()
		{
			return COLONY_ACHIEVEMENTS.STUDY_ARTIFACTS.REQUIREMENTS.SEVERAL_COLONIES;
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x00308658 File Offset: 0x00306858
		private int GetColonyCount()
		{
			int num = 0;
			for (int i = 0; i < Components.Telepads.Count; i++)
			{
				Activatable component = Components.Telepads[i].GetComponent<Activatable>();
				if (component == null || component.IsActivated)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x040059AD RID: 22957
		public static int BASE_COUNT = 5;
	}
}
