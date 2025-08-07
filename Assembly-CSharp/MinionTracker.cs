using System;

// Token: 0x0200063C RID: 1596
public abstract class MinionTracker : Tracker
{
	// Token: 0x06002681 RID: 9857 RVA: 0x000DB539 File Offset: 0x000D9739
	public MinionTracker(MinionIdentity identity)
	{
		this.identity = identity;
	}

	// Token: 0x0400167F RID: 5759
	public MinionIdentity identity;
}
