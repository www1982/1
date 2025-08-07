using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D2F RID: 3375
public class CrewRationsScreen : CrewListScreen<CrewRationsEntry>
{
	// Token: 0x0600684F RID: 26703 RVA: 0x00275B2C File Offset: 0x00273D2C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.closebutton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
	}

	// Token: 0x06006850 RID: 26704 RVA: 0x00275B5E File Offset: 0x00273D5E
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		base.RefreshCrewPortraitContent();
		this.SortByPreviousSelected();
	}

	// Token: 0x06006851 RID: 26705 RVA: 0x00275B74 File Offset: 0x00273D74
	private void SortByPreviousSelected()
	{
		if (this.sortToggleGroup == null)
		{
			return;
		}
		if (this.lastSortToggle == null)
		{
			return;
		}
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			OverviewColumnIdentity component = this.ColumnTitlesContainer.GetChild(i).GetComponent<OverviewColumnIdentity>();
			if (this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>() == this.lastSortToggle)
			{
				if (component.columnID == "name")
				{
					base.SortByName(this.lastSortReversed);
				}
				if (component.columnID == "health")
				{
					this.SortByAmount("HitPoints", this.lastSortReversed);
				}
				if (component.columnID == "stress")
				{
					this.SortByAmount("Stress", this.lastSortReversed);
				}
				if (component.columnID == "calories")
				{
					this.SortByAmount("Calories", this.lastSortReversed);
				}
			}
		}
	}

	// Token: 0x06006852 RID: 26706 RVA: 0x00275C78 File Offset: 0x00273E78
	protected override void PositionColumnTitles()
	{
		base.PositionColumnTitles();
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			OverviewColumnIdentity component = this.ColumnTitlesContainer.GetChild(i).GetComponent<OverviewColumnIdentity>();
			if (component.Sortable)
			{
				Toggle toggle = this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>();
				toggle.group = this.sortToggleGroup;
				ImageToggleState toggleImage = toggle.GetComponentInChildren<ImageToggleState>(true);
				if (component.columnID == "name")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByName(!toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "health")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("HitPoints", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "stress")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("Stress", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
				if (component.columnID == "calories")
				{
					toggle.onValueChanged.AddListener(delegate(bool value)
					{
						this.SortByAmount("Calories", !toggle.isOn);
						this.lastSortToggle = toggle;
						this.lastSortReversed = !toggle.isOn;
						this.ResetSortToggles(toggle);
						if (toggle.isOn)
						{
							toggleImage.SetActive();
							return;
						}
						toggleImage.SetInactive();
					});
				}
			}
		}
	}

	// Token: 0x06006853 RID: 26707 RVA: 0x00275DC4 File Offset: 0x00273FC4
	protected override void SpawnEntries()
	{
		base.SpawnEntries();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			CrewRationsEntry component = Util.KInstantiateUI(this.Prefab_CrewEntry, this.EntriesPanelTransform.gameObject, false).GetComponent<CrewRationsEntry>();
			component.Populate(minionIdentity);
			this.EntryObjects.Add(component);
		}
		this.SortByPreviousSelected();
	}

	// Token: 0x06006854 RID: 26708 RVA: 0x00275E50 File Offset: 0x00274050
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		foreach (CrewRationsEntry crewRationsEntry in this.EntryObjects)
		{
			crewRationsEntry.Refresh();
		}
	}

	// Token: 0x06006855 RID: 26709 RVA: 0x00275EA8 File Offset: 0x002740A8
	private void SortByAmount(string amount_id, bool reverse)
	{
		List<CrewRationsEntry> list = new List<CrewRationsEntry>(this.EntryObjects);
		list.Sort(delegate(CrewRationsEntry a, CrewRationsEntry b)
		{
			float value = a.Identity.GetAmounts().GetValue(amount_id);
			float value2 = b.Identity.GetAmounts().GetValue(amount_id);
			return value.CompareTo(value2);
		});
		base.ReorderEntries(list, reverse);
	}

	// Token: 0x06006856 RID: 26710 RVA: 0x00275EE8 File Offset: 0x002740E8
	private void ResetSortToggles(Toggle exceptToggle)
	{
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			Toggle component = this.ColumnTitlesContainer.GetChild(i).GetComponent<Toggle>();
			ImageToggleState componentInChildren = component.GetComponentInChildren<ImageToggleState>(true);
			if (component != exceptToggle)
			{
				componentInChildren.SetDisabled();
			}
		}
	}

	// Token: 0x04004798 RID: 18328
	[SerializeField]
	private KButton closebutton;
}
