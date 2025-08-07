using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000C3C RID: 3132
public class TemperatureOverlayThresholdAdjustmentWidget : KMonoBehaviour
{
	// Token: 0x06005F55 RID: 24405 RVA: 0x00230AB5 File Offset: 0x0022ECB5
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.scrollbar.onValueChanged.AddListener(new UnityAction<float>(this.OnValueChanged));
	}

	// Token: 0x06005F56 RID: 24406 RVA: 0x00230ADC File Offset: 0x0022ECDC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.scrollbar.size = TemperatureOverlayThresholdAdjustmentWidget.temperatureWindowSize / TemperatureOverlayThresholdAdjustmentWidget.maxTemperatureRange;
		this.scrollbar.value = this.KelvinToScrollPercentage(SaveGame.Instance.relativeTemperatureOverlaySliderValue);
		this.defaultButton.onClick += this.OnDefaultPressed;
	}

	// Token: 0x06005F57 RID: 24407 RVA: 0x00230B37 File Offset: 0x0022ED37
	private void OnValueChanged(float data)
	{
		this.SetUserConfig(data);
	}

	// Token: 0x06005F58 RID: 24408 RVA: 0x00230B40 File Offset: 0x0022ED40
	private float KelvinToScrollPercentage(float kelvin)
	{
		kelvin -= TemperatureOverlayThresholdAdjustmentWidget.minimumSelectionTemperature;
		if (kelvin < 1f)
		{
			kelvin = 1f;
		}
		return Mathf.Clamp01(kelvin / TemperatureOverlayThresholdAdjustmentWidget.maxTemperatureRange);
	}

	// Token: 0x06005F59 RID: 24409 RVA: 0x00230B68 File Offset: 0x0022ED68
	private void SetUserConfig(float scrollPercentage)
	{
		float num = TemperatureOverlayThresholdAdjustmentWidget.minimumSelectionTemperature + TemperatureOverlayThresholdAdjustmentWidget.maxTemperatureRange * scrollPercentage;
		float num2 = num - TemperatureOverlayThresholdAdjustmentWidget.temperatureWindowSize / 2f;
		float num3 = num + TemperatureOverlayThresholdAdjustmentWidget.temperatureWindowSize / 2f;
		SimDebugView.Instance.user_temperatureThresholds[0] = num2;
		SimDebugView.Instance.user_temperatureThresholds[1] = num3;
		this.scrollBarRangeCenterText.SetText(GameUtil.GetFormattedTemperature(num, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, true));
		this.scrollBarRangeLowText.SetText(GameUtil.GetFormattedTemperature((float)Mathf.RoundToInt(num2), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, true));
		this.scrollBarRangeHighText.SetText(GameUtil.GetFormattedTemperature((float)Mathf.RoundToInt(num3), GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, true));
		SaveGame.Instance.relativeTemperatureOverlaySliderValue = num;
	}

	// Token: 0x06005F5A RID: 24410 RVA: 0x00230C17 File Offset: 0x0022EE17
	private void OnDefaultPressed()
	{
		this.scrollbar.value = this.KelvinToScrollPercentage(294.15f);
	}

	// Token: 0x04003F91 RID: 16273
	public const float DEFAULT_TEMPERATURE = 294.15f;

	// Token: 0x04003F92 RID: 16274
	[SerializeField]
	private Scrollbar scrollbar;

	// Token: 0x04003F93 RID: 16275
	[SerializeField]
	private LocText scrollBarRangeLowText;

	// Token: 0x04003F94 RID: 16276
	[SerializeField]
	private LocText scrollBarRangeCenterText;

	// Token: 0x04003F95 RID: 16277
	[SerializeField]
	private LocText scrollBarRangeHighText;

	// Token: 0x04003F96 RID: 16278
	[SerializeField]
	private KButton defaultButton;

	// Token: 0x04003F97 RID: 16279
	private static float maxTemperatureRange = 700f;

	// Token: 0x04003F98 RID: 16280
	private static float temperatureWindowSize = 200f;

	// Token: 0x04003F99 RID: 16281
	private static float minimumSelectionTemperature = TemperatureOverlayThresholdAdjustmentWidget.temperatureWindowSize / 2f;
}
