using System;
using System.Linq;
using STRINGS;

namespace TUNING
{
	// Token: 0x02000F7C RID: 3964
	public class MATERIALS
	{
		// Token: 0x06007C13 RID: 31763 RVA: 0x00315DFC File Offset: 0x00313FFC
		public static string GetMaterialString(string materialCategory)
		{
			string[] array = materialCategory.Split('&', StringSplitOptions.None);
			string text;
			if (array.Length == 1)
			{
				text = UI.FormatAsLink(Strings.Get("STRINGS.MISC.TAGS." + materialCategory.ToUpper()), materialCategory);
			}
			else
			{
				text = string.Join(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.PREPARED_SEPARATOR, array.Select((string s) => UI.FormatAsLink(Strings.Get("STRINGS.MISC.TAGS." + s.ToUpper()), s)));
			}
			return text;
		}

		// Token: 0x04005B47 RID: 23367
		public const string METAL = "Metal";

		// Token: 0x04005B48 RID: 23368
		public const string REFINED_METAL = "RefinedMetal";

		// Token: 0x04005B49 RID: 23369
		public const string GLASS = "Glass";

		// Token: 0x04005B4A RID: 23370
		public const string TRANSPARENT = "Transparent";

		// Token: 0x04005B4B RID: 23371
		public const string PLASTIC = "Plastic";

		// Token: 0x04005B4C RID: 23372
		public const string BUILDABLERAW = "BuildableRaw";

		// Token: 0x04005B4D RID: 23373
		public const string PRECIOUSROCK = "PreciousRock";

		// Token: 0x04005B4E RID: 23374
		public const string WOOD = "BuildingWood";

		// Token: 0x04005B4F RID: 23375
		public const string BUILDINGFIBER = "BuildingFiber";

		// Token: 0x04005B50 RID: 23376
		public const string LEAD = "Lead";

		// Token: 0x04005B51 RID: 23377
		public const string INSULATOR = "Insulator";

		// Token: 0x04005B52 RID: 23378
		public const string FOSSILS_TAG = "Fossils";

		// Token: 0x04005B53 RID: 23379
		public static readonly string[] ALL_METALS = new string[] { "Metal" };

		// Token: 0x04005B54 RID: 23380
		public static readonly string[] RAW_METALS = new string[] { "Metal" };

		// Token: 0x04005B55 RID: 23381
		public static readonly string[] REFINED_METALS = new string[] { "RefinedMetal" };

		// Token: 0x04005B56 RID: 23382
		public static readonly string[] ALLOYS = new string[] { "Alloy" };

		// Token: 0x04005B57 RID: 23383
		public static readonly string[] ALL_MINERALS = new string[] { "BuildableRaw" };

		// Token: 0x04005B58 RID: 23384
		public static readonly string[] RAW_MINERALS = new string[] { "BuildableRaw" };

		// Token: 0x04005B59 RID: 23385
		public static readonly string[] RAW_MINERALS_OR_METALS = new string[] { "BuildableRaw&Metal" };

		// Token: 0x04005B5A RID: 23386
		public static readonly string[] RAW_MINERALS_OR_WOOD = new string[] { "BuildableRaw&" + GameTags.BuildingWood.ToString() };

		// Token: 0x04005B5B RID: 23387
		public static readonly string[] WOODS = new string[] { "BuildingWood" };

		// Token: 0x04005B5C RID: 23388
		public static readonly string[] FOSSILS = new string[] { "Fossils" };

		// Token: 0x04005B5D RID: 23389
		public static readonly string[] REFINED_MINERALS = new string[] { "BuildableProcessed" };

		// Token: 0x04005B5E RID: 23390
		public static readonly string[] PRECIOUS_ROCKS = new string[] { "PreciousRock" };

		// Token: 0x04005B5F RID: 23391
		public static readonly string[] FARMABLE = new string[] { "Farmable" };

		// Token: 0x04005B60 RID: 23392
		public static readonly string[] EXTRUDABLE = new string[] { "Extrudable" };

		// Token: 0x04005B61 RID: 23393
		public static readonly string[] PLUMBABLE = new string[] { "Plumbable" };

		// Token: 0x04005B62 RID: 23394
		public static readonly string[] PLUMBABLE_OR_METALS = new string[] { "Plumbable&Metal" };

		// Token: 0x04005B63 RID: 23395
		public static readonly string[] PLASTICS = new string[] { "Plastic" };

		// Token: 0x04005B64 RID: 23396
		public static readonly string[] GLASSES = new string[] { "Glass" };

		// Token: 0x04005B65 RID: 23397
		public static readonly string[] TRANSPARENTS = new string[] { "Transparent" };

		// Token: 0x04005B66 RID: 23398
		public static readonly string[] BUILDING_FIBER = new string[] { "BuildingFiber" };

		// Token: 0x04005B67 RID: 23399
		public static readonly string[] ANY_BUILDABLE = new string[] { "BuildableAny" };

		// Token: 0x04005B68 RID: 23400
		public static readonly string[] FLYING_CRITTER_FOOD = new string[] { "FlyingCritterEdible" };

		// Token: 0x04005B69 RID: 23401
		public static readonly string[] RADIATION_CONTAINMENT = new string[] { "Metal", "Lead" };
	}
}
