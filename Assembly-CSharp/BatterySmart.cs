using System;
using System.Diagnostics;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006DE RID: 1758
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name}")]
public class BatterySmart : Battery, IActivationRangeTarget
{
	// Token: 0x06002B87 RID: 11143 RVA: 0x000FB20F File Offset: 0x000F940F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<BatterySmart>(-905833192, BatterySmart.OnCopySettingsDelegate);
	}

	// Token: 0x06002B88 RID: 11144 RVA: 0x000FB228 File Offset: 0x000F9428
	private void OnCopySettings(object data)
	{
		BatterySmart component = ((GameObject)data).GetComponent<BatterySmart>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x06002B89 RID: 11145 RVA: 0x000FB262 File Offset: 0x000F9462
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.CreateLogicMeter();
		base.Subscribe<BatterySmart>(-801688580, BatterySmart.OnLogicValueChangedDelegate);
		base.Subscribe<BatterySmart>(-592767678, BatterySmart.UpdateLogicCircuitDelegate);
	}

	// Token: 0x06002B8A RID: 11146 RVA: 0x000FB292 File Offset: 0x000F9492
	private void CreateLogicMeter()
	{
		this.logicMeter = new MeterController(base.GetComponent<KBatchedAnimController>(), "logicmeter_target", "logicmeter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x06002B8B RID: 11147 RVA: 0x000FB2B7 File Offset: 0x000F94B7
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		this.UpdateLogicCircuit(null);
	}

	// Token: 0x06002B8C RID: 11148 RVA: 0x000FB2C8 File Offset: 0x000F94C8
	private void UpdateLogicCircuit(object data)
	{
		float num = (float)Mathf.RoundToInt(base.PercentFull * 100f);
		if (this.activated)
		{
			if (num >= (float)this.deactivateValue)
			{
				this.activated = false;
			}
		}
		else if (num <= (float)this.activateValue)
		{
			this.activated = true;
		}
		bool isOperational = this.operational.IsOperational;
		bool flag = this.activated && isOperational;
		this.logicPorts.SendSignal(BatterySmart.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x06002B8D RID: 11149 RVA: 0x000FB340 File Offset: 0x000F9540
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == BatterySmart.PORT_ID)
		{
			this.SetLogicMeter(LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue));
		}
	}

	// Token: 0x06002B8E RID: 11150 RVA: 0x000FB378 File Offset: 0x000F9578
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06002B8F RID: 11151 RVA: 0x000FB39C File Offset: 0x000F959C
	// (set) Token: 0x06002B90 RID: 11152 RVA: 0x000FB3A5 File Offset: 0x000F95A5
	public float ActivateValue
	{
		get
		{
			return (float)this.deactivateValue;
		}
		set
		{
			this.deactivateValue = (int)value;
			this.UpdateLogicCircuit(null);
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06002B91 RID: 11153 RVA: 0x000FB3B6 File Offset: 0x000F95B6
	// (set) Token: 0x06002B92 RID: 11154 RVA: 0x000FB3BF File Offset: 0x000F95BF
	public float DeactivateValue
	{
		get
		{
			return (float)this.activateValue;
		}
		set
		{
			this.activateValue = (int)value;
			this.UpdateLogicCircuit(null);
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000FB3D0 File Offset: 0x000F95D0
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06002B94 RID: 11156 RVA: 0x000FB3D7 File Offset: 0x000F95D7
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000FB3DE File Offset: 0x000F95DE
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06002B96 RID: 11158 RVA: 0x000FB3E1 File Offset: 0x000F95E1
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000FB3ED File Offset: 0x000F95ED
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06002B98 RID: 11160 RVA: 0x000FB3F9 File Offset: 0x000F95F9
	public string ActivationRangeTitleText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_TITLE;
		}
	}

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06002B99 RID: 11161 RVA: 0x000FB405 File Offset: 0x000F9605
	public string ActivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_DEACTIVATE;
		}
	}

	// Token: 0x17000233 RID: 563
	// (get) Token: 0x06002B9A RID: 11162 RVA: 0x000FB411 File Offset: 0x000F9611
	public string DeactivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.BATTERYSMART.SIDESCREEN_ACTIVATE;
		}
	}

	// Token: 0x040019C2 RID: 6594
	public static readonly HashedString PORT_ID = "BatterySmartLogicPort";

	// Token: 0x040019C3 RID: 6595
	[Serialize]
	private int activateValue;

	// Token: 0x040019C4 RID: 6596
	[Serialize]
	private int deactivateValue = 100;

	// Token: 0x040019C5 RID: 6597
	[Serialize]
	private bool activated;

	// Token: 0x040019C6 RID: 6598
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x040019C7 RID: 6599
	private MeterController logicMeter;

	// Token: 0x040019C8 RID: 6600
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040019C9 RID: 6601
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x040019CA RID: 6602
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x040019CB RID: 6603
	private static readonly EventSystem.IntraObjectHandler<BatterySmart> UpdateLogicCircuitDelegate = new EventSystem.IntraObjectHandler<BatterySmart>(delegate(BatterySmart component, object data)
	{
		component.UpdateLogicCircuit(data);
	});
}
