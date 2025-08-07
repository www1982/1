using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200006D RID: 109
public class DeepfryerConfig : IBuildingConfig
{
	// Token: 0x0600020A RID: 522 RVA: 0x0000E9FE File Offset: 0x0000CBFE
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000EA08 File Offset: 0x0000CC08
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Deepfryer";
		int num = 2;
		int num2 = 2;
		string text2 = "deepfryer_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		BuildingTemplates.CreateElectricalBuildingDef(buildingDef);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 8f;
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanDeepFry.Id;
		buildingDef.AddSearchTerms(SEARCH_TERMS.FOOD);
		return buildingDef;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Deepfryer deepfryer = go.AddOrGet<Deepfryer>();
		deepfryer.heatedTemperature = 368.15f;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Kitchen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_deepfryer_kanim") };
		deepfryer.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		Prioritizable.AddRef(go);
		go.AddOrGet<DropAllWorkable>();
		this.ConfigureRecipes();
		go.AddOrGetDef<PoweredController.Def>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, deepfryer);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.CookTop, false);
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000EB7C File Offset: 0x0000CD7C
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(CarrotConfig.ID, 1f),
			new ComplexRecipe.RecipeElement("Tallow", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("FriesCarrot", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		FriesCarrotConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array, array2), array, array2, this.GetRequiredDlcIds())
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.FRIESCARROT.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Deepfryer" },
			sortOrder = 100
		};
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BeanPlantSeed", 6f),
			new ComplexRecipe.RecipeElement("Tallow", 1f)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("DeepFriedNosh", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array3, array4), array3, array4, this.GetRequiredDlcIds());
		complexRecipe.time = FOOD.RECIPES.STANDARD_COOK_TIME;
		complexRecipe.description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDNOSH.RECIPEDESC;
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
		complexRecipe.fabricators = new List<Tag> { "Deepfryer" };
		complexRecipe.sortOrder = 200;
		ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("FishMeat", 1f),
			new ComplexRecipe.RecipeElement("Tallow", 2.4f),
			new ComplexRecipe.RecipeElement(new Tag[]
			{
				"ColdWheatSeed",
				FernFoodConfig.ID
			}, 2f)
		};
		ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("DeepFriedFish", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		DeepFriedFishConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array5, array6), array5, array6, this.GetRequiredDlcIds())
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDFISH.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Deepfryer" },
			sortOrder = 300
		};
		ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("ShellfishMeat", 1f),
			new ComplexRecipe.RecipeElement("Tallow", 2.4f),
			new ComplexRecipe.RecipeElement(new Tag[]
			{
				"ColdWheatSeed",
				FernFoodConfig.ID
			}, 2f)
		};
		ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("DeepFriedShellfish", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		DeepFriedShellfishConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Deepfryer", array7, array8), array7, array8, this.GetRequiredDlcIds())
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.DEEPFRIEDSHELLFISH.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Deepfryer" },
			sortOrder = 300
		};
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000EEE2 File Offset: 0x0000D0E2
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000153 RID: 339
	public const string ID = "Deepfryer";
}
