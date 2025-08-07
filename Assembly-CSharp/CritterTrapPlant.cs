using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000A5F RID: 2655
public class CritterTrapPlant : StateMachineComponent<CritterTrapPlant.StatesInstance>, IPlantConsumeEntities
{
	// Token: 0x06004CE6 RID: 19686 RVA: 0x001BDE8C File Offset: 0x001BC08C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.master.growing.enabled = false;
		base.Subscribe<CritterTrapPlant>(-216549700, CritterTrapPlant.OnUprootedDelegate);
		base.smi.StartSM();
	}

	// Token: 0x06004CE7 RID: 19687 RVA: 0x001BDEC6 File Offset: 0x001BC0C6
	public void RefreshPositionPercent()
	{
		this.animController.SetPositionPercent(this.growing.PercentOfCurrentHarvest());
	}

	// Token: 0x06004CE8 RID: 19688 RVA: 0x001BDEE0 File Offset: 0x001BC0E0
	private void OnUprooted(object data = null)
	{
		GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), base.gameObject.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
		base.gameObject.Trigger(1623392196, null);
		base.gameObject.GetComponent<KBatchedAnimController>().StopAndClear();
		global::UnityEngine.Object.Destroy(base.gameObject.GetComponent<KBatchedAnimController>());
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004CE9 RID: 19689 RVA: 0x001BDF57 File Offset: 0x001BC157
	protected void DestroySelf(object callbackParam)
	{
		CreatureHelpers.DeselectCreature(base.gameObject);
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x06004CEA RID: 19690 RVA: 0x001BDF70 File Offset: 0x001BC170
	public Notification CreateDeathNotification()
	{
		return new Notification(CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION, NotificationType.Bad, (List<Notification> notificationList, object data) => CREATURES.STATUSITEMS.PLANTDEATH.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), "/t• " + base.gameObject.GetProperName(), true, 0f, null, null, null, true, false, false);
	}

	// Token: 0x06004CEB RID: 19691 RVA: 0x001BDFCD File Offset: 0x001BC1CD
	public string GetConsumableEntitiesCategoryName()
	{
		return CREATURES.SPECIES.CRITTERTRAPPLANT.VICTIM_IDENTIFIER;
	}

	// Token: 0x06004CEC RID: 19692 RVA: 0x001BDFD9 File Offset: 0x001BC1D9
	public string GetRequirementText()
	{
		return CREATURES.SPECIES.CRITTERTRAPPLANT.PLANT_HUNGER_REQUIREMENT;
	}

	// Token: 0x06004CED RID: 19693 RVA: 0x001BDFE5 File Offset: 0x001BC1E5
	public bool AreEntitiesConsumptionRequirementsSatisfied()
	{
		return base.smi != null && base.smi.sm.hasEatenCreature.Get(base.smi);
	}

	// Token: 0x06004CEE RID: 19694 RVA: 0x001BE00C File Offset: 0x001BC20C
	public string GetConsumedEntityName()
	{
		if (base.smi != null)
		{
			return base.smi.LastConsumedEntityName;
		}
		return "Unknown Critter";
	}

	// Token: 0x06004CEF RID: 19695 RVA: 0x001BE028 File Offset: 0x001BC228
	public List<KPrefabID> GetPrefabsOfPossiblePrey()
	{
		List<GameObject> prefabsWithComponent = Assets.GetPrefabsWithComponent<CreatureBrain>();
		List<KPrefabID> list = new List<KPrefabID>();
		for (int i = 0; i < prefabsWithComponent.Count; i++)
		{
			KPrefabID component = prefabsWithComponent[i].GetComponent<KPrefabID>();
			if (!list.Contains(component) && this.IsEntityEdible(component) && Game.IsCorrectDlcActiveForCurrentSave(component))
			{
				list.Add(component);
			}
		}
		return list;
	}

	// Token: 0x06004CF0 RID: 19696 RVA: 0x001BE084 File Offset: 0x001BC284
	public string[] GetFormattedPossiblePreyList()
	{
		List<string> list = new List<string>();
		foreach (KPrefabID kprefabID in this.GetPrefabsOfPossiblePrey())
		{
			CreatureBrain component = kprefabID.GetComponent<CreatureBrain>();
			if (component != null)
			{
				string text = component.species.ProperName();
				if (!list.Contains(text))
				{
					list.Add(text);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06004CF1 RID: 19697 RVA: 0x001BE108 File Offset: 0x001BC308
	public bool IsEntityEdible(GameObject entity)
	{
		return this.IsEntityEdible(entity.GetComponent<KPrefabID>());
	}

	// Token: 0x06004CF2 RID: 19698 RVA: 0x001BE116 File Offset: 0x001BC316
	public bool IsEntityEdible(KPrefabID entity)
	{
		return entity.HasAnyTags(this.CONSUMABLE_TAGs) && entity.GetComponent<Trappable>() != null && entity.GetComponent<OccupyArea>().OccupiedCellsOffsets.Length < 3;
	}

	// Token: 0x0400330D RID: 13069
	private const string CONSUMED_ENTITY_NAME_FALLBACK = "Unknown Critter";

	// Token: 0x0400330E RID: 13070
	[MyCmpReq]
	private Crop crop;

	// Token: 0x0400330F RID: 13071
	[MyCmpReq]
	private WiltCondition wiltCondition;

	// Token: 0x04003310 RID: 13072
	[MyCmpReq]
	private ReceptacleMonitor rm;

	// Token: 0x04003311 RID: 13073
	[MyCmpReq]
	private Growing growing;

	// Token: 0x04003312 RID: 13074
	[MyCmpReq]
	private KAnimControllerBase animController;

	// Token: 0x04003313 RID: 13075
	[MyCmpReq]
	private Harvestable harvestable;

	// Token: 0x04003314 RID: 13076
	[MyCmpReq]
	private Storage storage;

	// Token: 0x04003315 RID: 13077
	public Tag[] CONSUMABLE_TAGs = new Tag[0];

	// Token: 0x04003316 RID: 13078
	public float gasOutputRate;

	// Token: 0x04003317 RID: 13079
	public float gasVentThreshold;

	// Token: 0x04003318 RID: 13080
	public SimHashes outputElement;

	// Token: 0x04003319 RID: 13081
	private float GAS_TEMPERATURE_DELTA = 10f;

	// Token: 0x0400331A RID: 13082
	private static readonly EventSystem.IntraObjectHandler<CritterTrapPlant> OnUprootedDelegate = new EventSystem.IntraObjectHandler<CritterTrapPlant>(delegate(CritterTrapPlant component, object data)
	{
		component.OnUprooted(data);
	});

	// Token: 0x02001B1E RID: 6942
	public class StatesInstance : GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.GameInstance
	{
		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600A621 RID: 42529 RVA: 0x003AB486 File Offset: 0x003A9686
		public string LastConsumedEntityName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.lastConsumedEntityPrefabID))
				{
					return Assets.GetPrefab(this.lastConsumedEntityPrefabID).GetProperName();
				}
				return "Unknown Critter";
			}
		}

		// Token: 0x0600A622 RID: 42530 RVA: 0x003AB4B0 File Offset: 0x003A96B0
		public StatesInstance(CritterTrapPlant master)
			: base(master)
		{
		}

		// Token: 0x0600A623 RID: 42531 RVA: 0x003AB4B9 File Offset: 0x003A96B9
		public void OnTrapTriggered(object data)
		{
			base.smi.sm.trapTriggered.Trigger(base.smi);
		}

		// Token: 0x0600A624 RID: 42532 RVA: 0x003AB4D8 File Offset: 0x003A96D8
		public void AddGas(float dt)
		{
			float num = base.smi.GetComponent<PrimaryElement>().Temperature + base.smi.master.GAS_TEMPERATURE_DELTA;
			base.smi.master.storage.AddGasChunk(base.smi.master.outputElement, base.smi.master.gasOutputRate * dt, num, byte.MaxValue, 0, false, true);
			if (this.ShouldVentGas())
			{
				base.smi.sm.ventGas.Trigger(base.smi);
			}
		}

		// Token: 0x0600A625 RID: 42533 RVA: 0x003AB56C File Offset: 0x003A976C
		public void VentGas()
		{
			PrimaryElement primaryElement = base.smi.master.storage.FindPrimaryElement(base.smi.master.outputElement);
			if (primaryElement != null)
			{
				SimMessages.AddRemoveSubstance(Grid.PosToCell(base.smi.transform.GetPosition()), primaryElement.ElementID, CellEventLogger.Instance.Dumpable, primaryElement.Mass, primaryElement.Temperature, primaryElement.DiseaseIdx, primaryElement.DiseaseCount, true, -1);
				base.smi.master.storage.ConsumeIgnoringDisease(primaryElement.gameObject);
			}
		}

		// Token: 0x0600A626 RID: 42534 RVA: 0x003AB608 File Offset: 0x003A9808
		public bool ShouldVentGas()
		{
			PrimaryElement primaryElement = base.smi.master.storage.FindPrimaryElement(base.smi.master.outputElement);
			return !(primaryElement == null) && primaryElement.Mass >= base.smi.master.gasVentThreshold;
		}

		// Token: 0x040081AD RID: 33197
		[Serialize]
		public string lastConsumedEntityPrefabID;
	}

	// Token: 0x02001B1F RID: 6943
	public class States : GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant>
	{
		// Token: 0x0600A627 RID: 42535 RVA: 0x003AB664 File Offset: 0x003A9864
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			default_state = this.trap;
			this.trap.DefaultState(this.trap.open);
			this.trap.open.ToggleComponent<TrapTrigger>(false).ToggleStatusItem(Db.Get().CreatureStatusItems.CarnivorousPlantAwaitingVictim, (CritterTrapPlant.StatesInstance smi) => smi.master.GetComponent<IPlantConsumeEntities>()).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				smi.VentGas();
				smi.master.storage.ConsumeAllIgnoringDisease();
			})
				.EventHandler(GameHashes.TrapTriggered, delegate(CritterTrapPlant.StatesInstance smi, object data)
				{
					smi.OnTrapTriggered(data);
				})
				.EventTransition(GameHashes.Wilt, this.trap.wilting, null)
				.OnSignal(this.trapTriggered, this.trap.trigger)
				.ParamTransition<bool>(this.hasEatenCreature, this.trap.digesting, GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.IsTrue)
				.PlayAnim("idle_open", KAnim.PlayMode.Loop);
			this.trap.trigger.PlayAnim("trap", KAnim.PlayMode.Once).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				GameObject gameObject = smi.master.storage.FindFirst(GameTags.Creature);
				smi.lastConsumedEntityPrefabID = ((gameObject != null) ? gameObject.PrefabID().ToString() : null);
				smi.master.storage.ConsumeAllIgnoringDisease();
				smi.sm.hasEatenCreature.Set(true, smi, false);
			}).OnAnimQueueComplete(this.trap.digesting);
			this.trap.digesting.PlayAnim("digesting_loop", KAnim.PlayMode.Loop).ToggleComponent<Growing>(false).EventTransition(GameHashes.Grow, this.fruiting.enter, (CritterTrapPlant.StatesInstance smi) => smi.master.growing.ReachedNextHarvest())
				.EventTransition(GameHashes.Wilt, this.trap.wilting, null)
				.DefaultState(this.trap.digesting.idle);
			this.trap.digesting.idle.PlayAnim("digesting_loop", KAnim.PlayMode.Loop).Update(delegate(CritterTrapPlant.StatesInstance smi, float dt)
			{
				smi.AddGas(dt);
			}, UpdateRate.SIM_4000ms, false).OnSignal(this.ventGas, this.trap.digesting.vent_pre);
			this.trap.digesting.vent_pre.PlayAnim("vent_pre").Exit(delegate(CritterTrapPlant.StatesInstance smi)
			{
				smi.VentGas();
			}).OnAnimQueueComplete(this.trap.digesting.vent);
			this.trap.digesting.vent.PlayAnim("vent_loop", KAnim.PlayMode.Once).QueueAnim("vent_pst", false, null).OnAnimQueueComplete(this.trap.digesting.idle);
			this.trap.wilting.PlayAnim("wilt1", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.trap, (CritterTrapPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.fruiting.EventTransition(GameHashes.Wilt, this.fruiting.wilting, null).EventTransition(GameHashes.Harvest, this.harvest, null).DefaultState(this.fruiting.idle);
			this.fruiting.enter.PlayAnim("open_harvest", KAnim.PlayMode.Once).Exit(delegate(CritterTrapPlant.StatesInstance smi)
			{
				smi.VentGas();
				smi.master.storage.ConsumeAllIgnoringDisease();
			}).OnAnimQueueComplete(this.fruiting.idle);
			this.fruiting.idle.PlayAnim("harvestable_loop", KAnim.PlayMode.Once).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(true);
				}
			}).Transition(this.fruiting.old, new StateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.Transition.ConditionCallback(this.IsOld), UpdateRate.SIM_4000ms);
			this.fruiting.old.PlayAnim("wilt1", KAnim.PlayMode.Once).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				if (smi.master.harvestable != null)
				{
					smi.master.harvestable.SetCanBeHarvested(true);
				}
			}).Transition(this.fruiting.idle, GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.Not(new StateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.Transition.ConditionCallback(this.IsOld)), UpdateRate.SIM_4000ms);
			this.fruiting.wilting.PlayAnim("wilt1", KAnim.PlayMode.Once).EventTransition(GameHashes.WiltRecover, this.fruiting, (CritterTrapPlant.StatesInstance smi) => !smi.master.wiltCondition.IsWilting());
			this.harvest.PlayAnim("harvest", KAnim.PlayMode.Once).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				if (GameScheduler.Instance != null && smi.master != null)
				{
					GameScheduler.Instance.Schedule("SpawnFruit", 0.2f, new Action<object>(smi.master.crop.SpawnConfiguredFruit), null, null);
				}
				smi.master.harvestable.SetCanBeHarvested(false);
			}).Exit(delegate(CritterTrapPlant.StatesInstance smi)
			{
				smi.sm.hasEatenCreature.Set(false, smi, false);
			})
				.OnAnimQueueComplete(this.trap.open);
			this.dead.ToggleMainStatusItem(Db.Get().CreatureStatusItems.Dead, null).Enter(delegate(CritterTrapPlant.StatesInstance smi)
			{
				if (smi.master.rm.Replanted && !smi.master.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted))
				{
					Notifier notifier = smi.master.gameObject.AddOrGet<Notifier>();
					Notification notification = smi.master.CreateDeathNotification();
					notifier.Add(notification, "");
				}
				GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
				Harvestable harvestable = smi.master.harvestable;
				if (harvestable != null && harvestable.CanBeHarvested && GameScheduler.Instance != null)
				{
					GameScheduler.Instance.Schedule("SpawnFruit", 0.2f, new Action<object>(smi.master.crop.SpawnConfiguredFruit), null, null);
				}
				smi.master.Trigger(1623392196, null);
				smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
				global::UnityEngine.Object.Destroy(smi.master.GetComponent<KBatchedAnimController>());
				smi.Schedule(0.5f, new Action<object>(smi.master.DestroySelf), null);
			});
		}

		// Token: 0x0600A628 RID: 42536 RVA: 0x003ABBA4 File Offset: 0x003A9DA4
		public bool IsOld(CritterTrapPlant.StatesInstance smi)
		{
			return smi.master.growing.PercentOldAge() > 0.5f;
		}

		// Token: 0x040081AE RID: 33198
		public StateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.Signal trapTriggered;

		// Token: 0x040081AF RID: 33199
		public StateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.Signal ventGas;

		// Token: 0x040081B0 RID: 33200
		public StateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.BoolParameter hasEatenCreature;

		// Token: 0x040081B1 RID: 33201
		public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State dead;

		// Token: 0x040081B2 RID: 33202
		public CritterTrapPlant.States.FruitingStates fruiting;

		// Token: 0x040081B3 RID: 33203
		public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State harvest;

		// Token: 0x040081B4 RID: 33204
		public CritterTrapPlant.States.TrapStates trap;

		// Token: 0x0200287D RID: 10365
		public class DigestingStates : GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State
		{
			// Token: 0x0400B380 RID: 45952
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State idle;

			// Token: 0x0400B381 RID: 45953
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State vent_pre;

			// Token: 0x0400B382 RID: 45954
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State vent;
		}

		// Token: 0x0200287E RID: 10366
		public class TrapStates : GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State
		{
			// Token: 0x0400B383 RID: 45955
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State open;

			// Token: 0x0400B384 RID: 45956
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State trigger;

			// Token: 0x0400B385 RID: 45957
			public CritterTrapPlant.States.DigestingStates digesting;

			// Token: 0x0400B386 RID: 45958
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State wilting;
		}

		// Token: 0x0200287F RID: 10367
		public class FruitingStates : GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State
		{
			// Token: 0x0400B387 RID: 45959
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State enter;

			// Token: 0x0400B388 RID: 45960
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State idle;

			// Token: 0x0400B389 RID: 45961
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State old;

			// Token: 0x0400B38A RID: 45962
			public GameStateMachine<CritterTrapPlant.States, CritterTrapPlant.StatesInstance, CritterTrapPlant, object>.State wilting;
		}
	}
}
