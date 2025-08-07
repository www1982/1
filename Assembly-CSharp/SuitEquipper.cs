using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BAF RID: 2991
[AddComponentMenu("KMonoBehaviour/scripts/SuitEquipper")]
public class SuitEquipper : KMonoBehaviour
{
	// Token: 0x0600596F RID: 22895 RVA: 0x00204E0D File Offset: 0x0020300D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SuitEquipper>(493375141, SuitEquipper.OnRefreshUserMenuDelegate);
	}

	// Token: 0x06005970 RID: 22896 RVA: 0x00204E28 File Offset: 0x00203028
	private void OnRefreshUserMenu(object data)
	{
		foreach (AssignableSlotInstance assignableSlotInstance in base.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			EquipmentSlotInstance equipmentSlotInstance = (EquipmentSlotInstance)assignableSlotInstance;
			Equippable equippable = equipmentSlotInstance.assignable as Equippable;
			if (equippable && equippable.unequippable)
			{
				string text = string.Format(UI.USERMENUACTIONS.UNEQUIP.NAME, equippable.def.GenericName);
				Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("iconDown", text, delegate
				{
					equippable.Unassign();
				}, global::Action.NumActions, null, null, null, "", true), 2f);
			}
		}
	}

	// Token: 0x06005971 RID: 22897 RVA: 0x00204F1C File Offset: 0x0020311C
	public Equippable IsWearingAirtightSuit()
	{
		Equippable equippable = null;
		foreach (AssignableSlotInstance assignableSlotInstance in base.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			Equippable equippable2 = ((EquipmentSlotInstance)assignableSlotInstance).assignable as Equippable;
			if (equippable2 && equippable2.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit) && equippable2.isEquipped)
			{
				equippable = equippable2;
				break;
			}
		}
		return equippable;
	}

	// Token: 0x04003B5A RID: 15194
	private static readonly EventSystem.IntraObjectHandler<SuitEquipper> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<SuitEquipper>(delegate(SuitEquipper component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
