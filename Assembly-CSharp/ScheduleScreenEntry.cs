using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000DC0 RID: 3520
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleScreenEntry")]
public class ScheduleScreenEntry : KMonoBehaviour
{
	// Token: 0x170007A7 RID: 1959
	// (get) Token: 0x06006F08 RID: 28424 RVA: 0x002A41E5 File Offset: 0x002A23E5
	// (set) Token: 0x06006F09 RID: 28425 RVA: 0x002A41ED File Offset: 0x002A23ED
	public Schedule schedule { get; private set; }

	// Token: 0x06006F0A RID: 28426 RVA: 0x002A41F8 File Offset: 0x002A23F8
	public void Setup(Schedule schedule)
	{
		this.schedule = schedule;
		base.gameObject.name = "Schedule_" + schedule.name;
		this.title.SetTitle(schedule.name);
		this.title.OnNameChanged += this.OnNameChanged;
		this.duplicateScheduleButton.onClick += this.DuplicateSchedule;
		this.deleteScheduleButton.onClick += this.DeleteSchedule;
		this.timetableRows = new List<GameObject>();
		this.blockButtonsByTimetableRow = new Dictionary<GameObject, List<ScheduleBlockButton>>();
		int num = Mathf.CeilToInt((float)(schedule.GetBlocks().Count / 24));
		for (int i = 0; i < num; i++)
		{
			this.AddTimetableRow(i * 24);
		}
		this.minionWidgets = new List<ScheduleMinionWidget>();
		this.blankMinionWidget = Util.KInstantiateUI<ScheduleMinionWidget>(this.minionWidgetPrefab.gameObject, this.minionWidgetContainer, false);
		this.blankMinionWidget.SetupBlank(schedule);
		this.RebuildMinionWidgets();
		this.RefreshStatus();
		this.RefreshAlarmButton();
		MultiToggle multiToggle = this.alarmButton;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(this.OnAlarmClicked));
		schedule.onChanged = (Action<Schedule>)Delegate.Combine(schedule.onChanged, new Action<Schedule>(this.OnScheduleChanged));
		this.ConfigPaintButton(this.PaintButtonBathtime, Db.Get().ScheduleGroups.Hygene, Def.GetUISprite(Assets.GetPrefab(ShowerConfig.ID), "ui", false).first);
		this.ConfigPaintButton(this.PaintButtonWorktime, Db.Get().ScheduleGroups.Worktime, Def.GetUISprite(Assets.GetPrefab("ManualGenerator"), "ui", false).first);
		this.ConfigPaintButton(this.PaintButtonRecreation, Db.Get().ScheduleGroups.Recreation, Def.GetUISprite(Assets.GetPrefab("WaterCooler"), "ui", false).first);
		this.ConfigPaintButton(this.PaintButtonSleep, Db.Get().ScheduleGroups.Sleep, Def.GetUISprite(Assets.GetPrefab("Bed"), "ui", false).first);
		this.RefreshPaintButtons();
		this.RefreshTimeOfDayPositioner();
	}

	// Token: 0x06006F0B RID: 28427 RVA: 0x002A4441 File Offset: 0x002A2641
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.Deregister();
	}

	// Token: 0x06006F0C RID: 28428 RVA: 0x002A444F File Offset: 0x002A264F
	public void Deregister()
	{
		if (this.schedule != null)
		{
			Schedule schedule = this.schedule;
			schedule.onChanged = (Action<Schedule>)Delegate.Remove(schedule.onChanged, new Action<Schedule>(this.OnScheduleChanged));
		}
	}

	// Token: 0x06006F0D RID: 28429 RVA: 0x002A4480 File Offset: 0x002A2680
	private void DuplicateSchedule()
	{
		ScheduleManager.Instance.DuplicateSchedule(this.schedule);
	}

	// Token: 0x06006F0E RID: 28430 RVA: 0x002A4493 File Offset: 0x002A2693
	private void DeleteSchedule()
	{
		ScheduleManager.Instance.DeleteSchedule(this.schedule);
	}

	// Token: 0x06006F0F RID: 28431 RVA: 0x002A44A8 File Offset: 0x002A26A8
	public void RefreshTimeOfDayPositioner()
	{
		if (this.schedule.ProgressTimetableIdx >= this.timetableRows.Count || this.schedule.ProgressTimetableIdx < 0)
		{
			KCrashReporter.ReportDevNotification("RefreshTimeOfDayPositionerError", Environment.StackTrace, string.Format("DevError: schedule.ProgressTimetableIdx is out of bounds. schedule.name:{0}, schedule.ProgressTimetableIdx:{1}, : timetableRows.Count:{2}", this.schedule.name, this.schedule.ProgressTimetableIdx, this.timetableRows.Count), true, null);
			this.timeOfDayPositioner.SetTargetTimetable(null);
			return;
		}
		GameObject gameObject = this.timetableRows[this.schedule.ProgressTimetableIdx];
		this.timeOfDayPositioner.SetTargetTimetable(gameObject);
	}

	// Token: 0x06006F10 RID: 28432 RVA: 0x002A4554 File Offset: 0x002A2754
	private void DuplicateTimetableRow(int sourceTimetableIdx)
	{
		List<ScheduleBlock> range = this.schedule.GetBlocks().GetRange(sourceTimetableIdx * 24, 24);
		List<ScheduleBlock> list = new List<ScheduleBlock>();
		for (int i = 0; i < range.Count; i++)
		{
			list.Add(new ScheduleBlock(range[i].name, range[i].GroupId));
		}
		int num = sourceTimetableIdx + 1;
		this.schedule.InsertTimetable(num, list);
		this.AddTimetableRow(num * 24);
	}

	// Token: 0x06006F11 RID: 28433 RVA: 0x002A45D0 File Offset: 0x002A27D0
	private void AddTimetableRow(int startingBlockIdx)
	{
		GameObject row = Util.KInstantiateUI(this.timetableRowPrefab, this.timetableRowContainer, true);
		int num = startingBlockIdx / 24;
		this.timetableRows.Insert(num, row);
		row.transform.SetSiblingIndex(num);
		HierarchyReferences component = row.GetComponent<HierarchyReferences>();
		List<ScheduleBlockButton> list = new List<ScheduleBlockButton>();
		for (int i = startingBlockIdx; i < startingBlockIdx + 24; i++)
		{
			GameObject gameObject = component.GetReference<RectTransform>("BlockContainer").gameObject;
			ScheduleBlockButton scheduleBlockButton = Util.KInstantiateUI<ScheduleBlockButton>(this.blockButtonPrefab.gameObject, gameObject, true);
			scheduleBlockButton.Setup(i - startingBlockIdx);
			scheduleBlockButton.SetBlockTypes(this.schedule.GetBlock(i).allowed_types);
			list.Add(scheduleBlockButton);
		}
		this.blockButtonsByTimetableRow.Add(row, list);
		component.GetReference<ScheduleBlockPainter>("BlockPainter").SetEntry(this);
		component.GetReference<KButton>("DuplicateButton").onClick += delegate
		{
			this.DuplicateTimetableRow(this.timetableRows.IndexOf(row));
		};
		component.GetReference<KButton>("DeleteButton").onClick += delegate
		{
			this.RemoveTimetableRow(row);
		};
		component.GetReference<KButton>("RotateLeftButton").onClick += delegate
		{
			this.schedule.RotateBlocks(true, this.timetableRows.IndexOf(row));
		};
		component.GetReference<KButton>("RotateRightButton").onClick += delegate
		{
			this.schedule.RotateBlocks(false, this.timetableRows.IndexOf(row));
		};
		KButton rotateUpButton = component.GetReference<KButton>("ShiftUpButton");
		rotateUpButton.onClick += delegate
		{
			int num2 = this.timetableRows.IndexOf(row);
			this.schedule.ShiftTimetable(true, num2);
			if (rotateUpButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName == "ScheduleMenu_Shift_up")
			{
				rotateUpButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName = "ScheduleMenu_Shift_up_reset";
				return;
			}
			rotateUpButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName = "ScheduleMenu_Shift_up";
		};
		KButton rotateDownButton = component.GetReference<KButton>("ShiftDownButton");
		rotateDownButton.onClick += delegate
		{
			int num3 = this.timetableRows.IndexOf(row);
			this.schedule.ShiftTimetable(false, num3);
			if (rotateDownButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName == "ScheduleMenu_Shift_down")
			{
				rotateDownButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName = "ScheduleMenu_Shift_down_reset";
				return;
			}
			rotateDownButton.soundPlayer.button_widget_sound_events[0].OverrideAssetName = "ScheduleMenu_Shift_down";
		};
	}

	// Token: 0x06006F12 RID: 28434 RVA: 0x002A4788 File Offset: 0x002A2988
	private void RemoveTimetableRow(GameObject row)
	{
		if (this.timetableRows.Count == 1)
		{
			return;
		}
		this.timeOfDayPositioner.SetTargetTimetable(null);
		int num = this.timetableRows.IndexOf(row);
		this.timetableRows.Remove(row);
		this.blockButtonsByTimetableRow.Remove(row);
		global::UnityEngine.Object.Destroy(row);
		this.schedule.RemoveTimetable(num);
	}

	// Token: 0x06006F13 RID: 28435 RVA: 0x002A47E9 File Offset: 0x002A29E9
	public GameObject GetNameInputField()
	{
		return this.title.inputField.gameObject;
	}

	// Token: 0x06006F14 RID: 28436 RVA: 0x002A47FC File Offset: 0x002A29FC
	private void RebuildMinionWidgets()
	{
		if (this.IsNullOrDestroyed())
		{
			return;
		}
		if (!this.MinionWidgetsNeedRebuild())
		{
			return;
		}
		foreach (ScheduleMinionWidget scheduleMinionWidget in this.minionWidgets)
		{
			Util.KDestroyGameObject(scheduleMinionWidget);
		}
		this.minionWidgets.Clear();
		foreach (Ref<Schedulable> @ref in this.schedule.GetAssigned())
		{
			ScheduleMinionWidget scheduleMinionWidget2 = Util.KInstantiateUI<ScheduleMinionWidget>(this.minionWidgetPrefab.gameObject, this.minionWidgetContainer, true);
			scheduleMinionWidget2.Setup(@ref.Get());
			this.minionWidgets.Add(scheduleMinionWidget2);
		}
		if (Components.LiveMinionIdentities.Count > this.schedule.GetAssigned().Count)
		{
			this.blankMinionWidget.transform.SetAsLastSibling();
			this.blankMinionWidget.gameObject.SetActive(true);
			return;
		}
		this.blankMinionWidget.gameObject.SetActive(false);
	}

	// Token: 0x06006F15 RID: 28437 RVA: 0x002A4928 File Offset: 0x002A2B28
	private bool MinionWidgetsNeedRebuild()
	{
		List<Ref<Schedulable>> assigned = this.schedule.GetAssigned();
		if (assigned.Count != this.minionWidgets.Count)
		{
			return true;
		}
		if (assigned.Count != Components.LiveMinionIdentities.Count != this.blankMinionWidget.gameObject.activeSelf)
		{
			return true;
		}
		for (int i = 0; i < assigned.Count; i++)
		{
			if (assigned[i].Get() != this.minionWidgets[i].schedulable)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006F16 RID: 28438 RVA: 0x002A49B8 File Offset: 0x002A2BB8
	public void RefreshWidgetWorldData()
	{
		foreach (ScheduleMinionWidget scheduleMinionWidget in this.minionWidgets)
		{
			if (!scheduleMinionWidget.IsNullOrDestroyed())
			{
				scheduleMinionWidget.RefreshWidgetWorldData();
			}
		}
	}

	// Token: 0x06006F17 RID: 28439 RVA: 0x002A4A14 File Offset: 0x002A2C14
	private void OnNameChanged(string newName)
	{
		this.schedule.name = newName;
		base.gameObject.name = "Schedule_" + this.schedule.name;
	}

	// Token: 0x06006F18 RID: 28440 RVA: 0x002A4A42 File Offset: 0x002A2C42
	private void OnAlarmClicked()
	{
		this.schedule.alarmActivated = !this.schedule.alarmActivated;
		this.RefreshAlarmButton();
	}

	// Token: 0x06006F19 RID: 28441 RVA: 0x002A4A64 File Offset: 0x002A2C64
	private void RefreshAlarmButton()
	{
		this.alarmButton.ChangeState(this.schedule.alarmActivated ? 1 : 0);
		ToolTip component = this.alarmButton.GetComponent<ToolTip>();
		component.SetSimpleTooltip(this.schedule.alarmActivated ? UI.SCHEDULESCREEN.ALARM_BUTTON_ON_TOOLTIP : UI.SCHEDULESCREEN.ALARM_BUTTON_OFF_TOOLTIP);
		ToolTipScreen.Instance.MarkTooltipDirty(component);
		this.alarmField.text = (this.schedule.alarmActivated ? UI.SCHEDULESCREEN.ALARM_TITLE_ENABLED : UI.SCHEDULESCREEN.ALARM_TITLE_DISABLED);
	}

	// Token: 0x06006F1A RID: 28442 RVA: 0x002A4AF1 File Offset: 0x002A2CF1
	private void OnResetClicked()
	{
		this.schedule.SetBlocksToGroupDefaults(Db.Get().ScheduleGroups.allGroups);
	}

	// Token: 0x06006F1B RID: 28443 RVA: 0x002A4B0D File Offset: 0x002A2D0D
	private void OnDeleteClicked()
	{
		ScheduleManager.Instance.DeleteSchedule(this.schedule);
	}

	// Token: 0x06006F1C RID: 28444 RVA: 0x002A4B20 File Offset: 0x002A2D20
	private void OnScheduleChanged(Schedule changedSchedule)
	{
		foreach (KeyValuePair<GameObject, List<ScheduleBlockButton>> keyValuePair in this.blockButtonsByTimetableRow)
		{
			GameObject key = keyValuePair.Key;
			int num = this.timetableRows.IndexOf(key);
			List<ScheduleBlockButton> value = keyValuePair.Value;
			for (int i = 0; i < value.Count; i++)
			{
				int num2 = num * 24 + i;
				value[i].SetBlockTypes(changedSchedule.GetBlock(num2).allowed_types);
			}
		}
		this.RefreshStatus();
		this.RebuildMinionWidgets();
	}

	// Token: 0x06006F1D RID: 28445 RVA: 0x002A4BD0 File Offset: 0x002A2DD0
	private void RefreshStatus()
	{
		this.blockTypeCounts.Clear();
		foreach (ScheduleBlockType scheduleBlockType in Db.Get().ScheduleBlockTypes.resources)
		{
			this.blockTypeCounts[scheduleBlockType.Id] = 0;
		}
		foreach (ScheduleBlock scheduleBlock in this.schedule.GetBlocks())
		{
			foreach (ScheduleBlockType scheduleBlockType2 in scheduleBlock.allowed_types)
			{
				Dictionary<string, int> dictionary = this.blockTypeCounts;
				string id = scheduleBlockType2.Id;
				int num = dictionary[id];
				dictionary[id] = num + 1;
			}
		}
		if (this.noteEntryRight == null)
		{
			return;
		}
		int num2 = 0;
		ToolTip component = this.noteEntryRight.GetComponent<ToolTip>();
		component.ClearMultiStringTooltip();
		foreach (KeyValuePair<string, int> keyValuePair in this.blockTypeCounts)
		{
			if (keyValuePair.Value == 0)
			{
				num2++;
				component.AddMultiStringTooltip(string.Format(UI.SCHEDULEGROUPS.NOTIME, Db.Get().ScheduleBlockTypes.Get(keyValuePair.Key).Name), null);
			}
		}
		if (num2 > 0)
		{
			this.noteEntryRight.text = string.Format(UI.SCHEDULEGROUPS.MISSINGBLOCKS, num2);
			return;
		}
		this.noteEntryRight.text = "";
	}

	// Token: 0x06006F1E RID: 28446 RVA: 0x002A4DB4 File Offset: 0x002A2FB4
	private void ConfigPaintButton(GameObject button, ScheduleGroup group, Sprite iconSprite)
	{
		string groupID = group.Id;
		button.GetComponent<MultiToggle>().onClick = delegate
		{
			ScheduleScreen.Instance.SelectedPaint = groupID;
			ScheduleScreen.Instance.RefreshAllPaintButtons();
		};
		this.paintButtons.Add(group.Id, button);
		HierarchyReferences component = button.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("Icon").sprite = iconSprite;
		component.GetReference<LocText>("Label").text = group.Name;
	}

	// Token: 0x06006F1F RID: 28447 RVA: 0x002A4E28 File Offset: 0x002A3028
	public void RefreshPaintButtons()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.paintButtons)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState((keyValuePair.Key == ScheduleScreen.Instance.SelectedPaint) ? 1 : 0);
		}
	}

	// Token: 0x06006F20 RID: 28448 RVA: 0x002A4EA4 File Offset: 0x002A30A4
	public bool PaintBlock(ScheduleBlockButton blockButton)
	{
		foreach (KeyValuePair<GameObject, List<ScheduleBlockButton>> keyValuePair in this.blockButtonsByTimetableRow)
		{
			GameObject key = keyValuePair.Key;
			int i = 0;
			while (i < keyValuePair.Value.Count)
			{
				if (keyValuePair.Value[i] == blockButton)
				{
					int num = this.timetableRows.IndexOf(key) * 24 + i;
					ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.Get(ScheduleScreen.Instance.SelectedPaint);
					if (this.schedule.GetBlock(num).GroupId != scheduleGroup.Id)
					{
						this.schedule.SetBlockGroup(num, scheduleGroup);
						return true;
					}
					return false;
				}
				else
				{
					i++;
				}
			}
		}
		return false;
	}

	// Token: 0x04004C4F RID: 19535
	[SerializeField]
	private ScheduleBlockButton blockButtonPrefab;

	// Token: 0x04004C50 RID: 19536
	[SerializeField]
	private ScheduleMinionWidget minionWidgetPrefab;

	// Token: 0x04004C51 RID: 19537
	[SerializeField]
	private GameObject minionWidgetContainer;

	// Token: 0x04004C52 RID: 19538
	private ScheduleMinionWidget blankMinionWidget;

	// Token: 0x04004C53 RID: 19539
	[SerializeField]
	private KButton duplicateScheduleButton;

	// Token: 0x04004C54 RID: 19540
	[SerializeField]
	private KButton deleteScheduleButton;

	// Token: 0x04004C55 RID: 19541
	[SerializeField]
	private EditableTitleBar title;

	// Token: 0x04004C56 RID: 19542
	[SerializeField]
	private LocText alarmField;

	// Token: 0x04004C57 RID: 19543
	[SerializeField]
	private KButton optionsButton;

	// Token: 0x04004C58 RID: 19544
	[SerializeField]
	private LocText noteEntryLeft;

	// Token: 0x04004C59 RID: 19545
	[SerializeField]
	private LocText noteEntryRight;

	// Token: 0x04004C5A RID: 19546
	[SerializeField]
	private MultiToggle alarmButton;

	// Token: 0x04004C5B RID: 19547
	private List<GameObject> timetableRows;

	// Token: 0x04004C5C RID: 19548
	private Dictionary<GameObject, List<ScheduleBlockButton>> blockButtonsByTimetableRow;

	// Token: 0x04004C5D RID: 19549
	private List<ScheduleMinionWidget> minionWidgets;

	// Token: 0x04004C5E RID: 19550
	[SerializeField]
	private GameObject timetableRowPrefab;

	// Token: 0x04004C5F RID: 19551
	[SerializeField]
	private GameObject timetableRowContainer;

	// Token: 0x04004C60 RID: 19552
	private Dictionary<string, GameObject> paintButtons = new Dictionary<string, GameObject>();

	// Token: 0x04004C61 RID: 19553
	[SerializeField]
	private GameObject PaintButtonBathtime;

	// Token: 0x04004C62 RID: 19554
	[SerializeField]
	private GameObject PaintButtonWorktime;

	// Token: 0x04004C63 RID: 19555
	[SerializeField]
	private GameObject PaintButtonRecreation;

	// Token: 0x04004C64 RID: 19556
	[SerializeField]
	private GameObject PaintButtonSleep;

	// Token: 0x04004C65 RID: 19557
	[SerializeField]
	private TimeOfDayPositioner timeOfDayPositioner;

	// Token: 0x04004C67 RID: 19559
	private Dictionary<string, int> blockTypeCounts = new Dictionary<string, int>();
}
