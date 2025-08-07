using System;
using TUNING;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class ParkSignConfig : IBuildingConfig
{
	// Token: 0x060011E8 RID: 4584 RVA: 0x00068914 File Offset: 0x00066B14
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("ParkSign", 1, 2, "parksign_kanim", 10, 10f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER1, MATERIALS.ANY_BUILDABLE, 1600f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.NONE, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		return buildingDef;
	}

	// Token: 0x060011E9 RID: 4585 RVA: 0x0006896E File Offset: 0x00066B6E
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.Park, false);
		go.AddOrGet<ParkSign>();
	}

	// Token: 0x060011EA RID: 4586 RVA: 0x00068988 File Offset: 0x00066B88
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B5D RID: 2909
	public const string ID = "ParkSign";
}
