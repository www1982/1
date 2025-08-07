using System;

// Token: 0x0200062D RID: 1581
public class CropTracker : WorldTracker
{
	// Token: 0x06002652 RID: 9810 RVA: 0x000DAD3F File Offset: 0x000D8F3F
	public CropTracker(int worldID)
		: base(worldID)
	{
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x000DAD48 File Offset: 0x000D8F48
	public override void UpdateData()
	{
		float num = 0f;
		foreach (PlantablePlot plantablePlot in Components.PlantablePlots.GetItems(base.WorldID))
		{
			if (!(plantablePlot.plant == null) && plantablePlot.HasDepositTag(GameTags.CropSeed) && !plantablePlot.plant.HasTag(GameTags.Wilting))
			{
				num += 1f;
			}
		}
		base.AddPoint(num);
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x000DADE0 File Offset: 0x000D8FE0
	public override string FormatValueString(float value)
	{
		return value.ToString() + "%";
	}
}
