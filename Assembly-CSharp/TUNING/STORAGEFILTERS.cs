using System;
using System.Collections.Generic;
using System.Linq;

namespace TUNING
{
	// Token: 0x02000F94 RID: 3988
	public class STORAGEFILTERS
	{
		// Token: 0x04005CDC RID: 23772
		public static List<Tag> DEHYDRATED = new List<Tag> { GameTags.Dehydrated };

		// Token: 0x04005CDD RID: 23773
		public static List<Tag> FOOD = new List<Tag>
		{
			GameTags.Edible,
			GameTags.CookingIngredient,
			GameTags.Medicine
		};

		// Token: 0x04005CDE RID: 23774
		public static List<Tag> BAGABLE_CREATURES = new List<Tag> { GameTags.BagableCreature };

		// Token: 0x04005CDF RID: 23775
		public static List<Tag> SWIMMING_CREATURES = new List<Tag> { GameTags.SwimmingCreature };

		// Token: 0x04005CE0 RID: 23776
		public static List<Tag> NOT_EDIBLE_SOLIDS = new List<Tag>
		{
			GameTags.Alloy,
			GameTags.RefinedMetal,
			GameTags.Metal,
			GameTags.BuildableRaw,
			GameTags.BuildableProcessed,
			GameTags.Farmable,
			GameTags.Organics,
			GameTags.Compostable,
			GameTags.Seed,
			GameTags.Agriculture,
			GameTags.Filter,
			GameTags.ConsumableOre,
			GameTags.Sublimating,
			GameTags.Liquifiable,
			GameTags.IndustrialProduct,
			GameTags.IndustrialIngredient,
			GameTags.MedicalSupplies,
			GameTags.Clothes,
			GameTags.ManufacturedMaterial,
			GameTags.Egg,
			GameTags.RareMaterials,
			GameTags.Other,
			GameTags.StoryTraitResource,
			GameTags.Dehydrated,
			GameTags.ChargedPortableBattery,
			GameTags.BionicUpgrade
		};

		// Token: 0x04005CE1 RID: 23777
		public static List<Tag> SPECIAL_STORAGE = new List<Tag>
		{
			GameTags.Clothes,
			GameTags.Egg,
			GameTags.Sublimating
		};

		// Token: 0x04005CE2 RID: 23778
		public static List<Tag> STORAGE_LOCKERS_STANDARD = STORAGEFILTERS.NOT_EDIBLE_SOLIDS.Union(new List<Tag> { GameTags.Medicine }).ToList<Tag>();

		// Token: 0x04005CE3 RID: 23779
		public static List<Tag> POWER_BANKS = new List<Tag> { GameTags.ChargedPortableBattery };

		// Token: 0x04005CE4 RID: 23780
		public static List<Tag> LIQUIDS = new List<Tag> { GameTags.Liquid };

		// Token: 0x04005CE5 RID: 23781
		public static List<Tag> GASES = new List<Tag>
		{
			GameTags.Breathable,
			GameTags.Unbreathable
		};

		// Token: 0x04005CE6 RID: 23782
		public static List<Tag> PAYLOADS = new List<Tag> { "RailGunPayload" };

		// Token: 0x04005CE7 RID: 23783
		public static Tag[] SOLID_TRANSFER_ARM_CONVEYABLE = new List<Tag>
		{
			GameTags.Seed,
			GameTags.CropSeed
		}.Concat(STORAGEFILTERS.STORAGE_LOCKERS_STANDARD.Concat(STORAGEFILTERS.FOOD).Concat(STORAGEFILTERS.PAYLOADS)).ToArray<Tag>();
	}
}
