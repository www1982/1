using System;

// Token: 0x02000634 RID: 1588
public class KCalTracker : WorldTracker
{
	// Token: 0x06002669 RID: 9833 RVA: 0x000DB214 File Offset: 0x000D9414
	public KCalTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x000DB21D File Offset: 0x000D941D
	public override void UpdateData()
	{
		base.AddPoint(WorldResourceAmountTracker<RationTracker>.Get().CountAmount(null, ClusterManager.Instance.GetWorld(base.WorldID).worldInventory, true));
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x000DB246 File Offset: 0x000D9446
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedCalories(value, GameUtil.TimeSlice.None, true);
	}
}
