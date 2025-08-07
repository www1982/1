using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000258 RID: 600
public class MachinePartsConfig : IEntityConfig
{
	// Token: 0x06000C22 RID: 3106 RVA: 0x000495A8 File Offset: 0x000477A8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("MachineParts", ITEMS.INDUSTRIAL_PRODUCTS.MACHINE_PARTS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.MACHINE_PARTS.DESC, 5f, true, Assets.GetAnim("buildingrelocate_kanim"), "idle", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, null);
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x00049602 File Offset: 0x00047802
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x00049604 File Offset: 0x00047804
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000866 RID: 2150
	public const string ID = "MachineParts";

	// Token: 0x04000867 RID: 2151
	public static readonly Tag TAG = TagManager.Create("MachineParts");

	// Token: 0x04000868 RID: 2152
	public const float MASS = 5f;
}
