using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003BE RID: 958
public class ResearchCenterConfig : IBuildingConfig
{
	// Token: 0x06001381 RID: 4993 RVA: 0x0006F064 File Offset: 0x0006D264
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ResearchCenter";
		int num = 2;
		int num2 = 2;
		string text2 = "research_center_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 60f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.AddSearchTerms(SEARCH_TERMS.RESEARCH);
		return buildingDef;
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x0006F104 File Offset: 0x0006D304
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = ResearchCenterConfig.INPUT_MATERIAL;
		manualDeliveryKG.refillMass = 150f;
		manualDeliveryKG.capacity = 750f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.ResearchFetch.IdHash;
		ResearchCenter researchCenter = go.AddOrGet<ResearchCenter>();
		researchCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_research_center_kanim") };
		researchCenter.research_point_type_id = "basic";
		researchCenter.inputMaterial = ResearchCenterConfig.INPUT_MATERIAL;
		researchCenter.mass_per_point = 50f;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(ResearchCenterConfig.INPUT_MATERIAL, 1.1111112f, true)
		};
		elementConverter.showDescriptors = false;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x0006F212 File Offset: 0x0006D412
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000BCD RID: 3021
	public const float BASE_SECONDS_PER_POINT = 45f;

	// Token: 0x04000BCE RID: 3022
	public const float MASS_PER_POINT = 50f;

	// Token: 0x04000BCF RID: 3023
	public const float BASE_MASS_PER_SECOND = 1.1111112f;

	// Token: 0x04000BD0 RID: 3024
	public static readonly Tag INPUT_MATERIAL = GameTags.Dirt;

	// Token: 0x04000BD1 RID: 3025
	public const float CAPACITY = 750f;

	// Token: 0x04000BD2 RID: 3026
	public const string ID = "ResearchCenter";
}
