using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020002CB RID: 715
public class LubricationStickConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000E7B RID: 3707 RVA: 0x000543B3 File Offset: 0x000525B3
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000E7C RID: 3708 RVA: 0x000543BA File Offset: 0x000525BA
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000E7D RID: 3709 RVA: 0x000543C0 File Offset: 0x000525C0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("LubricationStick", ITEMS.LUBRICATIONSTICK.NAME, ITEMS.LUBRICATIONSTICK.DESC, LubricationStickConfig.MASS_PER_RECIPE, true, Assets.GetAnim("lubricant_applicator_kanim"), "idle1", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.4f, 1f, true, 0, SimHashes.LiquidGunk, null);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddTag(GameTags.MedicalSupplies);
		gameObject.AddTag(GameTags.SolidLubricant);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.LiquidGunk.CreateTag(), GunkMonitor.GUNK_CAPACITY),
			new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 200f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("LubricationStick".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
			new ComplexRecipe.RecipeElement(SimHashes.DirtyWater.CreateTag(), 200f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		LubricationStickConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 100f,
			description = ITEMS.LUBRICATIONSTICK.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 1,
			requiredTech = Db.Get().TechItems.lubricationStick.parentTechId
		};
		return gameObject;
	}

	// Token: 0x06000E7E RID: 3710 RVA: 0x0005451D File Offset: 0x0005271D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E7F RID: 3711 RVA: 0x0005451F File Offset: 0x0005271F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000954 RID: 2388
	public const string ID = "LubricationStick";

	// Token: 0x04000955 RID: 2389
	public static ComplexRecipe recipe;

	// Token: 0x04000956 RID: 2390
	private const float WATER_MASS = 200f;

	// Token: 0x04000957 RID: 2391
	public static float MASS_PER_RECIPE = GunkMonitor.GUNK_CAPACITY;
}
