using System;
using UnityEngine;

// Token: 0x02000611 RID: 1553
[AddComponentMenu("KMonoBehaviour/scripts/SimpleVent")]
public class SimpleVent : KMonoBehaviour
{
	// Token: 0x060024E6 RID: 9446 RVA: 0x000D2C02 File Offset: 0x000D0E02
	protected override void OnPrefabInit()
	{
		base.Subscribe<SimpleVent>(-592767678, SimpleVent.OnChangedDelegate);
		base.Subscribe<SimpleVent>(-111137758, SimpleVent.OnChangedDelegate);
	}

	// Token: 0x060024E7 RID: 9447 RVA: 0x000D2C26 File Offset: 0x000D0E26
	protected override void OnSpawn()
	{
		this.OnChanged(null);
	}

	// Token: 0x060024E8 RID: 9448 RVA: 0x000D2C30 File Offset: 0x000D0E30
	private void OnChanged(object data)
	{
		if (this.operational.IsFunctional)
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, this);
			return;
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
	}

	// Token: 0x040015A2 RID: 5538
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040015A3 RID: 5539
	private static readonly EventSystem.IntraObjectHandler<SimpleVent> OnChangedDelegate = new EventSystem.IntraObjectHandler<SimpleVent>(delegate(SimpleVent component, object data)
	{
		component.OnChanged(data);
	});
}
