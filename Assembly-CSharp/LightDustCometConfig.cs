using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002FA RID: 762
public class LightDustCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000FA2 RID: 4002 RVA: 0x0005EB9B File Offset: 0x0005CD9B
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000FA3 RID: 4003 RVA: 0x0005EBA2 File Offset: 0x0005CDA2
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000FA4 RID: 4004 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(LightDustCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.LIGHTDUSTCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(10f, 14f);
		comet.temperatureRange = new Vector2(223.15f, 253.15f);
		comet.explosionTemperatureRange = comet.temperatureRange;
		comet.explosionOreCount = new Vector2I(1, 2);
		comet.explosionSpeedRange = new Vector2(4f, 7f);
		comet.entityDamage = 0;
		comet.totalTileDamage = 0f;
		comet.splashRadius = 0;
		comet.impactSound = "Meteor_dust_light_Impact";
		comet.flyingSoundID = 0;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactLightDust;
		comet.EXHAUST_ELEMENT = SimHashes.Void;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Regolith, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_dust_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x0005ED2D File Offset: 0x0005CF2D
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x0005ED2F File Offset: 0x0005CF2F
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A0B RID: 2571
	public static string ID = "LightDustComet";
}
