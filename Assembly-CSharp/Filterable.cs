using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000727 RID: 1831
[AddComponentMenu("KMonoBehaviour/scripts/Filterable")]
public class Filterable : KMonoBehaviour
{
	// Token: 0x14000012 RID: 18
	// (add) Token: 0x06002E1D RID: 11805 RVA: 0x001089DC File Offset: 0x00106BDC
	// (remove) Token: 0x06002E1E RID: 11806 RVA: 0x00108A14 File Offset: 0x00106C14
	public event Action<Tag> onFilterChanged;

	// Token: 0x1700027B RID: 635
	// (get) Token: 0x06002E1F RID: 11807 RVA: 0x00108A49 File Offset: 0x00106C49
	// (set) Token: 0x06002E20 RID: 11808 RVA: 0x00108A51 File Offset: 0x00106C51
	public Tag SelectedTag
	{
		get
		{
			return this.selectedTag;
		}
		set
		{
			this.selectedTag = value;
			this.OnFilterChanged();
		}
	}

	// Token: 0x06002E21 RID: 11809 RVA: 0x00108A60 File Offset: 0x00106C60
	public Dictionary<Tag, HashSet<Tag>> GetTagOptions()
	{
		Dictionary<Tag, HashSet<Tag>> dictionary = new Dictionary<Tag, HashSet<Tag>>();
		if (this.filterElementState == Filterable.ElementState.Solid)
		{
			dictionary = DiscoveredResources.Instance.GetDiscoveredResourcesFromTagSet(Filterable.filterableCategories);
		}
		else
		{
			foreach (Element element in ElementLoader.elements)
			{
				if (!element.disabled && ((element.IsGas && this.filterElementState == Filterable.ElementState.Gas) || (element.IsLiquid && this.filterElementState == Filterable.ElementState.Liquid)))
				{
					Tag materialCategoryTag = element.GetMaterialCategoryTag();
					if (!dictionary.ContainsKey(materialCategoryTag))
					{
						dictionary[materialCategoryTag] = new HashSet<Tag>();
					}
					Tag tag = GameTagExtensions.Create(element.id);
					dictionary[materialCategoryTag].Add(tag);
				}
			}
		}
		dictionary.Add(GameTags.Void, new HashSet<Tag> { GameTags.Void });
		return dictionary;
	}

	// Token: 0x06002E22 RID: 11810 RVA: 0x00108B50 File Offset: 0x00106D50
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Filterable>(-905833192, Filterable.OnCopySettingsDelegate);
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x00108B6C File Offset: 0x00106D6C
	private void OnCopySettings(object data)
	{
		Filterable component = ((GameObject)data).GetComponent<Filterable>();
		if (component != null)
		{
			this.SelectedTag = component.SelectedTag;
		}
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x00108B9A File Offset: 0x00106D9A
	protected override void OnSpawn()
	{
		this.OnFilterChanged();
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x00108BA4 File Offset: 0x00106DA4
	private void OnFilterChanged()
	{
		if (this.onFilterChanged != null)
		{
			this.onFilterChanged(this.selectedTag);
		}
		Operational component = base.GetComponent<Operational>();
		if (component != null)
		{
			component.SetFlag(Filterable.filterSelected, this.selectedTag.IsValid);
		}
	}

	// Token: 0x04001B3B RID: 6971
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001B3C RID: 6972
	[Serialize]
	public Filterable.ElementState filterElementState;

	// Token: 0x04001B3D RID: 6973
	[Serialize]
	private Tag selectedTag = GameTags.Void;

	// Token: 0x04001B3F RID: 6975
	private static TagSet filterableCategories = new TagSet(new TagSet[]
	{
		GameTags.CalorieCategories,
		GameTags.UnitCategories,
		GameTags.MaterialCategories,
		GameTags.MaterialBuildingElements
	});

	// Token: 0x04001B40 RID: 6976
	private static readonly Operational.Flag filterSelected = new Operational.Flag("filterSelected", Operational.Flag.Type.Requirement);

	// Token: 0x04001B41 RID: 6977
	private static readonly EventSystem.IntraObjectHandler<Filterable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Filterable>(delegate(Filterable component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x020015CC RID: 5580
	public enum ElementState
	{
		// Token: 0x040070DE RID: 28894
		None,
		// Token: 0x040070DF RID: 28895
		Solid,
		// Token: 0x040070E0 RID: 28896
		Liquid,
		// Token: 0x040070E1 RID: 28897
		Gas
	}
}
