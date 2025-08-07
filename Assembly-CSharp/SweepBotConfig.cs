using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003C8 RID: 968
public class SweepBotConfig : IEntityConfig
{
	// Token: 0x060013C4 RID: 5060 RVA: 0x000713E4 File Offset: 0x0006F5E4
	public GameObject CreatePrefab()
	{
		string text = "SweepBot";
		string text2 = this.name;
		string text3 = this.desc;
		float mass = SweepBotConfig.MASS;
		EffectorValues none = global::TUNING.BUILDINGS.DECOR.NONE;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, mass, Assets.GetAnim("sweep_bot_kanim"), "idle", Grid.SceneLayer.Creatures, 1, 1, none, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.GetComponent<KBatchedAnimController>().isMovable = true;
		KPrefabID kprefabID = gameObject.AddOrGet<KPrefabID>();
		kprefabID.AddTag(GameTags.Creature, false);
		kprefabID.AddTag(GameTags.Robot, false);
		gameObject.AddComponent<Pickupable>();
		gameObject.AddOrGet<Clearable>().isClearable = false;
		Trait trait = Db.Get().CreateTrait("SweepBotBaseTrait", this.name, this.name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.InternalBattery.maxAttribute.Id, 9000f, this.name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.InternalBattery.deltaAttribute.Id, -17.142857f, this.name, false, false, true));
		Modifiers modifiers = gameObject.AddOrGet<Modifiers>();
		modifiers.initialTraits.Add("SweepBotBaseTrait");
		modifiers.initialAmounts.Add(Db.Get().Amounts.HitPoints.Id);
		modifiers.initialAmounts.Add(Db.Get().Amounts.InternalBattery.Id);
		gameObject.AddOrGet<KBatchedAnimController>().SetSymbolVisiblity("snapto_pivot", false);
		gameObject.AddOrGet<Traits>();
		gameObject.AddOrGet<Effects>();
		gameObject.AddOrGetDef<AnimInterruptMonitor.Def>();
		gameObject.AddOrGetDef<StorageUnloadMonitor.Def>();
		RobotBatteryMonitor.Def def = gameObject.AddOrGetDef<RobotBatteryMonitor.Def>();
		def.batteryAmountId = Db.Get().Amounts.InternalBattery.Id;
		def.canCharge = true;
		def.lowBatteryWarningPercent = 0.5f;
		gameObject.AddOrGetDef<SweepBotReactMonitor.Def>();
		gameObject.AddOrGetDef<CreatureFallMonitor.Def>();
		gameObject.AddOrGetDef<SweepBotTrappedMonitor.Def>();
		gameObject.AddOrGetDef<DrinkMilkMonitor.Def>().consumesMilk = false;
		gameObject.AddOrGet<AnimEventHandler>();
		gameObject.AddOrGet<SnapOn>().snapPoints = new List<SnapOn.SnapPoint>(new SnapOn.SnapPoint[]
		{
			new SnapOn.SnapPoint
			{
				pointName = "carry",
				automatic = false,
				context = "",
				buildFile = null,
				overrideSymbol = "snapTo_ornament"
			}
		});
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		gameObject.AddComponent<Storage>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.capacityKg = 500f;
		storage.storageFXOffset = new Vector3(0f, 0.5f, 0f);
		gameObject.AddOrGet<OrnamentReceptacle>().AddDepositTag(GameTags.PedestalDisplayable);
		gameObject.AddOrGet<DecorProvider>();
		gameObject.AddOrGet<UserNameable>();
		gameObject.AddOrGet<CharacterOverlay>();
		gameObject.AddOrGet<ItemPedestal>();
		Navigator navigator = gameObject.AddOrGet<Navigator>();
		navigator.NavGridName = "WalkerBabyNavGrid";
		navigator.CurrentNavType = NavType.Floor;
		navigator.defaultSpeed = 1f;
		navigator.updateProber = true;
		navigator.maxProbingRadius = 32;
		navigator.sceneLayer = Grid.SceneLayer.Creatures;
		kprefabID.AddTag(GameTags.Creatures.Walker, false);
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new FallStates.Def(), true, -1).Add(new AnimInterruptStates.Def(), true, -1).Add(new SweepBotTrappedStates.Def(), true, -1)
			.Add(new DeliverToSweepLockerStates.Def(), true, -1)
			.Add(new ReturnToChargeStationStates.Def(), true, -1)
			.PushInterruptGroup()
			.Add(new DrinkMilkStates.Def
			{
				shouldBeBehindMilkTank = true
			}, true, -1)
			.PopInterruptGroup()
			.Add(new SweepStates.Def(), true, -1)
			.Add(new IdleStates.Def(), true, -1);
		gameObject.AddOrGet<LoopingSounds>();
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Robots.Models.SweepBot, null);
		return gameObject;
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x00071777 File Offset: 0x0006F977
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060013C6 RID: 5062 RVA: 0x0007177C File Offset: 0x0006F97C
	public void OnSpawn(GameObject inst)
	{
		StorageUnloadMonitor.Instance smi = inst.GetSMI<StorageUnloadMonitor.Instance>();
		smi.sm.internalStorage.Set(inst.GetComponents<Storage>()[1], smi, false);
		inst.GetComponent<OrnamentReceptacle>();
		inst.GetSMI<CreatureFallMonitor.Instance>().anim = "idle_loop";
	}

	// Token: 0x04000BFC RID: 3068
	public const string ID = "SweepBot";

	// Token: 0x04000BFD RID: 3069
	public const string BASE_TRAIT_ID = "SweepBotBaseTrait";

	// Token: 0x04000BFE RID: 3070
	public const float STORAGE_CAPACITY = 500f;

	// Token: 0x04000BFF RID: 3071
	public const float BATTERY_CAPACITY = 9000f;

	// Token: 0x04000C00 RID: 3072
	public const float BATTERY_DEPLETION_RATE = 17.142857f;

	// Token: 0x04000C01 RID: 3073
	public const float MAX_SWEEP_AMOUNT = 10f;

	// Token: 0x04000C02 RID: 3074
	public const float MOP_SPEED = 10f;

	// Token: 0x04000C03 RID: 3075
	private string name = global::STRINGS.ROBOTS.MODELS.SWEEPBOT.NAME;

	// Token: 0x04000C04 RID: 3076
	private string desc = global::STRINGS.ROBOTS.MODELS.SWEEPBOT.DESC;

	// Token: 0x04000C05 RID: 3077
	public static float MASS = 25f;
}
