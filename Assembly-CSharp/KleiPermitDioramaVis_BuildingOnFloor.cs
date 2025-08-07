using System;
using Database;
using UnityEngine;

// Token: 0x02000D12 RID: 3346
public class KleiPermitDioramaVis_BuildingOnFloor : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006722 RID: 26402 RVA: 0x0026F183 File Offset: 0x0026D383
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006723 RID: 26403 RVA: 0x0026F18B File Offset: 0x0026D38B
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006724 RID: 26404 RVA: 0x0026F190 File Offset: 0x0026D390
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046B7 RID: 18103
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
