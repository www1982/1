using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000F37 RID: 3895
	public class CritterTypesWithTraits : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x06007AAD RID: 31405 RVA: 0x00308FA8 File Offset: 0x003071A8
		public CritterTypesWithTraits(List<Tag> critterTypes)
		{
			foreach (Tag tag in critterTypes)
			{
				if (!this.critterTypesToCheck.ContainsKey(tag))
				{
					this.critterTypesToCheck.Add(tag, false);
				}
			}
			this.hasTrait = false;
			this.trait = GameTags.Creatures.Wild;
		}

		// Token: 0x06007AAE RID: 31406 RVA: 0x00309038 File Offset: 0x00307238
		public override bool Success()
		{
			HashSet<Tag> tamedCritterTypes = SaveGame.Instance.ColonyAchievementTracker.tamedCritterTypes;
			bool flag = true;
			foreach (KeyValuePair<Tag, bool> keyValuePair in this.critterTypesToCheck)
			{
				flag = flag && tamedCritterTypes.Contains(keyValuePair.Key);
			}
			this.UpdateSavedState();
			return flag;
		}

		// Token: 0x06007AAF RID: 31407 RVA: 0x003090B4 File Offset: 0x003072B4
		public void UpdateSavedState()
		{
			this.revisedCritterTypesToCheckState.Clear();
			HashSet<Tag> tamedCritterTypes = SaveGame.Instance.ColonyAchievementTracker.tamedCritterTypes;
			foreach (KeyValuePair<Tag, bool> keyValuePair in this.critterTypesToCheck)
			{
				this.revisedCritterTypesToCheckState.Add(keyValuePair.Key, tamedCritterTypes.Contains(keyValuePair.Key));
			}
			foreach (KeyValuePair<Tag, bool> keyValuePair2 in this.revisedCritterTypesToCheckState)
			{
				this.critterTypesToCheck[keyValuePair2.Key] = keyValuePair2.Value;
			}
		}

		// Token: 0x06007AB0 RID: 31408 RVA: 0x00309190 File Offset: 0x00307390
		public void Deserialize(IReader reader)
		{
			this.critterTypesToCheck = new Dictionary<Tag, bool>();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = reader.ReadKleiString();
				bool flag = reader.ReadByte() > 0;
				this.critterTypesToCheck.Add(new Tag(text), flag);
			}
			this.hasTrait = reader.ReadByte() > 0;
			this.trait = GameTags.Creatures.Wild;
		}

		// Token: 0x040059B6 RID: 22966
		public Dictionary<Tag, bool> critterTypesToCheck = new Dictionary<Tag, bool>();

		// Token: 0x040059B7 RID: 22967
		private Tag trait;

		// Token: 0x040059B8 RID: 22968
		private bool hasTrait;

		// Token: 0x040059B9 RID: 22969
		private Dictionary<Tag, bool> revisedCritterTypesToCheckState = new Dictionary<Tag, bool>();
	}
}
