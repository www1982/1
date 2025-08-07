using System;
using STRINGS;
using TUNING;

namespace Database
{
	// Token: 0x02000F62 RID: 3938
	public class SkillPerks : ResourceSet<SkillPerk>
	{
		// Token: 0x06007B53 RID: 31571 RVA: 0x0030D310 File Offset: 0x0030B510
		public SkillPerks(ResourceSet parent)
			: base("SkillPerks", parent)
		{
			this.IncreaseDigSpeedSmall = base.Add(new SkillAttributePerk("IncreaseDigSpeedSmall", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_MINER.NAME, false));
			this.IncreaseDigSpeedMedium = base.Add(new SkillAttributePerk("IncreaseDigSpeedMedium", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MINER.NAME, false));
			this.IncreaseDigSpeedLarge = base.Add(new SkillAttributePerk("IncreaseDigSpeedLarge", Db.Get().Attributes.Digging.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_MINER.NAME, false));
			this.CanDigVeryFirm = base.Add(new SimpleSkillPerk("CanDigVeryFirm", UI.ROLES_SCREEN.PERKS.CAN_DIG_VERY_FIRM.DESCRIPTION));
			this.CanDigNearlyImpenetrable = base.Add(new SimpleSkillPerk("CanDigAbyssalite", UI.ROLES_SCREEN.PERKS.CAN_DIG_NEARLY_IMPENETRABLE.DESCRIPTION));
			this.CanDigSuperDuperHard = base.Add(new SimpleSkillPerk("CanDigDiamondAndObsidan", UI.ROLES_SCREEN.PERKS.CAN_DIG_SUPER_SUPER_HARD.DESCRIPTION));
			this.CanDigRadioactiveMaterials = base.Add(new SimpleSkillPerk("CanDigCorium", UI.ROLES_SCREEN.PERKS.CAN_DIG_RADIOACTIVE_MATERIALS.DESCRIPTION));
			this.CanDigUnobtanium = base.Add(new SimpleSkillPerk("CanDigUnobtanium", UI.ROLES_SCREEN.PERKS.CAN_DIG_UNOBTANIUM.DESCRIPTION));
			this.IncreaseConstructionSmall = base.Add(new SkillAttributePerk("IncreaseConstructionSmall", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_BUILDER.NAME, false));
			this.IncreaseConstructionMedium = base.Add(new SkillAttributePerk("IncreaseConstructionMedium", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.BUILDER.NAME, false));
			this.IncreaseConstructionLarge = base.Add(new SkillAttributePerk("IncreaseConstructionLarge", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_BUILDER.NAME, false));
			this.IncreaseConstructionMechatronics = base.Add(new SkillAttributePerk("IncreaseConstructionMechatronics", Db.Get().Attributes.Construction.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME, false));
			this.CanDemolish = base.Add(new SimpleSkillPerk("CanDemonlish", UI.ROLES_SCREEN.PERKS.CAN_DEMOLISH.DESCRIPTION));
			this.IncreaseLearningSmall = base.Add(new SkillAttributePerk("IncreaseLearningSmall", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_RESEARCHER.NAME, false));
			this.IncreaseLearningMedium = base.Add(new SkillAttributePerk("IncreaseLearningMedium", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.RESEARCHER.NAME, false));
			this.IncreaseLearningLarge = base.Add(new SkillAttributePerk("IncreaseLearningLarge", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_RESEARCHER.NAME, false));
			this.IncreaseLearningLargeSpace = base.Add(new SkillAttributePerk("IncreaseLearningLargeSpace", Db.Get().Attributes.Learning.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SPACE_RESEARCHER.NAME, false));
			this.IncreaseBotanySmall = base.Add(new SkillAttributePerk("IncreaseBotanySmall", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_FARMER.NAME, false));
			this.IncreaseBotanyMedium = base.Add(new SkillAttributePerk("IncreaseBotanyMedium", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.FARMER.NAME, false));
			this.IncreaseBotanyLarge = base.Add(new SkillAttributePerk("IncreaseBotanyLarge", Db.Get().Attributes.Botanist.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_FARMER.NAME, false));
			this.CanFarmTinker = base.Add(new SimpleSkillPerk("CanFarmTinker", UI.ROLES_SCREEN.PERKS.CAN_FARM_TINKER.DESCRIPTION));
			this.CanIdentifyMutantSeeds = base.Add(new SimpleSkillPerk("CanIdentifyMutantSeeds", UI.ROLES_SCREEN.PERKS.CAN_IDENTIFY_MUTANT_SEEDS.DESCRIPTION));
			this.CanFarmStation = base.Add(new SimpleSkillPerk("CanFarmStation", UI.ROLES_SCREEN.PERKS.CAN_FARM_STATION.DESCRIPTION));
			this.IncreaseRanchingSmall = base.Add(new SkillAttributePerk("IncreaseRanchingSmall", Db.Get().Attributes.Ranching.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.RANCHER.NAME, false));
			this.IncreaseRanchingMedium = base.Add(new SkillAttributePerk("IncreaseRanchingMedium", Db.Get().Attributes.Ranching.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.SENIOR_RANCHER.NAME, false));
			this.CanWrangleCreatures = base.Add(new SimpleSkillPerk("CanWrangleCreatures", UI.ROLES_SCREEN.PERKS.CAN_WRANGLE_CREATURES.DESCRIPTION));
			this.CanUseRanchStation = base.Add(new SimpleSkillPerk("CanUseRanchStation", UI.ROLES_SCREEN.PERKS.CAN_USE_RANCH_STATION.DESCRIPTION));
			this.CanUseMilkingStation = base.Add(new SimpleSkillPerk("CanUseMilkingStation", UI.ROLES_SCREEN.PERKS.CAN_USE_MILKING_STATION.DESCRIPTION));
			this.IncreaseAthleticsSmall = base.Add(new SkillAttributePerk("IncreaseAthleticsSmall", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HAULER.NAME, false));
			this.IncreaseAthleticsMedium = base.Add(new SkillAttributePerk("IncreaseAthletics", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.SUIT_EXPERT.NAME, false));
			this.IncreaseAthleticsLarge = base.Add(new SkillAttributePerk("IncreaseAthleticsLarge", Db.Get().Attributes.Athletics.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SUIT_DURABILITY.NAME, false));
			this.IncreaseStrengthGofer = base.Add(new SkillAttributePerk("IncreaseStrengthGofer", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HAULER.NAME, false));
			this.IncreaseStrengthCourier = base.Add(new SkillAttributePerk("IncreaseStrengthCourier", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME, false));
			this.IncreaseStrengthGroundskeeper = base.Add(new SkillAttributePerk("IncreaseStrengthGroundskeeper", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.HANDYMAN.NAME, false));
			this.IncreaseStrengthPlumber = base.Add(new SkillAttributePerk("IncreaseStrengthPlumber", Db.Get().Attributes.Strength.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.PLUMBER.NAME, false));
			this.IncreaseCarryAmountSmall = base.Add(new SkillAttributePerk("IncreaseCarryAmountSmall", Db.Get().Attributes.CarryAmount.Id, 400f, DUPLICANTS.ROLES.HAULER.NAME, false));
			this.IncreaseCarryAmountMedium = base.Add(new SkillAttributePerk("IncreaseCarryAmountMedium", Db.Get().Attributes.CarryAmount.Id, 800f, DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME, false));
			this.IncreaseArtSmall = base.Add(new SkillAttributePerk("IncreaseArtSmall", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_ARTIST.NAME, false));
			this.IncreaseArtMedium = base.Add(new SkillAttributePerk("IncreaseArt", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.ARTIST.NAME, false));
			this.IncreaseArtLarge = base.Add(new SkillAttributePerk("IncreaseArtLarge", Db.Get().Attributes.Art.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MASTER_ARTIST.NAME, false));
			this.CanArt = base.Add(new SimpleSkillPerk("CanArt", UI.ROLES_SCREEN.PERKS.CAN_ART.DESCRIPTION));
			this.CanArtUgly = base.Add(new SimpleSkillPerk("CanArtUgly", UI.ROLES_SCREEN.PERKS.CAN_ART_UGLY.DESCRIPTION));
			this.CanArtOkay = base.Add(new SimpleSkillPerk("CanArtOkay", UI.ROLES_SCREEN.PERKS.CAN_ART_OKAY.DESCRIPTION));
			this.CanArtGreat = base.Add(new SimpleSkillPerk("CanArtGreat", UI.ROLES_SCREEN.PERKS.CAN_ART_GREAT.DESCRIPTION));
			this.CanStudyArtifact = base.Add(new SimpleSkillPerk("CanStudyArtifact", UI.ROLES_SCREEN.PERKS.CAN_STUDY_ARTIFACTS.DESCRIPTION));
			this.CanClothingAlteration = base.Add(new SimpleSkillPerk("CanClothingAlteration", UI.ROLES_SCREEN.PERKS.CAN_CLOTHING_ALTERATION.DESCRIPTION));
			this.IncreaseMachinerySmall = base.Add(new SkillAttributePerk("IncreaseMachinerySmall", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.MACHINE_TECHNICIAN.NAME, false));
			this.IncreaseMachineryMedium = base.Add(new SkillAttributePerk("IncreaseMachineryMedium", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME, false));
			this.IncreaseMachineryLarge = base.Add(new SkillAttributePerk("IncreaseMachineryLarge", Db.Get().Attributes.Machinery.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME, false));
			this.ConveyorBuild = base.Add(new SimpleSkillPerk("ConveyorBuild", UI.ROLES_SCREEN.PERKS.CONVEYOR_BUILD.DESCRIPTION));
			this.CanPowerTinker = base.Add(new SimpleSkillPerk("CanPowerTinker", UI.ROLES_SCREEN.PERKS.CAN_POWER_TINKER.DESCRIPTION));
			this.CanMakeMissiles = base.Add(new SimpleSkillPerk("CanMakeMissiles", UI.ROLES_SCREEN.PERKS.CAN_MAKE_MISSILES.DESCRIPTION));
			this.CanCraftElectronics = base.Add(new SimpleSkillPerk("CanCraftElectronics", UI.ROLES_SCREEN.PERKS.CAN_CRAFT_ELECTRONICS.DESCRIPTION, DlcManager.DLC3));
			this.CanElectricGrill = base.Add(new SimpleSkillPerk("CanElectricGrill", UI.ROLES_SCREEN.PERKS.CAN_ELECTRIC_GRILL.DESCRIPTION));
			this.CanGasRange = base.Add(new SimpleSkillPerk("CanGasRange", UI.ROLES_SCREEN.PERKS.CAN_GAS_RANGE.DESCRIPTION));
			this.CanDeepFry = base.Add(new SimpleSkillPerk("CanDeepFry", UI.ROLES_SCREEN.PERKS.CAN_DEEP_FRYER.DESCRIPTION));
			this.IncreaseCookingSmall = base.Add(new SkillAttributePerk("IncreaseCookingSmall", Db.Get().Attributes.Cooking.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_COOK.NAME, false));
			this.IncreaseCookingMedium = base.Add(new SkillAttributePerk("IncreaseCookingMedium", Db.Get().Attributes.Cooking.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.COOK.NAME, false));
			this.CanSpiceGrinder = base.Add(new SimpleSkillPerk("CanSpiceGrinder ", UI.ROLES_SCREEN.PERKS.CAN_SPICE_GRINDER.DESCRIPTION));
			this.IncreaseCaringSmall = base.Add(new SkillAttributePerk("IncreaseCaringSmall", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.JUNIOR_MEDIC.NAME, false));
			this.IncreaseCaringMedium = base.Add(new SkillAttributePerk("IncreaseCaringMedium", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_SECOND, DUPLICANTS.ROLES.MEDIC.NAME, false));
			this.IncreaseCaringLarge = base.Add(new SkillAttributePerk("IncreaseCaringLarge", Db.Get().Attributes.Caring.Id, (float)ROLES.ATTRIBUTE_BONUS_THIRD, DUPLICANTS.ROLES.SENIOR_MEDIC.NAME, false));
			this.CanCompound = base.Add(new SimpleSkillPerk("CanCompound", UI.ROLES_SCREEN.PERKS.CAN_COMPOUND.DESCRIPTION));
			this.CanDoctor = base.Add(new SimpleSkillPerk("CanDoctor", UI.ROLES_SCREEN.PERKS.CAN_DOCTOR.DESCRIPTION));
			this.CanAdvancedMedicine = base.Add(new SimpleSkillPerk("CanAdvancedMedicine", UI.ROLES_SCREEN.PERKS.CAN_ADVANCED_MEDICINE.DESCRIPTION));
			this.ExosuitExpertise = base.Add(new SimpleSkillPerk("ExosuitExpertise", UI.ROLES_SCREEN.PERKS.EXOSUIT_EXPERTISE.DESCRIPTION));
			this.ExosuitDurability = base.Add(new SimpleSkillPerk("ExosuitDurability", UI.ROLES_SCREEN.PERKS.EXOSUIT_DURABILITY.DESCRIPTION));
			this.AllowAdvancedResearch = base.Add(new SimpleSkillPerk("AllowAdvancedResearch", UI.ROLES_SCREEN.PERKS.ADVANCED_RESEARCH.DESCRIPTION));
			this.AllowInterstellarResearch = base.Add(new SimpleSkillPerk("AllowInterStellarResearch", UI.ROLES_SCREEN.PERKS.INTERSTELLAR_RESEARCH.DESCRIPTION));
			this.AllowNuclearResearch = base.Add(new SimpleSkillPerk("AllowNuclearResearch", UI.ROLES_SCREEN.PERKS.NUCLEAR_RESEARCH.DESCRIPTION));
			this.AllowOrbitalResearch = base.Add(new SimpleSkillPerk("AllowOrbitalResearch", UI.ROLES_SCREEN.PERKS.ORBITAL_RESEARCH.DESCRIPTION));
			this.AllowGeyserTuning = base.Add(new SimpleSkillPerk("AllowGeyserTuning", UI.ROLES_SCREEN.PERKS.GEYSER_TUNING.DESCRIPTION));
			this.AllowChemistry = base.Add(new SimpleSkillPerk("AllowChemistry", UI.ROLES_SCREEN.PERKS.CHEMISTRY.DESCRIPTION));
			this.CanStudyWorldObjects = base.Add(new SimpleSkillPerk("CanStudyWorldObjects", UI.ROLES_SCREEN.PERKS.CAN_STUDY_WORLD_OBJECTS.DESCRIPTION));
			this.CanUseClusterTelescope = base.Add(new SimpleSkillPerk("CanUseClusterTelescope", UI.ROLES_SCREEN.PERKS.CAN_USE_CLUSTER_TELESCOPE.DESCRIPTION));
			this.CanUseClusterTelescopeEnclosed = base.Add(new SimpleSkillPerk("CanUseClusterTelescopeEnclosed", UI.ROLES_SCREEN.PERKS.CAN_CLUSTERTELESCOPEENCLOSED.DESCRIPTION));
			this.CanDoPlumbing = base.Add(new SimpleSkillPerk("CanDoPlumbing", UI.ROLES_SCREEN.PERKS.CAN_DO_PLUMBING.DESCRIPTION));
			this.CanUseRockets = base.Add(new SimpleSkillPerk("CanUseRockets", UI.ROLES_SCREEN.PERKS.CAN_USE_ROCKETS.DESCRIPTION));
			this.FasterSpaceFlight = base.Add(new SkillAttributePerk("FasterSpaceFlight", Db.Get().Attributes.SpaceNavigation.Id, 0.1f, DUPLICANTS.ROLES.ASTRONAUT.NAME, false));
			this.CanTrainToBeAstronaut = base.Add(new SimpleSkillPerk("CanTrainToBeAstronaut", UI.ROLES_SCREEN.PERKS.CAN_DO_ASTRONAUT_TRAINING.DESCRIPTION));
			this.CanMissionControl = base.Add(new SimpleSkillPerk("CanMissionControl", UI.ROLES_SCREEN.PERKS.CAN_MISSION_CONTROL.DESCRIPTION));
			this.CanUseRocketControlStation = base.Add(new SimpleSkillPerk("CanUseRocketControlStation", UI.ROLES_SCREEN.PERKS.CAN_PILOT_ROCKET.DESCRIPTION));
			this.IncreaseRocketSpeedSmall = base.Add(new SkillAttributePerk("IncreaseRocketSpeedSmall", Db.Get().Attributes.SpaceNavigation.Id, (float)ROLES.ATTRIBUTE_BONUS_FIRST, DUPLICANTS.ROLES.ROCKETPILOT.NAME, false));
			if (DlcManager.IsContentSubscribed("DLC3_ID"))
			{
				this.IncreaseCarryAmountBionic = base.Add(new SkillAttributePerk("IncreaseCarryAmountBionic", Db.Get().Attributes.CarryAmount.Id, 600f, DUPLICANTS.ROLES.MATERIALS_MANAGER.NAME, false));
				this.ExtraBionicBooster1 = base.Add(new SkillAttributePerk("ExtraBionicBooster1", Db.Get().Attributes.BionicBoosterSlots.Id, 1f, DUPLICANTS.ATTRIBUTES.BIONICBOOSTERSLOTS.DESC, false));
				this.ExtraBionicBooster2 = base.Add(new SkillAttributePerk("ExtraBionicBooster2", Db.Get().Attributes.BionicBoosterSlots.Id, 1f, DUPLICANTS.ATTRIBUTES.BIONICBOOSTERSLOTS.DESC, false));
				this.ExtraBionicBooster3 = base.Add(new SkillAttributePerk("ExtraBionicBooster3", Db.Get().Attributes.BionicBoosterSlots.Id, 2f, DUPLICANTS.ATTRIBUTES.BIONICBOOSTERSLOTS.DESC, false));
				this.ExtraBionicBooster4 = base.Add(new SkillAttributePerk("ExtraBionicBooster4", Db.Get().Attributes.BionicBoosterSlots.Id, 1f, DUPLICANTS.ATTRIBUTES.BIONICBOOSTERSLOTS.DESC, false));
				this.ExtraBionicBooster5 = base.Add(new SkillAttributePerk("ExtraBionicBooster5", Db.Get().Attributes.BionicBoosterSlots.Id, 1f, "", false));
				this.ExtraBionicBooster6 = base.Add(new SkillAttributePerk("ExtraBionicBooster6", Db.Get().Attributes.BionicBoosterSlots.Id, 1f, DUPLICANTS.ATTRIBUTES.BIONICBOOSTERSLOTS.DESC, false));
				this.ExtraBionicBatteries = base.Add(new SkillAttributePerk("ExtraBionicBatteries", Db.Get().Attributes.BionicBatteryCountCapacity.Id, 2f, UI.ROLES_SCREEN.PERKS.EXTRA_BIONIC_BATTERIES.DESCRIPTION, false));
				this.ReducedBionicGunkProduction = base.Add(new SimpleSkillPerk("ReducedBionicGunkProduction", UI.ROLES_SCREEN.PERKS.REDUCED_GUNK_PRODUCTION.DESCRIPTION));
				this.EfficientBionicGears = base.Add(new SimpleSkillPerk("EfficientBionicGears", UI.ROLES_SCREEN.PERKS.EFFICIENT_BIONIC_GEARS.DESCRIPTION));
				this.IncreaseAthleticsBionicsC1 = base.Add(new SkillAttributePerk("IncreaseAthleticsBionicsC1", Db.Get().Attributes.Athletics.Id, 2f, DUPLICANTS.ROLES.BIONICS_C1.NAME, false));
				this.IncreaseAthleticsBionicsC2 = base.Add(new SkillAttributePerk("IncreaseAthleticsBionicsC2", Db.Get().Attributes.Athletics.Id, 2f, DUPLICANTS.ROLES.BIONICS_C2.NAME, false));
				this.IncreaseAthleticsBionicsB2 = base.Add(new SkillAttributePerk("IncreaseAthleticsBionicsB2", Db.Get().Attributes.Athletics.Id, 2f, DUPLICANTS.ROLES.BIONICS_B2.NAME, false));
				this.IncreaseAthleticsBionicsA2 = base.Add(new SkillAttributePerk("IncreaseAthleticsBionicsA2", Db.Get().Attributes.Athletics.Id, 2f, DUPLICANTS.ROLES.BIONICS_A2.NAME, false));
				this.IncreasedCarryBionics = base.Add(new SkillAttributePerk("IncreasedCarryBionics", Db.Get().Attributes.CarryAmount.Id, 400f, global::STRINGS.ITEMS.BIONIC_BOOSTERS.BOOSTER_CARRY1.NAME, true));
			}
		}

		// Token: 0x04005A25 RID: 23077
		public SkillPerk IncreaseDigSpeedSmall;

		// Token: 0x04005A26 RID: 23078
		public SkillPerk IncreaseDigSpeedMedium;

		// Token: 0x04005A27 RID: 23079
		public SkillPerk IncreaseDigSpeedLarge;

		// Token: 0x04005A28 RID: 23080
		public SkillPerk CanDigVeryFirm;

		// Token: 0x04005A29 RID: 23081
		public SkillPerk CanDigNearlyImpenetrable;

		// Token: 0x04005A2A RID: 23082
		public SkillPerk CanDigSuperDuperHard;

		// Token: 0x04005A2B RID: 23083
		public SkillPerk CanDigRadioactiveMaterials;

		// Token: 0x04005A2C RID: 23084
		public SkillPerk CanDigUnobtanium;

		// Token: 0x04005A2D RID: 23085
		public SkillPerk IncreaseConstructionSmall;

		// Token: 0x04005A2E RID: 23086
		public SkillPerk IncreaseConstructionMedium;

		// Token: 0x04005A2F RID: 23087
		public SkillPerk IncreaseConstructionLarge;

		// Token: 0x04005A30 RID: 23088
		public SkillPerk IncreaseConstructionMechatronics;

		// Token: 0x04005A31 RID: 23089
		public SkillPerk CanDemolish;

		// Token: 0x04005A32 RID: 23090
		public SkillPerk IncreaseLearningSmall;

		// Token: 0x04005A33 RID: 23091
		public SkillPerk IncreaseLearningMedium;

		// Token: 0x04005A34 RID: 23092
		public SkillPerk IncreaseLearningLarge;

		// Token: 0x04005A35 RID: 23093
		public SkillPerk IncreaseLearningLargeSpace;

		// Token: 0x04005A36 RID: 23094
		public SkillPerk IncreaseBotanySmall;

		// Token: 0x04005A37 RID: 23095
		public SkillPerk IncreaseBotanyMedium;

		// Token: 0x04005A38 RID: 23096
		public SkillPerk IncreaseBotanyLarge;

		// Token: 0x04005A39 RID: 23097
		public SkillPerk CanFarmTinker;

		// Token: 0x04005A3A RID: 23098
		public SkillPerk CanIdentifyMutantSeeds;

		// Token: 0x04005A3B RID: 23099
		public SkillPerk CanFarmStation;

		// Token: 0x04005A3C RID: 23100
		public SkillPerk CanWrangleCreatures;

		// Token: 0x04005A3D RID: 23101
		public SkillPerk CanUseRanchStation;

		// Token: 0x04005A3E RID: 23102
		public SkillPerk CanUseMilkingStation;

		// Token: 0x04005A3F RID: 23103
		public SkillPerk IncreaseRanchingSmall;

		// Token: 0x04005A40 RID: 23104
		public SkillPerk IncreaseRanchingMedium;

		// Token: 0x04005A41 RID: 23105
		public SkillPerk IncreaseAthleticsSmall;

		// Token: 0x04005A42 RID: 23106
		public SkillPerk IncreaseAthleticsMedium;

		// Token: 0x04005A43 RID: 23107
		public SkillPerk IncreaseAthleticsLarge;

		// Token: 0x04005A44 RID: 23108
		public SkillPerk IncreaseStrengthSmall;

		// Token: 0x04005A45 RID: 23109
		public SkillPerk IncreaseStrengthMedium;

		// Token: 0x04005A46 RID: 23110
		public SkillPerk IncreaseStrengthGofer;

		// Token: 0x04005A47 RID: 23111
		public SkillPerk IncreaseStrengthCourier;

		// Token: 0x04005A48 RID: 23112
		public SkillPerk IncreaseStrengthGroundskeeper;

		// Token: 0x04005A49 RID: 23113
		public SkillPerk IncreaseStrengthPlumber;

		// Token: 0x04005A4A RID: 23114
		public SkillPerk IncreaseCarryAmountSmall;

		// Token: 0x04005A4B RID: 23115
		public SkillPerk IncreaseCarryAmountMedium;

		// Token: 0x04005A4C RID: 23116
		public SkillPerk IncreaseCarryAmountBionic;

		// Token: 0x04005A4D RID: 23117
		public SkillPerk IncreaseArtSmall;

		// Token: 0x04005A4E RID: 23118
		public SkillPerk IncreaseArtMedium;

		// Token: 0x04005A4F RID: 23119
		public SkillPerk IncreaseArtLarge;

		// Token: 0x04005A50 RID: 23120
		public SkillPerk CanArt;

		// Token: 0x04005A51 RID: 23121
		public SkillPerk CanArtUgly;

		// Token: 0x04005A52 RID: 23122
		public SkillPerk CanArtOkay;

		// Token: 0x04005A53 RID: 23123
		public SkillPerk CanArtGreat;

		// Token: 0x04005A54 RID: 23124
		public SkillPerk CanStudyArtifact;

		// Token: 0x04005A55 RID: 23125
		public SkillPerk CanClothingAlteration;

		// Token: 0x04005A56 RID: 23126
		public SkillPerk IncreaseMachinerySmall;

		// Token: 0x04005A57 RID: 23127
		public SkillPerk IncreaseMachineryMedium;

		// Token: 0x04005A58 RID: 23128
		public SkillPerk IncreaseMachineryLarge;

		// Token: 0x04005A59 RID: 23129
		public SkillPerk ConveyorBuild;

		// Token: 0x04005A5A RID: 23130
		public SkillPerk CanMakeMissiles;

		// Token: 0x04005A5B RID: 23131
		public SkillPerk CanPowerTinker;

		// Token: 0x04005A5C RID: 23132
		public SkillPerk CanCraftElectronics;

		// Token: 0x04005A5D RID: 23133
		public SkillPerk CanElectricGrill;

		// Token: 0x04005A5E RID: 23134
		public SkillPerk CanGasRange;

		// Token: 0x04005A5F RID: 23135
		public SkillPerk CanDeepFry;

		// Token: 0x04005A60 RID: 23136
		public SkillPerk IncreaseCookingSmall;

		// Token: 0x04005A61 RID: 23137
		public SkillPerk IncreaseCookingMedium;

		// Token: 0x04005A62 RID: 23138
		public SkillPerk CanSpiceGrinder;

		// Token: 0x04005A63 RID: 23139
		public SkillPerk IncreaseCaringSmall;

		// Token: 0x04005A64 RID: 23140
		public SkillPerk IncreaseCaringMedium;

		// Token: 0x04005A65 RID: 23141
		public SkillPerk IncreaseCaringLarge;

		// Token: 0x04005A66 RID: 23142
		public SkillPerk CanCompound;

		// Token: 0x04005A67 RID: 23143
		public SkillPerk CanDoctor;

		// Token: 0x04005A68 RID: 23144
		public SkillPerk CanAdvancedMedicine;

		// Token: 0x04005A69 RID: 23145
		public SkillPerk ExosuitExpertise;

		// Token: 0x04005A6A RID: 23146
		public SkillPerk ExosuitDurability;

		// Token: 0x04005A6B RID: 23147
		public SkillPerk AllowAdvancedResearch;

		// Token: 0x04005A6C RID: 23148
		public SkillPerk AllowInterstellarResearch;

		// Token: 0x04005A6D RID: 23149
		public SkillPerk AllowNuclearResearch;

		// Token: 0x04005A6E RID: 23150
		public SkillPerk AllowOrbitalResearch;

		// Token: 0x04005A6F RID: 23151
		public SkillPerk AllowGeyserTuning;

		// Token: 0x04005A70 RID: 23152
		public SkillPerk AllowChemistry;

		// Token: 0x04005A71 RID: 23153
		public SkillPerk CanStudyWorldObjects;

		// Token: 0x04005A72 RID: 23154
		public SkillPerk CanUseClusterTelescope;

		// Token: 0x04005A73 RID: 23155
		public SkillPerk CanUseClusterTelescopeEnclosed;

		// Token: 0x04005A74 RID: 23156
		public SkillPerk IncreaseRocketSpeedSmall;

		// Token: 0x04005A75 RID: 23157
		public SkillPerk CanMissionControl;

		// Token: 0x04005A76 RID: 23158
		public SkillPerk CanDoPlumbing;

		// Token: 0x04005A77 RID: 23159
		public SkillPerk CanUseRockets;

		// Token: 0x04005A78 RID: 23160
		public SkillPerk FasterSpaceFlight;

		// Token: 0x04005A79 RID: 23161
		public SkillPerk CanTrainToBeAstronaut;

		// Token: 0x04005A7A RID: 23162
		public SkillPerk CanUseRocketControlStation;

		// Token: 0x04005A7B RID: 23163
		public SkillPerk ExtraBionicBooster1;

		// Token: 0x04005A7C RID: 23164
		public SkillPerk ExtraBionicBooster2;

		// Token: 0x04005A7D RID: 23165
		public SkillPerk ExtraBionicBooster3;

		// Token: 0x04005A7E RID: 23166
		public SkillPerk ExtraBionicBooster4;

		// Token: 0x04005A7F RID: 23167
		public SkillPerk ExtraBionicBooster5;

		// Token: 0x04005A80 RID: 23168
		public SkillPerk ExtraBionicBooster6;

		// Token: 0x04005A81 RID: 23169
		public SkillPerk ReducedBionicGunkProduction;

		// Token: 0x04005A82 RID: 23170
		public SkillPerk EfficientBionicGears;

		// Token: 0x04005A83 RID: 23171
		public SkillPerk ExtraBionicBatteries;

		// Token: 0x04005A84 RID: 23172
		public SkillPerk IncreaseAthleticsBionicsC1;

		// Token: 0x04005A85 RID: 23173
		public SkillPerk IncreaseAthleticsBionicsC2;

		// Token: 0x04005A86 RID: 23174
		public SkillPerk IncreaseAthleticsBionicsB2;

		// Token: 0x04005A87 RID: 23175
		public SkillPerk IncreaseAthleticsBionicsA2;

		// Token: 0x04005A88 RID: 23176
		public SkillPerk IncreasedCarryBionics;
	}
}
