using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DB1 RID: 3505
public class ResourceRemainingDisplayScreen : KScreen
{
	// Token: 0x06006E2B RID: 28203 RVA: 0x0029D312 File Offset: 0x0029B512
	public static void DestroyInstance()
	{
		ResourceRemainingDisplayScreen.instance = null;
	}

	// Token: 0x06006E2C RID: 28204 RVA: 0x0029D31A File Offset: 0x0029B51A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Activate();
		ResourceRemainingDisplayScreen.instance = this;
		this.dispayPrefab.SetActive(false);
	}

	// Token: 0x06006E2D RID: 28205 RVA: 0x0029D33A File Offset: 0x0029B53A
	public void ActivateDisplay(GameObject target)
	{
		this.numberOfPendingConstructions = 0;
		this.dispayPrefab.SetActive(true);
	}

	// Token: 0x06006E2E RID: 28206 RVA: 0x0029D34F File Offset: 0x0029B54F
	public void DeactivateDisplay()
	{
		this.dispayPrefab.SetActive(false);
	}

	// Token: 0x06006E2F RID: 28207 RVA: 0x0029D360 File Offset: 0x0029B560
	public void SetResources(IList<Tag> _selected_elements, Recipe recipe)
	{
		this.selected_elements.Clear();
		foreach (Tag tag in _selected_elements)
		{
			this.selected_elements.Add(tag);
		}
		this.currentRecipe = recipe;
		global::Debug.Assert(this.selected_elements.Count == recipe.Ingredients.Count, string.Format("{0} Mismatch number of selected elements {1} and recipe requirements {2}", recipe.Name, this.selected_elements.Count, recipe.Ingredients.Count));
	}

	// Token: 0x06006E30 RID: 28208 RVA: 0x0029D40C File Offset: 0x0029B60C
	public void SetNumberOfPendingConstructions(int number)
	{
		this.numberOfPendingConstructions = number;
	}

	// Token: 0x06006E31 RID: 28209 RVA: 0x0029D418 File Offset: 0x0029B618
	public void Update()
	{
		if (!this.dispayPrefab.activeSelf)
		{
			return;
		}
		if (base.canvas != null)
		{
			if (this.rect == null)
			{
				this.rect = base.GetComponent<RectTransform>();
			}
			this.rect.anchoredPosition = base.WorldToScreen(PlayerController.GetCursorPos(KInputManager.GetMousePos()));
		}
		if (this.displayedConstructionCostMultiplier == this.numberOfPendingConstructions)
		{
			this.label.text = "";
			return;
		}
		this.displayedConstructionCostMultiplier = this.numberOfPendingConstructions;
	}

	// Token: 0x06006E32 RID: 28210 RVA: 0x0029D4A8 File Offset: 0x0029B6A8
	public string GetString()
	{
		string text = "";
		if (this.selected_elements != null && this.currentRecipe != null)
		{
			for (int i = 0; i < this.currentRecipe.Ingredients.Count; i++)
			{
				Tag tag = this.selected_elements[i];
				float num = this.currentRecipe.Ingredients[i].amount * (float)this.numberOfPendingConstructions;
				float num2 = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag, true);
				num2 -= num;
				if (num2 < 0f)
				{
					num2 = 0f;
				}
				string text2 = tag.ProperName();
				if (MaterialSelector.DeprioritizeAutoSelectElementList.Contains(tag) && MaterialSelector.GetValidMaterials(this.currentRecipe.Ingredients[i].tag, false).Count > 1)
				{
					text2 = string.Concat(new string[]
					{
						"<b>",
						UIConstants.ColorPrefixYellow,
						text2,
						UIConstants.ColorSuffix,
						"</b>"
					});
				}
				text = string.Concat(new string[]
				{
					text,
					text2,
					": ",
					GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"),
					" / ",
					GameUtil.GetFormattedMass(this.currentRecipe.Ingredients[i].amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")
				});
				if (i < this.selected_elements.Count - 1)
				{
					text += "\n";
				}
			}
		}
		return text;
	}

	// Token: 0x04004BA8 RID: 19368
	public static ResourceRemainingDisplayScreen instance;

	// Token: 0x04004BA9 RID: 19369
	public GameObject dispayPrefab;

	// Token: 0x04004BAA RID: 19370
	public LocText label;

	// Token: 0x04004BAB RID: 19371
	private Recipe currentRecipe;

	// Token: 0x04004BAC RID: 19372
	private List<Tag> selected_elements = new List<Tag>();

	// Token: 0x04004BAD RID: 19373
	private int numberOfPendingConstructions;

	// Token: 0x04004BAE RID: 19374
	private int displayedConstructionCostMultiplier;

	// Token: 0x04004BAF RID: 19375
	private RectTransform rect;
}
