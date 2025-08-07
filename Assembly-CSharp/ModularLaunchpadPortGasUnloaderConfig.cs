using System;
using UnityEngine;

// Token: 0x02000334 RID: 820
public class ModularLaunchpadPortGasUnloaderConfig : IBuildingConfig
{
	// Token: 0x060010EF RID: 4335 RVA: 0x00063BFF File Offset: 0x00061DFF
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x00063C06 File Offset: 0x00061E06
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortGasUnloader", "conduit_port_gas_unloader_kanim", ConduitType.Gas, false, 2, 3);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x00063C1B File Offset: 0x00061E1B
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Gas, 1f, false);
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x00063C2B File Offset: 0x00061E2B
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, false);
	}

	// Token: 0x04000AB1 RID: 2737
	public const string ID = "ModularLaunchpadPortGasUnloader";
}
