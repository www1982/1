using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C5 RID: 709
public class BasicBoosterConfig : IEntityConfig
{
	// Token: 0x06000E5F RID: 3679 RVA: 0x00053D0C File Offset: 0x00051F0C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicBooster", global::STRINGS.ITEMS.PILLS.BASICBOOSTER.NAME, global::STRINGS.ITEMS.PILLS.BASICBOOSTER.DESC, 1f, true, Assets.GetAnim("pill_2_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICBOOSTER);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicBooster".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicBoosterConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = global::STRINGS.ITEMS.PILLS.BASICBOOSTER.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 1
		};
		return gameObject;
	}

	// Token: 0x06000E60 RID: 3680 RVA: 0x00053E0E File Offset: 0x0005200E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x00053E10 File Offset: 0x00052010
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000948 RID: 2376
	public const string ID = "BasicBooster";

	// Token: 0x04000949 RID: 2377
	public static ComplexRecipe recipe;
}
