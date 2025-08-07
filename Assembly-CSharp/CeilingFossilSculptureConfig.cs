using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200003B RID: 59
public class CeilingFossilSculptureConfig : IBuildingConfig
{
	// Token: 0x06000119 RID: 281 RVA: 0x00008C18 File Offset: 0x00006E18
	public override string[] GetRequiredDlcIds()
	{
		return new string[] { "DLC4_ID" };
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00008C28 File Offset: 0x00006E28
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CeilingFossilSculpture", 3, 2, "fossilsculpture_hanging_kanim", 100, 240f, global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.FOSSILS, 800f, BuildLocationRule.OnCeiling, DECOR.BONUS.TIER5, NOISE_POLLUTION.NONE, 0.2f);
		buildingDef.InputConduitType = ConduitType.None;
		buildingDef.OutputConduitType = ConduitType.None;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.RequiresPowerOutput = false;
		buildingDef.PowerInputOffset = new CellOffset(0, 1);
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
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

	// Token: 0x0600011B RID: 283 RVA: 0x00008DCD File Offset: 0x00006FCD
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.AddOrGet<LoopingSounds>();
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00008DF3 File Offset: 0x00006FF3
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00008DF5 File Offset: 0x00006FF5
	public override void DoPostConfigureComplete(GameObject go)
	{
		LongRangeSculpture longRangeSculpture = go.AddOrGet<LongRangeSculpture>();
		longRangeSculpture.requiredSkillPerk = Db.Get().SkillPerks.CanArtGreat.Id;
		longRangeSculpture.defaultAnimName = "slab";
	}

	// Token: 0x040000AF RID: 175
	public const string ID = "CeilingFossilSculpture";
}
