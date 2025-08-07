using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002CA RID: 714
public class IntermediateRadPillConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000E75 RID: 3701 RVA: 0x00054299 File Offset: 0x00052499
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x000542A0 File Offset: 0x000524A0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x000542A4 File Offset: 0x000524A4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("IntermediateRadPill", global::STRINGS.ITEMS.PILLS.INTERMEDIATERADPILL.NAME, global::STRINGS.ITEMS.PILLS.INTERMEDIATERADPILL.DESC, 1f, true, Assets.GetAnim("vial_radiation_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToMedicine(gameObject, MEDICINE.INTERMEDIATERADPILL);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("Carbon", 1f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("IntermediateRadPill".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		IntermediateRadPillConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("AdvancedApothecary", array, array2), array, array2)
		{
			time = 50f,
			description = global::STRINGS.ITEMS.PILLS.INTERMEDIATERADPILL.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "AdvancedApothecary" },
			sortOrder = 21
		};
		return gameObject;
	}

	// Token: 0x06000E78 RID: 3704 RVA: 0x000543A7 File Offset: 0x000525A7
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x000543A9 File Offset: 0x000525A9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000952 RID: 2386
	public const string ID = "IntermediateRadPill";

	// Token: 0x04000953 RID: 2387
	public static ComplexRecipe recipe;
}
