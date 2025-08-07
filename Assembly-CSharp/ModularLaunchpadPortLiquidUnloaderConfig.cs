using System;
using UnityEngine;

// Token: 0x02000336 RID: 822
public class ModularLaunchpadPortLiquidUnloaderConfig : IBuildingConfig
{
	// Token: 0x060010F9 RID: 4345 RVA: 0x00063C79 File Offset: 0x00061E79
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x00063C80 File Offset: 0x00061E80
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortLiquidUnloader", "conduit_port_liquid_unloader_kanim", ConduitType.Liquid, false, 2, 3);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x00063C95 File Offset: 0x00061E95
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Liquid, 10f, false);
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x00063CA5 File Offset: 0x00061EA5
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000AB3 RID: 2739
	public const string ID = "ModularLaunchpadPortLiquidUnloader";
}
