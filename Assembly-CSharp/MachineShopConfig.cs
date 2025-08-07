using System;
using TUNING;
using UnityEngine;

// Token: 0x020002B7 RID: 695
public class MachineShopConfig : IBuildingConfig
{
	// Token: 0x06000E12 RID: 3602 RVA: 0x00052674 File Offset: 0x00050874
	public override BuildingDef CreateBuildingDef()
	{
		string text = "MachineShop";
		int num = 4;
		int num2 = 2;
		string text2 = "machineshop_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Deprecated = true;
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		return buildingDef;
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000526E9 File Offset: 0x000508E9
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.MachineShopType, false);
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x00052703 File Offset: 0x00050903
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400091C RID: 2332
	public const string ID = "MachineShop";

	// Token: 0x0400091D RID: 2333
	public static readonly Tag MATERIAL_FOR_TINKER = GameTags.RefinedMetal;

	// Token: 0x0400091E RID: 2334
	public const float MASS_PER_TINKER = 5f;

	// Token: 0x0400091F RID: 2335
	public static readonly string ROLE_PERK = "IncreaseMachinery";
}
