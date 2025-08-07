using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class SmokedVegetablesConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A02 RID: 2562 RVA: 0x0003E56C File Offset: 0x0003C76C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x0003E573 File Offset: 0x0003C773
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0003E578 File Offset: 0x0003C778
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SmokedVegetables", global::STRINGS.ITEMS.FOOD.SMOKEDVEGETABLES.NAME, global::STRINGS.ITEMS.FOOD.SMOKEDVEGETABLES.DESC, 1f, false, Assets.GetAnim("smokedvegetables_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SMOKED_VEGETABLES);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0003E5DC File Offset: 0x0003C7DC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0003E5DE File Offset: 0x0003C7DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000703 RID: 1795
	public const string ID = "SmokedVegetables";

	// Token: 0x04000704 RID: 1796
	public static ComplexRecipe recipe;
}
