using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000768 RID: 1896
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGateFilter : LogicGate, ISingleSliderControl, ISliderControl
{
	// Token: 0x170002BB RID: 699
	// (get) Token: 0x060030F2 RID: 12530 RVA: 0x00118171 File Offset: 0x00116371
	// (set) Token: 0x060030F3 RID: 12531 RVA: 0x0011817C File Offset: 0x0011637C
	public float DelayAmount
	{
		get
		{
			return this.delayAmount;
		}
		set
		{
			this.delayAmount = value;
			int delayAmountTicks = this.DelayAmountTicks;
			if (this.delayTicksRemaining > delayAmountTicks)
			{
				this.delayTicksRemaining = delayAmountTicks;
			}
		}
	}

	// Token: 0x170002BC RID: 700
	// (get) Token: 0x060030F4 RID: 12532 RVA: 0x001181A7 File Offset: 0x001163A7
	private int DelayAmountTicks
	{
		get
		{
			return Mathf.RoundToInt(this.delayAmount / LogicCircuitManager.ClockTickInterval);
		}
	}

	// Token: 0x170002BD RID: 701
	// (get) Token: 0x060030F5 RID: 12533 RVA: 0x001181BA File Offset: 0x001163BA
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x170002BE RID: 702
	// (get) Token: 0x060030F6 RID: 12534 RVA: 0x001181C1 File Offset: 0x001163C1
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.SECOND;
		}
	}

	// Token: 0x060030F7 RID: 12535 RVA: 0x001181CD File Offset: 0x001163CD
	public int SliderDecimalPlaces(int index)
	{
		return 1;
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x001181D0 File Offset: 0x001163D0
	public float GetSliderMin(int index)
	{
		return 0.1f;
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x001181D7 File Offset: 0x001163D7
	public float GetSliderMax(int index)
	{
		return 200f;
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x001181DE File Offset: 0x001163DE
	public float GetSliderValue(int index)
	{
		return this.DelayAmount;
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x001181E6 File Offset: 0x001163E6
	public void SetSliderValue(float value, int index)
	{
		this.DelayAmount = value;
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x001181EF File Offset: 0x001163EF
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP";
	}

	// Token: 0x060030FD RID: 12541 RVA: 0x001181F6 File Offset: 0x001163F6
	string ISliderControl.GetSliderTooltip(int index)
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_FILTER_SIDE_SCREEN.TOOLTIP"), this.DelayAmount);
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x00118217 File Offset: 0x00116417
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicGateFilter>(-905833192, LogicGateFilter.OnCopySettingsDelegate);
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x00118230 File Offset: 0x00116430
	private void OnCopySettings(object data)
	{
		LogicGateFilter component = ((GameObject)data).GetComponent<LogicGateFilter>();
		if (component != null)
		{
			this.DelayAmount = component.DelayAmount;
		}
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x00118260 File Offset: 0x00116460
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.meter.SetPositionPercent(0f);
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x001182AC File Offset: 0x001164AC
	private void Update()
	{
		float num;
		if (this.input_was_previously_negative)
		{
			num = 0f;
		}
		else if (this.delayTicksRemaining > 0)
		{
			num = (float)(this.DelayAmountTicks - this.delayTicksRemaining) / (float)this.DelayAmountTicks;
		}
		else
		{
			num = 1f;
		}
		this.meter.SetPositionPercent(num);
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x00118303 File Offset: 0x00116503
	public override void LogicTick()
	{
		if (!this.input_was_previously_negative && this.delayTicksRemaining > 0)
		{
			this.delayTicksRemaining--;
			if (this.delayTicksRemaining <= 0)
			{
				this.OnDelay();
			}
		}
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x00118334 File Offset: 0x00116534
	protected override int GetCustomValue(int val1, int val2)
	{
		if (val1 == 0)
		{
			this.input_was_previously_negative = true;
			this.delayTicksRemaining = 0;
			this.meter.SetPositionPercent(1f);
		}
		else if (this.delayTicksRemaining <= 0)
		{
			if (this.input_was_previously_negative)
			{
				this.delayTicksRemaining = this.DelayAmountTicks;
			}
			this.input_was_previously_negative = false;
		}
		if (val1 != 0 && this.delayTicksRemaining <= 0)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x00118398 File Offset: 0x00116598
	private void OnDelay()
	{
		if (this.cleaningUp)
		{
			return;
		}
		this.delayTicksRemaining = 0;
		this.meter.SetPositionPercent(0f);
		if (this.outputValueOne == 1)
		{
			return;
		}
		int outputCellOne = base.OutputCellOne;
		if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne) is LogicCircuitNetwork))
		{
			return;
		}
		this.outputValueOne = 1;
		base.RefreshAnimation();
	}

	// Token: 0x04001D7F RID: 7551
	[Serialize]
	private bool input_was_previously_negative;

	// Token: 0x04001D80 RID: 7552
	[Serialize]
	private float delayAmount = 5f;

	// Token: 0x04001D81 RID: 7553
	[Serialize]
	private int delayTicksRemaining;

	// Token: 0x04001D82 RID: 7554
	private MeterController meter;

	// Token: 0x04001D83 RID: 7555
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001D84 RID: 7556
	private static readonly EventSystem.IntraObjectHandler<LogicGateFilter> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateFilter>(delegate(LogicGateFilter component, object data)
	{
		component.OnCopySettings(data);
	});
}
