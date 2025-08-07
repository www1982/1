using System;
using System.Collections.Generic;
using System.Reflection;
using STRINGS;

// Token: 0x0200092F RID: 2351
public class GameTags
{
	// Token: 0x060041DA RID: 16858 RVA: 0x0017834C File Offset: 0x0017654C
	public static Tag[] Reflection_GetTagsInClass(Type classAddress, BindingFlags variableFlags = BindingFlags.Static | BindingFlags.Public)
	{
		List<FieldInfo> list = new List<FieldInfo>(classAddress.GetFields(variableFlags)).FindAll((FieldInfo f) => f.FieldType == typeof(Tag));
		Tag[] array = new Tag[list.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = list[i].Name;
		}
		return array;
	}

	// Token: 0x04002B57 RID: 11095
	public static readonly Tag DeprecatedContent = TagManager.Create("DeprecatedContent");

	// Token: 0x04002B58 RID: 11096
	public static readonly Tag Any = TagManager.Create("Any");

	// Token: 0x04002B59 RID: 11097
	public static readonly Tag SpawnsInWorld = TagManager.Create("SpawnsInWorld");

	// Token: 0x04002B5A RID: 11098
	public static readonly Tag Experimental = TagManager.Create("Experimental");

	// Token: 0x04002B5B RID: 11099
	public static readonly Tag Gravitas = TagManager.Create("Gravitas");

	// Token: 0x04002B5C RID: 11100
	public static readonly Tag Miscellaneous = TagManager.Create("Miscellaneous");

	// Token: 0x04002B5D RID: 11101
	public static readonly Tag Specimen = TagManager.Create("Specimen");

	// Token: 0x04002B5E RID: 11102
	public static readonly Tag Seed = TagManager.Create("Seed");

	// Token: 0x04002B5F RID: 11103
	public static readonly Tag Dehydrated = TagManager.Create("Dehydrated");

	// Token: 0x04002B60 RID: 11104
	public static readonly Tag Rehydrated = TagManager.Create("Rehydrated");

	// Token: 0x04002B61 RID: 11105
	public static readonly Tag Edible = TagManager.Create("Edible");

	// Token: 0x04002B62 RID: 11106
	public static readonly Tag CookingIngredient = TagManager.Create("CookingIngredient");

	// Token: 0x04002B63 RID: 11107
	public static readonly Tag Medicine = TagManager.Create("Medicine");

	// Token: 0x04002B64 RID: 11108
	public static readonly Tag MedicalSupplies = TagManager.Create("MedicalSupplies");

	// Token: 0x04002B65 RID: 11109
	public static readonly Tag Plant = TagManager.Create("Plant");

	// Token: 0x04002B66 RID: 11110
	public static readonly Tag PlantBranch = TagManager.Create("PlantBranch");

	// Token: 0x04002B67 RID: 11111
	public static readonly Tag GrowingPlant = TagManager.Create("GrowingPlant");

	// Token: 0x04002B68 RID: 11112
	public static readonly Tag FullyGrown = TagManager.Create("FullyGrown");

	// Token: 0x04002B69 RID: 11113
	public static readonly Tag PlantedOnFloorVessel = TagManager.Create("PlantedOnFloorVessel");

	// Token: 0x04002B6A RID: 11114
	public static readonly Tag Pickupable = TagManager.Create("Pickupable");

	// Token: 0x04002B6B RID: 11115
	public static readonly Tag Liquifiable = TagManager.Create("Liquifiable");

	// Token: 0x04002B6C RID: 11116
	public static readonly Tag IceOre = TagManager.Create("IceOre");

	// Token: 0x04002B6D RID: 11117
	public static readonly Tag OxyRock = TagManager.Create("OxyRock");

	// Token: 0x04002B6E RID: 11118
	public static readonly Tag Life = TagManager.Create("Life");

	// Token: 0x04002B6F RID: 11119
	public static readonly Tag Fertilizer = TagManager.Create("Fertilizer");

	// Token: 0x04002B70 RID: 11120
	public static readonly Tag Farmable = TagManager.Create("Farmable");

	// Token: 0x04002B71 RID: 11121
	public static readonly Tag Agriculture = TagManager.Create("Agriculture");

	// Token: 0x04002B72 RID: 11122
	public static readonly Tag Organics = TagManager.Create("Organics");

	// Token: 0x04002B73 RID: 11123
	public static readonly Tag IndustrialProduct = TagManager.Create("IndustrialProduct");

	// Token: 0x04002B74 RID: 11124
	public static readonly Tag IndustrialIngredient = TagManager.Create("IndustrialIngredient");

	// Token: 0x04002B75 RID: 11125
	public static readonly Tag Other = TagManager.Create("Other");

	// Token: 0x04002B76 RID: 11126
	public static readonly Tag ManufacturedMaterial = TagManager.Create("ManufacturedMaterial");

	// Token: 0x04002B77 RID: 11127
	public static readonly Tag Plastic = TagManager.Create("Plastic");

	// Token: 0x04002B78 RID: 11128
	public static readonly Tag Steel = TagManager.Create("Steel");

	// Token: 0x04002B79 RID: 11129
	public static readonly Tag BuildableAny = TagManager.Create("BuildableAny");

	// Token: 0x04002B7A RID: 11130
	public static readonly Tag Decoration = TagManager.Create("Decoration");

	// Token: 0x04002B7B RID: 11131
	public static readonly Tag Window = TagManager.Create("Window");

	// Token: 0x04002B7C RID: 11132
	public static readonly Tag Bunker = TagManager.Create("Bunker");

	// Token: 0x04002B7D RID: 11133
	public static readonly Tag Transition = TagManager.Create("Transition");

	// Token: 0x04002B7E RID: 11134
	public static readonly Tag Detecting = TagManager.Create("Detecting");

	// Token: 0x04002B7F RID: 11135
	public static readonly Tag RareMaterials = TagManager.Create("RareMaterials");

	// Token: 0x04002B80 RID: 11136
	public static readonly Tag BuildingFiber = TagManager.Create("BuildingFiber");

	// Token: 0x04002B81 RID: 11137
	public static readonly Tag Transparent = TagManager.Create("Transparent");

	// Token: 0x04002B82 RID: 11138
	public static readonly Tag Insulator = TagManager.Create("Insulator");

	// Token: 0x04002B83 RID: 11139
	public static readonly Tag Plumbable = TagManager.Create("Plumbable");

	// Token: 0x04002B84 RID: 11140
	public static readonly Tag BuildingWood = TagManager.Create("BuildingWood");

	// Token: 0x04002B85 RID: 11141
	public static readonly Tag PreciousRock = TagManager.Create("PreciousRock");

	// Token: 0x04002B86 RID: 11142
	public static readonly Tag Artifact = TagManager.Create("Artifact");

	// Token: 0x04002B87 RID: 11143
	public static readonly Tag BionicUpgrade = TagManager.Create("BionicUpgrade");

	// Token: 0x04002B88 RID: 11144
	public static readonly Tag BionicBedTime = TagManager.Create("BionicBedTime");

	// Token: 0x04002B89 RID: 11145
	public static readonly Tag CharmedArtifact = TagManager.Create("CharmedArtifact");

	// Token: 0x04002B8A RID: 11146
	public static readonly Tag TerrestrialArtifact = TagManager.Create("TerrestrialArtifact");

	// Token: 0x04002B8B RID: 11147
	public static readonly Tag Keepsake = TagManager.Create("Keepsake");

	// Token: 0x04002B8C RID: 11148
	public static readonly Tag MiscPickupable = TagManager.Create("MiscPickupable");

	// Token: 0x04002B8D RID: 11149
	public static readonly Tag PlastifiableLiquid = TagManager.Create("PlastifiableLiquid");

	// Token: 0x04002B8E RID: 11150
	public static readonly Tag CombustibleGas = TagManager.Create("CombustibleGas");

	// Token: 0x04002B8F RID: 11151
	public static readonly Tag CombustibleLiquid = TagManager.Create("CombustibleLiquid");

	// Token: 0x04002B90 RID: 11152
	public static readonly Tag CombustibleSolid = TagManager.Create("CombustibleSolid");

	// Token: 0x04002B91 RID: 11153
	public static readonly Tag FlyingCritterEdible = TagManager.Create("FlyingCritterEdible");

	// Token: 0x04002B92 RID: 11154
	public static readonly Tag Comet = TagManager.Create("Comet");

	// Token: 0x04002B93 RID: 11155
	public static readonly Tag DeadReactor = TagManager.Create("DeadReactor");

	// Token: 0x04002B94 RID: 11156
	public static readonly Tag Robot = TagManager.Create("Robot");

	// Token: 0x04002B95 RID: 11157
	public static readonly Tag StoryTraitResource = TagManager.Create("StoryTraitResource");

	// Token: 0x04002B96 RID: 11158
	public static readonly Tag RoomProberBuilding = TagManager.Create("RoomProberBuilding");

	// Token: 0x04002B97 RID: 11159
	public static readonly Tag DevBuilding = TagManager.Create("DevBuilding");

	// Token: 0x04002B98 RID: 11160
	public static readonly Tag MarkedForMove = TagManager.Create("MarkedForMove");

	// Token: 0x04002B99 RID: 11161
	public static readonly Tag HideHealthBar = TagManager.Create("HideHealthBar");

	// Token: 0x04002B9A RID: 11162
	public static readonly Tag Incapacitated = TagManager.Create("Incapacitated");

	// Token: 0x04002B9B RID: 11163
	public static readonly Tag CaloriesDepleted = TagManager.Create("CaloriesDepleted");

	// Token: 0x04002B9C RID: 11164
	public static readonly Tag HitPointsDepleted = TagManager.Create("HitPointsDepleted");

	// Token: 0x04002B9D RID: 11165
	public static readonly Tag RadiationSicknessIncapacitation = TagManager.Create("RadiationSickness");

	// Token: 0x04002B9E RID: 11166
	public static readonly Tag Wilting = TagManager.Create("Wilting");

	// Token: 0x04002B9F RID: 11167
	public static readonly Tag Blighted = TagManager.Create("Blighted");

	// Token: 0x04002BA0 RID: 11168
	public static readonly Tag PreventEmittingDisease = TagManager.Create("EmittingDisease");

	// Token: 0x04002BA1 RID: 11169
	public static readonly Tag Creature = TagManager.Create("Creature");

	// Token: 0x04002BA2 RID: 11170
	public static readonly Tag OriginalCreature = TagManager.Create("OriginalCreature");

	// Token: 0x04002BA3 RID: 11171
	public static readonly Tag Hexaped = TagManager.Create("Hexaped");

	// Token: 0x04002BA4 RID: 11172
	public static readonly Tag HeatBulb = TagManager.Create("HeatBulb");

	// Token: 0x04002BA5 RID: 11173
	public static readonly Tag Egg = TagManager.Create("Egg");

	// Token: 0x04002BA6 RID: 11174
	public static readonly Tag IncubatableEgg = TagManager.Create("IncubatableEgg");

	// Token: 0x04002BA7 RID: 11175
	public static readonly Tag Trapped = TagManager.Create("Trapped");

	// Token: 0x04002BA8 RID: 11176
	public static readonly Tag BagableCreature = TagManager.Create("BagableCreature");

	// Token: 0x04002BA9 RID: 11177
	public static readonly Tag SwimmingCreature = TagManager.Create("SwimmingCreature");

	// Token: 0x04002BAA RID: 11178
	public static readonly Tag Spawner = TagManager.Create("Spawner");

	// Token: 0x04002BAB RID: 11179
	public static readonly Tag FullyIncubated = TagManager.Create("FullyIncubated");

	// Token: 0x04002BAC RID: 11180
	public static readonly Tag Amphibious = TagManager.Create("Amphibious");

	// Token: 0x04002BAD RID: 11181
	public static readonly Tag LargeCreature = TagManager.Create("LargeCreature");

	// Token: 0x04002BAE RID: 11182
	public static readonly Tag MoltShell = TagManager.Create("MoltShell");

	// Token: 0x04002BAF RID: 11183
	public static readonly Tag BaseMinion = TagManager.Create("BaseMinion");

	// Token: 0x04002BB0 RID: 11184
	public static readonly Tag Corpse = TagManager.Create("Corpse");

	// Token: 0x04002BB1 RID: 11185
	public static readonly Tag Alloy = TagManager.Create("Alloy");

	// Token: 0x04002BB2 RID: 11186
	public static readonly Tag Metal = TagManager.Create("Metal");

	// Token: 0x04002BB3 RID: 11187
	public static readonly Tag RefinedMetal = TagManager.Create("RefinedMetal");

	// Token: 0x04002BB4 RID: 11188
	public static readonly Tag PreciousMetal = TagManager.Create("PreciousMetal");

	// Token: 0x04002BB5 RID: 11189
	public static readonly Tag StoredMetal = TagManager.Create("StoredMetal");

	// Token: 0x04002BB6 RID: 11190
	public static readonly Tag Solid = TagManager.Create("Solid");

	// Token: 0x04002BB7 RID: 11191
	public static readonly Tag Liquid = TagManager.Create("Liquid");

	// Token: 0x04002BB8 RID: 11192
	public static readonly Tag LiquidSource = TagManager.Create("LiquidSource");

	// Token: 0x04002BB9 RID: 11193
	public static readonly Tag GasSource = TagManager.Create("GasSource");

	// Token: 0x04002BBA RID: 11194
	public static readonly Tag Water = TagManager.Create("Water");

	// Token: 0x04002BBB RID: 11195
	public static readonly Tag DirtyWater = TagManager.Create("DirtyWater");

	// Token: 0x04002BBC RID: 11196
	public static readonly Tag AnyWater = TagManager.Create("AnyWater");

	// Token: 0x04002BBD RID: 11197
	public static readonly Tag LubricatingOil = TagManager.Create("LubricatingOil");

	// Token: 0x04002BBE RID: 11198
	public static readonly Tag Algae = TagManager.Create("Algae");

	// Token: 0x04002BBF RID: 11199
	public static readonly Tag Void = TagManager.Create("Void");

	// Token: 0x04002BC0 RID: 11200
	public static readonly Tag Chlorine = TagManager.Create("Chlorine");

	// Token: 0x04002BC1 RID: 11201
	public static readonly Tag Oxygen = TagManager.Create("Oxygen");

	// Token: 0x04002BC2 RID: 11202
	public static readonly Tag Hydrogen = TagManager.Create("Hydrogen");

	// Token: 0x04002BC3 RID: 11203
	public static readonly Tag Methane = TagManager.Create("Methane");

	// Token: 0x04002BC4 RID: 11204
	public static readonly Tag CarbonDioxide = TagManager.Create("CarbonDioxide");

	// Token: 0x04002BC5 RID: 11205
	public static readonly Tag Carbon = TagManager.Create("Carbon");

	// Token: 0x04002BC6 RID: 11206
	public static readonly Tag BuildableRaw = TagManager.Create("BuildableRaw");

	// Token: 0x04002BC7 RID: 11207
	public static readonly Tag BuildableProcessed = TagManager.Create("BuildableProcessed");

	// Token: 0x04002BC8 RID: 11208
	public static readonly Tag Phosphorus = TagManager.Create("Phosphorus");

	// Token: 0x04002BC9 RID: 11209
	public static readonly Tag Phosphorite = TagManager.Create("Phosphorite");

	// Token: 0x04002BCA RID: 11210
	public static readonly Tag SlimeMold = TagManager.Create("SlimeMold");

	// Token: 0x04002BCB RID: 11211
	public static readonly Tag Filler = TagManager.Create("Filler");

	// Token: 0x04002BCC RID: 11212
	public static readonly Tag Item = TagManager.Create("Item");

	// Token: 0x04002BCD RID: 11213
	public static readonly Tag Ore = TagManager.Create("Ore");

	// Token: 0x04002BCE RID: 11214
	public static readonly Tag GenericOre = TagManager.Create("GenericOre");

	// Token: 0x04002BCF RID: 11215
	public static readonly Tag Ingot = TagManager.Create("Ingot");

	// Token: 0x04002BD0 RID: 11216
	public static readonly Tag Dirt = TagManager.Create("Dirt");

	// Token: 0x04002BD1 RID: 11217
	public static readonly Tag Filter = TagManager.Create("Filter");

	// Token: 0x04002BD2 RID: 11218
	public static readonly Tag ConsumableOre = TagManager.Create("ConsumableOre");

	// Token: 0x04002BD3 RID: 11219
	public static readonly Tag Unstable = TagManager.Create("Unstable");

	// Token: 0x04002BD4 RID: 11220
	public static readonly Tag Slippery = TagManager.Create("Slippery");

	// Token: 0x04002BD5 RID: 11221
	public static readonly Tag Sublimating = TagManager.Create("Sublimating");

	// Token: 0x04002BD6 RID: 11222
	public static readonly Tag HideFromSpawnTool = TagManager.Create("HideFromSpawnTool");

	// Token: 0x04002BD7 RID: 11223
	public static readonly Tag HideFromCodex = TagManager.Create("HideFromCodex");

	// Token: 0x04002BD8 RID: 11224
	public static readonly Tag EmitsLight = TagManager.Create("EmitsLight");

	// Token: 0x04002BD9 RID: 11225
	public static readonly Tag Special = TagManager.Create("Special");

	// Token: 0x04002BDA RID: 11226
	public static readonly Tag Breathable = TagManager.Create("Breathable");

	// Token: 0x04002BDB RID: 11227
	public static readonly Tag Unbreathable = TagManager.Create("Unbreathable");

	// Token: 0x04002BDC RID: 11228
	public static readonly Tag Gas = TagManager.Create("Gas");

	// Token: 0x04002BDD RID: 11229
	public static readonly Tag Crushable = TagManager.Create("Crushable");

	// Token: 0x04002BDE RID: 11230
	public static readonly Tag Noncrushable = TagManager.Create("Noncrushable");

	// Token: 0x04002BDF RID: 11231
	public static readonly Tag IronOre = TagManager.Create("IronOre");

	// Token: 0x04002BE0 RID: 11232
	public static readonly Tag HighEnergyParticle = TagManager.Create("HighEnergyParticle");

	// Token: 0x04002BE1 RID: 11233
	public static readonly Tag IgnoreMaterialCategory = TagManager.Create("IgnoreMaterialCategory");

	// Token: 0x04002BE2 RID: 11234
	public static readonly Tag Oxidizer = TagManager.Create("Oxidizer");

	// Token: 0x04002BE3 RID: 11235
	public static readonly Tag UnrefinedOil = TagManager.Create("UnrefinedOil");

	// Token: 0x04002BE4 RID: 11236
	public static readonly Tag RiverSource = TagManager.Create("RiverSource");

	// Token: 0x04002BE5 RID: 11237
	public static readonly Tag RiverSink = TagManager.Create("RiverSink");

	// Token: 0x04002BE6 RID: 11238
	public static readonly Tag Garbage = TagManager.Create("Garbage");

	// Token: 0x04002BE7 RID: 11239
	public static readonly Tag OilWell = TagManager.Create("OilWell");

	// Token: 0x04002BE8 RID: 11240
	public static readonly Tag Glass = TagManager.Create("Glass");

	// Token: 0x04002BE9 RID: 11241
	public static readonly Tag Door = TagManager.Create("Door");

	// Token: 0x04002BEA RID: 11242
	public static readonly Tag Farm = TagManager.Create("Farm");

	// Token: 0x04002BEB RID: 11243
	public static readonly Tag StorageLocker = TagManager.Create("StorageLocker");

	// Token: 0x04002BEC RID: 11244
	public static readonly Tag LadderBed = TagManager.Create("LadderBed");

	// Token: 0x04002BED RID: 11245
	public static readonly Tag FloorTiles = TagManager.Create("FloorTiles");

	// Token: 0x04002BEE RID: 11246
	public static readonly Tag Carpeted = TagManager.Create("Carpeted");

	// Token: 0x04002BEF RID: 11247
	public static readonly Tag FarmTiles = TagManager.Create("FarmTiles");

	// Token: 0x04002BF0 RID: 11248
	public static readonly Tag Ladders = TagManager.Create("Ladders");

	// Token: 0x04002BF1 RID: 11249
	public static readonly Tag NavTeleporters = TagManager.Create("NavTeleporters");

	// Token: 0x04002BF2 RID: 11250
	public static readonly Tag Wires = TagManager.Create("Wires");

	// Token: 0x04002BF3 RID: 11251
	public static readonly Tag Vents = TagManager.Create("Vents");

	// Token: 0x04002BF4 RID: 11252
	public static readonly Tag Pipes = TagManager.Create("Pipes");

	// Token: 0x04002BF5 RID: 11253
	public static readonly Tag WireBridges = TagManager.Create("WireBridges");

	// Token: 0x04002BF6 RID: 11254
	public static readonly Tag TravelTubeBridges = TagManager.Create("TravelTubeBridges");

	// Token: 0x04002BF7 RID: 11255
	public static readonly Tag Backwall = TagManager.Create("Backwall");

	// Token: 0x04002BF8 RID: 11256
	public static readonly Tag MISSING_TAG = TagManager.Create("MISSING_TAG");

	// Token: 0x04002BF9 RID: 11257
	public static readonly Tag PlantRenderer = TagManager.Create("PlantRenderer");

	// Token: 0x04002BFA RID: 11258
	public static readonly Tag Usable = TagManager.Create("Usable");

	// Token: 0x04002BFB RID: 11259
	public static readonly Tag PedestalDisplayable = TagManager.Create("PedestalDisplayable");

	// Token: 0x04002BFC RID: 11260
	public static readonly Tag HasChores = TagManager.Create("HasChores");

	// Token: 0x04002BFD RID: 11261
	public static readonly Tag Suit = TagManager.Create("Suit");

	// Token: 0x04002BFE RID: 11262
	public static readonly Tag AirtightSuit = TagManager.Create("AirtightSuit");

	// Token: 0x04002BFF RID: 11263
	public static readonly Tag AtmoSuit = TagManager.Create("Atmo_Suit");

	// Token: 0x04002C00 RID: 11264
	public static readonly Tag OxygenMask = TagManager.Create("Oxygen_Mask");

	// Token: 0x04002C01 RID: 11265
	public static readonly Tag LeadSuit = TagManager.Create("Lead_Suit");

	// Token: 0x04002C02 RID: 11266
	public static readonly Tag JetSuit = TagManager.Create("Jet_Suit");

	// Token: 0x04002C03 RID: 11267
	public static readonly Tag JetSuitOutOfFuel = TagManager.Create("JetSuitOutOfFuel");

	// Token: 0x04002C04 RID: 11268
	public static readonly Tag SuitBatteryLow = TagManager.Create("SuitBatteryLow");

	// Token: 0x04002C05 RID: 11269
	public static readonly Tag SuitBatteryOut = TagManager.Create("SuitBatteryOut");

	// Token: 0x04002C06 RID: 11270
	public static readonly List<Tag> AllSuitTags = new List<Tag>
	{
		GameTags.Suit,
		GameTags.AtmoSuit,
		GameTags.JetSuit,
		GameTags.LeadSuit
	};

	// Token: 0x04002C07 RID: 11271
	public static readonly List<Tag> OxygenSuitTags = new List<Tag>
	{
		GameTags.AtmoSuit,
		GameTags.JetSuit,
		GameTags.LeadSuit
	};

	// Token: 0x04002C08 RID: 11272
	public static readonly Tag EquippableBalloon = TagManager.Create("EquippableBalloon");

	// Token: 0x04002C09 RID: 11273
	public static readonly Tag Clothes = TagManager.Create("Clothes");

	// Token: 0x04002C0A RID: 11274
	public static readonly Tag WarmVest = TagManager.Create("Warm_Vest");

	// Token: 0x04002C0B RID: 11275
	public static readonly Tag FunkyVest = TagManager.Create("Funky_Vest");

	// Token: 0x04002C0C RID: 11276
	public static readonly List<Tag> AllClothesTags = new List<Tag>
	{
		GameTags.Clothes,
		GameTags.WarmVest,
		GameTags.FunkyVest
	};

	// Token: 0x04002C0D RID: 11277
	public static readonly Tag Assigned = TagManager.Create("Assigned");

	// Token: 0x04002C0E RID: 11278
	public static readonly Tag Helmet = TagManager.Create("Helmet");

	// Token: 0x04002C0F RID: 11279
	public static readonly Tag Equipped = TagManager.Create("Equipped");

	// Token: 0x04002C10 RID: 11280
	public static readonly Tag DisposablePortableBattery = TagManager.Create("DisposablePortableBattery");

	// Token: 0x04002C11 RID: 11281
	public static readonly Tag ChargedPortableBattery = TagManager.Create("ChargedPortableBattery");

	// Token: 0x04002C12 RID: 11282
	public static readonly Tag EmptyPortableBattery = TagManager.Create("EmptyPortableBattery");

	// Token: 0x04002C13 RID: 11283
	public static readonly Tag SolidLubricant = TagManager.Create("SolidLubricant");

	// Token: 0x04002C14 RID: 11284
	public static readonly Tag Entombed = TagManager.Create("Entombed");

	// Token: 0x04002C15 RID: 11285
	public static readonly Tag Uprooted = TagManager.Create("Uprooted");

	// Token: 0x04002C16 RID: 11286
	public static readonly Tag Preserved = TagManager.Create("Preserved");

	// Token: 0x04002C17 RID: 11287
	public static readonly Tag Compostable = TagManager.Create("Compostable");

	// Token: 0x04002C18 RID: 11288
	public static readonly Tag Pickled = TagManager.Create("Pickled");

	// Token: 0x04002C19 RID: 11289
	public static readonly Tag UnspicedFood = TagManager.Create("UnspicedFood");

	// Token: 0x04002C1A RID: 11290
	public static readonly Tag SpicedFood = TagManager.Create("SpicedFood");

	// Token: 0x04002C1B RID: 11291
	public static readonly Tag Dying = TagManager.Create("Dying");

	// Token: 0x04002C1C RID: 11292
	public static readonly Tag Dead = TagManager.Create("Dead");

	// Token: 0x04002C1D RID: 11293
	public static readonly Tag PreventDeadAnimation = TagManager.Create("PreventDeadAnimation");

	// Token: 0x04002C1E RID: 11294
	public static readonly Tag Reachable = TagManager.Create("Reachable");

	// Token: 0x04002C1F RID: 11295
	public static readonly Tag PreventChoreInterruption = TagManager.Create("PreventChoreInterruption");

	// Token: 0x04002C20 RID: 11296
	public static readonly Tag PerformingWorkRequest = TagManager.Create("PerformingWorkRequest");

	// Token: 0x04002C21 RID: 11297
	public static readonly Tag RecoveringBreath = TagManager.Create("RecoveringBreath");

	// Token: 0x04002C22 RID: 11298
	public static readonly Tag FeelingCold = TagManager.Create("FeelingCold");

	// Token: 0x04002C23 RID: 11299
	public static readonly Tag FeelingWarm = TagManager.Create("FeelingWarm");

	// Token: 0x04002C24 RID: 11300
	public static readonly Tag RecoveringWarmnth = TagManager.Create("RecoveringWarmnth");

	// Token: 0x04002C25 RID: 11301
	public static readonly Tag RecoveringFromHeat = TagManager.Create("RecoveringFromHeat");

	// Token: 0x04002C26 RID: 11302
	public static readonly Tag NoOxygen = TagManager.Create("NoOxygen");

	// Token: 0x04002C27 RID: 11303
	public static readonly Tag Idle = TagManager.Create("Idle");

	// Token: 0x04002C28 RID: 11304
	public static readonly Tag StationaryIdling = TagManager.Create("StationaryIdling");

	// Token: 0x04002C29 RID: 11305
	public static readonly Tag AlwaysConverse = TagManager.Create("AlwaysConverse");

	// Token: 0x04002C2A RID: 11306
	public static readonly Tag HasDebugDestination = TagManager.Create("HasDebugDestination");

	// Token: 0x04002C2B RID: 11307
	public static readonly Tag Shaded = TagManager.Create("Shaded");

	// Token: 0x04002C2C RID: 11308
	public static readonly Tag TakingMedicine = TagManager.Create("TakingMedicine");

	// Token: 0x04002C2D RID: 11309
	public static readonly Tag Partying = TagManager.Create("Partying");

	// Token: 0x04002C2E RID: 11310
	public static readonly Tag MakingMess = TagManager.Create("MakingMess");

	// Token: 0x04002C2F RID: 11311
	public static readonly Tag DupeBrain = TagManager.Create("DupeBrain");

	// Token: 0x04002C30 RID: 11312
	public static readonly Tag CreatureBrain = TagManager.Create("CreatureBrain");

	// Token: 0x04002C31 RID: 11313
	public static readonly Tag Asleep = TagManager.Create("Asleep");

	// Token: 0x04002C32 RID: 11314
	public static readonly Tag HoldingBreath = TagManager.Create("HoldingBreath");

	// Token: 0x04002C33 RID: 11315
	public static readonly Tag Overjoyed = TagManager.Create("Overjoyed");

	// Token: 0x04002C34 RID: 11316
	public static readonly Tag PleasantConversation = TagManager.Create("PleasantConversation");

	// Token: 0x04002C35 RID: 11317
	public static readonly Tag HasSuitTank = TagManager.Create("HasSuitTank");

	// Token: 0x04002C36 RID: 11318
	public static readonly Tag HasAirtightSuit = TagManager.Create("HasAirtightSuit");

	// Token: 0x04002C37 RID: 11319
	public static readonly Tag NoCreatureIdling = TagManager.Create("NoCreatureIdling");

	// Token: 0x04002C38 RID: 11320
	public static readonly Tag UnderConstruction = TagManager.Create("UnderConstruction");

	// Token: 0x04002C39 RID: 11321
	public static readonly Tag Operational = TagManager.Create("Operational");

	// Token: 0x04002C3A RID: 11322
	public static readonly Tag JetSuitBlocker = TagManager.Create("JetSuitBlocker");

	// Token: 0x04002C3B RID: 11323
	public static readonly Tag HasInvalidPorts = TagManager.Create("HasInvalidPorts");

	// Token: 0x04002C3C RID: 11324
	public static readonly Tag NotRoomAssignable = TagManager.Create("NotRoomAssignable");

	// Token: 0x04002C3D RID: 11325
	public static readonly Tag OneTimeUseLure = TagManager.Create("OneTimeUseLure");

	// Token: 0x04002C3E RID: 11326
	public static readonly Tag LureUsed = TagManager.Create("LureUsed");

	// Token: 0x04002C3F RID: 11327
	public static readonly Tag TemplateBuilding = TagManager.Create("TemplateBuilding");

	// Token: 0x04002C40 RID: 11328
	public static readonly Tag ModularConduitPort = TagManager.Create("ModularConduitPort");

	// Token: 0x04002C41 RID: 11329
	public static readonly Tag WarpTech = TagManager.Create("WarpTech");

	// Token: 0x04002C42 RID: 11330
	public static readonly Tag HEPPassThrough = TagManager.Create("HEPPassThrough");

	// Token: 0x04002C43 RID: 11331
	public static readonly Tag TelephoneRinging = TagManager.Create("TelephoneRinging");

	// Token: 0x04002C44 RID: 11332
	public static readonly Tag LongDistanceCall = TagManager.Create("LongDistanceCall");

	// Token: 0x04002C45 RID: 11333
	public static readonly Tag Telepad = TagManager.Create("Telepad");

	// Token: 0x04002C46 RID: 11334
	public static readonly Tag InTransitTube = TagManager.Create("InTransitTube");

	// Token: 0x04002C47 RID: 11335
	public static readonly Tag TrapArmed = TagManager.Create("TrapArmed");

	// Token: 0x04002C48 RID: 11336
	public static readonly Tag GeyserFeature = TagManager.Create("GeyserFeature");

	// Token: 0x04002C49 RID: 11337
	public static readonly Tag Rocket = TagManager.Create("Rocket");

	// Token: 0x04002C4A RID: 11338
	public static readonly Tag RocketOnGround = TagManager.Create("RocketOnGround");

	// Token: 0x04002C4B RID: 11339
	public static readonly Tag RocketNotOnGround = TagManager.Create("RocketNotOnGround");

	// Token: 0x04002C4C RID: 11340
	public static readonly Tag RocketInSpace = TagManager.Create("RocketInSpace");

	// Token: 0x04002C4D RID: 11341
	public static readonly Tag RocketStranded = TagManager.Create("RocketStranded");

	// Token: 0x04002C4E RID: 11342
	public static readonly Tag RailGunPayloadEmptyable = TagManager.Create("RailGunPayloadEmptyable");

	// Token: 0x04002C4F RID: 11343
	public static readonly Tag TransferringCargoComplete = TagManager.Create("TransferringCargoComplete");

	// Token: 0x04002C50 RID: 11344
	public static readonly Tag NoseRocketModule = TagManager.Create("NoseRocketModule");

	// Token: 0x04002C51 RID: 11345
	public static readonly Tag LaunchButtonRocketModule = TagManager.Create("LaunchButtonRocketModule");

	// Token: 0x04002C52 RID: 11346
	public static readonly Tag RocketInteriorBuilding = TagManager.Create("RocketInteriorBuilding");

	// Token: 0x04002C53 RID: 11347
	public static readonly Tag NotRocketInteriorBuilding = TagManager.Create("NotRocketInteriorBuilding");

	// Token: 0x04002C54 RID: 11348
	public static readonly Tag UniquePerWorld = TagManager.Create("UniquePerWorld");

	// Token: 0x04002C55 RID: 11349
	public static readonly Tag RocketEnvelopeTile = TagManager.Create("RocketEnvelopeTile");

	// Token: 0x04002C56 RID: 11350
	public static readonly Tag NoRocketRefund = TagManager.Create("NoRocketRefund");

	// Token: 0x04002C57 RID: 11351
	public static readonly Tag RocketModule = TagManager.Create("RocketModule");

	// Token: 0x04002C58 RID: 11352
	public static readonly Tag GantryExtended = TagManager.Create("GantryExtended");

	// Token: 0x04002C59 RID: 11353
	public static readonly Tag POIHarvesting = TagManager.Create("POIHarvesting");

	// Token: 0x04002C5A RID: 11354
	public static readonly Tag BallisticEntityLanding = TagManager.Create("BallisticEntityLanding");

	// Token: 0x04002C5B RID: 11355
	public static readonly Tag BallisticEntityLaunching = TagManager.Create("BallisticEntityLaunching");

	// Token: 0x04002C5C RID: 11356
	public static readonly Tag BallisticEntityMoving = TagManager.Create("BallisticEntityMoving");

	// Token: 0x04002C5D RID: 11357
	public static readonly Tag ClusterEntityGrounded = TagManager.Create("ClusterEntityGrounded ");

	// Token: 0x04002C5E RID: 11358
	public static readonly Tag LongRangeMissileMoving = TagManager.Create("LongRangeMissileMoving");

	// Token: 0x04002C5F RID: 11359
	public static readonly Tag LongRangeMissileIdle = TagManager.Create("LongRangeMissileIdle");

	// Token: 0x04002C60 RID: 11360
	public static readonly Tag LongRangeMissileExploding = TagManager.Create("LongRangeMissileExploding");

	// Token: 0x04002C61 RID: 11361
	public static readonly Tag EntityInSpace = TagManager.Create("EntityInSpace");

	// Token: 0x04002C62 RID: 11362
	public static readonly Tag Monument = TagManager.Create("Monument");

	// Token: 0x04002C63 RID: 11363
	public static readonly Tag Stored = TagManager.Create("Stored");

	// Token: 0x04002C64 RID: 11364
	public static readonly Tag StoredPrivate = TagManager.Create("StoredPrivate");

	// Token: 0x04002C65 RID: 11365
	public static readonly Tag Sealed = TagManager.Create("Sealed");

	// Token: 0x04002C66 RID: 11366
	public static readonly Tag CorrosionProof = TagManager.Create("CorrosionProof");

	// Token: 0x04002C67 RID: 11367
	public static readonly Tag PickupableStorage = TagManager.Create("PickupableStorage");

	// Token: 0x04002C68 RID: 11368
	public static readonly Tag UnidentifiedSeed = TagManager.Create("UnidentifiedSeed");

	// Token: 0x04002C69 RID: 11369
	public static readonly Tag CropSeed = TagManager.Create("CropSeed");

	// Token: 0x04002C6A RID: 11370
	public static readonly Tag DecorSeed = TagManager.Create("DecorSeed");

	// Token: 0x04002C6B RID: 11371
	public static readonly Tag WaterSeed = TagManager.Create("WaterSeed");

	// Token: 0x04002C6C RID: 11372
	public static readonly Tag Harvestable = TagManager.Create("Harvestable");

	// Token: 0x04002C6D RID: 11373
	public static readonly Tag Hanging = TagManager.Create("Hanging");

	// Token: 0x04002C6E RID: 11374
	public static readonly Tag FarmingMaterial = TagManager.Create("FarmingMaterial");

	// Token: 0x04002C6F RID: 11375
	public static readonly Tag MutatedSeed = TagManager.Create("MutatedSeed");

	// Token: 0x04002C70 RID: 11376
	public static readonly Tag OverlayInFrontOfConduits = TagManager.Create("OverlayFrontLayer");

	// Token: 0x04002C71 RID: 11377
	public static readonly Tag OverlayBehindConduits = TagManager.Create("OverlayBackLayer");

	// Token: 0x04002C72 RID: 11378
	public static readonly Tag MassChunk = TagManager.Create("MassChunk");

	// Token: 0x04002C73 RID: 11379
	public static readonly Tag UnitChunk = TagManager.Create("UnitChunk");

	// Token: 0x04002C74 RID: 11380
	public static readonly Tag NotConversationTopic = TagManager.Create("NotConversationTopic");

	// Token: 0x04002C75 RID: 11381
	public static readonly Tag MinionSelectPreview = TagManager.Create("MinionSelectPreview");

	// Token: 0x04002C76 RID: 11382
	public static readonly Tag Empty = TagManager.Create("Empty");

	// Token: 0x04002C77 RID: 11383
	public static readonly Tag ExcludeFromTemplate = TagManager.Create("ExcludeFromTemplate");

	// Token: 0x04002C78 RID: 11384
	public static readonly Tag SpaceDanger = TagManager.Create("SpaceDanger");

	// Token: 0x04002C79 RID: 11385
	public static TagSet SolidElements = new TagSet();

	// Token: 0x04002C7A RID: 11386
	public static TagSet LiquidElements = new TagSet();

	// Token: 0x04002C7B RID: 11387
	public static TagSet GasElements = new TagSet();

	// Token: 0x04002C7C RID: 11388
	public static TagSet CalorieCategories = new TagSet { GameTags.Edible };

	// Token: 0x04002C7D RID: 11389
	public static TagSet UnitCategories = new TagSet
	{
		GameTags.Medicine,
		GameTags.MedicalSupplies,
		GameTags.Seed,
		GameTags.Egg,
		GameTags.Clothes,
		GameTags.IndustrialIngredient,
		GameTags.IndustrialProduct,
		GameTags.Compostable,
		GameTags.HighEnergyParticle,
		GameTags.StoryTraitResource,
		GameTags.Dehydrated,
		GameTags.ChargedPortableBattery,
		GameTags.BionicUpgrade
	};

	// Token: 0x04002C7E RID: 11390
	public static TagSet IgnoredMaterialCategories = new TagSet
	{
		GameTags.Special,
		GameTags.IgnoreMaterialCategory
	};

	// Token: 0x04002C7F RID: 11391
	public static TagSet MaterialCategories = new TagSet
	{
		GameTags.Alloy,
		GameTags.Metal,
		GameTags.RefinedMetal,
		GameTags.BuildableRaw,
		GameTags.BuildableProcessed,
		GameTags.Filter,
		GameTags.Liquifiable,
		GameTags.Liquid,
		GameTags.Breathable,
		GameTags.Unbreathable,
		GameTags.ConsumableOre,
		GameTags.Sublimating,
		GameTags.Organics,
		GameTags.Farmable,
		GameTags.Agriculture,
		GameTags.Other,
		GameTags.ManufacturedMaterial,
		GameTags.CookingIngredient,
		GameTags.RareMaterials
	};

	// Token: 0x04002C80 RID: 11392
	public static TagSet BionicCompatibleBatteries = new TagSet
	{
		"Electrobank",
		GameTags.DisposablePortableBattery,
		GameTags.EmptyPortableBattery
	};

	// Token: 0x04002C81 RID: 11393
	public static TagSet BionicIncompatibleBatteries = new TagSet { "SelfChargingElectrobank" };

	// Token: 0x04002C82 RID: 11394
	public static TagSet MaterialBuildingElements = new TagSet
	{
		GameTags.BuildingFiber,
		GameTags.BuildingWood
	};

	// Token: 0x04002C83 RID: 11395
	public static TagSet OtherEntityTags = new TagSet
	{
		GameTags.BagableCreature,
		GameTags.SwimmingCreature,
		GameTags.MiscPickupable
	};

	// Token: 0x04002C84 RID: 11396
	public static TagSet AllCategories = new TagSet(new TagSet[]
	{
		GameTags.CalorieCategories,
		GameTags.UnitCategories,
		GameTags.MaterialCategories,
		GameTags.MaterialBuildingElements,
		GameTags.OtherEntityTags
	});

	// Token: 0x04002C85 RID: 11397
	public static TagSet DisplayAsCalories = new TagSet(GameTags.CalorieCategories);

	// Token: 0x04002C86 RID: 11398
	public static TagSet DisplayAsUnits = new TagSet(GameTags.UnitCategories);

	// Token: 0x04002C87 RID: 11399
	public static TagSet DisplayAsInformation = new TagSet();

	// Token: 0x04002C88 RID: 11400
	public static Tag StartingMetalOre = new Tag("StartingMetalOre");

	// Token: 0x04002C89 RID: 11401
	public static Tag StartingRefinedMetal = new Tag("StartingRefinedMetal");

	// Token: 0x04002C8A RID: 11402
	public static Tag[] StartingMetalOres;

	// Token: 0x04002C8B RID: 11403
	public static Tag[] StartingRefinedMetals = null;

	// Token: 0x04002C8C RID: 11404
	public static Tag[] BasicMetalOres = new Tag[] { SimHashes.IronOre.CreateTag() };

	// Token: 0x04002C8D RID: 11405
	public static Tag[] BasicRefinedMetals = new Tag[] { SimHashes.Iron.CreateTag() };

	// Token: 0x04002C8E RID: 11406
	public static TagSet HiddenElementTags = new TagSet
	{
		GameTags.HideFromCodex,
		GameTags.HideFromSpawnTool,
		GameTags.StartingMetalOre,
		GameTags.StartingRefinedMetal
	};

	// Token: 0x04002C8F RID: 11407
	public static Tag[] Fabrics = new Tag[]
	{
		"BasicFabric".ToTag(),
		FeatherFabricConfig.ID
	};

	// Token: 0x020018EB RID: 6379
	public static class Worlds
	{
		// Token: 0x04007A37 RID: 31287
		public static readonly Tag Ceres = TagManager.Create("Ceres");
	}

	// Token: 0x020018EC RID: 6380
	public abstract class ChoreTypes
	{
		// Token: 0x04007A38 RID: 31288
		public static readonly Tag Farming = TagManager.Create("Farming");

		// Token: 0x04007A39 RID: 31289
		public static readonly Tag Ranching = TagManager.Create("Ranching");

		// Token: 0x04007A3A RID: 31290
		public static readonly Tag Research = TagManager.Create("Research");

		// Token: 0x04007A3B RID: 31291
		public static readonly Tag Power = TagManager.Create("Power");

		// Token: 0x04007A3C RID: 31292
		public static readonly Tag Building = TagManager.Create("Building");

		// Token: 0x04007A3D RID: 31293
		public static readonly Tag Cooking = TagManager.Create("Cooking");

		// Token: 0x04007A3E RID: 31294
		public static readonly Tag Fabricating = TagManager.Create("Fabricating");

		// Token: 0x04007A3F RID: 31295
		public static readonly Tag Wiring = TagManager.Create("Wiring");

		// Token: 0x04007A40 RID: 31296
		public static readonly Tag Art = TagManager.Create("Art");

		// Token: 0x04007A41 RID: 31297
		public static readonly Tag Digging = TagManager.Create("Digging");

		// Token: 0x04007A42 RID: 31298
		public static readonly Tag Doctoring = TagManager.Create("Doctoring");

		// Token: 0x04007A43 RID: 31299
		public static readonly Tag Conveyor = TagManager.Create("Conveyor");
	}

	// Token: 0x020018ED RID: 6381
	public static class Creatures
	{
		// Token: 0x04007A44 RID: 31300
		public static readonly Tag ReservedByCreature = TagManager.Create("ReservedByCreature");

		// Token: 0x04007A45 RID: 31301
		public static readonly Tag PreventGrowAnimation = TagManager.Create("PreventGrowAnimation");

		// Token: 0x04007A46 RID: 31302
		public static readonly Tag TrappedInCargoBay = TagManager.Create("TrappedInCargoBay");

		// Token: 0x04007A47 RID: 31303
		public static readonly Tag PausedHunger = TagManager.Create("PausedHunger");

		// Token: 0x04007A48 RID: 31304
		public static readonly Tag PausedReproduction = TagManager.Create("PausedReproduction");

		// Token: 0x04007A49 RID: 31305
		public static readonly Tag Bagged = TagManager.Create("Bagged");

		// Token: 0x04007A4A RID: 31306
		public static readonly Tag InIncubator = TagManager.Create("InIncubator");

		// Token: 0x04007A4B RID: 31307
		public static readonly Tag Deliverable = TagManager.Create("Deliverable");

		// Token: 0x04007A4C RID: 31308
		public static readonly Tag StunnedForCapture = TagManager.Create("StunnedForCapture");

		// Token: 0x04007A4D RID: 31309
		public static readonly Tag StunnedBeingEaten = TagManager.Create("StunnedBeingEaten");

		// Token: 0x04007A4E RID: 31310
		public static readonly Tag Falling = TagManager.Create("Falling");

		// Token: 0x04007A4F RID: 31311
		public static readonly Tag Flopping = TagManager.Create("Flopping");

		// Token: 0x04007A50 RID: 31312
		public static readonly Tag WantsToEnterBurrow = TagManager.Create("WantsToBurrow");

		// Token: 0x04007A51 RID: 31313
		public static readonly Tag Burrowed = TagManager.Create("Burrowed");

		// Token: 0x04007A52 RID: 31314
		public static readonly Tag WantsToExitBurrow = TagManager.Create("WantsToExitBurrow");

		// Token: 0x04007A53 RID: 31315
		public static readonly Tag WantsToEat = TagManager.Create("WantsToEat");

		// Token: 0x04007A54 RID: 31316
		public static readonly Tag SuppressedDiet = TagManager.Create("SuppressedDiet");

		// Token: 0x04007A55 RID: 31317
		public static readonly Tag UrgeToPoke = TagManager.Create("UrgeToPoke");

		// Token: 0x04007A56 RID: 31318
		public static readonly Tag WantsToStomp = TagManager.Create("WantsToStomp");

		// Token: 0x04007A57 RID: 31319
		public static readonly Tag WantsToHarvest = TagManager.Create("WantsToHarvest");

		// Token: 0x04007A58 RID: 31320
		public static readonly Tag Behaviour_TryToDrinkMilkFromFeeder = TagManager.Create("Behaviour_TryToDrinkMilkFromFeeder");

		// Token: 0x04007A59 RID: 31321
		public static readonly Tag Behaviour_InteractWithCritterCondo = TagManager.Create("Behaviour_InteractWithCritterCondo");

		// Token: 0x04007A5A RID: 31322
		public static readonly Tag WantsToGetRanched = TagManager.Create("WantsToGetRanched");

		// Token: 0x04007A5B RID: 31323
		public static readonly Tag WantsToGetCaptured = TagManager.Create("WantsToGetCaptured");

		// Token: 0x04007A5C RID: 31324
		public static readonly Tag WantsToClimbTree = TagManager.Create("WantsToClimbTree");

		// Token: 0x04007A5D RID: 31325
		public static readonly Tag WantsToPlantSeed = TagManager.Create("WantsToPlantSeed");

		// Token: 0x04007A5E RID: 31326
		public static readonly Tag WantsToForage = TagManager.Create("WantsToForage");

		// Token: 0x04007A5F RID: 31327
		public static readonly Tag WantsToLayEgg = TagManager.Create("WantsToLayEgg");

		// Token: 0x04007A60 RID: 31328
		public static readonly Tag WantsToTendEgg = TagManager.Create("WantsToTendEgg");

		// Token: 0x04007A61 RID: 31329
		public static readonly Tag WantsAHug = TagManager.Create("WantsAHug");

		// Token: 0x04007A62 RID: 31330
		public static readonly Tag WantsConduitConnection = TagManager.Create("WantsConduitConnection");

		// Token: 0x04007A63 RID: 31331
		public static readonly Tag WantsToGoHome = TagManager.Create("WantsToGoHome");

		// Token: 0x04007A64 RID: 31332
		public static readonly Tag WantsToMakeHome = TagManager.Create("WantsToMakeHome");

		// Token: 0x04007A65 RID: 31333
		public static readonly Tag BeeWantsToSleep = TagManager.Create("BeeWantsToSleep");

		// Token: 0x04007A66 RID: 31334
		public static readonly Tag WantsToTendCrops = TagManager.Create("WantsToTendPlants");

		// Token: 0x04007A67 RID: 31335
		public static readonly Tag WantsToStore = TagManager.Create("WantsToStore");

		// Token: 0x04007A68 RID: 31336
		public static readonly Tag WantsToBeckon = TagManager.Create("WantsToBeckon");

		// Token: 0x04007A69 RID: 31337
		public static readonly Tag Flee = TagManager.Create("Flee");

		// Token: 0x04007A6A RID: 31338
		public static readonly Tag Attack = TagManager.Create("Attack");

		// Token: 0x04007A6B RID: 31339
		public static readonly Tag Defend = TagManager.Create("Defend");

		// Token: 0x04007A6C RID: 31340
		public static readonly Tag ReturnToEgg = TagManager.Create("ReturnToEgg");

		// Token: 0x04007A6D RID: 31341
		public static readonly Tag CrabFriend = TagManager.Create("CrabFriend");

		// Token: 0x04007A6E RID: 31342
		public static readonly Tag Die = TagManager.Create("Die");

		// Token: 0x04007A6F RID: 31343
		public static readonly Tag Poop = TagManager.Create("Poop");

		// Token: 0x04007A70 RID: 31344
		public static readonly Tag MoveToLure = TagManager.Create("MoveToLure");

		// Token: 0x04007A71 RID: 31345
		public static readonly Tag Drowning = TagManager.Create("Drowning");

		// Token: 0x04007A72 RID: 31346
		public static readonly Tag Hungry = TagManager.Create("Hungry");

		// Token: 0x04007A73 RID: 31347
		public static readonly Tag Flyer = TagManager.Create("Flyer");

		// Token: 0x04007A74 RID: 31348
		public static readonly Tag FishTrapLure = TagManager.Create("FishTrapLure");

		// Token: 0x04007A75 RID: 31349
		public static readonly Tag FlyersLure = TagManager.Create("MasterLure");

		// Token: 0x04007A76 RID: 31350
		public static readonly Tag Walker = TagManager.Create("Walker");

		// Token: 0x04007A77 RID: 31351
		public static readonly Tag Hoverer = TagManager.Create("Hoverer");

		// Token: 0x04007A78 RID: 31352
		public static readonly Tag Swimmer = TagManager.Create("Swimmer");

		// Token: 0x04007A79 RID: 31353
		public static readonly Tag Fertile = TagManager.Create("Fertile");

		// Token: 0x04007A7A RID: 31354
		public static readonly Tag Submerged = TagManager.Create("Submerged");

		// Token: 0x04007A7B RID: 31355
		public static readonly Tag ExitSubmerged = TagManager.Create("ExitSubmerged");

		// Token: 0x04007A7C RID: 31356
		public static readonly Tag WantsToDropElements = TagManager.Create("WantsToDropElements");

		// Token: 0x04007A7D RID: 31357
		public static readonly Tag OriginallyWild = TagManager.Create("Wild");

		// Token: 0x04007A7E RID: 31358
		public static readonly Tag Wild = TagManager.Create("Wild");

		// Token: 0x04007A7F RID: 31359
		public static readonly Tag Overcrowded = TagManager.Create("Overcrowded");

		// Token: 0x04007A80 RID: 31360
		public static readonly Tag Expecting = TagManager.Create("Expecting");

		// Token: 0x04007A81 RID: 31361
		public static readonly Tag Confined = TagManager.Create("Confined");

		// Token: 0x04007A82 RID: 31362
		public static readonly Tag Digger = TagManager.Create("Digger");

		// Token: 0x04007A83 RID: 31363
		public static readonly Tag Tunnel = TagManager.Create("Tunnel");

		// Token: 0x04007A84 RID: 31364
		public static readonly Tag Builder = TagManager.Create("Builder");

		// Token: 0x04007A85 RID: 31365
		public static readonly Tag ScalesGrown = TagManager.Create("ScalesGrown");

		// Token: 0x04007A86 RID: 31366
		public static readonly Tag CanMolt = TagManager.Create("CanMolt");

		// Token: 0x04007A87 RID: 31367
		public static readonly Tag ReadyToMolt = TagManager.Create("ReadyToMolt");

		// Token: 0x04007A88 RID: 31368
		public static readonly Tag CantReachEgg = TagManager.Create("CantReachEgg");

		// Token: 0x04007A89 RID: 31369
		public static readonly Tag HasNoFoundation = TagManager.Create("HasNoFoundation");

		// Token: 0x04007A8A RID: 31370
		public static readonly Tag Cleaning = TagManager.Create("Cleaning");

		// Token: 0x04007A8B RID: 31371
		public static readonly Tag Happy = TagManager.Create("Happy");

		// Token: 0x04007A8C RID: 31372
		public static readonly Tag Unhappy = TagManager.Create("Unhappy");

		// Token: 0x04007A8D RID: 31373
		public static readonly Tag RequiresMilking = TagManager.Create("RequiresMilking");

		// Token: 0x04007A8E RID: 31374
		public static readonly Tag TargetedPreyBehaviour = TagManager.Create("TargetedPrey");

		// Token: 0x04007A8F RID: 31375
		public static readonly Tag WantsToPollinate = TagManager.Create("WantsToPollinate");

		// Token: 0x04007A90 RID: 31376
		public static readonly Tag Pollinator = TagManager.Create("Pollinator");

		// Token: 0x0200283A RID: 10298
		public static class Species
		{
			// Token: 0x0600CB44 RID: 52036 RVA: 0x00419B9A File Offset: 0x00417D9A
			public static Tag[] AllSpecies_REFLECTION()
			{
				return GameTags.Reflection_GetTagsInClass(typeof(GameTags.Creatures.Species), BindingFlags.Static | BindingFlags.Public);
			}

			// Token: 0x0400B253 RID: 45651
			public static readonly Tag HatchSpecies = TagManager.Create("HatchSpecies", CREATURES.FAMILY_PLURAL.HATCHSPECIES);

			// Token: 0x0400B254 RID: 45652
			public static readonly Tag LightBugSpecies = TagManager.Create("LightBugSpecies", CREATURES.FAMILY_PLURAL.LIGHTBUGSPECIES);

			// Token: 0x0400B255 RID: 45653
			public static readonly Tag OilFloaterSpecies = TagManager.Create("OilFloaterSpecies", CREATURES.FAMILY_PLURAL.OILFLOATERSPECIES);

			// Token: 0x0400B256 RID: 45654
			public static readonly Tag DreckoSpecies = TagManager.Create("DreckoSpecies", CREATURES.FAMILY_PLURAL.DRECKOSPECIES);

			// Token: 0x0400B257 RID: 45655
			public static readonly Tag GlomSpecies = TagManager.Create("GlomSpecies", CREATURES.FAMILY_PLURAL.GLOMSPECIES);

			// Token: 0x0400B258 RID: 45656
			public static readonly Tag PuftSpecies = TagManager.Create("PuftSpecies", CREATURES.FAMILY_PLURAL.PUFTSPECIES);

			// Token: 0x0400B259 RID: 45657
			public static readonly Tag MosquitoSpecies = TagManager.Create("MosquitoSpecies", CREATURES.FAMILY_PLURAL.MOSQUITOSPECIES);

			// Token: 0x0400B25A RID: 45658
			public static readonly Tag PacuSpecies = TagManager.Create("PacuSpecies", CREATURES.FAMILY_PLURAL.PACUSPECIES);

			// Token: 0x0400B25B RID: 45659
			public static readonly Tag MooSpecies = TagManager.Create("MooSpecies", CREATURES.FAMILY_PLURAL.MOOSPECIES);

			// Token: 0x0400B25C RID: 45660
			public static readonly Tag MoleSpecies = TagManager.Create("MoleSpecies", CREATURES.FAMILY_PLURAL.MOLESPECIES);

			// Token: 0x0400B25D RID: 45661
			public static readonly Tag SquirrelSpecies = TagManager.Create("SquirrelSpecies", CREATURES.FAMILY_PLURAL.SQUIRRELSPECIES);

			// Token: 0x0400B25E RID: 45662
			public static readonly Tag CrabSpecies = TagManager.Create("CrabSpecies", CREATURES.FAMILY_PLURAL.CRABSPECIES);

			// Token: 0x0400B25F RID: 45663
			public static readonly Tag StaterpillarSpecies = TagManager.Create("StaterpillarSpecies", CREATURES.FAMILY_PLURAL.STATERPILLARSPECIES);

			// Token: 0x0400B260 RID: 45664
			public static readonly Tag BeetaSpecies = TagManager.Create("BeetaSpecies", CREATURES.FAMILY_PLURAL.BEETASPECIES);

			// Token: 0x0400B261 RID: 45665
			public static readonly Tag DivergentSpecies = TagManager.Create("DivergentSpecies", CREATURES.FAMILY_PLURAL.DIVERGENTSPECIES);

			// Token: 0x0400B262 RID: 45666
			public static readonly Tag DeerSpecies = TagManager.Create("DeerSpecies", CREATURES.FAMILY_PLURAL.DEERSPECIES);

			// Token: 0x0400B263 RID: 45667
			public static readonly Tag BellySpecies = TagManager.Create("BellySpecies", CREATURES.FAMILY_PLURAL.BELLYSPECIES);

			// Token: 0x0400B264 RID: 45668
			public static readonly Tag SealSpecies = TagManager.Create("SealSpecies", CREATURES.FAMILY_PLURAL.SEALSPECIES);

			// Token: 0x0400B265 RID: 45669
			public static readonly Tag RaptorSpecies = TagManager.Create("RaptorSpecies", CREATURES.FAMILY_PLURAL.RAPTORSPECIES);

			// Token: 0x0400B266 RID: 45670
			public static readonly Tag ChameleonSpecies = TagManager.Create("ChameleonSpecies", CREATURES.FAMILY_PLURAL.CHAMELEONSPECIES);

			// Token: 0x0400B267 RID: 45671
			public static readonly Tag PrehistoricPacuSpecies = TagManager.Create("PrehistoricPacuSpecies", CREATURES.FAMILY_PLURAL.PREHISTORICPACUSPECIES);

			// Token: 0x0400B268 RID: 45672
			public static readonly Tag StegoSpecies = TagManager.Create("StegoSpecies", CREATURES.FAMILY_PLURAL.STEGOSPECIES);

			// Token: 0x0400B269 RID: 45673
			public static readonly Tag ButterflySpecies = TagManager.Create("ButterflySpecies", CREATURES.FAMILY_PLURAL.BUTTERFLYSPECIES);
		}

		// Token: 0x0200283B RID: 10299
		public static class Behaviours
		{
			// Token: 0x0400B26A RID: 45674
			public static readonly Tag HarvestHiveBehaviour = TagManager.Create("HarvestHiveBehaviour");

			// Token: 0x0400B26B RID: 45675
			public static readonly Tag GrowUpBehaviour = TagManager.Create("GrowUpBehaviour");

			// Token: 0x0400B26C RID: 45676
			public static readonly Tag SleepBehaviour = TagManager.Create("SleepBehaviour");

			// Token: 0x0400B26D RID: 45677
			public static readonly Tag CallAdultBehaviour = TagManager.Create("CallAdultBehaviour");

			// Token: 0x0400B26E RID: 45678
			public static readonly Tag SearchForEggBehaviour = TagManager.Create("SearchForEggBehaviour");

			// Token: 0x0400B26F RID: 45679
			public static readonly Tag PlayInterruptAnim = TagManager.Create("PlayInterruptAnim");

			// Token: 0x0400B270 RID: 45680
			public static readonly Tag DisableCreature = TagManager.Create("DisableCreature");
		}
	}

	// Token: 0x020018EE RID: 6382
	public static class StoragesIds
	{
		// Token: 0x04007A91 RID: 31377
		public static readonly Tag DefaultStorage = TagManager.Create("Storage");

		// Token: 0x04007A92 RID: 31378
		public static readonly Tag BionicBatteryStorage = TagManager.Create("BionicBatteryStorage");

		// Token: 0x04007A93 RID: 31379
		public static readonly Tag BionicUpgradeStorage = TagManager.Create("BionicUpgradeStorage");

		// Token: 0x04007A94 RID: 31380
		public static readonly Tag BionicOxygenTankStorage = TagManager.Create("BionicOxygenTankStorage");
	}

	// Token: 0x020018EF RID: 6383
	public static class Minions
	{
		// Token: 0x0200283C RID: 10300
		public static class Models
		{
			// Token: 0x0600CB47 RID: 52039 RVA: 0x00419E72 File Offset: 0x00418072
			public static string GetModelTooltipForTag(Tag modelTag)
			{
				if (modelTag == GameTags.Minions.Models.Bionic)
				{
					return DUPLICANTS.MODEL.BIONIC.NAME_TOOLTIP;
				}
				return "";
			}

			// Token: 0x0400B271 RID: 45681
			public static readonly Tag Standard = TagManager.Create("Minion", DUPLICANTS.MODEL.STANDARD.NAME);

			// Token: 0x0400B272 RID: 45682
			public static readonly Tag Bionic = TagManager.Create("BionicMinion", DUPLICANTS.MODEL.BIONIC.NAME);

			// Token: 0x0400B273 RID: 45683
			public static readonly Tag[] AllModels = new Tag[]
			{
				GameTags.Minions.Models.Standard,
				GameTags.Minions.Models.Bionic
			};
		}
	}

	// Token: 0x020018F0 RID: 6384
	public static class CodexCategories
	{
		// Token: 0x06009E0A RID: 40458 RVA: 0x00395390 File Offset: 0x00393590
		public static string GetCategoryLabelText(Tag tag)
		{
			StringEntry stringEntry = null;
			string text = "STRINGS.CODEX.CATEGORIES." + tag.ToString().ToUpper() + ".NAME";
			if (!Strings.TryGet(new StringKey(text), out stringEntry))
			{
				return ROOMS.CRITERIA.IN_CODE_ERROR.text.Replace("{0}", text);
			}
			return stringEntry;
		}

		// Token: 0x04007A95 RID: 31381
		public static List<Tag> AllTags = new List<Tag>();

		// Token: 0x04007A96 RID: 31382
		public static Tag CreatureRelocator = GameTags.CodexCategories.AllTags.AddAndReturn(TagManager.Create("CreatureRelocator"));

		// Token: 0x04007A97 RID: 31383
		public static Tag FarmBuilding = GameTags.CodexCategories.AllTags.AddAndReturn("FarmBuilding".ToTag());

		// Token: 0x04007A98 RID: 31384
		public static Tag BionicBuilding = GameTags.CodexCategories.AllTags.AddAndReturn("BionicBuilding".ToTag());
	}

	// Token: 0x020018F1 RID: 6385
	public static class Robots
	{
		// Token: 0x0200283D RID: 10301
		public static class Models
		{
			// Token: 0x0400B274 RID: 45684
			public static readonly Tag SweepBot = TagManager.Create("SweepBot");

			// Token: 0x0400B275 RID: 45685
			public static readonly Tag ScoutRover = TagManager.Create("ScoutRover");

			// Token: 0x0400B276 RID: 45686
			public static readonly Tag MorbRover = TagManager.Create("MorbRover");

			// Token: 0x0400B277 RID: 45687
			public static readonly Tag FetchDrone = TagManager.Create("FetchDrone");

			// Token: 0x0400B278 RID: 45688
			public static readonly Tag RemoteWorker = TagManager.Create("RemoteWorker");
		}

		// Token: 0x0200283E RID: 10302
		public static class Behaviours
		{
			// Token: 0x0400B279 RID: 45689
			public static readonly Tag UnloadBehaviour = TagManager.Create("UnloadBehaviour");

			// Token: 0x0400B27A RID: 45690
			public static readonly Tag RechargeBehaviour = TagManager.Create("RechargeBehaviour");

			// Token: 0x0400B27B RID: 45691
			public static readonly Tag EmoteBehaviour = TagManager.Create("EmoteBehaviour");

			// Token: 0x0400B27C RID: 45692
			public static readonly Tag TrappedBehaviour = TagManager.Create("TrappedBehaviour");

			// Token: 0x0400B27D RID: 45693
			public static readonly Tag NoElectroBank = TagManager.Create("NoElectroBank");
		}
	}

	// Token: 0x020018F2 RID: 6386
	public class Search
	{
		// Token: 0x04007A99 RID: 31385
		public static readonly Tag Tile = TagManager.Create("Tile");

		// Token: 0x04007A9A RID: 31386
		public static readonly Tag Ladder = TagManager.Create("Ladder");

		// Token: 0x04007A9B RID: 31387
		public static readonly Tag Powered = TagManager.Create("Powered");

		// Token: 0x04007A9C RID: 31388
		public static readonly Tag Rocket = TagManager.Create("Rocket");

		// Token: 0x04007A9D RID: 31389
		public static readonly Tag Monument = TagManager.Create("Monument");

		// Token: 0x04007A9E RID: 31390
		public static readonly Tag Farming = TagManager.Create("Farming");

		// Token: 0x04007A9F RID: 31391
		public static readonly Tag Cooking = TagManager.Create("Cooking");
	}
}
