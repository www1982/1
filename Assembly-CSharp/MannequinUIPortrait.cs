using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000317 RID: 791
public class MannequinUIPortrait : IEntityConfig
{
	// Token: 0x0600104E RID: 4174 RVA: 0x00061700 File Offset: 0x0005F900
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MannequinUIPortrait.ID, MannequinUIPortrait.ID, true);
		RectTransform rectTransform = gameObject.AddOrGet<RectTransform>();
		rectTransform.anchorMin = new Vector2(0f, 0f);
		rectTransform.anchorMax = new Vector2(1f, 1f);
		rectTransform.pivot = new Vector2(0.5f, 0f);
		rectTransform.anchoredPosition = new Vector2(0f, 0f);
		rectTransform.sizeDelta = new Vector2(0f, 0f);
		LayoutElement layoutElement = gameObject.AddOrGet<LayoutElement>();
		layoutElement.preferredHeight = 100f;
		layoutElement.preferredWidth = 100f;
		gameObject.AddOrGet<BoxCollider2D>().size = new Vector2(1f, 1f);
		gameObject.AddOrGet<Accessorizer>();
		gameObject.AddOrGet<WearableAccessorizer>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.materialType = KAnimBatchGroup.MaterialType.UI;
		kbatchedAnimController.animScale = 0.5f;
		kbatchedAnimController.setScaleFromAnim = false;
		kbatchedAnimController.animOverrideSize = new Vector2(100f, 120f);
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("mannequin_kanim") };
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		BaseMinionConfig.ConfigureSymbols(gameObject, false);
		return gameObject;
	}

	// Token: 0x0600104F RID: 4175 RVA: 0x0006182F File Offset: 0x0005FA2F
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001050 RID: 4176 RVA: 0x00061831 File Offset: 0x0005FA31
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A6F RID: 2671
	public static string ID = "MannequinUIPortrait";
}
