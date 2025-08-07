using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002E7 RID: 743
public class CarePackageConfig : IEntityConfig
{
	// Token: 0x06000F36 RID: 3894 RVA: 0x0005D2A8 File Offset: 0x0005B4A8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity(CarePackageConfig.ID, ITEMS.CARGO_CAPSULE.NAME, ITEMS.CARGO_CAPSULE.DESC, 1f, true, Assets.GetAnim("portal_carepackage_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, false, 0, SimHashes.Creature, null);
	}

	// Token: 0x06000F37 RID: 3895 RVA: 0x0005D302 File Offset: 0x0005B502
	public void OnPrefabInit(GameObject go)
	{
		go.AddOrGet<CarePackage>();
	}

	// Token: 0x06000F38 RID: 3896 RVA: 0x0005D30B File Offset: 0x0005B50B
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040009F0 RID: 2544
	public static readonly string ID = "CarePackage";
}
