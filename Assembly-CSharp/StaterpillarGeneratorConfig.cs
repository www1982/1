using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class StaterpillarGeneratorConfig : IBuildingConfig
{
	// Token: 0x06001569 RID: 5481 RVA: 0x00079DD4 File Offset: 0x00077FD4
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600156A RID: 5482 RVA: 0x00079DDC File Offset: 0x00077FDC
	public override BuildingDef CreateBuildingDef()
	{
		string id = StaterpillarGeneratorConfig.ID;
		int num = 1;
		int num2 = 2;
		string text = "egg_caterpillar_kanim";
		int num3 = 1000;
		float num4 = 10f;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] array = all_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFoundationRotatable;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 1600f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 2f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.OverheatTemperature = 423.15f;
		buildingDef.PermittedRotations = PermittedRotations.FlipV;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 1);
		buildingDef.PlayConstructionSounds = false;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600156B RID: 5483 RVA: 0x00079EAD File Offset: 0x000780AD
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x0600156C RID: 5484 RVA: 0x00079EC4 File Offset: 0x000780C4
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x0600156D RID: 5485 RVA: 0x00079EC6 File Offset: 0x000780C6
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x00079EC8 File Offset: 0x000780C8
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<StaterpillarGenerator>().powerDistributionOrder = 9;
		go.GetComponent<Deconstructable>().SetAllowDeconstruction(false);
		go.AddOrGet<Modifiers>();
		go.AddOrGet<Effects>();
		go.GetComponent<KSelectable>().IsSelectable = false;
	}

	// Token: 0x04000CB9 RID: 3257
	public static readonly string ID = "StaterpillarGenerator";

	// Token: 0x04000CBA RID: 3258
	private const int WIDTH = 1;

	// Token: 0x04000CBB RID: 3259
	private const int HEIGHT = 2;
}
