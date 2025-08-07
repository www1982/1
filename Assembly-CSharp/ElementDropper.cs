using System;
using UnityEngine;

// Token: 0x02000722 RID: 1826
[AddComponentMenu("KMonoBehaviour/scripts/ElementDropper")]
public class ElementDropper : KMonoBehaviour
{
	// Token: 0x06002E01 RID: 11777 RVA: 0x00107D3A File Offset: 0x00105F3A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<ElementDropper>(-1697596308, ElementDropper.OnStorageChangedDelegate);
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x00107D53 File Offset: 0x00105F53
	private void OnStorageChanged(object data)
	{
		if (this.storage.GetMassAvailable(this.emitTag) >= this.emitMass)
		{
			this.storage.DropSome(this.emitTag, this.emitMass, false, false, this.emitOffset, true, true);
		}
	}

	// Token: 0x04001B22 RID: 6946
	[SerializeField]
	public Tag emitTag;

	// Token: 0x04001B23 RID: 6947
	[SerializeField]
	public float emitMass;

	// Token: 0x04001B24 RID: 6948
	[SerializeField]
	public Vector3 emitOffset = Vector3.zero;

	// Token: 0x04001B25 RID: 6949
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001B26 RID: 6950
	private static readonly EventSystem.IntraObjectHandler<ElementDropper> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<ElementDropper>(delegate(ElementDropper component, object data)
	{
		component.OnStorageChanged(data);
	});
}
