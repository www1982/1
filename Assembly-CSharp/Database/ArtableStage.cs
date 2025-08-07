using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000ED3 RID: 3795
	public class ArtableStage : PermitResource
	{
		// Token: 0x06007924 RID: 31012 RVA: 0x002ED550 File Offset: 0x002EB750
		[Obsolete("Use ArtableStage with required/forbidden")]
		public ArtableStage(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, ArtableStatusItem status_item, string prefabId, string symbolName, string[] dlcIds)
			: base(id, name, desc, PermitCategory.Artwork, rarity, null, null)
		{
			this.id = id;
			this.animFile = animFile;
			this.anim = anim;
			this.symbolName = symbolName;
			this.decor = decor_value;
			this.cheerOnComplete = cheer_on_complete;
			this.statusItem = status_item;
			this.prefabId = prefabId;
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x002ED5AC File Offset: 0x002EB7AC
		public ArtableStage(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, ArtableStatusItem status_item, string prefabId, string symbolName, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, name, desc, PermitCategory.Artwork, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.id = id;
			this.animFile = animFile;
			this.anim = anim;
			this.symbolName = symbolName;
			this.decor = decor_value;
			this.cheerOnComplete = cheer_on_complete;
			this.statusItem = status_item;
			this.prefabId = prefabId;
		}

		// Token: 0x06007926 RID: 31014 RVA: 0x002ED60C File Offset: 0x002EB80C
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(this.animFile), "ui", false, "");
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.ARTABLE_ITEM_FACADE_FOR.Replace("{ConfigProperName}", Assets.GetPrefab(this.prefabId).GetProperName()).Replace("{ArtableQuality}", this.statusItem.GetName(null)));
			return permitPresentationInfo;
		}

		// Token: 0x04005422 RID: 21538
		public string id;

		// Token: 0x04005423 RID: 21539
		public string anim;

		// Token: 0x04005424 RID: 21540
		public string animFile;

		// Token: 0x04005425 RID: 21541
		public string prefabId;

		// Token: 0x04005426 RID: 21542
		public string symbolName;

		// Token: 0x04005427 RID: 21543
		public int decor;

		// Token: 0x04005428 RID: 21544
		public bool cheerOnComplete;

		// Token: 0x04005429 RID: 21545
		public ArtableStatusItem statusItem;
	}
}
