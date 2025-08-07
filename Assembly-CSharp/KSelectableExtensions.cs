using System;
using UnityEngine;

// Token: 0x020005CD RID: 1485
public static class KSelectableExtensions
{
	// Token: 0x0600225E RID: 8798 RVA: 0x000C5667 File Offset: 0x000C3867
	public static string GetProperName(this Component cmp)
	{
		if (cmp != null && cmp.gameObject != null)
		{
			return cmp.gameObject.GetProperName();
		}
		return "";
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x000C5694 File Offset: 0x000C3894
	public static string GetProperName(this GameObject go)
	{
		if (go != null)
		{
			KSelectable component = go.GetComponent<KSelectable>();
			if (component != null)
			{
				return component.GetName();
			}
		}
		return "";
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x000C56C6 File Offset: 0x000C38C6
	public static string GetProperName(this KSelectable cmp)
	{
		if (cmp != null)
		{
			return cmp.GetName();
		}
		return "";
	}
}
