using System;
using UnityEngine;

// Token: 0x020002E1 RID: 737
public class BackgroundEarthConfig : IEntityConfig
{
	// Token: 0x06000EF1 RID: 3825 RVA: 0x00058814 File Offset: 0x00056A14
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(BackgroundEarthConfig.ID, BackgroundEarthConfig.ID, true);
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("earth_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "idle";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<LoopingSounds>();
		return gameObject;
	}

	// Token: 0x06000EF2 RID: 3826 RVA: 0x0005887F File Offset: 0x00056A7F
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000EF3 RID: 3827 RVA: 0x00058881 File Offset: 0x00056A81
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040009C3 RID: 2499
	public static string ID = "BackgroundEarth";
}
