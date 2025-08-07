using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CCA RID: 3274
public class FacadeSelectionPanel : KMonoBehaviour
{
	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x060064E5 RID: 25829 RVA: 0x0025F236 File Offset: 0x0025D436
	private int GridLayoutConstraintCount
	{
		get
		{
			if (this.gridLayout != null)
			{
				return this.gridLayout.constraintCount;
			}
			return 3;
		}
	}

	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x060064E7 RID: 25831 RVA: 0x0025F262 File Offset: 0x0025D462
	// (set) Token: 0x060064E6 RID: 25830 RVA: 0x0025F253 File Offset: 0x0025D453
	public ClothingOutfitUtility.OutfitType SelectedOutfitCategory
	{
		get
		{
			return this.selectedOutfitCategory;
		}
		set
		{
			this.selectedOutfitCategory = value;
			this.Refresh();
		}
	}

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x060064E8 RID: 25832 RVA: 0x0025F26A File Offset: 0x0025D46A
	public string SelectedBuildingDefID
	{
		get
		{
			return this.selectedBuildingDefID;
		}
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x060064E9 RID: 25833 RVA: 0x0025F272 File Offset: 0x0025D472
	// (set) Token: 0x060064EA RID: 25834 RVA: 0x0025F27C File Offset: 0x0025D47C
	public string SelectedFacade
	{
		get
		{
			return this._selectedFacade;
		}
		set
		{
			if (this._selectedFacade != value)
			{
				this._selectedFacade = value;
				FacadeSelectionPanel.ConfigType configType = this.currentConfigType;
				if (configType != FacadeSelectionPanel.ConfigType.BuildingFacade)
				{
					if (configType == FacadeSelectionPanel.ConfigType.MinionOutfit)
					{
						this.RefreshTogglesForOutfit(this.selectedOutfitCategory);
					}
				}
				else
				{
					this.RefreshTogglesForBuilding();
				}
				if (this.OnFacadeSelectionChanged != null)
				{
					this.OnFacadeSelectionChanged();
				}
			}
		}
	}

	// Token: 0x060064EB RID: 25835 RVA: 0x0025F2D5 File Offset: 0x0025D4D5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.gridLayout = this.toggleContainer.GetComponent<GridLayoutGroup>();
	}

	// Token: 0x060064EC RID: 25836 RVA: 0x0025F2EE File Offset: 0x0025D4EE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.getMoreButton.ClearOnClick();
		this.getMoreButton.onClick += LockerMenuScreen.Instance.ShowInventoryScreen;
	}

	// Token: 0x060064ED RID: 25837 RVA: 0x0025F31C File Offset: 0x0025D51C
	public void SetBuildingDef(string defID, string currentFacadeID = null)
	{
		this.currentConfigType = FacadeSelectionPanel.ConfigType.BuildingFacade;
		this.ClearToggles();
		this.selectedBuildingDefID = defID;
		this.SelectedFacade = ((currentFacadeID == null) ? "DEFAULT_FACADE" : currentFacadeID);
		this.RefreshTogglesForBuilding();
		if (this.hideWhenEmpty)
		{
			base.gameObject.SetActive(Assets.GetBuildingDef(defID).AvailableFacades.Count != 0);
		}
	}

	// Token: 0x060064EE RID: 25838 RVA: 0x0025F37A File Offset: 0x0025D57A
	public void SetOutfitTarget(ClothingOutfitTarget outfitTarget, ClothingOutfitUtility.OutfitType outfitType)
	{
		this.currentConfigType = FacadeSelectionPanel.ConfigType.MinionOutfit;
		this.ClearToggles();
		this.SelectedFacade = outfitTarget.OutfitId;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060064EF RID: 25839 RVA: 0x0025F3A4 File Offset: 0x0025D5A4
	private void ClearToggles()
	{
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair in this.activeFacadeToggles)
		{
			this.pooledFacadeToggles.Add(keyValuePair.Value.gameObject);
			keyValuePair.Value.gameObject.SetActive(false);
		}
		this.activeFacadeToggles.Clear();
	}

	// Token: 0x060064F0 RID: 25840 RVA: 0x0025F42C File Offset: 0x0025D62C
	public void Refresh()
	{
		FacadeSelectionPanel.ConfigType configType = this.currentConfigType;
		if (configType != FacadeSelectionPanel.ConfigType.BuildingFacade)
		{
			if (configType == FacadeSelectionPanel.ConfigType.MinionOutfit)
			{
				this.RefreshTogglesForOutfit(this.selectedOutfitCategory);
			}
		}
		else
		{
			this.RefreshTogglesForBuilding();
		}
		this.getMoreButton.gameObject.SetActive(this.showGetMoreButton);
		if (this.useDummyPlaceholder)
		{
			for (int i = 0; i < this.dummyGridPlaceholders.Count; i++)
			{
				this.dummyGridPlaceholders[i].SetActive(false);
			}
			int num = 0;
			for (int j = 0; j < this.toggleContainer.transform.childCount; j++)
			{
				if (this.toggleContainer.GetChild(j).gameObject.activeInHierarchy)
				{
					num++;
				}
			}
			this.getMoreButton.transform.SetAsLastSibling();
			if (num % this.GridLayoutConstraintCount != 0)
			{
				for (int k = 0; k < this.GridLayoutConstraintCount - 1; k++)
				{
					this.dummyGridPlaceholders[k].SetActive(k < this.GridLayoutConstraintCount - num % this.GridLayoutConstraintCount);
					this.dummyGridPlaceholders[k].transform.SetAsLastSibling();
				}
				return;
			}
		}
		else
		{
			this.getMoreButton.transform.SetAsLastSibling();
		}
	}

	// Token: 0x060064F1 RID: 25841 RVA: 0x0025F55C File Offset: 0x0025D75C
	private void RefreshTogglesForOutfit(ClothingOutfitUtility.OutfitType outfitType)
	{
		IEnumerable<ClothingOutfitTarget> enumerable = from outfit in ClothingOutfitTarget.GetAllTemplates()
			where outfit.OutfitType == outfitType
			select outfit;
		List<string> list = new List<string>();
		using (Dictionary<string, FacadeSelectionPanel.FacadeToggle>.Enumerator enumerator = this.activeFacadeToggles.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> toggle = enumerator.Current;
				if (!enumerable.Any((ClothingOutfitTarget match) => match.OutfitId == toggle.Key))
				{
					list.Add(toggle.Key);
				}
			}
		}
		foreach (string text in list)
		{
			this.pooledFacadeToggles.Add(this.activeFacadeToggles[text].gameObject);
			this.activeFacadeToggles[text].gameObject.SetActive(false);
			this.activeFacadeToggles.Remove(text);
		}
		list.Clear();
		this.AddDefaultOutfitToggle();
		enumerable = enumerable.StableSort((ClothingOutfitTarget a, ClothingOutfitTarget b) => a.OutfitId.CompareTo(b.OutfitId));
		foreach (ClothingOutfitTarget clothingOutfitTarget in enumerable)
		{
			if (!clothingOutfitTarget.DoesContainLockedItems())
			{
				this.AddNewOutfitToggle(clothingOutfitTarget.OutfitId, false);
			}
		}
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair in this.activeFacadeToggles)
		{
			keyValuePair.Value.multiToggle.ChangeState((this.SelectedFacade != null && this.SelectedFacade == keyValuePair.Key) ? 1 : 0);
		}
		this.RefreshHeight();
	}

	// Token: 0x060064F2 RID: 25842 RVA: 0x0025F780 File Offset: 0x0025D980
	private void RefreshTogglesForBuilding()
	{
		BuildingDef buildingDef = Assets.GetBuildingDef(this.selectedBuildingDefID);
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair in this.activeFacadeToggles)
		{
			if (!buildingDef.AvailableFacades.Contains(keyValuePair.Key))
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (string text in list)
		{
			this.pooledFacadeToggles.Add(this.activeFacadeToggles[text].gameObject);
			this.activeFacadeToggles[text].gameObject.SetActive(false);
			this.activeFacadeToggles.Remove(text);
		}
		list.Clear();
		this.AddDefaultBuildingFacadeToggle();
		foreach (string text2 in buildingDef.AvailableFacades)
		{
			PermitResource permitResource = Db.Get().Permits.TryGet(text2);
			if (permitResource != null && permitResource.IsUnlocked())
			{
				this.AddNewBuildingToggle(text2);
			}
		}
		foreach (KeyValuePair<string, FacadeSelectionPanel.FacadeToggle> keyValuePair2 in this.activeFacadeToggles)
		{
			keyValuePair2.Value.multiToggle.ChangeState((this.SelectedFacade == keyValuePair2.Key) ? 1 : 0);
		}
		this.activeFacadeToggles["DEFAULT_FACADE"].gameObject.transform.SetAsFirstSibling();
		this.RefreshHeight();
	}

	// Token: 0x060064F3 RID: 25843 RVA: 0x0025F988 File Offset: 0x0025DB88
	private void RefreshHeight()
	{
		if (this.usesScrollRect)
		{
			LayoutElement component = this.scrollRect.GetComponent<LayoutElement>();
			component.minHeight = (float)(58 * ((this.activeFacadeToggles.Count <= 5) ? 1 : 2));
			component.preferredHeight = component.minHeight;
		}
	}

	// Token: 0x060064F4 RID: 25844 RVA: 0x0025F9C4 File Offset: 0x0025DBC4
	private void AddDefaultBuildingFacadeToggle()
	{
		this.AddNewBuildingToggle("DEFAULT_FACADE");
	}

	// Token: 0x060064F5 RID: 25845 RVA: 0x0025F9D1 File Offset: 0x0025DBD1
	private void AddDefaultOutfitToggle()
	{
		this.AddNewOutfitToggle("DEFAULT_FACADE", true);
	}

	// Token: 0x060064F6 RID: 25846 RVA: 0x0025F9E0 File Offset: 0x0025DBE0
	private void AddNewBuildingToggle(string facadeID)
	{
		if (this.activeFacadeToggles.ContainsKey(facadeID))
		{
			return;
		}
		GameObject gameObject;
		if (this.pooledFacadeToggles.Count > 0)
		{
			gameObject = this.pooledFacadeToggles[0];
			this.pooledFacadeToggles.RemoveAt(0);
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.togglePrefab, this.toggleContainer.gameObject, false);
		}
		FacadeSelectionPanel.FacadeToggle newToggle = new FacadeSelectionPanel.FacadeToggle(facadeID, this.selectedBuildingDefID, gameObject);
		MultiToggle multiToggle = newToggle.multiToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.SelectFacade(newToggle.id);
		}));
		this.activeFacadeToggles.Add(newToggle.id, newToggle);
	}

	// Token: 0x060064F7 RID: 25847 RVA: 0x0025FAA8 File Offset: 0x0025DCA8
	private void AddNewOutfitToggle(string outfitID, bool setAsFirstSibling = false)
	{
		if (this.activeFacadeToggles.ContainsKey(outfitID))
		{
			if (setAsFirstSibling)
			{
				this.activeFacadeToggles[outfitID].gameObject.transform.SetAsFirstSibling();
			}
			return;
		}
		GameObject gameObject;
		if (this.pooledFacadeToggles.Count > 0)
		{
			gameObject = this.pooledFacadeToggles[0];
			this.pooledFacadeToggles.RemoveAt(0);
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.togglePrefab, this.toggleContainer.gameObject, false);
		}
		FacadeSelectionPanel.FacadeToggle newToggle = new FacadeSelectionPanel.FacadeToggle(outfitID, gameObject, this.selectedOutfitCategory);
		MultiToggle multiToggle = newToggle.multiToggle;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.SelectFacade(newToggle.id);
		}));
		this.activeFacadeToggles.Add(newToggle.id, newToggle);
		if (setAsFirstSibling)
		{
			this.activeFacadeToggles[outfitID].gameObject.transform.SetAsFirstSibling();
		}
	}

	// Token: 0x060064F8 RID: 25848 RVA: 0x0025FBB1 File Offset: 0x0025DDB1
	private void SelectFacade(string id)
	{
		this.SelectedFacade = id;
	}

	// Token: 0x040044EB RID: 17643
	[SerializeField]
	private GameObject togglePrefab;

	// Token: 0x040044EC RID: 17644
	[SerializeField]
	private RectTransform toggleContainer;

	// Token: 0x040044ED RID: 17645
	[SerializeField]
	private bool usesScrollRect;

	// Token: 0x040044EE RID: 17646
	[SerializeField]
	private LayoutElement scrollRect;

	// Token: 0x040044EF RID: 17647
	private Dictionary<string, FacadeSelectionPanel.FacadeToggle> activeFacadeToggles = new Dictionary<string, FacadeSelectionPanel.FacadeToggle>();

	// Token: 0x040044F0 RID: 17648
	private List<GameObject> pooledFacadeToggles = new List<GameObject>();

	// Token: 0x040044F1 RID: 17649
	[SerializeField]
	private KButton getMoreButton;

	// Token: 0x040044F2 RID: 17650
	[SerializeField]
	private bool showGetMoreButton;

	// Token: 0x040044F3 RID: 17651
	[SerializeField]
	private bool hideWhenEmpty = true;

	// Token: 0x040044F4 RID: 17652
	[SerializeField]
	private bool useDummyPlaceholder;

	// Token: 0x040044F5 RID: 17653
	private GridLayoutGroup gridLayout;

	// Token: 0x040044F6 RID: 17654
	[SerializeField]
	private List<GameObject> dummyGridPlaceholders;

	// Token: 0x040044F7 RID: 17655
	public global::System.Action OnFacadeSelectionChanged;

	// Token: 0x040044F8 RID: 17656
	private ClothingOutfitUtility.OutfitType selectedOutfitCategory;

	// Token: 0x040044F9 RID: 17657
	private string selectedBuildingDefID;

	// Token: 0x040044FA RID: 17658
	private FacadeSelectionPanel.ConfigType currentConfigType;

	// Token: 0x040044FB RID: 17659
	private string _selectedFacade;

	// Token: 0x040044FC RID: 17660
	public const string DEFAULT_FACADE_ID = "DEFAULT_FACADE";

	// Token: 0x02001E93 RID: 7827
	private struct FacadeToggle
	{
		// Token: 0x0600B0D2 RID: 45266 RVA: 0x003D359C File Offset: 0x003D179C
		public FacadeToggle(string buildingFacadeID, string buildingPrefabID, GameObject gameObject)
		{
			this.id = buildingFacadeID;
			this.gameObject = gameObject;
			gameObject.SetActive(true);
			this.multiToggle = gameObject.GetComponent<MultiToggle>();
			this.multiToggle.onClick = null;
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			component.GetReference<UIMannequin>("Mannequin").gameObject.SetActive(false);
			component.GetReference<Image>("FGImage").SetAlpha(1f);
			Sprite sprite;
			string text;
			string text2;
			if (buildingFacadeID != "DEFAULT_FACADE")
			{
				BuildingFacadeResource buildingFacadeResource = Db.GetBuildingFacades().Get(buildingFacadeID);
				sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(buildingFacadeResource.AnimFile), "ui", false, "");
				text = KleiItemsUI.GetTooltipStringFor(buildingFacadeResource);
				text2 = buildingFacadeResource.GetDlcIdFrom();
			}
			else
			{
				GameObject prefab = Assets.GetPrefab(buildingPrefabID);
				Building component2 = prefab.GetComponent<Building>();
				StringEntry stringEntry;
				string text3;
				if (Strings.TryGet(string.Concat(new string[]
				{
					"STRINGS.BUILDINGS.PREFABS.",
					buildingPrefabID.ToUpperInvariant(),
					".FACADES.DEFAULT_",
					buildingPrefabID.ToUpperInvariant(),
					".NAME"
				}), out stringEntry))
				{
					text3 = stringEntry;
				}
				else if (component2 != null)
				{
					text3 = component2.Def.Name;
				}
				else
				{
					text3 = prefab.GetProperName();
				}
				StringEntry stringEntry2;
				string text4;
				if (Strings.TryGet(string.Concat(new string[]
				{
					"STRINGS.BUILDINGS.PREFABS.",
					buildingPrefabID.ToUpperInvariant(),
					".FACADES.DEFAULT_",
					buildingPrefabID.ToUpperInvariant(),
					".DESC"
				}), out stringEntry2))
				{
					text4 = stringEntry2;
				}
				else if (component2 != null)
				{
					text4 = component2.Def.Desc;
				}
				else
				{
					text4 = "";
				}
				sprite = Def.GetUISprite(buildingPrefabID, "ui", false).first;
				text = KleiItemsUI.WrapAsToolTipTitle(text3) + "\n" + text4;
				text2 = null;
			}
			component.GetReference<Image>("FGImage").sprite = sprite;
			this.gameObject.GetComponent<ToolTip>().SetSimpleTooltip(text);
			Image reference = component.GetReference<Image>("DlcBanner");
			if (DlcManager.IsDlcId(text2))
			{
				reference.gameObject.SetActive(true);
				reference.color = DlcManager.GetDlcBannerColor(text2);
				return;
			}
			reference.gameObject.SetActive(false);
		}

		// Token: 0x0600B0D3 RID: 45267 RVA: 0x003D37C8 File Offset: 0x003D19C8
		public FacadeToggle(string outfitID, GameObject gameObject, ClothingOutfitUtility.OutfitType outfitType)
		{
			this.id = outfitID;
			this.gameObject = gameObject;
			gameObject.SetActive(true);
			this.multiToggle = gameObject.GetComponent<MultiToggle>();
			this.multiToggle.onClick = null;
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			UIMannequin reference = component.GetReference<UIMannequin>("Mannequin");
			reference.gameObject.SetActive(true);
			component.GetReference<Image>("FGImage").SetAlpha(0f);
			ToolTip component2 = this.gameObject.GetComponent<ToolTip>();
			component2.SetSimpleTooltip("");
			if (outfitID != "DEFAULT_FACADE")
			{
				ClothingOutfitTarget clothingOutfitTarget = ClothingOutfitTarget.FromTemplateId(outfitID);
				component.GetReference<UIMannequin>("Mannequin").SetOutfit(clothingOutfitTarget);
				component2.SetSimpleTooltip(GameUtil.ApplyBoldString(clothingOutfitTarget.ReadName()));
			}
			else
			{
				component.GetReference<UIMannequin>("Mannequin").ClearOutfit(outfitType);
				component2.SetSimpleTooltip(GameUtil.ApplyBoldString(UI.OUTFIT_NAME.NONE));
			}
			string text = null;
			if (outfitID != "DEFAULT_FACADE")
			{
				ClothingOutfitTarget.Implementation impl = ClothingOutfitTarget.FromTemplateId(outfitID).impl;
				if (impl is ClothingOutfitTarget.DatabaseAuthoredTemplate)
				{
					ClothingOutfitTarget.DatabaseAuthoredTemplate databaseAuthoredTemplate = (ClothingOutfitTarget.DatabaseAuthoredTemplate)impl;
					text = databaseAuthoredTemplate.resource.GetDlcIdFrom();
				}
			}
			Image reference2 = component.GetReference<Image>("DlcBanner");
			if (DlcManager.IsDlcId(text))
			{
				reference2.gameObject.SetActive(true);
				reference2.color = DlcManager.GetDlcBannerColor(text);
			}
			else
			{
				reference2.gameObject.SetActive(false);
			}
			Vector2 vector = new Vector2(0f, 0f);
			if (outfitType == ClothingOutfitUtility.OutfitType.AtmoSuit)
			{
				vector = new Vector2(-16f, -16f);
			}
			reference.rectTransform().sizeDelta = vector;
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x0600B0D4 RID: 45268 RVA: 0x003D395A File Offset: 0x003D1B5A
		// (set) Token: 0x0600B0D5 RID: 45269 RVA: 0x003D3962 File Offset: 0x003D1B62
		public string id { readonly get; set; }

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x0600B0D6 RID: 45270 RVA: 0x003D396B File Offset: 0x003D1B6B
		// (set) Token: 0x0600B0D7 RID: 45271 RVA: 0x003D3973 File Offset: 0x003D1B73
		public GameObject gameObject { readonly get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x0600B0D8 RID: 45272 RVA: 0x003D397C File Offset: 0x003D1B7C
		// (set) Token: 0x0600B0D9 RID: 45273 RVA: 0x003D3984 File Offset: 0x003D1B84
		public MultiToggle multiToggle { readonly get; set; }
	}

	// Token: 0x02001E94 RID: 7828
	private enum ConfigType
	{
		// Token: 0x04008DFB RID: 36347
		BuildingFacade,
		// Token: 0x04008DFC RID: 36348
		MinionOutfit
	}
}
