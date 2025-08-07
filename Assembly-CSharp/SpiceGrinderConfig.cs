using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class SpiceGrinderConfig : IBuildingConfig
{
	// Token: 0x06001557 RID: 5463 RVA: 0x00079828 File Offset: 0x00077A28
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpiceGrinder";
		int num = 2;
		int num2 = 3;
		string text2 = "spice_grinder_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanSpiceGrinder.Id;
		return buildingDef;
	}

	// Token: 0x06001558 RID: 5464 RVA: 0x000798C2 File Offset: 0x00077AC2
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.SpiceStation, false);
	}

	// Token: 0x06001559 RID: 5465 RVA: 0x000798DC File Offset: 0x00077ADC
	public override void DoPostConfigureComplete(GameObject go)
	{
		SpiceGrinder.InitializeSpices();
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGetDef<SpiceGrinder.Def>();
		go.AddOrGet<SpiceGrinderWorkable>();
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGet<TreeFilterable>().uiHeight = TreeFilterable.UISideScreenHeight.Short;
		go.AddOrGet<Prioritizable>().SetMasterPriority(new PrioritySetting(PriorityScreen.PriorityClass.basic, SpiceGrinderConfig.STORAGE_PRIORITY));
		Storage storage = go.AddComponent<Storage>();
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.storageFilters = new List<Tag> { GameTags.Edible };
		storage.allowItemRemoval = false;
		storage.capacityKg = 1f;
		storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage.fetchCategory = Storage.FetchCategory.Building;
		storage.showCapacityStatusItem = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showSideScreenTitleBar = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		Storage storage2 = go.AddComponent<Storage>();
		storage2.showInUI = true;
		storage2.showDescriptor = true;
		storage2.storageFilters = new List<Tag> { GameTags.Seed };
		storage2.allowItemRemoval = false;
		storage2.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
		storage2.fetchCategory = Storage.FetchCategory.Building;
		storage2.showCapacityStatusItem = true;
		storage2.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Kitchen.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
	}

	// Token: 0x04000CA4 RID: 3236
	public const string ID = "SpiceGrinder";

	// Token: 0x04000CA5 RID: 3237
	public static Tag MATERIAL_FOR_TINKER = GameTags.CropSeed;

	// Token: 0x04000CA6 RID: 3238
	public static Tag TINKER_TOOLS = FarmStationToolsConfig.tag;

	// Token: 0x04000CA7 RID: 3239
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x04000CA8 RID: 3240
	public const float OUTPUT_TEMPERATURE = 313.15f;

	// Token: 0x04000CA9 RID: 3241
	public const float WORK_TIME_PER_1000KCAL = 5f;

	// Token: 0x04000CAA RID: 3242
	public const short SPICE_CAPACITY_PER_INGREDIENT = 10;

	// Token: 0x04000CAB RID: 3243
	public const string PrimaryColorSymbol = "stripe_anim2";

	// Token: 0x04000CAC RID: 3244
	public const string SecondaryColorSymbol = "stripe_anim1";

	// Token: 0x04000CAD RID: 3245
	public const string GrinderColorSymbol = "grinder";

	// Token: 0x04000CAE RID: 3246
	public static StatusItem SpicedStatus = Db.Get().MiscStatusItems.SpicedFood;

	// Token: 0x04000CAF RID: 3247
	private static int STORAGE_PRIORITY = Chore.DefaultPrioritySetting.priority_value - 1;
}
