using System;
using Database;
using UnityEngine;

// Token: 0x02000D14 RID: 3348
public class KleiPermitDioramaVis_BuildingPresentationStand : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600672A RID: 26410 RVA: 0x0026F214 File Offset: 0x0026D414
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600672B RID: 26411 RVA: 0x0026F21C File Offset: 0x0026D41C
	public void ConfigureSetup()
	{
	}

	// Token: 0x0600672C RID: 26412 RVA: 0x0026F220 File Offset: 0x0026D420
	public void ConfigureWith(PermitResource permit)
	{
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		KleiPermitVisUtil.ConfigureBuildingPosition(this.buildingKAnim.rectTransform(), this.anchorPos, KleiPermitVisUtil.GetBuildingDef(permit), this.lastAlignment);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x0600672D RID: 26413 RVA: 0x0026F278 File Offset: 0x0026D478
	public KleiPermitDioramaVis_BuildingPresentationStand WithAlignment(Alignment alignment)
	{
		this.lastAlignment = alignment;
		this.anchorPos = new Vector2(alignment.x.Remap(new ValueTuple<float, float>(0f, 1f), new ValueTuple<float, float>(-160f, 160f)), alignment.y.Remap(new ValueTuple<float, float>(0f, 1f), new ValueTuple<float, float>(-156f, 156f)));
		return this;
	}

	// Token: 0x040046B9 RID: 18105
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040046BA RID: 18106
	private Alignment lastAlignment;

	// Token: 0x040046BB RID: 18107
	private Vector2 anchorPos;

	// Token: 0x040046BC RID: 18108
	public const float LEFT = -160f;

	// Token: 0x040046BD RID: 18109
	public const float TOP = 156f;
}
