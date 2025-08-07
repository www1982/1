using System;
using TUNING;
using UnityEngine;

// Token: 0x02000064 RID: 100
public class CrewCapsuleConfig : IBuildingConfig
{
	// Token: 0x060001DD RID: 477 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CrewCapsule";
		int num = 5;
		int num2 = 19;
		string text2 = "rocket_small_steam_kanim";
		int num3 = 1000;
		float num4 = 480f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER7;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.UtilityInputOffset = new CellOffset(2, 6);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 10f;
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0000DB7F File Offset: 0x0000BD7F
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<LaunchConditionManager>();
		go.AddOrGet<RocketLaunchConditionVisualizer>();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Storage>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
	}

	// Token: 0x0400012B RID: 299
	public const string ID = "CrewCapsule";
}
