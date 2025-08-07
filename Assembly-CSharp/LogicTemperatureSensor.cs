using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000775 RID: 1909
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicTemperatureSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x17000301 RID: 769
	// (get) Token: 0x060031F7 RID: 12791 RVA: 0x0011B07C File Offset: 0x0011927C
	public float StructureTemperature
	{
		get
		{
			return GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
	}

	// Token: 0x060031F8 RID: 12792 RVA: 0x0011B0A1 File Offset: 0x001192A1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicTemperatureSensor>(-905833192, LogicTemperatureSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060031F9 RID: 12793 RVA: 0x0011B0BC File Offset: 0x001192BC
	private void OnCopySettings(object data)
	{
		LogicTemperatureSensor component = ((GameObject)data).GetComponent<LogicTemperatureSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x060031FA RID: 12794 RVA: 0x0011B0F8 File Offset: 0x001192F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x060031FB RID: 12795 RVA: 0x0011B14C File Offset: 0x0011934C
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8 && !this.dirty)
		{
			int num = Grid.PosToCell(this);
			if (Grid.Mass[num] > 0f)
			{
				this.temperatures[this.simUpdateCounter] = Grid.Temperature[num];
				this.simUpdateCounter++;
			}
			return;
		}
		this.simUpdateCounter = 0;
		this.dirty = false;
		this.averageTemp = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageTemp += this.temperatures[i];
		}
		this.averageTemp /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageTemp > this.thresholdTemperature && !base.IsSwitchedOn) || (this.averageTemp <= this.thresholdTemperature && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageTemp >= this.thresholdTemperature && base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x060031FC RID: 12796 RVA: 0x0011B263 File Offset: 0x00119463
	public float GetTemperature()
	{
		return this.averageTemp;
	}

	// Token: 0x060031FD RID: 12797 RVA: 0x0011B26B File Offset: 0x0011946B
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x060031FE RID: 12798 RVA: 0x0011B27A File Offset: 0x0011947A
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060031FF RID: 12799 RVA: 0x0011B298 File Offset: 0x00119498
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06003200 RID: 12800 RVA: 0x0011B320 File Offset: 0x00119520
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06003201 RID: 12801 RVA: 0x0011B373 File Offset: 0x00119573
	// (set) Token: 0x06003202 RID: 12802 RVA: 0x0011B37B File Offset: 0x0011957B
	public float Threshold
	{
		get
		{
			return this.thresholdTemperature;
		}
		set
		{
			this.thresholdTemperature = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06003203 RID: 12803 RVA: 0x0011B38B File Offset: 0x0011958B
	// (set) Token: 0x06003204 RID: 12804 RVA: 0x0011B393 File Offset: 0x00119593
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnWarmerThan;
		}
		set
		{
			this.activateOnWarmerThan = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06003205 RID: 12805 RVA: 0x0011B3A3 File Offset: 0x001195A3
	public float CurrentValue
	{
		get
		{
			return this.GetTemperature();
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06003206 RID: 12806 RVA: 0x0011B3AB File Offset: 0x001195AB
	public float RangeMin
	{
		get
		{
			return this.minTemp;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06003207 RID: 12807 RVA: 0x0011B3B3 File Offset: 0x001195B3
	public float RangeMax
	{
		get
		{
			return this.maxTemp;
		}
	}

	// Token: 0x06003208 RID: 12808 RVA: 0x0011B3BB File Offset: 0x001195BB
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06003209 RID: 12809 RVA: 0x0011B3C9 File Offset: 0x001195C9
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x0600320A RID: 12810 RVA: 0x0011B3D7 File Offset: 0x001195D7
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x0600320B RID: 12811 RVA: 0x0011B3DE File Offset: 0x001195DE
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x0600320C RID: 12812 RVA: 0x0011B3E5 File Offset: 0x001195E5
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x0600320D RID: 12813 RVA: 0x0011B3F1 File Offset: 0x001195F1
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x0600320E RID: 12814 RVA: 0x0011B3FD File Offset: 0x001195FD
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, true);
	}

	// Token: 0x0600320F RID: 12815 RVA: 0x0011B409 File Offset: 0x00119609
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003210 RID: 12816 RVA: 0x0011B411 File Offset: 0x00119611
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x0011B41C File Offset: 0x0011961C
	public LocString ThresholdValueUnits()
	{
		LocString locString = null;
		switch (GameUtil.temperatureUnit)
		{
		case GameUtil.TemperatureUnit.Celsius:
			locString = UI.UNITSUFFIXES.TEMPERATURE.CELSIUS;
			break;
		case GameUtil.TemperatureUnit.Fahrenheit:
			locString = UI.UNITSUFFIXES.TEMPERATURE.FAHRENHEIT;
			break;
		case GameUtil.TemperatureUnit.Kelvin:
			locString = UI.UNITSUFFIXES.TEMPERATURE.KELVIN;
			break;
		}
		return locString;
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x06003212 RID: 12818 RVA: 0x0011B45C File Offset: 0x0011965C
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06003213 RID: 12819 RVA: 0x0011B45F File Offset: 0x0011965F
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06003214 RID: 12820 RVA: 0x0011B464 File Offset: 0x00119664
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(25f, 260f),
				new NonLinearSlider.Range(50f, 400f),
				new NonLinearSlider.Range(12f, 1500f),
				new NonLinearSlider.Range(13f, 10000f)
			};
		}
	}

	// Token: 0x04001E07 RID: 7687
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x04001E08 RID: 7688
	private int simUpdateCounter;

	// Token: 0x04001E09 RID: 7689
	[Serialize]
	public float thresholdTemperature = 280f;

	// Token: 0x04001E0A RID: 7690
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x04001E0B RID: 7691
	[Serialize]
	private bool dirty = true;

	// Token: 0x04001E0C RID: 7692
	public float minTemp;

	// Token: 0x04001E0D RID: 7693
	public float maxTemp = 373.15f;

	// Token: 0x04001E0E RID: 7694
	private const int NumFrameDelay = 8;

	// Token: 0x04001E0F RID: 7695
	private float[] temperatures = new float[8];

	// Token: 0x04001E10 RID: 7696
	private float averageTemp;

	// Token: 0x04001E11 RID: 7697
	private bool wasOn;

	// Token: 0x04001E12 RID: 7698
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001E13 RID: 7699
	private static readonly EventSystem.IntraObjectHandler<LogicTemperatureSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicTemperatureSensor>(delegate(LogicTemperatureSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
