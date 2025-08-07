using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000AF8 RID: 2808
[SerializationConfig(MemberSerialization.OptIn)]
public class Schedule : ISaveLoadable, IListableOption
{
	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x0600527D RID: 21117 RVA: 0x001E0E68 File Offset: 0x001DF068
	// (set) Token: 0x0600527E RID: 21118 RVA: 0x001E0E70 File Offset: 0x001DF070
	public int ProgressTimetableIdx
	{
		get
		{
			return this.progressTimetableIdx;
		}
		set
		{
			this.progressTimetableIdx = value;
		}
	}

	// Token: 0x0600527F RID: 21119 RVA: 0x001E0E79 File Offset: 0x001DF079
	public ScheduleBlock GetCurrentScheduleBlock()
	{
		return this.GetBlock(this.GetCurrentBlockIdx());
	}

	// Token: 0x06005280 RID: 21120 RVA: 0x001E0E87 File Offset: 0x001DF087
	public int GetCurrentBlockIdx()
	{
		return Math.Min((int)(GameClock.Instance.GetCurrentCycleAsPercentage() * 24f), 23) + this.progressTimetableIdx * 24;
	}

	// Token: 0x06005281 RID: 21121 RVA: 0x001E0EAB File Offset: 0x001DF0AB
	public ScheduleBlock GetPreviousScheduleBlock()
	{
		return this.GetBlock(this.GetPreviousBlockIdx());
	}

	// Token: 0x06005282 RID: 21122 RVA: 0x001E0EBC File Offset: 0x001DF0BC
	public int GetPreviousBlockIdx()
	{
		int num = this.GetCurrentBlockIdx() - 1;
		if (num == -1)
		{
			num = this.blocks.Count - 1;
		}
		return num;
	}

	// Token: 0x06005283 RID: 21123 RVA: 0x001E0EE5 File Offset: 0x001DF0E5
	public void ClearNullReferences()
	{
		this.assigned.RemoveAll((Ref<Schedulable> x) => x.Get() == null);
	}

	// Token: 0x06005284 RID: 21124 RVA: 0x001E0F14 File Offset: 0x001DF114
	public Schedule(string name, List<ScheduleGroup> defaultGroups, bool alarmActivated)
	{
		this.name = name;
		this.alarmActivated = alarmActivated;
		this.blocks = new List<ScheduleBlock>(defaultGroups.Count);
		this.assigned = new List<Ref<Schedulable>>();
		this.tones = this.GenerateTones();
		this.SetBlocksToGroupDefaults(defaultGroups);
	}

	// Token: 0x06005285 RID: 21125 RVA: 0x001E0F6C File Offset: 0x001DF16C
	public Schedule(string name, List<ScheduleBlock> sourceBlocks, bool alarmActivated)
	{
		this.name = name;
		this.alarmActivated = alarmActivated;
		this.blocks = new List<ScheduleBlock>();
		for (int i = 0; i < sourceBlocks.Count; i++)
		{
			this.blocks.Add(new ScheduleBlock(sourceBlocks[i].name, sourceBlocks[i].GroupId));
		}
		this.assigned = new List<Ref<Schedulable>>();
		this.tones = this.GenerateTones();
		this.Changed();
	}

	// Token: 0x06005286 RID: 21126 RVA: 0x001E0FF5 File Offset: 0x001DF1F5
	public void SetBlocksToGroupDefaults(List<ScheduleGroup> defaultGroups)
	{
		this.blocks = Schedule.GetScheduleBlocksFromGroupDefaults(defaultGroups);
		global::Debug.Assert(this.blocks.Count == 24);
		this.Changed();
	}

	// Token: 0x06005287 RID: 21127 RVA: 0x001E1020 File Offset: 0x001DF220
	public static List<ScheduleBlock> GetScheduleBlocksFromGroupDefaults(List<ScheduleGroup> defaultGroups)
	{
		List<ScheduleBlock> list = new List<ScheduleBlock>();
		for (int i = 0; i < defaultGroups.Count; i++)
		{
			ScheduleGroup scheduleGroup = defaultGroups[i];
			for (int j = 0; j < scheduleGroup.defaultSegments; j++)
			{
				list.Add(new ScheduleBlock(scheduleGroup.Name, scheduleGroup.Id));
			}
		}
		return list;
	}

	// Token: 0x06005288 RID: 21128 RVA: 0x001E1078 File Offset: 0x001DF278
	public void Tick()
	{
		ScheduleBlock currentScheduleBlock = this.GetCurrentScheduleBlock();
		ScheduleBlock block = this.GetBlock(this.GetPreviousBlockIdx());
		global::Debug.Assert(block != currentScheduleBlock);
		if (this.GetCurrentBlockIdx() % 24 == 0)
		{
			this.progressTimetableIdx++;
			if (this.progressTimetableIdx >= this.blocks.Count / 24)
			{
				this.progressTimetableIdx = 0;
			}
			if (ScheduleScreen.Instance != null)
			{
				ScheduleScreen.Instance.OnChangeCurrentTimetable();
			}
		}
		if (!Schedule.AreScheduleTypesIdentical(currentScheduleBlock.allowed_types, block.allowed_types))
		{
			ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(currentScheduleBlock.allowed_types);
			ScheduleGroup scheduleGroup2 = Db.Get().ScheduleGroups.FindGroupForScheduleTypes(block.allowed_types);
			if (this.alarmActivated && scheduleGroup2.alarm != scheduleGroup.alarm)
			{
				ScheduleManager.Instance.PlayScheduleAlarm(this, currentScheduleBlock, scheduleGroup.alarm);
			}
			foreach (Ref<Schedulable> @ref in this.GetAssigned())
			{
				@ref.Get().OnScheduleBlocksChanged(this);
			}
		}
		foreach (Ref<Schedulable> ref2 in this.GetAssigned())
		{
			ref2.Get().OnScheduleBlocksTick(this);
		}
	}

	// Token: 0x06005289 RID: 21129 RVA: 0x001E11EC File Offset: 0x001DF3EC
	string IListableOption.GetProperName()
	{
		return this.name;
	}

	// Token: 0x0600528A RID: 21130 RVA: 0x001E11F4 File Offset: 0x001DF3F4
	public int[] GenerateTones()
	{
		int minToneIndex = TuningData<ScheduleManager.Tuning>.Get().minToneIndex;
		int maxToneIndex = TuningData<ScheduleManager.Tuning>.Get().maxToneIndex;
		int firstLastToneSpacing = TuningData<ScheduleManager.Tuning>.Get().firstLastToneSpacing;
		int[] array = new int[4];
		array[0] = global::UnityEngine.Random.Range(minToneIndex, maxToneIndex - firstLastToneSpacing + 1);
		array[1] = global::UnityEngine.Random.Range(minToneIndex, maxToneIndex + 1);
		array[2] = global::UnityEngine.Random.Range(minToneIndex, maxToneIndex + 1);
		array[3] = global::UnityEngine.Random.Range(array[0] + firstLastToneSpacing, maxToneIndex + 1);
		return array;
	}

	// Token: 0x0600528B RID: 21131 RVA: 0x001E1260 File Offset: 0x001DF460
	public List<Ref<Schedulable>> GetAssigned()
	{
		if (this.assigned == null)
		{
			this.assigned = new List<Ref<Schedulable>>();
		}
		return this.assigned;
	}

	// Token: 0x0600528C RID: 21132 RVA: 0x001E127B File Offset: 0x001DF47B
	public int[] GetTones()
	{
		if (this.tones == null)
		{
			this.tones = this.GenerateTones();
		}
		return this.tones;
	}

	// Token: 0x0600528D RID: 21133 RVA: 0x001E1297 File Offset: 0x001DF497
	public void SetBlockGroup(int idx, ScheduleGroup group)
	{
		if (0 <= idx && idx < this.blocks.Count)
		{
			this.blocks[idx] = new ScheduleBlock(group.Name, group.Id);
			this.Changed();
		}
	}

	// Token: 0x0600528E RID: 21134 RVA: 0x001E12D0 File Offset: 0x001DF4D0
	private void Changed()
	{
		foreach (Ref<Schedulable> @ref in this.GetAssigned())
		{
			@ref.Get().OnScheduleChanged(this);
		}
		if (this.onChanged != null)
		{
			this.onChanged(this);
		}
	}

	// Token: 0x0600528F RID: 21135 RVA: 0x001E133C File Offset: 0x001DF53C
	public List<ScheduleBlock> GetBlocks()
	{
		return this.blocks;
	}

	// Token: 0x06005290 RID: 21136 RVA: 0x001E1344 File Offset: 0x001DF544
	public ScheduleBlock GetBlock(int idx)
	{
		return this.blocks[idx];
	}

	// Token: 0x06005291 RID: 21137 RVA: 0x001E1352 File Offset: 0x001DF552
	public void InsertTimetable(int timetableIdx, List<ScheduleBlock> newBlocks)
	{
		this.blocks.InsertRange(timetableIdx * 24, newBlocks);
		if (timetableIdx <= this.progressTimetableIdx)
		{
			this.progressTimetableIdx++;
		}
	}

	// Token: 0x06005292 RID: 21138 RVA: 0x001E137B File Offset: 0x001DF57B
	public void AddTimetable(List<ScheduleBlock> newBlocks)
	{
		this.blocks.AddRange(newBlocks);
	}

	// Token: 0x06005293 RID: 21139 RVA: 0x001E138C File Offset: 0x001DF58C
	public void RemoveTimetable(int TimetableToRemoveIdx)
	{
		int num = TimetableToRemoveIdx * 24;
		int num2 = this.blocks.Count / 24;
		this.blocks.RemoveRange(num, 24);
		bool flag = TimetableToRemoveIdx == this.progressTimetableIdx;
		bool flag2 = this.progressTimetableIdx == num2 - 1;
		if (TimetableToRemoveIdx < this.progressTimetableIdx || (flag && flag2))
		{
			this.progressTimetableIdx--;
		}
		ScheduleScreen.Instance.OnChangeCurrentTimetable();
	}

	// Token: 0x06005294 RID: 21140 RVA: 0x001E13F7 File Offset: 0x001DF5F7
	public void Assign(Schedulable schedulable)
	{
		if (!this.IsAssigned(schedulable))
		{
			this.GetAssigned().Add(new Ref<Schedulable>(schedulable));
		}
		this.Changed();
	}

	// Token: 0x06005295 RID: 21141 RVA: 0x001E141C File Offset: 0x001DF61C
	public void Unassign(Schedulable schedulable)
	{
		for (int i = 0; i < this.GetAssigned().Count; i++)
		{
			if (this.GetAssigned()[i].Get() == schedulable)
			{
				this.GetAssigned().RemoveAt(i);
				break;
			}
		}
		this.Changed();
	}

	// Token: 0x06005296 RID: 21142 RVA: 0x001E146C File Offset: 0x001DF66C
	public bool IsAssigned(Schedulable schedulable)
	{
		using (List<Ref<Schedulable>>.Enumerator enumerator = this.GetAssigned().GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get() == schedulable)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06005297 RID: 21143 RVA: 0x001E14CC File Offset: 0x001DF6CC
	public static bool AreScheduleTypesIdentical(List<ScheduleBlockType> a, List<ScheduleBlockType> b)
	{
		if (a.Count != b.Count)
		{
			return false;
		}
		foreach (ScheduleBlockType scheduleBlockType in a)
		{
			bool flag = false;
			foreach (ScheduleBlockType scheduleBlockType2 in b)
			{
				if (scheduleBlockType.IdHash == scheduleBlockType2.IdHash)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06005298 RID: 21144 RVA: 0x001E1580 File Offset: 0x001DF780
	public bool ShiftTimetable(bool up, int timetableToShiftIdx = 0)
	{
		if (timetableToShiftIdx == 0 && up)
		{
			return false;
		}
		if (timetableToShiftIdx == this.blocks.Count / 24 - 1 && !up)
		{
			return false;
		}
		int num = timetableToShiftIdx * 24;
		List<ScheduleBlock> list = new List<ScheduleBlock>();
		List<ScheduleBlock> list2 = new List<ScheduleBlock>();
		if (up)
		{
			list = this.blocks.GetRange(num, 24);
			list2 = this.blocks.GetRange(num - 24, 24);
			this.blocks.RemoveRange(num - 24, 48);
			this.blocks.InsertRange(num - 24, list2);
			this.blocks.InsertRange(num - 24, list);
		}
		else
		{
			list = this.blocks.GetRange(num, 24);
			list2 = this.blocks.GetRange(num + 24, 24);
			this.blocks.RemoveRange(num, 48);
			this.blocks.InsertRange(num, list);
			this.blocks.InsertRange(num, list2);
		}
		this.Changed();
		return true;
	}

	// Token: 0x06005299 RID: 21145 RVA: 0x001E1668 File Offset: 0x001DF868
	public void RotateBlocks(bool directionLeft, int timetableToRotateIdx = 0)
	{
		List<ScheduleBlock> list = new List<ScheduleBlock>();
		int num = timetableToRotateIdx * 24;
		list = this.blocks.GetRange(num, 24);
		if (!directionLeft)
		{
			ScheduleGroup scheduleGroup = Db.Get().ScheduleGroups.Get(list[list.Count - 1].GroupId);
			for (int i = list.Count - 1; i >= 1; i--)
			{
				ScheduleGroup scheduleGroup2 = Db.Get().ScheduleGroups.Get(list[i - 1].GroupId);
				list[i].GroupId = scheduleGroup2.Id;
			}
			list[0].GroupId = scheduleGroup.Id;
		}
		else
		{
			ScheduleGroup scheduleGroup3 = Db.Get().ScheduleGroups.Get(list[0].GroupId);
			for (int j = 0; j < list.Count - 1; j++)
			{
				ScheduleGroup scheduleGroup4 = Db.Get().ScheduleGroups.Get(list[j + 1].GroupId);
				list[j].GroupId = scheduleGroup4.Id;
			}
			list[list.Count - 1].GroupId = scheduleGroup3.Id;
		}
		this.blocks.RemoveRange(num, 24);
		this.blocks.InsertRange(num, list);
		this.Changed();
	}

	// Token: 0x0400377B RID: 14203
	[Serialize]
	private List<ScheduleBlock> blocks;

	// Token: 0x0400377C RID: 14204
	[Serialize]
	private List<Ref<Schedulable>> assigned;

	// Token: 0x0400377D RID: 14205
	[Serialize]
	public string name;

	// Token: 0x0400377E RID: 14206
	[Serialize]
	public bool alarmActivated = true;

	// Token: 0x0400377F RID: 14207
	[Serialize]
	private int[] tones;

	// Token: 0x04003780 RID: 14208
	[Serialize]
	public bool isDefaultForBionics;

	// Token: 0x04003781 RID: 14209
	[Serialize]
	private int progressTimetableIdx;

	// Token: 0x04003782 RID: 14210
	public Action<Schedule> onChanged;
}
