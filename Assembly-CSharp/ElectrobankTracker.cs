using System;
using KSerialization;
using UnityEngine;

// Token: 0x020008D8 RID: 2264
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ElectrobankTracker")]
public class ElectrobankTracker : WorldResourceAmountTracker<ElectrobankTracker>, ISaveLoadable
{
	// Token: 0x06003EDA RID: 16090 RVA: 0x00161EB7 File Offset: 0x001600B7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ignoredTags = new Tag[GameTags.BionicIncompatibleBatteries.Count];
		GameTags.BionicIncompatibleBatteries.CopyTo(this.ignoredTags, 0);
		this.itemTag = GameTags.ChargedPortableBattery;
	}

	// Token: 0x06003EDB RID: 16091 RVA: 0x00161EF0 File Offset: 0x001600F0
	protected override WorldResourceAmountTracker<ElectrobankTracker>.ItemData GetItemData(Pickupable item)
	{
		Electrobank component = item.GetComponent<Electrobank>();
		return new WorldResourceAmountTracker<ElectrobankTracker>.ItemData
		{
			ID = component.ID,
			amountValue = component.Charge * item.PrimaryElement.Units,
			units = item.PrimaryElement.Units
		};
	}
}
