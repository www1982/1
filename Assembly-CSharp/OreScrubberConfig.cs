using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000359 RID: 857
public class OreScrubberConfig : IBuildingConfig
{
	// Token: 0x060011A4 RID: 4516 RVA: 0x00066D5C File Offset: 0x00064F5C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OreScrubber";
		int num = 3;
		int num2 = 3;
		string text2 = "orescrubber_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] array = new string[] { "Metal" };
		float[] array2 = new float[] { global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3[0] };
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.UtilityInputOffset = new CellOffset(1, 1);
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.AddSearchTerms(SEARCH_TERMS.FILTER);
		return buildingDef;
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00066DE4 File Offset: 0x00064FE4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		OreScrubber oreScrubber = go.AddOrGet<OreScrubber>();
		oreScrubber.massConsumedPerUse = 0.07f;
		oreScrubber.consumedElement = SimHashes.ChlorineGas;
		oreScrubber.diseaseRemovalCount = OreScrubberConfig.DISEASE_REMOVAL_COUNT;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.ChlorineGas).tag;
		go.AddOrGet<DirectionControl>();
		OreScrubber.Work work = go.AddOrGet<OreScrubber.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_ore_scrubber_kanim") };
		work.workTime = 10.200001f;
		work.trackUses = true;
		work.workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<Storage>().SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00066EBC File Offset: 0x000650BC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<RequireInputs>().requireConduitHasMass = false;
	}

	// Token: 0x04000B35 RID: 2869
	public const string ID = "OreScrubber";

	// Token: 0x04000B36 RID: 2870
	private const float MASS_PER_USE = 0.07f;

	// Token: 0x04000B37 RID: 2871
	private static readonly int DISEASE_REMOVAL_COUNT = WashBasinConfig.DISEASE_REMOVAL_COUNT * 4;

	// Token: 0x04000B38 RID: 2872
	private const SimHashes CONSUMED_ELEMENT = SimHashes.ChlorineGas;
}
