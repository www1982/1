using System;

// Token: 0x02000639 RID: 1593
public class RocketOxidizerTracker : WorldTracker
{
	// Token: 0x06002678 RID: 9848 RVA: 0x000DB43C File Offset: 0x000D963C
	public RocketOxidizerTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002679 RID: 9849 RVA: 0x000DB448 File Offset: 0x000D9648
	public override void UpdateData()
	{
		Clustercraft component = ClusterManager.Instance.GetWorld(base.WorldID).GetComponent<Clustercraft>();
		base.AddPoint((component != null) ? component.ModuleInterface.OxidizerPowerRemaining : 0f);
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x000DB48C File Offset: 0x000D968C
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
