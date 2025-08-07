using System;
using STRINGS;
using UnityEngine;

// Token: 0x020002F0 RID: 752
public class NuclearWasteCometConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F62 RID: 3938 RVA: 0x0005DCB4 File Offset: 0x0005BEB4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000F63 RID: 3939 RVA: 0x0005DCBB File Offset: 0x0005BEBB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F64 RID: 3940 RVA: 0x0005DCC0 File Offset: 0x0005BEC0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateEntity(NuclearWasteCometConfig.ID, UI.SPACEDESTINATIONS.COMETS.NUCLEAR_WASTE.NAME, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<LoopingSounds>();
		Comet comet = gameObject.AddOrGet<Comet>();
		comet.massRange = new Vector2(NuclearWasteCometConfig.MASS, NuclearWasteCometConfig.MASS);
		comet.EXHAUST_ELEMENT = SimHashes.Fallout;
		comet.EXHAUST_RATE = NuclearWasteCometConfig.MASS * 0.2f;
		comet.temperatureRange = new Vector2(473.15f, 573.15f);
		comet.entityDamage = 2;
		comet.totalTileDamage = 0.45f;
		comet.splashRadius = 0;
		comet.impactSound = "Meteor_Nuclear_Impact";
		comet.flyingSoundID = 3;
		comet.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		comet.addTiles = 1;
		comet.diseaseIdx = Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id);
		comet.addDiseaseCount = 1000000;
		comet.affectedByDifficulty = false;
		PrimaryElement primaryElement = gameObject.AddOrGet<PrimaryElement>();
		primaryElement.SetElement(SimHashes.Corium, true);
		primaryElement.Temperature = (comet.temperatureRange.x + comet.temperatureRange.y) / 2f;
		KBatchedAnimController kbatchedAnimController = gameObject.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim("nuclear_metldown_comet_fx_kanim") };
		kbatchedAnimController.isMovable = true;
		kbatchedAnimController.initialAnim = "fall_loop";
		kbatchedAnimController.initialMode = KAnim.PlayMode.Loop;
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		gameObject.AddOrGet<KCircleCollider2D>().radius = 0.5f;
		gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1f);
		gameObject.AddTag(GameTags.Comet);
		return gameObject;
	}

	// Token: 0x06000F65 RID: 3941 RVA: 0x0005DE6F File Offset: 0x0005C06F
	public void OnPrefabInit(GameObject go)
	{
	}

	// Token: 0x06000F66 RID: 3942 RVA: 0x0005DE71 File Offset: 0x0005C071
	public void OnSpawn(GameObject go)
	{
	}

	// Token: 0x040009FA RID: 2554
	public static string ID = "NuclearWasteComet";

	// Token: 0x040009FB RID: 2555
	public static float MASS = 1f;
}
