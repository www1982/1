using System;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D16 RID: 3350
public class KleiPermitDioramaVis_DupeEquipment : KMonoBehaviour, IKleiPermitDioramaVisTarget
{
	// Token: 0x06006733 RID: 26419 RVA: 0x0026F338 File Offset: 0x0026D538
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06006734 RID: 26420 RVA: 0x0026F340 File Offset: 0x0026D540
	public void ConfigureSetup()
	{
		this.uiMannequin.shouldShowOutfitWithDefaultItems = false;
	}

	// Token: 0x06006735 RID: 26421 RVA: 0x0026F350 File Offset: 0x0026D550
	public void ConfigureWith(PermitResource permit)
	{
		ClothingItemResource clothingItemResource = permit as ClothingItemResource;
		if (clothingItemResource != null)
		{
			this.uiMannequin.SetOutfit(clothingItemResource.outfitType, new ClothingItemResource[] { clothingItemResource });
			this.uiMannequin.ReactToClothingItemChange(clothingItemResource.Category);
		}
		this.dioramaBGImage.sprite = KleiPermitDioramaVis.GetDioramaBackground(permit.Category);
	}

	// Token: 0x040046BF RID: 18111
	[SerializeField]
	private UIMannequin uiMannequin;

	// Token: 0x040046C0 RID: 18112
	[Header("Diorama Backgrounds")]
	[SerializeField]
	private Image dioramaBGImage;

	// Token: 0x040046C1 RID: 18113
	[SerializeField]
	private Sprite clothingBG;

	// Token: 0x040046C2 RID: 18114
	[SerializeField]
	private Sprite atmosuitBG;
}
