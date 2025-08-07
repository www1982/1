using System;

namespace TUNING
{
	// Token: 0x02000F95 RID: 3989
	public class EQUIPMENT
	{
		// Token: 0x02002173 RID: 8563
		public class TOYS
		{
			// Token: 0x04009995 RID: 39317
			public static string SLOT = "Toy";

			// Token: 0x04009996 RID: 39318
			public static float BALLOON_MASS = 1f;
		}

		// Token: 0x02002174 RID: 8564
		public class ATTRIBUTE_MOD_IDS
		{
			// Token: 0x04009997 RID: 39319
			public static string DECOR = "Decor";

			// Token: 0x04009998 RID: 39320
			public static string INSULATION = "Insulation";

			// Token: 0x04009999 RID: 39321
			public static string ATHLETICS = "Athletics";

			// Token: 0x0400999A RID: 39322
			public static string DIGGING = "Digging";

			// Token: 0x0400999B RID: 39323
			public static string MAX_UNDERWATER_TRAVELCOST = "MaxUnderwaterTravelCost";

			// Token: 0x0400999C RID: 39324
			public static string THERMAL_CONDUCTIVITY_BARRIER = "ThermalConductivityBarrier";
		}

		// Token: 0x02002175 RID: 8565
		public class TOOLS
		{
			// Token: 0x0400999D RID: 39325
			public static string TOOLSLOT = "Multitool";

			// Token: 0x0400999E RID: 39326
			public static string TOOLFABRICATOR = "MultitoolWorkbench";

			// Token: 0x0400999F RID: 39327
			public static string TOOL_ANIM = "constructor_gun_kanim";
		}

		// Token: 0x02002176 RID: 8566
		public class CLOTHING
		{
			// Token: 0x040099A0 RID: 39328
			public static string SLOT = "Outfit";
		}

		// Token: 0x02002177 RID: 8567
		public class SUITS
		{
			// Token: 0x040099A1 RID: 39329
			public static string SLOT = "Suit";

			// Token: 0x040099A2 RID: 39330
			public static string FABRICATOR = "SuitFabricator";

			// Token: 0x040099A3 RID: 39331
			public static string ANIM = "clothing_kanim";

			// Token: 0x040099A4 RID: 39332
			public static string SNAPON = "snapTo_neck";

			// Token: 0x040099A5 RID: 39333
			public static float SUIT_DURABILITY_SKILL_BONUS = 0.25f;

			// Token: 0x040099A6 RID: 39334
			public static int OXYMASK_FABTIME = 20;

			// Token: 0x040099A7 RID: 39335
			public static int ATMOSUIT_FABTIME = 40;

			// Token: 0x040099A8 RID: 39336
			public static int ATMOSUIT_INSULATION = 50;

			// Token: 0x040099A9 RID: 39337
			public static int ATMOSUIT_ATHLETICS = -6;

			// Token: 0x040099AA RID: 39338
			public static float ATMOSUIT_THERMAL_CONDUCTIVITY_BARRIER = 0.2f;

			// Token: 0x040099AB RID: 39339
			public static int ATMOSUIT_DIGGING = 10;

			// Token: 0x040099AC RID: 39340
			public static int ATMOSUIT_CONSTRUCTION = 10;

			// Token: 0x040099AD RID: 39341
			public static float ATMOSUIT_BLADDER = -0.18333334f;

			// Token: 0x040099AE RID: 39342
			public static int ATMOSUIT_MASS = 200;

			// Token: 0x040099AF RID: 39343
			public static int ATMOSUIT_SCALDING = 1000;

			// Token: 0x040099B0 RID: 39344
			public static int ATMOSUIT_SCOLDING = -1000;

			// Token: 0x040099B1 RID: 39345
			public static float ATMOSUIT_DECAY = -0.1f;

			// Token: 0x040099B2 RID: 39346
			public static float LEADSUIT_THERMAL_CONDUCTIVITY_BARRIER = 0.3f;

			// Token: 0x040099B3 RID: 39347
			public static int LEADSUIT_SCALDING = 1000;

			// Token: 0x040099B4 RID: 39348
			public static int LEADSUIT_SCOLDING = -1000;

			// Token: 0x040099B5 RID: 39349
			public static int LEADSUIT_INSULATION = 50;

			// Token: 0x040099B6 RID: 39350
			public static int LEADSUIT_STRENGTH = 10;

			// Token: 0x040099B7 RID: 39351
			public static int LEADSUIT_ATHLETICS = -8;

			// Token: 0x040099B8 RID: 39352
			public static float LEADSUIT_RADIATION_SHIELDING = 0.66f;

			// Token: 0x040099B9 RID: 39353
			public static int AQUASUIT_FABTIME = EQUIPMENT.SUITS.ATMOSUIT_FABTIME;

			// Token: 0x040099BA RID: 39354
			public static int AQUASUIT_INSULATION = 0;

			// Token: 0x040099BB RID: 39355
			public static int AQUASUIT_ATHLETICS = EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS;

			// Token: 0x040099BC RID: 39356
			public static int AQUASUIT_MASS = EQUIPMENT.SUITS.ATMOSUIT_MASS;

			// Token: 0x040099BD RID: 39357
			public static int AQUASUIT_UNDERWATER_TRAVELCOST = 6;

			// Token: 0x040099BE RID: 39358
			public static int TEMPERATURESUIT_FABTIME = EQUIPMENT.SUITS.ATMOSUIT_FABTIME;

			// Token: 0x040099BF RID: 39359
			public static float TEMPERATURESUIT_INSULATION = 0.2f;

			// Token: 0x040099C0 RID: 39360
			public static int TEMPERATURESUIT_ATHLETICS = EQUIPMENT.SUITS.ATMOSUIT_ATHLETICS;

			// Token: 0x040099C1 RID: 39361
			public static int TEMPERATURESUIT_MASS = EQUIPMENT.SUITS.ATMOSUIT_MASS;

			// Token: 0x040099C2 RID: 39362
			public const int OXYGEN_MASK_MASS = 15;

			// Token: 0x040099C3 RID: 39363
			public static int OXYGEN_MASK_ATHLETICS = -2;

			// Token: 0x040099C4 RID: 39364
			public static float OXYGEN_MASK_DECAY = -0.2f;

			// Token: 0x040099C5 RID: 39365
			public static float INDESTRUCTIBLE_DURABILITY_MOD = 0f;

			// Token: 0x040099C6 RID: 39366
			public static float REINFORCED_DURABILITY_MOD = 0.5f;

			// Token: 0x040099C7 RID: 39367
			public static float FLIMSY_DURABILITY_MOD = 1.5f;

			// Token: 0x040099C8 RID: 39368
			public static float THREADBARE_DURABILITY_MOD = 2f;

			// Token: 0x040099C9 RID: 39369
			public static float MINIMUM_USABLE_SUIT_CHARGE = 0.95f;
		}

		// Token: 0x02002178 RID: 8568
		public class VESTS
		{
			// Token: 0x040099CA RID: 39370
			public static string SLOT = "Suit";

			// Token: 0x040099CB RID: 39371
			public static string FABRICATOR = "ClothingFabricator";

			// Token: 0x040099CC RID: 39372
			public static string SNAPON0 = "snapTo_body";

			// Token: 0x040099CD RID: 39373
			public static string SNAPON1 = "snapTo_arm";

			// Token: 0x040099CE RID: 39374
			public static string WARM_VEST_ANIM0 = "body_shirt_hot_shearling_kanim";

			// Token: 0x040099CF RID: 39375
			public static string WARM_VEST_ICON0 = "shirt_hot_shearling_kanim";

			// Token: 0x040099D0 RID: 39376
			public static float WARM_VEST_FABTIME = 180f;

			// Token: 0x040099D1 RID: 39377
			public static float WARM_VEST_INSULATION = 0.01f;

			// Token: 0x040099D2 RID: 39378
			public static int WARM_VEST_MASS = 4;

			// Token: 0x040099D3 RID: 39379
			public static float COOL_VEST_FABTIME = EQUIPMENT.VESTS.WARM_VEST_FABTIME;

			// Token: 0x040099D4 RID: 39380
			public static float COOL_VEST_INSULATION = 0.01f;

			// Token: 0x040099D5 RID: 39381
			public static int COOL_VEST_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS;

			// Token: 0x040099D6 RID: 39382
			public static float FUNKY_VEST_FABTIME = EQUIPMENT.VESTS.WARM_VEST_FABTIME;

			// Token: 0x040099D7 RID: 39383
			public static float FUNKY_VEST_DECOR = 1f;

			// Token: 0x040099D8 RID: 39384
			public static int FUNKY_VEST_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS;

			// Token: 0x040099D9 RID: 39385
			public static float CUSTOM_CLOTHING_FABTIME = 180f;

			// Token: 0x040099DA RID: 39386
			public static float CUSTOM_ATMOSUIT_FABTIME = 15f;

			// Token: 0x040099DB RID: 39387
			public static int CUSTOM_CLOTHING_MASS = EQUIPMENT.VESTS.WARM_VEST_MASS + 3;
		}
	}
}
