using System;

// Token: 0x02000A95 RID: 2709
public class ManuallySetRemoteWorkTargetComponent : RemoteDockWorkTargetComponent
{
	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x001C72BE File Offset: 0x001C54BE
	public override Chore RemoteDockChore
	{
		get
		{
			return this.chore;
		}
	}

	// Token: 0x06004EA3 RID: 20131 RVA: 0x001C72C6 File Offset: 0x001C54C6
	public void SetChore(Chore chore_)
	{
		this.chore = chore_;
	}

	// Token: 0x04003430 RID: 13360
	private Chore chore;
}
