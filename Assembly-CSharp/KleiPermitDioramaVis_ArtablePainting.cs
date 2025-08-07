using System;
using Database;
using UnityEngine;

// Token: 0x02000D0C RID: 3340
public class KleiPermitDioramaVis_ArtablePainting : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600670A RID: 26378 RVA: 0x0026ECF2 File Offset: 0x0026CEF2
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600670B RID: 26379 RVA: 0x0026ECFA File Offset: 0x0026CEFA
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x0600670C RID: 26380 RVA: 0x0026ED10 File Offset: 0x0026CF10
	public void ConfigureWith(PermitResource permit)
	{
		ArtableStage artableStage = (ArtableStage)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, artableStage);
		BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
		this.buildingKAnimPosition.SetOn(this.buildingKAnim);
		this.buildingKAnim.rectTransform().anchoredPosition += new Vector2(0f, -176f * (float)buildingDef.HeightInCells / 2f + 176f);
		this.buildingKAnim.rectTransform().localScale = Vector3.one * 0.9f;
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046AC RID: 18092
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040046AD RID: 18093
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
