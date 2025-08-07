using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000B18 RID: 2840
[AddComponentMenu("KMonoBehaviour/scripts/SmartReservoir")]
public class SmartReservoir : KMonoBehaviour, IActivationRangeTarget, ISim200ms
{
	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x060053AD RID: 21421 RVA: 0x001E757E File Offset: 0x001E577E
	public float PercentFull
	{
		get
		{
			return this.storage.MassStored() / this.storage.Capacity();
		}
	}

	// Token: 0x060053AE RID: 21422 RVA: 0x001E7597 File Offset: 0x001E5797
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SmartReservoir>(-801688580, SmartReservoir.OnLogicValueChangedDelegate);
		base.Subscribe<SmartReservoir>(-592767678, SmartReservoir.UpdateLogicCircuitDelegate);
	}

	// Token: 0x060053AF RID: 21423 RVA: 0x001E75C1 File Offset: 0x001E57C1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<SmartReservoir>(-905833192, SmartReservoir.OnCopySettingsDelegate);
	}

	// Token: 0x060053B0 RID: 21424 RVA: 0x001E75DA File Offset: 0x001E57DA
	public void Sim200ms(float dt)
	{
		this.UpdateLogicCircuit(null);
	}

	// Token: 0x060053B1 RID: 21425 RVA: 0x001E75E4 File Offset: 0x001E57E4
	private void UpdateLogicCircuit(object data)
	{
		float num = this.PercentFull * 100f;
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
		bool flag = this.activated;
		this.logicPorts.SendSignal(SmartReservoir.PORT_ID, flag ? 1 : 0);
	}

	// Token: 0x060053B2 RID: 21426 RVA: 0x001E7648 File Offset: 0x001E5848
	private void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID == SmartReservoir.PORT_ID)
		{
			this.SetLogicMeter(LogicCircuitNetwork.IsBitActive(0, logicValueChanged.newValue));
		}
	}

	// Token: 0x060053B3 RID: 21427 RVA: 0x001E7680 File Offset: 0x001E5880
	private void OnCopySettings(object data)
	{
		SmartReservoir component = ((GameObject)data).GetComponent<SmartReservoir>();
		if (component != null)
		{
			this.ActivateValue = component.ActivateValue;
			this.DeactivateValue = component.DeactivateValue;
		}
	}

	// Token: 0x060053B4 RID: 21428 RVA: 0x001E76BA File Offset: 0x001E58BA
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x060053B5 RID: 21429 RVA: 0x001E76DE File Offset: 0x001E58DE
	// (set) Token: 0x060053B6 RID: 21430 RVA: 0x001E76E7 File Offset: 0x001E58E7
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

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x060053B7 RID: 21431 RVA: 0x001E76F8 File Offset: 0x001E58F8
	// (set) Token: 0x060053B8 RID: 21432 RVA: 0x001E7701 File Offset: 0x001E5901
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

	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x060053B9 RID: 21433 RVA: 0x001E7712 File Offset: 0x001E5912
	public float MinValue
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x060053BA RID: 21434 RVA: 0x001E7719 File Offset: 0x001E5919
	public float MaxValue
	{
		get
		{
			return 100f;
		}
	}

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x060053BB RID: 21435 RVA: 0x001E7720 File Offset: 0x001E5920
	public bool UseWholeNumbers
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x060053BC RID: 21436 RVA: 0x001E7723 File Offset: 0x001E5923
	public string ActivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.DEACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x060053BD RID: 21437 RVA: 0x001E772F File Offset: 0x001E592F
	public string DeactivateTooltip
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.ACTIVATE_TOOLTIP;
		}
	}

	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x060053BE RID: 21438 RVA: 0x001E773B File Offset: 0x001E593B
	public string ActivationRangeTitleText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_TITLE;
		}
	}

	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x060053BF RID: 21439 RVA: 0x001E7747 File Offset: 0x001E5947
	public string ActivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_DEACTIVATE;
		}
	}

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x060053C0 RID: 21440 RVA: 0x001E7753 File Offset: 0x001E5953
	public string DeactivateSliderLabelText
	{
		get
		{
			return BUILDINGS.PREFABS.SMARTRESERVOIR.SIDESCREEN_ACTIVATE;
		}
	}

	// Token: 0x0400383F RID: 14399
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003840 RID: 14400
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04003841 RID: 14401
	[Serialize]
	private int activateValue;

	// Token: 0x04003842 RID: 14402
	[Serialize]
	private int deactivateValue = 100;

	// Token: 0x04003843 RID: 14403
	[Serialize]
	private bool activated;

	// Token: 0x04003844 RID: 14404
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x04003845 RID: 14405
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04003846 RID: 14406
	private MeterController logicMeter;

	// Token: 0x04003847 RID: 14407
	public static readonly HashedString PORT_ID = "SmartReservoirLogicPort";

	// Token: 0x04003848 RID: 14408
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04003849 RID: 14409
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x0400384A RID: 14410
	private static readonly EventSystem.IntraObjectHandler<SmartReservoir> UpdateLogicCircuitDelegate = new EventSystem.IntraObjectHandler<SmartReservoir>(delegate(SmartReservoir component, object data)
	{
		component.UpdateLogicCircuit(data);
	});
}
