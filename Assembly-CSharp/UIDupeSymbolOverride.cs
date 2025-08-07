using System;
using Database;
using UnityEngine;

// Token: 0x02000E6E RID: 3694
[RequireComponent(typeof(SymbolOverrideController))]
public class UIDupeSymbolOverride : MonoBehaviour
{
	// Token: 0x060075D9 RID: 30169 RVA: 0x002D1D54 File Offset: 0x002CFF54
	public void Apply(MinionIdentity minionIdentity)
	{
		if (this.slots == null)
		{
			this.slots = new AccessorySlots(null);
		}
		if (this.symbolOverrideController == null)
		{
			this.symbolOverrideController = base.GetComponent<SymbolOverrideController>();
		}
		if (this.animController == null)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
		}
		Personality personalityFromNameStringKey = Db.Get().Personalities.GetPersonalityFromNameStringKey(minionIdentity.nameStringKey);
		DebugUtil.DevAssert(personalityFromNameStringKey != null, "Personality is not found", null);
		KCompBuilder.BodyData bodyData = MinionStartingStats.CreateBodyData(personalityFromNameStringKey);
		this.symbolOverrideController.RemoveAllSymbolOverrides(0);
		this.SetAccessory(this.animController, this.slots.Hair.Lookup(bodyData.hair));
		this.SetAccessory(this.animController, this.slots.HatHair.Lookup("hat_" + HashCache.Get().Get(bodyData.hair)));
		this.SetAccessory(this.animController, this.slots.Eyes.Lookup(bodyData.eyes));
		this.SetAccessory(this.animController, this.slots.HeadShape.Lookup(bodyData.headShape));
		this.SetAccessory(this.animController, this.slots.Mouth.Lookup(bodyData.mouth));
		this.SetAccessory(this.animController, this.slots.Neck.Lookup(bodyData.neck));
		this.SetAccessory(this.animController, this.slots.Body.Lookup(bodyData.body));
		this.SetAccessory(this.animController, this.slots.Leg.Lookup(bodyData.legs));
		this.SetAccessory(this.animController, this.slots.Arm.Lookup(bodyData.arms));
		this.SetAccessory(this.animController, this.slots.ArmLower.Lookup(bodyData.armslower));
		this.SetAccessory(this.animController, this.slots.Pelvis.Lookup(bodyData.pelvis));
		this.SetAccessory(this.animController, this.slots.Belt.Lookup(bodyData.belt));
		this.SetAccessory(this.animController, this.slots.Foot.Lookup(bodyData.foot));
		this.SetAccessory(this.animController, this.slots.Cuff.Lookup(bodyData.cuff));
		this.SetAccessory(this.animController, this.slots.Hand.Lookup(bodyData.hand));
	}

	// Token: 0x060075DA RID: 30170 RVA: 0x002D2000 File Offset: 0x002D0200
	private KAnimHashedString SetAccessory(KBatchedAnimController minion, Accessory accessory)
	{
		if (accessory != null)
		{
			this.symbolOverrideController.TryRemoveSymbolOverride(accessory.slot.targetSymbolId, 0);
			this.symbolOverrideController.AddSymbolOverride(accessory.slot.targetSymbolId, accessory.symbol, 0);
			minion.SetSymbolVisiblity(accessory.slot.targetSymbolId, true);
			return accessory.slot.targetSymbolId;
		}
		return HashedString.Invalid;
	}

	// Token: 0x040051BF RID: 20927
	private KBatchedAnimController animController;

	// Token: 0x040051C0 RID: 20928
	private AccessorySlots slots;

	// Token: 0x040051C1 RID: 20929
	private SymbolOverrideController symbolOverrideController;
}
