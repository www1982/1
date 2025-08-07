using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C2 RID: 450
public class BasicPlantBarConfig : IEntityConfig
{
	// Token: 0x06000903 RID: 2307 RVA: 0x0003C99C File Offset: 0x0003AB9C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("BasicPlantBar", global::STRINGS.ITEMS.FOOD.BASICPLANTBAR.NAME, global::STRINGS.ITEMS.FOOD.BASICPLANTBAR.DESC, 1f, false, Assets.GetAnim("liceloaf_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BASICPLANTBAR);
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0003CA00 File Offset: 0x0003AC00
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0003CA02 File Offset: 0x0003AC02
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000697 RID: 1687
	public const string ID = "BasicPlantBar";

	// Token: 0x04000698 RID: 1688
	public const string ANIM = "liceloaf_kanim";

	// Token: 0x04000699 RID: 1689
	public static ComplexRecipe recipe;
}
