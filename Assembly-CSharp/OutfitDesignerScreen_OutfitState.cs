using System;
using System.Collections.Generic;
using Database;
using UnityEngine;

// Token: 0x02000D8F RID: 3471
public class OutfitDesignerScreen_OutfitState
{
	// Token: 0x06006C36 RID: 27702 RVA: 0x0028D904 File Offset: 0x0028BB04
	private OutfitDesignerScreen_OutfitState(ClothingOutfitUtility.OutfitType outfitType, ClothingOutfitTarget sourceTarget, ClothingOutfitTarget destinationTarget)
	{
		this.outfitType = outfitType;
		this.destinationTarget = destinationTarget;
		this.sourceTarget = sourceTarget;
		this.name = sourceTarget.ReadName();
		this.slots = OutfitDesignerScreen_OutfitState.Slots.For(outfitType);
		foreach (ClothingItemResource clothingItemResource in sourceTarget.ReadItemValues())
		{
			this.ApplyItem(clothingItemResource);
		}
	}

	// Token: 0x06006C37 RID: 27703 RVA: 0x0028D988 File Offset: 0x0028BB88
	public static OutfitDesignerScreen_OutfitState ForTemplateOutfit(ClothingOutfitTarget outfitTemplate)
	{
		global::Debug.Assert(outfitTemplate.IsTemplateOutfit());
		return new OutfitDesignerScreen_OutfitState(outfitTemplate.OutfitType, outfitTemplate, outfitTemplate);
	}

	// Token: 0x06006C38 RID: 27704 RVA: 0x0028D9A4 File Offset: 0x0028BBA4
	public static OutfitDesignerScreen_OutfitState ForMinionInstance(ClothingOutfitTarget sourceTarget, GameObject minionInstance)
	{
		return new OutfitDesignerScreen_OutfitState(sourceTarget.OutfitType, sourceTarget, ClothingOutfitTarget.FromMinion(sourceTarget.OutfitType, minionInstance));
	}

	// Token: 0x06006C39 RID: 27705 RVA: 0x0028D9C0 File Offset: 0x0028BBC0
	public unsafe void ApplyItem(ClothingItemResource item)
	{
		*this.slots.GetItemSlotForCategory(item.Category) = item;
	}

	// Token: 0x06006C3A RID: 27706 RVA: 0x0028D9DE File Offset: 0x0028BBDE
	public unsafe Option<ClothingItemResource> GetItemForCategory(PermitCategory category)
	{
		return *this.slots.GetItemSlotForCategory(category);
	}

	// Token: 0x06006C3B RID: 27707 RVA: 0x0028D9F4 File Offset: 0x0028BBF4
	public unsafe void SetItemForCategory(PermitCategory category, Option<ClothingItemResource> item)
	{
		if (item.IsSome())
		{
			DebugUtil.DevAssert(item.Unwrap().outfitType == this.outfitType, string.Format("Tried to set clothing item with outfit type \"{0}\" to outfit of type \"{1}\"", item.Unwrap().outfitType, this.outfitType), null);
			DebugUtil.DevAssert(item.Unwrap().Category == category, string.Format("Tried to set clothing item with category \"{0}\" to slot with type \"{1}\"", item.Unwrap().Category, category), null);
		}
		*this.slots.GetItemSlotForCategory(category) = item;
	}

	// Token: 0x06006C3C RID: 27708 RVA: 0x0028DA94 File Offset: 0x0028BC94
	public void AddItemValuesTo(ICollection<ClothingItemResource> clothingItems)
	{
		for (int i = 0; i < this.slots.array.Length; i++)
		{
			ref Option<ClothingItemResource> ptr = ref this.slots.array[i];
			if (ptr.IsSome())
			{
				clothingItems.Add(ptr.Unwrap());
			}
		}
	}

	// Token: 0x06006C3D RID: 27709 RVA: 0x0028DAE0 File Offset: 0x0028BCE0
	public void AddItemsTo(ICollection<string> itemIds)
	{
		for (int i = 0; i < this.slots.array.Length; i++)
		{
			ref Option<ClothingItemResource> ptr = ref this.slots.array[i];
			if (ptr.IsSome())
			{
				itemIds.Add(ptr.Unwrap().Id);
			}
		}
	}

	// Token: 0x06006C3E RID: 27710 RVA: 0x0028DB30 File Offset: 0x0028BD30
	public string[] GetItems()
	{
		List<string> list = new List<string>();
		this.AddItemsTo(list);
		return list.ToArray();
	}

	// Token: 0x06006C3F RID: 27711 RVA: 0x0028DB50 File Offset: 0x0028BD50
	public bool DoesContainLockedItems()
	{
		bool flag;
		using (ListPool<string, OutfitDesignerScreen_OutfitState>.PooledList pooledList = PoolsFor<OutfitDesignerScreen_OutfitState>.AllocateList<string>())
		{
			this.AddItemsTo(pooledList);
			flag = ClothingOutfitTarget.DoesContainLockedItems(pooledList);
		}
		return flag;
	}

	// Token: 0x06006C40 RID: 27712 RVA: 0x0028DB90 File Offset: 0x0028BD90
	public bool IsDirty()
	{
		using (HashSetPool<string, OutfitDesignerScreen>.PooledHashSet pooledHashSet = PoolsFor<OutfitDesignerScreen>.AllocateHashSet<string>())
		{
			this.AddItemsTo(pooledHashSet);
			string[] array = this.destinationTarget.ReadItems();
			if (pooledHashSet.Count != array.Length)
			{
				return true;
			}
			foreach (string text in array)
			{
				if (!pooledHashSet.Contains(text))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x040049C3 RID: 18883
	public string name;

	// Token: 0x040049C4 RID: 18884
	private OutfitDesignerScreen_OutfitState.Slots slots;

	// Token: 0x040049C5 RID: 18885
	public ClothingOutfitUtility.OutfitType outfitType;

	// Token: 0x040049C6 RID: 18886
	public ClothingOutfitTarget sourceTarget;

	// Token: 0x040049C7 RID: 18887
	public ClothingOutfitTarget destinationTarget;

	// Token: 0x02001F8A RID: 8074
	public abstract class Slots
	{
		// Token: 0x0600B391 RID: 45969 RVA: 0x003DAD38 File Offset: 0x003D8F38
		private Slots(int slotsCount)
		{
			this.array = new Option<ClothingItemResource>[slotsCount];
		}

		// Token: 0x0600B392 RID: 45970 RVA: 0x003DAD4C File Offset: 0x003D8F4C
		public static OutfitDesignerScreen_OutfitState.Slots For(ClothingOutfitUtility.OutfitType outfitType)
		{
			switch (outfitType)
			{
			case ClothingOutfitUtility.OutfitType.Clothing:
				return new OutfitDesignerScreen_OutfitState.Slots.Clothing();
			case ClothingOutfitUtility.OutfitType.JoyResponse:
				throw new NotSupportedException("OutfitType.JoyResponse cannot be used with OutfitDesignerScreen_OutfitState. Use JoyResponseOutfitTarget instead.");
			case ClothingOutfitUtility.OutfitType.AtmoSuit:
				return new OutfitDesignerScreen_OutfitState.Slots.Atmosuit();
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600B393 RID: 45971
		public abstract ref Option<ClothingItemResource> GetItemSlotForCategory(PermitCategory category);

		// Token: 0x0600B394 RID: 45972 RVA: 0x003DAD80 File Offset: 0x003D8F80
		private ref Option<ClothingItemResource> FallbackSlot(OutfitDesignerScreen_OutfitState.Slots self, PermitCategory category)
		{
			DebugUtil.DevAssert(false, string.Format("Couldn't get a {0}<{1}> for {2} \"{3}\" on {4}.{5}", new object[]
			{
				"Option",
				"ClothingItemResource",
				"PermitCategory",
				category,
				"Slots",
				self.GetType().Name
			}), null);
			return ref OutfitDesignerScreen_OutfitState.Slots.dummySlot;
		}

		// Token: 0x0400913A RID: 37178
		public Option<ClothingItemResource>[] array;

		// Token: 0x0400913B RID: 37179
		private static Option<ClothingItemResource> dummySlot;

		// Token: 0x02002905 RID: 10501
		public class Clothing : OutfitDesignerScreen_OutfitState.Slots
		{
			// Token: 0x0600CE12 RID: 52754 RVA: 0x0041EAB3 File Offset: 0x0041CCB3
			public Clothing()
				: base(6)
			{
			}

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x0600CE13 RID: 52755 RVA: 0x0041EABC File Offset: 0x0041CCBC
			public ref Option<ClothingItemResource> hatSlot
			{
				get
				{
					return ref this.array[0];
				}
			}

			// Token: 0x17000CEC RID: 3308
			// (get) Token: 0x0600CE14 RID: 52756 RVA: 0x0041EACA File Offset: 0x0041CCCA
			public ref Option<ClothingItemResource> topSlot
			{
				get
				{
					return ref this.array[1];
				}
			}

			// Token: 0x17000CED RID: 3309
			// (get) Token: 0x0600CE15 RID: 52757 RVA: 0x0041EAD8 File Offset: 0x0041CCD8
			public ref Option<ClothingItemResource> glovesSlot
			{
				get
				{
					return ref this.array[2];
				}
			}

			// Token: 0x17000CEE RID: 3310
			// (get) Token: 0x0600CE16 RID: 52758 RVA: 0x0041EAE6 File Offset: 0x0041CCE6
			public ref Option<ClothingItemResource> bottomSlot
			{
				get
				{
					return ref this.array[3];
				}
			}

			// Token: 0x17000CEF RID: 3311
			// (get) Token: 0x0600CE17 RID: 52759 RVA: 0x0041EAF4 File Offset: 0x0041CCF4
			public ref Option<ClothingItemResource> shoesSlot
			{
				get
				{
					return ref this.array[4];
				}
			}

			// Token: 0x17000CF0 RID: 3312
			// (get) Token: 0x0600CE18 RID: 52760 RVA: 0x0041EB02 File Offset: 0x0041CD02
			public ref Option<ClothingItemResource> accessorySlot
			{
				get
				{
					return ref this.array[5];
				}
			}

			// Token: 0x0600CE19 RID: 52761 RVA: 0x0041EB10 File Offset: 0x0041CD10
			public override ref Option<ClothingItemResource> GetItemSlotForCategory(PermitCategory category)
			{
				if (category == PermitCategory.DupeHats)
				{
					return this.hatSlot;
				}
				if (category == PermitCategory.DupeTops)
				{
					return this.topSlot;
				}
				if (category == PermitCategory.DupeGloves)
				{
					return this.glovesSlot;
				}
				if (category == PermitCategory.DupeBottoms)
				{
					return this.bottomSlot;
				}
				if (category == PermitCategory.DupeShoes)
				{
					return this.shoesSlot;
				}
				if (category == PermitCategory.DupeAccessories)
				{
					return this.accessorySlot;
				}
				return base.FallbackSlot(this, category);
			}
		}

		// Token: 0x02002906 RID: 10502
		public class Atmosuit : OutfitDesignerScreen_OutfitState.Slots
		{
			// Token: 0x0600CE1A RID: 52762 RVA: 0x0041EB67 File Offset: 0x0041CD67
			public Atmosuit()
				: base(5)
			{
			}

			// Token: 0x17000CF1 RID: 3313
			// (get) Token: 0x0600CE1B RID: 52763 RVA: 0x0041EB70 File Offset: 0x0041CD70
			public ref Option<ClothingItemResource> helmetSlot
			{
				get
				{
					return ref this.array[0];
				}
			}

			// Token: 0x17000CF2 RID: 3314
			// (get) Token: 0x0600CE1C RID: 52764 RVA: 0x0041EB7E File Offset: 0x0041CD7E
			public ref Option<ClothingItemResource> bodySlot
			{
				get
				{
					return ref this.array[1];
				}
			}

			// Token: 0x17000CF3 RID: 3315
			// (get) Token: 0x0600CE1D RID: 52765 RVA: 0x0041EB8C File Offset: 0x0041CD8C
			public ref Option<ClothingItemResource> glovesSlot
			{
				get
				{
					return ref this.array[2];
				}
			}

			// Token: 0x17000CF4 RID: 3316
			// (get) Token: 0x0600CE1E RID: 52766 RVA: 0x0041EB9A File Offset: 0x0041CD9A
			public ref Option<ClothingItemResource> beltSlot
			{
				get
				{
					return ref this.array[3];
				}
			}

			// Token: 0x17000CF5 RID: 3317
			// (get) Token: 0x0600CE1F RID: 52767 RVA: 0x0041EBA8 File Offset: 0x0041CDA8
			public ref Option<ClothingItemResource> shoesSlot
			{
				get
				{
					return ref this.array[4];
				}
			}

			// Token: 0x0600CE20 RID: 52768 RVA: 0x0041EBB8 File Offset: 0x0041CDB8
			public override ref Option<ClothingItemResource> GetItemSlotForCategory(PermitCategory category)
			{
				if (category == PermitCategory.AtmoSuitHelmet)
				{
					return this.helmetSlot;
				}
				if (category == PermitCategory.AtmoSuitBody)
				{
					return this.bodySlot;
				}
				if (category == PermitCategory.AtmoSuitGloves)
				{
					return this.glovesSlot;
				}
				if (category == PermitCategory.AtmoSuitBelt)
				{
					return this.beltSlot;
				}
				if (category == PermitCategory.AtmoSuitShoes)
				{
					return this.shoesSlot;
				}
				return base.FallbackSlot(this, category);
			}
		}
	}
}
