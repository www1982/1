using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FMOD.Studio;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000D98 RID: 3480
public class PlanScreen : KIconToggleMenu
{
	// Token: 0x17000796 RID: 1942
	// (get) Token: 0x06006CAD RID: 27821 RVA: 0x00291064 File Offset: 0x0028F264
	// (set) Token: 0x06006CAE RID: 27822 RVA: 0x0029106B File Offset: 0x0028F26B
	public static PlanScreen Instance { get; private set; }

	// Token: 0x06006CAF RID: 27823 RVA: 0x00291073 File Offset: 0x0028F273
	public static void DestroyInstance()
	{
		PlanScreen.Instance = null;
	}

	// Token: 0x17000797 RID: 1943
	// (get) Token: 0x06006CB0 RID: 27824 RVA: 0x0029107B File Offset: 0x0028F27B
	public static Dictionary<HashedString, string> IconNameMap
	{
		get
		{
			return PlanScreen.iconNameMap;
		}
	}

	// Token: 0x06006CB1 RID: 27825 RVA: 0x00291082 File Offset: 0x0028F282
	private static HashedString CacheHashedString(string str)
	{
		return HashCache.Get().Add(str);
	}

	// Token: 0x17000798 RID: 1944
	// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x0029108F File Offset: 0x0028F28F
	// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x00291097 File Offset: 0x0028F297
	public ProductInfoScreen ProductInfoScreen { get; private set; }

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x002910A0 File Offset: 0x0028F2A0
	public KIconToggleMenu.ToggleInfo ActiveCategoryToggleInfo
	{
		get
		{
			return this.activeCategoryInfo;
		}
	}

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x06006CB5 RID: 27829 RVA: 0x002910A8 File Offset: 0x0028F2A8
	// (set) Token: 0x06006CB6 RID: 27830 RVA: 0x002910B0 File Offset: 0x0028F2B0
	public GameObject SelectedBuildingGameObject { get; private set; }

	// Token: 0x06006CB7 RID: 27831 RVA: 0x002910B9 File Offset: 0x0028F2B9
	public override float GetSortKey()
	{
		return 2f;
	}

	// Token: 0x06006CB8 RID: 27832 RVA: 0x002910C0 File Offset: 0x0028F2C0
	public PlanScreen.RequirementsState GetBuildableState(BuildingDef def)
	{
		if (def == null)
		{
			return PlanScreen.RequirementsState.Materials;
		}
		return this._buildableStatesByID[def.PrefabID];
	}

	// Token: 0x06006CB9 RID: 27833 RVA: 0x002910E0 File Offset: 0x0028F2E0
	private bool IsDefResearched(BuildingDef def)
	{
		bool flag = false;
		if (!this._researchedDefs.TryGetValue(def, out flag))
		{
			flag = this.UpdateDefResearched(def);
		}
		return flag;
	}

	// Token: 0x06006CBA RID: 27834 RVA: 0x00291108 File Offset: 0x0028F308
	private bool UpdateDefResearched(BuildingDef def)
	{
		return this._researchedDefs[def] = Db.Get().TechItems.IsTechItemComplete(def.PrefabID);
	}

	// Token: 0x06006CBB RID: 27835 RVA: 0x0029113C File Offset: 0x0028F33C
	protected override void OnPrefabInit()
	{
		if (BuildMenu.UseHotkeyBuildMenu())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.OnPrefabInit();
			PlanScreen.Instance = this;
			this.ProductInfoScreen = global::Util.KInstantiateUI<ProductInfoScreen>(this.productInfoScreenPrefab, this.recipeInfoScreenParent, false);
			this.ProductInfoScreen.rectTransform().pivot = new Vector2(0f, 0f);
			this.ProductInfoScreen.rectTransform().SetLocalPosition(new Vector3(326f, 0f, 0f));
			this.ProductInfoScreen.onElementsFullySelected = new global::System.Action(this.OnRecipeElementsFullySelected);
			KInputManager.InputChange.AddListener(new UnityAction(this.RefreshToolTip));
			this.planScreenScrollRect = base.transform.parent.GetComponentInParent<KScrollRect>();
			Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
			Game.Instance.Subscribe(1174281782, new Action<object>(this.OnActiveToolChanged));
			Game.Instance.Subscribe(1557339983, new Action<object>(this.ForceUpdateAllCategoryToggles));
		}
		this.buildingGroupsRoot.gameObject.SetActive(false);
	}

	// Token: 0x06006CBC RID: 27836 RVA: 0x00291274 File Offset: 0x0028F474
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.useSubCategoryLayout = KPlayerPrefs.GetInt("usePlanScreenListView") == 1;
		this.initTime = KTime.Instance.UnscaledGameTime;
		foreach (BuildingDef buildingDef in Assets.BuildingDefs)
		{
			this._buildableStatesByID.Add(buildingDef.PrefabID, PlanScreen.RequirementsState.Materials);
		}
		if (BuildMenu.UseHotkeyBuildMenu())
		{
			base.gameObject.SetActive(false);
		}
		else
		{
			base.onSelect += this.OnClickCategory;
			this.Refresh();
			foreach (KToggle ktoggle in this.toggles)
			{
				ktoggle.group = base.GetComponent<ToggleGroup>();
			}
			this.RefreshBuildableStates(true);
			Game.Instance.Subscribe(288942073, new Action<object>(this.OnUIClear));
		}
		this.copyBuildingButton.GetComponent<MultiToggle>().onClick = delegate
		{
			this.OnClickCopyBuilding();
		};
		this.RefreshCopyBuildingButton(null);
		Game.Instance.Subscribe(-1503271301, new Action<object>(this.RefreshCopyBuildingButton));
		Game.Instance.Subscribe(1983128072, delegate(object data)
		{
			this.CloseRecipe(false);
		});
		this.pointerEnterActions = (KScreen.PointerEnterActions)Delegate.Combine(this.pointerEnterActions, new KScreen.PointerEnterActions(this.PointerEnter));
		this.pointerExitActions = (KScreen.PointerExitActions)Delegate.Combine(this.pointerExitActions, new KScreen.PointerExitActions(this.PointerExit));
		this.copyBuildingButton.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.COPY_BUILDING_TOOLTIP, global::Action.CopyBuilding));
		this.RefreshScale(null);
		this.refreshScaleHandle = Game.Instance.Subscribe(-442024484, new Action<object>(this.RefreshScale));
		this.CacheSearchCaches();
		this.BuildButtonList();
		this.gridViewButton.onClick += this.OnClickGridView;
		this.listViewButton.onClick += this.OnClickListView;
	}

	// Token: 0x06006CBD RID: 27837 RVA: 0x002914BC File Offset: 0x0028F6BC
	private void RefreshScale(object data = null)
	{
		base.GetComponent<GridLayoutGroup>().cellSize = (ScreenResolutionMonitor.UsingGamepadUIMode() ? new Vector2(54f, 50f) : new Vector2(45f, 45f));
		this.toggles.ForEach(delegate(KToggle to)
		{
			to.GetComponentInChildren<LocText>().fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
		});
		LayoutElement component = this.copyBuildingButton.GetComponent<LayoutElement>();
		component.minWidth = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? 58 : 54);
		component.minHeight = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? 58 : 54);
		base.gameObject.rectTransform().anchoredPosition = new Vector2(0f, (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? (-68) : (-74)));
		this.adjacentPinnedButtons.GetComponent<HorizontalLayoutGroup>().padding.bottom = (ScreenResolutionMonitor.UsingGamepadUIMode() ? 14 : 6);
		Vector2 sizeDelta = this.buildingGroupsRoot.rectTransform().sizeDelta;
		Vector2 vector = (ScreenResolutionMonitor.UsingGamepadUIMode() ? new Vector2(320f, sizeDelta.y) : new Vector2(264f, sizeDelta.y));
		this.buildingGroupsRoot.rectTransform().sizeDelta = vector;
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
			GridLayoutGroup componentInChildren = keyValuePair.Value.GetComponentInChildren<GridLayoutGroup>(true);
			if (this.useSubCategoryLayout)
			{
				componentInChildren.constraintCount = 1;
				componentInChildren.cellSize = new Vector2(vector.x - 24f, 36f);
			}
			else
			{
				componentInChildren.constraintCount = 3;
				componentInChildren.cellSize = (ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.bigBuildingButtonSize : PlanScreen.standarduildingButtonSize);
			}
		}
		this.ProductInfoScreen.rectTransform().anchoredPosition = new Vector2(vector.x + 8f, this.ProductInfoScreen.rectTransform().anchoredPosition.y);
	}

	// Token: 0x06006CBE RID: 27838 RVA: 0x002916C4 File Offset: 0x0028F8C4
	protected override void OnForcedCleanUp()
	{
		KInputManager.InputChange.RemoveListener(new UnityAction(this.RefreshToolTip));
		base.OnForcedCleanUp();
	}

	// Token: 0x06006CBF RID: 27839 RVA: 0x002916E2 File Offset: 0x0028F8E2
	protected override void OnCleanUp()
	{
		if (Game.Instance != null)
		{
			Game.Instance.Unsubscribe(this.refreshScaleHandle);
		}
		base.OnCleanUp();
	}

	// Token: 0x06006CC0 RID: 27840 RVA: 0x00291708 File Offset: 0x0028F908
	private void OnClickCopyBuilding()
	{
		if (!this.LastSelectedBuilding.IsNullOrDestroyed() && this.LastSelectedBuilding.gameObject.activeInHierarchy && (!this.lastSelectedBuilding.Def.DebugOnly || DebugHandler.InstantBuildMode))
		{
			PlanScreen.Instance.CopyBuildingOrder(this.LastSelectedBuilding);
			return;
		}
		if (this.lastSelectedBuildingDef != null && (!this.lastSelectedBuildingDef.DebugOnly || DebugHandler.InstantBuildMode))
		{
			PlanScreen.Instance.CopyBuildingOrder(this.lastSelectedBuildingDef, this.LastSelectedBuildingFacade);
		}
	}

	// Token: 0x06006CC1 RID: 27841 RVA: 0x00291796 File Offset: 0x0028F996
	private void OnClickListView()
	{
		this.useSubCategoryLayout = true;
		this.BuildButtonList();
		this.ConfigurePanelSize(null);
		this.RefreshScale(null);
		KPlayerPrefs.SetInt("usePlanScreenListView", 1);
	}

	// Token: 0x06006CC2 RID: 27842 RVA: 0x002917BE File Offset: 0x0028F9BE
	private void OnClickGridView()
	{
		this.useSubCategoryLayout = false;
		this.BuildButtonList();
		this.ConfigurePanelSize(null);
		this.RefreshScale(null);
		KPlayerPrefs.SetInt("usePlanScreenListView", 0);
	}

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x002917E6 File Offset: 0x0028F9E6
	// (set) Token: 0x06006CC4 RID: 27844 RVA: 0x002917F0 File Offset: 0x0028F9F0
	private Building LastSelectedBuilding
	{
		get
		{
			return this.lastSelectedBuilding;
		}
		set
		{
			this.lastSelectedBuilding = value;
			if (this.lastSelectedBuilding != null)
			{
				this.lastSelectedBuildingDef = this.lastSelectedBuilding.Def;
				if (this.lastSelectedBuilding.gameObject.activeInHierarchy)
				{
					this.LastSelectedBuildingFacade = this.lastSelectedBuilding.GetComponent<BuildingFacade>().CurrentFacade;
				}
			}
		}
	}

	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x06006CC5 RID: 27845 RVA: 0x0029184B File Offset: 0x0028FA4B
	// (set) Token: 0x06006CC6 RID: 27846 RVA: 0x00291853 File Offset: 0x0028FA53
	public string LastSelectedBuildingFacade
	{
		get
		{
			return this.lastSelectedBuildingFacade;
		}
		set
		{
			this.lastSelectedBuildingFacade = value;
		}
	}

	// Token: 0x06006CC7 RID: 27847 RVA: 0x0029185C File Offset: 0x0028FA5C
	public void RefreshCopyBuildingButton(object data = null)
	{
		this.adjacentPinnedButtons.rectTransform().anchoredPosition = new Vector2(Mathf.Min(base.gameObject.rectTransform().sizeDelta.x, base.transform.parent.rectTransform().rect.width), 0f);
		MultiToggle component = this.copyBuildingButton.GetComponent<MultiToggle>();
		if (SelectTool.Instance != null && SelectTool.Instance.selected != null)
		{
			Building component2 = SelectTool.Instance.selected.GetComponent<Building>();
			if (component2 != null && component2.Def.ShouldShowInBuildMenu() && component2.Def.IsAvailable())
			{
				this.LastSelectedBuilding = component2;
			}
		}
		if (this.lastSelectedBuildingDef != null)
		{
			component.gameObject.SetActive(PlanScreen.Instance.gameObject.activeInHierarchy);
			Sprite sprite = this.lastSelectedBuildingDef.GetUISprite("ui", false);
			if (this.LastSelectedBuildingFacade != null && this.LastSelectedBuildingFacade != "DEFAULT_FACADE" && Db.Get().Permits.BuildingFacades.TryGet(this.LastSelectedBuildingFacade) != null)
			{
				sprite = Def.GetFacadeUISprite(this.LastSelectedBuildingFacade);
			}
			component.transform.Find("FG").GetComponent<Image>().sprite = sprite;
			component.transform.Find("FG").GetComponent<Image>().color = Color.white;
			component.ChangeState(1);
			return;
		}
		component.gameObject.SetActive(false);
		component.ChangeState(0);
	}

	// Token: 0x06006CC8 RID: 27848 RVA: 0x002919F4 File Offset: 0x0028FBF4
	public void RefreshToolTip()
	{
		for (int i = 0; i < global::TUNING.BUILDINGS.PLANORDER.Count; i++)
		{
			PlanScreen.PlanInfo planInfo = global::TUNING.BUILDINGS.PLANORDER[i];
			if (Game.IsCorrectDlcActiveForCurrentSave(planInfo))
			{
				global::Action action = ((i < 14) ? (global::Action.Plan1 + i) : global::Action.NumActions);
				string text = HashCache.Get().Get(planInfo.category).ToUpper();
				this.toggleInfo[i].tooltip = GameUtil.ReplaceHotkeyString(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text + ".TOOLTIP"), action);
			}
		}
		this.copyBuildingButton.GetComponent<ToolTip>().SetSimpleTooltip(GameUtil.ReplaceHotkeyString(UI.COPY_BUILDING_TOOLTIP, global::Action.CopyBuilding));
	}

	// Token: 0x06006CC9 RID: 27849 RVA: 0x00291AAC File Offset: 0x0028FCAC
	public void Refresh()
	{
		List<KIconToggleMenu.ToggleInfo> list = new List<KIconToggleMenu.ToggleInfo>();
		if (this.tagCategoryMap == null)
		{
			int num = 0;
			this.tagCategoryMap = new Dictionary<Tag, HashedString>();
			this.tagOrderMap = new Dictionary<Tag, int>();
			if (global::TUNING.BUILDINGS.PLANORDER.Count > 15)
			{
				DebugUtil.LogWarningArgs(new object[]
				{
					"Insufficient keys to cover root plan menu",
					"Max of 14 keys supported but TUNING.BUILDINGS.PLANORDER has " + global::TUNING.BUILDINGS.PLANORDER.Count.ToString()
				});
			}
			this.toggleEntries.Clear();
			for (int i = 0; i < global::TUNING.BUILDINGS.PLANORDER.Count; i++)
			{
				PlanScreen.PlanInfo planInfo = global::TUNING.BUILDINGS.PLANORDER[i];
				if (Game.IsCorrectDlcActiveForCurrentSave(planInfo))
				{
					global::Action action = ((i < 15) ? (global::Action.Plan1 + i) : global::Action.NumActions);
					string text = PlanScreen.iconNameMap[planInfo.category];
					string text2 = HashCache.Get().Get(planInfo.category).ToUpper();
					KIconToggleMenu.ToggleInfo toggleInfo = new KIconToggleMenu.ToggleInfo(UI.StripLinkFormatting(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text2 + ".NAME")), text, planInfo.category, action, GameUtil.ReplaceHotkeyString(Strings.Get("STRINGS.UI.BUILDCATEGORIES." + text2 + ".TOOLTIP"), action), "");
					list.Add(toggleInfo);
					PlanScreen.PopulateOrderInfo(planInfo.category, planInfo.buildingAndSubcategoryData, this.tagCategoryMap, this.tagOrderMap, ref num);
					List<BuildingDef> list2 = new List<BuildingDef>();
					foreach (BuildingDef buildingDef in Assets.BuildingDefs)
					{
						HashedString hashedString;
						if (buildingDef.IsAvailable() && this.tagCategoryMap.TryGetValue(buildingDef.Tag, out hashedString) && !(hashedString != planInfo.category))
						{
							list2.Add(buildingDef);
						}
					}
					this.toggleEntries.Add(new PlanScreen.ToggleEntry(toggleInfo, planInfo.category, list2, planInfo.hideIfNotResearched));
				}
			}
			base.Setup(list);
			this.toggleBouncers.Clear();
			this.toggles.ForEach(delegate(KToggle to)
			{
				foreach (ImageToggleState imageToggleState in to.GetComponents<ImageToggleState>())
				{
					if (imageToggleState.TargetImage.sprite != null && imageToggleState.TargetImage.name == "FG" && !imageToggleState.useSprites)
					{
						imageToggleState.SetSprites(Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"), imageToggleState.TargetImage.sprite, imageToggleState.TargetImage.sprite, Assets.GetSprite(imageToggleState.TargetImage.sprite.name + "_disabled"));
					}
				}
				to.GetComponent<KToggle>().soundPlayer.Enabled = false;
				to.GetComponentInChildren<LocText>().fontSize = (float)(ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.fontSizeBigMode : PlanScreen.fontSizeStandardMode);
				this.toggleBouncers.Add(to, to.GetComponent<Bouncer>());
			});
			for (int j = 0; j < this.toggleEntries.Count; j++)
			{
				PlanScreen.ToggleEntry toggleEntry = this.toggleEntries[j];
				toggleEntry.CollectToggleImages();
				this.toggleEntries[j] = toggleEntry;
			}
			this.ForceUpdateAllCategoryToggles(null);
		}
	}

	// Token: 0x06006CCA RID: 27850 RVA: 0x00291D3C File Offset: 0x0028FF3C
	private void ForceUpdateAllCategoryToggles(object data = null)
	{
		this.forceUpdateAllCategoryToggles = true;
	}

	// Token: 0x06006CCB RID: 27851 RVA: 0x00291D45 File Offset: 0x0028FF45
	public void ForceRefreshAllBuildingToggles()
	{
		this.forceRefreshAllBuildings = true;
	}

	// Token: 0x06006CCC RID: 27852 RVA: 0x00291D50 File Offset: 0x0028FF50
	public void CopyBuildingOrder(BuildingDef buildingDef, string facadeID)
	{
		foreach (PlanScreen.PlanInfo planInfo in global::TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				if (buildingDef.PrefabID == keyValuePair.Key)
				{
					this.OpenCategoryByName(HashCache.Get().Get(planInfo.category));
					this.OnSelectBuilding(this.activeCategoryBuildingToggles[buildingDef].gameObject, buildingDef, facadeID);
					this.ProductInfoScreen.ToggleExpandedInfo(true);
					break;
				}
			}
		}
	}

	// Token: 0x06006CCD RID: 27853 RVA: 0x00291E30 File Offset: 0x00290030
	public void CopyBuildingOrder(Building building)
	{
		this.CopyBuildingOrder(building.Def, building.GetComponent<BuildingFacade>().CurrentFacade);
		if (this.ProductInfoScreen.materialSelectionPanel == null)
		{
			DebugUtil.DevLogError(building.Def.name + " def likely needs to be marked def.ShowInBuildMenu = false");
			return;
		}
		this.ProductInfoScreen.materialSelectionPanel.SelectSourcesMaterials(building);
		Rotatable component = building.GetComponent<Rotatable>();
		if (component != null)
		{
			BuildTool.Instance.SetToolOrientation(component.GetOrientation());
		}
	}

	// Token: 0x06006CCE RID: 27854 RVA: 0x00291EB4 File Offset: 0x002900B4
	private static void PopulateOrderInfo(HashedString category, object data, Dictionary<Tag, HashedString> category_map, Dictionary<Tag, int> order_map, ref int building_index)
	{
		if (data.GetType() == typeof(PlanScreen.PlanInfo))
		{
			PlanScreen.PlanInfo planInfo = (PlanScreen.PlanInfo)data;
			PlanScreen.PopulateOrderInfo(planInfo.category, planInfo.buildingAndSubcategoryData, category_map, order_map, ref building_index);
			return;
		}
		foreach (KeyValuePair<string, string> keyValuePair in ((List<KeyValuePair<string, string>>)data))
		{
			Tag tag = new Tag(keyValuePair.Key);
			category_map[tag] = category;
			order_map[tag] = building_index;
			building_index++;
		}
	}

	// Token: 0x06006CCF RID: 27855 RVA: 0x00291F5C File Offset: 0x0029015C
	protected override void OnCmpEnable()
	{
		this.Refresh();
		this.RefreshCopyBuildingButton(null);
	}

	// Token: 0x06006CD0 RID: 27856 RVA: 0x00291F6B File Offset: 0x0029016B
	protected override void OnCmpDisable()
	{
		this.ClearButtons();
	}

	// Token: 0x06006CD1 RID: 27857 RVA: 0x00291F74 File Offset: 0x00290174
	private void ClearButtons()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
		}
		foreach (KeyValuePair<string, PlanBuildingToggle> keyValuePair2 in this.allBuildingToggles)
		{
			keyValuePair2.Value.gameObject.SetActive(false);
		}
		this.activeCategoryBuildingToggles.Clear();
		this.copyBuildingButton.gameObject.SetActive(false);
		this.copyBuildingButton.GetComponent<MultiToggle>().ChangeState(0);
	}

	// Token: 0x06006CD2 RID: 27858 RVA: 0x0029203C File Offset: 0x0029023C
	public void OnSelectBuilding(GameObject button_go, BuildingDef def, string facadeID = null)
	{
		if (button_go == null)
		{
			global::Debug.Log("Button gameObject is null", base.gameObject);
			return;
		}
		if (button_go == this.SelectedBuildingGameObject)
		{
			this.CloseRecipe(true);
			return;
		}
		this.ignoreToolChangeMessages++;
		PlanBuildingToggle planBuildingToggle = null;
		if (this.currentlySelectedToggle != null)
		{
			planBuildingToggle = this.currentlySelectedToggle.GetComponent<PlanBuildingToggle>();
		}
		this.SelectedBuildingGameObject = button_go;
		this.currentlySelectedToggle = button_go.GetComponent<KToggle>();
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
		HashedString hashedString = this.tagCategoryMap[def.Tag];
		PlanScreen.ToggleEntry toggleEntry;
		if (this.GetToggleEntryForCategory(hashedString, out toggleEntry) && toggleEntry.pendingResearchAttentions.Contains(def.Tag))
		{
			toggleEntry.pendingResearchAttentions.Remove(def.Tag);
			button_go.GetComponent<PlanCategoryNotifications>().ToggleAttention(false);
			if (toggleEntry.pendingResearchAttentions.Count == 0)
			{
				toggleEntry.toggleInfo.toggle.GetComponent<PlanCategoryNotifications>().ToggleAttention(false);
			}
		}
		this.ProductInfoScreen.ClearProduct(false);
		if (planBuildingToggle != null)
		{
			planBuildingToggle.Refresh(BuildingGroupScreen.SearchIsEmpty ? null : new bool?(this.buildingDefSearchCaches[def.PrefabID].IsPassingScore()));
		}
		ToolMenu.Instance.ClearSelection();
		PrebuildTool.Instance.Activate(def, this.GetTooltipForBuildable(def));
		this.LastSelectedBuilding = def.BuildingComplete.GetComponent<Building>();
		this.RefreshCopyBuildingButton(null);
		this.ProductInfoScreen.Show(true);
		this.ProductInfoScreen.ConfigureScreen(def, facadeID);
		this.ignoreToolChangeMessages--;
	}

	// Token: 0x06006CD3 RID: 27859 RVA: 0x002921DC File Offset: 0x002903DC
	private void RefreshBuildableStates(bool force_update)
	{
		if (Assets.BuildingDefs == null || Assets.BuildingDefs.Count == 0)
		{
			return;
		}
		if (this.timeSinceNotificationPing < this.specialNotificationEmbellishDelay)
		{
			this.timeSinceNotificationPing += Time.unscaledDeltaTime;
		}
		if (this.timeSinceNotificationPing >= this.notificationPingExpire)
		{
			this.notificationPingCount = 0;
		}
		int num = 10;
		if (force_update)
		{
			num = Assets.BuildingDefs.Count;
			this.buildable_state_update_idx = 0;
		}
		ListPool<HashedString, PlanScreen>.PooledList pooledList = ListPool<HashedString, PlanScreen>.Allocate();
		for (int i = 0; i < num; i++)
		{
			this.buildable_state_update_idx = (this.buildable_state_update_idx + 1) % Assets.BuildingDefs.Count;
			BuildingDef buildingDef = Assets.BuildingDefs[this.buildable_state_update_idx];
			PlanScreen.RequirementsState buildableStateForDef = this.GetBuildableStateForDef(buildingDef);
			HashedString hashedString;
			if (this.tagCategoryMap.TryGetValue(buildingDef.Tag, out hashedString) && this._buildableStatesByID[buildingDef.PrefabID] != buildableStateForDef)
			{
				this._buildableStatesByID[buildingDef.PrefabID] = buildableStateForDef;
				if (this.ProductInfoScreen.currentDef == buildingDef)
				{
					this.ignoreToolChangeMessages++;
					this.ProductInfoScreen.ClearProduct(false);
					this.ProductInfoScreen.Show(true);
					this.ProductInfoScreen.ConfigureScreen(buildingDef);
					this.ignoreToolChangeMessages--;
				}
				if (buildableStateForDef == PlanScreen.RequirementsState.Complete)
				{
					foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
					{
						if ((HashedString)toggleInfo.userData == hashedString)
						{
							Bouncer bouncer = this.toggleBouncers[toggleInfo.toggle];
							if (bouncer != null && !bouncer.IsBouncing() && !pooledList.Contains(hashedString))
							{
								pooledList.Add(hashedString);
								bouncer.Bounce();
								if (KTime.Instance.UnscaledGameTime - this.initTime > 1.5f)
								{
									if (this.timeSinceNotificationPing >= this.specialNotificationEmbellishDelay)
									{
										string sound = GlobalAssets.GetSound("NewBuildable_Embellishment", false);
										if (sound != null)
										{
											SoundEvent.EndOneShot(SoundEvent.BeginOneShot(sound, SoundListenerController.Instance.transform.GetPosition(), 1f, false));
										}
									}
									string sound2 = GlobalAssets.GetSound("NewBuildable", false);
									if (sound2 != null)
									{
										EventInstance eventInstance = SoundEvent.BeginOneShot(sound2, SoundListenerController.Instance.transform.GetPosition(), 1f, false);
										eventInstance.setParameterByName("playCount", (float)this.notificationPingCount, false);
										SoundEvent.EndOneShot(eventInstance);
									}
								}
								this.timeSinceNotificationPing = 0f;
								this.notificationPingCount++;
							}
						}
					}
				}
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06006CD4 RID: 27860 RVA: 0x002924AC File Offset: 0x002906AC
	private PlanScreen.RequirementsState GetBuildableStateForDef(BuildingDef def)
	{
		if (!def.IsAvailable())
		{
			return PlanScreen.RequirementsState.Invalid;
		}
		PlanScreen.RequirementsState requirementsState = PlanScreen.RequirementsState.Complete;
		KPrefabID component = def.BuildingComplete.GetComponent<KPrefabID>();
		if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive && !this.IsDefResearched(def))
		{
			requirementsState = PlanScreen.RequirementsState.Tech;
		}
		else if (component.HasTag(GameTags.Telepad) && ClusterUtil.ActiveWorldHasPrinter())
		{
			requirementsState = PlanScreen.RequirementsState.TelepadBuilt;
		}
		else if (component.HasTag(GameTags.RocketInteriorBuilding) && !ClusterUtil.ActiveWorldIsRocketInterior())
		{
			requirementsState = PlanScreen.RequirementsState.RocketInteriorOnly;
		}
		else if (component.HasTag(GameTags.NotRocketInteriorBuilding) && ClusterUtil.ActiveWorldIsRocketInterior())
		{
			requirementsState = PlanScreen.RequirementsState.RocketInteriorForbidden;
		}
		else if (component.HasTag(GameTags.UniquePerWorld) && BuildingInventory.Instance.BuildingCountForWorld_BAD_PERF(def.Tag, ClusterManager.Instance.activeWorldId) > 0)
		{
			requirementsState = PlanScreen.RequirementsState.UniquePerWorld;
		}
		else if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive && !ProductInfoScreen.MaterialsMet(def.CraftRecipe))
		{
			requirementsState = PlanScreen.RequirementsState.Materials;
		}
		return requirementsState;
	}

	// Token: 0x06006CD5 RID: 27861 RVA: 0x00292590 File Offset: 0x00290790
	private void SetCategoryButtonState()
	{
		this.nextCategoryToUpdateIDX = (this.nextCategoryToUpdateIDX + 1) % this.toggleEntries.Count;
		for (int i = 0; i < this.toggleEntries.Count; i++)
		{
			if (this.forceUpdateAllCategoryToggles || i == this.nextCategoryToUpdateIDX)
			{
				PlanScreen.ToggleEntry toggleEntry = this.toggleEntries[i];
				KIconToggleMenu.ToggleInfo toggleInfo = toggleEntry.toggleInfo;
				toggleInfo.toggle.ActivateFlourish(this.activeCategoryInfo != null && toggleInfo.userData == this.activeCategoryInfo.userData);
				bool flag = false;
				bool flag2 = true;
				if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
				{
					flag = true;
					flag2 = false;
				}
				else
				{
					foreach (BuildingDef buildingDef in toggleEntry.buildingDefs)
					{
						if (this.GetBuildableState(buildingDef) == PlanScreen.RequirementsState.Complete)
						{
							flag = true;
							flag2 = false;
							break;
						}
					}
					if (flag2 && toggleEntry.AreAnyRequiredTechItemsAvailable())
					{
						flag2 = false;
					}
				}
				this.CategoryInteractive[toggleInfo] = !flag2;
				GameObject gameObject = toggleInfo.toggle.fgImage.transform.Find("ResearchIcon").gameObject;
				if (!flag)
				{
					if (flag2 && toggleEntry.hideIfNotResearched)
					{
						toggleInfo.toggle.gameObject.SetActive(false);
					}
					else if (flag2)
					{
						toggleInfo.toggle.gameObject.SetActive(true);
						gameObject.gameObject.SetActive(true);
					}
					else
					{
						toggleInfo.toggle.gameObject.SetActive(true);
						gameObject.gameObject.SetActive(false);
					}
					ImageToggleState.State state = ((this.activeCategoryInfo != null && toggleInfo.userData == this.activeCategoryInfo.userData) ? ImageToggleState.State.DisabledActive : ImageToggleState.State.Disabled);
					ImageToggleState[] array = toggleEntry.toggleImages;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetState(state);
					}
				}
				else
				{
					toggleInfo.toggle.gameObject.SetActive(true);
					gameObject.gameObject.SetActive(false);
					ImageToggleState.State state2 = ((this.activeCategoryInfo == null || toggleInfo.userData != this.activeCategoryInfo.userData) ? ImageToggleState.State.Inactive : ImageToggleState.State.Active);
					ImageToggleState[] array = toggleEntry.toggleImages;
					for (int j = 0; j < array.Length; j++)
					{
						array[j].SetState(state2);
					}
				}
			}
		}
		this.RefreshCopyBuildingButton(null);
		this.forceUpdateAllCategoryToggles = false;
	}

	// Token: 0x06006CD6 RID: 27862 RVA: 0x002927FC File Offset: 0x002909FC
	private void DeactivateBuildTools()
	{
		InterfaceTool activeTool = PlayerController.Instance.ActiveTool;
		if (activeTool != null)
		{
			Type type = activeTool.GetType();
			if (type == typeof(BuildTool) || typeof(BaseUtilityBuildTool).IsAssignableFrom(type) || type == typeof(PrebuildTool))
			{
				activeTool.DeactivateTool(null);
				PlayerController.Instance.ActivateTool(SelectTool.Instance);
			}
		}
	}

	// Token: 0x06006CD7 RID: 27863 RVA: 0x00292870 File Offset: 0x00290A70
	public void CloseRecipe(bool playSound = false)
	{
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
		}
		if (PlayerController.Instance.ActiveTool is PrebuildTool || PlayerController.Instance.ActiveTool is BuildTool)
		{
			ToolMenu.Instance.ClearSelection();
		}
		this.DeactivateBuildTools();
		if (this.ProductInfoScreen != null)
		{
			this.ProductInfoScreen.ClearProduct(true);
		}
		if (this.activeCategoryInfo != null)
		{
			this.UpdateBuildingButtonList(this.activeCategoryInfo);
		}
		this.SelectedBuildingGameObject = null;
	}

	// Token: 0x06006CD8 RID: 27864 RVA: 0x002928F8 File Offset: 0x00290AF8
	public void SoftCloseRecipe()
	{
		this.ignoreToolChangeMessages++;
		if (PlayerController.Instance.ActiveTool is PrebuildTool || PlayerController.Instance.ActiveTool is BuildTool)
		{
			ToolMenu.Instance.ClearSelection();
		}
		this.DeactivateBuildTools();
		if (this.ProductInfoScreen != null)
		{
			this.ProductInfoScreen.ClearProduct(true);
		}
		this.currentlySelectedToggle = null;
		this.SelectedBuildingGameObject = null;
		this.ignoreToolChangeMessages--;
	}

	// Token: 0x06006CD9 RID: 27865 RVA: 0x0029297C File Offset: 0x00290B7C
	public void CloseCategoryPanel(bool playSound = true)
	{
		this.activeCategoryInfo = null;
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		}
		this.buildingGroupsRoot.GetComponent<ExpandRevealUIContent>().Collapse(delegate(object s)
		{
			this.ClearButtons();
			this.buildingGroupsRoot.gameObject.SetActive(false);
			this.ForceUpdateAllCategoryToggles(null);
		});
		this.PlanCategoryLabel.text = "";
		this.ForceUpdateAllCategoryToggles(null);
	}

	// Token: 0x06006CDA RID: 27866 RVA: 0x002929D8 File Offset: 0x00290BD8
	private void OnClickCategory(KIconToggleMenu.ToggleInfo toggle_info)
	{
		this.CloseRecipe(false);
		if (!this.CategoryInteractive.ContainsKey(toggle_info) || !this.CategoryInteractive[toggle_info])
		{
			this.CloseCategoryPanel(false);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
			return;
		}
		if (this.activeCategoryInfo == toggle_info)
		{
			this.CloseCategoryPanel(true);
		}
		else
		{
			this.OpenCategoryPanel(toggle_info, true);
		}
		this.ConfigurePanelSize(null);
		this.SetScrollPoint(0f);
	}

	// Token: 0x06006CDB RID: 27867 RVA: 0x00292A4C File Offset: 0x00290C4C
	private void OpenCategoryPanel(KIconToggleMenu.ToggleInfo toggle_info, bool play_sound = true)
	{
		HashedString hashedString = (HashedString)toggle_info.userData;
		if (BuildingGroupScreen.Instance != null)
		{
			BuildingGroupScreen.Instance.ClearSearch();
		}
		this.ClearButtons();
		this.buildingGroupsRoot.gameObject.SetActive(true);
		this.activeCategoryInfo = toggle_info;
		if (play_sound)
		{
			UISounds.PlaySound(UISounds.Sound.ClickObject);
		}
		this.BuildButtonList();
		this.UpdateBuildingButtonList(this.activeCategoryInfo);
		this.RefreshCategoryPanelTitle();
		this.ForceUpdateAllCategoryToggles(null);
		this.buildingGroupsRoot.GetComponent<ExpandRevealUIContent>().Expand(null);
	}

	// Token: 0x06006CDC RID: 27868 RVA: 0x00292AD4 File Offset: 0x00290CD4
	public void RefreshCategoryPanelTitle()
	{
		if (this.activeCategoryInfo != null)
		{
			this.PlanCategoryLabel.text = this.activeCategoryInfo.text.ToUpper();
		}
		if (!BuildingGroupScreen.SearchIsEmpty)
		{
			this.PlanCategoryLabel.text = UI.BUILDMENU.SEARCH_RESULTS_HEADER;
		}
	}

	// Token: 0x06006CDD RID: 27869 RVA: 0x00292B20 File Offset: 0x00290D20
	public void RefreshSearch()
	{
		if (BuildingGroupScreen.SearchIsEmpty)
		{
			using (Dictionary<string, SearchUtil.SubcategoryCache>.Enumerator enumerator = this.subcategorySearchCaches.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<string, SearchUtil.SubcategoryCache> keyValuePair = enumerator.Current;
					keyValuePair.Value.Reset();
				}
				goto IL_00C5;
			}
		}
		string text = BuildingGroupScreen.Instance.inputField.text.ToUpper().Trim();
		foreach (KeyValuePair<string, SearchUtil.SubcategoryCache> keyValuePair2 in this.subcategorySearchCaches)
		{
			try
			{
				keyValuePair2.Value.Bind(text);
			}
			catch (Exception ex)
			{
				KCrashReporter.ReportDevNotification("Fuzzy score bind failed", Environment.StackTrace, ex.Message, false, null);
				keyValuePair2.Value.Reset();
			}
		}
		IL_00C5:
		this.SortButtons();
		this.SortSubcategories();
		this.ForceRefreshAllBuildingToggles();
	}

	// Token: 0x06006CDE RID: 27870 RVA: 0x00292C2C File Offset: 0x00290E2C
	public void OpenCategoryByName(string category)
	{
		PlanScreen.ToggleEntry toggleEntry;
		if (this.GetToggleEntryForCategory(category, out toggleEntry))
		{
			this.OpenCategoryPanel(toggleEntry.toggleInfo, false);
			this.ConfigurePanelSize(null);
		}
	}

	// Token: 0x06006CDF RID: 27871 RVA: 0x00292C60 File Offset: 0x00290E60
	private void UpdateBuildingButton(int i, bool checkScore)
	{
		KeyValuePair<string, PlanBuildingToggle> keyValuePair = this.allBuildingToggles.ElementAt(i);
		bool? flag = (checkScore ? new bool?(this.buildingDefSearchCaches[keyValuePair.Key].IsPassingScore()) : null);
		if (keyValuePair.Value.Refresh(flag))
		{
			this.categoryPanelSizeNeedsRefresh = true;
		}
		keyValuePair.Value.SwitchViewMode(this.useSubCategoryLayout);
	}

	// Token: 0x06006CE0 RID: 27872 RVA: 0x00292CD0 File Offset: 0x00290ED0
	private void UpdateBuildingButtonList(KIconToggleMenu.ToggleInfo toggle_info)
	{
		KToggle ktoggle = toggle_info.toggle;
		if (ktoggle == null)
		{
			foreach (KIconToggleMenu.ToggleInfo toggleInfo in this.toggleInfo)
			{
				if (toggleInfo.userData == toggle_info.userData)
				{
					ktoggle = toggleInfo.toggle;
					break;
				}
			}
		}
		bool flag = false;
		if (ktoggle != null && this.allBuildingToggles.Count != 0)
		{
			bool flag2 = !BuildingGroupScreen.SearchIsEmpty;
			if (this.forceRefreshAllBuildings)
			{
				this.forceRefreshAllBuildings = false;
				for (int num = 0; num != this.allBuildingToggles.Count; num++)
				{
					this.UpdateBuildingButton(num, flag2);
				}
				flag = this.categoryPanelSizeNeedsRefresh;
			}
			else
			{
				for (int i = 0; i < this.maxToggleRefreshPerFrame; i++)
				{
					if (this.building_button_refresh_idx >= this.allBuildingToggles.Count)
					{
						this.building_button_refresh_idx = 0;
					}
					this.UpdateBuildingButton(this.building_button_refresh_idx, flag2);
					this.building_button_refresh_idx++;
				}
			}
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.allSubCategoryObjects)
		{
			GridLayoutGroup componentInChildren = keyValuePair.Value.GetComponentInChildren<GridLayoutGroup>(true);
			if (!(componentInChildren == null))
			{
				int num2 = 0;
				for (int j = 0; j < componentInChildren.transform.childCount; j++)
				{
					if (componentInChildren.transform.GetChild(j).gameObject.activeSelf)
					{
						num2++;
					}
				}
				bool flag3 = num2 > 0;
				if (keyValuePair.Value.activeSelf != flag3)
				{
					keyValuePair.Value.SetActive(flag3);
				}
			}
		}
		if (flag || (this.categoryPanelSizeNeedsRefresh && this.building_button_refresh_idx >= this.activeCategoryBuildingToggles.Count))
		{
			this.categoryPanelSizeNeedsRefresh = false;
			this.ConfigurePanelSize(null);
		}
	}

	// Token: 0x06006CE1 RID: 27873 RVA: 0x00292ED8 File Offset: 0x002910D8
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		this.RefreshBuildableStates(false);
		this.SetCategoryButtonState();
		if (this.activeCategoryInfo != null)
		{
			this.UpdateBuildingButtonList(this.activeCategoryInfo);
		}
	}

	// Token: 0x06006CE2 RID: 27874 RVA: 0x00292F04 File Offset: 0x00291104
	private void CacheSearchCaches()
	{
		this.<CacheSearchCaches>g__ManifestSubcategoryCache|128_0("default", string.Empty);
		foreach (PlanScreen.PlanInfo planInfo in global::TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				BuildingDef buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				SearchUtil.BuildingDefCache buildingDefCache = null;
				if (buildingDef.IsAvailable() && buildingDef.ShouldShowInBuildMenu() && Game.IsCorrectDlcActiveForCurrentSave(buildingDef) && !this.buildingDefSearchCaches.TryGetValue(buildingDef.PrefabID, out buildingDefCache))
				{
					buildingDefCache = SearchUtil.MakeBuildingDefCache(buildingDef);
					this.buildingDefSearchCaches[buildingDef.PrefabID] = buildingDefCache;
				}
				SearchUtil.SubcategoryCache subcategoryCache = this.<CacheSearchCaches>g__ManifestSubcategoryCache|128_0(keyValuePair.Value, null);
				if (buildingDefCache != null)
				{
					subcategoryCache.buildingDefs.Add(buildingDefCache);
				}
			}
		}
	}

	// Token: 0x06006CE3 RID: 27875 RVA: 0x00293020 File Offset: 0x00291220
	private void CollectRequiredBuildingDefs(List<BuildingDef> defs)
	{
		foreach (PlanScreen.PlanInfo planInfo in global::TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				BuildingDef buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				if (buildingDef.IsAvailable() && buildingDef.ShouldShowInBuildMenu() && Game.IsCorrectDlcActiveForCurrentSave(buildingDef))
				{
					defs.Add(buildingDef);
				}
			}
		}
	}

	// Token: 0x06006CE4 RID: 27876 RVA: 0x002930D0 File Offset: 0x002912D0
	private int CompareScores(global::Tuple<PlanBuildingToggle, string> a, global::Tuple<PlanBuildingToggle, string> b)
	{
		return this.buildingDefSearchCaches[a.second].CompareTo(this.buildingDefSearchCaches[b.second]);
	}

	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x06006CE5 RID: 27877 RVA: 0x002930F9 File Offset: 0x002912F9
	private Comparer<global::Tuple<PlanBuildingToggle, string>> BuildingDefComparer
	{
		get
		{
			if (this.buildingDefComparer == null)
			{
				this.buildingDefComparer = Comparer<global::Tuple<PlanBuildingToggle, string>>.Create(new Comparison<global::Tuple<PlanBuildingToggle, string>>(this.CompareScores));
			}
			return this.buildingDefComparer;
		}
	}

	// Token: 0x06006CE6 RID: 27878 RVA: 0x00293120 File Offset: 0x00291320
	private void SortButtons()
	{
		ListPool<BuildingDef, PlanScreen>.PooledList pooledList = ListPool<BuildingDef, PlanScreen>.Allocate();
		this.CollectRequiredBuildingDefs(pooledList);
		ListPool<global::Tuple<PlanBuildingToggle, string>, PlanScreen>.PooledList pooledList2 = ListPool<global::Tuple<PlanBuildingToggle, string>, PlanScreen>.Allocate();
		foreach (BuildingDef buildingDef in pooledList)
		{
			global::Tuple<PlanBuildingToggle, string> tuple = new global::Tuple<PlanBuildingToggle, string>(this.allBuildingToggles[buildingDef.PrefabID], buildingDef.PrefabID);
			int num = pooledList2.BinarySearch(tuple, this.BuildingDefComparer);
			if (num < 0)
			{
				num = ~num;
			}
			while (num < pooledList2.Count && this.CompareScores(tuple, pooledList2[num]) == 0)
			{
				num++;
			}
			pooledList2.Insert(num, tuple);
		}
		pooledList.Recycle();
		foreach (global::Tuple<PlanBuildingToggle, string> tuple2 in pooledList2)
		{
			tuple2.first.transform.SetAsLastSibling();
		}
		pooledList2.Recycle();
	}

	// Token: 0x06006CE7 RID: 27879 RVA: 0x00293234 File Offset: 0x00291434
	private void SortSubcategories()
	{
		Comparer<global::Tuple<GameObject, string>> comparer = Comparer<global::Tuple<GameObject, string>>.Create(new Comparison<global::Tuple<GameObject, string>>(this.<SortSubcategories>g__CompareScores|135_0));
		ListPool<global::Tuple<GameObject, string>, PlanScreen>.PooledList pooledList = ListPool<global::Tuple<GameObject, string>, PlanScreen>.Allocate();
		foreach (string text in this.stableSubcategoryOrder)
		{
			global::Tuple<GameObject, string> tuple = new global::Tuple<GameObject, string>(this.allSubCategoryObjects[text], text);
			int num = pooledList.BinarySearch(tuple, comparer);
			if (num < 0)
			{
				num = ~num;
			}
			while (num < pooledList.Count && this.<SortSubcategories>g__CompareScores|135_0(tuple, pooledList[num]) == 0)
			{
				num++;
			}
			pooledList.Insert(num, tuple);
		}
		foreach (global::Tuple<GameObject, string> tuple2 in pooledList)
		{
			tuple2.first.transform.SetAsLastSibling();
		}
		pooledList.Recycle();
	}

	// Token: 0x06006CE8 RID: 27880 RVA: 0x0029333C File Offset: 0x0029153C
	private void BuildButtonList()
	{
		this.activeCategoryBuildingToggles.Clear();
		this.CacheSearchCaches();
		DictionaryPool<string, HashedString, PlanScreen>.PooledDictionary pooledDictionary = DictionaryPool<string, HashedString, PlanScreen>.Allocate();
		DictionaryPool<string, List<BuildingDef>, PlanScreen>.PooledDictionary pooledDictionary2 = DictionaryPool<string, List<BuildingDef>, PlanScreen>.Allocate();
		if (!pooledDictionary2.ContainsKey("default"))
		{
			pooledDictionary2.Add("default", new List<BuildingDef>());
		}
		foreach (PlanScreen.PlanInfo planInfo in global::TUNING.BUILDINGS.PLANORDER)
		{
			foreach (KeyValuePair<string, string> keyValuePair in planInfo.buildingAndSubcategoryData)
			{
				BuildingDef buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				if (buildingDef.IsAvailable() && buildingDef.ShouldShowInBuildMenu() && Game.IsCorrectDlcActiveForCurrentSave(buildingDef))
				{
					pooledDictionary.Add(buildingDef.PrefabID, planInfo.category);
					if (!pooledDictionary2.ContainsKey(keyValuePair.Value))
					{
						pooledDictionary2.Add(keyValuePair.Value, new List<BuildingDef>());
					}
					pooledDictionary2[keyValuePair.Value].Add(buildingDef);
				}
			}
		}
		if (this.stableSubcategoryOrder.Count == 0)
		{
			foreach (ref PlanScreen.PlanInfo ptr in global::TUNING.BUILDINGS.PLANORDER)
			{
				this.<BuildButtonList>g__RegisterSubcategory|136_0("default");
				foreach (KeyValuePair<string, string> keyValuePair2 in ptr.buildingAndSubcategoryData)
				{
					this.<BuildButtonList>g__RegisterSubcategory|136_0(keyValuePair2.Value);
				}
			}
		}
		GameObject gameObject = this.allSubCategoryObjects["default"].GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid").gameObject;
		bool flag = !BuildingGroupScreen.SearchIsEmpty;
		foreach (string text in this.stableSubcategoryOrder)
		{
			List<BuildingDef> list;
			if (pooledDictionary2.TryGetValue(text, out list))
			{
				if (text == "default")
				{
					this.allSubCategoryObjects[text].SetActive(this.useSubCategoryLayout);
				}
				HierarchyReferences component = this.allSubCategoryObjects[text].GetComponent<HierarchyReferences>();
				GameObject gameObject2;
				if (this.useSubCategoryLayout)
				{
					component.GetReference<RectTransform>("Header").gameObject.SetActive(true);
					gameObject2 = this.allSubCategoryObjects[text].GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid").gameObject;
					StringEntry stringEntry;
					if (Strings.TryGet("STRINGS.UI.NEWBUILDCATEGORIES." + text.ToUpper() + ".BUILDMENUTITLE", out stringEntry))
					{
						component.GetReference<LocText>("HeaderLabel").SetText(stringEntry);
					}
				}
				else
				{
					component.GetReference<RectTransform>("Header").gameObject.SetActive(false);
					gameObject2 = gameObject;
				}
				foreach (BuildingDef buildingDef2 in list)
				{
					HashedString hashedString = pooledDictionary[buildingDef2.PrefabID];
					GameObject gameObject3 = this.CreateButton(buildingDef2, gameObject2, hashedString, flag);
					PlanScreen.ToggleEntry toggleEntry;
					this.GetToggleEntryForCategory(hashedString, out toggleEntry);
					if (toggleEntry != null && toggleEntry.pendingResearchAttentions.Contains(buildingDef2.PrefabID))
					{
						gameObject3.GetComponent<PlanCategoryNotifications>().ToggleAttention(true);
					}
				}
			}
		}
		pooledDictionary2.Recycle();
		pooledDictionary.Recycle();
		if (flag)
		{
			this.RefreshSearch();
		}
		this.ForceRefreshAllBuildingToggles();
		this.RefreshScale(null);
	}

	// Token: 0x06006CE9 RID: 27881 RVA: 0x00293760 File Offset: 0x00291960
	public void ConfigurePanelSize(object data = null)
	{
		if (this.useSubCategoryLayout)
		{
			this.buildGrid_bg_rowHeight = 48f;
		}
		else
		{
			this.buildGrid_bg_rowHeight = (ScreenResolutionMonitor.UsingGamepadUIMode() ? PlanScreen.bigBuildingButtonSize.y : PlanScreen.standarduildingButtonSize.y);
		}
		GridLayoutGroup reference = this.subgroupPrefab.GetComponent<HierarchyReferences>().GetReference<GridLayoutGroup>("Grid");
		this.buildGrid_bg_rowHeight += reference.spacing.y;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.GroupsTransform.childCount; i++)
		{
			int num3 = 0;
			HierarchyReferences component = this.GroupsTransform.GetChild(i).GetComponent<HierarchyReferences>();
			if (!(component == null))
			{
				GridLayoutGroup reference2 = component.GetReference<GridLayoutGroup>("Grid");
				if (!(reference2 == null))
				{
					for (int j = 0; j < reference2.transform.childCount; j++)
					{
						if (reference2.transform.GetChild(j).gameObject.activeSelf)
						{
							num3++;
						}
					}
					if (num3 > 0)
					{
						num2 += 24;
					}
					num += num3 / reference2.constraintCount;
					if (num3 % reference2.constraintCount != 0)
					{
						num++;
					}
				}
			}
		}
		num2 = Math.Min(72, num2);
		this.noResultMessage.SetActive(num == 0);
		int num4 = num;
		int num5 = Math.Max(1, Screen.height / (int)this.buildGrid_bg_rowHeight - 3);
		num5 = Math.Min(num5, this.useSubCategoryLayout ? 12 : 6);
		if (BuildingGroupScreen.IsEditing || !BuildingGroupScreen.SearchIsEmpty)
		{
			num4 = Mathf.Min(num5, this.useSubCategoryLayout ? 8 : 4);
		}
		this.BuildingGroupContentsRect.GetComponent<ScrollRect>().verticalScrollbar.gameObject.SetActive(num4 >= num5 - 1);
		float num6 = this.buildGrid_bg_borderHeight + (float)num2 + 36f + (float)Mathf.Clamp(num4, 0, num5) * this.buildGrid_bg_rowHeight;
		if (BuildingGroupScreen.IsEditing || !BuildingGroupScreen.SearchIsEmpty)
		{
			num6 = Mathf.Max(num6, this.buildingGroupsRoot.sizeDelta.y);
		}
		this.buildingGroupsRoot.sizeDelta = new Vector2(this.buildGrid_bg_width, num6);
		this.RefreshScale(null);
	}

	// Token: 0x06006CEA RID: 27882 RVA: 0x00293988 File Offset: 0x00291B88
	private void SetScrollPoint(float targetY)
	{
		this.BuildingGroupContentsRect.anchoredPosition = new Vector2(this.BuildingGroupContentsRect.anchoredPosition.x, targetY);
	}

	// Token: 0x06006CEB RID: 27883 RVA: 0x002939AC File Offset: 0x00291BAC
	private GameObject CreateButton(BuildingDef def, GameObject parent, HashedString plan_category, bool checkScore)
	{
		bool? flag = (checkScore ? new bool?(this.buildingDefSearchCaches[def.PrefabID].IsPassingScore()) : null);
		PlanBuildingToggle componentInChildren;
		GameObject gameObject;
		if (this.allBuildingToggles.TryGetValue(def.PrefabID, out componentInChildren))
		{
			gameObject = componentInChildren.gameObject;
			componentInChildren.Refresh(flag);
		}
		else
		{
			gameObject = global::Util.KInstantiateUI(this.planButtonPrefab, parent, false);
			gameObject.name = UI.StripLinkFormatting(def.name) + " Group:" + plan_category.ToString();
			componentInChildren = gameObject.GetComponentInChildren<PlanBuildingToggle>();
			componentInChildren.Config(def, this, plan_category, flag);
			componentInChildren.soundPlayer.Enabled = false;
			componentInChildren.SwitchViewMode(this.useSubCategoryLayout);
			this.allBuildingToggles.Add(def.PrefabID, componentInChildren);
		}
		if (gameObject.transform.parent != parent)
		{
			gameObject.transform.SetParent(parent.transform);
		}
		this.activeCategoryBuildingToggles.Add(def, componentInChildren);
		return gameObject;
	}

	// Token: 0x06006CEC RID: 27884 RVA: 0x00293AAD File Offset: 0x00291CAD
	public static bool TechRequirementsMet(TechItem techItem)
	{
		return DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || techItem == null || techItem.IsComplete();
	}

	// Token: 0x06006CED RID: 27885 RVA: 0x00293ACD File Offset: 0x00291CCD
	private static bool TechRequirementsUpcoming(TechItem techItem)
	{
		return PlanScreen.TechRequirementsMet(techItem);
	}

	// Token: 0x06006CEE RID: 27886 RVA: 0x00293AD8 File Offset: 0x00291CD8
	private bool GetToggleEntryForCategory(HashedString category, out PlanScreen.ToggleEntry toggleEntry)
	{
		toggleEntry = null;
		foreach (PlanScreen.ToggleEntry toggleEntry2 in this.toggleEntries)
		{
			if (toggleEntry2.planCategory == category)
			{
				toggleEntry = toggleEntry2;
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006CEF RID: 27887 RVA: 0x00293B40 File Offset: 0x00291D40
	public bool IsDefBuildable(BuildingDef def)
	{
		return this.GetBuildableState(def) == PlanScreen.RequirementsState.Complete;
	}

	// Token: 0x06006CF0 RID: 27888 RVA: 0x00293B4C File Offset: 0x00291D4C
	public string GetTooltipForBuildable(BuildingDef def)
	{
		PlanScreen.RequirementsState buildableState = this.GetBuildableState(def);
		return PlanScreen.GetTooltipForRequirementsState(def, buildableState);
	}

	// Token: 0x06006CF1 RID: 27889 RVA: 0x00293B68 File Offset: 0x00291D68
	public static string GetTooltipForRequirementsState(BuildingDef def, PlanScreen.RequirementsState state)
	{
		TechItem techItem = Db.Get().TechItems.TryGet(def.PrefabID);
		string text = null;
		if (Game.Instance.SandboxModeActive)
		{
			text = UIConstants.ColorPrefixYellow + UI.SANDBOXTOOLS.SETTINGS.INSTANT_BUILD.NAME + UIConstants.ColorSuffix;
		}
		else if (DebugHandler.InstantBuildMode)
		{
			text = UIConstants.ColorPrefixYellow + UI.DEBUG_TOOLS.DEBUG_ACTIVE + UIConstants.ColorSuffix;
		}
		else
		{
			switch (state)
			{
			case PlanScreen.RequirementsState.Tech:
				text = string.Format(UI.PRODUCTINFO_REQUIRESRESEARCHDESC, techItem.ParentTech.Name);
				break;
			case PlanScreen.RequirementsState.Materials:
				text = UI.PRODUCTINFO_MISSINGRESOURCES_HOVER;
				foreach (Recipe.Ingredient ingredient in def.CraftRecipe.Ingredients)
				{
					string text2 = string.Format("{0}{1}: {2}", "• ", ingredient.tag.ProperName(), GameUtil.GetFormattedMass(ingredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
					text = text + "\n" + text2;
				}
				break;
			case PlanScreen.RequirementsState.TelepadBuilt:
				text = UI.PRODUCTINFO_UNIQUE_PER_WORLD;
				break;
			case PlanScreen.RequirementsState.UniquePerWorld:
				text = UI.PRODUCTINFO_UNIQUE_PER_WORLD;
				break;
			case PlanScreen.RequirementsState.RocketInteriorOnly:
				text = UI.PRODUCTINFO_ROCKET_INTERIOR;
				break;
			case PlanScreen.RequirementsState.RocketInteriorForbidden:
				text = UI.PRODUCTINFO_ROCKET_NOT_INTERIOR;
				break;
			}
		}
		return text;
	}

	// Token: 0x06006CF2 RID: 27890 RVA: 0x00293CF4 File Offset: 0x00291EF4
	private void PointerEnter(PointerEventData data)
	{
		this.planScreenScrollRect.mouseIsOver = true;
	}

	// Token: 0x06006CF3 RID: 27891 RVA: 0x00293D02 File Offset: 0x00291F02
	private void PointerExit(PointerEventData data)
	{
		this.planScreenScrollRect.mouseIsOver = false;
	}

	// Token: 0x06006CF4 RID: 27892 RVA: 0x00293D10 File Offset: 0x00291F10
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (this.mouseOver && base.ConsumeMouseScroll)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.ZoomIn) || e.IsAction(global::Action.ZoomOut))
				{
					this.planScreenScrollRect.OnKeyDown(e);
				}
			}
			else if (!e.TryConsume(global::Action.ZoomIn))
			{
				e.TryConsume(global::Action.ZoomOut);
			}
		}
		if (e.IsAction(global::Action.CopyBuilding) && e.TryConsume(global::Action.CopyBuilding))
		{
			this.OnClickCopyBuilding();
		}
		if (this.toggles == null)
		{
			return;
		}
		if (!e.Consumed && this.activeCategoryInfo != null && e.TryConsume(global::Action.Escape))
		{
			this.OnClickCategory(this.activeCategoryInfo);
			SelectTool.Instance.Activate();
			this.ClearSelection();
			return;
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06006CF5 RID: 27893 RVA: 0x00293DD8 File Offset: 0x00291FD8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.mouseOver && base.ConsumeMouseScroll)
		{
			if (KInputManager.currentControllerIsGamepad)
			{
				if (e.IsAction(global::Action.ZoomIn) || e.IsAction(global::Action.ZoomOut))
				{
					this.planScreenScrollRect.OnKeyUp(e);
				}
			}
			else if (!e.TryConsume(global::Action.ZoomIn))
			{
				e.TryConsume(global::Action.ZoomOut);
			}
		}
		if (e.Consumed)
		{
			return;
		}
		if (this.SelectedBuildingGameObject != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.CloseRecipe(false);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		}
		else if (this.activeCategoryInfo != null && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.OnUIClear(null);
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06006CF6 RID: 27894 RVA: 0x00293E98 File Offset: 0x00292098
	private void OnRecipeElementsFullySelected()
	{
		BuildingDef buildingDef = null;
		foreach (KeyValuePair<string, PlanBuildingToggle> keyValuePair in this.allBuildingToggles)
		{
			if (keyValuePair.Value == this.currentlySelectedToggle)
			{
				buildingDef = Assets.GetBuildingDef(keyValuePair.Key);
				break;
			}
		}
		DebugUtil.DevAssert(buildingDef, "def is null", null);
		if (buildingDef)
		{
			if (buildingDef.isKAnimTile && buildingDef.isUtility)
			{
				IList<Tag> getSelectedElementAsList = this.ProductInfoScreen.materialSelectionPanel.GetSelectedElementAsList;
				((buildingDef.BuildingComplete.GetComponent<Wire>() != null) ? WireBuildTool.Instance : UtilityBuildTool.Instance).Activate(buildingDef, getSelectedElementAsList, this.ProductInfoScreen.FacadeSelectionPanel.SelectedFacade);
				return;
			}
			BuildTool.Instance.Activate(buildingDef, this.ProductInfoScreen.materialSelectionPanel.GetSelectedElementAsList, this.ProductInfoScreen.FacadeSelectionPanel.SelectedFacade);
		}
	}

	// Token: 0x06006CF7 RID: 27895 RVA: 0x00293FA8 File Offset: 0x002921A8
	public void OnResearchComplete(object tech)
	{
		if (tech is Tech)
		{
			using (List<TechItem>.Enumerator enumerator = ((Tech)tech).unlockedItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TechItem techItem = enumerator.Current;
					BuildingDef buildingDef = Assets.GetBuildingDef(techItem.Id);
					this.AddResearchedBuildingCategory(buildingDef);
				}
				return;
			}
		}
		if (tech is BuildingDef)
		{
			BuildingDef buildingDef2 = tech as BuildingDef;
			this.AddResearchedBuildingCategory(buildingDef2);
		}
	}

	// Token: 0x06006CF8 RID: 27896 RVA: 0x00294028 File Offset: 0x00292228
	private void AddResearchedBuildingCategory(BuildingDef def)
	{
		if (def != null && Game.IsCorrectDlcActiveForCurrentSave(def))
		{
			this.UpdateDefResearched(def);
			if (this.tagCategoryMap.ContainsKey(def.Tag))
			{
				HashedString hashedString = this.tagCategoryMap[def.Tag];
				PlanScreen.ToggleEntry toggleEntry;
				if (this.GetToggleEntryForCategory(hashedString, out toggleEntry))
				{
					toggleEntry.pendingResearchAttentions.Add(def.Tag);
					toggleEntry.toggleInfo.toggle.GetComponent<PlanCategoryNotifications>().ToggleAttention(true);
					toggleEntry.Refresh();
				}
			}
		}
	}

	// Token: 0x06006CF9 RID: 27897 RVA: 0x002940AC File Offset: 0x002922AC
	private void OnUIClear(object data)
	{
		if (this.activeCategoryInfo != null)
		{
			this.selected = -1;
			this.OnClickCategory(this.activeCategoryInfo);
			SelectTool.Instance.Activate();
			PlayerController.Instance.ActivateTool(SelectTool.Instance);
			SelectTool.Instance.Select(null, true);
		}
	}

	// Token: 0x06006CFA RID: 27898 RVA: 0x002940FC File Offset: 0x002922FC
	private void OnActiveToolChanged(object data)
	{
		if (data == null)
		{
			return;
		}
		if (this.ignoreToolChangeMessages > 0)
		{
			return;
		}
		Type type = data.GetType();
		if (!typeof(BuildTool).IsAssignableFrom(type) && !typeof(PrebuildTool).IsAssignableFrom(type) && !typeof(BaseUtilityBuildTool).IsAssignableFrom(type))
		{
			this.CloseRecipe(false);
			this.CloseCategoryPanel(false);
		}
	}

	// Token: 0x06006CFB RID: 27899 RVA: 0x00294162 File Offset: 0x00292362
	public PrioritySetting GetBuildingPriority()
	{
		return this.ProductInfoScreen.materialSelectionPanel.PriorityScreen.GetLastSelectedPriority();
	}

	// Token: 0x06006D02 RID: 27906 RVA: 0x00294520 File Offset: 0x00292720
	[CompilerGenerated]
	private SearchUtil.SubcategoryCache <CacheSearchCaches>g__ManifestSubcategoryCache|128_0(string subcategory, string _text = null)
	{
		SearchUtil.SubcategoryCache subcategoryCache;
		if (!this.subcategorySearchCaches.TryGetValue(subcategory, out subcategoryCache))
		{
			subcategoryCache = new SearchUtil.SubcategoryCache
			{
				subcategory = new SearchUtil.MatchCache
				{
					text = SearchUtil.Canonicalize(_text ?? subcategory)
				},
				buildingDefs = new HashSet<SearchUtil.BuildingDefCache>()
			};
			this.subcategorySearchCaches[subcategory] = subcategoryCache;
		}
		return subcategoryCache;
	}

	// Token: 0x06006D03 RID: 27907 RVA: 0x00294578 File Offset: 0x00292778
	[CompilerGenerated]
	private int <SortSubcategories>g__CompareScores|135_0(global::Tuple<GameObject, string> a, global::Tuple<GameObject, string> b)
	{
		return this.subcategorySearchCaches[a.second].CompareTo(this.subcategorySearchCaches[b.second]);
	}

	// Token: 0x06006D04 RID: 27908 RVA: 0x002945A4 File Offset: 0x002927A4
	[CompilerGenerated]
	private void <BuildButtonList>g__RegisterSubcategory|136_0(string subcategory)
	{
		if (this.allSubCategoryObjects.ContainsKey(subcategory))
		{
			return;
		}
		GameObject gameObject = global::Util.KInstantiateUI(this.subgroupPrefab, this.GroupsTransform.gameObject, true);
		this.stableSubcategoryOrder.Add(subcategory);
		this.allSubCategoryObjects[subcategory] = gameObject;
		gameObject.SetActive(false);
	}

	// Token: 0x04004A16 RID: 18966
	[SerializeField]
	private GameObject planButtonPrefab;

	// Token: 0x04004A17 RID: 18967
	[SerializeField]
	private GameObject recipeInfoScreenParent;

	// Token: 0x04004A18 RID: 18968
	[SerializeField]
	private GameObject productInfoScreenPrefab;

	// Token: 0x04004A19 RID: 18969
	[SerializeField]
	private GameObject copyBuildingButton;

	// Token: 0x04004A1A RID: 18970
	[SerializeField]
	private KButton gridViewButton;

	// Token: 0x04004A1B RID: 18971
	[SerializeField]
	private KButton listViewButton;

	// Token: 0x04004A1C RID: 18972
	private bool useSubCategoryLayout;

	// Token: 0x04004A1D RID: 18973
	private int refreshScaleHandle = -1;

	// Token: 0x04004A1E RID: 18974
	[SerializeField]
	private GameObject adjacentPinnedButtons;

	// Token: 0x04004A1F RID: 18975
	private static Dictionary<HashedString, string> iconNameMap = new Dictionary<HashedString, string>
	{
		{
			PlanScreen.CacheHashedString("Base"),
			"icon_category_base"
		},
		{
			PlanScreen.CacheHashedString("Oxygen"),
			"icon_category_oxygen"
		},
		{
			PlanScreen.CacheHashedString("Power"),
			"icon_category_electrical"
		},
		{
			PlanScreen.CacheHashedString("Food"),
			"icon_category_food"
		},
		{
			PlanScreen.CacheHashedString("Plumbing"),
			"icon_category_plumbing"
		},
		{
			PlanScreen.CacheHashedString("HVAC"),
			"icon_category_ventilation"
		},
		{
			PlanScreen.CacheHashedString("Refining"),
			"icon_category_refinery"
		},
		{
			PlanScreen.CacheHashedString("Medical"),
			"icon_category_medical"
		},
		{
			PlanScreen.CacheHashedString("Furniture"),
			"icon_category_furniture"
		},
		{
			PlanScreen.CacheHashedString("Equipment"),
			"icon_category_misc"
		},
		{
			PlanScreen.CacheHashedString("Utilities"),
			"icon_category_utilities"
		},
		{
			PlanScreen.CacheHashedString("Automation"),
			"icon_category_automation"
		},
		{
			PlanScreen.CacheHashedString("Conveyance"),
			"icon_category_shipping"
		},
		{
			PlanScreen.CacheHashedString("Rocketry"),
			"icon_category_rocketry"
		},
		{
			PlanScreen.CacheHashedString("HEP"),
			"icon_category_radiation"
		}
	};

	// Token: 0x04004A20 RID: 18976
	private Dictionary<KIconToggleMenu.ToggleInfo, bool> CategoryInteractive = new Dictionary<KIconToggleMenu.ToggleInfo, bool>();

	// Token: 0x04004A22 RID: 18978
	[SerializeField]
	public PlanScreen.BuildingToolTipSettings buildingToolTipSettings;

	// Token: 0x04004A23 RID: 18979
	public PlanScreen.BuildingNameTextSetting buildingNameTextSettings;

	// Token: 0x04004A24 RID: 18980
	private KIconToggleMenu.ToggleInfo activeCategoryInfo;

	// Token: 0x04004A25 RID: 18981
	public Dictionary<BuildingDef, PlanBuildingToggle> activeCategoryBuildingToggles = new Dictionary<BuildingDef, PlanBuildingToggle>();

	// Token: 0x04004A26 RID: 18982
	private float timeSinceNotificationPing;

	// Token: 0x04004A27 RID: 18983
	private float notificationPingExpire = 0.5f;

	// Token: 0x04004A28 RID: 18984
	private float specialNotificationEmbellishDelay = 8f;

	// Token: 0x04004A29 RID: 18985
	private int notificationPingCount;

	// Token: 0x04004A2A RID: 18986
	private Dictionary<KToggle, Bouncer> toggleBouncers = new Dictionary<KToggle, Bouncer>();

	// Token: 0x04004A2B RID: 18987
	public const string DEFAULT_SUBCATEGORY_KEY = "default";

	// Token: 0x04004A2C RID: 18988
	private Dictionary<string, GameObject> allSubCategoryObjects = new Dictionary<string, GameObject>();

	// Token: 0x04004A2D RID: 18989
	private Dictionary<string, PlanBuildingToggle> allBuildingToggles = new Dictionary<string, PlanBuildingToggle>();

	// Token: 0x04004A2E RID: 18990
	private readonly Dictionary<string, SearchUtil.BuildingDefCache> buildingDefSearchCaches = new Dictionary<string, SearchUtil.BuildingDefCache>();

	// Token: 0x04004A2F RID: 18991
	private readonly Dictionary<string, SearchUtil.SubcategoryCache> subcategorySearchCaches = new Dictionary<string, SearchUtil.SubcategoryCache>();

	// Token: 0x04004A30 RID: 18992
	private readonly List<string> stableSubcategoryOrder = new List<string>();

	// Token: 0x04004A31 RID: 18993
	private static Vector2 bigBuildingButtonSize = new Vector2(98f, 123f);

	// Token: 0x04004A32 RID: 18994
	private static Vector2 standarduildingButtonSize = PlanScreen.bigBuildingButtonSize * 0.8f;

	// Token: 0x04004A33 RID: 18995
	public static int fontSizeBigMode = 16;

	// Token: 0x04004A34 RID: 18996
	public static int fontSizeStandardMode = 14;

	// Token: 0x04004A36 RID: 18998
	[SerializeField]
	private GameObject subgroupPrefab;

	// Token: 0x04004A37 RID: 18999
	public Transform GroupsTransform;

	// Token: 0x04004A38 RID: 19000
	public Sprite Overlay_NeedTech;

	// Token: 0x04004A39 RID: 19001
	public RectTransform buildingGroupsRoot;

	// Token: 0x04004A3A RID: 19002
	public RectTransform BuildButtonBGPanel;

	// Token: 0x04004A3B RID: 19003
	public RectTransform BuildingGroupContentsRect;

	// Token: 0x04004A3C RID: 19004
	public Sprite defaultBuildingIconSprite;

	// Token: 0x04004A3D RID: 19005
	private KScrollRect planScreenScrollRect;

	// Token: 0x04004A3E RID: 19006
	public Material defaultUIMaterial;

	// Token: 0x04004A3F RID: 19007
	public Material desaturatedUIMaterial;

	// Token: 0x04004A40 RID: 19008
	public LocText PlanCategoryLabel;

	// Token: 0x04004A41 RID: 19009
	public GameObject noResultMessage;

	// Token: 0x04004A42 RID: 19010
	private int nextCategoryToUpdateIDX = -1;

	// Token: 0x04004A43 RID: 19011
	private bool forceUpdateAllCategoryToggles;

	// Token: 0x04004A44 RID: 19012
	private bool forceRefreshAllBuildings = true;

	// Token: 0x04004A45 RID: 19013
	private List<PlanScreen.ToggleEntry> toggleEntries = new List<PlanScreen.ToggleEntry>();

	// Token: 0x04004A46 RID: 19014
	private int ignoreToolChangeMessages;

	// Token: 0x04004A47 RID: 19015
	private Dictionary<string, PlanScreen.RequirementsState> _buildableStatesByID = new Dictionary<string, PlanScreen.RequirementsState>();

	// Token: 0x04004A48 RID: 19016
	private Dictionary<Def, bool> _researchedDefs = new Dictionary<Def, bool>();

	// Token: 0x04004A49 RID: 19017
	[SerializeField]
	private TextStyleSetting[] CategoryLabelTextStyles;

	// Token: 0x04004A4A RID: 19018
	private float initTime;

	// Token: 0x04004A4B RID: 19019
	private Dictionary<Tag, HashedString> tagCategoryMap;

	// Token: 0x04004A4C RID: 19020
	private Dictionary<Tag, int> tagOrderMap;

	// Token: 0x04004A4D RID: 19021
	private BuildingDef lastSelectedBuildingDef;

	// Token: 0x04004A4E RID: 19022
	private Building lastSelectedBuilding;

	// Token: 0x04004A4F RID: 19023
	private string lastSelectedBuildingFacade = "DEFAULT_FACADE";

	// Token: 0x04004A50 RID: 19024
	private int buildable_state_update_idx;

	// Token: 0x04004A51 RID: 19025
	private int building_button_refresh_idx;

	// Token: 0x04004A52 RID: 19026
	private readonly int maxToggleRefreshPerFrame = 10;

	// Token: 0x04004A53 RID: 19027
	private bool categoryPanelSizeNeedsRefresh;

	// Token: 0x04004A54 RID: 19028
	private Comparer<global::Tuple<PlanBuildingToggle, string>> buildingDefComparer;

	// Token: 0x04004A55 RID: 19029
	private float buildGrid_bg_width = 320f;

	// Token: 0x04004A56 RID: 19030
	private float buildGrid_bg_borderHeight = 48f;

	// Token: 0x04004A57 RID: 19031
	private const float BUILDGRID_SEARCHBAR_HEIGHT = 36f;

	// Token: 0x04004A58 RID: 19032
	private const int SUBCATEGORY_HEADER_HEIGHT = 24;

	// Token: 0x04004A59 RID: 19033
	private float buildGrid_bg_rowHeight;

	// Token: 0x02001F9B RID: 8091
	public struct PlanInfo : IHasDlcRestrictions
	{
		// Token: 0x0600B3BC RID: 46012 RVA: 0x003DB168 File Offset: 0x003D9368
		public PlanInfo(HashedString category, bool hideIfNotResearched, List<string> listData, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			foreach (string text in listData)
			{
				list.Add(new KeyValuePair<string, string>(text, global::TUNING.BUILDINGS.PLANSUBCATEGORYSORTING.ContainsKey(text) ? global::TUNING.BUILDINGS.PLANSUBCATEGORYSORTING[text] : "uncategorized"));
			}
			this.category = category;
			this.hideIfNotResearched = hideIfNotResearched;
			this.data = listData;
			this.buildingAndSubcategoryData = list;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
		}

		// Token: 0x0600B3BD RID: 46013 RVA: 0x003DB20C File Offset: 0x003D940C
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x0600B3BE RID: 46014 RVA: 0x003DB214 File Offset: 0x003D9414
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x0400916E RID: 37230
		public HashedString category;

		// Token: 0x0400916F RID: 37231
		public bool hideIfNotResearched;

		// Token: 0x04009170 RID: 37232
		[Obsolete("Modders: Use ModUtil.AddBuildingToPlanScreen")]
		public List<string> data;

		// Token: 0x04009171 RID: 37233
		public List<KeyValuePair<string, string>> buildingAndSubcategoryData;

		// Token: 0x04009172 RID: 37234
		private string[] requiredDlcIds;

		// Token: 0x04009173 RID: 37235
		private string[] forbiddenDlcIds;
	}

	// Token: 0x02001F9C RID: 8092
	[Serializable]
	public struct BuildingToolTipSettings
	{
		// Token: 0x04009174 RID: 37236
		public TextStyleSetting BuildButtonName;

		// Token: 0x04009175 RID: 37237
		public TextStyleSetting BuildButtonDescription;

		// Token: 0x04009176 RID: 37238
		public TextStyleSetting MaterialRequirement;

		// Token: 0x04009177 RID: 37239
		public TextStyleSetting ResearchRequirement;
	}

	// Token: 0x02001F9D RID: 8093
	[Serializable]
	public struct BuildingNameTextSetting
	{
		// Token: 0x04009178 RID: 37240
		public TextStyleSetting ActiveSelected;

		// Token: 0x04009179 RID: 37241
		public TextStyleSetting ActiveDeselected;

		// Token: 0x0400917A RID: 37242
		public TextStyleSetting InactiveSelected;

		// Token: 0x0400917B RID: 37243
		public TextStyleSetting InactiveDeselected;
	}

	// Token: 0x02001F9E RID: 8094
	private class ToggleEntry
	{
		// Token: 0x0600B3BF RID: 46015 RVA: 0x003DB21C File Offset: 0x003D941C
		public ToggleEntry(KIconToggleMenu.ToggleInfo toggle_info, HashedString plan_category, List<BuildingDef> building_defs, bool hideIfNotResearched)
		{
			this.toggleInfo = toggle_info;
			this.planCategory = plan_category;
			building_defs.RemoveAll((BuildingDef def) => !Game.IsCorrectDlcActiveForCurrentSave(def));
			this.buildingDefs = building_defs;
			this.hideIfNotResearched = hideIfNotResearched;
			this.pendingResearchAttentions = new List<Tag>();
			this.requiredTechItems = new List<TechItem>();
			this.toggleImages = null;
			foreach (BuildingDef buildingDef in building_defs)
			{
				TechItem techItem = Db.Get().TechItems.TryGet(buildingDef.PrefabID);
				if (techItem == null)
				{
					this.requiredTechItems.Clear();
					break;
				}
				if (!this.requiredTechItems.Contains(techItem))
				{
					this.requiredTechItems.Add(techItem);
				}
			}
			this._areAnyRequiredTechItemsAvailable = false;
			this.Refresh();
		}

		// Token: 0x0600B3C0 RID: 46016 RVA: 0x003DB318 File Offset: 0x003D9518
		public bool AreAnyRequiredTechItemsAvailable()
		{
			return this._areAnyRequiredTechItemsAvailable;
		}

		// Token: 0x0600B3C1 RID: 46017 RVA: 0x003DB320 File Offset: 0x003D9520
		public void Refresh()
		{
			if (this._areAnyRequiredTechItemsAvailable)
			{
				return;
			}
			if (this.requiredTechItems.Count == 0)
			{
				this._areAnyRequiredTechItemsAvailable = true;
				return;
			}
			using (List<TechItem>.Enumerator enumerator = this.requiredTechItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (PlanScreen.TechRequirementsUpcoming(enumerator.Current))
					{
						this._areAnyRequiredTechItemsAvailable = true;
						break;
					}
				}
			}
		}

		// Token: 0x0600B3C2 RID: 46018 RVA: 0x003DB39C File Offset: 0x003D959C
		public void CollectToggleImages()
		{
			this.toggleImages = this.toggleInfo.toggle.gameObject.GetComponents<ImageToggleState>();
		}

		// Token: 0x0400917C RID: 37244
		public KIconToggleMenu.ToggleInfo toggleInfo;

		// Token: 0x0400917D RID: 37245
		public HashedString planCategory;

		// Token: 0x0400917E RID: 37246
		public List<BuildingDef> buildingDefs;

		// Token: 0x0400917F RID: 37247
		public List<Tag> pendingResearchAttentions;

		// Token: 0x04009180 RID: 37248
		private List<TechItem> requiredTechItems;

		// Token: 0x04009181 RID: 37249
		public ImageToggleState[] toggleImages;

		// Token: 0x04009182 RID: 37250
		public bool hideIfNotResearched;

		// Token: 0x04009183 RID: 37251
		private bool _areAnyRequiredTechItemsAvailable;
	}

	// Token: 0x02001F9F RID: 8095
	public enum RequirementsState
	{
		// Token: 0x04009185 RID: 37253
		Invalid,
		// Token: 0x04009186 RID: 37254
		Tech,
		// Token: 0x04009187 RID: 37255
		Materials,
		// Token: 0x04009188 RID: 37256
		Complete,
		// Token: 0x04009189 RID: 37257
		TelepadBuilt,
		// Token: 0x0400918A RID: 37258
		UniquePerWorld,
		// Token: 0x0400918B RID: 37259
		RocketInteriorOnly,
		// Token: 0x0400918C RID: 37260
		RocketInteriorForbidden
	}
}
