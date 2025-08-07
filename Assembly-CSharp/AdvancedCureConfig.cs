using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C3 RID: 707
public class AdvancedCureConfig : IEntityConfig
{
	// Token: 0x06000E55 RID: 3669 RVA: 0x00053A5C File Offset: 0x00051C5C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("AdvancedCure", global::STRINGS.ITEMS.PILLS.ADVANCEDCURE.NAME, global::STRINGS.ITEMS.PILLS.ADVANCEDCURE.DESC, 1f, true, Assets.GetAnim("vial_spore_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.ADVANCEDCURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Steel.CreateTag(), 1f),
			new ComplexRecipe.RecipeElement("LightBugOrangeEgg", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("AdvancedCure", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		string text = "Apothecary";
		AdvancedCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(text, array, array2), array, array2)
		{
			time = 200f,
			description = global::STRINGS.ITEMS.PILLS.ADVANCEDCURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { text },
			sortOrder = 20,
			requiredTech = "MedicineIV"
		};
		return gameObject;
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x00053B7F File Offset: 0x00051D7F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x00053B81 File Offset: 0x00051D81
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000944 RID: 2372
	public const string ID = "AdvancedCure";

	// Token: 0x04000945 RID: 2373
	public static ComplexRecipe recipe;
}
