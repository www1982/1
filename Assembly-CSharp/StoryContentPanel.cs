using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.CustomSettings;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E5D RID: 3677
public class StoryContentPanel : KMonoBehaviour
{
	// Token: 0x0600753B RID: 30011 RVA: 0x002CE2AC File Offset: 0x002CC4AC
	public List<string> GetActiveStories()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x0600753C RID: 30012 RVA: 0x002CE318 File Offset: 0x002CC518
	public void Init()
	{
		this.SpawnRows();
		this.RefreshRows();
		this.RefreshDescriptionPanel();
		this.SelectDefault();
		CustomGameSettings.Instance.OnStorySettingChanged += this.OnStorySettingChanged;
	}

	// Token: 0x0600753D RID: 30013 RVA: 0x002CE348 File Offset: 0x002CC548
	public void Cleanup()
	{
		CustomGameSettings.Instance.OnStorySettingChanged -= this.OnStorySettingChanged;
	}

	// Token: 0x0600753E RID: 30014 RVA: 0x002CE360 File Offset: 0x002CC560
	private void OnStorySettingChanged(SettingConfig config, SettingLevel level)
	{
		this.storyStates[config.id] = ((level.id == "Guaranteed") ? StoryContentPanel.StoryState.Guaranteed : StoryContentPanel.StoryState.Forbidden);
		this.RefreshStoryDisplay(config.id);
	}

	// Token: 0x0600753F RID: 30015 RVA: 0x002CE398 File Offset: 0x002CC598
	private void SpawnRows()
	{
		using (List<Story>.Enumerator enumerator = Db.Get().Stories.resources.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Story story = enumerator.Current;
				GameObject gameObject = global::Util.KInstantiateUI(this.storyRowPrefab, this.storyRowContainer, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Label").SetText(Strings.Get(story.StoryTrait.name));
				MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
				component2.onClick = (global::System.Action)Delegate.Combine(component2.onClick, new global::System.Action(delegate
				{
					this.SelectRow(story.Id);
				}));
				this.storyRows.Add(story.Id, gameObject);
				component.GetReference<Image>("Icon").sprite = Assets.GetSprite(story.StoryTrait.icon);
				MultiToggle reference = component.GetReference<MultiToggle>("checkbox");
				reference.onClick = (global::System.Action)Delegate.Combine(reference.onClick, new global::System.Action(delegate
				{
					this.IncrementStorySetting(story.Id, true);
					this.RefreshStoryDisplay(story.Id);
				}));
				this.storyStates.Add(story.Id, this._defaultStoryState);
			}
		}
		this.RefreshAllStoryStates();
		this.mainScreen.RefreshStoryLabel();
	}

	// Token: 0x06007540 RID: 30016 RVA: 0x002CE514 File Offset: 0x002CC714
	private void SelectRow(string id)
	{
		this.selectedStoryId = id;
		this.RefreshRows();
		this.RefreshDescriptionPanel();
	}

	// Token: 0x06007541 RID: 30017 RVA: 0x002CE52C File Offset: 0x002CC72C
	public void SelectDefault()
	{
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				this.SelectRow(keyValuePair.Key);
				return;
			}
		}
		using (Dictionary<string, StoryContentPanel.StoryState>.Enumerator enumerator = this.storyStates.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair2 = enumerator.Current;
				this.SelectRow(keyValuePair2.Key);
			}
		}
	}

	// Token: 0x06007542 RID: 30018 RVA: 0x002CE5DC File Offset: 0x002CC7DC
	private void IncrementStorySetting(string storyId, bool forward = true)
	{
		int num = (int)this.storyStates[storyId];
		num += (forward ? 1 : (-1));
		if (num < 0)
		{
			num += 2;
		}
		num %= 2;
		this.SetStoryState(storyId, (StoryContentPanel.StoryState)num);
		this.mainScreen.RefreshRowsAndDescriptions();
	}

	// Token: 0x06007543 RID: 30019 RVA: 0x002CE620 File Offset: 0x002CC820
	private void SetStoryState(string storyId, StoryContentPanel.StoryState state)
	{
		this.storyStates[storyId] = state;
		SettingConfig settingConfig = CustomGameSettings.Instance.StorySettings[storyId];
		CustomGameSettings.Instance.SetStorySetting(settingConfig, this.storyStates[storyId] == StoryContentPanel.StoryState.Guaranteed);
	}

	// Token: 0x06007544 RID: 30020 RVA: 0x002CE668 File Offset: 0x002CC868
	public void SelectRandomStories(int min = 5, int max = 5, bool useBias = false)
	{
		int num = global::UnityEngine.Random.Range(min, max);
		List<Story> list = new List<Story>(Db.Get().Stories.resources);
		List<Story> list2 = new List<Story>();
		list.Shuffle<Story>();
		int num2 = 0;
		while (num2 < num && list.Count - 1 >= num2)
		{
			list2.Add(list[num2]);
			num2++;
		}
		float num3 = 0.7f;
		int num4 = list2.Count((Story x) => x.IsNew());
		if (useBias && num4 == 0 && global::UnityEngine.Random.value < num3)
		{
			List<Story> list3 = Db.Get().Stories.resources.Where((Story x) => x.IsNew()).ToList<Story>();
			list3.Shuffle<Story>();
			if (list3.Count > 0)
			{
				list2.RemoveAt(0);
				list2.Add(list3[0]);
			}
		}
		foreach (Story story in list)
		{
			this.SetStoryState(story.Id, list2.Contains(story) ? StoryContentPanel.StoryState.Guaranteed : StoryContentPanel.StoryState.Forbidden);
		}
		this.RefreshAllStoryStates();
		this.mainScreen.RefreshRowsAndDescriptions();
	}

	// Token: 0x06007545 RID: 30021 RVA: 0x002CE7CC File Offset: 0x002CC9CC
	private void RefreshAllStoryStates()
	{
		foreach (string text in this.storyRows.Keys)
		{
			this.RefreshStoryDisplay(text);
		}
	}

	// Token: 0x06007546 RID: 30022 RVA: 0x002CE824 File Offset: 0x002CCA24
	private void RefreshStoryDisplay(string id)
	{
		MultiToggle reference = this.storyRows[id].GetComponent<HierarchyReferences>().GetReference<MultiToggle>("checkbox");
		StoryContentPanel.StoryState storyState = this.storyStates[id];
		if (storyState == StoryContentPanel.StoryState.Forbidden)
		{
			reference.ChangeState(0);
			return;
		}
		if (storyState != StoryContentPanel.StoryState.Guaranteed)
		{
			return;
		}
		reference.ChangeState(1);
	}

	// Token: 0x06007547 RID: 30023 RVA: 0x002CE874 File Offset: 0x002CCA74
	private void RefreshRows()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.storyRows)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState((keyValuePair.Key == this.selectedStoryId) ? 1 : 0);
		}
	}

	// Token: 0x06007548 RID: 30024 RVA: 0x002CE8EC File Offset: 0x002CCAEC
	private void RefreshDescriptionPanel()
	{
		if (this.selectedStoryId.IsNullOrWhiteSpace())
		{
			this.selectedStoryTitleLabel.SetText("");
			this.selectedStoryDescriptionLabel.SetText("");
			return;
		}
		WorldTrait storyTrait = Db.Get().Stories.GetStoryTrait(this.selectedStoryId, true);
		this.selectedStoryTitleLabel.SetText(Strings.Get(storyTrait.name));
		this.selectedStoryDescriptionLabel.SetText(Strings.Get(storyTrait.description));
		string text = storyTrait.icon.Replace("_icon", "_image");
		this.selectedStoryImage.sprite = Assets.GetSprite(text);
	}

	// Token: 0x06007549 RID: 30025 RVA: 0x002CE9A0 File Offset: 0x002CCBA0
	public string GetTraitsString(bool tooltip = false)
	{
		int num = 0;
		int num2 = 5;
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				num++;
			}
		}
		string text = UI.FRONTEND.COLONYDESTINATIONSCREEN.STORY_TRAITS_HEADER;
		string text2;
		if (num != 0)
		{
			if (num != 1)
			{
				text2 = string.Format(UI.FRONTEND.COLONYDESTINATIONSCREEN.TRAIT_COUNT, num);
			}
			else
			{
				text2 = UI.FRONTEND.COLONYDESTINATIONSCREEN.SINGLE_TRAIT;
			}
		}
		else
		{
			text2 = UI.FRONTEND.COLONYDESTINATIONSCREEN.NO_TRAITS;
		}
		text = text + ": " + text2;
		if (num > num2)
		{
			text = text + " " + UI.FRONTEND.COLONYDESTINATIONSCREEN.TOO_MANY_TRAITS_WARNING;
		}
		if (tooltip)
		{
			foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair2 in this.storyStates)
			{
				if (keyValuePair2.Value == StoryContentPanel.StoryState.Guaranteed)
				{
					WorldTrait storyTrait = Db.Get().Stories.Get(keyValuePair2.Key).StoryTrait;
					text = string.Concat(new string[]
					{
						text,
						"\n\n<b>",
						Strings.Get(storyTrait.name).String,
						"</b>\n",
						Strings.Get(storyTrait.description).String
					});
				}
			}
			if (num > num2)
			{
				text = text + "\n\n" + UI.FRONTEND.COLONYDESTINATIONSCREEN.TOO_MANY_TRAITS_WARNING_TOOLTIP;
			}
		}
		return text;
	}

	// Token: 0x04005146 RID: 20806
	[SerializeField]
	private GameObject storyRowPrefab;

	// Token: 0x04005147 RID: 20807
	[SerializeField]
	private GameObject storyRowContainer;

	// Token: 0x04005148 RID: 20808
	private Dictionary<string, GameObject> storyRows = new Dictionary<string, GameObject>();

	// Token: 0x04005149 RID: 20809
	public const int DEFAULT_RANDOMIZE_STORY_COUNT = 5;

	// Token: 0x0400514A RID: 20810
	private Dictionary<string, StoryContentPanel.StoryState> storyStates = new Dictionary<string, StoryContentPanel.StoryState>();

	// Token: 0x0400514B RID: 20811
	private string selectedStoryId = "";

	// Token: 0x0400514C RID: 20812
	[SerializeField]
	private ColonyDestinationSelectScreen mainScreen;

	// Token: 0x0400514D RID: 20813
	[Header("Trait Count")]
	[Header("SelectedStory")]
	[SerializeField]
	private Image selectedStoryImage;

	// Token: 0x0400514E RID: 20814
	[SerializeField]
	private LocText selectedStoryTitleLabel;

	// Token: 0x0400514F RID: 20815
	[SerializeField]
	private LocText selectedStoryDescriptionLabel;

	// Token: 0x04005150 RID: 20816
	[SerializeField]
	private Sprite spriteForbidden;

	// Token: 0x04005151 RID: 20817
	[SerializeField]
	private Sprite spritePossible;

	// Token: 0x04005152 RID: 20818
	[SerializeField]
	private Sprite spriteGuaranteed;

	// Token: 0x04005153 RID: 20819
	private StoryContentPanel.StoryState _defaultStoryState;

	// Token: 0x04005154 RID: 20820
	private List<string> storyTraitSettings = new List<string> { "None", "Few", "Lots" };

	// Token: 0x0200205B RID: 8283
	private enum StoryState
	{
		// Token: 0x040093C8 RID: 37832
		Forbidden,
		// Token: 0x040093C9 RID: 37833
		Guaranteed,
		// Token: 0x040093CA RID: 37834
		LENGTH
	}
}
