using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020002CC RID: 716
public class TallowLubricationStickConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000E82 RID: 3714 RVA: 0x00054535 File Offset: 0x00052735
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x0005453C File Offset: 0x0005273C
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x00054540 File Offset: 0x00052740
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("TallowLubricationStick", ITEMS.TALLOWLUBRICATIONSTICK.NAME, ITEMS.TALLOWLUBRICATIONSTICK.DESC, TallowLubricationStickConfig.MASS_PER_RECIPE, true, Assets.GetAnim("lubricant_applicator_tallow_kanim"), "idle1", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.4f, 1f, true, 0, SimHashes.Tallow, null);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddTag(GameTags.MedicalSupplies);
		gameObject.AddTag(GameTags.SolidLubricant);
		ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement(SimHashes.Tallow.CreateTag(), 10f),
			new ComplexRecipe.RecipeElement(SimHashes.Water.CreateTag(), GunkMonitor.GUNK_CAPACITY - 10f)
		};
		ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
		{
			new ComplexRecipe.RecipeElement("TallowLubricationStick".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
		};
		TallowLubricationStickConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("Apothecary", array, array2), array, array2)
		{
			time = 100f,
			description = ITEMS.TALLOWLUBRICATIONSTICK.RECIPEDESC,
			nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
			fabricators = new List<Tag> { "Apothecary" },
			sortOrder = 1,
			requiredTech = Db.Get().TechItems.lubricationStick.parentTechId
		};
		return gameObject;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x0005468A File Offset: 0x0005288A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x0005468C File Offset: 0x0005288C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000958 RID: 2392
	public const string ID = "TallowLubricationStick";

	// Token: 0x04000959 RID: 2393
	public static ComplexRecipe recipe;

	// Token: 0x0400095A RID: 2394
	public static float MASS_PER_RECIPE = GunkMonitor.GUNK_CAPACITY;
}
