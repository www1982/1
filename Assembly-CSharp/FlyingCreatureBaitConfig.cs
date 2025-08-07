using System;
using TUNING;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class FlyingCreatureBaitConfig : IBuildingConfig
{
	// Token: 0x060008FD RID: 2301 RVA: 0x0003C8A4 File Offset: 0x0003AAA4
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("FlyingCreatureBait", 1, 2, "airborne_critter_bait_kanim", 10, 10f, new float[] { 50f, 10f }, new string[] { "Metal", "FlyingCritterEdible" }, 1600f, BuildLocationRule.Anywhere, BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.Deprecated = true;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0003C923 File Offset: 0x0003AB23
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<CreatureBait>();
		go.AddTag(GameTags.OneTimeUseLure);
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0003C937 File Offset: 0x0003AB37
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0003C939 File Offset: 0x0003AB39
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0003C93C File Offset: 0x0003AB3C
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.DoPostConfigure(go);
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGet<SymbolOverrideController>().applySymbolOverridesEveryFrame = true;
		Lure.Def def = go.AddOrGetDef<Lure.Def>();
		def.defaultLurePoints = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		def.radius = 32;
		Prioritizable.AddRef(go);
	}

	// Token: 0x04000696 RID: 1686
	public const string ID = "FlyingCreatureBait";
}
