using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200034B RID: 843
public class OilChangerConfig : IBuildingConfig
{
	// Token: 0x0600116A RID: 4458 RVA: 0x00065E39 File Offset: 0x00064039
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x00065E40 File Offset: 0x00064040
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OilChanger";
		int num = 3;
		int num2 = 3;
		string text2 = "oilchange_station_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.Overheatable = false;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.PowerInputOffset = new CellOffset(1, 0);
		buildingDef.UtilityInputOffset = new CellOffset(1, 2);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.PermittedRotations = PermittedRotations.Unrotatable;
		buildingDef.AddSearchTerms(SEARCH_TERMS.BIONIC);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MEDICINE);
		return buildingDef;
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x00065F14 File Offset: 0x00064114
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.BionicUpkeepType, false);
		go.GetComponent<KPrefabID>().AddTag(GameTags.CodexCategories.BionicBuilding, false);
		Storage storage = go.AddComponent<Storage>();
		storage.capacityKg = this.OIL_CAPACITY;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		OilChangerWorkableUse oilChangerWorkableUse = go.AddOrGet<OilChangerWorkableUse>();
		oilChangerWorkableUse.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_oilchange_kanim") };
		oilChangerWorkableUse.resetProgressOnStop = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = GameTags.LubricatingOil;
		conduitConsumer.capacityKG = this.OIL_CAPACITY;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		go.AddOrGetDef<OilChanger.Def>();
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x00065FC3 File Offset: 0x000641C3
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B1B RID: 2843
	public const string ID = "OilChanger";

	// Token: 0x04000B1C RID: 2844
	public float OIL_CAPACITY = 400f;
}
