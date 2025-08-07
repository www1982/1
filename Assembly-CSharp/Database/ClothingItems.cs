using System;

namespace Database
{
	// Token: 0x02000EE4 RID: 3812
	public class ClothingItems : ResourceSet<ClothingItemResource>
	{
		// Token: 0x0600795E RID: 31070 RVA: 0x002F7974 File Offset: 0x002F5B74
		public ClothingItems(ResourceSet parent)
			: base("ClothingItems", parent)
		{
			base.Initialize();
			foreach (ClothingItemInfo clothingItemInfo in Blueprints.Get().all.clothingItems)
			{
				this.Add(clothingItemInfo.id, clothingItemInfo.name, clothingItemInfo.desc, clothingItemInfo.outfitType, clothingItemInfo.category, clothingItemInfo.rarity, clothingItemInfo.animFile, clothingItemInfo.GetRequiredDlcIds(), clothingItemInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x002F7A18 File Offset: 0x002F5C18
		public ClothingItemResource TryResolveAccessoryResource(ResourceGuid AccessoryGuid)
		{
			if (AccessoryGuid.Guid != null)
			{
				string[] array = AccessoryGuid.Guid.Split('.', StringSplitOptions.None);
				if (array.Length != 0)
				{
					string symbol_name = array[array.Length - 1];
					return this.resources.Find((ClothingItemResource ci) => symbol_name.Contains(ci.Id));
				}
			}
			return null;
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x002F7A6C File Offset: 0x002F5C6C
		public void Add(string id, string name, string desc, ClothingOutfitUtility.OutfitType outfitType, PermitCategory category, PermitRarity rarity, string animFile, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			ClothingItemResource clothingItemResource = new ClothingItemResource(id, name, desc, outfitType, category, rarity, animFile, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(clothingItemResource);
		}
	}
}
