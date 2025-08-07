using System;

// Token: 0x02000A94 RID: 2708
public abstract class RemoteDockWorkTargetComponent : KMonoBehaviour, IRemoteDockWorkTarget
{
	// Token: 0x06004E9D RID: 20125 RVA: 0x001C726D File Offset: 0x001C546D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.RemoteDockWorkTargets.Add(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x06004E9E RID: 20126 RVA: 0x001C728B File Offset: 0x001C548B
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Components.RemoteDockWorkTargets.Remove(base.gameObject.GetMyWorldId(), this);
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x06004E9F RID: 20127
	public abstract Chore RemoteDockChore { get; }

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x06004EA0 RID: 20128 RVA: 0x001C72A9 File Offset: 0x001C54A9
	public virtual IApproachable Approachable
	{
		get
		{
			return base.gameObject.GetComponent<IApproachable>();
		}
	}
}
