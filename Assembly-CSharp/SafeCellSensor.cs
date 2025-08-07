using System;
using System.Collections.Generic;
using Klei.AI;

// Token: 0x02000501 RID: 1281
public class SafeCellSensor : Sensor
{
	// Token: 0x06001B60 RID: 7008 RVA: 0x0009660C File Offset: 0x0009480C
	private SafeCellQuery.SafeFlags GetIgnoredFlags()
	{
		SafeCellQuery.SafeFlags safeFlags = (SafeCellQuery.SafeFlags)0;
		foreach (string text in this.ignoredFlagsSets.Keys)
		{
			SafeCellQuery.SafeFlags safeFlags2 = this.ignoredFlagsSets[text];
			safeFlags |= safeFlags2;
		}
		return safeFlags;
	}

	// Token: 0x06001B61 RID: 7009 RVA: 0x00096674 File Offset: 0x00094874
	public void AddIgnoredFlagsSet(string setID, SafeCellQuery.SafeFlags flagsToIgnore)
	{
		if (this.ignoredFlagsSets.ContainsKey(setID))
		{
			this.ignoredFlagsSets[setID] = flagsToIgnore;
			return;
		}
		this.ignoredFlagsSets.Add(setID, flagsToIgnore);
	}

	// Token: 0x06001B62 RID: 7010 RVA: 0x0009669F File Offset: 0x0009489F
	public void RemoveIgnoredFlagsSet(string setID)
	{
		if (this.ignoredFlagsSets.ContainsKey(setID))
		{
			this.ignoredFlagsSets.Remove(setID);
		}
	}

	// Token: 0x06001B63 RID: 7011 RVA: 0x000966BC File Offset: 0x000948BC
	public SafeCellSensor(Sensors sensors, bool startEnabled = true)
		: base(sensors, startEnabled)
	{
		this.navigator = base.GetComponent<Navigator>();
		this.brain = base.GetComponent<MinionBrain>();
		this.prefabid = base.GetComponent<KPrefabID>();
		this.traits = base.GetComponent<Traits>();
	}

	// Token: 0x06001B64 RID: 7012 RVA: 0x00096718 File Offset: 0x00094918
	public override void Update()
	{
		if (!this.prefabid.HasTag(GameTags.Idle))
		{
			this.cell = Grid.InvalidCell;
			return;
		}
		bool flag = this.HasSafeCell();
		this.RunSafeCellQuery(false);
		bool flag2 = this.HasSafeCell();
		if (flag2 != flag)
		{
			if (flag2)
			{
				this.sensors.Trigger(982561777, null);
				return;
			}
			this.sensors.Trigger(506919987, null);
		}
	}

	// Token: 0x06001B65 RID: 7013 RVA: 0x00096782 File Offset: 0x00094982
	public void RunSafeCellQuery(bool avoid_light)
	{
		this.cell = this.RunAndGetSafeCellQueryResult(avoid_light);
		if (this.cell == Grid.PosToCell(this.navigator))
		{
			this.cell = Grid.InvalidCell;
		}
	}

	// Token: 0x06001B66 RID: 7014 RVA: 0x000967B0 File Offset: 0x000949B0
	public int RunAndGetSafeCellQueryResult(bool avoid_light)
	{
		MinionPathFinderAbilities minionPathFinderAbilities = (MinionPathFinderAbilities)this.navigator.GetCurrentAbilities();
		minionPathFinderAbilities.SetIdleNavMaskEnabled(true);
		SafeCellQuery safeCellQuery = PathFinderQueries.safeCellQuery.Reset(this.brain, avoid_light, this.GetIgnoredFlags());
		this.navigator.RunQuery(safeCellQuery);
		minionPathFinderAbilities.SetIdleNavMaskEnabled(false);
		this.cell = safeCellQuery.GetResultCell();
		return this.cell;
	}

	// Token: 0x06001B67 RID: 7015 RVA: 0x00096810 File Offset: 0x00094A10
	public int GetSensorCell()
	{
		return this.cell;
	}

	// Token: 0x06001B68 RID: 7016 RVA: 0x00096818 File Offset: 0x00094A18
	public int GetCellQuery()
	{
		if (this.cell == Grid.InvalidCell)
		{
			this.RunSafeCellQuery(false);
		}
		return this.cell;
	}

	// Token: 0x06001B69 RID: 7017 RVA: 0x00096834 File Offset: 0x00094A34
	public int GetSleepCellQuery()
	{
		if (this.cell == Grid.InvalidCell)
		{
			this.RunSafeCellQuery(!this.traits.HasTrait("NightLight"));
		}
		return this.cell;
	}

	// Token: 0x06001B6A RID: 7018 RVA: 0x00096865 File Offset: 0x00094A65
	public bool HasSafeCell()
	{
		return this.cell != Grid.InvalidCell && this.cell != Grid.PosToCell(this.sensors);
	}

	// Token: 0x04001028 RID: 4136
	private MinionBrain brain;

	// Token: 0x04001029 RID: 4137
	private Navigator navigator;

	// Token: 0x0400102A RID: 4138
	private KPrefabID prefabid;

	// Token: 0x0400102B RID: 4139
	private Traits traits;

	// Token: 0x0400102C RID: 4140
	private int cell = Grid.InvalidCell;

	// Token: 0x0400102D RID: 4141
	private Dictionary<string, SafeCellQuery.SafeFlags> ignoredFlagsSets = new Dictionary<string, SafeCellQuery.SafeFlags>();
}
