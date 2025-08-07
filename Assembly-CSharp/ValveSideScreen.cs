using System;
using System.Collections;
using STRINGS;
using UnityEngine;

// Token: 0x02000E47 RID: 3655
public class ValveSideScreen : SideScreenContent
{
	// Token: 0x06007423 RID: 29731 RVA: 0x002C16FC File Offset: 0x002BF8FC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.unitsLabel.text = GameUtil.AddTimeSliceText(UI.UNITSUFFIXES.MASS.GRAM, GameUtil.TimeSlice.PerSecond);
		this.flowSlider.onReleaseHandle += this.OnReleaseHandle;
		this.flowSlider.onDrag += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
		};
		this.flowSlider.onPointerDown += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
		};
		this.flowSlider.onMove += delegate
		{
			this.ReceiveValueFromSlider(this.flowSlider.value);
			this.OnReleaseHandle();
		};
		this.numberInput.onEndEdit += delegate
		{
			this.ReceiveValueFromInput(this.numberInput.currentValue);
		};
		this.numberInput.decimalPlaces = 1;
	}

	// Token: 0x06007424 RID: 29732 RVA: 0x002C17A9 File Offset: 0x002BF9A9
	public void OnReleaseHandle()
	{
		this.targetValve.ChangeFlow(this.targetFlow);
	}

	// Token: 0x06007425 RID: 29733 RVA: 0x002C17BC File Offset: 0x002BF9BC
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Valve>() != null;
	}

	// Token: 0x06007426 RID: 29734 RVA: 0x002C17CC File Offset: 0x002BF9CC
	public override void SetTarget(GameObject target)
	{
		this.targetValve = target.GetComponent<Valve>();
		if (this.targetValve == null)
		{
			global::Debug.LogError("The target object does not have a Valve component.");
			return;
		}
		this.flowSlider.minValue = 0f;
		this.flowSlider.maxValue = this.targetValve.MaxFlow;
		this.flowSlider.value = this.targetValve.DesiredFlow;
		this.minFlowLabel.text = GameUtil.GetFormattedMass(0f, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}");
		this.maxFlowLabel.text = GameUtil.GetFormattedMass(this.targetValve.MaxFlow, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, true, "{0:0.#}");
		this.numberInput.minValue = 0f;
		this.numberInput.maxValue = this.targetValve.MaxFlow * 1000f;
		this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(Mathf.Max(0f, this.targetValve.DesiredFlow), GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, false, "{0:0.#####}"));
		this.numberInput.Activate();
	}

	// Token: 0x06007427 RID: 29735 RVA: 0x002C18DE File Offset: 0x002BFADE
	private void ReceiveValueFromSlider(float newValue)
	{
		newValue = Mathf.Round(newValue * 1000f) / 1000f;
		this.UpdateFlowValue(newValue);
	}

	// Token: 0x06007428 RID: 29736 RVA: 0x002C18FC File Offset: 0x002BFAFC
	private void ReceiveValueFromInput(float input)
	{
		float num = input / 1000f;
		this.UpdateFlowValue(num);
		this.targetValve.ChangeFlow(this.targetFlow);
	}

	// Token: 0x06007429 RID: 29737 RVA: 0x002C1929 File Offset: 0x002BFB29
	private void UpdateFlowValue(float newValue)
	{
		this.targetFlow = newValue;
		this.flowSlider.value = newValue;
		this.numberInput.SetDisplayValue(GameUtil.GetFormattedMass(newValue, GameUtil.TimeSlice.PerSecond, GameUtil.MetricMassFormat.Gram, false, "{0:0.#####}"));
	}

	// Token: 0x0600742A RID: 29738 RVA: 0x002C1957 File Offset: 0x002BFB57
	private IEnumerator SettingDelay(float delay)
	{
		float startTime = Time.realtimeSinceStartup;
		float currentTime = startTime;
		while (currentTime < startTime + delay)
		{
			currentTime += Time.unscaledDeltaTime;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.OnReleaseHandle();
		yield break;
	}

	// Token: 0x04004FF1 RID: 20465
	private Valve targetValve;

	// Token: 0x04004FF2 RID: 20466
	[Header("Slider")]
	[SerializeField]
	private KSlider flowSlider;

	// Token: 0x04004FF3 RID: 20467
	[SerializeField]
	private LocText minFlowLabel;

	// Token: 0x04004FF4 RID: 20468
	[SerializeField]
	private LocText maxFlowLabel;

	// Token: 0x04004FF5 RID: 20469
	[Header("Input Field")]
	[SerializeField]
	private KNumberInputField numberInput;

	// Token: 0x04004FF6 RID: 20470
	[SerializeField]
	private LocText unitsLabel;

	// Token: 0x04004FF7 RID: 20471
	private float targetFlow;
}
