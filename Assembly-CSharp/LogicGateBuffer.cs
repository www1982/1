using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000767 RID: 1895
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicGateBuffer : LogicGate, ISingleSliderControl, ISliderControl
{
	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x060030DD RID: 12509 RVA: 0x00117EBB File Offset: 0x001160BB
	// (set) Token: 0x060030DE RID: 12510 RVA: 0x00117EC4 File Offset: 0x001160C4
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

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x060030DF RID: 12511 RVA: 0x00117EEF File Offset: 0x001160EF
	private int DelayAmountTicks
	{
		get
		{
			return Mathf.RoundToInt(this.delayAmount / LogicCircuitManager.ClockTickInterval);
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x060030E0 RID: 12512 RVA: 0x00117F02 File Offset: 0x00116102
	public string SliderTitleKey
	{
		get
		{
			return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TITLE";
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x060030E1 RID: 12513 RVA: 0x00117F09 File Offset: 0x00116109
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.SECOND;
		}
	}

	// Token: 0x060030E2 RID: 12514 RVA: 0x00117F15 File Offset: 0x00116115
	public int SliderDecimalPlaces(int index)
	{
		return 1;
	}

	// Token: 0x060030E3 RID: 12515 RVA: 0x00117F18 File Offset: 0x00116118
	public float GetSliderMin(int index)
	{
		return 0.1f;
	}

	// Token: 0x060030E4 RID: 12516 RVA: 0x00117F1F File Offset: 0x0011611F
	public float GetSliderMax(int index)
	{
		return 200f;
	}

	// Token: 0x060030E5 RID: 12517 RVA: 0x00117F26 File Offset: 0x00116126
	public float GetSliderValue(int index)
	{
		return this.DelayAmount;
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x00117F2E File Offset: 0x0011612E
	public void SetSliderValue(float value, int index)
	{
		this.DelayAmount = value;
	}

	// Token: 0x060030E7 RID: 12519 RVA: 0x00117F37 File Offset: 0x00116137
	public string GetSliderTooltipKey(int index)
	{
		return "STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP";
	}

	// Token: 0x060030E8 RID: 12520 RVA: 0x00117F3E File Offset: 0x0011613E
	string ISliderControl.GetSliderTooltip(int index)
	{
		return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.LOGIC_BUFFER_SIDE_SCREEN.TOOLTIP"), this.DelayAmount);
	}

	// Token: 0x060030E9 RID: 12521 RVA: 0x00117F5F File Offset: 0x0011615F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicGateBuffer>(-905833192, LogicGateBuffer.OnCopySettingsDelegate);
	}

	// Token: 0x060030EA RID: 12522 RVA: 0x00117F78 File Offset: 0x00116178
	private void OnCopySettings(object data)
	{
		LogicGateBuffer component = ((GameObject)data).GetComponent<LogicGateBuffer>();
		if (component != null)
		{
			this.DelayAmount = component.DelayAmount;
		}
	}

	// Token: 0x060030EB RID: 12523 RVA: 0x00117FA8 File Offset: 0x001161A8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		this.meter = new MeterController(component, "meter_target", "meter", Meter.Offset.UserSpecified, Grid.SceneLayer.LogicGatesFront, Vector3.zero, null);
		this.meter.SetPositionPercent(1f);
	}

	// Token: 0x060030EC RID: 12524 RVA: 0x00117FF4 File Offset: 0x001161F4
	private void Update()
	{
		float num;
		if (this.input_was_previously_positive)
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

	// Token: 0x060030ED RID: 12525 RVA: 0x0011804B File Offset: 0x0011624B
	public override void LogicTick()
	{
		if (!this.input_was_previously_positive && this.delayTicksRemaining > 0)
		{
			this.delayTicksRemaining--;
			if (this.delayTicksRemaining <= 0)
			{
				this.OnDelay();
			}
		}
	}

	// Token: 0x060030EE RID: 12526 RVA: 0x0011807C File Offset: 0x0011627C
	protected override int GetCustomValue(int val1, int val2)
	{
		if (val1 != 0)
		{
			this.input_was_previously_positive = true;
			this.delayTicksRemaining = 0;
			this.meter.SetPositionPercent(0f);
		}
		else if (this.delayTicksRemaining <= 0)
		{
			if (this.input_was_previously_positive)
			{
				this.delayTicksRemaining = this.DelayAmountTicks;
			}
			this.input_was_previously_positive = false;
		}
		if (val1 == 0 && this.delayTicksRemaining <= 0)
		{
			return 0;
		}
		return 1;
	}

	// Token: 0x060030EF RID: 12527 RVA: 0x001180E0 File Offset: 0x001162E0
	private void OnDelay()
	{
		if (this.cleaningUp)
		{
			return;
		}
		this.delayTicksRemaining = 0;
		this.meter.SetPositionPercent(1f);
		if (this.outputValueOne == 0)
		{
			return;
		}
		int outputCellOne = base.OutputCellOne;
		if (!(Game.Instance.logicCircuitSystem.GetNetworkForCell(outputCellOne) is LogicCircuitNetwork))
		{
			return;
		}
		this.outputValueOne = 0;
		base.RefreshAnimation();
	}

	// Token: 0x04001D79 RID: 7545
	[Serialize]
	private bool input_was_previously_positive;

	// Token: 0x04001D7A RID: 7546
	[Serialize]
	private float delayAmount = 5f;

	// Token: 0x04001D7B RID: 7547
	[Serialize]
	private int delayTicksRemaining;

	// Token: 0x04001D7C RID: 7548
	private MeterController meter;

	// Token: 0x04001D7D RID: 7549
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001D7E RID: 7550
	private static readonly EventSystem.IntraObjectHandler<LogicGateBuffer> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicGateBuffer>(delegate(LogicGateBuffer component, object data)
	{
		component.OnCopySettings(data);
	});
}
