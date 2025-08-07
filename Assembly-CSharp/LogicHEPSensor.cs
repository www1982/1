using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000769 RID: 1897
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicHEPSensor : Switch, ISaveLoadable, IThresholdSwitch, ISimEveryTick
{
	// Token: 0x06003107 RID: 12551 RVA: 0x0011842A File Offset: 0x0011662A
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicHEPSensor>(-905833192, LogicHEPSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x00118444 File Offset: 0x00116644
	private void OnCopySettings(object data)
	{
		LogicHEPSensor component = ((GameObject)data).GetComponent<LogicHEPSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06003109 RID: 12553 RVA: 0x00118480 File Offset: 0x00116680
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
	}

	// Token: 0x0600310A RID: 12554 RVA: 0x001184E9 File Offset: 0x001166E9
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (global::System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new global::System.Action(this.LogicTick));
		base.OnCleanUp();
	}

	// Token: 0x0600310B RID: 12555 RVA: 0x0011851C File Offset: 0x0011671C
	public void SimEveryTick(float dt)
	{
		if (this.waitForLogicTick)
		{
			return;
		}
		Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(this));
		ListPool<ScenePartitionerEntry, LogicHEPSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicHEPSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(vector2I.x, vector2I.y, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
		float num = 0f;
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			HighEnergyParticle component = (scenePartitionerEntry.obj as KCollider2D).gameObject.GetComponent<HighEnergyParticle>();
			if (!(component == null) && component.isCollideable)
			{
				num += component.payload;
			}
		}
		pooledList.Recycle();
		this.foundPayload = num;
		bool flag = (this.activateOnHigherThan && num > this.thresholdPayload) || (!this.activateOnHigherThan && num < this.thresholdPayload);
		if (flag != this.switchedOn)
		{
			this.waitForLogicTick = true;
		}
		this.SetState(flag);
	}

	// Token: 0x0600310C RID: 12556 RVA: 0x00118628 File Offset: 0x00116828
	private void LogicTick()
	{
		this.waitForLogicTick = false;
	}

	// Token: 0x0600310D RID: 12557 RVA: 0x00118631 File Offset: 0x00116831
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x0600310E RID: 12558 RVA: 0x00118640 File Offset: 0x00116840
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x0600310F RID: 12559 RVA: 0x00118660 File Offset: 0x00116860
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

	// Token: 0x06003110 RID: 12560 RVA: 0x001186E8 File Offset: 0x001168E8
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06003111 RID: 12561 RVA: 0x0011873B File Offset: 0x0011693B
	// (set) Token: 0x06003112 RID: 12562 RVA: 0x00118743 File Offset: 0x00116943
	public float Threshold
	{
		get
		{
			return this.thresholdPayload;
		}
		set
		{
			this.thresholdPayload = value;
			this.dirty = true;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06003113 RID: 12563 RVA: 0x00118753 File Offset: 0x00116953
	// (set) Token: 0x06003114 RID: 12564 RVA: 0x0011875B File Offset: 0x0011695B
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

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06003115 RID: 12565 RVA: 0x0011876B File Offset: 0x0011696B
	public float CurrentValue
	{
		get
		{
			return this.foundPayload;
		}
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06003116 RID: 12566 RVA: 0x00118773 File Offset: 0x00116973
	public float RangeMin
	{
		get
		{
			return this.minPayload;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06003117 RID: 12567 RVA: 0x0011877B File Offset: 0x0011697B
	public float RangeMax
	{
		get
		{
			return this.maxPayload;
		}
	}

	// Token: 0x06003118 RID: 12568 RVA: 0x00118783 File Offset: 0x00116983
	public float GetRangeMinInputField()
	{
		return this.minPayload;
	}

	// Token: 0x06003119 RID: 12569 RVA: 0x0011878B File Offset: 0x0011698B
	public float GetRangeMaxInputField()
	{
		return this.maxPayload;
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x0600311A RID: 12570 RVA: 0x00118793 File Offset: 0x00116993
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.HEPSWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x0600311B RID: 12571 RVA: 0x0011879A File Offset: 0x0011699A
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x0600311C RID: 12572 RVA: 0x001187A1 File Offset: 0x001169A1
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x0600311D RID: 12573 RVA: 0x001187AD File Offset: 0x001169AD
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS_TOOLTIP_BELOW;
		}
	}

	// Token: 0x0600311E RID: 12574 RVA: 0x001187B9 File Offset: 0x001169B9
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedHighEnergyParticles(value, GameUtil.TimeSlice.None, units);
	}

	// Token: 0x0600311F RID: 12575 RVA: 0x001187C3 File Offset: 0x001169C3
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003120 RID: 12576 RVA: 0x001187CB File Offset: 0x001169CB
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06003121 RID: 12577 RVA: 0x001187CE File Offset: 0x001169CE
	public LocString ThresholdValueUnits()
	{
		return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x06003122 RID: 12578 RVA: 0x001187D5 File Offset: 0x001169D5
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x06003123 RID: 12579 RVA: 0x001187D8 File Offset: 0x001169D8
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06003124 RID: 12580 RVA: 0x001187DC File Offset: 0x001169DC
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(30f, 50f),
				new NonLinearSlider.Range(30f, 200f),
				new NonLinearSlider.Range(40f, 500f)
			};
		}
	}

	// Token: 0x04001D85 RID: 7557
	[Serialize]
	public float thresholdPayload;

	// Token: 0x04001D86 RID: 7558
	[Serialize]
	public bool activateOnHigherThan;

	// Token: 0x04001D87 RID: 7559
	[Serialize]
	public bool dirty = true;

	// Token: 0x04001D88 RID: 7560
	private readonly float minPayload;

	// Token: 0x04001D89 RID: 7561
	private readonly float maxPayload = 500f;

	// Token: 0x04001D8A RID: 7562
	private float foundPayload;

	// Token: 0x04001D8B RID: 7563
	private bool waitForLogicTick;

	// Token: 0x04001D8C RID: 7564
	private bool wasOn;

	// Token: 0x04001D8D RID: 7565
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001D8E RID: 7566
	private static readonly EventSystem.IntraObjectHandler<LogicHEPSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicHEPSensor>(delegate(LogicHEPSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
