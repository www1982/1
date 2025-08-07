using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200041E RID: 1054
public class SuitFabricatorConfig : IBuildingConfig
{
	// Token: 0x060015B6 RID: 5558 RVA: 0x0007B6CC File Offset: 0x000798CC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SuitFabricator";
		int num = 4;
		int num2 = 3;
		string text2 = "suit_maker_kanim";
		int num3 = 100;
		float num4 = 240f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.AddSearchTerms(SEARCH_TERMS.ATMOSUIT);
		return buildingDef;
	}

	// Token: 0x060015B7 RID: 5559 RVA: 0x0007B758 File Offset: 0x00079958
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<Prioritizable>();
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.heatedTemperature = 318.15f;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_suit_fabricator_kanim") };
		Prioritizable.AddRef(go);
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		this.ConfigureRecipes();
	}

	// Token: 0x060015B8 RID: 5560 RVA: 0x0007B7E4 File Offset: 0x000799E4
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(GameTags.BasicRefinedMetals, 300f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, "", false, true),
			new ComplexRecipe.RecipeElement(GameTags.Fabrics, 2f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, "", false, false)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array, array2), array, array2)
		{
			time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
			description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "SuitFabricator" },
			requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
			sortOrder = 1,
			recipeCategoryID = ComplexRecipeManager.MakeRecipeCategoryID("SuitFabricator", "StartingMetals", "Atmo_Suit")
		};
		ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Worn_Atmo_Suit".ToTag(), 1f, true),
			new ComplexRecipe.RecipeElement(GameTags.Fabrics, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, "", false, false)
		};
		ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Atmo_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		AtmoSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array3, array4), array3, array4)
		{
			time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
			description = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.REPAIR_WORN_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Custom,
			fabricators = new List<Tag> { "SuitFabricator" },
			requiredTech = Db.Get().TechItems.atmoSuit.parentTechId,
			sortOrder = 2
		};
		AtmoSuitConfig.recipe.customName = global::STRINGS.EQUIPMENT.PREFABS.ATMO_SUIT.REPAIR_WORN_RECIPE_NAME;
		AtmoSuitConfig.recipe.ProductHasFacade = true;
		ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Steel.ToString(), 200f),
			new ComplexRecipe.RecipeElement(SimHashes.Petroleum.ToString(), 25f)
		};
		ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Jet_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		JetSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array5, array6), array5, array6)
		{
			time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
			description = global::STRINGS.EQUIPMENT.PREFABS.JET_SUIT.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "SuitFabricator" },
			requiredTech = Db.Get().TechItems.jetSuit.parentTechId,
			sortOrder = 3
		};
		ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Worn_Jet_Suit".ToTag(), 1f),
			new ComplexRecipe.RecipeElement(GameTags.Fabrics, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, "", false, false)
		};
		ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Jet_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
		};
		JetSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array7, array8), array7, array8)
		{
			time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
			description = global::STRINGS.EQUIPMENT.PREFABS.JET_SUIT.RECIPE_DESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
			fabricators = new List<Tag> { "SuitFabricator" },
			requiredTech = Db.Get().TechItems.jetSuit.parentTechId,
			sortOrder = 4
		};
		if (DlcManager.FeatureRadiationEnabled())
		{
			ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement(SimHashes.Lead.ToString(), 200f),
				new ComplexRecipe.RecipeElement(SimHashes.Glass.ToString(), 10f)
			};
			ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Lead_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
			};
			LeadSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array9, array10), array9, array10)
			{
				time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
				description = global::STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.RECIPE_DESC,
				nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
				fabricators = new List<Tag> { "SuitFabricator" },
				requiredTech = Db.Get().TechItems.leadSuit.parentTechId,
				sortOrder = 5
			};
		}
		if (DlcManager.FeatureRadiationEnabled())
		{
			ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Worn_Lead_Suit".ToTag(), 1f),
				new ComplexRecipe.RecipeElement(SimHashes.Glass.ToString(), 5f)
			};
			ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("Lead_Suit".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated, false)
			};
			LeadSuitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("SuitFabricator", array11, array12), array11, array12)
			{
				time = (float)global::TUNING.EQUIPMENT.SUITS.ATMOSUIT_FABTIME,
				description = global::STRINGS.EQUIPMENT.PREFABS.LEAD_SUIT.RECIPE_DESC,
				nameDisplay = ComplexRecipe.RecipeNameDisplay.ResultWithIngredient,
				fabricators = new List<Tag> { "SuitFabricator" },
				requiredTech = Db.Get().TechItems.leadSuit.parentTechId,
				sortOrder = 6
			};
		}
	}

	// Token: 0x060015B9 RID: 5561 RVA: 0x0007BD78 File Offset: 0x00079F78
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Suits, true);
		};
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			ComplexFabricatorWorkable component = game_object.GetComponent<ComplexFabricatorWorkable>();
			component.WorkerStatusItem = Db.Get().DuplicantStatusItems.Fabricating;
			component.AttributeConverter = Db.Get().AttributeConverters.MachinerySpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.PART_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Technicals.Id;
			component.SkillExperienceMultiplier = SKILLS.PART_DAY_EXPERIENCE;
		};
	}

	// Token: 0x04000CDD RID: 3293
	public const string ID = "SuitFabricator";
}
