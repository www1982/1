using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000EF6 RID: 3830
	public class MonumentParts : ResourceSet<MonumentPartResource>
	{
		// Token: 0x0600799F RID: 31135 RVA: 0x002FEDC0 File Offset: 0x002FCFC0
		public MonumentParts(ResourceSet parent)
			: base("MonumentParts", parent)
		{
			base.Initialize();
			foreach (MonumentPartInfo monumentPartInfo in Blueprints.Get().all.monumentParts)
			{
				this.Add(monumentPartInfo.id, monumentPartInfo.name, monumentPartInfo.desc, monumentPartInfo.rarity, monumentPartInfo.animFile, monumentPartInfo.state, monumentPartInfo.symbolName, monumentPartInfo.part, monumentPartInfo.requiredDlcIds, monumentPartInfo.forbiddenDlcIds);
			}
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x002FEE6C File Offset: 0x002FD06C
		public void Add(string id, string name, string desc, PermitRarity rarity, string animFilename, string state, string symbolName, MonumentPartResource.Part part, string[] requiredDlcIds, string[] forbiddenDlcIds)
		{
			MonumentPartResource monumentPartResource = new MonumentPartResource(id, name, desc, rarity, animFilename, state, symbolName, part, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(monumentPartResource);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x002FEE9C File Offset: 0x002FD09C
		public List<MonumentPartResource> GetParts(MonumentPartResource.Part part)
		{
			return this.resources.FindAll((MonumentPartResource mpr) => mpr.part == part);
		}
	}
}
