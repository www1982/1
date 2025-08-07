using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F7 RID: 759
public class SpaceTreeSeedCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F8D RID: 3981 RVA: 0x0005E6C1 File Offset: 0x0005C8C1
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000F8E RID: 3982 RVA: 0x0005E6C8 File Offset: 0x0005C8C8
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F8F RID: 3983 RVA: 0x0005E6CC File Offset: 0x0005C8CC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(SpaceTreeSeedCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.SPACETREESEEDCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		SpaceTreeSeededComet spaceTreeSeededComet = gameObject.AddOrGet<SpaceTreeSeededComet>();
		spaceTreeSeededComet.massRange = new Vector2(50f, 100f);
		spaceTreeSeededComet.temperatureRange = new Vector2(253.15f, 263.15f);
		spaceTreeSeededComet.explosionTemperatureRange = spaceTreeSeededComet.temperatureRange;
		spaceTreeSeededComet.impactSound = "Meteor_copper_Impact";
		spaceTreeSeededComet.flyingSoundID = 5;
		spaceTreeSeededComet.EXHAUST_ELEMENT = SimHashes.Void;
		spaceTreeSeededComet.explosionEffectHash = SpawnFXHashes.None;
		spaceTreeSeededComet.entityDamage = 0;
		spaceTreeSeededComet.totalTileDamage = 0f;
		spaceTreeSeededComet.splashRadius = 1;
		spaceTreeSeededComet.addTiles = 3;
		spaceTreeSeededComet.addTilesMinHeight = 1;
		spaceTreeSeededComet.addTilesMaxHeight = 2;
		spaceTreeSeededComet.lootOnDestroyedByMissile = new string[] { "SpaceTreeSeed" };
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Snow, true);
		primaryElement.Temperature = (spaceTreeSeededComet.temperatureRange.x + spaceTreeSeededComet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_bonbon_snow_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000F90 RID: 3984 RVA: 0x0005E854 File Offset: 0x0005CA54
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F91 RID: 3985 RVA: 0x0005E856 File Offset: 0x0005CA56
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A06 RID: 2566
	public static string ID = "SpaceTreeSeedComet";
}
