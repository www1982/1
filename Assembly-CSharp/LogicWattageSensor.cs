using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000778 RID: 1912
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicWattageSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x0600322C RID: 12844 RVA: 0x0011B9C4 File Offset: 0x00119BC4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicWattageSensor>(-905833192, LogicWattageSensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600322D RID: 12845 RVA: 0x0011B9E0 File Offset: 0x00119BE0
	private void OnCopySettings(object data)
	{
		LogicWattageSensor component = ((GameObject)data).GetComponent<LogicWattageSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x0600322E RID: 12846 RVA: 0x0011BA1A File Offset: 0x00119C1A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateVisualState(true);
		this.UpdateLogicCircuit();
		this.wasOn = this.switchedOn;
	}

	// Token: 0x0600322F RID: 12847 RVA: 0x0011BA50 File Offset: 0x00119C50
	public void Sim200ms(float dt)
	{
		float wattsUsedByCircuit = Game.Instance.circuitManager.GetWattsUsedByCircuit(Game.Instance.circuitManager.GetCircuitID(Grid.PosToCell(this)));
		if (wattsUsedByCircuit < 0f)
		{
			return;
		}
		this.currentWattage = wattsUsedByCircuit;
		if (this.activateOnHigherThan)
		{
			if ((this.currentWattage > this.thresholdWattage && !base.IsSwitchedOn) || (this.currentWattage <= this.thresholdWattage && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((this.currentWattage >= this.thresholdWattage && base.IsSwitchedOn) || (this.currentWattage < this.thresholdWattage && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06003230 RID: 12848 RVA: 0x0011BAFF File Offset: 0x00119CFF
	public float GetWattageUsed()
	{
		return this.currentWattage;
	}

	// Token: 0x06003231 RID: 12849 RVA: 0x0011BB07 File Offset: 0x00119D07
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateVisualState(false);
		this.UpdateLogicCircuit();
	}

	// Token: 0x06003232 RID: 12850 RVA: 0x0011BB16 File Offset: 0x00119D16
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06003233 RID: 12851 RVA: 0x0011BB34 File Offset: 0x00119D34
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

	// Token: 0x06003234 RID: 12852 RVA: 0x0011BBBC File Offset: 0x00119DBC
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06003235 RID: 12853 RVA: 0x0011BC0F File Offset: 0x00119E0F
	// (set) Token: 0x06003236 RID: 12854 RVA: 0x0011BC17 File Offset: 0x00119E17
	public float Threshold
	{
		get
		{
			return this.thresholdWattage;
		}
		set
		{
			this.thresholdWattage = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06003237 RID: 12855 RVA: 0x0011BC27 File Offset: 0x00119E27
	// (set) Token: 0x06003238 RID: 12856 RVA: 0x0011BC2F File Offset: 0x00119E2F
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnHigherThan;
		}
		set
		{
			this.activateOnHigherThan = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06003239 RID: 12857 RVA: 0x0011BC3F File Offset: 0x00119E3F
	public float CurrentValue
	{
		get
		{
			return this.GetWattageUsed();
		}
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x0600323A RID: 12858 RVA: 0x0011BC47 File Offset: 0x00119E47
	public float RangeMin
	{
		get
		{
			return this.minWattage;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x0600323B RID: 12859 RVA: 0x0011BC4F File Offset: 0x00119E4F
	public float RangeMax
	{
		get
		{
			return this.maxWattage;
		}
	}

	// Token: 0x0600323C RID: 12860 RVA: 0x0011BC57 File Offset: 0x00119E57
	public float GetRangeMinInputField()
	{
		return this.minWattage;
	}

	// Token: 0x0600323D RID: 12861 RVA: 0x0011BC5F File Offset: 0x00119E5F
	public float GetRangeMaxInputField()
	{
		return this.maxWattage;
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x0600323E RID: 12862 RVA: 0x0011BC67 File Offset: 0x00119E67
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.WATTAGESWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x0600323F RID: 12863 RVA: 0x0011BC6E File Offset: 0x00119E6E
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE;
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06003240 RID: 12864 RVA: 0x0011BC75 File Offset: 0x00119E75
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06003241 RID: 12865 RVA: 0x0011BC81 File Offset: 0x00119E81
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.WATTAGE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003242 RID: 12866 RVA: 0x0011BC8D File Offset: 0x00119E8D
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedWattage(value, GameUtil.WattageFormatterUnit.Watts, units);
	}

	// Token: 0x06003243 RID: 12867 RVA: 0x0011BC97 File Offset: 0x00119E97
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003244 RID: 12868 RVA: 0x0011BC9F File Offset: 0x00119E9F
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06003245 RID: 12869 RVA: 0x0011BCA2 File Offset: 0x00119EA2
	public LocString ThresholdValueUnits()
	{
		return UI.UNITSUFFIXES.ELECTRICAL.WATT;
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x06003246 RID: 12870 RVA: 0x0011BCA9 File Offset: 0x00119EA9
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x06003247 RID: 12871 RVA: 0x0011BCAC File Offset: 0x00119EAC
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x06003248 RID: 12872 RVA: 0x0011BCB0 File Offset: 0x00119EB0
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(5f, 5f),
				new NonLinearSlider.Range(35f, 1000f),
				new NonLinearSlider.Range(50f, 3000f),
				new NonLinearSlider.Range(10f, this.maxWattage)
			};
		}
	}

	// Token: 0x04001E20 RID: 7712
	[Serialize]
	public float thresholdWattage;

	// Token: 0x04001E21 RID: 7713
	[Serialize]
	public bool activateOnHigherThan;

	// Token: 0x04001E22 RID: 7714
	[Serialize]
	public bool dirty = true;

	// Token: 0x04001E23 RID: 7715
	private readonly float minWattage;

	// Token: 0x04001E24 RID: 7716
	private readonly float maxWattage = 1.5f * Wire.GetMaxWattageAsFloat(Wire.WattageRating.Max50000);

	// Token: 0x04001E25 RID: 7717
	private float currentWattage;

	// Token: 0x04001E26 RID: 7718
	private bool wasOn;

	// Token: 0x04001E27 RID: 7719
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001E28 RID: 7720
	private static readonly EventSystem.IntraObjectHandler<LogicWattageSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicWattageSensor>(delegate(LogicWattageSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
