using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CF4 RID: 3316
public class JoyResponseDesignerScreen : KMonoBehaviour
{
	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x06006609 RID: 26121 RVA: 0x002689AF File Offset: 0x00266BAF
	// (set) Token: 0x0600660A RID: 26122 RVA: 0x002689B7 File Offset: 0x00266BB7
	public JoyResponseScreenConfig Config { get; private set; }

	// Token: 0x0600660B RID: 26123 RVA: 0x002689C0 File Offset: 0x00266BC0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		global::Debug.Assert(this.categoryRowPrefab.transform.parent == this.categoryListContent.transform);
		global::Debug.Assert(this.galleryItemPrefab.transform.parent == this.galleryGridContent.transform);
		this.categoryRowPrefab.SetActive(false);
		this.galleryItemPrefab.SetActive(false);
		this.galleryGridLayouter = new GridLayouter
		{
			minCellSize = 64f,
			maxCellSize = 96f,
			targetGridLayouts = this.galleryGridContent.GetComponents<GridLayoutGroup>().ToList<GridLayoutGroup>()
		};
		this.categoryRowPool = new UIPrefabLocalPool(this.categoryRowPrefab, this.categoryListContent.gameObject);
		this.galleryGridItemPool = new UIPrefabLocalPool(this.galleryItemPrefab, this.galleryGridContent.gameObject);
		JoyResponseDesignerScreen.JoyResponseCategory[] array = new JoyResponseDesignerScreen.JoyResponseCategory[1];
		int num = 0;
		JoyResponseDesignerScreen.JoyResponseCategory joyResponseCategory = new JoyResponseDesignerScreen.JoyResponseCategory();
		joyResponseCategory.displayName = UI.KLEI_INVENTORY_SCREEN.CATEGORIES.JOY_RESPONSES.BALLOON_ARTIST;
		joyResponseCategory.icon = Assets.GetSprite("icon_inventory_balloonartist");
		JoyResponseDesignerScreen.GalleryItem[] array2 = Db.Get().Permits.BalloonArtistFacades.resources.Select((BalloonArtistFacadeResource r) => JoyResponseDesignerScreen.GalleryItem.Of(r)).Prepend(JoyResponseDesignerScreen.GalleryItem.Of(Option.None)).ToArray<JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget>();
		joyResponseCategory.items = array2;
		array[num] = joyResponseCategory;
		this.joyResponseCategories = array;
		this.dioramaVis.ConfigureSetup();
	}

	// Token: 0x0600660C RID: 26124 RVA: 0x00268B41 File Offset: 0x00266D41
	private void Update()
	{
		this.galleryGridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x0600660D RID: 26125 RVA: 0x00268B4E File Offset: 0x00266D4E
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		if (this.Config.isValid)
		{
			this.Configure(this.Config);
			return;
		}
		throw new InvalidOperationException("Cannot open up JoyResponseDesignerScreen without a target personality or minion instance");
	}

	// Token: 0x0600660E RID: 26126 RVA: 0x00268B7B File Offset: 0x00266D7B
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.Configure(this.Config);
		});
	}

	// Token: 0x0600660F RID: 26127 RVA: 0x00268B9C File Offset: 0x00266D9C
	public void Configure(JoyResponseScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.RegisterPreventScreenPop();
		this.primaryButton.ClearOnClick();
		TMP_Text componentInChildren = this.primaryButton.GetComponentInChildren<LocText>();
		LocString button_APPLY_TO_MINION = UI.JOY_RESPONSE_DESIGNER_SCREEN.BUTTON_APPLY_TO_MINION;
		string text = "{MinionName}";
		JoyResponseScreenConfig joyResponseScreenConfig = this.Config;
		componentInChildren.SetText(button_APPLY_TO_MINION.Replace(text, joyResponseScreenConfig.target.GetMinionName()));
		this.primaryButton.onClick += delegate
		{
			Option<PermitResource> permitResource = this.selectedGalleryItem.GetPermitResource();
			if (permitResource.IsSome())
			{
				string text2 = "Save selected balloon ";
				string name = this.selectedGalleryItem.GetName();
				string text3 = " for ";
				JoyResponseScreenConfig joyResponseScreenConfig2 = this.Config;
				global::Debug.Log(text2 + name + text3 + joyResponseScreenConfig2.target.GetMinionName());
				if (this.CanSaveSelection())
				{
					joyResponseScreenConfig2 = this.Config;
					joyResponseScreenConfig2.target.WriteFacadeId(permitResource.Unwrap().Id);
				}
			}
			else
			{
				string text4 = "Save selected balloon ";
				string name2 = this.selectedGalleryItem.GetName();
				string text5 = " for ";
				JoyResponseScreenConfig joyResponseScreenConfig2 = this.Config;
				global::Debug.Log(text4 + name2 + text5 + joyResponseScreenConfig2.target.GetMinionName());
				joyResponseScreenConfig2 = this.Config;
				joyResponseScreenConfig2.target.WriteFacadeId(Option.None);
			}
			LockerNavigator.Instance.PopScreen();
		};
		this.PopulateCategories();
		this.PopulateGallery();
		this.PopulatePreview();
		joyResponseScreenConfig = this.Config;
		if (joyResponseScreenConfig.initalSelectedItem.IsSome())
		{
			joyResponseScreenConfig = this.Config;
			this.SelectGalleryItem(joyResponseScreenConfig.initalSelectedItem.Unwrap());
		}
	}

	// Token: 0x06006610 RID: 26128 RVA: 0x00268C54 File Offset: 0x00266E54
	private bool CanSaveSelection()
	{
		return this.GetSaveSelectionError().IsNone();
	}

	// Token: 0x06006611 RID: 26129 RVA: 0x00268C70 File Offset: 0x00266E70
	private Option<string> GetSaveSelectionError()
	{
		if (!this.selectedGalleryItem.IsUnlocked())
		{
			return Option.Some<string>(UI.JOY_RESPONSE_DESIGNER_SCREEN.TOOLTIP_PICK_JOY_RESPONSE_ERROR_LOCKED.Replace("{MinionName}", this.Config.target.GetMinionName()));
		}
		return Option.None;
	}

	// Token: 0x06006612 RID: 26130 RVA: 0x00268CBC File Offset: 0x00266EBC
	private void RefreshCategories()
	{
		if (this.RefreshCategoriesFn != null)
		{
			this.RefreshCategoriesFn();
		}
	}

	// Token: 0x06006613 RID: 26131 RVA: 0x00268CD4 File Offset: 0x00266ED4
	public void PopulateCategories()
	{
		this.RefreshCategoriesFn = null;
		this.categoryRowPool.ReturnAll();
		JoyResponseDesignerScreen.JoyResponseCategory[] array = this.joyResponseCategories;
		for (int i = 0; i < array.Length; i++)
		{
			JoyResponseDesignerScreen.<>c__DisplayClass28_0 CS$<>8__locals1 = new JoyResponseDesignerScreen.<>c__DisplayClass28_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.category = array[i];
			GameObject gameObject = this.categoryRowPool.Borrow();
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<LocText>("Label").SetText(CS$<>8__locals1.category.displayName);
			component.GetReference<Image>("Icon").sprite = CS$<>8__locals1.category.icon;
			MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
			MultiToggle toggle2 = toggle;
			toggle2.onEnter = (global::System.Action)Delegate.Combine(toggle2.onEnter, new global::System.Action(this.OnMouseOverToggle));
			toggle.onClick = delegate
			{
				CS$<>8__locals1.<>4__this.SelectCategory(CS$<>8__locals1.category);
			};
			this.RefreshCategoriesFn = (global::System.Action)Delegate.Combine(this.RefreshCategoriesFn, new global::System.Action(delegate
			{
				toggle.ChangeState((CS$<>8__locals1.category == CS$<>8__locals1.<>4__this.selectedCategoryOpt) ? 1 : 0);
			}));
			this.SetCatogoryClickUISound(CS$<>8__locals1.category, toggle);
		}
		this.SelectCategory(this.joyResponseCategories[0]);
	}

	// Token: 0x06006614 RID: 26132 RVA: 0x00268E1B File Offset: 0x0026701B
	public void SelectCategory(JoyResponseDesignerScreen.JoyResponseCategory category)
	{
		this.selectedCategoryOpt = category;
		this.galleryHeaderLabel.text = category.displayName;
		this.RefreshCategories();
		this.PopulateGallery();
		this.RefreshPreview();
	}

	// Token: 0x06006615 RID: 26133 RVA: 0x00268E4C File Offset: 0x0026704C
	private void SetCatogoryClickUISound(JoyResponseDesignerScreen.JoyResponseCategory category, MultiToggle toggle)
	{
	}

	// Token: 0x06006616 RID: 26134 RVA: 0x00268E4E File Offset: 0x0026704E
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06006617 RID: 26135 RVA: 0x00268E64 File Offset: 0x00267064
	public void PopulateGallery()
	{
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		if (this.selectedCategoryOpt.IsNone())
		{
			return;
		}
		JoyResponseDesignerScreen.JoyResponseCategory joyResponseCategory = this.selectedCategoryOpt.Unwrap();
		foreach (JoyResponseDesignerScreen.GalleryItem galleryItem in joyResponseCategory.items)
		{
			this.<PopulateGallery>g__AddGridIcon|36_0(galleryItem);
		}
		this.SelectGalleryItem(joyResponseCategory.items[0]);
	}

	// Token: 0x06006618 RID: 26136 RVA: 0x00268ECB File Offset: 0x002670CB
	public void SelectGalleryItem(JoyResponseDesignerScreen.GalleryItem item)
	{
		this.selectedGalleryItem = item;
		this.RefreshGallery();
		this.RefreshPreview();
	}

	// Token: 0x06006619 RID: 26137 RVA: 0x00268EE0 File Offset: 0x002670E0
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x0600661A RID: 26138 RVA: 0x00268EF2 File Offset: 0x002670F2
	public void RefreshPreview()
	{
		if (this.RefreshPreviewFn != null)
		{
			this.RefreshPreviewFn();
		}
	}

	// Token: 0x0600661B RID: 26139 RVA: 0x00268F07 File Offset: 0x00267107
	public void PopulatePreview()
	{
		this.RefreshPreviewFn = (global::System.Action)Delegate.Combine(this.RefreshPreviewFn, new global::System.Action(delegate
		{
			JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget balloonArtistFacadeTarget = this.selectedGalleryItem as JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget;
			if (balloonArtistFacadeTarget == null)
			{
				throw new NotImplementedException();
			}
			Option<PermitResource> permitResource = balloonArtistFacadeTarget.GetPermitResource();
			this.selectionHeaderLabel.SetText(balloonArtistFacadeTarget.GetName());
			KleiPermitDioramaVis_JoyResponseBalloon kleiPermitDioramaVis_JoyResponseBalloon = this.dioramaVis;
			JoyResponseScreenConfig joyResponseScreenConfig = this.Config;
			kleiPermitDioramaVis_JoyResponseBalloon.SetMinion(joyResponseScreenConfig.target.GetPersonality());
			this.dioramaVis.ConfigureWith(balloonArtistFacadeTarget.permit);
			OutfitDescriptionPanel outfitDescriptionPanel = this.outfitDescriptionPanel;
			PermitResource permitResource2 = permitResource.UnwrapOr(null, null);
			ClothingOutfitUtility.OutfitType outfitType = ClothingOutfitUtility.OutfitType.JoyResponse;
			joyResponseScreenConfig = this.Config;
			outfitDescriptionPanel.Refresh(permitResource2, outfitType, joyResponseScreenConfig.target.GetPersonality());
			Option<string> saveSelectionError = this.GetSaveSelectionError();
			if (saveSelectionError.IsSome())
			{
				this.primaryButton.isInteractable = false;
				this.primaryButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(saveSelectionError.Unwrap());
				return;
			}
			this.primaryButton.isInteractable = true;
			this.primaryButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
		}));
		this.RefreshPreview();
	}

	// Token: 0x0600661C RID: 26140 RVA: 0x00268F31 File Offset: 0x00267131
	private void RegisterPreventScreenPop()
	{
		this.UnregisterPreventScreenPop();
		this.preventScreenPopFn = delegate
		{
			if (this.Config.target.ReadFacadeId() != this.selectedGalleryItem.GetPermitResource().AndThen<string>((PermitResource r) => r.Id))
			{
				this.RegisterPreventScreenPop();
				JoyResponseDesignerScreen.MakeSaveWarningPopup(this.Config.target, delegate
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

	// Token: 0x0600661D RID: 26141 RVA: 0x00268F60 File Offset: 0x00267160
	private void UnregisterPreventScreenPop()
	{
		if (this.preventScreenPopFn != null)
		{
			LockerNavigator.Instance.preventScreenPop.Remove(this.preventScreenPopFn);
			this.preventScreenPopFn = null;
		}
	}

	// Token: 0x0600661E RID: 26142 RVA: 0x00268F88 File Offset: 0x00267188
	public static void MakeSaveWarningPopup(JoyResponseOutfitTarget target, global::System.Action discardChangesFn)
	{
		Action<InfoDialogScreen> <>9__1;
		LockerNavigator.Instance.ShowDialogPopup(delegate(InfoDialogScreen dialog)
		{
			InfoDialogScreen infoDialogScreen = dialog.SetHeader(UI.JOY_RESPONSE_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.HEADER.Replace("{MinionName}", target.GetMinionName())).AddPlainText(UI.OUTFIT_DESIGNER_SCREEN.CHANGES_NOT_SAVED_WARNING_POPUP.BODY);
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

	// Token: 0x06006622 RID: 26146 RVA: 0x002690B8 File Offset: 0x002672B8
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIcon|36_0(JoyResponseDesignerScreen.GalleryItem item)
	{
		GameObject gameObject = this.galleryGridItemPool.Borrow();
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("Icon").sprite = item.GetIcon();
		component.GetReference<Image>("IsUnownedOverlay").gameObject.SetActive(!item.IsUnlocked());
		Option<PermitResource> permitResource = item.GetPermitResource();
		if (permitResource.IsSome())
		{
			KleiItemsUI.ConfigureTooltipOn(gameObject, KleiItemsUI.GetTooltipStringFor(permitResource.Unwrap()));
		}
		else
		{
			KleiItemsUI.ConfigureTooltipOn(gameObject, KleiItemsUI.GetNoneTooltipStringFor(PermitCategory.JoyResponse));
		}
		MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
		MultiToggle toggle3 = toggle;
		toggle3.onEnter = (global::System.Action)Delegate.Combine(toggle3.onEnter, new global::System.Action(this.OnMouseOverToggle));
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
		{
			this.SelectGalleryItem(item);
		}));
		this.RefreshGalleryFn = (global::System.Action)Delegate.Combine(this.RefreshGalleryFn, new global::System.Action(delegate
		{
			toggle.ChangeState((item == this.selectedGalleryItem) ? 1 : 0);
		}));
	}

	// Token: 0x040045E7 RID: 17895
	[Header("CategoryColumn")]
	[SerializeField]
	private RectTransform categoryListContent;

	// Token: 0x040045E8 RID: 17896
	[SerializeField]
	private GameObject categoryRowPrefab;

	// Token: 0x040045E9 RID: 17897
	[Header("GalleryColumn")]
	[SerializeField]
	private LocText galleryHeaderLabel;

	// Token: 0x040045EA RID: 17898
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x040045EB RID: 17899
	[SerializeField]
	private GameObject galleryItemPrefab;

	// Token: 0x040045EC RID: 17900
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private LocText selectionHeaderLabel;

	// Token: 0x040045ED RID: 17901
	[SerializeField]
	private KleiPermitDioramaVis_JoyResponseBalloon dioramaVis;

	// Token: 0x040045EE RID: 17902
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x040045EF RID: 17903
	[SerializeField]
	private KButton primaryButton;

	// Token: 0x040045F1 RID: 17905
	public JoyResponseDesignerScreen.JoyResponseCategory[] joyResponseCategories;

	// Token: 0x040045F2 RID: 17906
	private bool postponeConfiguration = true;

	// Token: 0x040045F3 RID: 17907
	private Option<JoyResponseDesignerScreen.JoyResponseCategory> selectedCategoryOpt;

	// Token: 0x040045F4 RID: 17908
	private UIPrefabLocalPool categoryRowPool;

	// Token: 0x040045F5 RID: 17909
	private global::System.Action RefreshCategoriesFn;

	// Token: 0x040045F6 RID: 17910
	private JoyResponseDesignerScreen.GalleryItem selectedGalleryItem;

	// Token: 0x040045F7 RID: 17911
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x040045F8 RID: 17912
	private GridLayouter galleryGridLayouter;

	// Token: 0x040045F9 RID: 17913
	private global::System.Action RefreshGalleryFn;

	// Token: 0x040045FA RID: 17914
	public global::System.Action RefreshPreviewFn;

	// Token: 0x040045FB RID: 17915
	private Func<bool> preventScreenPopFn;

	// Token: 0x02001EB0 RID: 7856
	public class JoyResponseCategory
	{
		// Token: 0x04008E8B RID: 36491
		public string displayName;

		// Token: 0x04008E8C RID: 36492
		public Sprite icon;

		// Token: 0x04008E8D RID: 36493
		public JoyResponseDesignerScreen.GalleryItem[] items;
	}

	// Token: 0x02001EB1 RID: 7857
	private enum MultiToggleState
	{
		// Token: 0x04008E8F RID: 36495
		Default,
		// Token: 0x04008E90 RID: 36496
		Selected
	}

	// Token: 0x02001EB2 RID: 7858
	public abstract class GalleryItem : IEquatable<JoyResponseDesignerScreen.GalleryItem>
	{
		// Token: 0x0600B118 RID: 45336
		public abstract string GetName();

		// Token: 0x0600B119 RID: 45337
		public abstract Sprite GetIcon();

		// Token: 0x0600B11A RID: 45338
		public abstract string GetUniqueId();

		// Token: 0x0600B11B RID: 45339
		public abstract bool IsUnlocked();

		// Token: 0x0600B11C RID: 45340
		public abstract Option<PermitResource> GetPermitResource();

		// Token: 0x0600B11D RID: 45341 RVA: 0x003D404C File Offset: 0x003D224C
		public override bool Equals(object obj)
		{
			JoyResponseDesignerScreen.GalleryItem galleryItem = obj as JoyResponseDesignerScreen.GalleryItem;
			return galleryItem != null && this.Equals(galleryItem);
		}

		// Token: 0x0600B11E RID: 45342 RVA: 0x003D406C File Offset: 0x003D226C
		public bool Equals(JoyResponseDesignerScreen.GalleryItem other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x0600B11F RID: 45343 RVA: 0x003D407C File Offset: 0x003D227C
		public override int GetHashCode()
		{
			return Hash.SDBMLower(this.GetUniqueId());
		}

		// Token: 0x0600B120 RID: 45344 RVA: 0x003D4089 File Offset: 0x003D2289
		public override string ToString()
		{
			return this.GetUniqueId();
		}

		// Token: 0x0600B121 RID: 45345 RVA: 0x003D4091 File Offset: 0x003D2291
		public static JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget Of(Option<BalloonArtistFacadeResource> permit)
		{
			return new JoyResponseDesignerScreen.GalleryItem.BalloonArtistFacadeTarget
			{
				permit = permit
			};
		}

		// Token: 0x020028FE RID: 10494
		public class BalloonArtistFacadeTarget : JoyResponseDesignerScreen.GalleryItem
		{
			// Token: 0x0600CDEE RID: 52718 RVA: 0x0041E854 File Offset: 0x0041CA54
			public override Sprite GetIcon()
			{
				return this.permit.AndThen<Sprite>((BalloonArtistFacadeResource p) => p.GetPermitPresentationInfo().sprite).UnwrapOrElse(() => KleiItemsUI.GetNoneBalloonArtistIcon(), null);
			}

			// Token: 0x0600CDEF RID: 52719 RVA: 0x0041E8B4 File Offset: 0x0041CAB4
			public override string GetName()
			{
				return this.permit.AndThen<string>((BalloonArtistFacadeResource p) => p.Name).UnwrapOrElse(() => KleiItemsUI.GetNoneClothingItemStrings(PermitCategory.JoyResponse).Item1, null);
			}

			// Token: 0x0600CDF0 RID: 52720 RVA: 0x0041E914 File Offset: 0x0041CB14
			public override string GetUniqueId()
			{
				return "balloon_artist_facade::" + this.permit.AndThen<string>((BalloonArtistFacadeResource p) => p.Id).UnwrapOr("<none>", null);
			}

			// Token: 0x0600CDF1 RID: 52721 RVA: 0x0041E963 File Offset: 0x0041CB63
			public override Option<PermitResource> GetPermitResource()
			{
				return this.permit.AndThen<PermitResource>((BalloonArtistFacadeResource p) => p);
			}

			// Token: 0x0600CDF2 RID: 52722 RVA: 0x0041E990 File Offset: 0x0041CB90
			public override bool IsUnlocked()
			{
				return this.GetPermitResource().AndThen<bool>((PermitResource p) => p.IsUnlocked()).UnwrapOr(true, null);
			}

			// Token: 0x0400B58A RID: 46474
			public Option<BalloonArtistFacadeResource> permit;
		}
	}
}
