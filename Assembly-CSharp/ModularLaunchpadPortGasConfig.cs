using System;
using UnityEngine;

// Token: 0x02000333 RID: 819
public class ModularLaunchpadPortGasConfig : IBuildingConfig
{
	// Token: 0x060010EA RID: 4330 RVA: 0x00063BC2 File Offset: 0x00061DC2
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x00063BC9 File Offset: 0x00061DC9
	public override BuildingDef CreateBuildingDef()
	{
		return BaseModularLaunchpadPortConfig.CreateBaseLaunchpadPort("ModularLaunchpadPortGas", "conduit_port_gas_loader_kanim", ConduitType.Gas, true, 2, 2);
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x00063BDE File Offset: 0x00061DDE
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BaseModularLaunchpadPortConfig.ConfigureBuildingTemplate(go, prefab_tag, ConduitType.Gas, 1f, true);
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x00063BEE File Offset: 0x00061DEE
	public override void DoPostConfigureComplete(GameObject go)
	{
		BaseModularLaunchpadPortConfig.DoPostConfigureComplete(go, true);
	}

	// Token: 0x04000AB0 RID: 2736
	public const string ID = "ModularLaunchpadPortGas";
}
