using System;

// Token: 0x0200062B RID: 1579
public class AllWorkTimeTracker : WorldTracker
{
	// Token: 0x0600264C RID: 9804 RVA: 0x000DABCE File Offset: 0x000D8DCE
	public AllWorkTimeTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x0600264D RID: 9805 RVA: 0x000DABD8 File Offset: 0x000D8DD8
	public override void UpdateData()
	{
		float num = 0f;
		for (int i = 0; i < Db.Get().ChoreGroups.Count; i++)
		{
			num += TrackerTool.Instance.GetWorkTimeTracker(base.WorldID, Db.Get().ChoreGroups[i]).GetCurrentValue();
		}
		base.AddPoint(num);
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x000DAC34 File Offset: 0x000D8E34
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedPercent(value, GameUtil.TimeSlice.None).ToString();
	}
}
