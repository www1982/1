using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C6D RID: 3181
public class BuildMenu : KScreen
{
	// Token: 0x06006114 RID: 24852 RVA: 0x0023ECA2 File Offset: 0x0023CEA2
	public override float GetSortKey()
	{
		return 6f;
	}

	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x06006115 RID: 24853 RVA: 0x0023ECA9 File Offset: 0x0023CEA9
	// (set) Token: 0x06006116 RID: 24854 RVA: 0x0023ECB0 File Offset: 0x0023CEB0
	public static BuildMenu Instance { get; private set; }

	// Token: 0x06006117 RID: 24855 RVA: 0x0023ECB8 File Offset: 0x0023CEB8
	public static void DestroyInstance()
	{
		BuildMenu.Instance = null;
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x06006118 RID: 24856 RVA: 0x0023ECC0 File Offset: 0x0023CEC0
	public BuildingDef SelectedBuildingDef
	{
		get
		{
			return this.selectedBuilding;
		}
	}

	// Token: 0x06006119 RID: 24857 RVA: 0x0023ECC8 File Offset: 0x0023CEC8
	private static HashedString CacheHashString(string str)
	{
		return HashCache.Get().Add(str);
	}

	// Token: 0x0600611A RID: 24858 RVA: 0x0023ECD5 File Offset: 0x0023CED5
	public static bool UseHotkeyBuildMenu()
	{
		return KPlayerPrefs.GetInt("ENABLE_HOTKEY_BUILD_MENU") != 0;
	}

	// Token: 0x0600611B RID: 24859 RVA: 0x0023ECE4 File Offset: 0x0023CEE4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.ConsumeMouseScroll = true;
		this.initTime = KTime.Instance.UnscaledGameTime;
		bool flag = BuildMenu.UseHotkeyBuildMenu();
		if (flag)
		{
			BuildMenu.Instance = this;
			this.productInfoScreen = global::Util.KInstantiateUI<ProductInfoScreen>(this.productInfoScreenPrefab, base.gameObject, true);
			this.productInfoScreen.rectTransform().pivot = new Vector2(0f, 0f);
			this.productInfoScreen.onElementsFullySelected = new global::System.Action(this.OnRecipeElementsFullySelected);
			this.productInfoScreen.Show(false);
			this.buildingsScreen = global::Util.KInstantiateUI<BuildMenuBuildingsScreen>(this.buildingsMenuPrefab.gameObject, base.gameObject, true);
			BuildMenuBuildingsScreen buildMenuBuildingsScreen = this.buildingsScreen;
			buildMenuBuildingsScreen.onBuildingSelected = (Action<BuildingDef>)Delegate.Combine(buildMenuBuildingsScreen.onBuildingSelected, new Action<BuildingDef>(this.OnBuildingSelected));
			this.buildingsScreen.Show(false);
			Game.Instance.Subscribe(288942073, new Action<object>(this.OnUIClear));
			Game.Instance.Subscribe(-1190690038, new Action<object>(this.OnBuildToolDeactivated));
			this.Initialize();
			this.rectTransform().anchoredPosition = Vector2.zero;
			return;
		}
		base.gameObject.SetActive(flag);
	}

	// Token: 0x0600611C RID: 24860 RVA: 0x0023EE28 File Offset: 0x0023D028
	private void Initialize()
	{
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair in this.submenus)
		{
			BuildMenuCategoriesScreen value = keyValuePair.Value;
			value.Close();
			global::UnityEngine.Object.DestroyImmediate(value.gameObject);
		}
		this.submenuStack.Clear();
		this.tagCategoryMap = new Dictionary<Tag, HashedString>();
		this.tagOrderMap = new Dictionary<Tag, int>();
		this.categorizedBuildingMap = new Dictionary<HashedString, List<BuildingDef>>();
		this.categorizedCategoryMap = new Dictionary<HashedString, List<HashedString>>();
		int num = 0;
		BuildMenu.DisplayInfo orderedBuildings = BuildMenu.OrderedBuildings;
		this.PopulateCategorizedMaps(orderedBuildings.category, 0, orderedBuildings.data, this.tagCategoryMap, this.tagOrderMap, ref num, this.categorizedBuildingMap, this.categorizedCategoryMap);
		BuildMenuCategoriesScreen buildMenuCategoriesScreen = this.submenus[BuildMenu.ROOT_HASHSTR];
		buildMenuCategoriesScreen.Show(true);
		buildMenuCategoriesScreen.modalKeyInputBehaviour = false;
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair2 in this.submenus)
		{
			HashedString key = keyValuePair2.Key;
			List<HashedString> list;
			if (!(key == BuildMenu.ROOT_HASHSTR) && this.categorizedCategoryMap.TryGetValue(key, out list))
			{
				Image component = keyValuePair2.Value.GetComponent<Image>();
				if (component != null)
				{
					component.enabled = list.Count > 0;
				}
			}
		}
		this.PositionMenus();
	}

	// Token: 0x0600611D RID: 24861 RVA: 0x0023EFA8 File Offset: 0x0023D1A8
	[ContextMenu("PositionMenus")]
	private void PositionMenus()
	{
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair in this.submenus)
		{
			HashedString key = keyValuePair.Key;
			BuildMenuCategoriesScreen value = keyValuePair.Value;
			LayoutGroup component = value.GetComponent<LayoutGroup>();
			Vector2 vector;
			BuildMenu.PadInfo padInfo;
			if (key == BuildMenu.ROOT_HASHSTR)
			{
				vector = this.rootMenuOffset;
				padInfo = this.rootMenuPadding;
				value.GetComponent<Image>().enabled = false;
			}
			else
			{
				vector = this.nestedMenuOffset;
				padInfo = this.nestedMenuPadding;
			}
			value.rectTransform().anchoredPosition = vector;
			component.padding.left = padInfo.left;
			component.padding.right = padInfo.right;
			component.padding.top = padInfo.top;
			component.padding.bottom = padInfo.bottom;
		}
		this.buildingsScreen.rectTransform().anchoredPosition = this.buildingsMenuOffset;
	}

	// Token: 0x0600611E RID: 24862 RVA: 0x0023F0B8 File Offset: 0x0023D2B8
	public void Refresh()
	{
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair in this.submenus)
		{
			keyValuePair.Value.UpdateBuildableStates(true);
		}
	}

	// Token: 0x0600611F RID: 24863 RVA: 0x0023F114 File Offset: 0x0023D314
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		Game.Instance.Subscribe(-107300940, new Action<object>(this.OnResearchComplete));
	}

	// Token: 0x06006120 RID: 24864 RVA: 0x0023F138 File Offset: 0x0023D338
	protected override void OnCmpDisable()
	{
		Game.Instance.Unsubscribe(-107300940, new Action<object>(this.OnResearchComplete));
		base.OnCmpDisable();
	}

	// Token: 0x06006121 RID: 24865 RVA: 0x0023F15C File Offset: 0x0023D35C
	private BuildMenuCategoriesScreen CreateCategorySubMenu(HashedString category, int depth, object data, Dictionary<HashedString, List<BuildingDef>> categorized_building_map, Dictionary<HashedString, List<HashedString>> categorized_category_map, Dictionary<Tag, HashedString> tag_category_map, BuildMenuBuildingsScreen buildings_screen)
	{
		BuildMenuCategoriesScreen buildMenuCategoriesScreen = global::Util.KInstantiateUI<BuildMenuCategoriesScreen>(this.categoriesMenuPrefab.gameObject, base.gameObject, true);
		buildMenuCategoriesScreen.Show(false);
		buildMenuCategoriesScreen.Configure(category, depth, data, this.categorizedBuildingMap, this.categorizedCategoryMap, this.buildingsScreen);
		buildMenuCategoriesScreen.onCategoryClicked = (Action<HashedString, int>)Delegate.Combine(buildMenuCategoriesScreen.onCategoryClicked, new Action<HashedString, int>(this.OnCategoryClicked));
		buildMenuCategoriesScreen.name = "BuildMenu_" + category.ToString();
		return buildMenuCategoriesScreen;
	}

	// Token: 0x06006122 RID: 24866 RVA: 0x0023F1E4 File Offset: 0x0023D3E4
	private void PopulateCategorizedMaps(HashedString category, int depth, object data, Dictionary<Tag, HashedString> category_map, Dictionary<Tag, int> order_map, ref int building_index, Dictionary<HashedString, List<BuildingDef>> categorized_building_map, Dictionary<HashedString, List<HashedString>> categorized_category_map)
	{
		Type type = data.GetType();
		if (type == typeof(BuildMenu.DisplayInfo))
		{
			BuildMenu.DisplayInfo displayInfo = (BuildMenu.DisplayInfo)data;
			List<HashedString> list;
			if (!categorized_category_map.TryGetValue(category, out list))
			{
				list = new List<HashedString>();
				categorized_category_map[category] = list;
			}
			list.Add(displayInfo.category);
			this.PopulateCategorizedMaps(displayInfo.category, depth + 1, displayInfo.data, category_map, order_map, ref building_index, categorized_building_map, categorized_category_map);
		}
		else
		{
			if (typeof(IList<BuildMenu.DisplayInfo>).IsAssignableFrom(type))
			{
				IEnumerable<BuildMenu.DisplayInfo> enumerable = (IList<BuildMenu.DisplayInfo>)data;
				List<HashedString> list2;
				if (!categorized_category_map.TryGetValue(category, out list2))
				{
					list2 = new List<HashedString>();
					categorized_category_map[category] = list2;
				}
				using (IEnumerator<BuildMenu.DisplayInfo> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						BuildMenu.DisplayInfo displayInfo2 = enumerator.Current;
						list2.Add(displayInfo2.category);
						this.PopulateCategorizedMaps(displayInfo2.category, depth + 1, displayInfo2.data, category_map, order_map, ref building_index, categorized_building_map, categorized_category_map);
					}
					goto IL_0195;
				}
			}
			foreach (BuildMenu.BuildingInfo buildingInfo in ((IList<BuildMenu.BuildingInfo>)data))
			{
				Tag tag = new Tag(buildingInfo.id);
				category_map[tag] = category;
				order_map[tag] = building_index;
				building_index++;
				List<BuildingDef> list3;
				if (!categorized_building_map.TryGetValue(category, out list3))
				{
					list3 = new List<BuildingDef>();
					categorized_building_map[category] = list3;
				}
				BuildingDef buildingDef = Assets.GetBuildingDef(buildingInfo.id);
				buildingDef.HotKey = buildingInfo.hotkey;
				list3.Add(buildingDef);
			}
		}
		IL_0195:
		this.submenus[category] = this.CreateCategorySubMenu(category, depth, data, this.categorizedBuildingMap, this.categorizedCategoryMap, this.tagCategoryMap, this.buildingsScreen);
	}

	// Token: 0x06006123 RID: 24867 RVA: 0x0023F3D0 File Offset: 0x0023D5D0
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (this.mouseOver && base.ConsumeMouseScroll && !e.TryConsume(global::Action.ZoomIn))
		{
			e.TryConsume(global::Action.ZoomOut);
		}
		if (!e.Consumed && this.selectedCategory.IsValid && e.TryConsume(global::Action.Escape))
		{
			this.OnUIClear(null);
			return;
		}
		if (!e.Consumed)
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06006124 RID: 24868 RVA: 0x0023F43C File Offset: 0x0023D63C
	public override void OnKeyUp(KButtonEvent e)
	{
		if (this.selectedCategory.IsValid && PlayerController.Instance.ConsumeIfNotDragging(e, global::Action.MouseRight))
		{
			this.OnUIClear(null);
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Deselect", false));
		}
		if (!e.Consumed)
		{
			base.OnKeyUp(e);
		}
	}

	// Token: 0x06006125 RID: 24869 RVA: 0x0023F48C File Offset: 0x0023D68C
	private void OnUIClear(object data)
	{
		SelectTool.Instance.Activate();
		PlayerController.Instance.ActivateTool(SelectTool.Instance);
		SelectTool.Instance.Select(null, true);
		this.productInfoScreen.materialSelectionPanel.PriorityScreen.ResetPriority();
		this.CloseMenus();
	}

	// Token: 0x06006126 RID: 24870 RVA: 0x0023F4D9 File Offset: 0x0023D6D9
	private void OnBuildToolDeactivated(object data)
	{
		if (this.updating)
		{
			this.deactivateToolQueued = true;
			return;
		}
		this.CloseMenus();
		this.productInfoScreen.materialSelectionPanel.PriorityScreen.ResetPriority();
	}

	// Token: 0x06006127 RID: 24871 RVA: 0x0023F508 File Offset: 0x0023D708
	private void CloseMenus()
	{
		this.productInfoScreen.Close();
		while (this.submenuStack.Count > 0)
		{
			this.submenuStack.Pop().Close();
			this.productInfoScreen.Close();
		}
		this.selectedCategory = HashedString.Invalid;
		this.submenus[BuildMenu.ROOT_HASHSTR].ClearSelection();
	}

	// Token: 0x06006128 RID: 24872 RVA: 0x0023F56B File Offset: 0x0023D76B
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		if (this.timeSinceNotificationPing < 8f)
		{
			this.timeSinceNotificationPing += Time.unscaledDeltaTime;
		}
		if (this.timeSinceNotificationPing >= 0.5f)
		{
			this.notificationPingCount = 0;
		}
	}

	// Token: 0x06006129 RID: 24873 RVA: 0x0023F5A8 File Offset: 0x0023D7A8
	public void PlayNewBuildingSounds()
	{
		if (KTime.Instance.UnscaledGameTime - this.initTime > 1.5f)
		{
			if (BuildMenu.Instance.timeSinceNotificationPing >= 8f)
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
				eventInstance.setParameterByName("playCount", (float)BuildMenu.Instance.notificationPingCount, false);
				SoundEvent.EndOneShot(eventInstance);
			}
		}
		this.timeSinceNotificationPing = 0f;
		this.notificationPingCount++;
	}

	// Token: 0x0600612A RID: 24874 RVA: 0x0023F678 File Offset: 0x0023D878
	public PlanScreen.RequirementsState BuildableState(BuildingDef def)
	{
		PlanScreen.RequirementsState requirementsState = PlanScreen.RequirementsState.Complete;
		if (!DebugHandler.InstantBuildMode && !Game.Instance.SandboxModeActive)
		{
			if (!Db.Get().TechItems.IsTechItemComplete(def.PrefabID))
			{
				requirementsState = PlanScreen.RequirementsState.Tech;
			}
			else if (!ProductInfoScreen.MaterialsMet(def.CraftRecipe))
			{
				requirementsState = PlanScreen.RequirementsState.Materials;
			}
		}
		return requirementsState;
	}

	// Token: 0x0600612B RID: 24875 RVA: 0x0023F6C5 File Offset: 0x0023D8C5
	private void CloseProductInfoScreen()
	{
		this.productInfoScreen.ClearProduct(true);
		this.productInfoScreen.Show(false);
	}

	// Token: 0x0600612C RID: 24876 RVA: 0x0023F6E0 File Offset: 0x0023D8E0
	private void Update()
	{
		if (this.deactivateToolQueued)
		{
			this.deactivateToolQueued = false;
			this.OnBuildToolDeactivated(null);
		}
		this.elapsedTime += Time.unscaledDeltaTime;
		if (this.elapsedTime <= this.updateInterval)
		{
			return;
		}
		this.elapsedTime = 0f;
		this.updating = true;
		foreach (KIconToggleMenu kiconToggleMenu in this.submenuStack)
		{
			if (kiconToggleMenu is BuildMenuCategoriesScreen)
			{
				(kiconToggleMenu as BuildMenuCategoriesScreen).UpdateBuildableStates(false);
			}
		}
		this.submenus[BuildMenu.ROOT_HASHSTR].UpdateBuildableStates(false);
		this.updating = false;
	}

	// Token: 0x0600612D RID: 24877 RVA: 0x0023F7A8 File Offset: 0x0023D9A8
	private void OnRecipeElementsFullySelected()
	{
		if (this.selectedBuilding == null)
		{
			global::Debug.Log("No def!");
		}
		if (this.selectedBuilding.isKAnimTile && this.selectedBuilding.isUtility)
		{
			IList<Tag> getSelectedElementAsList = this.productInfoScreen.materialSelectionPanel.GetSelectedElementAsList;
			((this.selectedBuilding.BuildingComplete.GetComponent<Wire>() != null) ? WireBuildTool.Instance : UtilityBuildTool.Instance).Activate(this.selectedBuilding, getSelectedElementAsList);
			return;
		}
		BuildTool.Instance.Activate(this.selectedBuilding, this.productInfoScreen.materialSelectionPanel.GetSelectedElementAsList);
	}

	// Token: 0x0600612E RID: 24878 RVA: 0x0023F84C File Offset: 0x0023DA4C
	private void OnBuildingSelected(BuildingDef def)
	{
		if (this.selecting)
		{
			return;
		}
		this.selecting = true;
		this.selectedBuilding = def;
		this.buildingsScreen.SetHasFocus(false);
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair in this.submenus)
		{
			keyValuePair.Value.SetHasFocus(false);
		}
		ToolMenu.Instance.ClearSelection();
		if (def != null)
		{
			Vector2 anchoredPosition = this.productInfoScreen.rectTransform().anchoredPosition;
			RectTransform rectTransform = this.buildingsScreen.rectTransform();
			anchoredPosition.y = rectTransform.anchoredPosition.y;
			anchoredPosition.x = rectTransform.anchoredPosition.x + rectTransform.sizeDelta.x + 10f;
			this.productInfoScreen.rectTransform().anchoredPosition = anchoredPosition;
			this.productInfoScreen.ClearProduct(false);
			this.productInfoScreen.Show(true);
			this.productInfoScreen.ConfigureScreen(def);
		}
		else
		{
			this.productInfoScreen.Close();
		}
		this.selecting = false;
	}

	// Token: 0x0600612F RID: 24879 RVA: 0x0023F97C File Offset: 0x0023DB7C
	private void OnCategoryClicked(HashedString new_category, int depth)
	{
		while (this.submenuStack.Count > depth)
		{
			KIconToggleMenu kiconToggleMenu = this.submenuStack.Pop();
			kiconToggleMenu.ClearSelection();
			kiconToggleMenu.Close();
		}
		this.productInfoScreen.Close();
		if (new_category != this.selectedCategory && new_category.IsValid)
		{
			foreach (KIconToggleMenu kiconToggleMenu2 in this.submenuStack)
			{
				if (kiconToggleMenu2 is BuildMenuCategoriesScreen)
				{
					(kiconToggleMenu2 as BuildMenuCategoriesScreen).SetHasFocus(false);
				}
			}
			this.selectedCategory = new_category;
			BuildMenuCategoriesScreen buildMenuCategoriesScreen;
			this.submenus.TryGetValue(new_category, out buildMenuCategoriesScreen);
			if (buildMenuCategoriesScreen != null)
			{
				buildMenuCategoriesScreen.Show(true);
				buildMenuCategoriesScreen.SetHasFocus(true);
				this.submenuStack.Push(buildMenuCategoriesScreen);
			}
		}
		else
		{
			this.selectedCategory = HashedString.Invalid;
		}
		foreach (KIconToggleMenu kiconToggleMenu3 in this.submenuStack)
		{
			if (kiconToggleMenu3 is BuildMenuCategoriesScreen)
			{
				(kiconToggleMenu3 as BuildMenuCategoriesScreen).UpdateBuildableStates(true);
			}
		}
		this.submenus[BuildMenu.ROOT_HASHSTR].UpdateBuildableStates(true);
	}

	// Token: 0x06006130 RID: 24880 RVA: 0x0023FAD4 File Offset: 0x0023DCD4
	public void RefreshProductInfoScreen(BuildingDef def)
	{
		if (this.productInfoScreen.currentDef == def)
		{
			this.productInfoScreen.ClearProduct(false);
			this.productInfoScreen.Show(true);
			this.productInfoScreen.ConfigureScreen(def);
		}
	}

	// Token: 0x06006131 RID: 24881 RVA: 0x0023FB10 File Offset: 0x0023DD10
	private HashedString GetParentCategory(HashedString desired_category)
	{
		foreach (KeyValuePair<HashedString, List<HashedString>> keyValuePair in this.categorizedCategoryMap)
		{
			using (List<HashedString>.Enumerator enumerator2 = keyValuePair.Value.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == desired_category)
					{
						return keyValuePair.Key;
					}
				}
			}
		}
		return HashedString.Invalid;
	}

	// Token: 0x06006132 RID: 24882 RVA: 0x0023FBB0 File Offset: 0x0023DDB0
	private void AddParentCategories(HashedString child_category, ICollection<HashedString> categories)
	{
		for (;;)
		{
			HashedString parentCategory = this.GetParentCategory(child_category);
			if (parentCategory == HashedString.Invalid)
			{
				break;
			}
			categories.Add(parentCategory);
			child_category = parentCategory;
		}
	}

	// Token: 0x06006133 RID: 24883 RVA: 0x0023FBE0 File Offset: 0x0023DDE0
	private void OnResearchComplete(object data)
	{
		HashSet<HashedString> hashSet = new HashSet<HashedString>();
		if (data is Tech)
		{
			Tech tech = (Tech)data;
			using (List<TechItem>.Enumerator enumerator = tech.unlockedItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TechItem techItem = enumerator.Current;
					BuildingDef buildingDef = Assets.GetBuildingDef(techItem.Id);
					if (buildingDef == null)
					{
						DebugUtil.LogWarningArgs(new object[] { string.Format("Tech '{0}' unlocked building '{1}' but no such building exists", tech.Name, techItem.Id) });
					}
					else
					{
						HashedString hashedString = this.tagCategoryMap[buildingDef.Tag];
						hashSet.Add(hashedString);
						this.AddParentCategories(hashedString, hashSet);
					}
				}
				goto IL_00E0;
			}
		}
		if (data is BuildingDef)
		{
			BuildingDef buildingDef2 = data as BuildingDef;
			HashedString hashedString2 = this.tagCategoryMap[buildingDef2.Tag];
			hashSet.Add(hashedString2);
			this.AddParentCategories(hashedString2, hashSet);
		}
		IL_00E0:
		this.UpdateNotifications(hashSet, BuildMenu.OrderedBuildings);
	}

	// Token: 0x06006134 RID: 24884 RVA: 0x0023FCF0 File Offset: 0x0023DEF0
	private void UpdateNotifications(ICollection<HashedString> updated_categories, object data)
	{
		foreach (KeyValuePair<HashedString, BuildMenuCategoriesScreen> keyValuePair in this.submenus)
		{
			keyValuePair.Value.UpdateNotifications(updated_categories);
		}
	}

	// Token: 0x06006135 RID: 24885 RVA: 0x0023FD4C File Offset: 0x0023DF4C
	public PrioritySetting GetBuildingPriority()
	{
		return this.productInfoScreen.materialSelectionPanel.PriorityScreen.GetLastSelectedPriority();
	}

	// Token: 0x040041C2 RID: 16834
	public const string ENABLE_HOTKEY_BUILD_MENU_KEY = "ENABLE_HOTKEY_BUILD_MENU";

	// Token: 0x040041C3 RID: 16835
	[SerializeField]
	private BuildMenuCategoriesScreen categoriesMenuPrefab;

	// Token: 0x040041C4 RID: 16836
	[SerializeField]
	private BuildMenuBuildingsScreen buildingsMenuPrefab;

	// Token: 0x040041C5 RID: 16837
	[SerializeField]
	private GameObject productInfoScreenPrefab;

	// Token: 0x040041C6 RID: 16838
	private ProductInfoScreen productInfoScreen;

	// Token: 0x040041C7 RID: 16839
	private BuildMenuBuildingsScreen buildingsScreen;

	// Token: 0x040041C8 RID: 16840
	private BuildingDef selectedBuilding;

	// Token: 0x040041C9 RID: 16841
	private HashedString selectedCategory;

	// Token: 0x040041CA RID: 16842
	private static readonly HashedString ROOT_HASHSTR = new HashedString("ROOT");

	// Token: 0x040041CB RID: 16843
	private Dictionary<HashedString, BuildMenuCategoriesScreen> submenus = new Dictionary<HashedString, BuildMenuCategoriesScreen>();

	// Token: 0x040041CC RID: 16844
	private Stack<KIconToggleMenu> submenuStack = new Stack<KIconToggleMenu>();

	// Token: 0x040041CD RID: 16845
	private bool selecting;

	// Token: 0x040041CE RID: 16846
	private bool updating;

	// Token: 0x040041CF RID: 16847
	private bool deactivateToolQueued;

	// Token: 0x040041D0 RID: 16848
	[SerializeField]
	private Vector2 rootMenuOffset = Vector2.zero;

	// Token: 0x040041D1 RID: 16849
	[SerializeField]
	private BuildMenu.PadInfo rootMenuPadding;

	// Token: 0x040041D2 RID: 16850
	[SerializeField]
	private Vector2 nestedMenuOffset = Vector2.zero;

	// Token: 0x040041D3 RID: 16851
	[SerializeField]
	private BuildMenu.PadInfo nestedMenuPadding;

	// Token: 0x040041D4 RID: 16852
	[SerializeField]
	private Vector2 buildingsMenuOffset = Vector2.zero;

	// Token: 0x040041D5 RID: 16853
	public static BuildMenu.DisplayInfo OrderedBuildings = new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("ROOT"), "icon_category_base", global::Action.NumActions, KKeyCode.None, new List<BuildMenu.DisplayInfo>
	{
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Base"), "icon_category_base", global::Action.Plan1, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Tiles"), "icon_category_base", global::Action.BuildCategoryTiles, KKeyCode.T, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Tile", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("SnowTile", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("WoodTile", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("GasPermeableMembrane", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("MeshTile", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("InsulationTile", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("PlasticTile", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("MetalTile", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("GlassTile", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("BunkerTile", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("CarpetTile", global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("ExteriorWall", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("ExobaseHeadquarters", global::Action.BuildMenuKeyH)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Ladders"), "icon_category_base", global::Action.BuildCategoryLadders, KKeyCode.A, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Ladder", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("LadderFast", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("FirePole", global::Action.BuildMenuKeyF)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Doors"), "icon_category_base", global::Action.BuildCategoryDoors, KKeyCode.D, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Door", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("ManualPressureDoor", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("PressureDoor", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("BunkerDoor", global::Action.BuildMenuKeyB)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Storage"), "icon_category_base", global::Action.BuildCategoryStorage, KKeyCode.S, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("StorageLocker", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("RationBox", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("Refrigerator", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("StorageLockerSmart", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("LiquidReservoir", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("GasReservoir", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("ObjectDispenser", global::Action.BuildMenuKeyO)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Research"), "icon_category_misc", global::Action.BuildCategoryResearch, KKeyCode.R, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("ResearchCenter", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("AdvancedResearchCenter", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("CosmicResearchCenter", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("DLC1CosmicResearchCenter", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("NuclearResearchCenter", global::Action.BuildMenuKeyN),
				new BuildMenu.BuildingInfo("Telescope", global::Action.BuildMenuKeyT)
			})
		}),
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Food And Agriculture"), "icon_category_food", global::Action.Plan2, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Farming"), "icon_category_food", global::Action.BuildCategoryFarming, KKeyCode.F, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("PlanterBox", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("FarmTile", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("HydroponicFarm", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("Compost", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("FertilizerMaker", global::Action.BuildMenuKeyR)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Cooking"), "icon_category_food", global::Action.BuildCategoryCooking, KKeyCode.C, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("MicrobeMusher", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("CookingStation", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("Deepfryer", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("SpiceGrinder", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("GourmetCookingStation", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("EggCracker", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("Smoker", global::Action.BuildMenuKeyM)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Ranching"), "icon_category_food", global::Action.BuildCategoryRanching, KKeyCode.R, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("CreatureDeliveryPoint", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("FishDeliveryPoint", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("CreatureFeeder", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("FishFeeder", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("RanchStation", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("ShearingStation", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("EggIncubator", global::Action.BuildMenuKeyI),
				new BuildMenu.BuildingInfo("CreatureGroundTrap", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("WaterTrap", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("CreatureAirTrap", global::Action.BuildMenuKeyL)
			})
		}),
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Health And Happiness"), "icon_category_medical", global::Action.Plan3, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Medical"), "icon_category_medical", global::Action.BuildCategoryMedical, KKeyCode.C, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Apothecary", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("DoctorStation", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("AdvancedDoctorStation", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("MedicalCot", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("MassageTable", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("Grave", global::Action.BuildMenuKeyR)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Hygiene"), "icon_category_medical", global::Action.BuildCategoryHygiene, KKeyCode.E, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Outhouse", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("FlushToilet", global::Action.BuildMenuKeyV),
				new BuildMenu.BuildingInfo(ShowerConfig.ID, global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("WashBasin", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("WashSink", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("HandSanitizer", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("DecontaminationShower", global::Action.BuildMenuKeyD)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Furniture"), "icon_category_furniture", global::Action.BuildCategoryFurniture, KKeyCode.F, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Bed", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("LuxuryBed", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo(LadderBedConfig.ID, global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("DiningTable", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("FloorLamp", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("CeilingLight", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("SunLamp", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("MercuryCeilingLight", global::Action.BuildMenuKeyM),
				new BuildMenu.BuildingInfo("RadiationLight", global::Action.BuildMenuKeyR)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Decor"), "icon_category_furniture", global::Action.BuildCategoryDecor, KKeyCode.D, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("CeilingFossilSculpture", global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("FossilSculpture", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("FlowerVase", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("Canvas", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("CanvasWide", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("CanvasTall", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("Sculpture", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("IceSculpture", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("ItemPedestal", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("CrownMoulding", global::Action.BuildMenuKeyM),
				new BuildMenu.BuildingInfo("CornerMoulding", global::Action.BuildMenuKeyN)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Recreation"), "icon_category_medical", global::Action.BuildCategoryRecreation, KKeyCode.R, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("WaterCooler", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("ArcadeMachine", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("Phonobox", global::Action.BuildMenuKeyP),
				new BuildMenu.BuildingInfo("EspressoMachine", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("HotTub", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("MechanicalSurfboard", global::Action.BuildMenuKeyM),
				new BuildMenu.BuildingInfo("Sauna", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("BeachChair", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("Juicer", global::Action.BuildMenuKeyJ),
				new BuildMenu.BuildingInfo("SodaFountain", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("VerticalWindTunnel", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("ParkSign", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("Telephone", global::Action.BuildMenuKeyT)
			})
		}),
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Infrastructure"), "icon_category_utilities", global::Action.Plan4, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Wires"), "icon_category_electrical", global::Action.BuildCategoryWires, KKeyCode.W, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Wire", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("WireBridge", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("HighWattageWire", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("WireBridgeHighWattage", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("WireRefined", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("WireRefinedBridge", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("WireRefinedHighWattage", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("WireRefinedBridgeHighWattage", global::Action.BuildMenuKeyA)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Generators"), "icon_category_electrical", global::Action.BuildCategoryGenerators, KKeyCode.G, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("ManualGenerator", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("Generator", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("WoodGasGenerator", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("PeatGenerator", global::Action.BuildMenuKeyP),
				new BuildMenu.BuildingInfo("NuclearReactor", global::Action.BuildMenuKeyN),
				new BuildMenu.BuildingInfo("HydrogenGenerator", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("MethaneGenerator", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("PetroleumGenerator", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("SteamTurbine", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("SteamTurbine2", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("SolarPanel", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("DevGenerator", global::Action.BuildMenuKeyX)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("PowerControl"), "icon_category_electrical", global::Action.BuildCategoryPowerControl, KKeyCode.R, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Battery", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("BatteryMedium", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("BatterySmart", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("PowerTransformerSmall", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("PowerTransformer", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo(SwitchConfig.ID, global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo(TemperatureControlledSwitchConfig.ID, global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo(PressureSwitchLiquidConfig.ID, global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo(PressureSwitchGasConfig.ID, global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo(LogicPowerRelayConfig.ID, global::Action.BuildMenuKeyX)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Pipes"), "icon_category_plumbing", global::Action.BuildCategoryPipes, KKeyCode.E, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("LiquidConduit", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("LiquidConduitBridge", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("InsulatedLiquidConduit", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("LiquidConduitRadiant", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("GasConduit", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("GasConduitBridge", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("InsulatedGasConduit", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("GasConduitRadiant", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("ContactConductivePipeBridge", global::Action.BuildMenuKeyA)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Plumbing Structures"), "icon_category_plumbing", global::Action.BuildCategoryPlumbingStructures, KKeyCode.B, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("IceKettle", global::Action.BuildMenuKeyI),
				new BuildMenu.BuildingInfo("LiquidPumpingStation", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("BottleEmptier", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("LiquidPump", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("LiquidMiniPump", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("LiquidValve", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("LiquidLogicValve", global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("LiquidVent", global::Action.BuildMenuKeyV),
				new BuildMenu.BuildingInfo("LiquidFilter", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("LiquidConduitPreferentialFlow", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("LiquidConduitOverflow", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("LiquidLimitValve", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortLiquid", global::Action.BuildMenuKeyM),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortLiquidUnloader", global::Action.BuildMenuKeyU)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Ventilation Structures"), "icon_category_ventilation", global::Action.BuildCategoryVentilationStructures, KKeyCode.V, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("GasPump", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("GasMiniPump", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("GasValve", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("GasLogicValve", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("GasVent", global::Action.BuildMenuKeyV),
				new BuildMenu.BuildingInfo("GasVentHighPressure", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("GasFilter", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("GasBottler", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("BottleEmptierGas", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("GasConduitPreferentialFlow", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("GasConduitOverflow", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("GasLimitValve", global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortGas", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortGasUnloader", global::Action.BuildMenuKeyU)
			})
		}),
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Industrial"), "icon_category_refinery", global::Action.Plan5, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Oxygen"), "icon_category_oxygen", global::Action.BuildCategoryOxygen, KKeyCode.X, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("MineralDeoxidizer", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("SublimationStation", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("Oxysconce", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("AlgaeHabitat", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("AirFilter", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("CO2Scrubber", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("Electrolyzer", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("RustDeoxidizer", global::Action.BuildMenuKeyF)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Utilities"), "icon_category_utilities", global::Action.BuildCategoryUtilities, KKeyCode.T, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Campfire", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("DevHeater", global::Action.BuildMenuKeyH),
				new BuildMenu.BuildingInfo("SpaceHeater", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("LiquidHeater", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("IceCooledFan", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("IceMachine", global::Action.BuildMenuKeyI),
				new BuildMenu.BuildingInfo("AirConditioner", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("LiquidConditioner", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("OreScrubber", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("ThermalBlock", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("HighEnergyParticleRedirector", global::Action.BuildMenuKeyP)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Refining"), "icon_category_refinery", global::Action.BuildCategoryRefining, KKeyCode.R, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("WaterPurifier", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("AlgaeDistillery", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("EthanolDistillery", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("RockCrusher", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("SludgePress", global::Action.BuildMenuKeyP),
				new BuildMenu.BuildingInfo("Kiln", global::Action.BuildMenuKeyZ),
				new BuildMenu.BuildingInfo("OilWellCap", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("OilRefinery", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("Polymerizer", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("MetalRefinery", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("GlassForge", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("OxyliteRefinery", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("ChemicalRefinery", global::Action.BuildMenuKeyN),
				new BuildMenu.BuildingInfo("SupermaterialRefinery", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("UraniumCentrifuge", global::Action.BuildMenuKeyU)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Equipment"), "icon_category_misc", global::Action.BuildCategoryEquipment, KKeyCode.S, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("RoleStation", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("FarmStation", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo("PowerControlStation", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("AstronautTrainingCenter", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("ResetSkillsStation", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("CraftingTable", global::Action.BuildMenuKeyZ),
				new BuildMenu.BuildingInfo("OxygenMaskMarker", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("OxygenMaskLocker", global::Action.BuildMenuKeyY),
				new BuildMenu.BuildingInfo("ClothingFabricator", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("SuitFabricator", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("SuitMarker", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("SuitLocker", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("JetSuitMarker", global::Action.BuildMenuKeyJ),
				new BuildMenu.BuildingInfo("JetSuitLocker", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("LeadSuitMarker", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("LeadSuitLocker", global::Action.BuildMenuKeyD)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Rocketry"), "icon_category_rocketry", global::Action.BuildCategoryRocketry, KKeyCode.C, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("Gantry", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortBridge", global::Action.BuildMenuKeyU),
				new BuildMenu.BuildingInfo("KeroseneEngine", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("SolidBooster", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("SteamEngine", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("LiquidFuelTank", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("CargoBay", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("GasCargoBay", global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo("LiquidCargoBay", global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo("SpecialCargoBay", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("SpecialCargoBayCluster", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("CommandModule", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("TouristModule", global::Action.BuildMenuKeyY),
				new BuildMenu.BuildingInfo("ResearchModule", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("HydrogenEngine", global::Action.BuildMenuKeyH),
				new BuildMenu.BuildingInfo("RailGun", global::Action.BuildMenuKeyP),
				new BuildMenu.BuildingInfo("LandingBeacon", global::Action.BuildMenuKeyL)
			})
		}),
		new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Logistics"), "icon_category_ventilation", global::Action.Plan6, KKeyCode.None, new List<BuildMenu.DisplayInfo>
		{
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("TravelTubes"), "icon_category_ventilation", global::Action.BuildCategoryTravelTubes, KKeyCode.T, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("TravelTube", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("TravelTubeEntrance", global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo("TravelTubeWallBridge", global::Action.BuildMenuKeyB)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("Conveyance"), "icon_category_ventilation", global::Action.BuildCategoryConveyance, KKeyCode.C, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("SolidTransferArm", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("SolidConduit", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo("SolidConduitInbox", global::Action.BuildMenuKeyI),
				new BuildMenu.BuildingInfo("SolidConduitOutbox", global::Action.BuildMenuKeyO),
				new BuildMenu.BuildingInfo("SolidVent", global::Action.BuildMenuKeyV),
				new BuildMenu.BuildingInfo("SolidLogicValve", global::Action.BuildMenuKeyL),
				new BuildMenu.BuildingInfo("SolidLimitValve", global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo("SolidConduitBridge", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("AutoMiner", global::Action.BuildMenuKeyM),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortSolid", global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo("ModularLaunchpadPortSolidUnloader", global::Action.BuildMenuKeyU)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("LogicWiring"), "icon_category_automation", global::Action.BuildCategoryLogicWiring, KKeyCode.W, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("LogicWire", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("LogicWireBridge", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("LogicRibbon", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("LogicRibbonBridge", global::Action.BuildMenuKeyV)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("LogicGates"), "icon_category_automation", global::Action.BuildCategoryLogicGates, KKeyCode.G, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo("LogicGateAND", global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo("LogicGateOR", global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("LogicGateXOR", global::Action.BuildMenuKeyX),
				new BuildMenu.BuildingInfo("LogicGateNOT", global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo("LogicGateBUFFER", global::Action.BuildMenuKeyB),
				new BuildMenu.BuildingInfo("LogicGateFILTER", global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo(LogicMemoryConfig.ID, global::Action.BuildMenuKeyV)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("LogicSwitches"), "icon_category_automation", global::Action.BuildCategoryLogicSwitches, KKeyCode.S, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo(LogicSwitchConfig.ID, global::Action.BuildMenuKeyS),
				new BuildMenu.BuildingInfo(LogicPressureSensorGasConfig.ID, global::Action.BuildMenuKeyA),
				new BuildMenu.BuildingInfo(LogicPressureSensorLiquidConfig.ID, global::Action.BuildMenuKeyQ),
				new BuildMenu.BuildingInfo(LogicTemperatureSensorConfig.ID, global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo(LogicTimeOfDaySensorConfig.ID, global::Action.BuildMenuKeyD),
				new BuildMenu.BuildingInfo(LogicTimerSensorConfig.ID, global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo(LogicCritterCountSensorConfig.ID, global::Action.BuildMenuKeyV),
				new BuildMenu.BuildingInfo(LogicDiseaseSensorConfig.ID, global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo(LogicElementSensorGasConfig.ID, global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo(LogicWattageSensorConfig.ID, global::Action.BuildMenuKeyP),
				new BuildMenu.BuildingInfo("FloorSwitch", global::Action.BuildMenuKeyW),
				new BuildMenu.BuildingInfo("Checkpoint", global::Action.BuildMenuKeyC),
				new BuildMenu.BuildingInfo(CometDetectorConfig.ID, global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo("LogicDuplicantSensor", global::Action.BuildMenuKeyF)
			}),
			new BuildMenu.DisplayInfo(BuildMenu.CacheHashString("ConduitSensors"), "icon_category_automation", global::Action.BuildCategoryLogicConduits, KKeyCode.X, new List<BuildMenu.BuildingInfo>
			{
				new BuildMenu.BuildingInfo(LiquidConduitTemperatureSensorConfig.ID, global::Action.BuildMenuKeyT),
				new BuildMenu.BuildingInfo(LiquidConduitDiseaseSensorConfig.ID, global::Action.BuildMenuKeyG),
				new BuildMenu.BuildingInfo(LiquidConduitElementSensorConfig.ID, global::Action.BuildMenuKeyE),
				new BuildMenu.BuildingInfo(GasConduitTemperatureSensorConfig.ID, global::Action.BuildMenuKeyR),
				new BuildMenu.BuildingInfo(GasConduitDiseaseSensorConfig.ID, global::Action.BuildMenuKeyF),
				new BuildMenu.BuildingInfo(GasConduitElementSensorConfig.ID, global::Action.BuildMenuKeyS)
			})
		})
	});

	// Token: 0x040041D6 RID: 16854
	private Dictionary<HashedString, List<BuildingDef>> categorizedBuildingMap;

	// Token: 0x040041D7 RID: 16855
	private Dictionary<HashedString, List<HashedString>> categorizedCategoryMap;

	// Token: 0x040041D8 RID: 16856
	private Dictionary<Tag, HashedString> tagCategoryMap;

	// Token: 0x040041D9 RID: 16857
	private Dictionary<Tag, int> tagOrderMap;

	// Token: 0x040041DA RID: 16858
	private const float NotificationPingExpire = 0.5f;

	// Token: 0x040041DB RID: 16859
	private const float SpecialNotificationEmbellishDelay = 8f;

	// Token: 0x040041DC RID: 16860
	private float timeSinceNotificationPing;

	// Token: 0x040041DD RID: 16861
	private int notificationPingCount;

	// Token: 0x040041DE RID: 16862
	private float initTime;

	// Token: 0x040041DF RID: 16863
	private float updateInterval = 1f;

	// Token: 0x040041E0 RID: 16864
	private float elapsedTime;

	// Token: 0x02001E30 RID: 7728
	[Serializable]
	private struct PadInfo
	{
		// Token: 0x04008CD1 RID: 36049
		public int left;

		// Token: 0x04008CD2 RID: 36050
		public int right;

		// Token: 0x04008CD3 RID: 36051
		public int top;

		// Token: 0x04008CD4 RID: 36052
		public int bottom;
	}

	// Token: 0x02001E31 RID: 7729
	public struct BuildingInfo
	{
		// Token: 0x0600AFDB RID: 45019 RVA: 0x003D133A File Offset: 0x003CF53A
		public BuildingInfo(string id, global::Action hotkey)
		{
			this.id = id;
			this.hotkey = hotkey;
		}

		// Token: 0x04008CD5 RID: 36053
		public string id;

		// Token: 0x04008CD6 RID: 36054
		public global::Action hotkey;
	}

	// Token: 0x02001E32 RID: 7730
	public struct DisplayInfo
	{
		// Token: 0x0600AFDC RID: 45020 RVA: 0x003D134A File Offset: 0x003CF54A
		public DisplayInfo(HashedString category, string icon_name, global::Action hotkey, KKeyCode key_code, object data)
		{
			this.category = category;
			this.iconName = icon_name;
			this.hotkey = hotkey;
			this.keyCode = key_code;
			this.data = data;
		}

		// Token: 0x0600AFDD RID: 45021 RVA: 0x003D1374 File Offset: 0x003CF574
		public BuildMenu.DisplayInfo GetInfo(HashedString category)
		{
			BuildMenu.DisplayInfo displayInfo = default(BuildMenu.DisplayInfo);
			if (this.data != null && typeof(IList<BuildMenu.DisplayInfo>).IsAssignableFrom(this.data.GetType()))
			{
				foreach (BuildMenu.DisplayInfo displayInfo2 in ((IList<BuildMenu.DisplayInfo>)this.data))
				{
					displayInfo = displayInfo2.GetInfo(category);
					if (displayInfo.category == category)
					{
						break;
					}
					if (displayInfo2.category == category)
					{
						displayInfo = displayInfo2;
						break;
					}
				}
			}
			return displayInfo;
		}

		// Token: 0x04008CD7 RID: 36055
		public HashedString category;

		// Token: 0x04008CD8 RID: 36056
		public string iconName;

		// Token: 0x04008CD9 RID: 36057
		public global::Action hotkey;

		// Token: 0x04008CDA RID: 36058
		public KKeyCode keyCode;

		// Token: 0x04008CDB RID: 36059
		public object data;
	}
}
