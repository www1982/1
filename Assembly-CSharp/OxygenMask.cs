using System;
using UnityEngine;

// Token: 0x02000A4F RID: 2639
public class OxygenMask : KMonoBehaviour, ISim200ms
{
	// Token: 0x06004C88 RID: 19592 RVA: 0x001BC269 File Offset: 0x001BA469
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OxygenMask>(608245985, OxygenMask.OnSuitTankDeltaDelegate);
	}

	// Token: 0x06004C89 RID: 19593 RVA: 0x001BC284 File Offset: 0x001BA484
	private void CheckOxygenLevels(object data)
	{
		if (this.suitTank.IsEmpty())
		{
			Equippable component = base.GetComponent<Equippable>();
			if (component.assignee != null)
			{
				Ownables soleOwner = component.assignee.GetSoleOwner();
				if (soleOwner != null)
				{
					soleOwner.GetComponent<Equipment>().Unequip(component);
				}
			}
		}
	}

	// Token: 0x06004C8A RID: 19594 RVA: 0x001BC2D0 File Offset: 0x001BA4D0
	public void Sim200ms(float dt)
	{
		if (base.GetComponent<Equippable>().assignee == null)
		{
			float num = this.leakRate * dt;
			float massAvailable = this.storage.GetMassAvailable(this.suitTank.elementTag);
			num = Mathf.Min(num, massAvailable);
			this.storage.DropSome(this.suitTank.elementTag, num, true, true, default(Vector3), true, false);
		}
		if (this.suitTank.IsEmpty())
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x040032BA RID: 12986
	private static readonly EventSystem.IntraObjectHandler<OxygenMask> OnSuitTankDeltaDelegate = new EventSystem.IntraObjectHandler<OxygenMask>(delegate(OxygenMask component, object data)
	{
		component.CheckOxygenLevels(data);
	});

	// Token: 0x040032BB RID: 12987
	[MyCmpGet]
	private SuitTank suitTank;

	// Token: 0x040032BC RID: 12988
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040032BD RID: 12989
	private float leakRate = 0.1f;
}
