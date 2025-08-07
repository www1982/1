using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Database;
using Klei.AI;
using ProcGen;
using TUNING;
using UnityEngine;

// Token: 0x020008CF RID: 2255
public class EconomyDetails
{
	// Token: 0x06003E8B RID: 16011 RVA: 0x0015DDAC File Offset: 0x0015BFAC
	public EconomyDetails()
	{
		this.massResourceType = new EconomyDetails.Resource.Type("Mass", "kg");
		this.heatResourceType = new EconomyDetails.Resource.Type("Heat Energy", "kdtu");
		this.energyResourceType = new EconomyDetails.Resource.Type("Energy", "joules");
		this.timeResourceType = new EconomyDetails.Resource.Type("Time", "seconds");
		this.attributeResourceType = new EconomyDetails.Resource.Type("Attribute", "units");
		this.caloriesResourceType = new EconomyDetails.Resource.Type("Calories", "kcal");
		this.amountResourceType = new EconomyDetails.Resource.Type("Amount", "units");
		this.buildingTransformationType = new EconomyDetails.Transformation.Type("Building");
		this.foodTransformationType = new EconomyDetails.Transformation.Type("Food");
		this.plantTransformationType = new EconomyDetails.Transformation.Type("Plant");
		this.creatureTransformationType = new EconomyDetails.Transformation.Type("Creature");
		this.dupeTransformationType = new EconomyDetails.Transformation.Type("Duplicant");
		this.referenceTransformationType = new EconomyDetails.Transformation.Type("Reference");
		this.effectTransformationType = new EconomyDetails.Transformation.Type("Effect");
		this.geyserActivePeriodTransformationType = new EconomyDetails.Transformation.Type("GeyserActivePeriod");
		this.geyserLifetimeTransformationType = new EconomyDetails.Transformation.Type("GeyserLifetime");
		this.energyResource = this.CreateResource(TagManager.Create("Energy"), this.energyResourceType);
		this.heatResource = this.CreateResource(TagManager.Create("Heat"), this.heatResourceType);
		this.duplicantTimeResource = this.CreateResource(TagManager.Create("DupeTime"), this.timeResourceType);
		this.caloriesResource = this.CreateResource(new Tag(Db.Get().Amounts.Calories.deltaAttribute.Id), this.caloriesResourceType);
		this.fixedCaloriesResource = this.CreateResource(new Tag(Db.Get().Amounts.Calories.Id), this.caloriesResourceType);
		foreach (Element element in ElementLoader.elements)
		{
			this.CreateResource(element);
		}
		foreach (Tag tag in new List<Tag>
		{
			GameTags.CombustibleLiquid,
			GameTags.CombustibleGas,
			GameTags.CombustibleSolid
		})
		{
			this.CreateResource(tag, this.massResourceType);
		}
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			this.CreateResource(foodInfo.Id.ToTag(), this.amountResourceType);
		}
		this.GatherStartingBiomeAmounts();
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			this.CreateTransformation(kprefabID, kprefabID.PrefabTag);
			if (kprefabID.GetComponent<GeyserConfigurator>() != null)
			{
				KPrefabID kprefabID2 = kprefabID;
				Tag prefabTag = kprefabID.PrefabTag;
				this.CreateTransformation(kprefabID2, prefabTag.ToString() + "_ActiveOnly");
			}
		}
		foreach (Effect effect in Db.Get().effects.resources)
		{
			this.CreateTransformation(effect);
		}
		EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(TagManager.Create("Duplicant"), this.dupeTransformationType, 1f, false);
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_USED_PER_SECOND));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), DUPLICANTSTATS.STANDARD.BaseStats.OXYGEN_USED_PER_SECOND * Assets.GetPrefab(MinionConfig.ID).GetComponent<OxygenBreather>().O2toCO2conversion));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, 0.875f));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, DUPLICANTSTATS.STANDARD.BaseStats.GUESSTIMATE_CALORIES_BURNED_PER_SECOND * 0.001f));
		transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), DUPLICANTSTATS.STANDARD.BaseStats.BLADDER_INCREASE_PER_SECOND));
		this.transformations.Add(transformation);
		EconomyDetails.Transformation transformation2 = new EconomyDetails.Transformation(TagManager.Create("Electrolysis"), this.referenceTransformationType, 1f, false);
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), 1.7777778f));
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Hydrogen), 0.22222222f));
		transformation2.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Water), -2f));
		this.transformations.Add(transformation2);
		EconomyDetails.Transformation transformation3 = new EconomyDetails.Transformation(TagManager.Create("MethaneCombustion"), this.referenceTransformationType, 1f, false);
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Methane), -1f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -4f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), 2.75f));
		transformation3.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Water), 2.25f));
		this.transformations.Add(transformation3);
		EconomyDetails.Transformation transformation4 = new EconomyDetails.Transformation(TagManager.Create("CoalCombustion"), this.referenceTransformationType, 1f, false);
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Carbon), -1f));
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.Oxygen), -2.6666667f));
		transformation4.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(GameTags.CarbonDioxide), 3.6666667f));
		this.transformations.Add(transformation4);
	}

	// Token: 0x06003E8C RID: 16012 RVA: 0x0015E430 File Offset: 0x0015C630
	private static void WriteProduct(StreamWriter o, string a, string b)
	{
		o.Write(string.Concat(new string[] { "\"=PRODUCT(", a, ", ", b, ")\"" }));
	}

	// Token: 0x06003E8D RID: 16013 RVA: 0x0015E463 File Offset: 0x0015C663
	private static void WriteProduct(StreamWriter o, string a, string b, string c)
	{
		o.Write(string.Concat(new string[] { "\"=PRODUCT(", a, ", ", b, ", ", c, ")\"" }));
	}

	// Token: 0x06003E8E RID: 16014 RVA: 0x0015E4A4 File Offset: 0x0015C6A4
	public void DumpTransformations(EconomyDetails.Scenario scenario, StreamWriter o)
	{
		List<EconomyDetails.Resource> used_resources = new List<EconomyDetails.Resource>();
		foreach (EconomyDetails.Transformation transformation in this.transformations)
		{
			if (scenario.IncludesTransformation(transformation))
			{
				foreach (EconomyDetails.Transformation.Delta delta in transformation.deltas)
				{
					if (!used_resources.Contains(delta.resource))
					{
						used_resources.Add(delta.resource);
					}
				}
			}
		}
		used_resources.Sort((EconomyDetails.Resource x, EconomyDetails.Resource y) => x.tag.Name.CompareTo(y.tag.Name));
		List<EconomyDetails.Ratio> list = new List<EconomyDetails.Ratio>();
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Algae), this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.energyResource, this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Oxygen), this.energyResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Water), this.GetResource(GameTags.Oxygen), false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.DirtyWater), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Water), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.GetResource(GameTags.Fertilizer), this.caloriesResource, false));
		list.Add(new EconomyDetails.Ratio(this.energyResource, this.CreateResource(new Tag(Db.Get().Amounts.Stress.deltaAttribute.Id), this.amountResourceType), true));
		list.RemoveAll((EconomyDetails.Ratio x) => !used_resources.Contains(x.input) || !used_resources.Contains(x.output));
		o.Write("Id");
		o.Write(",Count");
		o.Write(",Type");
		o.Write(",Time(s)");
		int num = 4;
		foreach (EconomyDetails.Resource resource in used_resources)
		{
			o.Write(string.Concat(new string[]
			{
				", ",
				resource.tag.Name,
				"(",
				resource.type.unit,
				")"
			}));
			num++;
		}
		o.Write(",MassDelta");
		foreach (EconomyDetails.Ratio ratio in list)
		{
			o.Write(string.Concat(new string[]
			{
				", ",
				ratio.output.tag.Name,
				"(",
				ratio.output.type.unit,
				")/",
				ratio.input.tag.Name,
				"(",
				ratio.input.type.unit,
				")"
			}));
			num++;
		}
		string text = "B";
		o.Write("\n");
		int num2 = 1;
		this.transformations.Sort((EconomyDetails.Transformation x, EconomyDetails.Transformation y) => x.tag.Name.CompareTo(y.tag.Name));
		for (int i = 0; i < this.transformations.Count; i++)
		{
			EconomyDetails.Transformation transformation2 = this.transformations[i];
			if (scenario.IncludesTransformation(transformation2))
			{
				num2++;
			}
		}
		string text2 = "B" + (num2 + 4).ToString();
		int num3 = 1;
		for (int j = 0; j < this.transformations.Count; j++)
		{
			EconomyDetails.Transformation transformation3 = this.transformations[j];
			if (scenario.IncludesTransformation(transformation3))
			{
				if (transformation3.tag == new Tag(EconomyDetails.debugTag))
				{
					int num4 = 0 + 1;
				}
				num3++;
				o.Write("\"" + transformation3.tag.Name + "\"");
				o.Write("," + scenario.GetCount(transformation3.tag).ToString());
				o.Write(",\"" + transformation3.type.id + "\"");
				if (!transformation3.timeInvariant)
				{
					o.Write(",\"" + transformation3.timeInSeconds.ToString("0.00") + "\"");
				}
				else
				{
					o.Write(",\"invariant\"");
				}
				string text3 = text + num3.ToString();
				float num5 = 0f;
				bool flag = false;
				foreach (EconomyDetails.Resource resource2 in used_resources)
				{
					EconomyDetails.Transformation.Delta delta2 = null;
					foreach (EconomyDetails.Transformation.Delta delta3 in transformation3.deltas)
					{
						if (delta3.resource.tag == resource2.tag)
						{
							delta2 = delta3;
							break;
						}
					}
					o.Write(",");
					if (delta2 != null && delta2.amount != 0f)
					{
						if (delta2.resource.type == this.massResourceType)
						{
							flag = true;
							num5 += delta2.amount;
						}
						if (!transformation3.timeInvariant)
						{
							EconomyDetails.WriteProduct(o, text3, (delta2.amount / transformation3.timeInSeconds).ToString("0.00000"), text2);
						}
						else
						{
							EconomyDetails.WriteProduct(o, text3, delta2.amount.ToString("0.00000"));
						}
					}
				}
				o.Write(",");
				if (flag)
				{
					num5 /= transformation3.timeInSeconds;
					EconomyDetails.WriteProduct(o, text3, num5.ToString("0.00000"), text2);
				}
				foreach (EconomyDetails.Ratio ratio2 in list)
				{
					o.Write(", ");
					EconomyDetails.Transformation.Delta delta4 = transformation3.GetDelta(ratio2.input);
					EconomyDetails.Transformation.Delta delta5 = transformation3.GetDelta(ratio2.output);
					if (delta5 != null && delta4 != null && delta4.amount < 0f && (delta5.amount > 0f || ratio2.allowNegativeOutput))
					{
						o.Write(delta5.amount / Mathf.Abs(delta4.amount));
					}
				}
				o.Write("\n");
			}
		}
		int num6 = 4;
		for (int k = 0; k < num; k++)
		{
			if (k >= num6 && k < num6 + used_resources.Count)
			{
				string text4 = ((char)(65 + k % 26)).ToString();
				int num7 = Mathf.FloorToInt((float)k / 26f);
				if (num7 > 0)
				{
					text4 = ((char)(65 + num7 - 1)).ToString() + text4;
				}
				o.Write(string.Concat(new string[]
				{
					"\"=SUM(",
					text4,
					"2: ",
					text4,
					num2.ToString(),
					")\""
				}));
			}
			o.Write(",");
		}
		string text5 = "B" + (num2 + 5).ToString();
		o.Write("\n");
		o.Write("\nTiming:");
		o.Write("\nTimeInSeconds:," + scenario.timeInSeconds.ToString());
		o.Write("\nSecondsPerCycle:," + 600f.ToString());
		o.Write("\nCycles:,=" + text2 + "/" + text5);
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x0015EDD0 File Offset: 0x0015CFD0
	public EconomyDetails.Resource CreateResource(Tag tag, EconomyDetails.Resource.Type resource_type)
	{
		foreach (EconomyDetails.Resource resource in this.resources)
		{
			if (resource.tag == tag)
			{
				return resource;
			}
		}
		EconomyDetails.Resource resource2 = new EconomyDetails.Resource(tag, resource_type);
		this.resources.Add(resource2);
		return resource2;
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x0015EE48 File Offset: 0x0015D048
	public EconomyDetails.Resource CreateResource(Element element)
	{
		return this.CreateResource(element.tag, this.massResourceType);
	}

	// Token: 0x06003E91 RID: 16017 RVA: 0x0015EE5C File Offset: 0x0015D05C
	public EconomyDetails.Transformation CreateTransformation(Effect effect)
	{
		EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(new Tag(effect.Id), this.effectTransformationType, 1f, false);
		foreach (AttributeModifier attributeModifier in effect.SelfModifiers)
		{
			EconomyDetails.Resource resource = this.CreateResource(new Tag(attributeModifier.AttributeId), this.attributeResourceType);
			transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource, attributeModifier.Value));
		}
		this.transformations.Add(transformation);
		return transformation;
	}

	// Token: 0x06003E92 RID: 16018 RVA: 0x0015EEFC File Offset: 0x0015D0FC
	public EconomyDetails.Transformation GetTransformation(Tag tag)
	{
		foreach (EconomyDetails.Transformation transformation in this.transformations)
		{
			if (transformation.tag == tag)
			{
				return transformation;
			}
		}
		return null;
	}

	// Token: 0x06003E93 RID: 16019 RVA: 0x0015EF60 File Offset: 0x0015D160
	public EconomyDetails.Transformation CreateTransformation(KPrefabID prefab_id, Tag tag)
	{
		if (tag == new Tag(EconomyDetails.debugTag))
		{
			int num = 0 + 1;
		}
		Building component = prefab_id.GetComponent<Building>();
		ElementConverter component2 = prefab_id.GetComponent<ElementConverter>();
		EnergyConsumer component3 = prefab_id.GetComponent<EnergyConsumer>();
		ElementConsumer component4 = prefab_id.GetComponent<ElementConsumer>();
		BuildingElementEmitter component5 = prefab_id.GetComponent<BuildingElementEmitter>();
		Generator component6 = prefab_id.GetComponent<Generator>();
		EnergyGenerator component7 = prefab_id.GetComponent<EnergyGenerator>();
		ManualGenerator component8 = prefab_id.GetComponent<ManualGenerator>();
		ManualDeliveryKG[] components = prefab_id.GetComponents<ManualDeliveryKG>();
		StateMachineController component9 = prefab_id.GetComponent<StateMachineController>();
		Edible component10 = prefab_id.GetComponent<Edible>();
		Crop component11 = prefab_id.GetComponent<Crop>();
		Uprootable component12 = prefab_id.GetComponent<Uprootable>();
		ComplexRecipe complexRecipe = ComplexRecipeManager.Get().recipes.Find((ComplexRecipe r) => r.FirstResult == prefab_id.PrefabTag);
		List<FertilizationMonitor.Def> list = null;
		List<IrrigationMonitor.Def> list2 = null;
		GeyserConfigurator component13 = prefab_id.GetComponent<GeyserConfigurator>();
		Toilet component14 = prefab_id.GetComponent<Toilet>();
		FlushToilet component15 = prefab_id.GetComponent<FlushToilet>();
		RelaxationPoint component16 = prefab_id.GetComponent<RelaxationPoint>();
		CreatureCalorieMonitor.Def def = prefab_id.gameObject.GetDef<CreatureCalorieMonitor.Def>();
		if (component9 != null)
		{
			list = component9.GetDefs<FertilizationMonitor.Def>();
			list2 = component9.GetDefs<IrrigationMonitor.Def>();
		}
		EconomyDetails.Transformation transformation = null;
		float num2 = 1f;
		if (component10 != null)
		{
			transformation = new EconomyDetails.Transformation(tag, this.foodTransformationType, num2, complexRecipe != null);
		}
		else if (component2 != null || component3 != null || component4 != null || component5 != null || component6 != null || component7 != null || component12 != null || component13 != null || component14 != null || component15 != null || component16 != null || def != null)
		{
			if (component12 != null || component11 != null)
			{
				if (component11 != null)
				{
					num2 = component11.cropVal.cropDuration;
				}
				transformation = new EconomyDetails.Transformation(tag, this.plantTransformationType, num2, false);
			}
			else if (def != null)
			{
				transformation = new EconomyDetails.Transformation(tag, this.creatureTransformationType, num2, false);
			}
			else if (component13 != null)
			{
				GeyserConfigurator.GeyserInstanceConfiguration geyserInstanceConfiguration = new GeyserConfigurator.GeyserInstanceConfiguration
				{
					typeId = component13.presetType,
					rateRoll = 0.5f,
					iterationLengthRoll = 0.5f,
					iterationPercentRoll = 0.5f,
					yearLengthRoll = 0.5f,
					yearPercentRoll = 0.5f
				};
				if (tag.Name.Contains("_ActiveOnly"))
				{
					float iterationLength = geyserInstanceConfiguration.GetIterationLength();
					transformation = new EconomyDetails.Transformation(tag, this.geyserActivePeriodTransformationType, iterationLength, false);
				}
				else
				{
					float yearLength = geyserInstanceConfiguration.GetYearLength();
					transformation = new EconomyDetails.Transformation(tag, this.geyserLifetimeTransformationType, yearLength, false);
				}
			}
			else
			{
				if (component14 != null || component15 != null)
				{
					num2 = 600f;
				}
				transformation = new EconomyDetails.Transformation(tag, this.buildingTransformationType, num2, false);
			}
		}
		if (transformation != null)
		{
			if (component2 != null && component2.consumedElements != null)
			{
				foreach (ElementConverter.ConsumedElement consumedElement in component2.consumedElements)
				{
					EconomyDetails.Resource resource = this.CreateResource(consumedElement.Tag, this.massResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource, -consumedElement.MassConsumptionRate));
				}
				if (component2.outputElements != null)
				{
					foreach (ElementConverter.OutputElement outputElement in component2.outputElements)
					{
						Element element = ElementLoader.FindElementByHash(outputElement.elementHash);
						EconomyDetails.Resource resource2 = this.CreateResource(element.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource2, outputElement.massGenerationRate));
					}
				}
			}
			if (component4 != null && component7 == null && (component2 == null || prefab_id.GetComponent<AlgaeHabitat>() != null))
			{
				EconomyDetails.Resource resource3 = this.GetResource(ElementLoader.FindElementByHash(component4.elementToConsume).tag);
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource3, -component4.consumptionRate));
			}
			if (component3 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.energyResource, -component3.WattsNeededWhenActive));
			}
			if (component5 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(component5.element), component5.emitRate));
			}
			if (component6 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.energyResource, component6.GetComponent<Building>().Def.GeneratorWattageRating));
			}
			if (component7 != null)
			{
				if (component7.formula.inputs != null)
				{
					foreach (EnergyGenerator.InputItem inputItem in component7.formula.inputs)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(inputItem.tag), -inputItem.consumptionRate));
					}
				}
				if (component7.formula.outputs != null)
				{
					foreach (EnergyGenerator.OutputItem outputItem in component7.formula.outputs)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(outputItem.element), outputItem.creationRate));
					}
				}
			}
			if (component)
			{
				BuildingDef def2 = component.Def;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.heatResource, def2.SelfHeatKilowattsWhenActive + def2.ExhaustKilowattsWhenActive));
			}
			if (component8)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, -1f));
			}
			if (component10)
			{
				EdiblesManager.FoodInfo foodInfo = component10.FoodInfo;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.fixedCaloriesResource, foodInfo.CaloriesPerUnit * 0.001f));
				ComplexRecipeManager.Get().recipes.Find((ComplexRecipe a) => a.FirstResult == tag);
			}
			if (component11 != null)
			{
				EconomyDetails.Resource resource4 = this.CreateResource(TagManager.Create(component11.cropVal.cropId), this.amountResourceType);
				float num3 = (float)component11.cropVal.numProduced;
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource4, num3));
				GameObject prefab = Assets.GetPrefab(new Tag(component11.cropVal.cropId));
				if (prefab != null)
				{
					Edible component17 = prefab.GetComponent<Edible>();
					if (component17 != null)
					{
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, component17.FoodInfo.CaloriesPerUnit * num3 * 0.001f));
					}
				}
			}
			if (complexRecipe != null)
			{
				foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
				{
					this.CreateResource(recipeElement.material, this.amountResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(recipeElement.material), -recipeElement.amount));
				}
				foreach (ComplexRecipe.RecipeElement recipeElement2 in complexRecipe.results)
				{
					this.CreateResource(recipeElement2.material, this.amountResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(recipeElement2.material), recipeElement2.amount));
				}
			}
			if (components != null)
			{
				for (int j = 0; j < components.Length; j++)
				{
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.duplicantTimeResource, -0.1f * transformation.timeInSeconds));
				}
			}
			if (list != null && list.Count > 0)
			{
				foreach (FertilizationMonitor.Def def3 in list)
				{
					foreach (PlantElementAbsorber.ConsumeInfo consumeInfo in def3.consumedElements)
					{
						EconomyDetails.Resource resource5 = this.CreateResource(consumeInfo.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource5, -consumeInfo.massConsumptionRate * transformation.timeInSeconds));
					}
				}
			}
			if (list2 != null && list2.Count > 0)
			{
				foreach (IrrigationMonitor.Def def4 in list2)
				{
					foreach (PlantElementAbsorber.ConsumeInfo consumeInfo2 in def4.consumedElements)
					{
						EconomyDetails.Resource resource6 = this.CreateResource(consumeInfo2.tag, this.massResourceType);
						transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource6, -consumeInfo2.massConsumptionRate * transformation.timeInSeconds));
					}
				}
			}
			if (component13 != null)
			{
				GeyserConfigurator.GeyserInstanceConfiguration geyserInstanceConfiguration2 = new GeyserConfigurator.GeyserInstanceConfiguration
				{
					typeId = component13.presetType,
					rateRoll = 0.5f,
					iterationLengthRoll = 0.5f,
					iterationPercentRoll = 0.5f,
					yearLengthRoll = 0.5f,
					yearPercentRoll = 0.5f
				};
				if (tag.Name.Contains("_ActiveOnly"))
				{
					float num4 = geyserInstanceConfiguration2.GetMassPerCycle() / 600f * geyserInstanceConfiguration2.GetIterationLength();
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(geyserInstanceConfiguration2.GetElement().CreateTag(), this.massResourceType), num4));
				}
				else
				{
					float num5 = geyserInstanceConfiguration2.GetMassPerCycle() / 600f * geyserInstanceConfiguration2.GetYearLength() * geyserInstanceConfiguration2.GetYearPercent();
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(geyserInstanceConfiguration2.GetElement().CreateTag(), this.massResourceType), num5));
				}
			}
			if (component14 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), -DUPLICANTSTATS.STANDARD.BaseStats.BLADDER_INCREASE_PER_SECOND));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.Dirt), -component14.solidWastePerUse.mass));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(component14.solidWastePerUse.elementID), component14.solidWastePerUse.mass));
			}
			if (component15 != null)
			{
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(Db.Get().Amounts.Bladder.deltaAttribute.Id), this.amountResourceType), -DUPLICANTSTATS.STANDARD.BaseStats.BLADDER_INCREASE_PER_SECOND));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.Water), -component15.massConsumedPerUse));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.GetResource(SimHashes.DirtyWater), component15.massEmittedPerUse));
			}
			if (component16 != null)
			{
				foreach (AttributeModifier attributeModifier in component16.CreateEffect().SelfModifiers)
				{
					EconomyDetails.Resource resource7 = this.CreateResource(new Tag(attributeModifier.AttributeId), this.attributeResourceType);
					transformation.AddDelta(new EconomyDetails.Transformation.Delta(resource7, attributeModifier.Value));
				}
			}
			if (def != null)
			{
				this.CollectDietTransformations(prefab_id);
			}
			this.transformations.Add(transformation);
		}
		return transformation;
	}

	// Token: 0x06003E94 RID: 16020 RVA: 0x0015FB60 File Offset: 0x0015DD60
	private void CollectDietTransformations(KPrefabID prefab_id)
	{
		Trait trait = Db.Get().traits.Get(prefab_id.GetComponent<Modifiers>().initialTraits[0]);
		CreatureCalorieMonitor.Def def = prefab_id.gameObject.GetDef<CreatureCalorieMonitor.Def>();
		WildnessMonitor.Def def2 = prefab_id.gameObject.GetDef<WildnessMonitor.Def>();
		List<AttributeModifier> list = new List<AttributeModifier>();
		list.AddRange(trait.SelfModifiers);
		list.AddRange(def2.tameEffect.SelfModifiers);
		float num = 0f;
		float num2 = 0f;
		foreach (AttributeModifier attributeModifier in list)
		{
			if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.maxAttribute.Id)
			{
				num = attributeModifier.Value;
			}
			if (attributeModifier.AttributeId == Db.Get().Amounts.Calories.deltaAttribute.Id)
			{
				num2 = attributeModifier.Value;
			}
		}
		foreach (Diet.Info info in def.diet.infos)
		{
			foreach (Tag tag in info.consumedTags)
			{
				float num3 = Mathf.Abs(num / num2);
				float num4 = num / info.caloriesPerKg;
				float num5 = num4 * info.producedConversionRate;
				EconomyDetails.Transformation transformation = new EconomyDetails.Transformation(new Tag(prefab_id.PrefabTag.Name + "Diet" + tag.Name), this.creatureTransformationType, num3, false);
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(tag, this.massResourceType), -num4));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.CreateResource(new Tag(info.producedElement.ToString()), this.massResourceType), num5));
				transformation.AddDelta(new EconomyDetails.Transformation.Delta(this.caloriesResource, num));
				this.transformations.Add(transformation);
			}
		}
	}

	// Token: 0x06003E95 RID: 16021 RVA: 0x0015FDA8 File Offset: 0x0015DFA8
	private static void CollectDietScenarios(List<EconomyDetails.Scenario> scenarios)
	{
		EconomyDetails.Scenario scenario = new EconomyDetails.Scenario("diets/all", 0f, null);
		foreach (KPrefabID kprefabID in Assets.Prefabs)
		{
			CreatureCalorieMonitor.Def def = kprefabID.gameObject.GetDef<CreatureCalorieMonitor.Def>();
			if (def != null)
			{
				EconomyDetails.Scenario scenario2 = new EconomyDetails.Scenario("diets/" + kprefabID.name, 0f, null);
				Diet.Info[] infos = def.diet.infos;
				for (int i = 0; i < infos.Length; i++)
				{
					foreach (Tag tag in infos[i].consumedTags)
					{
						Tag tag2 = kprefabID.PrefabTag.Name + "Diet" + tag.Name;
						scenario2.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
						scenario.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
					}
				}
				scenarios.Add(scenario2);
			}
		}
		scenarios.Add(scenario);
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x0015FEF8 File Offset: 0x0015E0F8
	public void GatherStartingBiomeAmounts()
	{
		for (int i = 0; i < Grid.CellCount; i++)
		{
			if (global::World.Instance.zoneRenderData.worldZoneTypes[i] == SubWorld.ZoneType.Sandstone)
			{
				Element element = Grid.Element[i];
				float num = 0f;
				this.startingBiomeAmounts.TryGetValue(element, out num);
				this.startingBiomeAmounts[element] = num + Grid.Mass[i];
				this.startingBiomeCellCount++;
			}
		}
	}

	// Token: 0x06003E97 RID: 16023 RVA: 0x0015FF6D File Offset: 0x0015E16D
	public EconomyDetails.Resource GetResource(SimHashes element)
	{
		return this.GetResource(ElementLoader.FindElementByHash(element).tag);
	}

	// Token: 0x06003E98 RID: 16024 RVA: 0x0015FF80 File Offset: 0x0015E180
	public EconomyDetails.Resource GetResource(Tag tag)
	{
		foreach (EconomyDetails.Resource resource in this.resources)
		{
			if (resource.tag == tag)
			{
				return resource;
			}
		}
		DebugUtil.LogErrorArgs(new object[] { "Found a tag without a matching resource!", tag });
		return null;
	}

	// Token: 0x06003E99 RID: 16025 RVA: 0x00160000 File Offset: 0x0015E200
	private float GetDupeBreathingPerSecond(EconomyDetails details)
	{
		return details.GetTransformation(TagManager.Create("Duplicant")).GetDelta(details.GetResource(GameTags.Oxygen)).amount;
	}

	// Token: 0x06003E9A RID: 16026 RVA: 0x00160028 File Offset: 0x0015E228
	private EconomyDetails.BiomeTransformation CreateBiomeTransformationFromTransformation(EconomyDetails details, Tag transformation_tag, Tag input_resource_tag, Tag output_resource_tag)
	{
		EconomyDetails.Resource resource = details.GetResource(input_resource_tag);
		EconomyDetails.Resource resource2 = details.GetResource(output_resource_tag);
		EconomyDetails.Transformation transformation = details.GetTransformation(transformation_tag);
		float num = transformation.GetDelta(resource2).amount / -transformation.GetDelta(resource).amount;
		float num2 = this.GetDupeBreathingPerSecond(details) * 600f;
		return new EconomyDetails.BiomeTransformation((transformation_tag.Name + input_resource_tag.Name + "Cycles").ToTag(), resource, num / -num2);
	}

	// Token: 0x06003E9B RID: 16027 RVA: 0x001600A0 File Offset: 0x0015E2A0
	private static void DumpEconomyDetails()
	{
		global::Debug.Log("Starting Economy Details Dump...");
		EconomyDetails details = new EconomyDetails();
		List<EconomyDetails.Scenario> list = new List<EconomyDetails.Scenario>();
		EconomyDetails.Scenario scenario = new EconomyDetails.Scenario("default", 1f, (EconomyDetails.Transformation t) => true);
		list.Add(scenario);
		EconomyDetails.Scenario scenario2 = new EconomyDetails.Scenario("all_buildings", 1f, (EconomyDetails.Transformation t) => t.type == details.buildingTransformationType);
		list.Add(scenario2);
		EconomyDetails.Scenario scenario3 = new EconomyDetails.Scenario("all_plants", 1f, (EconomyDetails.Transformation t) => t.type == details.plantTransformationType);
		list.Add(scenario3);
		EconomyDetails.Scenario scenario4 = new EconomyDetails.Scenario("all_creatures", 1f, (EconomyDetails.Transformation t) => t.type == details.creatureTransformationType);
		list.Add(scenario4);
		EconomyDetails.Scenario scenario5 = new EconomyDetails.Scenario("all_stress", 1f, (EconomyDetails.Transformation t) => t.GetDelta(details.GetResource(new Tag(Db.Get().Amounts.Stress.deltaAttribute.Id))) != null);
		list.Add(scenario5);
		EconomyDetails.Scenario scenario6 = new EconomyDetails.Scenario("all_foods", 1f, (EconomyDetails.Transformation t) => t.type == details.foodTransformationType);
		list.Add(scenario6);
		EconomyDetails.Scenario scenario7 = new EconomyDetails.Scenario("geysers/geysers_active_period_only", 1f, (EconomyDetails.Transformation t) => t.type == details.geyserActivePeriodTransformationType);
		list.Add(scenario7);
		EconomyDetails.Scenario scenario8 = new EconomyDetails.Scenario("geysers/geysers_whole_lifetime", 1f, (EconomyDetails.Transformation t) => t.type == details.geyserLifetimeTransformationType);
		list.Add(scenario8);
		EconomyDetails.Scenario scenario9 = new EconomyDetails.Scenario("oxygen/algae_distillery", 0f, null);
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("AlgaeDistillery"), 3f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("AlgaeHabitat"), 22f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 9f));
		scenario9.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		list.Add(scenario9);
		EconomyDetails.Scenario scenario10 = new EconomyDetails.Scenario("oxygen/algae_habitat_electrolyzer", 0f, null);
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("AlgaeHabitat", 1f));
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("Duplicant", 1f));
		scenario10.AddEntry(new EconomyDetails.Scenario.Entry("Electrolyzer", 1f));
		list.Add(scenario10);
		EconomyDetails.Scenario scenario11 = new EconomyDetails.Scenario("oxygen/electrolyzer", 0f, null);
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 9f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario11.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		list.Add(scenario11);
		EconomyDetails.Scenario scenario12 = new EconomyDetails.Scenario("purifiers/methane_generator", 0f, null);
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MethaneGenerator"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FertilizerMaker"), 3f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 2f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario12.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PrickleFlower"), 0f));
		list.Add(scenario12);
		EconomyDetails.Scenario scenario13 = new EconomyDetails.Scenario("purifiers/water_purifier", 0f, null);
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Compost"), 2f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Electrolyzer"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 2f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("HydrogenGenerator"), 1f));
		scenario13.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PrickleFlower"), 29f));
		list.Add(scenario13);
		EconomyDetails.Scenario scenario14 = new EconomyDetails.Scenario("energy/petroleum_generator", 0f, null);
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("PetroleumGenerator"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("OilRefinery"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("GasPump"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("CO2Scrubber"), 1f));
		scenario14.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MethaneGenerator"), 1f));
		list.Add(scenario14);
		EconomyDetails.Scenario scenario15 = new EconomyDetails.Scenario("energy/coal_generator", 0f, (EconomyDetails.Transformation t) => t.tag.Name.Contains("Hatch"));
		scenario15.AddEntry(new EconomyDetails.Scenario.Entry("Generator", 1f));
		list.Add(scenario15);
		EconomyDetails.Scenario scenario16 = new EconomyDetails.Scenario("waste/outhouse", 0f, null);
		scenario16.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Outhouse"), 1f));
		scenario16.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Compost"), 1f));
		list.Add(scenario16);
		EconomyDetails.Scenario scenario17 = new EconomyDetails.Scenario("stress/massage_table", 0f, null);
		scenario17.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("MassageTable"), 1f));
		scenario17.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("ManualGenerator"), 1f));
		list.Add(scenario17);
		EconomyDetails.Scenario scenario18 = new EconomyDetails.Scenario("waste/flush_toilet", 0f, null);
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FlushToilet"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("WaterPurifier"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("LiquidPump"), 1f));
		scenario18.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("FertilizerMaker"), 1f));
		list.Add(scenario18);
		EconomyDetails.CollectDietScenarios(list);
		foreach (EconomyDetails.Transformation transformation in details.transformations)
		{
			EconomyDetails.Transformation transformation_iter = transformation;
			EconomyDetails.Scenario scenario19 = new EconomyDetails.Scenario("transformations/" + transformation.tag.Name, 1f, (EconomyDetails.Transformation t) => transformation_iter == t);
			list.Add(scenario19);
		}
		foreach (EconomyDetails.Transformation transformation2 in details.transformations)
		{
			EconomyDetails.Scenario scenario20 = new EconomyDetails.Scenario("transformation_groups/" + transformation2.tag.Name, 0f, null);
			scenario20.AddEntry(new EconomyDetails.Scenario.Entry(transformation2.tag, 1f));
			foreach (EconomyDetails.Transformation transformation3 in details.transformations)
			{
				bool flag = false;
				foreach (EconomyDetails.Transformation.Delta delta in transformation2.deltas)
				{
					if (delta.resource.type != details.energyResourceType)
					{
						foreach (EconomyDetails.Transformation.Delta delta2 in transformation3.deltas)
						{
							if (delta.resource == delta2.resource)
							{
								scenario20.AddEntry(new EconomyDetails.Scenario.Entry(transformation3.tag, 0f));
								flag = true;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
			list.Add(scenario20);
		}
		foreach (EdiblesManager.FoodInfo foodInfo in EdiblesManager.GetAllFoodTypes())
		{
			EconomyDetails.Scenario scenario21 = new EconomyDetails.Scenario("food/" + foodInfo.Id, 0f, null);
			Tag tag2 = TagManager.Create(foodInfo.Id);
			scenario21.AddEntry(new EconomyDetails.Scenario.Entry(tag2, 1f));
			scenario21.AddEntry(new EconomyDetails.Scenario.Entry(TagManager.Create("Duplicant"), 1f));
			List<Tag> list2 = new List<Tag>();
			list2.Add(tag2);
			while (list2.Count > 0)
			{
				Tag tag = list2[0];
				list2.RemoveAt(0);
				ComplexRecipe complexRecipe = ComplexRecipeManager.Get().recipes.Find((ComplexRecipe a) => a.FirstResult == tag);
				if (complexRecipe != null)
				{
					foreach (ComplexRecipe.RecipeElement recipeElement in complexRecipe.ingredients)
					{
						scenario21.AddEntry(new EconomyDetails.Scenario.Entry(recipeElement.material, 1f));
						list2.Add(recipeElement.material);
					}
				}
				foreach (KPrefabID kprefabID in Assets.Prefabs)
				{
					Crop component = kprefabID.GetComponent<Crop>();
					if (component != null && component.cropVal.cropId == tag.Name)
					{
						scenario21.AddEntry(new EconomyDetails.Scenario.Entry(kprefabID.PrefabTag, 1f));
						list2.Add(kprefabID.PrefabTag);
					}
				}
			}
			list.Add(scenario21);
		}
		if (!Directory.Exists("assets/Tuning/Economy"))
		{
			Directory.CreateDirectory("assets/Tuning/Economy");
		}
		foreach (EconomyDetails.Scenario scenario22 in list)
		{
			string text = "assets/Tuning/Economy/" + scenario22.name + ".csv";
			if (!Directory.Exists(global::System.IO.Path.GetDirectoryName(text)))
			{
				Directory.CreateDirectory(global::System.IO.Path.GetDirectoryName(text));
			}
			using (StreamWriter streamWriter = new StreamWriter(text))
			{
				details.DumpTransformations(scenario22, streamWriter);
			}
		}
		float dupeBreathingPerSecond = details.GetDupeBreathingPerSecond(details);
		List<EconomyDetails.BiomeTransformation> list3 = new List<EconomyDetails.BiomeTransformation>();
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "MineralDeoxidizer".ToTag(), GameTags.Algae, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "AlgaeHabitat".ToTag(), GameTags.Algae, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "AlgaeHabitat".ToTag(), GameTags.Water, GameTags.Oxygen));
		list3.Add(details.CreateBiomeTransformationFromTransformation(details, "Electrolyzer".ToTag(), GameTags.Water, GameTags.Oxygen));
		list3.Add(new EconomyDetails.BiomeTransformation("StartingOxygenCycles".ToTag(), details.GetResource(GameTags.Oxygen), 1f / -(dupeBreathingPerSecond * 600f)));
		list3.Add(new EconomyDetails.BiomeTransformation("StartingOxyliteCycles".ToTag(), details.CreateResource(GameTags.OxyRock, details.massResourceType), 1f / -(dupeBreathingPerSecond * 600f)));
		string text2 = "assets/Tuning/Economy/biomes/starting_amounts.csv";
		if (!Directory.Exists(global::System.IO.Path.GetDirectoryName(text2)))
		{
			Directory.CreateDirectory(global::System.IO.Path.GetDirectoryName(text2));
		}
		using (StreamWriter streamWriter2 = new StreamWriter(text2))
		{
			streamWriter2.Write("Resource,Amount");
			foreach (EconomyDetails.BiomeTransformation biomeTransformation in list3)
			{
				streamWriter2.Write("," + biomeTransformation.tag.ToString());
			}
			streamWriter2.Write("\n");
			streamWriter2.Write("Cells, " + details.startingBiomeCellCount.ToString() + "\n");
			foreach (KeyValuePair<Element, float> keyValuePair in details.startingBiomeAmounts)
			{
				streamWriter2.Write(keyValuePair.Key.id.ToString() + ", " + keyValuePair.Value.ToString());
				foreach (EconomyDetails.BiomeTransformation biomeTransformation2 in list3)
				{
					streamWriter2.Write(",");
					float num = biomeTransformation2.Transform(keyValuePair.Key, keyValuePair.Value);
					if (num > 0f)
					{
						streamWriter2.Write(num);
					}
				}
				streamWriter2.Write("\n");
			}
		}
		global::Debug.Log("Completed economy details dump!!");
	}

	// Token: 0x06003E9C RID: 16028 RVA: 0x00161028 File Offset: 0x0015F228
	private static void DumpNameMapping()
	{
		string text = "assets/Tuning/Economy/name_mapping.csv";
		if (!Directory.Exists("assets/Tuning/Economy"))
		{
			Directory.CreateDirectory("assets/Tuning/Economy");
		}
		using (StreamWriter streamWriter = new StreamWriter(text))
		{
			streamWriter.Write("Game Name, Prefab Name, Anim Files\n");
			foreach (KPrefabID kprefabID in Assets.Prefabs)
			{
				string text2 = TagManager.StripLinkFormatting(kprefabID.GetProperName());
				Tag tag = kprefabID.PrefabID();
				if (!text2.IsNullOrWhiteSpace() && !tag.Name.Contains("UnderConstruction") && !tag.Name.Contains("Preview"))
				{
					streamWriter.Write(text2);
					TextWriter textWriter = streamWriter;
					string text3 = ",";
					Tag tag2 = tag;
					textWriter.Write(text3 + tag2.ToString());
					KAnimControllerBase component = kprefabID.GetComponent<KAnimControllerBase>();
					if (component != null)
					{
						foreach (KAnimFile kanimFile in component.AnimFiles)
						{
							streamWriter.Write("," + kanimFile.name);
						}
					}
					else
					{
						streamWriter.Write(",");
					}
					streamWriter.Write("\n");
				}
			}
			using (List<PermitResource>.Enumerator enumerator2 = Db.Get().Permits.resources.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					PermitResource permit = enumerator2.Current;
					if (Blueprints.Get().skinsRelease.buildingFacades.Any((BuildingFacadeInfo info) => info.id == permit.Id) || Blueprints.Get().skinsRelease.clothingItems.Any((ClothingItemInfo info) => info.id == permit.Id) || Blueprints.Get().skinsRelease.artables.Any((ArtableInfo info) => info.id == permit.Id))
					{
						string text4 = TagManager.StripLinkFormatting(permit.Name);
						streamWriter.Write(text4);
						string id = permit.Id;
						streamWriter.Write("," + id);
						BuildingFacadeResource buildingFacadeResource = permit as BuildingFacadeResource;
						string text5;
						if (buildingFacadeResource != null)
						{
							text5 = buildingFacadeResource.AnimFile;
						}
						else
						{
							ClothingItemResource clothingItemResource = permit as ClothingItemResource;
							if (clothingItemResource != null)
							{
								text5 = clothingItemResource.AnimFile.name;
							}
							else
							{
								ArtableStage artableStage = permit as ArtableStage;
								if (artableStage != null)
								{
									text5 = artableStage.animFile;
								}
								else
								{
									text5 = "";
								}
							}
						}
						streamWriter.Write("," + text5);
						streamWriter.Write("\n");
					}
				}
			}
		}
	}

	// Token: 0x0400267E RID: 9854
	private List<EconomyDetails.Transformation> transformations = new List<EconomyDetails.Transformation>();

	// Token: 0x0400267F RID: 9855
	private List<EconomyDetails.Resource> resources = new List<EconomyDetails.Resource>();

	// Token: 0x04002680 RID: 9856
	public Dictionary<Element, float> startingBiomeAmounts = new Dictionary<Element, float>();

	// Token: 0x04002681 RID: 9857
	public int startingBiomeCellCount;

	// Token: 0x04002682 RID: 9858
	public EconomyDetails.Resource energyResource;

	// Token: 0x04002683 RID: 9859
	public EconomyDetails.Resource heatResource;

	// Token: 0x04002684 RID: 9860
	public EconomyDetails.Resource duplicantTimeResource;

	// Token: 0x04002685 RID: 9861
	public EconomyDetails.Resource caloriesResource;

	// Token: 0x04002686 RID: 9862
	public EconomyDetails.Resource fixedCaloriesResource;

	// Token: 0x04002687 RID: 9863
	public EconomyDetails.Resource.Type massResourceType;

	// Token: 0x04002688 RID: 9864
	public EconomyDetails.Resource.Type heatResourceType;

	// Token: 0x04002689 RID: 9865
	public EconomyDetails.Resource.Type energyResourceType;

	// Token: 0x0400268A RID: 9866
	public EconomyDetails.Resource.Type timeResourceType;

	// Token: 0x0400268B RID: 9867
	public EconomyDetails.Resource.Type attributeResourceType;

	// Token: 0x0400268C RID: 9868
	public EconomyDetails.Resource.Type caloriesResourceType;

	// Token: 0x0400268D RID: 9869
	public EconomyDetails.Resource.Type amountResourceType;

	// Token: 0x0400268E RID: 9870
	public EconomyDetails.Transformation.Type buildingTransformationType;

	// Token: 0x0400268F RID: 9871
	public EconomyDetails.Transformation.Type foodTransformationType;

	// Token: 0x04002690 RID: 9872
	public EconomyDetails.Transformation.Type plantTransformationType;

	// Token: 0x04002691 RID: 9873
	public EconomyDetails.Transformation.Type creatureTransformationType;

	// Token: 0x04002692 RID: 9874
	public EconomyDetails.Transformation.Type dupeTransformationType;

	// Token: 0x04002693 RID: 9875
	public EconomyDetails.Transformation.Type referenceTransformationType;

	// Token: 0x04002694 RID: 9876
	public EconomyDetails.Transformation.Type effectTransformationType;

	// Token: 0x04002695 RID: 9877
	private const string GEYSER_ACTIVE_SUFFIX = "_ActiveOnly";

	// Token: 0x04002696 RID: 9878
	public EconomyDetails.Transformation.Type geyserActivePeriodTransformationType;

	// Token: 0x04002697 RID: 9879
	public EconomyDetails.Transformation.Type geyserLifetimeTransformationType;

	// Token: 0x04002698 RID: 9880
	private static string debugTag = "CO2Scrubber";

	// Token: 0x02001879 RID: 6265
	public class Resource
	{
		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06009CAA RID: 40106 RVA: 0x00391554 File Offset: 0x0038F754
		// (set) Token: 0x06009CAB RID: 40107 RVA: 0x0039155C File Offset: 0x0038F75C
		public Tag tag { get; private set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06009CAC RID: 40108 RVA: 0x00391565 File Offset: 0x0038F765
		// (set) Token: 0x06009CAD RID: 40109 RVA: 0x0039156D File Offset: 0x0038F76D
		public EconomyDetails.Resource.Type type { get; private set; }

		// Token: 0x06009CAE RID: 40110 RVA: 0x00391576 File Offset: 0x0038F776
		public Resource(Tag tag, EconomyDetails.Resource.Type type)
		{
			this.tag = tag;
			this.type = type;
		}

		// Token: 0x0200282E RID: 10286
		public class Type
		{
			// Token: 0x17000CE0 RID: 3296
			// (get) Token: 0x0600CB19 RID: 51993 RVA: 0x00419977 File Offset: 0x00417B77
			// (set) Token: 0x0600CB1A RID: 51994 RVA: 0x0041997F File Offset: 0x00417B7F
			public string id { get; private set; }

			// Token: 0x17000CE1 RID: 3297
			// (get) Token: 0x0600CB1B RID: 51995 RVA: 0x00419988 File Offset: 0x00417B88
			// (set) Token: 0x0600CB1C RID: 51996 RVA: 0x00419990 File Offset: 0x00417B90
			public string unit { get; private set; }

			// Token: 0x0600CB1D RID: 51997 RVA: 0x00419999 File Offset: 0x00417B99
			public Type(string id, string unit)
			{
				this.id = id;
				this.unit = unit;
			}
		}
	}

	// Token: 0x0200187A RID: 6266
	public class BiomeTransformation
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06009CAF RID: 40111 RVA: 0x0039158C File Offset: 0x0038F78C
		// (set) Token: 0x06009CB0 RID: 40112 RVA: 0x00391594 File Offset: 0x0038F794
		public Tag tag { get; private set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06009CB1 RID: 40113 RVA: 0x0039159D File Offset: 0x0038F79D
		// (set) Token: 0x06009CB2 RID: 40114 RVA: 0x003915A5 File Offset: 0x0038F7A5
		public EconomyDetails.Resource resource { get; private set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06009CB3 RID: 40115 RVA: 0x003915AE File Offset: 0x0038F7AE
		// (set) Token: 0x06009CB4 RID: 40116 RVA: 0x003915B6 File Offset: 0x0038F7B6
		public float ratio { get; private set; }

		// Token: 0x06009CB5 RID: 40117 RVA: 0x003915BF File Offset: 0x0038F7BF
		public BiomeTransformation(Tag tag, EconomyDetails.Resource resource, float ratio)
		{
			this.tag = tag;
			this.resource = resource;
			this.ratio = ratio;
		}

		// Token: 0x06009CB6 RID: 40118 RVA: 0x003915DC File Offset: 0x0038F7DC
		public float Transform(Element element, float amount)
		{
			if (this.resource.tag == element.tag)
			{
				return this.ratio * amount;
			}
			return 0f;
		}
	}

	// Token: 0x0200187B RID: 6267
	public class Ratio
	{
		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06009CB7 RID: 40119 RVA: 0x00391604 File Offset: 0x0038F804
		// (set) Token: 0x06009CB8 RID: 40120 RVA: 0x0039160C File Offset: 0x0038F80C
		public EconomyDetails.Resource input { get; private set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06009CB9 RID: 40121 RVA: 0x00391615 File Offset: 0x0038F815
		// (set) Token: 0x06009CBA RID: 40122 RVA: 0x0039161D File Offset: 0x0038F81D
		public EconomyDetails.Resource output { get; private set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06009CBB RID: 40123 RVA: 0x00391626 File Offset: 0x0038F826
		// (set) Token: 0x06009CBC RID: 40124 RVA: 0x0039162E File Offset: 0x0038F82E
		public bool allowNegativeOutput { get; private set; }

		// Token: 0x06009CBD RID: 40125 RVA: 0x00391637 File Offset: 0x0038F837
		public Ratio(EconomyDetails.Resource input, EconomyDetails.Resource output, bool allow_negative_output)
		{
			this.input = input;
			this.output = output;
			this.allowNegativeOutput = allow_negative_output;
		}
	}

	// Token: 0x0200187C RID: 6268
	public class Scenario
	{
		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06009CBE RID: 40126 RVA: 0x00391654 File Offset: 0x0038F854
		// (set) Token: 0x06009CBF RID: 40127 RVA: 0x0039165C File Offset: 0x0038F85C
		public string name { get; private set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06009CC0 RID: 40128 RVA: 0x00391665 File Offset: 0x0038F865
		// (set) Token: 0x06009CC1 RID: 40129 RVA: 0x0039166D File Offset: 0x0038F86D
		public float defaultCount { get; private set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06009CC2 RID: 40130 RVA: 0x00391676 File Offset: 0x0038F876
		// (set) Token: 0x06009CC3 RID: 40131 RVA: 0x0039167E File Offset: 0x0038F87E
		public float timeInSeconds { get; set; }

		// Token: 0x06009CC4 RID: 40132 RVA: 0x00391687 File Offset: 0x0038F887
		public Scenario(string name, float default_count, Func<EconomyDetails.Transformation, bool> filter)
		{
			this.name = name;
			this.defaultCount = default_count;
			this.filter = filter;
			this.timeInSeconds = 600f;
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x003916BA File Offset: 0x0038F8BA
		public void AddEntry(EconomyDetails.Scenario.Entry entry)
		{
			this.entries.Add(entry);
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x003916C8 File Offset: 0x0038F8C8
		public float GetCount(Tag tag)
		{
			foreach (EconomyDetails.Scenario.Entry entry in this.entries)
			{
				if (entry.tag == tag)
				{
					return entry.count;
				}
			}
			return this.defaultCount;
		}

		// Token: 0x06009CC7 RID: 40135 RVA: 0x00391734 File Offset: 0x0038F934
		public bool IncludesTransformation(EconomyDetails.Transformation transformation)
		{
			if (this.filter != null && this.filter(transformation))
			{
				return true;
			}
			using (List<EconomyDetails.Scenario.Entry>.Enumerator enumerator = this.entries.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.tag == transformation.tag)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04007909 RID: 30985
		private Func<EconomyDetails.Transformation, bool> filter;

		// Token: 0x0400790A RID: 30986
		private List<EconomyDetails.Scenario.Entry> entries = new List<EconomyDetails.Scenario.Entry>();

		// Token: 0x0200282F RID: 10287
		public class Entry
		{
			// Token: 0x17000CE2 RID: 3298
			// (get) Token: 0x0600CB1E RID: 51998 RVA: 0x004199AF File Offset: 0x00417BAF
			// (set) Token: 0x0600CB1F RID: 51999 RVA: 0x004199B7 File Offset: 0x00417BB7
			public Tag tag { get; private set; }

			// Token: 0x17000CE3 RID: 3299
			// (get) Token: 0x0600CB20 RID: 52000 RVA: 0x004199C0 File Offset: 0x00417BC0
			// (set) Token: 0x0600CB21 RID: 52001 RVA: 0x004199C8 File Offset: 0x00417BC8
			public float count { get; private set; }

			// Token: 0x0600CB22 RID: 52002 RVA: 0x004199D1 File Offset: 0x00417BD1
			public Entry(Tag tag, float count)
			{
				this.tag = tag;
				this.count = count;
			}
		}
	}

	// Token: 0x0200187D RID: 6269
	public class Transformation
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06009CC8 RID: 40136 RVA: 0x003917B0 File Offset: 0x0038F9B0
		// (set) Token: 0x06009CC9 RID: 40137 RVA: 0x003917B8 File Offset: 0x0038F9B8
		public Tag tag { get; private set; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06009CCA RID: 40138 RVA: 0x003917C1 File Offset: 0x0038F9C1
		// (set) Token: 0x06009CCB RID: 40139 RVA: 0x003917C9 File Offset: 0x0038F9C9
		public EconomyDetails.Transformation.Type type { get; private set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06009CCC RID: 40140 RVA: 0x003917D2 File Offset: 0x0038F9D2
		// (set) Token: 0x06009CCD RID: 40141 RVA: 0x003917DA File Offset: 0x0038F9DA
		public float timeInSeconds { get; private set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06009CCE RID: 40142 RVA: 0x003917E3 File Offset: 0x0038F9E3
		// (set) Token: 0x06009CCF RID: 40143 RVA: 0x003917EB File Offset: 0x0038F9EB
		public bool timeInvariant { get; private set; }

		// Token: 0x06009CD0 RID: 40144 RVA: 0x003917F4 File Offset: 0x0038F9F4
		public Transformation(Tag tag, EconomyDetails.Transformation.Type type, float time_in_seconds, bool timeInvariant = false)
		{
			this.tag = tag;
			this.type = type;
			this.timeInSeconds = time_in_seconds;
			this.timeInvariant = timeInvariant;
		}

		// Token: 0x06009CD1 RID: 40145 RVA: 0x00391824 File Offset: 0x0038FA24
		public void AddDelta(EconomyDetails.Transformation.Delta delta)
		{
			global::Debug.Assert(delta.resource != null);
			this.deltas.Add(delta);
		}

		// Token: 0x06009CD2 RID: 40146 RVA: 0x00391840 File Offset: 0x0038FA40
		public EconomyDetails.Transformation.Delta GetDelta(EconomyDetails.Resource resource)
		{
			foreach (EconomyDetails.Transformation.Delta delta in this.deltas)
			{
				if (delta.resource == resource)
				{
					return delta;
				}
			}
			return null;
		}

		// Token: 0x0400790C RID: 30988
		public List<EconomyDetails.Transformation.Delta> deltas = new List<EconomyDetails.Transformation.Delta>();

		// Token: 0x02002830 RID: 10288
		public class Delta
		{
			// Token: 0x17000CE4 RID: 3300
			// (get) Token: 0x0600CB23 RID: 52003 RVA: 0x004199E7 File Offset: 0x00417BE7
			// (set) Token: 0x0600CB24 RID: 52004 RVA: 0x004199EF File Offset: 0x00417BEF
			public EconomyDetails.Resource resource { get; private set; }

			// Token: 0x17000CE5 RID: 3301
			// (get) Token: 0x0600CB25 RID: 52005 RVA: 0x004199F8 File Offset: 0x00417BF8
			// (set) Token: 0x0600CB26 RID: 52006 RVA: 0x00419A00 File Offset: 0x00417C00
			public float amount { get; set; }

			// Token: 0x0600CB27 RID: 52007 RVA: 0x00419A09 File Offset: 0x00417C09
			public Delta(EconomyDetails.Resource resource, float amount)
			{
				this.resource = resource;
				this.amount = amount;
			}
		}

		// Token: 0x02002831 RID: 10289
		public class Type
		{
			// Token: 0x17000CE6 RID: 3302
			// (get) Token: 0x0600CB28 RID: 52008 RVA: 0x00419A1F File Offset: 0x00417C1F
			// (set) Token: 0x0600CB29 RID: 52009 RVA: 0x00419A27 File Offset: 0x00417C27
			public string id { get; private set; }

			// Token: 0x0600CB2A RID: 52010 RVA: 0x00419A30 File Offset: 0x00417C30
			public Type(string id)
			{
				this.id = id;
			}
		}
	}
}
