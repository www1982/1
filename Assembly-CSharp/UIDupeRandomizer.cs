using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000E6D RID: 3693
public class UIDupeRandomizer : MonoBehaviour
{
	// Token: 0x060075D1 RID: 30161 RVA: 0x002D1768 File Offset: 0x002CF968
	protected virtual void Start()
	{
		this.slots = Db.Get().AccessorySlots;
		for (int i = 0; i < this.anims.Length; i++)
		{
			this.anims[i].curBody = null;
			this.GetNewBody(i);
		}
	}

	// Token: 0x060075D2 RID: 30162 RVA: 0x002D17B4 File Offset: 0x002CF9B4
	protected void GetNewBody(int minion_idx)
	{
		Personality random = Db.Get().Personalities.GetRandom(true, false);
		foreach (KBatchedAnimController kbatchedAnimController in this.anims[minion_idx].minions)
		{
			this.Apply(kbatchedAnimController, random);
		}
	}

	// Token: 0x060075D3 RID: 30163 RVA: 0x002D1828 File Offset: 0x002CFA28
	private void Apply(KBatchedAnimController dupe, Personality personality)
	{
		KCompBuilder.BodyData bodyData = MinionStartingStats.CreateBodyData(personality);
		SymbolOverrideController component = dupe.GetComponent<SymbolOverrideController>();
		component.RemoveAllSymbolOverrides(0);
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Hair.Lookup(bodyData.hair));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.HatHair.Lookup("hat_" + HashCache.Get().Get(bodyData.hair)));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Eyes.Lookup(bodyData.eyes));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.HeadShape.Lookup(bodyData.headShape));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Mouth.Lookup(bodyData.mouth));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Body.Lookup(bodyData.body));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Arm.Lookup(bodyData.arms));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.ArmLower.Lookup(bodyData.armslower));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Belt.Lookup(bodyData.belt));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Hand.Lookup(bodyData.hand));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Neck.Lookup(bodyData.neck));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Cuff.Lookup(bodyData.cuff));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Pelvis.Lookup(bodyData.pelvis));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Leg.Lookup(bodyData.legs));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.Foot.Lookup(bodyData.foot));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.ArmLowerSkin.Lookup(bodyData.armLowerSkin));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.ArmUpperSkin.Lookup(bodyData.armUpperSkin));
		UIDupeRandomizer.AddAccessory(dupe, this.slots.LegSkin.Lookup(bodyData.legSkin));
		if (this.applySuit && global::UnityEngine.Random.value < 0.15f)
		{
			component.AddBuildOverride(Assets.GetAnim("body_oxygen_kanim").GetData(), 6);
			dupe.SetSymbolVisiblity("snapto_neck", true);
			dupe.SetSymbolVisiblity("belt", false);
		}
		else
		{
			dupe.SetSymbolVisiblity("snapto_neck", false);
		}
		if (this.applyHat && global::UnityEngine.Random.value < 0.5f)
		{
			List<string> list = new List<string>();
			foreach (Skill skill in Db.Get().Skills.resources)
			{
				if (skill.requiredDuplicantModel.IsNullOrWhiteSpace() || skill.requiredDuplicantModel == personality.model)
				{
					list.Add(skill.hat);
				}
			}
			string text = list[global::UnityEngine.Random.Range(0, list.Count)];
			UIDupeRandomizer.AddAccessory(dupe, this.slots.Hat.Lookup(text));
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
		}
		else
		{
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
			dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, false);
		}
		dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Skirt.targetSymbolId, false);
		dupe.SetSymbolVisiblity(Db.Get().AccessorySlots.Necklace.targetSymbolId, false);
	}

	// Token: 0x060075D4 RID: 30164 RVA: 0x002D1C54 File Offset: 0x002CFE54
	public static KAnimHashedString AddAccessory(KBatchedAnimController minion, Accessory accessory)
	{
		if (accessory != null)
		{
			SymbolOverrideController component = minion.GetComponent<SymbolOverrideController>();
			DebugUtil.Assert(component != null, minion.name + " is missing symbol override controller");
			component.TryRemoveSymbolOverride(accessory.slot.targetSymbolId, 0);
			component.AddSymbolOverride(accessory.slot.targetSymbolId, accessory.symbol, 0);
			minion.SetSymbolVisiblity(accessory.slot.targetSymbolId, true);
			return accessory.slot.targetSymbolId;
		}
		return HashedString.Invalid;
	}

	// Token: 0x060075D5 RID: 30165 RVA: 0x002D1CE4 File Offset: 0x002CFEE4
	public KAnimHashedString AddRandomAccessory(KBatchedAnimController minion, List<Accessory> choices)
	{
		Accessory accessory = choices[global::UnityEngine.Random.Range(1, choices.Count)];
		return UIDupeRandomizer.AddAccessory(minion, accessory);
	}

	// Token: 0x060075D6 RID: 30166 RVA: 0x002D1D0C File Offset: 0x002CFF0C
	public void Randomize()
	{
		if (this.slots == null)
		{
			return;
		}
		for (int i = 0; i < this.anims.Length; i++)
		{
			this.GetNewBody(i);
		}
	}

	// Token: 0x060075D7 RID: 30167 RVA: 0x002D1D3C File Offset: 0x002CFF3C
	protected virtual void Update()
	{
	}

	// Token: 0x040051BB RID: 20923
	[Tooltip("Enable this to allow for a chance for skill hats to appear")]
	public bool applyHat = true;

	// Token: 0x040051BC RID: 20924
	[Tooltip("Enable this to allow for a chance for suit helmets to appear (ie. atmosuit and leadsuit)")]
	public bool applySuit = true;

	// Token: 0x040051BD RID: 20925
	public UIDupeRandomizer.AnimChoice[] anims;

	// Token: 0x040051BE RID: 20926
	private AccessorySlots slots;

	// Token: 0x02002066 RID: 8294
	[Serializable]
	public struct AnimChoice
	{
		// Token: 0x04009413 RID: 37907
		public string anim_name;

		// Token: 0x04009414 RID: 37908
		public List<KBatchedAnimController> minions;

		// Token: 0x04009415 RID: 37909
		public float minSecondsBetweenAction;

		// Token: 0x04009416 RID: 37910
		public float maxSecondsBetweenAction;

		// Token: 0x04009417 RID: 37911
		public float lastWaitTime;

		// Token: 0x04009418 RID: 37912
		public KAnimFile curBody;
	}
}
