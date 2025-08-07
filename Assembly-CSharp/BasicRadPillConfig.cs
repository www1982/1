using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002C7 RID: 711
public class BasicRadPillConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000E67 RID: 3687 RVA: 0x00053F42 File Offset: 0x00052142
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000E68 RID: 3688 RVA: 0x00053F49 File Offset: 0x00052149
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000E69 RID: 3689 RVA: 0x00053F4C File Offset: 0x0005214C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicRadPill", global::STRINGS.ITEMS.PILLS.BASICRADPILL.NAME, global::STRINGS.ITEMS.PILLS.BASICRADPILL.DESC, 1f, true, Assets.GetAnim("pill_radiation_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.BASICRADPILL);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("BasicRadPill".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		BasicRadPillConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 50f,
			description = global::STRINGS.ITEMS.PILLS.BASICRADPILL.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 10
		};
		return gameObject;
	}

	// Token: 0x06000E6A RID: 3690 RVA: 0x0005404F File Offset: 0x0005224F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x00054051 File Offset: 0x00052251
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400094C RID: 2380
	public const string ID = "BasicRadPill";

	// Token: 0x0400094D RID: 2381
	public static ComplexRecipe recipe;
}
