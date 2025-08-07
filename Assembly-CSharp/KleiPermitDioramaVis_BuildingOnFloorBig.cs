using System;
using Database;
using UnityEngine;

// Token: 0x02000D13 RID: 3347
public class KleiPermitDioramaVis_BuildingOnFloorBig : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006726 RID: 26406 RVA: 0x0026F1CC File Offset: 0x0026D3CC
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006727 RID: 26407 RVA: 0x0026F1D4 File Offset: 0x0026D3D4
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006728 RID: 26408 RVA: 0x0026F1D8 File Offset: 0x0026D3D8
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046B8 RID: 18104
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
