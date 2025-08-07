using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using FMOD.Studio;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000AF9 RID: 2809
[AddComponentMenu("KMonoBehaviour/scripts/ScheduleManager")]
public class ScheduleManager : KMonoBehaviour, ISim33ms
{
	// Token: 0x1400001F RID: 31
	// (add) Token: 0x0600529A RID: 21146 RVA: 0x001E17B4 File Offset: 0x001DF9B4
	// (remove) Token: 0x0600529B RID: 21147 RVA: 0x001E17EC File Offset: 0x001DF9EC
	public event Action<List<Schedule>> onSchedulesChanged;

	// Token: 0x0600529C RID: 21148 RVA: 0x001E1821 File Offset: 0x001DFA21
	public static void DestroyInstance()
	{
		ScheduleManager.Instance = null;
	}

	// Token: 0x0600529D RID: 21149 RVA: 0x001E1829 File Offset: 0x001DFA29
	public Schedule GetDefaultBionicSchedule()
	{
		return this.schedules.Find((Schedule match) => match.isDefaultForBionics);
	}

	// Token: 0x0600529E RID: 21150 RVA: 0x001E1855 File Offset: 0x001DFA55
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.schedules.Count == 0)
		{
			this.AddDefaultSchedule(true, true);
		}
	}

	// Token: 0x0600529F RID: 21151 RVA: 0x001E186C File Offset: 0x001DFA6C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.schedules = new List<Schedule>();
		ScheduleManager.Instance = this;
	}

	// Token: 0x060052A0 RID: 21152 RVA: 0x001E1888 File Offset: 0x001DFA88
	protected override void OnSpawn()
	{
		if (this.schedules.Count == 0)
		{
			this.AddDefaultSchedule(true, true);
		}
		foreach (Schedule schedule in this.schedules)
		{
			schedule.ClearNullReferences();
		}
		List<ScheduleBlock> scheduleBlocksFromGroupDefaults = Schedule.GetScheduleBlocksFromGroupDefaults(Db.Get().ScheduleGroups.allGroups);
		foreach (Schedule schedule2 in this.schedules)
		{
			List<ScheduleBlock> blocks = schedule2.GetBlocks();
			for (int i = 0; i < blocks.Count; i++)
			{
				ScheduleBlock scheduleBlock = blocks[i];
				if (Db.Get().ScheduleGroups.FindGroupForScheduleTypes(scheduleBlock.allowed_types) == null)
				{
					ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(scheduleBlocksFromGroupDefaults[i].allowed_types);
					schedule2.SetBlockGroup(i, scheduleGroup);
				}
			}
		}
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.Items)
		{
			Schedulable component = minionIdentity.GetComponent<Schedulable>();
			if (this.GetSchedule(component) == null)
			{
				this.schedules[0].Assign(component);
			}
		}
		Components.LiveMinionIdentities.OnAdd += this.OnAddDupe;
		Components.LiveMinionIdentities.OnRemove += this.OnRemoveDupe;
	}

	// Token: 0x060052A1 RID: 21153 RVA: 0x001E1A34 File Offset: 0x001DFC34
	private void OnAddDupe(MinionIdentity minion)
	{
		Schedulable component = minion.GetComponent<Schedulable>();
		if (component.GetSchedule() != null)
		{
			return;
		}
		Schedule schedule = this.schedules[0];
		if (minion.model == GameTags.Minions.Models.Bionic)
		{
			if (this.GetDefaultBionicSchedule() == null)
			{
				if (!this.hasDeletedDefaultBionicSchedule)
				{
					Schedule schedule2 = this.AddSchedule(Db.Get().ScheduleGroups.allGroups, UI.SCHEDULESCREEN.SCHEDULE_NAME_DEFAULT_BIONIC, true);
					schedule2.AddTimetable(Schedule.GetScheduleBlocksFromGroupDefaults(Db.Get().ScheduleGroups.allGroups));
					schedule2.AddTimetable(Schedule.GetScheduleBlocksFromGroupDefaults(Db.Get().ScheduleGroups.allGroups));
					for (int i = 0; i < schedule2.GetBlocks().Count; i++)
					{
						schedule2.SetBlockGroup(i, Db.Get().ScheduleGroups.Worktime);
					}
					for (int j = 1; j <= 6; j++)
					{
						schedule2.SetBlockGroup(schedule2.GetBlocks().Count - j, Db.Get().ScheduleGroups.Sleep);
					}
					for (int k = 7; k <= 12; k++)
					{
						schedule2.SetBlockGroup(schedule2.GetBlocks().Count - k, Db.Get().ScheduleGroups.Recreation);
					}
					schedule = schedule2;
					schedule2.isDefaultForBionics = true;
					if (this.onSchedulesChanged != null)
					{
						this.onSchedulesChanged(this.schedules);
					}
				}
			}
			else
			{
				schedule = this.GetDefaultBionicSchedule();
			}
		}
		else if (this.GetSchedule(component) != null)
		{
			schedule = this.GetSchedule(component);
		}
		schedule.Assign(component);
	}

	// Token: 0x060052A2 RID: 21154 RVA: 0x001E1BB8 File Offset: 0x001DFDB8
	private void OnRemoveDupe(MinionIdentity minion)
	{
		Schedulable component = minion.GetComponent<Schedulable>();
		Schedule schedule = this.GetSchedule(component);
		if (schedule != null)
		{
			schedule.Unassign(component);
		}
	}

	// Token: 0x060052A3 RID: 21155 RVA: 0x001E1BE0 File Offset: 0x001DFDE0
	public void OnStoredDupeDestroyed(StoredMinionIdentity dupe)
	{
		foreach (Schedule schedule in this.schedules)
		{
			schedule.Unassign(dupe.gameObject.GetComponent<Schedulable>());
		}
	}

	// Token: 0x060052A4 RID: 21156 RVA: 0x001E1C3C File Offset: 0x001DFE3C
	public void AddDefaultSchedule(bool alarmOn, bool useDefaultName = true)
	{
		Schedule schedule = this.AddSchedule(Db.Get().ScheduleGroups.allGroups, useDefaultName ? UI.SCHEDULESCREEN.SCHEDULE_NAME_DEFAULT : UI.SCHEDULESCREEN.SCHEDULE_NAME_NEW, alarmOn);
		if (Game.Instance.FastWorkersModeActive)
		{
			for (int i = 0; i < 21; i++)
			{
				schedule.SetBlockGroup(i, Db.Get().ScheduleGroups.Worktime);
			}
			schedule.SetBlockGroup(21, Db.Get().ScheduleGroups.Recreation);
			schedule.SetBlockGroup(22, Db.Get().ScheduleGroups.Recreation);
			schedule.SetBlockGroup(23, Db.Get().ScheduleGroups.Sleep);
		}
	}

	// Token: 0x060052A5 RID: 21157 RVA: 0x001E1CE8 File Offset: 0x001DFEE8
	public Schedule AddSchedule(List<ScheduleGroup> groups, string name = null, bool alarmOn = false)
	{
		if (name == null)
		{
			this.scheduleNameIncrementor++;
			name = string.Format(UI.SCHEDULESCREEN.SCHEDULE_NAME_FORMAT, this.scheduleNameIncrementor.ToString());
		}
		Schedule schedule = new Schedule(name, groups, alarmOn);
		this.schedules.Add(schedule);
		if (this.onSchedulesChanged != null)
		{
			this.onSchedulesChanged(this.schedules);
		}
		return schedule;
	}

	// Token: 0x060052A6 RID: 21158 RVA: 0x001E1D54 File Offset: 0x001DFF54
	public Schedule DuplicateSchedule(Schedule source)
	{
		if (base.name == null)
		{
			this.scheduleNameIncrementor++;
			base.name = string.Format(UI.SCHEDULESCREEN.SCHEDULE_NAME_FORMAT, this.scheduleNameIncrementor.ToString());
		}
		Schedule schedule = new Schedule("copy of " + source.name, source.GetBlocks(), source.alarmActivated);
		schedule.ProgressTimetableIdx = source.ProgressTimetableIdx;
		this.schedules.Add(schedule);
		if (this.onSchedulesChanged != null)
		{
			this.onSchedulesChanged(this.schedules);
		}
		return schedule;
	}

	// Token: 0x060052A7 RID: 21159 RVA: 0x001E1DEC File Offset: 0x001DFFEC
	public void DeleteSchedule(Schedule schedule)
	{
		if (this.schedules.Count == 1)
		{
			return;
		}
		List<Ref<Schedulable>> assigned = schedule.GetAssigned();
		if (schedule.isDefaultForBionics)
		{
			this.hasDeletedDefaultBionicSchedule = true;
		}
		this.schedules.Remove(schedule);
		foreach (Ref<Schedulable> @ref in assigned)
		{
			this.schedules[0].Assign(@ref.Get());
		}
		if (this.onSchedulesChanged != null)
		{
			this.onSchedulesChanged(this.schedules);
		}
	}

	// Token: 0x060052A8 RID: 21160 RVA: 0x001E1E94 File Offset: 0x001E0094
	public Schedule GetSchedule(Schedulable schedulable)
	{
		foreach (Schedule schedule in this.schedules)
		{
			if (schedule.IsAssigned(schedulable))
			{
				return schedule;
			}
		}
		return null;
	}

	// Token: 0x060052A9 RID: 21161 RVA: 0x001E1EF0 File Offset: 0x001E00F0
	public List<Schedule> GetSchedules()
	{
		return this.schedules;
	}

	// Token: 0x060052AA RID: 21162 RVA: 0x001E1EF8 File Offset: 0x001E00F8
	public bool IsAllowed(Schedulable schedulable, ScheduleBlockType schedule_block_type)
	{
		Schedule schedule = this.GetSchedule(schedulable);
		return schedule != null && schedule.GetCurrentScheduleBlock().IsAllowed(schedule_block_type);
	}

	// Token: 0x060052AB RID: 21163 RVA: 0x001E1F1E File Offset: 0x001E011E
	public static int GetCurrentHour()
	{
		return Math.Min((int)(GameClock.Instance.GetCurrentCycleAsPercentage() * 24f), 23);
	}

	// Token: 0x060052AC RID: 21164 RVA: 0x001E1F38 File Offset: 0x001E0138
	public void Sim33ms(float dt)
	{
		int currentHour = ScheduleManager.GetCurrentHour();
		if (ScheduleManager.GetCurrentHour() != this.lastHour)
		{
			foreach (Schedule schedule in this.schedules)
			{
				schedule.Tick();
			}
			this.lastHour = currentHour;
		}
	}

	// Token: 0x060052AD RID: 21165 RVA: 0x001E1FA4 File Offset: 0x001E01A4
	public void PlayScheduleAlarm(Schedule schedule, ScheduleBlock block, bool forwards)
	{
		Notification notification = new Notification(string.Format(MISC.NOTIFICATIONS.SCHEDULE_CHANGED.NAME, schedule.name, block.name), NotificationType.Good, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.SCHEDULE_CHANGED.TOOLTIP.Replace("{0}", schedule.name).Replace("{1}", block.name).Replace("{2}", Db.Get().ScheduleGroups.Get(block.GroupId).notificationTooltip), null, true, 0f, null, null, null, true, false, false);
		base.GetComponent<Notifier>().Add(notification, "");
		base.StartCoroutine(this.PlayScheduleTone(schedule, forwards));
	}

	// Token: 0x060052AE RID: 21166 RVA: 0x001E202F File Offset: 0x001E022F
	private IEnumerator PlayScheduleTone(Schedule schedule, bool forwards)
	{
		int[] tones = schedule.GetTones();
		int num2;
		for (int i = 0; i < tones.Length; i = num2 + 1)
		{
			int num = (forwards ? i : (tones.Length - 1 - i));
			this.PlayTone(tones[num], forwards);
			yield return SequenceUtil.WaitForSeconds(TuningData<ScheduleManager.Tuning>.Get().toneSpacingSeconds);
			num2 = i;
		}
		yield break;
	}

	// Token: 0x060052AF RID: 21167 RVA: 0x001E204C File Offset: 0x001E024C
	private void PlayTone(int pitch, bool forwards)
	{
		EventInstance eventInstance = KFMOD.BeginOneShot(GlobalAssets.GetSound("WorkChime_tone", false), Vector3.zero, 1f);
		eventInstance.setParameterByName("WorkChime_pitch", (float)pitch, false);
		eventInstance.setParameterByName("WorkChime_start", (float)(forwards ? 1 : 0), false);
		KFMOD.EndOneShot(eventInstance);
	}

	// Token: 0x04003783 RID: 14211
	[Serialize]
	private List<Schedule> schedules;

	// Token: 0x04003784 RID: 14212
	[Serialize]
	private int lastHour;

	// Token: 0x04003785 RID: 14213
	[Serialize]
	private int scheduleNameIncrementor;

	// Token: 0x04003787 RID: 14215
	public static ScheduleManager Instance;

	// Token: 0x04003788 RID: 14216
	[Serialize]
	private bool hasDeletedDefaultBionicSchedule;

	// Token: 0x02001C01 RID: 7169
	public class Tuning : TuningData<ScheduleManager.Tuning>
	{
		// Token: 0x040084C4 RID: 33988
		public float toneSpacingSeconds;

		// Token: 0x040084C5 RID: 33989
		public int minToneIndex;

		// Token: 0x040084C6 RID: 33990
		public int maxToneIndex;

		// Token: 0x040084C7 RID: 33991
		public int firstLastToneSpacing;
	}
}
