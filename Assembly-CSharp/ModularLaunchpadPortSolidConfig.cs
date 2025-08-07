using System;
using UnityEngine;

// Token: 0x02000337 RID: 823
public class ModularLaunchpadPortSolidConfig : IBuildingConfig
{
	// Token: 0x060010FE RID: 4350 RVA: 0x00063CB6 File Offset: 0x00061EB6
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x00063CBD File Offset: 0x00061EBD
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortSolid", "conduit_port_solid_loader_kanim", ConduitType.Solid, true, 2, 2);
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x00063CD2 File Offset: 0x00061ED2
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Solid, 20f, true);
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x00063CE2 File Offset: 0x00061EE2
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000AB4 RID: 2740
	public const string ID = "ModularLaunchpadPortSolid";
}
