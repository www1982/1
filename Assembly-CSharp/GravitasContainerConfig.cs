using System;
using TUNING;
using UnityEngine;

// Token: 0x02000238 RID: 568
public class GravitasContainerConfig : IBuildingConfig
{
	// Token: 0x06000B67 RID: 2919 RVA: 0x00045A30 File Offset: 0x00043C30
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasContainer";
		int num = 2;
		int num2 = 2;
		string text2 = "gravitas_container_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000B68 RID: 2920 RVA: 0x00045A9D File Offset: 0x00043C9D
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.Gravitas);
		go.AddOrGet<KBatchedAnimController>().sceneLayer = Grid.SceneLayer.Building;
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000B69 RID: 2921 RVA: 0x00045AC0 File Offset: 0x00043CC0
	public override void DoPostConfigureComplete(GameObject go)
	{
		PajamaDispenser pajamaDispenser = go.AddComponent<PajamaDispenser>();
		pajamaDispenser.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_gravitas_container_kanim") };
		pajamaDispenser.SetWorkTime(30f);
		go.AddOrGet<Demolishable>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
	}

	// Token: 0x040007E7 RID: 2023
	public const string ID = "GravitasContainer";

	// Token: 0x040007E8 RID: 2024
	private const float WORK_TIME = 1.5f;
}
