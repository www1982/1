using System;
using Database;
using UnityEngine;

// Token: 0x02000D19 RID: 3353
public class KleiPermitDioramaVis_MonumentPart : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006745 RID: 26437 RVA: 0x0026F6AA File Offset: 0x0026D8AA
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006746 RID: 26438 RVA: 0x0026F6B2 File Offset: 0x0026D8B2
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006747 RID: 26439 RVA: 0x0026F6B4 File Offset: 0x0026D8B4
	public void ConfigureWith(PermitResource permit)
	{
		MonumentPartResource monumentPartResource = (MonumentPartResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, monumentPartResource);
		BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
		this.buildingKAnimPosition.SetOn(this.buildingKAnim);
		this.buildingKAnim.rectTransform().anchoredPosition += new Vector2(0f, -176f + (float)(buildingDef.HeightInCells * 6));
		this.buildingKAnim.rectTransform().localScale = Vector3.one * 0.55f;
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046D2 RID: 18130
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040046D3 RID: 18131
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
