using System;
using System.Collections.Generic;
using System.Linq;
using TUNING;
using UnityEngine;

// Token: 0x020003B7 RID: 951
public class RailGunPayloadOpenerConfig : IBuildingConfig
{
	// Token: 0x0600135B RID: 4955 RVA: 0x0006E28C File Offset: 0x0006C48C
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600135C RID: 4956 RVA: 0x0006E294 File Offset: 0x0006C494
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RailGunPayloadOpener";
		int num = 3;
		int num2 = 3;
		string text2 = "railgun_emptier_kanim";
		int num3 = 250;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.DefaultAnimState = "on";
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		return buildingDef;
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x0006E32B File Offset: 0x0006C52B
	private void AttachPorts(GameObject go)
	{
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
		go.AddComponent<ConduitSecondaryOutput>().portInfo = this.solidOutputPort;
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x0006E360 File Offset: 0x0006C560
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		RailGunPayloadOpener railGunPayloadOpener = go.AddOrGet<RailGunPayloadOpener>();
		railGunPayloadOpener.liquidPortInfo = this.liquidOutputPort;
		railGunPayloadOpener.gasPortInfo = this.gasOutputPort;
		railGunPayloadOpener.solidPortInfo = this.solidOutputPort;
		railGunPayloadOpener.payloadStorage = go.AddComponent<Storage>();
		railGunPayloadOpener.payloadStorage.showInUI = true;
		railGunPayloadOpener.payloadStorage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		railGunPayloadOpener.payloadStorage.storageFilters = new List<Tag> { GameTags.RailGunPayloadEmptyable };
		railGunPayloadOpener.payloadStorage.capacityKg = 2000f;
		railGunPayloadOpener.resourceStorage = go.AddComponent<Storage>();
		railGunPayloadOpener.resourceStorage.showInUI = true;
		railGunPayloadOpener.resourceStorage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		List<Tag> list = STORAGEFILTERS.STORAGE_LOCKERS_STANDARD.Concat(STORAGEFILTERS.GASES).ToList<Tag>();
		list = list.Concat(STORAGEFILTERS.LIQUIDS).ToList<Tag>();
		railGunPayloadOpener.resourceStorage.storageFilters = list;
		railGunPayloadOpener.resourceStorage.capacityKg = 20000f;
		ManualDeliveryKG manualDeliveryKG = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(railGunPayloadOpener.payloadStorage);
		manualDeliveryKG.RequestedItemTag = GameTags.RailGunPayloadEmptyable;
		manualDeliveryKG.capacity = 2000f;
		manualDeliveryKG.refillMass = 200f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.operationalRequirement = Operational.State.None;
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x0006E4A8 File Offset: 0x0006C6A8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingCellVisualizer>();
		DropAllWorkable dropAllWorkable = go.AddOrGet<DropAllWorkable>();
		dropAllWorkable.dropWorkTime = 90f;
		dropAllWorkable.choreTypeID = Db.Get().ChoreTypes.Fetch.Id;
		dropAllWorkable.ConfigureMultitoolContext("build", EffectConfigs.BuildSplashId);
		RequireInputs component = go.GetComponent<RequireInputs>();
		component.SetRequirements(true, false);
		component.requireConduitHasMass = false;
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x0006E514 File Offset: 0x0006C714
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AttachPorts(go);
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x0006E52C File Offset: 0x0006C72C
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		this.AttachPorts(go);
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x04000BB7 RID: 2999
	public const string ID = "RailGunPayloadOpener";

	// Token: 0x04000BB8 RID: 3000
	private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(0, 0));

	// Token: 0x04000BB9 RID: 3001
	private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(1, 0));

	// Token: 0x04000BBA RID: 3002
	private ConduitPortInfo solidOutputPort = new ConduitPortInfo(ConduitType.Solid, new CellOffset(-1, 0));
}
