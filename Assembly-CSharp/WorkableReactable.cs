using System;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class WorkableReactable : Reactable
{
	// Token: 0x06001B0F RID: 6927 RVA: 0x0009537C File Offset: 0x0009357C
	public WorkableReactable(Workable workable, HashedString id, ChoreType chore_type, WorkableReactable.AllowedDirection allowed_direction = WorkableReactable.AllowedDirection.Any)
		: base(workable.gameObject, id, chore_type, 1, 1, false, 0f, 0f, float.PositiveInfinity, 0f, ObjectLayer.NumLayers)
	{
		this.workable = workable;
		this.allowedDirection = allowed_direction;
	}

	// Token: 0x06001B10 RID: 6928 RVA: 0x000953C0 File Offset: 0x000935C0
	public override bool InternalCanBegin(GameObject new_reactor, Navigator.ActiveTransition transition)
	{
		if (this.workable == null)
		{
			return false;
		}
		if (this.reactor != null)
		{
			return false;
		}
		Brain component = new_reactor.GetComponent<Brain>();
		if (component == null)
		{
			return false;
		}
		if (!component.IsRunning())
		{
			return false;
		}
		Navigator component2 = new_reactor.GetComponent<Navigator>();
		if (component2 == null)
		{
			return false;
		}
		if (!component2.IsMoving())
		{
			return false;
		}
		if (this.allowedDirection == WorkableReactable.AllowedDirection.Any)
		{
			return true;
		}
		Facing component3 = new_reactor.GetComponent<Facing>();
		if (component3 == null)
		{
			return false;
		}
		bool facing = component3.GetFacing();
		return (!facing || this.allowedDirection != WorkableReactable.AllowedDirection.Right) && (facing || this.allowedDirection != WorkableReactable.AllowedDirection.Left);
	}

	// Token: 0x06001B11 RID: 6929 RVA: 0x00095465 File Offset: 0x00093665
	protected override void InternalBegin()
	{
		this.worker = this.reactor.GetComponent<WorkerBase>();
		this.worker.StartWork(new WorkerBase.StartWorkInfo(this.workable));
	}

	// Token: 0x06001B12 RID: 6930 RVA: 0x0009548E File Offset: 0x0009368E
	public override void Update(float dt)
	{
		if (this.worker.GetWorkable() == null)
		{
			base.End();
			return;
		}
		if (this.worker.Work(dt) != WorkerBase.WorkResult.InProgress)
		{
			base.End();
		}
	}

	// Token: 0x06001B13 RID: 6931 RVA: 0x000954BF File Offset: 0x000936BF
	protected override void InternalEnd()
	{
		if (this.worker != null)
		{
			this.worker.StopWork();
		}
	}

	// Token: 0x06001B14 RID: 6932 RVA: 0x000954DA File Offset: 0x000936DA
	protected override void InternalCleanup()
	{
	}

	// Token: 0x04000FF9 RID: 4089
	protected Workable workable;

	// Token: 0x04000FFA RID: 4090
	private WorkerBase worker;

	// Token: 0x04000FFB RID: 4091
	public WorkableReactable.AllowedDirection allowedDirection;

	// Token: 0x0200133E RID: 4926
	public enum AllowedDirection
	{
		// Token: 0x040068EC RID: 26860
		Any,
		// Token: 0x040068ED RID: 26861
		Left,
		// Token: 0x040068EE RID: 26862
		Right
	}
}
