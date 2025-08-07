using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D29 RID: 3369
public class CrewJobsEntry : CrewListEntry
{
	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x06006800 RID: 26624 RVA: 0x0027369E File Offset: 0x0027189E
	// (set) Token: 0x06006801 RID: 26625 RVA: 0x002736A6 File Offset: 0x002718A6
	public ChoreConsumer consumer { get; private set; }

	// Token: 0x06006802 RID: 26626 RVA: 0x002736B0 File Offset: 0x002718B0
	public override void Populate(MinionIdentity _identity)
	{
		base.Populate(_identity);
		this.consumer = _identity.GetComponent<ChoreConsumer>();
		ChoreConsumer consumer = this.consumer;
		consumer.choreRulesChanged = (global::System.Action)Delegate.Combine(consumer.choreRulesChanged, new global::System.Action(this.Dirty));
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.CreateChoreButton(choreGroup);
		}
		this.CreateAllTaskButton();
		this.dirty = true;
	}

	// Token: 0x06006803 RID: 26627 RVA: 0x00273754 File Offset: 0x00271954
	private void CreateChoreButton(ChoreGroup chore_group)
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_JobPriorityButton, base.transform.gameObject, false);
		gameObject.GetComponent<OverviewColumnIdentity>().columnID = chore_group.Id;
		gameObject.GetComponent<OverviewColumnIdentity>().Column_DisplayName = chore_group.Name;
		CrewJobsEntry.PriorityButton priorityButton = default(CrewJobsEntry.PriorityButton);
		priorityButton.button = gameObject.GetComponent<Button>();
		priorityButton.border = gameObject.transform.GetChild(1).GetComponent<Image>();
		priorityButton.baseBorderColor = priorityButton.border.color;
		priorityButton.background = gameObject.transform.GetChild(0).GetComponent<Image>();
		priorityButton.baseBackgroundColor = priorityButton.background.color;
		priorityButton.choreGroup = chore_group;
		priorityButton.ToggleIcon = gameObject.transform.GetChild(2).gameObject;
		priorityButton.tooltip = gameObject.GetComponent<ToolTip>();
		priorityButton.tooltip.OnToolTip = () => this.OnPriorityButtonTooltip(priorityButton);
		priorityButton.button.onClick.AddListener(delegate
		{
			this.OnPriorityPress(chore_group);
		});
		this.PriorityButtons.Add(priorityButton);
	}

	// Token: 0x06006804 RID: 26628 RVA: 0x002738D0 File Offset: 0x00271AD0
	private void CreateAllTaskButton()
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_JobPriorityButtonAllTasks, base.transform.gameObject, false);
		gameObject.GetComponent<OverviewColumnIdentity>().columnID = "AllTasks";
		gameObject.GetComponent<OverviewColumnIdentity>().Column_DisplayName = "";
		Button b = gameObject.GetComponent<Button>();
		b.onClick.AddListener(delegate
		{
			this.ToggleTasksAll(b);
		});
		CrewJobsEntry.PriorityButton priorityButton = default(CrewJobsEntry.PriorityButton);
		priorityButton.button = gameObject.GetComponent<Button>();
		priorityButton.border = gameObject.transform.GetChild(1).GetComponent<Image>();
		priorityButton.baseBorderColor = priorityButton.border.color;
		priorityButton.background = gameObject.transform.GetChild(0).GetComponent<Image>();
		priorityButton.baseBackgroundColor = priorityButton.background.color;
		priorityButton.ToggleIcon = gameObject.transform.GetChild(2).gameObject;
		priorityButton.tooltip = gameObject.GetComponent<ToolTip>();
		this.AllTasksButton = priorityButton;
	}

	// Token: 0x06006805 RID: 26629 RVA: 0x002739E0 File Offset: 0x00271BE0
	private void ToggleTasksAll(Button button)
	{
		bool flag = this.rowToggleState != CrewJobsScreen.everyoneToggleState.on;
		string text = "HUD_Click_Deselect";
		if (flag)
		{
			text = "HUD_Click";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.consumer.SetPermittedByUser(choreGroup, flag);
		}
	}

	// Token: 0x06006806 RID: 26630 RVA: 0x00273A6C File Offset: 0x00271C6C
	private void OnPriorityPress(ChoreGroup chore_group)
	{
		bool flag = this.consumer.IsPermittedByUser(chore_group);
		string text = "HUD_Click";
		if (flag)
		{
			text = "HUD_Click_Deselect";
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound(text, false));
		this.consumer.SetPermittedByUser(chore_group, !this.consumer.IsPermittedByUser(chore_group));
	}

	// Token: 0x06006807 RID: 26631 RVA: 0x00273AC0 File Offset: 0x00271CC0
	private void Refresh(object data = null)
	{
		if (this.identity == null)
		{
			this.dirty = false;
			return;
		}
		if (this.dirty)
		{
			Attributes attributes = this.identity.GetAttributes();
			foreach (CrewJobsEntry.PriorityButton priorityButton in this.PriorityButtons)
			{
				bool flag = this.consumer.IsPermittedByUser(priorityButton.choreGroup);
				if (priorityButton.ToggleIcon.activeSelf != flag)
				{
					priorityButton.ToggleIcon.SetActive(flag);
				}
				float num = Mathf.Min(attributes.Get(priorityButton.choreGroup.attribute).GetTotalValue() / 10f, 1f);
				Color baseBorderColor = priorityButton.baseBorderColor;
				baseBorderColor.r = Mathf.Lerp(priorityButton.baseBorderColor.r, 0.72156864f, num);
				baseBorderColor.g = Mathf.Lerp(priorityButton.baseBorderColor.g, 0.44313726f, num);
				baseBorderColor.b = Mathf.Lerp(priorityButton.baseBorderColor.b, 0.5803922f, num);
				if (priorityButton.border.color != baseBorderColor)
				{
					priorityButton.border.color = baseBorderColor;
				}
				Color color = priorityButton.baseBackgroundColor;
				color.a = Mathf.Lerp(0f, 1f, num);
				bool flag2 = this.consumer.IsPermittedByTraits(priorityButton.choreGroup);
				if (!flag2)
				{
					color = Color.clear;
					priorityButton.border.color = Color.clear;
					priorityButton.ToggleIcon.SetActive(false);
				}
				priorityButton.button.interactable = flag2;
				if (priorityButton.background.color != color)
				{
					priorityButton.background.color = color;
				}
			}
			int num2 = 0;
			int num3 = 0;
			foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
			{
				if (this.consumer.IsPermittedByTraits(choreGroup))
				{
					num3++;
					if (this.consumer.IsPermittedByUser(choreGroup))
					{
						num2++;
					}
				}
			}
			if (num2 == 0)
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.off;
			}
			else if (num2 < num3)
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.mixed;
			}
			else
			{
				this.rowToggleState = CrewJobsScreen.everyoneToggleState.on;
			}
			ImageToggleState component = this.AllTasksButton.ToggleIcon.GetComponent<ImageToggleState>();
			switch (this.rowToggleState)
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
			this.dirty = false;
		}
	}

	// Token: 0x06006808 RID: 26632 RVA: 0x00273DB0 File Offset: 0x00271FB0
	private string OnPriorityButtonTooltip(CrewJobsEntry.PriorityButton b)
	{
		b.tooltip.ClearMultiStringTooltip();
		if (this.identity != null)
		{
			Attributes attributes = this.identity.GetAttributes();
			if (attributes != null)
			{
				if (!this.consumer.IsPermittedByTraits(b.choreGroup))
				{
					string text = string.Format(UI.TOOLTIPS.JOBSSCREEN_CANNOTPERFORMTASK, this.consumer.GetComponent<MinionIdentity>().GetProperName());
					b.tooltip.AddMultiStringTooltip(text, this.TooltipTextStyle_AbilityNegativeModifier);
					return "";
				}
				b.tooltip.AddMultiStringTooltip(UI.TOOLTIPS.JOBSSCREEN_RELEVANT_ATTRIBUTES, this.TooltipTextStyle_Ability);
				Klei.AI.Attribute attribute = b.choreGroup.attribute;
				AttributeInstance attributeInstance = attributes.Get(attribute);
				float totalValue = attributeInstance.GetTotalValue();
				TextStyleSetting textStyleSetting = this.TooltipTextStyle_Ability;
				if (totalValue > 0f)
				{
					textStyleSetting = this.TooltipTextStyle_AbilityPositiveModifier;
				}
				else if (totalValue < 0f)
				{
					textStyleSetting = this.TooltipTextStyle_AbilityNegativeModifier;
				}
				b.tooltip.AddMultiStringTooltip(attribute.Name + " " + attributeInstance.GetTotalValue().ToString(), textStyleSetting);
			}
		}
		return "";
	}

	// Token: 0x06006809 RID: 26633 RVA: 0x00273EC9 File Offset: 0x002720C9
	private void LateUpdate()
	{
		this.Refresh(null);
	}

	// Token: 0x0600680A RID: 26634 RVA: 0x00273ED2 File Offset: 0x002720D2
	private void OnLevelUp(object data)
	{
		this.Dirty();
	}

	// Token: 0x0600680B RID: 26635 RVA: 0x00273EDA File Offset: 0x002720DA
	private void Dirty()
	{
		this.dirty = true;
		CrewJobsScreen.Instance.Dirty(null);
	}

	// Token: 0x0600680C RID: 26636 RVA: 0x00273EEE File Offset: 0x002720EE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.consumer != null)
		{
			ChoreConsumer consumer = this.consumer;
			consumer.choreRulesChanged = (global::System.Action)Delegate.Remove(consumer.choreRulesChanged, new global::System.Action(this.Dirty));
		}
	}

	// Token: 0x04004758 RID: 18264
	public GameObject Prefab_JobPriorityButton;

	// Token: 0x04004759 RID: 18265
	public GameObject Prefab_JobPriorityButtonAllTasks;

	// Token: 0x0400475A RID: 18266
	private List<CrewJobsEntry.PriorityButton> PriorityButtons = new List<CrewJobsEntry.PriorityButton>();

	// Token: 0x0400475B RID: 18267
	private CrewJobsEntry.PriorityButton AllTasksButton;

	// Token: 0x0400475C RID: 18268
	public TextStyleSetting TooltipTextStyle_Title;

	// Token: 0x0400475D RID: 18269
	public TextStyleSetting TooltipTextStyle_Ability;

	// Token: 0x0400475E RID: 18270
	public TextStyleSetting TooltipTextStyle_AbilityPositiveModifier;

	// Token: 0x0400475F RID: 18271
	public TextStyleSetting TooltipTextStyle_AbilityNegativeModifier;

	// Token: 0x04004760 RID: 18272
	private bool dirty;

	// Token: 0x04004762 RID: 18274
	private CrewJobsScreen.everyoneToggleState rowToggleState;

	// Token: 0x02001EF6 RID: 7926
	[Serializable]
	public struct PriorityButton
	{
		// Token: 0x04008F66 RID: 36710
		public Button button;

		// Token: 0x04008F67 RID: 36711
		public GameObject ToggleIcon;

		// Token: 0x04008F68 RID: 36712
		public ChoreGroup choreGroup;

		// Token: 0x04008F69 RID: 36713
		public ToolTip tooltip;

		// Token: 0x04008F6A RID: 36714
		public Image border;

		// Token: 0x04008F6B RID: 36715
		public Image background;

		// Token: 0x04008F6C RID: 36716
		public Color baseBorderColor;

		// Token: 0x04008F6D RID: 36717
		public Color baseBackgroundColor;
	}
}
