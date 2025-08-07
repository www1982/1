using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200007C RID: 124
[EntityConfigOrder(2)]
public class EggCrackerConfig : IBuildingConfig
{
	// Token: 0x0600024E RID: 590 RVA: 0x0001037C File Offset: 0x0000E57C
	public static void RegisterEgg(Tag eggPrefabTag, string name, string description, float mass, string[] requiredDLC, string[] forbiddenDLC)
	{
		EggCrackerConfig.EggData eggData = new EggCrackerConfig.EggData(eggPrefabTag, name, description, mass, requiredDLC, forbiddenDLC);
		EggCrackerConfig.uncategorizedEggData.Add(eggData);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000103A4 File Offset: 0x0000E5A4
	public static void CategorizeEggs()
	{
		foreach (EggCrackerConfig.EggData eggData in EggCrackerConfig.uncategorizedEggData)
		{
			Tag species = Assets.GetPrefab(Assets.GetPrefab(eggData.id).GetDef<IncubationMonitor.Def>().spawnedCreature).GetComponent<CreatureBrain>().species;
			if (!EggCrackerConfig.EggsBySpecies.ContainsKey(species))
			{
				EggCrackerConfig.EggsBySpecies.Add(species, new List<EggCrackerConfig.EggData>());
			}
			EggCrackerConfig.EggsBySpecies[species].Add(eggData);
		}
	}

	// Token: 0x06000250 RID: 592 RVA: 0x00010444 File Offset: 0x0000E644
	public override BuildingDef CreateBuildingDef()
	{
		string text = "EggCracker";
		int num = 2;
		int num2 = 2;
		string text2 = "egg_cracker_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.AddSearchTerms(SEARCH_TERMS.FOOD);
		return buildingDef;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x000104C8 File Offset: 0x0000E6C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<KBatchedAnimController>().SetSymbolVisiblity("snapto_egg", false);
		ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
		complexFabricator.labelByResult = false;
		complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
		complexFabricator.duplicantOperated = true;
		go.AddOrGet<FabricatorIngredientStatusManager>();
		go.AddOrGet<CopyBuildingSettings>();
		Workable workable = go.AddOrGet<ComplexFabricatorWorkable>();
		BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
		workable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_egg_cracker_kanim") };
		complexFabricator.outputOffset = new Vector3(1f, 1f, 0f);
		Prioritizable.AddRef(go);
		go.AddOrGet<EggCracker>();
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00010579 File Offset: 0x0000E779
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00010582 File Offset: 0x0000E782
	public override void ConfigurePost(BuildingDef def)
	{
		base.ConfigurePost(def);
		this.MakeRecipes();
	}

	// Token: 0x06000254 RID: 596 RVA: 0x00010594 File Offset: 0x0000E794
	public void MakeRecipes()
	{
		EggCrackerConfig.CategorizeEggs();
		foreach (KeyValuePair<Tag, List<EggCrackerConfig.EggData>> keyValuePair in EggCrackerConfig.EggsBySpecies)
		{
			Tag[] array = new Tag[keyValuePair.Value.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = keyValuePair.Value[i].id;
			}
			EggCrackerConfig.EggData eggData = keyValuePair.Value[0];
			string text = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RESULT_DESCRIPTION, eggData.name);
			ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement(array, 1f)
				{
					material = array[0]
				}
			};
			ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[]
			{
				new ComplexRecipe.RecipeElement("RawEgg", 0.5f * eggData.mass, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false),
				new ComplexRecipe.RecipeElement("EggShell", 0.5f * eggData.mass, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature, false)
			};
			string text2 = ComplexRecipeManager.MakeObsoleteRecipeID("EggCracker", "RawEgg");
			string text3 = ComplexRecipeManager.MakeRecipeID("EggCracker", array2, array3);
			ComplexRecipe complexRecipe = new ComplexRecipe(text3, array2, array3, eggData.requiredDlcIds, eggData.forbiddenDlcIds);
			complexRecipe.description = string.Format(global::STRINGS.BUILDINGS.PREFABS.EGGCRACKER.RECIPE_DESCRIPTION, eggData.name, text);
			complexRecipe.fabricators = new List<Tag> { "EggCracker" };
			complexRecipe.time = 5f;
			complexRecipe.nameDisplay = ComplexRecipe.RecipeNameDisplay.Custom;
			complexRecipe.customName = keyValuePair.Key.ProperName();
			complexRecipe.customSpritePrefabID = ((array2[0].material != null) ? array2[0].material.Name : array2[0].possibleMaterials[0].Name);
			ComplexRecipeManager.Get().AddObsoleteIDMapping(text2, text3);
		}
	}

	// Token: 0x04000176 RID: 374
	public const string ID = "EggCracker";

	// Token: 0x04000177 RID: 375
	private static Dictionary<Tag, List<EggCrackerConfig.EggData>> EggsBySpecies = new Dictionary<Tag, List<EggCrackerConfig.EggData>>();

	// Token: 0x04000178 RID: 376
	private static List<EggCrackerConfig.EggData> uncategorizedEggData = new List<EggCrackerConfig.EggData>();

	// Token: 0x0200105A RID: 4186
	private class EggData : IHasDlcRestrictions
	{
		// Token: 0x06007FB2 RID: 32690 RVA: 0x0032C64A File Offset: 0x0032A84A
		public EggData(Tag id, string name, string description, float mass, string[] requiredDLC, string[] forbiddenDLC)
		{
			this.id = id;
			this.name = name;
			this.description = description;
			this.mass = mass;
			this.requiredDlcIds = requiredDLC;
			this.forbiddenDlcIds = forbiddenDLC;
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x0032C67F File Offset: 0x0032A87F
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x0032C687 File Offset: 0x0032A887
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x04006057 RID: 24663
		public Tag id;

		// Token: 0x04006058 RID: 24664
		public float mass;

		// Token: 0x04006059 RID: 24665
		public string name;

		// Token: 0x0400605A RID: 24666
		public string description;

		// Token: 0x0400605B RID: 24667
		public string[] requiredDlcIds;

		// Token: 0x0400605C RID: 24668
		public string[] forbiddenDlcIds;
	}
}
