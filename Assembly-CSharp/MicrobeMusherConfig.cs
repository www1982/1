using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D4 RID: 724
public class MicrobeMusherConfig : IBuildingConfig
{
	// Token: 0x06000EAD RID: 3757 RVA: 0x00055C48 File Offset: 0x00053E48
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MicrobeMusher";
		int num = 2;
		int num2 = 3;
		string text2 = "microbemusher_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, MicrobeMusherConfig.DECOR, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.AddSearchTerms(SEARCH_TERMS.FOOD);
		return buildingDef;
	}

	// Token: 0x06000EAE RID: 3758 RVA: 0x00055CE8 File Offset: 0x00053EE8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		Prioritizable.AddRef(go);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<ConduitConsumer>().conduitType = ConduitType.Liquid;
		MicrobeMusher microbeMusher = go.AddOrGet<MicrobeMusher>();
		microbeMusher.mushbarSpawnOffset = new Vector3(1f, 0f, 0f);
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_musher_kanim") };
		microbeMusher.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		BuildingTemplates.CreateComplexFabricatorStorage(go, microbeMusher);
		this.ConfigureRecipes();
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06000EAF RID: 3759 RVA: 0x00055D88 File Offset: 0x00053F88
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Dirt".ToTag(), 75f),
			new ComplexRecipe.RecipeElement("Water".ToTag(), 75f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("MushBar".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		MushBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array, array2), array, array2)
		{
			time = 40f,
			description = global::STRINGS.ITEMS.FOOD.MUSHBAR.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "MicrobeMusher" },
			sortOrder = 1
		};
		MushBarConfig.recipe.SetFabricationAnim("mushbar_kanim");
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicPlantFood", 2f),
			new ComplexRecipe.RecipeElement("Water".ToTag(), 50f)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicPlantBar".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicPlantBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array3, array4), array3, array4)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.BASICPLANTBAR.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "MicrobeMusher" },
			sortOrder = 2
		};
		BasicPlantBarConfig.recipe.SetFabricationAnim("liceloaf_kanim");
		ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BeanPlantSeed", 6f),
			new ComplexRecipe.RecipeElement("Water".ToTag(), 50f)
		};
		ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Tofu".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		TofuConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array5, array6), array5, array6)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.TOFU.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "MicrobeMusher" },
			sortOrder = 3
		};
		TofuConfig.recipe.SetFabricationAnim("loafu_kanim");
		ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(new Tag[]
			{
				"ColdWheatSeed",
				FernFoodConfig.ID
			}, 5f),
			new ComplexRecipe.RecipeElement(new Tag[]
			{
				PrickleFruitConfig.ID,
				"HardSkinBerry"
			}, new float[] { 1f, 2f })
		};
		ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("FruitCake".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		FruitCakeConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array7, array8), array7, array8)
		{
			time = FOOD.RECIPES.STANDARD_COOK_TIME,
			description = global::STRINGS.ITEMS.FOOD.FRUITCAKE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "MicrobeMusher" },
			sortOrder = 3
		};
		FruitCakeConfig.recipe.SetFabricationAnim("fruitcake_kanim");
		if (DlcManager.IsContentSubscribed("DLC2_ID"))
		{
			ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Meat", 1f),
				new ComplexRecipe.RecipeElement("Tallow", 1f)
			};
			ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Pemmican".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
			};
			PemmicanConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("MicrobeMusher", array9, array10), array9, array10, DlcManager.DLC2)
			{
				time = FOOD.RECIPES.STANDARD_COOK_TIME,
				description = global::STRINGS.ITEMS.FOOD.PEMMICAN.RECIPEDESC,
				nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
				fabricators = new List<Tag> { "MicrobeMusher" },
				sortOrder = 4
			};
			PemmicanConfig.recipe.SetFabricationAnim("pemmican_kanim");
		}
	}

	// Token: 0x06000EB0 RID: 3760 RVA: 0x000561C8 File Offset: 0x000543C8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400098D RID: 2445
	public const string ID = "MicrobeMusher";

	// Token: 0x0400098E RID: 2446
	public static EffectorValues DECOR = global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2;
}
