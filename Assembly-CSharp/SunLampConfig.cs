using System;
using TUNING;
using UnityEngine;

// Token: 0x02000421 RID: 1057
public class SunLampConfig : IBuildingConfig
{
	// Token: 0x060015C3 RID: 5571 RVA: 0x0007C048 File Offset: 0x0007A248
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SunLamp";
		int num = 2;
		int num2 = 4;
		string text2 = "sun_lamp_kanim";
		int num3 = 10;
		float num4 = 60f;
		float[] array = new float[] { 200f, 50f };
		string[] array2 = new string[] { "RefinedMetal", "Glass" };
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER3, none, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 960f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.ViewMode = OverlayModes.Light.ID;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060015C4 RID: 5572 RVA: 0x0007C0F0 File Offset: 0x0007A2F0
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		LightShapePreview lightShapePreview = go.AddComponent<LightShapePreview>();
		lightShapePreview.lux = LIGHT2D.SUNLAMP_LUX;
		lightShapePreview.radius = 16f;
		lightShapePreview.shape = global::LightShape.Cone;
		lightShapePreview.offset = new CellOffset((int)LIGHT2D.SUNLAMP_OFFSET.x, (int)LIGHT2D.SUNLAMP_OFFSET.y);
	}

	// Token: 0x060015C5 RID: 5573 RVA: 0x0007C140 File Offset: 0x0007A340
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
	}

	// Token: 0x060015C6 RID: 5574 RVA: 0x0007C154 File Offset: 0x0007A354
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<EnergyConsumer>();
		go.AddOrGet<LoopingSounds>();
		Light2D light2D = go.AddOrGet<Light2D>();
		light2D.Lux = LIGHT2D.SUNLAMP_LUX;
		light2D.overlayColour = LIGHT2D.SUNLAMP_OVERLAYCOLOR;
		light2D.Color = LIGHT2D.SUNLAMP_COLOR;
		light2D.Range = 16f;
		light2D.Angle = 5.2f;
		light2D.Direction = LIGHT2D.SUNLAMP_DIRECTION;
		light2D.Offset = LIGHT2D.SUNLAMP_OFFSET;
		light2D.shape = global::LightShape.Cone;
		light2D.drawOverlay = true;
		go.AddOrGetDef<LightController.Def>();
	}

	// Token: 0x04000CE0 RID: 3296
	public const string ID = "SunLamp";
}
