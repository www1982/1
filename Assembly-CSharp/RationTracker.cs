using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x02000A89 RID: 2697
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/RationTracker")]
public class RationTracker : WorldResourceAmountTracker<RationTracker>, ISaveLoadable
{
	// Token: 0x06004E3F RID: 20031 RVA: 0x001C5278 File Offset: 0x001C3478
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.itemTag = GameTags.Edible;
	}

	// Token: 0x06004E40 RID: 20032 RVA: 0x001C528C File Offset: 0x001C348C
	[OnDeserialized]
	private void OnDeserialized()
	{
		if (this.caloriesConsumedByFood != null && this.caloriesConsumedByFood.Count > 0)
		{
			foreach (string text in this.caloriesConsumedByFood.Keys)
			{
				float num = this.caloriesConsumedByFood[text];
				float num2 = 0f;
				if (this.amountsConsumedByID.TryGetValue(text, out num2))
				{
					this.amountsConsumedByID[text] = num2 + num;
				}
				else
				{
					this.amountsConsumedByID.Add(text, num);
				}
			}
		}
		this.caloriesConsumedByFood = null;
	}

	// Token: 0x06004E41 RID: 20033 RVA: 0x001C5340 File Offset: 0x001C3540
	protected override WorldResourceAmountTracker<RationTracker>.ItemData GetItemData(Pickupable item)
	{
		Edible component = item.GetComponent<Edible>();
		return new WorldResourceAmountTracker<RationTracker>.ItemData
		{
			ID = component.FoodID,
			amountValue = component.Calories,
			units = component.Units
		};
	}

	// Token: 0x06004E42 RID: 20034 RVA: 0x001C5384 File Offset: 0x001C3584
	public float GetAmountConsumed()
	{
		float num = 0f;
		foreach (KeyValuePair<string, float> keyValuePair in this.amountsConsumedByID)
		{
			num += keyValuePair.Value;
		}
		return num;
	}

	// Token: 0x06004E43 RID: 20035 RVA: 0x001C53E4 File Offset: 0x001C35E4
	public float GetAmountConsumedForIDs(List<string> itemIDs)
	{
		float num = 0f;
		foreach (string text in itemIDs)
		{
			if (this.amountsConsumedByID.ContainsKey(text))
			{
				num += this.amountsConsumedByID[text];
			}
		}
		return num;
	}

	// Token: 0x06004E44 RID: 20036 RVA: 0x001C5450 File Offset: 0x001C3650
	public float CountAmountForItemWithID(string ID, WorldInventory inventory, bool excludeUnreachable = true)
	{
		float num = 0f;
		ICollection<Pickupable> pickupables = inventory.GetPickupables(this.itemTag, false);
		if (pickupables != null)
		{
			foreach (Pickupable pickupable in pickupables)
			{
				if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
				{
					WorldResourceAmountTracker<RationTracker>.ItemData itemData = this.GetItemData(pickupable);
					if (itemData.ID == ID)
					{
						num += itemData.amountValue;
					}
				}
			}
		}
		return num;
	}

	// Token: 0x040033FB RID: 13307
	[Serialize]
	public Dictionary<string, float> caloriesConsumedByFood = new Dictionary<string, float>();
}
