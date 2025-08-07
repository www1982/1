using System;
using Database;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200033A RID: 826
public class MonumentMiddleConfig : IBuildingConfig
{
	// Token: 0x0600110E RID: 4366 RVA: 0x00063EDC File Offset: 0x000620DC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MonumentMiddle";
		int num = 5;
		int num2 = 5;
		string text2 = "monument_mid_a_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] array = new float[] { 2500f, 2500f, 5000f };
		string[] array2 = new string[]
		{
			SimHashes.Ceramic.ToString(),
			SimHashes.Polypropylene.ToString(),
			SimHashes.Steel.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.MONUMENT.INCOMPLETE, tier, 0.2f);
		BuildingTemplates.CreateMonumentBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = "MonumentMiddle";
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = false;
		buildingDef.AddSearchTerms(SEARCH_TERMS.STATUE);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x00063FF0 File Offset: 0x000621F0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), "MonumentTop", null)
		};
		go.AddOrGet<MonumentPart>().part = MonumentPartResource.Part.Middle;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x00064054 File Offset: 0x00062254
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x00064056 File Offset: 0x00062256
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x00064058 File Offset: 0x00062258
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<KBatchedAnimController>().initialAnim = "option_a";
		go.GetComponent<KPrefabID>().prefabSpawnFn += delegate(GameObject game_object)
		{
			MonumentPart monumentPart = game_object.AddOrGet<MonumentPart>();
			monumentPart.part = MonumentPartResource.Part.Middle;
			monumentPart.stateUISymbol = "mid";
		};
	}

	// Token: 0x04000AB7 RID: 2743
	public const string ID = "MonumentMiddle";
}
