using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000320 RID: 800
public class MissileLongRangeConfig : IEntityConfig
{
	// Token: 0x06001080 RID: 4224 RVA: 0x0006276C File Offset: 0x0006096C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("MissileLongRange", ITEMS.MISSILE_LONGRANGE.NAME, ITEMS.MISSILE_LONGRANGE.DESC, 200f, true, Assets.GetAnim("longrange_missile_kanim"), "object", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 1f, true, 0, SimHashes.Iron, new List<Tag>());
		gameObject.AddTag(GameTags.IndustrialProduct);
		gameObject.AddOrGetDef<MissileLongRangeProjectile.Def>();
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = 200f;
		return gameObject;
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000627EC File Offset: 0x000609EC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000627EE File Offset: 0x000609EE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A7F RID: 2687
	public const string ID = "MissileLongRange";

	// Token: 0x04000A80 RID: 2688
	public const float MASS_PER_MISSILE = 200f;

	// Token: 0x04000A81 RID: 2689
	public const int DAMAGE_PER_MISSILE = 10;

	// Token: 0x020011D8 RID: 4568
	public class DamageEventPayload
	{
		// Token: 0x0600841D RID: 33821 RVA: 0x00334E90 File Offset: 0x00333090
		public DamageEventPayload(int damage = 10)
		{
			this.damage = damage;
		}

		// Token: 0x04006455 RID: 25685
		public int damage;

		// Token: 0x04006456 RID: 25686
		public static MissileLongRangeConfig.DamageEventPayload sharedInstance = new MissileLongRangeConfig.DamageEventPayload(10);
	}
}
