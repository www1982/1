using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200003F RID: 63
public class ChlorinatorConfig : IBuildingConfig
{
	// Token: 0x0600012D RID: 301 RVA: 0x00009608 File Offset: 0x00007808
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Chlorinator";
		int num = 3;
		int num2 = 3;
		string text2 = "chlorinator_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
		return buildingDef;
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000096A7 File Offset: 0x000078A7
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000096AC File Offset: 0x000078AC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = false;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.duplicantOperated = false;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.storeProduced = true;
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		this.ConfigureRecipes();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x00009720 File Offset: 0x00007920
	private void ConfigureRecipes()
	{
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Salt.CreateTag(), 30f),
			new ComplexRecipe.RecipeElement(SimHashes.Gold.CreateTag(), 0.5f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(ChlorinatorConfig.BLEACH_STONE_TAG, 10f),
			new ComplexRecipe.RecipeElement(ChlorinatorConfig.SAND_TAG, 19.999998f)
		};
		ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Chlorinator", array, array2), array, array2);
		complexRecipe.time = 40f;
		complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, ElementLoader.FindElementByHash(SimHashes.Salt).name, ElementLoader.FindElementByHash(SimHashes.BleachStone).name);
		complexRecipe.fabricators = new List<Tag> { TagManager.Create("Chlorinator") };
		complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Result;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00009800 File Offset: 0x00007A00
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		Chlorinator.Def def = go.AddOrGetDef<Chlorinator.Def>();
		def.primaryOreTag = ChlorinatorConfig.BLEACH_STONE_TAG;
		def.primaryOreMassPerOre = 2f;
		def.primaryOreCount = ChlorinatorConfig.EMIT_ORE_COUNT_RANGE_BLEACH_STONE;
		def.secondaryOreTag = ChlorinatorConfig.SAND_TAG;
		def.secondaryOreMassPerOre = 6f;
		def.secondaryOreCount = ChlorinatorConfig.EMIT_ORE_COUNT_RANGE_SAND;
		def.initialVelocity = ChlorinatorConfig.EMIT_ORE_INITIAL_VELOCITY_RANGE;
		def.initialDirectionHalfAngleDegreesRange = ChlorinatorConfig.EMIT_ORE_INITIAL_DIRECTION_HALF_ANGLE_IN_DEGREES_RANGE;
		def.offset = new Vector3(0.6f, 2.2f, 0f);
		def.popWaitRange = ChlorinatorConfig.POP_TIMING;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00009896 File Offset: 0x00007A96
	public override void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x040000B7 RID: 183
	public const string ID = "Chlorinator";

	// Token: 0x040000B8 RID: 184
	public static readonly Tag BLEACH_STONE_TAG = SimHashes.BleachStone.CreateTag();

	// Token: 0x040000B9 RID: 185
	public static readonly Tag SAND_TAG = SimHashes.Sand.CreateTag();

	// Token: 0x040000BA RID: 186
	private const float BLEACH_STONE_PER_CYCLE = 150f;

	// Token: 0x040000BB RID: 187
	public const float BLEACH_STONE_OUTPUT_PER_RECIPE = 10f;

	// Token: 0x040000BC RID: 188
	public const float INPUT_KG = 30f;

	// Token: 0x040000BD RID: 189
	public const float OUTPUT_BLEACH_STONE_PERCENT = 0.33333334f;

	// Token: 0x040000BE RID: 190
	public const float OUTPUT_BLEACHSTONE_ORE_SIZE = 2f;

	// Token: 0x040000BF RID: 191
	public const float OUTPUT_SAND_ORE_SIZE = 6f;

	// Token: 0x040000C0 RID: 192
	public static readonly MathUtil.MinMax POP_TIMING = new MathUtil.MinMax(0.1f, 0.4f);

	// Token: 0x040000C1 RID: 193
	public static readonly MathUtil.MinMaxInt EMIT_ORE_COUNT_RANGE_BLEACH_STONE = new MathUtil.MinMaxInt(2, 3);

	// Token: 0x040000C2 RID: 194
	public static readonly MathUtil.MinMaxInt EMIT_ORE_COUNT_RANGE_SAND = new MathUtil.MinMaxInt(1, 1);

	// Token: 0x040000C3 RID: 195
	public static readonly MathUtil.MinMax EMIT_ORE_INITIAL_VELOCITY_RANGE = new MathUtil.MinMax(2f, 4f);

	// Token: 0x040000C4 RID: 196
	public static readonly MathUtil.MinMax EMIT_ORE_INITIAL_DIRECTION_HALF_ANGLE_IN_DEGREES_RANGE = new MathUtil.MinMax(40f, 0f);
}
