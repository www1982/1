using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class ClusterMapLongRangeMissileConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000148 RID: 328 RVA: 0x0000A1DF File Offset: 0x000083DF
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x0000A1E6 File Offset: 0x000083E6
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0000A1EC File Offset: 0x000083EC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("ClusterMapLongRangeMissile", ITEMS.MISSILE_LONGRANGE.NAME, ITEMS.MISSILE_LONGRANGE.DESC, 2000f, true, Assets.GetAnim("longrange_missile_clustermap_kanim"), "object", Grid.SceneLayer.Front, SimHashes.Creature, new List<Tag>
		{
			GameTags.IgnoreMaterialCategory,
			GameTags.Experimental
		}, 293f);
		gameObject.AddOrGetDef<ClusterMapLongRangeMissile.Def>();
		gameObject.AddComponent<LoopingSounds>();
		gameObject.AddOrGet<KSelectable>().IsSelectable = true;
		ClusterMapLongRangeMissileGridEntity clusterMapLongRangeMissileGridEntity = gameObject.AddOrGet<ClusterMapLongRangeMissileGridEntity>();
		clusterMapLongRangeMissileGridEntity.clusterAnimName = "longrange_missile_clustermap_kanim";
		clusterMapLongRangeMissileGridEntity.isWorldEntity = false;
		clusterMapLongRangeMissileGridEntity.nameKey = new StringKey("STRINGS.ITEMS.MISSILE_LONGRANGE.NAME");
		clusterMapLongRangeMissileGridEntity.keepRotationWhenSpacingOutInHex = true;
		ClusterDestinationSelector clusterDestinationSelector = gameObject.AddOrGet<ClusterDestinationSelector>();
		clusterDestinationSelector.canNavigateFogOfWar = false;
		clusterDestinationSelector.dodgesHiddenAsteroids = true;
		clusterDestinationSelector.requireAsteroidDestination = false;
		clusterDestinationSelector.requireLaunchPadOnAsteroidDestination = false;
		clusterDestinationSelector.assignable = false;
		clusterDestinationSelector.shouldPointTowardsPath = true;
		gameObject.AddOrGet<ClusterTraveler>();
		return gameObject;
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0000A2D6 File Offset: 0x000084D6
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600014C RID: 332 RVA: 0x0000A2D8 File Offset: 0x000084D8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040000CE RID: 206
	public const string ID = "ClusterMapLongRangeMissile";

	// Token: 0x040000CF RID: 207
	public const float MASS = 2000f;

	// Token: 0x040000D0 RID: 208
	public const float STARMAP_SPEED = 10f;
}
