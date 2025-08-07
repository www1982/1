using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C5 RID: 453
public class DehydratedBerryPieConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000911 RID: 2321 RVA: 0x0003CAF8 File Offset: 0x0003ACF8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0003CAFF File Offset: 0x0003ACFF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0003CB02 File Offset: 0x0003AD02
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0003CB04 File Offset: 0x0003AD04
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0003CB08 File Offset: 0x0003AD08
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_berry_pie_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedBerryPieConfig.ID.Name, global::STRINGS.ITEMS.FOOD.BERRYPIE.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.BERRYPIE.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.BERRY_PIE);
		return gameObject;
	}

	// Token: 0x0400069D RID: 1693
	public static Tag ID = new Tag("DehydratedBerryPie");

	// Token: 0x0400069E RID: 1694
	public const float MASS = 1f;

	// Token: 0x0400069F RID: 1695
	public const string ANIM_FILE = "dehydrated_food_berry_pie_kanim";

	// Token: 0x040006A0 RID: 1696
	public const string INITIAL_ANIM = "idle";
}
