using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000760 RID: 1888
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicDiseaseSensor : Switch, ISaveLoadable, IThresholdSwitch, ISim200ms
{
	// Token: 0x06003060 RID: 12384 RVA: 0x001149A5 File Offset: 0x00112BA5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicDiseaseSensor>(-905833192, LogicDiseaseSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06003061 RID: 12385 RVA: 0x001149C0 File Offset: 0x00112BC0
	private void OnCopySettings(object data)
	{
		LogicDiseaseSensor component = ((GameObject)data).GetComponent<LogicDiseaseSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06003062 RID: 12386 RVA: 0x001149FA File Offset: 0x00112BFA
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.animController = base.GetComponent<KBatchedAnimController>();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x00114A3C File Offset: 0x00112C3C
	public void Sim200ms(float dt)
	{
		if (this.sampleIdx < 8)
		{
			int num = Grid.PosToCell(this);
			if (Grid.Mass[num] > 0f)
			{
				this.samples[this.sampleIdx] = Grid.DiseaseCount[num];
				this.sampleIdx++;
			}
			return;
		}
		this.sampleIdx = 0;
		float currentValue = this.CurrentValue;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
		this.animController.SetSymbolVisiblity(LogicDiseaseSensor.TINT_SYMBOL, currentValue > 0f);
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x00114B17 File Offset: 0x00112D17
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06003065 RID: 12389 RVA: 0x00114B26 File Offset: 0x00112D26
	// (set) Token: 0x06003066 RID: 12390 RVA: 0x00114B2E File Offset: 0x00112D2E
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

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06003067 RID: 12391 RVA: 0x00114B37 File Offset: 0x00112D37
	// (set) Token: 0x06003068 RID: 12392 RVA: 0x00114B3F File Offset: 0x00112D3F
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

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06003069 RID: 12393 RVA: 0x00114B48 File Offset: 0x00112D48
	public float CurrentValue
	{
		get
		{
			float num = 0f;
			for (int i = 0; i < 8; i++)
			{
				num += (float)this.samples[i];
			}
			return num / 8f;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x0600306A RID: 12394 RVA: 0x00114B7A File Offset: 0x00112D7A
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x0600306B RID: 12395 RVA: 0x00114B81 File Offset: 0x00112D81
	public float RangeMax
	{
		get
		{
			return 100000f;
		}
	}

	// Token: 0x0600306C RID: 12396 RVA: 0x00114B88 File Offset: 0x00112D88
	public float GetRangeMinInputField()
	{
		return 0f;
	}

	// Token: 0x0600306D RID: 12397 RVA: 0x00114B8F File Offset: 0x00112D8F
	public float GetRangeMaxInputField()
	{
		return 100000f;
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x0600306E RID: 12398 RVA: 0x00114B96 File Offset: 0x00112D96
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE;
		}
	}

	// Token: 0x170002A3 RID: 675
	// (get) Token: 0x0600306F RID: 12399 RVA: 0x00114B9D File Offset: 0x00112D9D
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x170002A4 RID: 676
	// (get) Token: 0x06003070 RID: 12400 RVA: 0x00114BA9 File Offset: 0x00112DA9
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06003071 RID: 12401 RVA: 0x00114BB5 File Offset: 0x00112DB5
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedInt((float)((int)value), GameUtil.TimeSlice.None);
	}

	// Token: 0x06003072 RID: 12402 RVA: 0x00114BC0 File Offset: 0x00112DC0
	public float ProcessedSliderValue(float input)
	{
		return input;
	}

	// Token: 0x06003073 RID: 12403 RVA: 0x00114BC3 File Offset: 0x00112DC3
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06003074 RID: 12404 RVA: 0x00114BC6 File Offset: 0x00112DC6
	public LocString ThresholdValueUnits()
	{
		return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_UNITS;
	}

	// Token: 0x170002A5 RID: 677
	// (get) Token: 0x06003075 RID: 12405 RVA: 0x00114BCD File Offset: 0x00112DCD
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x06003076 RID: 12406 RVA: 0x00114BD0 File Offset: 0x00112DD0
	public int IncrementScale
	{
		get
		{
			return 100;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x06003077 RID: 12407 RVA: 0x00114BD4 File Offset: 0x00112DD4
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x06003078 RID: 12408 RVA: 0x00114BE1 File Offset: 0x00112DE1
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x06003079 RID: 12409 RVA: 0x00114C00 File Offset: 0x00112E00
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(LogicDiseaseSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				int num = Grid.PosToCell(this);
				byte b = Grid.DiseaseIdx[num];
				Color32 color = Color.white;
				if (b != 255)
				{
					Disease disease = Db.Get().Diseases[(int)b];
					color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
				}
				this.animController.SetSymbolTint(LogicDiseaseSensor.TINT_SYMBOL, color);
				return;
			}
			this.animController.Play(LogicDiseaseSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x0600307A RID: 12410 RVA: 0x00114CC4 File Offset: 0x00112EC4
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x0600307B RID: 12411 RVA: 0x00114D17 File Offset: 0x00112F17
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TITLE;
		}
	}

	// Token: 0x04001CE4 RID: 7396
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x04001CE5 RID: 7397
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x04001CE6 RID: 7398
	private KBatchedAnimController animController;

	// Token: 0x04001CE7 RID: 7399
	private bool wasOn;

	// Token: 0x04001CE8 RID: 7400
	private const float rangeMin = 0f;

	// Token: 0x04001CE9 RID: 7401
	private const float rangeMax = 100000f;

	// Token: 0x04001CEA RID: 7402
	private const int WINDOW_SIZE = 8;

	// Token: 0x04001CEB RID: 7403
	private int[] samples = new int[8];

	// Token: 0x04001CEC RID: 7404
	private int sampleIdx;

	// Token: 0x04001CED RID: 7405
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001CEE RID: 7406
	private static readonly EventSystem.IntraObjectHandler<LogicDiseaseSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicDiseaseSensor>(delegate(LogicDiseaseSensor component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001CEF RID: 7407
	private static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on_loop" };

	// Token: 0x04001CF0 RID: 7408
	private static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };

	// Token: 0x04001CF1 RID: 7409
	private static readonly HashedString TINT_SYMBOL = "germs";
}
