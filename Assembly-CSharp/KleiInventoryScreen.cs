using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CFE RID: 3326
public class KleiInventoryScreen : KModalScreen
{
	// Token: 0x17000765 RID: 1893
	// (get) Token: 0x06006683 RID: 26243 RVA: 0x0026AFAB File Offset: 0x002691AB
	// (set) Token: 0x06006684 RID: 26244 RVA: 0x0026AFB3 File Offset: 0x002691B3
	private PermitResource SelectedPermit { get; set; }

	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x06006685 RID: 26245 RVA: 0x0026AFBC File Offset: 0x002691BC
	// (set) Token: 0x06006686 RID: 26246 RVA: 0x0026AFC4 File Offset: 0x002691C4
	private string SelectedCategoryId { get; set; }

	// Token: 0x06006687 RID: 26247 RVA: 0x0026AFD0 File Offset: 0x002691D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.closeButton.onClick += delegate
		{
			this.Show(false);
		};
		base.ConsumeMouseScroll = true;
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayouts = new List<GridLayoutGroup>()
		};
		this.galleryGridLayouter.overrideParentForSizeReference = this.galleryGridContent;
		InventoryOrganization.Initialize();
	}

	// Token: 0x06006688 RID: 26248 RVA: 0x0026B043 File Offset: 0x00269243
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.TryConsume(global::Action.Escape) || e.TryConsume(global::Action.MouseRight))
		{
			this.Show(false);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006689 RID: 26249 RVA: 0x0026B065 File Offset: 0x00269265
	public override float GetSortKey()
	{
		return 20f;
	}

	// Token: 0x0600668A RID: 26250 RVA: 0x0026B06C File Offset: 0x0026926C
	protected override void OnActivate()
	{
		this.OnShow(true);
	}

	// Token: 0x0600668B RID: 26251 RVA: 0x0026B075 File Offset: 0x00269275
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			this.InitConfig();
			this.ToggleDoublesOnly(0);
			this.ClearSearch();
		}
	}

	// Token: 0x0600668C RID: 26252 RVA: 0x0026B094 File Offset: 0x00269294
	private void ToggleDoublesOnly(int newState)
	{
		this.showFilterState = newState;
		this.doublesOnlyToggle.ChangeState(this.showFilterState);
		this.doublesOnlyToggle.GetComponentInChildren<LocText>().text = this.showFilterState.ToString() + "+";
		string text = "";
		switch (this.showFilterState)
		{
		case 0:
			text = UI.KLEI_INVENTORY_SCREEN.TOOLTIP_VIEW_ALL_ITEMS;
			break;
		case 1:
			text = UI.KLEI_INVENTORY_SCREEN.TOOLTIP_VIEW_OWNED_ONLY;
			break;
		case 2:
			text = UI.KLEI_INVENTORY_SCREEN.TOOLTIP_VIEW_DOUBLES_ONLY;
			break;
		}
		ToolTip component = this.doublesOnlyToggle.GetComponent<ToolTip>();
		component.SetSimpleTooltip(text);
		component.refreshWhileHovering = true;
		component.forceRefresh = true;
		this.RefreshGallery();
	}

	// Token: 0x0600668D RID: 26253 RVA: 0x0026B14C File Offset: 0x0026934C
	private void InitConfig()
	{
		if (this.initConfigComplete)
		{
			return;
		}
		this.initConfigComplete = true;
		this.galleryGridLayouter.RequestGridResize();
		this.categoryListContent.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
		this.PopulateCategories();
		this.PopulateGallery();
		this.SelectCategory("BUILDINGS");
		this.searchField.onValueChanged.RemoveAllListeners();
		this.searchField.onValueChanged.AddListener(delegate(string value)
		{
			this.RefreshGallery();
		});
		this.clearSearchButton.ClearOnClick();
		this.clearSearchButton.onClick += this.ClearSearch;
		MultiToggle multiToggle = this.doublesOnlyToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			int num = (this.showFilterState + 1) % 3;
			this.ToggleDoublesOnly(num);
		}));
	}

	// Token: 0x0600668E RID: 26254 RVA: 0x0026B21F File Offset: 0x0026941F
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.ToggleDoublesOnly(0);
		this.ClearSearch();
		if (!this.initConfigComplete)
		{
			this.InitConfig();
		}
		this.RefreshUI();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshUI();
		});
	}

	// Token: 0x0600668F RID: 26255 RVA: 0x0026B25F File Offset: 0x0026945F
	private void ClearSearch()
	{
		this.searchField.text = "";
		this.searchField.placeholder.GetComponent<TextMeshProUGUI>().text = UI.KLEI_INVENTORY_SCREEN.SEARCH_PLACEHOLDER;
		this.RefreshGallery();
	}

	// Token: 0x06006690 RID: 26256 RVA: 0x0026B296 File Offset: 0x00269496
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06006691 RID: 26257 RVA: 0x0026B2A4 File Offset: 0x002694A4
	private void RefreshUI()
	{
		this.IS_ONLINE = ThreadedHttps<KleiAccount>.Instance.HasValidTicket();
		this.RefreshCategories();
		this.RefreshGallery();
		if (this.SelectedCategoryId.IsNullOrWhiteSpace())
		{
			this.SelectCategory("BUILDINGS");
		}
		this.RefreshDetails();
		this.RefreshBarterPanel();
	}

	// Token: 0x06006692 RID: 26258 RVA: 0x0026B2F1 File Offset: 0x002694F1
	private GameObject GetAvailableGridButton()
	{
		if (this.recycledGalleryGridButtons.Count == 0)
		{
			return Util.KInstantiateUI(this.gridItemPrefab, this.galleryGridContent.gameObject, true);
		}
		GameObject gameObject = this.recycledGalleryGridButtons[0];
		this.recycledGalleryGridButtons.RemoveAt(0);
		return gameObject;
	}

	// Token: 0x06006693 RID: 26259 RVA: 0x0026B330 File Offset: 0x00269530
	private void RecycleGalleryGridButton(GameObject button)
	{
		button.GetComponent<MultiToggle>().onClick = null;
		this.recycledGalleryGridButtons.Add(button);
	}

	// Token: 0x06006694 RID: 26260 RVA: 0x0026B34C File Offset: 0x0026954C
	public void PopulateCategories()
	{
		foreach (KeyValuePair<string, MultiToggle> keyValuePair in this.categoryToggles)
		{
			global::UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
		}
		this.categoryToggles.Clear();
		foreach (KeyValuePair<string, List<string>> keyValuePair2 in InventoryOrganization.categoryIdToSubcategoryIdsMap)
		{
			string text;
			List<string> list;
			keyValuePair2.Deconstruct(out text, out list);
			string categoryId = text;
			GameObject gameObject = Util.KInstantiateUI(this.categoryRowPrefab, this.categoryListContent.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(InventoryOrganization.GetCategoryName(categoryId));
			component.GetReference<Image>("Icon").sprite = InventoryOrganization.categoryIdToIconMap[categoryId];
			MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
			MultiToggle multiToggle = component2;
			multiToggle.onEnter = (global::System.Action)Delegate.Combine(multiToggle.onEnter, new global::System.Action(this.OnMouseOverToggle));
			component2.onClick = delegate
			{
				this.SelectCategory(categoryId);
			};
			this.categoryToggles.Add(categoryId, component2);
			this.SetCatogoryClickUISound(categoryId, component2);
		}
	}

	// Token: 0x06006695 RID: 26261 RVA: 0x0026B4CC File Offset: 0x002696CC
	public void PopulateGallery()
	{
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			this.RecycleGalleryGridButton(keyValuePair.Value.gameObject);
		}
		this.galleryGridButtons.Clear();
		this.galleryGridLayouter.ImmediateSizeGridToScreenResolution();
		foreach (PermitResource permitResource in Db.Get().Permits.resources)
		{
			if (!permitResource.Id.StartsWith("visonly_"))
			{
				this.AddItemToGallery(permitResource);
			}
		}
		this.subcategories.Sort((KleiInventoryUISubcategory a, KleiInventoryUISubcategory b) => InventoryOrganization.subcategoryIdToPresentationDataMap[a.subcategoryID].sortKey.CompareTo(InventoryOrganization.subcategoryIdToPresentationDataMap[b.subcategoryID].sortKey));
		foreach (KleiInventoryUISubcategory kleiInventoryUISubcategory in this.subcategories)
		{
			kleiInventoryUISubcategory.gameObject.transform.SetAsLastSibling();
		}
		this.CollectSubcategoryGridLayouts();
		this.CloseSubcategory("UNCATEGORIZED");
	}

	// Token: 0x06006696 RID: 26262 RVA: 0x0026B624 File Offset: 0x00269824
	private void CloseSubcategory(string subcategoryID)
	{
		KleiInventoryUISubcategory kleiInventoryUISubcategory = this.subcategories.Find((KleiInventoryUISubcategory match) => match.subcategoryID == subcategoryID);
		if (kleiInventoryUISubcategory != null)
		{
			kleiInventoryUISubcategory.ToggleOpen(false);
		}
	}

	// Token: 0x06006697 RID: 26263 RVA: 0x0026B668 File Offset: 0x00269868
	private void AddItemToSubcategoryUIContainer(GameObject itemButton, string subcategoryId)
	{
		KleiInventoryUISubcategory kleiInventoryUISubcategory = this.subcategories.Find((KleiInventoryUISubcategory match) => match.subcategoryID == subcategoryId);
		if (kleiInventoryUISubcategory == null)
		{
			kleiInventoryUISubcategory = Util.KInstantiateUI(this.subcategoryPrefab, this.galleryGridContent.gameObject, true).GetComponent<KleiInventoryUISubcategory>();
			kleiInventoryUISubcategory.subcategoryID = subcategoryId;
			this.subcategories.Add(kleiInventoryUISubcategory);
			kleiInventoryUISubcategory.SetIdentity(InventoryOrganization.GetSubcategoryName(subcategoryId), InventoryOrganization.subcategoryIdToPresentationDataMap[subcategoryId].icon);
		}
		itemButton.transform.SetParent(kleiInventoryUISubcategory.gridLayout.transform);
	}

	// Token: 0x06006698 RID: 26264 RVA: 0x0026B714 File Offset: 0x00269914
	private void CollectSubcategoryGridLayouts()
	{
		this.galleryGridLayouter.OnSizeGridComplete = null;
		foreach (KleiInventoryUISubcategory kleiInventoryUISubcategory in this.subcategories)
		{
			this.galleryGridLayouter.targetGridLayouts.Add(kleiInventoryUISubcategory.gridLayout);
			GridLayouter gridLayouter = this.galleryGridLayouter;
			gridLayouter.OnSizeGridComplete = (global::System.Action)Delegate.Combine(gridLayouter.OnSizeGridComplete, new global::System.Action(kleiInventoryUISubcategory.RefreshDisplay));
		}
		this.galleryGridLayouter.RequestGridResize();
	}

	// Token: 0x06006699 RID: 26265 RVA: 0x0026B7B4 File Offset: 0x002699B4
	private void AddItemToGallery(PermitResource permit)
	{
		if (this.galleryGridButtons.ContainsKey(permit))
		{
			return;
		}
		PermitPresentationInfo permitPresentationInfo = permit.GetPermitPresentationInfo();
		GameObject availableGridButton = this.GetAvailableGridButton();
		this.AddItemToSubcategoryUIContainer(availableGridButton, InventoryOrganization.GetPermitSubcategory(permit));
		HierarchyReferences component = availableGridButton.GetComponent<HierarchyReferences>();
		Image reference = component.GetReference<Image>("Icon");
		LocText reference2 = component.GetReference<LocText>("OwnedCountLabel");
		Image reference3 = component.GetReference<Image>("IsUnownedOverlay");
		Image reference4 = component.GetReference<Image>("DlcBanner");
		MultiToggle component2 = availableGridButton.GetComponent<MultiToggle>();
		reference.sprite = permitPresentationInfo.sprite;
		if (permit.IsOwnableOnServer())
		{
			int ownedCount = PermitItems.GetOwnedCount(permit);
			reference2.text = UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT_ICON.Replace("{OwnedCount}", ownedCount.ToString());
			reference2.gameObject.SetActive(ownedCount > 0);
			reference3.gameObject.SetActive(ownedCount <= 0);
		}
		else
		{
			reference2.gameObject.SetActive(false);
			reference3.gameObject.SetActive(false);
		}
		string dlcIdFrom = permit.GetDlcIdFrom();
		if (DlcManager.IsDlcId(dlcIdFrom))
		{
			reference4.gameObject.SetActive(true);
			reference4.color = DlcManager.GetDlcBannerColor(dlcIdFrom);
		}
		else
		{
			reference4.gameObject.SetActive(false);
		}
		MultiToggle multiToggle = component2;
		multiToggle.onEnter = (global::System.Action)Delegate.Combine(multiToggle.onEnter, new global::System.Action(this.OnMouseOverToggle));
		component2.onClick = delegate
		{
			this.SelectItem(permit);
		};
		this.galleryGridButtons.Add(permit, component2);
		this.SetItemClickUISound(permit, component2);
		KleiItemsUI.ConfigureTooltipOn(availableGridButton, KleiItemsUI.GetTooltipStringFor(permit));
	}

	// Token: 0x0600669A RID: 26266 RVA: 0x0026B97F File Offset: 0x00269B7F
	public void SelectCategory(string categoryId)
	{
		if (InventoryOrganization.categoryIdToIsEmptyMap[categoryId])
		{
			return;
		}
		this.SelectedCategoryId = categoryId;
		this.galleryHeaderLabel.SetText(InventoryOrganization.GetCategoryName(categoryId));
		this.RefreshCategories();
		this.SelectDefaultCategoryItem();
	}

	// Token: 0x0600669B RID: 26267 RVA: 0x0026B9B4 File Offset: 0x00269BB4
	private void SelectDefaultCategoryItem()
	{
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			if (InventoryOrganization.categoryIdToSubcategoryIdsMap[this.SelectedCategoryId].Contains(InventoryOrganization.GetPermitSubcategory(keyValuePair.Key)))
			{
				this.SelectItem(keyValuePair.Key);
				return;
			}
		}
		this.SelectItem(null);
	}

	// Token: 0x0600669C RID: 26268 RVA: 0x0026BA3C File Offset: 0x00269C3C
	public void SelectItem(PermitResource permit)
	{
		this.SelectedPermit = permit;
		this.RefreshGallery();
		this.RefreshDetails();
		this.RefreshBarterPanel();
	}

	// Token: 0x0600669D RID: 26269 RVA: 0x0026BA58 File Offset: 0x00269C58
	private void RefreshGallery()
	{
		string text = this.searchField.text.ToUpper();
		foreach (KeyValuePair<PermitResource, MultiToggle> keyValuePair in this.galleryGridButtons)
		{
			PermitResource permitResource;
			MultiToggle multiToggle;
			keyValuePair.Deconstruct(out permitResource, out multiToggle);
			PermitResource permitResource2 = permitResource;
			MultiToggle multiToggle2 = multiToggle;
			string permitSubcategory = InventoryOrganization.GetPermitSubcategory(permitResource2);
			bool flag = permitSubcategory == "UNCATEGORIZED" || InventoryOrganization.categoryIdToSubcategoryIdsMap[this.SelectedCategoryId].Contains(permitSubcategory);
			flag = flag && (permitResource2.Name.ToUpper().Contains(text) || permitResource2.Id.ToUpper().Contains(text) || permitResource2.Description.ToUpper().Contains(text));
			multiToggle2.ChangeState((permitResource2 == this.SelectedPermit) ? 1 : 0);
			HierarchyReferences component = multiToggle2.gameObject.GetComponent<HierarchyReferences>();
			LocText reference = component.GetReference<LocText>("OwnedCountLabel");
			Image reference2 = component.GetReference<Image>("IsUnownedOverlay");
			if (permitResource2.IsOwnableOnServer())
			{
				int ownedCount = PermitItems.GetOwnedCount(permitResource2);
				reference.text = UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT_ICON.Replace("{OwnedCount}", ownedCount.ToString());
				reference.gameObject.SetActive(ownedCount > 0);
				reference2.gameObject.SetActive(ownedCount <= 0);
				if (this.showFilterState == 2 && ownedCount < 2)
				{
					flag = false;
				}
				else if (this.showFilterState == 1 && ownedCount == 0)
				{
					flag = false;
				}
			}
			else if (!permitResource2.IsUnlocked())
			{
				reference.gameObject.SetActive(false);
				reference2.gameObject.SetActive(true);
				if (this.showFilterState != 0)
				{
					flag = false;
				}
			}
			else
			{
				reference.gameObject.SetActive(false);
				reference2.gameObject.SetActive(false);
				if (this.showFilterState == 2)
				{
					flag = false;
				}
			}
			if (multiToggle2.gameObject.activeSelf != flag)
			{
				multiToggle2.gameObject.SetActive(flag);
			}
		}
		foreach (KleiInventoryUISubcategory kleiInventoryUISubcategory in this.subcategories)
		{
			kleiInventoryUISubcategory.RefreshDisplay();
		}
	}

	// Token: 0x0600669E RID: 26270 RVA: 0x0026BCBC File Offset: 0x00269EBC
	private void RefreshCategories()
	{
		foreach (KeyValuePair<string, MultiToggle> keyValuePair in this.categoryToggles)
		{
			keyValuePair.Value.ChangeState((keyValuePair.Key == this.SelectedCategoryId) ? 1 : 0);
			if (InventoryOrganization.categoryIdToIsEmptyMap[keyValuePair.Key])
			{
				keyValuePair.Value.ChangeState(2);
			}
			else
			{
				keyValuePair.Value.ChangeState((keyValuePair.Key == this.SelectedCategoryId) ? 1 : 0);
			}
		}
	}

	// Token: 0x0600669F RID: 26271 RVA: 0x0026BD74 File Offset: 0x00269F74
	private void RefreshDetails()
	{
		PermitResource selectedPermit = this.SelectedPermit;
		PermitPresentationInfo permitPresentationInfo = selectedPermit.GetPermitPresentationInfo();
		this.permitVis.ConfigureWith(selectedPermit);
		this.selectionDetailsScrollRect.rectTransform().anchorMin = new Vector2(0f, 0f);
		this.selectionDetailsScrollRect.rectTransform().anchorMax = new Vector2(1f, 1f);
		this.selectionDetailsScrollRect.rectTransform().sizeDelta = new Vector2(-24f, 0f);
		this.selectionDetailsScrollRect.rectTransform().anchoredPosition = Vector2.zero;
		this.selectionDetailsScrollRect.content.rectTransform().sizeDelta = new Vector2(0f, this.selectionDetailsScrollRect.content.rectTransform().sizeDelta.y);
		this.selectionDetailsScrollRectScrollBarContainer.anchorMin = new Vector2(1f, 0f);
		this.selectionDetailsScrollRectScrollBarContainer.anchorMax = new Vector2(1f, 1f);
		this.selectionDetailsScrollRectScrollBarContainer.sizeDelta = new Vector2(24f, 0f);
		this.selectionDetailsScrollRectScrollBarContainer.anchoredPosition = Vector2.zero;
		this.selectionHeaderLabel.SetText(selectedPermit.Name);
		this.selectionNameLabel.SetText(selectedPermit.Name);
		this.selectionDescriptionLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(selectedPermit.Description));
		this.selectionDescriptionLabel.SetText(selectedPermit.Description);
		this.selectionFacadeForLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(permitPresentationInfo.facadeFor));
		this.selectionFacadeForLabel.SetText(permitPresentationInfo.facadeFor);
		string dlcIdFrom = selectedPermit.GetDlcIdFrom();
		if (DlcManager.IsDlcId(dlcIdFrom))
		{
			this.selectionRarityDetailsLabel.gameObject.SetActive(false);
			this.selectionOwnedCount.gameObject.SetActive(false);
			this.selectionCollectionLabel.gameObject.SetActive(true);
			if (selectedPermit.Rarity == PermitRarity.UniversalLocked)
			{
				this.selectionCollectionLabel.SetText(UI.KLEI_INVENTORY_SCREEN.COLLECTION_COMING_SOON.Replace("{Collection}", DlcManager.GetDlcTitle(dlcIdFrom)));
				return;
			}
			this.selectionCollectionLabel.SetText(UI.KLEI_INVENTORY_SCREEN.COLLECTION.Replace("{Collection}", DlcManager.GetDlcTitle(dlcIdFrom)));
			return;
		}
		else
		{
			this.selectionCollectionLabel.gameObject.SetActive(false);
			string text = UI.KLEI_INVENTORY_SCREEN.ITEM_RARITY_DETAILS.Replace("{RarityName}", selectedPermit.Rarity.GetLocStringName());
			this.selectionRarityDetailsLabel.gameObject.SetActive(!string.IsNullOrWhiteSpace(text));
			this.selectionRarityDetailsLabel.SetText(text);
			this.selectionOwnedCount.gameObject.SetActive(true);
			if (!selectedPermit.IsOwnableOnServer())
			{
				this.selectionOwnedCount.SetText(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_UNLOCKED_BUT_UNOWNABLE);
				return;
			}
			int ownedCount = PermitItems.GetOwnedCount(selectedPermit);
			if (ownedCount > 0)
			{
				this.selectionOwnedCount.SetText(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWNED_AMOUNT.Replace("{OwnedCount}", ownedCount.ToString()));
				return;
			}
			this.selectionOwnedCount.SetText(KleiItemsUI.WrapWithColor(UI.KLEI_INVENTORY_SCREEN.ITEM_PLAYER_OWN_NONE, KleiItemsUI.TEXT_COLOR__PERMIT_NOT_OWNED));
			return;
		}
	}

	// Token: 0x060066A0 RID: 26272 RVA: 0x0026C084 File Offset: 0x0026A284
	private KleiInventoryScreen.PermitPrintabilityState GetPermitPrintabilityState(PermitResource permit)
	{
		if (!this.IS_ONLINE)
		{
			return KleiInventoryScreen.PermitPrintabilityState.UserOffline;
		}
		ulong num;
		ulong num2;
		PermitItems.TryGetBarterPrice(this.SelectedPermit.Id, out num, out num2);
		if (num == 0UL)
		{
			if (permit.Rarity == PermitRarity.Universal || permit.Rarity == PermitRarity.UniversalLocked || permit.Rarity == PermitRarity.Loyalty || permit.Rarity == PermitRarity.Unknown)
			{
				return KleiInventoryScreen.PermitPrintabilityState.NotForSale;
			}
			return KleiInventoryScreen.PermitPrintabilityState.NotForSaleYet;
		}
		else
		{
			if (PermitItems.GetOwnedCount(permit) > 0)
			{
				return KleiInventoryScreen.PermitPrintabilityState.AlreadyOwned;
			}
			if (KleiItems.GetFilamentAmount() < num)
			{
				return KleiInventoryScreen.PermitPrintabilityState.TooExpensive;
			}
			return KleiInventoryScreen.PermitPrintabilityState.Printable;
		}
	}

	// Token: 0x060066A1 RID: 26273 RVA: 0x0026C0F8 File Offset: 0x0026A2F8
	private void RefreshBarterPanel()
	{
		this.barterBuyButton.ClearOnClick();
		this.barterSellButton.ClearOnClick();
		this.barterBuyButton.isInteractable = this.IS_ONLINE;
		this.barterSellButton.isInteractable = this.IS_ONLINE;
		HierarchyReferences component = this.barterBuyButton.GetComponent<HierarchyReferences>();
		HierarchyReferences component2 = this.barterSellButton.GetComponent<HierarchyReferences>();
		new Color(1f, 0.69411767f, 0.69411767f);
		Color color = new Color(0.6f, 0.9529412f, 0.5019608f);
		LocText reference = component.GetReference<LocText>("CostLabel");
		LocText reference2 = component2.GetReference<LocText>("CostLabel");
		this.barterPanelBG.color = (this.IS_ONLINE ? Util.ColorFromHex("575D6F") : Util.ColorFromHex("6F6F6F"));
		this.filamentWalletSection.gameObject.SetActive(this.IS_ONLINE);
		this.barterOfflineLabel.gameObject.SetActive(!this.IS_ONLINE);
		ulong filamentAmount = KleiItems.GetFilamentAmount();
		this.filamentWalletSection.GetComponent<ToolTip>().SetSimpleTooltip((filamentAmount > 1UL) ? string.Format(UI.KLEI_INVENTORY_SCREEN.BARTERING.WALLET_PLURAL_TOOLTIP, filamentAmount) : string.Format(UI.KLEI_INVENTORY_SCREEN.BARTERING.WALLET_TOOLTIP, filamentAmount));
		KleiInventoryScreen.PermitPrintabilityState permitPrintabilityState = this.GetPermitPrintabilityState(this.SelectedPermit);
		if (!this.IS_ONLINE)
		{
			component.GetReference<LocText>("CostLabel").SetText("");
			reference2.SetText("");
			reference2.color = Color.white;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_ACTION_INVALID_OFFLINE);
			this.barterSellButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_ACTION_INVALID_OFFLINE);
			return;
		}
		ulong num;
		ulong num2;
		PermitItems.TryGetBarterPrice(this.SelectedPermit.Id, out num, out num2);
		this.filamentWalletSection.GetComponentInChildren<LocText>().SetText(KleiItems.GetFilamentAmount().ToString());
		switch (permitPrintabilityState)
		{
		case KleiInventoryScreen.PermitPrintabilityState.Printable:
			this.barterBuyButton.isInteractable = true;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_BUY_ACTIVE, num.ToString()));
			reference.SetText("-" + num.ToString());
			this.barterBuyButton.onClick += delegate
			{
				GameObject gameObject = Util.KInstantiateUI(this.barterConfirmationScreenPrefab, LockerNavigator.Instance.gameObject, false);
				gameObject.rectTransform().sizeDelta = Vector2.zero;
				gameObject.GetComponent<BarterConfirmationScreen>().Present(this.SelectedPermit, true);
			};
			break;
		case KleiInventoryScreen.PermitPrintabilityState.AlreadyOwned:
			this.barterBuyButton.isInteractable = false;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_UNBUYABLE_ALREADY_OWNED);
			reference.SetText("-" + num.ToString());
			break;
		case KleiInventoryScreen.PermitPrintabilityState.TooExpensive:
			this.barterBuyButton.isInteractable = false;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_BUY_CANT_AFFORD.text);
			reference.SetText("-" + num.ToString());
			break;
		case KleiInventoryScreen.PermitPrintabilityState.NotForSale:
			this.barterBuyButton.isInteractable = false;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_UNBUYABLE);
			reference.SetText("");
			break;
		case KleiInventoryScreen.PermitPrintabilityState.NotForSaleYet:
			this.barterBuyButton.isInteractable = false;
			this.barterBuyButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_UNBUYABLE_BETA);
			reference.SetText("");
			break;
		}
		if (num2 == 0UL)
		{
			this.barterSellButton.isInteractable = false;
			this.barterSellButton.GetComponent<ToolTip>().SetSimpleTooltip(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_UNSELLABLE);
			reference2.SetText("");
			reference2.color = Color.white;
			return;
		}
		bool flag = PermitItems.GetOwnedCount(this.SelectedPermit) > 0;
		this.barterSellButton.isInteractable = flag;
		this.barterSellButton.GetComponent<ToolTip>().SetSimpleTooltip(flag ? string.Format(UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_SELL_ACTIVE, num2.ToString()) : UI.KLEI_INVENTORY_SCREEN.BARTERING.TOOLTIP_NONE_TO_SELL.text);
		if (flag)
		{
			reference2.color = color;
			reference2.SetText("+" + num2.ToString());
		}
		else
		{
			reference2.color = Color.white;
			reference2.SetText("+" + num2.ToString());
		}
		this.barterSellButton.onClick += delegate
		{
			GameObject gameObject2 = Util.KInstantiateUI(this.barterConfirmationScreenPrefab, LockerNavigator.Instance.gameObject, false);
			gameObject2.rectTransform().sizeDelta = Vector2.zero;
			gameObject2.GetComponent<BarterConfirmationScreen>().Present(this.SelectedPermit, false);
		};
	}

	// Token: 0x060066A2 RID: 26274 RVA: 0x0026C550 File Offset: 0x0026A750
	private void SetCatogoryClickUISound(string categoryID, MultiToggle toggle)
	{
		if (!this.categoryToggles.ContainsKey(categoryID))
		{
			toggle.states[1].on_click_override_sound_path = "";
			toggle.states[0].on_click_override_sound_path = "";
			return;
		}
		toggle.states[1].on_click_override_sound_path = "General_Category_Click";
		toggle.states[0].on_click_override_sound_path = "General_Category_Click";
	}

	// Token: 0x060066A3 RID: 26275 RVA: 0x0026C5C4 File Offset: 0x0026A7C4
	private void SetItemClickUISound(PermitResource permit, MultiToggle toggle)
	{
		string facadeItemSoundName = KleiInventoryScreen.GetFacadeItemSoundName(permit);
		toggle.states[1].on_click_override_sound_path = facadeItemSoundName + "_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = facadeItemSoundName + "_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (permit.IsUnlocked() ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x060066A4 RID: 26276 RVA: 0x0026C6AC File Offset: 0x0026A8AC
	public static string GetFacadeItemSoundName(PermitResource permit)
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
		case PermitCategory.AtmoSuitHelmet:
			return "atmosuit_helmet";
		case PermitCategory.AtmoSuitBody:
			return "tops";
		case PermitCategory.AtmoSuitGloves:
			return "gloves";
		case PermitCategory.AtmoSuitBelt:
			return "belt";
		case PermitCategory.AtmoSuitShoes:
			return "shoes";
		}
		if (permit.Category == PermitCategory.Building)
		{
			BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
			if (buildingDef == null)
			{
				return "HUD";
			}
			string text = buildingDef.PrefabID;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 1943253450U)
			{
				if (num <= 1036100273U)
				{
					if (num <= 297556592U)
					{
						if (num <= 228062815U)
						{
							if (num != 38823703U)
							{
								if (num != 112031228U)
								{
									if (num != 228062815U)
									{
										goto IL_08E5;
									}
									if (!(text == "LuxuryBed"))
									{
										goto IL_08E5;
									}
									string id = permit.Id;
									if (id == "LuxuryBed_boat")
									{
										return "elegantbed_boat";
									}
									if (!(id == "LuxuryBed_bouncy"))
									{
										return "elegantbed";
									}
									return "elegantbed_bouncy";
								}
								else
								{
									if (!(text == "LogicGateDemultiplexer"))
									{
										goto IL_08E5;
									}
									goto IL_08A9;
								}
							}
							else
							{
								if (!(text == "LogicGateXOR"))
								{
									goto IL_08E5;
								}
								goto IL_08A9;
							}
						}
						else if (num != 228549509U)
						{
							if (num != 296872528U)
							{
								if (num != 297556592U)
								{
									goto IL_08E5;
								}
								if (!(text == "LogicRibbonBridge"))
								{
									goto IL_08E5;
								}
								goto IL_08A9;
							}
							else if (!(text == "ItemPedestal"))
							{
								goto IL_08E5;
							}
						}
						else
						{
							if (!(text == "WashSink"))
							{
								goto IL_08E5;
							}
							return "sink";
						}
					}
					else if (num <= 595816591U)
					{
						if (num != 301047391U)
						{
							if (num != 585850236U)
							{
								if (num != 595816591U)
								{
									goto IL_08E5;
								}
								if (!(text == "FlowerVase"))
								{
									goto IL_08E5;
								}
								goto IL_0811;
							}
							else if (!(text == "GravitasPedestal"))
							{
								goto IL_08E5;
							}
						}
						else
						{
							if (!(text == "WireRefined"))
							{
								goto IL_08E5;
							}
							goto IL_0891;
						}
					}
					else if (num != 674245745U)
					{
						if (num != 781890915U)
						{
							if (num != 1036100273U)
							{
								goto IL_08E5;
							}
							if (!(text == "WireRefinedBridgeHighWattage"))
							{
								goto IL_08E5;
							}
							goto IL_0891;
						}
						else
						{
							if (!(text == "LogicGateNOT"))
							{
								goto IL_08E5;
							}
							goto IL_08A9;
						}
					}
					else
					{
						if (!(text == "CraftingTable"))
						{
							goto IL_08E5;
						}
						return "craftingstation";
					}
					return "sculpture";
				}
				if (num <= 1526604543U)
				{
					if (num <= 1232204109U)
					{
						if (num != 1038415088U)
						{
							if (num != 1089791339U)
							{
								if (num != 1232204109U)
								{
									goto IL_08E5;
								}
								if (!(text == "WireBridge"))
								{
									goto IL_08E5;
								}
								goto IL_0891;
							}
							else
							{
								if (!(text == "Refrigerator"))
								{
									goto IL_08E5;
								}
								return "refrigerator";
							}
						}
						else
						{
							if (!(text == "LogicGateFILTER"))
							{
								goto IL_08E5;
							}
							goto IL_08A9;
						}
					}
					else if (num != 1269853127U)
					{
						if (num != 1398532937U)
						{
							if (num != 1526604543U)
							{
								goto IL_08E5;
							}
							if (!(text == "StorageLockerSmart"))
							{
								goto IL_08E5;
							}
							return "storagelockersmart";
						}
						else
						{
							if (!(text == "LogicGateMultiplexer"))
							{
								goto IL_08E5;
							}
							goto IL_08A9;
						}
					}
					else
					{
						if (!(text == "AdvancedResearchCenter"))
						{
							goto IL_08E5;
						}
						return "advancedresearchcenter";
					}
				}
				else if (num <= 1734850496U)
				{
					if (num != 1607642960U)
					{
						if (num != 1633134164U)
						{
							if (num != 1734850496U)
							{
								goto IL_08E5;
							}
							if (!(text == "RockCrusher"))
							{
								goto IL_08E5;
							}
							return "rockrefinery";
						}
						else
						{
							if (!(text == "CeilingLight"))
							{
								goto IL_08E5;
							}
							goto IL_0855;
						}
					}
					else
					{
						if (!(text == "FlushToilet"))
						{
							goto IL_08E5;
						}
						return "flushtoilate";
					}
				}
				else if (num <= 1908704479U)
				{
					if (num != 1815117387U)
					{
						if (num != 1908704479U)
						{
							goto IL_08E5;
						}
						if (!(text == "LogicGateAND"))
						{
							goto IL_08E5;
						}
						goto IL_08A9;
					}
					else
					{
						if (!(text == "LogicGateOR"))
						{
							goto IL_08E5;
						}
						goto IL_08A9;
					}
				}
				else if (num != 1938276536U)
				{
					if (num != 1943253450U)
					{
						goto IL_08E5;
					}
					if (!(text == "WaterCooler"))
					{
						goto IL_08E5;
					}
					return "watercooler";
				}
				else
				{
					if (!(text == "Wire"))
					{
						goto IL_08E5;
					}
					goto IL_0891;
				}
			}
			else if (num <= 3132083755U)
			{
				if (num <= 2691468069U)
				{
					if (num <= 2076384603U)
					{
						if (num != 2028863301U)
						{
							if (num != 2041738741U)
							{
								if (num != 2076384603U)
								{
									goto IL_08E5;
								}
								if (!(text == "GasReservoir"))
								{
									goto IL_08E5;
								}
								return "gasstorage";
							}
							else
							{
								if (!(text == "CookingStation"))
								{
									goto IL_08E5;
								}
								return "grill";
							}
						}
						else if (!(text == "FlowerVaseHanging"))
						{
							goto IL_08E5;
						}
					}
					else if (num != 2402859370U)
					{
						if (num != 2406622476U)
						{
							if (num != 2691468069U)
							{
								goto IL_08E5;
							}
							if (!(text == "ResearchCenter"))
							{
								goto IL_08E5;
							}
							return "researchcenter";
						}
						else
						{
							if (!(text == "WireBridgeHighWattage"))
							{
								goto IL_08E5;
							}
							goto IL_0891;
						}
					}
					else
					{
						if (!(text == "StorageLocker"))
						{
							goto IL_08E5;
						}
						return "storagelocker";
					}
				}
				else if (num <= 2818521706U)
				{
					if (num != 2701698824U)
					{
						if (num != 2722382738U)
						{
							if (num != 2818521706U)
							{
								goto IL_08E5;
							}
							if (!(text == "GourmetCookingStation"))
							{
								goto IL_08E5;
							}
							return "gasrange";
						}
						else
						{
							if (!(text == "PlanterBox"))
							{
								goto IL_08E5;
							}
							return "planterbox";
						}
					}
					else
					{
						if (!(text == "ManualGenerator"))
						{
							goto IL_08E5;
						}
						return "manualgenerator";
					}
				}
				else if (num <= 3048425356U)
				{
					if (num != 2899744071U)
					{
						if (num != 3048425356U)
						{
							goto IL_08E5;
						}
						if (!(text == "Bed"))
						{
							goto IL_08E5;
						}
						return "bed";
					}
					else
					{
						if (!(text == "ExteriorWall"))
						{
							goto IL_08E5;
						}
						return "wall";
					}
				}
				else if (num != 3080524513U)
				{
					if (num != 3132083755U)
					{
						goto IL_08E5;
					}
					if (!(text == "FlowerVaseWall"))
					{
						goto IL_08E5;
					}
				}
				else
				{
					if (!(text == "MilkPress"))
					{
						goto IL_08E5;
					}
					return "pulverizer";
				}
			}
			else if (num <= 3562718686U)
			{
				if (num <= 3371266309U)
				{
					if (num != 3228988836U)
					{
						if (num != 3347778080U)
						{
							if (num != 3371266309U)
							{
								goto IL_08E5;
							}
							if (!(text == "LogicRibbon"))
							{
								goto IL_08E5;
							}
							goto IL_08A9;
						}
						else
						{
							if (!(text == "LogicGateBUFFER"))
							{
								goto IL_08E5;
							}
							goto IL_08A9;
						}
					}
					else
					{
						if (!(text == "LogicWire"))
						{
							goto IL_08E5;
						}
						goto IL_08A9;
					}
				}
				else if (num != 3422134480U)
				{
					if (num != 3534553076U)
					{
						if (num != 3562718686U)
						{
							goto IL_08E5;
						}
						if (!(text == "Headquarters"))
						{
							goto IL_08E5;
						}
						return "headquarters";
					}
					else
					{
						if (!(text == "MassageTable"))
						{
							goto IL_08E5;
						}
						return "massagetable";
					}
				}
				else
				{
					if (!(text == "MicrobeMusher"))
					{
						goto IL_08E5;
					}
					return "microbemusher";
				}
			}
			else if (num <= 3873680366U)
			{
				if (num != 3681463987U)
				{
					if (num != 3716494409U)
					{
						if (num != 3873680366U)
						{
							goto IL_08E5;
						}
						if (!(text == "WireRefinedBridge"))
						{
							goto IL_08E5;
						}
						goto IL_0891;
					}
					else
					{
						if (!(text == "HighWattageWire"))
						{
							goto IL_08E5;
						}
						goto IL_0891;
					}
				}
				else
				{
					if (!(text == "FloorLamp"))
					{
						goto IL_08E5;
					}
					goto IL_0855;
				}
			}
			else if (num <= 3958671086U)
			{
				if (num != 3903452895U)
				{
					if (num != 3958671086U)
					{
						goto IL_08E5;
					}
					if (!(text == "FlowerVaseHangingFancy"))
					{
						goto IL_08E5;
					}
				}
				else
				{
					if (!(text == "EggCracker"))
					{
						goto IL_08E5;
					}
					return "eggcracker";
				}
			}
			else if (num != 4217645425U)
			{
				if (num != 4243975822U)
				{
					goto IL_08E5;
				}
				if (!(text == "WireRefinedHighWattage"))
				{
					goto IL_08E5;
				}
				goto IL_0891;
			}
			else
			{
				if (!(text == "LogicWireBridge"))
				{
					goto IL_08E5;
				}
				goto IL_08A9;
			}
			IL_0811:
			return "flowervase";
			IL_0855:
			return "ceilingLight";
			IL_0891:
			return "wire";
			IL_08A9:
			return "logicwire";
		}
		IL_08E5:
		if (permit.Category == PermitCategory.Artwork)
		{
			BuildingDef buildingDef2 = KleiPermitVisUtil.GetBuildingDef(permit);
			if (buildingDef2 == null)
			{
				return "HUD";
			}
			if (KleiInventoryScreen.<GetFacadeItemSoundName>g__Has|76_0<Sculpture>(buildingDef2))
			{
				string text = buildingDef2.PrefabID;
				if (text == "IceSculpture")
				{
					return "icesculpture";
				}
				if (!(text == "WoodSculpture"))
				{
					return "sculpture";
				}
				return "woodsculpture";
			}
			else
			{
				if (KleiInventoryScreen.<GetFacadeItemSoundName>g__Has|76_0<Painting>(buildingDef2))
				{
					return "painting";
				}
				if (KleiInventoryScreen.<GetFacadeItemSoundName>g__Has|76_0<MonumentPart>(buildingDef2))
				{
					return "monument";
				}
			}
		}
		if (permit.Category == PermitCategory.JoyResponse && permit is BalloonArtistFacadeResource)
		{
			return "balloon";
		}
		return "HUD";
	}

	// Token: 0x060066A5 RID: 26277 RVA: 0x0026D03A File Offset: 0x0026B23A
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x060066AD RID: 26285 RVA: 0x0026D12D File Offset: 0x0026B32D
	[CompilerGenerated]
	internal static bool <GetFacadeItemSoundName>g__Has|76_0<T>(BuildingDef buildingDef) where T : Component
	{
		return !buildingDef.BuildingComplete.GetComponent<T>().IsNullOrDestroyed();
	}

	// Token: 0x04004637 RID: 17975
	[Header("Header")]
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004638 RID: 17976
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x04004639 RID: 17977
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x0400463A RID: 17978
	private Dictionary<string, MultiToggle> categoryToggles = new Dictionary<string, MultiToggle>();

	// Token: 0x0400463B RID: 17979
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x0400463C RID: 17980
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x0400463D RID: 17981
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x0400463E RID: 17982
	[SerializeField]
	private GameObject subcategoryPrefab;

	// Token: 0x0400463F RID: 17983
	[SerializeField]
	private GameObject itemDummyPrefab;

	// Token: 0x04004640 RID: 17984
	[Header("GalleryFilters")]
	[SerializeField]
	private KInputTextField searchField;

	// Token: 0x04004641 RID: 17985
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04004642 RID: 17986
	[SerializeField]
	private MultiToggle doublesOnlyToggle;

	// Token: 0x04004643 RID: 17987
	public const int FILTER_SHOW_ALL = 0;

	// Token: 0x04004644 RID: 17988
	public const int FILTER_SHOW_OWNED_ONLY = 1;

	// Token: 0x04004645 RID: 17989
	public const int FILTER_SHOW_DOUBLES_ONLY = 2;

	// Token: 0x04004646 RID: 17990
	private int showFilterState;

	// Token: 0x04004647 RID: 17991
	[Header("BarterSection")]
	[SerializeField]
	private Image barterPanelBG;

	// Token: 0x04004648 RID: 17992
	[SerializeField]
	private KButton barterBuyButton;

	// Token: 0x04004649 RID: 17993
	[SerializeField]
	private KButton barterSellButton;

	// Token: 0x0400464A RID: 17994
	[SerializeField]
	private GameObject barterConfirmationScreenPrefab;

	// Token: 0x0400464B RID: 17995
	[SerializeField]
	private GameObject filamentWalletSection;

	// Token: 0x0400464C RID: 17996
	[SerializeField]
	private GameObject barterOfflineLabel;

	// Token: 0x0400464D RID: 17997
	private Dictionary<PermitResource, MultiToggle> galleryGridButtons = new Dictionary<PermitResource, MultiToggle>();

	// Token: 0x0400464E RID: 17998
	private List<KleiInventoryUISubcategory> subcategories = new List<KleiInventoryUISubcategory>();

	// Token: 0x0400464F RID: 17999
	private List<GameObject> recycledGalleryGridButtons = new List<GameObject>();

	// Token: 0x04004650 RID: 18000
	private GridLayouter galleryGridLayouter;

	// Token: 0x04004651 RID: 18001
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x04004652 RID: 18002
	[SerializeField]
	private KleiPermitDioramaVis permitVis;

	// Token: 0x04004653 RID: 18003
	[SerializeField]
	private KScrollRect selectionDetailsScrollRect;

	// Token: 0x04004654 RID: 18004
	[SerializeField]
	private RectTransform selectionDetailsScrollRectScrollBarContainer;

	// Token: 0x04004655 RID: 18005
	[SerializeField]
	private LocText selectionNameLabel;

	// Token: 0x04004656 RID: 18006
	[SerializeField]
	private LocText selectionDescriptionLabel;

	// Token: 0x04004657 RID: 18007
	[SerializeField]
	private LocText selectionFacadeForLabel;

	// Token: 0x04004658 RID: 18008
	[SerializeField]
	private LocText selectionCollectionLabel;

	// Token: 0x04004659 RID: 18009
	[SerializeField]
	private LocText selectionRarityDetailsLabel;

	// Token: 0x0400465A RID: 18010
	[SerializeField]
	private LocText selectionOwnedCount;

	// Token: 0x0400465C RID: 18012
	private bool IS_ONLINE;

	// Token: 0x0400465D RID: 18013
	private bool initConfigComplete;

	// Token: 0x02001EC9 RID: 7881
	private enum PermitPrintabilityState
	{
		// Token: 0x04008EE2 RID: 36578
		Printable,
		// Token: 0x04008EE3 RID: 36579
		AlreadyOwned,
		// Token: 0x04008EE4 RID: 36580
		TooExpensive,
		// Token: 0x04008EE5 RID: 36581
		NotForSale,
		// Token: 0x04008EE6 RID: 36582
		NotForSaleYet,
		// Token: 0x04008EE7 RID: 36583
		UserOffline
	}

	// Token: 0x02001ECA RID: 7882
	private enum MultiToggleState
	{
		// Token: 0x04008EE9 RID: 36585
		Default,
		// Token: 0x04008EEA RID: 36586
		Selected,
		// Token: 0x04008EEB RID: 36587
		NonInteractable
	}
}
