using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000F04 RID: 3844
	public class Quests : ResourceSet<Quest>
	{
		// Token: 0x060079D0 RID: 31184 RVA: 0x00300820 File Offset: 0x002FEA20
		public Quests(ResourceSet parent)
			: base("Quests", parent)
		{
			this.LonelyMinionGreetingQuest = base.Add(new Quest("KnockQuest", new QuestCriteria[]
			{
				new QuestCriteria("Neighbor", null, 1, null, QuestCriteria.BehaviorFlags.None)
			}));
			this.LonelyMinionFoodQuest = base.Add(new Quest("FoodQuest", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("FoodQuality", new float[] { 4f }, 3, new HashSet<Tag> { GameTags.Edible }, QuestCriteria.BehaviorFlags.UniqueItems)
			}));
			this.LonelyMinionPowerQuest = base.Add(new Quest("PluggedIn", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("SuppliedPower", new float[] { 3000f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues)
			}));
			this.LonelyMinionDecorQuest = base.Add(new Quest("HighDecor", new QuestCriteria[]
			{
				new QuestCriteria_GreaterOrEqual("Decor", new float[] { 120f }, 1, null, (QuestCriteria.BehaviorFlags)6)
			}));
			this.FossilHuntQuest = base.Add(new Quest("FossilHuntQuest", new QuestCriteria[]
			{
				new QuestCriteria_Equals("LostSpecimen", new float[] { 1f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues),
				new QuestCriteria_Equals("LostIceFossil", new float[] { 1f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues),
				new QuestCriteria_Equals("LostResinFossil", new float[] { 1f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues),
				new QuestCriteria_Equals("LostRockFossil", new float[] { 1f }, 1, null, QuestCriteria.BehaviorFlags.TrackValues)
			}));
		}

		// Token: 0x040058AA RID: 22698
		public Quest LonelyMinionGreetingQuest;

		// Token: 0x040058AB RID: 22699
		public Quest LonelyMinionFoodQuest;

		// Token: 0x040058AC RID: 22700
		public Quest LonelyMinionPowerQuest;

		// Token: 0x040058AD RID: 22701
		public Quest LonelyMinionDecorQuest;

		// Token: 0x040058AE RID: 22702
		public Quest FossilHuntQuest;
	}
}
