using System;

// Token: 0x02000A92 RID: 2706
public interface IRemoteDockWorkTarget
{
	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x06004E96 RID: 20118
	Chore RemoteDockChore { get; }

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x06004E97 RID: 20119
	IApproachable Approachable { get; }
}
