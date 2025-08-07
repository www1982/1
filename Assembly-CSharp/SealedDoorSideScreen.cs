using System;
using UnityEngine;

// Token: 0x02000E2A RID: 3626
public class SealedDoorSideScreen : SideScreenContent
{
	// Token: 0x060072B5 RID: 29365 RVA: 0x002B97A4 File Offset: 0x002B79A4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.button.onClick += delegate
		{
			this.target.OrderUnseal();
		};
		this.Refresh();
	}

	// Token: 0x060072B6 RID: 29366 RVA: 0x002B97C9 File Offset: 0x002B79C9
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Door>() != null;
	}

	// Token: 0x060072B7 RID: 29367 RVA: 0x002B97D8 File Offset: 0x002B79D8
	public override void SetTarget(GameObject target)
	{
		Door component = target.GetComponent<Door>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a Door associated with it.");
			return;
		}
		this.target = component;
		this.Refresh();
	}

	// Token: 0x060072B8 RID: 29368 RVA: 0x002B980D File Offset: 0x002B7A0D
	private void Refresh()
	{
		if (!this.target.isSealed)
		{
			this.ContentContainer.SetActive(false);
			return;
		}
		this.ContentContainer.SetActive(true);
	}

	// Token: 0x04004EFC RID: 20220
	[SerializeField]
	private LocText label;

	// Token: 0x04004EFD RID: 20221
	[SerializeField]
	private KButton button;

	// Token: 0x04004EFE RID: 20222
	[SerializeField]
	private Door target;
}
