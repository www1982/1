using System;

namespace STRINGS
{
	// Token: 0x02000FB0 RID: 4016
	public class MISC
	{
		// Token: 0x02002504 RID: 9476
		public class TAGS
		{
			// Token: 0x0400A5C4 RID: 42436
			public static LocString OTHER = "Miscellaneous";

			// Token: 0x0400A5C5 RID: 42437
			public static LocString FILTER = UI.FormatAsLink("Filtration Medium", "FILTER");

			// Token: 0x0400A5C6 RID: 42438
			public static LocString FILTER_DESC = string.Concat(new string[]
			{
				"Filtration Mediums are materials used to separate purified ",
				UI.FormatAsLink("gases", "ELEMENTS_GAS"),
				" or ",
				UI.FormatAsLink("liquids", "ELEMENTS_LIQUID"),
				" from their polluted forms.\n\nThey are consumables that will be transformed by the filtering process. For example, ",
				UI.FormatAsLink("Sand", "SAND"),
				" that has been used to filter ",
				UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
				" will become ",
				UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
				"."
			});

			// Token: 0x0400A5C7 RID: 42439
			public static LocString ICEORE = UI.FormatAsLink("Ice", "ICEORE");

			// Token: 0x0400A5C8 RID: 42440
			public static LocString ICEORE_DESC = string.Concat(new string[]
			{
				"Ice is a class of materials made up mostly (if not completely) of ",
				UI.FormatAsLink("Water", "WATER"),
				" in a frozen or partially frozen form.\n\nAs a material in a frigid solid or semi-solid state, these elements are very useful as a low-cost way to cool the environment around them.\n\nWhen heated, ice will melt into its original liquified form (ie.",
				UI.FormatAsLink("Brine Ice", "BRINEICE"),
				" will liquify into ",
				UI.FormatAsLink("Brine", "BRINE"),
				"). Each ice element has a different freezing and melting point based upon its composition and state."
			});

			// Token: 0x0400A5C9 RID: 42441
			public static LocString PHOSPHORUS = UI.FormatAsLink("Phosphorus", "PHOSPHORUS");

			// Token: 0x0400A5CA RID: 42442
			public static LocString BUILDABLERAW = UI.FormatAsLink("Raw Mineral", "BUILDABLERAW");

			// Token: 0x0400A5CB RID: 42443
			public static LocString BUILDABLERAW_DESC = string.Concat(new string[]
			{
				"Raw minerals are the unrefined forms of organic solids. Almost all raw minerals can be processed in the ",
				UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
				", although a handful require the use of the ",
				UI.FormatAsLink("Molecular Forge", "SUPERMATERIALREFINERY"),
				"."
			});

			// Token: 0x0400A5CC RID: 42444
			public static LocString BUILDABLEPROCESSED = UI.FormatAsLink("Refined Mineral", "BUILDABLEPROCESSED");

			// Token: 0x0400A5CD RID: 42445
			public static LocString BUILDABLEANY = UI.FormatAsLink("General Buildable", "BUILDABLEANY");

			// Token: 0x0400A5CE RID: 42446
			public static LocString BUILDABLEANY_DESC = "";

			// Token: 0x0400A5CF RID: 42447
			public static LocString DEHYDRATED = "Dehydrated";

			// Token: 0x0400A5D0 RID: 42448
			public static LocString FOSSILS = UI.FormatAsLink("Fossil", "FOSSILS");

			// Token: 0x0400A5D1 RID: 42449
			public static LocString FOSSILS_DESC = "Fossil is a category of composite rocks and minerals that contain traces of petrified lifeforms.\n\nThey have varied uses as basic building materials, sculpting blocks, or raw ingredients in the production of higher-grade materials.";

			// Token: 0x0400A5D2 RID: 42450
			public static LocString PLASTIFIABLELIQUID = UI.FormatAsLink("Plastic Monomer", "PLASTIFIABLELIQUID");

			// Token: 0x0400A5D3 RID: 42451
			public static LocString PLASTIFIABLELIQUID_DESC = string.Concat(new string[]
			{
				"Plastic monomers are organic compounds that can be processed into ",
				UI.FormatAsLink("Plastics", "PLASTIC"),
				" that have valuable applications as advanced building materials.\n\nPlastics derived from these monomers can also be used as packaging materials for ",
				UI.FormatAsLink("Food", "FOOD"),
				" preservation."
			});

			// Token: 0x0400A5D4 RID: 42452
			public static LocString UNREFINEDOIL = UI.FormatAsLink("Unrefined Oil", "UNREFINEDOIL");

			// Token: 0x0400A5D5 RID: 42453
			public static LocString UNREFINEDOIL_DESC = "Oils in their raw, minimally processed forms. They can be used as industrial lubricants or refined for other applications at designated buildings.";

			// Token: 0x0400A5D6 RID: 42454
			public static LocString REFINEDMETAL = UI.FormatAsLink("Refined Metal", "REFINEDMETAL");

			// Token: 0x0400A5D7 RID: 42455
			public static LocString REFINEDMETAL_DESC = string.Concat(new string[]
			{
				"Refined metals are purified forms of metal often used in higher-tier electronics due to their tendency to be able to withstand higher temperatures when they are made into wires. Other benefits include the increased decor value for some metals which can greatly improve the well-being of a colony.\n\nMetal ore can be refined in either the ",
				UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
				" or the ",
				UI.FormatAsLink("Metal Refinery", "METALREFINERY"),
				"."
			});

			// Token: 0x0400A5D8 RID: 42456
			public static LocString METAL = UI.FormatAsLink("Metal Ore", "METAL");

			// Token: 0x0400A5D9 RID: 42457
			public static LocString METAL_DESC = string.Concat(new string[]
			{
				"Metal ore is the raw form of metal, and has a wide variety of practical applications in electronics and general construction.\n\nMetal ore is typically processed into ",
				UI.FormatAsLink("Refined Metal", "REFINEDMETAL"),
				" using the ",
				UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
				" or the ",
				UI.FormatAsLink("Metal Refinery", "METALREFINERY"),
				".\n\nSome rare metal ores can also be refined in the ",
				UI.FormatAsLink("Molecular Forge", "SUPERMATERIALREFINERY"),
				"."
			});

			// Token: 0x0400A5DA RID: 42458
			public static LocString PRECIOUSMETAL = UI.FormatAsLink("Precious Metal", "PRECIOUSMETAL");

			// Token: 0x0400A5DB RID: 42459
			public static LocString RAWPRECIOUSMETAL = "Precious Metal Ore";

			// Token: 0x0400A5DC RID: 42460
			public static LocString PRECIOUSROCK = UI.FormatAsLink("Precious Rock", "PRECIOUSROCK");

			// Token: 0x0400A5DD RID: 42461
			public static LocString PRECIOUSROCK_DESC = "Precious rocks are raw minerals. Their extreme hardness produces durable " + UI.FormatAsLink("Decor", "DECOR") + ".\n\nSome precious rocks are inherently attractive even in their natural, unfinished form.";

			// Token: 0x0400A5DE RID: 42462
			public static LocString ALLOY = UI.FormatAsLink("Alloy", "ALLOY");

			// Token: 0x0400A5DF RID: 42463
			public static LocString BUILDINGFIBER = UI.FormatAsLink("Fibers", "BUILDINGFIBER");

			// Token: 0x0400A5E0 RID: 42464
			public static LocString BUILDINGFIBER_DESC = "Fibers are organically sourced polymers which are both sturdy and sensorially pleasant, making them suitable in the construction of " + UI.FormatAsLink("Morale", "MORALE") + "-boosting buildings.";

			// Token: 0x0400A5E1 RID: 42465
			public static LocString BUILDINGWOOD = UI.FormatAsLink("Wood", "BUILDINGWOOD");

			// Token: 0x0400A5E2 RID: 42466
			public static LocString BUILDINGWOOD_DESC = string.Concat(new string[]
			{
				"Wood is a renewable building material which can also be used as a valuable source of fuel and electricity when refined at the ",
				UI.FormatAsLink("Wood Burner", "WOODGASGENERATOR"),
				" or the ",
				UI.FormatAsLink("Ethanol Distiller", "ETHANOLDISTILLERY"),
				"."
			});

			// Token: 0x0400A5E3 RID: 42467
			public static LocString CRUSHABLE = "Crushable";

			// Token: 0x0400A5E4 RID: 42468
			public static LocString CROPSEEDS = "Crop Seeds";

			// Token: 0x0400A5E5 RID: 42469
			public static LocString CERAMIC = UI.FormatAsLink("Ceramic", "CERAMIC");

			// Token: 0x0400A5E6 RID: 42470
			public static LocString POLYPROPYLENE = UI.FormatAsLink("Plastic", "POLYPROPYLENE");

			// Token: 0x0400A5E7 RID: 42471
			public static LocString BAGABLECREATURE = UI.FormatAsLink("Critter", "CREATURES");

			// Token: 0x0400A5E8 RID: 42472
			public static LocString SWIMMINGCREATURE = "Aquatic Critter";

			// Token: 0x0400A5E9 RID: 42473
			public static LocString LIFE = "Life";

			// Token: 0x0400A5EA RID: 42474
			public static LocString LIQUIFIABLE = "Liquefiable";

			// Token: 0x0400A5EB RID: 42475
			public static LocString LIQUID = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID");

			// Token: 0x0400A5EC RID: 42476
			public static LocString LUBRICATINGOIL = UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL");

			// Token: 0x0400A5ED RID: 42477
			public static LocString LUBRICATINGOIL_DESC = "Gear oils are lubricating fluids useful in the maintenance of complex machinery, protecting gear systems from damage and minimizing friction between moving parts to support optimal performance.";

			// Token: 0x0400A5EE RID: 42478
			public static LocString REMOTEOPERABLE = UI.FormatAsLink("Remote Workable", "REMOTEOPERABLE");

			// Token: 0x0400A5EF RID: 42479
			public static LocString REMOTEOPERABLE_DESC = string.Concat(new string[]
			{
				"These buildings can be operated from a distance by a ",
				UI.FormatAsLink("Remote Controller", "REMOTEWORKTERMINAL"),
				" so long as they are built within range of a ",
				UI.FormatAsLink("Remote Worker Dock", "REMOTEWORKERDOCK"),
				"."
			});

			// Token: 0x0400A5F0 RID: 42480
			public static LocString SLIPPERY = "Slippery";

			// Token: 0x0400A5F1 RID: 42481
			public static LocString LEAD = UI.FormatAsLink("Lead", "LEAD");

			// Token: 0x0400A5F2 RID: 42482
			public static LocString CHARGEDPORTABLEBATTERY = UI.FormatAsLink("Power Banks", "ELECTROBANK");

			// Token: 0x0400A5F3 RID: 42483
			public static LocString EMPTYPORTABLEBATTERY = UI.FormatAsLink("Empty Eco Power Banks", "ELECTROBANK_EMPTY");

			// Token: 0x0400A5F4 RID: 42484
			public static LocString SPECIAL = "Special";

			// Token: 0x0400A5F5 RID: 42485
			public static LocString FARMABLE = UI.FormatAsLink("Cultivable Soil", "FARMABLE");

			// Token: 0x0400A5F6 RID: 42486
			public static LocString FARMABLE_DESC = "Cultivable soil is a fundamental building block of basic agricultural systems and can also be useful in the production of clean " + UI.FormatAsLink("Oxygen", "OXYGEN") + ".";

			// Token: 0x0400A5F7 RID: 42487
			public static LocString AGRICULTURE = UI.FormatAsLink("Agriculture", "AGRICULTURE");

			// Token: 0x0400A5F8 RID: 42488
			public static LocString COAL = "Coal";

			// Token: 0x0400A5F9 RID: 42489
			public static LocString BLEACHSTONE = "Bleach Stone";

			// Token: 0x0400A5FA RID: 42490
			public static LocString ORGANICS = "Organic";

			// Token: 0x0400A5FB RID: 42491
			public static LocString CONSUMABLEORE = "Consumable Ore";

			// Token: 0x0400A5FC RID: 42492
			public static LocString SUBLIMATING = UI.FormatAsLink("Sublimators", "SUBLIMATES");

			// Token: 0x0400A5FD RID: 42493
			public static LocString SUBLIMATING_SUBHEADER = "Off-Gassing Elements";

			// Token: 0x0400A5FE RID: 42494
			public static LocString SUBLIMATING_DESC = string.Concat(new string[]
			{
				"Sublimators are a class of ",
				UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
				" elements that passively convert to a ",
				UI.FormatAsLink("Gaseous", "ELEMENTS_GAS"),
				" state. When off-gassing is complete, no trace of the original solid remains.\n\nThis passive conversion persists when the element is left in storage."
			});

			// Token: 0x0400A5FF RID: 42495
			public static LocString ORE = "Ore";

			// Token: 0x0400A600 RID: 42496
			public static LocString BREATHABLE = "Breathable Gas";

			// Token: 0x0400A601 RID: 42497
			public static LocString UNBREATHABLE = "Unbreathable Gas";

			// Token: 0x0400A602 RID: 42498
			public static LocString GAS = "Gas";

			// Token: 0x0400A603 RID: 42499
			public static LocString BURNS = "Flammable";

			// Token: 0x0400A604 RID: 42500
			public static LocString UNSTABLE = "Unstable";

			// Token: 0x0400A605 RID: 42501
			public static LocString TOXIC = "Toxic";

			// Token: 0x0400A606 RID: 42502
			public static LocString MIXTURE = "Mixture";

			// Token: 0x0400A607 RID: 42503
			public static LocString SOLID = UI.FormatAsLink("Solid", "ELEMENTS_SOLID");

			// Token: 0x0400A608 RID: 42504
			public static LocString FLYINGCRITTEREDIBLE = "Bait";

			// Token: 0x0400A609 RID: 42505
			public static LocString INDUSTRIALPRODUCT = "Industrial Product";

			// Token: 0x0400A60A RID: 42506
			public static LocString INDUSTRIALINGREDIENT = UI.FormatAsLink("Industrial Ingredient", "INDUSTRIALINGREDIENT");

			// Token: 0x0400A60B RID: 42507
			public static LocString MEDICALSUPPLIES = "Medical Supplies";

			// Token: 0x0400A60C RID: 42508
			public static LocString CLOTHES = UI.FormatAsLink("Clothing", "EQUIPMENT");

			// Token: 0x0400A60D RID: 42509
			public static LocString EMITSLIGHT = UI.FormatAsLink("Light Emitter", "LIGHT");

			// Token: 0x0400A60E RID: 42510
			public static LocString BED = "Beds";

			// Token: 0x0400A60F RID: 42511
			public static LocString MESSSTATION = "Dining Tables";

			// Token: 0x0400A610 RID: 42512
			public static LocString TOY = "Toy";

			// Token: 0x0400A611 RID: 42513
			public static LocString SUIT = "Suits";

			// Token: 0x0400A612 RID: 42514
			public static LocString MULTITOOL = "Multitool";

			// Token: 0x0400A613 RID: 42515
			public static LocString CLINIC = "Clinic";

			// Token: 0x0400A614 RID: 42516
			public static LocString RELAXATION_POINT = "Leisure Area";

			// Token: 0x0400A615 RID: 42517
			public static LocString SOLIDMATERIAL = "Solid Material";

			// Token: 0x0400A616 RID: 42518
			public static LocString EXTRUDABLE = "Extrudable";

			// Token: 0x0400A617 RID: 42519
			public static LocString PLUMBABLE = UI.FormatAsLink("Plumbable", "PLUMBABLE");

			// Token: 0x0400A618 RID: 42520
			public static LocString PLUMBABLE_DESC = "";

			// Token: 0x0400A619 RID: 42521
			public static LocString COMPOSTABLE = UI.FormatAsLink("Compostable", "COMPOSTABLE");

			// Token: 0x0400A61A RID: 42522
			public static LocString COMPOSTABLE_SUBHEADER = "Recyclable Organics";

			// Token: 0x0400A61B RID: 42523
			public static LocString COMPOSTABLE_DESC = string.Concat(new string[]
			{
				"Compostables are biological materials which can be put into a ",
				UI.FormatAsLink("Compost", "COMPOST"),
				" to generate clean ",
				UI.FormatAsLink("Dirt", "DIRT"),
				".\n\nComposting also generates a small amount of ",
				UI.FormatAsLink("Heat", "HEAT"),
				".\n\nOnce it starts to rot, consumable food should be composted to prevent ",
				UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
				"."
			});

			// Token: 0x0400A61C RID: 42524
			public static LocString COMPOSTBASICPLANTFOOD = "Compost Muckroot";

			// Token: 0x0400A61D RID: 42525
			public static LocString EDIBLE = "Edible";

			// Token: 0x0400A61E RID: 42526
			public static LocString OXIDIZER = "Oxidizer";

			// Token: 0x0400A61F RID: 42527
			public static LocString COOKINGINGREDIENT = "Cooking Ingredient";

			// Token: 0x0400A620 RID: 42528
			public static LocString MEDICINE = "Medicine";

			// Token: 0x0400A621 RID: 42529
			public static LocString SEED = "Seed";

			// Token: 0x0400A622 RID: 42530
			public static LocString ANYWATER = "Water Based";

			// Token: 0x0400A623 RID: 42531
			public static LocString MARKEDFORCOMPOST = "Marked For Compost";

			// Token: 0x0400A624 RID: 42532
			public static LocString MARKEDFORCOMPOSTINSTORAGE = "In Compost Storage";

			// Token: 0x0400A625 RID: 42533
			public static LocString COMPOSTMEAT = "Compost Meat";

			// Token: 0x0400A626 RID: 42534
			public static LocString PICKLED = "Pickled";

			// Token: 0x0400A627 RID: 42535
			public static LocString PLASTIC = UI.FormatAsLink("Plastics", "PLASTIC");

			// Token: 0x0400A628 RID: 42536
			public static LocString PLASTIC_DESC = string.Concat(new string[]
			{
				"Plastics are synthetic ",
				UI.FormatAsLink("Solids", "ELEMENTSSOLID"),
				" that are pliable and minimize the transfer of ",
				UI.FormatAsLink("Heat", "Heat"),
				". They typically have a low melting point, although more advanced plastics have been developed to circumvent this issue."
			});

			// Token: 0x0400A629 RID: 42537
			public static LocString TOILET = "Toilets";

			// Token: 0x0400A62A RID: 42538
			public static LocString MASSAGE_TABLE = "Massage Tables";

			// Token: 0x0400A62B RID: 42539
			public static LocString POWERSTATION = "Power Station";

			// Token: 0x0400A62C RID: 42540
			public static LocString FARMSTATION = "Farm Station";

			// Token: 0x0400A62D RID: 42541
			public static LocString MACHINE_SHOP = "Machine Shop";

			// Token: 0x0400A62E RID: 42542
			public static LocString ANTISEPTIC = "Antiseptic";

			// Token: 0x0400A62F RID: 42543
			public static LocString OIL = "Hydrocarbon";

			// Token: 0x0400A630 RID: 42544
			public static LocString DECORATION = "Decoration";

			// Token: 0x0400A631 RID: 42545
			public static LocString EGG = "Critter Egg";

			// Token: 0x0400A632 RID: 42546
			public static LocString EGGSHELL = "Egg Shell";

			// Token: 0x0400A633 RID: 42547
			public static LocString MANUFACTUREDMATERIAL = "Manufactured Material";

			// Token: 0x0400A634 RID: 42548
			public static LocString STEEL = "Steel";

			// Token: 0x0400A635 RID: 42549
			public static LocString RAW = "Raw Animal Product";

			// Token: 0x0400A636 RID: 42550
			public static LocString FOSSIL = "Fossil";

			// Token: 0x0400A637 RID: 42551
			public static LocString ICE = "Ice";

			// Token: 0x0400A638 RID: 42552
			public static LocString ANY = "Any";

			// Token: 0x0400A639 RID: 42553
			public static LocString TRANSPARENT = "Transparent";

			// Token: 0x0400A63A RID: 42554
			public static LocString TRANSPARENT_DESC = string.Concat(new string[]
			{
				"Transparent materials allow ",
				UI.FormatAsLink("Light", "LIGHT"),
				" to pass through. Illumination boosts Duplicant productivity during working hours, but undermines sleep quality.\n\nTransparency is also important for buildings that require a clear line of sight in order to function correctly, such as the ",
				UI.FormatAsLink("Space Scanner", "COMETDETECTOR"),
				"."
			});

			// Token: 0x0400A63B RID: 42555
			public static LocString RAREMATERIALS = "Rare Resource";

			// Token: 0x0400A63C RID: 42556
			public static LocString FARMINGMATERIAL = "Fertilizer";

			// Token: 0x0400A63D RID: 42557
			public static LocString INSULATOR = UI.FormatAsLink("Insulator", "INSULATOR");

			// Token: 0x0400A63E RID: 42558
			public static LocString INSULATOR_DESC = "Insulators have low thermal conductivity, and effectively reduce the speed at which " + UI.FormatAsLink("Heat", "Heat") + " is transferred through them.";

			// Token: 0x0400A63F RID: 42559
			public static LocString RAILGUNPAYLOADEMPTYABLE = "Payload";

			// Token: 0x0400A640 RID: 42560
			public static LocString NONCRUSHABLE = "Uncrushable";

			// Token: 0x0400A641 RID: 42561
			public static LocString STORYTRAITRESOURCE = "Story Trait";

			// Token: 0x0400A642 RID: 42562
			public static LocString GLASS = "Glass";

			// Token: 0x0400A643 RID: 42563
			public static LocString OBSIDIAN = UI.FormatAsLink("Obsidian", "OBSIDIAN");

			// Token: 0x0400A644 RID: 42564
			public static LocString DIAMOND = UI.FormatAsLink("Diamond", "DIAMOND");

			// Token: 0x0400A645 RID: 42565
			public static LocString SNOW = UI.FormatAsLink("Snow", "STABLESNOW");

			// Token: 0x0400A646 RID: 42566
			public static LocString WOODLOG = UI.FormatAsLink("Wood", "WOODLOG");

			// Token: 0x0400A647 RID: 42567
			public static LocString OXYGENCANISTER = "Oxygen Canister";

			// Token: 0x0400A648 RID: 42568
			public static LocString COMMAND_MODULE = "Command Module";

			// Token: 0x0400A649 RID: 42569
			public static LocString HABITAT_MODULE = "Habitat Module";

			// Token: 0x0400A64A RID: 42570
			public static LocString COMBUSTIBLEGAS = UI.FormatAsLink("Combustible Gas", "COMBUSTIBLEGAS");

			// Token: 0x0400A64B RID: 42571
			public static LocString COMBUSTIBLEGAS_DESC = string.Concat(new string[]
			{
				"Combustible Gases can be burned as fuel to be used in the production of ",
				UI.FormatAsLink("Power", "POWER"),
				" and ",
				UI.FormatAsLink("Food", "FOOD"),
				"."
			});

			// Token: 0x0400A64C RID: 42572
			public static LocString COMBUSTIBLELIQUID = UI.FormatAsLink("Combustible Liquid", "COMBUSTIBLELIQUID");

			// Token: 0x0400A64D RID: 42573
			public static LocString COMBUSTIBLELIQUID_DESC = string.Concat(new string[]
			{
				"Combustible Liquids can be burned as fuels to be used in energy production, such as in a ",
				UI.FormatAsLink("Petroleum Generator", "PETROLEUMGENERATOR"),
				" or a ",
				UI.FormatAsLink(KeroseneEngineHelper.NAME, KeroseneEngineHelper.CODEXID),
				".\n\nThough these liquids have other uses, such as fertilizer for growing a ",
				UI.FormatAsLink("Nosh Bean", "BEANPLANTSEED"),
				", their primary usefulness lies in their ability to be burned for ",
				UI.FormatAsLink("Power", "POWER"),
				"."
			});

			// Token: 0x0400A64E RID: 42574
			public static LocString COMBUSTIBLESOLID = UI.FormatAsLink("Combustible Solid", "COMBUSTIBLESOLID");

			// Token: 0x0400A64F RID: 42575
			public static LocString COMBUSTIBLESOLID_DESC = "Combustible Solids can be burned as fuel to be used in " + UI.FormatAsLink("Power", "POWER") + " production.";

			// Token: 0x0400A650 RID: 42576
			public static LocString UNIDENTIFIEDSEED = "Seed (Unidentified Mutation)";

			// Token: 0x0400A651 RID: 42577
			public static LocString CHARMEDARTIFACT = "Artifact of Interest";

			// Token: 0x0400A652 RID: 42578
			public static LocString GENE_SHUFFLER = "Neural Vacillator";

			// Token: 0x0400A653 RID: 42579
			public static LocString WARP_PORTAL = "Teleportal";

			// Token: 0x0400A654 RID: 42580
			public static LocString BIONICUPGRADE = "Boosters";

			// Token: 0x0400A655 RID: 42581
			public static LocString FARMING = "Farm Build-Delivery";

			// Token: 0x0400A656 RID: 42582
			public static LocString RESEARCH = "Research Delivery";

			// Token: 0x0400A657 RID: 42583
			public static LocString POWER = "Generator Delivery";

			// Token: 0x0400A658 RID: 42584
			public static LocString BUILDING = "Build Dig-Delivery";

			// Token: 0x0400A659 RID: 42585
			public static LocString COOKING = "Cook Delivery";

			// Token: 0x0400A65A RID: 42586
			public static LocString FABRICATING = "Fabricate Delivery";

			// Token: 0x0400A65B RID: 42587
			public static LocString WIRING = "Wire Build-Delivery";

			// Token: 0x0400A65C RID: 42588
			public static LocString ART = "Art Build-Delivery";

			// Token: 0x0400A65D RID: 42589
			public static LocString DOCTORING = "Treatment Delivery";

			// Token: 0x0400A65E RID: 42590
			public static LocString CONVEYOR = "Shipping Build";

			// Token: 0x0400A65F RID: 42591
			public static LocString COMPOST_FORMAT = "{Item}";

			// Token: 0x0400A660 RID: 42592
			public static LocString ADVANCEDDOCTORSTATIONMEDICALSUPPLIES = "Serum Vial";

			// Token: 0x0400A661 RID: 42593
			public static LocString DOCTORSTATIONMEDICALSUPPLIES = "Medical Pack";
		}

		// Token: 0x02002505 RID: 9477
		public class STATUSITEMS
		{
			// Token: 0x02003794 RID: 14228
			public class ATTENTIONREQUIRED
			{
				// Token: 0x0400E15B RID: 57691
				public static LocString NAME = "Attention Required!";

				// Token: 0x0400E15C RID: 57692
				public static LocString TOOLTIP = "Something in my colony needs to be attended to";
			}

			// Token: 0x02003795 RID: 14229
			public class SUBLIMATIONBLOCKED
			{
				// Token: 0x0400E15D RID: 57693
				public static LocString NAME = "{SubElement} emission blocked";

				// Token: 0x0400E15E RID: 57694
				public static LocString TOOLTIP = "This {Element} deposit is not exposed to air and cannot emit {SubElement}";
			}

			// Token: 0x02003796 RID: 14230
			public class SUBLIMATIONOVERPRESSURE
			{
				// Token: 0x0400E15F RID: 57695
				public static LocString NAME = "Inert";

				// Token: 0x0400E160 RID: 57696
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Environmental ",
					UI.PRE_KEYWORD,
					"Gas Pressure",
					UI.PST_KEYWORD,
					" is too high for this {Element} deposit to emit {SubElement}"
				});
			}

			// Token: 0x02003797 RID: 14231
			public class SUBLIMATIONEMITTING
			{
				// Token: 0x0400E161 RID: 57697
				public static LocString NAME = BUILDING.STATUSITEMS.EMITTINGGASAVG.NAME;

				// Token: 0x0400E162 RID: 57698
				public static LocString TOOLTIP = BUILDING.STATUSITEMS.EMITTINGGASAVG.TOOLTIP;
			}

			// Token: 0x02003798 RID: 14232
			public class SPACE
			{
				// Token: 0x0400E163 RID: 57699
				public static LocString NAME = "Space exposure";

				// Token: 0x0400E164 RID: 57700
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This region is exposed to the vacuum of space and will result in the loss of ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" resources"
				});
			}

			// Token: 0x02003799 RID: 14233
			public class EDIBLE
			{
				// Token: 0x0400E165 RID: 57701
				public static LocString NAME = "Rations: {0}";

				// Token: 0x0400E166 RID: 57702
				public static LocString TOOLTIP = "Can provide " + UI.FormatAsLink("{0}", "KCAL") + " of energy to Duplicants";
			}

			// Token: 0x0200379A RID: 14234
			public class REHYDRATEDFOOD
			{
				// Token: 0x0400E167 RID: 57703
				public static LocString NAME = "Rehydrated Food";

				// Token: 0x0400E168 RID: 57704
				public static LocString TOOLTIP = string.Format(string.Concat(new string[]
				{
					"This food has been carefully re-moistened for consumption\n\n",
					UI.PRE_KEYWORD,
					"{1}",
					UI.PST_KEYWORD,
					": {0}"
				}), -1f, UI.FormatAsLink(DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.NAME, DUPLICANTS.ATTRIBUTES.QUALITYOFLIFE.NAME));
			}

			// Token: 0x0200379B RID: 14235
			public class MARKEDFORDISINFECTION
			{
				// Token: 0x0400E169 RID: 57705
				public static LocString NAME = "Disinfect Errand";

				// Token: 0x0400E16A RID: 57706
				public static LocString TOOLTIP = "Building will be disinfected once a Duplicant is available";
			}

			// Token: 0x0200379C RID: 14236
			public class PENDINGCLEAR
			{
				// Token: 0x0400E16B RID: 57707
				public static LocString NAME = "Sweep Errand";

				// Token: 0x0400E16C RID: 57708
				public static LocString TOOLTIP = "Debris will be swept once a Duplicant is available";
			}

			// Token: 0x0200379D RID: 14237
			public class PENDINGCLEARNOSTORAGE
			{
				// Token: 0x0400E16D RID: 57709
				public static LocString NAME = "Storage Unavailable";

				// Token: 0x0400E16E RID: 57710
				public static LocString TOOLTIP = "No available " + BUILDINGS.PREFABS.STORAGELOCKER.NAME + " can accept this item\n\nMake sure the filter on your storage is correctly set and there is sufficient space remaining";
			}

			// Token: 0x0200379E RID: 14238
			public class MARKEDFORCOMPOST
			{
				// Token: 0x0400E16F RID: 57711
				public static LocString NAME = "Compost Errand";

				// Token: 0x0400E170 RID: 57712
				public static LocString TOOLTIP = "Object is marked and will be moved to " + BUILDINGS.PREFABS.COMPOST.NAME + " once a Duplicant is available";
			}

			// Token: 0x0200379F RID: 14239
			public class NOCLEARLOCATIONSAVAILABLE
			{
				// Token: 0x0400E171 RID: 57713
				public static LocString NAME = "No Sweep Destination";

				// Token: 0x0400E172 RID: 57714
				public static LocString TOOLTIP = "There are no valid destinations for this object to be swept to";
			}

			// Token: 0x020037A0 RID: 14240
			public class PENDINGHARVEST
			{
				// Token: 0x0400E173 RID: 57715
				public static LocString NAME = "Harvest Errand";

				// Token: 0x0400E174 RID: 57716
				public static LocString TOOLTIP = "Plant will be harvested once a Duplicant is available";
			}

			// Token: 0x020037A1 RID: 14241
			public class PENDINGUPROOT
			{
				// Token: 0x0400E175 RID: 57717
				public static LocString NAME = "Uproot Errand";

				// Token: 0x0400E176 RID: 57718
				public static LocString TOOLTIP = "Plant will be uprooted once a Duplicant is available";
			}

			// Token: 0x020037A2 RID: 14242
			public class WAITINGFORDIG
			{
				// Token: 0x0400E177 RID: 57719
				public static LocString NAME = "Dig Errand";

				// Token: 0x0400E178 RID: 57720
				public static LocString TOOLTIP = "Tile will be dug out once a Duplicant is available";
			}

			// Token: 0x020037A3 RID: 14243
			public class WAITINGFORMOP
			{
				// Token: 0x0400E179 RID: 57721
				public static LocString NAME = "Mop Errand";

				// Token: 0x0400E17A RID: 57722
				public static LocString TOOLTIP = "Spill will be mopped once a Duplicant is available";
			}

			// Token: 0x020037A4 RID: 14244
			public class NOTMARKEDFORHARVEST
			{
				// Token: 0x0400E17B RID: 57723
				public static LocString NAME = "No Harvest Pending";

				// Token: 0x0400E17C RID: 57724
				public static LocString TOOLTIP = "Use the " + UI.FormatAsTool("Harvest Tool", global::Action.Harvest) + " to mark this plant for harvest";
			}

			// Token: 0x020037A5 RID: 14245
			public class GROWINGBRANCHES
			{
				// Token: 0x0400E17D RID: 57725
				public static LocString NAME = "Growing Branches";

				// Token: 0x0400E17E RID: 57726
				public static LocString TOOLTIP = "This tree is working hard to grow new branches right now";
			}

			// Token: 0x020037A6 RID: 14246
			public class CLUSTERMETEORREMAININGTRAVELTIME
			{
				// Token: 0x0400E17F RID: 57727
				public static LocString NAME = "Time to collision: {time}";

				// Token: 0x0400E180 RID: 57728
				public static LocString TOOLTIP = "The time remaining before this meteor reaches its destination";
			}

			// Token: 0x020037A7 RID: 14247
			public class ELEMENTALCATEGORY
			{
				// Token: 0x0400E181 RID: 57729
				public static LocString NAME = "{Category}";

				// Token: 0x0400E182 RID: 57730
				public static LocString TOOLTIP = "The selected object belongs to the <b>{Category}</b> resource category";
			}

			// Token: 0x020037A8 RID: 14248
			public class ELEMENTALMASS
			{
				// Token: 0x0400E183 RID: 57731
				public static LocString NAME = "{Mass}";

				// Token: 0x0400E184 RID: 57732
				public static LocString TOOLTIP = "The selected object has a mass of <b>{Mass}</b>";
			}

			// Token: 0x020037A9 RID: 14249
			public class ELEMENTALDISEASE
			{
				// Token: 0x0400E185 RID: 57733
				public static LocString NAME = "{Disease}";

				// Token: 0x0400E186 RID: 57734
				public static LocString TOOLTIP = "Current disease: {Disease}";
			}

			// Token: 0x020037AA RID: 14250
			public class ELEMENTALTEMPERATURE
			{
				// Token: 0x0400E187 RID: 57735
				public static LocString NAME = "{Temp}";

				// Token: 0x0400E188 RID: 57736
				public static LocString TOOLTIP = "The selected object is currently <b>{Temp}</b>";
			}

			// Token: 0x020037AB RID: 14251
			public class MARKEDFORCOMPOSTINSTORAGE
			{
				// Token: 0x0400E189 RID: 57737
				public static LocString NAME = "Composted";

				// Token: 0x0400E18A RID: 57738
				public static LocString TOOLTIP = "The selected object is currently in the compost";
			}

			// Token: 0x020037AC RID: 14252
			public class BURIEDITEM
			{
				// Token: 0x0400E18B RID: 57739
				public static LocString NAME = "Buried Object";

				// Token: 0x0400E18C RID: 57740
				public static LocString TOOLTIP = "Something seems to be hidden here";

				// Token: 0x0400E18D RID: 57741
				public static LocString NOTIFICATION = "Buried object discovered";

				// Token: 0x0400E18E RID: 57742
				public static LocString NOTIFICATION_TOOLTIP = "My Duplicants have uncovered a {Uncoverable}!\n\n" + UI.CLICK(UI.ClickType.Click) + " to jump to its location.";
			}

			// Token: 0x020037AD RID: 14253
			public class GENETICANALYSISCOMPLETED
			{
				// Token: 0x0400E18F RID: 57743
				public static LocString NAME = "Genome Sequenced";

				// Token: 0x0400E190 RID: 57744
				public static LocString TOOLTIP = "This Station has sequenced a new seed mutation";
			}

			// Token: 0x020037AE RID: 14254
			public class HEALTHSTATUS
			{
				// Token: 0x02003BC5 RID: 15301
				public class PERFECT
				{
					// Token: 0x0400EBE2 RID: 60386
					public static LocString NAME = "None";

					// Token: 0x0400EBE3 RID: 60387
					public static LocString TOOLTIP = "This Duplicant is in peak condition";
				}

				// Token: 0x02003BC6 RID: 15302
				public class ALRIGHT
				{
					// Token: 0x0400EBE4 RID: 60388
					public static LocString NAME = "None";

					// Token: 0x0400EBE5 RID: 60389
					public static LocString TOOLTIP = "This Duplicant is none the worse for wear";
				}

				// Token: 0x02003BC7 RID: 15303
				public class SCUFFED
				{
					// Token: 0x0400EBE6 RID: 60390
					public static LocString NAME = "Minor";

					// Token: 0x0400EBE7 RID: 60391
					public static LocString TOOLTIP = "This Duplicant has a few scrapes and bruises";
				}

				// Token: 0x02003BC8 RID: 15304
				public class INJURED
				{
					// Token: 0x0400EBE8 RID: 60392
					public static LocString NAME = "Moderate";

					// Token: 0x0400EBE9 RID: 60393
					public static LocString TOOLTIP = "This Duplicant needs some patching up";
				}

				// Token: 0x02003BC9 RID: 15305
				public class CRITICAL
				{
					// Token: 0x0400EBEA RID: 60394
					public static LocString NAME = "Severe";

					// Token: 0x0400EBEB RID: 60395
					public static LocString TOOLTIP = "This Duplicant is in serious need of medical attention";
				}

				// Token: 0x02003BCA RID: 15306
				public class INCAPACITATED
				{
					// Token: 0x0400EBEC RID: 60396
					public static LocString NAME = "Paralyzing";

					// Token: 0x0400EBED RID: 60397
					public static LocString TOOLTIP = "This Duplicant will die if they do not receive medical attention";
				}

				// Token: 0x02003BCB RID: 15307
				public class DEAD
				{
					// Token: 0x0400EBEE RID: 60398
					public static LocString NAME = "Conclusive";

					// Token: 0x0400EBEF RID: 60399
					public static LocString TOOLTIP = "This Duplicant won't be getting back up";
				}
			}

			// Token: 0x020037AF RID: 14255
			public class HIT
			{
				// Token: 0x0400E191 RID: 57745
				public static LocString NAME = "{targetName} took {damageAmount} damage from {attackerName}'s attack!";
			}

			// Token: 0x020037B0 RID: 14256
			public class OREMASS
			{
				// Token: 0x0400E192 RID: 57746
				public static LocString NAME = MISC.STATUSITEMS.ELEMENTALMASS.NAME;

				// Token: 0x0400E193 RID: 57747
				public static LocString TOOLTIP = MISC.STATUSITEMS.ELEMENTALMASS.TOOLTIP;
			}

			// Token: 0x020037B1 RID: 14257
			public class ORETEMP
			{
				// Token: 0x0400E194 RID: 57748
				public static LocString NAME = MISC.STATUSITEMS.ELEMENTALTEMPERATURE.NAME;

				// Token: 0x0400E195 RID: 57749
				public static LocString TOOLTIP = MISC.STATUSITEMS.ELEMENTALTEMPERATURE.TOOLTIP;
			}

			// Token: 0x020037B2 RID: 14258
			public class TREEFILTERABLETAGS
			{
				// Token: 0x0400E196 RID: 57750
				public static LocString NAME = "{Tags}";

				// Token: 0x0400E197 RID: 57751
				public static LocString TOOLTIP = "{Tags}";
			}

			// Token: 0x020037B3 RID: 14259
			public class SPOUTOVERPRESSURE
			{
				// Token: 0x0400E198 RID: 57752
				public static LocString NAME = "Overpressure {StudiedDetails}";

				// Token: 0x0400E199 RID: 57753
				public static LocString TOOLTIP = "Spout cannot vent due to high environmental pressure";

				// Token: 0x0400E19A RID: 57754
				public static LocString STUDIED = "(idle in <b>{Time}</b>)";
			}

			// Token: 0x020037B4 RID: 14260
			public class SPOUTEMITTING
			{
				// Token: 0x0400E19B RID: 57755
				public static LocString NAME = "Venting {StudiedDetails}";

				// Token: 0x0400E19C RID: 57756
				public static LocString TOOLTIP = "This geyser is erupting";

				// Token: 0x0400E19D RID: 57757
				public static LocString STUDIED = "(idle in <b>{Time}</b>)";
			}

			// Token: 0x020037B5 RID: 14261
			public class SPOUTPRESSUREBUILDING
			{
				// Token: 0x0400E19E RID: 57758
				public static LocString NAME = "Rising pressure {StudiedDetails}";

				// Token: 0x0400E19F RID: 57759
				public static LocString TOOLTIP = "This geyser's internal pressure is steadily building";

				// Token: 0x0400E1A0 RID: 57760
				public static LocString STUDIED = "(erupts in <b>{Time}</b>)";
			}

			// Token: 0x020037B6 RID: 14262
			public class SPOUTIDLE
			{
				// Token: 0x0400E1A1 RID: 57761
				public static LocString NAME = "Idle {StudiedDetails}";

				// Token: 0x0400E1A2 RID: 57762
				public static LocString TOOLTIP = "This geyser is not currently erupting";

				// Token: 0x0400E1A3 RID: 57763
				public static LocString STUDIED = "(erupts in <b>{Time}</b>)";
			}

			// Token: 0x020037B7 RID: 14263
			public class SPOUTDORMANT
			{
				// Token: 0x0400E1A4 RID: 57764
				public static LocString NAME = "Dormant";

				// Token: 0x0400E1A5 RID: 57765
				public static LocString TOOLTIP = "This geyser's geoactivity has halted\n\nIt won't erupt again for some time";
			}

			// Token: 0x020037B8 RID: 14264
			public class SPICEDFOOD
			{
				// Token: 0x0400E1A6 RID: 57766
				public static LocString NAME = "Seasoned";

				// Token: 0x0400E1A7 RID: 57767
				public static LocString TOOLTIP = "This food has been improved with spice from the " + BUILDINGS.PREFABS.SPICEGRINDER.NAME;
			}

			// Token: 0x020037B9 RID: 14265
			public class PICKUPABLEUNREACHABLE
			{
				// Token: 0x0400E1A8 RID: 57768
				public static LocString NAME = "Unreachable";

				// Token: 0x0400E1A9 RID: 57769
				public static LocString TOOLTIP = "Duplicants cannot reach this object";
			}

			// Token: 0x020037BA RID: 14266
			public class PRIORITIZED
			{
				// Token: 0x0400E1AA RID: 57770
				public static LocString NAME = "High Priority";

				// Token: 0x0400E1AB RID: 57771
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Errand",
					UI.PST_KEYWORD,
					" has been marked as important and will be preferred over other pending ",
					UI.PRE_KEYWORD,
					"Errands",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020037BB RID: 14267
			public class USING
			{
				// Token: 0x0400E1AC RID: 57772
				public static LocString NAME = "Using {Target}";

				// Token: 0x0400E1AD RID: 57773
				public static LocString TOOLTIP = "{Target} is currently in use";
			}

			// Token: 0x020037BC RID: 14268
			public class ORDERATTACK
			{
				// Token: 0x0400E1AE RID: 57774
				public static LocString NAME = "Pending Attack";

				// Token: 0x0400E1AF RID: 57775
				public static LocString TOOLTIP = "Waiting for a Duplicant to murderize this defenseless " + UI.PRE_KEYWORD + "Critter" + UI.PST_KEYWORD;
			}

			// Token: 0x020037BD RID: 14269
			public class ORDERCAPTURE
			{
				// Token: 0x0400E1B0 RID: 57776
				public static LocString NAME = "Pending Wrangle";

				// Token: 0x0400E1B1 RID: 57777
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Waiting for a Duplicant to capture this ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					"\n\nOnly Duplicants with the ",
					DUPLICANTS.ROLES.RANCHER.NAME,
					" skill can catch critters without traps"
				});
			}

			// Token: 0x020037BE RID: 14270
			public class OPERATING
			{
				// Token: 0x0400E1B2 RID: 57778
				public static LocString NAME = "In Use";

				// Token: 0x0400E1B3 RID: 57779
				public static LocString TOOLTIP = "This object is currently being used";
			}

			// Token: 0x020037BF RID: 14271
			public class CLEANING
			{
				// Token: 0x0400E1B4 RID: 57780
				public static LocString NAME = "Cleaning";

				// Token: 0x0400E1B5 RID: 57781
				public static LocString TOOLTIP = "This building is currently being cleaned";
			}

			// Token: 0x020037C0 RID: 14272
			public class REGIONISBLOCKED
			{
				// Token: 0x0400E1B6 RID: 57782
				public static LocString NAME = "Blocked";

				// Token: 0x0400E1B7 RID: 57783
				public static LocString TOOLTIP = "Undug material is blocking off an essential tile";
			}

			// Token: 0x020037C1 RID: 14273
			public class STUDIED
			{
				// Token: 0x0400E1B8 RID: 57784
				public static LocString NAME = "Analysis Complete";

				// Token: 0x0400E1B9 RID: 57785
				public static LocString TOOLTIP = "Information on this Natural Feature has been compiled below.";
			}

			// Token: 0x020037C2 RID: 14274
			public class AWAITINGSTUDY
			{
				// Token: 0x0400E1BA RID: 57786
				public static LocString NAME = "Analysis Pending";

				// Token: 0x0400E1BB RID: 57787
				public static LocString TOOLTIP = "New information on this Natural Feature will be compiled once the field study is complete";
			}

			// Token: 0x020037C3 RID: 14275
			public class DURABILITY
			{
				// Token: 0x0400E1BC RID: 57788
				public static LocString NAME = "Durability: {durability}";

				// Token: 0x0400E1BD RID: 57789
				public static LocString TOOLTIP = "Items lose durability each time they are equipped, and can no longer be put on by a Duplicant once they reach 0% durability\n\nRepair of this item can be done in the appropriate fabrication station";
			}

			// Token: 0x020037C4 RID: 14276
			public class BIONICEXPLORERBOOSTER
			{
				// Token: 0x0400E1BE RID: 57790
				public static LocString NAME = "Stored Geodata: {0}";

				// Token: 0x0400E1BF RID: 57791
				public static LocString TOOLTIP = UI.PRE_KEYWORD + "Dowsing Boosters" + UI.PST_KEYWORD + " retain geodata gathered by Bionic Duplicants\n\nWhen dowsing is complete and this booster is installed in a Bionic Duplicant, a new geyser will be revealed";
			}

			// Token: 0x020037C5 RID: 14277
			public class BIONICEXPLORERBOOSTERREADY
			{
				// Token: 0x0400E1C0 RID: 57792
				public static LocString NAME = "Dowsing Complete";

				// Token: 0x0400E1C1 RID: 57793
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Dowsing Booster",
					UI.PST_KEYWORD,
					" has sufficient geodata stored to reveal a new geyser\n\nIt must be installed in a Bionic Duplicant in order to function"
				});
			}

			// Token: 0x020037C6 RID: 14278
			public class UNASSIGNEDBIONICBOOSTER
			{
				// Token: 0x0400E1C2 RID: 57794
				public static LocString NAME = "Unassigned";

				// Token: 0x0400E1C3 RID: 57795
				public static LocString TOOLTIP = "This booster has not yet been assigned to a Bionic Duplicant";
			}

			// Token: 0x020037C7 RID: 14279
			public class STOREDITEMDURABILITY
			{
				// Token: 0x0400E1C4 RID: 57796
				public static LocString NAME = "Durability: {durability}";

				// Token: 0x0400E1C5 RID: 57797
				public static LocString TOOLTIP = "Items lose durability each time they are equipped, and can no longer be put on by a Duplicant once they reach 0% durability\n\nRepair of this item can be done in the appropriate fabrication station";
			}

			// Token: 0x020037C8 RID: 14280
			public class ARTIFACTENTOMBED
			{
				// Token: 0x0400E1C6 RID: 57798
				public static LocString NAME = "Entombed Artifact";

				// Token: 0x0400E1C7 RID: 57799
				public static LocString TOOLTIP = "This artifact is trapped in an obscuring shell limiting its decor. A skilled artist can remove it at the " + BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.NAME;
			}

			// Token: 0x020037C9 RID: 14281
			public class TEAROPEN
			{
				// Token: 0x0400E1C8 RID: 57800
				public static LocString NAME = "Temporal Tear open";

				// Token: 0x0400E1C9 RID: 57801
				public static LocString TOOLTIP = "An open passage through spacetime";
			}

			// Token: 0x020037CA RID: 14282
			public class TEARCLOSED
			{
				// Token: 0x0400E1CA RID: 57802
				public static LocString NAME = "Temporal Tear closed";

				// Token: 0x0400E1CB RID: 57803
				public static LocString TOOLTIP = "Perhaps some technology could open the passage";
			}

			// Token: 0x020037CB RID: 14283
			public class LARGEIMPACTORSTATUS
			{
				// Token: 0x0400E1CC RID: 57804
				public static LocString NAME = "Time until impact: {0}";

				// Token: 0x0400E1CD RID: 57805
				public static LocString TOOLTIP = "This impactor asteroid will reach its target in {0}";
			}

			// Token: 0x020037CC RID: 14284
			public class LARGEIMPACTORHEALTH
			{
				// Token: 0x0400E1CE RID: 57806
				public static LocString NAME = "Health: {0} / {1}";

				// Token: 0x0400E1CF RID: 57807
				public static LocString TOOLTIP = "Collision damage can be avoided by destroying this impactor asteroid with " + UI.FormatAsLink("Intracosmic Blastshot", "LONGRANGEMISSILE") + " before it makes contact";
			}

			// Token: 0x020037CD RID: 14285
			public class LONGRANGEMISSILETTI
			{
				// Token: 0x0400E1D0 RID: 57808
				public static LocString NAME = "Time To Intercept {0}: {1}";

				// Token: 0x0400E1D1 RID: 57809
				public static LocString TOOLTIP = "This projectile will reach its destination in {1}";
			}

			// Token: 0x020037CE RID: 14286
			public class MARKEDFORMOVE
			{
				// Token: 0x0400E1D2 RID: 57810
				public static LocString NAME = "Pending Move";

				// Token: 0x0400E1D3 RID: 57811
				public static LocString TOOLTIP = "Waiting for a Duplicant to move this object";
			}

			// Token: 0x020037CF RID: 14287
			public class MOVESTORAGEUNREACHABLE
			{
				// Token: 0x0400E1D4 RID: 57812
				public static LocString NAME = "Unreachable Move";

				// Token: 0x0400E1D5 RID: 57813
				public static LocString TOOLTIP = "Duplicants cannot reach this object to move it";
			}

			// Token: 0x020037D0 RID: 14288
			public class PENDINGCARVE
			{
				// Token: 0x0400E1D6 RID: 57814
				public static LocString NAME = "Carve Errand";

				// Token: 0x0400E1D7 RID: 57815
				public static LocString TOOLTIP = "Rock will be carved once a Duplicant is available";
			}

			// Token: 0x020037D1 RID: 14289
			public class ELECTROBANKLIFETIMEREMAINING
			{
				// Token: 0x0400E1D8 RID: 57816
				public static LocString NAME = "Lifetime Remaining: {0}";

				// Token: 0x0400E1D9 RID: 57817
				public static LocString TOOLTIP = "Self-charging will continue for {0}\n\nWhen lifetime reaches zero, this  " + UI.FormatAsLink("Power Bank", "ELECTROBANK") + " will explode";
			}

			// Token: 0x020037D2 RID: 14290
			public class ELECTROBANKSELFCHARGING
			{
				// Token: 0x0400E1DA RID: 57818
				public static LocString NAME = "Self-Charging: {0}";

				// Token: 0x0400E1DB RID: 57819
				public static LocString TOOLTIP = "This " + UI.FormatAsLink("Power Bank", "ELECTROBANK") + " is always slowly charging itself";
			}
		}

		// Token: 0x02002506 RID: 9478
		public class POPFX
		{
			// Token: 0x0400A662 RID: 42594
			public static LocString RESOURCE_EATEN = "Resource Eaten";

			// Token: 0x0400A663 RID: 42595
			public static LocString RESOURCE_SELECTION_CHANGED = "Changed to {0}";

			// Token: 0x0400A664 RID: 42596
			public static LocString EXTRA_POWERBANKS_BIONIC = "Extra Power Banks";
		}

		// Token: 0x02002507 RID: 9479
		public class NOTIFICATIONS
		{
			// Token: 0x020037D3 RID: 14291
			public class BASICCONTROLS
			{
				// Token: 0x0400E1DC RID: 57820
				public static LocString NAME = "Tutorial: Basic Controls";

				// Token: 0x0400E1DD RID: 57821
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"• I can use ",
					UI.FormatAsHotKey(global::Action.PanLeft),
					" and ",
					UI.FormatAsHotKey(global::Action.PanRight),
					" to pan my view left and right, and ",
					UI.FormatAsHotKey(global::Action.PanUp),
					" and ",
					UI.FormatAsHotKey(global::Action.PanDown),
					" to pan up and down.\n\n• ",
					UI.FormatAsHotKey(global::Action.ZoomIn),
					" lets me zoom in, and ",
					UI.FormatAsHotKey(global::Action.ZoomOut),
					" zooms out.\n\n• ",
					UI.FormatAsHotKey(global::Action.CameraHome),
					" returns my view to the Printing Pod.\n\n• I can speed or slow my perception of time using the top left corner buttons, or by pressing ",
					UI.FormatAsHotKey(global::Action.SpeedUp),
					" or ",
					UI.FormatAsHotKey(global::Action.SlowDown),
					". Pressing ",
					UI.FormatAsHotKey(global::Action.TogglePause),
					" will pause the flow of time entirely.\n\n• I'll keep records of everything I discover in my personal DATABASE ",
					UI.FormatAsHotKey(global::Action.ManageDatabase),
					" to refer back to if I forget anything important."
				});

				// Token: 0x0400E1DE RID: 57822
				public static LocString MESSAGEBODYALT = string.Concat(new string[]
				{
					"• I can use ",
					UI.FormatAsHotKey(global::Action.AnalogCamera),
					" to pan my view.\n\n• ",
					UI.FormatAsHotKey(global::Action.ZoomIn),
					" lets me zoom in, and ",
					UI.FormatAsHotKey(global::Action.ZoomOut),
					" zooms out.\n\n• I can speed or slow my perception of time using the top left corner buttons, or by pressing ",
					UI.FormatAsHotKey(global::Action.CycleSpeed),
					". Pressing ",
					UI.FormatAsHotKey(global::Action.TogglePause),
					" will pause the flow of time entirely.\n\n• I'll keep records of everything I discover in my personal DATABASE ",
					UI.FormatAsHotKey(global::Action.ManageDatabase),
					" to refer back to if I forget anything important."
				});

				// Token: 0x0400E1DF RID: 57823
				public static LocString TOOLTIP = "Notes on using my HUD";
			}

			// Token: 0x020037D4 RID: 14292
			public class CODEXUNLOCK
			{
				// Token: 0x0400E1E0 RID: 57824
				public static LocString NAME = "New Log Entry";

				// Token: 0x0400E1E1 RID: 57825
				public static LocString MESSAGEBODY = "I've added a new log entry to my Database";

				// Token: 0x0400E1E2 RID: 57826
				public static LocString TOOLTIP = "I've added a new log entry to my Database";
			}

			// Token: 0x020037D5 RID: 14293
			public class WELCOMEMESSAGE
			{
				// Token: 0x0400E1E3 RID: 57827
				public static LocString NAME = "Tutorial: Colony Management";

				// Token: 0x0400E1E4 RID: 57828
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"I can use the ",
					UI.FormatAsTool("Dig Tool", global::Action.Dig),
					" and the ",
					UI.FormatAsBuildMenuTab("Build Menu"),
					" in the lower left of the screen to begin planning my first construction tasks.\n\nOnce I've placed a few errands my Duplicants will automatically get to work, without me needing to direct them individually."
				});

				// Token: 0x0400E1E5 RID: 57829
				public static LocString TOOLTIP = "Notes on getting Duplicants to do my bidding";
			}

			// Token: 0x020037D6 RID: 14294
			public class STRESSMANAGEMENTMESSAGE
			{
				// Token: 0x0400E1E6 RID: 57830
				public static LocString NAME = "Tutorial: Stress Management";

				// Token: 0x0400E1E7 RID: 57831
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"At 100% ",
					UI.FormatAsLink("Stress", "STRESS"),
					", a Duplicant will have a nervous breakdown and be unable to work.\n\nBreakdowns can manifest in different colony-threatening ways, such as the destruction of buildings or the binge eating of food.\n\nI can help my Duplicants manage stressful situations by giving them access to good ",
					UI.FormatAsLink("Food", "FOOD"),
					", fancy ",
					UI.FormatAsLink("Decor", "DECOR"),
					" and comfort items which boost their ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nI can select a Duplicant and mouse over ",
					UI.FormatAsLink("Stress", "STRESS"),
					" or ",
					UI.FormatAsLink("Morale", "MORALE"),
					" in their CONDITION TAB to view current statuses, and hopefully manage things before they become a problem.\n\nRelated ",
					UI.FormatAsLink("Video: Duplicant Morale", "VIDEOS13"),
					" "
				});

				// Token: 0x0400E1E8 RID: 57832
				public static LocString TOOLTIP = "Notes on keeping Duplicants happy and productive";
			}

			// Token: 0x020037D7 RID: 14295
			public class TASKPRIORITIESMESSAGE
			{
				// Token: 0x0400E1E9 RID: 57833
				public static LocString NAME = "Tutorial: Priority";

				// Token: 0x0400E1EA RID: 57834
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Duplicants always perform errands in order of highest to lowest priority. They will harvest ",
					UI.FormatAsLink("Food", "FOOD"),
					" before they build, for example, or always build new structures before they mine materials.\n\nI can open the ",
					UI.FormatAsManagementMenu("Priorities Screen", global::Action.ManagePriorities),
					" to set which Errand Types Duplicants may or may not perform, or to specialize skilled Duplicants for particular Errand Types."
				});

				// Token: 0x0400E1EB RID: 57835
				public static LocString TOOLTIP = "Notes on managing Duplicants' errands";
			}

			// Token: 0x020037D8 RID: 14296
			public class MOPPINGMESSAGE
			{
				// Token: 0x0400E1EC RID: 57836
				public static LocString NAME = "Tutorial: Polluted Water";

				// Token: 0x0400E1ED RID: 57837
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" slowly emits ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					" which accelerates the spread of ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nDuplicants will also be ",
					UI.FormatAsLink("Stressed", "STRESS"),
					" by walking through Polluted Water, so I should have my Duplicants clean up spills by ",
					UI.CLICK(UI.ClickType.clicking),
					" and dragging the ",
					UI.FormatAsTool("Mop Tool", global::Action.Mop)
				});

				// Token: 0x0400E1EE RID: 57838
				public static LocString TOOLTIP = "Notes on handling polluted materials";
			}

			// Token: 0x020037D9 RID: 14297
			public class LOCOMOTIONMESSAGE
			{
				// Token: 0x0400E1EF RID: 57839
				public static LocString NAME = "Video: Duplicant Movement";

				// Token: 0x0400E1F0 RID: 57840
				public static LocString MESSAGEBODY = "Duplicants have limited jumping and climbing abilities. They can only climb two tiles high and cannot fit into spaces shorter than two tiles, or cross gaps wider than one tile. I should keep this in mind while placing errands.\n\nTo check if an errand I've placed is accessible, I can select a Duplicant and " + UI.CLICK(UI.ClickType.click) + " <b>Show Navigation</b> to view all areas within their reach.";

				// Token: 0x0400E1F1 RID: 57841
				public static LocString TOOLTIP = "Notes on my Duplicants' maneuverability";
			}

			// Token: 0x020037DA RID: 14298
			public class PRIORITIESMESSAGE
			{
				// Token: 0x0400E1F2 RID: 57842
				public static LocString NAME = "Tutorial: Errand Priorities";

				// Token: 0x0400E1F3 RID: 57843
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Duplicants will choose where to work based on the priority of the errands that I give them. I can open the ",
					UI.FormatAsManagementMenu("Priorities Screen", global::Action.ManagePriorities),
					" to set their ",
					UI.PRE_KEYWORD,
					"Duplicant Priorities",
					UI.PST_KEYWORD,
					", and the ",
					UI.FormatAsTool("Priority Tool", global::Action.Prioritize),
					" to fine tune ",
					UI.PRE_KEYWORD,
					"Building Priority",
					UI.PST_KEYWORD,
					". Many buildings will also let me change their Priority level when I select them."
				});

				// Token: 0x0400E1F4 RID: 57844
				public static LocString TOOLTIP = "Notes on my Duplicants' priorities";
			}

			// Token: 0x020037DB RID: 14299
			public class FETCHINGWATERMESSAGE
			{
				// Token: 0x0400E1F5 RID: 57845
				public static LocString NAME = "Tutorial: Fetching Water";

				// Token: 0x0400E1F6 RID: 57846
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"By building a ",
					UI.FormatAsLink("Pitcher Pump", "LIQUIDPUMPINGSTATION"),
					" from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5),
					" over a pool of liquid, my Duplicants will be able to bottle it up and manually deliver it wherever it needs to go."
				});

				// Token: 0x0400E1F7 RID: 57847
				public static LocString TOOLTIP = "Notes on liquid resource gathering";
			}

			// Token: 0x020037DC RID: 14300
			public class SCHEDULEMESSAGE
			{
				// Token: 0x0400E1F8 RID: 57848
				public static LocString NAME = "Tutorial: Scheduling";

				// Token: 0x0400E1F9 RID: 57849
				public static LocString MESSAGEBODY = "My Duplicants will only eat, sleep, work, or bathe during the times I allot for such activities.\n\nTo make the best use of their time, I can open the " + UI.FormatAsManagementMenu("Schedule Tab", global::Action.ManageSchedule) + " to adjust the colony's schedule and plan how they should utilize their day.";

				// Token: 0x0400E1FA RID: 57850
				public static LocString TOOLTIP = "Notes on scheduling my Duplicants' time";
			}

			// Token: 0x020037DD RID: 14301
			public class THERMALCOMFORT
			{
				// Token: 0x0400E1FB RID: 57851
				public static LocString NAME = "Tutorial: Duplicant Temperature";

				// Token: 0x0400E1FC RID: 57852
				public static LocString TOOLTIP = "Notes on helping Duplicants keep their cool";

				// Token: 0x0400E1FD RID: 57853
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Environments that are extremely ",
					UI.FormatAsLink("Hot", "HEAT"),
					" or ",
					UI.FormatAsLink("Cold", "HEAT"),
					" affect my Duplicants' internal body temperature and cause undue ",
					UI.FormatAsLink("Stress", "STRESS"),
					" or unscheduled naps.\n\nOpening the ",
					UI.FormatAsOverlay("Temperature Overlay", global::Action.Overlay3),
					" and checking the <b>Thermal Tolerance</b> box allows me to view all areas where my Duplicants will feel discomfort and be unable to regulate their internal body temperature.\n\nRelated ",
					UI.FormatAsLink("Video: Insulation", "VIDEOS17")
				});
			}

			// Token: 0x020037DE RID: 14302
			public class TUTORIAL_OVERHEATING
			{
				// Token: 0x0400E1FE RID: 57854
				public static LocString NAME = "Tutorial: Building Temperature";

				// Token: 0x0400E1FF RID: 57855
				public static LocString TOOLTIP = "Notes on preventing building from breaking";

				// Token: 0x0400E200 RID: 57856
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"When constructing buildings, I should always take note of their ",
					UI.FormatAsLink("Overheat Temperature", "HEAT"),
					" and plan their locations accordingly. Maintaining low ambient temperatures and good ventilation in the colony will also help keep building temperatures down.\n\nThe <b>Relative Temperature</b> slider tool in the ",
					UI.FormatAsOverlay("Temperature Overlay", global::Action.Overlay3),
					" allows me to change adjust the overlay's color-coding in order to highlight specific temperature ranges.\n\nIf I allow buildings to exceed their Overheat Temperature they will begin to take damage, and if left unattended, they will break down and be unusable until repaired."
				});
			}

			// Token: 0x020037DF RID: 14303
			public class LOTS_OF_GERMS
			{
				// Token: 0x0400E201 RID: 57857
				public static LocString NAME = "Tutorial: Germs and Disease";

				// Token: 0x0400E202 RID: 57858
				public static LocString TOOLTIP = "Notes on Duplicant disease risks";

				// Token: 0x0400E203 RID: 57859
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Germs", "DISEASE"),
					" such as ",
					UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
					" and ",
					UI.FormatAsLink("Slimelung", "SLIMESICKNESS"),
					" can cause ",
					UI.FormatAsLink("Disease", "DISEASE"),
					" in my Duplicants. I can use the ",
					UI.FormatAsOverlay("Germ Overlay", global::Action.Overlay9),
					" to view all germ concentrations in my colony, and even detect the sources spawning them.\n\nBuilding Wash Basins from the ",
					UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8),
					" near colony toilets will tell my Duplicants they need to wash up.\n\nRelated ",
					UI.FormatAsLink("Video: Plumbing and Ventilation", "VIDEOS18")
				});
			}

			// Token: 0x020037E0 RID: 14304
			public class BEING_INFECTED
			{
				// Token: 0x0400E204 RID: 57860
				public static LocString NAME = "Tutorial: Immune Systems";

				// Token: 0x0400E205 RID: 57861
				public static LocString TOOLTIP = "Notes on keeping Duplicants in peak health";

				// Token: 0x0400E206 RID: 57862
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"When Duplicants come into contact with various ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", they'll need to expend points of ",
					UI.FormatAsLink("Immunity", "IMMUNE SYSTEM"),
					" to resist them and remain healthy. If repeated exposes causes their Immunity to drop to 0%, they'll be unable to resist germs and will contract the next disease they encounter.\n\nDoors with Access Permissions can be built from the BASE TAB<color=#F44A47> <b>[1]</b></color> of the ",
					UI.FormatAsLink("Build menu", "misc"),
					" to block Duplicants from entering biohazardous areas while they recover their spent immunity points."
				});
			}

			// Token: 0x020037E1 RID: 14305
			public class DISEASE_COOKING
			{
				// Token: 0x0400E207 RID: 57863
				public static LocString NAME = "Tutorial: Food Safety";

				// Token: 0x0400E208 RID: 57864
				public static LocString TOOLTIP = "Notes on managing food contamination";

				// Token: 0x0400E209 RID: 57865
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"The ",
					UI.FormatAsLink("Food", "FOOD"),
					" my Duplicants cook will only ever be as clean as the ingredients used to make it. Storing food in sterile or ",
					UI.FormatAsLink("Refrigerated", "REFRIGERATOR"),
					" environments will keep food free of ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", while carefully placed hygiene stations like ",
					BUILDINGS.PREFABS.WASHBASIN.NAME,
					" or ",
					BUILDINGS.PREFABS.SHOWER.NAME,
					" will prevent the cooks from infecting the food by handling it.\n\nDangerously contaminated food can be sent to compost by ",
					UI.CLICK(UI.ClickType.clicking),
					" the <b>Compost</b> button on the selected item."
				});
			}

			// Token: 0x020037E2 RID: 14306
			public class SUITS
			{
				// Token: 0x0400E20A RID: 57866
				public static LocString NAME = "Tutorial: Atmo Suits";

				// Token: 0x0400E20B RID: 57867
				public static LocString TOOLTIP = "Notes on using atmo suits";

				// Token: 0x0400E20C RID: 57868
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					" can be equipped to protect my Duplicants from environmental hazards like extreme ",
					UI.FormatAsLink("Heat", "Heat"),
					", airborne ",
					UI.FormatAsLink("Germs", "DISEASE"),
					", or unbreathable ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					". In order to utilize these suits, I'll need to hook up an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					" to an ",
					UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER"),
					" , then store one of the suits inside.\n\nDuplicants will equip a suit when they walk past the checkpoint in the chosen direction, and will unequip their suit when walking back the opposite way."
				});
			}

			// Token: 0x020037E3 RID: 14307
			public class RADIATION
			{
				// Token: 0x0400E20D RID: 57869
				public static LocString NAME = "Tutorial: Radiation";

				// Token: 0x0400E20E RID: 57870
				public static LocString TOOLTIP = "Notes on managing radiation";

				// Token: 0x0400E20F RID: 57871
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Objects such as ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					" and ",
					UI.FormatAsLink("Beeta Hives", "BEE"),
					" emit a ",
					UI.FormatAsLink("Radioactive", "RADIOACTIVE"),
					" energy that can be toxic to my Duplicants.\n\nI can use the ",
					UI.FormatAsOverlay("Radiation Overlay"),
					" ",
					UI.FormatAsHotKey(global::Action.Overlay15),
					" to check the scope of the Radiation field. Building thick walls around radiation emitters will dampen the field and protect my Duplicants from getting ",
					UI.FormatAsLink("Radiation Sickness", "RADIATIONSICKNESS"),
					" ."
				});
			}

			// Token: 0x020037E4 RID: 14308
			public class SPACETRAVEL
			{
				// Token: 0x0400E210 RID: 57872
				public static LocString NAME = "Tutorial: Space Travel";

				// Token: 0x0400E211 RID: 57873
				public static LocString TOOLTIP = "Notes on traveling in space";

				// Token: 0x0400E212 RID: 57874
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Building a rocket first requires constructing a ",
					UI.FormatAsLink("Rocket Platform", "LAUNCHPAD"),
					" and adding modules from the menu. All components of the Rocket Checklist will need to be complete before being capable of launching.\n\nA ",
					UI.FormatAsLink("Telescope", "CLUSTERTELESCOPE"),
					" needs to be built on the surface of a Planetoid in order to use the ",
					UI.PRE_KEYWORD,
					"Starmap Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageStarmap),
					" to see and set course for new destinations."
				});
			}

			// Token: 0x020037E5 RID: 14309
			public class MORALE
			{
				// Token: 0x0400E213 RID: 57875
				public static LocString NAME = "Video: Duplicant Morale";

				// Token: 0x0400E214 RID: 57876
				public static LocString TOOLTIP = "Notes on Duplicant expectations";

				// Token: 0x0400E215 RID: 57877
				public static LocString MESSAGEBODY = "Food, Rooms, Decor, and Recreation all have an effect on Duplicant Morale. Good experiences improve their Morale, while poor experiences lower it. When a Duplicant's Morale is below their Expectations, they will become Stressed.\n\nDuplicants' Expectations will get higher as they are given new Skills, and the colony will have to be improved to keep up their Morale. An overview of Morale and Stress can be viewed on the Vitals screen.\n\nRelated " + UI.FormatAsLink("Tutorial: Stress Management", "MISCELLANEOUSTIPS");
			}

			// Token: 0x020037E6 RID: 14310
			public class POWER
			{
				// Token: 0x0400E216 RID: 57878
				public static LocString NAME = "Video: Power Circuits";

				// Token: 0x0400E217 RID: 57879
				public static LocString TOOLTIP = "Notes on managing electricity";

				// Token: 0x0400E218 RID: 57880
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Generators are considered \"Producers\" of Power, while the various buildings and machines in the colony are considered \"Consumers\". Each Consumer will pull a certain wattage from the power circuit it is connected to, which can be checked at any time by ",
					UI.CLICK(UI.ClickType.clicking),
					" the building and going to the Energy Tab.\n\nI can use the Power Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay2),
					" to quickly check the status of all my circuits. If the Consumers are taking more wattage than the Generators are creating, the Batteries will drain and there will be brownouts.\n\nAdditionally, if the Consumers are pulling more wattage through the Wires than the Wires can handle, they will overload and burn out. To correct both these situations, I will need to reorganize my Consumers onto separate circuits."
				});
			}

			// Token: 0x020037E7 RID: 14311
			public class BIONICBATTERY
			{
				// Token: 0x0400E219 RID: 57881
				public static LocString NAME = "Tutorial: Powering Bionics";

				// Token: 0x0400E21A RID: 57882
				public static LocString TOOLTIP = "Notes on Duplicant power bank needs";

				// Token: 0x0400E21B RID: 57883
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Bionic Duplicants require ",
					UI.FormatAsLink("Power Banks", "ELECTROBANK"),
					" to function. Bionic Duplicants who run out of ",
					UI.FormatAsLink("Power", "POWER"),
					" will become incapacitated and require another Duplicant to reboot them.\n\nBasic power banks can be made at the ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x020037E8 RID: 14312
			public class GUNKEDTOILET
			{
				// Token: 0x0400E21C RID: 57884
				public static LocString NAME = "Tutorial: Gunked Toilets";

				// Token: 0x0400E21D RID: 57885
				public static LocString TOOLTIP = "Notes on unclogging toilets";

				// Token: 0x0400E21E RID: 57886
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Bionic Duplicants can dump built-up ",
					UI.FormatAsLink("Gunk", "LIQUIDGUNK"),
					" into ",
					UI.FormatAsLink("Toilets", "REQUIREMENTCLASSTOILETTYPE"),
					" if no other options are available. This invariably clogs the plumbing, however, and must be removed before facilities can be used by other Duplicants.\n\nBuilding a ",
					UI.FormatAsLink("Gunk Extractor", "GUNKEMPTIER"),
					" from the ",
					UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5),
					" will ensure that Bionic Duplicants can dispose of their waste appropriately."
				});
			}

			// Token: 0x020037E9 RID: 14313
			public class SLIPPERYSURFACE
			{
				// Token: 0x0400E21F RID: 57887
				public static LocString NAME = "Tutorial: Wet Surfaces";

				// Token: 0x0400E220 RID: 57888
				public static LocString TOOLTIP = "Notes on slipping hazards";

				// Token: 0x0400E221 RID: 57889
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"My Duplicants may slip and fall on wet surfaces, and Duplicants with bionic systems can experience disruptive glitching.\n\nI can help my colony avoid undue ",
					UI.FormatAsLink("Stress", "STRESS"),
					" and potential injury by using the ",
					UI.FormatAsTool("Mop Tool", global::Action.Mop),
					" to clean up spills. Building ",
					UI.FormatAsLink("Toilets", "REQUIREMENTCLASSTOILETTYPE"),
					" and ",
					UI.FormatAsLink("Gunk Extractors", "GUNKEMPTIER"),
					" can help minimize the incidence of spills."
				});
			}

			// Token: 0x020037EA RID: 14314
			public class BIONICOIL
			{
				// Token: 0x0400E222 RID: 57890
				public static LocString NAME = "Tutorial: Oiling Bionics";

				// Token: 0x0400E223 RID: 57891
				public static LocString TOOLTIP = "Notes on keeping Bionics working efficiently";

				// Token: 0x0400E224 RID: 57892
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"Bionic Duplicants with insufficient ",
					UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL"),
					" will slow down significantly to avoid grinding their gears.\n\nI can keep them running smoothly by supplying ",
					UI.FormatAsLink("Gear Balm", "LUBRICATIONSTICK"),
					", or by building a ",
					UI.FormatAsLink("Lubrication Station", "OILCHANGER"),
					" from the ",
					UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8),
					"."
				});
			}

			// Token: 0x020037EB RID: 14315
			public class DIGGING
			{
				// Token: 0x0400E225 RID: 57893
				public static LocString NAME = "Video: Digging for Resources";

				// Token: 0x0400E226 RID: 57894
				public static LocString TOOLTIP = "Notes on buried riches";

				// Token: 0x0400E227 RID: 57895
				public static LocString MESSAGEBODY = "Everything a colony needs to get going is found in the ground. Instructing Duplicants to dig out areas means we can find food, mine resources to build infrastructure, and clear space for the colony to grow. I can access the Dig Tool with " + UI.FormatAsHotKey(global::Action.Dig) + ", which allows me to select the area where I want my Duplicants to dig.\n\nDuplicants will need to gain the Superhard Digging skill to mine Abyssalite and the Superduperhard Digging skill to mine Diamond and Obsidian. Without the proper skills, these materials will be undiggable.";
			}

			// Token: 0x020037EC RID: 14316
			public class INSULATION
			{
				// Token: 0x0400E228 RID: 57896
				public static LocString NAME = "Video: Insulation";

				// Token: 0x0400E229 RID: 57897
				public static LocString TOOLTIP = "Notes on effective temperature management";

				// Token: 0x0400E22A RID: 57898
				public static LocString MESSAGEBODY = "The temperature of an environment can have positive or negative effects on the well-being of my Duplicants, as well as the plants and critters in my colony. Selecting " + UI.FormatAsHotKey(global::Action.Overlay3) + " will open the Temperature Overlay where I can check for any hot or cold spots.\n\nI can use a Utility building like an Ice-E Fan or a Space Heater to make an area colder or warmer. However, I will have limited success changing the temperature of a room unless I build the area with insulating tiles to prevent cold or warm air from escaping.";
			}

			// Token: 0x020037ED RID: 14317
			public class PLUMBING
			{
				// Token: 0x0400E22B RID: 57899
				public static LocString NAME = "Video: Plumbing and Ventilation";

				// Token: 0x0400E22C RID: 57900
				public static LocString TOOLTIP = "Notes on connecting buildings with pipes";

				// Token: 0x0400E22D RID: 57901
				public static LocString MESSAGEBODY = string.Concat(new string[]
				{
					"When connecting pipes for plumbing, it is useful to have the Plumbing Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay6),
					" selected. Each building which requires plumbing must have their Building Intake connected to the Output Pipe from a source such as a Liquid Pump. Liquid Pumps must be submerged in liquid and attached to a power source to function.\n\nBuildings often output contaminated water which must flow out of the building through piping from the Output Pipe. The water can then be expelled through a Liquid Vent, or filtered through a Water Sieve for reuse.\n\nVentilation applies the same principles to gases. Select the Ventilation Overlay ",
					UI.FormatAsHotKey(global::Action.Overlay7),
					" to see how gases are being moved around the colony."
				});
			}

			// Token: 0x020037EE RID: 14318
			public class NEW_AUTOMATION_WARNING
			{
				// Token: 0x0400E22E RID: 57902
				public static LocString NAME = "New Automation Port";

				// Token: 0x0400E22F RID: 57903
				public static LocString TOOLTIP = "This building has a new automation port and is unintentionally connected to an existing " + BUILDINGS.PREFABS.LOGICWIRE.NAME;
			}

			// Token: 0x020037EF RID: 14319
			public class DTU
			{
				// Token: 0x0400E230 RID: 57904
				public static LocString NAME = "Tutorial: Duplicant Thermal Units";

				// Token: 0x0400E231 RID: 57905
				public static LocString TOOLTIP = "Notes on measuring heat energy";

				// Token: 0x0400E232 RID: 57906
				public static LocString MESSAGEBODY = "My Duplicants measure heat energy in Duplicant Thermal Units or DTU.\n\n1 DTU = 1055.06 J";
			}

			// Token: 0x020037F0 RID: 14320
			public class NOMESSAGES
			{
				// Token: 0x0400E233 RID: 57907
				public static LocString NAME = "";

				// Token: 0x0400E234 RID: 57908
				public static LocString TOOLTIP = "";
			}

			// Token: 0x020037F1 RID: 14321
			public class NOALERTS
			{
				// Token: 0x0400E235 RID: 57909
				public static LocString NAME = "";

				// Token: 0x0400E236 RID: 57910
				public static LocString TOOLTIP = "";
			}

			// Token: 0x020037F2 RID: 14322
			public class NEWTRAIT
			{
				// Token: 0x0400E237 RID: 57911
				public static LocString NAME = "{0} has developed a trait";

				// Token: 0x0400E238 RID: 57912
				public static LocString TOOLTIP = "{0} has developed the trait(s):\n    • {1}";
			}

			// Token: 0x020037F3 RID: 14323
			public class RESEARCHCOMPLETE
			{
				// Token: 0x0400E239 RID: 57913
				public static LocString NAME = "Research Complete";

				// Token: 0x0400E23A RID: 57914
				public static LocString MESSAGEBODY = "Eureka! We've discovered {0} Technology.\n\nNew buildings have become available:\n  • {1}";

				// Token: 0x0400E23B RID: 57915
				public static LocString TOOLTIP = "{0} research complete!";
			}

			// Token: 0x020037F4 RID: 14324
			public class WORLDDETECTED
			{
				// Token: 0x0400E23C RID: 57916
				public static LocString NAME = "New " + UI.CLUSTERMAP.PLANETOID + " detected";

				// Token: 0x0400E23D RID: 57917
				public static LocString MESSAGEBODY = "My Duplicants' astronomical efforts have uncovered a new " + UI.CLUSTERMAP.PLANETOID + ":\n{0}";

				// Token: 0x0400E23E RID: 57918
				public static LocString TOOLTIP = "{0} discovered";
			}

			// Token: 0x020037F5 RID: 14325
			public class SKILL_POINT_EARNED
			{
				// Token: 0x0400E23F RID: 57919
				public static LocString NAME = "{Duplicant} earned a skill point!";

				// Token: 0x0400E240 RID: 57920
				public static LocString MESSAGEBODY = "These Duplicants have Skill Points that can be spent on new abilities:\n{0}";

				// Token: 0x0400E241 RID: 57921
				public static LocString LINE = "\n• <b>{0}</b>";

				// Token: 0x0400E242 RID: 57922
				public static LocString TOOLTIP = "{Duplicant} has been working hard and is ready to learn a new skill";
			}

			// Token: 0x020037F6 RID: 14326
			public class DUPLICANTABSORBED
			{
				// Token: 0x0400E243 RID: 57923
				public static LocString NAME = "Printables have been reabsorbed";

				// Token: 0x0400E244 RID: 57924
				public static LocString MESSAGEBODY = "The Printing Pod is no longer available for printing.\nCountdown to the next production has been rebooted.";

				// Token: 0x0400E245 RID: 57925
				public static LocString TOOLTIP = "Printing countdown rebooted";
			}

			// Token: 0x020037F7 RID: 14327
			public class DUPLICANTDIED
			{
				// Token: 0x0400E246 RID: 57926
				public static LocString NAME = "Duplicants have died";

				// Token: 0x0400E247 RID: 57927
				public static LocString TOOLTIP = "These Duplicants have died:";
			}

			// Token: 0x020037F8 RID: 14328
			public class FOODROT
			{
				// Token: 0x0400E248 RID: 57928
				public static LocString NAME = "Food has decayed";

				// Token: 0x0400E249 RID: 57929
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items have rotted and are no longer edible:{0}";
			}

			// Token: 0x020037F9 RID: 14329
			public class FOODSTALE
			{
				// Token: 0x0400E24A RID: 57930
				public static LocString NAME = "Food has become stale";

				// Token: 0x0400E24B RID: 57931
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items have become stale and could rot if not stored:";
			}

			// Token: 0x020037FA RID: 14330
			public class YELLOWALERT
			{
				// Token: 0x0400E24C RID: 57932
				public static LocString NAME = "Yellow Alert";

				// Token: 0x0400E24D RID: 57933
				public static LocString TOOLTIP = "The colony has some top priority tasks to complete before resuming a normal schedule";
			}

			// Token: 0x020037FB RID: 14331
			public class REDALERT
			{
				// Token: 0x0400E24E RID: 57934
				public static LocString NAME = "Red Alert";

				// Token: 0x0400E24F RID: 57935
				public static LocString TOOLTIP = "The colony is prioritizing work over their individual well-being";
			}

			// Token: 0x020037FC RID: 14332
			public class REACTORMELTDOWN
			{
				// Token: 0x0400E250 RID: 57936
				public static LocString NAME = "Reactor Meltdown";

				// Token: 0x0400E251 RID: 57937
				public static LocString TOOLTIP = "A Research Reactor has overheated and is melting down! Extreme radiation is flooding the area";
			}

			// Token: 0x020037FD RID: 14333
			public class HEALING
			{
				// Token: 0x0400E252 RID: 57938
				public static LocString NAME = "Healing";

				// Token: 0x0400E253 RID: 57939
				public static LocString TOOLTIP = "This Duplicant is recovering from an injury";
			}

			// Token: 0x020037FE RID: 14334
			public class UNREACHABLEITEM
			{
				// Token: 0x0400E254 RID: 57940
				public static LocString NAME = "Unreachable resources";

				// Token: 0x0400E255 RID: 57941
				public static LocString TOOLTIP = "Duplicants cannot retrieve these resources:";
			}

			// Token: 0x020037FF RID: 14335
			public class INVALIDCONSTRUCTIONLOCATION
			{
				// Token: 0x0400E256 RID: 57942
				public static LocString NAME = "Invalid construction location";

				// Token: 0x0400E257 RID: 57943
				public static LocString TOOLTIP = "These buildings cannot be constructed in the planned areas:";
			}

			// Token: 0x02003800 RID: 14336
			public class MISSINGMATERIALS
			{
				// Token: 0x0400E258 RID: 57944
				public static LocString NAME = "Missing materials";

				// Token: 0x0400E259 RID: 57945
				public static LocString TOOLTIP = "These resources are not available:";
			}

			// Token: 0x02003801 RID: 14337
			public class BUILDINGOVERHEATED
			{
				// Token: 0x0400E25A RID: 57946
				public static LocString NAME = "Damage: Overheated";

				// Token: 0x0400E25B RID: 57947
				public static LocString TOOLTIP = "Extreme heat is damaging these buildings:";
			}

			// Token: 0x02003802 RID: 14338
			public class TILECOLLAPSE
			{
				// Token: 0x0400E25C RID: 57948
				public static LocString NAME = "Ceiling Collapse!";

				// Token: 0x0400E25D RID: 57949
				public static LocString TOOLTIP = "Falling material fell on these Duplicants and displaced them:";
			}

			// Token: 0x02003803 RID: 14339
			public class NO_OXYGEN_GENERATOR
			{
				// Token: 0x0400E25E RID: 57950
				public static LocString NAME = "No " + UI.FormatAsLink("Oxygen Generator", "OXYGEN") + " built";

				// Token: 0x0400E25F RID: 57951
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"My colony is not producing any new ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					"\n\n",
					UI.FormatAsLink("Oxygen Diffusers", "MINERALDEOXIDIZER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Oxygen Tab", global::Action.Plan2)
				});
			}

			// Token: 0x02003804 RID: 14340
			public class INSUFFICIENTOXYGENLASTCYCLE
			{
				// Token: 0x0400E260 RID: 57952
				public static LocString NAME = "Insufficient Oxygen generation";

				// Token: 0x0400E261 RID: 57953
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"My colony is consuming more ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" than it is producing, and will run out air if I do not increase production.\n\nI should check my existing oxygen production buildings to ensure they're operating correctly\n\n• ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" produced last cycle: {EmittingRate}\n• Consumed last cycle: {ConsumptionRate}"
				});
			}

			// Token: 0x02003805 RID: 14341
			public class UNREFRIGERATEDFOOD
			{
				// Token: 0x0400E262 RID: 57954
				public static LocString NAME = "Unrefrigerated Food";

				// Token: 0x0400E263 RID: 57955
				public static LocString TOOLTIP = "These " + UI.FormatAsLink("Food", "FOOD") + " items are stored but not refrigerated:\n";
			}

			// Token: 0x02003806 RID: 14342
			public class FOODLOW
			{
				// Token: 0x0400E264 RID: 57956
				public static LocString NAME = "Food shortage";

				// Token: 0x0400E265 RID: 57957
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The colony's ",
					UI.FormatAsLink("Food", "FOOD"),
					" reserves are low:\n\n    • {0} are currently available\n    • {1} is being consumed per cycle\n\n",
					UI.FormatAsLink("Microbe Mushers", "MICROBEMUSHER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Food Tab", global::Action.Plan4)
				});
			}

			// Token: 0x02003807 RID: 14343
			public class NO_MEDICAL_COTS
			{
				// Token: 0x0400E266 RID: 57958
				public static LocString NAME = "No " + UI.FormatAsLink("Sick Bay", "DOCTORSTATION") + " built";

				// Token: 0x0400E267 RID: 57959
				public static LocString TOOLTIP = "There is nowhere for sick Duplicants receive medical care\n\n" + UI.FormatAsLink("Sick Bays", "DOCTORSTATION") + " can be built from the " + UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8);
			}

			// Token: 0x02003808 RID: 14344
			public class NEEDTOILET
			{
				// Token: 0x0400E268 RID: 57960
				public static LocString NAME = "No " + UI.FormatAsLink("Outhouse", "OUTHOUSE") + " built";

				// Token: 0x0400E269 RID: 57961
				public static LocString TOOLTIP = "My Duplicants have nowhere to relieve themselves\n\n" + UI.FormatAsLink("Outhouses", "OUTHOUSE") + " can be built from the " + UI.FormatAsBuildMenuTab("Plumbing Tab", global::Action.Plan5);
			}

			// Token: 0x02003809 RID: 14345
			public class NEEDFOOD
			{
				// Token: 0x0400E26A RID: 57962
				public static LocString NAME = "Colony requires a food source";

				// Token: 0x0400E26B RID: 57963
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The colony will exhaust their supplies without a new ",
					UI.FormatAsLink("Food", "FOOD"),
					" source\n\n",
					UI.FormatAsLink("Microbe Mushers", "MICROBEMUSHER"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Food Tab", global::Action.Plan4)
				});
			}

			// Token: 0x0200380A RID: 14346
			public class HYGENE_NEEDED
			{
				// Token: 0x0400E26C RID: 57964
				public static LocString NAME = "No " + UI.FormatAsLink("Wash Basin", "WASHBASIN") + " built";

				// Token: 0x0400E26D RID: 57965
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.FormatAsLink("Germs", "DISEASE"),
					" are spreading in the colony because my Duplicants have nowhere to clean up\n\n",
					UI.FormatAsLink("Wash Basins", "WASHBASIN"),
					" can be built from the ",
					UI.FormatAsBuildMenuTab("Medicine Tab", global::Action.Plan8)
				});
			}

			// Token: 0x0200380B RID: 14347
			public class NEEDSLEEP
			{
				// Token: 0x0400E26E RID: 57966
				public static LocString NAME = "No " + UI.FormatAsLink("Cots", "BED") + " built";

				// Token: 0x0400E26F RID: 57967
				public static LocString TOOLTIP = "My Duplicants would appreciate a place to sleep\n\n" + UI.FormatAsLink("Cots", "BED") + " can be built from the " + UI.FormatAsBuildMenuTab("Furniture Tab", global::Action.Plan9);
			}

			// Token: 0x0200380C RID: 14348
			public class NEEDENERGYSOURCE
			{
				// Token: 0x0400E270 RID: 57968
				public static LocString NAME = "Colony requires a " + UI.FormatAsLink("Power", "POWER") + " source";

				// Token: 0x0400E271 RID: 57969
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					UI.FormatAsLink("Power", "POWER"),
					" is required to operate electrical buildings\n\n",
					UI.FormatAsLink("Manual Generators", "MANUALGENERATOR"),
					" and ",
					UI.FormatAsLink("Wire", "WIRE"),
					" can be built from the ",
					UI.FormatAsLink("Power Tab", "[3]")
				});
			}

			// Token: 0x0200380D RID: 14349
			public class RESOURCEMELTED
			{
				// Token: 0x0400E272 RID: 57970
				public static LocString NAME = "Resources melted";

				// Token: 0x0400E273 RID: 57971
				public static LocString TOOLTIP = "These resources have melted:";
			}

			// Token: 0x0200380E RID: 14350
			public class VENTOVERPRESSURE
			{
				// Token: 0x0400E274 RID: 57972
				public static LocString NAME = "Vent overpressurized";

				// Token: 0x0400E275 RID: 57973
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" systems have exited the ideal ",
					UI.PRE_KEYWORD,
					"Pressure",
					UI.PST_KEYWORD,
					" range:"
				});
			}

			// Token: 0x0200380F RID: 14351
			public class VENTBLOCKED
			{
				// Token: 0x0400E276 RID: 57974
				public static LocString NAME = "Vent blocked";

				// Token: 0x0400E277 RID: 57975
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Blocked ",
					UI.PRE_KEYWORD,
					"Pipes",
					UI.PST_KEYWORD,
					" have stopped these systems from functioning:"
				});
			}

			// Token: 0x02003810 RID: 14352
			public class OUTPUTBLOCKED
			{
				// Token: 0x0400E278 RID: 57976
				public static LocString NAME = "Output blocked";

				// Token: 0x0400E279 RID: 57977
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Blocked ",
					UI.PRE_KEYWORD,
					"Pipes",
					UI.PST_KEYWORD,
					" have stopped these systems from functioning:"
				});
			}

			// Token: 0x02003811 RID: 14353
			public class BROKENMACHINE
			{
				// Token: 0x0400E27A RID: 57978
				public static LocString NAME = "Building broken";

				// Token: 0x0400E27B RID: 57979
				public static LocString TOOLTIP = "These buildings have taken significant damage and are nonfunctional:";
			}

			// Token: 0x02003812 RID: 14354
			public class STRUCTURALDAMAGE
			{
				// Token: 0x0400E27C RID: 57980
				public static LocString NAME = "Structural damage";

				// Token: 0x0400E27D RID: 57981
				public static LocString TOOLTIP = "These buildings' structural integrity has been compromised";
			}

			// Token: 0x02003813 RID: 14355
			public class STRUCTURALCOLLAPSE
			{
				// Token: 0x0400E27E RID: 57982
				public static LocString NAME = "Structural collapse";

				// Token: 0x0400E27F RID: 57983
				public static LocString TOOLTIP = "These buildings have collapsed:";
			}

			// Token: 0x02003814 RID: 14356
			public class GASCLOUDWARNING
			{
				// Token: 0x0400E280 RID: 57984
				public static LocString NAME = "A gas cloud approaches";

				// Token: 0x0400E281 RID: 57985
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A toxic ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" cloud will soon envelop the colony"
				});
			}

			// Token: 0x02003815 RID: 14357
			public class GASCLOUDARRIVING
			{
				// Token: 0x0400E282 RID: 57986
				public static LocString NAME = "The colony is entering a cloud of gas";

				// Token: 0x0400E283 RID: 57987
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02003816 RID: 14358
			public class GASCLOUDPEAK
			{
				// Token: 0x0400E284 RID: 57988
				public static LocString NAME = "The gas cloud is at its densest point";

				// Token: 0x0400E285 RID: 57989
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02003817 RID: 14359
			public class GASCLOUDDEPARTING
			{
				// Token: 0x0400E286 RID: 57990
				public static LocString NAME = "The gas cloud is receding";

				// Token: 0x0400E287 RID: 57991
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02003818 RID: 14360
			public class GASCLOUDGONE
			{
				// Token: 0x0400E288 RID: 57992
				public static LocString NAME = "The colony is once again in open space";

				// Token: 0x0400E289 RID: 57993
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02003819 RID: 14361
			public class AVAILABLE
			{
				// Token: 0x0400E28A RID: 57994
				public static LocString NAME = "Resource available";

				// Token: 0x0400E28B RID: 57995
				public static LocString TOOLTIP = "These resources have become available:";
			}

			// Token: 0x0200381A RID: 14362
			public class ALLOCATED
			{
				// Token: 0x0400E28C RID: 57996
				public static LocString NAME = "Resource allocated";

				// Token: 0x0400E28D RID: 57997
				public static LocString TOOLTIP = "These resources are reserved for a planned building:";
			}

			// Token: 0x0200381B RID: 14363
			public class INCREASEDEXPECTATIONS
			{
				// Token: 0x0400E28E RID: 57998
				public static LocString NAME = "Duplicants' expectations increased";

				// Token: 0x0400E28F RID: 57999
				public static LocString TOOLTIP = "Duplicants require better amenities over time.\nThese Duplicants have increased their expectations:";
			}

			// Token: 0x0200381C RID: 14364
			public class NEARLYDRY
			{
				// Token: 0x0400E290 RID: 58000
				public static LocString NAME = "Nearly dry";

				// Token: 0x0400E291 RID: 58001
				public static LocString TOOLTIP = "These Duplicants will dry off soon:";
			}

			// Token: 0x0200381D RID: 14365
			public class IMMIGRANTSLEFT
			{
				// Token: 0x0400E292 RID: 58002
				public static LocString NAME = "Printables have been reabsorbed";

				// Token: 0x0400E293 RID: 58003
				public static LocString TOOLTIP = "The care packages have been disintegrated and printable Duplicants have been Oozed";
			}

			// Token: 0x0200381E RID: 14366
			public class LEVELUP
			{
				// Token: 0x0400E294 RID: 58004
				public static LocString NAME = "Attribute increase";

				// Token: 0x0400E295 RID: 58005
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants' ",
					UI.PRE_KEYWORD,
					"Attributes",
					UI.PST_KEYWORD,
					" have improved:"
				});

				// Token: 0x0400E296 RID: 58006
				public static LocString SUFFIX = " - {0} Skill Level modifier raised to +{1}";
			}

			// Token: 0x0200381F RID: 14367
			public class RESETSKILL
			{
				// Token: 0x0400E297 RID: 58007
				public static LocString NAME = "Skills reset";

				// Token: 0x0400E298 RID: 58008
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have had their ",
					UI.PRE_KEYWORD,
					"Skill Points",
					UI.PST_KEYWORD,
					" refunded:"
				});
			}

			// Token: 0x02003820 RID: 14368
			public class BADROCKETPATH
			{
				// Token: 0x0400E299 RID: 58009
				public static LocString NAME = "Flight Path Obstructed";

				// Token: 0x0400E29A RID: 58010
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A rocket's flight path has been interrupted by a new astronomical discovery.\nOpen the ",
					UI.PRE_KEYWORD,
					"Starmap Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageStarmap),
					" to reassign rocket paths"
				});
			}

			// Token: 0x02003821 RID: 14369
			public class SCHEDULE_CHANGED
			{
				// Token: 0x0400E29B RID: 58011
				public static LocString NAME = "{0}: {1}!";

				// Token: 0x0400E29C RID: 58012
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Duplicants assigned to ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					" have started their <b>{1}</b> block.\n\n{2}\n\nOpen the ",
					UI.PRE_KEYWORD,
					"Schedule Screen",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ManageSchedule),
					" to change blocks or assignments"
				});
			}

			// Token: 0x02003822 RID: 14370
			public class GENESHUFFLER
			{
				// Token: 0x0400E29D RID: 58013
				public static LocString NAME = "Genes Shuffled";

				// Token: 0x0400E29E RID: 58014
				public static LocString TOOLTIP = "These Duplicants had their genetic makeup modified:";

				// Token: 0x0400E29F RID: 58015
				public static LocString SUFFIX = " has developed " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x02003823 RID: 14371
			public class HEALINGTRAITGAIN
			{
				// Token: 0x0400E2A0 RID: 58016
				public static LocString NAME = "New trait";

				// Token: 0x0400E2A1 RID: 58017
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants' injuries weren't set and healed improperly.\nThey developed ",
					UI.PRE_KEYWORD,
					"Traits",
					UI.PST_KEYWORD,
					" as a result:"
				});

				// Token: 0x0400E2A2 RID: 58018
				public static LocString SUFFIX = " has developed " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x02003824 RID: 14372
			public class COLONYLOST
			{
				// Token: 0x0400E2A3 RID: 58019
				public static LocString NAME = "Colony Lost";

				// Token: 0x0400E2A4 RID: 58020
				public static LocString TOOLTIP = "All Duplicants are dead or incapacitated";
			}

			// Token: 0x02003825 RID: 14373
			public class FABRICATOREMPTY
			{
				// Token: 0x0400E2A5 RID: 58021
				public static LocString NAME = "Fabricator idle";

				// Token: 0x0400E2A6 RID: 58022
				public static LocString TOOLTIP = "These fabricators have no recipes queued:";
			}

			// Token: 0x02003826 RID: 14374
			public class BUILDING_MELTED
			{
				// Token: 0x0400E2A7 RID: 58023
				public static LocString NAME = "Building melted";

				// Token: 0x0400E2A8 RID: 58024
				public static LocString TOOLTIP = "Extreme heat has melted these buildings:";
			}

			// Token: 0x02003827 RID: 14375
			public class LARGE_IMPACTOR_GEYSER_ERUPTION
			{
				// Token: 0x0400E2A9 RID: 58025
				public static LocString NAME = "Geyser triggered";

				// Token: 0x0400E2AA RID: 58026
				public static LocString TOOLTIP = "Demolior's impact has triggered the eruption of a natural vent on this world";
			}

			// Token: 0x02003828 RID: 14376
			public class LARGE_IMPACTOR_KEEPSAKE
			{
				// Token: 0x0400E2AB RID: 58027
				public static LocString NAME = "Stereoscope Found";

				// Token: 0x0400E2AC RID: 58028
				public static LocString TOOLTIP = "A stereoscope artifact has fallen from space";
			}

			// Token: 0x02003829 RID: 14377
			public class SUIT_DROPPED
			{
				// Token: 0x0400E2AD RID: 58029
				public static LocString NAME = "No Docks available";

				// Token: 0x0400E2AE RID: 58030
				public static LocString TOOLTIP = "An exosuit was dropped because there were no empty docks available";
			}

			// Token: 0x0200382A RID: 14378
			public class DEATH_SUFFOCATION
			{
				// Token: 0x0400E2AF RID: 58031
				public static LocString NAME = "Duplicants suffocated";

				// Token: 0x0400E2B0 RID: 58032
				public static LocString TOOLTIP = "These Duplicants died from a lack of " + ELEMENTS.OXYGEN.NAME + ":";
			}

			// Token: 0x0200382B RID: 14379
			public class DEATH_FROZENSOLID
			{
				// Token: 0x0400E2B1 RID: 58033
				public static LocString NAME = "Duplicants have frozen";

				// Token: 0x0400E2B2 RID: 58034
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from extremely low ",
					UI.PRE_KEYWORD,
					"Temperatures",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x0200382C RID: 14380
			public class DEATH_OVERHEATING
			{
				// Token: 0x0400E2B3 RID: 58035
				public static LocString NAME = "Duplicants have overheated";

				// Token: 0x0400E2B4 RID: 58036
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from extreme ",
					UI.PRE_KEYWORD,
					"Heat",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x0200382D RID: 14381
			public class DEATH_STARVATION
			{
				// Token: 0x0400E2B5 RID: 58037
				public static LocString NAME = "Duplicants have starved";

				// Token: 0x0400E2B6 RID: 58038
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants died from a lack of ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x0200382E RID: 14382
			public class DEATH_FELL
			{
				// Token: 0x0400E2B7 RID: 58039
				public static LocString NAME = "Duplicants splattered";

				// Token: 0x0400E2B8 RID: 58040
				public static LocString TOOLTIP = "These Duplicants fell to their deaths:";
			}

			// Token: 0x0200382F RID: 14383
			public class DEATH_CRUSHED
			{
				// Token: 0x0400E2B9 RID: 58041
				public static LocString NAME = "Duplicants crushed";

				// Token: 0x0400E2BA RID: 58042
				public static LocString TOOLTIP = "These Duplicants have been crushed:";
			}

			// Token: 0x02003830 RID: 14384
			public class DEATH_SUFFOCATEDTANKEMPTY
			{
				// Token: 0x0400E2BB RID: 58043
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400E2BC RID: 58044
				public static LocString TOOLTIP = "These Duplicants were unable to reach " + UI.FormatAsLink("Oxygen", "OXYGEN") + " and died:";
			}

			// Token: 0x02003831 RID: 14385
			public class DEATH_SUFFOCATEDAIRTOOHOT
			{
				// Token: 0x0400E2BD RID: 58045
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400E2BE RID: 58046
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"These Duplicants have asphyxiated in ",
					UI.PRE_KEYWORD,
					"Hot",
					UI.PST_KEYWORD,
					" air:"
				});
			}

			// Token: 0x02003832 RID: 14386
			public class DEATH_SUFFOCATEDAIRTOOCOLD
			{
				// Token: 0x0400E2BF RID: 58047
				public static LocString NAME = "Duplicants have suffocated";

				// Token: 0x0400E2C0 RID: 58048
				public static LocString TOOLTIP = "These Duplicants have asphyxiated in " + UI.FormatAsLink("Cold", "HEAT") + " air:";
			}

			// Token: 0x02003833 RID: 14387
			public class DEATH_DROWNED
			{
				// Token: 0x0400E2C1 RID: 58049
				public static LocString NAME = "Duplicants have drowned";

				// Token: 0x0400E2C2 RID: 58050
				public static LocString TOOLTIP = "These Duplicants have drowned:";
			}

			// Token: 0x02003834 RID: 14388
			public class DEATH_ENTOUMBED
			{
				// Token: 0x0400E2C3 RID: 58051
				public static LocString NAME = "Duplicants have been entombed";

				// Token: 0x0400E2C4 RID: 58052
				public static LocString TOOLTIP = "These Duplicants are trapped and need assistance:";
			}

			// Token: 0x02003835 RID: 14389
			public class DEATH_RAPIDDECOMPRESSION
			{
				// Token: 0x0400E2C5 RID: 58053
				public static LocString NAME = "Duplicants pressurized";

				// Token: 0x0400E2C6 RID: 58054
				public static LocString TOOLTIP = "These Duplicants died in a low pressure environment:";
			}

			// Token: 0x02003836 RID: 14390
			public class DEATH_OVERPRESSURE
			{
				// Token: 0x0400E2C7 RID: 58055
				public static LocString NAME = "Duplicants pressurized";

				// Token: 0x0400E2C8 RID: 58056
				public static LocString TOOLTIP = "These Duplicants died in a high pressure environment:";
			}

			// Token: 0x02003837 RID: 14391
			public class DEATH_POISONED
			{
				// Token: 0x0400E2C9 RID: 58057
				public static LocString NAME = "Duplicants poisoned";

				// Token: 0x0400E2CA RID: 58058
				public static LocString TOOLTIP = "These Duplicants died as a result of poisoning:";
			}

			// Token: 0x02003838 RID: 14392
			public class DEATH_DISEASE
			{
				// Token: 0x0400E2CB RID: 58059
				public static LocString NAME = "Duplicants have succumbed to disease";

				// Token: 0x0400E2CC RID: 58060
				public static LocString TOOLTIP = "These Duplicants died from an untreated " + UI.FormatAsLink("Disease", "DISEASE") + ":";
			}

			// Token: 0x02003839 RID: 14393
			public class CIRCUIT_OVERLOADED
			{
				// Token: 0x0400E2CD RID: 58061
				public static LocString NAME = "Circuit Overloaded";

				// Token: 0x0400E2CE RID: 58062
				public static LocString TOOLTIP = "These " + BUILDINGS.PREFABS.WIRE.NAME + "s melted due to excessive current demands on their circuits";
			}

			// Token: 0x0200383A RID: 14394
			public class LOGIC_CIRCUIT_OVERLOADED
			{
				// Token: 0x0400E2CF RID: 58063
				public static LocString NAME = "Logic Circuit Overloaded";

				// Token: 0x0400E2D0 RID: 58064
				public static LocString TOOLTIP = "These " + BUILDINGS.PREFABS.LOGICWIRE.NAME + "s melted due to more bits of data being sent over them than they can support";
			}

			// Token: 0x0200383B RID: 14395
			public class DISCOVERED_SPACE
			{
				// Token: 0x0400E2D1 RID: 58065
				public static LocString NAME = "ALERT - Surface Breach";

				// Token: 0x0400E2D2 RID: 58066
				public static LocString TOOLTIP = "Amazing!\n\nMy Duplicants have managed to breach the surface of our rocky prison.\n\nI should be careful; the region is extremely inhospitable and I could easily lose resources to the vacuum of space.";
			}

			// Token: 0x0200383C RID: 14396
			public class COLONY_ACHIEVEMENT_EARNED
			{
				// Token: 0x0400E2D3 RID: 58067
				public static LocString NAME = "Colony Achievement earned";

				// Token: 0x0400E2D4 RID: 58068
				public static LocString TOOLTIP = "The colony has earned a new achievement.";
			}

			// Token: 0x0200383D RID: 14397
			public class WARP_PORTAL_DUPE_READY
			{
				// Token: 0x0400E2D5 RID: 58069
				public static LocString NAME = "Duplicant warp ready";

				// Token: 0x0400E2D6 RID: 58070
				public static LocString TOOLTIP = "{dupe} is ready to warp from the " + BUILDINGS.PREFABS.WARPPORTAL.NAME;
			}

			// Token: 0x0200383E RID: 14398
			public class GENETICANALYSISCOMPLETE
			{
				// Token: 0x0400E2D7 RID: 58071
				public static LocString NAME = "Seed Analysis Complete";

				// Token: 0x0400E2D8 RID: 58072
				public static LocString MESSAGEBODY = "Deeply probing the genes of the {Plant} plant have led to the discovery of a promising new cultivatable mutation:\n\n<b>{Subspecies}</b>\n\n{Info}";

				// Token: 0x0400E2D9 RID: 58073
				public static LocString TOOLTIP = "{Plant} Analysis complete!";
			}

			// Token: 0x0200383F RID: 14399
			public class NEWMUTANTSEED
			{
				// Token: 0x0400E2DA RID: 58074
				public static LocString NAME = "New Mutant Seed Discovered";

				// Token: 0x0400E2DB RID: 58075
				public static LocString TOOLTIP = "A new mutant variety of the {Plant} has been found. Analyze it at the " + BUILDINGS.PREFABS.GENETICANALYSISSTATION.NAME + " to learn more!";
			}

			// Token: 0x02003840 RID: 14400
			public class DUPLICANT_CRASH_LANDED
			{
				// Token: 0x0400E2DC RID: 58076
				public static LocString NAME = "Duplicant Crash Landed!";

				// Token: 0x0400E2DD RID: 58077
				public static LocString TOOLTIP = "A Duplicant has successfully crashed an Escape Pod onto the surface of a nearby Planetoid.";
			}

			// Token: 0x02003841 RID: 14401
			public class POIRESEARCHUNLOCKCOMPLETE
			{
				// Token: 0x0400E2DE RID: 58078
				public static LocString NAME = "Portal Unlocked!";

				// Token: 0x0400E2DF RID: 58079
				public static LocString MESSAGEBODY = "Eureka! We've decrypted the Research Portal's final transmission. New buildings have become available:\n  {0}\n\nOne file was labeled \"Open This First.\" New Database Entry unlocked.";

				// Token: 0x0400E2E0 RID: 58080
				public static LocString TOOLTIP = "{0} unlocked!";

				// Token: 0x0400E2E1 RID: 58081
				public static LocString BUTTON_VIEW_LORE = "View entry";
			}

			// Token: 0x02003842 RID: 14402
			public class POIRESEARCHUNLOCKCOMPLETE_NOLORE
			{
				// Token: 0x0400E2E2 RID: 58082
				public static LocString NAME = "Portal Unlocked!";

				// Token: 0x0400E2E3 RID: 58083
				public static LocString MESSAGEBODY = "Eureka! We've decrypted the Research Portal's final transmission. New buildings have become available:\n  {0}\n\n";

				// Token: 0x0400E2E4 RID: 58084
				public static LocString TOOLTIP = "{0} unlocked!";
			}

			// Token: 0x02003843 RID: 14403
			public class INCOMINGPREHISTORICASTEROIDNOTIFICATION
			{
				// Token: 0x0400E2E5 RID: 58085
				public static LocString NAME = "DEMOLIOR";

				// Token: 0x0400E2E6 RID: 58086
				public static LocString TOOLTIP = "Incoming Asteroid: <b><color=#ff1111>DEMOLIOR</color></b>\n• Health: {0}/{1}\n• Time until impact: {2}\n\nCollision damage can be avoided by destroying <b><color=#ff1111>DEMOLIOR</color></b> with " + UI.FormatAsLink("Intracosmic Blastshot", "LONGRANGEMISSILE") + " before it makes contact";

				// Token: 0x0400E2E7 RID: 58087
				public static LocString TOGGLE_TOOLTIP = "Click to toggle impact zone preview";
			}

			// Token: 0x02003844 RID: 14404
			public class LARGEIMPACTORREVEALSEQUENCE
			{
				// Token: 0x02003BCC RID: 15308
				public class RETICLE
				{
					// Token: 0x0400EBF0 RID: 60400
					public static LocString LARGE_IMPACTOR_NAME = "DEMOLIOR";

					// Token: 0x0400EBF1 RID: 60401
					public static LocString SIDE_PANEL_TITLE = "IMMINENT THREAT";

					// Token: 0x0400EBF2 RID: 60402
					public static LocString SIDE_PANEL_DESCRIPTION = "\n\nTIME UNTIL IMPACT: {0} CYCLES.";

					// Token: 0x0400EBF3 RID: 60403
					public static LocString CALCULATING_IMPACT_ZONE_TEXT = "CALCULATING IMPACT ZONE...";
				}
			}

			// Token: 0x02003845 RID: 14405
			public class BIONICRESEARCHUNLOCK
			{
				// Token: 0x0400E2E8 RID: 58088
				public static LocString NAME = "Research Discovered";

				// Token: 0x0400E2E9 RID: 58089
				public static LocString MESSAGEBODY = "My new Bionic Duplicant has built-in programming that they've shared with the colony.\n\nNew buildings have become available:\n  • {0}";

				// Token: 0x0400E2EA RID: 58090
				public static LocString TOOLTIP = "{0} research discovered!";
			}

			// Token: 0x02003846 RID: 14406
			public class BIONICLIQUIDDAMAGE
			{
				// Token: 0x0400E2EB RID: 58091
				public static LocString NAME = "Liquid Damage";

				// Token: 0x0400E2EC RID: 58092
				public static LocString TOOLTIP = "This Duplicant stepped in liquid and damaged their bionic systems!";
			}
		}

		// Token: 0x02002508 RID: 9480
		public class TUTORIAL
		{
			// Token: 0x0400A665 RID: 42597
			public static LocString DONT_SHOW_AGAIN = "Don't Show Again";
		}

		// Token: 0x02002509 RID: 9481
		public class PLACERS
		{
			// Token: 0x02003847 RID: 14407
			public class DIGPLACER
			{
				// Token: 0x0400E2ED RID: 58093
				public static LocString NAME = "Dig";
			}

			// Token: 0x02003848 RID: 14408
			public class MOPPLACER
			{
				// Token: 0x0400E2EE RID: 58094
				public static LocString NAME = "Mop";
			}

			// Token: 0x02003849 RID: 14409
			public class MOVEPICKUPABLEPLACER
			{
				// Token: 0x0400E2EF RID: 58095
				public static LocString NAME = "Relocate Here";

				// Token: 0x0400E2F0 RID: 58096
				public static LocString PLACER_STATUS = "Next Destination";

				// Token: 0x0400E2F1 RID: 58097
				public static LocString PLACER_STATUS_TOOLTIP = "Click to see where this item will be relocated to";
			}
		}

		// Token: 0x0200250A RID: 9482
		public class MONUMENT_COMPLETE
		{
			// Token: 0x0400A666 RID: 42598
			public static LocString NAME = "Great Monument";

			// Token: 0x0400A667 RID: 42599
			public static LocString DESC = "A feat of artistic vision and expert engineering that will doubtless inspire Duplicants for thousands of cycles to come";
		}
	}
}
