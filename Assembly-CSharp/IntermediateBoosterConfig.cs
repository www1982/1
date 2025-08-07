using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class IntermediateBoosterConfig : IEntityConfig
{
	// Token: 0x06000E6D RID: 3693 RVA: 0x0005405C File Offset: 0x0005225C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateBooster", global::STRINGS.ITEMS.PILLS.INTERMEDIATEBOOSTER.NAME, global::STRINGS.ITEMS.PILLS.INTERMEDIATEBOOSTER.DESC, 1f, true, Assets.GetAnim("pill_3_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATEBOOSTER);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SpiceNutConfig.ID, 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateBooster", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		IntermediateBoosterConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 100f,
			description = global::STRINGS.ITEMS.PILLS.INTERMEDIATEBOOSTER.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 5
		};
		return gameObject;
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x0005415E File Offset: 0x0005235E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00054160 File Offset: 0x00052360
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400094E RID: 2382
	public const string ID = "IntermediateBooster";

	// Token: 0x0400094F RID: 2383
	public static ComplexRecipe recipe;
}
