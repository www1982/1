using System;

// Token: 0x020002DB RID: 731
public class MinorDigSiteWorkable : FossilExcavationWorkable
{
	// Token: 0x06000EDB RID: 3803 RVA: 0x000578BE File Offset: 0x00055ABE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(90f);
	}

	// Token: 0x06000EDC RID: 3804 RVA: 0x000578D1 File Offset: 0x00055AD1
	protected override void OnSpawn()
	{
		this.digsite = base.gameObject.GetSMI<MinorFossilDigSite.Instance>();
		base.OnSpawn();
	}

	// Token: 0x06000EDD RID: 3805 RVA: 0x000578EC File Offset: 0x00055AEC
	protected override bool IsMarkedForExcavation()
	{
		return this.digsite != null && !this.digsite.sm.IsRevealed.Get(this.digsite) && this.digsite.sm.MarkedForDig.Get(this.digsite);
	}

	// Token: 0x040009AF RID: 2479
	private MinorFossilDigSite.Instance digsite;
}
