using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200076F RID: 1903
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicPressureSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600317B RID: 12667 RVA: 0x00119B7F File Offset: 0x00117D7F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicPressureSensor>(-905833192, LogicPressureSensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600317C RID: 12668 RVA: 0x00119B98 File Offset: 0x00117D98
	private void OnCopySettings(object data)
	{
		LogicPressureSensor component = ((GameObject)data).GetComponent<LogicPressureSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x0600317D RID: 12669 RVA: 0x00119BD2 File Offset: 0x00117DD2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x0600317E RID: 12670 RVA: 0x00119C08 File Offset: 0x00117E08
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

	// Token: 0x0600317F RID: 12671 RVA: 0x00119CD0 File Offset: 0x00117ED0
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06003180 RID: 12672 RVA: 0x00119CDF File Offset: 0x00117EDF
	// (set) Token: 0x06003181 RID: 12673 RVA: 0x00119CE7 File Offset: 0x00117EE7
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

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x06003182 RID: 12674 RVA: 0x00119CF0 File Offset: 0x00117EF0
	// (set) Token: 0x06003183 RID: 12675 RVA: 0x00119CF8 File Offset: 0x00117EF8
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

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x06003184 RID: 12676 RVA: 0x00119D04 File Offset: 0x00117F04
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

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x06003185 RID: 12677 RVA: 0x00119D35 File Offset: 0x00117F35
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x170002E7 RID: 743
	// (get) Token: 0x06003186 RID: 12678 RVA: 0x00119D3D File Offset: 0x00117F3D
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x06003187 RID: 12679 RVA: 0x00119D45 File Offset: 0x00117F45
	public float GetRangeMinInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMin;
		}
		return this.rangeMin * 1000f;
	}

	// Token: 0x06003188 RID: 12680 RVA: 0x00119D63 File Offset: 0x00117F63
	public float GetRangeMaxInputField()
	{
		if (this.desiredState != Element.State.Gas)
		{
			return this.rangeMax;
		}
		return this.rangeMax * 1000f;
	}

	// Token: 0x170002E8 RID: 744
	// (get) Token: 0x06003189 RID: 12681 RVA: 0x00119D81 File Offset: 0x00117F81
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x170002E9 RID: 745
	// (get) Token: 0x0600318A RID: 12682 RVA: 0x00119D88 File Offset: 0x00117F88
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002EA RID: 746
	// (get) Token: 0x0600318B RID: 12683 RVA: 0x00119D94 File Offset: 0x00117F94
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x0600318C RID: 12684 RVA: 0x00119DA0 File Offset: 0x00117FA0
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

	// Token: 0x0600318D RID: 12685 RVA: 0x00119DCC File Offset: 0x00117FCC
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

	// Token: 0x0600318E RID: 12686 RVA: 0x00119DF6 File Offset: 0x00117FF6
	public float ProcessedInputValue(float input)
	{
		if (this.desiredState == Element.State.Gas)
		{
			input /= 1000f;
		}
		return input;
	}

	// Token: 0x0600318F RID: 12687 RVA: 0x00119E0B File Offset: 0x0011800B
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(this.desiredState == Element.State.Gas);
	}

	// Token: 0x170002EB RID: 747
	// (get) Token: 0x06003190 RID: 12688 RVA: 0x00119E1B File Offset: 0x0011801B
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002EC RID: 748
	// (get) Token: 0x06003191 RID: 12689 RVA: 0x00119E22 File Offset: 0x00118022
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002ED RID: 749
	// (get) Token: 0x06003192 RID: 12690 RVA: 0x00119E25 File Offset: 0x00118025
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002EE RID: 750
	// (get) Token: 0x06003193 RID: 12691 RVA: 0x00119E28 File Offset: 0x00118028
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x06003194 RID: 12692 RVA: 0x00119E35 File Offset: 0x00118035
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06003195 RID: 12693 RVA: 0x00119E54 File Offset: 0x00118054
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

	// Token: 0x06003196 RID: 12694 RVA: 0x00119EDC File Offset: 0x001180DC
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001DC8 RID: 7624
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001DC9 RID: 7625
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001DCA RID: 7626
	private bool wasOn;

	// Token: 0x04001DCB RID: 7627
	public float rangeMin;

	// Token: 0x04001DCC RID: 7628
	public float rangeMax = 1f;

	// Token: 0x04001DCD RID: 7629
	public Element.State desiredState = Element.State.Gas;

	// Token: 0x04001DCE RID: 7630
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001DCF RID: 7631
	private float[] samples = new float[8];

	// Token: 0x04001DD0 RID: 7632
	private int sampleIdx;

	// Token: 0x04001DD1 RID: 7633
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DD2 RID: 7634
	private static readonly EventSystem.IntraObjectHandler<LogicPressureSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicPressureSensor>(delegate(LogicPressureSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
