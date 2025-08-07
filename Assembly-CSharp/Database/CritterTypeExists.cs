using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000F47 RID: 3911
	public class CritterTypeExists : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AF0 RID: 31472 RVA: 0x0030A3B8 File Offset: 0x003085B8
		public CritterTypeExists(List<Tag> critterTypes)
		{
			this.critterTypes = critterTypes;
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x0030A3D4 File Offset: 0x003085D4
		public override bool Success()
		{
			foreach (Capturable capturable in Components.Capturables.Items)
			{
				if (this.critterTypes.Contains(capturable.PrefabID()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x0030A440 File Offset: 0x00308640
		public void Deserialize(IReader reader)
		{
			int num = reader.ReadInt32();
			this.critterTypes = new List<Tag>(num);
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				this.critterTypes.Add(new Tag(text));
			}
		}

		// Token: 0x06007AF3 RID: 31475 RVA: 0x0030A484 File Offset: 0x00308684
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.HATCH_A_MORPH;
		}

		// Token: 0x040059CB RID: 22987
		private List<Tag> critterTypes = new List<Tag>();
	}
}
