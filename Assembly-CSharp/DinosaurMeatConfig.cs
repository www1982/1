using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D5 RID: 469
public class DinosaurMeatConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000966 RID: 2406 RVA: 0x0003D3BC File Offset: 0x0003B5BC
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0003D3C3 File Offset: 0x0003B5C3
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0003D3C8 File Offset: 0x0003B5C8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("DinosaurMeat", global::STRINGS.ITEMS.FOOD.DINOSAURMEAT.NAME, global::STRINGS.ITEMS.FOOD.DINOSAURMEAT.DESC, 1f, false, Assets.GetAnim("dinomeat_raw_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.DINOSAURMEAT);
		return gameObject;
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0003D42E File Offset: 0x0003B62E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0003D430 File Offset: 0x0003B630
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C3 RID: 1731
	public const string ID = "DinosaurMeat";
}
