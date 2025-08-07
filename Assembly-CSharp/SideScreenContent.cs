using System;
using UnityEngine;

// Token: 0x02000E30 RID: 3632
public abstract class SideScreenContent : KScreen
{
	// Token: 0x0600730B RID: 29451 RVA: 0x002BC896 File Offset: 0x002BAA96
	public virtual void SetTarget(GameObject target)
	{
	}

	// Token: 0x0600730C RID: 29452 RVA: 0x002BC898 File Offset: 0x002BAA98
	public virtual void ClearTarget()
	{
	}

	// Token: 0x0600730D RID: 29453
	public abstract bool IsValidForTarget(GameObject target);

	// Token: 0x0600730E RID: 29454 RVA: 0x002BC89A File Offset: 0x002BAA9A
	public virtual int GetSideScreenSortOrder()
	{
		return 0;
	}

	// Token: 0x0600730F RID: 29455 RVA: 0x002BC89D File Offset: 0x002BAA9D
	public virtual string GetTitle()
	{
		return Strings.Get(this.titleKey);
	}

	// Token: 0x04004F44 RID: 20292
	[SerializeField]
	protected string titleKey;

	// Token: 0x04004F45 RID: 20293
	public GameObject ContentContainer;
}
