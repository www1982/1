using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class BasicCureConfig : IEntityConfig
{
	// Token: 0x06000E63 RID: 3683 RVA: 0x00053E1C File Offset: 0x0005201C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicCure", global::STRINGS.ITEMS.PILLS.BASICCURE.NAME, global::STRINGS.ITEMS.PILLS.BASICCURE.DESC, 1f, true, Assets.GetAnim("pill_foodpoisoning_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICCURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Carbon.CreateTag(), 1f),
			new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicCure", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = global::STRINGS.ITEMS.PILLS.BASICCURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
		return gameObject;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x00053F36 File Offset: 0x00052136
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E65 RID: 3685 RVA: 0x00053F38 File Offset: 0x00052138
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400094A RID: 2378
	public const string ID = "BasicCure";

	// Token: 0x0400094B RID: 2379
	public static ComplexRecipe recipe;
}
