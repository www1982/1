using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public class Reactor : StateMachineComponent<Reactor.StatesInstance>, IGameObjectEffectDescriptor
{
	// Token: 0x1700035B RID: 859
	// (get) Token: 0x06003465 RID: 13413 RVA: 0x00125796 File Offset: 0x00123996
	// (set) Token: 0x06003466 RID: 13414 RVA: 0x0012579E File Offset: 0x0012399E
	private float ReactionMassTarget
	{
		get
		{
			return this.reactionMassTarget;
		}
		set
		{
			this.fuelDelivery.capacity = value * 2f;
			this.fuelDelivery.refillMass = value * 0.2f;
			this.fuelDelivery.MinimumMass = value * 0.2f;
			this.reactionMassTarget = value;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x06003467 RID: 13415 RVA: 0x001257DD File Offset: 0x001239DD
	public float FuelTemperature
	{
		get
		{
			if (this.reactionStorage.items.Count > 0)
			{
				return this.reactionStorage.items[0].GetComponent<PrimaryElement>().Temperature;
			}
			return -1f;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x06003468 RID: 13416 RVA: 0x00125814 File Offset: 0x00123A14
	public float ReserveCoolantMass
	{
		get
		{
			PrimaryElement storedCoolant = this.GetStoredCoolant();
			if (!(storedCoolant == null))
			{
				return storedCoolant.Mass;
			}
			return 0f;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06003469 RID: 13417 RVA: 0x0012583D File Offset: 0x00123A3D
	public bool On
	{
		get
		{
			return base.smi.IsInsideState(base.smi.sm.on);
		}
	}

	// Token: 0x0600346A RID: 13418 RVA: 0x0012585C File Offset: 0x00123A5C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.NuclearReactors.Add(this);
		Storage[] components = base.GetComponents<Storage>();
		this.supplyStorage = components[0];
		this.reactionStorage = components[1];
		this.wasteStorage = components[2];
		this.CreateMeters();
		base.smi.StartSM();
		this.fuelDelivery = base.GetComponent<ManualDeliveryKG>();
		this.CheckLogicInputValueChanged(true);
	}

	// Token: 0x0600346B RID: 13419 RVA: 0x001258C0 File Offset: 0x00123AC0
	protected override void OnCleanUp()
	{
		Components.NuclearReactors.Remove(this);
		base.OnCleanUp();
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x001258D3 File Offset: 0x00123AD3
	private void Update()
	{
		this.CheckLogicInputValueChanged(false);
	}

	// Token: 0x0600346D RID: 13421 RVA: 0x001258DC File Offset: 0x00123ADC
	public Notification CreateMeltdownNotification()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		return new Notification(MISC.NOTIFICATIONS.REACTORMELTDOWN.NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.REACTORMELTDOWN.TOOLTIP + notificationList.ReduceMessages(false), "/t• " + component.GetProperName(), false, 0f, null, null, null, true, false, false);
	}

	// Token: 0x0600346E RID: 13422 RVA: 0x0012593B File Offset: 0x00123B3B
	public void SetStorages(Storage supply, Storage reaction, Storage waste)
	{
		this.supplyStorage = supply;
		this.reactionStorage = reaction;
		this.wasteStorage = waste;
	}

	// Token: 0x0600346F RID: 13423 RVA: 0x00125954 File Offset: 0x00123B54
	private void CheckLogicInputValueChanged(bool onLoad = false)
	{
		int num = 1;
		if (this.logicPorts.IsPortConnected("CONTROL_FUEL_DELIVERY"))
		{
			num = this.logicPorts.GetInputValue("CONTROL_FUEL_DELIVERY");
		}
		if (num == 0 && (this.fuelDeliveryEnabled || onLoad))
		{
			this.fuelDelivery.refillMass = -1f;
			this.fuelDeliveryEnabled = false;
			this.fuelDelivery.AbortDelivery("AutomationDisabled");
			return;
		}
		if (num == 1 && (!this.fuelDeliveryEnabled || onLoad))
		{
			this.fuelDelivery.refillMass = this.reactionMassTarget * 0.2f;
			this.fuelDeliveryEnabled = true;
		}
	}

	// Token: 0x06003470 RID: 13424 RVA: 0x001259F4 File Offset: 0x00123BF4
	private void OnLogicConnectionChanged(int value, bool connection)
	{
	}

	// Token: 0x06003471 RID: 13425 RVA: 0x001259F8 File Offset: 0x00123BF8
	private void CreateMeters()
	{
		this.temperatureMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "temperature_meter_target", "meter_temperature", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "temperature_meter_target" });
		this.waterMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "water_meter_target", "meter_water", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "water_meter_target" });
	}

	// Token: 0x06003472 RID: 13426 RVA: 0x00125A60 File Offset: 0x00123C60
	private void TransferFuel()
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		PrimaryElement storedFuel = this.GetStoredFuel();
		float num = ((activeFuel != null) ? activeFuel.Mass : 0f);
		float num2 = ((storedFuel != null) ? storedFuel.Mass : 0f);
		float num3 = this.ReactionMassTarget - num;
		num3 = Mathf.Min(num2, num3);
		if (num3 > 0.5f || num3 == num2)
		{
			this.supplyStorage.Transfer(this.reactionStorage, this.fuelTag, num3, false, true);
		}
	}

	// Token: 0x06003473 RID: 13427 RVA: 0x00125AE8 File Offset: 0x00123CE8
	private void TransferCoolant()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		PrimaryElement storedCoolant = this.GetStoredCoolant();
		float num = ((activeCoolant != null) ? activeCoolant.Mass : 0f);
		float num2 = ((storedCoolant != null) ? storedCoolant.Mass : 0f);
		float num3 = 30f - num;
		num3 = Mathf.Min(num2, num3);
		if (num3 > 0f)
		{
			this.supplyStorage.Transfer(this.reactionStorage, this.coolantTag, num3, false, true);
		}
	}

	// Token: 0x06003474 RID: 13428 RVA: 0x00125B64 File Offset: 0x00123D64
	private PrimaryElement GetStoredFuel()
	{
		GameObject gameObject = this.supplyStorage.FindFirst(this.fuelTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x06003475 RID: 13429 RVA: 0x00125BA0 File Offset: 0x00123DA0
	private PrimaryElement GetActiveFuel()
	{
		GameObject gameObject = this.reactionStorage.FindFirst(this.fuelTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x06003476 RID: 13430 RVA: 0x00125BDC File Offset: 0x00123DDC
	private PrimaryElement GetStoredCoolant()
	{
		GameObject gameObject = this.supplyStorage.FindFirst(this.coolantTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x00125C18 File Offset: 0x00123E18
	private PrimaryElement GetActiveCoolant()
	{
		GameObject gameObject = this.reactionStorage.FindFirst(this.coolantTag);
		if (gameObject && gameObject.GetComponent<PrimaryElement>())
		{
			return gameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x00125C54 File Offset: 0x00123E54
	private bool CanStartReaction()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		PrimaryElement activeFuel = this.GetActiveFuel();
		return activeCoolant && activeFuel && activeCoolant.Mass >= 30f && activeFuel.Mass >= 0.5f;
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x00125C9C File Offset: 0x00123E9C
	private void Cool(float dt)
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel == null)
		{
			return;
		}
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		if (activeCoolant == null)
		{
			return;
		}
		GameUtil.ForceConduction(activeFuel, activeCoolant, dt * 5f);
		if (activeCoolant.Temperature > 673.15f)
		{
			base.smi.sm.doVent.Trigger(base.smi);
		}
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x00125D04 File Offset: 0x00123F04
	private void React(float dt)
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel != null && activeFuel.Mass >= 0.25f)
		{
			float num = GameUtil.EnergyToTemperatureDelta(-100f * dt * activeFuel.Mass, activeFuel);
			activeFuel.Temperature += num;
			this.spentFuel += dt * 0.016666668f;
		}
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x00125D65 File Offset: 0x00123F65
	private void SetEmitRads(float rads)
	{
		base.smi.master.radEmitter.emitRads = rads;
		base.smi.master.radEmitter.Refresh();
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x00125D94 File Offset: 0x00123F94
	private bool ReadyToCool()
	{
		PrimaryElement activeCoolant = this.GetActiveCoolant();
		return activeCoolant != null && activeCoolant.Mass > 0f;
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x00125DC0 File Offset: 0x00123FC0
	private void DumpSpentFuel()
	{
		PrimaryElement activeFuel = this.GetActiveFuel();
		if (activeFuel != null)
		{
			if (this.spentFuel <= 0f)
			{
				return;
			}
			float num = this.spentFuel * 100f;
			if (num > 0f)
			{
				this.wasteStorage.AddLiquid(SimHashes.NuclearWaste, num, activeFuel.Temperature, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id), Mathf.RoundToInt(num * 50f), false, true);
			}
			if (this.wasteStorage.MassStored() >= 100f)
			{
				this.wasteStorage.DropAll(true, true, default(Vector3), true, null);
			}
			if (this.spentFuel >= activeFuel.Mass)
			{
				Util.KDestroyGameObject(activeFuel.gameObject);
				this.spentFuel = 0f;
				return;
			}
			activeFuel.Mass -= this.spentFuel;
			this.spentFuel = 0f;
		}
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x00125EBC File Offset: 0x001240BC
	private void UpdateVentStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.ClearToVent())
		{
			if (component.HasStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure))
			{
				base.smi.sm.canVent.Set(true, base.smi, false);
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, false);
				return;
			}
		}
		else if (!component.HasStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure))
		{
			base.smi.sm.canVent.Set(false, base.smi, false);
			component.AddStatusItem(Db.Get().BuildingStatusItems.GasVentOverPressure, null);
		}
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x00125F74 File Offset: 0x00124174
	private void UpdateCoolantStatus()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.GetStoredCoolant() != null || base.smi.GetCurrentState() == base.smi.sm.meltdown || base.smi.GetCurrentState() == base.smi.sm.dead)
		{
			if (component.HasStatusItem(Db.Get().BuildingStatusItems.NoCoolant))
			{
				component.RemoveStatusItem(Db.Get().BuildingStatusItems.NoCoolant, false);
				return;
			}
		}
		else if (!component.HasStatusItem(Db.Get().BuildingStatusItems.NoCoolant))
		{
			component.AddStatusItem(Db.Get().BuildingStatusItems.NoCoolant, null);
		}
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x00126030 File Offset: 0x00124230
	private void InitVentCells()
	{
		if (this.ventCells == null)
		{
			this.ventCells = new int[]
			{
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.zero),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.right + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.left + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.left),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.right + Vector3.right),
				Grid.PosToCell(base.transform.GetPosition() + base.smi.master.dumpOffset + Vector3.down + Vector3.left + Vector3.left)
			};
		}
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x0012629C File Offset: 0x0012449C
	public int GetVentCell()
	{
		this.InitVentCells();
		for (int i = 0; i < this.ventCells.Length; i++)
		{
			if (Grid.Mass[this.ventCells[i]] < 150f && !Grid.Solid[this.ventCells[i]])
			{
				return this.ventCells[i];
			}
		}
		return -1;
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x001262FC File Offset: 0x001244FC
	private bool ClearToVent()
	{
		this.InitVentCells();
		for (int i = 0; i < this.ventCells.Length; i++)
		{
			if (Grid.Mass[this.ventCells[i]] < 150f && !Grid.Solid[this.ventCells[i]])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003483 RID: 13443 RVA: 0x00126352 File Offset: 0x00124552
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>();
	}

	// Token: 0x04001FAC RID: 8108
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001FAD RID: 8109
	[MyCmpGet]
	private RadiationEmitter radEmitter;

	// Token: 0x04001FAE RID: 8110
	[MyCmpGet]
	private ManualDeliveryKG fuelDelivery;

	// Token: 0x04001FAF RID: 8111
	private MeterController temperatureMeter;

	// Token: 0x04001FB0 RID: 8112
	private MeterController waterMeter;

	// Token: 0x04001FB1 RID: 8113
	private Storage supplyStorage;

	// Token: 0x04001FB2 RID: 8114
	private Storage reactionStorage;

	// Token: 0x04001FB3 RID: 8115
	private Storage wasteStorage;

	// Token: 0x04001FB4 RID: 8116
	private Tag fuelTag = SimHashes.EnrichedUranium.CreateTag();

	// Token: 0x04001FB5 RID: 8117
	private Tag coolantTag = GameTags.AnyWater;

	// Token: 0x04001FB6 RID: 8118
	private Vector3 dumpOffset = new Vector3(0f, 5f, 0f);

	// Token: 0x04001FB7 RID: 8119
	public static string MELTDOWN_STINGER = "Stinger_Loop_NuclearMeltdown";

	// Token: 0x04001FB8 RID: 8120
	private static float meterFrameScaleHack = 3f;

	// Token: 0x04001FB9 RID: 8121
	[Serialize]
	private float spentFuel;

	// Token: 0x04001FBA RID: 8122
	private float timeSinceMeltdownEmit;

	// Token: 0x04001FBB RID: 8123
	private const float reactorMeltDownBonusMassAmount = 10f;

	// Token: 0x04001FBC RID: 8124
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x04001FBD RID: 8125
	private LogicEventHandler fuelControlPort;

	// Token: 0x04001FBE RID: 8126
	private bool fuelDeliveryEnabled = true;

	// Token: 0x04001FBF RID: 8127
	public Guid refuelStausHandle;

	// Token: 0x04001FC0 RID: 8128
	[Serialize]
	public int numCyclesRunning;

	// Token: 0x04001FC1 RID: 8129
	private float reactionMassTarget = 60f;

	// Token: 0x04001FC2 RID: 8130
	private int[] ventCells;

	// Token: 0x020016D4 RID: 5844
	public class StatesInstance : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.GameInstance
	{
		// Token: 0x060096C6 RID: 38598 RVA: 0x00379911 File Offset: 0x00377B11
		public StatesInstance(Reactor smi)
			: base(smi)
		{
		}
	}

	// Token: 0x020016D5 RID: 5845
	public class States : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor>
	{
		// Token: 0x060096C7 RID: 38599 RVA: 0x0037991C File Offset: 0x00377B1C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.serializable = StateMachine.SerializeType.ParamsOnly;
			default_state = this.off;
			this.root.EventHandler(GameHashes.OnStorageChange, delegate(Reactor.StatesInstance smi)
			{
				PrimaryElement storedCoolant = smi.master.GetStoredCoolant();
				if (!storedCoolant)
				{
					smi.master.waterMeter.SetPositionPercent(0f);
					return;
				}
				smi.master.waterMeter.SetPositionPercent(storedCoolant.Mass / 90f);
			});
			this.off_pre.QueueAnim("working_pst", false, null).OnAnimQueueComplete(this.off);
			this.off.PlayAnim("off").Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.radEmitter.SetEmitting(false);
				smi.master.SetEmitRads(0f);
			}).ParamTransition<bool>(this.reactionUnderway, this.on, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.ParamTransition<bool>(this.melted, this.dead, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.ParamTransition<bool>(this.meltingDown, this.meltdown, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue)
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.TransferFuel();
					smi.master.TransferCoolant();
					if (smi.master.CanStartReaction())
					{
						smi.GoTo(this.on);
					}
				}, UpdateRate.SIM_1000ms, false);
			this.on.Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.sm.reactionUnderway.Set(true, smi, false);
				smi.master.operational.SetActive(true, false);
				smi.master.SetEmitRads(2400f);
				smi.master.radEmitter.SetEmitting(true);
			}).EventHandler(GameHashes.NewDay, (Reactor.StatesInstance smi) => GameClock.Instance, delegate(Reactor.StatesInstance smi)
			{
				smi.master.numCyclesRunning++;
			}).Exit(delegate(Reactor.StatesInstance smi)
			{
				smi.sm.reactionUnderway.Set(false, smi, false);
				smi.master.numCyclesRunning = 0;
			})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.TransferFuel();
					smi.master.TransferCoolant();
					smi.master.React(dt);
					smi.master.UpdateCoolantStatus();
					smi.master.UpdateVentStatus();
					smi.master.DumpSpentFuel();
					if (!smi.master.fuelDeliveryEnabled)
					{
						smi.master.refuelStausHandle = smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.ReactorRefuelDisabled, null);
					}
					else
					{
						smi.master.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.ReactorRefuelDisabled, false);
						smi.master.refuelStausHandle = Guid.Empty;
					}
					if (smi.master.GetActiveCoolant() != null)
					{
						smi.master.Cool(dt);
					}
					PrimaryElement activeFuel = smi.master.GetActiveFuel();
					if (activeFuel != null)
					{
						smi.master.temperatureMeter.SetPositionPercent(Mathf.Clamp01(activeFuel.Temperature / 3000f) / Reactor.meterFrameScaleHack);
						if (activeFuel.Temperature >= 3000f)
						{
							smi.sm.meltdownMassRemaining.Set(10f + smi.master.supplyStorage.MassStored() + smi.master.reactionStorage.MassStored() + smi.master.wasteStorage.MassStored(), smi, false);
							smi.master.supplyStorage.ConsumeAllIgnoringDisease();
							smi.master.reactionStorage.ConsumeAllIgnoringDisease();
							smi.master.wasteStorage.ConsumeAllIgnoringDisease();
							smi.GoTo(this.meltdown.pre);
							return;
						}
						if (activeFuel.Mass <= 0.25f)
						{
							smi.GoTo(this.off_pre);
							smi.master.temperatureMeter.SetPositionPercent(0f);
							return;
						}
					}
					else
					{
						smi.GoTo(this.off_pre);
						smi.master.temperatureMeter.SetPositionPercent(0f);
					}
				}, UpdateRate.SIM_200ms, false)
				.DefaultState(this.on.pre);
			this.on.pre.PlayAnim("working_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.on.reacting).OnSignal(this.doVent, this.on.venting);
			this.on.reacting.PlayAnim("working_loop", KAnim.PlayMode.Loop).OnSignal(this.doVent, this.on.venting);
			this.on.venting.ParamTransition<bool>(this.canVent, this.on.venting.vent, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue).ParamTransition<bool>(this.canVent, this.on.venting.ventIssue, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsFalse);
			this.on.venting.ventIssue.PlayAnim("venting_issue", KAnim.PlayMode.Loop).ParamTransition<bool>(this.canVent, this.on.venting.vent, GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.IsTrue);
			this.on.venting.vent.PlayAnim("venting").Enter(delegate(Reactor.StatesInstance smi)
			{
				PrimaryElement activeCoolant = smi.master.GetActiveCoolant();
				if (activeCoolant != null)
				{
					activeCoolant.GetComponent<Dumpable>().Dump(Grid.CellToPos(smi.master.GetVentCell()));
				}
			}).OnAnimQueueComplete(this.on.reacting);
			this.meltdown.ToggleStatusItem(Db.Get().BuildingStatusItems.ReactorMeltdown, null).ToggleNotification((Reactor.StatesInstance smi) => smi.master.CreateMeltdownNotification()).ParamTransition<float>(this.meltdownMassRemaining, this.dead, (Reactor.StatesInstance smi, float p) => p <= 0f)
				.ToggleTag(GameTags.DeadReactor)
				.DefaultState(this.meltdown.loop);
			this.meltdown.pre.PlayAnim("almost_meltdown_pre", KAnim.PlayMode.Once).QueueAnim("almost_meltdown_loop", false, null).QueueAnim("meltdown_pre", false, null)
				.OnAnimQueueComplete(this.meltdown.loop);
			this.meltdown.loop.PlayAnim("meltdown_loop", KAnim.PlayMode.Loop).Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.radEmitter.SetEmitting(true);
				smi.master.SetEmitRads(4800f);
				smi.master.temperatureMeter.SetPositionPercent(1f / Reactor.meterFrameScaleHack);
				smi.master.UpdateCoolantStatus();
				if (this.meltingDown.Get(smi))
				{
					MusicManager.instance.PlaySong(Reactor.MELTDOWN_STINGER, false);
					MusicManager.instance.StopDynamicMusic(false);
				}
				else
				{
					MusicManager.instance.PlaySong(Reactor.MELTDOWN_STINGER, false);
					MusicManager.instance.SetSongParameter(Reactor.MELTDOWN_STINGER, "Music_PlayStinger", 1f, true);
					MusicManager.instance.StopDynamicMusic(false);
				}
				this.meltingDown.Set(true, smi, false);
			}).Exit(delegate(Reactor.StatesInstance smi)
			{
				this.meltingDown.Set(false, smi, false);
				MusicManager.instance.SetSongParameter(Reactor.MELTDOWN_STINGER, "Music_NuclearMeltdownActive", 0f, true);
			})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.master.timeSinceMeltdownEmit += dt;
					float num = 0.5f;
					float num2 = 5f;
					if (smi.master.timeSinceMeltdownEmit > num && smi.sm.meltdownMassRemaining.Get(smi) > 0f)
					{
						smi.master.timeSinceMeltdownEmit -= num;
						float num3 = Mathf.Min(smi.sm.meltdownMassRemaining.Get(smi), num2);
						smi.sm.meltdownMassRemaining.Delta(-num3, smi);
						for (int i = 0; i < 3; i++)
						{
							if (num3 >= NuclearWasteCometConfig.MASS)
							{
								GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(NuclearWasteCometConfig.ID), smi.master.transform.position + Vector3.up * 2f, Quaternion.identity, null, null, true, 0);
								gameObject.SetActive(true);
								Comet component = gameObject.GetComponent<Comet>();
								component.ignoreObstacleForDamage.Set(smi.master.gameObject.GetComponent<KPrefabID>());
								component.addTiles = 1;
								int num4 = 270;
								while (num4 > 225 && num4 < 335)
								{
									num4 = global::UnityEngine.Random.Range(0, 360);
								}
								float num5 = (float)num4 * 3.1415927f / 180f;
								component.Velocity = new Vector2(-Mathf.Cos(num5) * 20f, Mathf.Sin(num5) * 20f);
								component.GetComponent<KBatchedAnimController>().Rotation = (float)(-(float)num4) - 90f;
								num3 -= NuclearWasteCometConfig.MASS;
							}
						}
						for (int j = 0; j < 3; j++)
						{
							if (num3 >= 0.001f)
							{
								SimMessages.AddRemoveSubstance(Grid.PosToCell(smi.master.transform.position + Vector3.up * 3f + Vector3.right * (float)j * 2f), SimHashes.NuclearWaste, CellEventLogger.Instance.ElementEmitted, num3 / 3f, 3000f, Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.Id), Mathf.RoundToInt(50f * (num3 / 3f)), true, -1);
							}
						}
					}
				}, UpdateRate.SIM_200ms, false);
			this.dead.PlayAnim("dead").ToggleTag(GameTags.DeadReactor).Enter(delegate(Reactor.StatesInstance smi)
			{
				smi.master.temperatureMeter.SetPositionPercent(1f / Reactor.meterFrameScaleHack);
				smi.master.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.DeadReactorCoolingOff, smi);
				this.melted.Set(true, smi, false);
			})
				.Exit(delegate(Reactor.StatesInstance smi)
				{
					smi.master.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.DeadReactorCoolingOff, false);
				})
				.Update(delegate(Reactor.StatesInstance smi, float dt)
				{
					smi.sm.timeSinceMeltdown.Delta(dt, smi);
					smi.master.radEmitter.emitRads = Mathf.Lerp(4800f, 0f, smi.sm.timeSinceMeltdown.Get(smi) / 3000f);
					smi.master.radEmitter.Refresh();
				}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x040073E9 RID: 29673
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.Signal doVent;

		// Token: 0x040073EA RID: 29674
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter canVent = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(true);

		// Token: 0x040073EB RID: 29675
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter reactionUnderway = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter();

		// Token: 0x040073EC RID: 29676
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter meltdownMassRemaining = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter(0f);

		// Token: 0x040073ED RID: 29677
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter timeSinceMeltdown = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.FloatParameter(0f);

		// Token: 0x040073EE RID: 29678
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter meltingDown = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(false);

		// Token: 0x040073EF RID: 29679
		public StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter melted = new StateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.BoolParameter(false);

		// Token: 0x040073F0 RID: 29680
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State off;

		// Token: 0x040073F1 RID: 29681
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State off_pre;

		// Token: 0x040073F2 RID: 29682
		public Reactor.States.ReactingStates on;

		// Token: 0x040073F3 RID: 29683
		public Reactor.States.MeltdownStates meltdown;

		// Token: 0x040073F4 RID: 29684
		public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State dead;

		// Token: 0x020027C7 RID: 10183
		public class ReactingStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
		{
			// Token: 0x0400B05D RID: 45149
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pre;

			// Token: 0x0400B05E RID: 45150
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State reacting;

			// Token: 0x0400B05F RID: 45151
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pst;

			// Token: 0x0400B060 RID: 45152
			public Reactor.States.ReactingStates.VentingStates venting;

			// Token: 0x02003885 RID: 14469
			public class VentingStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
			{
				// Token: 0x0400E473 RID: 58483
				public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State ventIssue;

				// Token: 0x0400E474 RID: 58484
				public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State vent;
			}
		}

		// Token: 0x020027C8 RID: 10184
		public class MeltdownStates : GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State
		{
			// Token: 0x0400B061 RID: 45153
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State almost_pre;

			// Token: 0x0400B062 RID: 45154
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State almost_loop;

			// Token: 0x0400B063 RID: 45155
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State pre;

			// Token: 0x0400B064 RID: 45156
			public GameStateMachine<Reactor.States, Reactor.StatesInstance, Reactor, object>.State loop;
		}
	}
}
