using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class MilkFeederConfig : IBuildingConfig
{
	// Token: 0x06000EB9 RID: 3769 RVA: 0x0005644C File Offset: 0x0005464C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MilkFeeder";
		int num = 3;
		int num2 = 3;
		string text2 = "critter_milk_feeder_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		return buildingDef;
	}

	// Token: 0x06000EBA RID: 3770 RVA: 0x000564EA File Offset: 0x000546EA
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000EBB RID: 3771 RVA: 0x000564EC File Offset: 0x000546EC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		go.AddOrGet<LogicOperationalController>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.CreaturePen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 80f;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.allowItemRemoval = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = GameTagExtensions.Create(SimHashes.Milk);
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.storage = storage;
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RanchStationType, false);
	}

	// Token: 0x06000EBC RID: 3772 RVA: 0x000565B5 File Offset: 0x000547B5
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<MilkFeeder.Def>();
	}

	// Token: 0x06000EBD RID: 3773 RVA: 0x000565BE File Offset: 0x000547BE
	public override void ConfigurePost(BuildingDef def)
	{
	}

	// Token: 0x04000998 RID: 2456
	public const string ID = "MilkFeeder";

	// Token: 0x04000999 RID: 2457
	public const string HAD_CONSUMED_MILK_RECENTLY_EFFECT_ID = "HadMilk";

	// Token: 0x0400099A RID: 2458
	public const float EFFECT_DURATION_IN_SECONDS = 600f;

	// Token: 0x0400099B RID: 2459
	public static readonly CellOffset DRINK_FROM_OFFSET = new CellOffset(1, 0);

	// Token: 0x0400099C RID: 2460
	public static readonly Tag MILK_TAG = SimHashes.Milk.CreateTag();

	// Token: 0x0400099D RID: 2461
	public const float UNITS_OF_MILK_CONSUMED_PER_FEEDING = 5f;
}
