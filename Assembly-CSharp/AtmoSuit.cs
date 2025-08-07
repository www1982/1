using System;
using Klei.AI;
using UnityEngine;

// Token: 0x020006B1 RID: 1713
[AddComponentMenu("KMonoBehaviour/scripts/AtmoSuit")]
public class AtmoSuit : KMonoBehaviour
{
	// Token: 0x06002A15 RID: 10773 RVA: 0x000F3E77 File Offset: 0x000F2077
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<AtmoSuit>(-1697596308, AtmoSuit.OnStorageChangedDelegate);
	}

	// Token: 0x06002A16 RID: 10774 RVA: 0x000F3E90 File Offset: 0x000F2090
	private void RefreshStatusEffects(object data)
	{
		if (this == null)
		{
			return;
		}
		Equippable component = base.GetComponent<Equippable>();
		Storage component2 = base.GetComponent<Storage>();
		bool flag = component2.Has(GameTags.AnyWater) || component2.Has(SimHashes.LiquidGunk.CreateTag());
		if (component.assignee != null && flag)
		{
			Ownables soleOwner = component.assignee.GetSoleOwner();
			if (soleOwner != null)
			{
				GameObject targetGameObject = soleOwner.GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
				if (targetGameObject)
				{
					AssignableSlotInstance slot = ((KMonoBehaviour)component.assignee).GetComponent<Equipment>().GetSlot(component.slot);
					Effects component3 = targetGameObject.GetComponent<Effects>();
					if (component3 != null && !component3.HasEffect("SoiledSuit") && !slot.IsUnassigning())
					{
						component3.Add("SoiledSuit", true);
					}
				}
			}
		}
	}

	// Token: 0x040018F1 RID: 6385
	private static readonly EventSystem.IntraObjectHandler<AtmoSuit> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<AtmoSuit>(delegate(AtmoSuit component, object data)
	{
		component.RefreshStatusEffects(data);
	});
}
