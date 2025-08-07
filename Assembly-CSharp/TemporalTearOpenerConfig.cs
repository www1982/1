using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class TemporalTearOpenerConfig : IBuildingConfig
{
	// Token: 0x060015EF RID: 5615 RVA: 0x0007D216 File Offset: 0x0007B416
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060015F0 RID: 5616 RVA: 0x0007D220 File Offset: 0x0007B420
	public override BuildingDef CreateBuildingDef()
	{
		string text = "TemporalTearOpener";
		int num = 3;
		int num2 = 4;
		string text2 = "temporal_tear_opener_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.DefaultAnimState = "off";
		buildingDef.Entombable = false;
		buildingDef.Invincible = true;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 2);
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE, global::STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.HEPENGINE.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060015F1 RID: 5617 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.capacity = 1000f;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		TemporalTearOpener.Def def = go.AddOrGetDef<TemporalTearOpener.Def>();
		def.numParticlesToOpen = 10000f;
		def.consumeRate = 5f;
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x0007D366 File Offset: 0x0007B566
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
	}

	// Token: 0x04000CFA RID: 3322
	public const string ID = "TemporalTearOpener";

	// Token: 0x04000CFB RID: 3323
	public const string PORT_ID = "HEP_STORAGE";

	// Token: 0x04000CFC RID: 3324
	public const float PARTICLES_CAPACITY = 1000f;

	// Token: 0x04000CFD RID: 3325
	public const float NUM_PARTICLES_TO_OPEN_TEAR = 10000f;

	// Token: 0x04000CFE RID: 3326
	public const float PARTICLE_CONSUME_RATE = 5f;
}
