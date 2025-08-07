using System;

// Token: 0x020005D5 RID: 1493
public class LongRangeSculpture : Sculpture
{
	// Token: 0x0600228F RID: 8847 RVA: 0x000C64E0 File Offset: 0x000C46E0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = null;
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.multitoolContext = "dig";
		this.multitoolHitEffectTag = "fx_dig_splash";
	}
}
