using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200023A RID: 570
public class GravitasCreatureManipulatorConfig : IBuildingConfig
{
	// Token: 0x06000B7D RID: 2941 RVA: 0x00045DF0 File Offset: 0x00043FF0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasCreatureManipulator";
		int num = 3;
		int num2 = 4;
		string text2 = "gravitas_critter_manipulator_kanim";
		int num3 = 250;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 3200f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.Floodable = false;
		buildingDef.Entombable = true;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "medium";
		buildingDef.ForegroundLayer = Grid.SceneLayer.Ground;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x00045E8C File Offset: 0x0004408C
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		PrimaryElement component = go.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Steel, true);
		component.Temperature = 294.15f;
		BuildingTemplates.ExtendBuildingToGravitas(go);
		go.AddComponent<Storage>();
		Activatable activatable = go.AddComponent<Activatable>();
		activatable.synchronizeAnims = false;
		activatable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };
		activatable.SetWorkTime(30f);
		GravitasCreatureManipulator.Def def = go.AddOrGetDef<GravitasCreatureManipulator.Def>();
		def.pickupOffset = new CellOffset(-1, 0);
		def.dropOffset = new CellOffset(1, 0);
		def.numSpeciesToUnlockMorphMode = 5;
		def.workingDuration = 15f;
		def.cooldownDuration = 540f;
		MakeBaseSolid.Def def2 = go.AddOrGetDef<MakeBaseSolid.Def>();
		def2.solidOffsets = new CellOffset[4];
		for (int i = 0; i < 4; i++)
		{
			def2.solidOffsets[i] = new CellOffset(0, i);
		}
		go.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject game_object)
		{
			game_object.GetComponent<Activatable>().SetOffsets(OffsetGroups.LeftOrRight);
		};
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x00045FA4 File Offset: 0x000441A4
	public static Option<string> GetBodyContentForSpeciesTag(Tag species)
	{
		Option<string> nameForSpeciesTag = GravitasCreatureManipulatorConfig.GetNameForSpeciesTag(species);
		Option<string> descriptionForSpeciesTag = GravitasCreatureManipulatorConfig.GetDescriptionForSpeciesTag(species);
		if (nameForSpeciesTag.HasValue && descriptionForSpeciesTag.HasValue)
		{
			return GravitasCreatureManipulatorConfig.GetBodyContent(nameForSpeciesTag.Value, descriptionForSpeciesTag.Value);
		}
		return Option.None;
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x00045FF4 File Offset: 0x000441F4
	public static string GetBodyContentForUnknownSpecies()
	{
		return GravitasCreatureManipulatorConfig.GetBodyContent(CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.SPECIES_ENTRIES.UNKNOWN_TITLE, CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.SPECIES_ENTRIES.UNKNOWN);
	}

	// Token: 0x06000B81 RID: 2945 RVA: 0x0004600F File Offset: 0x0004420F
	public static string GetBodyContent(string name, string desc)
	{
		return "<size=125%><b>" + name + "</b></size><line-height=150%>\n</line-height>" + desc;
	}

	// Token: 0x06000B82 RID: 2946 RVA: 0x00046024 File Offset: 0x00044224
	public static Option<string> GetNameForSpeciesTag(Tag species)
	{
		StringEntry stringEntry;
		if (!Strings.TryGet("STRINGS.CREATURES.FAMILY_PLURAL." + species.ToString().ToUpper(), out stringEntry))
		{
			return Option.None;
		}
		return Option.Some<string>(stringEntry);
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0004606C File Offset: 0x0004426C
	public static Option<string> GetDescriptionForSpeciesTag(Tag species)
	{
		StringEntry stringEntry;
		if (!Strings.TryGet("STRINGS.CODEX.STORY_TRAITS.CRITTER_MANIPULATOR.SPECIES_ENTRIES." + species.ToString().ToUpper().Replace("SPECIES", ""), out stringEntry))
		{
			return Option.None;
		}
		return Option.Some<string>(stringEntry);
	}

	// Token: 0x040007EE RID: 2030
	public const string ID = "GravitasCreatureManipulator";

	// Token: 0x040007EF RID: 2031
	public const string CODEX_ENTRY_ID = "STORYTRAITCRITTERMANIPULATOR";

	// Token: 0x040007F0 RID: 2032
	public const string INITIAL_LORE_UNLOCK_ID = "story_trait_critter_manipulator_initial";

	// Token: 0x040007F1 RID: 2033
	public const string PARKING_LORE_UNLOCK_ID = "story_trait_critter_manipulator_parking";

	// Token: 0x040007F2 RID: 2034
	public const string COMPLETED_LORE_UNLOCK_ID = "story_trait_critter_manipulator_complete";

	// Token: 0x040007F3 RID: 2035
	private const int HEIGHT = 4;

	// Token: 0x02001191 RID: 4497
	public static class CRITTER_LORE_UNLOCK_ID
	{
		// Token: 0x060082F4 RID: 33524 RVA: 0x0033310B File Offset: 0x0033130B
		public static string For(Tag species)
		{
			return "story_trait_critter_manipulator_" + species.ToString().ToLower();
		}
	}
}
