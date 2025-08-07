using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006D4 RID: 1748
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/AirConditioner")]
public class AirConditioner : KMonoBehaviour, ISaveLoadable, IGameObjectEffectDescriptor, ISim200ms
{
	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06002B1A RID: 11034 RVA: 0x000F90F4 File Offset: 0x000F72F4
	// (set) Token: 0x06002B1B RID: 11035 RVA: 0x000F90FC File Offset: 0x000F72FC
	public float lastEnvTemp { get; private set; }

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06002B1C RID: 11036 RVA: 0x000F9105 File Offset: 0x000F7305
	// (set) Token: 0x06002B1D RID: 11037 RVA: 0x000F910D File Offset: 0x000F730D
	public float lastGasTemp { get; private set; }

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06002B1E RID: 11038 RVA: 0x000F9116 File Offset: 0x000F7316
	public float TargetTemperature
	{
		get
		{
			return this.targetTemperature;
		}
	}

	// Token: 0x06002B1F RID: 11039 RVA: 0x000F911E File Offset: 0x000F731E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<AirConditioner>(-592767678, AirConditioner.OnOperationalChangedDelegate);
		base.Subscribe<AirConditioner>(824508782, AirConditioner.OnActiveChangedDelegate);
	}

	// Token: 0x06002B20 RID: 11040 RVA: 0x000F9148 File Offset: 0x000F7348
	protected override void OnSpawn()
	{
		base.OnSpawn();
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		base.gameObject.AddOrGet<EntityCellVisualizer>().AddPort(EntityCellVisualizer.Ports.HeatSource, default(CellOffset));
		this.cooledAirOutputCell = this.building.GetUtilityOutputCell();
	}

	// Token: 0x06002B21 RID: 11041 RVA: 0x000F91D6 File Offset: 0x000F73D6
	public void Sim200ms(float dt)
	{
		if (this.operational != null && !this.operational.IsOperational)
		{
			this.operational.SetActive(false, false);
			return;
		}
		this.UpdateState(dt);
	}

	// Token: 0x06002B22 RID: 11042 RVA: 0x000F9208 File Offset: 0x000F7408
	private static bool UpdateStateCb(int cell, object data)
	{
		AirConditioner airConditioner = data as AirConditioner;
		airConditioner.cellCount++;
		airConditioner.envTemp += Grid.Temperature[cell];
		return true;
	}

	// Token: 0x06002B23 RID: 11043 RVA: 0x000F9238 File Offset: 0x000F7438
	private void UpdateState(float dt)
	{
		bool flag = this.consumer.IsSatisfied;
		this.envTemp = 0f;
		this.cellCount = 0;
		if (this.occupyArea != null && base.gameObject != null)
		{
			this.occupyArea.TestArea(Grid.PosToCell(base.gameObject), this, AirConditioner.UpdateStateCbDelegate);
			this.envTemp /= (float)this.cellCount;
		}
		this.lastEnvTemp = this.envTemp;
		List<GameObject> items = this.storage.items;
		for (int i = 0; i < items.Count; i++)
		{
			PrimaryElement component = items[i].GetComponent<PrimaryElement>();
			if (component.Mass > 0f && (!this.isLiquidConditioner || !component.Element.IsGas) && (this.isLiquidConditioner || !component.Element.IsLiquid))
			{
				flag = true;
				this.lastGasTemp = component.Temperature;
				float num = component.Temperature + this.temperatureDelta;
				if (num < 1f)
				{
					num = 1f;
					this.lowTempLag = Mathf.Min(this.lowTempLag + dt / 5f, 1f);
				}
				else
				{
					this.lowTempLag = Mathf.Min(this.lowTempLag - dt / 5f, 0f);
				}
				float num2 = (this.isLiquidConditioner ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow).AddElement(this.cooledAirOutputCell, component.ElementID, component.Mass, num, component.DiseaseIdx, component.DiseaseCount);
				component.KeepZeroMassObject = true;
				float num3 = num2 / component.Mass;
				int num4 = (int)((float)component.DiseaseCount * num3);
				component.Mass -= num2;
				component.ModifyDiseaseCount(-num4, "AirConditioner.UpdateState");
				float num5 = (num - component.Temperature) * component.Element.specificHeatCapacity * num2;
				float num6 = ((this.lastSampleTime > 0f) ? (Time.time - this.lastSampleTime) : 1f);
				this.lastSampleTime = Time.time;
				this.heatEffect.SetHeatBeingProducedValue(Mathf.Abs(num5));
				GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, -num5, BUILDING.STATUSITEMS.OPERATINGENERGY.PIPECONTENTS_TRANSFER, num6);
				break;
			}
		}
		if (Time.time - this.lastSampleTime > 2f)
		{
			GameComps.StructureTemperatures.ProduceEnergy(this.structureTemperature, 0f, BUILDING.STATUSITEMS.OPERATINGENERGY.PIPECONTENTS_TRANSFER, Time.time - this.lastSampleTime);
			this.lastSampleTime = Time.time;
		}
		this.operational.SetActive(flag, false);
		this.UpdateStatus();
	}

	// Token: 0x06002B24 RID: 11044 RVA: 0x000F94EE File Offset: 0x000F76EE
	private void OnOperationalChanged(object data)
	{
		if (this.operational.IsOperational)
		{
			this.UpdateState(0f);
		}
	}

	// Token: 0x06002B25 RID: 11045 RVA: 0x000F9508 File Offset: 0x000F7708
	private void OnActiveChanged(object data)
	{
		this.UpdateStatus();
		if (this.operational.IsActive)
		{
			this.heatEffect.enabled = true;
			return;
		}
		this.heatEffect.enabled = false;
	}

	// Token: 0x06002B26 RID: 11046 RVA: 0x000F9538 File Offset: 0x000F7738
	private void UpdateStatus()
	{
		if (this.operational.IsActive)
		{
			if (this.lowTempLag >= 1f && !this.showingLowTemp)
			{
				this.statusHandle = (this.isLiquidConditioner ? this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CoolingStalledColdLiquid, this) : this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.CoolingStalledColdGas, this));
				this.showingLowTemp = true;
				this.showingHotEnv = false;
				return;
			}
			if (this.lowTempLag <= 0f && (this.showingHotEnv || this.showingLowTemp))
			{
				this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Cooling, null);
				this.showingLowTemp = false;
				this.showingHotEnv = false;
				return;
			}
			if (this.statusHandle == Guid.Empty)
			{
				this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Cooling, null);
				this.showingLowTemp = false;
				this.showingHotEnv = false;
				return;
			}
		}
		else
		{
			this.statusHandle = this.selectable.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
		}
	}

	// Token: 0x06002B27 RID: 11047 RVA: 0x000F96AC File Offset: 0x000F78AC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		string formattedTemperature = GameUtil.GetFormattedTemperature(this.temperatureDelta, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false);
		Element element = ElementLoader.FindElementByName(this.isLiquidConditioner ? "Water" : "Oxygen");
		float num;
		if (this.isLiquidConditioner)
		{
			num = Mathf.Abs(this.temperatureDelta * element.specificHeatCapacity * 10000f);
		}
		else
		{
			num = Mathf.Abs(this.temperatureDelta * element.specificHeatCapacity * 1000f);
		}
		float num2 = num * 1f;
		Descriptor descriptor = default(Descriptor);
		string text = string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.HEATGENERATED_LIQUIDCONDITIONER : UI.BUILDINGEFFECTS.HEATGENERATED_AIRCONDITIONER, GameUtil.GetFormattedHeatEnergy(num2, GameUtil.HeatEnergyFormatterUnit.Automatic), GameUtil.GetFormattedTemperature(Mathf.Abs(this.temperatureDelta), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false));
		string text2 = string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED_LIQUIDCONDITIONER : UI.BUILDINGEFFECTS.TOOLTIPS.HEATGENERATED_AIRCONDITIONER, GameUtil.GetFormattedHeatEnergy(num2, GameUtil.HeatEnergyFormatterUnit.Automatic), GameUtil.GetFormattedTemperature(Mathf.Abs(this.temperatureDelta), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Relative, true, false));
		descriptor.SetupDescriptor(text, text2, Descriptor.DescriptorType.Effect);
		list.Add(descriptor);
		Descriptor descriptor2 = default(Descriptor);
		descriptor2.SetupDescriptor(string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.LIQUIDCOOLING : UI.BUILDINGEFFECTS.GASCOOLING, formattedTemperature), string.Format(this.isLiquidConditioner ? UI.BUILDINGEFFECTS.TOOLTIPS.LIQUIDCOOLING : UI.BUILDINGEFFECTS.TOOLTIPS.GASCOOLING, formattedTemperature), Descriptor.DescriptorType.Effect);
		list.Add(descriptor2);
		return list;
	}

	// Token: 0x04001963 RID: 6499
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04001964 RID: 6500
	[MyCmpReq]
	protected Storage storage;

	// Token: 0x04001965 RID: 6501
	[MyCmpReq]
	protected Operational operational;

	// Token: 0x04001966 RID: 6502
	[MyCmpReq]
	private ConduitConsumer consumer;

	// Token: 0x04001967 RID: 6503
	[MyCmpReq]
	private BuildingComplete building;

	// Token: 0x04001968 RID: 6504
	[MyCmpGet]
	private OccupyArea occupyArea;

	// Token: 0x04001969 RID: 6505
	[MyCmpGet]
	private KBatchedAnimHeatPostProcessingEffect heatEffect;

	// Token: 0x0400196A RID: 6506
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x0400196B RID: 6507
	public float temperatureDelta = -14f;

	// Token: 0x0400196C RID: 6508
	public float maxEnvironmentDelta = -50f;

	// Token: 0x0400196D RID: 6509
	private float lowTempLag;

	// Token: 0x0400196E RID: 6510
	private bool showingLowTemp;

	// Token: 0x0400196F RID: 6511
	public bool isLiquidConditioner;

	// Token: 0x04001970 RID: 6512
	private bool showingHotEnv;

	// Token: 0x04001973 RID: 6515
	private Guid statusHandle;

	// Token: 0x04001974 RID: 6516
	[Serialize]
	private float targetTemperature;

	// Token: 0x04001975 RID: 6517
	private int cooledAirOutputCell = -1;

	// Token: 0x04001976 RID: 6518
	private static readonly EventSystem.IntraObjectHandler<AirConditioner> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<AirConditioner>(delegate(AirConditioner component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x04001977 RID: 6519
	private static readonly EventSystem.IntraObjectHandler<AirConditioner> OnActiveChangedDelegate = new EventSystem.IntraObjectHandler<AirConditioner>(delegate(AirConditioner component, object data)
	{
		component.OnActiveChanged(data);
	});

	// Token: 0x04001978 RID: 6520
	private float lastSampleTime = -1f;

	// Token: 0x04001979 RID: 6521
	private float envTemp;

	// Token: 0x0400197A RID: 6522
	private int cellCount;

	// Token: 0x0400197B RID: 6523
	private static readonly Func<int, object, bool> UpdateStateCbDelegate = (int cell, object data) => AirConditioner.UpdateStateCb(cell, data);
}
