using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E2D RID: 3629
public class SelectedRecipeQueueScreen : KScreen
{
	// Token: 0x170007EC RID: 2028
	// (get) Token: 0x060072E4 RID: 29412 RVA: 0x002BAE45 File Offset: 0x002B9045
	private ComplexRecipe selectedRecipe
	{
		get
		{
			return this.CalculateSelectedRecipe();
		}
	}

	// Token: 0x170007ED RID: 2029
	// (get) Token: 0x060072E5 RID: 29413 RVA: 0x002BAE4D File Offset: 0x002B904D
	private List<ComplexRecipe> selectedRecipes
	{
		get
		{
			return this.target.GetRecipesWithCategoryID(this.selectedRecipeCategoryID);
		}
	}

	// Token: 0x170007EE RID: 2030
	// (get) Token: 0x060072E6 RID: 29414 RVA: 0x002BAE60 File Offset: 0x002B9060
	private ComplexRecipe firstSelectedRecipe
	{
		get
		{
			return this.selectedRecipes[0];
		}
	}

	// Token: 0x060072E7 RID: 29415 RVA: 0x002BAE70 File Offset: 0x002B9070
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.DecrementButton.onClick = delegate
		{
			if (this.selectedRecipe == null)
			{
				return;
			}
			this.target.DecrementRecipeQueueCount(this.selectedRecipe, false);
			this.RefreshIngredientDescriptors();
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipeCategory(this.selectedRecipeCategoryID, this.target);
		};
		this.IncrementButton.onClick = delegate
		{
			if (this.selectedRecipe == null)
			{
				return;
			}
			this.target.IncrementRecipeQueueCount(this.selectedRecipe);
			this.RefreshIngredientDescriptors();
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipeCategory(this.selectedRecipeCategoryID, this.target);
		};
		this.InfiniteButton.GetComponentInChildren<LocText>().text = UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_FOREVER;
		this.InfiniteButton.onClick += delegate
		{
			if (this.selectedRecipe == null)
			{
				return;
			}
			if (this.target.GetRecipeQueueCount(this.selectedRecipe) != ComplexFabricator.QUEUE_INFINITE)
			{
				this.target.SetRecipeQueueCount(this.selectedRecipe, ComplexFabricator.QUEUE_INFINITE);
			}
			else
			{
				this.target.SetRecipeQueueCount(this.selectedRecipe, 0);
			}
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipeCategory(this.selectedRecipeCategoryID, this.target);
		};
		this.QueueCount.onEndEdit += delegate
		{
			base.isEditing = false;
			if (this.selectedRecipe == null)
			{
				return;
			}
			this.target.SetRecipeQueueCount(this.selectedRecipe, Mathf.RoundToInt(this.QueueCount.currentValue));
			this.RefreshIngredientDescriptors();
			this.RefreshQueueCountDisplay();
			this.ownerScreen.RefreshQueueCountDisplayForRecipeCategory(this.selectedRecipeCategoryID, this.target);
		};
		this.QueueCount.onStartEdit += delegate
		{
			base.isEditing = true;
			KScreenManager.Instance.RefreshStack();
		};
		MultiToggle multiToggle = this.previousRecipeButton;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.CyclePreviousRecipe));
		MultiToggle multiToggle2 = this.nextRecipeButton;
		multiToggle2.onClick = (global::System.Action)Delegate.Combine(multiToggle2.onClick, new global::System.Action(this.CycleNextRecipe));
	}

	// Token: 0x060072E8 RID: 29416 RVA: 0x002BAF60 File Offset: 0x002B9160
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.firstSelectedRecipe != null)
		{
			GameObject prefab = Assets.GetPrefab(this.firstSelectedRecipe.results[0].material);
			Equippable equippable = ((prefab != null) ? prefab.GetComponent<Equippable>() : null);
			if (equippable != null && equippable.GetBuildOverride() != null)
			{
				this.minionWidget.RemoveEquipment(equippable);
			}
		}
	}

	// Token: 0x060072E9 RID: 29417 RVA: 0x002BAFCC File Offset: 0x002B91CC
	private void AutoSelectBestRecipeInCategory()
	{
		int num = -1;
		List<ComplexRecipe> list = new List<ComplexRecipe>();
		this.selectedMaterialOption.Clear();
		ComplexRecipe complexRecipe = null;
		if (this.target.mostRecentRecipeSelectionByCategory.ContainsKey(this.selectedRecipeCategoryID))
		{
			complexRecipe = this.target.GetRecipe(this.target.mostRecentRecipeSelectionByCategory[this.selectedRecipeCategoryID]);
		}
		if (complexRecipe != null)
		{
			foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
			{
				this.selectedMaterialOption.Add(recipeElement.material);
			}
		}
		else
		{
			foreach (ComplexRecipe complexRecipe2 in this.selectedRecipes)
			{
				int num2 = this.target.GetRecipeQueueCount(complexRecipe2);
				if (num2 == ComplexFabricator.QUEUE_INFINITE)
				{
					num2 = int.MaxValue;
				}
				if (num2 >= num)
				{
					if (num2 > num)
					{
						list.Clear();
						num = num2;
					}
					list.Add(complexRecipe2);
				}
			}
			int num3 = list[0].ingredients.Length;
			Tag[] array = new Tag[num3];
			for (int j = 0; j < num3; j++)
			{
				float num4 = -1f;
				foreach (ComplexRecipe complexRecipe3 in list)
				{
					float amount = this.target.GetMyWorld().worldInventory.GetAmount(complexRecipe3.ingredients[j].material, true);
					if (amount > num4)
					{
						array[j] = complexRecipe3.ingredients[j].material;
						num4 = amount;
					}
				}
			}
			this.selectedMaterialOption.AddRange(array);
		}
		this.RefreshIngredientDescriptors();
		this.RefreshQueueCountDisplay();
	}

	// Token: 0x060072EA RID: 29418 RVA: 0x002BB1B0 File Offset: 0x002B93B0
	public bool IsSelectedMaterials(ComplexRecipe recipe)
	{
		if (this.selectedRecipeCategoryID != recipe.recipeCategoryID)
		{
			return false;
		}
		for (int i = 0; i < recipe.ingredients.Length; i++)
		{
			if (recipe.ingredients[i].material != this.selectedMaterialOption[i])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060072EB RID: 29419 RVA: 0x002BB208 File Offset: 0x002B9408
	public void SelectNextQueuedRecipeInCategory()
	{
		this.cycleRecipeVariantIdx++;
		this.selectedMaterialOption.Clear();
		List<ComplexRecipe> list = this.selectedRecipes.Where((ComplexRecipe match) => this.target.IsRecipeQueued(match)).ToList<ComplexRecipe>();
		if (list.Count == 0)
		{
			this.AutoSelectBestRecipeInCategory();
			return;
		}
		ComplexRecipe complexRecipe = list[this.cycleRecipeVariantIdx % list.Count];
		for (int i = 0; i < complexRecipe.ingredients.Length; i++)
		{
			this.selectedMaterialOption.Add(complexRecipe.ingredients[i].material);
		}
		this.RefreshIngredientDescriptors();
		this.RefreshQueueCountDisplay();
	}

	// Token: 0x060072EC RID: 29420 RVA: 0x002BB2A8 File Offset: 0x002B94A8
	public void SetRecipeCategory(ComplexFabricatorSideScreen owner, ComplexFabricator target, string recipeCategoryID)
	{
		this.ownerScreen = owner;
		this.target = target;
		this.selectedRecipeCategoryID = recipeCategoryID;
		this.AutoSelectBestRecipeInCategory();
		this.recipeName.text = this.firstSelectedRecipe.GetUIName(false);
		global::Tuple<Sprite, Color> tuple;
		if (this.firstSelectedRecipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.Ingredient)
		{
			tuple = Def.GetUISprite(this.firstSelectedRecipe.ingredients[0].material, "ui", false);
		}
		else if (this.firstSelectedRecipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.Custom && !string.IsNullOrEmpty(this.firstSelectedRecipe.customSpritePrefabID))
		{
			tuple = Def.GetUISprite(this.firstSelectedRecipe.customSpritePrefabID, "ui", false);
		}
		else
		{
			tuple = Def.GetUISprite(this.firstSelectedRecipe.results[0].material, this.firstSelectedRecipe.results[0].facadeID);
		}
		if (this.firstSelectedRecipe.nameDisplay == ComplexRecipe.RecipeNameDisplay.HEP)
		{
			this.recipeIcon.sprite = owner.radboltSprite;
			this.recipeIcon.sprite = owner.radboltSprite;
		}
		else
		{
			this.recipeIcon.sprite = tuple.first;
			this.recipeIcon.color = tuple.second;
		}
		string text = (this.firstSelectedRecipe.time.ToString() + " " + UI.UNITSUFFIXES.SECONDS).ToLower();
		this.recipeMainDescription.SetText(this.firstSelectedRecipe.description);
		this.recipeDuration.SetText(text);
		string text2 = string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.TOOLTIPS.RECIPE_WORKTIME, text);
		this.recipeDurationTooltip.SetSimpleTooltip(text2);
		this.cycleRecipeVariantIdx = 0;
		this.RefreshIngredientDescriptors();
		this.RefreshResultDescriptors();
		this.RefreshSizeScrollContainerSize();
		this.RefreshQueueCountDisplay();
		this.ToggleAndRefreshMinionDisplay();
	}

	// Token: 0x060072ED RID: 29421 RVA: 0x002BB460 File Offset: 0x002B9660
	private void RefreshSizeScrollContainerSize()
	{
		float num = 16f;
		float num2 = 0f;
		float num3 = (float)((this.selectedRecipe.consumedHEP > 0) ? 94 : 0);
		num2 += (float)(this.materialSelectionRowsByContainer.Count * 32);
		foreach (KeyValuePair<GameObject, List<GameObject>> keyValuePair in this.materialSelectionRowsByContainer)
		{
			num2 += (float)(Mathf.Max(1, keyValuePair.Value.Count) * 48);
		}
		num2 += (float)((this.materialSelectionRowsByContainer.Count - 1) * 12);
		float num4 = (float)Mathf.Max(this.selectedRecipes[0].results.Length * 32 + (this.recipeEffectsDescriptorRows.Count - this.selectedRecipes[0].results.Length) * 16, 40);
		num4 += 46f;
		float num5 = num + num2 + num3 + num4;
		this.scrollContainer.minHeight = Mathf.Min((float)(Screen.height - 448), num5);
	}

	// Token: 0x060072EE RID: 29422 RVA: 0x002BB580 File Offset: 0x002B9780
	private void CyclePreviousRecipe()
	{
		this.ownerScreen.CycleRecipe(-1);
	}

	// Token: 0x060072EF RID: 29423 RVA: 0x002BB58E File Offset: 0x002B978E
	private void CycleNextRecipe()
	{
		this.ownerScreen.CycleRecipe(1);
	}

	// Token: 0x060072F0 RID: 29424 RVA: 0x002BB59C File Offset: 0x002B979C
	private void ToggleAndRefreshMinionDisplay()
	{
		this.minionWidget.gameObject.SetActive(this.RefreshMinionDisplayAnim());
	}

	// Token: 0x060072F1 RID: 29425 RVA: 0x002BB5B4 File Offset: 0x002B97B4
	private bool RefreshMinionDisplayAnim()
	{
		GameObject prefab = Assets.GetPrefab(this.firstSelectedRecipe.results[0].material);
		if (prefab == null)
		{
			return false;
		}
		Equippable component = prefab.GetComponent<Equippable>();
		if (component == null)
		{
			return false;
		}
		KAnimFile buildOverride = component.GetBuildOverride();
		if (buildOverride == null)
		{
			return false;
		}
		this.minionWidget.SetDefaultPortraitAnimator();
		KAnimFile kanimFile = buildOverride;
		if (!this.firstSelectedRecipe.results[0].facadeID.IsNullOrWhiteSpace())
		{
			EquippableFacadeResource equippableFacadeResource = Db.GetEquippableFacades().TryGet(this.firstSelectedRecipe.results[0].facadeID);
			if (equippableFacadeResource != null)
			{
				kanimFile = Assets.GetAnim(equippableFacadeResource.BuildOverride);
			}
		}
		this.minionWidget.UpdateEquipment(component, kanimFile);
		return true;
	}

	// Token: 0x060072F2 RID: 29426 RVA: 0x002BB670 File Offset: 0x002B9870
	private ComplexRecipe CalculateSelectedRecipe()
	{
		foreach (ComplexRecipe complexRecipe in this.target.GetRecipesWithCategoryID(this.selectedRecipeCategoryID))
		{
			bool flag = true;
			for (int i = 0; i < this.selectedMaterialOption.Count; i++)
			{
				if (complexRecipe.ingredients[i].material != this.selectedMaterialOption[i])
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return complexRecipe;
			}
		}
		return null;
	}

	// Token: 0x060072F3 RID: 29427 RVA: 0x002BB710 File Offset: 0x002B9910
	private void RefreshQueueCountDisplay()
	{
		this.ResearchRequiredContainer.SetActive(!this.selectedRecipes[0].IsRequiredTechUnlocked());
		if (this.selectedRecipe == null)
		{
			return;
		}
		bool flag = true;
		foreach (Tag tag in this.selectedMaterialOption)
		{
			if (!DiscoveredResources.Instance.IsDiscovered(tag))
			{
				flag = DebugHandler.InstantBuildMode;
			}
		}
		this.UndiscoveredMaterialsContainer.SetActive(!flag);
		int recipeQueueCount = this.target.GetRecipeQueueCount(this.selectedRecipe);
		bool flag2 = recipeQueueCount == ComplexFabricator.QUEUE_INFINITE;
		if (!flag2)
		{
			this.QueueCount.SetAmount((float)recipeQueueCount);
		}
		else
		{
			this.QueueCount.SetDisplayValue("");
		}
		this.InfiniteIcon.gameObject.SetActive(flag2);
	}

	// Token: 0x060072F4 RID: 29428 RVA: 0x002BB7F8 File Offset: 0x002B99F8
	private void RefreshResultDescriptors()
	{
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> list = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		list.AddRange(this.GetResultDescriptions(this.selectedRecipes[0]));
		foreach (Descriptor descriptor in this.target.AdditionalEffectsForRecipe(this.selectedRecipes[0]))
		{
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor, null, false));
		}
		if (list.Count > 0)
		{
			this.EffectsDescriptorPanel.gameObject.SetActive(true);
			foreach (KeyValuePair<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> keyValuePair in this.recipeEffectsDescriptorRows)
			{
				Util.KDestroyGameObject(keyValuePair.Value);
			}
			this.recipeEffectsDescriptorRows.Clear();
			bool flag = true;
			foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite in list)
			{
				GameObject gameObject = Util.KInstantiateUI(this.recipeElementDescriptorPrefab, this.EffectsDescriptorPanel.gameObject, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				Image reference = component.GetReference<Image>("Icon");
				bool flag2 = descriptorWithSprite.tintedSprite != null && descriptorWithSprite.tintedSprite.first != null;
				reference.sprite = ((descriptorWithSprite.tintedSprite == null) ? null : descriptorWithSprite.tintedSprite.first);
				reference.gameObject.SetActive(true);
				if (!flag2)
				{
					reference.color = Color.clear;
					if (flag)
					{
						gameObject.GetComponent<VerticalLayoutGroup>().padding.top = -8;
						flag = false;
					}
				}
				else
				{
					reference.color = ((descriptorWithSprite.tintedSprite == null) ? Color.white : descriptorWithSprite.tintedSprite.second);
					flag = true;
				}
				reference.gameObject.GetComponent<LayoutElement>().minWidth = (float)(flag2 ? 32 : 40);
				reference.gameObject.GetComponent<LayoutElement>().minHeight = (float)(flag2 ? 32 : 0);
				reference.gameObject.GetComponent<LayoutElement>().preferredHeight = (float)(flag2 ? 32 : 0);
				component.GetReference<LocText>("Label").SetText(flag2 ? descriptorWithSprite.descriptor.IndentedText() : descriptorWithSprite.descriptor.text);
				component.GetReference<RectTransform>("FilterControls").gameObject.SetActive(false);
				component.GetReference<ToolTip>("Tooltip").SetSimpleTooltip(descriptorWithSprite.descriptor.tooltipText);
				this.recipeEffectsDescriptorRows.Add(descriptorWithSprite, gameObject);
			}
		}
	}

	// Token: 0x060072F5 RID: 29429 RVA: 0x002BBAE4 File Offset: 0x002B9CE4
	private List<SelectedRecipeQueueScreen.DescriptorWithSprite> GetResultDescriptions(ComplexRecipe recipe)
	{
		List<SelectedRecipeQueueScreen.DescriptorWithSprite> list = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		if (recipe.producedHEP > 0)
		{
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(string.Format("<b>{0}</b>: {1}", UI.FormatAsLink(ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, "HEP"), recipe.producedHEP), string.Format("<b>{0}</b>: {1}", ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME, recipe.producedHEP), Descriptor.DescriptorType.Requirement, false), new global::Tuple<Sprite, Color>(Assets.GetSprite("radbolt"), Color.white), false));
		}
		foreach (ComplexRecipe.RecipeElement recipeElement in recipe.results)
		{
			GameObject prefab = Assets.GetPrefab(recipeElement.material);
			string formattedByTag = GameUtil.GetFormattedByTag(recipeElement.material, recipeElement.amount, GameUtil.TimeSlice.None);
			list.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(new Descriptor(string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPEPRODUCT, recipeElement.facadeID.IsNullOrWhiteSpace() ? recipeElement.material.ProperName() : recipeElement.facadeID.ProperName(), formattedByTag), string.Format(UI.UISIDESCREENS.FABRICATORSIDESCREEN.TOOLTIPS.RECIPEPRODUCT, recipeElement.facadeID.IsNullOrWhiteSpace() ? recipeElement.material.ProperName() : recipeElement.facadeID.ProperName(), formattedByTag), Descriptor.DescriptorType.Requirement, false), Def.GetUISprite(recipeElement.material, recipeElement.facadeID), false));
			Element element = ElementLoader.GetElement(recipeElement.material);
			if (element != null)
			{
				List<SelectedRecipeQueueScreen.DescriptorWithSprite> list2 = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
				foreach (Descriptor descriptor in GameUtil.GetMaterialDescriptors(element))
				{
					list2.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor, null, false));
				}
				foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite in list2)
				{
					descriptorWithSprite.descriptor.IncreaseIndent();
				}
				list.AddRange(list2);
			}
			else
			{
				List<SelectedRecipeQueueScreen.DescriptorWithSprite> list3 = new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
				foreach (Descriptor descriptor2 in GameUtil.GetEffectDescriptors(GameUtil.GetAllDescriptors(prefab, false)))
				{
					list3.Add(new SelectedRecipeQueueScreen.DescriptorWithSprite(descriptor2, null, false));
				}
				foreach (SelectedRecipeQueueScreen.DescriptorWithSprite descriptorWithSprite2 in list3)
				{
					descriptorWithSprite2.descriptor.IncreaseIndent();
				}
				list.AddRange(list3);
			}
		}
		return list;
	}

	// Token: 0x060072F6 RID: 29430 RVA: 0x002BBDB4 File Offset: 0x002B9FB4
	private void RefreshIngredientDescriptors()
	{
		new List<SelectedRecipeQueueScreen.DescriptorWithSprite>();
		this.IngredientsDescriptorPanel.gameObject.SetActive(true);
		this.radboltSpacer.gameObject.SetActive(this.selectedRecipe.consumedHEP > 0);
		this.radboltHeader.gameObject.SetActive(this.selectedRecipe.consumedHEP > 0);
		this.RadboltDescriptorPanel.gameObject.SetActive(this.selectedRecipe.consumedHEP > 0);
		this.radboltLabel.SetText(ITEMS.RADIATION.HIGHENERGYPARITCLE.NAME + ": " + this.selectedRecipe.consumedHEP.ToString());
		this.materialSelectionContainers.ForEach(delegate(GameObject container)
		{
			Util.KDestroyGameObject(container);
		});
		this.materialSelectionContainers.Clear();
		this.materialSelectionRowsByContainer.Clear();
		for (int i = 0; i < this.selectedRecipes[0].ingredients.Length; i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.materialSelectionContainerPrefab, this.IngredientsDescriptorPanel.gameObject, true);
			this.materialSelectionContainers.Add(gameObject);
			this.materialSelectionRowsByContainer.Add(this.materialSelectionContainers[i], new List<GameObject>());
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			int idx = i;
			List<Tag> list = new List<Tag>();
			bool flag = false;
			HashSet<Tag> hashSet = new HashSet<Tag>();
			for (int j = 0; j < this.selectedRecipes.Count; j++)
			{
				Tag newTag = this.selectedRecipes[j].ingredients[idx].material;
				if (!list.Contains(newTag))
				{
					bool flag2 = DiscoveredResources.Instance.IsDiscovered(newTag);
					if (!flag2)
					{
						hashSet.Add(newTag);
					}
					if (flag2 || DebugHandler.InstantBuildMode)
					{
						flag = true;
						GameObject gameObject2 = Util.KInstantiateUI(this.materialFilterRowPrefab, this.materialSelectionContainers[idx].gameObject, true);
						this.materialSelectionRowsByContainer[this.materialSelectionContainers[idx]].Add(gameObject2);
						list.Add(newTag);
						LocText reference = gameObject2.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
						bool flag3 = false;
						string ingredientDescription = this.GetIngredientDescription(this.selectedRecipes[j].ingredients[idx], out flag3);
						bool flag4 = this.selectedMaterialOption[i] == this.selectedRecipes[j].ingredients[i].material;
						if (flag4)
						{
							component.GetReference<Image>("HeaderBG").color = (flag3 ? Util.ColorFromHex("D9DAE3") : Util.ColorFromHex("E3DAD9"));
						}
						reference.color = (flag3 ? Color.black : new Color(0.2f, 0.2f, 0.2f, 1f));
						HierarchyReferences component2 = gameObject2.GetComponent<HierarchyReferences>();
						component2.GetReference<RectTransform>("SelectionHover").gameObject.SetActive(flag4);
						component2.GetReference<RectTransform>("SelectionHover").GetComponent<Image>().color = (flag3 ? Util.ColorFromHex("F0F6FC") : Util.ColorFromHex("FBE9EB"));
						component2.GetReference<LocText>("OrderCountLabel").SetText(this.target.GetIngredientQueueCount(this.selectedRecipeCategoryID, newTag).ToString());
						Image reference2 = component2.GetReference<Image>("Icon");
						reference2.material = ((!flag3) ? GlobalResources.Instance().AnimMaterialUIDesaturated : GlobalResources.Instance().AnimUIMaterial);
						reference2.color = (flag3 ? Color.white : new Color(1f, 1f, 1f, 0.55f));
						reference.SetText(ingredientDescription);
						reference2.sprite = Def.GetUISprite(newTag, "").first;
						MultiToggle component3 = gameObject2.GetComponent<MultiToggle>();
						component3.ChangeState(flag4 ? 1 : 0);
						component3.onClick = (global::System.Action)Delegate.Combine(component3.onClick, new global::System.Action(delegate
						{
							Tag newTag2 = newTag;
							this.selectedMaterialOption[idx] = newTag2;
							this.RefreshIngredientDescriptors();
							this.RefreshQueueCountDisplay();
							this.ownerScreen.RefreshQueueCountDisplayForRecipeCategory(this.selectedRecipeCategoryID, this.target);
						}));
					}
				}
			}
			ToolTip reference3 = component.GetReference<ToolTip>("HeaderTooltip");
			string text = UI.UISIDESCREENS.FABRICATORSIDESCREEN.UNDISCOVERED_INGREDIENTS_IN_CATEGORY;
			object[] array = new object[1];
			array[0] = "    • " + string.Join("\n    • ", hashSet.Select((Tag t) => t.ProperName()).ToArray<string>());
			string text2 = GameUtil.SafeStringFormat(text, array);
			reference3.SetSimpleTooltip((hashSet.Count == 0) ? UI.UISIDESCREENS.FABRICATORSIDESCREEN.ALL_INGREDIENTS_IN_CATEGORY_DISOVERED : text2);
			RectTransform reference4 = component.GetReference<RectTransform>("NoDiscoveredRow");
			reference4.gameObject.SetActive(!flag);
			if (!flag)
			{
				reference4.GetComponent<ToolTip>().SetSimpleTooltip(text2);
			}
			string text3 = GameUtil.SafeStringFormat(UI.UISIDESCREENS.FABRICATORSIDESCREEN.INGREDIENT_CATEGORY, new object[] { i + 1 });
			if (!flag)
			{
				component.GetReference<Image>("HeaderBG").color = Util.ColorFromHex("E3DAD9");
			}
			if (hashSet.Count > 0)
			{
				text3 = string.Concat(new string[]
				{
					text3,
					" <color=#bf5858>(",
					list.Count.ToString(),
					"/",
					(list.Count + hashSet.Count).ToString(),
					")",
					UIConstants.ColorSuffix
				});
			}
			component.GetReference<LocText>("HeaderLabel").SetText(text3);
		}
		if (!this.target.mostRecentRecipeSelectionByCategory.ContainsKey(this.selectedRecipeCategoryID))
		{
			this.target.mostRecentRecipeSelectionByCategory.Add(this.selectedRecipeCategoryID, null);
		}
		this.target.mostRecentRecipeSelectionByCategory[this.selectedRecipeCategoryID] = this.selectedRecipe.id;
	}

	// Token: 0x060072F7 RID: 29431 RVA: 0x002BC3E4 File Offset: 0x002BA5E4
	private string GetIngredientDescription(ComplexRecipe.RecipeElement ingredient, out bool hasEnoughMaterial)
	{
		GameObject prefab = Assets.GetPrefab(ingredient.material);
		string formattedByTag = GameUtil.GetFormattedByTag(ingredient.material, ingredient.amount, GameUtil.TimeSlice.None);
		float amount = this.target.GetMyWorld().worldInventory.GetAmount(ingredient.material, true);
		string formattedByTag2 = GameUtil.GetFormattedByTag(ingredient.material, amount, GameUtil.TimeSlice.None);
		hasEnoughMaterial = amount >= ingredient.amount;
		string text = GameUtil.SafeStringFormat(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_REQUIREMENT, new object[]
		{
			prefab.GetProperName(),
			formattedByTag
		});
		text += "\n";
		if (hasEnoughMaterial)
		{
			text = text + "<size=12>" + GameUtil.SafeStringFormat(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_AVAILABLE, new object[] { formattedByTag2 }) + "</size>";
		}
		else
		{
			text = text + "<size=12><color=#E68280>" + GameUtil.SafeStringFormat(UI.UISIDESCREENS.FABRICATORSIDESCREEN.RECIPE_AVAILABLE, new object[] { formattedByTag2 }) + "</color></size>";
		}
		return text;
	}

	// Token: 0x04004F1C RID: 20252
	public Image recipeIcon;

	// Token: 0x04004F1D RID: 20253
	public LocText recipeName;

	// Token: 0x04004F1E RID: 20254
	public LocText recipeMainDescription;

	// Token: 0x04004F1F RID: 20255
	public LocText recipeDuration;

	// Token: 0x04004F20 RID: 20256
	public ToolTip recipeDurationTooltip;

	// Token: 0x04004F21 RID: 20257
	public GameObject IngredientsDescriptorPanel;

	// Token: 0x04004F22 RID: 20258
	public GameObject radboltSpacer;

	// Token: 0x04004F23 RID: 20259
	public GameObject radboltHeader;

	// Token: 0x04004F24 RID: 20260
	public GameObject RadboltDescriptorPanel;

	// Token: 0x04004F25 RID: 20261
	public LocText radboltLabel;

	// Token: 0x04004F26 RID: 20262
	public GameObject EffectsDescriptorPanel;

	// Token: 0x04004F27 RID: 20263
	public KNumberInputField QueueCount;

	// Token: 0x04004F28 RID: 20264
	public MultiToggle DecrementButton;

	// Token: 0x04004F29 RID: 20265
	public MultiToggle IncrementButton;

	// Token: 0x04004F2A RID: 20266
	public KButton InfiniteButton;

	// Token: 0x04004F2B RID: 20267
	public GameObject InfiniteIcon;

	// Token: 0x04004F2C RID: 20268
	public GameObject ResearchRequiredContainer;

	// Token: 0x04004F2D RID: 20269
	public GameObject UndiscoveredMaterialsContainer;

	// Token: 0x04004F2E RID: 20270
	[SerializeField]
	private GameObject materialFilterRowPrefab;

	// Token: 0x04004F2F RID: 20271
	[SerializeField]
	private GameObject materialSelectionContainerPrefab;

	// Token: 0x04004F30 RID: 20272
	private List<GameObject> materialSelectionContainers = new List<GameObject>();

	// Token: 0x04004F31 RID: 20273
	private Dictionary<GameObject, List<GameObject>> materialSelectionRowsByContainer = new Dictionary<GameObject, List<GameObject>>();

	// Token: 0x04004F32 RID: 20274
	private ComplexFabricator target;

	// Token: 0x04004F33 RID: 20275
	private ComplexFabricatorSideScreen ownerScreen;

	// Token: 0x04004F34 RID: 20276
	private List<Tag> selectedMaterialOption = new List<Tag>();

	// Token: 0x04004F35 RID: 20277
	private string selectedRecipeCategoryID;

	// Token: 0x04004F36 RID: 20278
	[SerializeField]
	private GameObject recipeElementDescriptorPrefab;

	// Token: 0x04004F37 RID: 20279
	private Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> recipeIngredientDescriptorRows = new Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject>();

	// Token: 0x04004F38 RID: 20280
	private Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject> recipeEffectsDescriptorRows = new Dictionary<SelectedRecipeQueueScreen.DescriptorWithSprite, GameObject>();

	// Token: 0x04004F39 RID: 20281
	[SerializeField]
	private FullBodyUIMinionWidget minionWidget;

	// Token: 0x04004F3A RID: 20282
	[SerializeField]
	private MultiToggle previousRecipeButton;

	// Token: 0x04004F3B RID: 20283
	[SerializeField]
	private MultiToggle nextRecipeButton;

	// Token: 0x04004F3C RID: 20284
	[SerializeField]
	private LayoutElement scrollContainer;

	// Token: 0x04004F3D RID: 20285
	private int cycleRecipeVariantIdx;

	// Token: 0x02002031 RID: 8241
	private class DescriptorWithSprite
	{
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600B581 RID: 46465 RVA: 0x003DFB6F File Offset: 0x003DDD6F
		public Descriptor descriptor { get; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600B582 RID: 46466 RVA: 0x003DFB77 File Offset: 0x003DDD77
		public global::Tuple<Sprite, Color> tintedSprite { get; }

		// Token: 0x0600B583 RID: 46467 RVA: 0x003DFB7F File Offset: 0x003DDD7F
		public DescriptorWithSprite(Descriptor desc, global::Tuple<Sprite, Color> sprite, bool filterRowVisible = false)
		{
			this.descriptor = desc;
			this.tintedSprite = sprite;
			this.showFilterRow = filterRowVisible;
		}

		// Token: 0x04009353 RID: 37715
		public bool showFilterRow;
	}
}
