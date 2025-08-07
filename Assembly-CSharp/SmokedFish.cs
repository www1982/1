using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F4 RID: 500
public class SmokedFish : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060009FC RID: 2556 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0003E4F7 File Offset: 0x0003C6F7
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0003E4FC File Offset: 0x0003C6FC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SmokedFish", global::STRINGS.ITEMS.FOOD.SMOKEDFISH.NAME, global::STRINGS.ITEMS.FOOD.SMOKEDFISH.DESC, 1f, false, Assets.GetAnim("smokedfish_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SMOKED_FISH);
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0003E560 File Offset: 0x0003C760
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0003E562 File Offset: 0x0003C762
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000701 RID: 1793
	public const string ID = "SmokedFish";

	// Token: 0x04000702 RID: 1794
	public static ComplexRecipe recipe;
}
