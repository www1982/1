using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200073B RID: 1851
public class GeothermalController : StateMachineComponent<GeothermalController.StatesInstance>
{
	// Token: 0x1700027E RID: 638
	// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x0010C249 File Offset: 0x0010A449
	// (set) Token: 0x06002EC4 RID: 11972 RVA: 0x0010C251 File Offset: 0x0010A451
	public GeothermalController.ProgressState State
	{
		get
		{
			return this.state;
		}
		protected set
		{
			this.state = value;
		}
	}

	// Token: 0x06002EC5 RID: 11973 RVA: 0x0010C25C File Offset: 0x0010A45C
	public List<GeothermalVent> FindVents(bool requireEnabled)
	{
		if (!requireEnabled)
		{
			return Components.GeothermalVents.GetItems(base.gameObject.GetMyWorldId());
		}
		List<GeothermalVent> list = new List<GeothermalVent>();
		foreach (GeothermalVent geothermalVent in this.FindVents(false))
		{
			if (geothermalVent.IsVentConnected())
			{
				list.Add(geothermalVent);
			}
		}
		return list;
	}

	// Token: 0x06002EC6 RID: 11974 RVA: 0x0010C2D8 File Offset: 0x0010A4D8
	public void PushToVents(GeothermalVent.ElementInfo info)
	{
		List<GeothermalVent> list = this.FindVents(true);
		if (list.Count == 0)
		{
			return;
		}
		float[] array = new float[list.Count];
		float num = 0f;
		for (int i = 0; i < list.Count; i++)
		{
			array[i] = GeothermalControllerConfig.OUTPUT_VENT_WEIGHT_RANGE.Get();
			num += array[i];
		}
		GeothermalVent.ElementInfo elementInfo = info;
		for (int j = 0; j < list.Count; j++)
		{
			elementInfo.mass = array[j] * info.mass / num;
			elementInfo.diseaseCount = (int)(array[j] * (float)info.diseaseCount / num);
			list[j].addMaterial(elementInfo);
		}
	}

	// Token: 0x06002EC7 RID: 11975 RVA: 0x0010C381 File Offset: 0x0010A581
	public bool IsFull()
	{
		return this.storage.MassStored() > 11999.9f;
	}

	// Token: 0x06002EC8 RID: 11976 RVA: 0x0010C398 File Offset: 0x0010A598
	public float ComputeContentTemperature()
	{
		float num = 0f;
		float num2 = 0f;
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			float num3 = component.Mass * component.Element.specificHeatCapacity;
			num += num3 * component.Temperature;
			num2 += num3;
		}
		float num4 = 0f;
		if (num2 != 0f)
		{
			num4 = num / num2;
		}
		return num4;
	}

	// Token: 0x06002EC9 RID: 11977 RVA: 0x0010C438 File Offset: 0x0010A638
	public List<GeothermalVent.ElementInfo> ComputeOutputs()
	{
		float num = this.ComputeContentTemperature();
		float num2 = GeothermalControllerConfig.CalculateOutputTemperature(num);
		GeothermalController.ImpuritiesHelper impuritiesHelper = new GeothermalController.ImpuritiesHelper();
		foreach (GameObject gameObject in this.storage.items)
		{
			PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
			impuritiesHelper.AddMaterial(component.Element.idx, component.Mass * 0.92f, num2, component.DiseaseIdx, component.DiseaseCount);
		}
		foreach (GeothermalControllerConfig.Impurity impurity in GeothermalControllerConfig.GetImpurities())
		{
			MathUtil.MinMax required_temp_range = impurity.required_temp_range;
			if (required_temp_range.Contains(num))
			{
				impuritiesHelper.AddMaterial(impurity.elementIdx, impurity.mass_kg, num2, byte.MaxValue, 0);
			}
		}
		return impuritiesHelper.results;
	}

	// Token: 0x06002ECA RID: 11978 RVA: 0x0010C544 File Offset: 0x0010A744
	public void PushToVents()
	{
		SaveGame.Instance.ColonyAchievementTracker.GeothermalControllerHasVented = true;
		List<GeothermalVent.ElementInfo> list = this.ComputeOutputs();
		if (!SaveGame.Instance.ColonyAchievementTracker.GeothermalClearedEntombedVent && list[0].temperature >= 602f)
		{
			GeothermalPlantComponent.OnVentingHotMaterial(this.GetMyWorldId());
		}
		foreach (GeothermalVent.ElementInfo elementInfo in list)
		{
			this.PushToVents(elementInfo);
		}
		this.storage.ConsumeAllIgnoringDisease();
		this.fakeProgress = 1f;
	}

	// Token: 0x06002ECB RID: 11979 RVA: 0x0010C5F0 File Offset: 0x0010A7F0
	private void TryAddConduitConsumers()
	{
		if (base.GetComponents<EntityConduitConsumer>().Length != 0)
		{
			return;
		}
		foreach (CellOffset cellOffset in new CellOffset[]
		{
			new CellOffset(0, 0),
			new CellOffset(2, 0),
			new CellOffset(-2, 0)
		})
		{
			EntityConduitConsumer entityConduitConsumer = base.gameObject.AddComponent<EntityConduitConsumer>();
			entityConduitConsumer.offset = cellOffset;
			entityConduitConsumer.conduitType = ConduitType.Liquid;
		}
	}

	// Token: 0x06002ECC RID: 11980 RVA: 0x0010C668 File Offset: 0x0010A868
	public float GetPressure()
	{
		GeothermalController.ProgressState progressState = this.state;
		if (progressState > GeothermalController.ProgressState.RECONNECTING_PIPES)
		{
			if (progressState - GeothermalController.ProgressState.NOTIFY_REPAIRED > 3)
			{
			}
			return this.storage.MassStored() / 12000f;
		}
		return 0f;
	}

	// Token: 0x06002ECD RID: 11981 RVA: 0x0010C69F File Offset: 0x0010A89F
	private void FakeMeterDraining(float time)
	{
		this.fakeProgress -= time / 16f;
		if (this.fakeProgress < 0f)
		{
			this.fakeProgress = 0f;
		}
		this.barometer.SetPositionPercent(this.fakeProgress);
	}

	// Token: 0x06002ECE RID: 11982 RVA: 0x0010C6E0 File Offset: 0x0010A8E0
	private void UpdatePressure()
	{
		GeothermalController.ProgressState progressState = this.state;
		if (progressState > GeothermalController.ProgressState.RECONNECTING_PIPES)
		{
			if (progressState - GeothermalController.ProgressState.NOTIFY_REPAIRED > 3)
			{
			}
			float pressure = this.GetPressure();
			this.barometer.SetPositionPercent(pressure);
			float num = this.ComputeContentTemperature();
			if (num > 0f)
			{
				this.thermometer.SetPositionPercent((num - 50f) / 2450f);
			}
			int num2 = 0;
			for (int i = 1; i < GeothermalControllerConfig.PRESSURE_ANIM_THRESHOLDS.Length; i++)
			{
				if (pressure >= GeothermalControllerConfig.PRESSURE_ANIM_THRESHOLDS[i])
				{
					num2 = i;
				}
			}
			KAnim.Anim currentAnim = this.animController.GetCurrentAnim();
			if (((currentAnim != null) ? currentAnim.name : null) != GeothermalControllerConfig.PRESSURE_ANIM_LOOPS[num2])
			{
				this.animController.Play(GeothermalControllerConfig.PRESSURE_ANIM_LOOPS[num2], KAnim.PlayMode.Loop, 1f, 0f);
			}
			return;
		}
	}

	// Token: 0x06002ECF RID: 11983 RVA: 0x0010C7B0 File Offset: 0x0010A9B0
	public bool IsObstructed()
	{
		if (this.IsFull())
		{
			bool flag = false;
			foreach (GeothermalVent geothermalVent in this.FindVents(false))
			{
				if (geothermalVent.IsEntombed())
				{
					return true;
				}
				if (geothermalVent.IsVentConnected())
				{
					if (!geothermalVent.CanVent())
					{
						return true;
					}
					flag = true;
				}
			}
			return !flag;
		}
		return false;
	}

	// Token: 0x06002ED0 RID: 11984 RVA: 0x0010C834 File Offset: 0x0010AA34
	public GeothermalVent FirstObstructedVent()
	{
		foreach (GeothermalVent geothermalVent in this.FindVents(false))
		{
			if (geothermalVent.IsEntombed())
			{
				return geothermalVent;
			}
			if (geothermalVent.IsVentConnected() && !geothermalVent.CanVent())
			{
				return geothermalVent;
			}
		}
		return null;
	}

	// Token: 0x06002ED1 RID: 11985 RVA: 0x0010C8A4 File Offset: 0x0010AAA4
	public Notification CreateFirstBatchReadyNotification()
	{
		this.dismissOnSelect = new Notification(COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.NOTIFICATIONS.GEOTHERMAL_PLANT_FIRST_VENT_READY, NotificationType.Event, (List<Notification> _, object __) => COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.NOTIFICATIONS.GEOTHERMAL_PLANT_FIRST_VENT_READY_TOOLTIP, null, false, 0f, null, null, base.transform, true, false, false);
		return this.dismissOnSelect;
	}

	// Token: 0x06002ED2 RID: 11986 RVA: 0x0010C900 File Offset: 0x0010AB00
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.GeothermalControllers.Add(this.GetMyWorldId(), this);
		this.operational.SetFlag(GeothermalController.allowInputFlag, false);
		base.smi.StartSM();
		this.animController = base.GetComponent<KBatchedAnimController>();
		this.barometer = new MeterController(this.animController, "meter_target", "meter", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, GeothermalControllerConfig.BAROMETER_SYMBOLS);
		this.thermometer = new MeterController(this.animController, "meter_target", "meter_temp", Meter.Offset.NoChange, Grid.SceneLayer.NoLayer, GeothermalControllerConfig.THERMOMETER_SYMBOLS);
		base.Subscribe(-1503271301, new Action<object>(this.OnBuildingSelected));
	}

	// Token: 0x06002ED3 RID: 11987 RVA: 0x0010C9AC File Offset: 0x0010ABAC
	protected override void OnCleanUp()
	{
		base.Unsubscribe(-1503271301, new Action<object>(this.OnBuildingSelected));
		if (this.listener != null)
		{
			Components.GeothermalVents.Unregister(this.GetMyWorldId(), this.listener.onAdd, this.listener.onRemove);
		}
		Components.GeothermalControllers.Remove(this.GetMyWorldId(), this);
		base.OnCleanUp();
	}

	// Token: 0x06002ED4 RID: 11988 RVA: 0x0010CA18 File Offset: 0x0010AC18
	protected void OnBuildingSelected(object clicked)
	{
		if (!(bool)clicked)
		{
			return;
		}
		if (this.dismissOnSelect != null)
		{
			if (this.dismissOnSelect.customClickCallback != null)
			{
				this.dismissOnSelect.customClickCallback(this.dismissOnSelect.customClickData);
				return;
			}
			this.dismissOnSelect.Clear();
			this.dismissOnSelect = null;
		}
	}

	// Token: 0x06002ED5 RID: 11989 RVA: 0x0010CA74 File Offset: 0x0010AC74
	public bool VentingCanFreeKeepsake()
	{
		List<GeothermalVent.ElementInfo> list = this.ComputeOutputs();
		return list.Count != 0 && list[0].temperature >= 602f;
	}

	// Token: 0x04001BA8 RID: 7080
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04001BA9 RID: 7081
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001BAA RID: 7082
	private MeterController thermometer;

	// Token: 0x04001BAB RID: 7083
	private MeterController barometer;

	// Token: 0x04001BAC RID: 7084
	private KBatchedAnimController animController;

	// Token: 0x04001BAD RID: 7085
	public Notification dismissOnSelect;

	// Token: 0x04001BAE RID: 7086
	public static Operational.Flag allowInputFlag = new Operational.Flag("allowInputFlag", Operational.Flag.Type.Requirement);

	// Token: 0x04001BAF RID: 7087
	private GeothermalController.VentRegistrationListener listener;

	// Token: 0x04001BB0 RID: 7088
	[Serialize]
	private GeothermalController.ProgressState state;

	// Token: 0x04001BB1 RID: 7089
	private float fakeProgress;

	// Token: 0x020015F3 RID: 5619
	public class ReconnectPipes : Workable
	{
		// Token: 0x0600935D RID: 37725 RVA: 0x0036A38D File Offset: 0x0036858D
		protected override void OnPrefabInit()
		{
			base.OnPrefabInit();
			base.SetWorkTime(5f);
			this.overrideAnims = new KAnimFile[] { Assets.GetAnim(GeothermalControllerConfig.RECONNECT_PUMP_ANIM_OVERRIDE) };
			this.synchronizeAnims = false;
			this.faceTargetWhenWorking = true;
		}

		// Token: 0x0600935E RID: 37726 RVA: 0x0036A3C7 File Offset: 0x003685C7
		protected override void OnCompleteWork(WorkerBase worker)
		{
			base.OnCompleteWork(worker);
			if (this.storage != null)
			{
				this.storage.ConsumeAllIgnoringDisease();
			}
		}

		// Token: 0x0400717C RID: 29052
		[MyCmpGet]
		private Storage storage;
	}

	// Token: 0x020015F4 RID: 5620
	private class VentRegistrationListener
	{
		// Token: 0x0400717D RID: 29053
		public Action<GeothermalVent> onAdd;

		// Token: 0x0400717E RID: 29054
		public Action<GeothermalVent> onRemove;
	}

	// Token: 0x020015F5 RID: 5621
	public enum ProgressState
	{
		// Token: 0x04007180 RID: 29056
		NOT_STARTED,
		// Token: 0x04007181 RID: 29057
		FETCHING_STEEL,
		// Token: 0x04007182 RID: 29058
		RECONNECTING_PIPES,
		// Token: 0x04007183 RID: 29059
		NOTIFY_REPAIRED,
		// Token: 0x04007184 RID: 29060
		REPAIRED,
		// Token: 0x04007185 RID: 29061
		AT_CAPACITY,
		// Token: 0x04007186 RID: 29062
		COMPLETE
	}

	// Token: 0x020015F6 RID: 5622
	private class ImpuritiesHelper
	{
		// Token: 0x06009361 RID: 37729 RVA: 0x0036A3FC File Offset: 0x003685FC
		public void AddMaterial(ushort elementIdx, float mass, float temperature, byte diseaseIdx, int diseaseCount)
		{
			Element element = ElementLoader.elements[(int)elementIdx];
			if (element.lowTemp > temperature)
			{
				Element lowTempTransition = element.lowTempTransition;
				Element element2 = ElementLoader.FindElementByHash(element.lowTempTransitionOreID);
				this.AddMaterial(lowTempTransition.idx, mass * (1f - element.lowTempTransitionOreMassConversion), temperature, diseaseIdx, (int)((float)diseaseCount * (1f - element.lowTempTransitionOreMassConversion)));
				if (element2 != null)
				{
					this.AddMaterial(element2.idx, mass * element.lowTempTransitionOreMassConversion, temperature, diseaseIdx, (int)((float)diseaseCount * element.lowTempTransitionOreMassConversion));
				}
				return;
			}
			if (element.highTemp < temperature)
			{
				Element highTempTransition = element.highTempTransition;
				Element element3 = ElementLoader.FindElementByHash(element.highTempTransitionOreID);
				this.AddMaterial(highTempTransition.idx, mass * (1f - element.highTempTransitionOreMassConversion), temperature, diseaseIdx, (int)((float)diseaseCount * (1f - element.highTempTransitionOreMassConversion)));
				if (element3 != null)
				{
					this.AddMaterial(element3.idx, mass * element.highTempTransitionOreMassConversion, temperature, diseaseIdx, (int)((float)diseaseCount * element.highTempTransitionOreMassConversion));
				}
				return;
			}
			GeothermalVent.ElementInfo elementInfo = default(GeothermalVent.ElementInfo);
			for (int i = 0; i < this.results.Count; i++)
			{
				if (this.results[i].elementIdx == elementIdx)
				{
					elementInfo = this.results[i];
					elementInfo.mass += mass;
					this.results[i] = elementInfo;
					return;
				}
			}
			elementInfo.elementHash = element.id;
			elementInfo.elementIdx = elementIdx;
			elementInfo.mass = mass;
			elementInfo.temperature = temperature;
			elementInfo.diseaseCount = diseaseCount;
			elementInfo.diseaseIdx = diseaseIdx;
			elementInfo.isSolid = ElementLoader.elements[(int)elementIdx].IsSolid;
			this.results.Add(elementInfo);
		}

		// Token: 0x04007187 RID: 29063
		public List<GeothermalVent.ElementInfo> results = new List<GeothermalVent.ElementInfo>();
	}

	// Token: 0x020015F7 RID: 5623
	public class States : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController>
	{
		// Token: 0x06009363 RID: 37731 RVA: 0x0036A5C8 File Offset: 0x003687C8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.EnterTransition(this.online, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.COMPLETE).EnterTransition(this.offline, (GeothermalController.StatesInstance smi) => smi.master.State != GeothermalController.ProgressState.COMPLETE);
			this.offline.EnterTransition(this.offline.initial, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.NOT_STARTED).EnterTransition(this.offline.fetchSteel, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.FETCHING_STEEL).EnterTransition(this.offline.reconnectPipes, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.RECONNECTING_PIPES)
				.EnterTransition(this.offline.notifyRepaired, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.NOTIFY_REPAIRED)
				.EnterTransition(this.offline.filling, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.REPAIRED)
				.EnterTransition(this.offline.filled, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.AT_CAPACITY)
				.PlayAnim("off");
			this.offline.initial.Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.storage.DropAll(false, false, default(Vector3), true, null);
			}).Transition(this.offline.fetchSteel, (GeothermalController.StatesInstance smi) => smi.master.State == GeothermalController.ProgressState.FETCHING_STEEL, UpdateRate.SIM_200ms).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerOffline, null);
			this.offline.fetchSteel.ToggleChore((GeothermalController.StatesInstance smi) => this.CreateRepairFetchChore(smi, GeothermalControllerConfig.STEEL_FETCH_TAGS, 1200f - smi.master.storage.MassStored()), this.offline.checkSupplies).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerOffline, null).ToggleStatusItem(Db.Get().BuildingStatusItems.WaitingForMaterials, (GeothermalController.StatesInstance smi) => smi.GetFetchListForStatusItem());
			this.offline.checkSupplies.EnterTransition(this.offline.fetchSteel, (GeothermalController.StatesInstance smi) => smi.master.storage.MassStored() < 1200f).EnterTransition(this.offline.reconnectPipes, (GeothermalController.StatesInstance smi) => smi.master.storage.MassStored() >= 1200f).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerOffline, null);
			this.offline.reconnectPipes.Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.state = GeothermalController.ProgressState.RECONNECTING_PIPES;
			}).ToggleChore((GeothermalController.StatesInstance smi) => this.CreateRepairChore(smi), this.offline.notifyRepaired, this.offline.reconnectPipes).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerOffline, null)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoQuestPendingReconnectPipes, null);
			this.offline.notifyRepaired.Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.state = GeothermalController.ProgressState.NOTIFY_REPAIRED;
			}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerOffline, null).ToggleNotification((GeothermalController.StatesInstance smi) => this.CreateRepairedNotification(smi))
				.ToggleStatusItem(Db.Get().MiscStatusItems.AttentionRequired, null);
			this.offline.repaired.Exit(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.State = GeothermalController.ProgressState.REPAIRED;
			}).PlayAnim("on_pre").OnAnimQueueComplete(this.offline.filling)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master);
			this.offline.filling.PlayAnim("on").Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.TryAddConduitConsumers();
			}).ToggleOperationalFlag(GeothermalController.allowInputFlag)
				.Transition(this.offline.filled, (GeothermalController.StatesInstance smi) => smi.master.IsFull(), UpdateRate.SIM_200ms)
				.Update(delegate(GeothermalController.StatesInstance smi, float _)
				{
					smi.master.UpdatePressure();
				}, UpdateRate.SIM_1000ms, false)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master);
			this.offline.filled.Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.state = GeothermalController.ProgressState.AT_CAPACITY;
				smi.master.TryAddConduitConsumers();
			}).ToggleNotification((GeothermalController.StatesInstance smi) => smi.master.CreateFirstBatchReadyNotification()).EnterTransition(this.offline.filled.ready, (GeothermalController.StatesInstance smi) => !smi.master.IsObstructed())
				.EnterTransition(this.offline.filled.obstructed, (GeothermalController.StatesInstance smi) => smi.master.IsObstructed())
				.ToggleStatusItem(Db.Get().MiscStatusItems.AttentionRequired, null);
			this.offline.filled.ready.PlayAnim("on").Transition(this.offline.filled.obstructed, (GeothermalController.StatesInstance smi) => smi.master.IsObstructed(), UpdateRate.SIM_200ms).ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master);
			this.offline.filled.obstructed.Transition(this.offline.filled.ready, (GeothermalController.StatesInstance smi) => !smi.master.IsObstructed(), UpdateRate.SIM_200ms).PlayAnim("on").ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerCantVent, (GeothermalController.StatesInstance smi) => smi.master);
			this.online.Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.TryAddConduitConsumers();
			}).defaultState = this.online.active;
			this.online.active.PlayAnim("on").Transition(this.online.venting, (GeothermalController.StatesInstance smi) => smi.master.IsFull() && !smi.master.IsObstructed(), UpdateRate.SIM_1000ms).Transition(this.online.obstructed, (GeothermalController.StatesInstance smi) => smi.master.IsObstructed(), UpdateRate.SIM_1000ms)
				.Update(delegate(GeothermalController.StatesInstance smi, float _)
				{
					smi.master.UpdatePressure();
				}, UpdateRate.SIM_1000ms, false)
				.ToggleOperationalFlag(GeothermalController.allowInputFlag)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master);
			this.online.venting.Transition(this.online.obstructed, (GeothermalController.StatesInstance smi) => smi.master.IsObstructed(), UpdateRate.SIM_200ms).Enter(delegate(GeothermalController.StatesInstance smi)
			{
				smi.master.PushToVents();
			}).PlayAnim("venting_loop", KAnim.PlayMode.Loop)
				.Update(delegate(GeothermalController.StatesInstance smi, float f)
				{
					smi.master.FakeMeterDraining(f);
				}, UpdateRate.SIM_1000ms, false)
				.ScheduleGoTo(16f, this.online.active)
				.ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master);
			this.online.obstructed.Transition(this.online.active, (GeothermalController.StatesInstance smi) => !smi.master.IsObstructed(), UpdateRate.SIM_1000ms).PlayAnim("on").ToggleMainStatusItem(Db.Get().BuildingStatusItems.GeoControllerStorageStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerTemperatureStatus, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().BuildingStatusItems.GeoControllerCantVent, (GeothermalController.StatesInstance smi) => smi.master)
				.ToggleStatusItem(Db.Get().MiscStatusItems.AttentionRequired, null);
		}

		// Token: 0x06009364 RID: 37732 RVA: 0x0036B11C File Offset: 0x0036931C
		protected Chore CreateRepairFetchChore(GeothermalController.StatesInstance smi, HashSet<Tag> tags, float mass_required)
		{
			return new FetchChore(Db.Get().ChoreTypes.RepairFetch, smi.master.storage, mass_required, tags, FetchChore.MatchCriteria.MatchID, Tag.Invalid, null, null, true, null, null, null, Operational.State.None, 0);
		}

		// Token: 0x06009365 RID: 37733 RVA: 0x0036B158 File Offset: 0x00369358
		protected Chore CreateRepairChore(GeothermalController.StatesInstance smi)
		{
			return new WorkChore<GeothermalController.ReconnectPipes>(Db.Get().ChoreTypes.Repair, smi.master, null, true, null, null, null, true, null, false, false, null, false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
		}

		// Token: 0x06009366 RID: 37734 RVA: 0x0036B190 File Offset: 0x00369390
		protected Notification CreateRepairedNotification(GeothermalController.StatesInstance smi)
		{
			smi.master.dismissOnSelect = new Notification(COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.NOTIFICATIONS.GEOTHERMAL_PLANT_RECONNECTED, NotificationType.Event, (List<Notification> _, object __) => COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.NOTIFICATIONS.GEOTHERMAL_PLANT_RECONNECTED_TOOLTIP, null, false, 0f, delegate(object _)
			{
				smi.master.dismissOnSelect = null;
				this.SetProgressionToRepaired(smi);
			}, null, null, true, true, false);
			return smi.master.dismissOnSelect;
		}

		// Token: 0x06009367 RID: 37735 RVA: 0x0036B218 File Offset: 0x00369418
		protected void SetProgressionToRepaired(GeothermalController.StatesInstance smi)
		{
			SaveGame.Instance.ColonyAchievementTracker.GeothermalControllerRepaired = true;
			GeothermalPlantComponent.DisplayPopup(COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOTHERMAL_PLANT_REPAIRED_TITLE, COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.POPUPS.GEOTHERMAL_PLANT_REPAIRED_DESC, "geothermalplantonline_kanim", delegate
			{
				smi.GoTo(this.offline.repaired);
				SelectTool.Instance.Select(smi.master.GetComponent<KSelectable>(), true);
			}, smi.master.transform);
		}

		// Token: 0x04007188 RID: 29064
		public GeothermalController.States.OfflineStates offline;

		// Token: 0x04007189 RID: 29065
		public GeothermalController.States.OnlineStates online;

		// Token: 0x02002783 RID: 10115
		public class OfflineStates : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State
		{
			// Token: 0x0400AEBD RID: 44733
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State initial;

			// Token: 0x0400AEBE RID: 44734
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State fetchSteel;

			// Token: 0x0400AEBF RID: 44735
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State checkSupplies;

			// Token: 0x0400AEC0 RID: 44736
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State reconnectPipes;

			// Token: 0x0400AEC1 RID: 44737
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State notifyRepaired;

			// Token: 0x0400AEC2 RID: 44738
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State repaired;

			// Token: 0x0400AEC3 RID: 44739
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State filling;

			// Token: 0x0400AEC4 RID: 44740
			public GeothermalController.States.OfflineStates.FilledStates filled;

			// Token: 0x02003881 RID: 14465
			public class FilledStates : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State
			{
				// Token: 0x0400E46B RID: 58475
				public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State ready;

				// Token: 0x0400E46C RID: 58476
				public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State obstructed;
			}
		}

		// Token: 0x02002784 RID: 10116
		public class OnlineStates : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State
		{
			// Token: 0x0400AEC5 RID: 44741
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State active;

			// Token: 0x0400AEC6 RID: 44742
			public GeothermalController.States.OnlineStates.WorkingStates venting;

			// Token: 0x0400AEC7 RID: 44743
			public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State obstructed;

			// Token: 0x02003882 RID: 14466
			public class WorkingStates : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State
			{
				// Token: 0x0400E46D RID: 58477
				public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State pre;

				// Token: 0x0400E46E RID: 58478
				public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State loop;

				// Token: 0x0400E46F RID: 58479
				public GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.State post;
			}
		}
	}

	// Token: 0x020015F8 RID: 5624
	public class StatesInstance : GameStateMachine<GeothermalController.States, GeothermalController.StatesInstance, GeothermalController, object>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x0600936C RID: 37740 RVA: 0x0036B2C6 File Offset: 0x003694C6
		public StatesInstance(GeothermalController smi)
			: base(smi)
		{
		}

		// Token: 0x0600936D RID: 37741 RVA: 0x0036B2D0 File Offset: 0x003694D0
		public IFetchList GetFetchListForStatusItem()
		{
			GeothermalController.StatesInstance.FakeList fakeList = new GeothermalController.StatesInstance.FakeList();
			float num = 1200f - base.smi.master.storage.MassStored();
			fakeList.remaining[GameTagExtensions.Create(SimHashes.Steel)] = num;
			return fakeList;
		}

		// Token: 0x0600936E RID: 37742 RVA: 0x0036B314 File Offset: 0x00369514
		bool ISidescreenButtonControl.SidescreenButtonInteractable()
		{
			switch (base.smi.master.State)
			{
			case GeothermalController.ProgressState.NOT_STARTED:
			case GeothermalController.ProgressState.FETCHING_STEEL:
			case GeothermalController.ProgressState.RECONNECTING_PIPES:
				return true;
			case GeothermalController.ProgressState.NOTIFY_REPAIRED:
			case GeothermalController.ProgressState.REPAIRED:
				return false;
			case GeothermalController.ProgressState.AT_CAPACITY:
				return !base.smi.master.IsObstructed();
			case GeothermalController.ProgressState.COMPLETE:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x0600936F RID: 37743 RVA: 0x0036B371 File Offset: 0x00369571
		bool ISidescreenButtonControl.SidescreenEnabled()
		{
			return base.smi.master.State != GeothermalController.ProgressState.COMPLETE;
		}

		// Token: 0x06009370 RID: 37744 RVA: 0x0036B38C File Offset: 0x0036958C
		private string getSidescreenButtonText()
		{
			switch (base.smi.master.State)
			{
			case GeothermalController.ProgressState.NOT_STARTED:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.REPAIR_CONTROLLER_TITLE;
			case GeothermalController.ProgressState.FETCHING_STEEL:
			case GeothermalController.ProgressState.RECONNECTING_PIPES:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.CANCEL_REPAIR_CONTROLLER_TITLE;
			case GeothermalController.ProgressState.NOTIFY_REPAIRED:
			case GeothermalController.ProgressState.REPAIRED:
			case GeothermalController.ProgressState.AT_CAPACITY:
			case GeothermalController.ProgressState.COMPLETE:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.INITIATE_FIRST_VENT_TITLE;
			default:
				return "";
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06009371 RID: 37745 RVA: 0x0036B3F4 File Offset: 0x003695F4
		string ISidescreenButtonControl.SidescreenButtonText
		{
			get
			{
				return this.getSidescreenButtonText();
			}
		}

		// Token: 0x06009372 RID: 37746 RVA: 0x0036B3FC File Offset: 0x003695FC
		private string getSidescreenButtonTooltip()
		{
			switch (base.smi.master.State)
			{
			case GeothermalController.ProgressState.NOT_STARTED:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.REPAIR_CONTROLLER_TOOLTIP;
			case GeothermalController.ProgressState.FETCHING_STEEL:
			case GeothermalController.ProgressState.RECONNECTING_PIPES:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.CANCEL_REPAIR_CONTROLLER_TOOLTIP;
			case GeothermalController.ProgressState.NOTIFY_REPAIRED:
			case GeothermalController.ProgressState.REPAIRED:
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.INITIATE_FIRST_VENT_FILLING_TOOLTIP;
			case GeothermalController.ProgressState.AT_CAPACITY:
			case GeothermalController.ProgressState.COMPLETE:
				if (base.smi.master.IsObstructed())
				{
					return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.INITIATE_FIRST_VENT_UNAVAILABLE_TOOLTIP;
				}
				return COLONY_ACHIEVEMENTS.ACTIVATEGEOTHERMALPLANT.BUTTONS.INITIATE_FIRST_VENT_READY_TOOLTIP;
			default:
				return "";
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06009373 RID: 37747 RVA: 0x0036B48C File Offset: 0x0036968C
		string ISidescreenButtonControl.SidescreenButtonTooltip
		{
			get
			{
				return this.getSidescreenButtonTooltip();
			}
		}

		// Token: 0x06009374 RID: 37748 RVA: 0x0036B494 File Offset: 0x00369694
		void ISidescreenButtonControl.OnSidescreenButtonPressed()
		{
			switch (base.smi.master.state)
			{
			case GeothermalController.ProgressState.NOT_STARTED:
				base.smi.master.State = GeothermalController.ProgressState.FETCHING_STEEL;
				return;
			case GeothermalController.ProgressState.FETCHING_STEEL:
			case GeothermalController.ProgressState.RECONNECTING_PIPES:
				base.smi.master.State = GeothermalController.ProgressState.NOT_STARTED;
				base.smi.GoTo(base.sm.offline.initial);
				return;
			case GeothermalController.ProgressState.NOTIFY_REPAIRED:
			case GeothermalController.ProgressState.REPAIRED:
			case GeothermalController.ProgressState.COMPLETE:
				break;
			case GeothermalController.ProgressState.AT_CAPACITY:
			{
				MusicManager.instance.PlaySong("Music_Imperative_complete_DLC2", false);
				bool flag = base.smi.master.VentingCanFreeKeepsake();
				base.smi.master.state = GeothermalController.ProgressState.COMPLETE;
				base.smi.GoTo(base.sm.online.venting);
				if (!flag)
				{
					GeothermalFirstEmissionSequence.Start(base.smi.master);
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06009375 RID: 37749 RVA: 0x0036B572 File Offset: 0x00369772
		void ISidescreenButtonControl.SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009376 RID: 37750 RVA: 0x0036B579 File Offset: 0x00369779
		int ISidescreenButtonControl.HorizontalGroupID()
		{
			return -1;
		}

		// Token: 0x06009377 RID: 37751 RVA: 0x0036B57C File Offset: 0x0036977C
		int ISidescreenButtonControl.ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x02002788 RID: 10120
		protected class FakeList : IFetchList
		{
			// Token: 0x17000CD7 RID: 3287
			// (get) Token: 0x0600C7E4 RID: 51172 RVA: 0x004140F6 File Offset: 0x004122F6
			Storage IFetchList.Destination
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600C7E5 RID: 51173 RVA: 0x004140FD File Offset: 0x004122FD
			float IFetchList.GetMinimumAmount(Tag tag)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600C7E6 RID: 51174 RVA: 0x00414104 File Offset: 0x00412304
			Dictionary<Tag, float> IFetchList.GetRemaining()
			{
				return this.remaining;
			}

			// Token: 0x0600C7E7 RID: 51175 RVA: 0x0041410C File Offset: 0x0041230C
			Dictionary<Tag, float> IFetchList.GetRemainingMinimum()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0400AEFF RID: 44799
			public Dictionary<Tag, float> remaining = new Dictionary<Tag, float>();
		}
	}
}
