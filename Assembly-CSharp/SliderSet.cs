using System;
using UnityEngine;

// Token: 0x02000E38 RID: 3640
[Serializable]
public class SliderSet
{
	// Token: 0x0600735E RID: 29534 RVA: 0x002BD680 File Offset: 0x002BB880
	public void SetupSlider(int index)
	{
		this.index = index;
		this.valueSlider.onReleaseHandle += delegate
		{
			this.valueSlider.value = Mathf.Round(this.valueSlider.value * 10f) / 10f;
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.valueSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider();
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput();
		};
	}

	// Token: 0x0600735F RID: 29535 RVA: 0x002BD708 File Offset: 0x002BB908
	public void SetTarget(ISliderControl target, int index)
	{
		this.index = index;
		this.target = target;
		ToolTip component = this.valueSlider.handleRect.GetComponent<ToolTip>();
		if (component != null)
		{
			component.SetSimpleTooltip(target.GetSliderTooltip(index));
		}
		if (this.targetLabel != null)
		{
			this.targetLabel.text = ((target.SliderTitleKey != null) ? Strings.Get(target.SliderTitleKey) : "");
		}
		this.unitsLabel.text = target.SliderUnits;
		this.minLabel.text = target.GetSliderMin(index).ToString() + target.SliderUnits;
		this.maxLabel.text = target.GetSliderMax(index).ToString() + target.SliderUnits;
		this.numberInput.minValue = target.GetSliderMin(index);
		this.numberInput.maxValue = target.GetSliderMax(index);
		this.numberInput.decimalPlaces = target.SliderDecimalPlaces(index);
		this.numberInput.field.characterLimit = Mathf.FloorToInt(1f + Mathf.Log10(this.numberInput.maxValue + (float)this.numberInput.decimalPlaces));
		Vector2 sizeDelta = this.numberInput.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.x = (float)((this.numberInput.field.characterLimit + 1) * 10);
		this.numberInput.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		this.valueSlider.minValue = target.GetSliderMin(index);
		this.valueSlider.maxValue = target.GetSliderMax(index);
		this.valueSlider.value = target.GetSliderValue(index);
		this.SetValue(target.GetSliderValue(index));
		if (index == 0)
		{
			this.numberInput.Activate();
		}
	}

	// Token: 0x06007360 RID: 29536 RVA: 0x002BD8DC File Offset: 0x002BBADC
	private void ReceiveValueFromSlider()
	{
		float num = this.valueSlider.value;
		if (this.numberInput.decimalPlaces != -1)
		{
			float num2 = Mathf.Pow(10f, (float)this.numberInput.decimalPlaces);
			num = Mathf.Round(num * num2) / num2;
		}
		this.SetValue(num);
	}

	// Token: 0x06007361 RID: 29537 RVA: 0x002BD92C File Offset: 0x002BBB2C
	private void ReceiveValueFromInput()
	{
		float num = this.numberInput.currentValue;
		if (this.numberInput.decimalPlaces != -1)
		{
			float num2 = Mathf.Pow(10f, (float)this.numberInput.decimalPlaces);
			num = Mathf.Round(num * num2) / num2;
		}
		this.valueSlider.value = num;
		this.SetValue(num);
	}

	// Token: 0x06007362 RID: 29538 RVA: 0x002BD988 File Offset: 0x002BBB88
	private void SetValue(float value)
	{
		float num = value;
		if (num > this.target.GetSliderMax(this.index))
		{
			num = this.target.GetSliderMax(this.index);
		}
		else if (num < this.target.GetSliderMin(this.index))
		{
			num = this.target.GetSliderMin(this.index);
		}
		this.UpdateLabel(num);
		this.target.SetSliderValue(num, this.index);
		ToolTip component = this.valueSlider.handleRect.GetComponent<ToolTip>();
		if (component != null)
		{
			component.SetSimpleTooltip(this.target.GetSliderTooltip(this.index));
		}
	}

	// Token: 0x06007363 RID: 29539 RVA: 0x002BDA30 File Offset: 0x002BBC30
	private void UpdateLabel(float value)
	{
		float num = Mathf.Round(value * 10f) / 10f;
		this.numberInput.SetDisplayValue(num.ToString());
	}

	// Token: 0x04004F67 RID: 20327
	public KSlider valueSlider;

	// Token: 0x04004F68 RID: 20328
	public KNumberInputField numberInput;

	// Token: 0x04004F69 RID: 20329
	public LocText targetLabel;

	// Token: 0x04004F6A RID: 20330
	public LocText unitsLabel;

	// Token: 0x04004F6B RID: 20331
	public LocText minLabel;

	// Token: 0x04004F6C RID: 20332
	public LocText maxLabel;

	// Token: 0x04004F6D RID: 20333
	[NonSerialized]
	public int index;

	// Token: 0x04004F6E RID: 20334
	private ISliderControl target;
}
