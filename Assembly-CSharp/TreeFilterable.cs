using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007EB RID: 2027
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/TreeFilterable")]
public class TreeFilterable : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06003707 RID: 14087 RVA: 0x00131BCD File Offset: 0x0012FDCD
	public HashSet<Tag> AcceptedTags
	{
		get
		{
			return this.acceptedTagSet;
		}
	}

	// Token: 0x06003708 RID: 14088 RVA: 0x00131BD8 File Offset: 0x0012FDD8
	[OnDeserialized]
	[Obsolete]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 20))
		{
			this.filterByStorageCategoriesOnSpawn = false;
		}
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.acceptedTagSet.UnionWith(this.acceptedTags);
			this.acceptedTagSet.ExceptWith(this.ForbiddenTags);
			this.acceptedTags = null;
		}
	}

	// Token: 0x06003709 RID: 14089 RVA: 0x00131C44 File Offset: 0x0012FE44
	private void OnDiscover(Tag category_tag, Tag tag)
	{
		if (this.preventAutoAddOnDiscovery)
		{
			return;
		}
		if (this.storage.storageFilters.Contains(category_tag))
		{
			bool flag = false;
			if (DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(category_tag).Count <= 1)
			{
				foreach (Tag tag2 in this.storage.storageFilters)
				{
					if (!(tag2 == category_tag) && DiscoveredResources.Instance.IsDiscovered(tag2))
					{
						flag = true;
						foreach (Tag tag3 in DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(tag2))
						{
							if (!this.acceptedTagSet.Contains(tag3))
							{
								return;
							}
						}
					}
				}
				if (!flag)
				{
					return;
				}
			}
			foreach (Tag tag4 in DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(category_tag))
			{
				if (!(tag4 == tag) && !this.acceptedTagSet.Contains(tag4))
				{
					return;
				}
			}
			this.AddTagToFilter(tag);
		}
	}

	// Token: 0x0600370A RID: 14090 RVA: 0x00131DA0 File Offset: 0x0012FFA0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<TreeFilterable>(-905833192, TreeFilterable.OnCopySettingsDelegate);
	}

	// Token: 0x0600370B RID: 14091 RVA: 0x00131DBC File Offset: 0x0012FFBC
	protected override void OnSpawn()
	{
		DiscoveredResources.Instance.OnDiscover += this.OnDiscover;
		if (this.storageToFilterTag != Tag.Invalid)
		{
			foreach (Storage storage in base.GetComponents<Storage>())
			{
				if (storage.storageID == this.storageToFilterTag)
				{
					this.storage = storage;
					break;
				}
			}
		}
		if (this.autoSelectStoredOnLoad && this.storage != null)
		{
			HashSet<Tag> hashSet = new HashSet<Tag>(this.acceptedTagSet);
			hashSet.UnionWith(this.storage.GetAllIDsInStorage());
			this.UpdateFilters(hashSet);
		}
		if (this.OnFilterChanged != null)
		{
			this.OnFilterChanged(this.acceptedTagSet);
		}
		this.RefreshTint();
		if (this.filterByStorageCategoriesOnSpawn)
		{
			this.RemoveIncorrectAcceptedTags();
		}
	}

	// Token: 0x0600370C RID: 14092 RVA: 0x00131E90 File Offset: 0x00130090
	private void RemoveIncorrectAcceptedTags()
	{
		List<Tag> list = new List<Tag>();
		foreach (Tag tag in this.acceptedTagSet)
		{
			bool flag = false;
			foreach (Tag tag2 in this.storage.storageFilters)
			{
				if (DiscoveredResources.Instance.GetDiscoveredResourcesFromTag(tag2).Contains(tag))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(tag);
			}
		}
		foreach (Tag tag3 in list)
		{
			this.RemoveTagFromFilter(tag3);
		}
	}

	// Token: 0x0600370D RID: 14093 RVA: 0x00131F88 File Offset: 0x00130188
	protected override void OnCleanUp()
	{
		DiscoveredResources.Instance.OnDiscover -= this.OnDiscover;
		base.OnCleanUp();
	}

	// Token: 0x0600370E RID: 14094 RVA: 0x00131FA8 File Offset: 0x001301A8
	private void OnCopySettings(object data)
	{
		if (this.copySettingsEnabled)
		{
			TreeFilterable component = ((GameObject)data).GetComponent<TreeFilterable>();
			if (component != null)
			{
				this.UpdateFilters(component.GetTags());
			}
		}
	}

	// Token: 0x0600370F RID: 14095 RVA: 0x00131FDE File Offset: 0x001301DE
	public Storage GetFilterStorage()
	{
		return this.storage;
	}

	// Token: 0x06003710 RID: 14096 RVA: 0x00131FE6 File Offset: 0x001301E6
	public HashSet<Tag> GetTags()
	{
		return this.acceptedTagSet;
	}

	// Token: 0x06003711 RID: 14097 RVA: 0x00131FEE File Offset: 0x001301EE
	public bool ContainsTag(Tag t)
	{
		return this.acceptedTagSet.Contains(t);
	}

	// Token: 0x06003712 RID: 14098 RVA: 0x00131FFC File Offset: 0x001301FC
	public void AddTagToFilter(Tag t)
	{
		if (this.ContainsTag(t))
		{
			return;
		}
		this.UpdateFilters(new HashSet<Tag>(this.acceptedTagSet) { t });
	}

	// Token: 0x06003713 RID: 14099 RVA: 0x00132030 File Offset: 0x00130230
	public void RemoveTagFromFilter(Tag t)
	{
		if (!this.ContainsTag(t))
		{
			return;
		}
		HashSet<Tag> hashSet = new HashSet<Tag>(this.acceptedTagSet);
		hashSet.Remove(t);
		this.UpdateFilters(hashSet);
	}

	// Token: 0x06003714 RID: 14100 RVA: 0x00132064 File Offset: 0x00130264
	public void UpdateFilters(HashSet<Tag> filters)
	{
		this.acceptedTagSet.Clear();
		this.acceptedTagSet.UnionWith(filters);
		this.acceptedTagSet.ExceptWith(this.ForbiddenTags);
		if (this.OnFilterChanged != null)
		{
			this.OnFilterChanged(this.acceptedTagSet);
		}
		this.RefreshTint();
		if (!this.dropIncorrectOnFilterChange || this.storage == null || this.storage.items == null)
		{
			return;
		}
		if (!this.filterAllStoragesOnBuilding)
		{
			this.DropFilteredItemsFromTargetStorage(this.storage);
			return;
		}
		foreach (Storage storage in base.GetComponents<Storage>())
		{
			this.DropFilteredItemsFromTargetStorage(storage);
		}
	}

	// Token: 0x06003715 RID: 14101 RVA: 0x00132114 File Offset: 0x00130314
	private void DropFilteredItemsFromTargetStorage(Storage targetStorage)
	{
		for (int i = targetStorage.items.Count - 1; i >= 0; i--)
		{
			GameObject gameObject = targetStorage.items[i];
			if (!(gameObject == null))
			{
				KPrefabID component = gameObject.GetComponent<KPrefabID>();
				if (!this.acceptedTagSet.Contains(component.PrefabTag))
				{
					targetStorage.Drop(gameObject, true);
				}
			}
		}
	}

	// Token: 0x06003716 RID: 14102 RVA: 0x00132174 File Offset: 0x00130374
	public string GetTagsAsStatus(int maxDisplays = 6)
	{
		string text = "Tags:\n";
		List<Tag> list = new List<Tag>(this.storage.storageFilters);
		list.Intersect(this.acceptedTagSet);
		for (int i = 0; i < Mathf.Min(list.Count, maxDisplays); i++)
		{
			text += list[i].ProperName();
			if (i < Mathf.Min(list.Count, maxDisplays) - 1)
			{
				text += "\n";
			}
			if (i == maxDisplays - 1 && list.Count > maxDisplays)
			{
				text += "\n...";
				break;
			}
		}
		if (base.tag.Length == 0)
		{
			text = "No tags selected";
		}
		return text;
	}

	// Token: 0x06003717 RID: 14103 RVA: 0x00132220 File Offset: 0x00130420
	private void RefreshTint()
	{
		bool flag = this.acceptedTagSet != null && this.acceptedTagSet.Count != 0;
		if (this.tintOnNoFiltersSet)
		{
			base.GetComponent<KBatchedAnimController>().TintColour = (flag ? this.filterTint : this.noFilterTint);
		}
		base.GetComponent<KSelectable>().ToggleStatusItem(Db.Get().BuildingStatusItems.NoStorageFilterSet, !flag, this);
	}

	// Token: 0x0400213D RID: 8509
	[MyCmpReq]
	private Storage storage;

	// Token: 0x0400213E RID: 8510
	public Tag storageToFilterTag = Tag.Invalid;

	// Token: 0x0400213F RID: 8511
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04002140 RID: 8512
	public static readonly Color32 FILTER_TINT = Color.white;

	// Token: 0x04002141 RID: 8513
	public static readonly Color32 NO_FILTER_TINT = new Color(0.5019608f, 0.5019608f, 0.5019608f, 1f);

	// Token: 0x04002142 RID: 8514
	public Color32 filterTint = TreeFilterable.FILTER_TINT;

	// Token: 0x04002143 RID: 8515
	public Color32 noFilterTint = TreeFilterable.NO_FILTER_TINT;

	// Token: 0x04002144 RID: 8516
	[SerializeField]
	public bool dropIncorrectOnFilterChange = true;

	// Token: 0x04002145 RID: 8517
	[SerializeField]
	public bool autoSelectStoredOnLoad = true;

	// Token: 0x04002146 RID: 8518
	public bool showUserMenu = true;

	// Token: 0x04002147 RID: 8519
	public bool copySettingsEnabled = true;

	// Token: 0x04002148 RID: 8520
	public bool preventAutoAddOnDiscovery;

	// Token: 0x04002149 RID: 8521
	public string allResourceFilterLabelString = UI.UISIDESCREENS.TREEFILTERABLESIDESCREEN.ALLBUTTON;

	// Token: 0x0400214A RID: 8522
	public bool filterAllStoragesOnBuilding;

	// Token: 0x0400214B RID: 8523
	public bool tintOnNoFiltersSet = true;

	// Token: 0x0400214C RID: 8524
	public TreeFilterable.UISideScreenHeight uiHeight = TreeFilterable.UISideScreenHeight.Tall;

	// Token: 0x0400214D RID: 8525
	public bool filterByStorageCategoriesOnSpawn = true;

	// Token: 0x0400214E RID: 8526
	[SerializeField]
	[Serialize]
	[Obsolete("Deprecated, use acceptedTagSet")]
	private List<Tag> acceptedTags = new List<Tag>();

	// Token: 0x0400214F RID: 8527
	[SerializeField]
	[Serialize]
	private HashSet<Tag> acceptedTagSet = new HashSet<Tag>();

	// Token: 0x04002150 RID: 8528
	public HashSet<Tag> ForbiddenTags = new HashSet<Tag>();

	// Token: 0x04002151 RID: 8529
	public Action<HashSet<Tag>> OnFilterChanged;

	// Token: 0x04002152 RID: 8530
	private static readonly EventSystem.IntraObjectHandler<TreeFilterable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<TreeFilterable>(delegate(TreeFilterable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x02001745 RID: 5957
	public enum UISideScreenHeight
	{
		// Token: 0x0400752F RID: 29999
		Short,
		// Token: 0x04007530 RID: 30000
		Tall
	}
}
