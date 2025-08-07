using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000253 RID: 595
public class CrabShellConfig : IEntityConfig
{
	// Token: 0x06000C09 RID: 3081 RVA: 0x00049220 File Offset: 0x00047420
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("CrabShell", ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME, ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.DESC, 1f, false, Assets.GetAnim("crabshell_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.Organics,
			GameTags.MoltShell
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		gameObject.AddComponent<EntitySizeVisualizer>().TierSetType = OreSizeVisualizerComponents.TiersSetType.PokeShells;
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x000492B5 File Offset: 0x000474B5
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<Compostable>().OnDeserializeCb = delegate(KMonoBehaviour inst)
		{
			if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 36))
			{
				inst.GetComponent<PrimaryElement>();
				PrimaryElement component = inst.GetComponent<PrimaryElement>();
				if (component != null)
				{
					component.MassPerUnit = 1f;
					component.Mass = component.Units * 10f;
				}
				KPrefabID component2 = inst.GetComponent<KPrefabID>();
				if (component2 != null)
				{
					component2.RemoveTag(GameTags.IndustrialIngredient);
				}
			}
		};
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x000492E1 File Offset: 0x000474E1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000853 RID: 2131
	public const string ID = "CrabShell";

	// Token: 0x04000854 RID: 2132
	public static readonly Tag TAG = TagManager.Create("CrabShell");

	// Token: 0x04000855 RID: 2133
	public const float ADULT_MASS = 10f;
}
