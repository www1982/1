using System;
using System.Collections.Generic;
using STRINGS;
using TMPro;
using TUNING;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E44 RID: 3652
public class TreeFilterableSideScreen : SideScreenContent
{
	// Token: 0x170007FC RID: 2044
	// (get) Token: 0x060073D2 RID: 29650 RVA: 0x002C001C File Offset: 0x002BE21C
	private bool InputFieldEmpty
	{
		get
		{
			return this.inputField.text == "";
		}
	}

	// Token: 0x170007FD RID: 2045
	// (get) Token: 0x060073D3 RID: 29651 RVA: 0x002C0033 File Offset: 0x002BE233
	public bool IsStorage
	{
		get
		{
			return this.storage != null;
		}
	}

	// Token: 0x060073D4 RID: 29652 RVA: 0x002C0041 File Offset: 0x002BE241
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.Initialize();
	}

	// Token: 0x060073D5 RID: 29653 RVA: 0x002C0050 File Offset: 0x002BE250
	private void Initialize()
	{
		if (this.initialized)
		{
			return;
		}
		this.rowPool = new UIPool<TreeFilterableSideScreenRow>(this.rowPrefab);
		this.elementPool = new UIPool<TreeFilterableSideScreenElement>(this.elementPrefab);
		MultiToggle multiToggle = this.allCheckBox;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			TreeFilterableSideScreenRow.State allCheckboxState = this.GetAllCheckboxState();
			if (allCheckboxState > TreeFilterableSideScreenRow.State.Mixed)
			{
				if (allCheckboxState == TreeFilterableSideScreenRow.State.On)
				{
					this.SetAllCheckboxState(TreeFilterableSideScreenRow.State.Off);
					return;
				}
			}
			else
			{
				this.SetAllCheckboxState(TreeFilterableSideScreenRow.State.On);
			}
		}));
		this.onlyAllowTransportItemsCheckBox.onClick = new global::System.Action(this.OnlyAllowTransportItemsClicked);
		this.onlyAllowSpicedItemsCheckBox.onClick = new global::System.Action(this.OnlyAllowSpicedItemsClicked);
		this.initialized = true;
	}

	// Token: 0x060073D6 RID: 29654 RVA: 0x002C00E4 File Offset: 0x002BE2E4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.allCheckBox.transform.parent.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ALLBUTTONTOOLTIP);
		this.onlyAllowTransportItemsCheckBox.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ONLYALLOWTRANSPORTITEMSBUTTONTOOLTIP);
		this.onlyAllowSpicedItemsCheckBox.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ONLYALLOWSPICEDITEMSBUTTONTOOLTIP);
		this.inputField.ActivateInputField();
		this.inputField.placeholder.GetComponent<TextMeshProUGUI>().text = UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.SEARCH_PLACEHOLDER;
		this.InitSearch();
	}

	// Token: 0x060073D7 RID: 29655 RVA: 0x002C0198 File Offset: 0x002BE398
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return base.GetSortKey();
	}

	// Token: 0x060073D8 RID: 29656 RVA: 0x002C01AE File Offset: 0x002BE3AE
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x060073D9 RID: 29657 RVA: 0x002C01C8 File Offset: 0x002BE3C8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x060073DA RID: 29658 RVA: 0x002C01E2 File Offset: 0x002BE3E2
	public override int GetSideScreenSortOrder()
	{
		return 1;
	}

	// Token: 0x060073DB RID: 29659 RVA: 0x002C01E8 File Offset: 0x002BE3E8
	private void UpdateAllCheckBoxVisualState()
	{
		switch (this.GetAllCheckboxState())
		{
		case TreeFilterableSideScreenRow.State.Off:
			this.allCheckBox.ChangeState(0);
			return;
		case TreeFilterableSideScreenRow.State.Mixed:
			this.allCheckBox.ChangeState(1);
			return;
		case TreeFilterableSideScreenRow.State.On:
			this.allCheckBox.ChangeState(2);
			return;
		default:
			return;
		}
	}

	// Token: 0x060073DC RID: 29660 RVA: 0x002C0238 File Offset: 0x002BE438
	public void Update()
	{
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (keyValuePair.Value.visualDirty)
			{
				this.visualDirty = true;
				break;
			}
		}
		if (this.visualDirty)
		{
			foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair2 in this.tagRowMap)
			{
				keyValuePair2.Value.RefreshRowElements();
				keyValuePair2.Value.UpdateCheckBoxVisualState();
			}
			this.UpdateAllCheckBoxVisualState();
			this.visualDirty = false;
		}
	}

	// Token: 0x060073DD RID: 29661 RVA: 0x002C0304 File Offset: 0x002BE504
	private void OnlyAllowTransportItemsClicked()
	{
		this.storage.SetOnlyFetchMarkedItems(!this.storage.GetOnlyFetchMarkedItems());
	}

	// Token: 0x060073DE RID: 29662 RVA: 0x002C031F File Offset: 0x002BE51F
	private void OnlyAllowSpicedItemsClicked()
	{
		FoodStorage component = this.storage.GetComponent<FoodStorage>();
		component.SpicedFoodOnly = !component.SpicedFoodOnly;
	}

	// Token: 0x060073DF RID: 29663 RVA: 0x002C033C File Offset: 0x002BE53C
	private TreeFilterableSideScreenRow.State GetAllCheckboxState()
	{
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (keyValuePair.Value.standardCommodity)
			{
				switch (keyValuePair.Value.GetState())
				{
				case TreeFilterableSideScreenRow.State.Off:
					flag2 = true;
					break;
				case TreeFilterableSideScreenRow.State.Mixed:
					flag3 = true;
					break;
				case TreeFilterableSideScreenRow.State.On:
					flag = true;
					break;
				}
			}
		}
		if (flag3)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		if (flag && !flag2)
		{
			return TreeFilterableSideScreenRow.State.On;
		}
		if (!flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Off;
		}
		if (flag && flag2)
		{
			return TreeFilterableSideScreenRow.State.Mixed;
		}
		return TreeFilterableSideScreenRow.State.Off;
	}

	// Token: 0x060073E0 RID: 29664 RVA: 0x002C03EC File Offset: 0x002BE5EC
	private void SetAllCheckboxState(TreeFilterableSideScreenRow.State newState)
	{
		switch (newState)
		{
		case TreeFilterableSideScreenRow.State.Off:
		{
			using (Dictionary<Tag, TreeFilterableSideScreenRow>.Enumerator enumerator = this.tagRowMap.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair = enumerator.Current;
					if (keyValuePair.Value.standardCommodity)
					{
						keyValuePair.Value.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.Off);
					}
				}
				goto IL_00AB;
			}
			break;
		}
		case TreeFilterableSideScreenRow.State.Mixed:
			goto IL_00AB;
		case TreeFilterableSideScreenRow.State.On:
			break;
		default:
			goto IL_00AB;
		}
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair2 in this.tagRowMap)
		{
			if (keyValuePair2.Value.standardCommodity)
			{
				keyValuePair2.Value.ChangeCheckBoxState(TreeFilterableSideScreenRow.State.On);
			}
		}
		IL_00AB:
		this.visualDirty = true;
	}

	// Token: 0x060073E1 RID: 29665 RVA: 0x002C04C8 File Offset: 0x002BE6C8
	public bool GetElementTagAcceptedState(Tag t)
	{
		return this.targetFilterable.ContainsTag(t);
	}

	// Token: 0x060073E2 RID: 29666 RVA: 0x002C04D8 File Offset: 0x002BE6D8
	public override bool IsValidForTarget(GameObject target)
	{
		TreeFilterable component = target.GetComponent<TreeFilterable>();
		Storage component2 = target.GetComponent<Storage>();
		return component != null && target.GetComponent<FlatTagFilterable>() == null && component.showUserMenu && (component2 == null || component2.showInUI) && target.GetSMI<StorageTile.Instance>() == null;
	}

	// Token: 0x060073E3 RID: 29667 RVA: 0x002C052E File Offset: 0x002BE72E
	private void ReconfigureForPreviousTarget()
	{
		global::Debug.Assert(this.target != null, "TreeFilterableSideScreen trying to restore null target.");
		this.SetTarget(this.target);
	}

	// Token: 0x060073E4 RID: 29668 RVA: 0x002C0554 File Offset: 0x002BE754
	public override void SetTarget(GameObject target)
	{
		this.Initialize();
		this.target = target;
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetFilterable = target.GetComponent<TreeFilterable>();
		if (this.targetFilterable == null)
		{
			global::Debug.LogError("The target provided does not have a Tree Filterable component");
			return;
		}
		this.contentMask.GetComponent<LayoutElement>().minHeight = (float)((this.targetFilterable.uiHeight == TreeFilterable.UISideScreenHeight.Tall) ? 380 : 256);
		this.storage = this.targetFilterable.GetFilterStorage();
		this.storage.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.storage.Subscribe(1163645216, new Action<object>(this.OnOnlySpicedItemsSettingChanged));
		this.OnOnlyFetchMarkedItemsSettingChanged(null);
		this.OnOnlySpicedItemsSettingChanged(null);
		this.allCheckBoxLabel.SetText(this.targetFilterable.allResourceFilterLabelString);
		this.CreateCategories();
		this.CreateSpecialItemRows();
		this.titlebar.SetActive(false);
		if (this.storage.showSideScreenTitleBar)
		{
			this.titlebar.SetActive(true);
			this.titlebar.GetComponentInChildren<LocText>().SetText(this.storage.GetProperName());
		}
		if (!this.InputFieldEmpty)
		{
			this.ClearSearch();
		}
		this.ToggleSearchConfiguration(!this.InputFieldEmpty);
	}

	// Token: 0x060073E5 RID: 29669 RVA: 0x002C06AC File Offset: 0x002BE8AC
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		this.onlyAllowTransportItemsCheckBox.ChangeState(this.storage.GetOnlyFetchMarkedItems() ? 1 : 0);
		if (this.storage.allowSettingOnlyFetchMarkedItems)
		{
			this.onlyallowTransportItemsRow.SetActive(true);
			return;
		}
		this.onlyallowTransportItemsRow.SetActive(false);
	}

	// Token: 0x060073E6 RID: 29670 RVA: 0x002C06FC File Offset: 0x002BE8FC
	private void OnOnlySpicedItemsSettingChanged(object data)
	{
		FoodStorage component = this.storage.GetComponent<FoodStorage>();
		if (component != null)
		{
			this.onlyallowSpicedItemsRow.SetActive(true);
			this.onlyAllowSpicedItemsCheckBox.ChangeState(component.SpicedFoodOnly ? 1 : 0);
			return;
		}
		this.onlyallowSpicedItemsRow.SetActive(false);
	}

	// Token: 0x060073E7 RID: 29671 RVA: 0x002C074E File Offset: 0x002BE94E
	public bool IsTagAllowed(Tag tag)
	{
		return this.targetFilterable.AcceptedTags.Contains(tag);
	}

	// Token: 0x060073E8 RID: 29672 RVA: 0x002C0761 File Offset: 0x002BE961
	public void AddTag(Tag tag)
	{
		if (this.targetFilterable == null)
		{
			return;
		}
		this.targetFilterable.AddTagToFilter(tag);
	}

	// Token: 0x060073E9 RID: 29673 RVA: 0x002C077E File Offset: 0x002BE97E
	public void RemoveTag(Tag tag)
	{
		if (this.targetFilterable == null)
		{
			return;
		}
		this.targetFilterable.RemoveTagFromFilter(tag);
	}

	// Token: 0x060073EA RID: 29674 RVA: 0x002C079C File Offset: 0x002BE99C
	private List<TreeFilterableSideScreen.TagOrderInfo> GetTagsSortedAlphabetically(ICollection<Tag> tags)
	{
		List<TreeFilterableSideScreen.TagOrderInfo> list = new List<TreeFilterableSideScreen.TagOrderInfo>();
		foreach (Tag tag in tags)
		{
			list.Add(new TreeFilterableSideScreen.TagOrderInfo
			{
				tag = tag,
				strippedName = tag.ProperNameStripLink()
			});
		}
		list.Sort((TreeFilterableSideScreen.TagOrderInfo a, TreeFilterableSideScreen.TagOrderInfo b) => a.strippedName.CompareTo(b.strippedName));
		return list;
	}

	// Token: 0x060073EB RID: 29675 RVA: 0x002C0830 File Offset: 0x002BEA30
	private TreeFilterableSideScreenRow AddRow(Tag rowTag)
	{
		if (this.tagRowMap.ContainsKey(rowTag))
		{
			return this.tagRowMap[rowTag];
		}
		TreeFilterableSideScreenRow freeElement = this.rowPool.GetFreeElement(this.rowGroup, true);
		freeElement.Parent = this;
		freeElement.standardCommodity = !STORAGEFILTERS.SPECIAL_STORAGE.Contains(rowTag);
		this.tagRowMap.Add(rowTag, freeElement);
		Dictionary<Tag, bool> dictionary = new Dictionary<Tag, bool>();
		foreach (TreeFilterableSideScreen.TagOrderInfo tagOrderInfo in this.GetTagsSortedAlphabetically(DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(rowTag)).FindAll((TreeFilterableSideScreen.TagOrderInfo t) => !this.targetFilterable.ForbiddenTags.Contains(t.tag)))
		{
			dictionary.Add(tagOrderInfo.tag, this.targetFilterable.ContainsTag(tagOrderInfo.tag) || this.targetFilterable.ContainsTag(rowTag));
		}
		freeElement.SetElement(rowTag, this.targetFilterable.ContainsTag(rowTag), dictionary);
		freeElement.transform.SetAsLastSibling();
		return freeElement;
	}

	// Token: 0x060073EC RID: 29676 RVA: 0x002C0944 File Offset: 0x002BEB44
	public float GetAmountInStorage(Tag tag)
	{
		if (!this.IsStorage)
		{
			return 0f;
		}
		return this.storage.GetMassAvailable(tag);
	}

	// Token: 0x060073ED RID: 29677 RVA: 0x002C0960 File Offset: 0x002BEB60
	private void CreateCategories()
	{
		if (this.storage.storageFilters != null && this.storage.storageFilters.Count >= 1)
		{
			bool flag = this.target.GetComponent<CreatureDeliveryPoint>() != null;
			foreach (TreeFilterableSideScreen.TagOrderInfo tagOrderInfo in this.GetTagsSortedAlphabetically(this.storage.storageFilters))
			{
				Tag tag = tagOrderInfo.tag;
				if (flag || DiscoveredResources.Instance.IsDiscovered(tag))
				{
					this.AddRow(tag);
				}
			}
			this.visualDirty = true;
			return;
		}
		global::Debug.LogError("If you're filtering, your storage filter should have the filters set on it");
	}

	// Token: 0x060073EE RID: 29678 RVA: 0x002C0A20 File Offset: 0x002BEC20
	private void CreateSpecialItemRows()
	{
		this.specialItemsHeader.transform.SetAsLastSibling();
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (!keyValuePair.Value.standardCommodity)
			{
				keyValuePair.Value.transform.transform.SetAsLastSibling();
			}
		}
		this.RefreshSpecialItemsHeader();
	}

	// Token: 0x060073EF RID: 29679 RVA: 0x002C0AA8 File Offset: 0x002BECA8
	private void RefreshSpecialItemsHeader()
	{
		bool flag = false;
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (!keyValuePair.Value.standardCommodity)
			{
				flag = true;
				break;
			}
		}
		this.specialItemsHeader.gameObject.SetActive(flag);
	}

	// Token: 0x060073F0 RID: 29680 RVA: 0x002C0B1C File Offset: 0x002BED1C
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (this.target != null && (this.tagRowMap == null || this.tagRowMap.Count == 0))
		{
			this.ReconfigureForPreviousTarget();
		}
	}

	// Token: 0x060073F1 RID: 29681 RVA: 0x002C0B50 File Offset: 0x002BED50
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.storage != null)
		{
			this.storage.Unsubscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
			this.storage.Unsubscribe(1163645216, new Action<object>(this.OnOnlySpicedItemsSettingChanged));
		}
		this.rowPool.ClearAll();
		this.elementPool.ClearAll();
		this.tagRowMap.Clear();
	}

	// Token: 0x060073F2 RID: 29682 RVA: 0x002C0BCC File Offset: 0x002BEDCC
	private void RecordRowExpandedStatus()
	{
		this.rowExpandedStatusMemory.Clear();
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			this.rowExpandedStatusMemory.Add(keyValuePair.Key, keyValuePair.Value.ArrowExpanded);
		}
	}

	// Token: 0x060073F3 RID: 29683 RVA: 0x002C0C44 File Offset: 0x002BEE44
	private void RestoreRowExpandedStatus()
	{
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			if (this.rowExpandedStatusMemory.ContainsKey(keyValuePair.Key))
			{
				keyValuePair.Value.SetArrowToggleState(this.rowExpandedStatusMemory[keyValuePair.Key]);
			}
		}
	}

	// Token: 0x060073F4 RID: 29684 RVA: 0x002C0CC4 File Offset: 0x002BEEC4
	private void InitSearch()
	{
		KInputTextField kinputTextField = this.inputField;
		kinputTextField.onFocus = (global::System.Action)Delegate.Combine(kinputTextField.onFocus, new global::System.Action(delegate
		{
			base.isEditing = true;
			KScreenManager.Instance.RefreshStack();
			UISounds.PlaySound(UISounds.Sound.ClickHUD);
			this.RecordRowExpandedStatus();
		}));
		this.inputField.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
			KScreenManager.Instance.RefreshStack();
		});
		this.inputField.onValueChanged.AddListener(delegate(string value)
		{
			if (this.InputFieldEmpty)
			{
				this.RestoreRowExpandedStatus();
			}
			this.ToggleSearchConfiguration(!this.InputFieldEmpty);
			this.UpdateSearchFilter();
		});
		this.inputField.placeholder.GetComponent<TextMeshProUGUI>().text = UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.SEARCH_PLACEHOLDER;
		this.clearButton.onClick += delegate
		{
			if (!this.InputFieldEmpty)
			{
				this.ClearSearch();
			}
		};
	}

	// Token: 0x060073F5 RID: 29685 RVA: 0x002C0D68 File Offset: 0x002BEF68
	private void ToggleSearchConfiguration(bool searching)
	{
		this.configurationRowsContainer.gameObject.SetActive(!searching);
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			keyValuePair.Value.ShowToggleBox(!searching);
		}
		if (searching)
		{
			this.specialItemsHeader.gameObject.SetActive(false);
			return;
		}
		this.RefreshSpecialItemsHeader();
	}

	// Token: 0x060073F6 RID: 29686 RVA: 0x002C0DF4 File Offset: 0x002BEFF4
	private void ClearSearch()
	{
		this.inputField.text = "";
		this.RestoreRowExpandedStatus();
		this.ToggleSearchConfiguration(false);
	}

	// Token: 0x170007FE RID: 2046
	// (get) Token: 0x060073F7 RID: 29687 RVA: 0x002C0E13 File Offset: 0x002BF013
	public string CurrentSearchValue
	{
		get
		{
			if (this.inputField.text == null)
			{
				return "";
			}
			return this.inputField.text;
		}
	}

	// Token: 0x060073F8 RID: 29688 RVA: 0x002C0E34 File Offset: 0x002BF034
	private void UpdateSearchFilter()
	{
		foreach (KeyValuePair<Tag, TreeFilterableSideScreenRow> keyValuePair in this.tagRowMap)
		{
			keyValuePair.Value.FilterAgainstSearch(keyValuePair.Key, this.CurrentSearchValue);
		}
	}

	// Token: 0x04004FC5 RID: 20421
	[SerializeField]
	private MultiToggle allCheckBox;

	// Token: 0x04004FC6 RID: 20422
	[SerializeField]
	private LocText allCheckBoxLabel;

	// Token: 0x04004FC7 RID: 20423
	[SerializeField]
	private GameObject specialItemsHeader;

	// Token: 0x04004FC8 RID: 20424
	[SerializeField]
	private MultiToggle onlyAllowTransportItemsCheckBox;

	// Token: 0x04004FC9 RID: 20425
	[SerializeField]
	private GameObject onlyallowTransportItemsRow;

	// Token: 0x04004FCA RID: 20426
	[SerializeField]
	private MultiToggle onlyAllowSpicedItemsCheckBox;

	// Token: 0x04004FCB RID: 20427
	[SerializeField]
	private GameObject onlyallowSpicedItemsRow;

	// Token: 0x04004FCC RID: 20428
	[SerializeField]
	private TreeFilterableSideScreenRow rowPrefab;

	// Token: 0x04004FCD RID: 20429
	[SerializeField]
	private GameObject rowGroup;

	// Token: 0x04004FCE RID: 20430
	[SerializeField]
	private TreeFilterableSideScreenElement elementPrefab;

	// Token: 0x04004FCF RID: 20431
	[SerializeField]
	private GameObject titlebar;

	// Token: 0x04004FD0 RID: 20432
	[SerializeField]
	private GameObject contentMask;

	// Token: 0x04004FD1 RID: 20433
	[SerializeField]
	private KInputTextField inputField;

	// Token: 0x04004FD2 RID: 20434
	[SerializeField]
	private KButton clearButton;

	// Token: 0x04004FD3 RID: 20435
	[SerializeField]
	private GameObject configurationRowsContainer;

	// Token: 0x04004FD4 RID: 20436
	private GameObject target;

	// Token: 0x04004FD5 RID: 20437
	private bool visualDirty;

	// Token: 0x04004FD6 RID: 20438
	private bool initialized;

	// Token: 0x04004FD7 RID: 20439
	private KImage onlyAllowTransportItemsImg;

	// Token: 0x04004FD8 RID: 20440
	public UIPool<TreeFilterableSideScreenElement> elementPool;

	// Token: 0x04004FD9 RID: 20441
	private UIPool<TreeFilterableSideScreenRow> rowPool;

	// Token: 0x04004FDA RID: 20442
	private TreeFilterable targetFilterable;

	// Token: 0x04004FDB RID: 20443
	private Dictionary<Tag, TreeFilterableSideScreenRow> tagRowMap = new Dictionary<Tag, TreeFilterableSideScreenRow>();

	// Token: 0x04004FDC RID: 20444
	private Dictionary<Tag, bool> rowExpandedStatusMemory = new Dictionary<Tag, bool>();

	// Token: 0x04004FDD RID: 20445
	private Storage storage;

	// Token: 0x0200203C RID: 8252
	private struct TagOrderInfo
	{
		// Token: 0x0400936E RID: 37742
		public Tag tag;

		// Token: 0x0400936F RID: 37743
		public string strippedName;
	}
}
