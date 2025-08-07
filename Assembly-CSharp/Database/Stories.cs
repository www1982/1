using System;
using System.Collections.Generic;
using ProcGen;
using UnityEngine;

namespace Database
{
	// Token: 0x02000F15 RID: 3861
	public class Stories : ResourceSet<Story>
	{
		// Token: 0x06007A05 RID: 31237 RVA: 0x00304004 File Offset: 0x00302204
		public Stories(ResourceSet parent)
			: base("Stories", parent)
		{
			this.MegaBrainTank = base.Add(new Story("MegaBrainTank", "storytraits/MegaBrainTank", 0, 1, 43, "storytraits/mega_brain_tank").SetKeepsake("keepsake_megabrain"));
			this.CreatureManipulator = base.Add(new Story("CreatureManipulator", "storytraits/CritterManipulator", 1, 2, 43, "storytraits/creature_manipulator_retrofit").SetKeepsake("keepsake_crittermanipulator"));
			this.LonelyMinion = base.Add(new Story("LonelyMinion", "storytraits/LonelyMinion", 2, 3, 44, "storytraits/lonelyminion_retrofit").SetKeepsake("keepsake_lonelyminion"));
			this.FossilHunt = base.Add(new Story("FossilHunt", "storytraits/FossilHunt", 3, 4, 44, "storytraits/fossil_hunt_retrofit").SetKeepsake("keepsake_fossilhunt"));
			this.MorbRoverMaker = base.Add(new Story("MorbRoverMaker", "storytraits/MorbRoverMaker", 4, 5, 50, "storytraits/morb_rover_maker_retrofit").SetKeepsake("keepsake_morbrovermaker"));
			this.resources.Sort();
		}

		// Token: 0x06007A06 RID: 31238 RVA: 0x0030410E File Offset: 0x0030230E
		public void AddStoryMod(Story mod)
		{
			mod.kleiUseOnlyCoordinateOrder = -1;
			base.Add(mod);
			this.resources.Sort();
		}

		// Token: 0x06007A07 RID: 31239 RVA: 0x0030412C File Offset: 0x0030232C
		public int GetHighestCoordinate()
		{
			int num = 0;
			foreach (Story story in this.resources)
			{
				num = Mathf.Max(num, story.kleiUseOnlyCoordinateOrder);
			}
			return num;
		}

		// Token: 0x06007A08 RID: 31240 RVA: 0x00304188 File Offset: 0x00302388
		public WorldTrait GetStoryTrait(string id, bool assertMissingTrait = false)
		{
			Story story = this.resources.Find((Story x) => x.Id == id);
			if (story != null)
			{
				return SettingsCache.GetCachedStoryTrait(story.worldgenStoryTraitKey, assertMissingTrait);
			}
			return null;
		}

		// Token: 0x06007A09 RID: 31241 RVA: 0x003041CC File Offset: 0x003023CC
		public Story GetStoryFromStoryTrait(string storyTraitTemplate)
		{
			return this.resources.Find((Story x) => x.worldgenStoryTraitKey == storyTraitTemplate);
		}

		// Token: 0x06007A0A RID: 31242 RVA: 0x003041FD File Offset: 0x003023FD
		public List<Story> GetStoriesSortedByCoordinateOrder()
		{
			List<Story> list = new List<Story>(this.resources);
			list.Sort((Story s1, Story s2) => s1.kleiUseOnlyCoordinateOrder.CompareTo(s2.kleiUseOnlyCoordinateOrder));
			return list;
		}

		// Token: 0x0400593D RID: 22845
		public Story MegaBrainTank;

		// Token: 0x0400593E RID: 22846
		public Story CreatureManipulator;

		// Token: 0x0400593F RID: 22847
		public Story LonelyMinion;

		// Token: 0x04005940 RID: 22848
		public Story FossilHunt;

		// Token: 0x04005941 RID: 22849
		public Story MorbRoverMaker;
	}
}
