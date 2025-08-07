using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000AFD RID: 2813
public class MaterialsAvailable : SelectModuleCondition
{
	// Token: 0x060052BE RID: 21182 RVA: 0x001E21A2 File Offset: 0x001E03A2
	public override bool IgnoreInSanboxMode()
	{
		return true;
	}

	// Token: 0x060052BF RID: 21183 RVA: 0x001E21A5 File Offset: 0x001E03A5
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		return existingModule == null || ProductInfoScreen.MaterialsMet(selectedPart.CraftRecipe);
	}

	// Token: 0x060052C0 RID: 21184 RVA: 0x001E21C0 File Offset: 0x001E03C0
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MATERIALS_AVAILABLE.COMPLETE;
		}
		string text = UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.MATERIALS_AVAILABLE.FAILED;
		foreach (Recipe.Ingredient ingredient in selectedPart.CraftRecipe.Ingredients)
		{
			string text2 = "\n" + string.Format("{0}{1}: {2}", "    • ", ingredient.tag.ProperName(), GameUtil.GetFormattedMass(ingredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			text += text2;
		}
		return text;
	}
}
