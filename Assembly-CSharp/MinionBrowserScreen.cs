using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D6B RID: 3435
public class MinionBrowserScreen : KMonoBehaviour
{
	// Token: 0x06006A74 RID: 27252 RVA: 0x002820E0 File Offset: 0x002802E0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.gridLayouter = new GridLayouter
		{
			minCellSize = 112f,
			maxCellSize = 144f,
			targetGridLayouts = this.galleryGridContent.GetComponents<GridLayoutGroup>().ToList<GridLayoutGroup>()
		};
		this.galleryGridItemPool = new UIPrefabLocalPool(this.gridItemPrefab, this.galleryGridContent.gameObject);
	}

	// Token: 0x06006A75 RID: 27253 RVA: 0x00282148 File Offset: 0x00280348
	protected override void OnCmpEnable()
	{
		if (this.isFirstDisplay)
		{
			this.isFirstDisplay = false;
			this.PopulateGallery();
			this.RefreshPreview();
			this.cycler.Initialize(this.CreateCycleOptions());
			this.editButton.onClick += delegate
			{
				if (this.OnEditClickedFn != null)
				{
					this.OnEditClickedFn();
				}
			};
			this.changeOutfitButton.onClick += this.OnClickChangeOutfit;
		}
		else
		{
			this.RefreshGallery();
			this.RefreshPreview();
		}
		KleiItemsStatusRefresher.AddOrGetListener(this).OnRefreshUI(delegate
		{
			this.RefreshGallery();
			this.RefreshPreview();
		});
	}

	// Token: 0x06006A76 RID: 27254 RVA: 0x002821D4 File Offset: 0x002803D4
	private void Update()
	{
		this.gridLayouter.CheckIfShouldResizeGrid();
	}

	// Token: 0x06006A77 RID: 27255 RVA: 0x002821E4 File Offset: 0x002803E4
	protected override void OnSpawn()
	{
		this.postponeConfiguration = false;
		if (this.Config.isValid)
		{
			this.Configure(this.Config);
			return;
		}
		this.Configure(MinionBrowserScreenConfig.Personalities(default(Option<Personality>)));
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x06006A78 RID: 27256 RVA: 0x00282226 File Offset: 0x00280426
	// (set) Token: 0x06006A79 RID: 27257 RVA: 0x0028222E File Offset: 0x0028042E
	public MinionBrowserScreenConfig Config { get; private set; }

	// Token: 0x06006A7A RID: 27258 RVA: 0x00282237 File Offset: 0x00280437
	public void Configure(MinionBrowserScreenConfig config)
	{
		this.Config = config;
		if (this.postponeConfiguration)
		{
			return;
		}
		this.PopulateGallery();
		this.RefreshPreview();
	}

	// Token: 0x06006A7B RID: 27259 RVA: 0x00282255 File Offset: 0x00280455
	private void RefreshGallery()
	{
		if (this.RefreshGalleryFn != null)
		{
			this.RefreshGalleryFn();
		}
	}

	// Token: 0x06006A7C RID: 27260 RVA: 0x0028226C File Offset: 0x0028046C
	public void PopulateGallery()
	{
		this.RefreshGalleryFn = null;
		this.galleryGridItemPool.ReturnAll();
		foreach (MinionBrowserScreen.GridItem gridItem in this.Config.items)
		{
			this.<PopulateGallery>g__AddGridIcon|32_0(gridItem);
		}
		this.RefreshGallery();
		this.SelectMinion(this.Config.defaultSelectedItem.Unwrap());
	}

	// Token: 0x06006A7D RID: 27261 RVA: 0x002822D0 File Offset: 0x002804D0
	private void SelectMinion(MinionBrowserScreen.GridItem item)
	{
		this.selectedGridItem = item;
		this.RefreshGallery();
		this.RefreshPreview();
		this.UIMinion.GetMinionVoice().PlaySoundUI("voice_land");
	}

	// Token: 0x06006A7E RID: 27262 RVA: 0x00282308 File Offset: 0x00280508
	public void RefreshPreview()
	{
		this.UIMinion.SetMinion(this.selectedGridItem.GetPersonality());
		this.UIMinion.ReactToPersonalityChange();
		this.detailsHeaderText.SetText(this.selectedGridItem.GetName());
		this.detailHeaderIcon.sprite = this.selectedGridItem.GetIcon();
		this.RefreshOutfitDescription();
		this.RefreshPreviewButtonsInteractable();
		this.SetDioramaBG();
	}

	// Token: 0x06006A7F RID: 27263 RVA: 0x00282374 File Offset: 0x00280574
	private void RefreshOutfitDescription()
	{
		if (this.RefreshOutfitDescriptionFn != null)
		{
			this.RefreshOutfitDescriptionFn();
		}
	}

	// Token: 0x06006A80 RID: 27264 RVA: 0x0028238C File Offset: 0x0028058C
	private void OnClickChangeOutfit()
	{
		if (this.selectedOutfitType.IsNone())
		{
			return;
		}
		OutfitBrowserScreenConfig.Minion(this.selectedOutfitType.Unwrap(), this.selectedGridItem).WithOutfit(this.selectedOutfit).ApplyAndOpenScreen();
	}

	// Token: 0x06006A81 RID: 27265 RVA: 0x002823D4 File Offset: 0x002805D4
	private void RefreshPreviewButtonsInteractable()
	{
		this.editButton.isInteractable = true;
		if (this.currentOutfitType == ClothingOutfitUtility.OutfitType.JoyResponse)
		{
			Option<string> joyResponseEditError = this.GetJoyResponseEditError();
			if (joyResponseEditError.IsSome())
			{
				this.editButton.isInteractable = false;
				this.editButton.gameObject.AddOrGet<ToolTip>().SetSimpleTooltip(joyResponseEditError.Unwrap());
				return;
			}
			this.editButton.isInteractable = true;
			this.editButton.gameObject.AddOrGet<ToolTip>().ClearMultiStringTooltip();
		}
	}

	// Token: 0x06006A82 RID: 27266 RVA: 0x00282450 File Offset: 0x00280650
	private void SetDioramaBG()
	{
		this.dioramaBGImage.sprite = KleiPermitDioramaVis.GetDioramaBackground(this.currentOutfitType);
	}

	// Token: 0x06006A83 RID: 27267 RVA: 0x00282468 File Offset: 0x00280668
	private Option<string> GetJoyResponseEditError()
	{
		string joyTrait = this.selectedGridItem.GetPersonality().joyTrait;
		if (!(joyTrait == "BalloonArtist"))
		{
			return Option.Some<string>(UI.JOY_RESPONSE_DESIGNER_SCREEN.TOOLTIP_NO_FACADES_FOR_JOY_TRAIT.Replace("{JoyResponseType}", Db.Get().traits.Get(joyTrait).Name));
		}
		return Option.None;
	}

	// Token: 0x06006A84 RID: 27268 RVA: 0x002824C8 File Offset: 0x002806C8
	public void SetEditingOutfitType(ClothingOutfitUtility.OutfitType outfitType)
	{
		this.currentOutfitType = outfitType;
		switch (outfitType)
		{
		case ClothingOutfitUtility.OutfitType.Clothing:
			this.editButtonText.text = UI.MINION_BROWSER_SCREEN.BUTTON_EDIT_OUTFIT_ITEMS;
			this.changeOutfitButton.gameObject.SetActive(true);
			break;
		case ClothingOutfitUtility.OutfitType.JoyResponse:
			this.editButtonText.text = UI.MINION_BROWSER_SCREEN.BUTTON_EDIT_JOY_RESPONSE;
			this.changeOutfitButton.gameObject.SetActive(false);
			break;
		case ClothingOutfitUtility.OutfitType.AtmoSuit:
			this.editButtonText.text = UI.MINION_BROWSER_SCREEN.BUTTON_EDIT_ATMO_SUIT_OUTFIT_ITEMS;
			this.changeOutfitButton.gameObject.SetActive(true);
			break;
		default:
			throw new NotImplementedException();
		}
		this.RefreshPreviewButtonsInteractable();
		this.OnEditClickedFn = delegate
		{
			switch (outfitType)
			{
			case ClothingOutfitUtility.OutfitType.Clothing:
			case ClothingOutfitUtility.OutfitType.AtmoSuit:
				OutfitDesignerScreenConfig.Minion(this.selectedOutfit.IsSome() ? this.selectedOutfit.Unwrap() : ClothingOutfitTarget.ForNewTemplateOutfit(outfitType), this.selectedGridItem).ApplyAndOpenScreen();
				return;
			case ClothingOutfitUtility.OutfitType.JoyResponse:
			{
				JoyResponseScreenConfig joyResponseScreenConfig = JoyResponseScreenConfig.From(this.selectedGridItem);
				joyResponseScreenConfig = joyResponseScreenConfig.WithInitialSelection(this.selectedGridItem.GetJoyResponseOutfitTarget().ReadFacadeId().AndThen<BalloonArtistFacadeResource>((string id) => Db.Get().Permits.BalloonArtistFacades.Get(id)));
				joyResponseScreenConfig.ApplyAndOpenScreen();
				return;
			}
			default:
				throw new NotImplementedException();
			}
		};
		this.RefreshOutfitDescriptionFn = delegate
		{
			switch (outfitType)
			{
			case ClothingOutfitUtility.OutfitType.Clothing:
			case ClothingOutfitUtility.OutfitType.AtmoSuit:
				this.selectedOutfit = this.selectedGridItem.GetClothingOutfitTarget(outfitType);
				this.UIMinion.SetOutfit(outfitType, this.selectedOutfit);
				this.outfitDescriptionPanel.Refresh(this.selectedOutfit, outfitType, this.selectedGridItem.GetPersonality());
				return;
			case ClothingOutfitUtility.OutfitType.JoyResponse:
			{
				this.selectedOutfit = this.selectedGridItem.GetClothingOutfitTarget(ClothingOutfitUtility.OutfitType.Clothing);
				this.UIMinion.SetOutfit(ClothingOutfitUtility.OutfitType.Clothing, this.selectedOutfit);
				string text = this.selectedGridItem.GetJoyResponseOutfitTarget().ReadFacadeId().UnwrapOr(null, null);
				this.outfitDescriptionPanel.Refresh((text != null) ? Db.Get().Permits.Get(text) : null, outfitType, this.selectedGridItem.GetPersonality());
				return;
			}
			default:
				throw new NotImplementedException();
			}
		};
		this.RefreshOutfitDescription();
	}

	// Token: 0x06006A85 RID: 27269 RVA: 0x002825C0 File Offset: 0x002807C0
	private MinionBrowserScreen.CyclerUI.OnSelectedFn[] CreateCycleOptions()
	{
		MinionBrowserScreen.CyclerUI.OnSelectedFn[] array = new MinionBrowserScreen.CyclerUI.OnSelectedFn[3];
		for (int i = 0; i < 3; i++)
		{
			ClothingOutfitUtility.OutfitType outfitType = (ClothingOutfitUtility.OutfitType)i;
			array[i] = delegate
			{
				this.selectedOutfitType = Option.Some<ClothingOutfitUtility.OutfitType>(outfitType);
				this.cycler.SetLabel(outfitType.GetName());
				this.SetEditingOutfitType(outfitType);
				this.RefreshPreview();
			};
		}
		return array;
	}

	// Token: 0x06006A86 RID: 27270 RVA: 0x00282604 File Offset: 0x00280804
	private void OnMouseOverToggle()
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Mouseover", false));
	}

	// Token: 0x06006A8A RID: 27274 RVA: 0x00282650 File Offset: 0x00280850
	[CompilerGenerated]
	private void <PopulateGallery>g__AddGridIcon|32_0(MinionBrowserScreen.GridItem item)
	{
		GameObject gameObject = this.galleryGridItemPool.Borrow();
		gameObject.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = item.GetIcon();
		gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(item.GetName());
		string requiredDlcId = item.GetPersonality().requiredDlcId;
		ToolTip component = gameObject.GetComponent<ToolTip>();
		Image component2 = gameObject.transform.Find("DlcBanner").GetComponent<Image>();
		if (DlcManager.IsDlcId(requiredDlcId))
		{
			component2.gameObject.SetActive(true);
			component2.color = DlcManager.GetDlcBannerColor(requiredDlcId);
			component.SetSimpleTooltip(string.Format(UI.MINION_BROWSER_SCREEN.TOOLTIP_FROM_DLC, DlcManager.GetDlcTitle(requiredDlcId)));
		}
		else
		{
			component2.gameObject.SetActive(false);
			component.ClearMultiStringTooltip();
		}
		MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
		MultiToggle toggle3 = toggle;
		toggle3.onEnter = (global::System.Action)Delegate.Combine(toggle3.onEnter, new global::System.Action(this.OnMouseOverToggle));
		MultiToggle toggle2 = toggle;
		toggle2.onClick = (global::System.Action)Delegate.Combine(toggle2.onClick, new global::System.Action(delegate
		{
			this.SelectMinion(item);
		}));
		this.RefreshGalleryFn = (global::System.Action)Delegate.Combine(this.RefreshGalleryFn, new global::System.Action(delegate
		{
			toggle.ChangeState((item == this.selectedGridItem) ? 1 : 0);
		}));
	}

	// Token: 0x04004893 RID: 18579
	[Header("ItemGalleryColumn")]
	[SerializeField]
	private RectTransform galleryGridContent;

	// Token: 0x04004894 RID: 18580
	[SerializeField]
	private GameObject gridItemPrefab;

	// Token: 0x04004895 RID: 18581
	private GridLayouter gridLayouter;

	// Token: 0x04004896 RID: 18582
	[Header("SelectionDetailsColumn")]
	[SerializeField]
	private KleiPermitDioramaVis permitVis;

	// Token: 0x04004897 RID: 18583
	[SerializeField]
	private UIMinion UIMinion;

	// Token: 0x04004898 RID: 18584
	[SerializeField]
	private LocText detailsHeaderText;

	// Token: 0x04004899 RID: 18585
	[SerializeField]
	private Image detailHeaderIcon;

	// Token: 0x0400489A RID: 18586
	[SerializeField]
	private OutfitDescriptionPanel outfitDescriptionPanel;

	// Token: 0x0400489B RID: 18587
	[SerializeField]
	private MinionBrowserScreen.CyclerUI cycler;

	// Token: 0x0400489C RID: 18588
	[SerializeField]
	private KButton editButton;

	// Token: 0x0400489D RID: 18589
	[SerializeField]
	private LocText editButtonText;

	// Token: 0x0400489E RID: 18590
	[SerializeField]
	private KButton changeOutfitButton;

	// Token: 0x0400489F RID: 18591
	private Option<ClothingOutfitUtility.OutfitType> selectedOutfitType;

	// Token: 0x040048A0 RID: 18592
	private Option<ClothingOutfitTarget> selectedOutfit;

	// Token: 0x040048A1 RID: 18593
	[Header("Diorama Backgrounds")]
	[SerializeField]
	private Image dioramaBGImage;

	// Token: 0x040048A2 RID: 18594
	private MinionBrowserScreen.GridItem selectedGridItem;

	// Token: 0x040048A3 RID: 18595
	private global::System.Action OnEditClickedFn;

	// Token: 0x040048A4 RID: 18596
	private bool isFirstDisplay = true;

	// Token: 0x040048A6 RID: 18598
	private bool postponeConfiguration = true;

	// Token: 0x040048A7 RID: 18599
	private UIPrefabLocalPool galleryGridItemPool;

	// Token: 0x040048A8 RID: 18600
	private global::System.Action RefreshGalleryFn;

	// Token: 0x040048A9 RID: 18601
	private global::System.Action RefreshOutfitDescriptionFn;

	// Token: 0x040048AA RID: 18602
	private ClothingOutfitUtility.OutfitType currentOutfitType;

	// Token: 0x02001F45 RID: 8005
	private enum MultiToggleState
	{
		// Token: 0x0400902B RID: 36907
		Default,
		// Token: 0x0400902C RID: 36908
		Selected,
		// Token: 0x0400902D RID: 36909
		NonInteractable
	}

	// Token: 0x02001F46 RID: 8006
	[Serializable]
	public class CyclerUI
	{
		// Token: 0x0600B2C5 RID: 45765 RVA: 0x003D7DA8 File Offset: 0x003D5FA8
		public void Initialize(MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions)
		{
			this.cyclePrevButton.onClick += this.CyclePrev;
			this.cycleNextButton.onClick += this.CycleNext;
			this.SetCycleOptions(cycleOptions);
		}

		// Token: 0x0600B2C6 RID: 45766 RVA: 0x003D7DDF File Offset: 0x003D5FDF
		public void SetCycleOptions(MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions)
		{
			DebugUtil.Assert(cycleOptions != null);
			DebugUtil.Assert(cycleOptions.Length != 0);
			this.cycleOptions = cycleOptions;
			this.GoTo(0);
		}

		// Token: 0x0600B2C7 RID: 45767 RVA: 0x003D7E04 File Offset: 0x003D6004
		public void GoTo(int wrappingIndex)
		{
			if (this.cycleOptions == null || this.cycleOptions.Length == 0)
			{
				return;
			}
			while (wrappingIndex < 0)
			{
				wrappingIndex += this.cycleOptions.Length;
			}
			while (wrappingIndex >= this.cycleOptions.Length)
			{
				wrappingIndex -= this.cycleOptions.Length;
			}
			this.selectedIndex = wrappingIndex;
			this.cycleOptions[this.selectedIndex]();
		}

		// Token: 0x0600B2C8 RID: 45768 RVA: 0x003D7E65 File Offset: 0x003D6065
		public void CyclePrev()
		{
			this.GoTo(this.selectedIndex - 1);
		}

		// Token: 0x0600B2C9 RID: 45769 RVA: 0x003D7E75 File Offset: 0x003D6075
		public void CycleNext()
		{
			this.GoTo(this.selectedIndex + 1);
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x003D7E85 File Offset: 0x003D6085
		public void SetLabel(string text)
		{
			this.currentLabel.text = text;
		}

		// Token: 0x0400902E RID: 36910
		[SerializeField]
		public KButton cyclePrevButton;

		// Token: 0x0400902F RID: 36911
		[SerializeField]
		public KButton cycleNextButton;

		// Token: 0x04009030 RID: 36912
		[SerializeField]
		public LocText currentLabel;

		// Token: 0x04009031 RID: 36913
		[NonSerialized]
		private int selectedIndex = -1;

		// Token: 0x04009032 RID: 36914
		[NonSerialized]
		private MinionBrowserScreen.CyclerUI.OnSelectedFn[] cycleOptions;

		// Token: 0x02002902 RID: 10498
		// (Invoke) Token: 0x0600CE01 RID: 52737
		public delegate void OnSelectedFn();
	}

	// Token: 0x02001F47 RID: 8007
	public abstract class GridItem : IEquatable<MinionBrowserScreen.GridItem>
	{
		// Token: 0x0600B2CC RID: 45772
		public abstract string GetName();

		// Token: 0x0600B2CD RID: 45773
		public abstract Sprite GetIcon();

		// Token: 0x0600B2CE RID: 45774
		public abstract string GetUniqueId();

		// Token: 0x0600B2CF RID: 45775
		public abstract Personality GetPersonality();

		// Token: 0x0600B2D0 RID: 45776
		public abstract Option<ClothingOutfitTarget> GetClothingOutfitTarget(ClothingOutfitUtility.OutfitType outfitType);

		// Token: 0x0600B2D1 RID: 45777
		public abstract JoyResponseOutfitTarget GetJoyResponseOutfitTarget();

		// Token: 0x0600B2D2 RID: 45778 RVA: 0x003D7EA4 File Offset: 0x003D60A4
		public override bool Equals(object obj)
		{
			MinionBrowserScreen.GridItem gridItem = obj as MinionBrowserScreen.GridItem;
			return gridItem != null && this.Equals(gridItem);
		}

		// Token: 0x0600B2D3 RID: 45779 RVA: 0x003D7EC4 File Offset: 0x003D60C4
		public bool Equals(MinionBrowserScreen.GridItem other)
		{
			return this.GetHashCode() == other.GetHashCode();
		}

		// Token: 0x0600B2D4 RID: 45780 RVA: 0x003D7ED4 File Offset: 0x003D60D4
		public override int GetHashCode()
		{
			return Hash.SDBMLower(this.GetUniqueId());
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x003D7EE1 File Offset: 0x003D60E1
		public override string ToString()
		{
			return this.GetUniqueId();
		}

		// Token: 0x0600B2D6 RID: 45782 RVA: 0x003D7EEC File Offset: 0x003D60EC
		public static MinionBrowserScreen.GridItem.MinionInstanceTarget Of(GameObject minionInstance)
		{
			MinionIdentity component = minionInstance.GetComponent<MinionIdentity>();
			return new MinionBrowserScreen.GridItem.MinionInstanceTarget
			{
				minionInstance = minionInstance,
				minionIdentity = component,
				personality = Db.Get().Personalities.Get(component.personalityResourceId)
			};
		}

		// Token: 0x0600B2D7 RID: 45783 RVA: 0x003D7F2E File Offset: 0x003D612E
		public static MinionBrowserScreen.GridItem.PersonalityTarget Of(Personality personality)
		{
			return new MinionBrowserScreen.GridItem.PersonalityTarget
			{
				personality = personality
			};
		}

		// Token: 0x02002903 RID: 10499
		public class MinionInstanceTarget : MinionBrowserScreen.GridItem
		{
			// Token: 0x0600CE04 RID: 52740 RVA: 0x0041E9DC File Offset: 0x0041CBDC
			public override Sprite GetIcon()
			{
				return this.personality.GetMiniIcon();
			}

			// Token: 0x0600CE05 RID: 52741 RVA: 0x0041E9E9 File Offset: 0x0041CBE9
			public override string GetName()
			{
				return this.minionIdentity.GetProperName();
			}

			// Token: 0x0600CE06 RID: 52742 RVA: 0x0041E9F8 File Offset: 0x0041CBF8
			public override string GetUniqueId()
			{
				return "minion_instance_id::" + this.minionInstance.GetInstanceID().ToString();
			}

			// Token: 0x0600CE07 RID: 52743 RVA: 0x0041EA22 File Offset: 0x0041CC22
			public override Personality GetPersonality()
			{
				return this.personality;
			}

			// Token: 0x0600CE08 RID: 52744 RVA: 0x0041EA2A File Offset: 0x0041CC2A
			public override Option<ClothingOutfitTarget> GetClothingOutfitTarget(ClothingOutfitUtility.OutfitType outfitType)
			{
				return ClothingOutfitTarget.FromMinion(outfitType, this.minionInstance);
			}

			// Token: 0x0600CE09 RID: 52745 RVA: 0x0041EA3D File Offset: 0x0041CC3D
			public override JoyResponseOutfitTarget GetJoyResponseOutfitTarget()
			{
				return JoyResponseOutfitTarget.FromMinion(this.minionInstance);
			}

			// Token: 0x0400B58B RID: 46475
			public GameObject minionInstance;

			// Token: 0x0400B58C RID: 46476
			public MinionIdentity minionIdentity;

			// Token: 0x0400B58D RID: 46477
			public Personality personality;
		}

		// Token: 0x02002904 RID: 10500
		public class PersonalityTarget : MinionBrowserScreen.GridItem
		{
			// Token: 0x0600CE0B RID: 52747 RVA: 0x0041EA52 File Offset: 0x0041CC52
			public override Sprite GetIcon()
			{
				return this.personality.GetMiniIcon();
			}

			// Token: 0x0600CE0C RID: 52748 RVA: 0x0041EA5F File Offset: 0x0041CC5F
			public override string GetName()
			{
				return this.personality.Name;
			}

			// Token: 0x0600CE0D RID: 52749 RVA: 0x0041EA6C File Offset: 0x0041CC6C
			public override string GetUniqueId()
			{
				return "personality::" + this.personality.nameStringKey;
			}

			// Token: 0x0600CE0E RID: 52750 RVA: 0x0041EA83 File Offset: 0x0041CC83
			public override Personality GetPersonality()
			{
				return this.personality;
			}

			// Token: 0x0600CE0F RID: 52751 RVA: 0x0041EA8B File Offset: 0x0041CC8B
			public override Option<ClothingOutfitTarget> GetClothingOutfitTarget(ClothingOutfitUtility.OutfitType outfitType)
			{
				return ClothingOutfitTarget.TryFromTemplateId(this.personality.GetSelectedTemplateOutfitId(outfitType));
			}

			// Token: 0x0600CE10 RID: 52752 RVA: 0x0041EA9E File Offset: 0x0041CC9E
			public override JoyResponseOutfitTarget GetJoyResponseOutfitTarget()
			{
				return JoyResponseOutfitTarget.FromPersonality(this.personality);
			}

			// Token: 0x0400B58E RID: 46478
			public Personality personality;
		}
	}
}
