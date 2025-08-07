using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EE0 RID: 3808
	public class BuildingFacadeResource : PermitResource
	{
		// Token: 0x0600794D RID: 31053 RVA: 0x002EF220 File Offset: 0x002ED420
		[Obsolete("Please use constructor with dlcIds parameter")]
		public BuildingFacadeResource(string Id, string Name, string Description, PermitRarity Rarity, string PrefabID, string AnimFile, Dictionary<string, string> workables = null)
			: this(Id, Name, Description, Rarity, PrefabID, AnimFile, workables, null, null)
		{
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x002EF240 File Offset: 0x002ED440
		[Obsolete("Please use constructor with dlcIds parameter")]
		public BuildingFacadeResource(string Id, string Name, string Description, PermitRarity Rarity, string PrefabID, string AnimFile, string[] dlcIds, Dictionary<string, string> workables = null)
			: this(Id, Name, Description, Rarity, PrefabID, AnimFile, workables, null, null)
		{
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x002EF260 File Offset: 0x002ED460
		public BuildingFacadeResource(string Id, string Name, string Description, PermitRarity Rarity, string PrefabID, string AnimFile, Dictionary<string, string> workables = null, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(Id, Name, Description, PermitCategory.Building, Rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.Id = Id;
			this.PrefabID = PrefabID;
			this.AnimFile = AnimFile;
			this.InteractFile = workables;
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x002EF294 File Offset: 0x002ED494
		public void Init()
		{
			GameObject gameObject = Assets.TryGetPrefab(this.PrefabID);
			if (gameObject == null)
			{
				return;
			}
			gameObject.AddOrGet<BuildingFacade>();
			BuildingDef def = gameObject.GetComponent<Building>().Def;
			if (def != null)
			{
				def.AddFacade(this.Id);
			}
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x002EF2E4 File Offset: 0x002ED4E4
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim(this.AnimFile), "ui", false, "");
			permitPresentationInfo.SetFacadeForPrefabID(this.PrefabID);
			return permitPresentationInfo;
		}

		// Token: 0x0400548F RID: 21647
		public string PrefabID;

		// Token: 0x04005490 RID: 21648
		public string AnimFile;

		// Token: 0x04005491 RID: 21649
		public Dictionary<string, string> InteractFile;
	}
}
