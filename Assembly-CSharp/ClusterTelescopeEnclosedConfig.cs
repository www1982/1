using System;
using TUNING;
using UnityEngine;

// Token: 0x02000046 RID: 70
public class ClusterTelescopeEnclosedConfig : IBuildingConfig
{
	// Token: 0x06000157 RID: 343 RVA: 0x0000A483 File Offset: 0x00008683
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000A48C File Offset: 0x0000868C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ClusterTelescopeEnclosed";
		int num = 4;
		int num2 = 6;
		string text2 = "telescope_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanUseClusterTelescopeEnclosed.Id;
		return buildingDef;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x0000A54C File Offset: 0x0000874C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		go.AddOrGetDef<PoweredController.Def>();
		ClusterTelescope.Def def = go.AddOrGetDef<ClusterTelescope.Def>();
		def.clearScanCellRadius = 4;
		def.analyzeClusterRadius = 4;
		def.workableOverrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telescope_kanim") };
		def.skyVisibilityInfo = ClusterTelescopeEnclosedConfig.SKY_VISIBILITY_INFO;
		def.providesOxygen = true;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000A624 File Offset: 0x00008824
	public override void DoPostConfigureComplete(GameObject go)
	{
		ClusterTelescopeEnclosedConfig.AddVisualizer(go);
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000A62C File Offset: 0x0000882C
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		ClusterTelescopeEnclosedConfig.AddVisualizer(go);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0000A634 File Offset: 0x00008834
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		ClusterTelescopeEnclosedConfig.AddVisualizer(go);
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000A63C File Offset: 0x0000883C
	private static void AddVisualizer(GameObject prefab)
	{
		SkyVisibilityVisualizer skyVisibilityVisualizer = prefab.AddOrGet<SkyVisibilityVisualizer>();
		skyVisibilityVisualizer.OriginOffset.y = 3;
		skyVisibilityVisualizer.TwoWideOrgin = true;
		skyVisibilityVisualizer.RangeMin = -4;
		skyVisibilityVisualizer.RangeMax = 5;
		skyVisibilityVisualizer.SkipOnModuleInteriors = true;
	}

	// Token: 0x040000D5 RID: 213
	public const string ID = "ClusterTelescopeEnclosed";

	// Token: 0x040000D6 RID: 214
	public const int SCAN_RADIUS = 4;

	// Token: 0x040000D7 RID: 215
	public const int VERTICAL_SCAN_OFFSET = 3;

	// Token: 0x040000D8 RID: 216
	public static readonly SkyVisibilityInfo SKY_VISIBILITY_INFO = new SkyVisibilityInfo(new CellOffset(0, 3), 4, new CellOffset(1, 3), 4, 0);
}
