using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E35 RID: 3637
public abstract class SingleItemSelectionSideScreenBase : SideScreenContent
{
	// Token: 0x06007336 RID: 29494 RVA: 0x002BCE9E File Offset: 0x002BB09E
	private static bool TagContainsSearchWord(Tag tag, string search)
	{
		return string.IsNullOrEmpty(search) || tag.ProperNameStripLink().ToUpper().Contains(search.ToUpper());
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x06007338 RID: 29496 RVA: 0x002BCEC9 File Offset: 0x002BB0C9
	// (set) Token: 0x06007337 RID: 29495 RVA: 0x002BCEC0 File Offset: 0x002BB0C0
	private protected SingleItemSelectionRow CurrentSelectedItem { protected get; private set; }

	// Token: 0x06007339 RID: 29497 RVA: 0x002BCED4 File Offset: 0x002BB0D4
	protected override void OnPrefabInit()
	{
		if (this.searchbar != null)
		{
			this.searchbar.EditingStateChanged = new Action<bool>(this.OnSearchbarEditStateChanged);
			this.searchbar.ValueChanged = new Action<string>(this.OnSearchBarValueChanged);
			this.activateOnSpawn = true;
		}
		base.OnPrefabInit();
	}

	// Token: 0x0600733A RID: 29498 RVA: 0x002BCF2C File Offset: 0x002BB12C
	protected virtual void OnSearchbarEditStateChanged(bool isEditing)
	{
		base.isEditing = isEditing;
	}

	// Token: 0x0600733B RID: 29499 RVA: 0x002BCF38 File Offset: 0x002BB138
	protected virtual void OnSearchBarValueChanged(string value)
	{
		foreach (Tag tag in this.categories.Keys)
		{
			SingleItemSelectionSideScreenBase.Category category = this.categories[tag];
			bool flag = SingleItemSelectionSideScreenBase.TagContainsSearchWord(tag, value);
			int num = category.FilterItemsBySearch(flag ? null : value);
			category.SetUnfoldedState((num > 0) ? SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Unfolded : SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Folded);
			category.SetVisibilityState(flag || num > 0);
		}
	}

	// Token: 0x0600733C RID: 29500 RVA: 0x002BCFC8 File Offset: 0x002BB1C8
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return base.GetSortKey();
	}

	// Token: 0x0600733D RID: 29501 RVA: 0x002BCFDE File Offset: 0x002BB1DE
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

	// Token: 0x0600733E RID: 29502 RVA: 0x002BCFF8 File Offset: 0x002BB1F8
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

	// Token: 0x0600733F RID: 29503 RVA: 0x002BD014 File Offset: 0x002BB214
	public virtual void SetData(Dictionary<Tag, HashSet<Tag>> data)
	{
		this.ProhibitAllCategories();
		foreach (Tag tag in data.Keys)
		{
			ICollection<Tag> collection = data[tag];
			this.CreateCategoryWithItems(tag, collection);
		}
		this.SortAll();
		if (this.searchbar != null && !string.IsNullOrEmpty(this.searchbar.CurrentSearchValue))
		{
			this.searchbar.ClearSearch();
		}
	}

	// Token: 0x06007340 RID: 29504 RVA: 0x002BD0A8 File Offset: 0x002BB2A8
	public virtual SingleItemSelectionSideScreenBase.Category CreateCategoryWithItems(Tag categoryTag, ICollection<Tag> items)
	{
		SingleItemSelectionSideScreenBase.Category orCreateEmptyCategory = this.GetOrCreateEmptyCategory(categoryTag);
		if (!orCreateEmptyCategory.InitializeItemList(items.Count))
		{
			orCreateEmptyCategory.RemoveAllItems();
		}
		foreach (Tag tag in items)
		{
			SingleItemSelectionRow orCreateItemRow = this.GetOrCreateItemRow(tag);
			orCreateEmptyCategory.AddItem(orCreateItemRow);
		}
		return orCreateEmptyCategory;
	}

	// Token: 0x06007341 RID: 29505 RVA: 0x002BD118 File Offset: 0x002BB318
	public virtual SingleItemSelectionSideScreenBase.Category GetOrCreateEmptyCategory(Tag categoryTag)
	{
		this.original_CategoryRow.gameObject.SetActive(false);
		SingleItemSelectionSideScreenBase.Category category = null;
		if (!this.categories.TryGetValue(categoryTag, out category))
		{
			HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.original_CategoryRow.gameObject, this.original_CategoryRow.transform.parent.gameObject, false);
			hierarchyReferences.gameObject.SetActive(true);
			category = new SingleItemSelectionSideScreenBase.Category(hierarchyReferences, categoryTag);
			category.ItemRemoved = new Action<SingleItemSelectionRow>(this.RecycleItemRow);
			SingleItemSelectionSideScreenBase.Category category2 = category;
			category2.ToggleClicked = (Action<SingleItemSelectionSideScreenBase.Category>)Delegate.Combine(category2.ToggleClicked, new Action<SingleItemSelectionSideScreenBase.Category>(this.CategoryToggleClicked));
			this.categories.Add(categoryTag, category);
		}
		else
		{
			category.SetProihibedState(false);
			category.SetVisibilityState(true);
		}
		return category;
	}

	// Token: 0x06007342 RID: 29506 RVA: 0x002BD1D4 File Offset: 0x002BB3D4
	public virtual SingleItemSelectionRow GetOrCreateItemRow(Tag itemTag)
	{
		this.original_ItemRow.gameObject.SetActive(false);
		SingleItemSelectionRow singleItemSelectionRow = null;
		if (!this.pooledRows.TryGetValue(itemTag, out singleItemSelectionRow))
		{
			singleItemSelectionRow = Util.KInstantiateUI<SingleItemSelectionRow>(this.original_ItemRow.gameObject, this.original_ItemRow.transform.parent.gameObject, false);
			global::UnityEngine.Object @object = singleItemSelectionRow;
			string text = "Item-";
			Tag tag = itemTag;
			@object.name = text + tag.ToString();
		}
		else
		{
			this.pooledRows.Remove(itemTag);
		}
		singleItemSelectionRow.gameObject.SetActive(true);
		singleItemSelectionRow.SetTag(itemTag);
		singleItemSelectionRow.Clicked = new Action<SingleItemSelectionRow>(this.ItemRowClicked);
		singleItemSelectionRow.SetVisibleState(true);
		return singleItemSelectionRow;
	}

	// Token: 0x06007343 RID: 29507 RVA: 0x002BD288 File Offset: 0x002BB488
	public SingleItemSelectionSideScreenBase.Category GetCategoryWithItem(Tag itemTag, bool includeNotVisibleCategories = false)
	{
		foreach (SingleItemSelectionSideScreenBase.Category category in this.categories.Values)
		{
			if ((includeNotVisibleCategories || category.IsVisible) && category.GetItem(itemTag) != null)
			{
				return category;
			}
		}
		return null;
	}

	// Token: 0x06007344 RID: 29508 RVA: 0x002BD2FC File Offset: 0x002BB4FC
	public virtual void SetSelectedItem(SingleItemSelectionRow itemRow)
	{
		if (this.CurrentSelectedItem != null)
		{
			this.CurrentSelectedItem.SetSelected(false);
		}
		this.CurrentSelectedItem = itemRow;
		if (itemRow != null)
		{
			itemRow.SetSelected(true);
		}
	}

	// Token: 0x06007345 RID: 29509 RVA: 0x002BD330 File Offset: 0x002BB530
	public virtual bool SetSelectedItem(Tag itemTag)
	{
		foreach (Tag tag in this.categories.Keys)
		{
			SingleItemSelectionSideScreenBase.Category category = this.categories[tag];
			if (category.IsVisible)
			{
				SingleItemSelectionRow item = category.GetItem(itemTag);
				if (item != null)
				{
					this.SetSelectedItem(item);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06007346 RID: 29510 RVA: 0x002BD3B8 File Offset: 0x002BB5B8
	public virtual void ItemRowClicked(SingleItemSelectionRow rowClicked)
	{
		this.SetSelectedItem(rowClicked);
	}

	// Token: 0x06007347 RID: 29511 RVA: 0x002BD3C1 File Offset: 0x002BB5C1
	public virtual void CategoryToggleClicked(SingleItemSelectionSideScreenBase.Category categoryClicked)
	{
		categoryClicked.ToggleUnfoldedState();
	}

	// Token: 0x06007348 RID: 29512 RVA: 0x002BD3CC File Offset: 0x002BB5CC
	private void RecycleItemRow(SingleItemSelectionRow row)
	{
		if (this.pooledRows.ContainsKey(row.tag))
		{
			global::Debug.LogError(string.Format("Recycling an item row with tag {0} that was already in the recycle pool", row.tag));
		}
		if (this.CurrentSelectedItem == row)
		{
			this.SetSelectedItem(null);
		}
		row.Clicked = null;
		row.SetSelected(false);
		row.transform.SetParent(this.original_ItemRow.transform.parent.parent);
		row.gameObject.SetActive(false);
		this.pooledRows.Add(row.tag, row);
	}

	// Token: 0x06007349 RID: 29513 RVA: 0x002BD468 File Offset: 0x002BB668
	private void ProhibitAllCategories()
	{
		foreach (SingleItemSelectionSideScreenBase.Category category in this.categories.Values)
		{
			category.SetProihibedState(true);
		}
	}

	// Token: 0x0600734A RID: 29514 RVA: 0x002BD4C0 File Offset: 0x002BB6C0
	public virtual void SortAll()
	{
		foreach (SingleItemSelectionSideScreenBase.Category category in this.categories.Values)
		{
			if (category.IsVisible)
			{
				category.Sort();
				category.SendToLastSibiling();
			}
		}
	}

	// Token: 0x04004F5B RID: 20315
	[Space]
	[Header("Settings")]
	[SerializeField]
	private SearchBar searchbar;

	// Token: 0x04004F5C RID: 20316
	[SerializeField]
	protected HierarchyReferences original_CategoryRow;

	// Token: 0x04004F5D RID: 20317
	[SerializeField]
	protected SingleItemSelectionRow original_ItemRow;

	// Token: 0x04004F5E RID: 20318
	protected SortedDictionary<Tag, SingleItemSelectionSideScreenBase.Category> categories = new SortedDictionary<Tag, SingleItemSelectionSideScreenBase.Category>(SingleItemSelectionSideScreenBase.categoryComparer);

	// Token: 0x04004F5F RID: 20319
	private Dictionary<Tag, SingleItemSelectionRow> pooledRows = new Dictionary<Tag, SingleItemSelectionRow>();

	// Token: 0x04004F60 RID: 20320
	private static TagNameComparer categoryComparer = new TagNameComparer(GameTags.Void);

	// Token: 0x04004F61 RID: 20321
	private static SingleItemSelectionSideScreenBase.ItemComparer itemRowComparer = new SingleItemSelectionSideScreenBase.ItemComparer(GameTags.Void);

	// Token: 0x02002036 RID: 8246
	public class ItemComparer : IComparer<SingleItemSelectionRow>
	{
		// Token: 0x0600B58E RID: 46478 RVA: 0x003DFC77 File Offset: 0x003DDE77
		public ItemComparer()
		{
		}

		// Token: 0x0600B58F RID: 46479 RVA: 0x003DFC7F File Offset: 0x003DDE7F
		public ItemComparer(Tag firstTag)
		{
			this.firstTag = firstTag;
		}

		// Token: 0x0600B590 RID: 46480 RVA: 0x003DFC90 File Offset: 0x003DDE90
		public int Compare(SingleItemSelectionRow x, SingleItemSelectionRow y)
		{
			if (x == y)
			{
				return 0;
			}
			if (this.firstTag.IsValid)
			{
				if (x.tag == this.firstTag && y.tag != this.firstTag)
				{
					return 1;
				}
				if (x.tag != this.firstTag && y.tag == this.firstTag)
				{
					return -1;
				}
			}
			return x.tag.ProperNameStripLink().CompareTo(y.tag.ProperNameStripLink());
		}

		// Token: 0x0400935C RID: 37724
		private Tag firstTag;
	}

	// Token: 0x02002037 RID: 8247
	public class Category
	{
		// Token: 0x0600B591 RID: 46481 RVA: 0x003DFD20 File Offset: 0x003DDF20
		public virtual void ToggleUnfoldedState()
		{
			SingleItemSelectionSideScreenBase.Category.UnfoldedStates currentState = (SingleItemSelectionSideScreenBase.Category.UnfoldedStates)this.toggle.CurrentState;
			if (currentState == SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Folded)
			{
				this.SetUnfoldedState(SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Unfolded);
				return;
			}
			if (currentState != SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Unfolded)
			{
				return;
			}
			this.SetUnfoldedState(SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Folded);
		}

		// Token: 0x0600B592 RID: 46482 RVA: 0x003DFD50 File Offset: 0x003DDF50
		public virtual void SetUnfoldedState(SingleItemSelectionSideScreenBase.Category.UnfoldedStates new_state)
		{
			this.toggle.ChangeState((int)new_state);
			this.entries.gameObject.SetActive(new_state == SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Unfolded);
		}

		// Token: 0x0600B593 RID: 46483 RVA: 0x003DFD72 File Offset: 0x003DDF72
		public virtual void SetTitle(string text)
		{
			this.title.text = text;
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600B595 RID: 46485 RVA: 0x003DFD89 File Offset: 0x003DDF89
		// (set) Token: 0x0600B594 RID: 46484 RVA: 0x003DFD80 File Offset: 0x003DDF80
		public Tag CategoryTag { get; protected set; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600B597 RID: 46487 RVA: 0x003DFD9A File Offset: 0x003DDF9A
		// (set) Token: 0x0600B596 RID: 46486 RVA: 0x003DFD91 File Offset: 0x003DDF91
		public bool IsProhibited { get; protected set; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600B598 RID: 46488 RVA: 0x003DFDA2 File Offset: 0x003DDFA2
		public bool IsVisible
		{
			get
			{
				return this.hierarchyReferences != null && this.hierarchyReferences.gameObject.activeSelf;
			}
		}

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600B599 RID: 46489 RVA: 0x003DFDC4 File Offset: 0x003DDFC4
		protected RectTransform entries
		{
			get
			{
				return this.hierarchyReferences.GetReference<RectTransform>("Entries");
			}
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600B59A RID: 46490 RVA: 0x003DFDD6 File Offset: 0x003DDFD6
		protected LocText title
		{
			get
			{
				return this.hierarchyReferences.GetReference<LocText>("Label");
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600B59B RID: 46491 RVA: 0x003DFDE8 File Offset: 0x003DDFE8
		protected MultiToggle toggle
		{
			get
			{
				return this.hierarchyReferences.GetReference<MultiToggle>("Toggle");
			}
		}

		// Token: 0x0600B59C RID: 46492 RVA: 0x003DFDFA File Offset: 0x003DDFFA
		public Category(HierarchyReferences references, Tag categoryTag)
		{
			this.CategoryTag = categoryTag;
			this.hierarchyReferences = references;
			this.toggle.onClick = new global::System.Action(this.OnToggleClicked);
			this.SetTitle(categoryTag.ProperName());
		}

		// Token: 0x0600B59D RID: 46493 RVA: 0x003DFE34 File Offset: 0x003DE034
		public virtual void OnToggleClicked()
		{
			Action<SingleItemSelectionSideScreenBase.Category> toggleClicked = this.ToggleClicked;
			if (toggleClicked == null)
			{
				return;
			}
			toggleClicked(this);
		}

		// Token: 0x0600B59E RID: 46494 RVA: 0x003DFE48 File Offset: 0x003DE048
		public virtual void AddItems(SingleItemSelectionRow[] _items)
		{
			if (this.items == null)
			{
				this.items = new List<SingleItemSelectionRow>(_items);
				return;
			}
			for (int i = 0; i < _items.Length; i++)
			{
				if (!this.items.Contains(_items[i]))
				{
					_items[i].transform.SetParent(this.entries, false);
					this.items.Add(_items[i]);
				}
			}
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x003DFEAA File Offset: 0x003DE0AA
		public virtual void AddItem(SingleItemSelectionRow item)
		{
			if (this.items == null)
			{
				this.items = new List<SingleItemSelectionRow>();
			}
			item.transform.SetParent(this.entries, false);
			this.items.Add(item);
		}

		// Token: 0x0600B5A0 RID: 46496 RVA: 0x003DFEDD File Offset: 0x003DE0DD
		public virtual bool InitializeItemList(int size)
		{
			if (this.items == null)
			{
				this.items = new List<SingleItemSelectionRow>(size);
				return true;
			}
			return false;
		}

		// Token: 0x0600B5A1 RID: 46497 RVA: 0x003DFEF6 File Offset: 0x003DE0F6
		public virtual void SetVisibilityState(bool isVisible)
		{
			this.hierarchyReferences.gameObject.SetActive(isVisible && !this.IsProhibited);
		}

		// Token: 0x0600B5A2 RID: 46498 RVA: 0x003DFF18 File Offset: 0x003DE118
		public virtual void RemoveAllItems()
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				SingleItemSelectionRow singleItemSelectionRow = this.items[i];
				Action<SingleItemSelectionRow> itemRemoved = this.ItemRemoved;
				if (itemRemoved != null)
				{
					itemRemoved(singleItemSelectionRow);
				}
			}
			this.items.Clear();
			this.items = null;
		}

		// Token: 0x0600B5A3 RID: 46499 RVA: 0x003DFF6C File Offset: 0x003DE16C
		public virtual SingleItemSelectionRow RemoveItem(Tag itemTag)
		{
			if (this.items != null)
			{
				SingleItemSelectionRow singleItemSelectionRow = this.items.Find((SingleItemSelectionRow row) => row.tag == itemTag);
				if (singleItemSelectionRow != null)
				{
					Action<SingleItemSelectionRow> itemRemoved = this.ItemRemoved;
					if (itemRemoved != null)
					{
						itemRemoved(singleItemSelectionRow);
					}
					return singleItemSelectionRow;
				}
			}
			return null;
		}

		// Token: 0x0600B5A4 RID: 46500 RVA: 0x003DFFC4 File Offset: 0x003DE1C4
		public virtual bool RemoveItem(SingleItemSelectionRow itemRow)
		{
			if (this.items != null && this.items.Remove(itemRow))
			{
				Action<SingleItemSelectionRow> itemRemoved = this.ItemRemoved;
				if (itemRemoved != null)
				{
					itemRemoved(itemRow);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600B5A5 RID: 46501 RVA: 0x003DFFF4 File Offset: 0x003DE1F4
		public SingleItemSelectionRow GetItem(Tag itemTag)
		{
			if (this.items == null)
			{
				return null;
			}
			return this.items.Find((SingleItemSelectionRow row) => row.tag == itemTag);
		}

		// Token: 0x0600B5A6 RID: 46502 RVA: 0x003E0030 File Offset: 0x003DE230
		public int FilterItemsBySearch(string searchValue)
		{
			int num = 0;
			if (this.items != null)
			{
				foreach (SingleItemSelectionRow singleItemSelectionRow in this.items)
				{
					bool flag = SingleItemSelectionSideScreenBase.TagContainsSearchWord(singleItemSelectionRow.tag, searchValue);
					singleItemSelectionRow.SetVisibleState(flag);
					if (flag)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600B5A7 RID: 46503 RVA: 0x003E00A0 File Offset: 0x003DE2A0
		public void Sort()
		{
			if (this.items != null)
			{
				this.items.Sort(SingleItemSelectionSideScreenBase.itemRowComparer);
				foreach (SingleItemSelectionRow singleItemSelectionRow in this.items)
				{
					singleItemSelectionRow.transform.SetAsLastSibling();
				}
			}
		}

		// Token: 0x0600B5A8 RID: 46504 RVA: 0x003E0110 File Offset: 0x003DE310
		public void SendToLastSibiling()
		{
			this.hierarchyReferences.transform.SetAsLastSibling();
		}

		// Token: 0x0600B5A9 RID: 46505 RVA: 0x003E0122 File Offset: 0x003DE322
		public void SetProihibedState(bool isPohibited)
		{
			this.IsProhibited = isPohibited;
			if (this.IsVisible && isPohibited)
			{
				this.SetVisibilityState(false);
			}
		}

		// Token: 0x0400935D RID: 37725
		public Action<SingleItemSelectionRow> ItemRemoved;

		// Token: 0x0400935E RID: 37726
		public Action<SingleItemSelectionSideScreenBase.Category> ToggleClicked;

		// Token: 0x04009361 RID: 37729
		protected HierarchyReferences hierarchyReferences;

		// Token: 0x04009362 RID: 37730
		protected List<SingleItemSelectionRow> items;

		// Token: 0x0200290B RID: 10507
		public enum UnfoldedStates
		{
			// Token: 0x0400B59C RID: 46492
			Folded,
			// Token: 0x0400B59D RID: 46493
			Unfolded
		}
	}
}
