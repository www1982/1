using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000C05 RID: 3077
public class ComplexRecipe : IHasDlcRestrictions
{
	// Token: 0x06005CBF RID: 23743 RVA: 0x0021D38D File Offset: 0x0021B58D
	public void SetDLCRestrictions(string[] required, string[] forbidden)
	{
		this.requiredDlcIds = required;
		this.forbiddenDlcIds = forbidden;
	}

	// Token: 0x06005CC0 RID: 23744 RVA: 0x0021D39D File Offset: 0x0021B59D
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06005CC1 RID: 23745 RVA: 0x0021D3A5 File Offset: 0x0021B5A5
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x0021D3AD File Offset: 0x0021B5AD
	// (set) Token: 0x06005CC3 RID: 23747 RVA: 0x0021D3B5 File Offset: 0x0021B5B5
	public bool ProductHasFacade { get; set; }

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x06005CC4 RID: 23748 RVA: 0x0021D3BE File Offset: 0x0021B5BE
	// (set) Token: 0x06005CC5 RID: 23749 RVA: 0x0021D3C6 File Offset: 0x0021B5C6
	public bool RequiresAllIngredientsDiscovered { get; set; }

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x0021D3CF File Offset: 0x0021B5CF
	public Tag FirstResult
	{
		get
		{
			return this.results[0].material;
		}
	}

	// Token: 0x06005CC7 RID: 23751 RVA: 0x0021D3E0 File Offset: 0x0021B5E0
	private static GameObject CreateFabricationVisualizer(string anim, string nameRoot = null)
	{
		GameObject gameObject = new GameObject();
		if (nameRoot != null)
		{
			gameObject.name = nameRoot + "Visualizer";
		}
		gameObject.SetActive(false);
		gameObject.transform.SetLocalPosition(Vector3.zero);
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(anim) };
		kbatchedAnimController.initialAnim = "fabricating";
		kbatchedAnimController.isMovable = true;
		KBatchedAnimTracker kbatchedAnimTracker = gameObject.AddComponent<KBatchedAnimTracker>();
		kbatchedAnimTracker.symbol = new HashedString("meter_ration");
		kbatchedAnimTracker.offset = Vector3.zero;
		global::UnityEngine.Object.DontDestroyOnLoad(gameObject);
		return gameObject;
	}

	// Token: 0x06005CC8 RID: 23752 RVA: 0x0021D47C File Offset: 0x0021B67C
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results)
	{
		this.id = id;
		this.ingredients = ingredients;
		this.results = results;
		this.recipeCategoryID = ComplexRecipeManager.MakeRecipeCategoryID(id, "Default", results[0].material.ToString());
		if (!ComplexRecipeManager.Get().IsPostProcessing)
		{
			ComplexRecipeManager.Get().preProcessRecipes.Add(this);
		}
	}

	// Token: 0x06005CC9 RID: 23753 RVA: 0x0021D4E5 File Offset: 0x0021B6E5
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP, int producedHEP)
		: this(id, ingredients, results)
	{
		this.consumedHEP = consumedHEP;
		this.producedHEP = producedHEP;
	}

	// Token: 0x06005CCA RID: 23754 RVA: 0x0021D500 File Offset: 0x0021B700
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP)
		: this(id, ingredients, results, consumedHEP, 0)
	{
	}

	// Token: 0x06005CCB RID: 23755 RVA: 0x0021D50E File Offset: 0x0021B70E
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, string[] requiredDlcIds)
		: this(id, ingredients, results, requiredDlcIds, null)
	{
	}

	// Token: 0x06005CCC RID: 23756 RVA: 0x0021D51C File Offset: 0x0021B71C
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, string[] requiredDlcIds, string[] forbiddenDlcIds)
		: this(id, ingredients, results)
	{
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06005CCD RID: 23757 RVA: 0x0021D537 File Offset: 0x0021B737
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP, int producedHEP, string[] requiredDlcIds)
		: this(id, ingredients, results, consumedHEP, producedHEP, requiredDlcIds, null)
	{
	}

	// Token: 0x06005CCE RID: 23758 RVA: 0x0021D549 File Offset: 0x0021B749
	public ComplexRecipe(string id, ComplexRecipe.RecipeElement[] ingredients, ComplexRecipe.RecipeElement[] results, int consumedHEP, int producedHEP, string[] requiredDlcIds, string[] forbiddenDlcIds)
		: this(id, ingredients, results, consumedHEP, producedHEP)
	{
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06005CCF RID: 23759 RVA: 0x0021D568 File Offset: 0x0021B768
	public void SetFabricationAnim(string anim)
	{
		this.FabricationVisualizer = ComplexRecipe.CreateFabricationVisualizer(anim, this.id);
	}

	// Token: 0x06005CD0 RID: 23760 RVA: 0x0021D57C File Offset: 0x0021B77C
	public float TotalResultUnits()
	{
		float num = 0f;
		foreach (ComplexRecipe.RecipeElement recipeElement in this.results)
		{
			num += recipeElement.amount;
		}
		return num;
	}

	// Token: 0x06005CD1 RID: 23761 RVA: 0x0021D5B2 File Offset: 0x0021B7B2
	public bool RequiresTechUnlock()
	{
		return !string.IsNullOrEmpty(this.requiredTech);
	}

	// Token: 0x06005CD2 RID: 23762 RVA: 0x0021D5C2 File Offset: 0x0021B7C2
	public bool IsRequiredTechUnlocked()
	{
		return string.IsNullOrEmpty(this.requiredTech) || Db.Get().Techs.Get(this.requiredTech).IsComplete();
	}

	// Token: 0x06005CD3 RID: 23763 RVA: 0x0021D5F0 File Offset: 0x0021B7F0
	public Sprite GetUIIcon()
	{
		Sprite sprite = null;
		Tag tag = ((this.nameDisplay == ComplexRecipe.RecipeNameDisplay.Ingredient) ? this.ingredients[0].material : this.results[0].material);
		if (this.nameDisplay == ComplexRecipe.RecipeNameDisplay.Custom && !string.IsNullOrEmpty(this.customSpritePrefabID))
		{
			tag = this.customSpritePrefabID;
		}
		KBatchedAnimController component = Assets.GetPrefab(tag).GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
		}
		return sprite;
	}

	// Token: 0x06005CD4 RID: 23764 RVA: 0x0021D675 File Offset: 0x0021B875
	public Color GetUIColor()
	{
		return Color.white;
	}

	// Token: 0x06005CD5 RID: 23765 RVA: 0x0021D67C File Offset: 0x0021B87C
	public string GetUIName(bool includeAmounts)
	{
		string text = (this.results[0].facadeID.IsNullOrWhiteSpace() ? this.results[0].material.ProperName() : this.results[0].facadeID.ProperName());
		switch (this.nameDisplay)
		{
		case ComplexRecipe.RecipeNameDisplay.Result:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_SIMPLE_INCLUDE_AMOUNTS, text, this.results[0].amount);
			}
			return text;
		case ComplexRecipe.RecipeNameDisplay.IngredientToResult:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.ingredients[0].amount,
					this.results[0].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.ResultWithIngredient:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_WITH_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.ingredients[0].amount,
					this.results[0].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_WITH, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.Composite:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_COMPOSITE_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					text,
					this.results[1].material.ProperName(),
					this.ingredients[0].amount,
					this.results[0].amount,
					this.results[1].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_COMPOSITE, this.ingredients[0].material.ProperName(), text, this.results[1].material.ProperName());
		case ComplexRecipe.RecipeNameDisplay.HEP:
			if (includeAmounts)
			{
				return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_HEP_INCLUDE_AMOUNTS, new object[]
				{
					this.ingredients[0].material.ProperName(),
					this.results[1].material.ProperName(),
					this.ingredients[0].amount,
					this.producedHEP,
					this.results[1].amount
				});
			}
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_FROM_TO_HEP, this.ingredients[0].material.ProperName(), text);
		case ComplexRecipe.RecipeNameDisplay.Custom:
			return this.customName;
		}
		if (includeAmounts)
		{
			return string.Format(UI.UISIDESCREENS.REFINERYSIDESCREEN.RECIPE_SIMPLE_INCLUDE_AMOUNTS, this.ingredients[0].material.ProperName(), this.ingredients[0].amount);
		}
		return this.ingredients[0].material.ProperName();
	}

	// Token: 0x04003DB2 RID: 15794
	public string id;

	// Token: 0x04003DB3 RID: 15795
	public string recipeCategoryID;

	// Token: 0x04003DB4 RID: 15796
	public ComplexRecipe.RecipeElement[] ingredients;

	// Token: 0x04003DB5 RID: 15797
	public ComplexRecipe.RecipeElement[] results;

	// Token: 0x04003DB6 RID: 15798
	public float time;

	// Token: 0x04003DB7 RID: 15799
	public GameObject FabricationVisualizer;

	// Token: 0x04003DB8 RID: 15800
	public int consumedHEP;

	// Token: 0x04003DB9 RID: 15801
	public int producedHEP;

	// Token: 0x04003DBA RID: 15802
	private string[] requiredDlcIds;

	// Token: 0x04003DBB RID: 15803
	private string[] forbiddenDlcIds;

	// Token: 0x04003DBD RID: 15805
	public ComplexRecipe.RecipeNameDisplay nameDisplay;

	// Token: 0x04003DBE RID: 15806
	public string customName;

	// Token: 0x04003DBF RID: 15807
	public string customSpritePrefabID;

	// Token: 0x04003DC0 RID: 15808
	public string description;

	// Token: 0x04003DC1 RID: 15809
	public Func<string> runTimeDescription;

	// Token: 0x04003DC2 RID: 15810
	public List<Tag> fabricators;

	// Token: 0x04003DC3 RID: 15811
	public int sortOrder;

	// Token: 0x04003DC4 RID: 15812
	public string requiredTech;

	// Token: 0x02001D42 RID: 7490
	public enum RecipeNameDisplay
	{
		// Token: 0x0400889E RID: 34974
		Ingredient,
		// Token: 0x0400889F RID: 34975
		Result,
		// Token: 0x040088A0 RID: 34976
		IngredientToResult,
		// Token: 0x040088A1 RID: 34977
		ResultWithIngredient,
		// Token: 0x040088A2 RID: 34978
		Composite,
		// Token: 0x040088A3 RID: 34979
		HEP,
		// Token: 0x040088A4 RID: 34980
		Custom
	}

	// Token: 0x02001D43 RID: 7491
	public class RecipeElement
	{
		// Token: 0x0600AD94 RID: 44436 RVA: 0x003C5337 File Offset: 0x003C3537
		public RecipeElement(Tag[] materialOptions, float amount)
		{
			this.material = null;
			this.possibleMaterials = materialOptions;
			this.amount = amount;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
		}

		// Token: 0x0600AD95 RID: 44437 RVA: 0x003C5360 File Offset: 0x003C3560
		public RecipeElement(Tag[] materialOptions, float[] amounts)
		{
			this.material = null;
			this.possibleMaterials = materialOptions;
			this.possibleMaterialAmounts = amounts;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
		}

		// Token: 0x0600AD96 RID: 44438 RVA: 0x003C538C File Offset: 0x003C358C
		public RecipeElement(Tag[] materialOptions, float amount, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, string facadeID, bool storeElement = false, bool inheritElement = false)
		{
			this.material = null;
			this.possibleMaterials = materialOptions;
			this.amount = amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
			this.facadeID = facadeID;
			this.inheritElement = inheritElement;
		}

		// Token: 0x0600AD97 RID: 44439 RVA: 0x003C53D8 File Offset: 0x003C35D8
		public RecipeElement(Tag[] materialOptions, float[] amounts, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, string facadeID, bool storeElement = false, bool inheritElement = false, bool doNotConsume = false)
		{
			this.material = null;
			this.possibleMaterials = materialOptions;
			this.possibleMaterialAmounts = amounts;
			this.amount = this.amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
			this.facadeID = facadeID;
			this.inheritElement = inheritElement;
			this.doNotConsume = doNotConsume;
		}

		// Token: 0x0600AD98 RID: 44440 RVA: 0x003C5438 File Offset: 0x003C3638
		public RecipeElement(Tag material, float amount, bool inheritElement)
		{
			this.material = material;
			this.possibleMaterials = new Tag[] { material };
			this.amount = amount;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
			this.inheritElement = inheritElement;
		}

		// Token: 0x0600AD99 RID: 44441 RVA: 0x003C5470 File Offset: 0x003C3670
		public RecipeElement(Tag material, float amount)
		{
			this.material = material;
			this.possibleMaterials = new Tag[] { material };
			this.amount = amount;
			this.temperatureOperation = ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature;
		}

		// Token: 0x0600AD9A RID: 44442 RVA: 0x003C54A1 File Offset: 0x003C36A1
		public RecipeElement(Tag material, float amount, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, bool storeElement = false)
		{
			this.material = material;
			this.possibleMaterials = new Tag[] { material };
			this.amount = amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
		}

		// Token: 0x0600AD9B RID: 44443 RVA: 0x003C54DC File Offset: 0x003C36DC
		public RecipeElement(Tag material, float amount, ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation, string facadeID, bool storeElement = false)
		{
			this.material = material;
			this.possibleMaterials = new Tag[] { material };
			this.amount = amount;
			this.temperatureOperation = temperatureOperation;
			this.storeElement = storeElement;
			this.facadeID = facadeID;
		}

		// Token: 0x0600AD9C RID: 44444 RVA: 0x003C5528 File Offset: 0x003C3728
		public RecipeElement(EdiblesManager.FoodInfo foodInfo, float amount, bool DoNotConsume = false)
		{
			this.material = foodInfo.Id;
			this.possibleMaterials = new Tag[] { this.material };
			this.amount = amount;
			this.doNotConsume = DoNotConsume;
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600AD9D RID: 44445 RVA: 0x003C5568 File Offset: 0x003C3768
		// (set) Token: 0x0600AD9E RID: 44446 RVA: 0x003C5570 File Offset: 0x003C3770
		public float amount { get; set; }

		// Token: 0x040088A5 RID: 34981
		public Tag material;

		// Token: 0x040088A6 RID: 34982
		public Tag[] possibleMaterials;

		// Token: 0x040088A7 RID: 34983
		public float[] possibleMaterialAmounts;

		// Token: 0x040088A9 RID: 34985
		public ComplexRecipe.RecipeElement.TemperatureOperation temperatureOperation;

		// Token: 0x040088AA RID: 34986
		public bool storeElement;

		// Token: 0x040088AB RID: 34987
		public bool inheritElement;

		// Token: 0x040088AC RID: 34988
		public string facadeID;

		// Token: 0x040088AD RID: 34989
		public bool doNotConsume;

		// Token: 0x020028DB RID: 10459
		public struct IngredientDataSet
		{
			// Token: 0x0600CD96 RID: 52630 RVA: 0x0041D659 File Offset: 0x0041B859
			public IngredientDataSet(Tag[] substitutionOptions, float[] amounts)
			{
				this.substitutionOptions = substitutionOptions;
				this.amounts = amounts;
			}

			// Token: 0x0400B515 RID: 46357
			public Tag[] substitutionOptions;

			// Token: 0x0400B516 RID: 46358
			public float[] amounts;
		}

		// Token: 0x020028DC RID: 10460
		public enum TemperatureOperation
		{
			// Token: 0x0400B518 RID: 46360
			AverageTemperature,
			// Token: 0x0400B519 RID: 46361
			Heated,
			// Token: 0x0400B51A RID: 46362
			Melted,
			// Token: 0x0400B51B RID: 46363
			Dehydrated
		}
	}
}
