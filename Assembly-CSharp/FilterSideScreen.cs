using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DF6 RID: 3574
public class FilterSideScreen : SingleItemSelectionSideScreenBase
{
	// Token: 0x060070D5 RID: 28885 RVA: 0x002AE8AC File Offset: 0x002ACAAC
	public override bool IsValidForTarget(GameObject target)
	{
		bool flag;
		if (this.isLogicFilter)
		{
			flag = target.GetComponent<ConduitElementSensor>() != null || target.GetComponent<LogicElementSensor>() != null;
		}
		else
		{
			flag = target.GetComponent<ElementFilter>() != null || target.GetComponent<RocketConduitStorageAccess>() != null || target.GetComponent<DevPump>() != null;
		}
		return flag && target.GetComponent<Filterable>() != null;
	}

	// Token: 0x060070D6 RID: 28886 RVA: 0x002AE920 File Offset: 0x002ACB20
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetFilterable = target.GetComponent<Filterable>();
		if (this.targetFilterable == null)
		{
			return;
		}
		switch (this.targetFilterable.filterElementState)
		{
		case Filterable.ElementState.Solid:
			this.everythingElseHeaderLabel.text = UI.UISIDESCREENS.FILTERSIDESCREEN.UNFILTEREDELEMENTS.SOLID;
			goto IL_0087;
		case Filterable.ElementState.Gas:
			this.everythingElseHeaderLabel.text = UI.UISIDESCREENS.FILTERSIDESCREEN.UNFILTEREDELEMENTS.GAS;
			goto IL_0087;
		}
		this.everythingElseHeaderLabel.text = UI.UISIDESCREENS.FILTERSIDESCREEN.UNFILTEREDELEMENTS.LIQUID;
		IL_0087:
		this.Configure(this.targetFilterable);
		this.SetFilterTag(this.targetFilterable.SelectedTag);
	}

	// Token: 0x060070D7 RID: 28887 RVA: 0x002AE9D1 File Offset: 0x002ACBD1
	public override void ItemRowClicked(SingleItemSelectionRow rowClicked)
	{
		this.SetFilterTag(rowClicked.tag);
		base.ItemRowClicked(rowClicked);
	}

	// Token: 0x060070D8 RID: 28888 RVA: 0x002AE9E8 File Offset: 0x002ACBE8
	private void Configure(Filterable filterable)
	{
		Dictionary<Tag, HashSet<Tag>> tagOptions = filterable.GetTagOptions();
		Tag tag = GameTags.Void;
		foreach (Tag tag2 in tagOptions.Keys)
		{
			using (HashSet<Tag>.Enumerator enumerator2 = tagOptions[tag2].GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current == filterable.SelectedTag)
					{
						tag = tag2;
						break;
					}
				}
			}
		}
		this.SetData(tagOptions);
		SingleItemSelectionSideScreenBase.Category category = null;
		if (this.categories.TryGetValue(GameTags.Void, out category))
		{
			category.SetProihibedState(true);
		}
		if (tag != GameTags.Void)
		{
			this.categories[tag].SetUnfoldedState(SingleItemSelectionSideScreenBase.Category.UnfoldedStates.Unfolded);
		}
		if (this.voidRow == null)
		{
			this.voidRow = this.GetOrCreateItemRow(GameTags.Void);
		}
		this.voidRow.transform.SetAsFirstSibling();
		if (filterable.SelectedTag != GameTags.Void)
		{
			this.SetSelectedItem(filterable.SelectedTag);
		}
		else
		{
			this.SetSelectedItem(this.voidRow);
		}
		this.RefreshUI();
	}

	// Token: 0x060070D9 RID: 28889 RVA: 0x002AEB38 File Offset: 0x002ACD38
	private void SetFilterTag(Tag tag)
	{
		if (this.targetFilterable == null)
		{
			return;
		}
		if (tag.IsValid)
		{
			this.targetFilterable.SelectedTag = tag;
		}
		this.RefreshUI();
	}

	// Token: 0x060070DA RID: 28890 RVA: 0x002AEB64 File Offset: 0x002ACD64
	private void RefreshUI()
	{
		LocString locString;
		switch (this.targetFilterable.filterElementState)
		{
		case Filterable.ElementState.Solid:
			locString = UI.UISIDESCREENS.FILTERSIDESCREEN.FILTEREDELEMENT.SOLID;
			goto IL_0038;
		case Filterable.ElementState.Gas:
			locString = UI.UISIDESCREENS.FILTERSIDESCREEN.FILTEREDELEMENT.GAS;
			goto IL_0038;
		}
		locString = UI.UISIDESCREENS.FILTERSIDESCREEN.FILTEREDELEMENT.LIQUID;
		IL_0038:
		this.currentSelectionLabel.text = string.Format(locString, UI.UISIDESCREENS.FILTERSIDESCREEN.NOELEMENTSELECTED);
		if (base.CurrentSelectedItem == null || base.CurrentSelectedItem.tag != this.targetFilterable.SelectedTag)
		{
			this.SetSelectedItem(this.targetFilterable.SelectedTag);
		}
		if (this.targetFilterable.SelectedTag != GameTags.Void)
		{
			this.currentSelectionLabel.text = string.Format(locString, this.targetFilterable.SelectedTag.ProperName());
			return;
		}
		this.currentSelectionLabel.text = UI.UISIDESCREENS.FILTERSIDESCREEN.NO_SELECTION;
	}

	// Token: 0x04004DA0 RID: 19872
	public HierarchyReferences categoryFoldoutPrefab;

	// Token: 0x04004DA1 RID: 19873
	public RectTransform elementEntryContainer;

	// Token: 0x04004DA2 RID: 19874
	public Image outputIcon;

	// Token: 0x04004DA3 RID: 19875
	public Image everythingElseIcon;

	// Token: 0x04004DA4 RID: 19876
	public LocText outputElementHeaderLabel;

	// Token: 0x04004DA5 RID: 19877
	public LocText everythingElseHeaderLabel;

	// Token: 0x04004DA6 RID: 19878
	public LocText selectElementHeaderLabel;

	// Token: 0x04004DA7 RID: 19879
	public LocText currentSelectionLabel;

	// Token: 0x04004DA8 RID: 19880
	private SingleItemSelectionRow voidRow;

	// Token: 0x04004DA9 RID: 19881
	public bool isLogicFilter;

	// Token: 0x04004DAA RID: 19882
	private Filterable targetFilterable;
}
