using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class TableSaltConfig : IEntityConfig
{
	// Token: 0x06000A3C RID: 2620 RVA: 0x0003EB64 File Offset: 0x0003CD64
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(TableSaltConfig.ID, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.DESC, 1f, false, Assets.GetAnim("seed_saltPlant_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, SORTORDER.BUILDINGELEMENTS + TableSaltTuning.SORTORDER, SimHashes.Salt, new List<Tag>
		{
			GameTags.Other,
			GameTags.Experimental
		});
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000A3D RID: 2621 RVA: 0x0003EBE9 File Offset: 0x0003CDE9
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000A3E RID: 2622 RVA: 0x0003EBEB File Offset: 0x0003CDEB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000722 RID: 1826
	public static string ID = "TableSalt";
}
