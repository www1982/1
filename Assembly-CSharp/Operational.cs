using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020005F3 RID: 1523
[AddComponentMenu("KMonoBehaviour/scripts/Operational")]
public class Operational : KMonoBehaviour
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x060023AE RID: 9134 RVA: 0x000CBB47 File Offset: 0x000C9D47
	// (set) Token: 0x060023AF RID: 9135 RVA: 0x000CBB4F File Offset: 0x000C9D4F
	public bool IsFunctional { get; private set; }

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x060023B0 RID: 9136 RVA: 0x000CBB58 File Offset: 0x000C9D58
	// (set) Token: 0x060023B1 RID: 9137 RVA: 0x000CBB60 File Offset: 0x000C9D60
	public bool IsOperational { get; private set; }

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000CBB69 File Offset: 0x000C9D69
	// (set) Token: 0x060023B3 RID: 9139 RVA: 0x000CBB71 File Offset: 0x000C9D71
	public bool IsActive { get; private set; }

	// Token: 0x060023B4 RID: 9140 RVA: 0x000CBB7A File Offset: 0x000C9D7A
	[OnSerializing]
	private void OnSerializing()
	{
		this.AddTimeData(this.IsActive);
		this.activeStartTime = GameClock.Instance.GetTime();
		this.inactiveStartTime = GameClock.Instance.GetTime();
	}

	// Token: 0x060023B5 RID: 9141 RVA: 0x000CBBA8 File Offset: 0x000C9DA8
	protected override void OnPrefabInit()
	{
		this.UpdateFunctional();
		this.UpdateOperational();
		base.Subscribe<Operational>(-1661515756, Operational.OnNewBuildingDelegate);
		GameClock.Instance.Subscribe(631075836, new Action<object>(this.OnNewDay));
	}

	// Token: 0x060023B6 RID: 9142 RVA: 0x000CBBE4 File Offset: 0x000C9DE4
	public void OnNewBuilding(object data)
	{
		BuildingComplete component = base.GetComponent<BuildingComplete>();
		if (component.creationTime > 0f)
		{
			this.inactiveStartTime = component.creationTime;
			this.activeStartTime = component.creationTime;
		}
	}

	// Token: 0x060023B7 RID: 9143 RVA: 0x000CBC1D File Offset: 0x000C9E1D
	public bool IsOperationalType(Operational.Flag.Type type)
	{
		if (type == Operational.Flag.Type.Functional)
		{
			return this.IsFunctional;
		}
		return this.IsOperational;
	}

	// Token: 0x060023B8 RID: 9144 RVA: 0x000CBC30 File Offset: 0x000C9E30
	public void SetFlag(Operational.Flag flag, bool value)
	{
		bool flag2 = false;
		if (this.Flags.TryGetValue(flag, out flag2))
		{
			if (flag2 != value)
			{
				this.Flags[flag] = value;
				base.Trigger(187661686, flag);
			}
		}
		else
		{
			this.Flags[flag] = value;
			base.Trigger(187661686, flag);
		}
		if (flag.FlagType == Operational.Flag.Type.Functional && value != this.IsFunctional)
		{
			this.UpdateFunctional();
		}
		if (value != this.IsOperational)
		{
			this.UpdateOperational();
		}
	}

	// Token: 0x060023B9 RID: 9145 RVA: 0x000CBCB0 File Offset: 0x000C9EB0
	public bool GetFlag(Operational.Flag flag)
	{
		bool flag2 = false;
		this.Flags.TryGetValue(flag, out flag2);
		return flag2;
	}

	// Token: 0x060023BA RID: 9146 RVA: 0x000CBCD0 File Offset: 0x000C9ED0
	private void UpdateFunctional()
	{
		bool flag = true;
		foreach (KeyValuePair<Operational.Flag, bool> keyValuePair in this.Flags)
		{
			if (keyValuePair.Key.FlagType == Operational.Flag.Type.Functional && !keyValuePair.Value)
			{
				flag = false;
				break;
			}
		}
		this.IsFunctional = flag;
		base.Trigger(-1852328367, this.IsFunctional);
	}

	// Token: 0x060023BB RID: 9147 RVA: 0x000CBD58 File Offset: 0x000C9F58
	private void UpdateOperational()
	{
		Dictionary<Operational.Flag, bool>.Enumerator enumerator = this.Flags.GetEnumerator();
		bool flag = true;
		while (enumerator.MoveNext())
		{
			KeyValuePair<Operational.Flag, bool> keyValuePair = enumerator.Current;
			if (!keyValuePair.Value)
			{
				flag = false;
				break;
			}
		}
		if (flag != this.IsOperational)
		{
			this.IsOperational = flag;
			if (!this.IsOperational)
			{
				this.SetActive(false, false);
			}
			if (this.IsOperational)
			{
				base.GetComponent<KPrefabID>().AddTag(GameTags.Operational, false);
			}
			else
			{
				base.GetComponent<KPrefabID>().RemoveTag(GameTags.Operational);
			}
			base.Trigger(-592767678, this.IsOperational);
			Game.Instance.Trigger(-809948329, base.gameObject);
		}
	}

	// Token: 0x060023BC RID: 9148 RVA: 0x000CBE09 File Offset: 0x000CA009
	public void SetActive(bool value, bool force_ignore = false)
	{
		if (this.IsActive != value)
		{
			this.AddTimeData(value);
			base.Trigger(824508782, this);
			Game.Instance.Trigger(-809948329, base.gameObject);
		}
	}

	// Token: 0x060023BD RID: 9149 RVA: 0x000CBE3C File Offset: 0x000CA03C
	private void AddTimeData(bool value)
	{
		float num = (this.IsActive ? this.activeStartTime : this.inactiveStartTime);
		float time = GameClock.Instance.GetTime();
		float num2 = time - num;
		if (this.IsActive)
		{
			this.activeTime += num2;
		}
		else
		{
			this.inactiveTime += num2;
		}
		this.IsActive = value;
		if (this.IsActive)
		{
			this.activeStartTime = time;
			return;
		}
		this.inactiveStartTime = time;
	}

	// Token: 0x060023BE RID: 9150 RVA: 0x000CBEB4 File Offset: 0x000CA0B4
	public void OnNewDay(object data)
	{
		this.AddTimeData(this.IsActive);
		this.uptimeData.Add(this.activeTime / 600f);
		while (this.uptimeData.Count > this.MAX_DATA_POINTS)
		{
			this.uptimeData.RemoveAt(0);
		}
		this.activeTime = 0f;
		this.inactiveTime = 0f;
	}

	// Token: 0x060023BF RID: 9151 RVA: 0x000CBF1C File Offset: 0x000CA11C
	public float GetCurrentCycleUptime()
	{
		if (this.IsActive)
		{
			float num = (this.IsActive ? this.activeStartTime : this.inactiveStartTime);
			float num2 = GameClock.Instance.GetTime() - num;
			return (this.activeTime + num2) / GameClock.Instance.GetTimeSinceStartOfCycle();
		}
		return this.activeTime / GameClock.Instance.GetTimeSinceStartOfCycle();
	}

	// Token: 0x060023C0 RID: 9152 RVA: 0x000CBF7A File Offset: 0x000CA17A
	public float GetLastCycleUptime()
	{
		if (this.uptimeData.Count > 0)
		{
			return this.uptimeData[this.uptimeData.Count - 1];
		}
		return 0f;
	}

	// Token: 0x060023C1 RID: 9153 RVA: 0x000CBFA8 File Offset: 0x000CA1A8
	public float GetUptimeOverCycles(int num_cycles)
	{
		if (this.uptimeData.Count > 0)
		{
			int num = Mathf.Min(this.uptimeData.Count, num_cycles);
			float num2 = 0f;
			for (int i = num - 1; i >= 0; i--)
			{
				num2 += this.uptimeData[i];
			}
			return num2 / (float)num;
		}
		return 0f;
	}

	// Token: 0x060023C2 RID: 9154 RVA: 0x000CC002 File Offset: 0x000CA202
	public bool MeetsRequirements(Operational.State stateRequirement)
	{
		switch (stateRequirement)
		{
		case Operational.State.Operational:
			return this.IsOperational;
		case Operational.State.Functional:
			return this.IsFunctional;
		case Operational.State.Active:
			return this.IsActive;
		}
		return true;
	}

	// Token: 0x060023C3 RID: 9155 RVA: 0x000CC032 File Offset: 0x000CA232
	public static GameHashes GetEventForState(Operational.State state)
	{
		if (state == Operational.State.Operational)
		{
			return GameHashes.OperationalChanged;
		}
		if (state == Operational.State.Functional)
		{
			return GameHashes.FunctionalChanged;
		}
		return GameHashes.ActiveChanged;
	}

	// Token: 0x040014CD RID: 5325
	[Serialize]
	public float inactiveStartTime;

	// Token: 0x040014CE RID: 5326
	[Serialize]
	public float activeStartTime;

	// Token: 0x040014CF RID: 5327
	[Serialize]
	private List<float> uptimeData = new List<float>();

	// Token: 0x040014D0 RID: 5328
	[Serialize]
	private float activeTime;

	// Token: 0x040014D1 RID: 5329
	[Serialize]
	private float inactiveTime;

	// Token: 0x040014D2 RID: 5330
	private int MAX_DATA_POINTS = 5;

	// Token: 0x040014D3 RID: 5331
	public Dictionary<Operational.Flag, bool> Flags = new Dictionary<Operational.Flag, bool>();

	// Token: 0x040014D4 RID: 5332
	private static readonly EventSystem.IntraObjectHandler<Operational> OnNewBuildingDelegate = new EventSystem.IntraObjectHandler<Operational>(delegate(Operational component, object data)
	{
		component.OnNewBuilding(data);
	});

	// Token: 0x02001480 RID: 5248
	public enum State
	{
		// Token: 0x04006CC8 RID: 27848
		Operational,
		// Token: 0x04006CC9 RID: 27849
		Functional,
		// Token: 0x04006CCA RID: 27850
		Active,
		// Token: 0x04006CCB RID: 27851
		None
	}

	// Token: 0x02001481 RID: 5249
	public class Flag
	{
		// Token: 0x06008DED RID: 36333 RVA: 0x00359F05 File Offset: 0x00358105
		public Flag(string name, Operational.Flag.Type type)
		{
			this.Name = name;
			this.FlagType = type;
		}

		// Token: 0x06008DEE RID: 36334 RVA: 0x00359F1B File Offset: 0x0035811B
		public static Operational.Flag.Type GetFlagType(Operational.State operationalState)
		{
			switch (operationalState)
			{
			case Operational.State.Operational:
			case Operational.State.Active:
				return Operational.Flag.Type.Requirement;
			case Operational.State.Functional:
				return Operational.Flag.Type.Functional;
			}
			throw new InvalidOperationException("Can not convert NONE state to an Operational Flag Type");
		}

		// Token: 0x04006CCC RID: 27852
		public string Name;

		// Token: 0x04006CCD RID: 27853
		public Operational.Flag.Type FlagType;

		// Token: 0x02002741 RID: 10049
		public enum Type
		{
			// Token: 0x0400AD49 RID: 44361
			Requirement,
			// Token: 0x0400AD4A RID: 44362
			Functional
		}
	}
}
