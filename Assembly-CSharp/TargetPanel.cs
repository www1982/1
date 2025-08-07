using System;
using UnityEngine;

// Token: 0x02000E60 RID: 3680
public abstract class TargetPanel : KMonoBehaviour
{
	// Token: 0x06007553 RID: 30035
	public abstract bool IsValidForTarget(GameObject target);

	// Token: 0x06007554 RID: 30036 RVA: 0x002CEDB0 File Offset: 0x002CCFB0
	public virtual void SetTarget(GameObject target)
	{
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

	// Token: 0x06007555 RID: 30037 RVA: 0x002CEE06 File Offset: 0x002CD006
	protected virtual void OnSelectTarget(GameObject target)
	{
		target.Subscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x06007556 RID: 30038 RVA: 0x002CEE20 File Offset: 0x002CD020
	public virtual void OnDeselectTarget(GameObject target)
	{
		target.Unsubscribe(1502190696, new Action<object>(this.OnTargetDestroyed));
	}

	// Token: 0x06007557 RID: 30039 RVA: 0x002CEE39 File Offset: 0x002CD039
	private void OnTargetDestroyed(object data)
	{
		DetailsScreen.Instance.Show(false);
		this.SetTarget(null);
	}

	// Token: 0x0400515F RID: 20831
	protected GameObject selectedTarget;
}
