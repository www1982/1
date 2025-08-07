using System;
using Database;
using UnityEngine;

// Token: 0x02000D10 RID: 3344
public class KleiPermitDioramaVis_BuildingHangingHook : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600671A RID: 26394 RVA: 0x0026EF9C File Offset: 0x0026D19C
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600671B RID: 26395 RVA: 0x0026EFA4 File Offset: 0x0026D1A4
	public void ConfigureSetup()
	{
	}

	// Token: 0x0600671C RID: 26396 RVA: 0x0026EFA8 File Offset: 0x0026D1A8
	public void ConfigureWith(PermitResource permit)
	{
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, (BuildingFacadeResource)permit);
		KleiPermitVisUtil.ConfigureBuildingPosition(this.buildingKAnim.rectTransform(), this.buildingKAnimPosition, KleiPermitVisUtil.GetBuildingDef(permit), Alignment.Top());
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046B3 RID: 18099
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040046B4 RID: 18100
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
