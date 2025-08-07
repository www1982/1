using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200006A RID: 106
public class DataMinerConfig : IBuildingConfig
{
	// Token: 0x060001F9 RID: 505 RVA: 0x0000E41E File Offset: 0x0000C61E
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0000E428 File Offset: 0x0000C628
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DataMiner";
		int num = 3;
		int num2 = 2;
		string text2 = "data_miner_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.EnergyConsumptionWhenActive = 1000f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 3f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0000E4CC File Offset: 0x0000C6CC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGet<CopyBuildingSettings>();
		DataMiner dataMiner = go.AddOrGet<DataMiner>();
		dataMiner.duplicantOperated = false;
		dataMiner.showProgressBar = true;
		dataMiner.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		BuildingTemplates.CreateComplexFabricatorStorage(go, dataMiner);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(this.INPUT_MATERIAL_TAG, 5f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(this.OUTPUT_MATERIAL_TAG, 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		string text = ComplexRecipeManager.MakeObsoleteRecipeID("DataMiner", this.OUTPUT_MATERIAL_TAG);
		string text2 = ComplexRecipeManager.MakeRecipeID("DataMiner", array, array2);
		ComplexRecipe complexRecipe = new ComplexRecipe(text2, array, array2);
		complexRecipe.time = 200f;
		complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(this.INPUT_MATERIAL).name, this.OUTPUT_MATERIAL_NAME);
		complexRecipe.fabricators = new List<Tag> { TagManager.Create("DataMiner") };
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult;
		complexRecipe.sortOrder = 300;
		ComplexRecipeManager.Get().AddObsoleteIDMapping(text, text2);
		Prioritizable.AddRef(go);
	}

	// Token: 0x060001FC RID: 508 RVA: 0x0000E603 File Offset: 0x0000C803
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000139 RID: 313
	public const string ID = "DataMiner";

	// Token: 0x0400013A RID: 314
	public const float POWER_USAGE_W = 1000f;

	// Token: 0x0400013B RID: 315
	public const float BASE_UNITS_PRODUCED_PER_CYCLE = 3f;

	// Token: 0x0400013C RID: 316
	public const float BASE_DTU_PRODUCTION = 3f;

	// Token: 0x0400013D RID: 317
	public const float STORAGE_CAPACITY_KG = 1000f;

	// Token: 0x0400013E RID: 318
	public const float MASS_CONSUMED_PER_BANK_KG = 5f;

	// Token: 0x0400013F RID: 319
	public const float BASE_DURATION_SECONDS = 200f;

	// Token: 0x04000140 RID: 320
	public static MathUtil.MinMax PRODUCTION_RATE_SCALE = new MathUtil.MinMax(0.6f, 5.3333335f);

	// Token: 0x04000141 RID: 321
	public static MathUtil.MinMax TEMPERATURE_SCALING_RANGE = new MathUtil.MinMax(10f, 325f);

	// Token: 0x04000142 RID: 322
	public SimHashes INPUT_MATERIAL = SimHashes.Polypropylene;

	// Token: 0x04000143 RID: 323
	public Tag INPUT_MATERIAL_TAG = SimHashes.Polypropylene.CreateTag();

	// Token: 0x04000144 RID: 324
	public Tag OUTPUT_MATERIAL_TAG = DatabankHelper.TAG;

	// Token: 0x04000145 RID: 325
	public string OUTPUT_MATERIAL_NAME = DatabankHelper.NAME;

	// Token: 0x04000146 RID: 326
	public const float BASE_PRODUCTION_PROGRESS_PER_TICK = 0.001f;
}
