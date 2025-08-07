using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C03 RID: 3075
public class RecipeManager
{
	// Token: 0x06005CAA RID: 23722 RVA: 0x0021CA0F File Offset: 0x0021AC0F
	public static RecipeManager Get()
	{
		if (RecipeManager._Instance == null)
		{
			RecipeManager._Instance = new RecipeManager();
		}
		return RecipeManager._Instance;
	}

	// Token: 0x06005CAB RID: 23723 RVA: 0x0021CA27 File Offset: 0x0021AC27
	public static void DestroyInstance()
	{
		RecipeManager._Instance = null;
	}

	// Token: 0x06005CAC RID: 23724 RVA: 0x0021CA2F File Offset: 0x0021AC2F
	public void Add(Recipe recipe)
	{
		this.recipes.Add(recipe);
		if (recipe.FabricationVisualizer != null)
		{
			global::UnityEngine.Object.DontDestroyOnLoad(recipe.FabricationVisualizer);
		}
	}

	// Token: 0x04003DAB RID: 15787
	private static RecipeManager _Instance;

	// Token: 0x04003DAC RID: 15788
	public List<Recipe> recipes = new List<Recipe>();
}
