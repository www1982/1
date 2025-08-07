using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000203 RID: 515
public class VineFruitConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A45 RID: 2629 RVA: 0x0003EC74 File Offset: 0x0003CE74
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000A46 RID: 2630 RVA: 0x0003EC7B File Offset: 0x0003CE7B
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A47 RID: 2631 RVA: 0x0003EC80 File Offset: 0x0003CE80
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(VineFruitConfig.ID, global::STRINGS.ITEMS.FOOD.VINEFRUIT.NAME, global::STRINGS.ITEMS.FOOD.VINEFRUIT.DESC, 1f, false, Assets.GetAnim("ova_melon_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.VINEFRUIT);
		return gameObject;
	}

	// Token: 0x06000A48 RID: 2632 RVA: 0x0003ECE6 File Offset: 0x0003CEE6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A49 RID: 2633 RVA: 0x0003ECE8 File Offset: 0x0003CEE8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000726 RID: 1830
	public static string ID = "VineFruit";

	// Token: 0x04000727 RID: 1831
	public const float KCalPerUnit = 325000f;
}
