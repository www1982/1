using System;
using Database;
using UnityEngine;

// Token: 0x02000D15 RID: 3349
public class KleiPermitDioramaVis_BuildingRocket : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600672F RID: 26415 RVA: 0x0026F2F2 File Offset: 0x0026D4F2
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006730 RID: 26416 RVA: 0x0026F2FA File Offset: 0x0026D4FA
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006731 RID: 26417 RVA: 0x0026F2FC File Offset: 0x0026D4FC
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046BE RID: 18110
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
