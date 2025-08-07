using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000308 RID: 776
public class GarbageElectrobankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FF8 RID: 4088 RVA: 0x0005F870 File Offset: 0x0005DA70
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x0005F877 File Offset: 0x0005DA77
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FFA RID: 4090 RVA: 0x0005F87C File Offset: 0x0005DA7C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("GarbageElectrobank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_GARBAGE.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ELECTROBANK_GARBAGE.DESC, 20f, true, Assets.GetAnim("electrobank_large_destroyed_kanim"), "idle1", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.5f, 0.8f, true, 0, SimHashes.Katairite, new List<Tag> { GameTags.PedestalDisplayable });
		gameObject.GetComponent<KCollider2D>();
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGet<OccupyArea>().SetCellOffsets(EntityTemplates.GenerateOffsets(1, 1));
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.PENALTY.TIER0);
		return gameObject;
	}

	// Token: 0x06000FFB RID: 4091 RVA: 0x0005F919 File Offset: 0x0005DB19
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000FFC RID: 4092 RVA: 0x0005F91B File Offset: 0x0005DB1B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A22 RID: 2594
	public const string ID = "GarbageElectrobank";

	// Token: 0x04000A23 RID: 2595
	public const float MASS = 20f;
}
