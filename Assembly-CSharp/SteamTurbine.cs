using System;
using Klei;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007CE RID: 1998
public class SteamTurbine : Generator
{
	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06003597 RID: 13719 RVA: 0x0012B701 File Offset: 0x00129901
	// (set) Token: 0x06003598 RID: 13720 RVA: 0x0012B709 File Offset: 0x00129909
	public int BlockedInputs { get; private set; }

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06003599 RID: 13721 RVA: 0x0012B712 File Offset: 0x00129912
	public int TotalInputs
	{
		get
		{
			return this.srcCells.Length;
		}
	}

	// Token: 0x0600359A RID: 13722 RVA: 0x0012B71C File Offset: 0x0012991C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.accumulator = Game.Instance.accumulators.Add("Power", this);
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		this.simEmitCBHandle = Game.Instance.massEmitCallbackManager.Add(new Action<Sim.MassEmittedCallback, object>(SteamTurbine.OnSimEmittedCallback), this, "SteamTurbineEmit");
		BuildingDef def = base.GetComponent<BuildingComplete>().Def;
		this.srcCells = new int[def.WidthInCells];
		int num = Grid.PosToCell(this);
		for (int i = 0; i < def.WidthInCells; i++)
		{
			int num2 = i - (def.WidthInCells - 1) / 2;
			this.srcCells[i] = Grid.OffsetCell(num, new CellOffset(num2, -2));
		}
		this.smi = new SteamTurbine.Instance(this);
		this.smi.StartSM();
		this.CreateMeter();
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x0012B7FC File Offset: 0x001299FC
	private void CreateMeter()
	{
		this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x0012B84C File Offset: 0x00129A4C
	protected override void OnCleanUp()
	{
		if (this.smi != null)
		{
			this.smi.StopSM("cleanup");
		}
		Game.Instance.massEmitCallbackManager.Release(this.simEmitCBHandle, "SteamTurbine");
		this.simEmitCBHandle.Clear();
		base.OnCleanUp();
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x0012B8A0 File Offset: 0x00129AA0
	private void Pump(float dt)
	{
		float num = this.pumpKGRate * dt / (float)this.srcCells.Length;
		foreach (int num2 in this.srcCells)
		{
			HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(SteamTurbine.OnSimConsumeCallback), this, "SteamTurbineConsume");
			SimMessages.ConsumeMass(num2, this.srcElem, num, 1, handle.index);
		}
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x0012B90E File Offset: 0x00129B0E
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		((SteamTurbine)data).OnSimConsume(mass_cb_info);
	}

	// Token: 0x0600359F RID: 13727 RVA: 0x0012B91C File Offset: 0x00129B1C
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		if (mass_cb_info.mass > 0f)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, mass_cb_info.mass, mass_cb_info.temperature);
			this.storedMass += mass_cb_info.mass;
			SimUtil.DiseaseInfo diseaseInfo = SimUtil.CalculateFinalDiseaseInfo(this.diseaseIdx, this.diseaseCount, mass_cb_info.diseaseIdx, mass_cb_info.diseaseCount);
			this.diseaseIdx = diseaseInfo.idx;
			this.diseaseCount = diseaseInfo.count;
			if (this.storedMass > this.minConvertMass && this.simEmitCBHandle.IsValid())
			{
				Game.Instance.massEmitCallbackManager.GetItem(this.simEmitCBHandle);
				this.gasStorage.AddGasChunk(this.srcElem, this.storedMass, this.storedTemperature, this.diseaseIdx, this.diseaseCount, true, true);
				this.storedMass = 0f;
				this.storedTemperature = 0f;
				this.diseaseIdx = byte.MaxValue;
				this.diseaseCount = 0;
			}
		}
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x0012BA2A File Offset: 0x00129C2A
	private static void OnSimEmittedCallback(Sim.MassEmittedCallback info, object data)
	{
		((SteamTurbine)data).OnSimEmitted(info);
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x0012BA38 File Offset: 0x00129C38
	private void OnSimEmitted(Sim.MassEmittedCallback info)
	{
		if (info.suceeded != 1)
		{
			this.storedTemperature = SimUtil.CalculateFinalTemperature(this.storedMass, this.storedTemperature, info.mass, info.temperature);
			this.storedMass += info.mass;
			if (info.diseaseIdx != 255)
			{
				SimUtil.DiseaseInfo diseaseInfo = new SimUtil.DiseaseInfo
				{
					idx = this.diseaseIdx,
					count = this.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo2 = new SimUtil.DiseaseInfo
				{
					idx = info.diseaseIdx,
					count = info.diseaseCount
				};
				SimUtil.DiseaseInfo diseaseInfo3 = SimUtil.CalculateFinalDiseaseInfo(diseaseInfo, diseaseInfo2);
				this.diseaseIdx = diseaseInfo3.idx;
				this.diseaseCount = diseaseInfo3.count;
			}
		}
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x0012BAFC File Offset: 0x00129CFC
	public static void InitializeStatusItems()
	{
		SteamTurbine.activeStatusItem = new StatusItem("TURBINE_ACTIVE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Good, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputBlockedStatusItem = new StatusItem("TURBINE_BLOCKED_INPUT", "BUILDING", "status_item_vent_disabled", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputPartiallyBlockedStatusItem = new StatusItem("TURBINE_PARTIALLY_BLOCKED_INPUT", "BUILDING", "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.inputPartiallyBlockedStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolvePartialBlockedStatus);
		SteamTurbine.insufficientMassStatusItem = new StatusItem("TURBINE_INSUFFICIENT_MASS", "BUILDING", "status_item_resource_unavailable", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.insufficientMassStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.buildingTooHotItem = new StatusItem("TURBINE_TOO_HOT", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, null);
		SteamTurbine.buildingTooHotItem.resolveTooltipCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.insufficientTemperatureStatusItem = new StatusItem("TURBINE_INSUFFICIENT_TEMPERATURE", "BUILDING", "status_item_plant_temperature", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.insufficientTemperatureStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.insufficientTemperatureStatusItem.resolveTooltipCallback = new Func<string, object, string>(SteamTurbine.ResolveStrings);
		SteamTurbine.activeWattageStatusItem = new StatusItem("TURBINE_ACTIVE_WATTAGE", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.Power.ID, true, 129022, null);
		SteamTurbine.activeWattageStatusItem.resolveStringCallback = new Func<string, object, string>(SteamTurbine.ResolveWattageStatus);
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x0012BCA8 File Offset: 0x00129EA8
	private static string ResolveWattageStatus(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		float num = Game.Instance.accumulators.GetAverageRate(steamTurbine.accumulator) / steamTurbine.WattageRating;
		return str.Replace("{Wattage}", GameUtil.GetFormattedWattage(steamTurbine.CurrentWattage, GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{Max_Wattage}", GameUtil.GetFormattedWattage(steamTurbine.WattageRating, GameUtil.WattageFormatterUnit.Automatic, true)).Replace("{Efficiency}", GameUtil.GetFormattedPercent(num * 100f, GameUtil.TimeSlice.None))
			.Replace("{Src_Element}", ElementLoader.FindElementByHash(steamTurbine.srcElem).name);
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x0012BD3C File Offset: 0x00129F3C
	private static string ResolvePartialBlockedStatus(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		return str.Replace("{Blocked}", steamTurbine.BlockedInputs.ToString()).Replace("{Total}", steamTurbine.TotalInputs.ToString());
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x0012BD84 File Offset: 0x00129F84
	private static string ResolveStrings(string str, object data)
	{
		SteamTurbine steamTurbine = (SteamTurbine)data;
		str = str.Replace("{Src_Element}", ElementLoader.FindElementByHash(steamTurbine.srcElem).name);
		str = str.Replace("{Dest_Element}", ElementLoader.FindElementByHash(steamTurbine.destElem).name);
		str = str.Replace("{Overheat_Temperature}", GameUtil.GetFormattedTemperature(steamTurbine.maxBuildingTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		str = str.Replace("{Active_Temperature}", GameUtil.GetFormattedTemperature(steamTurbine.minActiveTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
		str = str.Replace("{Min_Mass}", GameUtil.GetFormattedMass(steamTurbine.requiredMass, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		return str;
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x0012BE2B File Offset: 0x0012A02B
	public void SetStorage(Storage steamStorage, Storage waterStorage)
	{
		this.gasStorage = steamStorage;
		this.liquidStorage = waterStorage;
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x0012BE3C File Offset: 0x0012A03C
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		ushort circuitID = base.CircuitID;
		this.operational.SetFlag(Generator.wireConnectedFlag, circuitID != ushort.MaxValue);
		if (!this.operational.IsOperational)
		{
			this.meter.SetPositionPercent(0f);
			return;
		}
		float num = 0f;
		if (this.gasStorage != null && this.gasStorage.items.Count > 0)
		{
			GameObject gameObject = this.gasStorage.FindFirst(ElementLoader.FindElementByHash(this.srcElem).tag);
			if (gameObject != null)
			{
				PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
				float num2 = 0.1f;
				if (component.Mass > num2)
				{
					num2 = Mathf.Min(component.Mass, this.pumpKGRate * dt);
					num = Mathf.Min(this.JoulesToGenerate(component) * (num2 / this.pumpKGRate), base.WattageRating * dt);
					float num3 = this.HeatFromCoolingSteam(component) * (num2 / component.Mass);
					float num4 = num2 / component.Mass;
					int num5 = Mathf.RoundToInt((float)component.DiseaseCount * num4);
					component.Mass -= num2;
					component.ModifyDiseaseCount(-num5, "SteamTurbine.EnergySim200ms");
					float num6 = ((this.lastSampleTime > 0f) ? (Time.time - this.lastSampleTime) : 1f);
					this.lastSampleTime = Time.time;
					GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, num3 * this.wasteHeatToTurbinePercent, BUILDINGS.PREFABS.STEAMTURBINE2.HEAT_SOURCE, num6);
					this.liquidStorage.AddLiquid(this.destElem, num2, this.outputElementTemperature, component.DiseaseIdx, num5, true, true);
				}
			}
		}
		num = Mathf.Clamp(num, 0f, base.WattageRating);
		Game.Instance.accumulators.Accumulate(this.accumulator, num);
		if (num > 0f)
		{
			base.GenerateJoules(num, false);
		}
		this.meter.SetPositionPercent(Game.Instance.accumulators.GetAverageRate(this.accumulator) / base.WattageRating);
		this.meter.SetSymbolTint(SteamTurbine.TINT_SYMBOL, Color.Lerp(Color.red, Color.green, Game.Instance.accumulators.GetAverageRate(this.accumulator) / base.WattageRating));
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x0012C09C File Offset: 0x0012A29C
	public float HeatFromCoolingSteam(PrimaryElement steam)
	{
		float temperature = steam.Temperature;
		return -GameUtil.CalculateEnergyDeltaForElement(steam, temperature, this.outputElementTemperature);
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x0012C0C0 File Offset: 0x0012A2C0
	public float JoulesToGenerate(PrimaryElement steam)
	{
		float num = (steam.Temperature - this.outputElementTemperature) / (this.idealSourceElementTemperature - this.outputElementTemperature);
		return base.WattageRating * (float)Math.Pow((double)num, 1.0);
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x060035AA RID: 13738 RVA: 0x0012C101 File Offset: 0x0012A301
	public float CurrentWattage
	{
		get
		{
			return Game.Instance.accumulators.GetAverageRate(this.accumulator);
		}
	}

	// Token: 0x0400205A RID: 8282
	private HandleVector<int>.Handle accumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x0400205B RID: 8283
	public SimHashes srcElem;

	// Token: 0x0400205C RID: 8284
	public SimHashes destElem;

	// Token: 0x0400205D RID: 8285
	public float requiredMass = 0.001f;

	// Token: 0x0400205E RID: 8286
	public float minActiveTemperature = 398.15f;

	// Token: 0x0400205F RID: 8287
	public float idealSourceElementTemperature = 473.15f;

	// Token: 0x04002060 RID: 8288
	public float maxBuildingTemperature = 373.15f;

	// Token: 0x04002061 RID: 8289
	public float outputElementTemperature = 368.15f;

	// Token: 0x04002062 RID: 8290
	public float minConvertMass;

	// Token: 0x04002063 RID: 8291
	public float pumpKGRate;

	// Token: 0x04002064 RID: 8292
	public float maxSelfHeat;

	// Token: 0x04002065 RID: 8293
	public float wasteHeatToTurbinePercent;

	// Token: 0x04002066 RID: 8294
	private static readonly HashedString TINT_SYMBOL = new HashedString("meter_fill");

	// Token: 0x04002067 RID: 8295
	[Serialize]
	private float storedMass;

	// Token: 0x04002068 RID: 8296
	[Serialize]
	private float storedTemperature;

	// Token: 0x04002069 RID: 8297
	[Serialize]
	private byte diseaseIdx = byte.MaxValue;

	// Token: 0x0400206A RID: 8298
	[Serialize]
	private int diseaseCount;

	// Token: 0x0400206B RID: 8299
	private static StatusItem inputBlockedStatusItem;

	// Token: 0x0400206C RID: 8300
	private static StatusItem inputPartiallyBlockedStatusItem;

	// Token: 0x0400206D RID: 8301
	private static StatusItem insufficientMassStatusItem;

	// Token: 0x0400206E RID: 8302
	private static StatusItem insufficientTemperatureStatusItem;

	// Token: 0x0400206F RID: 8303
	private static StatusItem activeWattageStatusItem;

	// Token: 0x04002070 RID: 8304
	private static StatusItem buildingTooHotItem;

	// Token: 0x04002071 RID: 8305
	private static StatusItem activeStatusItem;

	// Token: 0x04002073 RID: 8307
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)39;

	// Token: 0x04002074 RID: 8308
	private MeterController meter;

	// Token: 0x04002075 RID: 8309
	private HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.Handle simEmitCBHandle = HandleVector<Game.ComplexCallbackInfo<Sim.MassEmittedCallback>>.InvalidHandle;

	// Token: 0x04002076 RID: 8310
	private SteamTurbine.Instance smi;

	// Token: 0x04002077 RID: 8311
	private int[] srcCells;

	// Token: 0x04002078 RID: 8312
	private Storage gasStorage;

	// Token: 0x04002079 RID: 8313
	private Storage liquidStorage;

	// Token: 0x0400207A RID: 8314
	private ElementConsumer consumer;

	// Token: 0x0400207B RID: 8315
	private Guid statusHandle;

	// Token: 0x0400207C RID: 8316
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x0400207D RID: 8317
	private float lastSampleTime = -1f;

	// Token: 0x0200170D RID: 5901
	public class States : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine>
	{
		// Token: 0x06009775 RID: 38773 RVA: 0x0037C594 File Offset: 0x0037A794
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			SteamTurbine.InitializeStatusItems();
			default_state = this.operational;
			this.root.Update("UpdateBlocked", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.UpdateBlocked(dt);
			}, UpdateRate.SIM_200ms, false);
			this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational.active, (SteamTurbine.Instance smi) => smi.master.GetComponent<Operational>().IsOperational).QueueAnim("off", false, null);
			this.operational.DefaultState(this.operational.active).EventTransition(GameHashes.OperationalChanged, this.inoperational, (SteamTurbine.Instance smi) => !smi.master.GetComponent<Operational>().IsOperational).Update("UpdateOperational", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.UpdateState(dt);
			}, UpdateRate.SIM_200ms, false)
				.Exit(delegate(SteamTurbine.Instance smi)
				{
					smi.DisableStatusItems();
				});
			this.operational.idle.QueueAnim("on", false, null);
			this.operational.active.Update("UpdateActive", delegate(SteamTurbine.Instance smi, float dt)
			{
				smi.master.Pump(dt);
			}, UpdateRate.SIM_200ms, false).ToggleStatusItem((SteamTurbine.Instance smi) => SteamTurbine.activeStatusItem, (SteamTurbine.Instance smi) => smi.master).Enter(delegate(SteamTurbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(SteamTurbine.States.ACTIVE_ANIMS, KAnim.PlayMode.Loop);
				smi.GetComponent<Operational>().SetActive(true, false);
			})
				.Exit(delegate(SteamTurbine.Instance smi)
				{
					smi.master.GetComponent<Generator>().ResetJoules();
					smi.GetComponent<Operational>().SetActive(false, false);
				});
			this.operational.tooHot.Enter(delegate(SteamTurbine.Instance smi)
			{
				smi.GetComponent<KAnimControllerBase>().Play(SteamTurbine.States.TOOHOT_ANIMS, KAnim.PlayMode.Loop);
			});
		}

		// Token: 0x04007478 RID: 29816
		public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State inoperational;

		// Token: 0x04007479 RID: 29817
		public SteamTurbine.States.OperationalStates operational;

		// Token: 0x0400747A RID: 29818
		private static readonly HashedString[] ACTIVE_ANIMS = new HashedString[] { "working_pre", "working_loop" };

		// Token: 0x0400747B RID: 29819
		private static readonly HashedString[] TOOHOT_ANIMS = new HashedString[] { "working_pre" };

		// Token: 0x020027E1 RID: 10209
		public class OperationalStates : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State
		{
			// Token: 0x0400B0D1 RID: 45265
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State idle;

			// Token: 0x0400B0D2 RID: 45266
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State active;

			// Token: 0x0400B0D3 RID: 45267
			public GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.State tooHot;
		}
	}

	// Token: 0x0200170E RID: 5902
	public class Instance : GameStateMachine<SteamTurbine.States, SteamTurbine.Instance, SteamTurbine, object>.GameInstance
	{
		// Token: 0x06009778 RID: 38776 RVA: 0x0037C828 File Offset: 0x0037AA28
		public Instance(SteamTurbine master)
			: base(master)
		{
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x0037C880 File Offset: 0x0037AA80
		public void UpdateBlocked(float dt)
		{
			base.master.BlockedInputs = 0;
			for (int i = 0; i < base.master.TotalInputs; i++)
			{
				int num = base.master.srcCells[i];
				Element element = Grid.Element[num];
				if (element.IsLiquid || element.IsSolid)
				{
					SteamTurbine master = base.master;
					int blockedInputs = master.BlockedInputs;
					master.BlockedInputs = blockedInputs + 1;
				}
			}
			KSelectable component = base.GetComponent<KSelectable>();
			this.inputBlockedHandle = this.UpdateStatusItem(SteamTurbine.inputBlockedStatusItem, base.master.BlockedInputs == base.master.TotalInputs, this.inputBlockedHandle, component);
			this.inputPartiallyBlockedHandle = this.UpdateStatusItem(SteamTurbine.inputPartiallyBlockedStatusItem, base.master.BlockedInputs > 0 && base.master.BlockedInputs < base.master.TotalInputs, this.inputPartiallyBlockedHandle, component);
		}

		// Token: 0x0600977A RID: 38778 RVA: 0x0037C964 File Offset: 0x0037AB64
		public void UpdateState(float dt)
		{
			bool flag = this.CanSteamFlow(ref this.insufficientMass, ref this.insufficientTemperature);
			bool flag2 = this.IsTooHot(ref this.buildingTooHot);
			this.UpdateStatusItems();
			StateMachine.BaseState currentState = base.smi.GetCurrentState();
			if (flag2)
			{
				if (currentState != base.sm.operational.tooHot)
				{
					base.smi.GoTo(base.sm.operational.tooHot);
					return;
				}
			}
			else if (flag)
			{
				if (currentState != base.sm.operational.active)
				{
					base.smi.GoTo(base.sm.operational.active);
					return;
				}
			}
			else if (currentState != base.sm.operational.idle)
			{
				base.smi.GoTo(base.sm.operational.idle);
			}
		}

		// Token: 0x0600977B RID: 38779 RVA: 0x0037CA33 File Offset: 0x0037AC33
		private bool IsTooHot(ref bool building_too_hot)
		{
			building_too_hot = base.gameObject.GetComponent<PrimaryElement>().Temperature > base.smi.master.maxBuildingTemperature;
			return building_too_hot;
		}

		// Token: 0x0600977C RID: 38780 RVA: 0x0037CA5C File Offset: 0x0037AC5C
		private bool CanSteamFlow(ref bool insufficient_mass, ref bool insufficient_temperature)
		{
			float num = 0f;
			float num2 = 0f;
			for (int i = 0; i < base.master.srcCells.Length; i++)
			{
				int num3 = base.master.srcCells[i];
				float num4 = Grid.Mass[num3];
				if (Grid.Element[num3].id == base.master.srcElem)
				{
					num = Mathf.Max(num, num4);
					float num5 = Grid.Temperature[num3];
					num2 = Mathf.Max(num2, num5);
				}
			}
			insufficient_mass = num < base.master.requiredMass;
			insufficient_temperature = num2 < base.master.minActiveTemperature;
			return !insufficient_mass && !insufficient_temperature;
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x0037CB0C File Offset: 0x0037AD0C
		public void UpdateStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			this.insufficientMassHandle = this.UpdateStatusItem(SteamTurbine.insufficientMassStatusItem, this.insufficientMass, this.insufficientMassHandle, component);
			this.insufficientTemperatureHandle = this.UpdateStatusItem(SteamTurbine.insufficientTemperatureStatusItem, this.insufficientTemperature, this.insufficientTemperatureHandle, component);
			this.buildingTooHotHandle = this.UpdateStatusItem(SteamTurbine.buildingTooHotItem, this.buildingTooHot, this.buildingTooHotHandle, component);
			StatusItem statusItem = (base.master.operational.IsActive ? SteamTurbine.activeWattageStatusItem : Db.Get().BuildingStatusItems.GeneratorOffline);
			this.activeWattageHandle = component.SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, base.master);
		}

		// Token: 0x0600977E RID: 38782 RVA: 0x0037CBC8 File Offset: 0x0037ADC8
		private Guid UpdateStatusItem(StatusItem item, bool show, Guid current_handle, KSelectable ksel)
		{
			Guid guid = current_handle;
			if (show != (current_handle != Guid.Empty))
			{
				if (show)
				{
					guid = ksel.AddStatusItem(item, base.master);
				}
				else
				{
					guid = ksel.RemoveStatusItem(current_handle, false);
				}
			}
			return guid;
		}

		// Token: 0x0600977F RID: 38783 RVA: 0x0037CC04 File Offset: 0x0037AE04
		public void DisableStatusItems()
		{
			KSelectable component = base.GetComponent<KSelectable>();
			component.RemoveStatusItem(this.buildingTooHotHandle, false);
			component.RemoveStatusItem(this.insufficientMassHandle, false);
			component.RemoveStatusItem(this.insufficientTemperatureHandle, false);
			component.RemoveStatusItem(this.activeWattageHandle, false);
		}

		// Token: 0x0400747C RID: 29820
		public bool insufficientMass;

		// Token: 0x0400747D RID: 29821
		public bool insufficientTemperature;

		// Token: 0x0400747E RID: 29822
		public bool buildingTooHot;

		// Token: 0x0400747F RID: 29823
		private Guid inputBlockedHandle = Guid.Empty;

		// Token: 0x04007480 RID: 29824
		private Guid inputPartiallyBlockedHandle = Guid.Empty;

		// Token: 0x04007481 RID: 29825
		private Guid insufficientMassHandle = Guid.Empty;

		// Token: 0x04007482 RID: 29826
		private Guid insufficientTemperatureHandle = Guid.Empty;

		// Token: 0x04007483 RID: 29827
		private Guid buildingTooHotHandle = Guid.Empty;

		// Token: 0x04007484 RID: 29828
		private Guid activeWattageHandle = Guid.Empty;
	}
}
