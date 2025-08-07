using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class CreatureFeederConfig : IBuildingConfig
{
	// Token: 0x060001D0 RID: 464 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CreatureFeeder";
		int num = 1;
		int num2 = 2;
		string text2 = "feeder_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x0000D735 File Offset: 0x0000B935
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060001D2 RID: 466 RVA: 0x0000D738 File Offset: 0x0000B938
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 2000f;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.allowItemRemoval = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		go.AddOrGet<StorageLocker>().choreTypeID = Db.Get().ChoreTypes.RanchingFetch.Id;
		go.AddOrGet<UserNameable>();
		go.AddOrGet<TreeFilterable>();
		go.AddOrGet<CreatureFeeder>();
		go.GetComponent<KPrefabID>().prefabInitFn += this.OnPrefabInit;
	}

	// Token: 0x060001D3 RID: 467 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
	}

	// Token: 0x060001D4 RID: 468 RVA: 0x0000D7DC File Offset: 0x0000B9DC
	public override void ConfigurePost(BuildingDef def)
	{
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, Diet> keyValuePair in DietManager.CollectDiets(new Tag[]
		{
			GameTags.Creatures.Species.LightBugSpecies,
			GameTags.Creatures.Species.HatchSpecies,
			GameTags.Creatures.Species.MoleSpecies,
			GameTags.Creatures.Species.CrabSpecies,
			GameTags.Creatures.Species.StaterpillarSpecies,
			GameTags.Creatures.Species.DivergentSpecies,
			GameTags.Creatures.Species.DeerSpecies,
			GameTags.Creatures.Species.BellySpecies,
			GameTags.Creatures.Species.SealSpecies,
			GameTags.Creatures.Species.StegoSpecies,
			GameTags.Creatures.Species.RaptorSpecies,
			GameTags.Creatures.Species.ChameleonSpecies
		}))
		{
			Diet value = keyValuePair.Value;
			if (value.CanEatPreyCritter)
			{
				Diet.Info[] preyInfos = value.preyInfos;
				for (int i = 0; i < preyInfos.Length; i++)
				{
					foreach (Tag tag in preyInfos[i].consumedTags)
					{
						CreatureFeederConfig.forbiddenTags.Add(tag);
					}
				}
			}
			list.Add(keyValuePair.Key);
		}
		def.BuildingComplete.GetComponent<Storage>().storageFilters = list;
	}

	// Token: 0x060001D5 RID: 469 RVA: 0x0000D964 File Offset: 0x0000BB64
	private void OnPrefabInit(GameObject instance)
	{
		TreeFilterable component = instance.GetComponent<TreeFilterable>();
		foreach (Tag tag in CreatureFeederConfig.forbiddenTags)
		{
			component.ForbiddenTags.Add(tag);
		}
	}

	// Token: 0x04000127 RID: 295
	public const string ID = "CreatureFeeder";

	// Token: 0x04000128 RID: 296
	private static HashSet<Tag> forbiddenTags = new HashSet<Tag>();
}
