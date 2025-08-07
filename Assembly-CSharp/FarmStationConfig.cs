using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000094 RID: 148
public class FarmStationConfig : IBuildingConfig
{
	// Token: 0x060002E8 RID: 744 RVA: 0x0001536C File Offset: 0x0001356C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FarmStation";
		int num = 2;
		int num2 = 3;
		string text2 = "planttender_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanFarmStation.Id;
		buildingDef.AddSearchTerms(SEARCH_TERMS.FARM);
		return buildingDef;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x00015416 File Offset: 0x00013616
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.FarmStationType, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.CodexCategories.FarmBuilding, false);
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00015444 File Offset: 0x00013644
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		Storage storage = go.AddOrGet<Storage>();
		storage.showInUI = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = FarmStationConfig.MATERIAL_FOR_TINKER;
		manualDeliveryKG.refillMass = 5f;
		manualDeliveryKG.capacity = 50f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FarmFetch.IdHash;
		TinkerStation tinkerStation = go.AddOrGet<TinkerStation>();
		tinkerStation.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_planttender_kanim") };
		tinkerStation.inputMaterial = FarmStationConfig.MATERIAL_FOR_TINKER;
		tinkerStation.massPerTinker = 5f;
		tinkerStation.outputPrefab = FarmStationConfig.TINKER_TOOLS;
		tinkerStation.requiredSkillPerk = Db.Get().SkillPerks.CanFarmTinker.Id;
		tinkerStation.choreType = Db.Get().ChoreTypes.FarmingFabricate.IdHash;
		tinkerStation.fetchChoreType = Db.Get().ChoreTypes.FarmFetch.IdHash;
		tinkerStation.EffectTitle = UI.BUILDINGEFFECTS.IMPROVED_PLANTS;
		tinkerStation.EffectTooltip = UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_PLANTS;
		tinkerStation.EffectItemString = UI.BUILDINGEFFECTS.IMPROVED_PLANTS_ITEM;
		tinkerStation.EffectItemTooltip = UI.BUILDINGEFFECTS.TOOLTIPS.IMPROVED_PLANTS_ITEM;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.Farm.Id;
		roomTracker.requirement = RoomTracker.Requirement.Required;
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			TinkerStation component = game_object.GetComponent<TinkerStation>();
			component.AttributeConverter = Db.Get().AttributeConverters.HarvestSpeed;
			component.AttributeExperienceMultiplier = DUPLICANTSTATS.ATTRIBUTE_LEVELING.MOST_DAY_EXPERIENCE;
			component.SkillExperienceSkillGroup = Db.Get().SkillGroups.Farming.Id;
			component.SkillExperienceMultiplier = SKILLS.MOST_DAY_EXPERIENCE;
			component.toolProductionTime = 15f;
		};
	}

	// Token: 0x040001B0 RID: 432
	public const string ID = "FarmStation";

	// Token: 0x040001B1 RID: 433
	public static Tag MATERIAL_FOR_TINKER = GameTags.Fertilizer;

	// Token: 0x040001B2 RID: 434
	public static Tag TINKER_TOOLS = FarmStationToolsConfig.tag;

	// Token: 0x040001B3 RID: 435
	public const float MASS_PER_TINKER = 5f;
}
