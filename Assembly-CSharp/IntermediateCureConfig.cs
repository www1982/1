using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C9 RID: 713
public class IntermediateCureConfig : IEntityConfig
{
	// Token: 0x06000E71 RID: 3697 RVA: 0x0005416C File Offset: 0x0005236C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateCure", global::STRINGS.ITEMS.PILLS.INTERMEDIATECURE.NAME, global::STRINGS.ITEMS.PILLS.INTERMEDIATECURE.DESC, 1f, true, Assets.GetAnim("iv_slimelung_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		gameObject = EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATECURE);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SwampLilyFlowerConfig.ID, 1f),
			new ComplexRecipe.RecipeElement(SimHashes.Phosphorite.CreateTag(), 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateCure", 1f)
		};
		string text = "Apothecary";
		IntermediateCureConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(text, array, array2), array, array2)
		{
			time = 100f,
			description = global::STRINGS.ITEMS.PILLS.INTERMEDIATECURE.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { text },
			sortOrder = 10,
			requiredTech = "MedicineII"
		};
		return gameObject;
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x0005428D File Offset: 0x0005248D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x0005428F File Offset: 0x0005248F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000950 RID: 2384
	public const string ID = "IntermediateCure";

	// Token: 0x04000951 RID: 2385
	public static ComplexRecipe recipe;
}
