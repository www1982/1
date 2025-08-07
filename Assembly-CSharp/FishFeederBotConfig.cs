using System;
using UnityEngine;

// Token: 0x0200016F RID: 367
public class FishFeederBotConfig : IEntityConfig
{
	// Token: 0x06000705 RID: 1797 RVA: 0x00030FC4 File Offset: 0x0002F1C4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity("FishFeederBot", "FishFeederBot", true);
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("fishfeeder_kanim") };
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		SymbolOverrideControllerUtil.AddToPrefab(kbatchedAnimController.gameObject);
		return gameObject;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0003101C File Offset: 0x0002F21C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0003101E File Offset: 0x0002F21E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400054F RID: 1359
	public const string ID = "FishFeederBot";
}
