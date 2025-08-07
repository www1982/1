using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DB RID: 475
public class FriesCarrotConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000983 RID: 2435 RVA: 0x0003D684 File Offset: 0x0003B884
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0003D68B File Offset: 0x0003B88B
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x0003D690 File Offset: 0x0003B890
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FriesCarrot", global::STRINGS.ITEMS.FOOD.FRIESCARROT.NAME, global::STRINGS.ITEMS.FOOD.FRIESCARROT.DESC, 1f, false, Assets.GetAnim("rootfries_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRIES_CARROT);
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x0003D6F4 File Offset: 0x0003B8F4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x0003D6F6 File Offset: 0x0003B8F6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006CB RID: 1739
	public const string ID = "FriesCarrot";

	// Token: 0x040006CC RID: 1740
	public static ComplexRecipe recipe;
}
