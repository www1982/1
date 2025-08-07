using System;
using UnityEngine;

// Token: 0x02000E61 RID: 3681
public abstract class TargetScreen : KScreen
{
	// Token: 0x06007559 RID: 30041
	public abstract bool IsValidForTarget(GameObject target);

	// Token: 0x0600755A RID: 30042 RVA: 0x002CEE58 File Offset: 0x002CD058
	public virtual void SetTarget(GameObject target)
	{
		Console.WriteLine(target);
		if (this.selectedTarget != target)
		{
			if (this.selectedTarget != null)
			{
				this.OnDeselectTarget(this.selectedTarget);
			}
			this.selectedTarget = target;
			if (this.selectedTarget != null)
			{
				this.OnSelectTarget(this.selectedTarget);
			}
		}
	}

	// Token: 0x0600755B RID: 30043 RVA: 0x002CEEB9 File Offset: 0x002CD0B9
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		this.SetTarget(null);
	}

	// Token: 0x0600755C RID: 30044 RVA: 0x002CEEC8 File Offset: 0x002CD0C8
	public virtual void OnSelectTarget(GameObject target)
	{
		target.Subscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x0600755D RID: 30045 RVA: 0x002CEEE2 File Offset: 0x002CD0E2
	public virtual void OnDeselectTarget(GameObject target)
	{
		target.Unsubscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x0600755E RID: 30046 RVA: 0x002CEEFB File Offset: 0x002CD0FB
	private void OnTargetDestroyed(object data)
	{
		DetailsScreen.Instance.Show(false);
		this.SetTarget(null);
	}

	// Token: 0x04005160 RID: 20832
	protected GameObject selectedTarget;
}
