using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000254 RID: 596
public class CrabWoodShellConfig : IEntityConfig
{
	// Token: 0x06000C0E RID: 3086 RVA: 0x000492FC File Offset: 0x000474FC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("CrabWoodShell", ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.NAME, ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.DESC, 1f, false, Assets.GetAnim("woodcrabshell_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.Organics,
			GameTags.MoltShell
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		gameObject.AddComponent<EntitySizeVisualizer>().TierSetType = OreSizeVisualizerComponents.TiersSetType.WoodPokeShells;
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x06000C0F RID: 3087 RVA: 0x00049391 File Offset: 0x00047591
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
					component.Mass = component.Units * 100f;
				}
				KPrefabID component2 = inst.GetComponent<KPrefabID>();
				if (component2 != null)
				{
					component2.RemoveTag(GameTags.IndustrialIngredient);
				}
			}
		};
	}

	// Token: 0x06000C10 RID: 3088 RVA: 0x000493BD File Offset: 0x000475BD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000856 RID: 2134
	public const string ID = "CrabWoodShell";

	// Token: 0x04000857 RID: 2135
	public static readonly Tag TAG = TagManager.Create("CrabWoodShell");

	// Token: 0x04000858 RID: 2136
	public const float ADULT_MASS = 100f;

	// Token: 0x04000859 RID: 2137
	public const string symbolPrefix = "wood_";
}
