using System;
using System.Collections.Generic;
using System.Diagnostics;
using Klei;
using STRINGS;
using UnityEngine;

// Token: 0x02000A8C RID: 2700
[DebuggerDisplay("{Name}")]
public class Recipe : IHasSortOrder
{
	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x06004E5F RID: 20063 RVA: 0x001C5FAD File Offset: 0x001C41AD
	// (set) Token: 0x06004E60 RID: 20064 RVA: 0x001C5FB5 File Offset: 0x001C41B5
	public int sortOrder { get; set; }

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x06004E62 RID: 20066 RVA: 0x001C5FC7 File Offset: 0x001C41C7
	// (set) Token: 0x06004E61 RID: 20065 RVA: 0x001C5FBE File Offset: 0x001C41BE
	public string Name
	{
		get
		{
			if (this.nameOverride != null)
			{
				return this.nameOverride;
			}
			return this.Result.ProperName();
		}
		set
		{
			this.nameOverride = value;
		}
	}

	// Token: 0x06004E63 RID: 20067 RVA: 0x001C5FE3 File Offset: 0x001C41E3
	public Recipe()
	{
	}

	// Token: 0x06004E64 RID: 20068 RVA: 0x001C5FF8 File Offset: 0x001C41F8
	public Recipe(string prefabId, float outputUnits = 1f, SimHashes elementOverride = (SimHashes)0, string nameOverride = null, string recipeDescription = null, int sortOrder = 0)
	{
		global::Debug.Assert(prefabId != null);
		this.Result = TagManager.Create(prefabId);
		this.ResultElementOverride = elementOverride;
		this.nameOverride = nameOverride;
		this.OutputUnits = outputUnits;
		this.Ingredients = new List<Recipe.Ingredient>();
		this.recipeDescription = recipeDescription;
		this.sortOrder = sortOrder;
		this.FabricationVisualizer = null;
	}

	// Token: 0x06004E65 RID: 20069 RVA: 0x001C6063 File Offset: 0x001C4263
	public Recipe SetFabricator(string fabricator, float fabricationTime)
	{
		this.fabricators = new string[] { fabricator };
		this.FabricationTime = fabricationTime;
		RecipeManager.Get().Add(this);
		return this;
	}

	// Token: 0x06004E66 RID: 20070 RVA: 0x001C6088 File Offset: 0x001C4288
	public Recipe SetFabricators(string[] fabricators, float fabricationTime)
	{
		this.fabricators = fabricators;
		this.FabricationTime = fabricationTime;
		RecipeManager.Get().Add(this);
		return this;
	}

	// Token: 0x06004E67 RID: 20071 RVA: 0x001C60A4 File Offset: 0x001C42A4
	public Recipe SetIcon(Sprite Icon)
	{
		this.Icon = Icon;
		this.IconColor = Color.white;
		return this;
	}

	// Token: 0x06004E68 RID: 20072 RVA: 0x001C60B9 File Offset: 0x001C42B9
	public Recipe SetIcon(Sprite Icon, Color IconColor)
	{
		this.Icon = Icon;
		this.IconColor = IconColor;
		return this;
	}

	// Token: 0x06004E69 RID: 20073 RVA: 0x001C60CA File Offset: 0x001C42CA
	public Recipe AddIngredient(Recipe.Ingredient ingredient)
	{
		this.Ingredients.Add(ingredient);
		return this;
	}

	// Token: 0x06004E6A RID: 20074 RVA: 0x001C60DC File Offset: 0x001C42DC
	public Recipe.Ingredient[] GetAllIngredients(IList<Tag> selectedTags)
	{
		List<Recipe.Ingredient> list = new List<Recipe.Ingredient>();
		for (int i = 0; i < this.Ingredients.Count; i++)
		{
			float amount = this.Ingredients[i].amount;
			if (i < selectedTags.Count)
			{
				list.Add(new Recipe.Ingredient(selectedTags[i], amount));
			}
			else
			{
				list.Add(new Recipe.Ingredient(this.Ingredients[i].tag, amount));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004E6B RID: 20075 RVA: 0x001C6158 File Offset: 0x001C4358
	public Recipe.Ingredient[] GetAllIngredients(IList<Element> selected_elements)
	{
		List<Recipe.Ingredient> list = new List<Recipe.Ingredient>();
		for (int i = 0; i < this.Ingredients.Count; i++)
		{
			int num = (int)this.Ingredients[i].amount;
			bool flag = false;
			if (i < selected_elements.Count)
			{
				Element element = selected_elements[i];
				if (element != null && element.HasTag(this.Ingredients[i].tag))
				{
					list.Add(new Recipe.Ingredient(GameTagExtensions.Create(element.id), (float)num));
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add(new Recipe.Ingredient(this.Ingredients[i].tag, (float)num));
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004E6C RID: 20076 RVA: 0x001C6210 File Offset: 0x001C4410
	public GameObject Craft(Storage resource_storage, IList<Tag> selectedTags)
	{
		Recipe.Ingredient[] allIngredients = this.GetAllIngredients(selectedTags);
		return this.CraftRecipe(resource_storage, allIngredients);
	}

	// Token: 0x06004E6D RID: 20077 RVA: 0x001C6230 File Offset: 0x001C4430
	private GameObject CraftRecipe(Storage resource_storage, Recipe.Ingredient[] ingredientTags)
	{
		SimUtil.DiseaseInfo diseaseInfo = SimUtil.DiseaseInfo.Invalid;
		float num = 0f;
		float num2 = 0f;
		foreach (Recipe.Ingredient ingredient in ingredientTags)
		{
			GameObject gameObject = resource_storage.FindFirst(ingredient.tag);
			if (gameObject != null)
			{
				Edible component = gameObject.GetComponent<Edible>();
				if (component)
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, -component.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED_USED, "{0}", component.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
				}
			}
			SimUtil.DiseaseInfo diseaseInfo2;
			float num3;
			resource_storage.ConsumeAndGetDisease(ingredient, out diseaseInfo2, out num3);
			diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
			num = SimUtil.CalculateFinalTemperature(num2, num, ingredient.amount, num3);
			num2 += ingredient.amount;
		}
		GameObject prefab = Assets.GetPrefab(this.Result);
		GameObject gameObject2 = null;
		if (prefab != null)
		{
			gameObject2 = GameUtil.KInstantiate(prefab, Grid.SceneLayer.Ore, null, 0);
			PrimaryElement component2 = gameObject2.GetComponent<PrimaryElement>();
			gameObject2.GetComponent<KSelectable>().entityName = this.Name;
			if (component2 != null)
			{
				gameObject2.GetComponent<KPrefabID>().RemoveTag(TagManager.Create("Vacuum"));
				if (this.ResultElementOverride != (SimHashes)0)
				{
					if (component2.GetComponent<ElementChunk>() != null)
					{
						component2.SetElement(this.ResultElementOverride, true);
					}
					else
					{
						component2.ElementID = this.ResultElementOverride;
					}
				}
				component2.Temperature = num;
				component2.Units = this.OutputUnits;
			}
			Edible component3 = gameObject2.GetComponent<Edible>();
			if (component3)
			{
				ReportManager.Instance.ReportValue(ReportManager.ReportType.CaloriesCreated, component3.Calories, StringFormatter.Replace(UI.ENDOFDAYREPORT.NOTES.CRAFTED, "{0}", component3.GetProperName()), UI.ENDOFDAYREPORT.NOTES.CRAFTED_CONTEXT);
			}
			gameObject2.SetActive(true);
			if (component2 != null)
			{
				component2.AddDisease(diseaseInfo.idx, diseaseInfo.count, "Recipe.CraftRecipe");
			}
			gameObject2.GetComponent<KMonoBehaviour>().Trigger(748399584, null);
		}
		return gameObject2;
	}

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x06004E6E RID: 20078 RVA: 0x001C6438 File Offset: 0x001C4638
	public string[] MaterialOptionNames
	{
		get
		{
			List<string> list = new List<string>();
			foreach (Element element in ElementLoader.elements)
			{
				if (Array.IndexOf<Tag>(element.oreTags, this.Ingredients[0].tag) >= 0)
				{
					list.Add(element.id.ToString());
				}
			}
			return list.ToArray();
		}
	}

	// Token: 0x06004E6F RID: 20079 RVA: 0x001C64C8 File Offset: 0x001C46C8
	public Element[] MaterialOptions()
	{
		List<Element> list = new List<Element>();
		foreach (Element element in ElementLoader.elements)
		{
			if (Array.IndexOf<Tag>(element.oreTags, this.Ingredients[0].tag) >= 0)
			{
				list.Add(element);
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x001C6548 File Offset: 0x001C4748
	public BuildingDef GetBuildingDef()
	{
		BuildingComplete component = Assets.GetPrefab(this.Result).GetComponent<BuildingComplete>();
		if (component != null)
		{
			return component.Def;
		}
		return null;
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x001C6578 File Offset: 0x001C4778
	public Sprite GetUIIcon()
	{
		Sprite sprite = null;
		if (this.Icon != null)
		{
			sprite = this.Icon;
		}
		else
		{
			KBatchedAnimController component = Assets.GetPrefab(this.Result).GetComponent<KBatchedAnimController>();
			if (component != null)
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
			}
		}
		return sprite;
	}

	// Token: 0x06004E72 RID: 20082 RVA: 0x001C65D2 File Offset: 0x001C47D2
	public Color GetUIColor()
	{
		if (!(this.Icon != null))
		{
			return Color.white;
		}
		return this.IconColor;
	}

	// Token: 0x0400340A RID: 13322
	private string nameOverride;

	// Token: 0x0400340B RID: 13323
	public string HotKey;

	// Token: 0x0400340C RID: 13324
	public string Type;

	// Token: 0x0400340D RID: 13325
	public List<Recipe.Ingredient> Ingredients;

	// Token: 0x0400340E RID: 13326
	public string recipeDescription;

	// Token: 0x0400340F RID: 13327
	public Tag Result;

	// Token: 0x04003410 RID: 13328
	public GameObject FabricationVisualizer;

	// Token: 0x04003411 RID: 13329
	public SimHashes ResultElementOverride;

	// Token: 0x04003412 RID: 13330
	public Sprite Icon;

	// Token: 0x04003413 RID: 13331
	public Color IconColor = Color.white;

	// Token: 0x04003414 RID: 13332
	public string[] fabricators;

	// Token: 0x04003415 RID: 13333
	public float OutputUnits;

	// Token: 0x04003416 RID: 13334
	public float FabricationTime;

	// Token: 0x04003417 RID: 13335
	public string TechUnlock;

	// Token: 0x02001B71 RID: 7025
	[DebuggerDisplay("{tag} {amount}")]
	[Serializable]
	public class Ingredient
	{
		// Token: 0x0600A78F RID: 42895 RVA: 0x003B15AF File Offset: 0x003AF7AF
		public Ingredient(string tag, float amount)
		{
			this.tag = TagManager.Create(tag);
			this.amount = amount;
		}

		// Token: 0x0600A790 RID: 42896 RVA: 0x003B15CA File Offset: 0x003AF7CA
		public Ingredient(Tag tag, float amount)
		{
			this.tag = tag;
			this.amount = amount;
		}

		// Token: 0x0600A791 RID: 42897 RVA: 0x003B15E0 File Offset: 0x003AF7E0
		public List<Element> GetElementOptions()
		{
			List<Element> list = new List<Element>(ElementLoader.elements);
			list.RemoveAll((Element e) => !e.IsSolid);
			list.RemoveAll((Element e) => !e.HasTag(this.tag));
			return list;
		}

		// Token: 0x040082F0 RID: 33520
		public Tag tag;

		// Token: 0x040082F1 RID: 33521
		public float amount;
	}
}
