using System;
using System.Collections.Generic;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000086 RID: 134
public class EquippableBalloonConfig : IEquipmentConfig
{
	// Token: 0x060002A5 RID: 677 RVA: 0x000131B4 File Offset: 0x000113B4
	public EquipmentDef CreateEquipmentDef()
	{
		List<AttributeModifier> list = new List<AttributeModifier>();
		EquipmentDef equipmentDef = EquipmentTemplates.CreateEquipmentDef("EquippableBalloon", EQUIPMENT.TOYS.SLOT, SimHashes.Carbon, EQUIPMENT.TOYS.BALLOON_MASS, EQUIPMENT.VESTS.WARM_VEST_ICON0, null, null, 0, list, null, false, EntityTemplates.CollisionShape.RECTANGLE, 0.75f, 0.4f, null, null);
		equipmentDef.OnEquipCallBack = new Action<Equippable>(this.OnEquipBalloon);
		equipmentDef.OnUnequipCallBack = new Action<Equippable>(this.OnUnequipBalloon);
		return equipmentDef;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0001321C File Offset: 0x0001141C
	private void OnEquipBalloon(Equippable eq)
	{
		if (!eq.IsNullOrDestroyed() && !eq.assignee.IsNullOrDestroyed())
		{
			Ownables soleOwner = eq.assignee.GetSoleOwner();
			if (soleOwner.IsNullOrDestroyed())
			{
				return;
			}
			KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)soleOwner.GetComponent<MinionAssignablesProxy>().target;
			Effects component = kmonoBehaviour.GetComponent<Effects>();
			KSelectable component2 = kmonoBehaviour.GetComponent<KSelectable>();
			if (!component.IsNullOrDestroyed())
			{
				component.Add("HasBalloon", false);
				EquippableBalloon component3 = eq.GetComponent<EquippableBalloon>();
				EquippableBalloon.StatesInstance statesInstance = (EquippableBalloon.StatesInstance)component3.GetSMI();
				component2.AddStatusItem(Db.Get().DuplicantStatusItems.JoyResponse_HasBalloon, statesInstance);
				this.SpawnFxInstanceFor(kmonoBehaviour);
				component3.ApplyBalloonOverrideToBalloonFx();
			}
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x000132C4 File Offset: 0x000114C4
	private void OnUnequipBalloon(Equippable eq)
	{
		if (!eq.IsNullOrDestroyed() && !eq.assignee.IsNullOrDestroyed())
		{
			Ownables soleOwner = eq.assignee.GetSoleOwner();
			if (soleOwner.IsNullOrDestroyed())
			{
				return;
			}
			MinionAssignablesProxy component = soleOwner.GetComponent<MinionAssignablesProxy>();
			if (!component.target.IsNullOrDestroyed())
			{
				KMonoBehaviour kmonoBehaviour = (KMonoBehaviour)component.target;
				Effects component2 = kmonoBehaviour.GetComponent<Effects>();
				KSelectable component3 = kmonoBehaviour.GetComponent<KSelectable>();
				if (!component2.IsNullOrDestroyed())
				{
					component2.Remove("HasBalloon");
					component3.RemoveStatusItem(Db.Get().DuplicantStatusItems.JoyResponse_HasBalloon, false);
					this.DestroyFxInstanceFor(kmonoBehaviour);
				}
			}
		}
		Util.KDestroyGameObject(eq.gameObject);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0001336C File Offset: 0x0001156C
	public void DoPostConfigure(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Clothes, false);
		Equippable equippable = go.GetComponent<Equippable>();
		if (equippable.IsNullOrDestroyed())
		{
			equippable = go.AddComponent<Equippable>();
		}
		equippable.hideInCodex = true;
		equippable.unequippable = false;
		go.AddOrGet<EquippableBalloon>();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x000133B5 File Offset: 0x000115B5
	private void SpawnFxInstanceFor(KMonoBehaviour target)
	{
		new BalloonFX.Instance(target.GetComponent<KMonoBehaviour>()).StartSM();
	}

	// Token: 0x060002AA RID: 682 RVA: 0x000133C7 File Offset: 0x000115C7
	private void DestroyFxInstanceFor(KMonoBehaviour target)
	{
		target.GetSMI<BalloonFX.Instance>().StopSM("Unequipped");
	}

	// Token: 0x0400018D RID: 397
	public const string ID = "EquippableBalloon";
}
