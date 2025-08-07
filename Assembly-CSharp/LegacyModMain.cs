using System;
using System.Collections.Generic;
using System.Reflection;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000275 RID: 629
public class LegacyModMain
{
	// Token: 0x06000CB9 RID: 3257 RVA: 0x0004BE70 File Offset: 0x0004A070
	public static void Load()
	{
		List<Type> list = new List<Type>();
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		for (int i = 0; i < assemblies.Length; i++)
		{
			Type[] types = assemblies[i].GetTypes();
			if (types != null)
			{
				list.AddRange(types);
			}
		}
		EntityTemplates.CreateTemplates();
		EntityTemplates.CreateBaseOreTemplates();
		LegacyModMain.LoadOre(list);
		LegacyModMain.LoadBuildings(list);
		LegacyModMain.ConfigElements();
		LegacyModMain.LoadEntities(list);
		LegacyModMain.LoadEquipment(list);
		EntityTemplates.DestroyBaseOreTemplates();
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x0004BEDC File Offset: 0x0004A0DC
	private static void Test()
	{
		Dictionary<Type, int> dictionary = new Dictionary<Type, int>();
		global::UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(Component));
		for (int i = 0; i < array.Length; i++)
		{
			Type type = ((Component)array[i]).GetType();
			int num = 0;
			dictionary.TryGetValue(type, out num);
			dictionary[type] = num + 1;
		}
		List<LegacyModMain.Entry> list = new List<LegacyModMain.Entry>();
		foreach (KeyValuePair<Type, int> keyValuePair in dictionary)
		{
			if (keyValuePair.Key.GetMethod("Update", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy) != null)
			{
				list.Add(new LegacyModMain.Entry
				{
					count = keyValuePair.Value,
					type = keyValuePair.Key
				});
			}
		}
		list.Sort((LegacyModMain.Entry x, LegacyModMain.Entry y) => y.count.CompareTo(x.count));
		string text = "";
		foreach (LegacyModMain.Entry entry in list)
		{
			string[] array2 = new string[5];
			array2[0] = text;
			array2[1] = entry.type.Name;
			array2[2] = ": ";
			int num2 = 3;
			int i = entry.count;
			array2[num2] = i.ToString();
			array2[4] = "\n";
			text = string.Concat(array2);
		}
		global::Debug.Log(text);
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x0004C070 File Offset: 0x0004A270
	private static void ListUnusedTypes()
	{
		HashSet<Type> hashSet = new HashSet<Type>();
		global::UnityEngine.Object[] array = Resources.FindObjectsOfTypeAll(typeof(GameObject));
		for (int i = 0; i < array.Length; i++)
		{
			foreach (Component component in ((GameObject)array[i]).GetComponents<Component>())
			{
				if (!(component == null))
				{
					Type type = component.GetType();
					while (type != typeof(Component))
					{
						hashSet.Add(type);
						type = type.BaseType;
					}
				}
			}
		}
		HashSet<Type> hashSet2 = new HashSet<Type>();
		foreach (Type type2 in App.GetCurrentDomainTypes())
		{
			if (typeof(MonoBehaviour).IsAssignableFrom(type2) && !hashSet.Contains(type2))
			{
				hashSet2.Add(type2);
			}
		}
		List<Type> list = new List<Type>(hashSet2);
		list.Sort((Type x, Type y) => x.FullName.CompareTo(y.FullName));
		string text = "Unused types:";
		foreach (Type type3 in list)
		{
			text = text + "\n" + type3.FullName;
		}
		global::Debug.Log(text);
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x0004C1F8 File Offset: 0x0004A3F8
	private static void DebugSelected()
	{
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x0004C1FA File Offset: 0x0004A3FA
	private static void DebugSelected(GameObject go)
	{
		object component = go.GetComponent<Constructable>();
		int num = 0 + 1;
		global::Debug.Log(component);
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x0004C20B File Offset: 0x0004A40B
	private static void LoadOre(List<Type> types)
	{
		GeneratedOre.LoadGeneratedOre(types);
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x0004C214 File Offset: 0x0004A414
	private static void LoadBuildings(List<Type> types)
	{
		LocString.CreateLocStringKeys(typeof(BUILDINGS.PREFABS), "STRINGS.BUILDINGS.");
		LocString.CreateLocStringKeys(typeof(BUILDINGS.DAMAGESOURCES), "STRINGS.BUILDINGS.DAMAGESOURCES");
		LocString.CreateLocStringKeys(typeof(BUILDINGS.REPAIRABLE), "STRINGS.BUILDINGS.REPAIRABLE");
		LocString.CreateLocStringKeys(typeof(BUILDINGS.DISINFECTABLE), "STRINGS.BUILDINGS.DISINFECTABLE");
		GeneratedBuildings.LoadGeneratedBuildings(types);
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x0004C277 File Offset: 0x0004A477
	private static void LoadEntities(List<Type> types)
	{
		EntityConfigManager.Instance.LoadGeneratedEntities(types);
		BuildingConfigManager.Instance.ConfigurePost();
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x0004C28E File Offset: 0x0004A48E
	private static void LoadEquipment(List<Type> types)
	{
		LocString.CreateLocStringKeys(typeof(EQUIPMENT.PREFABS), "STRINGS.EQUIPMENT.");
		GeneratedEquipment.LoadGeneratedEquipment(types);
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x0004C2AC File Offset: 0x0004A4AC
	private static void ConfigElements()
	{
		foreach (LegacyModMain.ElementInfo elementInfo in new LegacyModMain.ElementInfo[]
		{
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Katairite,
				overheatMod = 200f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Cuprite,
				decor = 0.1f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Copper,
				decor = 0.2f,
				overheatMod = 50f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Gold,
				decor = 0.5f,
				overheatMod = 50f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Lead,
				overheatMod = -20f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Granite,
				decor = 0.2f,
				overheatMod = 15f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.SandStone,
				decor = 0.1f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.ToxicSand,
				overheatMod = -10f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Dirt,
				overheatMod = -10f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.IgneousRock,
				overheatMod = 15f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Obsidian,
				overheatMod = 15f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Ceramic,
				overheatMod = 200f,
				decor = 0.2f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.RefinedCarbon,
				overheatMod = 900f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Iron,
				overheatMod = 50f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Tungsten,
				overheatMod = 50f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Steel,
				overheatMod = 200f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.GoldAmalgam,
				overheatMod = 50f,
				decor = 0.1f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Diamond,
				overheatMod = 200f,
				decor = 1f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Niobium,
				decor = 0.5f,
				overheatMod = 500f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.TempConductorSolid,
				overheatMod = 900f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.HardPolypropylene,
				overheatMod = 900f
			},
			new LegacyModMain.ElementInfo
			{
				id = SimHashes.Iridium,
				overheatMod = 500f,
				decor = 0.2f
			}
		})
		{
			Element element = ElementLoader.FindElementByHash(elementInfo.id);
			if (elementInfo.decor != 0f)
			{
				AttributeModifier attributeModifier = new AttributeModifier("Decor", elementInfo.decor, element.name, true, false, true);
				element.attributeModifiers.Add(attributeModifier);
			}
			if (elementInfo.overheatMod != 0f)
			{
				AttributeModifier attributeModifier2 = new AttributeModifier(Db.Get().BuildingAttributes.OverheatTemperature.Id, elementInfo.overheatMod, element.name, false, false, true);
				element.attributeModifiers.Add(attributeModifier2);
			}
		}
	}

	// Token: 0x0200119E RID: 4510
	private struct Entry
	{
		// Token: 0x0400638D RID: 25485
		public int count;

		// Token: 0x0400638E RID: 25486
		public Type type;
	}

	// Token: 0x0200119F RID: 4511
	private struct ElementInfo
	{
		// Token: 0x0400638F RID: 25487
		public SimHashes id;

		// Token: 0x04006390 RID: 25488
		public float decor;

		// Token: 0x04006391 RID: 25489
		public float overheatMod;
	}
}
