using System;
using UnityEngine;

// Token: 0x0200052D RID: 1325
public class KBatchedAnimHeatPostProcessingEffect : KMonoBehaviour
{
	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0009DDD0 File Offset: 0x0009BFD0
	public float HeatProduction
	{
		get
		{
			return this.heatProduction;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0009DDD8 File Offset: 0x0009BFD8
	public bool IsHeatProductionEnoughToShowEffect
	{
		get
		{
			return this.HeatProduction >= 1f;
		}
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x0009DDEA File Offset: 0x0009BFEA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.animController.postProcessingEffectsAllowed |= KAnimConverter.PostProcessingEffects.TemperatureOverlay;
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x0009DE05 File Offset: 0x0009C005
	public void SetHeatBeingProducedValue(float heat)
	{
		this.heatProduction = heat;
		this.RefreshEffectVisualState();
	}

	// Token: 0x06001D33 RID: 7475 RVA: 0x0009DE14 File Offset: 0x0009C014
	public void RefreshEffectVisualState()
	{
		if (base.enabled && this.IsHeatProductionEnoughToShowEffect)
		{
			this.SetParameterValue(1f);
			return;
		}
		this.SetParameterValue(0f);
	}

	// Token: 0x06001D34 RID: 7476 RVA: 0x0009DE3D File Offset: 0x0009C03D
	private void SetParameterValue(float value)
	{
		if (this.animController != null)
		{
			this.animController.postProcessingParameters = value;
		}
	}

	// Token: 0x06001D35 RID: 7477 RVA: 0x0009DE59 File Offset: 0x0009C059
	protected override void OnCmpEnable()
	{
		this.RefreshEffectVisualState();
	}

	// Token: 0x06001D36 RID: 7478 RVA: 0x0009DE61 File Offset: 0x0009C061
	protected override void OnCmpDisable()
	{
		this.RefreshEffectVisualState();
	}

	// Token: 0x06001D37 RID: 7479 RVA: 0x0009DE6C File Offset: 0x0009C06C
	private void Update()
	{
		int num = Mathf.FloorToInt(Time.timeSinceLevelLoad / 1f);
		if (num != this.loopsPlayed)
		{
			this.loopsPlayed = num;
			this.OnNewLoopReached();
		}
	}

	// Token: 0x06001D38 RID: 7480 RVA: 0x0009DEA0 File Offset: 0x0009C0A0
	private void OnNewLoopReached()
	{
		if (OverlayScreen.Instance != null && OverlayScreen.Instance.mode == OverlayModes.Temperature.ID && this.IsHeatProductionEnoughToShowEffect)
		{
			Vector3 position = base.transform.GetPosition();
			string sound = GlobalAssets.GetSound("Temperature_Heat_Emission", false);
			position.z = 0f;
			SoundEvent.EndOneShot(SoundEvent.BeginOneShot(sound, position, 1f, false));
		}
	}

	// Token: 0x040010FE RID: 4350
	public const float SHOW_EFFECT_HEAT_TRESHOLD = 1f;

	// Token: 0x040010FF RID: 4351
	private const float DISABLING_VALUE = 0f;

	// Token: 0x04001100 RID: 4352
	private const float ENABLING_VALUE = 1f;

	// Token: 0x04001101 RID: 4353
	private float heatProduction;

	// Token: 0x04001102 RID: 4354
	public const float ANIM_DURATION = 1f;

	// Token: 0x04001103 RID: 4355
	private int loopsPlayed;

	// Token: 0x04001104 RID: 4356
	[MyCmpGet]
	private KBatchedAnimController animController;
}
