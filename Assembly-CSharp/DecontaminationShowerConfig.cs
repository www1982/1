using System;
using TUNING;
using UnityEngine;

// Token: 0x0200006C RID: 108
public class DecontaminationShowerConfig : IBuildingConfig
{
	// Token: 0x06000205 RID: 517 RVA: 0x0000E840 File Offset: 0x0000CA40
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x0000E848 File Offset: 0x0000CA48
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DecontaminationShower";
		int num = 2;
		int num2 = 4;
		string text2 = "decontamination_shower_kanim";
		int num3 = 250;
		float num4 = 120f;
		string[] radiation_CONTAINMENT = MATERIALS.RADIATION_CONTAINMENT;
		float[] array = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0]
		};
		string[] array2 = radiation_CONTAINMENT;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, tier, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(1, 2);
		return buildingDef;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KBatchedAnimController kbatchedAnimController = go.AddOrGet<KBatchedAnimController>();
		kbatchedAnimController.sceneLayer = Grid.SceneLayer.BuildingBack;
		kbatchedAnimController.fgLayer = Grid.SceneLayer.BuildingFront;
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 100f;
		handSanitizer.consumedElement = SimHashes.Water;
		handSanitizer.outputElement = SimHashes.DirtyWater;
		handSanitizer.diseaseRemovalCount = 1000000;
		handSanitizer.maxUses = 1;
		handSanitizer.canSanitizeSuit = true;
		handSanitizer.canSanitizeStorage = true;
		go.AddOrGet<DirectionControl>();
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_decontamination_shower_kanim") };
		work.workLayer = Grid.SceneLayer.BuildingUse;
		work.workTime = 15f;
		work.trackUses = true;
		work.removeIrritation = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 100f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Store;
		AutoStorageDropper.Def def = go.AddOrGetDef<AutoStorageDropper.Def>();
		def.elementFilter = new SimHashes[] { SimHashes.DirtyWater };
		def.dropOffset = new CellOffset(1, 0);
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400014B RID: 331
	public const string ID = "DecontaminationShower";

	// Token: 0x0400014C RID: 332
	private const float MASS_PER_USE = 100f;

	// Token: 0x0400014D RID: 333
	private const int DISEASE_REMOVAL_COUNT = 1000000;

	// Token: 0x0400014E RID: 334
	private const float WATER_PER_USE = 100f;

	// Token: 0x0400014F RID: 335
	private const int USES_PER_FLUSH = 1;

	// Token: 0x04000150 RID: 336
	private const float WORK_TIME = 15f;

	// Token: 0x04000151 RID: 337
	private const SimHashes CONSUMED_ELEMENT = SimHashes.Water;

	// Token: 0x04000152 RID: 338
	private const SimHashes PRODUCED_ELEMENT = SimHashes.DirtyWater;
}
