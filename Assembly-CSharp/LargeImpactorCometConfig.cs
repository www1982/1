using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000311 RID: 785
public class LargeImpactorCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600102B RID: 4139 RVA: 0x000610DA File Offset: 0x0005F2DA
	public string[] GetRequiredDlcIds()
	{
		return new string[] { "DLC4_ID" };
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x000610EA File Offset: 0x0005F2EA
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x000610F0 File Offset: 0x0005F2F0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(LargeImpactorCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.ROCKCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		LargeComet largeComet = gameObject.AddOrGet<LargeComet>();
		largeComet.impactSound = "Meteor_Large_Impact";
		largeComet.flyingSoundID = 2;
		largeComet.additionalAnimFiles.Add(new KeyValuePair<string, string>("asteroid_wind_kanim", "wind_loop"));
		largeComet.additionalAnimFiles.Add(new KeyValuePair<string, string>("asteroid_flame_inner_kanim", "flame_loop"));
		largeComet.mainAnimFile = new KeyValuePair<string, string>("asteroid_001_kanim", "idle");
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Regolith, true);
		primaryElement.Temperature = 20000f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("asteroid_flame_outer_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "flame_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.animScale = 0.2f;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x0006120C File Offset: 0x0005F40C
	public void OnPrefabInit(GameObject go)
	{
		LargeComet largeComet = go.AddOrGet<LargeComet>();
		largeComet.additionalAnimFiles.Add(new KeyValuePair<string, string>("asteroid_wind_kanim", "wind_loop"));
		largeComet.additionalAnimFiles.Add(new KeyValuePair<string, string>("asteroid_flame_inner_kanim", "flame_loop"));
		largeComet.mainAnimFile = new KeyValuePair<string, string>("asteroid_001_kanim", "idle");
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x00061267 File Offset: 0x0005F467
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A4D RID: 2637
	public static readonly string ID = "LargeImpactorComet";

	// Token: 0x04000A4E RID: 2638
	private const SimHashes element = SimHashes.Regolith;

	// Token: 0x04000A4F RID: 2639
	private const int ADDED_CELLS = 6;
}
