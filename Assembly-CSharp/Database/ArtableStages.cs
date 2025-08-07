using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000ED4 RID: 3796
	public class ArtableStages : ResourceSet<ArtableStage>
	{
		// Token: 0x06007927 RID: 31015 RVA: 0x002ED68C File Offset: 0x002EB88C
		[Obsolete("Use ArtableStages with required/forbidden")]
		public ArtableStage Add(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, string status_id, string prefabId, string symbolname, string[] dlcIds)
		{
			DlcRestrictionsUtil.TemporaryHelperObject transientHelperObjectFromAllowList = DlcRestrictionsUtil.GetTransientHelperObjectFromAllowList(dlcIds);
			return this.Add(id, name, desc, rarity, animFile, anim, decor_value, cheer_on_complete, status_id, prefabId, symbolname, transientHelperObjectFromAllowList.GetRequiredDlcIds(), transientHelperObjectFromAllowList.GetForbiddenDlcIds());
		}

		// Token: 0x06007928 RID: 31016 RVA: 0x002ED6C8 File Offset: 0x002EB8C8
		public ArtableStage Add(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, string status_id, string prefabId, string symbolname, string[] requiredDlcIds, string[] forbiddenDlcIds)
		{
			ArtableStatusItem artableStatusItem = Db.Get().ArtableStatuses.Get(status_id);
			ArtableStage artableStage = new ArtableStage(id, name, desc, rarity, animFile, anim, decor_value, cheer_on_complete, artableStatusItem, prefabId, symbolname, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(artableStage);
			return artableStage;
		}

		// Token: 0x06007929 RID: 31017 RVA: 0x002ED710 File Offset: 0x002EB910
		public ArtableStages(ResourceSet parent)
			: base("ArtableStages", parent)
		{
			foreach (ArtableInfo artableInfo in Blueprints.Get().all.artables)
			{
				this.Add(artableInfo.id, artableInfo.name, artableInfo.desc, artableInfo.rarity, artableInfo.animFile, artableInfo.anim, artableInfo.decor_value, artableInfo.cheer_on_complete, artableInfo.status_id, artableInfo.prefabId, artableInfo.symbolname, artableInfo.GetRequiredDlcIds(), artableInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x0600792A RID: 31018 RVA: 0x002ED7C8 File Offset: 0x002EB9C8
		public List<ArtableStage> GetPrefabStages(Tag prefab_id)
		{
			return this.resources.FindAll((ArtableStage stage) => stage.prefabId == prefab_id);
		}

		// Token: 0x0600792B RID: 31019 RVA: 0x002ED7F9 File Offset: 0x002EB9F9
		public ArtableStage DefaultPrefabStage(Tag prefab_id)
		{
			return this.GetPrefabStages(prefab_id).Find((ArtableStage stage) => stage.statusItem == Db.Get().ArtableStatuses.AwaitingArting);
		}
	}
}
