using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000E1E RID: 3614
public class RailGunSideScreen : SideScreenContent
{
	// Token: 0x0600723A RID: 29242 RVA: 0x002B6BE8 File Offset: 0x002B4DE8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = GameUtil.GetCurrentMassUnit(false);
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

	// Token: 0x0600723B RID: 29243 RVA: 0x002B6C79 File Offset: 0x002B4E79
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.selectedGun)
		{
			this.selectedGun = null;
		}
	}

	// Token: 0x0600723C RID: 29244 RVA: 0x002B6C95 File Offset: 0x002B4E95
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.selectedGun)
		{
			this.selectedGun = null;
		}
	}

	// Token: 0x0600723D RID: 29245 RVA: 0x002B6CB1 File Offset: 0x002B4EB1
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<RailGun>() != null;
	}

	// Token: 0x0600723E RID: 29246 RVA: 0x002B6CC0 File Offset: 0x002B4EC0
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.selectedGun = new_target.GetComponent<RailGun>();
		if (this.selectedGun == null)
		{
			global::Debug.LogError("The gameObject received does not contain a RailGun component");
			return;
		}
		this.targetRailgunHEPStorageSubHandle = this.selectedGun.Subscribe(-1837862626, new Action<object>(this.UpdateHEPLabels));
		this.slider.minValue = this.selectedGun.MinLaunchMass;
		this.slider.maxValue = this.selectedGun.MaxLaunchMass;
		this.slider.value = this.selectedGun.launchMass;
		this.unitsLabel.text = GameUtil.GetCurrentMassUnit(false);
		this.numberInput.minValue = this.selectedGun.MinLaunchMass;
		this.numberInput.maxValue = this.selectedGun.MaxLaunchMass;
		this.numberInput.currentValue = Mathf.Max(this.selectedGun.MinLaunchMass, Mathf.Min(this.selectedGun.MaxLaunchMass, this.selectedGun.launchMass));
		this.UpdateMaxCapacityLabel();
		this.numberInput.Activate();
		this.UpdateHEPLabels(null);
	}

	// Token: 0x0600723F RID: 29247 RVA: 0x002B6DFA File Offset: 0x002B4FFA
	public override void ClearTarget()
	{
		if (this.targetRailgunHEPStorageSubHandle != -1 && this.selectedGun != null)
		{
			this.selectedGun.Unsubscribe(this.targetRailgunHEPStorageSubHandle);
			this.targetRailgunHEPStorageSubHandle = -1;
		}
		this.selectedGun = null;
	}

	// Token: 0x06007240 RID: 29248 RVA: 0x002B6E34 File Offset: 0x002B5034
	public void UpdateHEPLabels(object data = null)
	{
		if (this.selectedGun == null)
		{
			return;
		}
		string text = BUILDINGS.PREFABS.RAILGUN.SIDESCREEN_HEP_REQUIRED;
		text = text.Replace("{current}", this.selectedGun.CurrentEnergy.ToString());
		text = text.Replace("{required}", this.selectedGun.EnergyCost.ToString());
		this.hepStorageInfo.text = text;
	}

	// Token: 0x06007241 RID: 29249 RVA: 0x002B6EA5 File Offset: 0x002B50A5
	private void ReceiveValueFromSlider(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06007242 RID: 29250 RVA: 0x002B6EAE File Offset: 0x002B50AE
	private void ReceiveValueFromInput(float newValue)
	{
		this.UpdateMaxCapacity(newValue);
	}

	// Token: 0x06007243 RID: 29251 RVA: 0x002B6EB7 File Offset: 0x002B50B7
	private void UpdateMaxCapacity(float newValue)
	{
		this.selectedGun.launchMass = newValue;
		this.slider.value = newValue;
		this.UpdateMaxCapacityLabel();
		this.selectedGun.Trigger(161772031, null);
	}

	// Token: 0x06007244 RID: 29252 RVA: 0x002B6EE8 File Offset: 0x002B50E8
	private void UpdateMaxCapacityLabel()
	{
		this.numberInput.SetDisplayValue(this.selectedGun.launchMass.ToString());
	}

	// Token: 0x04004E9D RID: 20125
	public GameObject content;

	// Token: 0x04004E9E RID: 20126
	private RailGun selectedGun;

	// Token: 0x04004E9F RID: 20127
	public LocText DescriptionText;

	// Token: 0x04004EA0 RID: 20128
	[Header("Slider")]
	[SerializeField]
	private KSlider slider;

	// Token: 0x04004EA1 RID: 20129
	[Header("Number Input")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04004EA2 RID: 20130
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x04004EA3 RID: 20131
	[SerializeField]
	private LocText hepStorageInfo;

	// Token: 0x04004EA4 RID: 20132
	private int targetRailgunHEPStorageSubHandle = -1;
}
