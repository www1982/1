using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x020002B5 RID: 693
public class LonelyMinionMailboxConfig : IBuildingConfig
{
	// Token: 0x06000E09 RID: 3593 RVA: 0x000523E4 File Offset: 0x000505E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LonelyMailBox";
		int num = 2;
		int num2 = 2;
		string text2 = "parcel_delivery_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.ViewMode = OverlayModes.None.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		return buildingDef;
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x00052474 File Offset: 0x00050674
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		SingleEntityReceptacle singleEntityReceptacle = go.AddComponent<SingleEntityReceptacle>();
		singleEntityReceptacle.AddDepositTag(GameTags.Edible);
		singleEntityReceptacle.enabled = false;
		go.AddComponent<LonelyMinionMailbox>();
		go.GetComponent<Deconstructable>().allowDeconstruction = false;
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = false;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Preserve
		});
		Prioritizable.AddRef(go);
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x000524D6 File Offset: 0x000506D6
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000919 RID: 2329
	public const string ID = "LonelyMailBox";

	// Token: 0x0400091A RID: 2330
	public static readonly HashedString IdHash = "LonelyMailBox";
}
