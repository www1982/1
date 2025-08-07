using System;
using UnityEngine;

// Token: 0x02000335 RID: 821
public class ModularLaunchpadPortLiquidConfig : IBuildingConfig
{
	// Token: 0x060010F4 RID: 4340 RVA: 0x00063C3C File Offset: 0x00061E3C
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x00063C43 File Offset: 0x00061E43
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortLiquid", "conduit_port_liquid_loader_kanim", ConduitType.Liquid, true, 2, 2);
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x00063C58 File Offset: 0x00061E58
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Liquid, 10f, true);
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x00063C68 File Offset: 0x00061E68
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000AB2 RID: 2738
	public const string ID = "ModularLaunchpadPortLiquid";
}
