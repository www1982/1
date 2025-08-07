using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020002E5 RID: 741
public class BionicMinionConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000F20 RID: 3872 RVA: 0x0005AF46 File Offset: 0x00059146
	public static string[] GetAttributes()
	{
		return BaseMinionConfig.BaseMinionAttributes().Append(new string[]
		{
			Db.Get().Attributes.BionicBoosterSlots.Id,
			Db.Get().Attributes.BionicBatteryCountCapacity.Id
		});
	}

	// Token: 0x06000F21 RID: 3873 RVA: 0x0005AF88 File Offset: 0x00059188
	public static string[] GetAmounts()
	{
		return BaseMinionConfig.BaseMinionAmounts().Append(new string[]
		{
			Db.Get().Amounts.BionicOil.Id,
			Db.Get().Amounts.BionicGunk.Id,
			Db.Get().Amounts.BionicInternalBattery.Id,
			Db.Get().Amounts.BionicOxygenTank.Id
		});
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0005B001 File Offset: 0x00059201
	public static AttributeModifier[] GetTraits()
	{
		return BaseMinionConfig.BaseMinionTraits(BionicMinionConfig.MODEL);
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x0005B00D File Offset: 0x0005920D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0005B014 File Offset: 0x00059214
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0005B018 File Offset: 0x00059218
	public GameObject CreatePrefab()
	{
		GameObject gameObject = BaseMinionConfig.BaseMinion(BionicMinionConfig.MODEL, BionicMinionConfig.GetAttributes(), BionicMinionConfig.GetAmounts(), BionicMinionConfig.GetTraits());
		gameObject.AddOrGet<AttributeLevels>().maxAttributeLevel = 0;
		Storage storage = gameObject.AddComponent<Storage>();
		storage.storageID = GameTags.StoragesIds.BionicBatteryStorage;
		storage.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Preserve,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		storage.storageFilters = new List<Tag>(GameTags.BionicCompatibleBatteries);
		storage.allowItemRemoval = false;
		storage.showInUI = false;
		Storage storage2 = gameObject.AddComponent<Storage>();
		storage2.storageID = GameTags.StoragesIds.BionicUpgradeStorage;
		storage2.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Preserve,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		storage2.storageFilters = new List<Tag> { GameTags.BionicUpgrade };
		storage2.allowItemRemoval = false;
		storage2.showInUI = false;
		Storage storage3 = gameObject.AddComponent<Storage>();
		storage3.capacityKg = BionicOxygenTankMonitor.OXYGEN_TANK_CAPACITY_KG;
		storage3.storageID = GameTags.StoragesIds.BionicOxygenTankStorage;
		storage3.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Preserve,
			Storage.StoredItemModifier.Seal,
			Storage.StoredItemModifier.Insulate
		});
		storage3.allowItemRemoval = false;
		storage3.showInUI = false;
		ManualDeliveryKG manualDeliveryKG = gameObject.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.capacity = 0f;
		manualDeliveryKG.refillMass = 0f;
		manualDeliveryKG.handlePrioritizable = false;
		gameObject.AddOrGet<ReanimateBionicWorkable>();
		gameObject.AddOrGet<WarmBlooded>().complexity = WarmBlooded.ComplexityType.HomeostasisWithoutCaloriesImpact;
		gameObject.AddOrGet<BionicMinionStorageExtension>();
		gameObject.AddOrGet<MinionStorageDataHolder>();
		return gameObject;
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0005B1B0 File Offset: 0x000593B0
	public void OnPrefabInit(GameObject go)
	{
		BaseMinionConfig.BasePrefabInit(go, BionicMinionConfig.MODEL);
		AmountInstance amountInstance = Db.Get().Amounts.BionicOil.Lookup(go);
		amountInstance.value = amountInstance.GetMax();
		AmountInstance amountInstance2 = Db.Get().Amounts.BionicGunk.Lookup(go);
		amountInstance2.value = amountInstance2.GetMin();
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0005B208 File Offset: 0x00059408
	public void OnSpawn(GameObject go)
	{
		Sensors component = go.GetComponent<Sensors>();
		component.Add(new ClosestElectrobankSensor(component, true));
		component.Add(new ClosestOxygenCanisterSensor(component, false));
		component.Add(new ClosestLubricantSensor(component, false));
		BaseMinionConfig.BaseOnSpawn(go, BionicMinionConfig.MODEL, this.RATIONAL_AI_STATE_MACHINES);
		component.GetSensor<SafeCellSensor>().AddIgnoredFlagsSet(BionicMinionConfig.ID, SafeCellQuery.SafeFlags.IsBreathable);
		BionicOxygenTankMonitor.Instance smi = go.GetSMI<BionicOxygenTankMonitor.Instance>();
		if (smi != null)
		{
			go.GetComponent<OxygenBreather>().AddGasProvider(smi);
		}
		this.BionicFreeDiscoveries(go);
		go.Trigger(1589886948, go);
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x0005B28D File Offset: 0x0005948D
	private void BionicFreeDiscoveries(GameObject instance)
	{
		GameScheduler.Instance.Schedule("BionicUnlockCraftingTable", 8f, delegate(object data)
		{
			TechItem techItem = Db.Get().TechItems.Get("CraftingTable");
			if (!techItem.IsComplete())
			{
				Notifier component = Game.Instance.GetComponent<Notifier>();
				Notification notification = new Notification(MISC.NOTIFICATIONS.BIONICRESEARCHUNLOCK.NAME, NotificationType.MessageImportant, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.BIONICRESEARCHUNLOCK.MESSAGEBODY.Replace("{0}", Assets.GetPrefab("CraftingTable").GetProperName()), Assets.GetPrefab("CraftingTable").GetProperName(), true, 0f, null, null, null, true, true, false);
				component.Add(notification, "");
				techItem.POIUnlocked();
			}
			DiscoveredResources.Instance.Discover(PowerControlStationConfig.TINKER_TOOLS);
		}, null, null);
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0005B2C8 File Offset: 0x000594C8
	public BionicMinionConfig()
	{
		Func<RationalAi.Instance, StateMachine.Instance>[] array = BaseMinionConfig.BaseRationalAiStateMachines();
		Func<RationalAi.Instance, StateMachine.Instance>[] array2 = new Func<RationalAi.Instance, StateMachine.Instance>[10];
		array2[0] = (RationalAi.Instance smi) => new BreathMonitor.Instance(smi.master)
		{
			canRecoverBreath = false
		};
		array2[1] = (RationalAi.Instance smi) => new SteppedInMonitor.Instance(smi.master, new string[] { "CarpetFeet" });
		array2[2] = (RationalAi.Instance smi) => new BionicBatteryMonitor.Instance(smi.master, new BionicBatteryMonitor.Def());
		array2[3] = (RationalAi.Instance smi) => new BionicBedTimeMonitor.Instance(smi.master, new BionicBedTimeMonitor.Def());
		array2[4] = (RationalAi.Instance smi) => new BionicMicrochipMonitor.Instance(smi.master, new BionicMicrochipMonitor.Def());
		array2[5] = (RationalAi.Instance smi) => new BionicOilMonitor.Instance(smi.master, new BionicOilMonitor.Def());
		array2[6] = (RationalAi.Instance smi) => new GunkMonitor.Instance(smi.master, new GunkMonitor.Def());
		array2[7] = (RationalAi.Instance smi) => new BionicWaterDamageMonitor.Instance(smi.master, new BionicWaterDamageMonitor.Def());
		array2[8] = (RationalAi.Instance smi) => new BionicUpgradesMonitor.Instance(smi.master, new BionicUpgradesMonitor.Def());
		array2[9] = (RationalAi.Instance smi) => new BionicOxygenTankMonitor.Instance(smi.master, new BionicOxygenTankMonitor.Def());
		this.RATIONAL_AI_STATE_MACHINES = array.Append(array2);
		base..ctor();
	}

	// Token: 0x040009D5 RID: 2517
	public static Tag MODEL = GameTags.Minions.Models.Bionic;

	// Token: 0x040009D6 RID: 2518
	public static string NAME = DUPLICANTS.MODEL.BIONIC.NAME;

	// Token: 0x040009D7 RID: 2519
	public static string ID = BionicMinionConfig.MODEL.ToString();

	// Token: 0x040009D8 RID: 2520
	public static string[] DEFAULT_BIONIC_TRAITS = new string[] { "BionicBaseline" };

	// Token: 0x040009D9 RID: 2521
	public Func<RationalAi.Instance, StateMachine.Instance>[] RATIONAL_AI_STATE_MACHINES;
}
