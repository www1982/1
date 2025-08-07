using System;
using UnityEngine;

// Token: 0x0200057C RID: 1404
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Cancellable")]
public class Cancellable : KMonoBehaviour
{
	// Token: 0x06001FC7 RID: 8135 RVA: 0x000B6AD0 File Offset: 0x000B4CD0
	protected override void OnPrefabInit()
	{
		base.Subscribe<Cancellable>(2127324410, Cancellable.OnCancelDelegate);
	}

	// Token: 0x06001FC8 RID: 8136 RVA: 0x000B6AE3 File Offset: 0x000B4CE3
	protected virtual void OnCancel(object data)
	{
		this.DeleteObject();
	}

	// Token: 0x04001283 RID: 4739
	private static readonly EventSystem.IntraObjectHandler<Cancellable> OnCancelDelegate = new EventSystem.IntraObjectHandler<Cancellable>(delegate(Cancellable component, object data)
	{
		component.OnCancel(data);
	});
}
