using System;
using System.Collections.Generic;
using Klei;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D47 RID: 3399
public class MaterialSelector : KScreen
{
	// Token: 0x06006962 RID: 26978 RVA: 0x0027E5C4 File Offset: 0x0027C7C4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.toggleGroup = base.GetComponent<ToggleGroup>();
	}

	// Token: 0x06006963 RID: 26979 RVA: 0x0027E5D8 File Offset: 0x0027C7D8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06006964 RID: 26980 RVA: 0x0027E5EC File Offset: 0x0027C7EC
	public void ClearMaterialToggles()
	{
		this.CurrentSelectedElement = null;
		this.NoMaterialDiscovered.gameObject.SetActive(false);
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			keyValuePair.Value.gameObject.SetActive(false);
			Util.KDestroyGameObject(keyValuePair.Value.gameObject);
		}
		this.ElementToggles.Clear();
	}

	// Token: 0x06006965 RID: 26981 RVA: 0x0027E684 File Offset: 0x0027C884
	public static List<Tag> GetValidMaterials(Tag _materialTypeTag, bool omitDisabledElements = false)
	{
		string[] array = _materialTypeTag.ToString().Split('&', StringSplitOptions.None);
		List<Tag> list = new List<Tag>();
		for (int i = 0; i < array.Length; i++)
		{
			Tag tag = array[i];
			foreach (Element element in ElementLoader.elements)
			{
				if ((!element.disabled || !omitDisabledElements) && element.IsSolid && (element.tag == tag || element.HasTag(tag)))
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
		}
		return list;
	}

	// Token: 0x06006966 RID: 26982 RVA: 0x0027E7F4 File Offset: 0x0027C9F4
	public void ConfigureScreen(Recipe.Ingredient ingredient, Recipe recipe)
	{
		this.activeIngredient = ingredient;
		this.activeRecipe = recipe;
		this.activeMass = ingredient.amount;
		List<Tag> validMaterials = MaterialSelector.GetValidMaterials(ingredient.tag, false);
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (!validMaterials.Contains(keyValuePair.Key))
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (Tag tag in list)
		{
			this.ElementToggles[tag].gameObject.SetActive(false);
			Util.KDestroyGameObject(this.ElementToggles[tag].gameObject);
			this.ElementToggles.Remove(tag);
		}
		foreach (Tag tag2 in validMaterials)
		{
			if (!this.ElementToggles.ContainsKey(tag2))
			{
				GameObject gameObject = Util.KInstantiate(this.TogglePrefab, this.LayoutContainer, "MaterialSelection_" + tag2.ProperName());
				gameObject.transform.localScale = Vector3.one;
				gameObject.SetActive(true);
				KToggle component = gameObject.GetComponent<KToggle>();
				this.ElementToggles.Add(tag2, component);
				component.group = this.toggleGroup;
				gameObject.gameObject.GetComponent<ToolTip>().toolTip = tag2.ProperName();
			}
		}
		this.ConfigureMaterialTooltips();
		this.RefreshToggleContents();
	}

	// Token: 0x06006967 RID: 26983 RVA: 0x0027E9CC File Offset: 0x0027CBCC
	private void SetToggleBGImage(KToggle toggle, Tag elem)
	{
		if (toggle == this.selectedToggle)
		{
			toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimUIMaterial;
			toggle.GetComponent<ImageToggleState>().SetActive();
			return;
		}
		if (ClusterManager.Instance.activeWorld.worldInventory.GetAmount(elem, true) >= this.activeMass || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
		{
			toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimUIMaterial;
			toggle.GetComponentsInChildren<Image>()[1].color = Color.white;
			toggle.GetComponent<ImageToggleState>().SetInactive();
			return;
		}
		toggle.GetComponentsInChildren<Image>()[1].material = GlobalResources.Instance().AnimMaterialUIDesaturated;
		toggle.GetComponentsInChildren<Image>()[1].color = new Color(1f, 1f, 1f, 0.6f);
		if (!MaterialSelector.AllowInsufficientMaterialBuild())
		{
			toggle.GetComponent<ImageToggleState>().SetDisabled();
		}
	}

	// Token: 0x06006968 RID: 26984 RVA: 0x0027EAC0 File Offset: 0x0027CCC0
	public void OnSelectMaterial(Tag elem, Recipe recipe, bool focusScrollRect = false)
	{
		KToggle ktoggle = this.ElementToggles[elem];
		if (ktoggle != this.selectedToggle)
		{
			this.selectedToggle = ktoggle;
			if (recipe != null)
			{
				SaveGame.Instance.materialSelectorSerializer.SetSelectedElement(ClusterManager.Instance.activeWorldId, this.selectorIndex, recipe.Result, elem);
			}
			this.CurrentSelectedElement = elem;
			if (this.selectMaterialActions != null)
			{
				this.selectMaterialActions();
			}
			this.UpdateHeader();
			this.SetDescription(elem);
			this.SetEffects(elem);
			if (this.MaterialDescriptionPane != null)
			{
				if (!this.MaterialDescriptionPane.gameObject.activeSelf && !this.MaterialEffectsPane.gameObject.activeSelf)
				{
					this.DescriptorsPanel.SetActive(false);
				}
				else
				{
					this.DescriptorsPanel.SetActive(true);
				}
			}
		}
		if (focusScrollRect && this.ElementToggles.Count > 1)
		{
			List<Tag> list = new List<Tag>();
			foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
			{
				list.Add(keyValuePair.Key);
			}
			list.Sort(new Comparison<Tag>(this.ElementSorter));
			float num = (float)list.IndexOf(elem);
			int constraintCount = this.LayoutContainer.GetComponent<GridLayoutGroup>().constraintCount;
			float num2 = num / (float)constraintCount / (float)Math.Max((list.Count - 1) / constraintCount, 1);
			this.ScrollRect.normalizedPosition = new Vector2(0f, 1f - num2);
		}
		this.RefreshToggleContents();
	}

	// Token: 0x06006969 RID: 26985 RVA: 0x0027EC64 File Offset: 0x0027CE64
	public void RefreshToggleContents()
	{
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			KToggle value = keyValuePair.Value;
			Tag elem = keyValuePair.Key;
			GameObject gameObject = value.gameObject;
			bool flag = DiscoveredResources.Instance.IsDiscovered(elem) || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive;
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
			if (flag)
			{
				LocText[] componentsInChildren = gameObject.GetComponentsInChildren<LocText>();
				LocText locText = componentsInChildren[0];
				TMP_Text tmp_Text = componentsInChildren[1];
				Image image = gameObject.GetComponentsInChildren<Image>()[1];
				tmp_Text.text = Util.FormatWholeNumber(ClusterManager.Instance.activeWorld.worldInventory.GetAmount(elem, true));
				locText.text = Util.FormatWholeNumber(this.activeMass);
				GameObject gameObject2 = Assets.TryGetPrefab(keyValuePair.Key);
				if (gameObject2 != null)
				{
					KBatchedAnimController component = gameObject2.GetComponent<KBatchedAnimController>();
					image.sprite = Def.GetUISpriteFromMultiObjectAnim(component.AnimFiles[0], "ui", false, "");
				}
				this.SetToggleBGImage(keyValuePair.Value, keyValuePair.Key);
				value.soundPlayer.AcceptClickCondition = () => this.IsEnoughMass(elem);
				value.ClearOnClick();
				if (this.IsEnoughMass(elem))
				{
					value.onClick += delegate
					{
						this.OnSelectMaterial(elem, this.activeRecipe, false);
					};
				}
			}
		}
		this.SortElementToggles();
		this.UpdateHeader();
	}

	// Token: 0x0600696A RID: 26986 RVA: 0x0027EE20 File Offset: 0x0027D020
	private bool IsEnoughMass(Tag t)
	{
		return ClusterManager.Instance.activeWorld.worldInventory.GetAmount(t, true) >= this.activeMass || DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || MaterialSelector.AllowInsufficientMaterialBuild();
	}

	// Token: 0x0600696B RID: 26987 RVA: 0x0027EE5C File Offset: 0x0027D05C
	public bool AutoSelectAvailableMaterial()
	{
		if (this.activeRecipe == null || this.ElementToggles.Count == 0)
		{
			return false;
		}
		Tag previousElement = SaveGame.Instance.materialSelectorSerializer.GetPreviousElement(ClusterManager.Instance.activeWorldId, this.selectorIndex, this.activeRecipe.Result);
		if (previousElement != null)
		{
			KToggle ktoggle;
			this.ElementToggles.TryGetValue(previousElement, out ktoggle);
			if (ktoggle != null && (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || ClusterManager.Instance.activeWorld.worldInventory.GetAmount(previousElement, true) >= this.activeMass))
			{
				this.OnSelectMaterial(previousElement, this.activeRecipe, true);
				return true;
			}
		}
		float num = -1f;
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			list.Add(keyValuePair.Key);
		}
		list.Sort(new Comparison<Tag>(this.ElementSorter));
		if (DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive)
		{
			this.OnSelectMaterial(list[0], this.activeRecipe, true);
			return true;
		}
		Tag tag = null;
		foreach (Tag tag2 in list)
		{
			float num2 = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(tag2, true);
			if (MaterialSelector.DeprioritizeAutoSelectElementList.Contains(tag2))
			{
				num2 = Mathf.Min(this.activeMass, num2);
			}
			if (num2 >= this.activeMass && num2 > num)
			{
				num = num2;
				tag = tag2;
			}
		}
		if (tag != null)
		{
			UISounds.PlaySound(UISounds.Sound.Object_AutoSelected);
			Element element = ElementLoader.GetElement(tag);
			string text;
			if (element == null)
			{
				GameObject prefab = Assets.GetPrefab(tag);
				text = ((prefab != null) ? prefab.GetProperName() : tag.Name);
			}
			else
			{
				text = element.name;
			}
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, string.Format(MISC.POPFX.RESOURCE_SELECTION_CHANGED, text), null, Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()), 1.5f, false, false);
			this.OnSelectMaterial(tag, this.activeRecipe, true);
			return true;
		}
		return false;
	}

	// Token: 0x0600696C RID: 26988 RVA: 0x0027F0DC File Offset: 0x0027D2DC
	private void SortElementToggles()
	{
		bool flag = false;
		int num = -1;
		this.elementsToSort.Clear();
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				this.elementsToSort.Add(keyValuePair.Key);
			}
		}
		this.elementsToSort.Sort(new Comparison<Tag>(this.ElementSorter));
		for (int i = 0; i < this.elementsToSort.Count; i++)
		{
			int siblingIndex = this.ElementToggles[this.elementsToSort[i]].transform.GetSiblingIndex();
			if (siblingIndex <= num)
			{
				flag = true;
				break;
			}
			num = siblingIndex;
		}
		if (flag)
		{
			foreach (Tag tag in this.elementsToSort)
			{
				this.ElementToggles[tag].transform.SetAsLastSibling();
			}
		}
		this.UpdateScrollBar();
	}

	// Token: 0x0600696D RID: 26989 RVA: 0x0027F21C File Offset: 0x0027D41C
	private void ConfigureMaterialTooltips()
	{
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			ToolTip component = keyValuePair.Value.gameObject.GetComponent<ToolTip>();
			if (component != null)
			{
				component.toolTip = GameUtil.GetMaterialTooltips(keyValuePair.Key);
			}
		}
	}

	// Token: 0x0600696E RID: 26990 RVA: 0x0027F298 File Offset: 0x0027D498
	private void UpdateScrollBar()
	{
		if (this.Scrollbar == null)
		{
			return;
		}
		int num = 0;
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				num++;
			}
		}
		if (this.Scrollbar.activeSelf != num > 5)
		{
			this.Scrollbar.SetActive(num > 5);
		}
		this.ScrollRect.GetComponent<LayoutElement>().minHeight = (float)(74 * ((num <= 5) ? 1 : 2));
	}

	// Token: 0x0600696F RID: 26991 RVA: 0x0027F348 File Offset: 0x0027D548
	private void UpdateHeader()
	{
		if (this.activeIngredient == null)
		{
			return;
		}
		int num = 0;
		foreach (KeyValuePair<Tag, KToggle> keyValuePair in this.ElementToggles)
		{
			if (keyValuePair.Value.gameObject.activeSelf)
			{
				num++;
			}
		}
		LocText componentInChildren = this.Headerbar.GetComponentInChildren<LocText>();
		string[] array = this.activeIngredient.tag.ToString().Split('&', StringSplitOptions.None);
		string text = array[0].ToTag().ProperName();
		for (int i = 1; i < array.Length; i++)
		{
			text = text + " or " + array[i].ToTag().ProperName();
		}
		if (num == 0)
		{
			componentInChildren.text = string.Format(UI.PRODUCTINFO_MISSINGRESOURCES_TITLE, text, GameUtil.GetFormattedMass(this.activeIngredient.amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
			string text2 = string.Format(UI.PRODUCTINFO_MISSINGRESOURCES_DESC, text);
			this.NoMaterialDiscovered.text = text2;
			this.NoMaterialDiscovered.gameObject.SetActive(true);
			this.NoMaterialDiscovered.color = Constants.NEGATIVE_COLOR;
			this.BadBG.SetActive(true);
			if (this.Scrollbar != null)
			{
				this.Scrollbar.SetActive(false);
			}
			this.LayoutContainer.SetActive(false);
			return;
		}
		componentInChildren.text = string.Format(UI.PRODUCTINFO_SELECTMATERIAL, text);
		this.NoMaterialDiscovered.gameObject.SetActive(false);
		this.BadBG.SetActive(false);
		this.LayoutContainer.SetActive(true);
		this.UpdateScrollBar();
	}

	// Token: 0x06006970 RID: 26992 RVA: 0x0027F50C File Offset: 0x0027D70C
	public void ToggleShowDescriptorsPanel(bool show)
	{
		if (this.DescriptorsPanel == null)
		{
			return;
		}
		this.DescriptorsPanel.gameObject.SetActive(show);
	}

	// Token: 0x06006971 RID: 26993 RVA: 0x0027F530 File Offset: 0x0027D730
	private void SetDescription(Tag element)
	{
		if (this.DescriptorsPanel == null)
		{
			return;
		}
		StringEntry stringEntry = null;
		if (Strings.TryGet(new StringKey("STRINGS.ELEMENTS." + element.ToString().ToUpper() + ".BUILD_DESC"), out stringEntry))
		{
			this.MaterialDescriptionText.text = stringEntry.ToString();
			this.MaterialDescriptionPane.SetActive(true);
			return;
		}
		this.MaterialDescriptionPane.SetActive(false);
	}

	// Token: 0x06006972 RID: 26994 RVA: 0x0027F5A8 File Offset: 0x0027D7A8
	private void SetEffects(Tag element)
	{
		if (this.MaterialDescriptionPane == null)
		{
			return;
		}
		List<Descriptor> materialDescriptors = GameUtil.GetMaterialDescriptors(element);
		if (materialDescriptors.Count > 0)
		{
			Descriptor descriptor = default(Descriptor);
			descriptor.SetupDescriptor(ELEMENTS.MATERIAL_MODIFIERS.EFFECTS_HEADER, ELEMENTS.MATERIAL_MODIFIERS.TOOLTIP.EFFECTS_HEADER, Descriptor.DescriptorType.Effect);
			materialDescriptors.Insert(0, descriptor);
			this.MaterialEffectsPane.gameObject.SetActive(true);
			this.MaterialEffectsPane.SetDescriptors(materialDescriptors);
			return;
		}
		this.MaterialEffectsPane.gameObject.SetActive(false);
	}

	// Token: 0x06006973 RID: 26995 RVA: 0x0027F62F File Offset: 0x0027D82F
	public static bool AllowInsufficientMaterialBuild()
	{
		return GenericGameSettings.instance.allowInsufficientMaterialBuild;
	}

	// Token: 0x06006974 RID: 26996 RVA: 0x0027F63C File Offset: 0x0027D83C
	private int ElementSorter(Tag at, Tag bt)
	{
		GameObject gameObject = Assets.TryGetPrefab(at);
		IHasSortOrder hasSortOrder = ((gameObject != null) ? gameObject.GetComponent<IHasSortOrder>() : null);
		GameObject gameObject2 = Assets.TryGetPrefab(bt);
		IHasSortOrder hasSortOrder2 = ((gameObject2 != null) ? gameObject2.GetComponent<IHasSortOrder>() : null);
		if (hasSortOrder == null || hasSortOrder2 == null)
		{
			return 0;
		}
		Element element = ElementLoader.GetElement(at);
		Element element2 = ElementLoader.GetElement(bt);
		if (element != null && element2 != null && element.buildMenuSort == element2.buildMenuSort)
		{
			return element.idx.CompareTo(element2.idx);
		}
		return hasSortOrder.sortOrder.CompareTo(hasSortOrder2.sortOrder);
	}

	// Token: 0x0400481B RID: 18459
	public static List<Tag> DeprioritizeAutoSelectElementList = new List<Tag>
	{
		SimHashes.WoodLog.ToString().ToTag(),
		SimHashes.SolidMercury.ToString().ToTag(),
		SimHashes.Lead.ToString().ToTag()
	};

	// Token: 0x0400481C RID: 18460
	public Tag CurrentSelectedElement;

	// Token: 0x0400481D RID: 18461
	public Dictionary<Tag, KToggle> ElementToggles = new Dictionary<Tag, KToggle>();

	// Token: 0x0400481E RID: 18462
	public int selectorIndex;

	// Token: 0x0400481F RID: 18463
	public MaterialSelector.SelectMaterialActions selectMaterialActions;

	// Token: 0x04004820 RID: 18464
	public MaterialSelector.SelectMaterialActions deselectMaterialActions;

	// Token: 0x04004821 RID: 18465
	private ToggleGroup toggleGroup;

	// Token: 0x04004822 RID: 18466
	public GameObject TogglePrefab;

	// Token: 0x04004823 RID: 18467
	public GameObject LayoutContainer;

	// Token: 0x04004824 RID: 18468
	public KScrollRect ScrollRect;

	// Token: 0x04004825 RID: 18469
	public GameObject Scrollbar;

	// Token: 0x04004826 RID: 18470
	public GameObject Headerbar;

	// Token: 0x04004827 RID: 18471
	public GameObject BadBG;

	// Token: 0x04004828 RID: 18472
	public LocText NoMaterialDiscovered;

	// Token: 0x04004829 RID: 18473
	public GameObject MaterialDescriptionPane;

	// Token: 0x0400482A RID: 18474
	public LocText MaterialDescriptionText;

	// Token: 0x0400482B RID: 18475
	public DescriptorPanel MaterialEffectsPane;

	// Token: 0x0400482C RID: 18476
	public GameObject DescriptorsPanel;

	// Token: 0x0400482D RID: 18477
	private KToggle selectedToggle;

	// Token: 0x0400482E RID: 18478
	private Recipe.Ingredient activeIngredient;

	// Token: 0x0400482F RID: 18479
	private Recipe activeRecipe;

	// Token: 0x04004830 RID: 18480
	private float activeMass;

	// Token: 0x04004831 RID: 18481
	private List<Tag> elementsToSort = new List<Tag>();

	// Token: 0x02001F36 RID: 7990
	// (Invoke) Token: 0x0600B299 RID: 45721
	public delegate void SelectMaterialActions();
}
