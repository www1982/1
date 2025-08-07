using System;
using System.Collections.Generic;

namespace Database
{
	// Token: 0x02000EDF RID: 3807
	public class BuildingFacades : ResourceSet<BuildingFacadeResource>
	{
		// Token: 0x0600794A RID: 31050 RVA: 0x002EF0E8 File Offset: 0x002ED2E8
		public BuildingFacades(ResourceSet parent)
			: base("BuildingFacades", parent)
		{
			base.Initialize();
			foreach (BuildingFacadeInfo buildingFacadeInfo in Blueprints.Get().all.buildingFacades)
			{
				this.Add(buildingFacadeInfo.id, buildingFacadeInfo.name, buildingFacadeInfo.desc, buildingFacadeInfo.rarity, buildingFacadeInfo.prefabId, buildingFacadeInfo.animFile, buildingFacadeInfo.workables, buildingFacadeInfo.GetRequiredDlcIds(), buildingFacadeInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x0600794B RID: 31051 RVA: 0x002EF198 File Offset: 0x002ED398
		public void Add(string id, LocString Name, LocString Desc, PermitRarity rarity, string prefabId, string animFile, Dictionary<string, string> workables = null, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		{
			BuildingFacadeResource buildingFacadeResource = new BuildingFacadeResource(id, Name, Desc, rarity, prefabId, animFile, workables, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(buildingFacadeResource);
		}

		// Token: 0x0600794C RID: 31052 RVA: 0x002EF1D0 File Offset: 0x002ED3D0
		public void PostProcess()
		{
			foreach (BuildingFacadeResource buildingFacadeResource in this.resources)
			{
				buildingFacadeResource.Init();
			}
		}
	}
}
