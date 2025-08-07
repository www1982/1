using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D2A RID: 3370
public class CrewJobsScreen : CrewListScreen<CrewJobsEntry>
{
	// Token: 0x0600680E RID: 26638 RVA: 0x00273F40 File Offset: 0x00272140
	protected override void OnActivate()
	{
		CrewJobsScreen.Instance = this;
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.choreGroups.Add(choreGroup);
		}
		base.OnActivate();
	}

	// Token: 0x0600680F RID: 26639 RVA: 0x00273FB0 File Offset: 0x002721B0
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		base.RefreshCrewPortraitContent();
		this.SortByPreviousSelected();
	}

	// Token: 0x06006810 RID: 26640 RVA: 0x00273FC4 File Offset: 0x002721C4
	protected override void OnForcedCleanUp()
	{
		CrewJobsScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06006811 RID: 26641 RVA: 0x00273FD4 File Offset: 0x002721D4
	protected override void SpawnEntries()
	{
		base.SpawnEntries();
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			CrewJobsEntry component = Util.KInstantiateUI(this.Prefab_CrewEntry, this.EntriesPanelTransform.gameObject, false).GetComponent<CrewJobsEntry>();
			component.Populate(minionIdentity);
			this.EntryObjects.Add(component);
		}
		this.SortEveryoneToggle.group = this.sortToggleGroup;
		ImageToggleState toggleImage = this.SortEveryoneToggle.GetComponentInChildren<ImageToggleState>(true);
		this.SortEveryoneToggle.onValueChanged.AddListener(delegate(bool value)
		{
			this.SortByName(!this.SortEveryoneToggle.isOn);
			this.lastSortToggle = this.SortEveryoneToggle;
			this.lastSortReversed = !this.SortEveryoneToggle.isOn;
			this.ResetSortToggles(this.SortEveryoneToggle);
			if (this.SortEveryoneToggle.isOn)
			{
				toggleImage.SetActive();
				return;
			}
			toggleImage.SetInactive();
		});
		this.SortByPreviousSelected();
		this.dirty = true;
	}

	// Token: 0x06006812 RID: 26642 RVA: 0x002740B4 File Offset: 0x002722B4
	private void SortByPreviousSelected()
	{
		if (this.sortToggleGroup == null || this.lastSortToggle == null)
		{
			return;
		}
		int childCount = this.ColumnTitlesContainer.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (i < this.choreGroups.Count && this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>() == this.lastSortToggle)
			{
				this.SortByEffectiveness(this.choreGroups[i], this.lastSortReversed, false);
				return;
			}
		}
		if (this.SortEveryoneToggle == this.lastSortToggle)
		{
			base.SortByName(this.lastSortReversed);
		}
	}

	// Token: 0x06006813 RID: 26643 RVA: 0x00274168 File Offset: 0x00272368
	protected override void PositionColumnTitles()
	{
		base.PositionColumnTitles();
		int childCount = this.ColumnTitlesContainer.childCount;
		for (int i = 0; i < childCount; i++)
		{
			if (i < this.choreGroups.Count)
			{
				Toggle sortToggle = this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>();
				this.ColumnTitlesContainer.GetChild(i).rectTransform().localScale = Vector3.one;
				ChoreGroup chore_group2 = this.choreGroups[i];
				ImageToggleState toggleImage = sortToggle.GetComponentInChildren<ImageToggleState>(true);
				sortToggle.group = this.sortToggleGroup;
				sortToggle.onValueChanged.AddListener(delegate(bool value)
				{
					bool flag = false;
					if (this.lastSortToggle == sortToggle)
					{
						flag = true;
					}
					this.SortByEffectiveness(chore_group2, !sortToggle.isOn, flag);
					this.lastSortToggle = sortToggle;
					this.lastSortReversed = !sortToggle.isOn;
					this.ResetSortToggles(sortToggle);
					if (sortToggle.isOn)
					{
						toggleImage.SetActive();
						return;
					}
					toggleImage.SetInactive();
				});
			}
			ToolTip JobTooltip = this.ColumnTitlesContainer.GetChild(i).GetComponent<ToolTip>();
			ToolTip jobTooltip = JobTooltip;
			jobTooltip.OnToolTip = (Func<string>)Delegate.Combine(jobTooltip.OnToolTip, new Func<string>(() => this.GetJobTooltip(JobTooltip.gameObject)));
			Button componentInChildren = this.ColumnTitlesContainer.GetChild(i).GetComponentInChildren<Button>();
			this.EveryoneToggles.Add(componentInChildren, CrewJobsScreen.everyoneToggleState.on);
		}
		for (int j = 0; j < this.choreGroups.Count; j++)
		{
			ChoreGroup chore_group = this.choreGroups[j];
			Button b = this.EveryoneToggles.Keys.ElementAt(j);
			this.EveryoneToggles.Keys.ElementAt(j).onClick.AddListener(delegate
			{
				this.ToggleJobEveryone(b, chore_group);
			});
		}
		Button key = this.EveryoneToggles.ElementAt(this.EveryoneToggles.Count - 1).Key;
		key.transform.parent.Find("Title").gameObject.GetComponentInChildren<Toggle>().gameObject.SetActive(false);
		key.onClick.AddListener(delegate
		{
			this.ToggleAllTasksEveryone();
		});
		this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(key, this.EveryoneAllTaskToggle.Value);
	}

	// Token: 0x06006814 RID: 26644 RVA: 0x002743B4 File Offset: 0x002725B4
	private string GetJobTooltip(GameObject go)
	{
		ToolTip component = go.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		OverviewColumnIdentity component2 = go.GetComponent<OverviewColumnIdentity>();
		if (component2.columnID != "AllTasks")
		{
			ChoreGroup choreGroup = Db.Get().ChoreGroups.Get(component2.columnID);
			component.AddMultiStringTooltip(component2.Column_DisplayName, this.TextStyle_JobTooltip_Title);
			component.AddMultiStringTooltip(choreGroup.description, this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip("\n", this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip(UI.TOOLTIPS.JOBSSCREEN_ATTRIBUTES, this.TextStyle_JobTooltip_Description);
			component.AddMultiStringTooltip("•  " + choreGroup.attribute.Name, this.TextStyle_JobTooltip_RelevantAttributes);
		}
		return "";
	}

	// Token: 0x06006815 RID: 26645 RVA: 0x00274474 File Offset: 0x00272674
	private void ToggleAllTasksEveryone()
	{
		string text = "HUD_Click_Deselect";
		if (this.EveryoneAllTaskToggle.Value != CrewJobsScreen.everyoneToggleState.on)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		for (int i = 0; i < this.choreGroups.Count; i++)
		{
			this.SetJobEveryone(this.EveryoneAllTaskToggle.Value != CrewJobsScreen.everyoneToggleState.on, this.choreGroups[i]);
		}
	}

	// Token: 0x06006816 RID: 26646 RVA: 0x002744E0 File Offset: 0x002726E0
	private void SetJobEveryone(Button button, ChoreGroup chore_group)
	{
		this.SetJobEveryone(this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on, chore_group);
	}

	// Token: 0x06006817 RID: 26647 RVA: 0x002744FC File Offset: 0x002726FC
	private void SetJobEveryone(bool state, ChoreGroup chore_group)
	{
		foreach (CrewJobsEntry crewJobsEntry in this.EntryObjects)
		{
			crewJobsEntry.consumer.SetPermittedByUser(chore_group, state);
		}
	}

	// Token: 0x06006818 RID: 26648 RVA: 0x00274554 File Offset: 0x00272754
	private void ToggleJobEveryone(Button button, ChoreGroup chore_group)
	{
		string text = "HUD_Click_Deselect";
		if (this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		foreach (CrewJobsEntry crewJobsEntry in this.EntryObjects)
		{
			crewJobsEntry.consumer.SetPermittedByUser(chore_group, this.EveryoneToggles[button] != CrewJobsScreen.everyoneToggleState.on);
		}
	}

	// Token: 0x06006819 RID: 26649 RVA: 0x002745E4 File Offset: 0x002727E4
	private void SortByEffectiveness(ChoreGroup chore_group, bool reverse, bool playSound)
	{
		if (playSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
		}
		List<CrewJobsEntry> list = new List<CrewJobsEntry>(this.EntryObjects);
		list.Sort(delegate(CrewJobsEntry a, CrewJobsEntry b)
		{
			float value = a.Identity.GetAttributes().GetValue(chore_group.attribute.Id);
			float value2 = b.Identity.GetAttributes().GetValue(chore_group.attribute.Id);
			return value.CompareTo(value2);
		});
		base.ReorderEntries(list, reverse);
	}

	// Token: 0x0600681A RID: 26650 RVA: 0x00274638 File Offset: 0x00272838
	private void ResetSortToggles(Toggle exceptToggle)
	{
		for (int i = 0; i < this.ColumnTitlesContainer.childCount; i++)
		{
			Toggle componentInChildren = this.ColumnTitlesContainer.GetChild(i).Find("Title").GetComponentInChildren<Toggle>();
			if (!(componentInChildren == null))
			{
				ImageToggleState componentInChildren2 = componentInChildren.GetComponentInChildren<ImageToggleState>(true);
				if (componentInChildren != exceptToggle)
				{
					componentInChildren2.SetDisabled();
				}
			}
		}
		ImageToggleState componentInChildren3 = this.SortEveryoneToggle.GetComponentInChildren<ImageToggleState>(true);
		if (this.SortEveryoneToggle != exceptToggle)
		{
			componentInChildren3.SetDisabled();
		}
	}

	// Token: 0x0600681B RID: 26651 RVA: 0x002746B8 File Offset: 0x002728B8
	private void Refresh()
	{
		if (this.dirty)
		{
			int childCount = this.ColumnTitlesContainer.childCount;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < childCount; i++)
			{
				bool flag3 = false;
				bool flag4 = false;
				if (this.choreGroups.Count - 1 >= i)
				{
					ChoreGroup choreGroup = this.choreGroups[i];
					for (int j = 0; j < this.EntryObjects.Count; j++)
					{
						ChoreConsumer consumer = this.EntryObjects[j].GetComponent<CrewJobsEntry>().consumer;
						if (consumer.IsPermittedByTraits(choreGroup))
						{
							if (consumer.IsPermittedByUser(choreGroup))
							{
								flag3 = true;
								flag = true;
							}
							else
							{
								flag4 = true;
								flag2 = true;
							}
						}
					}
					if (flag3 && flag4)
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.mixed;
					}
					else if (flag3)
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.on;
					}
					else
					{
						this.EveryoneToggles[this.EveryoneToggles.ElementAt(i).Key] = CrewJobsScreen.everyoneToggleState.off;
					}
					Button componentInChildren = this.ColumnTitlesContainer.GetChild(i).GetComponentInChildren<Button>();
					ImageToggleState component = componentInChildren.GetComponentsInChildren<Image>(true)[1].GetComponent<ImageToggleState>();
					switch (this.EveryoneToggles[componentInChildren])
					{
					case CrewJobsScreen.everyoneToggleState.off:
						component.SetDisabled();
						break;
					case CrewJobsScreen.everyoneToggleState.mixed:
						component.SetInactive();
						break;
					case CrewJobsScreen.everyoneToggleState.on:
						component.SetActive();
						break;
					}
				}
			}
			if (flag && flag2)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.mixed);
			}
			else if (flag)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.on);
			}
			else if (flag2)
			{
				this.EveryoneAllTaskToggle = new KeyValuePair<Button, CrewJobsScreen.everyoneToggleState>(this.EveryoneAllTaskToggle.Key, CrewJobsScreen.everyoneToggleState.off);
			}
			ImageToggleState component2 = this.EveryoneAllTaskToggle.Key.GetComponentsInChildren<Image>(true)[1].GetComponent<ImageToggleState>();
			switch (this.EveryoneAllTaskToggle.Value)
			{
			case CrewJobsScreen.everyoneToggleState.off:
				component2.SetDisabled();
				break;
			case CrewJobsScreen.everyoneToggleState.mixed:
				component2.SetInactive();
				break;
			case CrewJobsScreen.everyoneToggleState.on:
				component2.SetActive();
				break;
			}
			this.screenWidth = this.EntriesPanelTransform.rectTransform().sizeDelta.x;
			this.ScrollRectTransform.GetComponent<LayoutElement>().minWidth = this.screenWidth;
			float num = 31f;
			base.GetComponent<LayoutElement>().minWidth = this.screenWidth + num;
			this.dirty = false;
		}
	}

	// Token: 0x0600681C RID: 26652 RVA: 0x00274943 File Offset: 0x00272B43
	private void Update()
	{
		this.Refresh();
	}

	// Token: 0x0600681D RID: 26653 RVA: 0x0027494B File Offset: 0x00272B4B
	public void Dirty(object data = null)
	{
		this.dirty = true;
	}

	// Token: 0x04004763 RID: 18275
	public static CrewJobsScreen Instance;

	// Token: 0x04004764 RID: 18276
	private Dictionary<Button, CrewJobsScreen.everyoneToggleState> EveryoneToggles = new Dictionary<Button, CrewJobsScreen.everyoneToggleState>();

	// Token: 0x04004765 RID: 18277
	private KeyValuePair<Button, CrewJobsScreen.everyoneToggleState> EveryoneAllTaskToggle;

	// Token: 0x04004766 RID: 18278
	public TextStyleSetting TextStyle_JobTooltip_Title;

	// Token: 0x04004767 RID: 18279
	public TextStyleSetting TextStyle_JobTooltip_Description;

	// Token: 0x04004768 RID: 18280
	public TextStyleSetting TextStyle_JobTooltip_RelevantAttributes;

	// Token: 0x04004769 RID: 18281
	public Toggle SortEveryoneToggle;

	// Token: 0x0400476A RID: 18282
	private List<ChoreGroup> choreGroups = new List<ChoreGroup>();

	// Token: 0x0400476B RID: 18283
	private bool dirty;

	// Token: 0x0400476C RID: 18284
	private float screenWidth;

	// Token: 0x02001EF9 RID: 7929
	public enum everyoneToggleState
	{
		// Token: 0x04008F74 RID: 36724
		off,
		// Token: 0x04008F75 RID: 36725
		mixed,
		// Token: 0x04008F76 RID: 36726
		on
	}
}
