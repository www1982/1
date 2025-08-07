using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E7 RID: 487
public class PemmicanConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060009BB RID: 2491 RVA: 0x0003DD2C File Offset: 0x0003BF2C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x0003DD33 File Offset: 0x0003BF33
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x0003DD38 File Offset: 0x0003BF38
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Pemmican", global::STRINGS.ITEMS.FOOD.PEMMICAN.NAME, global::STRINGS.ITEMS.FOOD.PEMMICAN.DESC, 1f, false, Assets.GetAnim("pemmican_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PEMMICAN);
	}

	// Token: 0x060009BE RID: 2494 RVA: 0x0003DD9C File Offset: 0x0003BF9C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009BF RID: 2495 RVA: 0x0003DD9E File Offset: 0x0003BF9E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006E6 RID: 1766
	public const string ID = "Pemmican";

	// Token: 0x040006E7 RID: 1767
	public const string ANIM = "pemmican_kanim";

	// Token: 0x040006E8 RID: 1768
	public static ComplexRecipe recipe;
}
