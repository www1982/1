using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EF0 RID: 3824
	public class EquippableFacadeResource : PermitResource
	{
		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06007984 RID: 31108 RVA: 0x002FBBE2 File Offset: 0x002F9DE2
		// (set) Token: 0x06007985 RID: 31109 RVA: 0x002FBBEA File Offset: 0x002F9DEA
		public string BuildOverride { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06007986 RID: 31110 RVA: 0x002FBBF3 File Offset: 0x002F9DF3
		// (set) Token: 0x06007987 RID: 31111 RVA: 0x002FBBFB File Offset: 0x002F9DFB
		public string DefID { get; private set; }

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06007988 RID: 31112 RVA: 0x002FBC04 File Offset: 0x002F9E04
		// (set) Token: 0x06007989 RID: 31113 RVA: 0x002FBC0C File Offset: 0x002F9E0C
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x0600798A RID: 31114 RVA: 0x002FBC15 File Offset: 0x002F9E15
		public EquippableFacadeResource(string id, string name, string desc, PermitRarity rarity, string buildOverride, string defID, string animFile, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, name, desc, PermitCategory.Equipment, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.DefID = defID;
			this.BuildOverride = buildOverride;
			this.AnimFile = Assets.GetAnim(animFile);
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x002FBC4C File Offset: 0x002F9E4C
		public global::Tuple<Sprite, Color> GetUISprite()
		{
			if (this.AnimFile == null)
			{
				global::Debug.LogError("Facade AnimFile is null: " + this.DefID);
			}
			Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(this.AnimFile, "ui", false, "");
			return new global::Tuple<Sprite, Color>(uispriteFromMultiObjectAnim, (uispriteFromMultiObjectAnim != null) ? Color.white : Color.clear);
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x002FBCAC File Offset: 0x002F9EAC
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = this.GetUISprite().first;
			GameObject gameObject = Assets.TryGetPrefab(this.DefID);
			if (gameObject == null || !gameObject)
			{
				permitPresentationInfo.SetFacadeForPrefabID(this.DefID);
			}
			else
			{
				permitPresentationInfo.SetFacadeForPrefabName(gameObject.GetProperName());
			}
			return permitPresentationInfo;
		}
	}
}
