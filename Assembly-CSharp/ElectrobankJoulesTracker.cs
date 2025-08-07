using System;

// Token: 0x02000635 RID: 1589
public class ElectrobankJoulesTracker : WorldTracker
{
	// Token: 0x0600266C RID: 9836 RVA: 0x000DB250 File Offset: 0x000D9450
	public ElectrobankJoulesTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x000DB259 File Offset: 0x000D9459
	public override void UpdateData()
	{
		base.AddPoint(WorldResourceAmountTracker<ElectrobankTracker>.Get().CountAmount(null, ClusterManager.Instance.GetWorld(base.WorldID).worldInventory, true));
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x000DB282 File Offset: 0x000D9482
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedJoules(value, "F1", GameUtil.TimeSlice.None);
	}
}
