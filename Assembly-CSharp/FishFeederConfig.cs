using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class FishFeederConfig : IBuildingConfig
{
	// Token: 0x060006FD RID: 1789 RVA: 0x00030BAC File Offset: 0x0002EDAC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FishFeeder";
		int num = 1;
		int num2 = 3;
		string text2 = "fishfeeder_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.Entombable = true;
		buildingDef.Floodable = true;
		buildingDef.ForegroundLayer = Grid.SceneLayer.TileMain;
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		return buildingDef;
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x00030C33 File Offset: 0x0002EE33
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x00030C38 File Offset: 0x0002EE38
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 200f;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.allowItemRemoval = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.showCapacityStatusItem = true;
		storage.showCapacityAsMainStatus = true;
		storage.dropOffset = Vector2.up * 1f;
		storage.storageID = new Tag("FishFeederTop");
		Storage storage2 = go.AddComponent<Storage>();
		storage2.capacityKg = 200f;
		storage2.showInUI = true;
		storage2.showDescriptor = true;
		storage2.allowItemRemoval = false;
		storage2.dropOffset = Vector2.up * 3.5f;
		storage2.storageID = new Tag("FishFeederBot");
		go.AddOrGet<StorageLocker>().choreTypeID = Db.Get().ChoreTypes.RanchingFetch.Id;
		go.AddOrGet<UserNameable>();
		Effect effect = new Effect("AteFromFeeder", global::STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, global::STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.TOOLTIP, 1200f, true, false, false, null, -1f, 0f, null, "");
		effect.Add(new AttributeModifier(Db.Get().Amounts.Wildness.deltaAttribute.Id, -0.033333335f, global::STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, false, false, true));
		effect.Add(new AttributeModifier(Db.Get().CritterAttributes.Happiness.Id, 5f, global::STRINGS.CREATURES.MODIFIERS.ATE_FROM_FEEDER.NAME, false, false, true));
		Db.Get().effects.Add(effect);
		go.AddOrGet<TreeFilterable>().filterAllStoragesOnBuilding = true;
		CreatureFeeder creatureFeeder = go.AddOrGet<CreatureFeeder>();
		creatureFeeder.effectId = effect.Id;
		creatureFeeder.feederOffset = new CellOffset(0, -2);
		go.GetComponent<KPrefabID>().prefabInitFn += this.OnPrefabInit;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x00030E0C File Offset: 0x0002F00C
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<StorageController.Def>();
		go.AddOrGetDef<FishFeeder.Def>();
		go.AddOrGetDef<MakeBaseSolid.Def>().solidOffsets = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		SymbolOverrideControllerUtil.AddToPrefab(go);
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x00030E44 File Offset: 0x0002F044
	public override void ConfigurePost(BuildingDef def)
	{
		List<Tag> list = new List<Tag>();
		foreach (KeyValuePair<Tag, Diet> keyValuePair in DietManager.CollectDiets(new Tag[]
		{
			GameTags.Creatures.Species.PacuSpecies,
			GameTags.Creatures.Species.PrehistoricPacuSpecies
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
						FishFeederConfig.forbiddenTags.Add(tag);
					}
				}
			}
			list.Add(keyValuePair.Key);
		}
		def.BuildingComplete.GetComponent<Storage>().storageFilters = list;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x00030F50 File Offset: 0x0002F150
	private void OnPrefabInit(GameObject instance)
	{
		TreeFilterable component = instance.GetComponent<TreeFilterable>();
		foreach (Tag tag in FishFeederConfig.forbiddenTags)
		{
			component.ForbiddenTags.Add(tag);
		}
	}

	// Token: 0x0400054D RID: 1357
	public const string ID = "FishFeeder";

	// Token: 0x0400054E RID: 1358
	private static HashSet<Tag> forbiddenTags = new HashSet<Tag>();
}
