using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200035A RID: 858
public class OuthouseConfig : IBuildingConfig
{
	// Token: 0x060011A9 RID: 4521 RVA: 0x00066EE0 File Offset: 0x000650E0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Outhouse";
		int num = 2;
		int num2 = 3;
		string text2 = "outhouse_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_MINERALS_OR_WOOD = MATERIALS.RAW_MINERALS_OR_WOOD;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS_OR_WOOD, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER4, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.DiseaseCellVisName = DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AddSearchTerms(SEARCH_TERMS.TOILET);
		SoundEventVolumeCache.instance.AddVolume("outhouse_kanim", "Latrine_door_open", NOISE_POLLUTION.NOISY.TIER1);
		SoundEventVolumeCache.instance.AddVolume("outhouse_kanim", "Latrine_door_close", NOISE_POLLUTION.NOISY.TIER1);
		return buildingDef;
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x00066F9C File Offset: 0x0006519C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		go.AddOrGet<LoopingSounds>();
		component.AddTag(RoomConstraints.ConstraintTags.ToiletType, false);
		Toilet toilet = go.AddOrGet<Toilet>();
		toilet.maxFlushes = 15;
		toilet.dirtUsedPerFlush = 13f;
		toilet.solidWastePerUse = new Toilet.SpawnInfo(SimHashes.ToxicSand, DUPLICANTSTATS.STANDARD.Secretions.PEE_PER_TOILET_PEE, 0f);
		toilet.solidWasteTemperature = DUPLICANTSTATS.STANDARD.Temperature.Internal.IDEAL;
		toilet.diseaseId = DUPLICANTSTATS.STANDARD.Secretions.PEE_DISEASE;
		toilet.diseasePerFlush = DUPLICANTSTATS.STANDARD.Secretions.DISEASE_PER_PEE;
		toilet.diseaseOnDupePerFlush = DUPLICANTSTATS.STANDARD.Secretions.DISEASE_PER_PEE;
		go.AddOrGet<ToiletWorkableUse>().workLayer = Grid.SceneLayer.BuildingFront;
		ToiletWorkableClean toiletWorkableClean = go.AddOrGet<ToiletWorkableClean>();
		toiletWorkableClean.workTime = 90f;
		toiletWorkableClean.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_outhouse_kanim") };
		toiletWorkableClean.workLayer = Grid.SceneLayer.BuildingFront;
		Prioritizable.AddRef(go);
		toiletWorkableClean.SetIsCloggedByGunk(false);
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("Dirt");
		manualDeliveryKG.capacity = 200f;
		manualDeliveryKG.refillMass = 0.01f;
		manualDeliveryKG.MinimumMass = 200f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.Functional;
		manualDeliveryKG.FillToCapacity = true;
		Ownable ownable = go.AddOrGet<Ownable>();
		ownable.slotID = Db.Get().AssignableSlots.Toilet.Id;
		ownable.canBePublic = true;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
		component.prefabInitFn += this.OnInit;
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x00067168 File Offset: 0x00065368
	private void OnInit(GameObject go)
	{
		ToiletWorkableUse component = go.GetComponent<ToiletWorkableUse>();
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim("anim_interacts_outhouse_kanim") };
		component.workerTypeOverrideAnims.Add(MinionConfig.ID, array);
		component.workerTypeOverrideAnims.Add(BionicMinionConfig.ID, new KAnimFile[] { Assets.GetAnim("anim_bionic_interacts_outhouse_kanim") });
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000671D8 File Offset: 0x000653D8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B39 RID: 2873
	public const string ID = "Outhouse";

	// Token: 0x04000B3A RID: 2874
	private const int USES_PER_REFILL = 15;

	// Token: 0x04000B3B RID: 2875
	private const float DIRT_PER_REFILL = 200f;

	// Token: 0x04000B3C RID: 2876
	private const float DIRT_PER_USE = 13f;
}
