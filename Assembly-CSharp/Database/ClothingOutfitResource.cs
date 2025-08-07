using System;
using System.Linq;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EE7 RID: 3815
	public class ClothingOutfitResource : Resource, IHasDlcRestrictions
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600796A RID: 31082 RVA: 0x002F7CEC File Offset: 0x002F5EEC
		// (set) Token: 0x0600796B RID: 31083 RVA: 0x002F7CF4 File Offset: 0x002F5EF4
		public string[] itemsInOutfit { get; private set; }

		// Token: 0x0600796C RID: 31084 RVA: 0x002F7CFD File Offset: 0x002F5EFD
		public ClothingOutfitResource(string id, string[] items_in_outfit, string name, ClothingOutfitUtility.OutfitType outfitType, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, name)
		{
			this.itemsInOutfit = items_in_outfit;
			this.outfitType = outfitType;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x002F7D26 File Offset: 0x002F5F26
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			Sprite sprite = Assets.GetSprite("unknown");
			return new global::Tuple<Sprite, Color>(sprite, (sprite != null) ? Color.white : Color.clear);
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x002F7D51 File Offset: 0x002F5F51
		public string GetDlcIdFrom()
		{
			if (this.requiredDlcIds == null)
			{
				return null;
			}
			return this.requiredDlcIds.Last<string>();
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x002F7D68 File Offset: 0x002F5F68
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x002F7D70 File Offset: 0x002F5F70
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x04005677 RID: 22135
		public ClothingOutfitUtility.OutfitType outfitType;

		// Token: 0x04005679 RID: 22137
		public string[] requiredDlcIds;

		// Token: 0x0400567A RID: 22138
		public string[] forbiddenDlcIds;
	}
}
