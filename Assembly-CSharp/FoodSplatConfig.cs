using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200020B RID: 523
public class FoodSplatConfig : IEntityConfig
{
	// Token: 0x06000A72 RID: 2674 RVA: 0x0003F660 File Offset: 0x0003D860
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateBasicEntity("FoodSplat", global::STRINGS.ITEMS.FOOD.FOODSPLAT.NAME, global::STRINGS.ITEMS.FOOD.FOODSPLAT.DESC, 1f, true, Assets.GetAnim("sticker_a_kanim"), "idle_sticker_a", Grid.SceneLayer.Backwall, SimHashes.Creature, null, 293f);
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0003F6B1 File Offset: 0x0003D8B1
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<OccupyArea>().SetCellOffsets(new CellOffset[1]);
		inst.AddComponent<Modifiers>();
		inst.AddOrGet<KSelectable>();
		inst.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER2);
		inst.AddOrGetDef<Splat.Def>();
		inst.AddOrGet<SplatWorkable>();
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x0003F6F0 File Offset: 0x0003D8F0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000747 RID: 1863
	public const string ID = "FoodSplat";
}
