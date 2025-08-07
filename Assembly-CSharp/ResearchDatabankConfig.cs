using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class ResearchDatabankConfig : IEntityConfig
{
	// Token: 0x06000C3C RID: 3132 RVA: 0x00049898 File Offset: 0x00047A98
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("ResearchDatabank", global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.RESEARCH_DATABANK.DESC, 1f, true, Assets.GetAnim("floppy_disc_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.Experimental
		});
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			gameObject.AddTag(GameTags.HideFromSpawnTool);
		}
		gameObject.AddOrGet<EntitySplitter>().maxStackSize = (float)ROCKETRY.DESTINATION_RESEARCH.BASIC;
		return gameObject;
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x00049931 File Offset: 0x00047B31
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00049934 File Offset: 0x00047B34
	public void OnSpawn(GameObject inst)
	{
		if (Game.IsDlcActiveForCurrentSave("DLC2_ID") && SaveLoader.Instance.ClusterLayout != null && SaveLoader.Instance.ClusterLayout.clusterTags.Contains("CeresCluster"))
		{
			inst.AddOrGet<KBatchedAnimController>().SwapAnims(new KAnimFile[] { Assets.GetAnim("floppy_disc_ceres_kanim") });
		}
	}

	// Token: 0x04000870 RID: 2160
	public const string ID = "ResearchDatabank";

	// Token: 0x04000871 RID: 2161
	public static readonly Tag TAG = TagManager.Create("ResearchDatabank");

	// Token: 0x04000872 RID: 2162
	public const float MASS = 1f;
}
