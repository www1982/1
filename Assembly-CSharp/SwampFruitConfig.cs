using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class SwampFruitConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A2E RID: 2606 RVA: 0x0003EA10 File Offset: 0x0003CC10
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A2F RID: 2607 RVA: 0x0003EA17 File Offset: 0x0003CC17
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A30 RID: 2608 RVA: 0x0003EA1C File Offset: 0x0003CC1C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(SwampFruitConfig.ID, global::STRINGS.ITEMS.FOOD.SWAMPFRUIT.NAME, global::STRINGS.ITEMS.FOOD.SWAMPFRUIT.DESC, 1f, false, Assets.GetAnim("swampcrop_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 0.72f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMPFRUIT);
	}

	// Token: 0x06000A31 RID: 2609 RVA: 0x0003EA80 File Offset: 0x0003CC80
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0003EA82 File Offset: 0x0003CC82
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400071B RID: 1819
	public static string ID = "SwampFruit";
}
