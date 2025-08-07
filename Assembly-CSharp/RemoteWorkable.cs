using System;

// Token: 0x02000A93 RID: 2707
public abstract class RemoteWorkable : Workable, IRemoteDockWorkTarget
{
	// Token: 0x06004E98 RID: 20120 RVA: 0x001C7226 File Offset: 0x001C5426
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.RemoteDockWorkTargets.Add(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x06004E99 RID: 20121 RVA: 0x001C7244 File Offset: 0x001C5444
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.RemoteDockWorkTargets.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x06004E9A RID: 20122
	public abstract Chore RemoteDockChore { get; }

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x06004E9B RID: 20123 RVA: 0x001C7262 File Offset: 0x001C5462
	public virtual IApproachable Approachable
	{
		get
		{
			return this;
		}
	}
}
