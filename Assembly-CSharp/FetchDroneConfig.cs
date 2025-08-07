using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003C7 RID: 967
public class FetchDroneConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060013BD RID: 5053 RVA: 0x00070821 File Offset: 0x0006EA21
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x00070828 File Offset: 0x0006EA28
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x0007082C File Offset: 0x0006EA2C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateBasicEntity("FetchDrone", this.name, this.desc, 200f, true, Assets.GetAnim("swoopy_bot_kanim"), "idle_loop", Grid.SceneLayer.Move, SimHashes.Creature, new List<Tag> { GameTags.Experimental }, 293f);
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		component.isMovable = true;
		gameObject.AddOrGet<LoopingSounds>();
		KBoxCollider2D kboxCollider2D = gameObject.AddOrGet<KBoxCollider2D>();
		kboxCollider2D.size = new Vector2(1f, 1f);
		kboxCollider2D.offset = new Vector2f(0f, 0.5f);
		Modifiers modifiers = gameObject.AddOrGet<Modifiers>();
		modifiers.initialAmounts.Add(Db.Get().Amounts.HitPoints.Id);
		modifiers.initialAttributes.Add(Db.Get().Attributes.CarryAmount.Id);
		modifiers.initialAmounts.Add(Db.Get().Amounts.InternalElectroBank.Id);
		string text = "FetchDroneBaseTrait";
		gameObject.AddOrGet<Traits>();
		Trait trait = Db.Get().CreateTrait(text, this.name, this.name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Attributes.CarryAmount.Id, global::TUNING.ROBOTS.FETCHDRONE.CARRY_CAPACITY, this.name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.InternalElectroBank.maxAttribute.Id, 120000f, this.name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.InternalElectroBank.deltaAttribute.Id, -50f, this.name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, global::TUNING.ROBOTS.FETCHDRONE.HIT_POINTS, this.name, false, false, true));
		modifiers.initialTraits.Add(text);
		gameObject.AddOrGet<AttributeConverters>();
		GridVisibility gridVisibility = gameObject.AddOrGet<GridVisibility>();
		gridVisibility.radius = 30;
		gridVisibility.innerRadius = 20f;
		gameObject.AddOrGet<StandardWorker>().isFetchDrone = true;
		gameObject.AddOrGet<Effects>();
		gameObject.AddOrGet<Traits>();
		gameObject.AddOrGet<AnimEventHandler>();
		MoverLayerOccupier moverLayerOccupier = gameObject.AddOrGet<MoverLayerOccupier>();
		moverLayerOccupier.objectLayers = new ObjectLayer[]
		{
			ObjectLayer.Rover,
			ObjectLayer.Mover
		};
		moverLayerOccupier.cellOffsets = new CellOffset[]
		{
			CellOffset.none,
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<FetchDrone>();
		Storage storage = gameObject.AddComponent<Storage>();
		storage.fxPrefix = Storage.FXPrefix.PickedUp;
		storage.dropOnLoad = true;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		gameObject.AddOrGetDef<DebugGoToMonitor.Def>();
		Deconstructable deconstructable = gameObject.AddOrGet<Deconstructable>();
		deconstructable.enabled = false;
		deconstructable.audioSize = "medium";
		deconstructable.looseEntityDeconstructable = true;
		Storage storage2 = gameObject.AddComponent<Storage>();
		storage2.storageID = GameTags.ChargedPortableBattery;
		storage2.showInUI = true;
		storage2.storageFilters = new List<Tag> { GameTags.ChargedPortableBattery };
		storage2.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
		{
			Storage.StoredItemModifier.Hide,
			Storage.StoredItemModifier.Insulate
		});
		TreeFilterable treeFilterable = gameObject.AddOrGet<TreeFilterable>();
		treeFilterable.storageToFilterTag = storage2.storageID;
		treeFilterable.dropIncorrectOnFilterChange = false;
		treeFilterable.tintOnNoFiltersSet = false;
		ManualDeliveryKG manualDeliveryKG = gameObject.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage2);
		manualDeliveryKG.RequestedItemTag = GameTags.ChargedPortableBattery;
		manualDeliveryKG.capacity = 21f;
		manualDeliveryKG.refillMass = 21f;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.RepairFetch.IdHash;
		gameObject.AddOrGetDef<RobotElectroBankMonitor.Def>().lowBatteryWarningPercent = 0.2f;
		gameObject.AddOrGetDef<RobotAi.Def>().DeleteOnDead = true;
		ChoreTable.Builder builder = new ChoreTable.Builder().Add(new RobotDeathStates.Def
		{
			deathAnim = "idle_dead"
		}, true, Db.Get().ChoreTypes.Die.priority).Add(new RobotElectroBankDeadStates.Def(), true, Db.Get().ChoreTypes.Die.priority).Add(new DebugGoToStates.Def(), true, -1)
			.Add(new DrowningStates.Def(), true, -1)
			.Add(new IdleStates.Def
			{
				priorityClass = PriorityScreen.PriorityClass.idle
			}, true, Db.Get().ChoreTypes.Idle.priority);
		EntityTemplates.AddCreatureBrain(gameObject, builder, GameTags.Robots.Models.FetchDrone, null);
		KPrefabID kprefabID = gameObject.AddOrGet<KPrefabID>();
		kprefabID.RemoveTag(GameTags.CreatureBrain);
		kprefabID.AddTag(GameTags.DupeBrain, false);
		kprefabID.AddTag(GameTags.Robot, false);
		Navigator navigator = gameObject.AddOrGet<Navigator>();
		navigator.NavGridName = "FlyerNavGrid1x1";
		navigator.CurrentNavType = NavType.Hover;
		navigator.defaultSpeed = 2f;
		navigator.updateProber = true;
		navigator.sceneLayer = Grid.SceneLayer.Creatures;
		gameObject.AddOrGet<Sensors>();
		Pickupable pickupable = gameObject.AddOrGet<Pickupable>();
		pickupable.handleFallerComponents = false;
		pickupable.SetWorkTime(5f);
		gameObject.AddOrGet<Clearable>().isClearable = false;
		gameObject.AddOrGet<SnapOn>();
		gameObject.AddOrGet<Movable>();
		FetchDroneConfig.SetupLaserEffects(gameObject);
		component.SetSymbolVisiblity("snapto_pivot", false);
		component.SetSymbolVisiblity("snapto_thing", false);
		component.SetSymbolVisiblity("snapTo_chest", false);
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddComponent<OccupyArea>().SetCellOffsets(new CellOffset[] { CellOffset.none });
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGetDef<SubmergedMonitor.Def>();
		gameObject.AddOrGet<Health>();
		gameObject.AddOrGetDef<MoveToLocationMonitor.Def>().invalidTagsForMoveTo = new Tag[] { GameTags.Robots.Behaviours.NoElectroBank };
		SymbolOverrideControllerUtil.AddToPrefab(gameObject);
		gameObject.AddOrGet<CopyBuildingSettings>();
		return gameObject;
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x00070D9C File Offset: 0x0006EF9C
	private static void SetupLaserEffects(GameObject prefab)
	{
		GameObject gameObject = new GameObject("LaserEffect");
		gameObject.transform.parent = prefab.transform;
		KBatchedAnimEventToggler kbatchedAnimEventToggler = gameObject.AddComponent<KBatchedAnimEventToggler>();
		kbatchedAnimEventToggler.eventSource = prefab;
		kbatchedAnimEventToggler.enableEvent = "LaserOn";
		kbatchedAnimEventToggler.disableEvent = "LaserOff";
		kbatchedAnimEventToggler.entries = new List<KBatchedAnimEventToggler.Entry>();
		FetchDroneConfig.LaserEffect[] array = new FetchDroneConfig.LaserEffect[]
		{
			new FetchDroneConfig.LaserEffect
			{
				id = "DigEffect",
				animFile = "laser_kanim",
				anim = "idle",
				context = "dig"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "BuildEffect",
				animFile = "construct_beam_kanim",
				anim = "loop",
				context = "build"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "FetchLiquidEffect",
				animFile = "hose_fx_kanim",
				anim = "loop",
				context = "fetchliquid"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "PaintEffect",
				animFile = "paint_beam_kanim",
				anim = "loop",
				context = "paint"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "HarvestEffect",
				animFile = "plant_harvest_beam_kanim",
				anim = "loop",
				context = "harvest"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "CaptureEffect",
				animFile = "net_gun_fx_kanim",
				anim = "loop",
				context = "capture"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "AttackEffect",
				animFile = "attack_beam_fx_kanim",
				anim = "loop",
				context = "attack"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "PickupEffect",
				animFile = "vacuum_fx_kanim",
				anim = "loop",
				context = "pickup"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "StoreEffect",
				animFile = "vacuum_reverse_fx_kanim",
				anim = "loop",
				context = "store"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "DisinfectEffect",
				animFile = "plant_spray_beam_kanim",
				anim = "loop",
				context = "disinfect"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "TendEffect",
				animFile = "plant_tending_beam_fx_kanim",
				anim = "loop",
				context = "tend"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "PowerTinkerEffect",
				animFile = "electrician_beam_fx_kanim",
				anim = "idle",
				context = "powertinker"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "SpecialistDigEffect",
				animFile = "senior_miner_beam_fx_kanim",
				anim = "idle",
				context = "specialistdig"
			},
			new FetchDroneConfig.LaserEffect
			{
				id = "DemolishEffect",
				animFile = "poi_demolish_fx_kanim",
				anim = "idle",
				context = "demolish"
			}
		};
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		foreach (FetchDroneConfig.LaserEffect laserEffect in array)
		{
			GameObject gameObject2 = new GameObject(laserEffect.id);
			gameObject2.transform.parent = gameObject.transform;
			gameObject2.AddOrGet<KPrefabID>().PrefabTag = new Tag(laserEffect.id);
			KBatchedAnimTracker kbatchedAnimTracker = gameObject2.AddOrGet<KBatchedAnimTracker>();
			kbatchedAnimTracker.controller = component;
			kbatchedAnimTracker.symbol = new HashedString("snapto_thing");
			kbatchedAnimTracker.offset = new Vector3(40f, 0f, 0f);
			kbatchedAnimTracker.useTargetPoint = true;
			KBatchedAnimController kbatchedAnimController = gameObject2.AddOrGet<KBatchedAnimController>();
			kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(laserEffect.animFile) };
			KBatchedAnimEventToggler.Entry entry = new KBatchedAnimEventToggler.Entry
			{
				anim = laserEffect.anim,
				context = laserEffect.context,
				controller = kbatchedAnimController
			};
			kbatchedAnimEventToggler.entries.Add(entry);
			gameObject2.AddOrGet<LoopingSounds>();
		}
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x000712DC File Offset: 0x0006F4DC
	public void OnPrefabInit(GameObject inst)
	{
		ChoreConsumer component = inst.GetComponent<ChoreConsumer>();
		if (component != null)
		{
			component.AddProvider(GlobalChoreProvider.Instance);
		}
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x00071304 File Offset: 0x0006F504
	public void OnSpawn(GameObject inst)
	{
		Sensors component = inst.GetComponent<Sensors>();
		component.Add(new PathProberSensor(component));
		component.Add(new PickupableSensor(component));
		PathProber component2 = inst.GetComponent<PathProber>();
		if (component2 != null)
		{
			component2.SetGroupProber(MinionGroupProber.Get());
		}
		inst.GetComponent<LoopingSounds>().StartSound(GlobalAssets.GetSound("Flydo_flying_LP", false));
		Movable component3 = inst.GetComponent<Movable>();
		component3.tagRequiredForMove = GameTags.Robots.Behaviours.NoElectroBank;
		component3.onDeliveryComplete = delegate(GameObject go)
		{
			go.GetComponent<KBatchedAnimController>().Play("dead_battery", KAnim.PlayMode.Once, 1f, 0f);
		};
		component3.onPickupComplete = delegate(GameObject go)
		{
			go.GetComponent<KBatchedAnimController>().Play("in_storage", KAnim.PlayMode.Once, 1f, 0f);
		};
	}

	// Token: 0x04000BF3 RID: 3059
	public const string ID = "FetchDrone";

	// Token: 0x04000BF4 RID: 3060
	public const SimHashes MATERIAL = SimHashes.Steel;

	// Token: 0x04000BF5 RID: 3061
	public const float MASS = 200f;

	// Token: 0x04000BF6 RID: 3062
	private const float WIDTH = 1f;

	// Token: 0x04000BF7 RID: 3063
	private const float HEIGHT = 1f;

	// Token: 0x04000BF8 RID: 3064
	private const float CARRY_AMOUNT = 200f;

	// Token: 0x04000BF9 RID: 3065
	private const float HIT_POINTS = 50f;

	// Token: 0x04000BFA RID: 3066
	private string name = global::STRINGS.ROBOTS.MODELS.FLYDO.NAME;

	// Token: 0x04000BFB RID: 3067
	private string desc = global::STRINGS.ROBOTS.MODELS.FLYDO.DESC;

	// Token: 0x0200120A RID: 4618
	public struct LaserEffect
	{
		// Token: 0x040064E5 RID: 25829
		public string id;

		// Token: 0x040064E6 RID: 25830
		public string animFile;

		// Token: 0x040064E7 RID: 25831
		public string anim;

		// Token: 0x040064E8 RID: 25832
		public HashedString context;
	}
}
