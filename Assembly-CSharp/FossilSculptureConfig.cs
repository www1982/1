using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000214 RID: 532
public class FossilSculptureConfig : IBuildingConfig
{
	// Token: 0x06000AA8 RID: 2728 RVA: 0x00040562 File Offset: 0x0003E762
	public override string[] GetRequiredDlcIds()
	{
		return new string[] { "DLC4_ID" };
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x00040574 File Offset: 0x0003E774
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("FossilSculpture", 3, 3, "fossilsculpture_kanim", 100, 240f, global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.FOSSILS, 800f, BuildLocationRule.OnFloor, DECOR.BONUS.TIER5, NOISE_POLLUTION.NONE, 0.2f);
		buildingDef.InputConduitType = ConduitType.None;
		buildingDef.OutputConduitType = ConduitType.None;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.UseHighEnergyParticleInputPort = false;
		buildingDef.UseHighEnergyParticleOutputPort = false;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 0);
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 0);
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.DragBuild = false;
		buildingDef.Replaceable = true;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.EnergyConsumptionWhenActive = 0f;
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanArtGreat.Id;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.DefaultAnimState = "slab";
		buildingDef.UseStructureTemperature = true;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Disinfectable = true;
		buildingDef.Entombable = true;
		buildingDef.Invincible = false;
		buildingDef.Repairable = false;
		buildingDef.IsFoundation = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AddSearchTerms(SEARCH_TERMS.DINOSAUR);
		buildingDef.AddSearchTerms(SEARCH_TERMS.STATUE);
		buildingDef.AddSearchTerms(SEARCH_TERMS.ARTWORK);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x000406FF File Offset: 0x0003E8FF
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.AddOrGet<LoopingSounds>();
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x00040725 File Offset: 0x0003E925
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x00040727 File Offset: 0x0003E927
	public override void DoPostConfigureComplete(GameObject go)
	{
		Sculpture sculpture = go.AddOrGet<Sculpture>();
		sculpture.requiredSkillPerk = Db.Get().SkillPerks.CanArtGreat.Id;
		sculpture.defaultAnimName = "slab";
	}

	// Token: 0x0400075C RID: 1884
	public const string ID = "FossilSculpture";
}
