using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000D46 RID: 3398
public class MaterialSelectionPanel : KScreen, IRender200ms
{
	// Token: 0x06006949 RID: 26953 RVA: 0x0027DC73 File Offset: 0x0027BE73
	public static void ClearStatics()
	{
		MaterialSelectionPanel.elementsWithTag.Clear();
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x0600694A RID: 26954 RVA: 0x0027DC7F File Offset: 0x0027BE7F
	public Tag CurrentSelectedElement
	{
		get
		{
			if (this.materialSelectors.Count == 0)
			{
				return null;
			}
			return this.materialSelectors[0].CurrentSelectedElement;
		}
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x0600694B RID: 26955 RVA: 0x0027DCA8 File Offset: 0x0027BEA8
	public IList<Tag> GetSelectedElementAsList
	{
		get
		{
			this.currentSelectedElements.Clear();
			foreach (MaterialSelector materialSelector in this.materialSelectors)
			{
				if (materialSelector.gameObject.activeSelf)
				{
					global::Debug.Assert(materialSelector.CurrentSelectedElement != null);
					this.currentSelectedElements.Add(materialSelector.CurrentSelectedElement);
				}
			}
			return this.currentSelectedElements;
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x0600694C RID: 26956 RVA: 0x0027DD3C File Offset: 0x0027BF3C
	public PriorityScreen PriorityScreen
	{
		get
		{
			return this.priorityScreen;
		}
	}

	// Token: 0x0600694D RID: 26957 RVA: 0x0027DD44 File Offset: 0x0027BF44
	protected override void OnPrefabInit()
	{
		MaterialSelectionPanel.elementsWithTag.Clear();
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		for (int i = 0; i < 3; i++)
		{
			MaterialSelector materialSelector = Util.KInstantiateUI<MaterialSelector>(this.MaterialSelectorTemplate, base.gameObject, false);
			materialSelector.selectorIndex = i;
			this.materialSelectors.Add(materialSelector);
		}
		this.materialSelectors[0].gameObject.SetActive(true);
		this.MaterialSelectorTemplate.SetActive(false);
		this.ToggleResearchRequired(false);
		if (this.priorityScreenParent != null)
		{
			this.priorityScreen = Util.KInstantiateUI<PriorityScreen>(this.priorityScreenPrefab.gameObject, this.priorityScreenParent, false);
			this.priorityScreen.InstantiateButtons(new Action<PrioritySetting>(this.OnPriorityClicked), true);
			this.priorityScreenParent.transform.SetAsLastSibling();
		}
		this.gameSubscriptionHandles.Add(Game.Instance.Subscribe(-107300940, delegate(object d)
		{
			this.RefreshSelectors();
		}));
	}

	// Token: 0x0600694E RID: 26958 RVA: 0x0027DE3D File Offset: 0x0027C03D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.activateOnSpawn = true;
	}

	// Token: 0x0600694F RID: 26959 RVA: 0x0027DE4C File Offset: 0x0027C04C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		foreach (int num in this.gameSubscriptionHandles)
		{
			Game.Instance.Unsubscribe(num);
		}
		this.gameSubscriptionHandles.Clear();
	}

	// Token: 0x06006950 RID: 26960 RVA: 0x0027DEB4 File Offset: 0x0027C0B4
	public void AddSelectAction(MaterialSelector.SelectMaterialActions action)
	{
		this.materialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.selectMaterialActions = (MaterialSelector.SelectMaterialActions)Delegate.Combine(selector.selectMaterialActions, action);
		});
	}

	// Token: 0x06006951 RID: 26961 RVA: 0x0027DEE5 File Offset: 0x0027C0E5
	public void ClearSelectActions()
	{
		this.materialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.selectMaterialActions = null;
		});
	}

	// Token: 0x06006952 RID: 26962 RVA: 0x0027DF11 File Offset: 0x0027C111
	public void ClearMaterialToggles()
	{
		this.materialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.ClearMaterialToggles();
		});
	}

	// Token: 0x06006953 RID: 26963 RVA: 0x0027DF3D File Offset: 0x0027C13D
	public void ConfigureScreen(Recipe recipe, MaterialSelectionPanel.GetBuildableStateDelegate buildableStateCB, MaterialSelectionPanel.GetBuildableTooltipDelegate buildableTooltipCB)
	{
		this.activeRecipe = recipe;
		this.GetBuildableState = buildableStateCB;
		this.GetBuildableTooltip = buildableTooltipCB;
		this.RefreshSelectors();
	}

	// Token: 0x06006954 RID: 26964 RVA: 0x0027DF5C File Offset: 0x0027C15C
	public bool AllSelectorsSelected()
	{
		bool flag = false;
		foreach (MaterialSelector materialSelector in this.materialSelectors)
		{
			flag = flag || materialSelector.gameObject.activeInHierarchy;
			if (materialSelector.gameObject.activeInHierarchy && materialSelector.CurrentSelectedElement == null)
			{
				return false;
			}
		}
		return flag;
	}

	// Token: 0x06006955 RID: 26965 RVA: 0x0027DFE4 File Offset: 0x0027C1E4
	public void RefreshSelectors()
	{
		if (this.activeRecipe == null)
		{
			return;
		}
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.materialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			selector.gameObject.SetActive(false);
		});
		BuildingDef buildingDef = this.activeRecipe.GetBuildingDef();
		bool flag = this.GetBuildableState(buildingDef);
		string text = this.GetBuildableTooltip(buildingDef);
		if (!flag)
		{
			this.ToggleResearchRequired(true);
			LocText[] componentsInChildren = this.ResearchRequired.GetComponentsInChildren<LocText>();
			componentsInChildren[0].text = "";
			componentsInChildren[1].text = text;
			componentsInChildren[1].color = Constants.NEGATIVE_COLOR;
			if (this.priorityScreen != null)
			{
				this.priorityScreen.gameObject.SetActive(false);
			}
			if (this.buildToolRotateButton != null)
			{
				this.buildToolRotateButton.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.ToggleResearchRequired(false);
			for (int i = 0; i < this.activeRecipe.Ingredients.Count; i++)
			{
				this.materialSelectors[i].gameObject.SetActive(true);
				this.materialSelectors[i].ConfigureScreen(this.activeRecipe.Ingredients[i], this.activeRecipe);
			}
			if (this.priorityScreen != null)
			{
				this.priorityScreen.gameObject.SetActive(true);
				this.priorityScreen.transform.SetAsLastSibling();
			}
			if (this.buildToolRotateButton != null)
			{
				this.buildToolRotateButton.gameObject.SetActive(true);
				this.buildToolRotateButton.transform.SetAsLastSibling();
			}
		}
	}

	// Token: 0x06006956 RID: 26966 RVA: 0x0027E18E File Offset: 0x0027C38E
	private void UpdateResourceToggleValues()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.materialSelectors.ForEach(delegate(MaterialSelector selector)
		{
			if (selector.gameObject.activeSelf)
			{
				selector.RefreshToggleContents();
			}
		});
	}

	// Token: 0x06006957 RID: 26967 RVA: 0x0027E1C8 File Offset: 0x0027C3C8
	private void ToggleResearchRequired(bool state)
	{
		if (this.ResearchRequired == null)
		{
			return;
		}
		this.ResearchRequired.SetActive(state);
	}

	// Token: 0x06006958 RID: 26968 RVA: 0x0027E1E8 File Offset: 0x0027C3E8
	public bool AutoSelectAvailableMaterial()
	{
		bool flag = true;
		for (int i = 0; i < this.materialSelectors.Count; i++)
		{
			if (!this.materialSelectors[i].AutoSelectAvailableMaterial())
			{
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06006959 RID: 26969 RVA: 0x0027E224 File Offset: 0x0027C424
	public void SelectSourcesMaterials(Building building)
	{
		Tag[] array = null;
		Deconstructable component = building.gameObject.GetComponent<Deconstructable>();
		if (component != null)
		{
			array = component.constructionElements;
		}
		Constructable component2 = building.GetComponent<Constructable>();
		if (component2 != null)
		{
			array = component2.SelectedElementsTags.ToArray<Tag>();
		}
		if (array != null)
		{
			for (int i = 0; i < Mathf.Min(array.Length, this.materialSelectors.Count); i++)
			{
				if (this.materialSelectors[i].ElementToggles.ContainsKey(array[i]))
				{
					this.materialSelectors[i].OnSelectMaterial(array[i], this.activeRecipe, false);
				}
			}
		}
	}

	// Token: 0x0600695A RID: 26970 RVA: 0x0027E2CA File Offset: 0x0027C4CA
	public void ForceSelectPrimaryTag(Tag tag)
	{
		this.materialSelectors[0].OnSelectMaterial(tag, this.activeRecipe, false);
	}

	// Token: 0x0600695B RID: 26971 RVA: 0x0027E2E8 File Offset: 0x0027C4E8
	public static MaterialSelectionPanel.SelectedElemInfo Filter(Tag _materialCategoryTag)
	{
		MaterialSelectionPanel.SelectedElemInfo selectedElemInfo = default(MaterialSelectionPanel.SelectedElemInfo);
		selectedElemInfo.element = null;
		selectedElemInfo.kgAvailable = 0f;
		if (DiscoveredResources.Instance == null || ElementLoader.elements == null || ElementLoader.elements.Count == 0)
		{
			return selectedElemInfo;
		}
		string[] array = _materialCategoryTag.ToString().Split('&', StringSplitOptions.None);
		for (int i = 0; i < array.Length; i++)
		{
			Tag tag = array[i];
			List<Tag> list = null;
			if (!MaterialSelectionPanel.elementsWithTag.TryGetValue(tag, out list))
			{
				list = new List<Tag>();
				foreach (Element element in ElementLoader.elements)
				{
					if (element.tag == tag || element.HasTag(tag))
					{
						list.Add(element.tag);
					}
				}
				foreach (Tag tag2 in GameTags.MaterialBuildingElements)
				{
					if (tag2 == tag)
					{
						foreach (GameObject gameObject in Assets.GetPrefabsWithTag(tag2))
						{
							KPrefabID component = gameObject.GetComponent<KPrefabID>();
							if (component != null && !list.Contains(component.PrefabTag))
							{
								list.Add(component.PrefabTag);
							}
						}
					}
				}
				MaterialSelectionPanel.elementsWithTag[tag] = list;
			}
			foreach (Tag tag3 in list)
			{
				float amount = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag3, true);
				if (amount > selectedElemInfo.kgAvailable)
				{
					selectedElemInfo.kgAvailable = amount;
					selectedElemInfo.element = tag3;
				}
			}
		}
		return selectedElemInfo;
	}

	// Token: 0x0600695C RID: 26972 RVA: 0x0027E51C File Offset: 0x0027C71C
	public void ToggleShowDescriptorPanels(bool show)
	{
		for (int i = 0; i < this.materialSelectors.Count; i++)
		{
			if (this.materialSelectors[i] != null)
			{
				this.materialSelectors[i].ToggleShowDescriptorsPanel(show);
			}
		}
	}

	// Token: 0x0600695D RID: 26973 RVA: 0x0027E565 File Offset: 0x0027C765
	private void OnPriorityClicked(PrioritySetting priority)
	{
		this.priorityScreen.SetScreenPriority(priority, false);
	}

	// Token: 0x0600695E RID: 26974 RVA: 0x0027E574 File Offset: 0x0027C774
	public void Render200ms(float dt)
	{
		this.UpdateResourceToggleValues();
	}

	// Token: 0x0400480D RID: 18445
	public Dictionary<KToggle, Tag> ElementToggles = new Dictionary<KToggle, Tag>();

	// Token: 0x0400480E RID: 18446
	private List<MaterialSelector> materialSelectors = new List<MaterialSelector>();

	// Token: 0x0400480F RID: 18447
	private List<Tag> currentSelectedElements = new List<Tag>();

	// Token: 0x04004810 RID: 18448
	[SerializeField]
	protected PriorityScreen priorityScreenPrefab;

	// Token: 0x04004811 RID: 18449
	[SerializeField]
	protected GameObject priorityScreenParent;

	// Token: 0x04004812 RID: 18450
	[SerializeField]
	protected BuildToolRotateButtonUI buildToolRotateButton;

	// Token: 0x04004813 RID: 18451
	private PriorityScreen priorityScreen;

	// Token: 0x04004814 RID: 18452
	public GameObject MaterialSelectorTemplate;

	// Token: 0x04004815 RID: 18453
	public GameObject ResearchRequired;

	// Token: 0x04004816 RID: 18454
	private Recipe activeRecipe;

	// Token: 0x04004817 RID: 18455
	private static Dictionary<Tag, List<Tag>> elementsWithTag = new Dictionary<Tag, List<Tag>>();

	// Token: 0x04004818 RID: 18456
	private MaterialSelectionPanel.GetBuildableStateDelegate GetBuildableState;

	// Token: 0x04004819 RID: 18457
	private MaterialSelectionPanel.GetBuildableTooltipDelegate GetBuildableTooltip;

	// Token: 0x0400481A RID: 18458
	private List<int> gameSubscriptionHandles = new List<int>();

	// Token: 0x02001F30 RID: 7984
	// (Invoke) Token: 0x0600B285 RID: 45701
	public delegate bool GetBuildableStateDelegate(BuildingDef def);

	// Token: 0x02001F31 RID: 7985
	// (Invoke) Token: 0x0600B289 RID: 45705
	public delegate string GetBuildableTooltipDelegate(BuildingDef def);

	// Token: 0x02001F32 RID: 7986
	// (Invoke) Token: 0x0600B28D RID: 45709
	public delegate void SelectElement(Element element, float kgAvailable, float recipe_amount);

	// Token: 0x02001F33 RID: 7987
	public struct SelectedElemInfo
	{
		// Token: 0x04009005 RID: 36869
		public Tag element;

		// Token: 0x04009006 RID: 36870
		public float kgAvailable;
	}
}
