using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000703 RID: 1795
[SerializationConfig(MemberSerialization.OptIn)]
public class ConduitDiseaseSensor : ConduitThresholdSensor, IThresholdSwitch
{
	// Token: 0x06002CEC RID: 11500 RVA: 0x00102554 File Offset: 0x00100754
	protected override void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			if (this.switchedOn)
			{
				this.animController.Play(ConduitSensor.ON_ANIMS, KAnim.PlayMode.Loop);
				int num;
				int num2;
				bool flag;
				this.GetContentsDisease(out num, out num2, out flag);
				Color32 color = Color.white;
				if (num != 255)
				{
					Disease disease = Db.Get().Diseases[num];
					color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
				}
				this.animController.SetSymbolTint(ConduitDiseaseSensor.TINT_SYMBOL, color);
				return;
			}
			this.animController.Play(ConduitSensor.OFF_ANIMS, KAnim.PlayMode.Once);
		}
	}

	// Token: 0x06002CED RID: 11501 RVA: 0x00102614 File Offset: 0x00100814
	private void GetContentsDisease(out int diseaseIdx, out int diseaseCount, out bool hasMass)
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			ConduitFlow.ConduitContents contents = Conduit.GetFlowManager(this.conduitType).GetContents(num);
			diseaseIdx = (int)contents.diseaseIdx;
			diseaseCount = contents.diseaseCount;
			hasMass = contents.mass > 0f;
			return;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents2 = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents2.pickupableHandle);
		if (pickupable != null && pickupable.PrimaryElement.Mass > 0f)
		{
			diseaseIdx = (int)pickupable.PrimaryElement.DiseaseIdx;
			diseaseCount = pickupable.PrimaryElement.DiseaseCount;
			hasMass = true;
			return;
		}
		diseaseIdx = 0;
		diseaseCount = 0;
		hasMass = false;
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06002CEE RID: 11502 RVA: 0x001026C8 File Offset: 0x001008C8
	public override float CurrentValue
	{
		get
		{
			int num;
			int num2;
			bool flag;
			this.GetContentsDisease(out num, out num2, out flag);
			if (flag)
			{
				this.lastValue = (float)num2;
			}
			return this.lastValue;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06002CEF RID: 11503 RVA: 0x001026F2 File Offset: 0x001008F2
	public float RangeMin
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06002CF0 RID: 11504 RVA: 0x001026F9 File Offset: 0x001008F9
	public float RangeMax
	{
		get
		{
			return 100000f;
		}
	}

	// Token: 0x06002CF1 RID: 11505 RVA: 0x00102700 File Offset: 0x00100900
	public float GetRangeMinInputField()
	{
		return 0f;
	}

	// Token: 0x06002CF2 RID: 11506 RVA: 0x00102707 File Offset: 0x00100907
	public float GetRangeMaxInputField()
	{
		return 100000f;
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x0010270E File Offset: 0x0010090E
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TITLE;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x00102715 File Offset: 0x00100915
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.CONTENT_DISEASE;
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0010271C File Offset: 0x0010091C
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06002CF6 RID: 11510 RVA: 0x00102728 File Offset: 0x00100928
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x06002CF7 RID: 11511 RVA: 0x00102734 File Offset: 0x00100934
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedInt((float)((int)value), GameUtil.TimeSlice.None);
	}

	// Token: 0x06002CF8 RID: 11512 RVA: 0x0010273F File Offset: 0x0010093F
	public float ProcessedSliderValue(float input)
	{
		return input;
	}

	// Token: 0x06002CF9 RID: 11513 RVA: 0x00102742 File Offset: 0x00100942
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x06002CFA RID: 11514 RVA: 0x00102745 File Offset: 0x00100945
	public LocString ThresholdValueUnits()
	{
		return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.DISEASE_UNITS;
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06002CFB RID: 11515 RVA: 0x0010274C File Offset: 0x0010094C
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700025F RID: 607
	// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0010274F File Offset: 0x0010094F
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x06002CFD RID: 11517 RVA: 0x00102752 File Offset: 0x00100952
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x04001A80 RID: 6784
	private const float rangeMin = 0f;

	// Token: 0x04001A81 RID: 6785
	private const float rangeMax = 100000f;

	// Token: 0x04001A82 RID: 6786
	[Serialize]
	private float lastValue;

	// Token: 0x04001A83 RID: 6787
	private static readonly HashedString TINT_SYMBOL = "germs";
}
