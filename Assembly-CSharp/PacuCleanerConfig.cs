using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000145 RID: 325
[EntityConfigOrder(1)]
public class PacuCleanerConfig : IEntityConfig
{
	// Token: 0x06000616 RID: 1558 RVA: 0x0002D140 File Offset: 0x0002B340
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePacuConfig.CreatePrefab(id, "PacuCleanerBaseTrait", name, desc, anim_file, is_baby, "glp_", 243.15f, 278.15f, 223.15f, 298.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PacuTuning.PEN_SIZE_PER_CREATURE, false);
		if (!is_baby)
		{
			Storage storage = gameObject.AddComponent<Storage>();
			storage.capacityKg = 10f;
			storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
			PassiveElementConsumer passiveElementConsumer = gameObject.AddOrGet<PassiveElementConsumer>();
			passiveElementConsumer.elementToConsume = SimHashes.DirtyWater;
			passiveElementConsumer.consumptionRate = 0.2f;
			passiveElementConsumer.capacityKG = 10f;
			passiveElementConsumer.consumptionRadius = 3;
			passiveElementConsumer.showInStatusPanel = true;
			passiveElementConsumer.sampleCellOffset = new Vector3(0f, 0f, 0f);
			passiveElementConsumer.isRequired = false;
			passiveElementConsumer.storeOnConsume = true;
			passiveElementConsumer.showDescriptor = false;
			gameObject.AddOrGet<UpdateElementConsumerPosition>();
			BubbleSpawner bubbleSpawner = gameObject.AddComponent<BubbleSpawner>();
			bubbleSpawner.element = SimHashes.Water;
			bubbleSpawner.emitMass = 2f;
			bubbleSpawner.emitVariance = 0.5f;
			bubbleSpawner.initialVelocity = new Vector2f(0, 1);
			ElementConverter elementConverter = gameObject.AddOrGet<ElementConverter>();
			elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
			{
				new ElementConverter.ConsumedElement(SimHashes.DirtyWater.CreateTag(), 0.2f, true)
			};
			elementConverter.outputElements = new ElementConverter.OutputElement[]
			{
				new ElementConverter.OutputElement(0.2f, SimHashes.Water, 0f, true, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
			};
		}
		return gameObject;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002D2B8 File Offset: 0x0002B4B8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(EntityTemplates.ExtendEntityToWildCreature(PacuCleanerConfig.CreatePacu("PacuCleaner", global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.NAME, global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.DESC, "pacu_kanim", false), PacuTuning.PEN_SIZE_PER_CREATURE, false), this as IHasDlcRestrictions, "PacuCleanerEgg", global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_CLEANER.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuCleanerBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_CLEANER, 501, false, true, 0.75f, false);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002D343 File Offset: 0x0002B543
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002D348 File Offset: 0x0002B548
	public void OnSpawn(GameObject inst)
	{
		ElementConsumer component = inst.GetComponent<ElementConsumer>();
		if (component != null)
		{
			component.EnableConsumption(true);
		}
	}

	// Token: 0x0400048E RID: 1166
	public const string ID = "PacuCleaner";

	// Token: 0x0400048F RID: 1167
	public const string BASE_TRAIT_ID = "PacuCleanerBaseTrait";

	// Token: 0x04000490 RID: 1168
	public const string EGG_ID = "PacuCleanerEgg";

	// Token: 0x04000491 RID: 1169
	public const float POLLUTED_WATER_CONVERTED_PER_CYCLE = 120f;

	// Token: 0x04000492 RID: 1170
	public const SimHashes INPUT_ELEMENT = SimHashes.DirtyWater;

	// Token: 0x04000493 RID: 1171
	public const SimHashes OUTPUT_ELEMENT = SimHashes.Water;

	// Token: 0x04000494 RID: 1172
	public static readonly EffectorValues DECOR = global::TUNING.BUILDINGS.DECOR.BONUS.TIER4;

	// Token: 0x04000495 RID: 1173
	public const int EGG_SORT_ORDER = 501;
}
