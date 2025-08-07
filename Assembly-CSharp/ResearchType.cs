using System;
using UnityEngine;

// Token: 0x02000AC3 RID: 2755
public class ResearchType
{
	// Token: 0x06005007 RID: 20487 RVA: 0x001CF905 File Offset: 0x001CDB05
	public ResearchType(string id, string name, string description, Sprite sprite, Color color, Recipe.Ingredient[] fabricationIngredients, float fabricationTime, HashedString kAnim_ID, string[] fabricators, string recipeDescription)
	{
		this._id = id;
		this._name = name;
		this._description = description;
		this._sprite = sprite;
		this._color = color;
		this.CreatePrefab(fabricationIngredients, fabricationTime, kAnim_ID, fabricators, recipeDescription, color);
	}

	// Token: 0x06005008 RID: 20488 RVA: 0x001CF948 File Offset: 0x001CDB48
	public GameObject CreatePrefab(Recipe.Ingredient[] fabricationIngredients, float fabricationTime, HashedString kAnim_ID, string[] fabricators, string recipeDescription, Color color)
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity(this.id, this.name, this.description, 1f, true, Assets.GetAnim(kAnim_ID), "ui", Grid.SceneLayer.BuildingFront, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<ResearchPointObject>().TypeID = this.id;
		this._recipe = new Recipe(this.id, 1f, (SimHashes)0, this.name, recipeDescription, 0);
		this._recipe.SetFabricators(fabricators, fabricationTime);
		this._recipe.SetIcon(Assets.GetSprite("research_type_icon"), color);
		if (fabricationIngredients != null)
		{
			foreach (Recipe.Ingredient ingredient in fabricationIngredients)
			{
				this._recipe.AddIngredient(ingredient);
			}
		}
		return gameObject;
	}

	// Token: 0x06005009 RID: 20489 RVA: 0x001CFA0D File Offset: 0x001CDC0D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600500A RID: 20490 RVA: 0x001CFA0F File Offset: 0x001CDC0F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x0600500B RID: 20491 RVA: 0x001CFA11 File Offset: 0x001CDC11
	public string id
	{
		get
		{
			return this._id;
		}
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x0600500C RID: 20492 RVA: 0x001CFA19 File Offset: 0x001CDC19
	public string name
	{
		get
		{
			return this._name;
		}
	}

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x0600500D RID: 20493 RVA: 0x001CFA21 File Offset: 0x001CDC21
	public string description
	{
		get
		{
			return this._description;
		}
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x0600500E RID: 20494 RVA: 0x001CFA29 File Offset: 0x001CDC29
	public string recipe
	{
		get
		{
			return this.recipe;
		}
	}

	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x0600500F RID: 20495 RVA: 0x001CFA31 File Offset: 0x001CDC31
	public Color color
	{
		get
		{
			return this._color;
		}
	}

	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x06005010 RID: 20496 RVA: 0x001CFA39 File Offset: 0x001CDC39
	public Sprite sprite
	{
		get
		{
			return this._sprite;
		}
	}

	// Token: 0x040035E4 RID: 13796
	private string _id;

	// Token: 0x040035E5 RID: 13797
	private string _name;

	// Token: 0x040035E6 RID: 13798
	private string _description;

	// Token: 0x040035E7 RID: 13799
	private Recipe _recipe;

	// Token: 0x040035E8 RID: 13800
	private Sprite _sprite;

	// Token: 0x040035E9 RID: 13801
	private Color _color;
}
