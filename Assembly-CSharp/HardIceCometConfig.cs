using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F9 RID: 761
public class HardIceCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F9B RID: 3995 RVA: 0x0005EA01 File Offset: 0x0005CC01
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F9C RID: 3996 RVA: 0x0005EA08 File Offset: 0x0005CC08
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F9D RID: 3997 RVA: 0x0005EA0C File Offset: 0x0005CC0C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(HardIceCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.HARDICECOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		float mass = ElementLoader.FindElementByHash(SimHashes.CrushedIce).defaultValues.mass;
		comet.massRange = new Vector2(mass * 0.8f * 6f, mass * 1.2f * 6f);
		comet.temperatureRange = new Vector2(173.15f, 248.15f);
		comet.explosionTemperatureRange = comet.temperatureRange;
		comet.addTiles = 6;
		comet.addTilesMinHeight = 2;
		comet.addTilesMaxHeight = 8;
		comet.entityDamage = 0;
		comet.totalTileDamage = 0f;
		comet.splashRadius = 1;
		comet.impactSound = "Meteor_ice_Impact";
		comet.flyingSoundID = 6;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactIce;
		comet.EXHAUST_ELEMENT = SimHashes.Oxygen;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.CrushedIce, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_ice_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000F9E RID: 3998 RVA: 0x0005EB83 File Offset: 0x0005CD83
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F9F RID: 3999 RVA: 0x0005EB85 File Offset: 0x0005CD85
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A08 RID: 2568
	public static readonly string ID = "HardIceComet";

	// Token: 0x04000A09 RID: 2569
	private const SimHashes element = SimHashes.CrushedIce;

	// Token: 0x04000A0A RID: 2570
	private const int ADDED_CELLS = 6;
}
