using System;
using Database;
using UnityEngine;

// Token: 0x02000D0D RID: 3341
public class KleiPermitDioramaVis_ArtableSculpture : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x0600670E RID: 26382 RVA: 0x0026EDCC File Offset: 0x0026CFCC
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x0600670F RID: 26383 RVA: 0x0026EDD4 File Offset: 0x0026CFD4
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x06006710 RID: 26384 RVA: 0x0026EDE8 File Offset: 0x0026CFE8
	public void ConfigureWith(PermitResource permit)
	{
		ArtableStage artableStage = (ArtableStage)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, artableStage);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046AE RID: 18094
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
