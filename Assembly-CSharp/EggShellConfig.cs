using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000256 RID: 598
public class EggShellConfig : IEntityConfig
{
	// Token: 0x06000C18 RID: 3096 RVA: 0x00049478 File Offset: 0x00047678
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("EggShell", ITEMS.INDUSTRIAL_PRODUCTS.EGG_SHELL.NAME, ITEMS.INDUSTRIAL_PRODUCTS.EGG_SHELL.DESC, 1f, false, Assets.GetAnim("eggshells_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Organics, false);
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<SimpleMassStatusItem>();
		EntityTemplates.CreateAndRegisterCompostableFromPrefab(gameObject);
		return gameObject;
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x000494F8 File Offset: 0x000476F8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x000494FA File Offset: 0x000476FA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000860 RID: 2144
	public const string ID = "EggShell";

	// Token: 0x04000861 RID: 2145
	public static readonly Tag TAG = TagManager.Create("EggShell");

	// Token: 0x04000862 RID: 2146
	public const float EGG_TO_SHELL_RATIO = 0.5f;
}
