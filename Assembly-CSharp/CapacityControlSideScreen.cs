using System;
using UnityEngine;

// Token: 0x02000DDE RID: 3550
public class CapacityControlSideScreen : SideScreenContent
{
	// Token: 0x0600700F RID: 28687 RVA: 0x002A9EBC File Offset: 0x002A80BC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = this.target.CapacityUnits;
		this.slider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.slider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.slider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.slider.value);
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 1;
	}

	// Token: 0x06007010 RID: 28688 RVA: 0x002A9F52 File Offset: 0x002A8152
	public override bool IsValidForTarget(GameObject target)
	{
		return !target.GetComponent<IUserControlledCapacity>().IsNullOrDestroyed() || target.GetSMI<IUserControlledCapacity>() != null;
	}

	// Token: 0x06007011 RID: 28689 RVA: 0x002A9F6C File Offset: 0x002A816C
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IUserControlledCapacity>();
		if (this.target == null)
		{
			this.target = new_target.GetSMI<IUserControlledCapacity>();
		}
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a IThresholdSwitch component");
			return;
		}
		this.slider.minValue = this.target.MinCapacity;
		this.slider.maxValue = this.target.MaxCapacity;
		this.slider.value = this.target.UserMaxCapacity;
		this.slider.GetComponentInChildren<ToolTip>();
		this.unitsLabel.text = this.target.CapacityUnits;
		this.numberInput.minValue = this.target.MinCapacity;
		this.numberInput.maxValue = this.target.MaxCapacity;
		this.numberInput.currentValue = Mathf.Max(this.target.MinCapacity, Mathf.Min(this.target.MaxCapacity, this.target.UserMaxCapacity));
		this.numberInput.Activate();
		this.UpdateMaxCapacityLabel();
	}

	// Token: 0x06007012 RID: 28690 RVA: 0x002AA09C File Offset: 0x002A829C
	private void ReceiveValueFromSlider(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06007013 RID: 28691 RVA: 0x002AA0A5 File Offset: 0x002A82A5
	private void ReceiveValueFromInput(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06007014 RID: 28692 RVA: 0x002AA0AE File Offset: 0x002A82AE
	private void UpdateMaxCapacity(float newValue)
	{
		this.target.UserMaxCapacity = newValue;
		this.slider.value = newValue;
		this.UpdateMaxCapacityLabel();
	}

	// Token: 0x06007015 RID: 28693 RVA: 0x002AA0D0 File Offset: 0x002A82D0
	private void UpdateMaxCapacityLabel()
	{
		this.numberInput.SetDisplayValue(this.target.UserMaxCapacity.ToString());
	}

	// Token: 0x04004D1A RID: 19738
	private IUserControlledCapacity target;

	// Token: 0x04004D1B RID: 19739
	[Header("Slider")]
	[SerializeField]
	private KSlider slider;

	// Token: 0x04004D1C RID: 19740
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04004D1D RID: 19741
	[SerializeField]
	private LocText unitsLabel;
}
