using System;
using System.Collections.Generic;
using FMOD.Studio;
using STRINGS;
using UnityEngine;

// Token: 0x02000D9F RID: 3487
public class PriorityScreen : KScreen
{
	// Token: 0x06006D23 RID: 27939 RVA: 0x00294D48 File Offset: 0x00292F48
	public void InstantiateButtons(Action<PrioritySetting> on_click, bool playSelectionSound = true)
	{
		this.onClick = on_click;
		for (int i = 1; i <= 9; i++)
		{
			int num = i;
			PriorityButton priorityButton = global::Util.KInstantiateUI<PriorityButton>(this.buttonPrefab_basic.gameObject, this.buttonPrefab_basic.transform.parent.gameObject, false);
			this.buttons_basic.Add(priorityButton);
			priorityButton.playSelectionSound = playSelectionSound;
			priorityButton.onClick = this.onClick;
			priorityButton.text.text = num.ToString();
			priorityButton.priority = new PrioritySetting(PriorityScreen.PriorityClass.basic, num);
			priorityButton.tooltip.SetSimpleTooltip(string.Format(UI.PRIORITYSCREEN.BASIC, num));
		}
		this.buttonPrefab_basic.gameObject.SetActive(false);
		this.button_emergency.playSelectionSound = playSelectionSound;
		this.button_emergency.onClick = this.onClick;
		this.button_emergency.priority = new PrioritySetting(PriorityScreen.PriorityClass.topPriority, 1);
		this.button_emergency.tooltip.SetSimpleTooltip(UI.PRIORITYSCREEN.TOP_PRIORITY);
		this.button_toggleHigh.gameObject.SetActive(false);
		this.PriorityMenuContainer.SetActive(true);
		this.button_priorityMenu.gameObject.SetActive(true);
		this.button_priorityMenu.onClick += this.PriorityButtonClicked;
		this.button_priorityMenu.GetComponent<ToolTip>().SetSimpleTooltip(UI.PRIORITYSCREEN.OPEN_JOBS_SCREEN);
		this.diagram.SetActive(false);
		this.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5), false);
	}

	// Token: 0x06006D24 RID: 27940 RVA: 0x00294EC9 File Offset: 0x002930C9
	private void OnClick(PrioritySetting priority)
	{
		if (this.onClick != null)
		{
			this.onClick(priority);
		}
	}

	// Token: 0x06006D25 RID: 27941 RVA: 0x00294EDF File Offset: 0x002930DF
	public void ShowDiagram(bool show)
	{
		this.diagram.SetActive(show);
	}

	// Token: 0x06006D26 RID: 27942 RVA: 0x00294EED File Offset: 0x002930ED
	public void ResetPriority()
	{
		this.SetScreenPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, 5), false);
	}

	// Token: 0x06006D27 RID: 27943 RVA: 0x00294EFD File Offset: 0x002930FD
	public void PriorityButtonClicked()
	{
		ManagementMenu.Instance.TogglePriorities();
	}

	// Token: 0x06006D28 RID: 27944 RVA: 0x00294F0C File Offset: 0x0029310C
	private void RefreshButton(PriorityButton b, PrioritySetting priority, bool play_sound)
	{
		if (b.priority == priority)
		{
			b.toggle.Select();
			b.toggle.isOn = true;
			if (play_sound)
			{
				b.toggle.soundPlayer.Play(0);
				return;
			}
		}
		else
		{
			b.toggle.isOn = false;
		}
	}

	// Token: 0x06006D29 RID: 27945 RVA: 0x00294F60 File Offset: 0x00293160
	public void SetScreenPriority(PrioritySetting priority, bool play_sound = false)
	{
		if (this.lastSelectedPriority == priority)
		{
			return;
		}
		this.lastSelectedPriority = priority;
		if (priority.priority_class == PriorityScreen.PriorityClass.high)
		{
			this.button_toggleHigh.isOn = true;
		}
		else if (priority.priority_class == PriorityScreen.PriorityClass.basic)
		{
			this.button_toggleHigh.isOn = false;
		}
		for (int i = 0; i < this.buttons_basic.Count; i++)
		{
			this.buttons_basic[i].priority = new PrioritySetting(this.button_toggleHigh.isOn ? PriorityScreen.PriorityClass.high : PriorityScreen.PriorityClass.basic, i + 1);
			this.buttons_basic[i].tooltip.SetSimpleTooltip(string.Format(this.button_toggleHigh.isOn ? UI.PRIORITYSCREEN.HIGH : UI.PRIORITYSCREEN.BASIC, i + 1));
			this.RefreshButton(this.buttons_basic[i], this.lastSelectedPriority, play_sound);
		}
		this.RefreshButton(this.button_emergency, this.lastSelectedPriority, play_sound);
	}

	// Token: 0x06006D2A RID: 27946 RVA: 0x00295061 File Offset: 0x00293261
	public PrioritySetting GetLastSelectedPriority()
	{
		return this.lastSelectedPriority;
	}

	// Token: 0x06006D2B RID: 27947 RVA: 0x0029506C File Offset: 0x0029326C
	public static void PlayPriorityConfirmSound(PrioritySetting priority)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("Priority_Tool_Confirm", false), Vector3.zero, 1f);
		if (eventInstance.isValid())
		{
			float num = 0f;
			if (priority.priority_class >= PriorityScreen.PriorityClass.high)
			{
				num += 10f;
			}
			if (priority.priority_class >= PriorityScreen.PriorityClass.topPriority)
			{
				num += 0f;
			}
			num += (float)priority.priority_value;
			eventInstance.setParameterByName("priority", num, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x04004A83 RID: 19075
	[SerializeField]
	protected PriorityButton buttonPrefab_basic;

	// Token: 0x04004A84 RID: 19076
	[SerializeField]
	protected GameObject EmergencyContainer;

	// Token: 0x04004A85 RID: 19077
	[SerializeField]
	protected PriorityButton button_emergency;

	// Token: 0x04004A86 RID: 19078
	[SerializeField]
	protected GameObject PriorityMenuContainer;

	// Token: 0x04004A87 RID: 19079
	[SerializeField]
	protected KButton button_priorityMenu;

	// Token: 0x04004A88 RID: 19080
	[SerializeField]
	protected KToggle button_toggleHigh;

	// Token: 0x04004A89 RID: 19081
	[SerializeField]
	protected GameObject diagram;

	// Token: 0x04004A8A RID: 19082
	protected List<PriorityButton> buttons_basic = new List<PriorityButton>();

	// Token: 0x04004A8B RID: 19083
	protected List<PriorityButton> buttons_emergency = new List<PriorityButton>();

	// Token: 0x04004A8C RID: 19084
	private PrioritySetting priority;

	// Token: 0x04004A8D RID: 19085
	private PrioritySetting lastSelectedPriority = new PrioritySetting(PriorityScreen.PriorityClass.basic, -1);

	// Token: 0x04004A8E RID: 19086
	private Action<PrioritySetting> onClick;

	// Token: 0x02001FA2 RID: 8098
	public enum PriorityClass
	{
		// Token: 0x04009198 RID: 37272
		idle = -1,
		// Token: 0x04009199 RID: 37273
		basic,
		// Token: 0x0400919A RID: 37274
		high,
		// Token: 0x0400919B RID: 37275
		personalNeeds,
		// Token: 0x0400919C RID: 37276
		topPriority,
		// Token: 0x0400919D RID: 37277
		compulsory
	}
}
