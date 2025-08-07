using System;

// Token: 0x02000638 RID: 1592
public class RocketFuelTracker : WorldTracker
{
	// Token: 0x06002675 RID: 9845 RVA: 0x000DB3DD File Offset: 0x000D95DD
	public RocketFuelTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x000DB3E8 File Offset: 0x000D95E8
	public override void UpdateData()
	{
		Clustercraft component = ClusterManager.Instance.GetWorld(base.WorldID).GetComponent<Clustercraft>();
		base.AddPoint((component != null) ? component.ModuleInterface.FuelRemaining : 0f);
	}

	// Token: 0x06002677 RID: 9847 RVA: 0x000DB42C File Offset: 0x000D962C
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
