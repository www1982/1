using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000770 RID: 1904
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicRadiationSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x06003199 RID: 12697 RVA: 0x00119F78 File Offset: 0x00118178
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicRadiationSensor>(-905833192, LogicRadiationSensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600319A RID: 12698 RVA: 0x00119F94 File Offset: 0x00118194
	private void OnCopySettings(object data)
	{
		LogicRadiationSensor component = ((GameObject)data).GetComponent<LogicRadiationSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x0600319B RID: 12699 RVA: 0x00119FCE File Offset: 0x001181CE
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x0600319C RID: 12700 RVA: 0x0011A004 File Offset: 0x00118204
	public void Sim200ms(float dt)
	{
		if (this.simUpdateCounter < 8 && !this.dirty)
		{
			int num = Grid.PosToCell(this);
			this.radHistory[this.simUpdateCounter] = Grid.Radiation[num];
			this.simUpdateCounter++;
			return;
		}
		this.simUpdateCounter = 0;
		this.dirty = false;
		this.averageRads = 0f;
		for (int i = 0; i < 8; i++)
		{
			this.averageRads += this.radHistory[i];
		}
		this.averageRads /= 8f;
		if (this.activateOnWarmerThan)
		{
			if ((this.averageRads > this.thresholdRads && !base.IsSwitchedOn) || (this.averageRads <= this.thresholdRads && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.averageRads >= this.thresholdRads && base.IsSwitchedOn) || (this.averageRads < this.thresholdRads && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x0600319D RID: 12701 RVA: 0x0011A109 File Offset: 0x00118309
	public float GetAverageRads()
	{
		return this.averageRads;
	}

	// Token: 0x0600319E RID: 12702 RVA: 0x0011A111 File Offset: 0x00118311
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x0600319F RID: 12703 RVA: 0x0011A120 File Offset: 0x00118320
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060031A0 RID: 12704 RVA: 0x0011A140 File Offset: 0x00118340
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

	// Token: 0x060031A1 RID: 12705 RVA: 0x0011A1C8 File Offset: 0x001183C8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x170002EF RID: 751
	// (get) Token: 0x060031A2 RID: 12706 RVA: 0x0011A21B File Offset: 0x0011841B
	// (set) Token: 0x060031A3 RID: 12707 RVA: 0x0011A223 File Offset: 0x00118423
	public float Threshold
	{
		get
		{
			return this.thresholdRads;
		}
		set
		{
			this.thresholdRads = value;
			this.dirty = true;
		}
	}

	// Token: 0x170002F0 RID: 752
	// (get) Token: 0x060031A4 RID: 12708 RVA: 0x0011A233 File Offset: 0x00118433
	// (set) Token: 0x060031A5 RID: 12709 RVA: 0x0011A23B File Offset: 0x0011843B
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

	// Token: 0x170002F1 RID: 753
	// (get) Token: 0x060031A6 RID: 12710 RVA: 0x0011A24B File Offset: 0x0011844B
	public float CurrentValue
	{
		get
		{
			return this.GetAverageRads();
		}
	}

	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x060031A7 RID: 12711 RVA: 0x0011A253 File Offset: 0x00118453
	public float RangeMin
	{
		get
		{
			return this.minRads;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x060031A8 RID: 12712 RVA: 0x0011A25B File Offset: 0x0011845B
	public float RangeMax
	{
		get
		{
			return this.maxRads;
		}
	}

	// Token: 0x060031A9 RID: 12713 RVA: 0x0011A263 File Offset: 0x00118463
	public float GetRangeMinInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMin, false);
	}

	// Token: 0x060031AA RID: 12714 RVA: 0x0011A271 File Offset: 0x00118471
	public float GetRangeMaxInputField()
	{
		return GameUtil.GetConvertedTemperature(this.RangeMax, false);
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x060031AB RID: 12715 RVA: 0x0011A27F File Offset: 0x0011847F
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.RADIATIONSWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x060031AC RID: 12716 RVA: 0x0011A286 File Offset: 0x00118486
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION;
		}
	}

	// Token: 0x170002F6 RID: 758
	// (get) Token: 0x060031AD RID: 12717 RVA: 0x0011A28D File Offset: 0x0011848D
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002F7 RID: 759
	// (get) Token: 0x060031AE RID: 12718 RVA: 0x0011A299 File Offset: 0x00118499
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.RADIATION_TOOLTIP_BELOW;
		}
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x0011A2A5 File Offset: 0x001184A5
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedRads(value, GameUtil.TimeSlice.None);
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x0011A2AE File Offset: 0x001184AE
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x0011A2B6 File Offset: 0x001184B6
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x060031B2 RID: 12722 RVA: 0x0011A2B9 File Offset: 0x001184B9
	public LocString ThresholdValueUnits()
	{
		return "";
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x060031B3 RID: 12723 RVA: 0x0011A2C5 File Offset: 0x001184C5
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x060031B4 RID: 12724 RVA: 0x0011A2C8 File Offset: 0x001184C8
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x060031B5 RID: 12725 RVA: 0x0011A2CC File Offset: 0x001184CC
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(50f, 200f),
				new NonLinearSlider.Range(25f, 1000f),
				new NonLinearSlider.Range(25f, 5000f)
			};
		}
	}

	// Token: 0x04001DD3 RID: 7635
	private int simUpdateCounter;

	// Token: 0x04001DD4 RID: 7636
	[Serialize]
	public float thresholdRads = 280f;

	// Token: 0x04001DD5 RID: 7637
	[Serialize]
	public bool activateOnWarmerThan;

	// Token: 0x04001DD6 RID: 7638
	[Serialize]
	private bool dirty = true;

	// Token: 0x04001DD7 RID: 7639
	public float minRads;

	// Token: 0x04001DD8 RID: 7640
	public float maxRads = 5000f;

	// Token: 0x04001DD9 RID: 7641
	private const int NumFrameDelay = 8;

	// Token: 0x04001DDA RID: 7642
	private float[] radHistory = new float[8];

	// Token: 0x04001DDB RID: 7643
	private float averageRads;

	// Token: 0x04001DDC RID: 7644
	private bool wasOn;

	// Token: 0x04001DDD RID: 7645
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DDE RID: 7646
	private static readonly EventSystem.IntraObjectHandler<LogicRadiationSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicRadiationSensor>(delegate(LogicRadiationSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
