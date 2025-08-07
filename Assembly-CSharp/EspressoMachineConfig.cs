using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200008F RID: 143
public class EspressoMachineConfig : IBuildingConfig
{
	// Token: 0x060002D0 RID: 720 RVA: 0x00014970 File Offset: 0x00012B70
	public override BuildingDef CreateBuildingDef()
	{
		string text = "EspressoMachine";
		int num = 3;
		int num2 = 3;
		string text2 = "espresso_machine_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(1, 2);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(1, 2);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00014A28 File Offset: 0x00012C28
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 20f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
		conduitConsumer.capacityKG = 2f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("SpiceNut");
		manualDeliveryKG.capacity = 10f;
		manualDeliveryKG.refillMass = 5f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		go.AddOrGet<EspressoMachineWorkable>();
		go.AddOrGet<EspressoMachine>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		component.prefabInitFn += this.OnInit;
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x00014B34 File Offset: 0x00012D34
	private void OnInit(GameObject go)
	{
		EspressoMachineWorkable component = go.GetComponent<EspressoMachineWorkable>();
		KAnimFile[] array = new KAnimFile[] { Assets.GetAnim("anim_interacts_espresso_machine_kanim") };
		component.workerTypeOverrideAnims.Add(MinionConfig.ID, array);
		component.workerTypeOverrideAnims.Add(BionicMinionConfig.ID, new KAnimFile[] { Assets.GetAnim("anim_bionic_interacts_espresso_machine_kanim") });
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x00014BA4 File Offset: 0x00012DA4
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040001A4 RID: 420
	public const string ID = "EspressoMachine";
}
