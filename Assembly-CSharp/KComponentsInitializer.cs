using System;

// Token: 0x0200099E RID: 2462
public class KComponentsInitializer : KComponentSpawn
{
	// Token: 0x06004763 RID: 18275 RVA: 0x0019B603 File Offset: 0x00199803
	private void Awake()
	{
		KComponentSpawn.instance = this;
		this.comps = new GameComps();
	}
}
