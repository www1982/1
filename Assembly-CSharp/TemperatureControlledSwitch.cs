using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007E0 RID: 2016
[SerializationConfig(MemberSerialization.OptIn)]
public class TemperatureControlledSwitch : CircuitSwitch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x1700039A RID: 922
	// (get) Token: 0x06003685 RID: 13957 RVA: 0x0012F67C File Offset: 0x0012D87C
	public float StructureTemperature
	{
		get
		{
			return GameComps.StructureTemperatures.GetPayload(this.structureTemperature).Temperature;
		}
	}

	// Token: 0x06003686 RID: 13958 RVA: 0x0012F6A1 File Offset: 0x0012D8A1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.structureTemperature = GameComps.StructureTemperatures.GetHandle(base.gameObject);
	}

	// Token: 0x06003687 RID: 13959 RVA: 0x0012F6C0 File Offset: 0x0012D8C0
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8)
		{
			this.temperatures[this.simUpdateCounter] = Grid.Temperature[Grid.PosToCell(this)];
			this.simUpdateCounter++;
			return;
		}
		this.simUpdateCounter = 0;
		this.averageTemp = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageTemp += this.temperatures[i];
		}
		this.averageTemp /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageTemp > this.thresholdTemperature && !base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageTemp > this.thresholdTemperature && base.IsSwitchedOn) || (this.averageTemp < this.thresholdTemperature && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06003688 RID: 13960 RVA: 0x0012F7B4 File Offset: 0x0012D9B4
	public float GetTemperature()
	{
		return this.averageTemp;
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06003689 RID: 13961 RVA: 0x0012F7BC File Offset: 0x0012D9BC
	// (set) Token: 0x0600368A RID: 13962 RVA: 0x0012F7C4 File Offset: 0x0012D9C4
	public float Threshold
	{
		get
		{
			return this.thresholdTemperature;
		}
		set
		{
			this.thresholdTemperature = value;
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x0600368B RID: 13963 RVA: 0x0012F7CD File Offset: 0x0012D9CD
	// (set) Token: 0x0600368C RID: 13964 RVA: 0x0012F7D5 File Offset: 0x0012D9D5
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnWarmerThan;
		}
		set
		{
			this.activateOnWarmerThan = value;
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x0600368D RID: 13965 RVA: 0x0012F7DE File Offset: 0x0012D9DE
	public float CurrentValue
	{
		get
		{
			return this.GetTemperature();
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x0600368E RID: 13966 RVA: 0x0012F7E6 File Offset: 0x0012D9E6
	public float RangeMin
	{
		get
		{
			return this.minTemp;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x0600368F RID: 13967 RVA: 0x0012F7EE File Offset: 0x0012D9EE
	public float RangeMax
	{
		get
		{
			return this.maxTemp;
		}
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x0012F7F6 File Offset: 0x0012D9F6
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x0012F804 File Offset: 0x0012DA04
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06003692 RID: 13970 RVA: 0x0012F812 File Offset: 0x0012DA12
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06003693 RID: 13971 RVA: 0x0012F819 File Offset: 0x0012DA19
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06003694 RID: 13972 RVA: 0x0012F820 File Offset: 0x0012DA20
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06003695 RID: 13973 RVA: 0x0012F82C File Offset: 0x0012DA2C
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003696 RID: 13974 RVA: 0x0012F838 File Offset: 0x0012DA38
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, false);
	}

	// Token: 0x06003697 RID: 13975 RVA: 0x0012F844 File Offset: 0x0012DA44
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003698 RID: 13976 RVA: 0x0012F84C File Offset: 0x0012DA4C
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x06003699 RID: 13977 RVA: 0x0012F854 File Offset: 0x0012DA54
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

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x0600369A RID: 13978 RVA: 0x0012F894 File Offset: 0x0012DA94
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.InputField;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x0600369B RID: 13979 RVA: 0x0012F897 File Offset: 0x0012DA97
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x0600369C RID: 13980 RVA: 0x0012F89A File Offset: 0x0012DA9A
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x040020E1 RID: 8417
	private HandleVector<int>.Handle structureTemperature;

	// Token: 0x040020E2 RID: 8418
	private int simUpdateCounter;

	// Token: 0x040020E3 RID: 8419
	[Serialize]
	public float thresholdTemperature = 280f;

	// Token: 0x040020E4 RID: 8420
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x040020E5 RID: 8421
	public float minTemp;

	// Token: 0x040020E6 RID: 8422
	public float maxTemp = 373.15f;

	// Token: 0x040020E7 RID: 8423
	private const int NumFrameDelay = 8;

	// Token: 0x040020E8 RID: 8424
	private float[] temperatures = new float[8];

	// Token: 0x040020E9 RID: 8425
	private float averageTemp;
}
