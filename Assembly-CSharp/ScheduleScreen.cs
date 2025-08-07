using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DBD RID: 3517
public class ScheduleScreen : KScreen
{
	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x06006EF2 RID: 28402 RVA: 0x002A3D87 File Offset: 0x002A1F87
	// (set) Token: 0x06006EF3 RID: 28403 RVA: 0x002A3D8F File Offset: 0x002A1F8F
	public string SelectedPaint { get; set; }

	// Token: 0x06006EF4 RID: 28404 RVA: 0x002A3D98 File Offset: 0x002A1F98
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06006EF5 RID: 28405 RVA: 0x002A3D9F File Offset: 0x002A1F9F
	protected override void OnPrefabInit()
	{
		base.ConsumeMouseScroll = true;
		this.scheduleEntries = new List<ScheduleScreenEntry>();
		ScheduleScreen.Instance = this;
	}

	// Token: 0x06006EF6 RID: 28406 RVA: 0x002A3DBC File Offset: 0x002A1FBC
	protected override void OnSpawn()
	{
		foreach (Schedule schedule in ScheduleManager.Instance.GetSchedules())
		{
			this.AddScheduleEntry(schedule);
		}
		this.addScheduleButton.onClick += this.OnAddScheduleClick;
		this.closeButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		ScheduleManager.Instance.onSchedulesChanged += this.OnSchedulesChanged;
		Game.Instance.Subscribe(1983128072, new Action<object>(this.RefreshWidgetWorldData));
	}

	// Token: 0x06006EF7 RID: 28407 RVA: 0x002A3E88 File Offset: 0x002A2088
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		ScheduleManager.Instance.onSchedulesChanged -= this.OnSchedulesChanged;
		ScheduleScreen.Instance = null;
	}

	// Token: 0x06006EF8 RID: 28408 RVA: 0x002A3EAC File Offset: 0x002A20AC
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		if (show)
		{
			base.Activate();
			this.SetScreenHeight();
		}
	}

	// Token: 0x06006EF9 RID: 28409 RVA: 0x002A3EC4 File Offset: 0x002A20C4
	private void SetScreenHeight()
	{
		bool flag = ScheduleManager.Instance.GetSchedules().Count == 1;
		base.GetComponent<LayoutElement>().preferredHeight = (float)(flag ? 410 : 604);
		this.bottomSpacer.SetActive(flag);
	}

	// Token: 0x06006EFA RID: 28410 RVA: 0x002A3F0C File Offset: 0x002A210C
	public void RefreshAllPaintButtons()
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.scheduleEntries)
		{
			scheduleScreenEntry.RefreshPaintButtons();
		}
	}

	// Token: 0x06006EFB RID: 28411 RVA: 0x002A3F5C File Offset: 0x002A215C
	private void OnAddScheduleClick()
	{
		ScheduleManager.Instance.AddDefaultSchedule(false, false);
	}

	// Token: 0x06006EFC RID: 28412 RVA: 0x002A3F6C File Offset: 0x002A216C
	private void AddScheduleEntry(Schedule schedule)
	{
		ScheduleScreenEntry scheduleScreenEntry = Util.KInstantiateUI<ScheduleScreenEntry>(this.scheduleEntryPrefab.gameObject, this.scheduleEntryContainer, true);
		scheduleScreenEntry.Setup(schedule);
		this.scheduleEntries.Add(scheduleScreenEntry);
		this.SetScreenHeight();
	}

	// Token: 0x06006EFD RID: 28413 RVA: 0x002A3FAC File Offset: 0x002A21AC
	private void OnSchedulesChanged(List<Schedule> schedules)
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.scheduleEntries)
		{
			scheduleScreenEntry.Deregister();
			Util.KDestroyGameObject(scheduleScreenEntry.gameObject);
		}
		this.scheduleEntries.Clear();
		foreach (Schedule schedule in schedules)
		{
			this.AddScheduleEntry(schedule);
		}
		this.SetScreenHeight();
	}

	// Token: 0x06006EFE RID: 28414 RVA: 0x002A4058 File Offset: 0x002A2258
	private void RefreshWidgetWorldData(object data = null)
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.scheduleEntries)
		{
			scheduleScreenEntry.RefreshWidgetWorldData();
		}
	}

	// Token: 0x06006EFF RID: 28415 RVA: 0x002A40A8 File Offset: 0x002A22A8
	public void OnChangeCurrentTimetable()
	{
		foreach (ScheduleScreenEntry scheduleScreenEntry in this.scheduleEntries)
		{
			scheduleScreenEntry.RefreshTimeOfDayPositioner();
		}
	}

	// Token: 0x06006F00 RID: 28416 RVA: 0x002A40F8 File Offset: 0x002A22F8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.CheckBlockedInput())
		{
			if (!e.Consumed)
			{
				e.Consumed = true;
				return;
			}
		}
		else
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06006F01 RID: 28417 RVA: 0x002A411C File Offset: 0x002A231C
	private bool CheckBlockedInput()
	{
		bool flag = false;
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			GameObject currentSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				foreach (ScheduleScreenEntry scheduleScreenEntry in this.scheduleEntries)
				{
					if (currentSelectedGameObject == scheduleScreenEntry.GetNameInputField())
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x04004C44 RID: 19524
	public static ScheduleScreen Instance;

	// Token: 0x04004C46 RID: 19526
	[SerializeField]
	private ScheduleScreenEntry scheduleEntryPrefab;

	// Token: 0x04004C47 RID: 19527
	[SerializeField]
	private GameObject scheduleEntryContainer;

	// Token: 0x04004C48 RID: 19528
	[SerializeField]
	private KButton addScheduleButton;

	// Token: 0x04004C49 RID: 19529
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04004C4A RID: 19530
	[SerializeField]
	private GameObject bottomSpacer;

	// Token: 0x04004C4B RID: 19531
	private List<ScheduleScreenEntry> scheduleEntries;
}
