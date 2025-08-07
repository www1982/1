using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200031E RID: 798
public class MissileBasicConfig : IEntityConfig
{
	// Token: 0x06001074 RID: 4212 RVA: 0x0006220C File Offset: 0x0006040C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("MissileBasic", ITEMS.MISSILE_BASIC.NAME, ITEMS.MISSILE_BASIC.DESC, 10f, true, Assets.GetAnim("missile_kanim"), "object", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Iron, new List<Tag>());
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGetDef<MissileProjectile.Def>();
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = 50f;
		return gameObject;
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x0006228C File Offset: 0x0006048C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x0006228E File Offset: 0x0006048E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A7B RID: 2683
	public const string ID = "MissileBasic";

	// Token: 0x04000A7C RID: 2684
	public const float MASS_PER_MISSILE = 10f;
}
