using System;

// Token: 0x020002B9 RID: 697
public class MajorDigSiteWorkable : FossilExcavationWorkable
{
	// Token: 0x06000E27 RID: 3623 RVA: 0x00052B6A File Offset: 0x00050D6A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetWorkTime(90f);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x00052B7D File Offset: 0x00050D7D
	protected override void OnSpawn()
	{
		this.digsite = base.gameObject.GetSMI<MajorFossilDigSite.Instance>();
		base.OnSpawn();
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x00052B98 File Offset: 0x00050D98
	protected override bool IsMarkedForExcavation()
	{
		return this.digsite != null && !this.digsite.sm.IsRevealed.Get(this.digsite) && this.digsite.sm.MarkedForDig.Get(this.digsite);
	}

	// Token: 0x0400092A RID: 2346
	private MajorFossilDigSite.Instance digsite;
}
