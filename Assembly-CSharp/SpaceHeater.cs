using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007CB RID: 1995
[SerializationConfig(MemberSerialization.OptIn)]
public class SpaceHeater : StateMachineComponent<SpaceHeater.StatesInstance>, IGameObjectEffectDescriptor, ISingleSliderControl, ISliderControl
{
	// Token: 0x17000370 RID: 880
	// (get) Token: 0x0600356E RID: 13678 RVA: 0x0012AF39 File Offset: 0x00129139
	public float TargetTemperature
	{
		get
		{
			return this.targetTemperature;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x0600356F RID: 13679 RVA: 0x0012AF41 File Offset: 0x00129141
	public float MaxPower
	{
		get
		{
			return 240f;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06003570 RID: 13680 RVA: 0x0012AF48 File Offset: 0x00129148
	public float MinPower
	{
		get
		{
			return 120f;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06003571 RID: 13681 RVA: 0x0012AF4F File Offset: 0x0012914F
	public float MaxSelfHeatKWs
	{
		get
		{
			return 32f;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06003572 RID: 13682 RVA: 0x0012AF56 File Offset: 0x00129156
	public float MinSelfHeatKWs
	{
		get
		{
			return 16f;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06003573 RID: 13683 RVA: 0x0012AF5D File Offset: 0x0012915D
	public float MaxExhaustedKWs
	{
		get
		{
			return 4f;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06003574 RID: 13684 RVA: 0x0012AF64 File Offset: 0x00129164
	public float MinExhaustedKWs
	{
		get
		{
			return 2f;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06003575 RID: 13685 RVA: 0x0012AF6B File Offset: 0x0012916B
	public float CurrentSelfHeatKW
	{
		get
		{
			return Mathf.Lerp(this.MinSelfHeatKWs, this.MaxSelfHeatKWs, this.UserSliderSetting);
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06003576 RID: 13686 RVA: 0x0012AF84 File Offset: 0x00129184
	public float CurrentExhaustedKW
	{
		get
		{
			return Mathf.Lerp(this.MinExhaustedKWs, this.MaxExhaustedKWs, this.UserSliderSetting);
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06003577 RID: 13687 RVA: 0x0012AF9D File Offset: 0x0012919D
	public float CurrentPowerConsumption
	{
		get
		{
			return Mathf.Lerp(this.MinPower, this.MaxPower, this.UserSliderSetting);
		}
	}

	// Token: 0x06003578 RID: 13688 RVA: 0x0012AFB6 File Offset: 0x001291B6
	public static void GenerateHeat(SpaceHeater.StatesInstance smi, float dt)
	{
		if (smi.master.produceHeat)
		{
			SpaceHeater.AddExhaustHeat(smi, dt);
			SpaceHeater.AddSelfHeat(smi, dt);
		}
	}

	// Token: 0x06003579 RID: 13689 RVA: 0x0012AFD8 File Offset: 0x001291D8
	private static float AddExhaustHeat(SpaceHeater.StatesInstance smi, float dt)
	{
		float currentExhaustedKW = smi.master.CurrentExhaustedKW;
		StructureTemperatureComponents.ExhaustHeat(smi.master.extents, currentExhaustedKW, smi.master.overheatTemperature, dt);
		return currentExhaustedKW;
	}

	// Token: 0x0600357A RID: 13690 RVA: 0x0012B010 File Offset: 0x00129210
	public static void RefreshHeatEffect(SpaceHeater.StatesInstance smi)
	{
		if (smi.master.heatEffect != null && smi.master.produceHeat)
		{
			float num = (smi.IsInsideState(smi.sm.online.heating) ? (smi.master.CurrentExhaustedKW + smi.master.CurrentSelfHeatKW) : 0f);
			smi.master.heatEffect.SetHeatBeingProducedValue(num);
		}
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x0012B088 File Offset: 0x00129288
	private static float AddSelfHeat(SpaceHeater.StatesInstance smi, float dt)
	{
		float currentSelfHeatKW = smi.master.CurrentSelfHeatKW;
		GameComps.StructureTemperatures.ProduceEnergy(smi.master.structureTemperature, currentSelfHeatKW * dt, BUILDINGS.PREFABS.STEAMTURBINE2.HEAT_SOURCE, dt);
		return currentSelfHeatKW;
	}

	// Token: 0x0600357C RID: 13692 RVA: 0x0012B0C8 File Offset: 0x001292C8
	public void SetUserSpecifiedPowerConsumptionValue(float value)
	{
		if (this.produceHeat)
		{
			this.UserSliderSetting = (value - this.MinPower) / (this.MaxPower - this.MinPower);
			SpaceHeater.RefreshHeatEffect(base.smi);
			this.energyConsumer.BaseWattageRating = this.CurrentPowerConsumption;
		}
	}

	// Token: 0x0600357D RID: 13693 RVA: 0x0012B118 File Offset: 0x00129318
	protected override void OnPrefabInit()
	{
		if (this.produceHeat)
		{
			this.heatStatusItem = new StatusItem("OperatingEnergy", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			this.heatStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				SpaceHeater.StatesInstance statesInstance = (SpaceHeater.StatesInstance)data;
				float num = statesInstance.master.CurrentSelfHeatKW + statesInstance.master.CurrentExhaustedKW;
				str = string.Format(str, GameUtil.GetFormattedHeatEnergy(num * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic));
				return str;
			};
			this.heatStatusItem.resolveTooltipCallback = delegate(string str, object data)
			{
				SpaceHeater.StatesInstance statesInstance2 = (SpaceHeater.StatesInstance)data;
				float num2 = statesInstance2.master.CurrentSelfHeatKW + statesInstance2.master.CurrentExhaustedKW;
				str = str.Replace("{0}", GameUtil.GetFormattedHeatEnergy(num2 * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic));
				string text = string.Format(BUILDING.STATUSITEMS.OPERATINGENERGY.LINEITEM, BUILDING.STATUSITEMS.OPERATINGENERGY.OPERATING, GameUtil.GetFormattedHeatEnergy(statesInstance2.master.CurrentSelfHeatKW * 1000f, GameUtil.HeatEnergyFormatterUnit.DTU_S));
				text += string.Format(BUILDING.STATUSITEMS.OPERATINGENERGY.LINEITEM, BUILDING.STATUSITEMS.OPERATINGENERGY.EXHAUSTING, GameUtil.GetFormattedHeatEnergy(statesInstance2.master.CurrentExhaustedKW * 1000f, GameUtil.HeatEnergyFormatterUnit.DTU_S));
				str = str.Replace("{1}", text);
				return str;
			};
		}
		base.OnPrefabInit();
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x0012B1B0 File Offset: 0x001293B0
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		this.extents = base.GetComponent<OccupyArea>().GetExtents();
		this.overheatTemperature = base.GetComponent<BuildingComplete>().Def.OverheatTemperature;
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		base.smi.StartSM();
		this.SetUserSpecifiedPowerConsumptionValue(this.CurrentPowerConsumption);
	}

	// Token: 0x0600357F RID: 13695 RVA: 0x0012B24D File Offset: 0x0012944D
	public void SetLiquidHeater()
	{
		this.heatLiquid = true;
	}

	// Token: 0x06003580 RID: 13696 RVA: 0x0012B258 File Offset: 0x00129458
	private SpaceHeater.MonitorState MonitorHeating(float dt)
	{
		this.monitorCells.Clear();
		GameUtil.GetNonSolidCells(Grid.PosToCell(base.transform.GetPosition()), this.radius, this.monitorCells);
		int num = 0;
		float num2 = 0f;
		for (int i = 0; i < this.monitorCells.Count; i++)
		{
			if (Grid.Mass[this.monitorCells[i]] > this.minimumCellMass && ((Grid.Element[this.monitorCells[i]].IsGas && !this.heatLiquid) || (Grid.Element[this.monitorCells[i]].IsLiquid && this.heatLiquid)))
			{
				num++;
				num2 += Grid.Temperature[this.monitorCells[i]];
			}
		}
		if (num == 0)
		{
			if (!this.heatLiquid)
			{
				return SpaceHeater.MonitorState.NotEnoughGas;
			}
			return SpaceHeater.MonitorState.NotEnoughLiquid;
		}
		else
		{
			if (num2 / (float)num >= this.targetTemperature)
			{
				return SpaceHeater.MonitorState.TooHot;
			}
			return SpaceHeater.MonitorState.ReadyToHeat;
		}
	}

	// Token: 0x06003581 RID: 13697 RVA: 0x0012B358 File Offset: 0x00129558
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		Descriptor descriptor = default(Descriptor);
		descriptor.SetupDescriptor(string.Format(UI.BUILDINGEFFECTS.HEATER_TARGETTEMPERATURE, GameUtil.GetFormattedTemperature(this.targetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.BUILDINGEFFECTS.TOOLTIPS.HEATER_TARGETTEMPERATURE, GameUtil.GetFormattedTemperature(this.targetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		return list;
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06003582 RID: 13698 RVA: 0x0012B3BD File Offset: 0x001295BD
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.SPACEHEATERSIDESCREEN.TITLE";
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06003583 RID: 13699 RVA: 0x0012B3C4 File Offset: 0x001295C4
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.ELECTRICAL.WATT;
		}
	}

	// Token: 0x06003584 RID: 13700 RVA: 0x0012B3D0 File Offset: 0x001295D0
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x06003585 RID: 13701 RVA: 0x0012B3D3 File Offset: 0x001295D3
	public float GetSliderMin(int index)
	{
		if (!this.produceHeat)
		{
			return 0f;
		}
		return this.MinPower;
	}

	// Token: 0x06003586 RID: 13702 RVA: 0x0012B3E9 File Offset: 0x001295E9
	public float GetSliderMax(int index)
	{
		if (!this.produceHeat)
		{
			return 0f;
		}
		return this.MaxPower;
	}

	// Token: 0x06003587 RID: 13703 RVA: 0x0012B3FF File Offset: 0x001295FF
	public float GetSliderValue(int index)
	{
		return this.CurrentPowerConsumption;
	}

	// Token: 0x06003588 RID: 13704 RVA: 0x0012B407 File Offset: 0x00129607
	public void SetSliderValue(float value, int index)
	{
		this.SetUserSpecifiedPowerConsumptionValue(value);
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x0012B410 File Offset: 0x00129610
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.SPACEHEATERSIDESCREEN.TOOLTIP";
	}

	// Token: 0x0600358A RID: 13706 RVA: 0x0012B417 File Offset: 0x00129617
	string ISliderControl.GetSliderTooltip(int index)
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.SPACEHEATERSIDESCREEN.TOOLTIP"), GameUtil.GetFormattedHeatEnergyRate((this.CurrentSelfHeatKW + this.CurrentExhaustedKW) * 1000f, GameUtil.HeatEnergyFormatterUnit.Automatic));
	}

	// Token: 0x04002046 RID: 8262
	public float targetTemperature = 308.15f;

	// Token: 0x04002047 RID: 8263
	public float minimumCellMass;

	// Token: 0x04002048 RID: 8264
	public int radius = 2;

	// Token: 0x04002049 RID: 8265
	[SerializeField]
	private bool heatLiquid;

	// Token: 0x0400204A RID: 8266
	[Serialize]
	public float UserSliderSetting;

	// Token: 0x0400204B RID: 8267
	public bool produceHeat;

	// Token: 0x0400204C RID: 8268
	private StatusItem heatStatusItem;

	// Token: 0x0400204D RID: 8269
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x0400204E RID: 8270
	private Extents extents;

	// Token: 0x0400204F RID: 8271
	private float overheatTemperature;

	// Token: 0x04002050 RID: 8272
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04002051 RID: 8273
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04002052 RID: 8274
	[MyCmpGet]
	private KBatchedAnimHeatPostProcessingEffect heatEffect;

	// Token: 0x04002053 RID: 8275
	[MyCmpGet]
	private EnergyConsumer energyConsumer;

	// Token: 0x04002054 RID: 8276
	private List<int> monitorCells = new List<int>();

	// Token: 0x02001707 RID: 5895
	public class StatesInstance : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.GameInstance
	{
		// Token: 0x06009769 RID: 38761 RVA: 0x0037C062 File Offset: 0x0037A262
		public StatesInstance(SpaceHeater master)
			: base(master)
		{
		}
	}

	// Token: 0x02001708 RID: 5896
	public class States : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater>
	{
		// Token: 0x0600976A RID: 38762 RVA: 0x0037C06C File Offset: 0x0037A26C
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.offline;
			base.serializable = StateMachine.SerializeType.Never;
			this.statusItemUnderMassLiquid = new StatusItem("statusItemUnderMassLiquid", BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_LIQUID.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_LIQUID.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemUnderMassGas = new StatusItem("statusItemUnderMassGas", BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_GAS.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDLOWMASS_GAS.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemOverTemp = new StatusItem("statusItemOverTemp", BUILDING.STATUSITEMS.HEATINGSTALLEDHOTENV.NAME, BUILDING.STATUSITEMS.HEATINGSTALLEDHOTENV.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.BadMinor, false, OverlayModes.None.ID, 129022, true, null);
			this.statusItemOverTemp.resolveStringCallback = delegate(string str, object obj)
			{
				SpaceHeater.StatesInstance statesInstance = (SpaceHeater.StatesInstance)obj;
				return string.Format(str, GameUtil.GetFormattedTemperature(statesInstance.master.TargetTemperature, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
			};
			this.offline.Enter(new StateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State.Callback(SpaceHeater.RefreshHeatEffect)).EventTransition(GameHashes.OperationalChanged, this.online, (SpaceHeater.StatesInstance smi) => smi.master.operational.IsOperational);
			this.online.EventTransition(GameHashes.OperationalChanged, this.offline, (SpaceHeater.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.online.heating).Update("spaceheater_online", delegate(SpaceHeater.StatesInstance smi, float dt)
			{
				switch (smi.master.MonitorHeating(dt))
				{
				case SpaceHeater.MonitorState.ReadyToHeat:
					smi.GoTo(this.online.heating);
					return;
				case SpaceHeater.MonitorState.TooHot:
					smi.GoTo(this.online.overtemp);
					return;
				case SpaceHeater.MonitorState.NotEnoughLiquid:
					smi.GoTo(this.online.undermassliquid);
					return;
				case SpaceHeater.MonitorState.NotEnoughGas:
					smi.GoTo(this.online.undermassgas);
					return;
				default:
					return;
				}
			}, UpdateRate.SIM_4000ms, false);
			this.online.heating.Enter(new StateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State.Callback(SpaceHeater.RefreshHeatEffect)).Enter(delegate(SpaceHeater.StatesInstance smi)
			{
				smi.master.operational.SetActive(true, false);
			}).ToggleStatusItem((SpaceHeater.StatesInstance smi) => smi.master.heatStatusItem, (SpaceHeater.StatesInstance smi) => smi)
				.Update(new Action<SpaceHeater.StatesInstance, float>(SpaceHeater.GenerateHeat), UpdateRate.SIM_200ms, false)
				.Exit(delegate(SpaceHeater.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
				})
				.Exit(new StateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State.Callback(SpaceHeater.RefreshHeatEffect));
			this.online.undermassliquid.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemUnderMassLiquid, null);
			this.online.undermassgas.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemUnderMassGas, null);
			this.online.overtemp.ToggleCategoryStatusItem(Db.Get().StatusItemCategories.Heat, this.statusItemOverTemp, null);
		}

		// Token: 0x04007468 RID: 29800
		public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State offline;

		// Token: 0x04007469 RID: 29801
		public SpaceHeater.States.OnlineStates online;

		// Token: 0x0400746A RID: 29802
		private StatusItem statusItemUnderMassLiquid;

		// Token: 0x0400746B RID: 29803
		private StatusItem statusItemUnderMassGas;

		// Token: 0x0400746C RID: 29804
		private StatusItem statusItemOverTemp;

		// Token: 0x020027DE RID: 10206
		public class OnlineStates : GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State
		{
			// Token: 0x0400B0C1 RID: 45249
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State heating;

			// Token: 0x0400B0C2 RID: 45250
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State overtemp;

			// Token: 0x0400B0C3 RID: 45251
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State undermassliquid;

			// Token: 0x0400B0C4 RID: 45252
			public GameStateMachine<SpaceHeater.States, SpaceHeater.StatesInstance, SpaceHeater, object>.State undermassgas;
		}
	}

	// Token: 0x02001709 RID: 5897
	private enum MonitorState
	{
		// Token: 0x0400746E RID: 29806
		ReadyToHeat,
		// Token: 0x0400746F RID: 29807
		TooHot,
		// Token: 0x04007470 RID: 29808
		NotEnoughLiquid,
		// Token: 0x04007471 RID: 29809
		NotEnoughGas
	}
}
