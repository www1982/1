using System;

// Token: 0x02000BBE RID: 3006
public class TravelTubeUtilityNetworkLink : UtilityNetworkLink, IHaveUtilityNetworkMgr
{
	// Token: 0x060059FD RID: 23037 RVA: 0x00208131 File Offset: 0x00206331
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x060059FE RID: 23038 RVA: 0x00208139 File Offset: 0x00206339
	protected override void OnConnect(int cell1, int cell2)
	{
		Game.Instance.travelTubeSystem.AddLink(cell1, cell2);
	}

	// Token: 0x060059FF RID: 23039 RVA: 0x0020814C File Offset: 0x0020634C
	protected override void OnDisconnect(int cell1, int cell2)
	{
		Game.Instance.travelTubeSystem.RemoveLink(cell1, cell2);
	}

	// Token: 0x06005A00 RID: 23040 RVA: 0x0020815F File Offset: 0x0020635F
	public IUtilityNetworkMgr GetNetworkManager()
	{
		return Game.Instance.travelTubeSystem;
	}
}
