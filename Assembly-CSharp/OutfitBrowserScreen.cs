using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000D8A RID: 3466
public class OutfitBrowserScreen : KMonoBehaviour
{
	// Token: 0x06006BDF RID: 27615 RVA: 0x0028B064 File Offset: 0x00289264
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
		this.gridLayouter = new GridLayouter
		{
			minCellSize = 112f,
			maxCellSize = 144f,
			targetGridLayouts = this.galleryGridContent.GetComponents<GridLayoutGroup>().ToList<GridLayoutGroup>()
		};
		this.categoriesAndSearchBar.InitializeWith(this);
		this.pickOutfitButton.onClick += this.OnClickPickOutfit;
		this.editOutfitButton.onClick += delegate
		{
			if (this.state.SelectedOutfitOpt.IsNone())
			{
				return;
			}
			new OutfitDesignerScreenConfig(this.state.SelectedOutfitOpt.Unwrap(), this.Config.minionPersonality, this.Config.targetMinionInstance, new Action<ClothingOutfitTarget>(this.OnOutfitDesignerWritesToOutfitTarget)).ApplyAndOpenScreen();
		};
		this.renameOutfitButton.onClick += delegate
		{
			ClothingOutfitTarget selectedOutfit = this.state.SelectedOutfitOpt.Unwrap();
			OutfitBrowserScreen.MakeRenamePopup(this.inputFieldPrefab, selectedOutfit, () => selectedOutfit.ReadName(), delegate(string new_name)
			{
				selectedOutfit.WriteName(new_name);
				this.Configure(this.Config.WithOutfit(selectedOutfit));
			});
		};
		this.deleteOutfitButton.onClick += delegate
		{
			ClothingOutfitTarget selectedOutfit = this.state.SelectedOutfitOpt.Unwrap();
			OutfitBrowserScreen.MakeDeletePopup(selectedOutfit, delegate
			{
				selectedOutfit.Delete();
				this.Configure(this.Config.WithOutfit(Option.None));
			});
		};
	}

	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x06006BE0 RID: 27616 RVA: 0x0028B132 File Offset: 0x00289332
	// (set) Token: 0x06006BE1 RID: 27617 RVA: 0x0028B13A File Offset: 0x0028933A
	public OutfitBrowserScreenConfig Config { get; private set; }

	// Token: 0x06006BE2 RID: 27618 RVA: 0x0028B144 File Offset: 0x00289344
	protected override void OnCmpEnable()
	{
		if (this.isFirstDisplay)
		{
			this.isFirstDisplay = false;
			this.dioramaMinionOrMannequin.TrySpawn();
			this.FirstTimeSetup();
			this.postponeConfiguration = false;
			this.Configure(this.Config);
		}
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshGallery();
			this.outfitDescriptionPanel.Refresh(this.state.SelectedOutfitOpt, ClothingOutfitUtility.OutfitType.Clothing, this.Config.minionPersonality);
		});
	}

	// Token: 0x06006BE3 RID: 27619 RVA: 0x0028B19C File Offset: 0x0028939C
	private void FirstTimeSetup()
	{
		this.state.OnCurrentOutfitTypeChanged += delegate
		{
			this.PopulateGallery();
			OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
			Option<ClothingOutfitTarget> option;
			if (!outfitBrowserScreenConfig.minionPersonality.HasValue)
			{
				outfitBrowserScreenConfig = this.Config;
				if (!outfitBrowserScreenConfig.selectedTarget.HasValue)
				{
					option = ClothingOutfitTarget.GetRandom(this.state.CurrentOutfitType);
					goto IL_004F;
				}
			}
			option = this.Config.selectedTarget;
			IL_004F:
			if (option.IsSome() && option.Unwrap().DoesExist())
			{
				this.state.SelectedOutfitOpt = option;
				return;
			}
			this.state.SelectedOutfitOpt = Option.None;
		};
		this.state.OnSelectedOutfitOptChanged += delegate
		{
			if (this.state.SelectedOutfitOpt.IsSome())
			{
				this.selectionHeaderLabel.text = this.state.SelectedOutfitOpt.Unwrap().ReadName();
			}
			else
			{
				this.selectionHeaderLabel.text = UI.OUTFIT_NAME.NONE;
			}
			this.dioramaMinionOrMannequin.current.SetOutfit(this.state.CurrentOutfitType, this.state.SelectedOutfitOpt);
			this.dioramaMinionOrMannequin.current.ReactToFullOutfitChange();
			this.outfitDescriptionPanel.Refresh(this.state.SelectedOutfitOpt, this.state.CurrentOutfitType, this.Config.minionPersonality);
			this.dioramaBG.sprite = KleiPermitDioramaVis.GetDioramaBackground(this.state.CurrentOutfitType);
			this.pickOutfitButton.gameObject.SetActive(this.Config.isPickingOutfitForDupe);
			OutfitBrowserScreenConfig outfitBrowserScreenConfig2 = this.Config;
			if (outfitBrowserScreenConfig2.minionPersonality.IsSome())
			{
				this.pickOutfitButton.isInteractable = !this.state.SelectedOutfitOpt.IsSome() || !this.state.SelectedOutfitOpt.Unwrap().DoesContainLockedItems();
				GameObject gameObject = this.pickOutfitButton.gameObject;
				Option<string> option2;
				if (!this.pickOutfitButton.isInteractable)
				{
					LocString tooltip_PICK_OUTFIT_ERROR_LOCKED = UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_PICK_OUTFIT_ERROR_LOCKED;
					string text = "{MinionName}";
					outfitBrowserScreenConfig2 = this.Config;
					option2 = Option.Some<string>(tooltip_PICK_OUTFIT_ERROR_LOCKED.Replace(text, outfitBrowserScreenConfig2.GetMinionName()));
				}
				else
				{
					option2 = Option.None;
				}
				KleiItemsUI.ConfigureTooltipOn(gameObject, option2);
			}
			this.editOutfitButton.isInteractable = this.state.SelectedOutfitOpt.IsSome();
			this.renameOutfitButton.isInteractable = this.state.SelectedOutfitOpt.IsSome() && this.state.SelectedOutfitOpt.Unwrap().CanWriteName;
			KleiItemsUI.ConfigureTooltipOn(this.renameOutfitButton.gameObject, this.renameOutfitButton.isInteractable ? UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_RENAME_OUTFIT : UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_RENAME_OUTFIT_ERROR_READONLY);
			this.deleteOutfitButton.isInteractable = this.state.SelectedOutfitOpt.IsSome() && this.state.SelectedOutfitOpt.Unwrap().CanDelete;
			KleiItemsUI.ConfigureTooltipOn(this.deleteOutfitButton.gameObject, this.deleteOutfitButton.isInteractable ? UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_DELETE_OUTFIT : UI.OUTFIT_BROWSER_SCREEN.TOOLTIP_DELETE_OUTFIT_ERROR_READONLY);
			this.state.OnSelectedOutfitOptChanged += this.RefreshGallery;
			this.state.OnFilterChanged += this.RefreshGallery;
			this.state.OnCurrentOutfitTypeChanged += this.RefreshGallery;
			this.RefreshGallery();
		};
	}

	// Token: 0x06006BE4 RID: 27620 RVA: 0x0028B1CC File Offset: 0x002893CC
	public void Configure(OutfitBrowserScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.dioramaMinionOrMannequin.SetFrom(config.minionPersonality);
		if (config.targetMinionInstance.HasValue)
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.MINION_GALLERY_HEADER.Replace("{MinionName}", config.targetMinionInstance.Value.GetProperName());
		}
		else if (config.minionPersonality.HasValue)
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.MINION_GALLERY_HEADER.Replace("{MinionName}", config.minionPersonality.Value.Name);
		}
		else
		{
			this.galleryHeaderLabel.text = UI.OUTFIT_BROWSER_SCREEN.COLUMN_HEADERS.GALLERY_HEADER;
		}
		this.state.CurrentOutfitType = config.onlyShowOutfitType.UnwrapOr(this.lastShownOutfitType.UnwrapOr(ClothingOutfitUtility.OutfitType.Clothing, null), null);
		if (base.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(false);
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x06006BE5 RID: 27621 RVA: 0x0028B2D0 File Offset: 0x002894D0
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06006BE6 RID: 27622 RVA: 0x0028B2E8 File Offset: 0x002894E8
	private void PopulateGallery()
	{
		this.outfits.Clear();
		this.galleryGridItemPool.ReturnAll();
		this.RefreshGalleryFn = null;
		if (this.Config.isPickingOutfitForDupe)
		{
			this.<PopulateGallery>g__AddGridIconForTarget|35_0(Option.None);
		}
		OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
		if (outfitBrowserScreenConfig.targetMinionInstance.HasValue)
		{
			ClothingOutfitUtility.OutfitType currentOutfitType = this.state.CurrentOutfitType;
			outfitBrowserScreenConfig = this.Config;
			this.<PopulateGallery>g__AddGridIconForTarget|35_0(ClothingOutfitTarget.FromMinion(currentOutfitType, outfitBrowserScreenConfig.targetMinionInstance.Value));
		}
		foreach (ClothingOutfitTarget clothingOutfitTarget in from outfit in ClothingOutfitTarget.GetAllTemplates()
			where outfit.OutfitType == this.state.CurrentOutfitType
			select outfit)
		{
			this.<PopulateGallery>g__AddGridIconForTarget|35_0(clothingOutfitTarget);
		}
		this.addButtonGridItem.transform.SetAsLastSibling();
		this.addButtonGridItem.SetActive(true);
		this.addButtonGridItem.GetComponent<MultiToggle>().onClick = delegate
		{
			new OutfitDesignerScreenConfig(ClothingOutfitTarget.ForNewTemplateOutfit(this.state.CurrentOutfitType), this.Config.minionPersonality, this.Config.targetMinionInstance, new Action<ClothingOutfitTarget>(this.OnOutfitDesignerWritesToOutfitTarget)).ApplyAndOpenScreen();
		};
		this.RefreshGallery();
	}

	// Token: 0x06006BE7 RID: 27623 RVA: 0x0028B408 File Offset: 0x00289608
	private void OnOutfitDesignerWritesToOutfitTarget(ClothingOutfitTarget outfit)
	{
		this.Configure(this.Config.WithOutfit(outfit));
	}

	// Token: 0x06006BE8 RID: 27624 RVA: 0x0028B42F File Offset: 0x0028962F
	private void Update()
	{
		this.gridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06006BE9 RID: 27625 RVA: 0x0028B43C File Offset: 0x0028963C
	private void OnClickPickOutfit()
	{
		OutfitBrowserScreenConfig outfitBrowserScreenConfig = this.Config;
		if (outfitBrowserScreenConfig.targetMinionInstance.IsSome())
		{
			outfitBrowserScreenConfig = this.Config;
			WearableAccessorizer component = outfitBrowserScreenConfig.targetMinionInstance.Unwrap().GetComponent<WearableAccessorizer>();
			ClothingOutfitUtility.OutfitType currentOutfitType = this.state.CurrentOutfitType;
			Option<ClothingOutfitTarget> option = this.state.SelectedOutfitOpt;
			component.ApplyClothingItems(currentOutfitType, option.AndThen<IEnumerable<ClothingItemResource>>((ClothingOutfitTarget outfit) => outfit.ReadItemValues()).UnwrapOr(ClothingOutfitTarget.NO_ITEM_VALUES, null));
		}
		else
		{
			outfitBrowserScreenConfig = this.Config;
			if (outfitBrowserScreenConfig.minionPersonality.IsSome())
			{
				outfitBrowserScreenConfig = this.Config;
				Personality value = outfitBrowserScreenConfig.minionPersonality.Value;
				ClothingOutfitUtility.OutfitType currentOutfitType2 = this.state.CurrentOutfitType;
				Option<ClothingOutfitTarget> option = this.state.SelectedOutfitOpt;
				value.SetSelectedTemplateOutfitId(currentOutfitType2, option.AndThen<string>((ClothingOutfitTarget o) => o.OutfitId));
			}
		}
		LockerNavigator.Instance.PopScreen();
	}

	// Token: 0x06006BEA RID: 27626 RVA: 0x0028B540 File Offset: 0x00289740
	public static void MakeDeletePopup(ClothingOutfitTarget sourceTarget, global::System.Action deleteFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.HEADER.Replace("{OutfitName}", sourceTarget.ReadName())).AddPlainText(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BODY.Replace("{OutfitName}", sourceTarget.ReadName()));
			string text = UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BUTTON_YES_DELETE;
			Action<InfoDialogScreen> action;
			if ((action = <>9__1) == null)
			{
				action = (<>9__1 = delegate(InfoDialogScreen d)
				{
					deleteFn();
					d.Deactivate();
				});
			}
			infoDialogScreen.AddOption(text, action, true).AddOption(UI.OUTFIT_BROWSER_SCREEN.DELETE_WARNING_POPUP.BUTTON_DONT_DELETE, delegate(InfoDialogScreen d)
			{
				d.Deactivate();
			}, false);
		});
	}

	// Token: 0x06006BEB RID: 27627 RVA: 0x0028B578 File Offset: 0x00289778
	public static void MakeRenamePopup(KInputTextField inputFieldPrefab, ClothingOutfitTarget sourceTarget, Func<string> readName, Action<string> writeName)
	{
		KInputTextField inputField;
		InfoScreenPlainText errorText;
		KButton okButton;
		LocText okButtonText;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			dialog.SetHeader(UI.OUTFIT_BROWSER_SCREEN.RENAME_POPUP.HEADER).AddUI<KInputTextField>(inputFieldPrefab, out inputField).AddSpacer(8f)
				.AddUI<InfoScreenPlainText>(dialog.GetPlainTextPrefab(), out errorText)
				.AddOption(true, out okButton, out okButtonText)
				.AddOption(UI.CONFIRMDIALOG.CANCEL, delegate(InfoDialogScreen d)
				{
					d.Deactivate();
				}, false);
			inputField.onValueChanged.AddListener(new UnityAction<string>(base.<MakeRenamePopup>g__Refresh|1));
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
				writeName(inputField.text);
				dialog.Deactivate();
			};
			base.<MakeRenamePopup>g__Refresh|1(readName());
		});
	}

	// Token: 0x06006BEC RID: 27628 RVA: 0x0028B5C0 File Offset: 0x002897C0
	private void SetButtonClickUISound(Option<ClothingOutfitTarget> target, MultiToggle toggle)
	{
		if (!target.HasValue)
		{
			toggle.states[1].on_click_override_sound_path = "HUD_Click";
			toggle.states[0].on_click_override_sound_path = "HUD_Click";
			return;
		}
		bool flag = !target.Value.DoesContainLockedItems();
		toggle.states[1].on_click_override_sound_path = "ClothingItem_Click";
		toggle.states[1].sound_parameter_name = "Unlocked";
		toggle.states[1].sound_parameter_value = (flag ? 1f : 0f);
		toggle.states[1].has_sound_parameter = true;
		toggle.states[0].on_click_override_sound_path = "ClothingItem_Click";
		toggle.states[0].sound_parameter_name = "Unlocked";
		toggle.states[0].sound_parameter_value = (flag ? 1f : 0f);
		toggle.states[0].has_sound_parameter = true;
	}

	// Token: 0x06006BED RID: 27629 RVA: 0x0028B6D2 File Offset: 0x002898D2
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x06006BF5 RID: 27637 RVA: 0x0028BBCC File Offset: 0x00289DCC
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIconForTarget|35_0(Option<ClothingOutfitTarget> target)
	{
		GameObject spawn = this.galleryGridItemPool.Borrow();
		GameObject gameObject = spawn.transform.GetChild(1).gameObject;
		GameObject isUnownedOverlayGO = spawn.transform.GetChild(2).gameObject;
		GameObject dlcBannerGO = spawn.transform.GetChild(3).gameObject;
		gameObject.SetActive(true);
		bool flag = target.IsNone() || this.state.CurrentOutfitType == ClothingOutfitUtility.OutfitType.AtmoSuit;
		UIMannequin componentInChildren = gameObject.GetComponentInChildren<UIMannequin>();
		this.dioramaMinionOrMannequin.mannequin.shouldShowOutfitWithDefaultItems = flag;
		componentInChildren.shouldShowOutfitWithDefaultItems = flag;
		componentInChildren.personalityToUseForDefaultClothing = this.Config.minionPersonality;
		componentInChildren.SetOutfit(this.state.CurrentOutfitType, target);
		RectTransform component = gameObject.GetComponent<RectTransform>();
		float num;
		float num2;
		float num3;
		float num4;
		switch (this.state.CurrentOutfitType)
		{
		case ClothingOutfitUtility.OutfitType.Clothing:
			num = 8f;
			num2 = 8f;
			num3 = 8f;
			num4 = 8f;
			break;
		case ClothingOutfitUtility.OutfitType.JoyResponse:
			throw new NotSupportedException();
		case ClothingOutfitUtility.OutfitType.AtmoSuit:
			num = 24f;
			num2 = 16f;
			num3 = 32f;
			num4 = 8f;
			break;
		default:
			throw new NotImplementedException();
		}
		component.offsetMin = new Vector2(num, num4);
		component.offsetMax = new Vector2(-num2, -num3);
		MultiToggle button = spawn.GetComponent<MultiToggle>();
		MultiToggle button2 = button;
		button2.onEnter = (global::System.Action)Delegate.Combine(button2.onEnter, new global::System.Action(this.OnMouseOverToggle));
		button.onClick = delegate
		{
			this.state.SelectedOutfitOpt = target;
		};
		this.RefreshGalleryFn = (global::System.Action)Delegate.Combine(this.RefreshGalleryFn, new global::System.Action(delegate
		{
			button.ChangeState((target == this.state.SelectedOutfitOpt) ? 1 : 0);
			if (string.IsNullOrWhiteSpace(this.state.Filter) || target.IsNone())
			{
				spawn.SetActive(true);
			}
			else
			{
				spawn.SetActive(target.Unwrap().ReadName().ToLower()
					.Contains(this.state.Filter.ToLower()));
			}
			if (!target.HasValue)
			{
				KleiItemsUI.ConfigureTooltipOn(spawn, KleiItemsUI.WrapAsToolTipTitle(KleiItemsUI.GetNoneOutfitName(this.state.CurrentOutfitType)));
				isUnownedOverlayGO.SetActive(false);
			}
			else
			{
				KleiItemsUI.ConfigureTooltipOn(spawn, KleiItemsUI.WrapAsToolTipTitle(target.Value.ReadName()));
				isUnownedOverlayGO.SetActive(target.Value.DoesContainLockedItems());
			}
			if (target.IsSome())
			{
				ClothingOutfitTarget.Implementation impl = target.Unwrap().impl;
				if (impl is ClothingOutfitTarget.DatabaseAuthoredTemplate)
				{
					ClothingOutfitTarget.DatabaseAuthoredTemplate databaseAuthoredTemplate = (ClothingOutfitTarget.DatabaseAuthoredTemplate)impl;
					string dlcIdFrom = databaseAuthoredTemplate.resource.GetDlcIdFrom();
					if (DlcManager.IsDlcId(dlcIdFrom))
					{
						dlcBannerGO.GetComponent<Image>().color = DlcManager.GetDlcBannerColor(dlcIdFrom);
						dlcBannerGO.SetActive(true);
						return;
					}
					dlcBannerGO.SetActive(false);
					return;
				}
			}
			dlcBannerGO.SetActive(false);
		}));
		this.SetButtonClickUISound(target, button);
	}

	// Token: 0x0400497C RID: 18812
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x0400497D RID: 18813
	[SerializeField]
	private OutfitBrowserScreen_CategoriesAndSearchBar categoriesAndSearchBar;

	// Token: 0x0400497E RID: 18814
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x0400497F RID: 18815
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x04004980 RID: 18816
	[SerializeField]
	private GameObject addButtonGridItem;

	// Token: 0x04004981 RID: 18817
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x04004982 RID: 18818
	private GridLayouter gridLayouter;

	// Token: 0x04004983 RID: 18819
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x04004984 RID: 18820
	[SerializeField]
	private UIMinionOrMannequin dioramaMinionOrMannequin;

	// Token: 0x04004985 RID: 18821
	[SerializeField]
	private Image dioramaBG;

	// Token: 0x04004986 RID: 18822
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x04004987 RID: 18823
	[SerializeField]
	private KButton pickOutfitButton;

	// Token: 0x04004988 RID: 18824
	[SerializeField]
	private KButton editOutfitButton;

	// Token: 0x04004989 RID: 18825
	[SerializeField]
	private KButton renameOutfitButton;

	// Token: 0x0400498A RID: 18826
	[SerializeField]
	private KButton deleteOutfitButton;

	// Token: 0x0400498B RID: 18827
	[Header("Misc")]
	[SerializeField]
	private KInputTextField inputFieldPrefab;

	// Token: 0x0400498C RID: 18828
	[SerializeField]
	public ColorStyleSetting selectedCategoryStyle;

	// Token: 0x0400498D RID: 18829
	[SerializeField]
	public ColorStyleSetting notSelectedCategoryStyle;

	// Token: 0x0400498E RID: 18830
	public OutfitBrowserScreen.State state = new OutfitBrowserScreen.State();

	// Token: 0x0400498F RID: 18831
	public Option<ClothingOutfitUtility.OutfitType> lastShownOutfitType = Option.None;

	// Token: 0x04004990 RID: 18832
	private Dictionary<string, MultiToggle> outfits = new Dictionary<string, MultiToggle>();

	// Token: 0x04004992 RID: 18834
	private bool postponeConfiguration = true;

	// Token: 0x04004993 RID: 18835
	private bool isFirstDisplay = true;

	// Token: 0x04004994 RID: 18836
	private global::System.Action RefreshGalleryFn;

	// Token: 0x02001F71 RID: 8049
	public class State
	{
		// Token: 0x14000036 RID: 54
		// (add) Token: 0x0600B345 RID: 45893 RVA: 0x003D948C File Offset: 0x003D768C
		// (remove) Token: 0x0600B346 RID: 45894 RVA: 0x003D94C4 File Offset: 0x003D76C4
		public event global::System.Action OnSelectedOutfitOptChanged;

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x0600B347 RID: 45895 RVA: 0x003D94F9 File Offset: 0x003D76F9
		// (set) Token: 0x0600B348 RID: 45896 RVA: 0x003D9501 File Offset: 0x003D7701
		public Option<ClothingOutfitTarget> SelectedOutfitOpt
		{
			get
			{
				return this.m_selectedOutfitOpt;
			}
			set
			{
				this.m_selectedOutfitOpt = value;
				if (this.OnSelectedOutfitOptChanged != null)
				{
					this.OnSelectedOutfitOptChanged();
				}
			}
		}

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x0600B349 RID: 45897 RVA: 0x003D9520 File Offset: 0x003D7720
		// (remove) Token: 0x0600B34A RID: 45898 RVA: 0x003D9558 File Offset: 0x003D7758
		public event global::System.Action OnCurrentOutfitTypeChanged;

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x0600B34B RID: 45899 RVA: 0x003D958D File Offset: 0x003D778D
		// (set) Token: 0x0600B34C RID: 45900 RVA: 0x003D9595 File Offset: 0x003D7795
		public ClothingOutfitUtility.OutfitType CurrentOutfitType
		{
			get
			{
				return this.m_currentOutfitType;
			}
			set
			{
				this.m_currentOutfitType = value;
				if (this.OnCurrentOutfitTypeChanged != null)
				{
					this.OnCurrentOutfitTypeChanged();
				}
			}
		}

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600B34D RID: 45901 RVA: 0x003D95B4 File Offset: 0x003D77B4
		// (remove) Token: 0x0600B34E RID: 45902 RVA: 0x003D95EC File Offset: 0x003D77EC
		public event global::System.Action OnFilterChanged;

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600B34F RID: 45903 RVA: 0x003D9621 File Offset: 0x003D7821
		// (set) Token: 0x0600B350 RID: 45904 RVA: 0x003D9629 File Offset: 0x003D7829
		public string Filter
		{
			get
			{
				return this.m_filter;
			}
			set
			{
				this.m_filter = value;
				if (this.OnFilterChanged != null)
				{
					this.OnFilterChanged();
				}
			}
		}

		// Token: 0x040090D0 RID: 37072
		private Option<ClothingOutfitTarget> m_selectedOutfitOpt;

		// Token: 0x040090D1 RID: 37073
		private ClothingOutfitUtility.OutfitType m_currentOutfitType;

		// Token: 0x040090D2 RID: 37074
		private string m_filter;
	}

	// Token: 0x02001F72 RID: 8050
	private enum MultiToggleState
	{
		// Token: 0x040090D7 RID: 37079
		Default,
		// Token: 0x040090D8 RID: 37080
		Selected,
		// Token: 0x040090D9 RID: 37081
		NonInteractable
	}
}
