using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007AC RID: 1964
[SerializationConfig(MemberSerialization.OptIn)]
public class PressureSwitch : CircuitSwitch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600340A RID: 13322 RVA: 0x001240AC File Offset: 0x001222AC
	public void Sim200ms(float dt)
	{
		int num = Grid.PosToCell(this);
		if (this.sampleIdx < 8)
		{
			float num2 = (Grid.Element[num].IsState(this.desiredState) ? Grid.Mass[num] : 0f);
			this.samples[this.sampleIdx] = num2;
			this.sampleIdx++;
			return;
		}
		this.sampleIdx = 0;
		float currentValue = this.CurrentValue;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x00124174 File Offset: 0x00122374
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000340 RID: 832
	// (get) Token: 0x0600340C RID: 13324 RVA: 0x001241C7 File Offset: 0x001223C7
	// (set) Token: 0x0600340D RID: 13325 RVA: 0x001241CF File Offset: 0x001223CF
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
		}
	}

	// Token: 0x17000341 RID: 833
	// (get) Token: 0x0600340E RID: 13326 RVA: 0x001241D8 File Offset: 0x001223D8
	// (set) Token: 0x0600340F RID: 13327 RVA: 0x001241E0 File Offset: 0x001223E0
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
		}
	}

	// Token: 0x17000342 RID: 834
	// (get) Token: 0x06003410 RID: 13328 RVA: 0x001241EC File Offset: 0x001223EC
	public float CurrentValue
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				num += this.samples[i];
			}
			return num / 8f;
		}
	}

	// Token: 0x17000343 RID: 835
	// (get) Token: 0x06003411 RID: 13329 RVA: 0x0012421D File Offset: 0x0012241D
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x06003412 RID: 13330 RVA: 0x00124225 File Offset: 0x00122425
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x06003413 RID: 13331 RVA: 0x0012422D File Offset: 0x0012242D
	public float GetRangeMinInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMin;
		}
		return this.rangeMin * 1000f;
	}

	// Token: 0x06003414 RID: 13332 RVA: 0x0012424B File Offset: 0x0012244B
	public float GetRangeMaxInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMax;
		}
		return this.rangeMax * 1000f;
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x06003415 RID: 13333 RVA: 0x00124269 File Offset: 0x00122469
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x06003416 RID: 13334 RVA: 0x00124270 File Offset: 0x00122470
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x06003417 RID: 13335 RVA: 0x00124277 File Offset: 0x00122477
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x06003418 RID: 13336 RVA: 0x00124283 File Offset: 0x00122483
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003419 RID: 13337 RVA: 0x00124290 File Offset: 0x00122490
	public string Format(float value, bool units)
	{
		GameUtil.MetricMassFormat metricMassFormat;
		if (this.desiredState == Element.State.Gas)
		{
			metricMassFormat = GameUtil.MetricMassFormat.Gram;
		}
		else
		{
			metricMassFormat = GameUtil.MetricMassFormat.Kilogram;
		}
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, metricMassFormat, units, "{0:0.#}");
	}

	// Token: 0x0600341A RID: 13338 RVA: 0x001242BC File Offset: 0x001224BC
	public float ProcessedSliderValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input = Mathf.Round(input * 1000f) / 1000f;
		}
		else
		{
			input = Mathf.Round(input);
		}
		return input;
	}

	// Token: 0x0600341B RID: 13339 RVA: 0x001242E6 File Offset: 0x001224E6
	public float ProcessedInputValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input /= 1000f;
		}
		return input;
	}

	// Token: 0x0600341C RID: 13340 RVA: 0x001242FB File Offset: 0x001224FB
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(this.desiredState == Element.State.Gas);
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x0600341D RID: 13341 RVA: 0x0012430B File Offset: 0x0012250B
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x0600341E RID: 13342 RVA: 0x0012430E File Offset: 0x0012250E
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x0600341F RID: 13343 RVA: 0x00124311 File Offset: 0x00122511
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x04001F57 RID: 8023
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001F58 RID: 8024
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001F59 RID: 8025
	public float rangeMin;

	// Token: 0x04001F5A RID: 8026
	public float rangeMax = 1f;

	// Token: 0x04001F5B RID: 8027
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001F5C RID: 8028
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001F5D RID: 8029
	private float[] samples = new float[8];

	// Token: 0x04001F5E RID: 8030
	private int sampleIdx;
}
