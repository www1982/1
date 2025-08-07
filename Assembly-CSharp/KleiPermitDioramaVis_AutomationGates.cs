using System;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D0F RID: 3343
public class KleiPermitDioramaVis_AutomationGates : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006716 RID: 26390 RVA: 0x0026EE68 File Offset: 0x0026D068
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006717 RID: 26391 RVA: 0x0026EE70 File Offset: 0x0026D070
	public void ConfigureSetup()
	{
	}

	// Token: 0x06006718 RID: 26392 RVA: 0x0026EE74 File Offset: 0x0026D074
	public void ConfigureWith(PermitResource permit)
	{
		this.itemSprite.gameObject.SetActive(false);
		BuildingFacadeResource buildingFacadeResource = (BuildingFacadeResource)permit;
		KleiPermitVisUtil.ConfigureToRenderBuilding(this.buildingKAnim, buildingFacadeResource);
		BuildingDef buildingDef = KleiPermitVisUtil.GetBuildingDef(permit);
		Dictionary<int, float> dictionary = new Dictionary<int, float>
		{
			{ 3, 0.7f },
			{ 2, 0.9f },
			{ 1, 0.85f }
		};
		Dictionary<int, float> dictionary2 = new Dictionary<int, float>
		{
			{ 4, 32f },
			{ 3, 32f },
			{ 2, 32f },
			{ 1, 96f }
		};
		this.buildingKAnimPosition.SetOn(this.buildingKAnim);
		this.buildingKAnim.rectTransform().localScale = Vector3.one * dictionary[buildingDef.WidthInCells];
		this.buildingKAnim.rectTransform().anchoredPosition += new Vector2(0f, dictionary2[buildingDef.HeightInCells]);
		KleiPermitVisUtil.AnimateIn(this.buildingKAnim, default(Updater));
	}

	// Token: 0x040046B0 RID: 18096
	[SerializeField]
	private Image itemSprite;

	// Token: 0x040046B1 RID: 18097
	[SerializeField]
	private KBatchedAnimController buildingKAnim;

	// Token: 0x040046B2 RID: 18098
	private PrefabDefinedUIPosition buildingKAnimPosition = new PrefabDefinedUIPosition();
}
