using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class PlantMeatConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060009C5 RID: 2501 RVA: 0x0003DE29 File Offset: 0x0003C029
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x0003DE30 File Offset: 0x0003C030
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x0003DE34 File Offset: 0x0003C034
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PlantMeat", global::STRINGS.ITEMS.FOOD.PLANTMEAT.NAME, global::STRINGS.ITEMS.FOOD.PLANTMEAT.DESC, 1f, false, Assets.GetAnim("critter_trap_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.PLANTMEAT);
		return gameObject;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x0003DE9A File Offset: 0x0003C09A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x0003DE9C File Offset: 0x0003C09C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006EB RID: 1771
	public const string ID = "PlantMeat";
}
