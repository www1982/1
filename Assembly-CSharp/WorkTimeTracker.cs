using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200062C RID: 1580
public class WorkTimeTracker : WorldTracker
{
	// Token: 0x0600264F RID: 9807 RVA: 0x000DAC42 File Offset: 0x000D8E42
	public WorkTimeTracker(int worldID, ChoreGroup group)
		: base(worldID)
	{
		this.choreGroup = group;
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x000DAC54 File Offset: 0x000D8E54
	public override void UpdateData()
	{
		float num = 0f;
		List<MinionIdentity> worldItems = Components.LiveMinionIdentities.GetWorldItems(base.WorldID, false);
		Chore chore;
		Predicate<ChoreType> <>9__0;
		foreach (MinionIdentity minionIdentity in worldItems)
		{
			chore = minionIdentity.GetComponent<ChoreConsumer>().choreDriver.GetCurrentChore();
			if (chore != null)
			{
				List<ChoreType> choreTypes = this.choreGroup.choreTypes;
				Predicate<ChoreType> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = (ChoreType match) => match == chore.choreType);
				}
				if (choreTypes.Find(predicate) != null)
				{
					num += 1f;
				}
			}
		}
		base.AddPoint(num / (float)worldItems.Count * 100f);
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x000DAD2C File Offset: 0x000D8F2C
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedPercent(Mathf.Round(value), GameUtil.TimeSlice.None).ToString();
	}

	// Token: 0x0400167C RID: 5756
	public ChoreGroup choreGroup;
}
