using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F8 RID: 760
public class IridiumCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F94 RID: 3988 RVA: 0x0005E86C File Offset: 0x0005CA6C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000F95 RID: 3989 RVA: 0x0005E873 File Offset: 0x0005CA73
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x0005E878 File Offset: 0x0005CA78
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(IridiumCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.IRIDIUMCOMET.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(10f, 100f);
		comet.temperatureRange = new Vector2(473.15f, 548.15f);
		comet.explosionTemperatureRange = comet.temperatureRange;
		comet.explosionOreCount = new Vector2I(2, 4);
		comet.impactSound = "Meteor_copper_Impact";
		comet.flyingSoundID = 1;
		comet.EXHAUST_ELEMENT = SimHashes.CarbonDioxide;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactMetal;
		comet.entityDamage = 15;
		comet.totalTileDamage = 0.5f;
		comet.splashRadius = 1;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Iridium, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("meteor_iridium_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000F97 RID: 3991 RVA: 0x0005E9E9 File Offset: 0x0005CBE9
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F98 RID: 3992 RVA: 0x0005E9EB File Offset: 0x0005CBEB
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x04000A07 RID: 2567
	public static string ID = "IridiumComet";
}
