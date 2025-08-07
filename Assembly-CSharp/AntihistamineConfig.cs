using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C4 RID: 708
public class AntihistamineConfig : IEntityConfig
{
	// Token: 0x06000E59 RID: 3673 RVA: 0x00053B8C File Offset: 0x00051D8C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("Antihistamine", global::STRINGS.ITEMS.PILLS.ANTIHISTAMINE.NAME, global::STRINGS.ITEMS.PILLS.ANTIHISTAMINE.DESC, 1f, true, Assets.GetAnim("pill_allergies_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.ANTIHISTAMINE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Antihistamine", 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(new Tag[]
			{
				"PrickleFlowerSeed",
				KelpConfig.ID
			}, new float[] { 1f, 10f }),
			new ComplexRecipe.RecipeElement(SimHashes.Dirt.CreateTag(), 1f)
		};
		string text = ComplexRecipeManager.MakeRecipeID("Apothecary", array2, array);
		AntihistamineConfig.recipes.Add(this.CreateComplexRecipe(text, array2, array));
		return gameObject;
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x00053C98 File Offset: 0x00051E98
	public ComplexRecipe CreateComplexRecipe(string recipeID, ComplexRecipe.RecipeElement[] input, ComplexRecipe.RecipeElement[] output)
	{
		return new ComplexRecipe(recipeID, input, output)
		{
			time = 100f,
			description = global::STRINGS.ITEMS.PILLS.ANTIHISTAMINE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
	}

	// Token: 0x06000E5B RID: 3675 RVA: 0x00053CF2 File Offset: 0x00051EF2
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E5C RID: 3676 RVA: 0x00053CF4 File Offset: 0x00051EF4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000946 RID: 2374
	public const string ID = "Antihistamine";

	// Token: 0x04000947 RID: 2375
	public static List<ComplexRecipe> recipes = new List<ComplexRecipe>();
}
