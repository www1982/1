using System;
using UnityEngine;

// Token: 0x02000E29 RID: 3625
public class RoleStationSideScreen : SideScreenContent
{
	// Token: 0x060072B2 RID: 29362 RVA: 0x002B9791 File Offset: 0x002B7991
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x060072B3 RID: 29363 RVA: 0x002B9799 File Offset: 0x002B7999
	public override bool IsValidForTarget(GameObject target)
	{
		return false;
	}

	// Token: 0x04004EF9 RID: 20217
	public GameObject content;

	// Token: 0x04004EFA RID: 20218
	private GameObject target;

	// Token: 0x04004EFB RID: 20219
	public LocText DescriptionText;
}
