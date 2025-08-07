using System;

// Token: 0x02000629 RID: 1577
public class AllChoresCountTracker : WorldTracker
{
	// Token: 0x06002646 RID: 9798 RVA: 0x000DA9FA File Offset: 0x000D8BFA
	public AllChoresCountTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002647 RID: 9799 RVA: 0x000DAA04 File Offset: 0x000D8C04
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Db.Get().ChoreGroups.Count; i++)
		{
			Tracker choreGroupTracker = TrackerTool.Instance.GetChoreGroupTracker(base.WorldID, Db.Get().ChoreGroups[i]);
			num += ((choreGroupTracker == null) ? 0f : choreGroupTracker.GetCurrentValue());
		}
		base.AddPoint(num);
	}

	// Token: 0x06002648 RID: 9800 RVA: 0x000DAA6C File Offset: 0x000D8C6C
	public override string FormatValueString(float value)
	{
		return value.ToString();
	}
}
