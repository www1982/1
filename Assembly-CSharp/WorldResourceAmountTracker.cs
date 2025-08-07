using System;
using System.Collections.Generic;
using KSerialization;

// Token: 0x02000BEB RID: 3051
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class WorldResourceAmountTracker<T> : KMonoBehaviour where T : KMonoBehaviour
{
	// Token: 0x06005BEF RID: 23535 RVA: 0x002135D2 File Offset: 0x002117D2
	public static void DestroyInstance()
	{
		WorldResourceAmountTracker<T>.instance = default(T);
	}

	// Token: 0x06005BF0 RID: 23536 RVA: 0x002135DF File Offset: 0x002117DF
	public static T Get()
	{
		return WorldResourceAmountTracker<T>.instance;
	}

	// Token: 0x06005BF1 RID: 23537 RVA: 0x002135E8 File Offset: 0x002117E8
	protected override void OnPrefabInit()
	{
		Debug.Assert(WorldResourceAmountTracker<T>.instance == null, "Error, WorldResourceAmountTracker of type T has already been initialize and another instance is attempting to initialize. this isn't allowed because T is meant to be a singleton, ensure only one instance exist. existing instance GameObject: " + ((WorldResourceAmountTracker<T>.instance == null) ? "" : WorldResourceAmountTracker<T>.instance.gameObject.name) + ". Error triggered by instance of T in GameObject: " + base.gameObject.name);
		WorldResourceAmountTracker<T>.instance = this as T;
		this.itemTag = GameTags.Edible;
	}

	// Token: 0x06005BF2 RID: 23538 RVA: 0x0021366C File Offset: 0x0021186C
	protected override void OnSpawn()
	{
		base.Subscribe(631075836, new Action<object>(this.OnNewDay));
	}

	// Token: 0x06005BF3 RID: 23539 RVA: 0x00213686 File Offset: 0x00211886
	private void OnNewDay(object data)
	{
		this.previousFrame = this.currentFrame;
		this.currentFrame = default(WorldResourceAmountTracker<T>.Frame);
	}

	// Token: 0x06005BF4 RID: 23540
	protected abstract WorldResourceAmountTracker<T>.ItemData GetItemData(Pickupable item);

	// Token: 0x06005BF5 RID: 23541 RVA: 0x002136A0 File Offset: 0x002118A0
	public float CountAmount(Dictionary<string, float> unitCountByID, WorldInventory inventory, bool excludeUnreachable = true)
	{
		float num;
		return this.CountAmount(unitCountByID, out num, inventory, excludeUnreachable);
	}

	// Token: 0x06005BF6 RID: 23542 RVA: 0x002136B8 File Offset: 0x002118B8
	public float CountAmount(Dictionary<string, float> unitCountByID, out float totalUnitsFound, WorldInventory inventory, bool excludeUnreachable)
	{
		float num = 0f;
		totalUnitsFound = 0f;
		ICollection<Pickupable> pickupables = inventory.GetPickupables(this.itemTag, false);
		if (pickupables != null)
		{
			foreach (Pickupable pickupable in pickupables)
			{
				if (!pickupable.KPrefabID.HasTag(GameTags.StoredPrivate))
				{
					if (this.ignoredTags != null)
					{
						bool flag = false;
						foreach (Tag tag in this.ignoredTags)
						{
							if (pickupable.KPrefabID.HasTag(tag))
							{
								flag = true;
								break;
							}
						}
						if (flag)
						{
							continue;
						}
					}
					WorldResourceAmountTracker<T>.ItemData itemData = this.GetItemData(pickupable);
					num += itemData.amountValue;
					if (unitCountByID != null)
					{
						if (!unitCountByID.ContainsKey(itemData.ID))
						{
							unitCountByID[itemData.ID] = 0f;
						}
						string id = itemData.ID;
						unitCountByID[id] += itemData.units;
					}
					totalUnitsFound += itemData.units;
				}
			}
		}
		return num;
	}

	// Token: 0x06005BF7 RID: 23543 RVA: 0x002137E8 File Offset: 0x002119E8
	public void RegisterAmountProduced(float val)
	{
		this.currentFrame.amountProduced = this.currentFrame.amountProduced + val;
	}

	// Token: 0x06005BF8 RID: 23544 RVA: 0x002137FC File Offset: 0x002119FC
	public void RegisterAmountConsumed(string ID, float valueConsumed)
	{
		this.currentFrame.amountConsumed = this.currentFrame.amountConsumed + valueConsumed;
		if (!this.amountsConsumedByID.ContainsKey(ID))
		{
			this.amountsConsumedByID.Add(ID, valueConsumed);
			return;
		}
		Dictionary<string, float> dictionary = this.amountsConsumedByID;
		dictionary[ID] += valueConsumed;
	}

	// Token: 0x04003CE1 RID: 15585
	private static T instance;

	// Token: 0x04003CE2 RID: 15586
	[Serialize]
	public WorldResourceAmountTracker<T>.Frame currentFrame;

	// Token: 0x04003CE3 RID: 15587
	[Serialize]
	public WorldResourceAmountTracker<T>.Frame previousFrame;

	// Token: 0x04003CE4 RID: 15588
	[Serialize]
	public Dictionary<string, float> amountsConsumedByID = new Dictionary<string, float>();

	// Token: 0x04003CE5 RID: 15589
	protected Tag itemTag;

	// Token: 0x04003CE6 RID: 15590
	protected Tag[] ignoredTags;

	// Token: 0x02001D22 RID: 7458
	protected struct ItemData
	{
		// Token: 0x04008848 RID: 34888
		public string ID;

		// Token: 0x04008849 RID: 34889
		public float amountValue;

		// Token: 0x0400884A RID: 34890
		public float units;
	}

	// Token: 0x02001D23 RID: 7459
	public struct Frame
	{
		// Token: 0x0400884B RID: 34891
		public float amountProduced;

		// Token: 0x0400884C RID: 34892
		public float amountConsumed;
	}
}
