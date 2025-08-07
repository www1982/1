using System;
using System.Collections.Generic;
using Database;
using Klei.AI;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000DB6 RID: 3510
public class SandboxToolParameterMenu : KScreen
{
	// Token: 0x06006E91 RID: 28305 RVA: 0x002A0A25 File Offset: 0x0029EC25
	public static void DestroyInstance()
	{
		SandboxToolParameterMenu.instance = null;
	}

	// Token: 0x06006E92 RID: 28306 RVA: 0x002A0A2D File Offset: 0x0029EC2D
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06006E93 RID: 28307 RVA: 0x002A0A34 File Offset: 0x0029EC34
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.ConfigureSettings();
		this.activateOnSpawn = true;
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06006E94 RID: 28308 RVA: 0x002A0A50 File Offset: 0x0029EC50
	private void ConfigureSettings()
	{
		this.massSlider.clampValueLow = 0.001f;
		this.massSlider.clampValueHigh = 10000f;
		this.temperatureAdditiveSlider.clampValueLow = -9999f;
		this.temperatureAdditiveSlider.clampValueHigh = 9999f;
		this.temperatureSlider.clampValueLow = -458f;
		this.temperatureSlider.clampValueHigh = 9999f;
		this.brushRadiusSlider.clampValueLow = 1f;
		this.brushRadiusSlider.clampValueHigh = 50f;
		this.diseaseCountSlider.clampValueHigh = 1000000f;
		this.diseaseCountSlider.slideMaxValue = 1000000f;
		this.settings = new SandboxSettings();
		SandboxSettings sandboxSettings = this.settings;
		sandboxSettings.OnChangeElement = (Action<bool>)Delegate.Combine(sandboxSettings.OnChangeElement, new Action<bool>(delegate(bool forceElementDefaults)
		{
			int num = this.settings.GetIntSetting("SandboxTools.SelectedElement");
			if (num >= ElementLoader.elements.Count)
			{
				num = 0;
			}
			Element element = ElementLoader.elements[num];
			this.elementSelector.button.GetComponentInChildren<LocText>().text = element.name + " (" + element.GetStateString() + ")";
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(element, "ui", false);
			this.elementSelector.button.GetComponentsInChildren<Image>()[1].sprite = uisprite.first;
			this.elementSelector.button.GetComponentsInChildren<Image>()[1].color = uisprite.second;
			this.SetAbsoluteTemperatureSliderRange(element);
			this.massSlider.SetRange(0.1f, Mathf.Min(element.maxMass * 2f, this.massSlider.clampValueHigh), false);
			if (forceElementDefaults)
			{
				this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(element.defaultValues.temperature, true), true);
				this.massSlider.SetValue(element.defaultValues.mass, true);
			}
		}));
		SandboxSettings sandboxSettings2 = this.settings;
		sandboxSettings2.OnChangeMass = (global::System.Action)Delegate.Combine(sandboxSettings2.OnChangeMass, new global::System.Action(delegate
		{
			this.massSlider.SetValue(this.settings.GetFloatSetting("SandboxTools.Mass"), false);
		}));
		SandboxSettings sandboxSettings3 = this.settings;
		sandboxSettings3.OnChangeDisease = (global::System.Action)Delegate.Combine(sandboxSettings3.OnChangeDisease, new global::System.Action(delegate
		{
			Disease disease = Db.Get().Diseases.TryGet(SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedDisease"));
			if (disease == null)
			{
				disease = Db.Get().Diseases.Get("FoodPoisoning");
			}
			this.diseaseSelector.button.GetComponentInChildren<LocText>().text = disease.Name;
			this.diseaseSelector.button.GetComponentsInChildren<Image>()[1].sprite = Assets.GetSprite("germ");
			this.diseaseCountSlider.SetRange(0f, 1000000f, false);
		}));
		SandboxSettings sandboxSettings4 = this.settings;
		sandboxSettings4.OnChangeDiseaseCount = (global::System.Action)Delegate.Combine(sandboxSettings4.OnChangeDiseaseCount, new global::System.Action(delegate
		{
			this.diseaseCountSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.DiseaseCount"), false);
		}));
		SandboxSettings sandboxSettings5 = this.settings;
		sandboxSettings5.OnChangeStory = (global::System.Action)Delegate.Combine(sandboxSettings5.OnChangeStory, new global::System.Action(delegate
		{
			string stringSetting = SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedStory");
			Story story = Db.Get().Stories.Get(stringSetting);
			if (story == null)
			{
				this.settings.ForceDefaultStringSetting("SandboxTools.SelectedStory");
				return;
			}
			this.storySelector.button.GetComponentInChildren<LocText>().text = Strings.Get(story.StoryTrait.name);
			this.storySelector.button.GetComponentsInChildren<Image>()[1].sprite = Assets.GetSprite(story.StoryTrait.icon);
		}));
		SandboxSettings sandboxSettings6 = this.settings;
		sandboxSettings6.OnChangeEntity = (global::System.Action)Delegate.Combine(sandboxSettings6.OnChangeEntity, new global::System.Action(delegate
		{
			string stringSetting2 = SandboxToolParameterMenu.instance.settings.GetStringSetting("SandboxTools.SelectedEntity");
			GameObject gameObject = Assets.TryGetPrefab(stringSetting2);
			if (gameObject == null || !Game.IsCorrectDlcActiveForCurrentSave(gameObject.GetComponent<KPrefabID>()))
			{
				this.settings.ForceDefaultStringSetting("SandboxTools.SelectedEntity");
				return;
			}
			this.entitySelector.button.GetComponentInChildren<LocText>().text = gameObject.GetProperName();
			global::Tuple<Sprite, Color> tuple;
			if (gameObject.HasTag(GameTags.BaseMinion))
			{
				tuple = new global::Tuple<Sprite, Color>(BaseMinionConfig.GetSpriteForMinionModel(gameObject.PrefabID()), Color.white);
			}
			else
			{
				tuple = Def.GetUISprite(stringSetting2, "ui", false);
			}
			if (tuple != null)
			{
				this.entitySelector.button.GetComponentsInChildren<Image>()[1].sprite = tuple.first;
				this.entitySelector.button.GetComponentsInChildren<Image>()[1].color = tuple.second;
			}
		}));
		SandboxSettings sandboxSettings7 = this.settings;
		sandboxSettings7.OnChangeBrushSize = (global::System.Action)Delegate.Combine(sandboxSettings7.OnChangeBrushSize, new global::System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is BrushTool)
			{
				(PlayerController.Instance.ActiveTool as BrushTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings8 = this.settings;
		sandboxSettings8.OnChangeNoiseScale = (global::System.Action)Delegate.Combine(sandboxSettings8.OnChangeNoiseScale, new global::System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is SandboxSprinkleTool)
			{
				(PlayerController.Instance.ActiveTool as SandboxSprinkleTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings9 = this.settings;
		sandboxSettings9.OnChangeNoiseDensity = (global::System.Action)Delegate.Combine(sandboxSettings9.OnChangeNoiseDensity, new global::System.Action(delegate
		{
			if (PlayerController.Instance.ActiveTool is SandboxSprinkleTool)
			{
				(PlayerController.Instance.ActiveTool as SandboxSprinkleTool).SetBrushSize(this.settings.GetIntSetting("SandboxTools.BrushSize"));
			}
		}));
		SandboxSettings sandboxSettings10 = this.settings;
		sandboxSettings10.OnChangeTemperature = (global::System.Action)Delegate.Combine(sandboxSettings10.OnChangeTemperature, new global::System.Action(delegate
		{
			this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.Temperature"), false), false);
		}));
		SandboxSettings sandboxSettings11 = this.settings;
		sandboxSettings11.OnChangeAdditiveTemperature = (global::System.Action)Delegate.Combine(sandboxSettings11.OnChangeAdditiveTemperature, new global::System.Action(delegate
		{
			this.temperatureAdditiveSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.TemperatureAdditive"), true), false);
		}));
		Game.Instance.Subscribe(999382396, new Action<object>(this.OnTemperatureUnitChanged));
		SandboxSettings sandboxSettings12 = this.settings;
		sandboxSettings12.OnChangeAdditiveStress = (global::System.Action)Delegate.Combine(sandboxSettings12.OnChangeAdditiveStress, new global::System.Action(delegate
		{
			this.stressAdditiveSlider.SetValue(this.settings.GetFloatSetting("SandbosTools.StressAdditive"), false);
		}));
		SandboxSettings sandboxSettings13 = this.settings;
		sandboxSettings13.OnChangeMoraleAdjustment = (global::System.Action)Delegate.Combine(sandboxSettings13.OnChangeMoraleAdjustment, new global::System.Action(delegate
		{
			this.moraleSlider.SetValue((float)this.settings.GetIntSetting("SandbosTools.MoraleAdjustment"), false);
		}));
	}

	// Token: 0x06006E95 RID: 28309 RVA: 0x002A0D20 File Offset: 0x0029EF20
	public void DisableParameters()
	{
		this.elementSelector.row.SetActive(false);
		this.entitySelector.row.SetActive(false);
		this.brushRadiusSlider.row.SetActive(false);
		this.noiseScaleSlider.row.SetActive(false);
		this.noiseDensitySlider.row.SetActive(false);
		this.massSlider.row.SetActive(false);
		this.temperatureAdditiveSlider.row.SetActive(false);
		this.temperatureSlider.row.SetActive(false);
		this.diseaseCountSlider.row.SetActive(false);
		this.diseaseSelector.row.SetActive(false);
		this.stressAdditiveSlider.row.SetActive(false);
		this.moraleSlider.row.SetActive(false);
		this.storySelector.row.SetActive(false);
	}

	// Token: 0x06006E96 RID: 28310 RVA: 0x002A0E0C File Offset: 0x0029F00C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ConfigureElementSelector();
		this.ConfigureDiseaseSelector();
		this.ConfigureEntitySelector();
		this.ConfigureStoryTraitSelector();
		this.SpawnSelector(this.entitySelector);
		this.SpawnSelector(this.elementSelector);
		this.SpawnSelector(this.storySelector);
		this.SpawnSlider(this.brushRadiusSlider);
		this.SpawnSlider(this.noiseScaleSlider);
		this.SpawnSlider(this.noiseDensitySlider);
		this.SpawnSlider(this.massSlider);
		this.SpawnSlider(this.temperatureSlider);
		this.SpawnSlider(this.temperatureAdditiveSlider);
		this.SpawnSlider(this.stressAdditiveSlider);
		this.SpawnSelector(this.diseaseSelector);
		this.SpawnSlider(this.diseaseCountSlider);
		this.SpawnSlider(this.moraleSlider);
		if (SandboxToolParameterMenu.instance == null)
		{
			SandboxToolParameterMenu.instance = this;
			base.gameObject.SetActive(false);
			this.settings.RestorePrefs();
		}
	}

	// Token: 0x06006E97 RID: 28311 RVA: 0x002A0F0C File Offset: 0x0029F10C
	private void ConfigureElementSelector()
	{
		Func<object, bool> func = (object element) => (element as Element).IsSolid;
		Func<object, bool> func2 = (object element) => (element as Element).IsLiquid;
		Func<object, bool> func3 = (object element) => (element as Element).IsGas;
		List<Element> commonElements = new List<Element>();
		Func<object, bool> func4 = (object element) => commonElements.Contains(element as Element);
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Oxygen));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Water));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Vacuum));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Dirt));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.SandStone));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Cuprite));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Steel));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Algae));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.CrudeOil));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.CarbonDioxide));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Sand));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.SlimeMold));
		commonElements.Insert(0, ElementLoader.FindElementByHash(SimHashes.Granite));
		List<Element> list = new List<Element>();
		foreach (Element element2 in ElementLoader.elements)
		{
			if (!element2.disabled)
			{
				bool flag = false;
				Tag[] oreTags = element2.oreTags;
				for (int i = 0; i < oreTags.Length; i++)
				{
					if (oreTags[i] == GameTags.HideFromSpawnTool)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(element2);
				}
			}
		}
		list.Sort((Element a, Element b) => a.name.CompareTo(b.name));
		object[] array = list.ToArray();
		this.elementSelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object element)
		{
			this.settings.SetIntSetting("SandboxTools.SelectedElement", (int)((Element)element).idx);
		}, (object element) => (element as Element).name + " (" + (element as Element).GetStateString() + ")", (string filterString, object option) => ((option as Element).name.ToUpper() + (option as Element).GetStateString().ToUpper()).Contains(filterString.ToUpper()), (object element) => Def.GetUISprite(element as Element, "ui", false), UI.SANDBOXTOOLS.SETTINGS.ELEMENT.NAME, new SandboxToolParameterMenu.SelectorValue.SearchFilter[]
		{
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.COMMON, func4, null, null),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.SOLID, func, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.SandStone), "ui", false)),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.LIQUID, func2, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.Water), "ui", false)),
			new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.GAS, func3, null, Def.GetUISprite(ElementLoader.FindElementByHash(SimHashes.Oxygen), "ui", false))
		});
	}

	// Token: 0x06006E98 RID: 28312 RVA: 0x002A129C File Offset: 0x0029F49C
	private void ConfigureEntitySelector()
	{
		List<SandboxToolParameterMenu.SelectorValue.SearchFilter> list = new List<SandboxToolParameterMenu.SelectorValue.SearchFilter>();
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.FOOD, delegate(object entity)
		{
			KPrefabID kprefabID2 = entity as KPrefabID;
			string idString = kprefabID2.PrefabID().ToString();
			return (!kprefabID2.HasTag(GameTags.Egg) && EdiblesManager.GetAllFoodTypes().Find((EdiblesManager.FoodInfo match) => match.Id == idString) != null) || kprefabID2.HasTag(GameTags.Dehydrated);
		}, null, Def.GetUISprite(Assets.GetPrefab("MushBar"), "ui", false));
		list.Add(searchFilter);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter2 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.COMETS, delegate(object entity)
		{
			KPrefabID kprefabID3 = entity as KPrefabID;
			return kprefabID3.HasTag(GameTags.Comet) && Game.IsCorrectDlcActiveForCurrentSave(kprefabID3);
		}, null, Def.GetUISprite(Assets.GetPrefab(CopperCometConfig.ID), "ui", false));
		list.Add(searchFilter2);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter3 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.SPECIAL, delegate(object entity)
		{
			KPrefabID kprefabID4 = entity as KPrefabID;
			return (entity as KPrefabID).HasTag(GameTags.BaseMinion) && Game.IsCorrectDlcActiveForCurrentSave(kprefabID4);
		}, null, new global::Tuple<Sprite, Color>(BaseMinionConfig.GetSpriteForMinionModel(GameTags.Minions.Models.Standard), Color.white));
		list.Add(searchFilter3);
		if (Game.IsDlcActiveForCurrentSave("DLC3_ID"))
		{
			SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter4 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.BIONICUPGRADES, delegate(object entity)
			{
				KPrefabID kprefabID5 = entity as KPrefabID;
				return kprefabID5.HasTag(GameTags.BionicUpgrade) && Game.IsCorrectDlcActiveForCurrentSave(kprefabID5);
			}, null, new global::Tuple<Sprite, Color>(Def.GetUISpriteFromMultiObjectAnim(Assets.GetAnim("upgrade_disc_kanim"), "ui", false, ""), Color.white));
			list.Add(searchFilter4);
		}
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter5 = null;
		searchFilter5 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.CREATURE, (object entity) => false, null, Def.GetUISprite(Assets.GetPrefab("Hatch"), "ui", false));
		list.Add(searchFilter5);
		List<Tag> list2 = new List<Tag>();
		foreach (GameObject gameObject in Assets.GetPrefabsWithTag(GameTags.CreatureBrain))
		{
			CreatureBrain brain = gameObject.GetComponent<CreatureBrain>();
			if (!list2.Contains(brain.species) && Game.IsCorrectDlcActiveForCurrentSave(brain.GetComponent<KPrefabID>()))
			{
				global::Tuple<Sprite, Color> tuple = null;
				CodexEntry codexEntry;
				if (CodexCache.entries.TryGetValue(brain.species.ToString().ToUpper(), out codexEntry))
				{
					tuple = new global::Tuple<Sprite, Color>(codexEntry.icon, codexEntry.iconColor);
				}
				list2.Add(brain.species);
				SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter6 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(Strings.Get("STRINGS.CREATURES.FAMILY_PLURAL." + brain.species.ToString().ToUpper()), delegate(object entity)
				{
					CreatureBrain component = Assets.GetPrefab((entity as KPrefabID).PrefabID()).GetComponent<CreatureBrain>();
					return (entity as KPrefabID).HasTag(GameTags.CreatureBrain) && component.species == brain.species;
				}, searchFilter5, tuple);
				list.Add(searchFilter6);
			}
		}
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter7 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.CREATURE_EGG, (object entity) => (entity as KPrefabID).HasTag(GameTags.Egg), searchFilter5, Def.GetUISprite(Assets.GetPrefab("HatchEgg"), "ui", false));
		list.Add(searchFilter7);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter8 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.EQUIPMENT, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject2 = (entity as KPrefabID).gameObject;
			return gameObject2 != null && gameObject2.GetComponent<Equippable>() != null;
		}, null, Def.GetUISprite(Assets.GetPrefab("Funky_Vest"), "ui", false));
		list.Add(searchFilter8);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter9 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.PLANTS, delegate(object entity)
		{
			KPrefabID kprefabID6 = entity as KPrefabID;
			return !(kprefabID6 == null) && !(kprefabID6.gameObject == null) && (kprefabID6 != null && Game.IsCorrectDlcActiveForCurrentSave(kprefabID6)) && (kprefabID6.GetComponent<Harvestable>() != null || kprefabID6.GetComponent<WiltCondition>() != null);
		}, null, Def.GetUISprite(Assets.GetPrefab("PrickleFlower"), "ui", false));
		list.Add(searchFilter9);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter10 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.SEEDS, delegate(object entity)
		{
			if ((entity as KPrefabID).gameObject == null)
			{
				return false;
			}
			GameObject gameObject3 = (entity as KPrefabID).gameObject;
			return gameObject3 != null && gameObject3.GetComponent<PlantableSeed>() != null;
		}, searchFilter9, Def.GetUISprite(Assets.GetPrefab("PrickleFlowerSeed"), "ui", false));
		list.Add(searchFilter10);
		SandboxToolParameterMenu.SelectorValue.SearchFilter searchFilter11 = new SandboxToolParameterMenu.SelectorValue.SearchFilter(UI.SANDBOXTOOLS.FILTERS.ENTITIES.INDUSTRIAL_PRODUCTS, delegate(object entity)
		{
			KPrefabID kprefabID7 = entity as KPrefabID;
			return !(kprefabID7 == null) && !(kprefabID7.gameObject == null) && !kprefabID7.HasTag(GameTags.DeprecatedContent) && Game.IsCorrectDlcActiveForCurrentSave(kprefabID7) && (kprefabID7.HasTag(GameTags.IndustrialIngredient) || kprefabID7.HasTag(GameTags.IndustrialProduct) || kprefabID7.HasTag(GameTags.Medicine) || kprefabID7.HasTag(GameTags.MedicalSupplies) || kprefabID7.HasTag(GameTags.ChargedPortableBattery));
		}, null, Def.GetUISprite(Assets.GetPrefab("BasicCure"), "ui", false));
		list.Add(searchFilter11);
		List<KPrefabID> list3 = new List<KPrefabID>();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			if (Game.IsCorrectDlcActiveForCurrentSave(kprefabID) && !kprefabID.HasTag(GameTags.HideFromSpawnTool))
			{
				using (List<SandboxToolParameterMenu.SelectorValue.SearchFilter>.Enumerator enumerator3 = list.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						if (enumerator3.Current.condition(kprefabID))
						{
							list3.Add(kprefabID);
							break;
						}
					}
				}
			}
		}
		object[] array = list3.ToArray();
		this.entitySelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object entity)
		{
			this.settings.SetStringSetting("SandboxTools.SelectedEntity", (entity as KPrefabID).PrefabID().Name);
		}, (object entity) => (entity as KPrefabID).GetProperName(), null, delegate(object entity)
		{
			GameObject prefab = Assets.GetPrefab((entity as KPrefabID).PrefabTag);
			KPrefabID component2 = prefab.GetComponent<KPrefabID>();
			if (prefab != null)
			{
				if (component2.HasTag(GameTags.BaseMinion))
				{
					return new global::Tuple<Sprite, Color>(BaseMinionConfig.GetSpriteForMinionModel((entity as KPrefabID).PrefabID()), Color.white);
				}
				KBatchedAnimController component3 = prefab.GetComponent<KBatchedAnimController>();
				if (component3 != null && component3.AnimFiles.Length != 0 && component3.AnimFiles[0] != null)
				{
					return Def.GetUISprite(prefab, "ui", false);
				}
			}
			return null;
		}, UI.SANDBOXTOOLS.SETTINGS.SPAWN_ENTITY.NAME, list.ToArray());
	}

	// Token: 0x06006E99 RID: 28313 RVA: 0x002A187C File Offset: 0x0029FA7C
	private void ConfigureStoryTraitSelector()
	{
		object[] array = Db.Get().Stories.resources.ToArray();
		this.storySelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object story)
		{
			this.settings.SetStringSetting("SandboxTools.SelectedStory", ((Story)story).Id);
		}, (object story) => Strings.Get((story as Story).StoryTrait.name), null, (object story) => new global::Tuple<Sprite, Color>(Assets.GetSprite(((Story)story).StoryTrait.icon), Color.white), UI.SANDBOXTOOLS.SETTINGS.SPAWN_STORY_TRAIT.NAME, null);
	}

	// Token: 0x06006E9A RID: 28314 RVA: 0x002A1900 File Offset: 0x0029FB00
	private void ConfigureDiseaseSelector()
	{
		object[] array = Db.Get().Diseases.resources.ToArray();
		this.diseaseSelector = new SandboxToolParameterMenu.SelectorValue(array, delegate(object disease)
		{
			this.settings.SetStringSetting("SandboxTools.SelectedDisease", ((Disease)disease).Id);
		}, (object disease) => (disease as Disease).Name, null, (object disease) => new global::Tuple<Sprite, Color>(Assets.GetSprite("germ"), GlobalAssets.Instance.colorSet.GetColorByName((disease as Disease).overlayColourName)), UI.SANDBOXTOOLS.SETTINGS.DISEASE.NAME, null);
	}

	// Token: 0x06006E9B RID: 28315 RVA: 0x002A1984 File Offset: 0x0029FB84
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		if (PlayerController.Instance.ActiveTool != null && SandboxToolParameterMenu.instance != null)
		{
			this.RefreshDisplay();
		}
	}

	// Token: 0x06006E9C RID: 28316 RVA: 0x002A19B4 File Offset: 0x0029FBB4
	public void RefreshDisplay()
	{
		this.brushRadiusSlider.row.SetActive(PlayerController.Instance.ActiveTool is BrushTool);
		if (PlayerController.Instance.ActiveTool is BrushTool)
		{
			this.brushRadiusSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.BrushSize"), true);
		}
		this.massSlider.SetValue(this.settings.GetFloatSetting("SandboxTools.Mass"), true);
		this.stressAdditiveSlider.SetValue(this.settings.GetFloatSetting("SandbosTools.StressAdditive"), true);
		this.RefreshTemperatureUnitDisplays();
		this.temperatureSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.Temperature"), true), true);
		this.temperatureAdditiveSlider.SetValue(GameUtil.GetConvertedTemperature(this.settings.GetFloatSetting("SandbosTools.TemperatureAdditive"), true), true);
		this.diseaseCountSlider.SetValue((float)this.settings.GetIntSetting("SandboxTools.DiseaseCount"), true);
		this.moraleSlider.SetValue((float)this.settings.GetIntSetting("SandbosTools.MoraleAdjustment"), true);
	}

	// Token: 0x06006E9D RID: 28317 RVA: 0x002A1AD0 File Offset: 0x0029FCD0
	private void OnTemperatureUnitChanged(object unit)
	{
		int num = this.settings.GetIntSetting("SandboxTools.SelectedElement");
		if (num >= ElementLoader.elements.Count)
		{
			num = 0;
		}
		Element element = ElementLoader.elements[num];
		this.SetAbsoluteTemperatureSliderRange(element);
		this.temperatureAdditiveSlider.SetValue(5f, true);
	}

	// Token: 0x06006E9E RID: 28318 RVA: 0x002A1B24 File Offset: 0x0029FD24
	private void SetAbsoluteTemperatureSliderRange(Element element)
	{
		float num = Mathf.Max(element.lowTemp - 10f, 1f);
		float num2;
		if (element.IsGas)
		{
			num2 = Mathf.Min(new float[]
			{
				9999f,
				element.highTemp + 10f,
				element.defaultValues.temperature + 100f
			});
		}
		else
		{
			num2 = Mathf.Min(9999f, element.highTemp + 10f);
		}
		num = GameUtil.GetConvertedTemperature(num, true);
		num2 = GameUtil.GetConvertedTemperature(num2, true);
		this.temperatureSlider.SetRange(num, num2, false);
	}

	// Token: 0x06006E9F RID: 28319 RVA: 0x002A1BC0 File Offset: 0x0029FDC0
	private void RefreshTemperatureUnitDisplays()
	{
		this.temperatureSlider.unitString = GameUtil.GetTemperatureUnitSuffix();
		this.temperatureSlider.row.GetComponent<HierarchyReferences>().GetReference<LocText>("UnitLabel").text = this.temperatureSlider.unitString;
		this.temperatureAdditiveSlider.unitString = GameUtil.GetTemperatureUnitSuffix();
		this.temperatureAdditiveSlider.row.GetComponent<HierarchyReferences>().GetReference<LocText>("UnitLabel").text = this.temperatureSlider.unitString;
	}

	// Token: 0x06006EA0 RID: 28320 RVA: 0x002A1C44 File Offset: 0x0029FE44
	private GameObject SpawnSelector(SandboxToolParameterMenu.SelectorValue selector)
	{
		GameObject gameObject = Util.KInstantiateUI(this.selectorPropertyPrefab, base.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		GameObject panel = component.GetReference("ScrollPanel").gameObject;
		GameObject gameObject2 = component.GetReference("Content").gameObject;
		InputField filterInputField = component.GetReference<InputField>("Filter");
		component.GetReference<LocText>("Label").SetText(selector.labelText);
		Game.Instance.Subscribe(1174281782, delegate(object data)
		{
			if (panel.activeSelf)
			{
				panel.SetActive(false);
			}
		});
		KButton reference = component.GetReference<KButton>("Button");
		reference.onClick += delegate
		{
			panel.SetActive(!panel.activeSelf);
			if (panel.activeSelf)
			{
				panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
				filterInputField.ActivateInputField();
				filterInputField.onValueChanged.Invoke(filterInputField.text);
			}
		};
		GameObject gameObject3 = component.GetReference("optionPrefab").gameObject;
		selector.row = gameObject;
		selector.optionButtons = new List<KeyValuePair<object, GameObject>>();
		GameObject clearFilterButton = Util.KInstantiateUI(gameObject3, gameObject2, false);
		clearFilterButton.GetComponentInChildren<LocText>().text = UI.SANDBOXTOOLS.FILTERS.BACK;
		clearFilterButton.GetComponentsInChildren<Image>()[1].enabled = false;
		clearFilterButton.GetComponent<KButton>().onClick += delegate
		{
			selector.currentFilter = null;
			selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
			{
				if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter)
				{
					test.Value.SetActive((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null);
					return;
				}
				test.Value.SetActive(false);
			});
			clearFilterButton.SetActive(false);
			panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
			filterInputField.text = "";
			filterInputField.onValueChanged.Invoke(filterInputField.text);
		};
		if (selector.filters != null)
		{
			SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters = selector.filters;
			for (int i = 0; i < filters.Length; i++)
			{
				SandboxToolParameterMenu.SelectorValue.SearchFilter filter = filters[i];
				GameObject gameObject4 = Util.KInstantiateUI(gameObject3, gameObject2, false);
				gameObject4.SetActive(filter.parentFilter == null);
				gameObject4.GetComponentInChildren<LocText>().text = filter.Name;
				if (filter.icon != null)
				{
					gameObject4.GetComponentsInChildren<Image>()[1].sprite = filter.icon.first;
					gameObject4.GetComponentsInChildren<Image>()[1].color = filter.icon.second;
				}
				Action<KeyValuePair<object, GameObject>> <>9__6;
				gameObject4.GetComponent<KButton>().onClick += delegate
				{
					selector.currentFilter = filter;
					clearFilterButton.SetActive(true);
					List<KeyValuePair<object, GameObject>> optionButtons = selector.optionButtons;
					Action<KeyValuePair<object, GameObject>> action;
					if ((action = <>9__6) == null)
					{
						action = (<>9__6 = delegate(KeyValuePair<object, GameObject> test)
						{
							if (!(test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter))
							{
								test.Value.SetActive(selector.runCurrentFilter(test.Key));
								return;
							}
							if ((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null)
							{
								test.Value.SetActive(false);
								return;
							}
							test.Value.SetActive((test.Key as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == filter);
						});
					}
					optionButtons.ForEach(action);
					panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
				};
				selector.optionButtons.Add(new KeyValuePair<object, GameObject>(filter, gameObject4));
			}
		}
		object[] options = selector.options;
		for (int i = 0; i < options.Length; i++)
		{
			object option = options[i];
			GameObject gameObject5 = Util.KInstantiateUI(gameObject3, gameObject2, true);
			gameObject5.GetComponentInChildren<LocText>().text = selector.getOptionName(option);
			gameObject5.GetComponent<KButton>().onClick += delegate
			{
				selector.onValueChanged(option);
				panel.SetActive(false);
			};
			global::Tuple<Sprite, Color> tuple = selector.getOptionSprite(option);
			gameObject5.GetComponentsInChildren<Image>()[1].sprite = tuple.first;
			gameObject5.GetComponentsInChildren<Image>()[1].color = tuple.second;
			selector.optionButtons.Add(new KeyValuePair<object, GameObject>(option, gameObject5));
			if (option is SandboxToolParameterMenu.SelectorValue.SearchFilter)
			{
				gameObject5.SetActive((option as SandboxToolParameterMenu.SelectorValue.SearchFilter).parentFilter == null);
			}
			else
			{
				gameObject5.SetActive(false);
			}
		}
		selector.button = reference;
		filterInputField.onValueChanged.AddListener(delegate(string filterString)
		{
			if (!clearFilterButton.activeSelf && !string.IsNullOrEmpty(filterString))
			{
				clearFilterButton.SetActive(true);
			}
			new List<KeyValuePair<object, GameObject>>();
			bool flag = selector.optionButtons.Find((KeyValuePair<object, GameObject> match) => match.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter).Key != null;
			if (string.IsNullOrEmpty(filterString))
			{
				if (!flag)
				{
					selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
					{
						test.Value.SetActive(true);
					});
				}
				else
				{
					selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
					{
						if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter && ((SandboxToolParameterMenu.SelectorValue.SearchFilter)test.Key).parentFilter == null)
						{
							test.Value.SetActive(true);
							return;
						}
						test.Value.SetActive(false);
					});
				}
			}
			else
			{
				selector.optionButtons.ForEach(delegate(KeyValuePair<object, GameObject> test)
				{
					if (test.Key is SandboxToolParameterMenu.SelectorValue.SearchFilter)
					{
						test.Value.SetActive(((SandboxToolParameterMenu.SelectorValue.SearchFilter)test.Key).Name.ToUpper().Contains(filterString.ToUpper()));
						return;
					}
					test.Value.SetActive(selector.getOptionName(test.Key).ToUpper().Contains(filterString.ToUpper()));
				});
			}
			if (selector.filterOptionFunction != null)
			{
				object[] options2 = selector.options;
				for (int j = 0; j < options2.Length; j++)
				{
					object option = options2[j];
					foreach (KeyValuePair<object, GameObject> keyValuePair in selector.optionButtons.FindAll((KeyValuePair<object, GameObject> match) => match.Key == option))
					{
						if (string.IsNullOrEmpty(filterString))
						{
							keyValuePair.Value.SetActive(false);
						}
						else
						{
							keyValuePair.Value.SetActive(selector.filterOptionFunction(filterString, option));
						}
					}
				}
			}
			panel.GetComponent<KScrollRect>().verticalNormalizedPosition = 1f;
		});
		this.inputFields.Add(filterInputField.gameObject);
		panel.SetActive(false);
		return gameObject;
	}

	// Token: 0x06006EA1 RID: 28321 RVA: 0x002A201C File Offset: 0x002A021C
	private GameObject SpawnSlider(SandboxToolParameterMenu.SliderValue value)
	{
		GameObject gameObject = Util.KInstantiateUI(this.sliderPropertyPrefab, base.gameObject, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		component.GetReference<Image>("BottomIcon").sprite = Assets.GetSprite(value.bottomSprite);
		component.GetReference<Image>("TopIcon").sprite = Assets.GetSprite(value.topSprite);
		component.GetReference<LocText>("Label").SetText(value.labelText);
		KSlider slider = component.GetReference<KSlider>("Slider");
		KNumberInputField inputField = component.GetReference<KNumberInputField>("InputField");
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(value.tooltip);
		slider.minValue = value.slideMinValue;
		slider.maxValue = value.slideMaxValue;
		inputField.minValue = value.clampValueLow;
		inputField.maxValue = value.clampValueHigh;
		this.inputFields.Add(inputField.gameObject);
		value.slider = slider;
		inputField.decimalPlaces = value.roundToDecimalPlaces;
		value.inputField = inputField;
		value.row = gameObject;
		slider.onReleaseHandle += delegate
		{
			float num = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num;
			inputField.currentValue = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		slider.onDrag += delegate
		{
			float num2 = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num2;
			inputField.currentValue = num2;
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		slider.onMove += delegate
		{
			float num3 = Mathf.Round(slider.value * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			slider.value = num3;
			inputField.currentValue = num3;
			inputField.SetDisplayValue(inputField.currentValue.ToString());
			if (value.onValueChanged != null)
			{
				value.onValueChanged(slider.value);
			}
		};
		inputField.onEndEdit += delegate
		{
			float num4 = inputField.currentValue;
			num4 = Mathf.Round(num4 * Mathf.Pow(10f, (float)value.roundToDecimalPlaces)) / Mathf.Pow(10f, (float)value.roundToDecimalPlaces);
			inputField.SetDisplayValue(num4.ToString());
			slider.value = num4;
			if (value.onValueChanged != null)
			{
				value.onValueChanged(num4);
			}
		};
		component.GetReference<LocText>("UnitLabel").text = value.unitString;
		return gameObject;
	}

	// Token: 0x06006EA2 RID: 28322 RVA: 0x002A2217 File Offset: 0x002A0417
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.CheckBlockedInput())
		{
			if (!e.Consumed)
			{
				e.Consumed = true;
				return;
			}
		}
		else
		{
			base.OnKeyDown(e);
		}
	}

	// Token: 0x06006EA3 RID: 28323 RVA: 0x002A2238 File Offset: 0x002A0438
	private bool CheckBlockedInput()
	{
		bool flag = false;
		if (global::UnityEngine.EventSystems.EventSystem.current != null)
		{
			GameObject currentSelectedGameObject = global::UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
			if (currentSelectedGameObject != null)
			{
				foreach (GameObject gameObject in this.inputFields)
				{
					if (currentSelectedGameObject == gameObject.gameObject)
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x04004C0B RID: 19467
	public static SandboxToolParameterMenu instance;

	// Token: 0x04004C0C RID: 19468
	public SandboxSettings settings;

	// Token: 0x04004C0D RID: 19469
	[SerializeField]
	private GameObject sliderPropertyPrefab;

	// Token: 0x04004C0E RID: 19470
	[SerializeField]
	private GameObject selectorPropertyPrefab;

	// Token: 0x04004C0F RID: 19471
	private List<GameObject> inputFields = new List<GameObject>();

	// Token: 0x04004C10 RID: 19472
	public SandboxToolParameterMenu.SelectorValue elementSelector;

	// Token: 0x04004C11 RID: 19473
	public SandboxToolParameterMenu.SliderValue brushRadiusSlider = new SandboxToolParameterMenu.SliderValue(1f, 10f, "dash", "circle_hard", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_SIZE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_SIZE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.BrushSize", Mathf.Clamp(Mathf.RoundToInt(value), 1, 50));
	}, 0);

	// Token: 0x04004C12 RID: 19474
	public SandboxToolParameterMenu.SliderValue noiseScaleSlider = new SandboxToolParameterMenu.SliderValue(0f, 1f, "little", "lots", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.NoiseScale", value);
	}, 2);

	// Token: 0x04004C13 RID: 19475
	public SandboxToolParameterMenu.SliderValue noiseDensitySlider = new SandboxToolParameterMenu.SliderValue(1f, 20f, "little", "lots", "", UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_SCALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.BRUSH_NOISE_DENSITY.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.NoiseDensity", value);
	}, 2);

	// Token: 0x04004C14 RID: 19476
	public SandboxToolParameterMenu.SliderValue massSlider = new SandboxToolParameterMenu.SliderValue(0.1f, 1000f, "action_pacify", "status_item_plant_solid", UI.UNITSUFFIXES.MASS.KILOGRAM, UI.SANDBOXTOOLS.SETTINGS.MASS.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.MASS.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandboxTools.Mass", Mathf.Clamp(value, 0.001f, 9999f));
	}, 2);

	// Token: 0x04004C15 RID: 19477
	public SandboxToolParameterMenu.SliderValue temperatureSlider = new SandboxToolParameterMenu.SliderValue(150f, 500f, "cold", "hot", GameUtil.GetTemperatureUnitSuffix(), UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.Temperature", Mathf.Clamp(GameUtil.GetTemperatureConvertedToKelvin(value), 1f, 9999f));
	}, 0);

	// Token: 0x04004C16 RID: 19478
	public SandboxToolParameterMenu.SliderValue temperatureAdditiveSlider = new SandboxToolParameterMenu.SliderValue(-15f, 15f, "cold", "hot", GameUtil.GetTemperatureUnitSuffix(), UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE_ADDITIVE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.TEMPERATURE_ADDITIVE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.TemperatureAdditive", GameUtil.GetTemperatureConvertedToKelvin(value));
	}, 0);

	// Token: 0x04004C17 RID: 19479
	public SandboxToolParameterMenu.SliderValue stressAdditiveSlider = new SandboxToolParameterMenu.SliderValue(-10f, 10f, "little", "lots", UI.UNITSUFFIXES.PERCENT, UI.SANDBOXTOOLS.SETTINGS.STRESS_ADDITIVE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.STRESS_ADDITIVE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetFloatSetting("SandbosTools.StressAdditive", value);
	}, 0);

	// Token: 0x04004C18 RID: 19480
	public SandboxToolParameterMenu.SliderValue moraleSlider = new SandboxToolParameterMenu.SliderValue(-25f, 25f, "little", "lots", UI.UNITSUFFIXES.UNITS, UI.SANDBOXTOOLS.SETTINGS.MORALE.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.MORALE.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandbosTools.MoraleAdjustment", Mathf.RoundToInt(value));
	}, 0);

	// Token: 0x04004C19 RID: 19481
	public SandboxToolParameterMenu.SelectorValue diseaseSelector;

	// Token: 0x04004C1A RID: 19482
	public SandboxToolParameterMenu.SliderValue diseaseCountSlider = new SandboxToolParameterMenu.SliderValue(0f, 10000f, "status_item_barren", "germ", UI.UNITSUFFIXES.DISEASE.UNITS, UI.SANDBOXTOOLS.SETTINGS.DISEASE_COUNT.TOOLTIP, UI.SANDBOXTOOLS.SETTINGS.DISEASE_COUNT.NAME, delegate(float value)
	{
		SandboxToolParameterMenu.instance.settings.SetIntSetting("SandboxTools.DiseaseCount", Mathf.RoundToInt(value));
	}, 0);

	// Token: 0x04004C1B RID: 19483
	public SandboxToolParameterMenu.SelectorValue entitySelector;

	// Token: 0x04004C1C RID: 19484
	public SandboxToolParameterMenu.SelectorValue storySelector;

	// Token: 0x02001FCC RID: 8140
	public class SelectorValue
	{
		// Token: 0x0600B43A RID: 46138 RVA: 0x003DC2E4 File Offset: 0x003DA4E4
		public SelectorValue(object[] options, Action<object> onValueChanged, Func<object, string> getOptionName, Func<string, object, bool> filterOptionFunction, Func<object, global::Tuple<Sprite, Color>> getOptionSprite, string labelText, SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters = null)
		{
			this.options = options;
			this.onValueChanged = onValueChanged;
			this.getOptionName = getOptionName;
			this.filterOptionFunction = filterOptionFunction;
			this.getOptionSprite = getOptionSprite;
			this.filters = filters;
			this.labelText = labelText;
		}

		// Token: 0x0600B43B RID: 46139 RVA: 0x003DC337 File Offset: 0x003DA537
		public bool runCurrentFilter(object obj)
		{
			return this.currentFilter == null || this.currentFilter.condition(obj);
		}

		// Token: 0x0400920D RID: 37389
		public GameObject row;

		// Token: 0x0400920E RID: 37390
		public List<KeyValuePair<object, GameObject>> optionButtons;

		// Token: 0x0400920F RID: 37391
		public KButton button;

		// Token: 0x04009210 RID: 37392
		public object[] options;

		// Token: 0x04009211 RID: 37393
		public Action<object> onValueChanged;

		// Token: 0x04009212 RID: 37394
		public Func<object, string> getOptionName;

		// Token: 0x04009213 RID: 37395
		public Func<string, object, bool> filterOptionFunction;

		// Token: 0x04009214 RID: 37396
		public Func<object, global::Tuple<Sprite, Color>> getOptionSprite;

		// Token: 0x04009215 RID: 37397
		public SandboxToolParameterMenu.SelectorValue.SearchFilter[] filters;

		// Token: 0x04009216 RID: 37398
		public List<SandboxToolParameterMenu.SelectorValue.SearchFilter> activeFilters = new List<SandboxToolParameterMenu.SelectorValue.SearchFilter>();

		// Token: 0x04009217 RID: 37399
		public SandboxToolParameterMenu.SelectorValue.SearchFilter currentFilter;

		// Token: 0x04009218 RID: 37400
		public string labelText;

		// Token: 0x02002908 RID: 10504
		public class SearchFilter
		{
			// Token: 0x0600CE24 RID: 52772 RVA: 0x0041EC26 File Offset: 0x0041CE26
			public SearchFilter(string Name, Func<object, bool> condition, SandboxToolParameterMenu.SelectorValue.SearchFilter parentFilter = null, global::Tuple<Sprite, Color> icon = null)
			{
				this.Name = Name;
				this.condition = condition;
				this.parentFilter = parentFilter;
				this.icon = icon;
			}

			// Token: 0x0400B591 RID: 46481
			public string Name;

			// Token: 0x0400B592 RID: 46482
			public Func<object, bool> condition;

			// Token: 0x0400B593 RID: 46483
			public SandboxToolParameterMenu.SelectorValue.SearchFilter parentFilter;

			// Token: 0x0400B594 RID: 46484
			public global::Tuple<Sprite, Color> icon;
		}
	}

	// Token: 0x02001FCD RID: 8141
	public class SliderValue
	{
		// Token: 0x0600B43C RID: 46140 RVA: 0x003DC35C File Offset: 0x003DA55C
		public SliderValue(float slideMinValue, float slideMaxValue, string bottomSprite, string topSprite, string unitString, string tooltip, string labelText, Action<float> onValueChanged, int decimalPlaces = 0)
		{
			this.slideMinValue = slideMinValue;
			this.slideMaxValue = slideMaxValue;
			this.bottomSprite = bottomSprite;
			this.topSprite = topSprite;
			this.unitString = unitString;
			this.onValueChanged = onValueChanged;
			this.tooltip = tooltip;
			this.roundToDecimalPlaces = decimalPlaces;
			this.labelText = labelText;
			this.clampValueLow = slideMinValue;
			this.clampValueHigh = slideMaxValue;
		}

		// Token: 0x0600B43D RID: 46141 RVA: 0x003DC3C4 File Offset: 0x003DA5C4
		public void SetRange(float min, float max, bool resetCurrentValue = true)
		{
			this.slideMinValue = min;
			this.slideMaxValue = max;
			this.slider.minValue = this.slideMinValue;
			this.slider.maxValue = this.slideMaxValue;
			this.inputField.currentValue = this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f;
			this.inputField.SetDisplayValue(this.inputField.currentValue.ToString());
			if (resetCurrentValue)
			{
				this.slider.value = this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f;
				this.onValueChanged(this.slideMinValue + (this.slideMaxValue - this.slideMinValue) / 2f);
			}
		}

		// Token: 0x0600B43E RID: 46142 RVA: 0x003DC490 File Offset: 0x003DA690
		public void SetValue(float value, bool runOnValueChanged = true)
		{
			value = Mathf.Clamp(value, this.clampValueLow, this.clampValueHigh);
			this.slider.value = value;
			this.inputField.currentValue = value;
			if (runOnValueChanged)
			{
				this.onValueChanged(value);
			}
			this.RefreshDisplay();
		}

		// Token: 0x0600B43F RID: 46143 RVA: 0x003DC4E0 File Offset: 0x003DA6E0
		public void RefreshDisplay()
		{
			this.inputField.SetDisplayValue(((this.roundToDecimalPlaces == 0) ? ((float)Mathf.RoundToInt(this.inputField.currentValue)) : this.inputField.currentValue).ToString());
		}

		// Token: 0x04009219 RID: 37401
		public GameObject row;

		// Token: 0x0400921A RID: 37402
		public string bottomSprite;

		// Token: 0x0400921B RID: 37403
		public string topSprite;

		// Token: 0x0400921C RID: 37404
		public float slideMinValue;

		// Token: 0x0400921D RID: 37405
		public float slideMaxValue;

		// Token: 0x0400921E RID: 37406
		public float clampValueLow;

		// Token: 0x0400921F RID: 37407
		public float clampValueHigh;

		// Token: 0x04009220 RID: 37408
		public string unitString;

		// Token: 0x04009221 RID: 37409
		public Action<float> onValueChanged;

		// Token: 0x04009222 RID: 37410
		public string tooltip;

		// Token: 0x04009223 RID: 37411
		public int roundToDecimalPlaces;

		// Token: 0x04009224 RID: 37412
		public string labelText;

		// Token: 0x04009225 RID: 37413
		public KSlider slider;

		// Token: 0x04009226 RID: 37414
		public KNumberInputField inputField;
	}
}
