using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000245 RID: 581
public class HandSanitizerConfig : IBuildingConfig
{
	// Token: 0x06000BC5 RID: 3013 RVA: 0x0004768C File Offset: 0x0004588C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HandSanitizer";
		int num = 1;
		int num2 = 3;
		string text2 = "handsanitizer_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] array = new string[] { "Metal" };
		float[] array2 = new float[] { global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0] };
		string[] array3 = array;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_out", NOISE_POLLUTION.NOISY.TIER0);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_in", NOISE_POLLUTION.NOISY.TIER0);
		SoundEventVolumeCache.instance.AddVolume("handsanitizer_kanim", "HandSanitizer_tongue_slurp", NOISE_POLLUTION.NOISY.TIER0);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MEDICINE);
		return buildingDef;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00047744 File Offset: 0x00045944
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WashStation, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.AdvancedWashStation, false);
		HandSanitizer handSanitizer = go.AddOrGet<HandSanitizer>();
		handSanitizer.massConsumedPerUse = 0.07f;
		handSanitizer.consumedElement = SimHashes.BleachStone;
		handSanitizer.diseaseRemovalCount = HandSanitizerConfig.DISEASE_REMOVAL_COUNT;
		HandSanitizer.Work work = go.AddOrGet<HandSanitizer.Work>();
		work.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_handsanitizer_kanim") };
		work.workTime = 1.8f;
		work.trackUses = true;
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		go.AddOrGet<DirectionControl>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTagExtensions.Create(SimHashes.BleachStone);
		manualDeliveryKG.capacity = 15f;
		manualDeliveryKG.refillMass = 3f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.Functional;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00047843 File Offset: 0x00045A43
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000815 RID: 2069
	public const string ID = "HandSanitizer";

	// Token: 0x04000816 RID: 2070
	private const float STORAGE_SIZE = 15f;

	// Token: 0x04000817 RID: 2071
	private const float MASS_PER_USE = 0.07f;

	// Token: 0x04000818 RID: 2072
	private static readonly int DISEASE_REMOVAL_COUNT = WashBasinConfig.DISEASE_REMOVAL_COUNT * 4;

	// Token: 0x04000819 RID: 2073
	private const float WORK_TIME = 1.8f;

	// Token: 0x0400081A RID: 2074
	private const SimHashes CONSUMED_ELEMENT = SimHashes.BleachStone;
}
