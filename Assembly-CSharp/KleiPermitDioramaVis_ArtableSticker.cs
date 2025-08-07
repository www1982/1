using System;
using Database;
using UnityEngine;

// Token: 0x02000D0E RID: 3342
public class KleiPermitDioramaVis_ArtableSticker : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006712 RID: 26386 RVA: 0x0026EE24 File Offset: 0x0026D024
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006713 RID: 26387 RVA: 0x0026EE2C File Offset: 0x0026D02C
	public void ConfigureSetup()
	{
		SymbolOverrideControllerUtil.AddToPrefab(this.buildingKAnim.gameObject);
	}

	// Token: 0x06006714 RID: 26388 RVA: 0x0026EE40 File Offset: 0x0026D040
	public void ConfigureWith(PermitResource permit)
	{
		DbStickerBomb dbStickerBomb = (DbStickerBomb)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, dbStickerBomb);
	}

	// Token: 0x040046AF RID: 18095
	[SerializeField]
	private KBatchedAnimController buildingKAnim;
}
