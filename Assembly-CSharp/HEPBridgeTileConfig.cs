using System;
using TUNING;
using UnityEngine;

// Token: 0x02000241 RID: 577
public class HEPBridgeTileConfig : IBuildingConfig
{
	// Token: 0x06000BA7 RID: 2983 RVA: 0x000469FD File Offset: 0x00044BFD
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000BA8 RID: 2984 RVA: 0x00046A04 File Offset: 0x00044C04
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HEPBridgeTile";
		int num = 2;
		int num2 = 1;
		string text2 = "radbolt_joint_plate_kanim";
		int num3 = 100;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Tile;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER5, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.UseStructureTemperature = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.InitialOrientation = Orientation.R180;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(1, 0);
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "HEPBridgeTile");
		return buildingDef;
	}

	// Token: 0x06000BA9 RID: 2985 RVA: 0x00046AE0 File Offset: 0x00044CE0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
		go.AddOrGet<TileTemperature>();
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.showInUI = false;
		highEnergyParticleStorage.capacity = 501f;
		HighEnergyParticleRedirector highEnergyParticleRedirector = go.AddOrGet<HighEnergyParticleRedirector>();
		highEnergyParticleRedirector.directorDelay = 0.5f;
		highEnergyParticleRedirector.directionControllable = false;
		highEnergyParticleRedirector.Direction = EightDirection.Right;
	}

	// Token: 0x06000BAA RID: 2986 RVA: 0x00046B57 File Offset: 0x00044D57
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		go.AddOrGet<HEPBridgeTileVisualizer>();
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x06000BAB RID: 2987 RVA: 0x00046B6F File Offset: 0x00044D6F
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.AddOrGet<BuildingCellVisualizer>();
	}

	// Token: 0x06000BAC RID: 2988 RVA: 0x00046B80 File Offset: 0x00044D80
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.HEPPassThrough, false);
		go.AddOrGet<BuildingCellVisualizer>();
		go.AddOrGetDef<MakeBaseSolid.Def>().solidOffsets = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject inst)
		{
			Rotatable component = inst.GetComponent<Rotatable>();
			HighEnergyParticleRedirector component2 = inst.GetComponent<HighEnergyParticleRedirector>();
			switch (component.Orientation)
			{
			case Orientation.Neutral:
				component2.Direction = EightDirection.Left;
				return;
			case Orientation.R90:
				component2.Direction = EightDirection.Up;
				return;
			case Orientation.R180:
				component2.Direction = EightDirection.Right;
				return;
			case Orientation.R270:
				component2.Direction = EightDirection.Down;
				return;
			default:
				return;
			}
		};
	}

	// Token: 0x04000804 RID: 2052
	public const string ID = "HEPBridgeTile";
}
