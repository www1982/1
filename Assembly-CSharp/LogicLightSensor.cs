using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200076B RID: 1899
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicLightSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600312F RID: 12591 RVA: 0x00118DA4 File Offset: 0x00116FA4
	private void OnCopySettings(object data)
	{
		LogicLightSensor component = ((GameObject)data).GetComponent<LogicLightSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06003130 RID: 12592 RVA: 0x00118DDE File Offset: 0x00116FDE
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicLightSensor>(-905833192, LogicLightSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06003131 RID: 12593 RVA: 0x00118DF7 File Offset: 0x00116FF7
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06003132 RID: 12594 RVA: 0x00118E2C File Offset: 0x0011702C
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 4)
		{
			this.levels[this.simUpdateCounter] = (float)Grid.LightIntensity[Grid.PosToCell(this)];
			this.simUpdateCounter++;
			return;
		}
		this.simUpdateCounter = 0;
		this.averageBrightness = 0f;
		for (int i = 0; i < 4; i++)
		{
			this.averageBrightness += this.levels[i];
		}
		this.averageBrightness /= 4f;
		if (this.activateOnBrighterThan)
		{
			if ((this.averageBrightness > this.thresholdBrightness && !base.IsSwitchedOn) || (this.averageBrightness < this.thresholdBrightness && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageBrightness > this.thresholdBrightness && base.IsSwitchedOn) || (this.averageBrightness < this.thresholdBrightness && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06003133 RID: 12595 RVA: 0x00118F21 File Offset: 0x00117121
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x06003134 RID: 12596 RVA: 0x00118F30 File Offset: 0x00117130
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06003135 RID: 12597 RVA: 0x00118F50 File Offset: 0x00117150
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

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06003136 RID: 12598 RVA: 0x00118FD7 File Offset: 0x001171D7
	// (set) Token: 0x06003137 RID: 12599 RVA: 0x00118FDF File Offset: 0x001171DF
	public float Threshold
	{
		get
		{
			return this.thresholdBrightness;
		}
		set
		{
			this.thresholdBrightness = value;
		}
	}

	// Token: 0x170002CC RID: 716
	// (get) Token: 0x06003138 RID: 12600 RVA: 0x00118FE8 File Offset: 0x001171E8
	// (set) Token: 0x06003139 RID: 12601 RVA: 0x00118FF0 File Offset: 0x001171F0
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnBrighterThan;
		}
		set
		{
			this.activateOnBrighterThan = value;
		}
	}

	// Token: 0x170002CD RID: 717
	// (get) Token: 0x0600313A RID: 12602 RVA: 0x00118FF9 File Offset: 0x001171F9
	public float CurrentValue
	{
		get
		{
			return this.averageBrightness;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x0600313B RID: 12603 RVA: 0x00119001 File Offset: 0x00117201
	public float RangeMin
	{
		get
		{
			return this.minBrightness;
		}
	}

	// Token: 0x170002CF RID: 719
	// (get) Token: 0x0600313C RID: 12604 RVA: 0x00119009 File Offset: 0x00117209
	public float RangeMax
	{
		get
		{
			return this.maxBrightness;
		}
	}

	// Token: 0x0600313D RID: 12605 RVA: 0x00119011 File Offset: 0x00117211
	public float GetRangeMinInputField()
	{
		return this.RangeMin;
	}

	// Token: 0x0600313E RID: 12606 RVA: 0x00119019 File Offset: 0x00117219
	public float GetRangeMaxInputField()
	{
		return this.RangeMax;
	}

	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x0600313F RID: 12607 RVA: 0x00119021 File Offset: 0x00117221
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.BRIGHTNESSSWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06003140 RID: 12608 RVA: 0x00119028 File Offset: 0x00117228
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.BRIGHTNESS;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06003141 RID: 12609 RVA: 0x0011902F File Offset: 0x0011722F
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.BRIGHTNESS_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06003142 RID: 12610 RVA: 0x0011903B File Offset: 0x0011723B
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.BRIGHTNESS_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003143 RID: 12611 RVA: 0x00119047 File Offset: 0x00117247
	public string Format(float value, bool units)
	{
		if (units)
		{
			return GameUtil.GetFormattedLux((int)value);
		}
		return string.Format("{0}", (int)value);
	}

	// Token: 0x06003144 RID: 12612 RVA: 0x00119065 File Offset: 0x00117265
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003145 RID: 12613 RVA: 0x0011906D File Offset: 0x0011726D
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06003146 RID: 12614 RVA: 0x00119070 File Offset: 0x00117270
	public LocString ThresholdValueUnits()
	{
		return UI.UNITSUFFIXES.LIGHT.LUX;
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06003147 RID: 12615 RVA: 0x00119077 File Offset: 0x00117277
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06003148 RID: 12616 RVA: 0x0011907A File Offset: 0x0011727A
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06003149 RID: 12617 RVA: 0x0011907D File Offset: 0x0011727D
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x0600314A RID: 12618 RVA: 0x0011908C File Offset: 0x0011728C
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001D9F RID: 7583
	private int simUpdateCounter;

	// Token: 0x04001DA0 RID: 7584
	[Serialize]
	public float thresholdBrightness = 280f;

	// Token: 0x04001DA1 RID: 7585
	[Serialize]
	public bool activateOnBrighterThan = true;

	// Token: 0x04001DA2 RID: 7586
	public float minBrightness;

	// Token: 0x04001DA3 RID: 7587
	public float maxBrightness = 15000f;

	// Token: 0x04001DA4 RID: 7588
	private const int NumFrameDelay = 4;

	// Token: 0x04001DA5 RID: 7589
	private float[] levels = new float[4];

	// Token: 0x04001DA6 RID: 7590
	private float averageBrightness;

	// Token: 0x04001DA7 RID: 7591
	private bool wasOn;

	// Token: 0x04001DA8 RID: 7592
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DA9 RID: 7593
	private static readonly EventSystem.IntraObjectHandler<LogicLightSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicLightSensor>(delegate(LogicLightSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
