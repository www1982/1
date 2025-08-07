using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000319 RID: 793
public class MiniCometConfig : IEntityConfig
{
	// Token: 0x06001058 RID: 4184 RVA: 0x00061880 File Offset: 0x0005FA80
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(MiniCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.MINICOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		MiniComet miniComet = gameObject.AddOrGet<MiniComet>();
		Sim.PhysicsData defaultValues = ElementLoader.FindElementByHash(SimHashes.Regolith).defaultValues;
		miniComet.impactSound = "MeteorDamage_Rock";
		miniComet.flyingSoundID = 2;
		miniComet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		gameObject.AddOrGet<PrimaryElement>();
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_sand_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.AddTag(GameTags.Comet);
		gameObject.AddTag(GameTags.HideFromSpawnTool);
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		return gameObject;
	}

	// Token: 0x06001059 RID: 4185 RVA: 0x0006196E File Offset: 0x0005FB6E
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x0600105A RID: 4186 RVA: 0x00061970 File Offset: 0x0005FB70
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A71 RID: 2673
	public static readonly string ID = "MiniComet";

	// Token: 0x04000A72 RID: 2674
	private const SimHashes element = SimHashes.Regolith;

	// Token: 0x04000A73 RID: 2675
	private const int ADDED_CELLS = 6;
}
