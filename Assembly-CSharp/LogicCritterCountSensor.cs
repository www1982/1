using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200075F RID: 1887
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicCritterCountSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x06003042 RID: 12354 RVA: 0x001145D6 File Offset: 0x001127D6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.selectable = base.GetComponent<KSelectable>();
		base.Subscribe<LogicCritterCountSensor>(-905833192, LogicCritterCountSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06003043 RID: 12355 RVA: 0x001145FC File Offset: 0x001127FC
	private void OnCopySettings(object data)
	{
		LogicCritterCountSensor component = ((GameObject)data).GetComponent<LogicCritterCountSensor>();
		if (component != null)
		{
			this.countThreshold = component.countThreshold;
			this.activateOnGreaterThan = component.activateOnGreaterThan;
			this.countCritters = component.countCritters;
			this.countEggs = component.countEggs;
		}
	}

	// Token: 0x06003044 RID: 12356 RVA: 0x0011464E File Offset: 0x0011284E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06003045 RID: 12357 RVA: 0x00114684 File Offset: 0x00112884
	public void Sim200ms(float dt)
	{
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			this.currentCount = 0;
			if (this.countCritters)
			{
				this.currentCount += roomOfGameObject.cavity.creatures.Count;
			}
			if (this.countEggs)
			{
				this.currentCount += roomOfGameObject.cavity.eggs.Count;
			}
			bool flag = (this.activateOnGreaterThan ? (this.currentCount > this.countThreshold) : (this.currentCount < this.countThreshold));
			this.SetState(flag);
			if (this.selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
			{
				this.selectable.RemoveStatusItem(this.roomStatusGUID, false);
				return;
			}
		}
		else
		{
			if (!this.selectable.HasStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom))
			{
				this.roomStatusGUID = this.selectable.AddStatusItem(Db.Get().BuildingStatusItems.NotInAnyRoom, null);
			}
			this.SetState(false);
		}
	}

	// Token: 0x06003046 RID: 12358 RVA: 0x001147A0 File Offset: 0x001129A0
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x06003047 RID: 12359 RVA: 0x001147AF File Offset: 0x001129AF
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06003048 RID: 12360 RVA: 0x001147D0 File Offset: 0x001129D0
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			if (this.switchedOn)
			{
				component.Queue("on", KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
			component.Queue("off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x06003049 RID: 12361 RVA: 0x00114870 File Offset: 0x00112A70
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x0600304A RID: 12362 RVA: 0x001148C3 File Offset: 0x00112AC3
	// (set) Token: 0x0600304B RID: 12363 RVA: 0x001148CC File Offset: 0x00112ACC
	public float Threshold
	{
		get
		{
			return (float)this.countThreshold;
		}
		set
		{
			this.countThreshold = (int)value;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x0600304C RID: 12364 RVA: 0x001148D6 File Offset: 0x00112AD6
	// (set) Token: 0x0600304D RID: 12365 RVA: 0x001148DE File Offset: 0x00112ADE
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnGreaterThan;
		}
		set
		{
			this.activateOnGreaterThan = value;
		}
	}

	// Token: 0x17000293 RID: 659
	// (get) Token: 0x0600304E RID: 12366 RVA: 0x001148E7 File Offset: 0x00112AE7
	public float CurrentValue
	{
		get
		{
			return (float)this.currentCount;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x0600304F RID: 12367 RVA: 0x001148F0 File Offset: 0x00112AF0
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000295 RID: 661
	// (get) Token: 0x06003050 RID: 12368 RVA: 0x001148F7 File Offset: 0x00112AF7
	public float RangeMax
	{
		get
		{
			return 64f;
		}
	}

	// Token: 0x06003051 RID: 12369 RVA: 0x001148FE File Offset: 0x00112AFE
	public float GetRangeMinInputField()
	{
		return this.RangeMin;
	}

	// Token: 0x06003052 RID: 12370 RVA: 0x00114906 File Offset: 0x00112B06
	public float GetRangeMaxInputField()
	{
		return this.RangeMax;
	}

	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06003053 RID: 12371 RVA: 0x0011490E File Offset: 0x00112B0E
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TITLE;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06003054 RID: 12372 RVA: 0x00114915 File Offset: 0x00112B15
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.VALUE_NAME;
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06003055 RID: 12373 RVA: 0x0011491C File Offset: 0x00112B1C
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06003056 RID: 12374 RVA: 0x00114928 File Offset: 0x00112B28
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.CRITTER_COUNT_SIDE_SCREEN.TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x00114934 File Offset: 0x00112B34
	public string Format(float value, bool units)
	{
		return value.ToString();
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x0011493D File Offset: 0x00112B3D
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x06003059 RID: 12377 RVA: 0x00114945 File Offset: 0x00112B45
	public float ProcessedInputValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x0600305A RID: 12378 RVA: 0x0011494D File Offset: 0x00112B4D
	public LocString ThresholdValueUnits()
	{
		return "";
	}

	// Token: 0x1700029A RID: 666
	// (get) Token: 0x0600305B RID: 12379 RVA: 0x00114959 File Offset: 0x00112B59
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x0600305C RID: 12380 RVA: 0x0011495C File Offset: 0x00112B5C
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x0600305D RID: 12381 RVA: 0x0011495F File Offset: 0x00112B5F
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x04001CDA RID: 7386
	private bool wasOn;

	// Token: 0x04001CDB RID: 7387
	[Serialize]
	public bool countEggs = true;

	// Token: 0x04001CDC RID: 7388
	[Serialize]
	public bool countCritters = true;

	// Token: 0x04001CDD RID: 7389
	[Serialize]
	public int countThreshold;

	// Token: 0x04001CDE RID: 7390
	[Serialize]
	public bool activateOnGreaterThan = true;

	// Token: 0x04001CDF RID: 7391
	[Serialize]
	public int currentCount;

	// Token: 0x04001CE0 RID: 7392
	private KSelectable selectable;

	// Token: 0x04001CE1 RID: 7393
	private Guid roomStatusGUID;

	// Token: 0x04001CE2 RID: 7394
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001CE3 RID: 7395
	private static readonly EventSystem.IntraObjectHandler<LogicCritterCountSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicCritterCountSensor>(delegate(LogicCritterCountSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
