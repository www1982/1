using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Database;
using KSerialization;
using UnityEngine;

// Token: 0x0200064D RID: 1613
[AddComponentMenu("KMonoBehaviour/scripts/WearableAccessorizer")]
public class WearableAccessorizer : KMonoBehaviour
{
	// Token: 0x060026FC RID: 9980 RVA: 0x000DECFB File Offset: 0x000DCEFB
	public Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> GetCustomClothingItems()
	{
		return this.customOutfitItems;
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x060026FD RID: 9981 RVA: 0x000DED03 File Offset: 0x000DCF03
	public Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> Wearables
	{
		get
		{
			return this.wearables;
		}
	}

	// Token: 0x060026FE RID: 9982 RVA: 0x000DED0C File Offset: 0x000DCF0C
	public string[] GetClothingItemsIds(ClothingOutfitUtility.OutfitType outfitType)
	{
		if (this.customOutfitItems.ContainsKey(outfitType))
		{
			string[] array = new string[this.customOutfitItems[outfitType].Count];
			for (int i = 0; i < this.customOutfitItems[outfitType].Count; i++)
			{
				array[i] = this.customOutfitItems[outfitType][i].Get().Id;
			}
			return array;
		}
		return new string[0];
	}

	// Token: 0x060026FF RID: 9983 RVA: 0x000DED81 File Offset: 0x000DCF81
	public Option<string> GetJoyResponseId()
	{
		return this.joyResponsePermitId;
	}

	// Token: 0x06002700 RID: 9984 RVA: 0x000DED8E File Offset: 0x000DCF8E
	public void SetJoyResponseId(Option<string> joyResponsePermitId)
	{
		this.joyResponsePermitId = joyResponsePermitId.UnwrapOr(null, null);
	}

	// Token: 0x06002701 RID: 9985 RVA: 0x000DEDA0 File Offset: 0x000DCFA0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.animController == null)
		{
			this.animController = base.GetComponent<KAnimControllerBase>();
		}
		base.Subscribe(-448952673, new Action<object>(this.EquippedItem));
		base.Subscribe(-1285462312, new Action<object>(this.UnequippedItem));
	}

	// Token: 0x06002702 RID: 9986 RVA: 0x000DEE00 File Offset: 0x000DD000
	[OnDeserialized]
	[Obsolete]
	private void OnDeserialized()
	{
		List<WearableAccessorizer.WearableType> list = new List<WearableAccessorizer.WearableType>();
		foreach (KeyValuePair<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> keyValuePair in this.wearables)
		{
			keyValuePair.Value.Deserialize();
			if (keyValuePair.Value.BuildAnims == null || keyValuePair.Value.BuildAnims.Count == 0)
			{
				list.Add(keyValuePair.Key);
			}
		}
		foreach (WearableAccessorizer.WearableType wearableType in list)
		{
			this.wearables.Remove(wearableType);
		}
		foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> keyValuePair2 in this.customOutfitItems)
		{
			ClothingOutfitUtility.OutfitType outfitType;
			List<ResourceRef<ClothingItemResource>> list2;
			keyValuePair2.Deconstruct(out outfitType, out list2);
			List<ResourceRef<ClothingItemResource>> list3 = list2;
			if (list3 != null && list3.Count != 0)
			{
				for (int num = list3.Count - 1; num != -1; num--)
				{
					if (list3[num].Get() == null)
					{
						list3.RemoveAt(num);
					}
				}
			}
		}
		if (this.clothingItems.Count > 0)
		{
			this.customOutfitItems[ClothingOutfitUtility.OutfitType.Clothing] = new List<ResourceRef<ClothingItemResource>>(this.clothingItems);
			this.clothingItems.Clear();
			if (!this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomClothing))
			{
				foreach (ResourceRef<ClothingItemResource> resourceRef in this.customOutfitItems[ClothingOutfitUtility.OutfitType.Clothing])
				{
					this.Internal_ApplyClothingItem(ClothingOutfitUtility.OutfitType.Clothing, resourceRef.Get());
				}
			}
		}
		this.ApplyWearable();
	}

	// Token: 0x06002703 RID: 9987 RVA: 0x000DEFF0 File Offset: 0x000DD1F0
	public void EquippedItem(object data)
	{
		KPrefabID kprefabID = data as KPrefabID;
		if (kprefabID != null)
		{
			Equippable component = kprefabID.GetComponent<Equippable>();
			this.ApplyEquipment(component, component.GetBuildOverride());
		}
	}

	// Token: 0x06002704 RID: 9988 RVA: 0x000DF024 File Offset: 0x000DD224
	public void ApplyEquipment(Equippable equippable, KAnimFile animFile)
	{
		WearableAccessorizer.WearableType wearableType;
		if (equippable != null && animFile != null && Enum.TryParse<WearableAccessorizer.WearableType>(equippable.def.Slot, out wearableType))
		{
			if (this.wearables.ContainsKey(wearableType))
			{
				this.RemoveAnimBuild(this.wearables[wearableType].BuildAnims[0], this.wearables[wearableType].buildOverridePriority);
			}
			ClothingOutfitUtility.OutfitType outfitType;
			if (this.TryGetEquippableClothingType(equippable.def, out outfitType) && this.customOutfitItems.ContainsKey(outfitType))
			{
				this.wearables[WearableAccessorizer.WearableType.CustomSuit] = new WearableAccessorizer.Wearable(animFile, equippable.def.BuildOverridePriority);
				this.wearables[WearableAccessorizer.WearableType.CustomSuit].AddCustomItems(this.customOutfitItems[outfitType]);
			}
			else
			{
				this.wearables[wearableType] = new WearableAccessorizer.Wearable(animFile, equippable.def.BuildOverridePriority);
			}
			this.ApplyWearable();
		}
	}

	// Token: 0x06002705 RID: 9989 RVA: 0x000DF119 File Offset: 0x000DD319
	private bool TryGetEquippableClothingType(EquipmentDef equipment, out ClothingOutfitUtility.OutfitType outfitType)
	{
		if (equipment.Id == "Atmo_Suit")
		{
			outfitType = ClothingOutfitUtility.OutfitType.AtmoSuit;
			return true;
		}
		outfitType = ClothingOutfitUtility.OutfitType.LENGTH;
		return false;
	}

	// Token: 0x06002706 RID: 9990 RVA: 0x000DF138 File Offset: 0x000DD338
	private Equippable GetSuitEquippable()
	{
		MinionIdentity component = base.GetComponent<MinionIdentity>();
		if (component != null && component.assignableProxy != null && component.assignableProxy.Get() != null)
		{
			Equipment equipment = component.GetEquipment();
			Assignable assignable = ((equipment != null) ? equipment.GetAssignable(Db.Get().AssignableSlots.Suit) : null);
			if (assignable != null)
			{
				return assignable.GetComponent<Equippable>();
			}
		}
		return null;
	}

	// Token: 0x06002707 RID: 9991 RVA: 0x000DF1AC File Offset: 0x000DD3AC
	private WearableAccessorizer.WearableType GetHighestAccessory()
	{
		WearableAccessorizer.WearableType wearableType = WearableAccessorizer.WearableType.Basic;
		foreach (WearableAccessorizer.WearableType wearableType2 in this.wearables.Keys)
		{
			if (wearableType2 > wearableType)
			{
				wearableType = wearableType2;
			}
		}
		return wearableType;
	}

	// Token: 0x06002708 RID: 9992 RVA: 0x000DF208 File Offset: 0x000DD408
	private void ApplyWearable()
	{
		if (this.animController == null)
		{
			this.animController = base.GetComponent<KAnimControllerBase>();
			if (this.animController == null)
			{
				global::Debug.LogWarning("Missing animcontroller for WearableAccessorizer, bailing early to prevent a crash!");
				return;
			}
		}
		SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
		WearableAccessorizer.WearableType highestAccessory = this.GetHighestAccessory();
		foreach (object obj in Enum.GetValues(typeof(WearableAccessorizer.WearableType)))
		{
			WearableAccessorizer.WearableType wearableType = (WearableAccessorizer.WearableType)obj;
			if (this.wearables.ContainsKey(wearableType))
			{
				WearableAccessorizer.Wearable wearable = this.wearables[wearableType];
				int buildOverridePriority = wearable.buildOverridePriority;
				foreach (KAnimFile kanimFile in wearable.BuildAnims)
				{
					KAnim.Build build = kanimFile.GetData().build;
					if (build != null)
					{
						for (int i = 0; i < build.symbols.Length; i++)
						{
							string text = HashCache.Get().Get(build.symbols[i].hash);
							if (wearableType == highestAccessory)
							{
								component.AddSymbolOverride(text, build.symbols[i], buildOverridePriority);
								this.animController.SetSymbolVisiblity(text, true);
							}
							else
							{
								component.RemoveSymbolOverride(text, buildOverridePriority);
							}
						}
					}
				}
			}
		}
		this.UpdateVisibleSymbols(highestAccessory);
	}

	// Token: 0x06002709 RID: 9993 RVA: 0x000DF3A0 File Offset: 0x000DD5A0
	public void UpdateVisibleSymbols(ClothingOutfitUtility.OutfitType outfitType)
	{
		if (this.animController == null)
		{
			this.animController = base.GetComponent<KAnimControllerBase>();
		}
		this.UpdateVisibleSymbols(this.ConvertOutfitTypeToWearableType(outfitType));
	}

	// Token: 0x0600270A RID: 9994 RVA: 0x000DF3CC File Offset: 0x000DD5CC
	private void UpdateVisibleSymbols(WearableAccessorizer.WearableType wearableType)
	{
		bool flag = wearableType == WearableAccessorizer.WearableType.Basic;
		bool flag2 = base.GetComponent<Accessorizer>().GetAccessory(Db.Get().AccessorySlots.Hat) != null;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = true;
		bool flag6 = wearableType == WearableAccessorizer.WearableType.Basic;
		bool flag7 = wearableType == WearableAccessorizer.WearableType.Basic;
		if (this.wearables.ContainsKey(wearableType))
		{
			List<KAnimHashedString> list = this.wearables[wearableType].BuildAnims.SelectMany((KAnimFile x) => x.GetData().build.symbols.Select((KAnim.Build.Symbol s) => s.hash)).ToList<KAnimHashedString>();
			flag = flag || list.Contains(Db.Get().AccessorySlots.Belt.targetSymbolId);
			flag3 = list.Contains(Db.Get().AccessorySlots.Skirt.targetSymbolId);
			flag4 = list.Contains(Db.Get().AccessorySlots.Necklace.targetSymbolId);
			flag5 = list.Contains(Db.Get().AccessorySlots.ArmLower.targetSymbolId) || (wearableType != WearableAccessorizer.WearableType.Basic && !this.HasPermitCategoryItem(ClothingOutfitUtility.OutfitType.Clothing, PermitCategory.DupeTops));
			flag6 = list.Contains(Db.Get().AccessorySlots.Arm.targetSymbolId) || (wearableType != WearableAccessorizer.WearableType.Basic && !this.HasPermitCategoryItem(ClothingOutfitUtility.OutfitType.Clothing, PermitCategory.DupeTops));
			flag7 = list.Contains(Db.Get().AccessorySlots.Leg.targetSymbolId) || (wearableType != WearableAccessorizer.WearableType.Basic && !this.HasPermitCategoryItem(ClothingOutfitUtility.OutfitType.Clothing, PermitCategory.DupeBottoms));
		}
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Belt.targetSymbolId, flag);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Necklace.targetSymbolId, flag4);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.ArmLower.targetSymbolId, flag5);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Arm.targetSymbolId, flag6);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Leg.targetSymbolId, flag7);
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Skirt.targetSymbolId, flag3);
		if (flag3 || flag)
		{
			this.SkirtHACK(wearableType);
		}
		WearableAccessorizer.UpdateHairBasedOnHat(this.animController, flag2);
	}

	// Token: 0x0600270B RID: 9995 RVA: 0x000DF62C File Offset: 0x000DD82C
	private void SkirtHACK(WearableAccessorizer.WearableType wearable_type)
	{
		if (this.wearables.ContainsKey(wearable_type))
		{
			SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
			WearableAccessorizer.Wearable wearable = this.wearables[wearable_type];
			int buildOverridePriority = wearable.buildOverridePriority;
			foreach (KAnimFile kanimFile in wearable.BuildAnims)
			{
				foreach (KAnim.Build.Symbol symbol in kanimFile.GetData().build.symbols)
				{
					if (HashCache.Get().Get(symbol.hash).EndsWith(WearableAccessorizer.cropped))
					{
						component.AddSymbolOverride(WearableAccessorizer.torso, symbol, buildOverridePriority);
						break;
					}
				}
			}
		}
	}

	// Token: 0x0600270C RID: 9996 RVA: 0x000DF6FC File Offset: 0x000DD8FC
	public static void UpdateHairBasedOnHat(KAnimControllerBase kbac, bool hasHat)
	{
		if (hasHat)
		{
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, false);
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, true);
			kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, true);
			return;
		}
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, true);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, false);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Hat.targetSymbolId, false);
	}

	// Token: 0x0600270D RID: 9997 RVA: 0x000DF7AF File Offset: 0x000DD9AF
	public static void SkirtAccessory(KAnimControllerBase kbac, bool show_skirt)
	{
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Skirt.targetSymbolId, show_skirt);
		kbac.SetSymbolVisiblity(Db.Get().AccessorySlots.Leg.targetSymbolId, !show_skirt);
	}

	// Token: 0x0600270E RID: 9998 RVA: 0x000DF7EC File Offset: 0x000DD9EC
	private void RemoveAnimBuild(KAnimFile animFile, int override_priority)
	{
		SymbolOverrideController component = base.GetComponent<SymbolOverrideController>();
		KAnim.Build build = ((animFile != null) ? animFile.GetData().build : null);
		if (build != null)
		{
			for (int i = 0; i < build.symbols.Length; i++)
			{
				string text = HashCache.Get().Get(build.symbols[i].hash);
				component.RemoveSymbolOverride(text, override_priority);
			}
		}
	}

	// Token: 0x0600270F RID: 9999 RVA: 0x000DF854 File Offset: 0x000DDA54
	private void UnequippedItem(object data)
	{
		KPrefabID kprefabID = data as KPrefabID;
		if (kprefabID != null)
		{
			Equippable component = kprefabID.GetComponent<Equippable>();
			this.RemoveEquipment(component);
		}
	}

	// Token: 0x06002710 RID: 10000 RVA: 0x000DF880 File Offset: 0x000DDA80
	public void RemoveEquipment(Equippable equippable)
	{
		WearableAccessorizer.WearableType wearableType;
		if (equippable != null && Enum.TryParse<WearableAccessorizer.WearableType>(equippable.def.Slot, out wearableType))
		{
			ClothingOutfitUtility.OutfitType outfitType;
			if (this.TryGetEquippableClothingType(equippable.def, out outfitType) && this.customOutfitItems.ContainsKey(outfitType) && this.wearables.ContainsKey(WearableAccessorizer.WearableType.CustomSuit))
			{
				foreach (ResourceRef<ClothingItemResource> resourceRef in this.customOutfitItems[outfitType])
				{
					this.RemoveAnimBuild(resourceRef.Get().AnimFile, this.wearables[WearableAccessorizer.WearableType.CustomSuit].buildOverridePriority);
				}
				this.RemoveAnimBuild(equippable.GetBuildOverride(), this.wearables[WearableAccessorizer.WearableType.CustomSuit].buildOverridePriority);
				this.wearables.Remove(WearableAccessorizer.WearableType.CustomSuit);
			}
			if (this.wearables.ContainsKey(wearableType))
			{
				this.RemoveAnimBuild(equippable.GetBuildOverride(), this.wearables[wearableType].buildOverridePriority);
				this.wearables.Remove(wearableType);
			}
			this.ApplyWearable();
		}
	}

	// Token: 0x06002711 RID: 10001 RVA: 0x000DF9B4 File Offset: 0x000DDBB4
	public void ClearClothingItems(ClothingOutfitUtility.OutfitType? forOutfitType = null)
	{
		foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> keyValuePair in this.customOutfitItems)
		{
			ClothingOutfitUtility.OutfitType outfitType;
			List<ResourceRef<ClothingItemResource>> list;
			keyValuePair.Deconstruct(out outfitType, out list);
			ClothingOutfitUtility.OutfitType outfitType2 = outfitType;
			if (forOutfitType != null)
			{
				ClothingOutfitUtility.OutfitType? outfitType3 = forOutfitType;
				outfitType = outfitType2;
				if (!((outfitType3.GetValueOrDefault() == outfitType) & (outfitType3 != null)))
				{
					continue;
				}
			}
			this.ApplyClothingItems(outfitType2, Enumerable.Empty<ClothingItemResource>());
		}
	}

	// Token: 0x06002712 RID: 10002 RVA: 0x000DFA3C File Offset: 0x000DDC3C
	public void ApplyClothingItems(ClothingOutfitUtility.OutfitType outfitType, IEnumerable<ClothingItemResource> items)
	{
		items = items.StableSort(delegate(ClothingItemResource resource)
		{
			if (resource.Category == PermitCategory.DupeTops)
			{
				return 10;
			}
			if (resource.Category == PermitCategory.DupeGloves)
			{
				return 8;
			}
			if (resource.Category == PermitCategory.DupeBottoms)
			{
				return 7;
			}
			if (resource.Category == PermitCategory.DupeShoes)
			{
				return 6;
			}
			return 1;
		});
		if (this.customOutfitItems.ContainsKey(outfitType))
		{
			this.customOutfitItems[outfitType].Clear();
		}
		WearableAccessorizer.WearableType wearableType = this.ConvertOutfitTypeToWearableType(outfitType);
		if (this.wearables.ContainsKey(wearableType))
		{
			foreach (KAnimFile kanimFile in this.wearables[wearableType].BuildAnims)
			{
				this.RemoveAnimBuild(kanimFile, this.wearables[wearableType].buildOverridePriority);
			}
			this.wearables[wearableType].ClearAnims();
			if (items.Count<ClothingItemResource>() <= 0)
			{
				this.wearables.Remove(wearableType);
			}
		}
		foreach (ClothingItemResource clothingItemResource in items)
		{
			this.Internal_ApplyClothingItem(outfitType, clothingItemResource);
		}
		this.ApplyWearable();
		Equippable suitEquippable = this.GetSuitEquippable();
		ClothingOutfitUtility.OutfitType outfitType2;
		bool flag = (suitEquippable == null && outfitType == ClothingOutfitUtility.OutfitType.Clothing) || (suitEquippable != null && this.TryGetEquippableClothingType(suitEquippable.def, out outfitType2) && outfitType2 == outfitType);
		if (!base.GetComponent<MinionIdentity>().IsNullOrDestroyed() && this.animController.materialType != KAnimBatchGroup.MaterialType.UI && flag)
		{
			this.QueueOutfitChangedFX();
		}
	}

	// Token: 0x06002713 RID: 10003 RVA: 0x000DFBDC File Offset: 0x000DDDDC
	private void Internal_ApplyClothingItem(ClothingOutfitUtility.OutfitType outfitType, ClothingItemResource clothingItem)
	{
		WearableAccessorizer.WearableType wearableType = this.ConvertOutfitTypeToWearableType(outfitType);
		if (!this.customOutfitItems.ContainsKey(outfitType))
		{
			this.customOutfitItems.Add(outfitType, new List<ResourceRef<ClothingItemResource>>());
		}
		if (!this.customOutfitItems[outfitType].Exists((ResourceRef<ClothingItemResource> x) => x.Get().IdHash == clothingItem.IdHash))
		{
			if (this.wearables.ContainsKey(wearableType))
			{
				foreach (ResourceRef<ClothingItemResource> resourceRef in this.customOutfitItems[outfitType].FindAll((ResourceRef<ClothingItemResource> x) => x.Get().Category == clothingItem.Category))
				{
					this.Internal_RemoveClothingItem(outfitType, resourceRef.Get());
				}
			}
			this.customOutfitItems[outfitType].Add(new ResourceRef<ClothingItemResource>(clothingItem));
		}
		bool flag;
		if (base.GetComponent<MinionIdentity>().IsNullOrDestroyed() || this.animController.materialType == KAnimBatchGroup.MaterialType.UI)
		{
			flag = true;
		}
		else if (outfitType == ClothingOutfitUtility.OutfitType.Clothing)
		{
			flag = true;
		}
		else
		{
			Equippable suitEquippable = this.GetSuitEquippable();
			ClothingOutfitUtility.OutfitType outfitType2;
			flag = suitEquippable != null && this.TryGetEquippableClothingType(suitEquippable.def, out outfitType2) && outfitType2 == outfitType;
		}
		if (flag)
		{
			if (!this.wearables.ContainsKey(wearableType))
			{
				int num = ((wearableType == WearableAccessorizer.WearableType.CustomClothing) ? 4 : 6);
				this.wearables[wearableType] = new WearableAccessorizer.Wearable(new List<KAnimFile>(), num);
			}
			this.wearables[wearableType].AddAnim(clothingItem.AnimFile);
		}
	}

	// Token: 0x06002714 RID: 10004 RVA: 0x000DFD74 File Offset: 0x000DDF74
	private void Internal_RemoveClothingItem(ClothingOutfitUtility.OutfitType outfitType, ClothingItemResource clothing_item)
	{
		WearableAccessorizer.WearableType wearableType = this.ConvertOutfitTypeToWearableType(outfitType);
		if (this.customOutfitItems.ContainsKey(outfitType))
		{
			this.customOutfitItems[outfitType].RemoveAll((ResourceRef<ClothingItemResource> x) => x.Get().IdHash == clothing_item.IdHash);
		}
		if (this.wearables.ContainsKey(wearableType))
		{
			if (this.wearables[wearableType].RemoveAnim(clothing_item.AnimFile))
			{
				this.RemoveAnimBuild(clothing_item.AnimFile, this.wearables[wearableType].buildOverridePriority);
			}
			if (this.wearables[wearableType].BuildAnims.Count <= 0)
			{
				this.wearables.Remove(wearableType);
			}
		}
	}

	// Token: 0x06002715 RID: 10005 RVA: 0x000DFE36 File Offset: 0x000DE036
	private WearableAccessorizer.WearableType ConvertOutfitTypeToWearableType(ClothingOutfitUtility.OutfitType outfitType)
	{
		if (outfitType == ClothingOutfitUtility.OutfitType.Clothing)
		{
			return WearableAccessorizer.WearableType.CustomClothing;
		}
		if (outfitType != ClothingOutfitUtility.OutfitType.AtmoSuit)
		{
			global::Debug.LogWarning("Add a wearable type for clothing outfit type " + outfitType.ToString());
			return WearableAccessorizer.WearableType.Basic;
		}
		return WearableAccessorizer.WearableType.CustomSuit;
	}

	// Token: 0x06002716 RID: 10006 RVA: 0x000DFE64 File Offset: 0x000DE064
	public void RestoreWearables(Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> stored_wearables, Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> clothing)
	{
		if (stored_wearables != null)
		{
			this.wearables = stored_wearables;
			foreach (KeyValuePair<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> keyValuePair in this.wearables)
			{
				keyValuePair.Value.Deserialize();
			}
		}
		if (clothing != null)
		{
			foreach (KeyValuePair<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> keyValuePair2 in clothing)
			{
				this.ApplyClothingItems(keyValuePair2.Key, keyValuePair2.Value.Select((ResourceRef<ClothingItemResource> i) => i.Get()));
			}
		}
		this.ApplyWearable();
	}

	// Token: 0x06002717 RID: 10007 RVA: 0x000DFF40 File Offset: 0x000DE140
	public bool HasPermitCategoryItem(ClothingOutfitUtility.OutfitType wearable_type, PermitCategory category)
	{
		bool flag = false;
		if (this.customOutfitItems.ContainsKey(wearable_type))
		{
			flag = this.customOutfitItems[wearable_type].Exists((ResourceRef<ClothingItemResource> resource) => resource.Get().Category == category);
		}
		return flag;
	}

	// Token: 0x06002718 RID: 10008 RVA: 0x000DFF89 File Offset: 0x000DE189
	private void QueueOutfitChangedFX()
	{
		this.waitingForOutfitChangeFX = true;
	}

	// Token: 0x06002719 RID: 10009 RVA: 0x000DFF94 File Offset: 0x000DE194
	private void Update()
	{
		if (this.waitingForOutfitChangeFX && !LockerNavigator.Instance.gameObject.activeInHierarchy)
		{
			Game.Instance.SpawnFX(SpawnFXHashes.MinionOutfitChanged, new Vector3(base.transform.position.x, base.transform.position.y, Grid.GetLayerZ(Grid.SceneLayer.FXFront)), 0f);
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Plus, "Changed Clothes", base.transform, new Vector3(0f, 0.5f, 0f), 1.5f, false, false);
			KFMOD.PlayOneShot(GlobalAssets.GetSound("SupplyCloset_Dupe_Clothing_Change", false), base.transform.position, 1f);
			this.waitingForOutfitChangeFX = false;
		}
	}

	// Token: 0x040016C9 RID: 5833
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x040016CA RID: 5834
	[Obsolete("Deprecated, use customOufitItems[ClothingOutfitUtility.OutfitType.Clothing]")]
	[Serialize]
	private List<ResourceRef<ClothingItemResource>> clothingItems = new List<ResourceRef<ClothingItemResource>>();

	// Token: 0x040016CB RID: 5835
	[Serialize]
	private string joyResponsePermitId;

	// Token: 0x040016CC RID: 5836
	[Serialize]
	private Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>> customOutfitItems = new Dictionary<ClothingOutfitUtility.OutfitType, List<ResourceRef<ClothingItemResource>>>();

	// Token: 0x040016CD RID: 5837
	private bool waitingForOutfitChangeFX;

	// Token: 0x040016CE RID: 5838
	[Serialize]
	private Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable> wearables = new Dictionary<WearableAccessorizer.WearableType, WearableAccessorizer.Wearable>();

	// Token: 0x040016CF RID: 5839
	private static string torso = "torso";

	// Token: 0x040016D0 RID: 5840
	private static string cropped = "_cropped";

	// Token: 0x020014D7 RID: 5335
	public enum WearableType
	{
		// Token: 0x04006E19 RID: 28185
		Basic,
		// Token: 0x04006E1A RID: 28186
		CustomClothing,
		// Token: 0x04006E1B RID: 28187
		Outfit,
		// Token: 0x04006E1C RID: 28188
		Suit,
		// Token: 0x04006E1D RID: 28189
		CustomSuit
	}

	// Token: 0x020014D8 RID: 5336
	[SerializationConfig(MemberSerialization.OptIn)]
	public class Wearable
	{
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06008F25 RID: 36645 RVA: 0x0035D0E6 File Offset: 0x0035B2E6
		public List<KAnimFile> BuildAnims
		{
			get
			{
				return this.buildAnims;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06008F26 RID: 36646 RVA: 0x0035D0EE File Offset: 0x0035B2EE
		public List<string> AnimNames
		{
			get
			{
				return this.animNames;
			}
		}

		// Token: 0x06008F27 RID: 36647 RVA: 0x0035D0F8 File Offset: 0x0035B2F8
		public Wearable(List<KAnimFile> buildAnims, int buildOverridePriority)
		{
			this.buildAnims = buildAnims;
			this.animNames = buildAnims.Select((KAnimFile animFile) => animFile.name).ToList<string>();
			this.buildOverridePriority = buildOverridePriority;
		}

		// Token: 0x06008F28 RID: 36648 RVA: 0x0035D149 File Offset: 0x0035B349
		public Wearable(KAnimFile buildAnim, int buildOverridePriority)
		{
			this.buildAnims = new List<KAnimFile> { buildAnim };
			this.animNames = new List<string> { buildAnim.name };
			this.buildOverridePriority = buildOverridePriority;
		}

		// Token: 0x06008F29 RID: 36649 RVA: 0x0035D184 File Offset: 0x0035B384
		public Wearable(List<ResourceRef<ClothingItemResource>> items, int buildOverridePriority)
		{
			this.buildAnims = new List<KAnimFile>();
			this.animNames = new List<string>();
			this.buildOverridePriority = buildOverridePriority;
			foreach (ResourceRef<ClothingItemResource> resourceRef in items)
			{
				ClothingItemResource clothingItemResource = resourceRef.Get();
				this.buildAnims.Add(clothingItemResource.AnimFile);
				this.animNames.Add(clothingItemResource.animFilename);
			}
		}

		// Token: 0x06008F2A RID: 36650 RVA: 0x0035D218 File Offset: 0x0035B418
		public void AddCustomItems(List<ResourceRef<ClothingItemResource>> items)
		{
			foreach (ResourceRef<ClothingItemResource> resourceRef in items)
			{
				ClothingItemResource clothingItemResource = resourceRef.Get();
				this.buildAnims.Add(clothingItemResource.AnimFile);
				this.animNames.Add(clothingItemResource.animFilename);
			}
		}

		// Token: 0x06008F2B RID: 36651 RVA: 0x0035D288 File Offset: 0x0035B488
		public void Deserialize()
		{
			if (this.animNames != null)
			{
				this.buildAnims = new List<KAnimFile>();
				for (int i = 0; i < this.animNames.Count; i++)
				{
					KAnimFile kanimFile = null;
					if (Assets.TryGetAnim(this.animNames[i], out kanimFile))
					{
						this.buildAnims.Add(kanimFile);
					}
				}
			}
		}

		// Token: 0x06008F2C RID: 36652 RVA: 0x0035D2E6 File Offset: 0x0035B4E6
		public void AddAnim(KAnimFile animFile)
		{
			this.buildAnims.Add(animFile);
			this.animNames.Add(animFile.name);
		}

		// Token: 0x06008F2D RID: 36653 RVA: 0x0035D305 File Offset: 0x0035B505
		public bool RemoveAnim(KAnimFile animFile)
		{
			return this.buildAnims.Remove(animFile) | this.animNames.Remove(animFile.name);
		}

		// Token: 0x06008F2E RID: 36654 RVA: 0x0035D325 File Offset: 0x0035B525
		public void ClearAnims()
		{
			this.buildAnims.Clear();
			this.animNames.Clear();
		}

		// Token: 0x04006E1E RID: 28190
		private List<KAnimFile> buildAnims;

		// Token: 0x04006E1F RID: 28191
		[Serialize]
		private List<string> animNames;

		// Token: 0x04006E20 RID: 28192
		[Serialize]
		public int buildOverridePriority;
	}
}
