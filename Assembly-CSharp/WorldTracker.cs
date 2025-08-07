using System;

// Token: 0x0200063B RID: 1595
public abstract class WorldTracker : Tracker
{
	// Token: 0x170001BC RID: 444
	// (get) Token: 0x0600267E RID: 9854 RVA: 0x000DB519 File Offset: 0x000D9719
	// (set) Token: 0x0600267F RID: 9855 RVA: 0x000DB521 File Offset: 0x000D9721
	public int WorldID { get; private set; }

	// Token: 0x06002680 RID: 9856 RVA: 0x000DB52A File Offset: 0x000D972A
	public WorldTracker(int worldID)
	{
		this.WorldID = worldID;
	}
}
