using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200076C RID: 1900
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicMassSensor : Switch, ISaveLoadable, IThresholdSwitch
{
	// Token: 0x0600314D RID: 12621 RVA: 0x0011912C File Offset: 0x0011732C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicMassSensor>(-905833192, LogicMassSensor.OnCopySettingsDelegate);
	}

	// Token: 0x0600314E RID: 12622 RVA: 0x00119148 File Offset: 0x00117348
	private void OnCopySettings(object data)
	{
		LogicMassSensor component = ((GameObject)data).GetComponent<LogicMassSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x0600314F RID: 12623 RVA: 0x00119184 File Offset: 0x00117384
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateVisualState(true);
		int num = Grid.CellAbove(this.NaturalBuildingCell());
		this.solidChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.SolidChanged", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.pickupablesChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.PickupablesChanged", base.gameObject, num, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnPickupablesChanged));
		this.floorSwitchActivatorChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.SwitchActivatorChanged", base.gameObject, num, GameScenePartitioner.Instance.floorSwitchActivatorChangedLayer, new Action<object>(this.OnActivatorsChanged));
		base.OnToggle += this.SwitchToggled;
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x00119252 File Offset: 0x00117452
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.solidChangedEntry);
		GameScenePartitioner.Instance.Free(ref this.pickupablesChangedEntry);
		GameScenePartitioner.Instance.Free(ref this.floorSwitchActivatorChangedEntry);
		base.OnCleanUp();
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x0011928C File Offset: 0x0011748C
	private void Update()
	{
		this.toggleCooldown = Mathf.Max(0f, this.toggleCooldown - Time.deltaTime);
		if (this.toggleCooldown == 0f)
		{
			float currentValue = this.CurrentValue;
			if ((this.activateAboveThreshold ? (currentValue > this.threshold) : (currentValue < this.threshold)) != base.IsSwitchedOn)
			{
				this.Toggle();
				this.toggleCooldown = 0.15f;
			}
			this.UpdateVisualState(false);
		}
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x00119308 File Offset: 0x00117508
	private void OnSolidChanged(object data)
	{
		int num = Grid.CellAbove(this.NaturalBuildingCell());
		if (Grid.Solid[num])
		{
			this.massSolid = Grid.Mass[num];
			return;
		}
		this.massSolid = 0f;
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x0011934C File Offset: 0x0011754C
	private void OnPickupablesChanged(object data)
	{
		float num = 0f;
		int num2 = Grid.CellAbove(this.NaturalBuildingCell());
		ListPool<ScenePartitionerEntry, LogicMassSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicMassSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(Grid.CellToXY(num2).x, Grid.CellToXY(num2).y, 1, 1, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			Pickupable pickupable = pooledList[i].obj as Pickupable;
			if (!(pickupable == null) && !pickupable.wasAbsorbed)
			{
				KPrefabID kprefabID = pickupable.KPrefabID;
				if (!kprefabID.HasTag(GameTags.Creature) || (kprefabID.HasTag(GameTags.Creatures.Walker) || kprefabID.HasTag(GameTags.Creatures.Hoverer) || kprefabID.HasTag(GameTags.Creatures.Flopping)))
				{
					num += pickupable.PrimaryElement.Mass;
				}
			}
		}
		pooledList.Recycle();
		this.massPickupables = num;
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x00119438 File Offset: 0x00117638
	private void OnActivatorsChanged(object data)
	{
		float num = 0f;
		int num2 = Grid.CellAbove(this.NaturalBuildingCell());
		ListPool<ScenePartitionerEntry, LogicMassSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicMassSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(Grid.CellToXY(num2).x, Grid.CellToXY(num2).y, 1, 1, GameScenePartitioner.Instance.floorSwitchActivatorLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			FloorSwitchActivator floorSwitchActivator = pooledList[i].obj as FloorSwitchActivator;
			if (!(floorSwitchActivator == null))
			{
				num += floorSwitchActivator.PrimaryElement.Mass;
			}
		}
		pooledList.Recycle();
		this.massActivators = num;
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06003155 RID: 12629 RVA: 0x001194D4 File Offset: 0x001176D4
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06003156 RID: 12630 RVA: 0x001194DB File Offset: 0x001176DB
	// (set) Token: 0x06003157 RID: 12631 RVA: 0x001194E3 File Offset: 0x001176E3
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

	// Token: 0x170002D9 RID: 729
	// (get) Token: 0x06003158 RID: 12632 RVA: 0x001194EC File Offset: 0x001176EC
	// (set) Token: 0x06003159 RID: 12633 RVA: 0x001194F4 File Offset: 0x001176F4
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

	// Token: 0x170002DA RID: 730
	// (get) Token: 0x0600315A RID: 12634 RVA: 0x001194FD File Offset: 0x001176FD
	public float CurrentValue
	{
		get
		{
			return this.massSolid + this.massPickupables + this.massActivators;
		}
	}

	// Token: 0x170002DB RID: 731
	// (get) Token: 0x0600315B RID: 12635 RVA: 0x00119513 File Offset: 0x00117713
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x170002DC RID: 732
	// (get) Token: 0x0600315C RID: 12636 RVA: 0x0011951B File Offset: 0x0011771B
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x0600315D RID: 12637 RVA: 0x00119523 File Offset: 0x00117723
	public float GetRangeMinInputField()
	{
		return this.rangeMin;
	}

	// Token: 0x0600315E RID: 12638 RVA: 0x0011952B File Offset: 0x0011772B
	public float GetRangeMaxInputField()
	{
		return this.rangeMax;
	}

	// Token: 0x170002DD RID: 733
	// (get) Token: 0x0600315F RID: 12639 RVA: 0x00119533 File Offset: 0x00117733
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06003160 RID: 12640 RVA: 0x0011953A File Offset: 0x0011773A
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002DF RID: 735
	// (get) Token: 0x06003161 RID: 12641 RVA: 0x00119546 File Offset: 0x00117746
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003162 RID: 12642 RVA: 0x00119554 File Offset: 0x00117754
	public string Format(float value, bool units)
	{
		GameUtil.MetricMassFormat metricMassFormat = GameUtil.MetricMassFormat.Kilogram;
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, metricMassFormat, units, "{0:0.#}");
	}

	// Token: 0x06003163 RID: 12643 RVA: 0x00119573 File Offset: 0x00117773
	public float ProcessedSliderValue(float input)
	{
		input = Mathf.Round(input);
		return input;
	}

	// Token: 0x06003164 RID: 12644 RVA: 0x0011957E File Offset: 0x0011777E
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06003165 RID: 12645 RVA: 0x00119581 File Offset: 0x00117781
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(false);
	}

	// Token: 0x170002E0 RID: 736
	// (get) Token: 0x06003166 RID: 12646 RVA: 0x00119589 File Offset: 0x00117789
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002E1 RID: 737
	// (get) Token: 0x06003167 RID: 12647 RVA: 0x0011958C File Offset: 0x0011778C
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06003168 RID: 12648 RVA: 0x0011958F File Offset: 0x0011778F
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x06003169 RID: 12649 RVA: 0x0011959C File Offset: 0x0011779C
	private void SwitchToggled(bool toggled_on)
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, toggled_on ? 1 : 0);
	}

	// Token: 0x0600316A RID: 12650 RVA: 0x001195B8 File Offset: 0x001177B8
	private void UpdateVisualState(bool force = false)
	{
		bool flag = this.CurrentValue > this.threshold;
		if (flag != this.was_pressed || this.was_on != base.IsSwitchedOn || force)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			if (flag)
			{
				if (force)
				{
					component.Play(base.IsSwitchedOn ? "on_down" : "off_down", KAnim.PlayMode.Once, 1f, 0f);
				}
				else
				{
					component.Play(base.IsSwitchedOn ? "on_down_pre" : "off_down_pre", KAnim.PlayMode.Once, 1f, 0f);
					component.Queue(base.IsSwitchedOn ? "on_down" : "off_down", KAnim.PlayMode.Once, 1f, 0f);
				}
			}
			else if (force)
			{
				component.Play(base.IsSwitchedOn ? "on_up" : "off_up", KAnim.PlayMode.Once, 1f, 0f);
			}
			else
			{
				component.Play(base.IsSwitchedOn ? "on_up_pre" : "off_up_pre", KAnim.PlayMode.Once, 1f, 0f);
				component.Queue(base.IsSwitchedOn ? "on_up" : "off_up", KAnim.PlayMode.Once, 1f, 0f);
			}
			this.was_pressed = flag;
			this.was_on = base.IsSwitchedOn;
		}
	}

	// Token: 0x0600316B RID: 12651 RVA: 0x00119728 File Offset: 0x00117928
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x04001DAA RID: 7594
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001DAB RID: 7595
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001DAC RID: 7596
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x04001DAD RID: 7597
	private bool was_pressed;

	// Token: 0x04001DAE RID: 7598
	private bool was_on;

	// Token: 0x04001DAF RID: 7599
	public float rangeMin;

	// Token: 0x04001DB0 RID: 7600
	public float rangeMax = 1f;

	// Token: 0x04001DB1 RID: 7601
	[Serialize]
	private float massSolid;

	// Token: 0x04001DB2 RID: 7602
	[Serialize]
	private float massPickupables;

	// Token: 0x04001DB3 RID: 7603
	[Serialize]
	private float massActivators;

	// Token: 0x04001DB4 RID: 7604
	private const float MIN_TOGGLE_TIME = 0.15f;

	// Token: 0x04001DB5 RID: 7605
	private float toggleCooldown = 0.15f;

	// Token: 0x04001DB6 RID: 7606
	private HandleVector<int>.Handle solidChangedEntry;

	// Token: 0x04001DB7 RID: 7607
	private HandleVector<int>.Handle pickupablesChangedEntry;

	// Token: 0x04001DB8 RID: 7608
	private HandleVector<int>.Handle floorSwitchActivatorChangedEntry;

	// Token: 0x04001DB9 RID: 7609
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001DBA RID: 7610
	private static readonly EventSystem.IntraObjectHandler<LogicMassSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicMassSensor>(delegate(LogicMassSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
