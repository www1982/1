using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000704 RID: 1796
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitTemperatureSensor : ConduitThresholdSensor, IThresholdSwitch
{
	// Token: 0x06002D00 RID: 11520 RVA: 0x00102778 File Offset: 0x00100978
	private void GetContentsTemperature(out float temperature, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			temperature = contents.temperature;
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		if (pickupable != null && pickupable.PrimaryElement.Mass > 0f)
		{
			temperature = pickupable.PrimaryElement.Temperature;
			hasMass = true;
			return;
		}
		temperature = 0f;
		hasMass = false;
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x06002D01 RID: 11521 RVA: 0x00102818 File Offset: 0x00100A18
	public override float CurrentValue
	{
		get
		{
			float num;
			bool flag;
			this.GetContentsTemperature(out num, out flag);
			if (flag)
			{
				this.lastValue = num;
			}
			return this.lastValue;
		}
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0010283F File Offset: 0x00100A3F
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x17000263 RID: 611
	// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00102847 File Offset: 0x00100A47
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x06002D04 RID: 11524 RVA: 0x0010284F File Offset: 0x00100A4F
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x06002D05 RID: 11525 RVA: 0x0010285D File Offset: 0x00100A5D
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x17000264 RID: 612
	// (get) Token: 0x06002D06 RID: 11526 RVA: 0x0010286B File Offset: 0x00100A6B
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.TEMPERATURESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00102872 File Offset: 0x00100A72
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.CONTENT_TEMPERATURE;
		}
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x06002D08 RID: 11528 RVA: 0x00102879 File Offset: 0x00100A79
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.CONTENT_TEMPERATURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06002D09 RID: 11529 RVA: 0x00102885 File Offset: 0x00100A85
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.CONTENT_TEMPERATURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002D0A RID: 11530 RVA: 0x00102891 File Offset: 0x00100A91
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedTemperature(value, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, units, false);
	}

	// Token: 0x06002D0B RID: 11531 RVA: 0x0010289D File Offset: 0x00100A9D
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06002D0C RID: 11532 RVA: 0x001028A5 File Offset: 0x00100AA5
	public float ProcessedInputValue(float input)
	{
		return GameUtil.GetTemperatureConvertedToKelvin(input);
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x001028B0 File Offset: 0x00100AB0
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

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06002D0E RID: 11534 RVA: 0x001028F0 File Offset: 0x00100AF0
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06002D0F RID: 11535 RVA: 0x001028F3 File Offset: 0x00100AF3
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06002D10 RID: 11536 RVA: 0x001028F8 File Offset: 0x00100AF8
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

	// Token: 0x04001A84 RID: 6788
	public float rangeMin;

	// Token: 0x04001A85 RID: 6789
	public float rangeMax = 373.15f;

	// Token: 0x04001A86 RID: 6790
	[Serialize]
	private float lastValue;
}
