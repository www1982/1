using System;

// Token: 0x02000630 RID: 1584
public class ResourceTracker : WorldTracker
{
	// Token: 0x170001BB RID: 443
	// (get) Token: 0x0600265B RID: 9819 RVA: 0x000DAEF8 File Offset: 0x000D90F8
	// (set) Token: 0x0600265C RID: 9820 RVA: 0x000DAF00 File Offset: 0x000D9100
	public Tag tag { get; private set; }

	// Token: 0x0600265D RID: 9821 RVA: 0x000DAF09 File Offset: 0x000D9109
	public ResourceTracker(int worldID, Tag materialCategoryTag)
		: base(worldID)
	{
		this.tag = materialCategoryTag;
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x000DAF1C File Offset: 0x000D911C
	public override void UpdateData()
	{
		if (ClusterManager.Instance.GetWorld(base.WorldID).worldInventory == null)
		{
			return;
		}
		base.AddPoint(ClusterManager.Instance.GetWorld(base.WorldID).worldInventory.GetAmount(this.tag, false));
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x000DAF6E File Offset: 0x000D916E
	public override string FormatValueString(float value)
	{
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
	}
}
