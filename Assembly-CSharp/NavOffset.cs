using System;

// Token: 0x020004CA RID: 1226
public struct NavOffset
{
	// Token: 0x06001A37 RID: 6711 RVA: 0x00091394 File Offset: 0x0008F594
	public NavOffset(NavType nav_type, int x, int y)
	{
		this.navType = nav_type;
		this.offset.x = x;
		this.offset.y = y;
	}

	// Token: 0x04000F24 RID: 3876
	public NavType navType;

	// Token: 0x04000F25 RID: 3877
	public CellOffset offset;
}
