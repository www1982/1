using System;

namespace Database
{
	// Token: 0x02000EEF RID: 3823
	public class EquippableFacades : ResourceSet<EquippableFacadeResource>
	{
		// Token: 0x06007980 RID: 31104 RVA: 0x002FBAB0 File Offset: 0x002F9CB0
		public EquippableFacades(ResourceSet parent)
			: base("EquippableFacades", parent)
		{
			base.Initialize();
			foreach (EquippableFacadeInfo equippableFacadeInfo in Blueprints.Get().all.equippableFacades)
			{
				this.Add(equippableFacadeInfo.id, equippableFacadeInfo.name, equippableFacadeInfo.desc, equippableFacadeInfo.rarity, equippableFacadeInfo.defID, equippableFacadeInfo.buildOverride, equippableFacadeInfo.animFile, equippableFacadeInfo.GetRequiredDlcIds(), equippableFacadeInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x06007981 RID: 31105 RVA: 0x002FBB54 File Offset: 0x002F9D54
		[Obsolete("Please use Add(...) with required forbidden")]
		public void Add(string id, string name, string desc, PermitRarity rarity, string defID, string buildOverride, string animFile)
		{
			this.Add(id, name, desc, rarity, defID, buildOverride, animFile, null, null);
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x002FBB74 File Offset: 0x002F9D74
		[Obsolete("Please use Add(...) with required forbidden")]
		public void Add(string id, string name, string desc, PermitRarity rarity, string defID, string buildOverride, string animFile, string[] dlcIds)
		{
			DlcRestrictionsUtil.TemporaryHelperObject transientHelperObjectFromAllowList = DlcRestrictionsUtil.GetTransientHelperObjectFromAllowList(dlcIds);
			EquippableFacadeResource equippableFacadeResource = new EquippableFacadeResource(id, name, desc, rarity, buildOverride, defID, animFile, transientHelperObjectFromAllowList.GetRequiredDlcIds(), transientHelperObjectFromAllowList.GetForbiddenDlcIds());
			this.resources.Add(equippableFacadeResource);
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x002FBBB4 File Offset: 0x002F9DB4
		public void Add(string id, string name, string desc, PermitRarity rarity, string defID, string buildOverride, string animFile, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			EquippableFacadeResource equippableFacadeResource = new EquippableFacadeResource(id, name, desc, rarity, buildOverride, defID, animFile, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(equippableFacadeResource);
		}
	}
}
