using System;
using System.Collections.Generic;
using System.Text;

// Token: 0x02000C04 RID: 3076
public class ComplexRecipeManager
{
	// Token: 0x06005CAE RID: 23726 RVA: 0x0021CA69 File Offset: 0x0021AC69
	public static ComplexRecipeManager Get()
	{
		if (ComplexRecipeManager._Instance == null)
		{
			ComplexRecipeManager._Instance = new ComplexRecipeManager();
		}
		return ComplexRecipeManager._Instance;
	}

	// Token: 0x06005CAF RID: 23727 RVA: 0x0021CA81 File Offset: 0x0021AC81
	public static void DestroyInstance()
	{
		ComplexRecipeManager._Instance = null;
	}

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06005CB0 RID: 23728 RVA: 0x0021CA89 File Offset: 0x0021AC89
	// (set) Token: 0x06005CB1 RID: 23729 RVA: 0x0021CA91 File Offset: 0x0021AC91
	public bool IsPostProcessing { get; private set; }

	// Token: 0x06005CB2 RID: 23730 RVA: 0x0021CA9C File Offset: 0x0021AC9C
	public void PostProcess()
	{
		this.IsPostProcessing = true;
		foreach (ComplexRecipe complexRecipe in this.preProcessRecipes)
		{
			ComplexRecipeManager.Get().Add(complexRecipe, true);
		}
		this.IsPostProcessing = false;
	}

	// Token: 0x06005CB3 RID: 23731 RVA: 0x0021CB04 File Offset: 0x0021AD04
	public static string MakeObsoleteRecipeID(string fabricator, Tag signatureElement)
	{
		string text = "_";
		Tag tag = signatureElement;
		return fabricator + text + tag.ToString();
	}

	// Token: 0x06005CB4 RID: 23732 RVA: 0x0021CB2B File Offset: 0x0021AD2B
	public static string MakeRecipeCategoryID(string fabricator, string categoryName, string productID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(fabricator);
		stringBuilder.Append("_");
		stringBuilder.Append(categoryName);
		stringBuilder.Append("_");
		stringBuilder.Append(productID);
		return stringBuilder.ToString();
	}

	// Token: 0x06005CB5 RID: 23733 RVA: 0x0021CB68 File Offset: 0x0021AD68
	public static string MakeRecipeID(string fabricator, IList<ComplexRecipe.RecipeElement> inputs, IList<ComplexRecipe.RecipeElement> outputs)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(fabricator);
		stringBuilder.Append("_I");
		foreach (ComplexRecipe.RecipeElement recipeElement in inputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement.material.ToString());
		}
		stringBuilder.Append("_O");
		foreach (ComplexRecipe.RecipeElement recipeElement2 in outputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement2.material.ToString());
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06005CB6 RID: 23734 RVA: 0x0021CC50 File Offset: 0x0021AE50
	public static string MakeRecipeID(string fabricator, IList<ComplexRecipe.RecipeElement> inputs, IList<ComplexRecipe.RecipeElement> outputs, string facadeID)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append(fabricator);
		stringBuilder.Append("_I");
		foreach (ComplexRecipe.RecipeElement recipeElement in inputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement.material.ToString());
		}
		stringBuilder.Append("_O");
		foreach (ComplexRecipe.RecipeElement recipeElement2 in outputs)
		{
			stringBuilder.Append("_");
			stringBuilder.Append(recipeElement2.material.ToString());
		}
		if (!string.IsNullOrEmpty(facadeID))
		{
			stringBuilder.Append("_" + facadeID);
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06005CB7 RID: 23735 RVA: 0x0021CD50 File Offset: 0x0021AF50
	public void Add(ComplexRecipe recipe, bool real)
	{
		this.recipes.AddRange(this.DeriveRecipiesFromSource(recipe));
	}

	// Token: 0x06005CB8 RID: 23736 RVA: 0x0021CD64 File Offset: 0x0021AF64
	private List<ComplexRecipe> DeriveRecipiesFromSource(ComplexRecipe sourceRecipe)
	{
		foreach (ComplexRecipe.RecipeElement recipeElement in sourceRecipe.ingredients)
		{
			ListPool<Tag, RecipeManager>.PooledList pooledList = ListPool<Tag, RecipeManager>.Allocate();
			ListPool<float, RecipeManager>.PooledList pooledList2 = ListPool<float, RecipeManager>.Allocate();
			for (int j = 0; j < recipeElement.possibleMaterials.Length; j++)
			{
				if (Assets.TryGetPrefab(recipeElement.possibleMaterials[j]) != null)
				{
					pooledList.Add(recipeElement.possibleMaterials[j]);
					pooledList2.Add((recipeElement.possibleMaterialAmounts == null) ? recipeElement.amount : recipeElement.possibleMaterialAmounts[j]);
				}
			}
			recipeElement.possibleMaterials = pooledList.ToArray();
			recipeElement.possibleMaterialAmounts = pooledList2.ToArray();
			pooledList.Recycle();
			pooledList2.Recycle();
		}
		List<ComplexRecipe> list = new List<ComplexRecipe>();
		List<ComplexRecipe.RecipeElement.IngredientDataSet> list2 = new List<ComplexRecipe.RecipeElement.IngredientDataSet>();
		int num = sourceRecipe.ingredients.Length;
		for (int k = 0; k < sourceRecipe.ingredients[0].possibleMaterials.Length; k++)
		{
			list2.Add(new ComplexRecipe.RecipeElement.IngredientDataSet(new Tag[] { sourceRecipe.ingredients[0].possibleMaterials[k] }, new float[] { (sourceRecipe.ingredients[0].possibleMaterialAmounts == null) ? sourceRecipe.ingredients[0].amount : sourceRecipe.ingredients[0].possibleMaterialAmounts[k] }));
		}
		for (int l = 1; l < num; l++)
		{
			ComplexRecipe.RecipeElement.IngredientDataSet ingredientDataSet = new ComplexRecipe.RecipeElement.IngredientDataSet(sourceRecipe.ingredients[l].possibleMaterials, sourceRecipe.ingredients[l].possibleMaterialAmounts);
			list2 = this.MultiplyIngredientDataSets(list2, ingredientDataSet);
		}
		for (int m = 0; m < list2.Count; m++)
		{
			ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[sourceRecipe.ingredients.Length];
			for (int n = 0; n < array.Length; n++)
			{
				array[n] = new ComplexRecipe.RecipeElement(sourceRecipe.ingredients[n].possibleMaterials, sourceRecipe.ingredients[n].possibleMaterialAmounts, sourceRecipe.ingredients[n].temperatureOperation, sourceRecipe.ingredients[n].facadeID, sourceRecipe.ingredients[n].storeElement, sourceRecipe.ingredients[n].inheritElement, sourceRecipe.ingredients[n].doNotConsume);
			}
			for (int num2 = 0; num2 < array.Length; num2++)
			{
				array[num2].possibleMaterials = new Tag[] { list2[m].substitutionOptions[num2] };
				array[num2].possibleMaterialAmounts = new float[] { list2[m].amounts[num2] };
				array[num2].material = array[num2].possibleMaterials[0];
				array[num2].amount = array[num2].possibleMaterialAmounts[0];
			}
			ComplexRecipe complexRecipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID(sourceRecipe.id.Substring(0, sourceRecipe.id.IndexOf("_")), array, sourceRecipe.results, sourceRecipe.results[0].facadeID), array, sourceRecipe.results);
			complexRecipe.consumedHEP = sourceRecipe.consumedHEP;
			complexRecipe.producedHEP = sourceRecipe.producedHEP;
			complexRecipe.requiredTech = sourceRecipe.requiredTech;
			complexRecipe.SetDLCRestrictions(sourceRecipe.GetRequiredDlcIds(), sourceRecipe.GetForbiddenDlcIds());
			complexRecipe.time = sourceRecipe.time;
			complexRecipe.description = sourceRecipe.description;
			complexRecipe.nameDisplay = sourceRecipe.nameDisplay;
			complexRecipe.fabricators = sourceRecipe.fabricators;
			complexRecipe.requiredTech = sourceRecipe.requiredTech;
			complexRecipe.sortOrder = sourceRecipe.sortOrder;
			complexRecipe.runTimeDescription = sourceRecipe.runTimeDescription;
			complexRecipe.customName = sourceRecipe.customName;
			complexRecipe.customSpritePrefabID = sourceRecipe.customSpritePrefabID;
			complexRecipe.ProductHasFacade = sourceRecipe.ProductHasFacade;
			complexRecipe.recipeCategoryID = sourceRecipe.recipeCategoryID;
			complexRecipe.FabricationVisualizer = sourceRecipe.FabricationVisualizer;
			list.Add(complexRecipe);
		}
		return list;
	}

	// Token: 0x06005CB9 RID: 23737 RVA: 0x0021D170 File Offset: 0x0021B370
	private List<ComplexRecipe.RecipeElement.IngredientDataSet> MultiplyIngredientDataSets(List<ComplexRecipe.RecipeElement.IngredientDataSet> inputList, ComplexRecipe.RecipeElement.IngredientDataSet multiplyAgainst)
	{
		List<ComplexRecipe.RecipeElement.IngredientDataSet> list = new List<ComplexRecipe.RecipeElement.IngredientDataSet>();
		foreach (ComplexRecipe.RecipeElement.IngredientDataSet ingredientDataSet in inputList)
		{
			for (int i = 0; i < multiplyAgainst.substitutionOptions.Length; i++)
			{
				Tag[] array = new Tag[ingredientDataSet.substitutionOptions.Length + 1];
				float[] array2 = new float[ingredientDataSet.amounts.Length + 1];
				ingredientDataSet.substitutionOptions.CopyTo(array, 0);
				ingredientDataSet.amounts.CopyTo(array2, 0);
				array[array.Length - 1] = multiplyAgainst.substitutionOptions[i];
				array2[array2.Length - 1] = multiplyAgainst.amounts[i];
				list.Add(new ComplexRecipe.RecipeElement.IngredientDataSet(array, array2));
			}
		}
		return list;
	}

	// Token: 0x06005CBA RID: 23738 RVA: 0x0021D254 File Offset: 0x0021B454
	public ComplexRecipe GetRecipe(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		ComplexRecipe complexRecipe = this.recipes.Find((ComplexRecipe r) => r.id == id);
		if (complexRecipe == null)
		{
			foreach (ComplexRecipe complexRecipe2 in this.preProcessRecipes)
			{
				if (complexRecipe2.id == id)
				{
					complexRecipe = complexRecipe2;
				}
			}
		}
		return complexRecipe;
	}

	// Token: 0x06005CBB RID: 23739 RVA: 0x0021D2F0 File Offset: 0x0021B4F0
	public List<ComplexRecipe> GetRecipesInCategory(string categoryID)
	{
		return this.recipes.FindAll((ComplexRecipe r) => r.recipeCategoryID == categoryID);
	}

	// Token: 0x06005CBC RID: 23740 RVA: 0x0021D321 File Offset: 0x0021B521
	public void AddObsoleteIDMapping(string obsolete_id, string new_id)
	{
		this.obsoleteIDMapping[obsolete_id] = new_id;
	}

	// Token: 0x06005CBD RID: 23741 RVA: 0x0021D330 File Offset: 0x0021B530
	public ComplexRecipe GetObsoleteRecipe(string id)
	{
		if (string.IsNullOrEmpty(id))
		{
			return null;
		}
		ComplexRecipe complexRecipe = null;
		string text = null;
		if (this.obsoleteIDMapping.TryGetValue(id, out text))
		{
			complexRecipe = this.GetRecipe(text);
		}
		return complexRecipe;
	}

	// Token: 0x04003DAD RID: 15789
	private static ComplexRecipeManager _Instance;

	// Token: 0x04003DAE RID: 15790
	public HashSet<ComplexRecipe> preProcessRecipes = new HashSet<ComplexRecipe>();

	// Token: 0x04003DB0 RID: 15792
	public List<ComplexRecipe> recipes = new List<ComplexRecipe>();

	// Token: 0x04003DB1 RID: 15793
	private Dictionary<string, string> obsoleteIDMapping = new Dictionary<string, string>();
}
