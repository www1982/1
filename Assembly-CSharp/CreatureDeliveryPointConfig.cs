using System;
using TUNING;
using UnityEngine;

// Token: 0x02000061 RID: 97
public class CreatureDeliveryPointConfig : IBuildingConfig
{
	// Token: 0x060001CC RID: 460 RVA: 0x0000D584 File Offset: 0x0000B784
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CreatureDeliveryPoint", 1, 3, "relocator_dropoff_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER1, MATERIALS.RAW_METALS, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x060001CD RID: 461 RVA: 0x0000D5E8 File Offset: 0x0000B7E8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.CodexCategories.CreatureRelocator, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = false;
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.BAGABLE_CREATURES;
		storage.workAnims = new HashedString[] { "place", "release" };
		storage.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_restrain_creature_kanim") };
		storage.workAnimPlayMode = KAnim.PlayMode.Once;
		storage.synchronizeAnims = false;
		storage.useGunForDelivery = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		go.AddOrGet<CreatureDeliveryPoint>();
		go.AddOrGet<BaggableCritterCapacityTracker>().maximumCreatures = 20;
		go.AddOrGet<FixedCapturePoint.AutoWrangleCapture>();
		go.AddOrGet<TreeFilterable>();
	}

	// Token: 0x060001CE RID: 462 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<FixedCapturePoint.Def>().isAmountStoredOverCapacity = delegate(FixedCapturePoint.Instance smi, FixedCapturableMonitor.Instance capturable)
		{
			TreeFilterable component = smi.GetComponent<TreeFilterable>();
			IUserControlledCapacity component2 = smi.GetComponent<IUserControlledCapacity>();
			float amountStored = component2.AmountStored;
			float userMaxCapacity = component2.UserMaxCapacity;
			return !component.ContainsTag(capturable.PrefabTag) || amountStored > userMaxCapacity;
		};
	}

	// Token: 0x04000126 RID: 294
	public const string ID = "CreatureDeliveryPoint";
}
