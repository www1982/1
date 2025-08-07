using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000259 RID: 601
public class OrbitalResearchDatabankConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000C27 RID: 3111 RVA: 0x0004961F File Offset: 0x0004781F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00049626 File Offset: 0x00047826
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x0004962C File Offset: 0x0004782C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("OrbitalResearchDatabank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.ORBITAL_RESEARCH_DATABANK.DESC, 1f, true, Assets.GetAnim("floppy_disc_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Experimental
		});
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = (float)ROCKETRY.DESTINATION_RESEARCH.BASIC;
		return gameObject;
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x000496B1 File Offset: 0x000478B1
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x000496B4 File Offset: 0x000478B4
	public void OnSpawn(GameObject inst)
	{
		if (Game.IsDlcActiveForCurrentSave("DLC2_ID") && SaveLoader.Instance.ClusterLayout != null && SaveLoader.Instance.ClusterLayout.clusterTags.Contains("CeresCluster"))
		{
			inst.AddOrGet<KBatchedAnimController>().SwapAnims(new KAnimFile[] { Assets.GetAnim("floppy_disc_ceres_kanim") });
		}
	}

	// Token: 0x04000869 RID: 2153
	public const string ID = "OrbitalResearchDatabank";

	// Token: 0x0400086A RID: 2154
	public static readonly Tag TAG = TagManager.Create("OrbitalResearchDatabank");

	// Token: 0x0400086B RID: 2155
	public const float MASS = 1f;
}
