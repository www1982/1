using System;
using TUNING;
using UnityEngine;

// Token: 0x020003FB RID: 1019
public class SolidCargoBayConfig : IBuildingConfig
{
	// Token: 0x060014DE RID: 5342 RVA: 0x000774B2 File Offset: 0x000756B2
	public override string[] GetForbiddenDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000774BC File Offset: 0x000756BC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CargoBay";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_storage_solid_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] array = new float[] { 1000f, 1000f };
		string[] array2 = new string[]
		{
			"BuildableRaw",
			SimHashes.Steel.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.Invincible = true;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.OutputConduitType = ConduitType.Solid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 3);
		return buildingDef;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x000775A0 File Offset: 0x000757A0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00077604 File Offset: 0x00075804
	public override void DoPostConfigureComplete(GameObject go)
	{
		CargoBay cargoBay = go.AddOrGet<CargoBay>();
		cargoBay.storage = go.AddOrGet<Storage>();
		cargoBay.storageType = CargoBay.CargoType.Solids;
		cargoBay.storage.capacityKg = 1000f;
		cargoBay.storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_storage_solid_bg_kanim", false);
		go.AddOrGet<SolidConduitDispenser>();
	}

	// Token: 0x04000C6C RID: 3180
	public const string ID = "CargoBay";
}
