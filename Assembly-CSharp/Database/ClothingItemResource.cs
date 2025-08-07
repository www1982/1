using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000EE5 RID: 3813
	public class ClothingItemResource : PermitResource
	{
		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06007961 RID: 31073 RVA: 0x002F7A9A File Offset: 0x002F5C9A
		// (set) Token: 0x06007962 RID: 31074 RVA: 0x002F7AA2 File Offset: 0x002F5CA2
		public string animFilename { get; private set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06007963 RID: 31075 RVA: 0x002F7AAB File Offset: 0x002F5CAB
		// (set) Token: 0x06007964 RID: 31076 RVA: 0x002F7AB3 File Offset: 0x002F5CB3
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06007965 RID: 31077 RVA: 0x002F7ABC File Offset: 0x002F5CBC
		// (set) Token: 0x06007966 RID: 31078 RVA: 0x002F7AC4 File Offset: 0x002F5CC4
		public ClothingOutfitUtility.OutfitType outfitType { get; private set; }

		// Token: 0x06007967 RID: 31079 RVA: 0x002F7ACD File Offset: 0x002F5CCD
		public ClothingItemResource(string id, string name, string desc, ClothingOutfitUtility.OutfitType outfitType, PermitCategory category, PermitRarity rarity, string animFile, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, name, desc, category, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.AnimFile = Assets.GetAnim(animFile);
			this.animFilename = animFile;
			this.outfitType = outfitType;
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x002F7B04 File Offset: 0x002F5D04
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			if (this.AnimFile == null)
			{
				Debug.LogError("Clothing kanim is missing from bundle: " + this.animFilename);
			}
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(this.AnimFile, "ui", false, "");
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.CLOTHING_ITEM_FACADE_FOR);
			return permitPresentationInfo;
		}
	}
}
