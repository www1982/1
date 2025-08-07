using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000D8E RID: 3470
public class OutfitDesignerScreen : KMonoBehaviour
{
	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x06006C0C RID: 27660 RVA: 0x0028C7D5 File Offset: 0x0028A9D5
	// (set) Token: 0x06006C0D RID: 27661 RVA: 0x0028C7DD File Offset: 0x0028A9DD
	public OutfitDesignerScreenConfig Config { get; private set; }

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x06006C0E RID: 27662 RVA: 0x0028C7E6 File Offset: 0x0028A9E6
	// (set) Token: 0x06006C0F RID: 27663 RVA: 0x0028C7EE File Offset: 0x0028A9EE
	public PermitResource SelectedPermit { get; private set; }

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x06006C10 RID: 27664 RVA: 0x0028C7F7 File Offset: 0x0028A9F7
	// (set) Token: 0x06006C11 RID: 27665 RVA: 0x0028C7FF File Offset: 0x0028A9FF
	public PermitCategory SelectedCategory { get; private set; }

	// Token: 0x17000792 RID: 1938
	// (get) Token: 0x06006C12 RID: 27666 RVA: 0x0028C808 File Offset: 0x0028AA08
	// (set) Token: 0x06006C13 RID: 27667 RVA: 0x0028C810 File Offset: 0x0028AA10
	public OutfitDesignerScreen_OutfitState outfitState { get; private set; }

	// Token: 0x06006C14 RID: 27668 RVA: 0x0028C81C File Offset: 0x0028AA1C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(this.categoryRowPrefab.transform.parent == this.categoryListContent.transform);
		global::Debug.Assert(this.gridItemPrefab.transform.parent == this.galleryGridContent.transform);
		global::Debug.Assert(this.subcategoryUiPrefab.transform.parent == this.galleryGridContent.transform);
		this.categoryRowPrefab.SetActive(false);
		this.gridItemPrefab.SetActive(false);
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayouts = this.galleryGridContent.GetComponents<GridLayoutGroup>().ToList<GridLayoutGroup>()
		};
		this.galleryGridLayouter.overrideParentForSizeReference = this.galleryGridContent;
		this.categoryRowPool = new UIPrefabLocalPool(this.categoryRowPrefab, this.categoryListContent.gameObject);
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
		this.subcategoryUiPool = new UIPrefabLocalPool(this.subcategoryUiPrefab, this.galleryGridContent.gameObject);
		if (OutfitDesignerScreen.outfitTypeToCategoriesDict == null)
		{
			Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]> dictionary = new Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]>();
			dictionary[ClothingOutfitUtility.OutfitType.Clothing] = ClothingOutfitUtility.PERMIT_CATEGORIES_FOR_CLOTHING;
			dictionary[ClothingOutfitUtility.OutfitType.AtmoSuit] = ClothingOutfitUtility.PERMIT_CATEGORIES_FOR_ATMO_SUITS;
			OutfitDesignerScreen.outfitTypeToCategoriesDict = dictionary;
		}
		InventoryOrganization.Initialize();
	}

	// Token: 0x06006C15 RID: 27669 RVA: 0x0028C980 File Offset: 0x0028AB80
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06006C16 RID: 27670 RVA: 0x0028C98D File Offset: 0x0028AB8D
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		this.minionOrMannequin.TrySpawn();
		if (!this.Config.isValid)
		{
			throw new NotSupportedException("Cannot open OutfitDesignerScreen without a config. Make sure to call Configure() before enabling the screen");
		}
		this.Configure(this.Config);
	}

	// Token: 0x06006C17 RID: 27671 RVA: 0x0028C9C6 File Offset: 0x0028ABC6
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshCategories();
			this.RefreshGallery();
			this.RefreshOutfitState();
		});
	}

	// Token: 0x06006C18 RID: 27672 RVA: 0x0028C9E5 File Offset: 0x0028ABE5
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.UnregisterPreventScreenPop();
	}

	// Token: 0x06006C19 RID: 27673 RVA: 0x0028C9F3 File Offset: 0x0028ABF3
	private void UpdateSaveButtons()
	{
		if (this.updateSaveButtonsFn != null)
		{
			this.updateSaveButtonsFn();
		}
	}

	// Token: 0x06006C1A RID: 27674 RVA: 0x0028CA08 File Offset: 0x0028AC08
	public void Configure(OutfitDesignerScreenConfig config)
	{
		this.Config = config;
		if (config.targetMinionInstance.HasValue)
		{
			this.outfitState = OutfitDesignerScreen_OutfitState.ForMinionInstance(this.Config.sourceTarget, config.targetMinionInstance.Value);
		}
		else
		{
			this.outfitState = OutfitDesignerScreen_OutfitState.ForTemplateOutfit(this.Config.sourceTarget);
		}
		if (this.postponeConfiguration)
		{
			return;
		}
		this.RegisterPreventScreenPop();
		this.minionOrMannequin.SetFrom(config.minionPersonality).SpawnedAvatar.GetComponent<WearableAccessorizer>();
		using (ListPool<ClothingItemResource, OutfitDesignerScreen>.PooledList pooledList = PoolsFor<OutfitDesignerScreen>.AllocateList<ClothingItemResource>())
		{
			this.outfitState.AddItemValuesTo(pooledList);
			this.minionOrMannequin.SetFrom(config.minionPersonality).SetOutfit(config.sourceTarget.OutfitType, pooledList);
		}
		this.PopulateCategories();
		this.SelectCategory(OutfitDesignerScreen.outfitTypeToCategoriesDict[this.outfitState.outfitType][0]);
		this.galleryGridLayouter.RequestGridResize();
		this.RefreshOutfitState();
		OutfitDesignerScreenConfig outfitDesignerScreenConfig = this.Config;
		if (outfitDesignerScreenConfig.targetMinionInstance.HasValue)
		{
			this.updateSaveButtonsFn = null;
			this.primaryButton.ClearOnClick();
			TMP_Text componentInChildren = this.primaryButton.GetComponentInChildren<LocText>();
			LocString button_APPLY_TO_MINION = UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.BUTTON_APPLY_TO_MINION;
			string text = "{MinionName}";
			outfitDesignerScreenConfig = this.Config;
			componentInChildren.SetText(button_APPLY_TO_MINION.Replace(text, outfitDesignerScreenConfig.targetMinionInstance.Value.GetProperName()));
			this.primaryButton.onClick += delegate
			{
				OutfitDesignerScreenConfig outfitDesignerScreenConfig2 = this.Config;
				ClothingOutfitUtility.OutfitType outfitType = outfitDesignerScreenConfig2.sourceTarget.OutfitType;
				outfitDesignerScreenConfig2 = this.Config;
				ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.FromMinion(outfitType, outfitDesignerScreenConfig2.targetMinionInstance.Value);
				outfitDesignerScreenConfig2 = this.Config;
				clothingOutfitTarget.WriteItems(outfitDesignerScreenConfig2.sourceTarget.OutfitType, this.outfitState.GetItems());
				if (this.Config.onWriteToOutfitTargetFn != null)
				{
					this.Config.onWriteToOutfitTargetFn(clothingOutfitTarget);
				}
				LockerNavigator.Instance.PopScreen();
			};
			this.secondaryButton.ClearOnClick();
			this.secondaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.BUTTON_APPLY_TO_TEMPLATE);
			this.secondaryButton.onClick += delegate
			{
				OutfitDesignerScreen.MakeApplyToTemplatePopup(this.inputFieldPrefab, this.outfitState, this.Config.targetMinionInstance.Value, this.Config.outfitTemplate, this.Config.onWriteToOutfitTargetFn);
			};
			this.updateSaveButtonsFn = (global::System.Action)Delegate.Combine(this.updateSaveButtonsFn, new global::System.Action(delegate
			{
				if (this.outfitState.DoesContainLockedItems())
				{
					this.primaryButton.isInteractable = false;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					this.secondaryButton.isInteractable = false;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
					return;
				}
				this.primaryButton.isInteractable = true;
				this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
				this.secondaryButton.isInteractable = true;
				this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
			}));
		}
		else
		{
			outfitDesignerScreenConfig = this.Config;
			if (!outfitDesignerScreenConfig.outfitTemplate.HasValue)
			{
				throw new NotSupportedException();
			}
			this.updateSaveButtonsFn = null;
			this.primaryButton.ClearOnClick();
			this.primaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.BUTTON_SAVE);
			this.primaryButton.onClick += delegate
			{
				this.outfitState.destinationTarget.WriteName(this.outfitState.name);
				this.outfitState.destinationTarget.WriteItems(this.outfitState.outfitType, this.outfitState.GetItems());
				OutfitDesignerScreenConfig outfitDesignerScreenConfig3 = this.Config;
				if (outfitDesignerScreenConfig3.minionPersonality.HasValue)
				{
					outfitDesignerScreenConfig3 = this.Config;
					outfitDesignerScreenConfig3.minionPersonality.Value.SetSelectedTemplateOutfitId(this.outfitState.destinationTarget.OutfitType, this.outfitState.destinationTarget.OutfitId);
				}
				if (this.Config.onWriteToOutfitTargetFn != null)
				{
					this.Config.onWriteToOutfitTargetFn(this.outfitState.destinationTarget);
				}
				LockerNavigator.Instance.PopScreen();
			};
			this.secondaryButton.ClearOnClick();
			this.secondaryButton.GetComponentInChildren<LocText>().SetText(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.BUTTON_COPY);
			this.secondaryButton.onClick += delegate
			{
				OutfitDesignerScreen.MakeCopyPopup(this, this.inputFieldPrefab, this.outfitState, this.Config.outfitTemplate.Value, this.Config.minionPersonality, this.Config.onWriteToOutfitTargetFn);
			};
			this.updateSaveButtonsFn = (global::System.Action)Delegate.Combine(this.updateSaveButtonsFn, new global::System.Action(delegate
			{
				if (!this.outfitState.destinationTarget.CanWriteItems)
				{
					this.primaryButton.isInteractable = false;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_READONLY);
					if (this.outfitState.DoesContainLockedItems())
					{
						this.secondaryButton.isInteractable = false;
						this.secondaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
						return;
					}
					this.secondaryButton.isInteractable = true;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
					return;
				}
				else
				{
					if (this.outfitState.DoesContainLockedItems())
					{
						this.primaryButton.isInteractable = false;
						this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
						this.secondaryButton.isInteractable = false;
						this.secondaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(UI.OUTFIT_DESIGNER_SCREEN.OUTFIT_TEMPLATE.TOOLTIP_SAVE_ERROR_LOCKED);
						return;
					}
					this.primaryButton.isInteractable = true;
					this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
					this.secondaryButton.isInteractable = true;
					this.secondaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
					return;
				}
			}));
		}
		this.UpdateSaveButtons();
	}

	// Token: 0x06006C1B RID: 27675 RVA: 0x0028CCB8 File Offset: 0x0028AEB8
	private void RefreshOutfitState()
	{
		this.selectionHeaderLabel.text = this.outfitState.name;
		this.outfitDescriptionPanel.Refresh(this.outfitState, this.Config.minionPersonality);
		this.UpdateSaveButtons();
	}

	// Token: 0x06006C1C RID: 27676 RVA: 0x0028CCF2 File Offset: 0x0028AEF2
	private void RefreshCategories()
	{
		if (this.RefreshCategoriesFn != null)
		{
			this.RefreshCategoriesFn();
		}
	}

	// Token: 0x06006C1D RID: 27677 RVA: 0x0028CD08 File Offset: 0x0028AF08
	public void PopulateCategories()
	{
		this.RefreshCategoriesFn = null;
		this.categoryRowPool.ReturnAll();
		PermitCategory[] array = OutfitDesignerScreen.outfitTypeToCategoriesDict[this.outfitState.outfitType];
		for (int i = 0; i < array.Length; i++)
		{
			OutfitDesignerScreen.<>c__DisplayClass47_0 CS$<>8__locals1 = new OutfitDesignerScreen.<>c__DisplayClass47_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.permitCategory = array[i];
			GameObject gameObject = this.categoryRowPool.Borrow();
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(PermitCategories.GetUppercaseDisplayName(CS$<>8__locals1.permitCategory));
			component.GetReference<Image>("Icon").sprite = Assets.GetSprite(PermitCategories.GetIconName(CS$<>8__locals1.permitCategory));
			MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
			MultiToggle toggle2 = toggle;
			toggle2.onEnter = (global::System.Action)Delegate.Combine(toggle2.onEnter, new global::System.Action(this.OnMouseOverToggle));
			toggle.onClick = delegate
			{
				CS$<>8__locals1.<>4__this.SelectCategory(CS$<>8__locals1.permitCategory);
			};
			this.RefreshCategoriesFn = (global::System.Action)Delegate.Combine(this.RefreshCategoriesFn, new global::System.Action(delegate
			{
				toggle.ChangeState((CS$<>8__locals1.permitCategory == CS$<>8__locals1.<>4__this.SelectedCategory) ? 1 : 0);
			}));
			this.SetCatogoryClickUISound(CS$<>8__locals1.permitCategory, toggle);
		}
	}

	// Token: 0x06006C1E RID: 27678 RVA: 0x0028CE5C File Offset: 0x0028B05C
	public void SelectCategory(PermitCategory permitCategory)
	{
		this.SelectedCategory = permitCategory;
		this.galleryHeaderLabel.text = PermitCategories.GetDisplayName(permitCategory);
		this.RefreshCategories();
		this.PopulateGallery();
		Option<ClothingItemResource> itemForCategory = this.outfitState.GetItemForCategory(permitCategory);
		if (itemForCategory.HasValue)
		{
			this.SelectPermit(itemForCategory.Value);
			return;
		}
		this.SelectPermit(null);
	}

	// Token: 0x06006C1F RID: 27679 RVA: 0x0028CEB8 File Offset: 0x0028B0B8
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06006C20 RID: 27680 RVA: 0x0028CED0 File Offset: 0x0028B0D0
	public void PopulateGallery()
	{
		OutfitDesignerScreen.<>c__DisplayClass51_0 CS$<>8__locals1 = new OutfitDesignerScreen.<>c__DisplayClass51_0();
		CS$<>8__locals1.<>4__this = this;
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		this.subcategoryUiPool.ReturnAll();
		this.galleryGridLayouter.targetGridLayouts.Clear();
		this.galleryGridLayouter.OnSizeGridComplete = null;
		CS$<>8__locals1.onFirstDisplayCategoryDecided = new Promise<KleiInventoryUISubcategory>();
		CS$<>8__locals1.<PopulateGallery>g__AddGridIconForPermit|0(null);
		foreach (ClothingItemResource clothingItemResource in Db.Get().Permits.ClothingItems.resources)
		{
			if (clothingItemResource.Category == this.SelectedCategory && clothingItemResource.outfitType == this.Config.sourceTarget.OutfitType && !clothingItemResource.Id.StartsWith("visonly_"))
			{
				CS$<>8__locals1.<PopulateGallery>g__AddGridIconForPermit|0(clothingItemResource);
			}
		}
		foreach (GameObject gameObject3 in this.subcategoryUiPool.GetBorrowedObjects().StableSort(Comparer<GameObject>.Create(delegate(GameObject a, GameObject b)
		{
			KleiInventoryUISubcategory component = a.GetComponent<KleiInventoryUISubcategory>();
			KleiInventoryUISubcategory component2 = b.GetComponent<KleiInventoryUISubcategory>();
			int sortKey = InventoryOrganization.subcategoryIdToPresentationDataMap[component.subcategoryID].sortKey;
			int sortKey2 = InventoryOrganization.subcategoryIdToPresentationDataMap[component2.subcategoryID].sortKey;
			return sortKey.CompareTo(sortKey2);
		})))
		{
			gameObject3.transform.SetAsLastSibling();
		}
		GameObject gameObject2 = this.subcategoryUiPool.GetBorrowedObjects().FirstOrDefault((GameObject gameObject) => gameObject.GetComponent<KleiInventoryUISubcategory>().IsOpen);
		if (gameObject2 != null)
		{
			CS$<>8__locals1.onFirstDisplayCategoryDecided.Resolve(gameObject2.GetComponent<KleiInventoryUISubcategory>());
		}
		this.galleryGridLayouter.RequestGridResize();
		this.RefreshGallery();
	}

	// Token: 0x06006C21 RID: 27681 RVA: 0x0028D094 File Offset: 0x0028B294
	public void SelectPermit(PermitResource permit)
	{
		this.SelectedPermit = permit;
		this.RefreshGallery();
		this.UpdateSelectedItemDetails();
		this.UpdateSaveButtons();
	}

	// Token: 0x06006C22 RID: 27682 RVA: 0x0028D0B0 File Offset: 0x0028B2B0
	public void UpdateSelectedItemDetails()
	{
		Option<ClothingItemResource> option = Option.None;
		if (this.SelectedPermit != null)
		{
			ClothingItemResource clothingItemResource = this.SelectedPermit as ClothingItemResource;
			if (clothingItemResource != null)
			{
				option = clothingItemResource;
			}
		}
		this.outfitState.SetItemForCategory(this.SelectedCategory, option);
		this.minionOrMannequin.current.SetOutfit(this.outfitState);
		this.minionOrMannequin.current.ReactToClothingItemChange(this.SelectedCategory);
		this.outfitDescriptionPanel.Refresh(this.outfitState, this.Config.minionPersonality);
		this.dioramaBG.sprite = KleiPermitDioramaVis.GetDioramaBackground(this.SelectedCategory);
	}

	// Token: 0x06006C23 RID: 27683 RVA: 0x0028D156 File Offset: 0x0028B356
	private void RegisterPreventScreenPop()
	{
		this.UnregisterPreventScreenPop();
		this.preventScreenPopFn = delegate
		{
			if (this.outfitState.IsDirty())
			{
				this.RegisterPreventScreenPop();
				OutfitDesignerScreen.MakeSaveWarningPopup(this.outfitState, delegate
				{
					this.UnregisterPreventScreenPop();
					LockerNavigator.Instance.PopScreen();
				});
				return true;
			}
			return false;
		};
		LockerNavigator.Instance.preventScreenPop.Add(this.preventScreenPopFn);
	}

	// Token: 0x06006C24 RID: 27684 RVA: 0x0028D185 File Offset: 0x0028B385
	private void UnregisterPreventScreenPop()
	{
		if (this.preventScreenPopFn != null)
		{
			LockerNavigator.Instance.preventScreenPop.Remove(this.preventScreenPopFn);
			this.preventScreenPopFn = null;
		}
	}

	// Token: 0x06006C25 RID: 27685 RVA: 0x0028D1AC File Offset: 0x0028B3AC
	public static void MakeSaveWarningPopup(OutfitDesignerScreen_OutfitState outfitState, global::System.Action discardChangesFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.HEADER.Replace("{OutfitName}", outfitState.name)).AddPlainText(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BODY);
			string text = UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BUTTON_DISCARD;
			Action<InfoDialogScreen> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(InfoDialogScreen d)
				{
					d.Deactivate();
					discardChangesFn();
				});
			}
			infoDialogScreen.AddOption(text, action, true).AddOption(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BUTTON_RETURN, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, false);
		});
	}

	// Token: 0x06006C26 RID: 27686 RVA: 0x0028D1E4 File Offset: 0x0028B3E4
	public static void MakeApplyToTemplatePopup(KInputTextField inputFieldPrefab, OutfitDesignerScreen_OutfitState outfitState, GameObject targetMinionInstance, Option<ClothingOutfitTarget> existingOutfitTemplate, Action<ClothingOutfitTarget> onWriteToOutfitTargetFn)
	{
		ClothingOutfitNameProposal proposal = default(ClothingOutfitNameProposal);
		Color errorTextColor = Util.ColorFromHex("F44A47");
		Color defaultTextColor = Util.ColorFromHex("FFFFFF");
		KInputTextField inputField;
		InfoScreenPlainText descLabel;
		KButton saveButton;
		LocText saveButtonText;
		LocText descLocText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.MINION_INSTANCE.APPLY_TEMPLATE_POPUP.HEADER.Replace("{OutfitName}", outfitState.name)).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out descLabel)
				.AddOption(true, out saveButton, out saveButtonText)
				.AddDefaultCancel();
			descLocText = descLabel.gameObject.GetComponent<LocText>();
			descLocText.allowOverride = true;
			descLocText.alignment = TextAlignmentOptions.BottomLeft;
			descLocText.color = errorTextColor;
			descLocText.fontSize = 14f;
			descLabel.SetText("");
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeApplyToTemplatePopup>g__Refresh|1));
			saveButton.onClick += delegate
			{
				ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.FromMinion(outfitState.outfitType, targetMinionInstance);
				ClothingOutfitNameProposal.Result result = proposal.result;
				ClothingOutfitTarget clothingOutfitTarget2;
				if (result != ClothingOutfitNameProposal.Result.NewOutfit)
				{
					if (result != ClothingOutfitNameProposal.Result.SameOutfit)
					{
						throw new NotSupportedException(string.Format("Can't save outfit with name \"{0}\", failed with result: {1}", proposal.candidateName, proposal.result));
					}
					clothingOutfitTarget2 = existingOutfitTemplate.Value;
				}
				else
				{
					clothingOutfitTarget2 = ClothingOutfitTarget.ForNewTemplateOutfit(outfitState.outfitType, proposal.candidateName);
				}
				clothingOutfitTarget2.WriteItems(outfitState.outfitType, outfitState.GetItems());
				clothingOutfitTarget.WriteItems(outfitState.outfitType, outfitState.GetItems());
				if (onWriteToOutfitTargetFn != null)
				{
					onWriteToOutfitTargetFn(clothingOutfitTarget2);
				}
				dialog.Deactivate();
				LockerNavigator.Instance.PopScreen();
			};
			if (!existingOutfitTemplate.HasValue)
			{
				base.<MakeApplyToTemplatePopup>g__Refresh|1(outfitState.name);
				return;
			}
			if (existingOutfitTemplate.Value.CanWriteName && existingOutfitTemplate.Value.CanWriteItems)
			{
				base.<MakeApplyToTemplatePopup>g__Refresh|1(existingOutfitTemplate.Value.OutfitId);
				return;
			}
			base.<MakeApplyToTemplatePopup>g__Refresh|1(ClothingOutfitTarget.ForTemplateCopyOf(existingOutfitTemplate.Value).OutfitId);
		});
	}

	// Token: 0x06006C27 RID: 27687 RVA: 0x0028D260 File Offset: 0x0028B460
	public static void MakeCopyPopup(OutfitDesignerScreen screen, KInputTextField inputFieldPrefab, OutfitDesignerScreen_OutfitState outfitState, ClothingOutfitTarget outfitTemplate, Option<Personality> minionPersonality, Action<ClothingOutfitTarget> onWriteToOutfitTargetFn)
	{
		ClothingOutfitNameProposal proposal = default(ClothingOutfitNameProposal);
		KInputTextField inputField;
		InfoScreenPlainText errorText;
		KButton okButton;
		LocText okButtonText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_DESIGNER_SCREEN.COPY_POPUP.HEADER).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out errorText)
				.AddOption(true, out okButton, out okButtonText)
				.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
				{
					d.Deactivate();
				}, false);
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeCopyPopup>g__Refresh|1));
			errorText.gameObject.SetActive(false);
			LocText component = errorText.gameObject.GetComponent<LocText>();
			component.allowOverride = true;
			component.alignment = TextAlignmentOptions.BottomLeft;
			component.color = Util.ColorFromHex("F44A47");
			component.fontSize = 14f;
			errorText.SetText("");
			okButtonText.text = UI.CONFIRMDIALOG.OK;
			okButton.onClick += delegate
			{
				if (proposal.result == ClothingOutfitNameProposal.Result.NewOutfit)
				{
					ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.ForNewTemplateOutfit(outfitTemplate.OutfitType, proposal.candidateName);
					clothingOutfitTarget.WriteItems(outfitState.outfitType, outfitState.GetItems());
					if (minionPersonality.HasValue)
					{
						minionPersonality.Value.SetSelectedTemplateOutfitId(clothingOutfitTarget.OutfitType, clothingOutfitTarget.OutfitId);
					}
					if (onWriteToOutfitTargetFn != null)
					{
						onWriteToOutfitTargetFn(clothingOutfitTarget);
					}
					dialog.Deactivate();
					screen.Configure(screen.Config.WithOutfit(clothingOutfitTarget));
					return;
				}
				throw new NotSupportedException(string.Format("Can't save outfit with name \"{0}\", failed with result: {1}", proposal.candidateName, proposal.result));
			};
			base.<MakeCopyPopup>g__Refresh|1(ClothingOutfitTarget.ForTemplateCopyOf(outfitTemplate).OutfitId);
		});
	}

	// Token: 0x06006C28 RID: 27688 RVA: 0x0028D2C4 File Offset: 0x0028B4C4
	private void SetCatogoryClickUISound(PermitCategory category, MultiToggle toggle)
	{
		toggle.states[1].on_click_override_sound_path = category.ToString() + "_Click";
		toggle.states[0].on_click_override_sound_path = category.ToString() + "_Click";
	}

	// Token: 0x06006C29 RID: 27689 RVA: 0x0028D324 File Offset: 0x0028B524
	private void SetItemClickUISound(PermitResource permit, MultiToggle toggle)
	{
		if (permit == null)
		{
			toggle.states[1].on_click_override_sound_path = "HUD_Click";
			toggle.states[0].on_click_override_sound_path = "HUD_Click";
			return;
		}
		string clothingItemSoundName = OutfitDesignerScreen.GetClothingItemSoundName(permit);
		toggle.states[1].on_click_override_sound_path = clothingItemSoundName + "_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = clothingItemSoundName + "_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x06006C2A RID: 27690 RVA: 0x0028D43C File Offset: 0x0028B63C
	public static string GetClothingItemSoundName(PermitResource permit)
	{
		if (permit == null)
		{
			return "HUD";
		}
		switch (permit.Category)
		{
		case PermitCategory.DupeTops:
			return "tops";
		case PermitCategory.DupeBottoms:
			return "bottoms";
		case PermitCategory.DupeGloves:
			return "gloves";
		case PermitCategory.DupeShoes:
			return "shoes";
		case PermitCategory.DupeHats:
			return "hats";
		default:
			return "HUD";
		}
	}

	// Token: 0x06006C2B RID: 27691 RVA: 0x0028D49D File Offset: 0x0028B69D
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x040049A8 RID: 18856
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x040049A9 RID: 18857
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x040049AA RID: 18858
	private UIPrefabLocalPool categoryRowPool;

	// Token: 0x040049AB RID: 18859
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x040049AC RID: 18860
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x040049AD RID: 18861
	[SerializeField]
	private GameObject subcategoryUiPrefab;

	// Token: 0x040049AE RID: 18862
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x040049AF RID: 18863
	private UIPrefabLocalPool subcategoryUiPool;

	// Token: 0x040049B0 RID: 18864
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x040049B1 RID: 18865
	private GridLayouter galleryGridLayouter;

	// Token: 0x040049B2 RID: 18866
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x040049B3 RID: 18867
	[SerializeField]
	private UIMinionOrMannequin minionOrMannequin;

	// Token: 0x040049B4 RID: 18868
	[SerializeField]
	private Image dioramaBG;

	// Token: 0x040049B5 RID: 18869
	[SerializeField]
	private KButton primaryButton;

	// Token: 0x040049B6 RID: 18870
	[SerializeField]
	private KButton secondaryButton;

	// Token: 0x040049B7 RID: 18871
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x040049B8 RID: 18872
	[SerializeField]
	private KInputTextField inputFieldPrefab;

	// Token: 0x040049BD RID: 18877
	public static Dictionary<ClothingOutfitUtility.OutfitType, PermitCategory[]> outfitTypeToCategoriesDict;

	// Token: 0x040049BE RID: 18878
	private bool postponeConfiguration = true;

	// Token: 0x040049BF RID: 18879
	private global::System.Action updateSaveButtonsFn;

	// Token: 0x040049C0 RID: 18880
	private global::System.Action RefreshCategoriesFn;

	// Token: 0x040049C1 RID: 18881
	private global::System.Action RefreshGalleryFn;

	// Token: 0x040049C2 RID: 18882
	private Func<bool> preventScreenPopFn;

	// Token: 0x02001F7E RID: 8062
	private enum MultiToggleState
	{
		// Token: 0x04009107 RID: 37127
		Default,
		// Token: 0x04009108 RID: 37128
		Selected,
		// Token: 0x04009109 RID: 37129
		NonInteractable
	}
}
