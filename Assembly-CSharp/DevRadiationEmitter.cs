using System;
using STRINGS;

// Token: 0x02000715 RID: 1813
public class DevRadiationEmitter : KMonoBehaviour, ISingleSliderControl, ISliderControl
{
	// Token: 0x06002D7F RID: 11647 RVA: 0x00104F01 File Offset: 0x00103101
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.radiationEmitter != null)
		{
			this.radiationEmitter.SetEmitting(true);
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06002D80 RID: 11648 RVA: 0x00104F23 File Offset: 0x00103123
	public string SliderTitleKey
	{
		get
		{
			return BUILDINGS.PREFABS.DEVRADIATIONGENERATOR.NAME;
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06002D81 RID: 11649 RVA: 0x00104F2F File Offset: 0x0010312F
	public string SliderUnits
	{
		get
		{
			return UI.UNITSUFFIXES.RADIATION.RADS;
		}
	}

	// Token: 0x06002D82 RID: 11650 RVA: 0x00104F3B File Offset: 0x0010313B
	public float GetSliderMax(int index)
	{
		return 5000f;
	}

	// Token: 0x06002D83 RID: 11651 RVA: 0x00104F42 File Offset: 0x00103142
	public float GetSliderMin(int index)
	{
		return 0f;
	}

	// Token: 0x06002D84 RID: 11652 RVA: 0x00104F49 File Offset: 0x00103149
	public string GetSliderTooltip(int index)
	{
		return "";
	}

	// Token: 0x06002D85 RID: 11653 RVA: 0x00104F50 File Offset: 0x00103150
	public string GetSliderTooltipKey(int index)
	{
		return "";
	}

	// Token: 0x06002D86 RID: 11654 RVA: 0x00104F57 File Offset: 0x00103157
	public float GetSliderValue(int index)
	{
		return this.radiationEmitter.emitRads;
	}

	// Token: 0x06002D87 RID: 11655 RVA: 0x00104F64 File Offset: 0x00103164
	public void SetSliderValue(float value, int index)
	{
		this.radiationEmitter.emitRads = value;
		this.radiationEmitter.Refresh();
	}

	// Token: 0x06002D88 RID: 11656 RVA: 0x00104F7D File Offset: 0x0010317D
	public int SliderDecimalPlaces(int index)
	{
		return 0;
	}

	// Token: 0x04001AC3 RID: 6851
	[MyCmpReq]
	private RadiationEmitter radiationEmitter;
}
