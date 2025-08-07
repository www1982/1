using System;
using UnityEngine;

// Token: 0x02000338 RID: 824
public class ModularLaunchpadPortSolidUnloaderConfig : IBuildingConfig
{
	// Token: 0x06001103 RID: 4355 RVA: 0x00063CF3 File Offset: 0x00061EF3
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x00063CFA File Offset: 0x00061EFA
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortSolidUnloader", "conduit_port_solid_unloader_kanim", ConduitType.Solid, false, 2, 3);
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x00063D0F File Offset: 0x00061F0F
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Solid, 20f, false);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x00063D1F File Offset: 0x00061F1F
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000AB5 RID: 2741
	public const string ID = "ModularLaunchpadPortSolidUnloader";
}
