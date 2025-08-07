using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Klei;
using ProcGenGame;
using STRINGS;
using UnityEngine;

// Token: 0x020009AA RID: 2474
public class ElementLoader
{
	// Token: 0x060047E2 RID: 18402 RVA: 0x0019EE08 File Offset: 0x0019D008
	public static float GetMinMeltingPointAmongElements(IList<Tag> elements)
	{
		float num = float.MaxValue;
		for (int i = 0; i < elements.Count; i++)
		{
			Element element = ElementLoader.GetElement(elements[i]);
			if (element != null)
			{
				num = Mathf.Min(num, element.highTemp);
			}
		}
		return num;
	}

	// Token: 0x060047E3 RID: 18403 RVA: 0x0019EE4C File Offset: 0x0019D04C
	public static List<ElementLoader.ElementEntry> CollectElementsFromYAML()
	{
		List<ElementLoader.ElementEntry> list = new List<ElementLoader.ElementEntry>();
		ListPool<FileHandle, ElementLoader>.PooledList pooledList = ListPool<FileHandle, ElementLoader>.Allocate();
		FileSystem.GetFiles(FileSystem.Normalize(ElementLoader.path), "*.yaml", pooledList);
		ListPool<YamlIO.Error, ElementLoader>.PooledList errors = ListPool<YamlIO.Error, ElementLoader>.Allocate();
		YamlIO.ErrorHandler <>9__0;
		foreach (FileHandle fileHandle in pooledList)
		{
			if (!Path.GetFileName(fileHandle.full_path).StartsWith("."))
			{
				string full_path = fileHandle.full_path;
				YamlIO.ErrorHandler errorHandler;
				if ((errorHandler = <>9__0) == null)
				{
					errorHandler = (<>9__0 = delegate(YamlIO.Error error, bool force_log_as_warning)
					{
						errors.Add(error);
					});
				}
				ElementLoader.ElementEntryCollection elementEntryCollection = YamlIO.LoadFile<ElementLoader.ElementEntryCollection>(full_path, errorHandler, null);
				if (elementEntryCollection != null)
				{
					list.AddRange(elementEntryCollection.elements);
				}
			}
		}
		pooledList.Recycle();
		if (Global.Instance != null && Global.Instance.modManager != null)
		{
			Global.Instance.modManager.HandleErrors(errors);
		}
		errors.Recycle();
		return list;
	}

	// Token: 0x060047E4 RID: 18404 RVA: 0x0019EF60 File Offset: 0x0019D160
	public static void Load(ref Hashtable substanceList, Dictionary<string, SubstanceTable> substanceTablesByDlc)
	{
		ElementLoader.elements = new List<Element>();
		ElementLoader.elementTable = new Dictionary<int, Element>();
		ElementLoader.elementTagTable = new Dictionary<Tag, Element>();
		foreach (ElementLoader.ElementEntry elementEntry in ElementLoader.CollectElementsFromYAML())
		{
			int num = Hash.SDBMLower(elementEntry.elementId);
			if (!ElementLoader.elementTable.ContainsKey(num) && substanceTablesByDlc.ContainsKey(elementEntry.dlcId))
			{
				Element element = new Element();
				element.id = (SimHashes)num;
				element.name = Strings.Get(elementEntry.localizationID);
				element.nameUpperCase = element.name.ToUpper();
				element.description = Strings.Get(elementEntry.description);
				element.tag = TagManager.Create(elementEntry.elementId, element.name);
				ElementLoader.CopyEntryToElement(elementEntry, element);
				ElementLoader.elements.Add(element);
				ElementLoader.elementTable[num] = element;
				ElementLoader.elementTagTable[element.tag] = element;
				if (!ElementLoader.ManifestSubstanceForElement(element, ref substanceList, substanceTablesByDlc[elementEntry.dlcId]))
				{
					global::Debug.LogWarning("Missing substance for element: " + element.id.ToString());
				}
			}
		}
		ElementLoader.FinaliseElementsTable(ref substanceList);
		WorldGen.SetupDefaultElements();
	}

	// Token: 0x060047E5 RID: 18405 RVA: 0x0019F0D8 File Offset: 0x0019D2D8
	private static void CopyEntryToElement(ElementLoader.ElementEntry entry, Element elem)
	{
		Hash.SDBMLower(entry.elementId);
		elem.tag = TagManager.Create(entry.elementId.ToString());
		elem.specificHeatCapacity = entry.specificHeatCapacity;
		elem.thermalConductivity = entry.thermalConductivity;
		elem.molarMass = entry.molarMass;
		elem.strength = entry.strength;
		elem.disabled = entry.isDisabled;
		elem.dlcId = entry.dlcId;
		elem.flow = entry.flow;
		elem.maxMass = entry.maxMass;
		elem.maxCompression = entry.liquidCompression;
		elem.viscosity = entry.speed;
		elem.minHorizontalFlow = entry.minHorizontalFlow;
		elem.minVerticalFlow = entry.minVerticalFlow;
		elem.solidSurfaceAreaMultiplier = entry.solidSurfaceAreaMultiplier;
		elem.liquidSurfaceAreaMultiplier = entry.liquidSurfaceAreaMultiplier;
		elem.gasSurfaceAreaMultiplier = entry.gasSurfaceAreaMultiplier;
		elem.state = entry.state;
		elem.hardness = entry.hardness;
		elem.lowTemp = entry.lowTemp;
		elem.lowTempTransitionTarget = (SimHashes)Hash.SDBMLower(entry.lowTempTransitionTarget);
		elem.highTemp = entry.highTemp;
		elem.highTempTransitionTarget = (SimHashes)Hash.SDBMLower(entry.highTempTransitionTarget);
		elem.highTempTransitionOreID = (SimHashes)Hash.SDBMLower(entry.highTempTransitionOreId);
		elem.highTempTransitionOreMassConversion = entry.highTempTransitionOreMassConversion;
		elem.lowTempTransitionOreID = (SimHashes)Hash.SDBMLower(entry.lowTempTransitionOreId);
		elem.lowTempTransitionOreMassConversion = entry.lowTempTransitionOreMassConversion;
		elem.refinedMetalTarget = (SimHashes)Hash.SDBMLower(entry.refinedMetalTarget);
		elem.sublimateId = (SimHashes)Hash.SDBMLower(entry.sublimateId);
		elem.convertId = (SimHashes)Hash.SDBMLower(entry.convertId);
		elem.sublimateFX = (SpawnFXHashes)Hash.SDBMLower(entry.sublimateFx);
		elem.sublimateRate = entry.sublimateRate;
		elem.sublimateEfficiency = entry.sublimateEfficiency;
		elem.sublimateProbability = entry.sublimateProbability;
		elem.offGasPercentage = entry.offGasPercentage;
		elem.lightAbsorptionFactor = entry.lightAbsorptionFactor;
		elem.radiationAbsorptionFactor = entry.radiationAbsorptionFactor;
		elem.radiationPer1000Mass = entry.radiationPer1000Mass;
		elem.toxicity = entry.toxicity;
		elem.elementComposition = entry.composition;
		Tag tag = TagManager.Create(entry.state.ToString());
		elem.materialCategory = ElementLoader.CreateMaterialCategoryTag(elem.id, tag, entry.materialCategory);
		elem.oreTags = ElementLoader.CreateOreTags(elem.materialCategory, tag, entry.tags);
		elem.buildMenuSort = entry.buildMenuSort;
		Sim.PhysicsData physicsData = default(Sim.PhysicsData);
		physicsData.temperature = entry.defaultTemperature;
		physicsData.mass = entry.defaultMass;
		physicsData.pressure = entry.defaultPressure;
		switch (entry.state)
		{
		case Element.State.Gas:
			GameTags.GasElements.Add(elem.tag);
			physicsData.mass = 1f;
			elem.maxMass = 1.8f;
			break;
		case Element.State.Liquid:
			GameTags.LiquidElements.Add(elem.tag);
			break;
		case Element.State.Solid:
			GameTags.SolidElements.Add(elem.tag);
			break;
		}
		elem.defaultValues = physicsData;
	}

	// Token: 0x060047E6 RID: 18406 RVA: 0x0019F3EC File Offset: 0x0019D5EC
	private static bool ManifestSubstanceForElement(Element elem, ref Hashtable substanceList, SubstanceTable substanceTable)
	{
		elem.substance = null;
		if (substanceList.ContainsKey(elem.id))
		{
			elem.substance = substanceList[elem.id] as Substance;
			return false;
		}
		if (substanceTable != null)
		{
			elem.substance = substanceTable.GetSubstance(elem.id);
		}
		if (elem.substance == null)
		{
			elem.substance = new Substance();
			substanceTable.GetList().Add(elem.substance);
		}
		elem.substance.elementID = elem.id;
		elem.substance.renderedByWorld = elem.IsSolid;
		elem.substance.idx = substanceList.Count;
		if (elem.substance.uiColour == ElementLoader.noColour)
		{
			int count = ElementLoader.elements.Count;
			int idx = elem.substance.idx;
			elem.substance.uiColour = Color.HSVToRGB((float)idx / (float)count, 1f, 1f);
		}
		string text = UI.StripLinkFormatting(elem.name);
		elem.substance.name = text;
		elem.substance.nameTag = elem.tag;
		elem.substance.audioConfig = ElementsAudio.Instance.GetConfigForElement(elem.id);
		substanceList.Add(elem.id, elem.substance);
		return true;
	}

	// Token: 0x060047E7 RID: 18407 RVA: 0x0019F55A File Offset: 0x0019D75A
	public static Element FindElementByName(string name)
	{
		return ElementLoader.FindElementByHash((SimHashes)Hash.SDBMLower(name));
	}

	// Token: 0x060047E8 RID: 18408 RVA: 0x0019F567 File Offset: 0x0019D767
	public static Element FindElementByTag(Tag tag)
	{
		return ElementLoader.GetElement(tag);
	}

	// Token: 0x060047E9 RID: 18409 RVA: 0x0019F570 File Offset: 0x0019D770
	public static List<Element> FindElements(Func<Element, bool> filter)
	{
		List<Element> list = new List<Element>();
		foreach (int num in ElementLoader.elementTable.Keys)
		{
			Element element = ElementLoader.elementTable[num];
			if (filter(element))
			{
				list.Add(element);
			}
		}
		return list;
	}

	// Token: 0x060047EA RID: 18410 RVA: 0x0019F5E4 File Offset: 0x0019D7E4
	public static Element FindElementByHash(SimHashes hash)
	{
		Element element = null;
		ElementLoader.elementTable.TryGetValue((int)hash, out element);
		return element;
	}

	// Token: 0x060047EB RID: 18411 RVA: 0x0019F604 File Offset: 0x0019D804
	public static ushort GetElementIndex(SimHashes hash)
	{
		Element element = null;
		ElementLoader.elementTable.TryGetValue((int)hash, out element);
		if (element != null)
		{
			return element.idx;
		}
		return ushort.MaxValue;
	}

	// Token: 0x060047EC RID: 18412 RVA: 0x0019F630 File Offset: 0x0019D830
	public static Element GetElement(Tag tag)
	{
		Element element;
		ElementLoader.elementTagTable.TryGetValue(tag, out element);
		return element;
	}

	// Token: 0x060047ED RID: 18413 RVA: 0x0019F64C File Offset: 0x0019D84C
	public static SimHashes GetElementID(Tag tag)
	{
		Element element;
		ElementLoader.elementTagTable.TryGetValue(tag, out element);
		if (element != null)
		{
			return element.id;
		}
		return SimHashes.Vacuum;
	}

	// Token: 0x060047EE RID: 18414 RVA: 0x0019F678 File Offset: 0x0019D878
	private static SimHashes GetID(int column, int row, string[,] grid, SimHashes defaultValue = SimHashes.Vacuum)
	{
		if (column >= grid.GetLength(0) || row > grid.GetLength(1))
		{
			global::Debug.LogError(string.Format("Could not find element at loc [{0},{1}] grid is only [{2},{3}]", new object[]
			{
				column,
				row,
				grid.GetLength(0),
				grid.GetLength(1)
			}));
			return defaultValue;
		}
		string text = grid[column, row];
		if (text == null || text == "")
		{
			return defaultValue;
		}
		object obj = null;
		try
		{
			obj = Enum.Parse(typeof(SimHashes), text);
		}
		catch (Exception ex)
		{
			global::Debug.LogError(string.Format("Could not find element {0}: {1}", text, ex.ToString()));
			return defaultValue;
		}
		return (SimHashes)obj;
	}

	// Token: 0x060047EF RID: 18415 RVA: 0x0019F744 File Offset: 0x0019D944
	private static SpawnFXHashes GetSpawnFX(int column, int row, string[,] grid)
	{
		if (column >= grid.GetLength(0) || row > grid.GetLength(1))
		{
			global::Debug.LogError(string.Format("Could not find SpawnFXHashes at loc [{0},{1}] grid is only [{2},{3}]", new object[]
			{
				column,
				row,
				grid.GetLength(0),
				grid.GetLength(1)
			}));
			return SpawnFXHashes.None;
		}
		string text = grid[column, row];
		if (text == null || text == "")
		{
			return SpawnFXHashes.None;
		}
		object obj = null;
		try
		{
			obj = Enum.Parse(typeof(SpawnFXHashes), text);
		}
		catch (Exception ex)
		{
			global::Debug.LogError(string.Format("Could not find FX {0}: {1}", text, ex.ToString()));
			return SpawnFXHashes.None;
		}
		return (SpawnFXHashes)obj;
	}

	// Token: 0x060047F0 RID: 18416 RVA: 0x0019F810 File Offset: 0x0019DA10
	private static Tag CreateMaterialCategoryTag(SimHashes element_id, Tag phaseTag, string materialCategoryField)
	{
		if (!string.IsNullOrEmpty(materialCategoryField))
		{
			Tag tag = TagManager.Create(materialCategoryField);
			if (!GameTags.MaterialCategories.Contains(tag) && !GameTags.IgnoredMaterialCategories.Contains(tag))
			{
				global::Debug.LogWarningFormat("Element {0} has category {1}, but that isn't in GameTags.MaterialCategores!", new object[] { element_id, materialCategoryField });
			}
			return tag;
		}
		return phaseTag;
	}

	// Token: 0x060047F1 RID: 18417 RVA: 0x0019F868 File Offset: 0x0019DA68
	private static Tag[] CreateOreTags(Tag materialCategory, Tag phaseTag, string[] ore_tags_split)
	{
		List<Tag> list = new List<Tag>();
		if (ore_tags_split != null)
		{
			foreach (string text in ore_tags_split)
			{
				if (!string.IsNullOrEmpty(text))
				{
					list.Add(TagManager.Create(text));
				}
			}
		}
		list.Add(phaseTag);
		if (materialCategory.IsValid && !list.Contains(materialCategory))
		{
			list.Add(materialCategory);
		}
		return list.ToArray();
	}

	// Token: 0x060047F2 RID: 18418 RVA: 0x0019F8CC File Offset: 0x0019DACC
	private static void FinaliseElementsTable(ref Hashtable substanceList)
	{
		foreach (Element element in ElementLoader.elements)
		{
			if (element != null)
			{
				if (element.substance == null)
				{
					global::Debug.LogWarning("Skipping finalise for missing element: " + element.id.ToString());
				}
				else
				{
					global::Debug.Assert(element.substance.nameTag.IsValid);
					if (element.thermalConductivity == 0f)
					{
						element.state |= Element.State.TemperatureInsulated;
					}
					if (element.strength == 0f)
					{
						element.state |= Element.State.Unbreakable;
					}
					if (element.IsSolid)
					{
						Element element2 = ElementLoader.FindElementByHash(element.highTempTransitionTarget);
						if (element2 != null)
						{
							element.highTempTransition = element2;
						}
					}
					else if (element.IsLiquid)
					{
						Element element3 = ElementLoader.FindElementByHash(element.highTempTransitionTarget);
						if (element3 != null)
						{
							element.highTempTransition = element3;
						}
						Element element4 = ElementLoader.FindElementByHash(element.lowTempTransitionTarget);
						if (element4 != null)
						{
							element.lowTempTransition = element4;
						}
					}
					else if (element.IsGas)
					{
						Element element5 = ElementLoader.FindElementByHash(element.lowTempTransitionTarget);
						if (element5 != null)
						{
							element.lowTempTransition = element5;
						}
					}
				}
			}
		}
		ElementLoader.elements = (from e in ElementLoader.elements
			orderby (int)(e.state & Element.State.Solid) descending, e.id
			select e).ToList<Element>();
		for (int i = 0; i < ElementLoader.elements.Count; i++)
		{
			if (ElementLoader.elements[i].substance != null)
			{
				ElementLoader.elements[i].substance.idx = i;
			}
			ElementLoader.elements[i].idx = (ushort)i;
		}
	}

	// Token: 0x060047F3 RID: 18419 RVA: 0x0019FAD4 File Offset: 0x0019DCD4
	private static void ValidateElements()
	{
		global::Debug.Log("------ Start Validating Elements ------");
		foreach (Element element in ElementLoader.elements)
		{
			string text = string.Format("{0} ({1})", element.tag.ProperNameStripLink(), element.state);
			if (element.IsLiquid && element.sublimateId != (SimHashes)0)
			{
				global::Debug.Assert(element.sublimateRate == 0f, text + ": Liquids don't use sublimateRate, use offGasPercentage instead.");
				global::Debug.Assert(element.offGasPercentage > 0f, text + ": Missing offGasPercentage");
			}
			if (element.IsSolid && element.sublimateId != (SimHashes)0)
			{
				global::Debug.Assert(element.offGasPercentage == 0f, text + ": Solids don't use offGasPercentage, use sublimateRate instead.");
				global::Debug.Assert(element.sublimateRate > 0f, text + ": Missing sublimationRate");
				global::Debug.Assert(element.sublimateRate * element.sublimateEfficiency > 0.001f, text + ": Sublimation rate and efficiency will result in gas that will be obliterated because its less than 1g. Increase these values and use sublimateProbability if you want a low amount of sublimation");
			}
			if (element.highTempTransition != null && element.highTempTransition.lowTempTransition == element)
			{
				global::Debug.Assert(element.highTemp >= element.highTempTransition.lowTemp, text + ": highTemp is higher than transition element's (" + element.highTempTransition.tag.ProperNameStripLink() + ") lowTemp");
			}
			global::Debug.Assert(element.defaultValues.mass <= element.maxMass, text + ": Default mass should be less than max mass");
			if (false)
			{
				if (element.IsSolid && element.highTempTransition != null && element.highTempTransition.IsLiquid && element.defaultValues.mass > element.highTempTransition.maxMass)
				{
					global::Debug.LogWarning(string.Format("{0} defaultMass {1} > {2}: maxMass {3}", new object[]
					{
						text,
						element.defaultValues.mass,
						element.highTempTransition.tag.ProperNameStripLink(),
						element.highTempTransition.maxMass
					}));
				}
				if (element.defaultValues.mass < element.maxMass && element.IsLiquid)
				{
					global::Debug.LogWarning(string.Format("{0} has defaultMass: {1} and maxMass {2}", element.tag.ProperNameStripLink(), element.defaultValues.mass, element.maxMass));
				}
			}
		}
		global::Debug.Log("------ End Validating Elements ------");
	}

	// Token: 0x04002F96 RID: 12182
	public static List<Element> elements;

	// Token: 0x04002F97 RID: 12183
	public static Dictionary<int, Element> elementTable;

	// Token: 0x04002F98 RID: 12184
	public static Dictionary<Tag, Element> elementTagTable;

	// Token: 0x04002F99 RID: 12185
	private static string path = Application.streamingAssetsPath + "/elements/";

	// Token: 0x04002F9A RID: 12186
	private static readonly Color noColour = new Color(0f, 0f, 0f, 0f);

	// Token: 0x020019B4 RID: 6580
	public class ElementEntryCollection
	{
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600A043 RID: 41027 RVA: 0x0039BE00 File Offset: 0x0039A000
		// (set) Token: 0x0600A044 RID: 41028 RVA: 0x0039BE08 File Offset: 0x0039A008
		public ElementLoader.ElementEntry[] elements { get; set; }
	}

	// Token: 0x020019B5 RID: 6581
	public class ElementComposition
	{
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600A047 RID: 41031 RVA: 0x0039BE21 File Offset: 0x0039A021
		// (set) Token: 0x0600A048 RID: 41032 RVA: 0x0039BE29 File Offset: 0x0039A029
		public string elementID { get; set; }

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600A049 RID: 41033 RVA: 0x0039BE32 File Offset: 0x0039A032
		// (set) Token: 0x0600A04A RID: 41034 RVA: 0x0039BE3A File Offset: 0x0039A03A
		public float percentage { get; set; }
	}

	// Token: 0x020019B6 RID: 6582
	public class ElementEntry
	{
		// Token: 0x0600A04B RID: 41035 RVA: 0x0039BE43 File Offset: 0x0039A043
		public ElementEntry()
		{
			this.lowTemp = 0f;
			this.highTemp = 10000f;
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600A04C RID: 41036 RVA: 0x0039BE61 File Offset: 0x0039A061
		// (set) Token: 0x0600A04D RID: 41037 RVA: 0x0039BE69 File Offset: 0x0039A069
		public string elementId { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600A04E RID: 41038 RVA: 0x0039BE72 File Offset: 0x0039A072
		// (set) Token: 0x0600A04F RID: 41039 RVA: 0x0039BE7A File Offset: 0x0039A07A
		public float specificHeatCapacity { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x0600A050 RID: 41040 RVA: 0x0039BE83 File Offset: 0x0039A083
		// (set) Token: 0x0600A051 RID: 41041 RVA: 0x0039BE8B File Offset: 0x0039A08B
		public float thermalConductivity { get; set; }

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600A052 RID: 41042 RVA: 0x0039BE94 File Offset: 0x0039A094
		// (set) Token: 0x0600A053 RID: 41043 RVA: 0x0039BE9C File Offset: 0x0039A09C
		public float solidSurfaceAreaMultiplier { get; set; }

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600A054 RID: 41044 RVA: 0x0039BEA5 File Offset: 0x0039A0A5
		// (set) Token: 0x0600A055 RID: 41045 RVA: 0x0039BEAD File Offset: 0x0039A0AD
		public float liquidSurfaceAreaMultiplier { get; set; }

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600A056 RID: 41046 RVA: 0x0039BEB6 File Offset: 0x0039A0B6
		// (set) Token: 0x0600A057 RID: 41047 RVA: 0x0039BEBE File Offset: 0x0039A0BE
		public float gasSurfaceAreaMultiplier { get; set; }

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600A058 RID: 41048 RVA: 0x0039BEC7 File Offset: 0x0039A0C7
		// (set) Token: 0x0600A059 RID: 41049 RVA: 0x0039BECF File Offset: 0x0039A0CF
		public float defaultMass { get; set; }

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600A05A RID: 41050 RVA: 0x0039BED8 File Offset: 0x0039A0D8
		// (set) Token: 0x0600A05B RID: 41051 RVA: 0x0039BEE0 File Offset: 0x0039A0E0
		public float defaultTemperature { get; set; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600A05C RID: 41052 RVA: 0x0039BEE9 File Offset: 0x0039A0E9
		// (set) Token: 0x0600A05D RID: 41053 RVA: 0x0039BEF1 File Offset: 0x0039A0F1
		public float defaultPressure { get; set; }

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600A05E RID: 41054 RVA: 0x0039BEFA File Offset: 0x0039A0FA
		// (set) Token: 0x0600A05F RID: 41055 RVA: 0x0039BF02 File Offset: 0x0039A102
		public float molarMass { get; set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600A060 RID: 41056 RVA: 0x0039BF0B File Offset: 0x0039A10B
		// (set) Token: 0x0600A061 RID: 41057 RVA: 0x0039BF13 File Offset: 0x0039A113
		public float lightAbsorptionFactor { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600A062 RID: 41058 RVA: 0x0039BF1C File Offset: 0x0039A11C
		// (set) Token: 0x0600A063 RID: 41059 RVA: 0x0039BF24 File Offset: 0x0039A124
		public float radiationAbsorptionFactor { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600A064 RID: 41060 RVA: 0x0039BF2D File Offset: 0x0039A12D
		// (set) Token: 0x0600A065 RID: 41061 RVA: 0x0039BF35 File Offset: 0x0039A135
		public float radiationPer1000Mass { get; set; }

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600A066 RID: 41062 RVA: 0x0039BF3E File Offset: 0x0039A13E
		// (set) Token: 0x0600A067 RID: 41063 RVA: 0x0039BF46 File Offset: 0x0039A146
		public string lowTempTransitionTarget { get; set; }

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600A068 RID: 41064 RVA: 0x0039BF4F File Offset: 0x0039A14F
		// (set) Token: 0x0600A069 RID: 41065 RVA: 0x0039BF57 File Offset: 0x0039A157
		public float lowTemp { get; set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600A06A RID: 41066 RVA: 0x0039BF60 File Offset: 0x0039A160
		// (set) Token: 0x0600A06B RID: 41067 RVA: 0x0039BF68 File Offset: 0x0039A168
		public string highTempTransitionTarget { get; set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600A06C RID: 41068 RVA: 0x0039BF71 File Offset: 0x0039A171
		// (set) Token: 0x0600A06D RID: 41069 RVA: 0x0039BF79 File Offset: 0x0039A179
		public float highTemp { get; set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600A06E RID: 41070 RVA: 0x0039BF82 File Offset: 0x0039A182
		// (set) Token: 0x0600A06F RID: 41071 RVA: 0x0039BF8A File Offset: 0x0039A18A
		public string lowTempTransitionOreId { get; set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x0600A070 RID: 41072 RVA: 0x0039BF93 File Offset: 0x0039A193
		// (set) Token: 0x0600A071 RID: 41073 RVA: 0x0039BF9B File Offset: 0x0039A19B
		public float lowTempTransitionOreMassConversion { get; set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x0600A072 RID: 41074 RVA: 0x0039BFA4 File Offset: 0x0039A1A4
		// (set) Token: 0x0600A073 RID: 41075 RVA: 0x0039BFAC File Offset: 0x0039A1AC
		public string highTempTransitionOreId { get; set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x0600A074 RID: 41076 RVA: 0x0039BFB5 File Offset: 0x0039A1B5
		// (set) Token: 0x0600A075 RID: 41077 RVA: 0x0039BFBD File Offset: 0x0039A1BD
		public float highTempTransitionOreMassConversion { get; set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x0600A076 RID: 41078 RVA: 0x0039BFC6 File Offset: 0x0039A1C6
		// (set) Token: 0x0600A077 RID: 41079 RVA: 0x0039BFCE File Offset: 0x0039A1CE
		public string sublimateId { get; set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x0600A078 RID: 41080 RVA: 0x0039BFD7 File Offset: 0x0039A1D7
		// (set) Token: 0x0600A079 RID: 41081 RVA: 0x0039BFDF File Offset: 0x0039A1DF
		public string sublimateFx { get; set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x0600A07A RID: 41082 RVA: 0x0039BFE8 File Offset: 0x0039A1E8
		// (set) Token: 0x0600A07B RID: 41083 RVA: 0x0039BFF0 File Offset: 0x0039A1F0
		public float sublimateRate { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x0600A07C RID: 41084 RVA: 0x0039BFF9 File Offset: 0x0039A1F9
		// (set) Token: 0x0600A07D RID: 41085 RVA: 0x0039C001 File Offset: 0x0039A201
		public float sublimateEfficiency { get; set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x0600A07E RID: 41086 RVA: 0x0039C00A File Offset: 0x0039A20A
		// (set) Token: 0x0600A07F RID: 41087 RVA: 0x0039C012 File Offset: 0x0039A212
		public float sublimateProbability { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600A080 RID: 41088 RVA: 0x0039C01B File Offset: 0x0039A21B
		// (set) Token: 0x0600A081 RID: 41089 RVA: 0x0039C023 File Offset: 0x0039A223
		public float offGasPercentage { get; set; }

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600A082 RID: 41090 RVA: 0x0039C02C File Offset: 0x0039A22C
		// (set) Token: 0x0600A083 RID: 41091 RVA: 0x0039C034 File Offset: 0x0039A234
		public string materialCategory { get; set; }

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600A084 RID: 41092 RVA: 0x0039C03D File Offset: 0x0039A23D
		// (set) Token: 0x0600A085 RID: 41093 RVA: 0x0039C045 File Offset: 0x0039A245
		public string[] tags { get; set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600A086 RID: 41094 RVA: 0x0039C04E File Offset: 0x0039A24E
		// (set) Token: 0x0600A087 RID: 41095 RVA: 0x0039C056 File Offset: 0x0039A256
		public bool isDisabled { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600A088 RID: 41096 RVA: 0x0039C05F File Offset: 0x0039A25F
		// (set) Token: 0x0600A089 RID: 41097 RVA: 0x0039C067 File Offset: 0x0039A267
		public float strength { get; set; }

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600A08A RID: 41098 RVA: 0x0039C070 File Offset: 0x0039A270
		// (set) Token: 0x0600A08B RID: 41099 RVA: 0x0039C078 File Offset: 0x0039A278
		public float maxMass { get; set; }

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600A08C RID: 41100 RVA: 0x0039C081 File Offset: 0x0039A281
		// (set) Token: 0x0600A08D RID: 41101 RVA: 0x0039C089 File Offset: 0x0039A289
		public byte hardness { get; set; }

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600A08E RID: 41102 RVA: 0x0039C092 File Offset: 0x0039A292
		// (set) Token: 0x0600A08F RID: 41103 RVA: 0x0039C09A File Offset: 0x0039A29A
		public float toxicity { get; set; }

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600A090 RID: 41104 RVA: 0x0039C0A3 File Offset: 0x0039A2A3
		// (set) Token: 0x0600A091 RID: 41105 RVA: 0x0039C0AB File Offset: 0x0039A2AB
		public float liquidCompression { get; set; }

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600A092 RID: 41106 RVA: 0x0039C0B4 File Offset: 0x0039A2B4
		// (set) Token: 0x0600A093 RID: 41107 RVA: 0x0039C0BC File Offset: 0x0039A2BC
		public float speed { get; set; }

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600A094 RID: 41108 RVA: 0x0039C0C5 File Offset: 0x0039A2C5
		// (set) Token: 0x0600A095 RID: 41109 RVA: 0x0039C0CD File Offset: 0x0039A2CD
		public float minHorizontalFlow { get; set; }

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x0600A096 RID: 41110 RVA: 0x0039C0D6 File Offset: 0x0039A2D6
		// (set) Token: 0x0600A097 RID: 41111 RVA: 0x0039C0DE File Offset: 0x0039A2DE
		public float minVerticalFlow { get; set; }

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x0600A098 RID: 41112 RVA: 0x0039C0E7 File Offset: 0x0039A2E7
		// (set) Token: 0x0600A099 RID: 41113 RVA: 0x0039C0EF File Offset: 0x0039A2EF
		public string convertId { get; set; }

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x0600A09A RID: 41114 RVA: 0x0039C0F8 File Offset: 0x0039A2F8
		// (set) Token: 0x0600A09B RID: 41115 RVA: 0x0039C100 File Offset: 0x0039A300
		public float flow { get; set; }

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600A09C RID: 41116 RVA: 0x0039C109 File Offset: 0x0039A309
		// (set) Token: 0x0600A09D RID: 41117 RVA: 0x0039C111 File Offset: 0x0039A311
		public int buildMenuSort { get; set; }

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600A09E RID: 41118 RVA: 0x0039C11A File Offset: 0x0039A31A
		// (set) Token: 0x0600A09F RID: 41119 RVA: 0x0039C122 File Offset: 0x0039A322
		public Element.State state { get; set; }

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600A0A0 RID: 41120 RVA: 0x0039C12B File Offset: 0x0039A32B
		// (set) Token: 0x0600A0A1 RID: 41121 RVA: 0x0039C133 File Offset: 0x0039A333
		public string localizationID { get; set; }

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600A0A2 RID: 41122 RVA: 0x0039C13C File Offset: 0x0039A33C
		// (set) Token: 0x0600A0A3 RID: 41123 RVA: 0x0039C144 File Offset: 0x0039A344
		public string dlcId { get; set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600A0A4 RID: 41124 RVA: 0x0039C14D File Offset: 0x0039A34D
		// (set) Token: 0x0600A0A5 RID: 41125 RVA: 0x0039C155 File Offset: 0x0039A355
		public string refinedMetalTarget { get; set; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600A0A6 RID: 41126 RVA: 0x0039C15E File Offset: 0x0039A35E
		// (set) Token: 0x0600A0A7 RID: 41127 RVA: 0x0039C166 File Offset: 0x0039A366
		public ElementLoader.ElementComposition[] composition { get; set; }

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x0600A0A8 RID: 41128 RVA: 0x0039C16F File Offset: 0x0039A36F
		// (set) Token: 0x0600A0A9 RID: 41129 RVA: 0x0039C19A File Offset: 0x0039A39A
		public string description
		{
			get
			{
				return this.description_backing ?? ("STRINGS.ELEMENTS." + this.elementId.ToString().ToUpper() + ".DESC");
			}
			set
			{
				this.description_backing = value;
			}
		}

		// Token: 0x04007D84 RID: 32132
		private string description_backing;
	}
}
