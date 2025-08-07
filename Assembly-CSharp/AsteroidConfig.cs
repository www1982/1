using System;
using UnityEngine;

// Token: 0x020002E0 RID: 736
public class AsteroidConfig : IEntityConfig
{
	// Token: 0x06000EED RID: 3821 RVA: 0x000587B8 File Offset: 0x000569B8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("Asteroid", "Asteroid", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<WorldInventory>();
		gameObject.AddOrGet<WorldContainer>();
		gameObject.AddOrGet<AsteroidGridEntity>();
		gameObject.AddOrGet<OrbitalMechanics>();
		gameObject.AddOrGetDef<GameplaySeasonManager.Def>();
		gameObject.AddOrGetDef<AlertStateManager.Def>();
		return gameObject;
	}

	// Token: 0x06000EEE RID: 3822 RVA: 0x00058806 File Offset: 0x00056A06
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000EEF RID: 3823 RVA: 0x00058808 File Offset: 0x00056A08
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040009C2 RID: 2498
	public const string ID = "Asteroid";
}
