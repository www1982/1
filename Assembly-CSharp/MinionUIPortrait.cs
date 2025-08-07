using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200031D RID: 797
public class MinionUIPortrait : IEntityConfig
{
	// Token: 0x0600106F RID: 4207 RVA: 0x00062068 File Offset: 0x00060268
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MinionUIPortrait.ID, MinionUIPortrait.ID, true);
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
		kbatchedAnimController.AnimFiles = new KAnimFile[]
		{
			Assets.GetAnim("body_comp_default_kanim"),
			Assets.GetAnim("anim_idles_default_kanim"),
			Assets.GetAnim("anim_idle_healthy_kanim"),
			Assets.GetAnim("anim_cheer_kanim"),
			Assets.GetAnim("inventory_screen_dupe_kanim"),
			Assets.GetAnim("anim_react_wave_shy_kanim")
		};
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		BaseMinionConfig.ConfigureSymbols(gameObject, false);
		return gameObject;
	}

	// Token: 0x06001070 RID: 4208 RVA: 0x000621F1 File Offset: 0x000603F1
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x000621F3 File Offset: 0x000603F3
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A7A RID: 2682
	public static string ID = "MinionUIPortrait";
}
