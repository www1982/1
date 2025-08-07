using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class GeothermalControllerConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000B3F RID: 2879 RVA: 0x00043F58 File Offset: 0x00042158
	public static List<GeothermalVent.ElementInfo> GetClearingEntombedVentReward()
	{
		return new List<GeothermalVent.ElementInfo>
		{
			new GeothermalVent.ElementInfo
			{
				isSolid = false,
				elementHash = SimHashes.Steam,
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Steam).idx,
				mass = 100f,
				temperature = 1102f,
				diseaseIdx = byte.MaxValue,
				diseaseCount = 0
			},
			new GeothermalVent.ElementInfo
			{
				isSolid = true,
				elementHash = SimHashes.Lead,
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Lead).idx,
				mass = 144f,
				temperature = 502f,
				diseaseIdx = byte.MaxValue,
				diseaseCount = 0
			}
		};
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x00044034 File Offset: 0x00042234
	public static List<GeothermalControllerConfig.Impurity> GetImpurities()
	{
		return new List<GeothermalControllerConfig.Impurity>
		{
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.IgneousRock).idx,
				mass_kg = 50f,
				required_temp_range = new MathUtil.MinMax(0f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Granite).idx,
				mass_kg = 50f,
				required_temp_range = new MathUtil.MinMax(0f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Obsidian).idx,
				mass_kg = 50f,
				required_temp_range = new MathUtil.MinMax(0f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.SaltWater).idx,
				mass_kg = 320f,
				required_temp_range = new MathUtil.MinMax(0f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.DirtyWater).idx,
				mass_kg = 400f,
				required_temp_range = new MathUtil.MinMax(0f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Rust).idx,
				mass_kg = 125f,
				required_temp_range = new MathUtil.MinMax(330f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.MoltenLead).idx,
				mass_kg = 65f,
				required_temp_range = new MathUtil.MinMax(540f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.SulfurGas).idx,
				mass_kg = 30f,
				required_temp_range = new MathUtil.MinMax(700f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.SourGas).idx,
				mass_kg = 200f,
				required_temp_range = new MathUtil.MinMax(800f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.IronOre).idx,
				mass_kg = 50f,
				required_temp_range = new MathUtil.MinMax(850f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.MoltenAluminum).idx,
				mass_kg = 100f,
				required_temp_range = new MathUtil.MinMax(1200f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.MoltenCopper).idx,
				mass_kg = 100f,
				required_temp_range = new MathUtil.MinMax(1300f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.MoltenGold).idx,
				mass_kg = 100f,
				required_temp_range = new MathUtil.MinMax(1400f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Magma).idx,
				mass_kg = 75f,
				required_temp_range = new MathUtil.MinMax(1800f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Hydrogen).idx,
				mass_kg = 50f,
				required_temp_range = new MathUtil.MinMax(1800f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.MoltenIron).idx,
				mass_kg = 250f,
				required_temp_range = new MathUtil.MinMax(1900f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Wolframite).idx,
				mass_kg = 275f,
				required_temp_range = new MathUtil.MinMax(2000f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Fullerene).idx,
				mass_kg = 3f,
				required_temp_range = new MathUtil.MinMax(2500f, float.MaxValue)
			},
			new GeothermalControllerConfig.Impurity
			{
				elementIdx = ElementLoader.FindElementByHash(SimHashes.Niobium).idx,
				mass_kg = 5f,
				required_temp_range = new MathUtil.MinMax(2500f, float.MaxValue)
			}
		};
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0004458B File Offset: 0x0004278B
	public static float CalculateOutputTemperature(float inputTemperature)
	{
		if (inputTemperature < 1650f)
		{
			return Math.Min(1650f, inputTemperature + 150f);
		}
		return Math.Max(1650f, inputTemperature - 150f);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x000445B8 File Offset: 0x000427B8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x000445BF File Offset: 0x000427BF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000445C4 File Offset: 0x000427C4
	GameObject IEntityConfig.CreatePrefab()
	{
		string text = "GeothermalControllerEntity";
		string text2 = global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALCONTROLLER.NAME;
		string text3 = global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALCONTROLLER.EFFECT + "\n\n" + global::STRINGS.BUILDINGS.PREFABS.GEOTHERMALCONTROLLER.DESC;
		float num = 100f;
		EffectorValues tier = global::TUNING.BUILDINGS.DECOR.PENALTY.TIER4;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("gravitas_geoplant_kanim"), "off", Grid.SceneLayer.BuildingBack, 7, 8, tier, tier2, SimHashes.Unobtanium, new List<Tag> { GameTags.Gravitas }, 293f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddComponent<EntityCellVisualizer>();
		gameObject.AddComponent<GeothermalController>();
		gameObject.AddComponent<GeothermalPlantComponent>();
		gameObject.AddComponent<Operational>();
		gameObject.AddComponent<GeothermalController.ReconnectPipes>();
		gameObject.AddComponent<Notifier>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.showDescriptor = false;
		storage.showInUI = false;
		storage.capacityKg = 12000f;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Insulate,
			Storage.StoredItemModifier.Seal
		});
		return gameObject;
	}

	// Token: 0x06000B45 RID: 2885 RVA: 0x000446C7 File Offset: 0x000428C7
	void IEntityConfig.OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000B46 RID: 2886 RVA: 0x000446C9 File Offset: 0x000428C9
	void IEntityConfig.OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400079F RID: 1951
	public const string ID = "GeothermalControllerEntity";

	// Token: 0x040007A0 RID: 1952
	public const string KEEPSAKE_ID = "keepsake_geothermalplant";

	// Token: 0x040007A1 RID: 1953
	public const string COMPLETED_LORE_ENTRY_UNLOCK_ID = "notes_earthquake";

	// Token: 0x040007A2 RID: 1954
	private const string ANIM_FILE = "gravitas_geoplant_kanim";

	// Token: 0x040007A3 RID: 1955
	public const string OFFLINE_ANIM = "off";

	// Token: 0x040007A4 RID: 1956
	public const string ONLINE_ANIM = "on";

	// Token: 0x040007A5 RID: 1957
	public const string OBSTRUCTED_ANIM = "on";

	// Token: 0x040007A6 RID: 1958
	public const float WORKING_LOOP_DURATION_SECONDS = 16f;

	// Token: 0x040007A7 RID: 1959
	public const float HEATPUMP_CAPACITY_KG = 12000f;

	// Token: 0x040007A8 RID: 1960
	public const float OUTPUT_TARGET_TEMPERATURE = 1650f;

	// Token: 0x040007A9 RID: 1961
	public const float OUTPUT_DELTA_TEMPERATURE = 150f;

	// Token: 0x040007AA RID: 1962
	public const float OUTPUT_PASSTHROUGH_RATIO = 0.92f;

	// Token: 0x040007AB RID: 1963
	public static MathUtil.MinMax OUTPUT_VENT_WEIGHT_RANGE = new MathUtil.MinMax(43f, 57f);

	// Token: 0x040007AC RID: 1964
	public static HashSet<Tag> STEEL_FETCH_TAGS = new HashSet<Tag> { GameTags.Steel };

	// Token: 0x040007AD RID: 1965
	public const float STEEL_FETCH_QUANTITY_KG = 1200f;

	// Token: 0x040007AE RID: 1966
	public const float RECONNECT_PUMP_CHORE_DURATION_SECONDS = 5f;

	// Token: 0x040007AF RID: 1967
	public static HashedString RECONNECT_PUMP_ANIM_OVERRIDE = "anim_use_remote_kanim";

	// Token: 0x040007B0 RID: 1968
	public const string BAROMETER_ANIM = "meter";

	// Token: 0x040007B1 RID: 1969
	public const string BAROMETER_TARGET = "meter_target";

	// Token: 0x040007B2 RID: 1970
	public static string[] BAROMETER_SYMBOLS = new string[] { "meter_target" };

	// Token: 0x040007B3 RID: 1971
	public const string THERMOMETER_ANIM = "meter_temp";

	// Token: 0x040007B4 RID: 1972
	public const string THERMOMETER_TARGET = "meter_target";

	// Token: 0x040007B5 RID: 1973
	public static string[] THERMOMETER_SYMBOLS = new string[] { "meter_target" };

	// Token: 0x040007B6 RID: 1974
	public const float THERMOMETER_MIN_TEMP = 50f;

	// Token: 0x040007B7 RID: 1975
	public const float THERMOMETER_RANGE = 2450f;

	// Token: 0x040007B8 RID: 1976
	public static HashedString[] PRESSURE_ANIM_LOOPS = new HashedString[] { "pressure_loop", "high_pressure_loop", "high_pressure_loop2" };

	// Token: 0x040007B9 RID: 1977
	public static float[] PRESSURE_ANIM_THRESHOLDS = new float[] { 0f, 0.35f, 0.85f };

	// Token: 0x040007BA RID: 1978
	public const float CLEAR_ENTOMBED_VENT_THRESHOLD_TEMPERATURE = 602f;

	// Token: 0x0200118E RID: 4494
	public struct Impurity
	{
		// Token: 0x04006371 RID: 25457
		public ushort elementIdx;

		// Token: 0x04006372 RID: 25458
		public float mass_kg;

		// Token: 0x04006373 RID: 25459
		public MathUtil.MinMax required_temp_range;
	}
}
